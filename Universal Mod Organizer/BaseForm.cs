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
using System.IO.Compression;
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

        private List<ModList> modListForListView = new List<ModList>();

        // List with files and pathes that lead to checksum change
        private List<string> checksumChangingFoldersAndFiles = new List<string>
        {
            "^common/.+$",
            "^events/.+$",
            "^map/.+$"
        };

        private Regex regexChecksum;

        // User made changes TODO
        private bool unsavedChanges = false;

        public BaseForm()
        {
            InitializeComponent();
            defaultFormWidth = Width - columnName.Width + columnName.MinimumWidth;
            regexChecksum = new Regex(string.Join("|", checksumChangingFoldersAndFiles.Select(item => item)));
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

        private void SettingsDefault()
        {
            // Our game list we will remove one by one if we find it already in xml file
            var gamesCheckList = new List<string>
            {
                "Crusader Kings II",
                "Europa Universalis IV",
                "Hearts of Iron IV",
                "Stellaris"
            };

            // Iterate the games in dictionary and remove games from checklist above that we already have.
            foreach (var gamesDict in globalProfiles)
            {
                // Check if we have that game in dictionary.
                var idx = gamesCheckList.IndexOf(gamesDict.Key);

                if (idx >= 0)
                {
                    // We do. So remove it from the checklist.
                    gamesCheckList.RemoveAt(idx);

                    // So check only default profile if available.
                    if (!globalProfiles[gamesDict.Key].ContainsKey("Default"))
                    {
                        globalProfiles[gamesDict.Key].Add("Default", new List<string>());
                    }
                }
            }

            // Now add games that were left in check list
            foreach (var gameInCheckList in gamesCheckList)
            {
                globalProfiles.Add(gameInCheckList, new SortedDictionary<string, List<string>>());
                globalProfiles[gameInCheckList].Add("Default", new List<string>());
            }
        }

        private void SettingsLoad()
        {
            // TODO 
            /*
             * missing xml doc
             * missing entries
             * misisng default profiles
             * */

            // If we didn`t find dataset.xml file then we need to create new one instead of loading settings.
            if (!File.Exists(@"dataset.xml"))
            {
                SettingsDefault();
                return;
            }

            // Now we load the document and check the default missing entries.
            doc = XDocument.Load(@"dataset.xml");

            // Restore window size or apply default values
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

            // Get missing entries from default settings.
            SettingsDefault();

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
            unsavedChanges = false;

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
            modListForListView.Sort((x, y) => x.Order.CompareTo(y.Order));

            // Temporary enableModsList that we populate with data
            var enabledModsList = new List<string>();

            // Put only active mods there.
            for (int i = 0; i < modListForListView.Count; i++)
            {
                if (modListForListView[i].Enabled == Helper.SymbolYes)
                {
                    enabledModsList.Add(modListForListView[i].Filename);
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
            // Laziness...
            var isCopy = ((ToolStripMenuItem)sender).Name.Equals("ProfileCopy");
            var profileNameInTextBox = isCopy ? (currentProfile + " - Copy") : "new profile";
            var isAddOrCopy = isCopy ? "Copy" : "Add";

            string newProfile = Interaction.InputBox("Enter profile name:", isAddOrCopy + " profile", profileNameInTextBox, Location.X + (Width / 3), Location.Y + (Height / 3));

            if (string.IsNullOrEmpty(newProfile))
            {
                return;
            }

            var index = comboBoxProfile.FindString(newProfile);

            // We did not find any profiles with that name.
            if (index < 0)
            {
                if (isCopy)
                {
                    // Try get existing mod list from current profile (the one we make copy from).
                    globalProfiles[currentGame].TryGetValue(currentProfile, out List<string> profileModsList);
                    globalProfiles[currentGame].Add(newProfile, profileModsList);
                }
                else
                {
                    // Add to global profiles under game name with empty list.
                    globalProfiles[currentGame].Add(newProfile, new List<string>());
                }

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

            if (MessageBox.Show("Delete '" + comboBoxProfile.SelectedItem + "' profile?", string.Empty, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // Remove the dictionary key with the profile and mod list inside
                globalProfiles[currentGame].Remove(comboBoxProfile.SelectedValue.ToString());
                currentProfile = "Default";
                SetComboBoxActiveProfile();
                ParseModsList();
            }
        }

        private void ProfileRenameSelect(object sender, EventArgs e)
        {
            string newProfile = Interaction.InputBox("Enter new profile name:", "Rename profile", currentProfile, Location.X + (Width / 3), Location.Y + (Height / 3));

            if (string.IsNullOrEmpty(newProfile))
            {
                return;
            }

            // Replace activegame activeprofile with new data, while keeping the rest untouched.

            // Get current enabled mod list for profile
            globalProfiles[currentGame].TryGetValue(currentProfile, out List<string> profileModsList);

            // Delete the profile to be renamed
            globalProfiles[currentGame].Remove(currentProfile);

            // Add same profile with new name
            globalProfiles[currentGame].Add(newProfile, profileModsList);

            currentProfile = newProfile;
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

        private void ProfileResetSelect(object sender, EventArgs e)
        {
            // This resets profile to clean state. Disable all, sort by name, reorder.            

            // Disable All
            for (int i = 0; i < modListView.Items.Count; i++)
            {
                var selectedIndex = modListView.Items[i].Index;
                var selectedSubItem = modListView.GetSubItem(selectedIndex, columnEnabled.DisplayIndex);
                var idx = modListForListView.IndexOf(new ModList(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, modListView.GetSubItem(selectedIndex, columnFilename.DisplayIndex).Text), 0, modListForListView.Count);

                selectedSubItem.Text = string.Empty;
                modListForListView[idx].Enabled = string.Empty;
            }

            modListView.Sort(columnName, SortOrder.Ascending);
            RenumberOrder();
            modListView.Sort(columnOrder, SortOrder.Ascending);

            unsavedChanges = true;
        }

        private void AnotherGameSelect(object sender, EventArgs e)
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

            AskToApplyChanges();

            currentProfile = "Default";
            SetComboBoxActiveProfile();

            PopulateModsList();
            modListView.SetObjects(modListForListView);
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

        private void WriteDataToModFile(ModList item, string gameFolder)
        {
            var fileName = gameFolder + item.Filename;

            string[] allFileLines = File.ReadAllLines(fileName);

            // Deleting the file
            File.Delete(fileName);

            using (StreamWriter sw = File.AppendText(fileName))
            {
                var regexMatchLineName = new Regex("^name=.*$");
                var regexMatchLineNameIrrelevantData = new Regex("^#(original_name|achievement_compatible)=.*$");

                foreach (string line in allFileLines)
                {
                    Match matchName = regexMatchLineName.Match(line);
                    Match matchOriginalName = regexMatchLineNameIrrelevantData.Match(line);

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
                        sw.WriteLine("#achievement_compatible=\"" + item.Achivements + "\"");
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

            // http://regexstorm.net/tester
            Regex lastModsEntry = new Regex("last_mods={((\r|\n)+(\t.*(\r|\n)+)+}|.+})(\r|\n)+");

            // Edit game file. settings.txt.

            // Making backup
            System.IO.File.Copy(settingsFile, settingsFileBackup, true);

            // Read all file lines into one big string.
            string alltext = File.ReadAllText(settingsFile, Encoding.ASCII);

            // Remove all last_mod entries by regex match.
            alltext = lastModsEntry.Replace(alltext, string.Empty);

            // Deleting the file.
            File.Delete(settingsFile);

            // Creating settings file without last_mods
            File.WriteAllText(settingsFile, alltext);

            // If you don`t have any mods enabled, then we are done here.
            if (enabledModsList.Count == 0)
            {
                return;
            }

            // Add our modified part with last_mods
            using (StreamWriter sw = File.AppendText(settingsFile))
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

                    sw.WriteLine("}");
                }
            }

            // Also edit the .mod file as we are changing the mod name
            foreach (var item in modListForListView)
            {
                WriteDataToModFile(item, gameFolder);
            }
        }

        private void ProfileSelectedByUser(object sender, EventArgs e)
        {
            AskToApplyChanges();

            // When new profile selected clean any
            currentProfile = comboBoxProfile.SelectedValue.ToString();
            ParseModsList();
        }

        private void AskToApplyChanges()
        {
            // TODO
            /* If user deletes or add profiles what happends?
             * */

            if (unsavedChanges)
            {
                if (MessageBox.Show("You have unsaved changes.\nApply changes?", string.Empty, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SettingsSave();
                }

                unsavedChanges = false;
            }
        }

        private void CheckAchievementStatus(object sender, EventArgs e)
        {
            BackgroundWorkderAchievementChecker.RunWorkerAsync();
        }

        private void BackgroundWorkderAchievementCheckerDoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < modListForListView.Count; i++)
            {
                BackgroundWorkderAchievementChecker.ReportProgress((int)Math.Round(((double)i / 100) * modListForListView.Count, 0));
                string zipPath = modListForListView[i].Archive;

                // Assume that all mods are compatible by default
                modListForListView[i].Achivements = "yes";

                // In case it is very short or empty.
                if (zipPath.Length < 5)
                {
                    continue;
                }

                using (ZipArchive archive = ZipFile.OpenRead(zipPath))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        // Found entry that probably changes checksum
                        Match match = regexChecksum.Match(entry.ToString());

                        if (match.Success)
                        {
                            modListForListView[i].Achivements = "no";
                        }
                    }
                }
            }

            BackgroundWorkderAchievementChecker.ReportProgress(0);
        }

        private void BackgroundWorkderAchievementCheckerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Change the value of the ProgressBar to the BackgroundWorker progress.
            progressBar1.Value = e.ProgressPercentage;

            // Set the text.
            this.Text = e.ProgressPercentage.ToString();
        }

        private void BackgroundWorkderAchievementWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            modListView.BuildList();
            modListView.Sort(columnOrder, SortOrder.Ascending);
            RenumberOrder();
        }
    }
}
