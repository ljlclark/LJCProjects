namespace LJCAppManager
{
	partial class ModuleList
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModuleList));
			this.ModuleTools = new System.Windows.Forms.ToolStrip();
			this.ModuleToolNew = new System.Windows.Forms.ToolStripButton();
			this.ModuleToolEdit = new System.Windows.Forms.ToolStripButton();
			this.ModuleToolDelete = new System.Windows.Forms.ToolStripButton();
			this.ModuleGrid = new LJCWinFormControls.LJCDataGrid(this.components);
			this.ModuleMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ModuleMenuNew = new System.Windows.Forms.ToolStripMenuItem();
			this.ModuleMenuEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.ModuleDeleteSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.ModuleMenuDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.ModuleRefreshSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.ModuleMenuRefresh = new System.Windows.Forms.ToolStripMenuItem();
			this.ModuleMenuSelect = new System.Windows.Forms.ToolStripMenuItem();
			this.ModuleCloseSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.ModuleMenuClose = new System.Windows.Forms.ToolStripMenuItem();
			this.ModuleToolPanel = new System.Windows.Forms.Panel();
			this.ModuleCounter = new System.Windows.Forms.Label();
			this.ModuleTools.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ModuleGrid)).BeginInit();
			this.ModuleMenu.SuspendLayout();
			this.ModuleToolPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// ModuleTools
			// 
			this.ModuleTools.Dock = System.Windows.Forms.DockStyle.None;
			this.ModuleTools.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.ModuleTools.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.ModuleTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ModuleToolNew,
            this.ModuleToolEdit,
            this.ModuleToolDelete});
			this.ModuleTools.Location = new System.Drawing.Point(3, 2);
			this.ModuleTools.Name = "ModuleTools";
			this.ModuleTools.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
			this.ModuleTools.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.ModuleTools.Size = new System.Drawing.Size(73, 25);
			this.ModuleTools.TabIndex = 3;
			this.ModuleTools.Text = "toolStrip1";
			// 
			// ModuleToolNew
			// 
			this.ModuleToolNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ModuleToolNew.Image = ((System.Drawing.Image)(resources.GetObject("ModuleToolNew.Image")));
			this.ModuleToolNew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.ModuleToolNew.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ModuleToolNew.Name = "ModuleToolNew";
			this.ModuleToolNew.Size = new System.Drawing.Size(23, 22);
			this.ModuleToolNew.Text = "New";
			this.ModuleToolNew.Click += new System.EventHandler(this.ModuleToolNew_Click);
			// 
			// ModuleToolEdit
			// 
			this.ModuleToolEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ModuleToolEdit.Image = ((System.Drawing.Image)(resources.GetObject("ModuleToolEdit.Image")));
			this.ModuleToolEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.ModuleToolEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ModuleToolEdit.Name = "ModuleToolEdit";
			this.ModuleToolEdit.Size = new System.Drawing.Size(23, 22);
			this.ModuleToolEdit.Text = "Edit";
			this.ModuleToolEdit.Click += new System.EventHandler(this.ModuleToolEdit_Click);
			// 
			// ModuleToolDelete
			// 
			this.ModuleToolDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ModuleToolDelete.Image = ((System.Drawing.Image)(resources.GetObject("ModuleToolDelete.Image")));
			this.ModuleToolDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.ModuleToolDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ModuleToolDelete.Name = "ModuleToolDelete";
			this.ModuleToolDelete.Size = new System.Drawing.Size(23, 22);
			this.ModuleToolDelete.Text = "Delete";
			this.ModuleToolDelete.Click += new System.EventHandler(this.ModuleToolDelete_Click);
			// 
			// ModuleGrid
			// 
			this.ModuleGrid.AllowUserToAddRows = false;
			this.ModuleGrid.AllowUserToDeleteRows = false;
			this.ModuleGrid.AllowUserToResizeRows = false;
			this.ModuleGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ModuleGrid.BackgroundColor = System.Drawing.Color.AliceBlue;
			this.ModuleGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.ModuleGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.ModuleGrid.ContextMenuStrip = this.ModuleMenu;
			this.ModuleGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.ModuleGrid.LJCAllowSelectionChange = false;
			this.ModuleGrid.LJCLastRowIndex = -1;
			this.ModuleGrid.Location = new System.Drawing.Point(0, 39);
			this.ModuleGrid.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ModuleGrid.MultiSelect = false;
			this.ModuleGrid.Name = "ModuleGrid";
			this.ModuleGrid.RowHeadersVisible = false;
			this.ModuleGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.ModuleGrid.ShowCellToolTips = false;
			this.ModuleGrid.Size = new System.Drawing.Size(778, 505);
			this.ModuleGrid.TabIndex = 8;
			this.ModuleGrid.Text = "LJCDataGrid";
			this.ModuleGrid.SelectionChanged += new System.EventHandler(this.ModuleGrid_SelectionChanged);
			this.ModuleGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ModuleGrid_KeyDown);
			this.ModuleGrid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ModuleGrid_MouseDoubleClick);
			this.ModuleGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ModuleGrid_MouseDown);
			// 
			// ModuleMenu
			// 
			this.ModuleMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.ModuleMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ModuleMenuNew,
            this.ModuleMenuEdit,
            this.ModuleDeleteSeparator,
            this.ModuleMenuDelete,
            this.ModuleRefreshSeparator,
            this.ModuleMenuRefresh,
            this.ModuleMenuSelect,
            this.ModuleCloseSeparator,
            this.ModuleMenuClose});
			this.ModuleMenu.Name = "mFacilityMenu";
			this.ModuleMenu.Size = new System.Drawing.Size(174, 202);
			// 
			// ModuleMenuNew
			// 
			this.ModuleMenuNew.Name = "ModuleMenuNew";
			this.ModuleMenuNew.Size = new System.Drawing.Size(173, 30);
			this.ModuleMenuNew.Text = "&New";
			this.ModuleMenuNew.Click += new System.EventHandler(this.ModuleMenuNew_Click);
			// 
			// ModuleMenuEdit
			// 
			this.ModuleMenuEdit.Name = "ModuleMenuEdit";
			this.ModuleMenuEdit.ShortcutKeyDisplayString = "Enter";
			this.ModuleMenuEdit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.ModuleMenuEdit.Size = new System.Drawing.Size(173, 30);
			this.ModuleMenuEdit.Text = "&Edit";
			this.ModuleMenuEdit.Click += new System.EventHandler(this.ModuleMenuEdit_Click);
			// 
			// ModuleDeleteSeparator
			// 
			this.ModuleDeleteSeparator.Name = "ModuleDeleteSeparator";
			this.ModuleDeleteSeparator.Size = new System.Drawing.Size(170, 6);
			// 
			// ModuleMenuDelete
			// 
			this.ModuleMenuDelete.Name = "ModuleMenuDelete";
			this.ModuleMenuDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.ModuleMenuDelete.Size = new System.Drawing.Size(173, 30);
			this.ModuleMenuDelete.Text = "&Delete";
			this.ModuleMenuDelete.Click += new System.EventHandler(this.ModuleMenuDelete_Click);
			// 
			// ModuleRefreshSeparator
			// 
			this.ModuleRefreshSeparator.Name = "ModuleRefreshSeparator";
			this.ModuleRefreshSeparator.Size = new System.Drawing.Size(170, 6);
			// 
			// ModuleMenuRefresh
			// 
			this.ModuleMenuRefresh.Name = "ModuleMenuRefresh";
			this.ModuleMenuRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
			this.ModuleMenuRefresh.Size = new System.Drawing.Size(173, 30);
			this.ModuleMenuRefresh.Text = "&Refresh";
			this.ModuleMenuRefresh.Click += new System.EventHandler(this.ModuleMenuRefresh_Click);
			// 
			// ModuleMenuSelect
			// 
			this.ModuleMenuSelect.Name = "ModuleMenuSelect";
			this.ModuleMenuSelect.Size = new System.Drawing.Size(173, 30);
			this.ModuleMenuSelect.Text = "&Select";
			this.ModuleMenuSelect.Click += new System.EventHandler(this.ModuleMenuSelect_Click);
			// 
			// ModuleCloseSeparator
			// 
			this.ModuleCloseSeparator.Name = "ModuleCloseSeparator";
			this.ModuleCloseSeparator.Size = new System.Drawing.Size(170, 6);
			// 
			// ModuleMenuClose
			// 
			this.ModuleMenuClose.Name = "ModuleMenuClose";
			this.ModuleMenuClose.Size = new System.Drawing.Size(173, 30);
			this.ModuleMenuClose.Text = "&Close";
			this.ModuleMenuClose.Click += new System.EventHandler(this.ModuleMenuClose_Click);
			// 
			// ModuleToolPanel
			// 
			this.ModuleToolPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ModuleToolPanel.BackColor = System.Drawing.SystemColors.Control;
			this.ModuleToolPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ModuleToolPanel.Controls.Add(this.ModuleCounter);
			this.ModuleToolPanel.Controls.Add(this.ModuleTools);
			this.ModuleToolPanel.Location = new System.Drawing.Point(0, 0);
			this.ModuleToolPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ModuleToolPanel.Name = "ModuleToolPanel";
			this.ModuleToolPanel.Size = new System.Drawing.Size(778, 40);
			this.ModuleToolPanel.TabIndex = 7;
			// 
			// ModuleCounter
			// 
			this.ModuleCounter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ModuleCounter.Location = new System.Drawing.Point(523, 13);
			this.ModuleCounter.Name = "ModuleCounter";
			this.ModuleCounter.Size = new System.Drawing.Size(250, 23);
			this.ModuleCounter.TabIndex = 6;
			this.ModuleCounter.Text = "Row 0 of 0";
			this.ModuleCounter.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// ModuleList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(778, 544);
			this.Controls.Add(this.ModuleGrid);
			this.Controls.Add(this.ModuleToolPanel);
			this.Name = "ModuleList";
			this.Text = "Module List";
			this.Load += new System.EventHandler(this.ModuleList_Load);
			this.ModuleTools.ResumeLayout(false);
			this.ModuleTools.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.ModuleGrid)).EndInit();
			this.ModuleMenu.ResumeLayout(false);
			this.ModuleToolPanel.ResumeLayout(false);
			this.ModuleToolPanel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolStrip ModuleTools;
		private System.Windows.Forms.ToolStripButton ModuleToolNew;
		private System.Windows.Forms.ToolStripButton ModuleToolEdit;
		private System.Windows.Forms.ToolStripButton ModuleToolDelete;
		private LJCWinFormControls.LJCDataGrid ModuleGrid;
		private System.Windows.Forms.Panel ModuleToolPanel;
		private System.Windows.Forms.Label ModuleCounter;
		private System.Windows.Forms.ContextMenuStrip ModuleMenu;
		private System.Windows.Forms.ToolStripMenuItem ModuleMenuNew;
		private System.Windows.Forms.ToolStripMenuItem ModuleMenuEdit;
		private System.Windows.Forms.ToolStripSeparator ModuleDeleteSeparator;
		private System.Windows.Forms.ToolStripMenuItem ModuleMenuDelete;
		private System.Windows.Forms.ToolStripSeparator ModuleRefreshSeparator;
		private System.Windows.Forms.ToolStripMenuItem ModuleMenuRefresh;
		private System.Windows.Forms.ToolStripMenuItem ModuleMenuSelect;
		private System.Windows.Forms.ToolStripSeparator ModuleCloseSeparator;
		private System.Windows.Forms.ToolStripMenuItem ModuleMenuClose;
	}
}