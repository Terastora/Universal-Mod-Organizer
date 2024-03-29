﻿#region License

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

        // List with all the mods loaded and contaning the all required data
        private List<Mod> modList = new List<Mod>();

        // User made changes
        private bool unsavedChanges = false;

        // We need to know when we have loaded all the mods. Used for cell format events.
        private bool modLoadComplete = false;

        public BaseForm()
        {
            InitializeComponent();

            // This is used for automatic column resizing.
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
            modList.Sort((x, y) => x.Order.CompareTo(y.Order));

            // Temporary enableModsList that we populate with data
            var enabledModsList = new List<string>();

            // Put only active mods there.
            for (int i = 0; i < modList.Count; i++)
            {
                if (modList[i].Enabled == Helper.SymbolYes)
                {
                    enabledModsList.Add(modList[i].Filename);
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
            SettingsGameUpdate(enabledModsList);
        }

        private void SettingsGameUpdate(List<string> enabledModsList)
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
            foreach (var item in modList)
            {
                item.WriteDataToModFile(GetGameSettingsFolder());
            }
        }

        private void SettingsApply(object sender, EventArgs e)
        {
            SettingsSave();
        }

        private void ProfileAddOrCopySelect(object sender, EventArgs e)
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
                    Console.WriteLine("iscopy new profile : {0}, old: {1}", newProfile, currentProfile);
                    globalProfiles[currentGame].TryGetValue(currentProfile, out List<string> profileModsList);
                    globalProfiles[currentGame].Add(newProfile, profileModsList);
                }
                else
                {
                    // Add to global profiles under game name with empty list.
                    globalProfiles[currentGame].Add(newProfile, new List<string>());
                }

                currentProfile = newProfile;
                Console.WriteLine("current profile is {0}", currentProfile);

                SetComboBoxActiveProfile();
                ApplyProfileData();
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
                ApplyProfileData();
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
            ApplyProfileData();
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
                                ApplyProfileData();
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
                var idx = modList.FindIndex(x => x.UID.Contains(modListView.GetSubItem(selectedIndex, columnUID.DisplayIndex).Text));

                selectedSubItem.Text = string.Empty;
                modList[idx].Enabled = string.Empty;
            }

            modListView.Sort(columnName, SortOrder.Ascending);
            RenumberOrder();
            modListView.Sort(columnOrder, SortOrder.Ascending);

            unsavedChanges = true;
        }

        private void ProfileSelectedByUser(object sender, EventArgs e)
        {
            AskToApplyChanges();

            // When new profile selected clean any
            currentProfile = comboBoxProfile.SelectedValue.ToString();
            ApplyProfileData();
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

            LoadMods();
            modListView.SetObjects(modList);
            ApplyProfileData();
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

        private void AskToApplyChanges()
        {
            if (unsavedChanges)
            {
                if (MessageBox.Show("You have unsaved changes.\nApply changes?", string.Empty, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SettingsSave();
                }

                unsavedChanges = false;
            }
        }

        private void CheckAchievementAndConflictStatus(object sender, EventArgs e)
        {
            BackgroundWorker.RunWorkerAsync();
        }

        private void BackgroundWorkderDoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < modList.Count; i++)
            {
                BackgroundWorker.ReportProgress(Math.Min((int)Math.Round(((double)i * 100) / modList.Count), 100));
                modList[i].GetAchievements();
            }

            GetAllConflicts();
            BackgroundWorker.ReportProgress(0);
        }

        private void BackgroundWorkderProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Change the value of the ProgressBar to the BackgroundWorker progress.
            progressBar.Value = e.ProgressPercentage;
        }

        private void BackgroundWorkderCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            modListView.BuildList();
            modListView.Sort(columnOrder, SortOrder.Ascending);
            RenumberOrder();
        }

        private void OpenGameSettingsFolder(object sender, EventArgs e)
        {
            Process.Start(GetGameSettingsFolder());
        }

        private void OpenModFolderOrArchive(object sender, EventArgs e)
        {
            // Ask if opening too many.
            if (modListView.SelectedIndices.Count > 5)
            {
#pragma warning disable S1066 // Collapsible "if" statements should be merged
                if (MessageBox.Show("There are more than " + modListView.SelectedIndices.Count + " entries to open, you are sure?", string.Empty, MessageBoxButtons.YesNo) == DialogResult.No)
#pragma warning restore S1066 // Collapsible "if" statements should be merged
                {
                    return;
                }
            }

            if (modListView.SelectedIndices.Count > 0)
            {
                for (int i = 0; i <= modListView.SelectedIndices.Count - 1; i++)
                {
                    var idx = modList.FindIndex(x => x.UID.Contains(modListView.GetSubItem(modListView.SelectedIndices[i], columnUID.DisplayIndex).Text));

                    if (sender.ToString().Equals("Open Workshop Folder"))
                    {
                        try
                        {
                            var filePath = Path.GetDirectoryName(modList[idx].Archive);
                            Process.Start(filePath);
                        }
                        catch
                        {
                            // We don`t need error handler here. So nothing to do.
                        }
                    }

                    if (sender.ToString().Equals("Open Archive File"))
                    {
                        try
                        {
                            var filePath = modList[idx].Archive;
                            Process.Start(filePath);
                        }
                        catch
                        {
                            // We don`t need error handler here. So nothing to do.
                        }
                    }
                }
            }
        }

        private void GetAllConflicts()
        {
            // Parse each mod.
            for (int baseMod = 0; baseMod < modList.Count; baseMod++)
            {
                // Parse each mod on to compare with previous.
                for (int otherMod = 0; otherMod < modList.Count; otherMod++)
                {
                    if (baseMod == otherMod)
                    {
                        continue;
                    }

                    var conflictingFilesResult = modList[baseMod].ModFiles.Intersect(modList[otherMod].ModFiles);

                    // Do we have something in that list.
                    if (conflictingFilesResult.Any())
                    {
                        // Remove existing key in dictionary if it exists.
                        if (modList[baseMod].ModConflicts.ContainsKey(modList[otherMod].UID))
                        {
                            modList[baseMod].ModConflicts.Remove(modList[otherMod].UID);
                        }

                        var conflictingFileList = new List<string>();

                        foreach (var item in conflictingFilesResult)
                        {
                            conflictingFileList.Add(item);
                        }

                        // Add new dictionary key with conflicing Mod Filename and new list.
                        modList[baseMod].ModConflicts.Add(modList[otherMod].UID, conflictingFileList);
                        modList[baseMod].Conflicts = modList[baseMod].ModConflicts.Count;
                    }
                }
            }
        }
    }
}