namespace LJCFacilityManager
{
	partial class PersonList
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PersonList));
			this.FilterSplit = new System.Windows.Forms.SplitContainer();
			this.FilterPanel = new System.Windows.Forms.Panel();
			this.FilterGroup = new System.Windows.Forms.GroupBox();
			this.TypeClassLabel = new System.Windows.Forms.Label();
			this.TypeClassCombo = new LJCFacilityManager.CodeTypeCombo();
			this.PersonHeading = new LJCWinFormControls.LJCHeaderBox();
			this.PersonToolPanel = new System.Windows.Forms.Panel();
			this.Counter = new System.Windows.Forms.Label();
			this.PersonTool = new System.Windows.Forms.ToolStrip();
			this.PersonToolNew = new System.Windows.Forms.ToolStripButton();
			this.PersonToolEdit = new System.Windows.Forms.ToolStripButton();
			this.PersonToolDelete = new System.Windows.Forms.ToolStripButton();
			this.PersonGrid = new LJCWinFormControls.LJCDataGrid(this.components);
			this.PersonMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.PersonMenuNew = new System.Windows.Forms.ToolStripMenuItem();
			this.PersonMenuEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.PersonDeleteSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.PersonMenuDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.PersonRefreshSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.PersonMenuRefresh = new System.Windows.Forms.ToolStripMenuItem();
			this.PersonMenuSelect = new System.Windows.Forms.ToolStripMenuItem();
			this.PersonCloseSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.PersonMenuClose = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.FilterSplit)).BeginInit();
			this.FilterSplit.Panel1.SuspendLayout();
			this.FilterSplit.Panel2.SuspendLayout();
			this.FilterSplit.SuspendLayout();
			this.FilterPanel.SuspendLayout();
			this.FilterGroup.SuspendLayout();
			this.PersonToolPanel.SuspendLayout();
			this.PersonTool.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PersonGrid)).BeginInit();
			this.PersonMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// FilterSplit
			// 
			this.FilterSplit.Dock = System.Windows.Forms.DockStyle.Fill;
			this.FilterSplit.IsSplitterFixed = true;
			this.FilterSplit.Location = new System.Drawing.Point(0, 0);
			this.FilterSplit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.FilterSplit.Name = "FilterSplit";
			this.FilterSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// FilterSplit.Panel1
			// 
			this.FilterSplit.Panel1.Controls.Add(this.FilterPanel);
			// 
			// FilterSplit.Panel2
			// 
			this.FilterSplit.Panel2.Controls.Add(this.PersonHeading);
			this.FilterSplit.Panel2.Controls.Add(this.PersonToolPanel);
			this.FilterSplit.Panel2.Controls.Add(this.PersonGrid);
			this.FilterSplit.Size = new System.Drawing.Size(876, 560);
			this.FilterSplit.SplitterDistance = 86;
			this.FilterSplit.SplitterWidth = 6;
			this.FilterSplit.TabIndex = 2;
			// 
			// FilterPanel
			// 
			this.FilterPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.FilterPanel.Controls.Add(this.FilterGroup);
			this.FilterPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.FilterPanel.Location = new System.Drawing.Point(0, 0);
			this.FilterPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.FilterPanel.Name = "FilterPanel";
			this.FilterPanel.Size = new System.Drawing.Size(876, 86);
			this.FilterPanel.TabIndex = 0;
			// 
			// FilterGroup
			// 
			this.FilterGroup.Controls.Add(this.TypeClassLabel);
			this.FilterGroup.Controls.Add(this.TypeClassCombo);
			this.FilterGroup.Location = new System.Drawing.Point(8, 3);
			this.FilterGroup.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.FilterGroup.Name = "FilterGroup";
			this.FilterGroup.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.FilterGroup.Size = new System.Drawing.Size(858, 72);
			this.FilterGroup.TabIndex = 1;
			this.FilterGroup.TabStop = false;
			this.FilterGroup.Text = "Filter";
			// 
			// TypeClassLabel
			// 
			this.TypeClassLabel.Location = new System.Drawing.Point(18, 31);
			this.TypeClassLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.TypeClassLabel.Name = "TypeClassLabel";
			this.TypeClassLabel.Size = new System.Drawing.Size(123, 20);
			this.TypeClassLabel.TabIndex = 12;
			this.TypeClassLabel.Text = "Person Type";
			// 
			// TypeClassCombo
			// 
			this.TypeClassCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.TypeClassCombo.FormattingEnabled = true;
			this.TypeClassCombo.Location = new System.Drawing.Point(142, 26);
			this.TypeClassCombo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.TypeClassCombo.Name = "TypeClassCombo";
			this.TypeClassCombo.Size = new System.Drawing.Size(433, 28);
			this.TypeClassCombo.TabIndex = 13;
			this.TypeClassCombo.SelectedIndexChanged += new System.EventHandler(this.TypeClassCombo_SelectedIndexChanged);
			// 
			// PersonHeading
			// 
			this.PersonHeading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PersonHeading.LJCBeginColor = System.Drawing.Color.AliceBlue;
			this.PersonHeading.LJCEndColor = System.Drawing.Color.SkyBlue;
			this.PersonHeading.Location = new System.Drawing.Point(0, 0);
			this.PersonHeading.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.PersonHeading.Name = "PersonHeading";
			this.PersonHeading.Size = new System.Drawing.Size(876, 31);
			this.PersonHeading.TabIndex = 16;
			this.PersonHeading.TabStop = false;
			this.PersonHeading.Text = "Person";
			// 
			// PersonToolPanel
			// 
			this.PersonToolPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PersonToolPanel.BackColor = System.Drawing.SystemColors.Control;
			this.PersonToolPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PersonToolPanel.Controls.Add(this.Counter);
			this.PersonToolPanel.Controls.Add(this.PersonTool);
			this.PersonToolPanel.Location = new System.Drawing.Point(0, 30);
			this.PersonToolPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.PersonToolPanel.Name = "PersonToolPanel";
			this.PersonToolPanel.Size = new System.Drawing.Size(875, 40);
			this.PersonToolPanel.TabIndex = 15;
			// 
			// Counter
			// 
			this.Counter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.Counter.Location = new System.Drawing.Point(620, 13);
			this.Counter.Name = "Counter";
			this.Counter.Size = new System.Drawing.Size(250, 23);
			this.Counter.TabIndex = 7;
			this.Counter.Text = "Row 0 of 0";
			this.Counter.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// PersonTool
			// 
			this.PersonTool.Dock = System.Windows.Forms.DockStyle.None;
			this.PersonTool.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.PersonTool.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.PersonTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PersonToolNew,
            this.PersonToolEdit,
            this.PersonToolDelete});
			this.PersonTool.Location = new System.Drawing.Point(3, 2);
			this.PersonTool.Name = "PersonTool";
			this.PersonTool.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
			this.PersonTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.PersonTool.Size = new System.Drawing.Size(73, 25);
			this.PersonTool.TabIndex = 3;
			this.PersonTool.Text = "toolStrip1";
			// 
			// PersonToolNew
			// 
			this.PersonToolNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.PersonToolNew.Image = ((System.Drawing.Image)(resources.GetObject("PersonToolNew.Image")));
			this.PersonToolNew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.PersonToolNew.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.PersonToolNew.Name = "PersonToolNew";
			this.PersonToolNew.Size = new System.Drawing.Size(23, 22);
			this.PersonToolNew.Text = "New";
			this.PersonToolNew.Click += new System.EventHandler(this.PersonToolNew_Click);
			// 
			// PersonToolEdit
			// 
			this.PersonToolEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.PersonToolEdit.Image = ((System.Drawing.Image)(resources.GetObject("PersonToolEdit.Image")));
			this.PersonToolEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.PersonToolEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.PersonToolEdit.Name = "PersonToolEdit";
			this.PersonToolEdit.Size = new System.Drawing.Size(23, 22);
			this.PersonToolEdit.Text = "Edit";
			this.PersonToolEdit.Click += new System.EventHandler(this.PersonToolEdit_Click);
			// 
			// PersonToolDelete
			// 
			this.PersonToolDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.PersonToolDelete.Image = ((System.Drawing.Image)(resources.GetObject("PersonToolDelete.Image")));
			this.PersonToolDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.PersonToolDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.PersonToolDelete.Name = "PersonToolDelete";
			this.PersonToolDelete.Size = new System.Drawing.Size(23, 22);
			this.PersonToolDelete.Text = "Delete";
			this.PersonToolDelete.Click += new System.EventHandler(this.PersonToolDelete_Click);
			// 
			// PersonGrid
			// 
			this.PersonGrid.AllowUserToAddRows = false;
			this.PersonGrid.AllowUserToDeleteRows = false;
			this.PersonGrid.AllowUserToResizeRows = false;
			this.PersonGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PersonGrid.BackgroundColor = System.Drawing.Color.AliceBlue;
			this.PersonGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.PersonGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.PersonGrid.ContextMenuStrip = this.PersonMenu;
			this.PersonGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.PersonGrid.LJCAllowSelectionChange = false;
			this.PersonGrid.LJCLastRowIndex = -1;
			this.PersonGrid.Location = new System.Drawing.Point(0, 70);
			this.PersonGrid.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.PersonGrid.MultiSelect = false;
			this.PersonGrid.Name = "PersonGrid";
			this.PersonGrid.RowHeadersVisible = false;
			this.PersonGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.PersonGrid.ShowCellToolTips = false;
			this.PersonGrid.Size = new System.Drawing.Size(876, 397);
			this.PersonGrid.TabIndex = 14;
			this.PersonGrid.Text = "LJCDataGrid";
			this.PersonGrid.SelectionChanged += new System.EventHandler(this.PersonGrid_SelectionChanged);
			this.PersonGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PersonGrid_KeyDown);
			this.PersonGrid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.PersonGrid_MouseDoubleClick);
			this.PersonGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PersonGrid_MouseDown);
			// 
			// PersonMenu
			// 
			this.PersonMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.PersonMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PersonMenuNew,
            this.PersonMenuEdit,
            this.PersonDeleteSeparator,
            this.PersonMenuDelete,
            this.PersonRefreshSeparator,
            this.PersonMenuRefresh,
            this.PersonMenuSelect,
            this.PersonCloseSeparator,
            this.PersonMenuClose});
			this.PersonMenu.Name = "mFacilityMenu";
			this.PersonMenu.Size = new System.Drawing.Size(174, 202);
			// 
			// PersonMenuNew
			// 
			this.PersonMenuNew.Name = "PersonMenuNew";
			this.PersonMenuNew.Size = new System.Drawing.Size(173, 30);
			this.PersonMenuNew.Text = "&New";
			this.PersonMenuNew.Click += new System.EventHandler(this.PersonMenuNew_Click);
			// 
			// PersonMenuEdit
			// 
			this.PersonMenuEdit.Name = "PersonMenuEdit";
			this.PersonMenuEdit.ShortcutKeyDisplayString = "Enter";
			this.PersonMenuEdit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.PersonMenuEdit.Size = new System.Drawing.Size(173, 30);
			this.PersonMenuEdit.Text = "&Edit";
			this.PersonMenuEdit.Click += new System.EventHandler(this.PersonMenuEdit_Click);
			// 
			// PersonDeleteSeparator
			// 
			this.PersonDeleteSeparator.Name = "PersonDeleteSeparator";
			this.PersonDeleteSeparator.Size = new System.Drawing.Size(170, 6);
			// 
			// PersonMenuDelete
			// 
			this.PersonMenuDelete.Name = "PersonMenuDelete";
			this.PersonMenuDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.PersonMenuDelete.Size = new System.Drawing.Size(173, 30);
			this.PersonMenuDelete.Text = "&Delete";
			this.PersonMenuDelete.Click += new System.EventHandler(this.PersonMenuDelete_Click);
			// 
			// PersonRefreshSeparator
			// 
			this.PersonRefreshSeparator.Name = "PersonRefreshSeparator";
			this.PersonRefreshSeparator.Size = new System.Drawing.Size(170, 6);
			// 
			// PersonMenuRefresh
			// 
			this.PersonMenuRefresh.Name = "PersonMenuRefresh";
			this.PersonMenuRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
			this.PersonMenuRefresh.Size = new System.Drawing.Size(173, 30);
			this.PersonMenuRefresh.Text = "&Refresh";
			this.PersonMenuRefresh.Click += new System.EventHandler(this.PersonMenuRefresh_Click);
			// 
			// PersonMenuSelect
			// 
			this.PersonMenuSelect.Name = "PersonMenuSelect";
			this.PersonMenuSelect.Size = new System.Drawing.Size(173, 30);
			this.PersonMenuSelect.Text = "&Select";
			this.PersonMenuSelect.Click += new System.EventHandler(this.PersonMenuSelect_Click);
			// 
			// PersonCloseSeparator
			// 
			this.PersonCloseSeparator.Name = "PersonCloseSeparator";
			this.PersonCloseSeparator.Size = new System.Drawing.Size(170, 6);
			// 
			// PersonMenuClose
			// 
			this.PersonMenuClose.Name = "PersonMenuClose";
			this.PersonMenuClose.Size = new System.Drawing.Size(173, 30);
			this.PersonMenuClose.Text = "&Close";
			this.PersonMenuClose.Click += new System.EventHandler(this.PersonMenuClose_Click);
			// 
			// PersonList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(876, 560);
			this.Controls.Add(this.FilterSplit);
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PersonList";
			this.Text = "Person List";
			this.Load += new System.EventHandler(this.PersonList_Load);
			this.FilterSplit.Panel1.ResumeLayout(false);
			this.FilterSplit.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.FilterSplit)).EndInit();
			this.FilterSplit.ResumeLayout(false);
			this.FilterPanel.ResumeLayout(false);
			this.FilterGroup.ResumeLayout(false);
			this.PersonToolPanel.ResumeLayout(false);
			this.PersonToolPanel.PerformLayout();
			this.PersonTool.ResumeLayout(false);
			this.PersonTool.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.PersonGrid)).EndInit();
			this.PersonMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer FilterSplit;
		private System.Windows.Forms.Panel FilterPanel;
		private System.Windows.Forms.GroupBox FilterGroup;
		private System.Windows.Forms.Label TypeClassLabel;
		private CodeTypeCombo TypeClassCombo;
		private LJCWinFormControls.LJCHeaderBox PersonHeading;
		private System.Windows.Forms.Panel PersonToolPanel;
		private System.Windows.Forms.ToolStrip PersonTool;
		private System.Windows.Forms.ToolStripButton PersonToolNew;
		private System.Windows.Forms.ToolStripButton PersonToolEdit;
		private System.Windows.Forms.ToolStripButton PersonToolDelete;
		private LJCWinFormControls.LJCDataGrid PersonGrid;
		private System.Windows.Forms.ContextMenuStrip PersonMenu;
		private System.Windows.Forms.ToolStripMenuItem PersonMenuNew;
		private System.Windows.Forms.ToolStripMenuItem PersonMenuEdit;
		private System.Windows.Forms.ToolStripSeparator PersonDeleteSeparator;
		private System.Windows.Forms.ToolStripMenuItem PersonMenuDelete;
		private System.Windows.Forms.ToolStripSeparator PersonRefreshSeparator;
		private System.Windows.Forms.ToolStripMenuItem PersonMenuRefresh;
		private System.Windows.Forms.ToolStripMenuItem PersonMenuSelect;
		private System.Windows.Forms.ToolStripSeparator PersonCloseSeparator;
		private System.Windows.Forms.ToolStripMenuItem PersonMenuClose;
		private System.Windows.Forms.Label Counter;
	}
}