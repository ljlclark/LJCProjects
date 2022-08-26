namespace CVRManager
{
	partial class CVVisitList
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CVVisitList));
			this.CVVisitGrid = new LJCWinFormControls.LJCDataGrid(this.components);
			this.MainMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.MainMenuNew = new System.Windows.Forms.ToolStripMenuItem();
			this.MainMenuEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.MainMenuEnter = new System.Windows.Forms.ToolStripMenuItem();
			this.MainMenuExit = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.MainMenuDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.MainMenuRefresh = new System.Windows.Forms.ToolStripMenuItem();
			this.MainMenuExportText = new System.Windows.Forms.ToolStripMenuItem();
			this.MainMenuExportCSV = new System.Windows.Forms.ToolStripMenuItem();
			this.MainFileEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.MainMenuFacility = new System.Windows.Forms.ToolStripMenuItem();
			this.MainMenuClose = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.MainMenuHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.MainToolPanel = new System.Windows.Forms.Panel();
			this.ClearFiltersButton = new System.Windows.Forms.Button();
			this.FiltersButton = new System.Windows.Forms.Button();
			this.DateButton = new System.Windows.Forms.Button();
			this.ButtonImages = new System.Windows.Forms.ImageList(this.components);
			this.DateMask = new CVRManager.LJCMaskBox();
			this.ShowButton = new System.Windows.Forms.Button();
			this.DateLabel = new System.Windows.Forms.Label();
			this.MainTools = new System.Windows.Forms.ToolStrip();
			this.MainToolsNew = new System.Windows.Forms.ToolStripButton();
			this.MainToolsEdit = new System.Windows.Forms.ToolStripButton();
			this.MainToolsBar1 = new System.Windows.Forms.ToolStripSeparator();
			this.MainToolsEnter = new System.Windows.Forms.ToolStripButton();
			this.MainToolsExit = new System.Windows.Forms.ToolStripButton();
			this.FacilityCombo = new LJCWinFormControls.LJCItemCombo();
			this.FacilityMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.FacilityMenuFacility = new System.Windows.Forms.ToolStripMenuItem();
			this.FacilityLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.CVVisitGrid)).BeginInit();
			this.MainMenu.SuspendLayout();
			this.MainToolPanel.SuspendLayout();
			this.MainTools.SuspendLayout();
			this.FacilityMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// CVVisitGrid
			// 
			this.CVVisitGrid.AllowUserToAddRows = false;
			this.CVVisitGrid.AllowUserToDeleteRows = false;
			this.CVVisitGrid.AllowUserToResizeRows = false;
			this.CVVisitGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.CVVisitGrid.BackgroundColor = System.Drawing.Color.AliceBlue;
			this.CVVisitGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.CVVisitGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.CVVisitGrid.ContextMenuStrip = this.MainMenu;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.CVVisitGrid.DefaultCellStyle = dataGridViewCellStyle1;
			this.CVVisitGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.CVVisitGrid.LJCAllowSelectionChange = false;
			this.CVVisitGrid.LJCLastRowIndex = -1;
			this.CVVisitGrid.LJCRowHeight = 22;
			this.CVVisitGrid.Location = new System.Drawing.Point(0, 130);
			this.CVVisitGrid.MultiSelect = false;
			this.CVVisitGrid.Name = "CVVisitGrid";
			this.CVVisitGrid.RowHeadersVisible = false;
			this.CVVisitGrid.RowTemplate.Height = 28;
			this.CVVisitGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.CVVisitGrid.ShowCellToolTips = false;
			this.CVVisitGrid.Size = new System.Drawing.Size(636, 514);
			this.CVVisitGrid.TabIndex = 3;
			this.CVVisitGrid.Text = "LJCDataGrid";
			this.CVVisitGrid.SelectionChanged += new System.EventHandler(this.CVVisitGrid_SelectionChanged);
			this.CVVisitGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CVVisitGrid_KeyDown);
			this.CVVisitGrid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.CVVisitGrid_MouseDoubleClick);
			this.CVVisitGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CVVisitGrid_MouseDown);
			// 
			// MainMenu
			// 
			this.MainMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenuNew,
            this.MainMenuEdit,
            this.toolStripSeparator1,
            this.MainMenuEnter,
            this.MainMenuExit,
            this.toolStripSeparator3,
            this.MainMenuDelete,
            this.toolStripSeparator2,
            this.MainMenuRefresh,
            this.MainMenuExportText,
            this.MainMenuExportCSV,
            this.MainFileEdit,
            this.toolStripSeparator4,
            this.MainMenuFacility,
            this.MainMenuClose,
            this.toolStripSeparator5,
            this.MainMenuHelp});
			this.MainMenu.Name = "MainMenu";
			this.MainMenu.Size = new System.Drawing.Size(184, 382);
			// 
			// MainMenuNew
			// 
			this.MainMenuNew.AutoSize = false;
			this.MainMenuNew.Name = "MainMenuNew";
			this.MainMenuNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.MainMenuNew.Size = new System.Drawing.Size(240, 28);
			this.MainMenuNew.Text = "&New";
			this.MainMenuNew.Click += new System.EventHandler(this.MainMenuNew_Click);
			// 
			// MainMenuEdit
			// 
			this.MainMenuEdit.AutoSize = false;
			this.MainMenuEdit.Name = "MainMenuEdit";
			this.MainMenuEdit.ShortcutKeyDisplayString = "Enter";
			this.MainMenuEdit.Size = new System.Drawing.Size(240, 28);
			this.MainMenuEdit.Text = "&Edit";
			this.MainMenuEdit.Click += new System.EventHandler(this.MainMenuEdit_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(180, 6);
			// 
			// MainMenuEnter
			// 
			this.MainMenuEnter.AutoSize = false;
			this.MainMenuEnter.Name = "MainMenuEnter";
			this.MainMenuEnter.Size = new System.Drawing.Size(240, 28);
			this.MainMenuEnter.Text = "E&nter";
			this.MainMenuEnter.Click += new System.EventHandler(this.MainMenuEnter_Click);
			// 
			// MainMenuExit
			// 
			this.MainMenuExit.AutoSize = false;
			this.MainMenuExit.Name = "MainMenuExit";
			this.MainMenuExit.Size = new System.Drawing.Size(240, 28);
			this.MainMenuExit.Text = "E&xit";
			this.MainMenuExit.Click += new System.EventHandler(this.MainMenuExit_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(180, 6);
			// 
			// MainMenuDelete
			// 
			this.MainMenuDelete.AutoSize = false;
			this.MainMenuDelete.Name = "MainMenuDelete";
			this.MainMenuDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.MainMenuDelete.Size = new System.Drawing.Size(240, 28);
			this.MainMenuDelete.Text = "&Delete";
			this.MainMenuDelete.Click += new System.EventHandler(this.MainMenuDelete_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(180, 6);
			// 
			// MainMenuRefresh
			// 
			this.MainMenuRefresh.Name = "MainMenuRefresh";
			this.MainMenuRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
			this.MainMenuRefresh.Size = new System.Drawing.Size(183, 30);
			this.MainMenuRefresh.Text = "&Refresh";
			this.MainMenuRefresh.Click += new System.EventHandler(this.MainMenuRefresh_Click);
			// 
			// MainMenuExportText
			// 
			this.MainMenuExportText.Name = "MainMenuExportText";
			this.MainMenuExportText.Size = new System.Drawing.Size(183, 30);
			this.MainMenuExportText.Text = "Export &Text";
			this.MainMenuExportText.Click += new System.EventHandler(this.MainMenuExportText_Click);
			// 
			// MainMenuExportCSV
			// 
			this.MainMenuExportCSV.Name = "MainMenuExportCSV";
			this.MainMenuExportCSV.Size = new System.Drawing.Size(183, 30);
			this.MainMenuExportCSV.Text = "E&xport CSV";
			this.MainMenuExportCSV.Click += new System.EventHandler(this.MainMenuExportCSV_Click);
			// 
			// MainFileEdit
			// 
			this.MainFileEdit.Name = "MainFileEdit";
			this.MainFileEdit.Size = new System.Drawing.Size(183, 30);
			this.MainFileEdit.Text = "F&ile Edit";
			this.MainFileEdit.Click += new System.EventHandler(this.MainFileEdit_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(180, 6);
			// 
			// MainMenuFacility
			// 
			this.MainMenuFacility.Name = "MainMenuFacility";
			this.MainMenuFacility.Size = new System.Drawing.Size(183, 30);
			this.MainMenuFacility.Text = "&Facility List";
			this.MainMenuFacility.Click += new System.EventHandler(this.MainMenuFacility_Click);
			// 
			// MainMenuClose
			// 
			this.MainMenuClose.AutoSize = false;
			this.MainMenuClose.Name = "MainMenuClose";
			this.MainMenuClose.ShortcutKeyDisplayString = "";
			this.MainMenuClose.Size = new System.Drawing.Size(240, 28);
			this.MainMenuClose.Text = "C&lose";
			this.MainMenuClose.Click += new System.EventHandler(this.MainMenuClose_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(180, 6);
			// 
			// MainMenuHelp
			// 
			this.MainMenuHelp.Name = "MainMenuHelp";
			this.MainMenuHelp.ShortcutKeyDisplayString = "F1";
			this.MainMenuHelp.Size = new System.Drawing.Size(183, 30);
			this.MainMenuHelp.Text = "&Help";
			this.MainMenuHelp.Click += new System.EventHandler(this.MainMenuHelp_Click);
			// 
			// MainToolPanel
			// 
			this.MainToolPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MainToolPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.MainToolPanel.Controls.Add(this.ClearFiltersButton);
			this.MainToolPanel.Controls.Add(this.FiltersButton);
			this.MainToolPanel.Controls.Add(this.DateButton);
			this.MainToolPanel.Controls.Add(this.DateMask);
			this.MainToolPanel.Controls.Add(this.ShowButton);
			this.MainToolPanel.Controls.Add(this.DateLabel);
			this.MainToolPanel.Controls.Add(this.MainTools);
			this.MainToolPanel.Location = new System.Drawing.Point(0, 47);
			this.MainToolPanel.Name = "MainToolPanel";
			this.MainToolPanel.Size = new System.Drawing.Size(636, 83);
			this.MainToolPanel.TabIndex = 2;
			// 
			// ClearFiltersButton
			// 
			this.ClearFiltersButton.Location = new System.Drawing.Point(431, 42);
			this.ClearFiltersButton.Name = "ClearFiltersButton";
			this.ClearFiltersButton.Size = new System.Drawing.Size(111, 30);
			this.ClearFiltersButton.TabIndex = 5;
			this.ClearFiltersButton.Text = "Clear Filters";
			this.ClearFiltersButton.UseVisualStyleBackColor = true;
			this.ClearFiltersButton.Click += new System.EventHandler(this.ClearFiltersButton_Click);
			// 
			// FiltersButton
			// 
			this.FiltersButton.Location = new System.Drawing.Point(548, 42);
			this.FiltersButton.Name = "FiltersButton";
			this.FiltersButton.Size = new System.Drawing.Size(75, 30);
			this.FiltersButton.TabIndex = 6;
			this.FiltersButton.Text = "Filters";
			this.FiltersButton.UseVisualStyleBackColor = true;
			this.FiltersButton.Click += new System.EventHandler(this.FiltersButton_Click);
			// 
			// DateButton
			// 
			this.DateButton.ImageKey = "Calendar.bmp";
			this.DateButton.ImageList = this.ButtonImages;
			this.DateButton.Location = new System.Drawing.Point(495, 6);
			this.DateButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.DateButton.Name = "DateButton";
			this.DateButton.Size = new System.Drawing.Size(30, 30);
			this.DateButton.TabIndex = 3;
			this.DateButton.UseVisualStyleBackColor = true;
			this.DateButton.Click += new System.EventHandler(this.DateButton_Click);
			// 
			// ButtonImages
			// 
			this.ButtonImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ButtonImages.ImageStream")));
			this.ButtonImages.TransparentColor = System.Drawing.Color.Magenta;
			this.ButtonImages.Images.SetKeyName(0, "Ellipse.bmp");
			this.ButtonImages.Images.SetKeyName(1, "Calendar.bmp");
			// 
			// DateMask
			// 
			this.DateMask.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.DateMask.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
			this.DateMask.LJCMaskValue = "00/00/0000";
			this.DateMask.LJCText = "";
			this.DateMask.LJCTextCheckString = "/  /";
			this.DateMask.Location = new System.Drawing.Point(375, 6);
			this.DateMask.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.DateMask.Name = "DateMask";
			this.DateMask.Size = new System.Drawing.Size(112, 30);
			this.DateMask.TabIndex = 2;
			// 
			// ShowButton
			// 
			this.ShowButton.Location = new System.Drawing.Point(548, 6);
			this.ShowButton.Name = "ShowButton";
			this.ShowButton.Size = new System.Drawing.Size(75, 30);
			this.ShowButton.TabIndex = 4;
			this.ShowButton.Text = "Show";
			this.ShowButton.UseVisualStyleBackColor = true;
			this.ShowButton.Click += new System.EventHandler(this.ShowButton_Click);
			// 
			// DateLabel
			// 
			this.DateLabel.Location = new System.Drawing.Point(295, 10);
			this.DateLabel.Name = "DateLabel";
			this.DateLabel.Size = new System.Drawing.Size(80, 23);
			this.DateLabel.TabIndex = 1;
			this.DateLabel.Text = "Date";
			// 
			// MainTools
			// 
			this.MainTools.AutoSize = false;
			this.MainTools.Dock = System.Windows.Forms.DockStyle.None;
			this.MainTools.ImageScalingSize = new System.Drawing.Size(36, 47);
			this.MainTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainToolsNew,
            this.MainToolsEdit,
            this.MainToolsBar1,
            this.MainToolsEnter,
            this.MainToolsExit});
			this.MainTools.Location = new System.Drawing.Point(0, 0);
			this.MainTools.Name = "MainTools";
			this.MainTools.ShowItemToolTips = false;
			this.MainTools.Size = new System.Drawing.Size(278, 83);
			this.MainTools.TabIndex = 0;
			this.MainTools.Text = "toolStrip1";
			// 
			// MainToolsNew
			// 
			this.MainToolsNew.AutoSize = false;
			this.MainToolsNew.Image = ((System.Drawing.Image)(resources.GetObject("MainToolsNew.Image")));
			this.MainToolsNew.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.MainToolsNew.Margin = new System.Windows.Forms.Padding(2);
			this.MainToolsNew.Name = "MainToolsNew";
			this.MainToolsNew.Size = new System.Drawing.Size(38, 48);
			this.MainToolsNew.Text = "New";
			this.MainToolsNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.MainToolsNew.Click += new System.EventHandler(this.MainToolsNew_Click);
			// 
			// MainToolsEdit
			// 
			this.MainToolsEdit.AutoSize = false;
			this.MainToolsEdit.Image = ((System.Drawing.Image)(resources.GetObject("MainToolsEdit.Image")));
			this.MainToolsEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.MainToolsEdit.Margin = new System.Windows.Forms.Padding(2);
			this.MainToolsEdit.Name = "MainToolsEdit";
			this.MainToolsEdit.Size = new System.Drawing.Size(38, 48);
			this.MainToolsEdit.Text = "Edit";
			this.MainToolsEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.MainToolsEdit.Click += new System.EventHandler(this.MainToolsEdit_Click);
			// 
			// MainToolsBar1
			// 
			this.MainToolsBar1.Name = "MainToolsBar1";
			this.MainToolsBar1.Size = new System.Drawing.Size(6, 83);
			// 
			// MainToolsEnter
			// 
			this.MainToolsEnter.AutoSize = false;
			this.MainToolsEnter.Image = ((System.Drawing.Image)(resources.GetObject("MainToolsEnter.Image")));
			this.MainToolsEnter.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.MainToolsEnter.Margin = new System.Windows.Forms.Padding(2);
			this.MainToolsEnter.Name = "MainToolsEnter";
			this.MainToolsEnter.Size = new System.Drawing.Size(38, 48);
			this.MainToolsEnter.Text = "Enter";
			this.MainToolsEnter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.MainToolsEnter.Click += new System.EventHandler(this.MainToolsEnter_Click);
			// 
			// MainToolsExit
			// 
			this.MainToolsExit.AutoSize = false;
			this.MainToolsExit.Image = ((System.Drawing.Image)(resources.GetObject("MainToolsExit.Image")));
			this.MainToolsExit.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.MainToolsExit.Margin = new System.Windows.Forms.Padding(2);
			this.MainToolsExit.Name = "MainToolsExit";
			this.MainToolsExit.Size = new System.Drawing.Size(38, 48);
			this.MainToolsExit.Text = "Exit";
			this.MainToolsExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.MainToolsExit.Click += new System.EventHandler(this.MainToolsExit_Click);
			// 
			// FacilityCombo
			// 
			this.FacilityCombo.ContextMenuStrip = this.FacilityMenu;
			this.FacilityCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.FacilityCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FacilityCombo.FormattingEnabled = true;
			this.FacilityCombo.Location = new System.Drawing.Point(120, 6);
			this.FacilityCombo.Name = "FacilityCombo";
			this.FacilityCombo.Size = new System.Drawing.Size(367, 33);
			this.FacilityCombo.TabIndex = 1;
			this.FacilityCombo.SelectedIndexChanged += new System.EventHandler(this.FacilityCombo_SelectedIndexChanged);
			// 
			// FacilityMenu
			// 
			this.FacilityMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.FacilityMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FacilityMenuFacility});
			this.FacilityMenu.Name = "FacilityMenu";
			this.FacilityMenu.Size = new System.Drawing.Size(168, 34);
			// 
			// FacilityMenuFacility
			// 
			this.FacilityMenuFacility.Name = "FacilityMenuFacility";
			this.FacilityMenuFacility.Size = new System.Drawing.Size(167, 30);
			this.FacilityMenuFacility.Text = "&Facility List";
			this.FacilityMenuFacility.Click += new System.EventHandler(this.FacilityMenuFacility_Click);
			// 
			// FacilityLabel
			// 
			this.FacilityLabel.Location = new System.Drawing.Point(8, 12);
			this.FacilityLabel.Name = "FacilityLabel";
			this.FacilityLabel.Size = new System.Drawing.Size(109, 23);
			this.FacilityLabel.TabIndex = 0;
			this.FacilityLabel.Text = "Facility";
			// 
			// CVVisitList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(636, 644);
			this.Controls.Add(this.FacilityLabel);
			this.Controls.Add(this.FacilityCombo);
			this.Controls.Add(this.MainToolPanel);
			this.Controls.Add(this.CVVisitGrid);
			this.Name = "CVVisitList";
			this.Text = "Contact Visit Record Manager";
			this.Load += new System.EventHandler(this.CVVisitList_Load);
			((System.ComponentModel.ISupportInitialize)(this.CVVisitGrid)).EndInit();
			this.MainMenu.ResumeLayout(false);
			this.MainToolPanel.ResumeLayout(false);
			this.MainToolPanel.PerformLayout();
			this.MainTools.ResumeLayout(false);
			this.MainTools.PerformLayout();
			this.FacilityMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		internal LJCWinFormControls.LJCDataGrid CVVisitGrid;
		private System.Windows.Forms.Panel MainToolPanel;
		private System.Windows.Forms.ToolStrip MainTools;
		private System.Windows.Forms.ToolStripButton MainToolsNew;
		private System.Windows.Forms.ToolStripButton MainToolsEdit;
		private System.Windows.Forms.ToolStripButton MainToolsEnter;
		private System.Windows.Forms.ToolStripButton MainToolsExit;
		private System.Windows.Forms.ContextMenuStrip MainMenu;
		private System.Windows.Forms.ToolStripMenuItem MainMenuNew;
		private System.Windows.Forms.ToolStripMenuItem MainMenuEdit;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem MainMenuDelete;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem MainMenuClose;
		private System.Windows.Forms.ToolStripMenuItem MainMenuEnter;
		private System.Windows.Forms.ToolStripMenuItem MainMenuExit;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripSeparator MainToolsBar1;
		private System.Windows.Forms.ToolStripMenuItem MainMenuRefresh;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem MainMenuExportText;
		private System.Windows.Forms.ToolStripMenuItem MainMenuExportCSV;
		private System.Windows.Forms.Button ShowButton;
		private System.Windows.Forms.Label DateLabel;
		private System.Windows.Forms.Button FiltersButton;
		private System.Windows.Forms.Button DateButton;
		internal LJCMaskBox DateMask;
		private System.Windows.Forms.ImageList ButtonImages;
		internal System.Windows.Forms.Button ClearFiltersButton;
		internal LJCWinFormControls.LJCItemCombo FacilityCombo;
		private System.Windows.Forms.Label FacilityLabel;
		private System.Windows.Forms.ContextMenuStrip FacilityMenu;
		private System.Windows.Forms.ToolStripMenuItem FacilityMenuFacility;
		private System.Windows.Forms.ToolStripMenuItem MainMenuHelp;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem MainMenuFacility;
		private System.Windows.Forms.ToolStripMenuItem MainFileEdit;
	}
}

