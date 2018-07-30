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

namespace Universal_Mod_Organizer
{
    public partial class BaseForm : Form
    {
        // This is used for automatic column resizing.
        private int defaultFormWidth;

        // XML Files handling.
        private XDocument doc;

        // Selected Game, Stellaris by default
        private string activeGame = "Stellaris";

        // Selected Profile, -Default- by default :D
        private string activeProfile = "-Default-";

        // Collection that stores game, profiles, and profile data
        private Dictionary<string, Dictionary<string, List<string>>> globalProfiles = new Dictionary<string, Dictionary<string, List<string>>>();

        // Various Regex
        private Regex regexGetOnlyLineName = new Regex("^#|=\".+$");
        private Regex regexGetOnlyLineData = new Regex("^(#|[a-z]+|_+)+=\"|\"$");

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

        private void SettingsSave()
        {
            var docText = new XDocument();
            XElement root = new XElement("Data");
            docText.Add(root);

            // Remember window size
            var settingsEntry = new XElement("Settings", new XElement("FormWidth", Width), new XElement("FormHeight", Height));
            root.Add(settingsEntry);

            // Remember last selected game
            settingsEntry = new XElement("LastGame", activeGame);
            root.Add(settingsEntry);

            // Remember last selected profile
            settingsEntry = new XElement("LastProfile", comboBoxProfile.SelectedItem);
            root.Add(settingsEntry);

            // TODO: must not clear it
            globalProfiles.Clear();

            // Take the mod list for list view
            if (Helper.ModListForListView.Count > 0)
            {
                // We sort it by order as it is important so save correctly.
                Helper.ModListForListView.Sort((x, y) => x.Order.CompareTo(y.Order));

                // Temporary enableModsList that we populate with data
                // TODO: rework once you got multiple profiles
                var enabledModsList = new List<string>();

                // Put only active mods there.
                for (int i = 0; i < Helper.ModListForListView.Count; i++)
                {
                    if (Helper.ModListForListView[i].Enabled == Helper.SymbolYes)
                    {
                        enabledModsList.Add(Helper.ModListForListView[i].Filename);
                    }
                }

                // <Games> Entry
                XElement games = new XElement("Games");
                root.Add(games);

                // Add games with their dictionaries.
                foreach (ToolStripMenuItem menuItem in MenuGames.DropDownItems)
                {
                    // Add game
                    globalProfiles.Add(menuItem.Text, new Dictionary<string, List<string>>());

                    // Add profile for that game. TODO: Multiple profiles.
                    globalProfiles[menuItem.Text].Add(activeProfile, enabledModsList);

                    // XML
                    XElement game = new XElement("Game", new XAttribute("Name", menuItem.Text));
                    games.Add(game);

                    XElement element = new XElement("Profile", new XAttribute("Name", activeProfile));

                    foreach (var dictItem in globalProfiles)
                    {
                        foreach (var item2 in dictItem.Value)
                        {
                            foreach (var item3 in item2.Value)
                            {
                                XElement elementMods = new XElement("Mod", new XAttribute("Path", item3));
                                element.Add(elementMods);
                            }
                        }
                    }

                    game.Add(element);
                }

                docText.Save(@"dataset.xml");
                WriteDataToGameSettingsFile(enabledModsList);
            }
        }

        private void SettingsLoad()
        {
            doc = XDocument.Load(@"dataset.xml");

            // Restore window size
            Width = Convert.ToInt32(Convert.ToString(doc.Root.Elements("Settings").Elements("FormWidth").Select(x => x.Value).SingleOrDefault()) ?? Width.ToString());
            Height = Convert.ToInt32(Convert.ToString(doc.Root.Elements("Settings").Elements("FormHeight").Select(x => x.Value).SingleOrDefault()) ?? Height.ToString());

            // Restore last selected game
            activeGame = doc.Root.Elements("Settings").Elements("LastGame").Select(x => x.Value).SingleOrDefault() ?? activeGame;
            SelectGameFromSettings();

            // Restore last selected profile
            activeProfile = doc.Root.Elements("Settings").Elements("LastProfile").Select(x => x.Value).SingleOrDefault() ?? activeProfile;
            SelectProfileFromSettings();

            // Get Games nodes
            foreach (var element in doc.Root.Elements("Games"))
            {
                // Parse each Game node
                foreach (var gameElement in element.Elements())
                {
                    // Add game
                    globalProfiles.Add(gameElement.Attribute("Name").Value, new Dictionary<string, List<string>>());

                    foreach (var profileElement in gameElement.Elements())
                    {
                        // Generate enabled mods list to add to dict.
                        var enabledModsList = new List<string>();

                        foreach (var modElement in profileElement.Elements())
                        {
                            enabledModsList.Add(modElement.Attribute("Path").Value);
                        }

                        // Add profile for that game. TODO: Multiple profiles.
                        globalProfiles[gameElement.Attribute("Name").Value].Add(profileElement.Attribute("Name").Value, enabledModsList);
                    }
                }
            }
        }

        private void ProfileAdd_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Will be available in future version");
        }

        private void ProfileDelete_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Will be available in future version");
        }

        private void ProfileImport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Will be available in future version");
        }

        private void ProfileExport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Will be available in future version");
        }

        private void MenuGame_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in ((ToolStripMenuItem)sender).GetCurrentParent().Items)
            {
                if (item == sender)
                {
                    item.Checked = true;
                    activeGame = item.Text;
                }

                if ((item != null) && (item != sender))
                {
                    item.Checked = false;
                }
            }

            MessageBox.Show("CK2 not fully implemented. Doesn`t work.");
        }

        private void SelectGameFromSettings()
        {
            foreach (ToolStripMenuItem item in MenuGames.DropDownItems)
            {
                if (item.Text.Equals(activeGame))
                {
                    item.Checked = true;
                }
            }
        }

        private void SelectProfileFromSettings()
        {
            for (int i = 0; i < comboBoxProfile.Items.Count; i++)
            {
                int index = comboBoxProfile.FindString(activeProfile);
                comboBoxProfile.SelectedIndex = index;
            }
        }

        private void ApplySettings(object sender, EventArgs e)
        {
            SettingsSave();
        }

        private void WriteDataToModFile(ModsList item, string gameFolder)
        {
            if (item.Enabled.Equals(Helper.SymbolYes))
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

                        // We always find one. TODO Rework?
                        if (matchName.Success)
                        {
                            // Write Name with Order and Original Name Commented
                            sw.WriteLine("name=\"" + item.Order + " " + item.Name + "\"");
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
        }

        private void WriteDataToGameSettingsFile(List<string> enabledModsList)
        {
            // Edit game file. settings.txt.
            if (activeGame.Equals("Stellaris"))
            {
                var gameFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Paradox Interactive\" + activeGame + @"\";
                var settingsFile = gameFolder + @"settings.txt";
                var settingsFileBackup = gameFolder + @"settings.bak";

                var regexDeleteModLine = new Regex("^\t\"mod.+\"$");

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
                        if (line.Contains("last_mods"))
                        {
                            sw.WriteLine(line);

                            // Cycle through list and write enabled mods to settings file
                            foreach (var item in enabledModsList)
                            {
                                sw.WriteLine("\t\"" + item.Replace("\\", "/") + "\"");
                            }

                            // Also edit the file as we are changing the mod name
                            foreach (var item in Helper.ModListForListView)
                            {
                                WriteDataToModFile(item, gameFolder);
                            }

                            continue;
                        }

                        // If there are old lines, just ignore then and do not write them to file.
                        Match match = regexDeleteModLine.Match(line);

                        if (match.Success)
                        {
                            // Write original line and the value.
                        }
                        else
                        {
                            // Rest of the config.
                            sw.WriteLine(line);
                        }
                    }
                }
            }
        }
    }
}
