namespace LJCFacilityManager
{
	partial class CodeTypeModule
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodeTypeModule));
			this.CodeTypeTabs = new LJCWinFormControls.LJCTabControl(this.components);
			this.CodeTypePage = new System.Windows.Forms.TabPage();
			this.CodeTypeGrid = new LJCWinFormControls.LJCDataGrid(this.components);
			this.CodeTypeMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.CodeTypeMenuNew = new System.Windows.Forms.ToolStripMenuItem();
			this.CodeTypeMenuEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.CodeTypeDeleteSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.CodeTypeMenuDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.CodeTypeRefreshSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.CodeTypeMenuRefresh = new System.Windows.Forms.ToolStripMenuItem();
			this.CodeTypeMenuText = new System.Windows.Forms.ToolStripMenuItem();
			this.CodeTypeMenuCSV = new System.Windows.Forms.ToolStripMenuItem();
			this.CodeTypeCloseSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.CodeTypeMenuClose = new System.Windows.Forms.ToolStripMenuItem();
			this.CodeTypeMenuHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.CodeTypeToolPanel = new System.Windows.Forms.Panel();
			this.CodeTypeCounter = new System.Windows.Forms.Label();
			this.CodeTypeTool = new System.Windows.Forms.ToolStrip();
			this.CodeTypeToolNew = new System.Windows.Forms.ToolStripButton();
			this.CodeTypeToolEdit = new System.Windows.Forms.ToolStripButton();
			this.CodeTypeToolDelete = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.CodeTypeTabs.SuspendLayout();
			this.CodeTypePage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.CodeTypeGrid)).BeginInit();
			this.CodeTypeMenu.SuspendLayout();
			this.CodeTypeToolPanel.SuspendLayout();
			this.CodeTypeTool.SuspendLayout();
			this.SuspendLayout();
			// 
			// CodeTypeTabs
			// 
			this.CodeTypeTabs.Controls.Add(this.CodeTypePage);
			this.CodeTypeTabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CodeTypeTabs.Location = new System.Drawing.Point(0, 0);
			this.CodeTypeTabs.Name = "CodeTypeTabs";
			this.CodeTypeTabs.SelectedIndex = 0;
			this.CodeTypeTabs.Size = new System.Drawing.Size(800, 600);
			this.CodeTypeTabs.TabIndex = 1;
			// 
			// CodeTypePage
			// 
			this.CodeTypePage.Controls.Add(this.CodeTypeGrid);
			this.CodeTypePage.Controls.Add(this.CodeTypeToolPanel);
			this.CodeTypePage.Location = new System.Drawing.Point(4, 29);
			this.CodeTypePage.Name = "CodeTypePage";
			this.CodeTypePage.Size = new System.Drawing.Size(792, 567);
			this.CodeTypePage.TabIndex = 0;
			this.CodeTypePage.Text = "Code Type";
			this.CodeTypePage.UseVisualStyleBackColor = true;
			// 
			// CodeTypeGrid
			// 
			this.CodeTypeGrid.AllowUserToAddRows = false;
			this.CodeTypeGrid.AllowUserToDeleteRows = false;
			this.CodeTypeGrid.AllowUserToResizeRows = false;
			this.CodeTypeGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.CodeTypeGrid.BackgroundColor = System.Drawing.Color.AliceBlue;
			this.CodeTypeGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.CodeTypeGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.CodeTypeGrid.ContextMenuStrip = this.CodeTypeMenu;
			this.CodeTypeGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.CodeTypeGrid.LJCAllowSelectionChange = false;
			this.CodeTypeGrid.LJCLastRowIndex = -1;
			this.CodeTypeGrid.LJCRowHeight = 0;
			this.CodeTypeGrid.Location = new System.Drawing.Point(0, 39);
			this.CodeTypeGrid.MultiSelect = false;
			this.CodeTypeGrid.Name = "CodeTypeGrid";
			this.CodeTypeGrid.RowHeadersVisible = false;
			this.CodeTypeGrid.RowTemplate.Height = 28;
			this.CodeTypeGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.CodeTypeGrid.ShowCellToolTips = false;
			this.CodeTypeGrid.Size = new System.Drawing.Size(792, 528);
			this.CodeTypeGrid.TabIndex = 1;
			this.CodeTypeGrid.Text = "LJCDataGrid";
			this.CodeTypeGrid.SelectionChanged += new System.EventHandler(this.CodeTypeGrid_SelectionChanged);
			this.CodeTypeGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CodeTypeGrid_KeyDown);
			this.CodeTypeGrid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.CodeTypeGrid_MouseDoubleClick);
			this.CodeTypeGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CodeTypeGrid_MouseDown);
			// 
			// CodeTypeMenu
			// 
			this.CodeTypeMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.CodeTypeMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CodeTypeMenuNew,
            this.CodeTypeMenuEdit,
            this.CodeTypeDeleteSeparator,
            this.CodeTypeMenuDelete,
            this.CodeTypeRefreshSeparator,
            this.CodeTypeMenuRefresh,
            this.CodeTypeMenuText,
            this.CodeTypeMenuCSV,
            this.CodeTypeCloseSeparator,
            this.CodeTypeMenuClose,
            this.toolStripSeparator1,
            this.CodeTypeMenuHelp});
			this.CodeTypeMenu.Name = "mFacilityMenu";
			this.CodeTypeMenu.Size = new System.Drawing.Size(174, 268);
			// 
			// CodeTypeMenuNew
			// 
			this.CodeTypeMenuNew.Name = "CodeTypeMenuNew";
			this.CodeTypeMenuNew.Size = new System.Drawing.Size(173, 30);
			this.CodeTypeMenuNew.Text = "&New";
			this.CodeTypeMenuNew.Click += new System.EventHandler(this.CodeTypeMenuNew_Click);
			// 
			// CodeTypeMenuEdit
			// 
			this.CodeTypeMenuEdit.Name = "CodeTypeMenuEdit";
			this.CodeTypeMenuEdit.ShortcutKeyDisplayString = "Enter";
			this.CodeTypeMenuEdit.Size = new System.Drawing.Size(173, 30);
			this.CodeTypeMenuEdit.Text = "&Edit";
			this.CodeTypeMenuEdit.Click += new System.EventHandler(this.CodeTypeMenuEdit_Click);
			// 
			// CodeTypeDeleteSeparator
			// 
			this.CodeTypeDeleteSeparator.Name = "CodeTypeDeleteSeparator";
			this.CodeTypeDeleteSeparator.Size = new System.Drawing.Size(170, 6);
			// 
			// CodeTypeMenuDelete
			// 
			this.CodeTypeMenuDelete.Name = "CodeTypeMenuDelete";
			this.CodeTypeMenuDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.CodeTypeMenuDelete.Size = new System.Drawing.Size(173, 30);
			this.CodeTypeMenuDelete.Text = "&Delete";
			this.CodeTypeMenuDelete.Click += new System.EventHandler(this.CodeTypeMenuDelete_Click);
			// 
			// CodeTypeRefreshSeparator
			// 
			this.CodeTypeRefreshSeparator.Name = "CodeTypeRefreshSeparator";
			this.CodeTypeRefreshSeparator.Size = new System.Drawing.Size(170, 6);
			// 
			// CodeTypeMenuRefresh
			// 
			this.CodeTypeMenuRefresh.Name = "CodeTypeMenuRefresh";
			this.CodeTypeMenuRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
			this.CodeTypeMenuRefresh.Size = new System.Drawing.Size(173, 30);
			this.CodeTypeMenuRefresh.Text = "&Refresh";
			this.CodeTypeMenuRefresh.Click += new System.EventHandler(this.CodeTypeMenuRefresh_Click);
			// 
			// CodeTypeMenuText
			// 
			this.CodeTypeMenuText.Name = "CodeTypeMenuText";
			this.CodeTypeMenuText.Size = new System.Drawing.Size(173, 30);
			this.CodeTypeMenuText.Text = "Export &Text";
			this.CodeTypeMenuText.Click += new System.EventHandler(this.CodeTypeMenuText_Click);
			// 
			// CodeTypeMenuCSV
			// 
			this.CodeTypeMenuCSV.Name = "CodeTypeMenuCSV";
			this.CodeTypeMenuCSV.Size = new System.Drawing.Size(173, 30);
			this.CodeTypeMenuCSV.Text = "E&xport CSV";
			this.CodeTypeMenuCSV.Click += new System.EventHandler(this.CodeTypeMenuCSV_Click);
			// 
			// CodeTypeCloseSeparator
			// 
			this.CodeTypeCloseSeparator.Name = "CodeTypeCloseSeparator";
			this.CodeTypeCloseSeparator.Size = new System.Drawing.Size(170, 6);
			// 
			// CodeTypeMenuClose
			// 
			this.CodeTypeMenuClose.Name = "CodeTypeMenuClose";
			this.CodeTypeMenuClose.Size = new System.Drawing.Size(173, 30);
			this.CodeTypeMenuClose.Text = "&Close";
			this.CodeTypeMenuClose.Click += new System.EventHandler(this.CodeTypeMenuClose_Click);
			// 
			// CodeTypeMenuHelp
			// 
			this.CodeTypeMenuHelp.Name = "CodeTypeMenuHelp";
			this.CodeTypeMenuHelp.Size = new System.Drawing.Size(173, 30);
			this.CodeTypeMenuHelp.Text = "&Help";
			this.CodeTypeMenuHelp.Click += new System.EventHandler(this.CodeTypeMenuHelp_Click);
			// 
			// CodeTypeToolPanel
			// 
			this.CodeTypeToolPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.CodeTypeToolPanel.BackColor = System.Drawing.SystemColors.Control;
			this.CodeTypeToolPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.CodeTypeToolPanel.Controls.Add(this.CodeTypeCounter);
			this.CodeTypeToolPanel.Controls.Add(this.CodeTypeTool);
			this.CodeTypeToolPanel.Location = new System.Drawing.Point(0, 0);
			this.CodeTypeToolPanel.Name = "CodeTypeToolPanel";
			this.CodeTypeToolPanel.Size = new System.Drawing.Size(791, 40);
			this.CodeTypeToolPanel.TabIndex = 0;
			// 
			// CodeTypeCounter
			// 
			this.CodeTypeCounter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.CodeTypeCounter.Location = new System.Drawing.Point(536, 13);
			this.CodeTypeCounter.Name = "CodeTypeCounter";
			this.CodeTypeCounter.Size = new System.Drawing.Size(250, 23);
			this.CodeTypeCounter.TabIndex = 6;
			this.CodeTypeCounter.Text = "Row 0 of 0";
			this.CodeTypeCounter.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// CodeTypeTool
			// 
			this.CodeTypeTool.Dock = System.Windows.Forms.DockStyle.None;
			this.CodeTypeTool.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.CodeTypeTool.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.CodeTypeTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CodeTypeToolNew,
            this.CodeTypeToolEdit,
            this.CodeTypeToolDelete});
			this.CodeTypeTool.Location = new System.Drawing.Point(3, 2);
			this.CodeTypeTool.Name = "CodeTypeTool";
			this.CodeTypeTool.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
			this.CodeTypeTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.CodeTypeTool.Size = new System.Drawing.Size(73, 25);
			this.CodeTypeTool.TabIndex = 0;
			this.CodeTypeTool.Text = "FirstTool";
			// 
			// CodeTypeToolNew
			// 
			this.CodeTypeToolNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.CodeTypeToolNew.Image = ((System.Drawing.Image)(resources.GetObject("CodeTypeToolNew.Image")));
			this.CodeTypeToolNew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.CodeTypeToolNew.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.CodeTypeToolNew.Name = "CodeTypeToolNew";
			this.CodeTypeToolNew.Size = new System.Drawing.Size(23, 22);
			this.CodeTypeToolNew.Text = "New";
			this.CodeTypeToolNew.Click += new System.EventHandler(this.CodeTypeToolNew_Click);
			// 
			// CodeTypeToolEdit
			// 
			this.CodeTypeToolEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.CodeTypeToolEdit.Image = ((System.Drawing.Image)(resources.GetObject("CodeTypeToolEdit.Image")));
			this.CodeTypeToolEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.CodeTypeToolEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.CodeTypeToolEdit.Name = "CodeTypeToolEdit";
			this.CodeTypeToolEdit.Size = new System.Drawing.Size(23, 22);
			this.CodeTypeToolEdit.Text = "Edit";
			this.CodeTypeToolEdit.Click += new System.EventHandler(this.CodeTypeToolEdit_Click);
			// 
			// CodeTypeToolDelete
			// 
			this.CodeTypeToolDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.CodeTypeToolDelete.Image = ((System.Drawing.Image)(resources.GetObject("CodeTypeToolDelete.Image")));
			this.CodeTypeToolDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.CodeTypeToolDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.CodeTypeToolDelete.Name = "CodeTypeToolDelete";
			this.CodeTypeToolDelete.Size = new System.Drawing.Size(23, 22);
			this.CodeTypeToolDelete.Text = "Delete";
			this.CodeTypeToolDelete.Click += new System.EventHandler(this.CodeTypeToolDelete_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(170, 6);
			// 
			// CodeTypeModule
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.CodeTypeTabs);
			this.Name = "CodeTypeModule";
			this.Size = new System.Drawing.Size(800, 600);
			this.CodeTypeTabs.ResumeLayout(false);
			this.CodeTypePage.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.CodeTypeGrid)).EndInit();
			this.CodeTypeMenu.ResumeLayout(false);
			this.CodeTypeToolPanel.ResumeLayout(false);
			this.CodeTypeToolPanel.PerformLayout();
			this.CodeTypeTool.ResumeLayout(false);
			this.CodeTypeTool.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private LJCWinFormControls.LJCTabControl CodeTypeTabs;
		private System.Windows.Forms.TabPage CodeTypePage;
		private LJCWinFormControls.LJCDataGrid CodeTypeGrid;
		private System.Windows.Forms.Panel CodeTypeToolPanel;
		private System.Windows.Forms.ToolStrip CodeTypeTool;
		private System.Windows.Forms.ToolStripButton CodeTypeToolNew;
		private System.Windows.Forms.ToolStripButton CodeTypeToolEdit;
		private System.Windows.Forms.ToolStripButton CodeTypeToolDelete;
		private System.Windows.Forms.ContextMenuStrip CodeTypeMenu;
		private System.Windows.Forms.ToolStripMenuItem CodeTypeMenuNew;
		private System.Windows.Forms.ToolStripMenuItem CodeTypeMenuEdit;
		private System.Windows.Forms.ToolStripSeparator CodeTypeDeleteSeparator;
		private System.Windows.Forms.ToolStripMenuItem CodeTypeMenuDelete;
		private System.Windows.Forms.ToolStripSeparator CodeTypeRefreshSeparator;
		private System.Windows.Forms.ToolStripMenuItem CodeTypeMenuRefresh;
		private System.Windows.Forms.ToolStripSeparator CodeTypeCloseSeparator;
		private System.Windows.Forms.ToolStripMenuItem CodeTypeMenuClose;
		private System.Windows.Forms.ToolStripMenuItem CodeTypeMenuText;
		private System.Windows.Forms.ToolStripMenuItem CodeTypeMenuCSV;
		private System.Windows.Forms.Label CodeTypeCounter;
		private System.Windows.Forms.ToolStripMenuItem CodeTypeMenuHelp;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
	}
}
