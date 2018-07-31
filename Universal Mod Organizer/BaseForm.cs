#region License

// ====================================================
// Universal Mod Organizer by ARZUMATA.
// 
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// 
// ====================================================

#endregion

using BrightIdeasSoftware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.VisualBasic;

namespace Universal_Mod_Organizer
{
    public partial class BaseForm : Form
    {
        // This is used for automatic column resizing.
        private int defaultFormWidth;

        // XML Files handling.
        private XDocument doc;

        // Selected Game, Stellaris by default
        private string currentGame = "Stellaris";

        // Selected Profile, Default by default :D
        private string currentProfile = "Default";

        // Collection that stores game, profiles, and profile data
        private SortedDictionary<string, SortedDictionary<string, List<string>>> globalProfiles = new SortedDictionary<string, SortedDictionary<string, List<string>>>();

        // Various Regex
        private Regex regexGetOnlyLineName = new Regex("^#|=\".+$");
        private Regex regexGetOnlyLineData = new Regex("^(#|[a-z]+|_+)+=\"|\"$");

        // User made changes TODO
        private bool unsavedChanges = false;

        public BaseForm()
        {
            InitializeComponent();
            defaultFormWidth = Width - columnName.Width + columnName.MinimumWidth;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void BaseFormLoad(object sender, EventArgs e)
        {
            SettingsLoad();
            InitializeModListView();
        }

        private void ResizeDone(object sender, EventArgs e)
        {
            // Adjust mod name columns on resizing.
            var difference = Width - defaultFormWidth;
            columnName.Width = columnName.MinimumWidth + difference;
        }

        private void SettingsLoad()
        {
            doc = XDocument.Load(@"dataset.xml");

            // Restore window size
            Width = Convert.ToInt32(Convert.ToString(doc.Root.Elements("Settings").Elements("FormWidth").Select(x => x.Value).SingleOrDefault()) ?? Width.ToString());
            Height = Convert.ToInt32(Convert.ToString(doc.Root.Elements("Settings").Elements("FormHeight").Select(x => x.Value).SingleOrDefault()) ?? Height.ToString());

            // Populate globalProfiles Dictionary Data for ALL Games.
            // Get Games nodes.
            foreach (var element in doc.Root.Elements("Games"))
            {
                // Parse each Game node
                foreach (var gameEntry in element.Elements())
                {
                    // Add game
                    globalProfiles.Add(gameEntry.Attribute("Name").Value, new SortedDictionary<string, List<string>>());

                    foreach (var profileEntry in gameEntry.Elements())
                    {
                        // Generate enabled mods list to add to dict.
                        var enabledModsList = new List<string>();

                        foreach (var modEntry in profileEntry.Elements())
                        {
                            enabledModsList.Add(modEntry.Attribute("Path").Value);
                        }

                        globalProfiles[gameEntry.Attribute("Name").Value].Add(profileEntry.Attribute("Name").Value, enabledModsList);
                    }
                }
            }

            // Updating UI for current or last remembered game.

            // Restore last selected game.
            currentGame = doc.Root.Elements("Settings").Elements("LastGame").Select(x => x.Value).SingleOrDefault() ?? currentGame;
            textBoxGame.Text = currentGame;
            SelectGameFromSettings();

            // Restore last selected profile.
            currentProfile = doc.Root.Elements("Settings").Elements("LastProfile").Select(x => x.Value).SingleOrDefault() ?? currentProfile;
            SetComboBoxActiveProfile();
        }

        private void SettingsSave()
        {
            var docText = new XDocument();
            XElement root = new XElement("Data");
            docText.Add(root);

            // Settings tree.
            var settingsRoot = new XElement("Settings");
            root.Add(settingsRoot);

            // Remember window size
            var settingsEntry = new XElement("FormWidth", Width);
            settingsRoot.Add(settingsEntry);
            settingsEntry = new XElement("FormHeight", Height);
            settingsRoot.Add(settingsEntry);

            // Remember last selected game
            settingsEntry = new XElement("LastGame", currentGame);
            settingsRoot.Add(settingsEntry);

            // Remember last selected profile
            settingsEntry = new XElement("LastProfile", comboBoxProfile.SelectedValue.ToString());
            settingsRoot.Add(settingsEntry);

            // Update current selected profile with new changes from list view.

            // We sort it by order as it is important so save correctly.
            Helper.ModListForListView.Sort((x, y) => x.Order.CompareTo(y.Order));

            // Temporary enableModsList that we populate with data
            var enabledModsList = new List<string>();

            // Put only active mods there.
            for (int i = 0; i < Helper.ModListForListView.Count; i++)
            {
                if (Helper.ModListForListView[i].Enabled == Helper.SymbolYes)
                {
                    enabledModsList.Add(Helper.ModListForListView[i].Filename);
                }
            }

            // Replace activegame activeprofile with new data, while keeping the rest untouched.
            foreach (var dictItem in globalProfiles[currentGame])
            {
                if (dictItem.Key.Equals(currentProfile))
                {
                    dictItem.Value.Clear();
                    dictItem.Value.AddRange(enabledModsList);
                }
            }

            // <Games> Entry
            XElement games = new XElement("Games");
            root.Add(games);

            // Add Game to XML
            foreach (var dictItemGame in globalProfiles)
            {
                XElement elementGame = new XElement("Game", new XAttribute("Name", dictItemGame.Key));
                games.Add(elementGame);

                foreach (var dictItemProfile in globalProfiles[dictItemGame.Key])
                {
                    XElement elementProfile = new XElement("Profile", new XAttribute("Name", dictItemProfile.Key));
                    elementGame.Add(elementProfile);

                    foreach (var dictItemMod in dictItemProfile.Value)
                    {
                        XElement elementMods = new XElement("Mod", new XAttribute("Path", dictItemMod));
                        elementProfile.Add(elementMods);
                    }
                }
            }

            docText.Save(@"dataset.xml");
            WriteDataToGameSettingsFile(enabledModsList);
        }

        private void ProfileAddSelect(object sender, EventArgs e)
        {
            string newProfile = Interaction.InputBox("Enter profile name:", "Add profile", "new profile", Location.X + (Width / 3), Location.Y + (Height / 3));

            if (string.IsNullOrEmpty(newProfile))
            {
                MessageBox.Show("Can not add.\nEnter valid profile name please.");
                return;
            }

            var index = comboBoxProfile.FindString(newProfile);

            // We did not find any profiles with that name.
            if (index < 0)
            {
                // Add to global profiles under game name with empty list.
                globalProfiles[currentGame].Add(newProfile, new List<string>());
                currentProfile = newProfile;
                SetComboBoxActiveProfile();
                ParseModsList();
            }

            // Profile already exists
            if (index >= 0)
            {
                MessageBox.Show("Profile with name \'" + newProfile + "\' already exists!");
            }
        }

        private void ProfileDeleteSelect(object sender, EventArgs e)
        {
            if (comboBoxProfile.SelectedItem.Equals("Default"))
            {
                MessageBox.Show("Can not delete Default profile.");
                return;
            }

            if (MessageBox.Show("Delete '" + comboBoxProfile.SelectedItem + "' profile?", string.Empty, MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            // Remove the dictionary key with the profile and mod list inside
            globalProfiles[currentGame].Remove(comboBoxProfile.SelectedValue.ToString());
            currentProfile = "Default";
            SetComboBoxActiveProfile();
            ParseModsList();
        }

        private void ProfileImportSelect(object sender, EventArgs e)
        {
            OpenFileDialog importProfileDialog = new OpenFileDialog();
            importProfileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            importProfileDialog.Title = "Select File";
            importProfileDialog.DefaultExt = "xml";
            importProfileDialog.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
            importProfileDialog.FileName = currentProfile + ".xml";
            importProfileDialog.CheckPathExists = true;
            if (importProfileDialog.ShowDialog() == DialogResult.OK)
            {
                var imprortProfileDoc = XDocument.Load(importProfileDialog.FileName);

                foreach (var gameEntry in imprortProfileDoc.Elements())
                {
                    var gameName = gameEntry.Attribute("Name").Value;

                    foreach (var profileEntry in gameEntry.Elements())
                    {
                        var profileName = profileEntry.Attribute("Name").Value;

                        // Generate enabled mods list to add to dict.
                        var enabledModsList = new List<string>();

                        foreach (var modEntry in profileEntry.Elements())
                        {
                            enabledModsList.Add(modEntry.Attribute("Path").Value);
                        }

                        if (globalProfiles[gameName].ContainsKey(profileName))
                        {
                            profileName = Interaction.InputBox("Profile already exists! Enter new profile name:", "Add profile", profileName + " - changeme", Location.X + (Width / 3), Location.Y + (Height / 3));
                        }

                        if (string.IsNullOrEmpty(profileName))
                        {
                            return;
                        }
                        else
                        {
                            globalProfiles[gameName].Add(profileName, enabledModsList);

                            // Check if our active game is same as in the imported profile. If not, just add but don`t activate.
                            if (gameName.Equals(currentGame))
                            {
                                // Activate profile.
                                currentProfile = profileName;
                                SetComboBoxActiveProfile();
                                ParseModsList();
                            }
                        }
                    }
                }
            }
        }

        private void ProfileExportSelect(object sender, EventArgs e)
        {
            SaveFileDialog exportProfileDialog = new SaveFileDialog();
            exportProfileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            exportProfileDialog.Title = "Select File";
            exportProfileDialog.DefaultExt = "xml";
            exportProfileDialog.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
            exportProfileDialog.FileName = currentProfile + ".xml";
            exportProfileDialog.CheckPathExists = true;

            if (exportProfileDialog.ShowDialog() == DialogResult.OK)
            {
                // Save the file.
                var docText = new XDocument();
                XElement elementGame = new XElement("Game", new XAttribute("Name", currentGame));
                docText.Add(elementGame);

                foreach (var dictItemProfile in globalProfiles[currentGame])
                {
                    if (dictItemProfile.Key.Equals(currentProfile))
                    {
                        XElement elementProfile = new XElement("Profile", new XAttribute("Name", dictItemProfile.Key));
                        elementGame.Add(elementProfile);

                        foreach (var dictItemMod in dictItemProfile.Value)
                        {
                            XElement elementMods = new XElement("Mod", new XAttribute("Path", dictItemMod));
                            elementProfile.Add(elementMods);
                        }
                    }
                }

                docText.Save(exportProfileDialog.FileName);
            }
        }

        private void MenuGame_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in ((ToolStripMenuItem)sender).GetCurrentParent().Items)
            {
                if (item == sender)
                {
                    item.Checked = true;
                    currentGame = item.Text;
                    textBoxGame.Text = currentGame;
                }

                if ((item != null) && (item != sender))
                {
                    item.Checked = false;
                }
            }

            currentProfile = "Default";
            PopulateModsList();
            SetComboBoxActiveProfile();
            ParseModsList();
        }

        private void SelectGameFromSettings()
        {
            foreach (ToolStripMenuItem item in MenuGames.DropDownItems)
            {
                if (item.Text.Equals(currentGame))
                {
                    item.Checked = true;
                }
            }
        }

        private void SetComboBoxActiveProfile()
        {
            comboBoxProfile.DataSource = globalProfiles[currentGame].Keys.ToArray();

            for (int i = 0; i < comboBoxProfile.Items.Count; i++)
            {
                int index = comboBoxProfile.FindString(currentProfile);
                comboBoxProfile.SelectedIndex = index;
            }
        }

        private void ApplySettings(object sender, EventArgs e)
        {
            SettingsSave();
        }

        private void WriteDataToModFile(ModsList item, string gameFolder)
        {
            var fileName = gameFolder + item.Filename;

            string[] allFileLines = File.ReadAllLines(fileName);

            // Deleting the file
            File.Delete(fileName);

            using (StreamWriter sw = File.AppendText(fileName))
            {
                var regexMatchLineName = new Regex("^name=.*$");
                var regexMatchLineNameOriginal = new Regex("^#original_name=.*$");

                foreach (string line in allFileLines)
                {
                    Match matchName = regexMatchLineName.Match(line);
                    Match matchOriginalName = regexMatchLineNameOriginal.Match(line);

                    if (matchName.Success)
                    {
                        // Write Name with Order and Original Name Commented
                        if (item.Enabled.Equals(Helper.SymbolYes))
                        {
                            sw.WriteLine("name=\"" + item.Order + " " + item.Name + "\"");
                        }
                        else
                        {
                            sw.WriteLine("name=\"" + item.Name + "\"");
                        }

                        sw.WriteLine("#original_name=\"" + item.Name + "\"");
                    }
                    else if (matchOriginalName.Success)
                    {
                        // We have found another one original name, skip.
                    }
                    else
                    {
                        sw.WriteLine(line);
                    }
                }
            }
        }

        private void WriteDataToGameSettingsFile(List<string> enabledModsList)
        {
            // For all games.
            var gameFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Paradox Interactive\" + currentGame + @"\";
            var settingsFile = gameFolder + @"settings.txt";
            var settingsFileBackup = gameFolder + @"settings.bak";

            Regex regexDeleteModLine = new Regex("^\t\"mod.+\"$");

            // Edit game file. settings.txt.

            // Making backup
            System.IO.File.Copy(settingsFile, settingsFileBackup, true);

            // Read file line by line.
            string[] allFileLines = File.ReadAllLines(settingsFile);

            // Deleting the file.
            File.Delete(settingsFile);

            using (StreamWriter sw = File.AppendText(settingsFile))
            {
                foreach (string line in allFileLines)
                {
                    // If we found that line, go to cycle and add our mods there
                    if (line.Contains("last_mods="))
                    {
                        // Different logic for CK2, will be case switch if there are more.
                        if (currentGame.Equals("Crusader Kings II"))
                        {
                            StringBuilder oneBigLine = new StringBuilder();

                            oneBigLine.Append("last_mods={");

                            // Cycle through list and write enabled mods to settings file
                            foreach (var item in enabledModsList)
                            {
                                oneBigLine.Append("\"" + item.Replace("\\", "/") + "\" ");
                            }

                            // Remove last space :) OCD...
                            oneBigLine.Remove(oneBigLine.Length - 1, 1);

                            // Enclosing bracket
                            oneBigLine.Append("}");

                            sw.WriteLine(oneBigLine);
                        }
                        else
                        {
                            // Write that line
                            sw.WriteLine("last_mods={");

                            // Cycle through list and write enabled mods to settings file
                            foreach (var item in enabledModsList)
                            {
                                sw.WriteLine("\t\"" + item.Replace("\\", "/") + "\"");
                            }
                        }

                        // Also edit the .mod file as we are changing the mod name
                        foreach (var item in Helper.ModListForListView)
                        {
                            WriteDataToModFile(item, gameFolder);
                        }

                        // Skip last_mods line.
                        continue;
                    }

                    // If there are old lines with mod entries. Just ignore then and do not write them to file. Works with CK2.
                    Match match = regexDeleteModLine.Match(line);

                    if (!match.Success)
                    {
                        sw.WriteLine(line);
                    }
                }
            }
        }

        private void ProfileSelectedByUser(object sender, EventArgs e)
        {
            // When new profile selected clean any
            currentProfile = comboBoxProfile.SelectedValue.ToString();
            ParseModsList();
        }
    }
}
