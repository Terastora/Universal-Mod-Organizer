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
using ByteSizeLib;
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

namespace Universal_Mod_Organizer
{
    public partial class BaseForm : Form
    {
        // Text search filters
        private TextMatchFilter filter;
        private HighlightTextRenderer hfilter;

        // Header Format
        private HeaderFormatStyle headerStyle = new HeaderFormatStyle();
        private HeaderFormatStyle headerStyleEmoji = new HeaderFormatStyle();

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

            headerStyleEmoji.SetFont(fontEmoji);

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

                if (item.AspectName.Equals("Enabled"))
                {
                    item.HeaderFormatStyle = headerStyleEmoji;
                }
            }

            // Set objects source.
            modListView.SetObjects(modList);
            
            // Load mods
            LoadMods();

            ApplyProfileData();

            // Build final listview.
            modListView.BuildList();
        }

        private void LoadMods()
        {
            modList.Clear();

            foreach (string modFile in Directory.GetFiles(GetGameSettingsFolder() + @"mod\"))
            {
                var fileExtension = Path.GetExtension(modFile);
                if (fileExtension == ".mod")
                {
                    modList.Add(new Mod(modFile));
                }
            }

            modList.ForEach(u =>
            {
                u.GetModInfo();
            });

            // After all files are processed sort them by name.
            modList.Sort((x, y) => string.Compare(x.Name, y.Name));
        }

        private void ApplyProfileData()
        {
           // This parses mod list according to selected profile etc.
           globalProfiles[currentGame].TryGetValue(currentProfile, out List<string> profileModsList);

           // Revert to "clean state" so we can apply profile to existing mod list.
           modList.ForEach(u =>
           {
               // Get index and availability of the mod in the our list i.e. it is enabled and its position in order
               var idx = profileModsList.IndexOf(u.Filename);

               // Clean ordering if any.
               if (idx < 0)
               {
                   // We don`t. So clean enabled state and order.
                   u.Enabled = string.Empty;
                   u.Order = "9999"; // think about workaround
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

        private string GetGameSettingsFolder()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Paradox Interactive\" + currentGame + @"\";
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
            if (modListView.SelectedItems.Count > 0 && modList.Count > 0)
            {
                var selectedIndex = modListView.SelectedIndex;
                var selectedSubItem = modListView.GetSubItem(selectedIndex, columnEnabled.DisplayIndex);

                var idx = modList.FindIndex(x => x.UID.Contains(modListView.GetSubItem(selectedIndex, columnUID.DisplayIndex).Text));

                if (selectedSubItem.Text == string.Empty)
                {
                    selectedSubItem.Text = Helper.SymbolYes;
                    modList[idx].Enabled = selectedSubItem.Text;
                }
                else
                {
                    selectedSubItem.Text = string.Empty;
                    modList[idx].Enabled = selectedSubItem.Text;
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
                    var idx = modList.FindIndex(x => x.UID.Contains(modListView.GetSubItem(selectedIndex, columnUID.DisplayIndex).Text));

                    selectedSubItem.Text = Helper.SymbolYes;
                    modList[idx].Enabled = selectedSubItem.Text;
                }

                unsavedChanges = true;
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
                    var idx = modList.FindIndex(x => x.UID.Contains(modListView.GetSubItem(selectedIndex, columnUID.DisplayIndex).Text));

                    selectedSubItem.Text = string.Empty;
                    modList[idx].Enabled = string.Empty;
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
            if (modListView.Items.Count > 0 && modList.Count > 0)
            {
                var startOrder = 0;
                for (int i = 0; i < modListView.Items.Count; i++)
                {
                    /* We check IndexOf because sorting by order != named order so we find the entry in ModListForListView and change order there.
                    We can`t just iterate that list from 0 to 9999.
                    */
                    var idx = modList.FindIndex(x => x.UID.Contains(modListView.GetSubItem(startOrder, columnUID.DisplayIndex).Text));

                    if (idx >= 0)
                    {
                        modList[idx].Order = startOrder.ToString("D3");
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

            if (e.ColumnIndex == columnSize.DisplayIndex)
            {
                e.SubItem.Text = ByteSize.FromBytes(Convert.ToDouble(e.CellValue)).ToString();
            }
        }

        private void CellToolTipShowing(object sender, ToolTipShowingEventArgs e)
        {
            // Show a long tooltip over cells only when the control key is down
            if (e.ColumnIndex == columnName.DisplayIndex)
            {
                var idx = modList.Find(x => x.UID.Contains(modListView.GetSubItem(e.RowIndex, columnUID.DisplayIndex).Text));

                var tooltipText = idx.FilePath;

                if (tooltipText != string.Empty)
                {
                    e.Text = string.Format("Filepath:\n'{0}'\n-\nFiles\n'{1}'", tooltipText, idx.GetFilesCount());
                }
            }

            if (e.ColumnIndex == columnAchivements.DisplayIndex)
            {
                var tooltipText = "This is testing feature.\nSo it still in development mode.\nSend me the mods links or steamID that are not detected.";
                if (tooltipText != string.Empty)
                {
                    e.Text = string.Format("{0}", tooltipText);
                }
            }
        }
    }
}
