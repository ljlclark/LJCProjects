namespace LJCTransformManager
{
	partial class DataSourceList
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataSourceList));
      this.DataSourceGrid = new LJCWinFormControls.LJCDataGrid(this.components);
      this.DataSourceMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.DataSourceMenuNew = new System.Windows.Forms.ToolStripMenuItem();
      this.DataSourceMenuEdit = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.DataSourceMenuDelete = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.DataSourceMenuRefresh = new System.Windows.Forms.ToolStripMenuItem();
      this.DataSourceMenuSelect = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.DataSourceMenuClose = new System.Windows.Forms.ToolStripMenuItem();
      this.DataSourceToolPanel = new System.Windows.Forms.Panel();
      this.DataSourceCounter = new System.Windows.Forms.Label();
      this.DataSourceTool = new System.Windows.Forms.ToolStrip();
      this.DataSourceToolNew = new System.Windows.Forms.ToolStripButton();
      this.DataSourceToolEdit = new System.Windows.Forms.ToolStripButton();
      this.DataSourceToolDelete = new System.Windows.Forms.ToolStripButton();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      ((System.ComponentModel.ISupportInitialize)(this.DataSourceGrid)).BeginInit();
      this.DataSourceMenu.SuspendLayout();
      this.DataSourceToolPanel.SuspendLayout();
      this.DataSourceTool.SuspendLayout();
      this.SuspendLayout();
      // 
      // DataSourceGrid
      // 
      this.DataSourceGrid.AllowUserToAddRows = false;
      this.DataSourceGrid.AllowUserToDeleteRows = false;
      this.DataSourceGrid.AllowUserToResizeRows = false;
      this.DataSourceGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.DataSourceGrid.BackgroundColor = System.Drawing.Color.AliceBlue;
      this.DataSourceGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.DataSourceGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.DataSourceGrid.ContextMenuStrip = this.DataSourceMenu;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.DataSourceGrid.DefaultCellStyle = dataGridViewCellStyle1;
      this.DataSourceGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
      this.DataSourceGrid.LJCAllowSelectionChange = false;
      this.DataSourceGrid.LJCLastRowIndex = -1;
      this.DataSourceGrid.LJCRowHeight = 0;
      this.DataSourceGrid.Location = new System.Drawing.Point(0, 39);
      this.DataSourceGrid.MultiSelect = false;
      this.DataSourceGrid.Name = "DataSourceGrid";
      this.DataSourceGrid.RowHeadersVisible = false;
      this.DataSourceGrid.RowHeadersWidth = 62;
      this.DataSourceGrid.RowTemplate.Height = 28;
      this.DataSourceGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.DataSourceGrid.ShowCellToolTips = false;
      this.DataSourceGrid.Size = new System.Drawing.Size(778, 505);
      this.DataSourceGrid.TabIndex = 3;
      this.DataSourceGrid.Text = "LJCDataGrid";
      this.DataSourceGrid.SelectionChanged += new System.EventHandler(this.DataSourceGrid_SelectionChanged);
      this.DataSourceGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataSourceGrid_KeyDown);
      this.DataSourceGrid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DataSourceGrid_MouseDoubleClick);
      this.DataSourceGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DataSourceGrid_MouseDown);
      // 
      // DataSourceMenu
      // 
      this.DataSourceMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.DataSourceMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.DataSourceMenuNew,
            this.DataSourceMenuEdit,
            this.toolStripSeparator1,
            this.DataSourceMenuDelete,
            this.toolStripSeparator2,
            this.DataSourceMenuRefresh,
            this.DataSourceMenuSelect,
            this.toolStripSeparator3,
            this.DataSourceMenuClose});
      this.DataSourceMenu.Name = "mFacilityMenu";
      this.DataSourceMenu.Size = new System.Drawing.Size(241, 279);
      // 
      // DataSourceMenuNew
      // 
      this.DataSourceMenuNew.Name = "DataSourceMenuNew";
      this.DataSourceMenuNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
      this.DataSourceMenuNew.Size = new System.Drawing.Size(240, 32);
      this.DataSourceMenuNew.Text = "&New";
      this.DataSourceMenuNew.Click += new System.EventHandler(this.DataSourceMenuNew_Click);
      // 
      // DataSourceMenuEdit
      // 
      this.DataSourceMenuEdit.Name = "DataSourceMenuEdit";
      this.DataSourceMenuEdit.ShortcutKeyDisplayString = "Enter";
      this.DataSourceMenuEdit.Size = new System.Drawing.Size(240, 32);
      this.DataSourceMenuEdit.Text = "&Edit";
      this.DataSourceMenuEdit.Click += new System.EventHandler(this.DataSourceMenuEdit_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(237, 6);
      // 
      // DataSourceMenuDelete
      // 
      this.DataSourceMenuDelete.Name = "DataSourceMenuDelete";
      this.DataSourceMenuDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
      this.DataSourceMenuDelete.Size = new System.Drawing.Size(240, 32);
      this.DataSourceMenuDelete.Text = "&Delete";
      this.DataSourceMenuDelete.Click += new System.EventHandler(this.DataSourceMenuDelete_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(237, 6);
      // 
      // DataSourceMenuRefresh
      // 
      this.DataSourceMenuRefresh.Name = "DataSourceMenuRefresh";
      this.DataSourceMenuRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
      this.DataSourceMenuRefresh.Size = new System.Drawing.Size(240, 32);
      this.DataSourceMenuRefresh.Text = "&Refresh";
      this.DataSourceMenuRefresh.Click += new System.EventHandler(this.DataSourceMenuRefresh_Click);
      // 
      // DataSourceMenuSelect
      // 
      this.DataSourceMenuSelect.Name = "DataSourceMenuSelect";
      this.DataSourceMenuSelect.Size = new System.Drawing.Size(240, 32);
      this.DataSourceMenuSelect.Text = "&Select";
      this.DataSourceMenuSelect.Click += new System.EventHandler(this.DataSourceMenuSelect_Click);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(237, 6);
      // 
      // DataSourceMenuClose
      // 
      this.DataSourceMenuClose.Name = "DataSourceMenuClose";
      this.DataSourceMenuClose.Size = new System.Drawing.Size(240, 32);
      this.DataSourceMenuClose.Text = "&Close";
      this.DataSourceMenuClose.Click += new System.EventHandler(this.DataSourceMenuClose_Click);
      // 
      // DataSourceToolPanel
      // 
      this.DataSourceToolPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.DataSourceToolPanel.BackColor = System.Drawing.SystemColors.Control;
      this.DataSourceToolPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.DataSourceToolPanel.Controls.Add(this.DataSourceCounter);
      this.DataSourceToolPanel.Controls.Add(this.DataSourceTool);
      this.DataSourceToolPanel.Location = new System.Drawing.Point(0, 0);
      this.DataSourceToolPanel.Name = "DataSourceToolPanel";
      this.DataSourceToolPanel.Size = new System.Drawing.Size(778, 40);
      this.DataSourceToolPanel.TabIndex = 2;
      // 
      // DataSourceCounter
      // 
      this.DataSourceCounter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.DataSourceCounter.Location = new System.Drawing.Point(523, 13);
      this.DataSourceCounter.Name = "DataSourceCounter";
      this.DataSourceCounter.Size = new System.Drawing.Size(250, 23);
      this.DataSourceCounter.TabIndex = 1;
      this.DataSourceCounter.Text = "Row 0 of 0";
      this.DataSourceCounter.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // DataSourceTool
      // 
      this.DataSourceTool.Dock = System.Windows.Forms.DockStyle.None;
      this.DataSourceTool.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.DataSourceTool.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.DataSourceTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DataSourceToolNew,
            this.DataSourceToolEdit,
            this.DataSourceToolDelete});
      this.DataSourceTool.Location = new System.Drawing.Point(3, 2);
      this.DataSourceTool.Name = "DataSourceTool";
      this.DataSourceTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.DataSourceTool.Size = new System.Drawing.Size(106, 27);
      this.DataSourceTool.TabIndex = 0;
      this.DataSourceTool.Text = "FirstTool";
      // 
      // DataSourceToolNew
      // 
      this.DataSourceToolNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.DataSourceToolNew.Image = ((System.Drawing.Image)(resources.GetObject("DataSourceToolNew.Image")));
      this.DataSourceToolNew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.DataSourceToolNew.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.DataSourceToolNew.Name = "DataSourceToolNew";
      this.DataSourceToolNew.Size = new System.Drawing.Size(34, 22);
      this.DataSourceToolNew.Text = "New";
      this.DataSourceToolNew.Click += new System.EventHandler(this.DataSourceToolNew_Click);
      // 
      // DataSourceToolEdit
      // 
      this.DataSourceToolEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.DataSourceToolEdit.Image = ((System.Drawing.Image)(resources.GetObject("DataSourceToolEdit.Image")));
      this.DataSourceToolEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.DataSourceToolEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.DataSourceToolEdit.Name = "DataSourceToolEdit";
      this.DataSourceToolEdit.Size = new System.Drawing.Size(34, 22);
      this.DataSourceToolEdit.Text = "Edit";
      this.DataSourceToolEdit.Click += new System.EventHandler(this.DataSourceToolEdit_Click);
      // 
      // DataSourceToolDelete
      // 
      this.DataSourceToolDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.DataSourceToolDelete.Image = ((System.Drawing.Image)(resources.GetObject("DataSourceToolDelete.Image")));
      this.DataSourceToolDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.DataSourceToolDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.DataSourceToolDelete.Name = "DataSourceToolDelete";
      this.DataSourceToolDelete.Size = new System.Drawing.Size(34, 22);
      this.DataSourceToolDelete.Text = "Delete";
      this.DataSourceToolDelete.Click += new System.EventHandler(this.DataSourceToolDelete_Click);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
      this.toolStripMenuItem1.Enabled = false;
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(240, 32);
      this.toolStripMenuItem1.Text = "DataSource Menu";
      // 
      // DataSourceList
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(778, 544);
      this.Controls.Add(this.DataSourceGrid);
      this.Controls.Add(this.DataSourceToolPanel);
      this.Name = "DataSourceList";
      this.Text = "Data Source List";
      this.Load += new System.EventHandler(this.DataSourceList_Load);
      ((System.ComponentModel.ISupportInitialize)(this.DataSourceGrid)).EndInit();
      this.DataSourceMenu.ResumeLayout(false);
      this.DataSourceToolPanel.ResumeLayout(false);
      this.DataSourceToolPanel.PerformLayout();
      this.DataSourceTool.ResumeLayout(false);
      this.DataSourceTool.PerformLayout();
      this.ResumeLayout(false);

		}

		#endregion

		internal LJCWinFormControls.LJCDataGrid DataSourceGrid;
		private System.Windows.Forms.Panel DataSourceToolPanel;
		private System.Windows.Forms.Label DataSourceCounter;
		private System.Windows.Forms.ToolStrip DataSourceTool;
		private System.Windows.Forms.ToolStripButton DataSourceToolNew;
		private System.Windows.Forms.ToolStripButton DataSourceToolEdit;
		private System.Windows.Forms.ToolStripButton DataSourceToolDelete;
		private System.Windows.Forms.ContextMenuStrip DataSourceMenu;
		private System.Windows.Forms.ToolStripMenuItem DataSourceMenuNew;
		private System.Windows.Forms.ToolStripMenuItem DataSourceMenuEdit;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem DataSourceMenuDelete;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem DataSourceMenuRefresh;
		private System.Windows.Forms.ToolStripMenuItem DataSourceMenuSelect;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem DataSourceMenuClose;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
  }
}