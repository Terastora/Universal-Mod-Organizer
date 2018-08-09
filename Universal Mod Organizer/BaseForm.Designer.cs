namespace Universal_Mod_Organizer
{
    partial class BaseForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStripBaseForm = new System.Windows.Forms.MenuStrip();
            this.MenuGames = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuGamesCrusaderKings2 = new System.Windows.Forms.ToolStripMenuItem();
            this.europaUniversalisIVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.heartsOfIronIVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuGameStellaris = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuProfiles = new System.Windows.Forms.ToolStripMenuItem();
            this.ProfileAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.ProfileCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.ProfileDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.ProfileRename = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ProfileImport = new System.Windows.Forms.ToolStripMenuItem();
            this.ProfileExport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ResetProfile = new System.Windows.Forms.ToolStripMenuItem();
            this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkAchievementsCompatibilityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.openGameSettingsFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelSearch = new System.Windows.Forms.Label();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.modListView = new BrightIdeasSoftware.ObjectListView();
            this.columnEnabled = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.columnOrder = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.columnName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.columnVersion = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.columnUID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.columnConflicts = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.columnWorkshop = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.columnAchivements = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.columnSize = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.columnFiles = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.columnHighlight = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.buttonApply = new System.Windows.Forms.Button();
            this.contextMenuStripModListView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.enableSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disableSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.openFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openArchiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelProfile = new System.Windows.Forms.Label();
            this.comboBoxProfile = new System.Windows.Forms.ComboBox();
            this.labelGame = new System.Windows.Forms.Label();
            this.textBoxGame = new System.Windows.Forms.TextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.BackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.menuStripBaseForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.modListView)).BeginInit();
            this.contextMenuStripModListView.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripBaseForm
            // 
            this.menuStripBaseForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuGames,
            this.MenuProfiles,
            this.actionsToolStripMenuItem});
            this.menuStripBaseForm.Location = new System.Drawing.Point(0, 0);
            this.menuStripBaseForm.Name = "menuStripBaseForm";
            this.menuStripBaseForm.Size = new System.Drawing.Size(1014, 24);
            this.menuStripBaseForm.TabIndex = 0;
            this.menuStripBaseForm.Text = "menuStripBaseForm";
            // 
            // MenuGames
            // 
            this.MenuGames.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuGamesCrusaderKings2,
            this.europaUniversalisIVToolStripMenuItem,
            this.heartsOfIronIVToolStripMenuItem,
            this.MenuGameStellaris});
            this.MenuGames.Name = "MenuGames";
            this.MenuGames.Size = new System.Drawing.Size(55, 20);
            this.MenuGames.Text = "Games";
            // 
            // MenuGamesCrusaderKings2
            // 
            this.MenuGamesCrusaderKings2.Name = "MenuGamesCrusaderKings2";
            this.MenuGamesCrusaderKings2.Size = new System.Drawing.Size(183, 22);
            this.MenuGamesCrusaderKings2.Text = "Crusader Kings II";
            this.MenuGamesCrusaderKings2.Click += new System.EventHandler(this.AnotherGameSelect);
            // 
            // europaUniversalisIVToolStripMenuItem
            // 
            this.europaUniversalisIVToolStripMenuItem.Name = "europaUniversalisIVToolStripMenuItem";
            this.europaUniversalisIVToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.europaUniversalisIVToolStripMenuItem.Text = "Europa Universalis IV";
            this.europaUniversalisIVToolStripMenuItem.Click += new System.EventHandler(this.AnotherGameSelect);
            // 
            // heartsOfIronIVToolStripMenuItem
            // 
            this.heartsOfIronIVToolStripMenuItem.Name = "heartsOfIronIVToolStripMenuItem";
            this.heartsOfIronIVToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.heartsOfIronIVToolStripMenuItem.Text = "Hearts of Iron IV";
            this.heartsOfIronIVToolStripMenuItem.Click += new System.EventHandler(this.AnotherGameSelect);
            // 
            // MenuGameStellaris
            // 
            this.MenuGameStellaris.Name = "MenuGameStellaris";
            this.MenuGameStellaris.Size = new System.Drawing.Size(183, 22);
            this.MenuGameStellaris.Text = "Stellaris";
            this.MenuGameStellaris.Click += new System.EventHandler(this.AnotherGameSelect);
            // 
            // MenuProfiles
            // 
            this.MenuProfiles.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProfileAdd,
            this.ProfileCopy,
            this.ProfileDelete,
            this.ProfileRename,
            this.toolStripSeparator2,
            this.ProfileImport,
            this.ProfileExport,
            this.toolStripSeparator3,
            this.ResetProfile});
            this.MenuProfiles.Name = "MenuProfiles";
            this.MenuProfiles.Size = new System.Drawing.Size(58, 20);
            this.MenuProfiles.Text = "Profiles";
            // 
            // ProfileAdd
            // 
            this.ProfileAdd.Name = "ProfileAdd";
            this.ProfileAdd.Size = new System.Drawing.Size(154, 22);
            this.ProfileAdd.Text = "Add Profile";
            this.ProfileAdd.Click += new System.EventHandler(this.ProfileAddSelect);
            // 
            // ProfileCopy
            // 
            this.ProfileCopy.Name = "ProfileCopy";
            this.ProfileCopy.Size = new System.Drawing.Size(154, 22);
            this.ProfileCopy.Text = "Copy Profile";
            this.ProfileCopy.Click += new System.EventHandler(this.ProfileAddSelect);
            // 
            // ProfileDelete
            // 
            this.ProfileDelete.Name = "ProfileDelete";
            this.ProfileDelete.Size = new System.Drawing.Size(154, 22);
            this.ProfileDelete.Text = "Delete Profile";
            this.ProfileDelete.Click += new System.EventHandler(this.ProfileDeleteSelect);
            // 
            // ProfileRename
            // 
            this.ProfileRename.Name = "ProfileRename";
            this.ProfileRename.Size = new System.Drawing.Size(154, 22);
            this.ProfileRename.Text = "Rename Profile";
            this.ProfileRename.Click += new System.EventHandler(this.ProfileRenameSelect);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(151, 6);
            // 
            // ProfileImport
            // 
            this.ProfileImport.Name = "ProfileImport";
            this.ProfileImport.Size = new System.Drawing.Size(154, 22);
            this.ProfileImport.Text = "Import Profile";
            this.ProfileImport.Click += new System.EventHandler(this.ProfileImportSelect);
            // 
            // ProfileExport
            // 
            this.ProfileExport.Name = "ProfileExport";
            this.ProfileExport.Size = new System.Drawing.Size(154, 22);
            this.ProfileExport.Text = "Export Profile";
            this.ProfileExport.Click += new System.EventHandler(this.ProfileExportSelect);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(151, 6);
            // 
            // ResetProfile
            // 
            this.ResetProfile.Name = "ResetProfile";
            this.ResetProfile.Size = new System.Drawing.Size(154, 22);
            this.ResetProfile.Text = "Reset Profile";
            this.ResetProfile.Click += new System.EventHandler(this.ProfileResetSelect);
            // 
            // actionsToolStripMenuItem
            // 
            this.actionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkAchievementsCompatibilityToolStripMenuItem,
            this.toolStripSeparator4,
            this.openGameSettingsFolderToolStripMenuItem});
            this.actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
            this.actionsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.actionsToolStripMenuItem.Text = "Actions";
            // 
            // checkAchievementsCompatibilityToolStripMenuItem
            // 
            this.checkAchievementsCompatibilityToolStripMenuItem.Name = "checkAchievementsCompatibilityToolStripMenuItem";
            this.checkAchievementsCompatibilityToolStripMenuItem.Size = new System.Drawing.Size(258, 22);
            this.checkAchievementsCompatibilityToolStripMenuItem.Text = "Check Achievements and Conflicts";
            this.checkAchievementsCompatibilityToolStripMenuItem.Click += new System.EventHandler(this.CheckAchievementAndConflictStatus);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(255, 6);
            // 
            // openGameSettingsFolderToolStripMenuItem
            // 
            this.openGameSettingsFolderToolStripMenuItem.Name = "openGameSettingsFolderToolStripMenuItem";
            this.openGameSettingsFolderToolStripMenuItem.Size = new System.Drawing.Size(258, 22);
            this.openGameSettingsFolderToolStripMenuItem.Text = "Open Mod Installation Folder";
            this.openGameSettingsFolderToolStripMenuItem.Click += new System.EventHandler(this.OpenGameSettingsFolder);
            // 
            // labelSearch
            // 
            this.labelSearch.Location = new System.Drawing.Point(-1, 534);
            this.labelSearch.Name = "labelSearch";
            this.labelSearch.Size = new System.Drawing.Size(42, 22);
            this.labelSearch.TabIndex = 45;
            this.labelSearch.Text = "Search";
            this.labelSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Location = new System.Drawing.Point(43, 536);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(130, 22);
            this.textBoxSearch.TabIndex = 46;
            this.textBoxSearch.TextChanged += new System.EventHandler(this.SearchTextChanged);
            // 
            // modListView
            // 
            this.modListView.AllColumns.Add(this.columnEnabled);
            this.modListView.AllColumns.Add(this.columnOrder);
            this.modListView.AllColumns.Add(this.columnName);
            this.modListView.AllColumns.Add(this.columnVersion);
            this.modListView.AllColumns.Add(this.columnUID);
            this.modListView.AllColumns.Add(this.columnConflicts);
            this.modListView.AllColumns.Add(this.columnWorkshop);
            this.modListView.AllColumns.Add(this.columnAchivements);
            this.modListView.AllColumns.Add(this.columnSize);
            this.modListView.AllColumns.Add(this.columnFiles);
            this.modListView.AllColumns.Add(this.columnHighlight);
            this.modListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.modListView.BackColor = System.Drawing.SystemColors.Window;
            this.modListView.CellEditUseWholeCell = false;
            this.modListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnEnabled,
            this.columnOrder,
            this.columnName,
            this.columnVersion,
            this.columnUID,
            this.columnConflicts,
            this.columnWorkshop,
            this.columnAchivements,
            this.columnSize,
            this.columnFiles,
            this.columnHighlight});
            this.modListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.modListView.FullRowSelect = true;
            this.modListView.GridLines = true;
            this.modListView.Location = new System.Drawing.Point(0, 52);
            this.modListView.Name = "modListView";
            this.modListView.SelectColumnsOnRightClick = false;
            this.modListView.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.None;
            this.modListView.ShowGroups = false;
            this.modListView.Size = new System.Drawing.Size(1014, 481);
            this.modListView.TabIndex = 47;
            this.modListView.TintSortColumn = true;
            this.modListView.UseCellFormatEvents = true;
            this.modListView.UseCompatibleStateImageBehavior = false;
            this.modListView.UseFiltering = true;
            this.modListView.UseHyperlinks = true;
            this.modListView.View = System.Windows.Forms.View.Details;
            this.modListView.CellToolTipShowing += new System.EventHandler<BrightIdeasSoftware.ToolTipShowingEventArgs>(this.CellToolTipShowing);
            this.modListView.Dropped += new System.EventHandler<BrightIdeasSoftware.OlvDropEventArgs>(this.DragDrop);
            this.modListView.FormatCell += new System.EventHandler<BrightIdeasSoftware.FormatCellEventArgs>(this.FormatCell);
            this.modListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ColumnClick);
            this.modListView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ModListClick);
            this.modListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ModListDoubleClick);
            // 
            // columnEnabled
            // 
            this.columnEnabled.AspectName = "Enabled";
            this.columnEnabled.MaximumWidth = 30;
            this.columnEnabled.MinimumWidth = 30;
            this.columnEnabled.Text = "✔";
            this.columnEnabled.Width = 30;
            // 
            // columnOrder
            // 
            this.columnOrder.AspectName = "Order";
            this.columnOrder.MaximumWidth = 60;
            this.columnOrder.MinimumWidth = 60;
            this.columnOrder.Text = "ORDER";
            this.columnOrder.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnName
            // 
            this.columnName.AspectName = "Name";
            this.columnName.MaximumWidth = 300;
            this.columnName.MinimumWidth = 80;
            this.columnName.Text = "NAME";
            this.columnName.Width = 300;
            // 
            // columnVersion
            // 
            this.columnVersion.AspectName = "Version";
            this.columnVersion.MaximumWidth = 70;
            this.columnVersion.MinimumWidth = 70;
            this.columnVersion.Text = "VERSION";
            this.columnVersion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnVersion.Width = 70;
            // 
            // columnUID
            // 
            this.columnUID.AspectName = "UID";
            this.columnUID.MaximumWidth = 90;
            this.columnUID.MinimumWidth = 90;
            this.columnUID.Text = "UID";
            this.columnUID.Width = 90;
            // 
            // columnConflicts
            // 
            this.columnConflicts.AspectName = "Conflicts";
            this.columnConflicts.MaximumWidth = 100;
            this.columnConflicts.MinimumWidth = 100;
            this.columnConflicts.Text = "CONFLICTS";
            this.columnConflicts.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnConflicts.ToolTipText = "Detected Conflicts";
            this.columnConflicts.Width = 100;
            // 
            // columnWorkshop
            // 
            this.columnWorkshop.AspectName = "Workshop";
            this.columnWorkshop.HeaderImageKey = "(none)";
            this.columnWorkshop.Hyperlink = true;
            this.columnWorkshop.MaximumWidth = 110;
            this.columnWorkshop.MinimumWidth = 110;
            this.columnWorkshop.Text = "WORKSHOP";
            this.columnWorkshop.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnWorkshop.Width = 110;
            // 
            // columnAchivements
            // 
            this.columnAchivements.AspectName = "Achivements";
            this.columnAchivements.MaximumWidth = 110;
            this.columnAchivements.MinimumWidth = 110;
            this.columnAchivements.Text = "ACHIEVEMENTS";
            this.columnAchivements.Width = 110;
            // 
            // columnSize
            // 
            this.columnSize.AspectName = "Size";
            this.columnSize.Text = "SIZE";
            // 
            // columnFiles
            // 
            this.columnFiles.AspectName = "FileCount";
            this.columnFiles.Text = "FILES";
            // 
            // columnHighlight
            // 
            this.columnHighlight.AspectName = "Highlight";
            this.columnHighlight.Width = 0;
            // 
            // buttonApply
            // 
            this.buttonApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonApply.Location = new System.Drawing.Point(937, 535);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(75, 24);
            this.buttonApply.TabIndex = 49;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.SettingsApply);
            // 
            // contextMenuStripModListView
            // 
            this.contextMenuStripModListView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enableSelectedToolStripMenuItem,
            this.disableSelectedToolStripMenuItem,
            this.toolStripSeparator1,
            this.openFolderToolStripMenuItem,
            this.openArchiveToolStripMenuItem});
            this.contextMenuStripModListView.Name = "contextMenuStripModListView";
            this.contextMenuStripModListView.Size = new System.Drawing.Size(197, 98);
            // 
            // enableSelectedToolStripMenuItem
            // 
            this.enableSelectedToolStripMenuItem.Name = "enableSelectedToolStripMenuItem";
            this.enableSelectedToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.enableSelectedToolStripMenuItem.Text = "Enable Selected";
            this.enableSelectedToolStripMenuItem.Click += new System.EventHandler(this.EnableSelectedMods);
            // 
            // disableSelectedToolStripMenuItem
            // 
            this.disableSelectedToolStripMenuItem.Name = "disableSelectedToolStripMenuItem";
            this.disableSelectedToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.disableSelectedToolStripMenuItem.Text = "Disable Selected";
            this.disableSelectedToolStripMenuItem.Click += new System.EventHandler(this.DisableSelectedMods);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(193, 6);
            // 
            // openFolderToolStripMenuItem
            // 
            this.openFolderToolStripMenuItem.Name = "openFolderToolStripMenuItem";
            this.openFolderToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.openFolderToolStripMenuItem.Text = "Open Workshop Folder";
            this.openFolderToolStripMenuItem.Click += new System.EventHandler(this.OpenModFolderOrArchive);
            // 
            // openArchiveToolStripMenuItem
            // 
            this.openArchiveToolStripMenuItem.Name = "openArchiveToolStripMenuItem";
            this.openArchiveToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.openArchiveToolStripMenuItem.Text = "Open Archive File";
            this.openArchiveToolStripMenuItem.Click += new System.EventHandler(this.OpenModFolderOrArchive);
            // 
            // labelProfile
            // 
            this.labelProfile.Location = new System.Drawing.Point(698, 27);
            this.labelProfile.Name = "labelProfile";
            this.labelProfile.Size = new System.Drawing.Size(48, 21);
            this.labelProfile.TabIndex = 92;
            this.labelProfile.Text = "Profile";
            this.labelProfile.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBoxProfile
            // 
            this.comboBoxProfile.DropDownHeight = 120;
            this.comboBoxProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProfile.DropDownWidth = 200;
            this.comboBoxProfile.FormattingEnabled = true;
            this.comboBoxProfile.IntegralHeight = false;
            this.comboBoxProfile.Location = new System.Drawing.Point(741, 27);
            this.comboBoxProfile.Name = "comboBoxProfile";
            this.comboBoxProfile.Size = new System.Drawing.Size(200, 21);
            this.comboBoxProfile.TabIndex = 91;
            this.comboBoxProfile.SelectionChangeCommitted += new System.EventHandler(this.ProfileSelectedByUser);
            // 
            // labelGame
            // 
            this.labelGame.Location = new System.Drawing.Point(564, 26);
            this.labelGame.Name = "labelGame";
            this.labelGame.Size = new System.Drawing.Size(40, 22);
            this.labelGame.TabIndex = 93;
            this.labelGame.Text = "Game";
            this.labelGame.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxGame
            // 
            this.textBoxGame.Location = new System.Drawing.Point(603, 27);
            this.textBoxGame.Name = "textBoxGame";
            this.textBoxGame.ReadOnly = true;
            this.textBoxGame.Size = new System.Drawing.Size(100, 22);
            this.textBoxGame.TabIndex = 94;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(458, 25);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 23);
            this.progressBar.TabIndex = 95;
            // 
            // BackgroundWorker
            // 
            this.BackgroundWorker.WorkerReportsProgress = true;
            this.BackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorkderDoWork);
            this.BackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorkderProgressChanged);
            this.BackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorkderCompleted);
            // 
            // BaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 561);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.textBoxGame);
            this.Controls.Add(this.comboBoxProfile);
            this.Controls.Add(this.labelGame);
            this.Controls.Add(this.labelProfile);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.modListView);
            this.Controls.Add(this.labelSearch);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.menuStripBaseForm);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStripBaseForm;
            this.Name = "BaseForm";
            this.Text = "Universal Mod Organizer";
            this.Load += new System.EventHandler(this.BaseFormLoad);
            this.Resize += new System.EventHandler(this.ResizeDone);
            this.menuStripBaseForm.ResumeLayout(false);
            this.menuStripBaseForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.modListView)).EndInit();
            this.contextMenuStripModListView.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripBaseForm;
        public System.Windows.Forms.Label labelSearch;
        private System.Windows.Forms.TextBox textBoxSearch;
        private BrightIdeasSoftware.ObjectListView modListView;
        private BrightIdeasSoftware.OLVColumn columnEnabled;
        private BrightIdeasSoftware.OLVColumn columnOrder;
        private BrightIdeasSoftware.OLVColumn columnName;
        private BrightIdeasSoftware.OLVColumn columnVersion;
        private BrightIdeasSoftware.OLVColumn columnUID;
        private BrightIdeasSoftware.OLVColumn columnConflicts;
        private BrightIdeasSoftware.OLVColumn columnWorkshop;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripModListView;
        private System.Windows.Forms.ToolStripMenuItem enableSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disableSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem openFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuGames;
        private System.Windows.Forms.ToolStripMenuItem MenuGameStellaris;
        private System.Windows.Forms.ToolStripMenuItem MenuGamesCrusaderKings2;
        private System.Windows.Forms.Label labelProfile;
        public System.Windows.Forms.ComboBox comboBoxProfile;
        private System.Windows.Forms.ToolStripMenuItem MenuProfiles;
        private System.Windows.Forms.ToolStripMenuItem ProfileAdd;
        private System.Windows.Forms.ToolStripMenuItem ProfileDelete;
        private System.Windows.Forms.ToolStripMenuItem ProfileImport;
        private System.Windows.Forms.ToolStripMenuItem ProfileExport;
        private System.Windows.Forms.Label labelGame;
        private System.Windows.Forms.TextBox textBoxGame;
        private System.Windows.Forms.ToolStripMenuItem europaUniversalisIVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem heartsOfIronIVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ProfileCopy;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem ProfileRename;
        private BrightIdeasSoftware.OLVColumn columnAchivements;
        private System.Windows.Forms.ToolStripMenuItem actionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkAchievementsCompatibilityToolStripMenuItem;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.ComponentModel.BackgroundWorker BackgroundWorker;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem ResetProfile;
        private BrightIdeasSoftware.OLVColumn columnSize;
        private BrightIdeasSoftware.OLVColumn columnFiles;
        private System.Windows.Forms.ToolStripMenuItem openArchiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openGameSettingsFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private BrightIdeasSoftware.OLVColumn columnHighlight;
    }
}

