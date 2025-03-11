namespace LJCFacilityManager
{
	partial class AddressList
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddressList));
			this.AddressMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.AddressMenuNew = new System.Windows.Forms.ToolStripMenuItem();
			this.AddressMenuEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.AddressDeleteSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.AddressMenuDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.AddressRefreshSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.AddressMenuRefresh = new System.Windows.Forms.ToolStripMenuItem();
			this.AddressMenuSelect = new System.Windows.Forms.ToolStripMenuItem();
			this.AddressCloseSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.AddressMenuClose = new System.Windows.Forms.ToolStripMenuItem();
			this.mFilterSplit = new System.Windows.Forms.SplitContainer();
			this.mFilterPanel = new System.Windows.Forms.Panel();
			this.FilterGroup = new System.Windows.Forms.GroupBox();
			this.TypeClassLabel = new System.Windows.Forms.Label();
			this.TypeClassCombo = new LJCFacilityManager.CodeTypeCombo();
			this.AddressHeading = new LJCWinFormControls.LJCHeaderBox();
			this.AddressToolPanel = new System.Windows.Forms.Panel();
			this.Counter = new System.Windows.Forms.Label();
			this.AddressTool = new System.Windows.Forms.ToolStrip();
			this.AddressToolNew = new System.Windows.Forms.ToolStripButton();
			this.AddressToolEdit = new System.Windows.Forms.ToolStripButton();
			this.AddressToolDelete = new System.Windows.Forms.ToolStripButton();
			this.AddressGrid = new LJCWinFormControls.LJCDataGrid(this.components);
			this.AddressMenuHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.AddressMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mFilterSplit)).BeginInit();
			this.mFilterSplit.Panel1.SuspendLayout();
			this.mFilterSplit.Panel2.SuspendLayout();
			this.mFilterSplit.SuspendLayout();
			this.mFilterPanel.SuspendLayout();
			this.FilterGroup.SuspendLayout();
			this.AddressToolPanel.SuspendLayout();
			this.AddressTool.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.AddressGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// AddressMenu
			// 
			this.AddressMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.AddressMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddressMenuNew,
            this.AddressMenuEdit,
            this.AddressDeleteSeparator,
            this.AddressMenuDelete,
            this.AddressRefreshSeparator,
            this.AddressMenuRefresh,
            this.AddressMenuSelect,
            this.AddressCloseSeparator,
            this.AddressMenuClose,
            this.toolStripSeparator1,
            this.AddressMenuHelp});
			this.AddressMenu.Name = "mFacilityMenu";
			this.AddressMenu.Size = new System.Drawing.Size(241, 271);
			// 
			// AddressMenuNew
			// 
			this.AddressMenuNew.Name = "AddressMenuNew";
			this.AddressMenuNew.Size = new System.Drawing.Size(240, 30);
			this.AddressMenuNew.Text = "&New";
			this.AddressMenuNew.Click += new System.EventHandler(this.AddressMenuNew_Click);
			// 
			// AddressMenuEdit
			// 
			this.AddressMenuEdit.Name = "AddressMenuEdit";
			this.AddressMenuEdit.ShortcutKeyDisplayString = "Enter";
			this.AddressMenuEdit.Size = new System.Drawing.Size(240, 30);
			this.AddressMenuEdit.Text = "&Edit";
			this.AddressMenuEdit.Click += new System.EventHandler(this.AddressMenuEdit_Click);
			// 
			// AddressDeleteSeparator
			// 
			this.AddressDeleteSeparator.Name = "AddressDeleteSeparator";
			this.AddressDeleteSeparator.Size = new System.Drawing.Size(237, 6);
			// 
			// AddressMenuDelete
			// 
			this.AddressMenuDelete.Name = "AddressMenuDelete";
			this.AddressMenuDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.AddressMenuDelete.Size = new System.Drawing.Size(240, 30);
			this.AddressMenuDelete.Text = "&Delete";
			this.AddressMenuDelete.Click += new System.EventHandler(this.AddressMenuDelete_Click);
			// 
			// AddressRefreshSeparator
			// 
			this.AddressRefreshSeparator.Name = "AddressRefreshSeparator";
			this.AddressRefreshSeparator.Size = new System.Drawing.Size(237, 6);
			// 
			// AddressMenuRefresh
			// 
			this.AddressMenuRefresh.Name = "AddressMenuRefresh";
			this.AddressMenuRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
			this.AddressMenuRefresh.Size = new System.Drawing.Size(240, 30);
			this.AddressMenuRefresh.Text = "&Refresh";
			this.AddressMenuRefresh.Click += new System.EventHandler(this.AddressMenuRefresh_Click);
			// 
			// AddressMenuSelect
			// 
			this.AddressMenuSelect.Name = "AddressMenuSelect";
			this.AddressMenuSelect.Size = new System.Drawing.Size(240, 30);
			this.AddressMenuSelect.Text = "&Select";
			this.AddressMenuSelect.Click += new System.EventHandler(this.AddressMenuSelect_Click);
			// 
			// AddressCloseSeparator
			// 
			this.AddressCloseSeparator.Name = "AddressCloseSeparator";
			this.AddressCloseSeparator.Size = new System.Drawing.Size(237, 6);
			// 
			// AddressMenuClose
			// 
			this.AddressMenuClose.Name = "AddressMenuClose";
			this.AddressMenuClose.Size = new System.Drawing.Size(240, 30);
			this.AddressMenuClose.Text = "&Close";
			this.AddressMenuClose.Click += new System.EventHandler(this.AddressMenuClose_Click);
			// 
			// mFilterSplit
			// 
			this.mFilterSplit.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mFilterSplit.IsSplitterFixed = true;
			this.mFilterSplit.Location = new System.Drawing.Point(0, 0);
			this.mFilterSplit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.mFilterSplit.Name = "mFilterSplit";
			this.mFilterSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// mFilterSplit.Panel1
			// 
			this.mFilterSplit.Panel1.Controls.Add(this.mFilterPanel);
			// 
			// mFilterSplit.Panel2
			// 
			this.mFilterSplit.Panel2.Controls.Add(this.AddressHeading);
			this.mFilterSplit.Panel2.Controls.Add(this.AddressToolPanel);
			this.mFilterSplit.Panel2.Controls.Add(this.AddressGrid);
			this.mFilterSplit.Size = new System.Drawing.Size(876, 560);
			this.mFilterSplit.SplitterDistance = 86;
			this.mFilterSplit.SplitterWidth = 6;
			this.mFilterSplit.TabIndex = 1;
			// 
			// mFilterPanel
			// 
			this.mFilterPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.mFilterPanel.Controls.Add(this.FilterGroup);
			this.mFilterPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mFilterPanel.Location = new System.Drawing.Point(0, 0);
			this.mFilterPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.mFilterPanel.Name = "mFilterPanel";
			this.mFilterPanel.Size = new System.Drawing.Size(876, 86);
			this.mFilterPanel.TabIndex = 0;
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
			this.TypeClassLabel.Text = "Address Type";
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
			this.TypeClassCombo.SelectedIndexChanged += new System.EventHandler(this.TypeCombo_SelectedIndexChanged);
			// 
			// AddressHeading
			// 
			this.AddressHeading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.AddressHeading.LJCBeginColor = System.Drawing.Color.AliceBlue;
			this.AddressHeading.LJCEndColor = System.Drawing.Color.SkyBlue;
			this.AddressHeading.Location = new System.Drawing.Point(0, 0);
			this.AddressHeading.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.AddressHeading.Name = "AddressHeading";
			this.AddressHeading.Size = new System.Drawing.Size(876, 31);
			this.AddressHeading.TabIndex = 16;
			this.AddressHeading.TabStop = false;
			this.AddressHeading.Text = "Address";
			// 
			// AddressToolPanel
			// 
			this.AddressToolPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.AddressToolPanel.BackColor = System.Drawing.SystemColors.Control;
			this.AddressToolPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.AddressToolPanel.Controls.Add(this.Counter);
			this.AddressToolPanel.Controls.Add(this.AddressTool);
			this.AddressToolPanel.Location = new System.Drawing.Point(0, 30);
			this.AddressToolPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.AddressToolPanel.Name = "AddressToolPanel";
			this.AddressToolPanel.Size = new System.Drawing.Size(875, 40);
			this.AddressToolPanel.TabIndex = 15;
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
			// AddressTool
			// 
			this.AddressTool.Dock = System.Windows.Forms.DockStyle.None;
			this.AddressTool.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.AddressTool.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.AddressTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddressToolNew,
            this.AddressToolEdit,
            this.AddressToolDelete});
			this.AddressTool.Location = new System.Drawing.Point(3, 2);
			this.AddressTool.Name = "AddressTool";
			this.AddressTool.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
			this.AddressTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.AddressTool.Size = new System.Drawing.Size(73, 25);
			this.AddressTool.TabIndex = 3;
			this.AddressTool.Text = "toolStrip1";
			// 
			// AddressToolNew
			// 
			this.AddressToolNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.AddressToolNew.Image = ((System.Drawing.Image)(resources.GetObject("AddressToolNew.Image")));
			this.AddressToolNew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.AddressToolNew.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.AddressToolNew.Name = "AddressToolNew";
			this.AddressToolNew.Size = new System.Drawing.Size(23, 22);
			this.AddressToolNew.Text = "New";
			this.AddressToolNew.Click += new System.EventHandler(this.AddressToolNew_Click);
			// 
			// AddressToolEdit
			// 
			this.AddressToolEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.AddressToolEdit.Image = ((System.Drawing.Image)(resources.GetObject("AddressToolEdit.Image")));
			this.AddressToolEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.AddressToolEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.AddressToolEdit.Name = "AddressToolEdit";
			this.AddressToolEdit.Size = new System.Drawing.Size(23, 22);
			this.AddressToolEdit.Text = "Edit";
			this.AddressToolEdit.Click += new System.EventHandler(this.AddressToolEdit_Click);
			// 
			// AddressToolDelete
			// 
			this.AddressToolDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.AddressToolDelete.Image = ((System.Drawing.Image)(resources.GetObject("AddressToolDelete.Image")));
			this.AddressToolDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.AddressToolDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.AddressToolDelete.Name = "AddressToolDelete";
			this.AddressToolDelete.Size = new System.Drawing.Size(23, 22);
			this.AddressToolDelete.Text = "Delete";
			this.AddressToolDelete.Click += new System.EventHandler(this.AddressToolDelete_Click);
			// 
			// AddressGrid
			// 
			this.AddressGrid.AllowUserToAddRows = false;
			this.AddressGrid.AllowUserToDeleteRows = false;
			this.AddressGrid.AllowUserToResizeRows = false;
			this.AddressGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.AddressGrid.BackgroundColor = System.Drawing.Color.AliceBlue;
			this.AddressGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.AddressGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.AddressGrid.ContextMenuStrip = this.AddressMenu;
			this.AddressGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.AddressGrid.LJCAllowSelectionChange = false;
			this.AddressGrid.LJCLastRowIndex = -1;
			this.AddressGrid.LJCRowHeight = 0;
			this.AddressGrid.Location = new System.Drawing.Point(0, 70);
			this.AddressGrid.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.AddressGrid.MultiSelect = false;
			this.AddressGrid.Name = "AddressGrid";
			this.AddressGrid.RowHeadersVisible = false;
			this.AddressGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.AddressGrid.ShowCellToolTips = false;
			this.AddressGrid.Size = new System.Drawing.Size(876, 395);
			this.AddressGrid.TabIndex = 14;
			this.AddressGrid.Text = "LJCDataGrid";
			this.AddressGrid.SelectionChanged += new System.EventHandler(this.AddressGrid_SelectionChanged);
			this.AddressGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AddressGrid_KeyDown);
			this.AddressGrid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.AddressGrid_MouseDoubleClick);
			this.AddressGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AddressGrid_MouseDown);
			// 
			// AddressMenuHelp
			// 
			this.AddressMenuHelp.Name = "AddressMenuHelp";
			this.AddressMenuHelp.Size = new System.Drawing.Size(240, 30);
			this.AddressMenuHelp.Text = "&Help";
			this.AddressMenuHelp.Click += new System.EventHandler(this.AddressMenuHelp_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(237, 6);
			// 
			// AddressList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(876, 560);
			this.Controls.Add(this.mFilterSplit);
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AddressList";
			this.ShowInTaskbar = false;
			this.Text = "Address List";
			this.Load += new System.EventHandler(this.AddressList_Load);
			this.AddressMenu.ResumeLayout(false);
			this.mFilterSplit.Panel1.ResumeLayout(false);
			this.mFilterSplit.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.mFilterSplit)).EndInit();
			this.mFilterSplit.ResumeLayout(false);
			this.mFilterPanel.ResumeLayout(false);
			this.FilterGroup.ResumeLayout(false);
			this.AddressToolPanel.ResumeLayout(false);
			this.AddressToolPanel.PerformLayout();
			this.AddressTool.ResumeLayout(false);
			this.AddressTool.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.AddressGrid)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ContextMenuStrip AddressMenu;
		private System.Windows.Forms.ToolStripMenuItem AddressMenuNew;
		private System.Windows.Forms.ToolStripMenuItem AddressMenuEdit;
		private System.Windows.Forms.ToolStripSeparator AddressDeleteSeparator;
		private System.Windows.Forms.ToolStripMenuItem AddressMenuDelete;
		private System.Windows.Forms.ToolStripSeparator AddressRefreshSeparator;
		private System.Windows.Forms.ToolStripMenuItem AddressMenuRefresh;
		private System.Windows.Forms.ToolStripMenuItem AddressMenuClose;
		private System.Windows.Forms.SplitContainer mFilterSplit;
		private LJCWinFormControls.LJCHeaderBox AddressHeading;
		private System.Windows.Forms.Panel AddressToolPanel;
		private System.Windows.Forms.ToolStrip AddressTool;
		private System.Windows.Forms.ToolStripButton AddressToolNew;
		private System.Windows.Forms.ToolStripButton AddressToolEdit;
		private System.Windows.Forms.ToolStripButton AddressToolDelete;
		private LJCWinFormControls.LJCDataGrid AddressGrid;
		private System.Windows.Forms.Panel mFilterPanel;
		private System.Windows.Forms.GroupBox FilterGroup;
		private System.Windows.Forms.Label TypeClassLabel;
		private CodeTypeCombo TypeClassCombo;
		private System.Windows.Forms.ToolStripMenuItem AddressMenuSelect;
		private System.Windows.Forms.ToolStripSeparator AddressCloseSeparator;
		private System.Windows.Forms.Label Counter;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem AddressMenuHelp;
	}
}