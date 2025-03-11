namespace LJCAppManager
{
	partial class ProgramList
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgramList));
			this.ProgramGrid = new LJCWinFormControls.LJCDataGrid(this.components);
			this.ProgramMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ProgramMenuNew = new System.Windows.Forms.ToolStripMenuItem();
			this.ProgramMenuEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.ProgramDeleteSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.ProgramMenuDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.ProgramRefreshSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.ProgramMenuRefresh = new System.Windows.Forms.ToolStripMenuItem();
			this.ProgramMenuSelect = new System.Windows.Forms.ToolStripMenuItem();
			this.ProgramCloseSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.ProgramMenuClose = new System.Windows.Forms.ToolStripMenuItem();
			this.ProgramTools = new System.Windows.Forms.ToolStrip();
			this.ProgramToolNew = new System.Windows.Forms.ToolStripButton();
			this.ProgramToolEdit = new System.Windows.Forms.ToolStripButton();
			this.ProgramToolDelete = new System.Windows.Forms.ToolStripButton();
			this.ProgramToolPanel = new System.Windows.Forms.Panel();
			this.ProgramCounter = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.ProgramGrid)).BeginInit();
			this.ProgramMenu.SuspendLayout();
			this.ProgramTools.SuspendLayout();
			this.ProgramToolPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// ProgramGrid
			// 
			this.ProgramGrid.AllowUserToAddRows = false;
			this.ProgramGrid.AllowUserToDeleteRows = false;
			this.ProgramGrid.AllowUserToResizeRows = false;
			this.ProgramGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ProgramGrid.BackgroundColor = System.Drawing.Color.AliceBlue;
			this.ProgramGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.ProgramGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.ProgramGrid.ContextMenuStrip = this.ProgramMenu;
			this.ProgramGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.ProgramGrid.LJCAllowSelectionChange = false;
			this.ProgramGrid.LJCLastRowIndex = -1;
			this.ProgramGrid.Location = new System.Drawing.Point(0, 39);
			this.ProgramGrid.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ProgramGrid.MultiSelect = false;
			this.ProgramGrid.Name = "ProgramGrid";
			this.ProgramGrid.RowHeadersVisible = false;
			this.ProgramGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.ProgramGrid.ShowCellToolTips = false;
			this.ProgramGrid.Size = new System.Drawing.Size(778, 505);
			this.ProgramGrid.TabIndex = 6;
			this.ProgramGrid.Text = "LJCDataGrid";
			this.ProgramGrid.SelectionChanged += new System.EventHandler(this.ProgramGrid_SelectionChanged);
			this.ProgramGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProgramGrid_KeyDown);
			this.ProgramGrid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ProgramGrid_MouseDoubleClick);
			this.ProgramGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ProgramGrid_MouseDown);
			// 
			// ProgramMenu
			// 
			this.ProgramMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.ProgramMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProgramMenuNew,
            this.ProgramMenuEdit,
            this.ProgramDeleteSeparator,
            this.ProgramMenuDelete,
            this.ProgramRefreshSeparator,
            this.ProgramMenuRefresh,
            this.ProgramMenuSelect,
            this.ProgramCloseSeparator,
            this.ProgramMenuClose});
			this.ProgramMenu.Name = "mFacilityMenu";
			this.ProgramMenu.Size = new System.Drawing.Size(174, 202);
			// 
			// ProgramMenuNew
			// 
			this.ProgramMenuNew.Name = "ProgramMenuNew";
			this.ProgramMenuNew.Size = new System.Drawing.Size(173, 30);
			this.ProgramMenuNew.Text = "&New";
			this.ProgramMenuNew.Click += new System.EventHandler(this.ProgramMenuNew_Click);
			// 
			// ProgramMenuEdit
			// 
			this.ProgramMenuEdit.Name = "ProgramMenuEdit";
			this.ProgramMenuEdit.ShortcutKeyDisplayString = "Enter";
			this.ProgramMenuEdit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.ProgramMenuEdit.Size = new System.Drawing.Size(173, 30);
			this.ProgramMenuEdit.Text = "&Edit";
			this.ProgramMenuEdit.Click += new System.EventHandler(this.ProgramMenuEdit_Click);
			// 
			// ProgramDeleteSeparator
			// 
			this.ProgramDeleteSeparator.Name = "ProgramDeleteSeparator";
			this.ProgramDeleteSeparator.Size = new System.Drawing.Size(170, 6);
			// 
			// ProgramMenuDelete
			// 
			this.ProgramMenuDelete.Name = "ProgramMenuDelete";
			this.ProgramMenuDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.ProgramMenuDelete.Size = new System.Drawing.Size(173, 30);
			this.ProgramMenuDelete.Text = "&Delete";
			this.ProgramMenuDelete.Click += new System.EventHandler(this.ProgramMenuDelete_Click);
			// 
			// ProgramRefreshSeparator
			// 
			this.ProgramRefreshSeparator.Name = "ProgramRefreshSeparator";
			this.ProgramRefreshSeparator.Size = new System.Drawing.Size(170, 6);
			// 
			// ProgramMenuRefresh
			// 
			this.ProgramMenuRefresh.Name = "ProgramMenuRefresh";
			this.ProgramMenuRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
			this.ProgramMenuRefresh.Size = new System.Drawing.Size(173, 30);
			this.ProgramMenuRefresh.Text = "&Refresh";
			this.ProgramMenuRefresh.Click += new System.EventHandler(this.ProgramMenuRefresh_Click);
			// 
			// ProgramMenuSelect
			// 
			this.ProgramMenuSelect.Name = "ProgramMenuSelect";
			this.ProgramMenuSelect.Size = new System.Drawing.Size(173, 30);
			this.ProgramMenuSelect.Text = "&Select";
			this.ProgramMenuSelect.Click += new System.EventHandler(this.ProgramMenuSelect_Click);
			// 
			// ProgramCloseSeparator
			// 
			this.ProgramCloseSeparator.Name = "ProgramCloseSeparator";
			this.ProgramCloseSeparator.Size = new System.Drawing.Size(170, 6);
			// 
			// ProgramMenuClose
			// 
			this.ProgramMenuClose.Name = "ProgramMenuClose";
			this.ProgramMenuClose.Size = new System.Drawing.Size(173, 30);
			this.ProgramMenuClose.Text = "&Close";
			this.ProgramMenuClose.Click += new System.EventHandler(this.ProgramMenuClose_Click);
			// 
			// ProgramTools
			// 
			this.ProgramTools.Dock = System.Windows.Forms.DockStyle.None;
			this.ProgramTools.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.ProgramTools.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.ProgramTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProgramToolNew,
            this.ProgramToolEdit,
            this.ProgramToolDelete});
			this.ProgramTools.Location = new System.Drawing.Point(3, 2);
			this.ProgramTools.Name = "ProgramTools";
			this.ProgramTools.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
			this.ProgramTools.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.ProgramTools.Size = new System.Drawing.Size(73, 25);
			this.ProgramTools.TabIndex = 3;
			this.ProgramTools.Text = "toolStrip1";
			// 
			// ProgramToolNew
			// 
			this.ProgramToolNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ProgramToolNew.Image = ((System.Drawing.Image)(resources.GetObject("ProgramToolNew.Image")));
			this.ProgramToolNew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.ProgramToolNew.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ProgramToolNew.Name = "ProgramToolNew";
			this.ProgramToolNew.Size = new System.Drawing.Size(23, 22);
			this.ProgramToolNew.Text = "New";
			this.ProgramToolNew.Click += new System.EventHandler(this.ProgramToolNew_Click);
			// 
			// ProgramToolEdit
			// 
			this.ProgramToolEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ProgramToolEdit.Image = ((System.Drawing.Image)(resources.GetObject("ProgramToolEdit.Image")));
			this.ProgramToolEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.ProgramToolEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ProgramToolEdit.Name = "ProgramToolEdit";
			this.ProgramToolEdit.Size = new System.Drawing.Size(23, 22);
			this.ProgramToolEdit.Text = "Edit";
			this.ProgramToolEdit.Click += new System.EventHandler(this.ProgramToolEdit_Click);
			// 
			// ProgramToolDelete
			// 
			this.ProgramToolDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ProgramToolDelete.Image = ((System.Drawing.Image)(resources.GetObject("ProgramToolDelete.Image")));
			this.ProgramToolDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.ProgramToolDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ProgramToolDelete.Name = "ProgramToolDelete";
			this.ProgramToolDelete.Size = new System.Drawing.Size(23, 22);
			this.ProgramToolDelete.Text = "Delete";
			this.ProgramToolDelete.Click += new System.EventHandler(this.ProgramToolDelete_Click);
			// 
			// ProgramToolPanel
			// 
			this.ProgramToolPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ProgramToolPanel.BackColor = System.Drawing.SystemColors.Control;
			this.ProgramToolPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ProgramToolPanel.Controls.Add(this.ProgramCounter);
			this.ProgramToolPanel.Controls.Add(this.ProgramTools);
			this.ProgramToolPanel.Location = new System.Drawing.Point(0, 0);
			this.ProgramToolPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ProgramToolPanel.Name = "ProgramToolPanel";
			this.ProgramToolPanel.Size = new System.Drawing.Size(778, 40);
			this.ProgramToolPanel.TabIndex = 5;
			// 
			// ProgramCounter
			// 
			this.ProgramCounter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ProgramCounter.Location = new System.Drawing.Point(523, 13);
			this.ProgramCounter.Name = "ProgramCounter";
			this.ProgramCounter.Size = new System.Drawing.Size(250, 23);
			this.ProgramCounter.TabIndex = 6;
			this.ProgramCounter.Text = "Row 0 of 0";
			this.ProgramCounter.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// ProgramList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(778, 544);
			this.Controls.Add(this.ProgramGrid);
			this.Controls.Add(this.ProgramToolPanel);
			this.Name = "ProgramList";
			this.Text = "Program List";
			this.Load += new System.EventHandler(this.ProgramList_Load);
			((System.ComponentModel.ISupportInitialize)(this.ProgramGrid)).EndInit();
			this.ProgramMenu.ResumeLayout(false);
			this.ProgramTools.ResumeLayout(false);
			this.ProgramTools.PerformLayout();
			this.ProgramToolPanel.ResumeLayout(false);
			this.ProgramToolPanel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private LJCWinFormControls.LJCDataGrid ProgramGrid;
		private System.Windows.Forms.ToolStrip ProgramTools;
		private System.Windows.Forms.ToolStripButton ProgramToolNew;
		private System.Windows.Forms.ToolStripButton ProgramToolEdit;
		private System.Windows.Forms.ToolStripButton ProgramToolDelete;
		private System.Windows.Forms.Panel ProgramToolPanel;
		private System.Windows.Forms.Label ProgramCounter;
		private System.Windows.Forms.ContextMenuStrip ProgramMenu;
		private System.Windows.Forms.ToolStripMenuItem ProgramMenuNew;
		private System.Windows.Forms.ToolStripMenuItem ProgramMenuEdit;
		private System.Windows.Forms.ToolStripSeparator ProgramDeleteSeparator;
		private System.Windows.Forms.ToolStripMenuItem ProgramMenuDelete;
		private System.Windows.Forms.ToolStripSeparator ProgramRefreshSeparator;
		private System.Windows.Forms.ToolStripMenuItem ProgramMenuRefresh;
		private System.Windows.Forms.ToolStripMenuItem ProgramMenuSelect;
		private System.Windows.Forms.ToolStripSeparator ProgramCloseSeparator;
		private System.Windows.Forms.ToolStripMenuItem ProgramMenuClose;
	}
}