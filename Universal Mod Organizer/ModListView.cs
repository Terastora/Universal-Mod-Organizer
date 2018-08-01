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

namespace Universal_Mod_Organizer
{
    public partial class BaseForm : Form
    {
        // Text search filters
        private TextMatchFilter filter;
        private HighlightTextRenderer hfilter;

        // Header Format
        private HeaderFormatStyle headerStyle = new HeaderFormatStyle();

        // Fonts
        private Font fontBold = new Font(DefaultFont, FontStyle.Bold);
        private Font fontEmoji = new Font("Segoe UI Emoji", 8.25f, FontStyle.Regular);
        private Font fontUnderline = new Font(DefaultFont, FontStyle.Bold | FontStyle.Underline);

        // Rearrangable list
        private IDragSource dragSource = new SimpleDragSource();
        private IDropSink dropSink = new RearrangingDropSink(false);

        private void InitializeModListView()
        {
            headerStyle.SetFont(fontBold);
            headerStyle.Hot.Font = fontUnderline;

            modListView.EmptyListMsg = "Nothing for now";

            // This is changed in form but I keep track of what has changed from default.
            modListView.GridLines = true;
            modListView.ShowGroups = false;
            modListView.FullRowSelect = true;
            modListView.UseHyperlinks = true;

            // No context menu filter on column right click.
            modListView.SelectColumnsOnRightClick = false;
            modListView.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.None;

            modListView.TintSortColumn = true;

            // Textbox search filter.
            modListView.UseFiltering = true;

            // Cell format events, so we can apply different colors and wonts
            modListView.UseCellFormatEvents = true;

            // Drag and drop support
            modListView.DragSource = dragSource;
            modListView.DropSink = dropSink;

            // Apply header style to exch column
            foreach (OLVColumn item in modListView.Columns)
            {
                item.HeaderFormatStyle = headerStyle;
            }

            // Load Mods to List View
            PopulateModsList();

            // Get Lootables list.
            modListView.SetObjects(ModsList.GetLootLists());
            ParseModsList();
        }

        private void ParseModsList()
        {
            // This parses mod list according to selected profile etc.
            globalProfiles[currentGame].TryGetValue(currentProfile, out List<string> profileModsList);

            // Revert to "clean state"
            Helper.ModListForListView.ForEach(u =>
            {
                // Get index and availability of the mod in the our list i.e. it is enabled and its position in order
                var idx = profileModsList.IndexOf(u.Filename);

                // Clean ordering if any.
                if (idx < 0)
                {
                    // We don`t. So clean enabled state and order.
                    u.Enabled = string.Empty;
                    u.Order = "99999"; // think about workaround
                }

                if (idx >= 0)
                {
                    // We do.
                    u.Enabled = Helper.SymbolYes;
                    u.Order = idx.ToString("D3");
                }
            });

            modListView.BuildList();
            modListView.Sort(columnOrder, SortOrder.Ascending);
            RenumberOrder();
        }

        private void PopulateModsList()
        {
            Helper.ModListForListView.Clear();

            var myMods = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Paradox Interactive\" + currentGame + @"\mod\";

            try
            {
                foreach (string filePath in Directory.GetFiles(myMods))
                {
                    var fileExtension = Path.GetExtension(filePath);
                    if (fileExtension == ".mod")
                    {
                        // Create modList to add to ListView later on.
                        var modListForView = new ModsList();

                        foreach (var line in File.ReadLines(filePath))
                        {
                            var stringSwitch = regexGetOnlyLineName.Replace(line, string.Empty);

                            switch (stringSwitch)
                            {
                                case "original_name":
                                    modListForView.Name = regexGetOnlyLineData.Replace(line, string.Empty);
                                    break;

                                case "name":
                                    modListForView.Name = regexGetOnlyLineData.Replace(line, string.Empty);
                                    break;

                                case "supported_version":
                                    modListForView.Version = regexGetOnlyLineData.Replace(line, string.Empty);
                                    break;

                                case "remote_file_id":
                                    modListForView.UID = regexGetOnlyLineData.Replace(line, string.Empty);
                                    modListForView.Workshop = "https://steamcommunity.com/sharedfiles/filedetails/?id=" + modListForView.UID;
                                    break;

                                default:
                                    break;
                            }
                        }

                        modListForView.Filename = new DirectoryInfo(Path.GetDirectoryName(filePath)).Name + @"\" + Path.GetFileName(filePath);

                        // Add to ListView.
                        Helper.ModListForListView.Add(modListForView);
                    }
                }

                // After all files are processed.
                Helper.ModListForListView.Sort((x, y) => string.Compare(x.Name, y.Name));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString()); // Can`t find mod folder, is the game installed?
            }
        }

        private void ModListClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStripModListView.Show(Cursor.Position);
            }
        }

        private void ModListDoubleClick(object sender, MouseEventArgs e)
        {
            if (modListView.SelectedItems.Count > 0 && Helper.ModListForListView.Count > 0)
            {
                var selectedIndex = modListView.SelectedIndex;
                var selectedSubItem = modListView.GetSubItem(selectedIndex, columnEnabled.DisplayIndex);

                var idx = Helper.ModListForListView.IndexOf(new ModsList(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, modListView.GetSubItem(selectedIndex, columnFilename.DisplayIndex).Text), 0, Helper.ModListForListView.Count);

                if (selectedSubItem.Text == string.Empty)
                {
                    selectedSubItem.Text = Helper.SymbolYes;
                    Helper.ModListForListView[idx].Enabled = selectedSubItem.Text;
                }
                else
                {
                    selectedSubItem.Text = string.Empty;
                    Helper.ModListForListView[idx].Enabled = selectedSubItem.Text;
                }

                unsavedChanges = true;
                modListView.Refresh();
            }
        }

        private void ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // If we are sorting by different column, forbin rearrange by drag and drop.
            if (modListView.LastSortColumn.Index == columnOrder.DisplayIndex)
            {
                modListView.DragSource = dragSource;
                modListView.DropSink = dropSink;
            }
            else
            {
                modListView.DragSource = null;
                modListView.DropSink = null;
            }
        }

        private void EnableSelectedMods(object sender, EventArgs e)
        {
            if (modListView.SelectedIndices.Count > 0)
            {
                for (int i = 0; i < modListView.SelectedIndices.Count; i++)
                {
                    var selectedIndex = modListView.SelectedIndices[i];
                    var selectedSubItem = modListView.GetSubItem(selectedIndex, columnEnabled.DisplayIndex);
                    var idx = Helper.ModListForListView.IndexOf(new ModsList(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, modListView.GetSubItem(selectedIndex, columnFilename.DisplayIndex).Text), 0, Helper.ModListForListView.Count);

                    selectedSubItem.Text = Helper.SymbolYes;
                    Helper.ModListForListView[idx].Enabled = selectedSubItem.Text;
                }

                unsavedChanges = true;
            }
        }

        private void OpenModFolder(object sender, EventArgs e)
        {
            if (modListView.SelectedIndices.Count > 0)
            {
                if (modListView.SelectedIndices.Count > 5)
                {
                    if (MessageBox.Show("There are more than " + modListView.SelectedIndices.Count + " entries to open, you are sure?", string.Empty, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        for (int i = 0; i <= modListView.SelectedIndices.Count - 1; i++)
                        {
                            var filePath = Path.GetDirectoryName(modListView.GetSubItem(modListView.SelectedIndices[i], columnFilename.DisplayIndex).Text);
                            Process.Start(filePath);
                        }
                    }
                }
            }
        }

        private void DisableSelectedMods(object sender, EventArgs e)
        {
            if (modListView.SelectedIndices.Count > 0)
            {
                for (int i = 0; i < modListView.SelectedIndices.Count; i++)
                {
                    var selectedIndex = modListView.SelectedIndices[i];
                    var selectedSubItem = modListView.GetSubItem(selectedIndex, columnEnabled.DisplayIndex);
                    var idx = Helper.ModListForListView.IndexOf(new ModsList(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, modListView.GetSubItem(selectedIndex, columnFilename.DisplayIndex).Text), 0, Helper.ModListForListView.Count);

                    selectedSubItem.Text = string.Empty;
                    Helper.ModListForListView[idx].Enabled = string.Empty;
                }

                unsavedChanges = true;
            }
        }

        private void SearchTextChanged(object sender, EventArgs e)
        {
            filter = TextMatchFilter.Contains(modListView, textBoxSearch.Text);
            hfilter = new HighlightTextRenderer(filter);
            modListView.ModelFilter = filter;
            modListView.DefaultRenderer = hfilter;
            hfilter.UseRoundedRectangle = false;
        }

        private void RenumberOrder()
        {
            if (modListView.Items.Count > 0 && Helper.ModListForListView.Count > 0)
            {
                var startOrder = 0;
                for (int i = 0; i < modListView.Items.Count; i++)
                {
                    /* We check IndexOf because sorting by order != named order so we find the entry in ModListForListView and change order there.
                    We can`t just iterate that list from 0 to 9999.
                    */
                    var idx = Helper.ModListForListView.IndexOf(new ModsList(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, modListView.GetSubItem(startOrder, columnFilename.DisplayIndex).Text), 0, Helper.ModListForListView.Count);
                    if (idx >= 0)
                    {
                        Helper.ModListForListView[idx].Order = startOrder.ToString("D3");
                        modListView.GetSubItem(startOrder, columnOrder.DisplayIndex).Text = startOrder.ToString("D3");
                        startOrder++;
                    }
                }
            }
        }

        private new void DragDrop(object sender, OlvDropEventArgs e)
        {
            unsavedChanges = true;
            RenumberOrder();
        }

        private void FormatCell(object sender, FormatCellEventArgs e)
        {
            if (e.ColumnIndex == columnEnabled.DisplayIndex)
            {
                switch (e.CellValue)
                {
                    case "":
                        e.SubItem.Font = fontEmoji;

                        break;
                    case Helper.SymbolYes:
                        e.SubItem.Font = fontEmoji;
                        break;
                }
            }

            if (e.ColumnIndex == columnWorkshop.DisplayIndex)
            {
                if (e.SubItem.Url.Length > 0)
                {
                    e.SubItem.Url = e.SubItem.Text;
                    e.SubItem.Text = "link";
                }
                else
                {
                    e.SubItem.Text = string.Empty;
                }
            }
        }

        private void CellToolTipShowing(object sender, ToolTipShowingEventArgs e)
        {
            // Show a long tooltip over cells only when the control key is down
            if (e.ColumnIndex == columnName.DisplayIndex)
            {
                var tooltipText = modListView.GetSubItem(e.RowIndex, columnFilename.DisplayIndex).Text;
                if (tooltipText != string.Empty)
                {
                    e.Text = string.Format("Filepath:'\r\n'{0}'", tooltipText);
                }
            }
        }
    }
}
