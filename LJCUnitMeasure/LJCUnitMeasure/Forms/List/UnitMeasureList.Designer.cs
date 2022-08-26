namespace LJCUnitMeasure
{
	partial class UnitMeasureList
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
			this.CategoryLabel = new System.Windows.Forms.Label();
			this.CategoryCombo = new LJCWinFormControls.LJCItemCombo();
			this.SystemCombo = new LJCWinFormControls.LJCItemCombo();
			this.SystemLabel = new System.Windows.Forms.Label();
			this.UnitMeasureGrid = new LJCWinFormControls.LJCDataGrid(this.components);
			this.MeasureMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.MeasureMenuNew = new System.Windows.Forms.ToolStripMenuItem();
			this.MeasureMenuEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.PersonDeleteSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.MeasureMenuDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.PersonRefreshSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.MeasureMenuRefresh = new System.Windows.Forms.ToolStripMenuItem();
			this.MeasureMenuText = new System.Windows.Forms.ToolStripMenuItem();
			this.MeasureMenuCSV = new System.Windows.Forms.ToolStripMenuItem();
			this.PersonCloseSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.MeasureMenuExit = new System.Windows.Forms.ToolStripMenuItem();
			this.MeasureMenuHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.UnitMeasureHeading = new LJCWinFormControls.LJCHeaderBox();
			this.SystemMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.SystemMenuNew = new System.Windows.Forms.ToolStripMenuItem();
			this.SystemMenuEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.SystemMenuDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.SystemMenuRefresh = new System.Windows.Forms.ToolStripMenuItem();
			this.SystemMenuText = new System.Windows.Forms.ToolStripMenuItem();
			this.SystemMenuCSV = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.SystemMenuExit = new System.Windows.Forms.ToolStripMenuItem();
			this.SystemMenuHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.CategoryMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.CategoryMenuNew = new System.Windows.Forms.ToolStripMenuItem();
			this.CategoryMenuEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.CategoryMenuDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.CategoryMenuRefresh = new System.Windows.Forms.ToolStripMenuItem();
			this.CategoryMenuText = new System.Windows.Forms.ToolStripMenuItem();
			this.CategoryMenuCSV = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.CategoryMenuExit = new System.Windows.Forms.ToolStripMenuItem();
			this.CategoryMenuHelp = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.UnitMeasureGrid)).BeginInit();
			this.MeasureMenu.SuspendLayout();
			this.SystemMenu.SuspendLayout();
			this.CategoryMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// CategoryLabel
			// 
			this.CategoryLabel.Location = new System.Drawing.Point(6, 53);
			this.CategoryLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.CategoryLabel.Name = "CategoryLabel";
			this.CategoryLabel.Size = new System.Drawing.Size(127, 20);
			this.CategoryLabel.TabIndex = 2;
			this.CategoryLabel.Text = "Unit Category";
			// 
			// CategoryCombo
			// 
			this.CategoryCombo.ContextMenuStrip = this.CategoryMenu;
			this.CategoryCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CategoryCombo.FormattingEnabled = true;
			this.CategoryCombo.Location = new System.Drawing.Point(136, 50);
			this.CategoryCombo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.CategoryCombo.Name = "CategoryCombo";
			this.CategoryCombo.Size = new System.Drawing.Size(215, 28);
			this.CategoryCombo.TabIndex = 3;
			this.CategoryCombo.SelectedIndexChanged += new System.EventHandler(this.CategoryCombo_SelectedIndexChanged);
			// 
			// SystemCombo
			// 
			this.SystemCombo.ContextMenuStrip = this.SystemMenu;
			this.SystemCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.SystemCombo.FormattingEnabled = true;
			this.SystemCombo.Location = new System.Drawing.Point(136, 12);
			this.SystemCombo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.SystemCombo.Name = "SystemCombo";
			this.SystemCombo.Size = new System.Drawing.Size(215, 28);
			this.SystemCombo.TabIndex = 1;
			this.SystemCombo.SelectedIndexChanged += new System.EventHandler(this.SystemCombo_SelectedIndexChanged);
			// 
			// SystemLabel
			// 
			this.SystemLabel.Location = new System.Drawing.Point(6, 15);
			this.SystemLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.SystemLabel.Name = "SystemLabel";
			this.SystemLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.SystemLabel.Size = new System.Drawing.Size(127, 20);
			this.SystemLabel.TabIndex = 0;
			this.SystemLabel.Text = "Unit System";
			// 
			// UnitMeasureGrid
			// 
			this.UnitMeasureGrid.AllowUserToAddRows = false;
			this.UnitMeasureGrid.AllowUserToDeleteRows = false;
			this.UnitMeasureGrid.AllowUserToResizeRows = false;
			this.UnitMeasureGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.UnitMeasureGrid.BackgroundColor = System.Drawing.Color.AliceBlue;
			this.UnitMeasureGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.UnitMeasureGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.UnitMeasureGrid.ContextMenuStrip = this.MeasureMenu;
			this.UnitMeasureGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.UnitMeasureGrid.LJCAllowSelectionChange = false;
			this.UnitMeasureGrid.LJCLastRowIndex = -1;
			this.UnitMeasureGrid.LJCRowHeight = 0;
			this.UnitMeasureGrid.Location = new System.Drawing.Point(0, 127);
			this.UnitMeasureGrid.MultiSelect = false;
			this.UnitMeasureGrid.Name = "UnitMeasureGrid";
			this.UnitMeasureGrid.RowHeadersVisible = false;
			this.UnitMeasureGrid.RowTemplate.Height = 28;
			this.UnitMeasureGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.UnitMeasureGrid.ShowCellToolTips = false;
			this.UnitMeasureGrid.Size = new System.Drawing.Size(778, 414);
			this.UnitMeasureGrid.TabIndex = 4;
			this.UnitMeasureGrid.Text = "LJCDataGrid";
			this.UnitMeasureGrid.SelectionChanged += new System.EventHandler(this.UnitMeasureGrid_SelectionChanged);
			this.UnitMeasureGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UnitMeasureGrid_KeyDown);
			this.UnitMeasureGrid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.UnitMeasureGrid_MouseDoubleClick);
			this.UnitMeasureGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UnitMeasureGrid_MouseDown);
			// 
			// MeasureMenu
			// 
			this.MeasureMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.MeasureMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MeasureMenuNew,
            this.MeasureMenuEdit,
            this.PersonDeleteSeparator,
            this.MeasureMenuDelete,
            this.PersonRefreshSeparator,
            this.MeasureMenuRefresh,
            this.MeasureMenuText,
            this.MeasureMenuCSV,
            this.PersonCloseSeparator,
            this.MeasureMenuExit,
            this.MeasureMenuHelp});
			this.MeasureMenu.Name = "mFacilityMenu";
			this.MeasureMenu.Size = new System.Drawing.Size(174, 262);
			// 
			// MeasureMenuNew
			// 
			this.MeasureMenuNew.Name = "MeasureMenuNew";
			this.MeasureMenuNew.Size = new System.Drawing.Size(173, 30);
			this.MeasureMenuNew.Text = "&New";
			this.MeasureMenuNew.Click += new System.EventHandler(this.MeasureMenuNew_Click);
			// 
			// MeasureMenuEdit
			// 
			this.MeasureMenuEdit.Name = "MeasureMenuEdit";
			this.MeasureMenuEdit.ShortcutKeyDisplayString = "Enter";
			this.MeasureMenuEdit.Size = new System.Drawing.Size(173, 30);
			this.MeasureMenuEdit.Text = "&Edit";
			this.MeasureMenuEdit.Click += new System.EventHandler(this.MeasureMenuEdit_Click);
			// 
			// PersonDeleteSeparator
			// 
			this.PersonDeleteSeparator.Name = "PersonDeleteSeparator";
			this.PersonDeleteSeparator.Size = new System.Drawing.Size(170, 6);
			// 
			// MeasureMenuDelete
			// 
			this.MeasureMenuDelete.Name = "MeasureMenuDelete";
			this.MeasureMenuDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.MeasureMenuDelete.Size = new System.Drawing.Size(173, 30);
			this.MeasureMenuDelete.Text = "&Delete";
			this.MeasureMenuDelete.Click += new System.EventHandler(this.MeasureMenuDelete_Click);
			// 
			// PersonRefreshSeparator
			// 
			this.PersonRefreshSeparator.Name = "PersonRefreshSeparator";
			this.PersonRefreshSeparator.Size = new System.Drawing.Size(170, 6);
			// 
			// MeasureMenuRefresh
			// 
			this.MeasureMenuRefresh.Name = "MeasureMenuRefresh";
			this.MeasureMenuRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
			this.MeasureMenuRefresh.Size = new System.Drawing.Size(173, 30);
			this.MeasureMenuRefresh.Text = "&Refresh";
			this.MeasureMenuRefresh.Click += new System.EventHandler(this.MeasureMenuRefresh_Click);
			// 
			// MeasureMenuText
			// 
			this.MeasureMenuText.Name = "MeasureMenuText";
			this.MeasureMenuText.Size = new System.Drawing.Size(173, 30);
			this.MeasureMenuText.Text = "Export &Text";
			this.MeasureMenuText.Click += new System.EventHandler(this.MeasureMenuText_Click);
			// 
			// MeasureMenuCSV
			// 
			this.MeasureMenuCSV.Name = "MeasureMenuCSV";
			this.MeasureMenuCSV.Size = new System.Drawing.Size(173, 30);
			this.MeasureMenuCSV.Text = "E&xportCSV";
			this.MeasureMenuCSV.Click += new System.EventHandler(this.MeasureMenuCSV_Click);
			// 
			// PersonCloseSeparator
			// 
			this.PersonCloseSeparator.Name = "PersonCloseSeparator";
			this.PersonCloseSeparator.Size = new System.Drawing.Size(170, 6);
			// 
			// MeasureMenuExit
			// 
			this.MeasureMenuExit.Name = "MeasureMenuExit";
			this.MeasureMenuExit.Size = new System.Drawing.Size(173, 30);
			this.MeasureMenuExit.Text = "E&xit";
			this.MeasureMenuExit.Click += new System.EventHandler(this.MeasureMenuExit_Click);
			// 
			// MeasureMenuHelp
			// 
			this.MeasureMenuHelp.Name = "MeasureMenuHelp";
			this.MeasureMenuHelp.Size = new System.Drawing.Size(173, 30);
			this.MeasureMenuHelp.Text = "&Help";
			this.MeasureMenuHelp.Click += new System.EventHandler(this.MeasureMenuHelp_Click);
			// 
			// UnitMeasureHeading
			// 
			this.UnitMeasureHeading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.UnitMeasureHeading.LJCBeginColor = System.Drawing.Color.AliceBlue;
			this.UnitMeasureHeading.LJCEndColor = System.Drawing.Color.LightSkyBlue;
			this.UnitMeasureHeading.Location = new System.Drawing.Point(0, 91);
			this.UnitMeasureHeading.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.UnitMeasureHeading.Name = "UnitMeasureHeading";
			this.UnitMeasureHeading.Size = new System.Drawing.Size(778, 31);
			this.UnitMeasureHeading.TabIndex = 13;
			this.UnitMeasureHeading.TabStop = false;
			this.UnitMeasureHeading.Text = "Unit Measure";
			// 
			// SystemMenu
			// 
			this.SystemMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.SystemMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SystemMenuNew,
            this.SystemMenuEdit,
            this.toolStripSeparator1,
            this.SystemMenuDelete,
            this.toolStripSeparator2,
            this.SystemMenuRefresh,
            this.SystemMenuText,
            this.SystemMenuCSV,
            this.toolStripSeparator3,
            this.SystemMenuExit,
            this.SystemMenuHelp});
			this.SystemMenu.Name = "mFacilityMenu";
			this.SystemMenu.Size = new System.Drawing.Size(174, 262);
			// 
			// SystemMenuNew
			// 
			this.SystemMenuNew.Name = "SystemMenuNew";
			this.SystemMenuNew.Size = new System.Drawing.Size(173, 30);
			this.SystemMenuNew.Text = "&New";
			this.SystemMenuNew.Click += new System.EventHandler(this.SystemMenuNew_Click);
			// 
			// SystemMenuEdit
			// 
			this.SystemMenuEdit.Name = "SystemMenuEdit";
			this.SystemMenuEdit.ShortcutKeyDisplayString = "Enter";
			this.SystemMenuEdit.Size = new System.Drawing.Size(173, 30);
			this.SystemMenuEdit.Text = "&Edit";
			this.SystemMenuEdit.Click += new System.EventHandler(this.SystemMenuEdit_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(170, 6);
			// 
			// SystemMenuDelete
			// 
			this.SystemMenuDelete.Name = "SystemMenuDelete";
			this.SystemMenuDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.SystemMenuDelete.Size = new System.Drawing.Size(173, 30);
			this.SystemMenuDelete.Text = "&Delete";
			this.SystemMenuDelete.Click += new System.EventHandler(this.SystemMenuDelete_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(170, 6);
			// 
			// SystemMenuRefresh
			// 
			this.SystemMenuRefresh.Name = "SystemMenuRefresh";
			this.SystemMenuRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
			this.SystemMenuRefresh.Size = new System.Drawing.Size(173, 30);
			this.SystemMenuRefresh.Text = "&Refresh";
			this.SystemMenuRefresh.Click += new System.EventHandler(this.SystemMenuRefresh_Click);
			// 
			// SystemMenuText
			// 
			this.SystemMenuText.Name = "SystemMenuText";
			this.SystemMenuText.Size = new System.Drawing.Size(173, 30);
			this.SystemMenuText.Text = "Export &Text";
			this.SystemMenuText.Click += new System.EventHandler(this.SystemMenuText_Click);
			// 
			// SystemMenuCSV
			// 
			this.SystemMenuCSV.Name = "SystemMenuCSV";
			this.SystemMenuCSV.Size = new System.Drawing.Size(173, 30);
			this.SystemMenuCSV.Text = "E&xportCSV";
			this.SystemMenuCSV.Click += new System.EventHandler(this.SystemMenuCSV_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(170, 6);
			// 
			// SystemMenuExit
			// 
			this.SystemMenuExit.Name = "SystemMenuExit";
			this.SystemMenuExit.Size = new System.Drawing.Size(173, 30);
			this.SystemMenuExit.Text = "E&xit";
			this.SystemMenuExit.Click += new System.EventHandler(this.SystemMenuExit_Click);
			// 
			// SystemMenuHelp
			// 
			this.SystemMenuHelp.Name = "SystemMenuHelp";
			this.SystemMenuHelp.Size = new System.Drawing.Size(173, 30);
			this.SystemMenuHelp.Text = "&Help";
			this.SystemMenuHelp.Click += new System.EventHandler(this.SystemMenuHelp_Click);
			// 
			// CategoryMenu
			// 
			this.CategoryMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.CategoryMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CategoryMenuNew,
            this.CategoryMenuEdit,
            this.toolStripSeparator4,
            this.CategoryMenuDelete,
            this.toolStripSeparator5,
            this.CategoryMenuRefresh,
            this.CategoryMenuText,
            this.CategoryMenuCSV,
            this.toolStripSeparator6,
            this.CategoryMenuExit,
            this.CategoryMenuHelp});
			this.CategoryMenu.Name = "mFacilityMenu";
			this.CategoryMenu.Size = new System.Drawing.Size(174, 262);
			// 
			// CategoryMenuNew
			// 
			this.CategoryMenuNew.Name = "CategoryMenuNew";
			this.CategoryMenuNew.Size = new System.Drawing.Size(240, 30);
			this.CategoryMenuNew.Text = "&New";
			this.CategoryMenuNew.Click += new System.EventHandler(this.CategoryMenuNew_Click);
			// 
			// CategoryMenuEdit
			// 
			this.CategoryMenuEdit.Name = "CategoryMenuEdit";
			this.CategoryMenuEdit.ShortcutKeyDisplayString = "Enter";
			this.CategoryMenuEdit.Size = new System.Drawing.Size(240, 30);
			this.CategoryMenuEdit.Text = "&Edit";
			this.CategoryMenuEdit.Click += new System.EventHandler(this.CategoryMenuEdit_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(237, 6);
			// 
			// CategoryMenuDelete
			// 
			this.CategoryMenuDelete.Name = "CategoryMenuDelete";
			this.CategoryMenuDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.CategoryMenuDelete.Size = new System.Drawing.Size(240, 30);
			this.CategoryMenuDelete.Text = "&Delete";
			this.CategoryMenuDelete.Click += new System.EventHandler(this.CategoryMenuDelete_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(237, 6);
			// 
			// CategoryMenuRefresh
			// 
			this.CategoryMenuRefresh.Name = "CategoryMenuRefresh";
			this.CategoryMenuRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
			this.CategoryMenuRefresh.Size = new System.Drawing.Size(240, 30);
			this.CategoryMenuRefresh.Text = "&Refresh";
			this.CategoryMenuRefresh.Click += new System.EventHandler(this.CategoryMenuRefresh_Click);
			// 
			// CategoryMenuText
			// 
			this.CategoryMenuText.Name = "CategoryMenuText";
			this.CategoryMenuText.Size = new System.Drawing.Size(240, 30);
			this.CategoryMenuText.Text = "Export &Text";
			this.CategoryMenuText.Click += new System.EventHandler(this.CategoryMenuText_Click);
			// 
			// CategoryMenuCSV
			// 
			this.CategoryMenuCSV.Name = "CategoryMenuCSV";
			this.CategoryMenuCSV.Size = new System.Drawing.Size(240, 30);
			this.CategoryMenuCSV.Text = "E&xportCSV";
			this.CategoryMenuCSV.Click += new System.EventHandler(this.CategoryMenuCSV_Click);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(237, 6);
			// 
			// CategoryMenuExit
			// 
			this.CategoryMenuExit.Name = "CategoryMenuExit";
			this.CategoryMenuExit.Size = new System.Drawing.Size(240, 30);
			this.CategoryMenuExit.Text = "E&xit";
			this.CategoryMenuExit.Click += new System.EventHandler(this.CategoryMenuExit_Click);
			// 
			// CategoryMenuHelp
			// 
			this.CategoryMenuHelp.Name = "CategoryMenuHelp";
			this.CategoryMenuHelp.Size = new System.Drawing.Size(240, 30);
			this.CategoryMenuHelp.Text = "&Help";
			this.CategoryMenuHelp.Click += new System.EventHandler(this.CategoryMenuHelp_Click);
			// 
			// UnitMeasureList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(778, 544);
			this.Controls.Add(this.UnitMeasureHeading);
			this.Controls.Add(this.UnitMeasureGrid);
			this.Controls.Add(this.SystemCombo);
			this.Controls.Add(this.SystemLabel);
			this.Controls.Add(this.CategoryCombo);
			this.Controls.Add(this.CategoryLabel);
			this.Name = "UnitMeasureList";
			this.Text = "UnitMeasure List";
			this.Load += new System.EventHandler(this.UnitMeasureList_Load);
			((System.ComponentModel.ISupportInitialize)(this.UnitMeasureGrid)).EndInit();
			this.MeasureMenu.ResumeLayout(false);
			this.SystemMenu.ResumeLayout(false);
			this.CategoryMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Label CategoryLabel;
		private System.Windows.Forms.Label SystemLabel;
		internal LJCWinFormControls.LJCItemCombo CategoryCombo;
		internal LJCWinFormControls.LJCItemCombo SystemCombo;
		internal LJCWinFormControls.LJCDataGrid UnitMeasureGrid;
		private System.Windows.Forms.ContextMenuStrip MeasureMenu;
		private System.Windows.Forms.ToolStripMenuItem MeasureMenuNew;
		private System.Windows.Forms.ToolStripMenuItem MeasureMenuEdit;
		private System.Windows.Forms.ToolStripSeparator PersonDeleteSeparator;
		private System.Windows.Forms.ToolStripMenuItem MeasureMenuDelete;
		private System.Windows.Forms.ToolStripSeparator PersonRefreshSeparator;
		private System.Windows.Forms.ToolStripMenuItem MeasureMenuRefresh;
		private System.Windows.Forms.ToolStripMenuItem MeasureMenuText;
		private System.Windows.Forms.ToolStripMenuItem MeasureMenuCSV;
		private System.Windows.Forms.ToolStripSeparator PersonCloseSeparator;
		private System.Windows.Forms.ToolStripMenuItem MeasureMenuExit;
		private System.Windows.Forms.ToolStripMenuItem MeasureMenuHelp;
		private LJCWinFormControls.LJCHeaderBox UnitMeasureHeading;
		private System.Windows.Forms.ContextMenuStrip SystemMenu;
		private System.Windows.Forms.ToolStripMenuItem SystemMenuNew;
		private System.Windows.Forms.ToolStripMenuItem SystemMenuEdit;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem SystemMenuDelete;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem SystemMenuRefresh;
		private System.Windows.Forms.ToolStripMenuItem SystemMenuText;
		private System.Windows.Forms.ToolStripMenuItem SystemMenuCSV;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem SystemMenuExit;
		private System.Windows.Forms.ToolStripMenuItem SystemMenuHelp;
		private System.Windows.Forms.ContextMenuStrip CategoryMenu;
		private System.Windows.Forms.ToolStripMenuItem CategoryMenuNew;
		private System.Windows.Forms.ToolStripMenuItem CategoryMenuEdit;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem CategoryMenuDelete;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem CategoryMenuRefresh;
		private System.Windows.Forms.ToolStripMenuItem CategoryMenuText;
		private System.Windows.Forms.ToolStripMenuItem CategoryMenuCSV;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripMenuItem CategoryMenuExit;
		private System.Windows.Forms.ToolStripMenuItem CategoryMenuHelp;
	}
}