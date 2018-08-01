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
            this.columnFilename = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.buttonApply = new System.Windows.Forms.Button();
            this.contextMenuStripModListView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.enableSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disableSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.openFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelProfile = new System.Windows.Forms.Label();
            this.comboBoxProfile = new System.Windows.Forms.ComboBox();
            this.labelGame = new System.Windows.Forms.Label();
            this.textBoxGame = new System.Windows.Forms.TextBox();
            this.menuStripBaseForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.modListView)).BeginInit();
            this.contextMenuStripModListView.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripBaseForm
            // 
            this.menuStripBaseForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuGames,
            this.MenuProfiles});
            this.menuStripBaseForm.Location = new System.Drawing.Point(0, 0);
            this.menuStripBaseForm.Name = "menuStripBaseForm";
            this.menuStripBaseForm.Size = new System.Drawing.Size(944, 24);
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
            this.ProfileExport});
            this.MenuProfiles.Name = "MenuProfiles";
            this.MenuProfiles.Size = new System.Drawing.Size(58, 20);
            this.MenuProfiles.Text = "Profiles";
            // 
            // ProfileAdd
            // 
            this.ProfileAdd.Name = "ProfileAdd";
            this.ProfileAdd.Size = new System.Drawing.Size(180, 22);
            this.ProfileAdd.Text = "Add Profile";
            this.ProfileAdd.Click += new System.EventHandler(this.ProfileAddSelect);
            // 
            // ProfileCopy
            // 
            this.ProfileCopy.Name = "ProfileCopy";
            this.ProfileCopy.Size = new System.Drawing.Size(180, 22);
            this.ProfileCopy.Text = "Copy Profile";
            this.ProfileCopy.Click += new System.EventHandler(this.ProfileAddSelect);
            // 
            // ProfileDelete
            // 
            this.ProfileDelete.Name = "ProfileDelete";
            this.ProfileDelete.Size = new System.Drawing.Size(180, 22);
            this.ProfileDelete.Text = "Delete Profile";
            this.ProfileDelete.Click += new System.EventHandler(this.ProfileDeleteSelect);
            // 
            // ProfileRename
            // 
            this.ProfileRename.Name = "ProfileRename";
            this.ProfileRename.Size = new System.Drawing.Size(180, 22);
            this.ProfileRename.Text = "Rename Profile";
            this.ProfileRename.Click += new System.EventHandler(this.ProfileRenameSelect);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // ProfileImport
            // 
            this.ProfileImport.Name = "ProfileImport";
            this.ProfileImport.Size = new System.Drawing.Size(180, 22);
            this.ProfileImport.Text = "Import Profile";
            this.ProfileImport.Click += new System.EventHandler(this.ProfileImportSelect);
            // 
            // ProfileExport
            // 
            this.ProfileExport.Name = "ProfileExport";
            this.ProfileExport.Size = new System.Drawing.Size(180, 22);
            this.ProfileExport.Text = "Export Profile";
            this.ProfileExport.Click += new System.EventHandler(this.ProfileExportSelect);
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
            this.modListView.AllColumns.Add(this.columnFilename);
            this.modListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.modListView.CellEditUseWholeCell = false;
            this.modListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnEnabled,
            this.columnOrder,
            this.columnName,
            this.columnVersion,
            this.columnUID,
            this.columnConflicts,
            this.columnWorkshop,
            this.columnFilename});
            this.modListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.modListView.FullRowSelect = true;
            this.modListView.GridLines = true;
            this.modListView.Location = new System.Drawing.Point(0, 52);
            this.modListView.Name = "modListView";
            this.modListView.SelectColumnsOnRightClick = false;
            this.modListView.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.None;
            this.modListView.ShowGroups = false;
            this.modListView.Size = new System.Drawing.Size(944, 481);
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
            this.columnEnabled.MaximumWidth = 80;
            this.columnEnabled.MinimumWidth = 80;
            this.columnEnabled.Text = "ENABLED";
            this.columnEnabled.Width = 80;
            // 
            // columnOrder
            // 
            this.columnOrder.AspectName = "Order";
            this.columnOrder.MaximumWidth = 80;
            this.columnOrder.MinimumWidth = 80;
            this.columnOrder.Text = "ORDER";
            this.columnOrder.Width = 80;
            // 
            // columnName
            // 
            this.columnName.AspectName = "Name";
            this.columnName.MinimumWidth = 80;
            this.columnName.Text = "NAME";
            this.columnName.Width = 340;
            // 
            // columnVersion
            // 
            this.columnVersion.AspectName = "Version";
            this.columnVersion.MaximumWidth = 90;
            this.columnVersion.MinimumWidth = 90;
            this.columnVersion.Text = "VERSION";
            this.columnVersion.Width = 90;
            // 
            // columnUID
            // 
            this.columnUID.AspectName = "UID";
            this.columnUID.MaximumWidth = 120;
            this.columnUID.MinimumWidth = 120;
            this.columnUID.Text = "UID";
            this.columnUID.Width = 120;
            // 
            // columnConflicts
            // 
            this.columnConflicts.AspectName = "Conflicts";
            this.columnConflicts.MaximumWidth = 100;
            this.columnConflicts.MinimumWidth = 100;
            this.columnConflicts.Text = "CONFLICTS";
            this.columnConflicts.Width = 100;
            // 
            // columnWorkshop
            // 
            this.columnWorkshop.AspectName = "Workshop";
            this.columnWorkshop.Hyperlink = true;
            this.columnWorkshop.MaximumWidth = 110;
            this.columnWorkshop.MinimumWidth = 110;
            this.columnWorkshop.Text = "WORKSHOP";
            this.columnWorkshop.Width = 110;
            // 
            // columnFilename
            // 
            this.columnFilename.AspectName = "Filename";
            this.columnFilename.Text = "";
            this.columnFilename.Width = 0;
            // 
            // buttonApply
            // 
            this.buttonApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonApply.Location = new System.Drawing.Point(867, 535);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(75, 24);
            this.buttonApply.TabIndex = 49;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.ApplySettings);
            // 
            // contextMenuStripModListView
            // 
            this.contextMenuStripModListView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enableSelectedToolStripMenuItem,
            this.disableSelectedToolStripMenuItem,
            this.toolStripSeparator1,
            this.openFolderToolStripMenuItem});
            this.contextMenuStripModListView.Name = "contextMenuStripModListView";
            this.contextMenuStripModListView.Size = new System.Drawing.Size(160, 76);
            // 
            // enableSelectedToolStripMenuItem
            // 
            this.enableSelectedToolStripMenuItem.Name = "enableSelectedToolStripMenuItem";
            this.enableSelectedToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.enableSelectedToolStripMenuItem.Text = "Enable Selected";
            this.enableSelectedToolStripMenuItem.Click += new System.EventHandler(this.EnableSelectedMods);
            // 
            // disableSelectedToolStripMenuItem
            // 
            this.disableSelectedToolStripMenuItem.Name = "disableSelectedToolStripMenuItem";
            this.disableSelectedToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.disableSelectedToolStripMenuItem.Text = "Disable Selected";
            this.disableSelectedToolStripMenuItem.Click += new System.EventHandler(this.DisableSelectedMods);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(156, 6);
            // 
            // openFolderToolStripMenuItem
            // 
            this.openFolderToolStripMenuItem.Name = "openFolderToolStripMenuItem";
            this.openFolderToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.openFolderToolStripMenuItem.Text = "Open Folder";
            this.openFolderToolStripMenuItem.Click += new System.EventHandler(this.OpenModFolder);
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
            // BaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 561);
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
        private BrightIdeasSoftware.OLVColumn columnFilename;
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
    }
}

