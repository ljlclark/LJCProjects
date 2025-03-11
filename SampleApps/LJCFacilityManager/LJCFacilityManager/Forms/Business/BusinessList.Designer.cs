namespace LJCFacilityManager
{
	partial class BusinessList
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BusinessList));
			this.FilterSplit = new System.Windows.Forms.SplitContainer();
			this.FilterPanel = new System.Windows.Forms.Panel();
			this.FilterGroup = new System.Windows.Forms.GroupBox();
			this.TypeClassLabel = new System.Windows.Forms.Label();
			this.TypeClassCombo = new LJCFacilityManager.CodeTypeCombo();
			this.ListHeading = new LJCWinFormControls.LJCHeaderBox();
			this.ListToolPanel = new System.Windows.Forms.Panel();
			this.BusinessCounter = new System.Windows.Forms.Label();
			this.BusinessTool = new System.Windows.Forms.ToolStrip();
			this.BusinessToolNew = new System.Windows.Forms.ToolStripButton();
			this.BusinessToolEdit = new System.Windows.Forms.ToolStripButton();
			this.BusinessToolDelete = new System.Windows.Forms.ToolStripButton();
			this.BusinessGrid = new LJCWinFormControls.LJCDataGrid(this.components);
			this.BusinessMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.BusinessMenuNew = new System.Windows.Forms.ToolStripMenuItem();
			this.BusinessMenuEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.BusinessDeleteSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.BusinessMenuDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.BusinessRefreshSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.BusinessMenuRefresh = new System.Windows.Forms.ToolStripMenuItem();
			this.BusinessMenuSelect = new System.Windows.Forms.ToolStripMenuItem();
			this.BusinessCloseSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.BusinessMenuClose = new System.Windows.Forms.ToolStripMenuItem();
			this.BusinessMenuHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			((System.ComponentModel.ISupportInitialize)(this.FilterSplit)).BeginInit();
			this.FilterSplit.Panel1.SuspendLayout();
			this.FilterSplit.Panel2.SuspendLayout();
			this.FilterSplit.SuspendLayout();
			this.FilterPanel.SuspendLayout();
			this.FilterGroup.SuspendLayout();
			this.ListToolPanel.SuspendLayout();
			this.BusinessTool.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.BusinessGrid)).BeginInit();
			this.BusinessMenu.SuspendLayout();
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
			this.FilterSplit.Panel2.Controls.Add(this.ListHeading);
			this.FilterSplit.Panel2.Controls.Add(this.ListToolPanel);
			this.FilterSplit.Panel2.Controls.Add(this.BusinessGrid);
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
			this.TypeClassLabel.Text = "Business Type";
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
			// ListHeading
			// 
			this.ListHeading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ListHeading.LJCBeginColor = System.Drawing.Color.AliceBlue;
			this.ListHeading.LJCEndColor = System.Drawing.Color.SkyBlue;
			this.ListHeading.Location = new System.Drawing.Point(0, 0);
			this.ListHeading.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ListHeading.Name = "ListHeading";
			this.ListHeading.Size = new System.Drawing.Size(876, 31);
			this.ListHeading.TabIndex = 16;
			this.ListHeading.TabStop = false;
			this.ListHeading.Text = "Business";
			// 
			// ListToolPanel
			// 
			this.ListToolPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ListToolPanel.BackColor = System.Drawing.SystemColors.Control;
			this.ListToolPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ListToolPanel.Controls.Add(this.BusinessCounter);
			this.ListToolPanel.Controls.Add(this.BusinessTool);
			this.ListToolPanel.Location = new System.Drawing.Point(0, 30);
			this.ListToolPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ListToolPanel.Name = "ListToolPanel";
			this.ListToolPanel.Size = new System.Drawing.Size(875, 40);
			this.ListToolPanel.TabIndex = 15;
			// 
			// BusinessCounter
			// 
			this.BusinessCounter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BusinessCounter.Location = new System.Drawing.Point(620, 13);
			this.BusinessCounter.Name = "BusinessCounter";
			this.BusinessCounter.Size = new System.Drawing.Size(250, 23);
			this.BusinessCounter.TabIndex = 6;
			this.BusinessCounter.Text = "Row 0 of 0";
			this.BusinessCounter.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// BusinessTool
			// 
			this.BusinessTool.Dock = System.Windows.Forms.DockStyle.None;
			this.BusinessTool.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.BusinessTool.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.BusinessTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BusinessToolNew,
            this.BusinessToolEdit,
            this.BusinessToolDelete});
			this.BusinessTool.Location = new System.Drawing.Point(3, 2);
			this.BusinessTool.Name = "BusinessTool";
			this.BusinessTool.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
			this.BusinessTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.BusinessTool.Size = new System.Drawing.Size(73, 25);
			this.BusinessTool.TabIndex = 3;
			this.BusinessTool.Text = "toolStrip1";
			// 
			// BusinessToolNew
			// 
			this.BusinessToolNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.BusinessToolNew.Image = ((System.Drawing.Image)(resources.GetObject("BusinessToolNew.Image")));
			this.BusinessToolNew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.BusinessToolNew.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.BusinessToolNew.Name = "BusinessToolNew";
			this.BusinessToolNew.Size = new System.Drawing.Size(23, 22);
			this.BusinessToolNew.Text = "New";
			this.BusinessToolNew.Click += new System.EventHandler(this.BusinessToolNew_Click);
			// 
			// BusinessToolEdit
			// 
			this.BusinessToolEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.BusinessToolEdit.Image = ((System.Drawing.Image)(resources.GetObject("BusinessToolEdit.Image")));
			this.BusinessToolEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.BusinessToolEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.BusinessToolEdit.Name = "BusinessToolEdit";
			this.BusinessToolEdit.Size = new System.Drawing.Size(23, 22);
			this.BusinessToolEdit.Text = "Edit";
			this.BusinessToolEdit.Click += new System.EventHandler(this.BusinessToolEdit_Click);
			// 
			// BusinessToolDelete
			// 
			this.BusinessToolDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.BusinessToolDelete.Image = ((System.Drawing.Image)(resources.GetObject("BusinessToolDelete.Image")));
			this.BusinessToolDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.BusinessToolDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.BusinessToolDelete.Name = "BusinessToolDelete";
			this.BusinessToolDelete.Size = new System.Drawing.Size(23, 22);
			this.BusinessToolDelete.Text = "Delete";
			this.BusinessToolDelete.Click += new System.EventHandler(this.BusinessToolDelete_Click);
			// 
			// BusinessGrid
			// 
			this.BusinessGrid.AllowUserToAddRows = false;
			this.BusinessGrid.AllowUserToDeleteRows = false;
			this.BusinessGrid.AllowUserToResizeRows = false;
			this.BusinessGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.BusinessGrid.BackgroundColor = System.Drawing.Color.AliceBlue;
			this.BusinessGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.BusinessGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.BusinessGrid.ContextMenuStrip = this.BusinessMenu;
			this.BusinessGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.BusinessGrid.LJCAllowSelectionChange = false;
			this.BusinessGrid.LJCLastRowIndex = -1;
			this.BusinessGrid.LJCRowHeight = 0;
			this.BusinessGrid.Location = new System.Drawing.Point(0, 70);
			this.BusinessGrid.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.BusinessGrid.MultiSelect = false;
			this.BusinessGrid.Name = "BusinessGrid";
			this.BusinessGrid.RowHeadersVisible = false;
			this.BusinessGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.BusinessGrid.ShowCellToolTips = false;
			this.BusinessGrid.Size = new System.Drawing.Size(876, 392);
			this.BusinessGrid.TabIndex = 14;
			this.BusinessGrid.Text = "LJCDataGrid";
			this.BusinessGrid.SelectionChanged += new System.EventHandler(this.BusinessGrid_SelectionChanged);
			this.BusinessGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BusinessGrid_KeyDown);
			this.BusinessGrid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.BusinessGrid_MouseDoubleClick);
			this.BusinessGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BusinessGrid_MouseDown);
			// 
			// BusinessMenu
			// 
			this.BusinessMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.BusinessMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BusinessMenuNew,
            this.BusinessMenuEdit,
            this.BusinessDeleteSeparator,
            this.BusinessMenuDelete,
            this.BusinessRefreshSeparator,
            this.BusinessMenuRefresh,
            this.BusinessMenuSelect,
            this.BusinessCloseSeparator,
            this.BusinessMenuClose,
            this.toolStripSeparator1,
            this.BusinessMenuHelp});
			this.BusinessMenu.Name = "mFacilityMenu";
			this.BusinessMenu.Size = new System.Drawing.Size(174, 238);
			// 
			// BusinessMenuNew
			// 
			this.BusinessMenuNew.Name = "BusinessMenuNew";
			this.BusinessMenuNew.Size = new System.Drawing.Size(173, 30);
			this.BusinessMenuNew.Text = "&New";
			this.BusinessMenuNew.Click += new System.EventHandler(this.BusinessMenuNew_Click);
			// 
			// BusinessMenuEdit
			// 
			this.BusinessMenuEdit.Name = "BusinessMenuEdit";
			this.BusinessMenuEdit.ShortcutKeyDisplayString = "Enter";
			this.BusinessMenuEdit.Size = new System.Drawing.Size(173, 30);
			this.BusinessMenuEdit.Text = "&Edit";
			this.BusinessMenuEdit.Click += new System.EventHandler(this.BusinessMenuEdit_Click);
			// 
			// BusinessDeleteSeparator
			// 
			this.BusinessDeleteSeparator.Name = "BusinessDeleteSeparator";
			this.BusinessDeleteSeparator.Size = new System.Drawing.Size(170, 6);
			// 
			// BusinessMenuDelete
			// 
			this.BusinessMenuDelete.Name = "BusinessMenuDelete";
			this.BusinessMenuDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.BusinessMenuDelete.Size = new System.Drawing.Size(173, 30);
			this.BusinessMenuDelete.Text = "&Delete";
			this.BusinessMenuDelete.Click += new System.EventHandler(this.BusinessMenuDelete_Click);
			// 
			// BusinessRefreshSeparator
			// 
			this.BusinessRefreshSeparator.Name = "BusinessRefreshSeparator";
			this.BusinessRefreshSeparator.Size = new System.Drawing.Size(170, 6);
			// 
			// BusinessMenuRefresh
			// 
			this.BusinessMenuRefresh.Name = "BusinessMenuRefresh";
			this.BusinessMenuRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
			this.BusinessMenuRefresh.Size = new System.Drawing.Size(173, 30);
			this.BusinessMenuRefresh.Text = "&Refresh";
			this.BusinessMenuRefresh.Click += new System.EventHandler(this.BusinessMenuRefresh_Click);
			// 
			// BusinessMenuSelect
			// 
			this.BusinessMenuSelect.Name = "BusinessMenuSelect";
			this.BusinessMenuSelect.Size = new System.Drawing.Size(173, 30);
			this.BusinessMenuSelect.Text = "&Select";
			this.BusinessMenuSelect.Click += new System.EventHandler(this.BusinessMenuSelect_Click);
			// 
			// BusinessCloseSeparator
			// 
			this.BusinessCloseSeparator.Name = "BusinessCloseSeparator";
			this.BusinessCloseSeparator.Size = new System.Drawing.Size(170, 6);
			// 
			// BusinessMenuClose
			// 
			this.BusinessMenuClose.Name = "BusinessMenuClose";
			this.BusinessMenuClose.Size = new System.Drawing.Size(173, 30);
			this.BusinessMenuClose.Text = "&Close";
			this.BusinessMenuClose.Click += new System.EventHandler(this.BusinessMenuClose_Click);
			// 
			// BusinessMenuHelp
			// 
			this.BusinessMenuHelp.Name = "BusinessMenuHelp";
			this.BusinessMenuHelp.Size = new System.Drawing.Size(173, 30);
			this.BusinessMenuHelp.Text = "&Help";
			this.BusinessMenuHelp.Click += new System.EventHandler(this.BusinessMenuHelp_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(170, 6);
			// 
			// BusinessList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(876, 560);
			this.Controls.Add(this.FilterSplit);
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "BusinessList";
			this.ShowInTaskbar = false;
			this.Text = "Business List";
			this.Load += new System.EventHandler(this.BusinessList_Load);
			this.FilterSplit.Panel1.ResumeLayout(false);
			this.FilterSplit.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.FilterSplit)).EndInit();
			this.FilterSplit.ResumeLayout(false);
			this.FilterPanel.ResumeLayout(false);
			this.FilterGroup.ResumeLayout(false);
			this.ListToolPanel.ResumeLayout(false);
			this.ListToolPanel.PerformLayout();
			this.BusinessTool.ResumeLayout(false);
			this.BusinessTool.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.BusinessGrid)).EndInit();
			this.BusinessMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer FilterSplit;
		private System.Windows.Forms.Panel FilterPanel;
		private System.Windows.Forms.GroupBox FilterGroup;
		private System.Windows.Forms.Label TypeClassLabel;
		private CodeTypeCombo TypeClassCombo;
		private LJCWinFormControls.LJCHeaderBox ListHeading;
		private System.Windows.Forms.Panel ListToolPanel;
		private System.Windows.Forms.ToolStrip BusinessTool;
		private System.Windows.Forms.ToolStripButton BusinessToolNew;
		private System.Windows.Forms.ToolStripButton BusinessToolEdit;
		private System.Windows.Forms.ToolStripButton BusinessToolDelete;
		private LJCWinFormControls.LJCDataGrid BusinessGrid;
		private System.Windows.Forms.ContextMenuStrip BusinessMenu;
		private System.Windows.Forms.ToolStripMenuItem BusinessMenuNew;
		private System.Windows.Forms.ToolStripMenuItem BusinessMenuEdit;
		private System.Windows.Forms.ToolStripSeparator BusinessDeleteSeparator;
		private System.Windows.Forms.ToolStripMenuItem BusinessMenuDelete;
		private System.Windows.Forms.ToolStripSeparator BusinessRefreshSeparator;
		private System.Windows.Forms.ToolStripMenuItem BusinessMenuRefresh;
		private System.Windows.Forms.ToolStripMenuItem BusinessMenuSelect;
		private System.Windows.Forms.ToolStripSeparator BusinessCloseSeparator;
		private System.Windows.Forms.ToolStripMenuItem BusinessMenuClose;
		private System.Windows.Forms.Label BusinessCounter;
		private System.Windows.Forms.ToolStripMenuItem BusinessMenuHelp;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
	}
}