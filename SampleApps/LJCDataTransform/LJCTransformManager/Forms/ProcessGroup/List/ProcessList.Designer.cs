namespace LJCTransformManager
{
	partial class DataProcessList
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataProcessList));
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      this.DataProcessToolPanel = new System.Windows.Forms.Panel();
      this.DataProcessCounter = new System.Windows.Forms.Label();
      this.DataProcessTool = new System.Windows.Forms.ToolStrip();
      this.DataProcessToolNew = new System.Windows.Forms.ToolStripButton();
      this.DataProcessToolEdit = new System.Windows.Forms.ToolStripButton();
      this.DataProcessToolDelete = new System.Windows.Forms.ToolStripButton();
      this.DataProcessGrid = new LJCWinFormControls.LJCDataGrid(this.components);
      this.DataProcessMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.DataProcessMenuNew = new System.Windows.Forms.ToolStripMenuItem();
      this.DataProcessMenuEdit = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.DataProcessMenuDelete = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.DataProcessMenuRefresh = new System.Windows.Forms.ToolStripMenuItem();
      this.DataProcessMenuSelect = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.DataProcessMenuClose = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.DataProcessToolPanel.SuspendLayout();
      this.DataProcessTool.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.DataProcessGrid)).BeginInit();
      this.DataProcessMenu.SuspendLayout();
      this.SuspendLayout();
      // 
      // DataProcessToolPanel
      // 
      this.DataProcessToolPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.DataProcessToolPanel.BackColor = System.Drawing.SystemColors.Control;
      this.DataProcessToolPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.DataProcessToolPanel.Controls.Add(this.DataProcessCounter);
      this.DataProcessToolPanel.Controls.Add(this.DataProcessTool);
      this.DataProcessToolPanel.Location = new System.Drawing.Point(0, 0);
      this.DataProcessToolPanel.Name = "DataProcessToolPanel";
      this.DataProcessToolPanel.Size = new System.Drawing.Size(778, 40);
      this.DataProcessToolPanel.TabIndex = 0;
      // 
      // DataProcessCounter
      // 
      this.DataProcessCounter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.DataProcessCounter.Location = new System.Drawing.Point(523, 13);
      this.DataProcessCounter.Name = "DataProcessCounter";
      this.DataProcessCounter.Size = new System.Drawing.Size(250, 23);
      this.DataProcessCounter.TabIndex = 1;
      this.DataProcessCounter.Text = "Row 0 of 0";
      this.DataProcessCounter.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // DataProcessTool
      // 
      this.DataProcessTool.Dock = System.Windows.Forms.DockStyle.None;
      this.DataProcessTool.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.DataProcessTool.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.DataProcessTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DataProcessToolNew,
            this.DataProcessToolEdit,
            this.DataProcessToolDelete});
      this.DataProcessTool.Location = new System.Drawing.Point(3, 2);
      this.DataProcessTool.Name = "DataProcessTool";
      this.DataProcessTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.DataProcessTool.Size = new System.Drawing.Size(106, 27);
      this.DataProcessTool.TabIndex = 0;
      this.DataProcessTool.Text = "FirstTool";
      // 
      // DataProcessToolNew
      // 
      this.DataProcessToolNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.DataProcessToolNew.Image = ((System.Drawing.Image)(resources.GetObject("DataProcessToolNew.Image")));
      this.DataProcessToolNew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.DataProcessToolNew.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.DataProcessToolNew.Name = "DataProcessToolNew";
      this.DataProcessToolNew.Size = new System.Drawing.Size(34, 22);
      this.DataProcessToolNew.Text = "New";
      this.DataProcessToolNew.Click += new System.EventHandler(this.DataProcessToolNew_Click);
      // 
      // DataProcessToolEdit
      // 
      this.DataProcessToolEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.DataProcessToolEdit.Image = ((System.Drawing.Image)(resources.GetObject("DataProcessToolEdit.Image")));
      this.DataProcessToolEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.DataProcessToolEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.DataProcessToolEdit.Name = "DataProcessToolEdit";
      this.DataProcessToolEdit.Size = new System.Drawing.Size(34, 22);
      this.DataProcessToolEdit.Text = "Edit";
      this.DataProcessToolEdit.Click += new System.EventHandler(this.DataProcessToolEdit_Click);
      // 
      // DataProcessToolDelete
      // 
      this.DataProcessToolDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.DataProcessToolDelete.Image = ((System.Drawing.Image)(resources.GetObject("DataProcessToolDelete.Image")));
      this.DataProcessToolDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.DataProcessToolDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.DataProcessToolDelete.Name = "DataProcessToolDelete";
      this.DataProcessToolDelete.Size = new System.Drawing.Size(34, 22);
      this.DataProcessToolDelete.Text = "Delete";
      this.DataProcessToolDelete.Click += new System.EventHandler(this.DataProcessToolDelete_Click);
      // 
      // DataProcessGrid
      // 
      this.DataProcessGrid.AllowUserToAddRows = false;
      this.DataProcessGrid.AllowUserToDeleteRows = false;
      this.DataProcessGrid.AllowUserToResizeRows = false;
      this.DataProcessGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.DataProcessGrid.BackgroundColor = System.Drawing.Color.AliceBlue;
      this.DataProcessGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.DataProcessGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.DataProcessGrid.ContextMenuStrip = this.DataProcessMenu;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.DataProcessGrid.DefaultCellStyle = dataGridViewCellStyle1;
      this.DataProcessGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
      this.DataProcessGrid.LJCAllowSelectionChange = false;
      this.DataProcessGrid.LJCLastRowIndex = -1;
      this.DataProcessGrid.LJCRowHeight = 0;
      this.DataProcessGrid.Location = new System.Drawing.Point(0, 39);
      this.DataProcessGrid.MultiSelect = false;
      this.DataProcessGrid.Name = "DataProcessGrid";
      this.DataProcessGrid.RowHeadersVisible = false;
      this.DataProcessGrid.RowHeadersWidth = 62;
      this.DataProcessGrid.RowTemplate.Height = 28;
      this.DataProcessGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.DataProcessGrid.ShowCellToolTips = false;
      this.DataProcessGrid.Size = new System.Drawing.Size(778, 505);
      this.DataProcessGrid.TabIndex = 1;
      this.DataProcessGrid.Text = "LJCDataGrid";
      this.DataProcessGrid.SelectionChanged += new System.EventHandler(this.DataProcessGrid_SelectionChanged);
      this.DataProcessGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataProcessGrid_KeyDown);
      this.DataProcessGrid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DataProcessGrid_MouseDoubleClick);
      this.DataProcessGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DataProcessGrid_MouseDown);
      // 
      // DataProcessMenu
      // 
      this.DataProcessMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.DataProcessMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.DataProcessMenuNew,
            this.DataProcessMenuEdit,
            this.toolStripSeparator1,
            this.DataProcessMenuDelete,
            this.toolStripSeparator2,
            this.DataProcessMenuRefresh,
            this.DataProcessMenuSelect,
            this.toolStripSeparator3,
            this.DataProcessMenuClose});
      this.DataProcessMenu.Name = "mFacilityMenu";
      this.DataProcessMenu.Size = new System.Drawing.Size(232, 246);
      // 
      // DataProcessMenuNew
      // 
      this.DataProcessMenuNew.Name = "DataProcessMenuNew";
      this.DataProcessMenuNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
      this.DataProcessMenuNew.Size = new System.Drawing.Size(231, 32);
      this.DataProcessMenuNew.Text = "&New";
      this.DataProcessMenuNew.Click += new System.EventHandler(this.DataProcessMenuNew_Click);
      // 
      // DataProcessMenuEdit
      // 
      this.DataProcessMenuEdit.Name = "DataProcessMenuEdit";
      this.DataProcessMenuEdit.ShortcutKeyDisplayString = "Enter";
      this.DataProcessMenuEdit.Size = new System.Drawing.Size(231, 32);
      this.DataProcessMenuEdit.Text = "&Edit";
      this.DataProcessMenuEdit.Click += new System.EventHandler(this.DataProcessMenuEdit_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(228, 6);
      // 
      // DataProcessMenuDelete
      // 
      this.DataProcessMenuDelete.Name = "DataProcessMenuDelete";
      this.DataProcessMenuDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
      this.DataProcessMenuDelete.Size = new System.Drawing.Size(231, 32);
      this.DataProcessMenuDelete.Text = "&Delete";
      this.DataProcessMenuDelete.Click += new System.EventHandler(this.DataProcessMenuDelete_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(228, 6);
      // 
      // DataProcessMenuRefresh
      // 
      this.DataProcessMenuRefresh.Name = "DataProcessMenuRefresh";
      this.DataProcessMenuRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
      this.DataProcessMenuRefresh.Size = new System.Drawing.Size(231, 32);
      this.DataProcessMenuRefresh.Text = "&Refresh";
      this.DataProcessMenuRefresh.Click += new System.EventHandler(this.DataProcessMenuRefresh_Click);
      // 
      // DataProcessMenuSelect
      // 
      this.DataProcessMenuSelect.Name = "DataProcessMenuSelect";
      this.DataProcessMenuSelect.Size = new System.Drawing.Size(231, 32);
      this.DataProcessMenuSelect.Text = "&Select";
      this.DataProcessMenuSelect.Click += new System.EventHandler(this.DataProcessMenuSelect_Click);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(228, 6);
      // 
      // DataProcessMenuClose
      // 
      this.DataProcessMenuClose.Name = "DataProcessMenuClose";
      this.DataProcessMenuClose.Size = new System.Drawing.Size(231, 32);
      this.DataProcessMenuClose.Text = "&Close";
      this.DataProcessMenuClose.Click += new System.EventHandler(this.DataProcessMenuClose_Click);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
      this.toolStripMenuItem1.Enabled = false;
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(231, 32);
      this.toolStripMenuItem1.Text = "DataProcess Menu";
      // 
      // DataProcessList
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(778, 544);
      this.Controls.Add(this.DataProcessGrid);
      this.Controls.Add(this.DataProcessToolPanel);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "DataProcessList";
      this.ShowInTaskbar = false;
      this.Text = "Data Process List";
      this.Load += new System.EventHandler(this.ProcessList_Load);
      this.DataProcessToolPanel.ResumeLayout(false);
      this.DataProcessToolPanel.PerformLayout();
      this.DataProcessTool.ResumeLayout(false);
      this.DataProcessTool.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.DataProcessGrid)).EndInit();
      this.DataProcessMenu.ResumeLayout(false);
      this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel DataProcessToolPanel;
		private System.Windows.Forms.Label DataProcessCounter;
		private System.Windows.Forms.ToolStrip DataProcessTool;
		private System.Windows.Forms.ToolStripButton DataProcessToolNew;
		private System.Windows.Forms.ToolStripButton DataProcessToolEdit;
		private System.Windows.Forms.ToolStripButton DataProcessToolDelete;
		internal LJCWinFormControls.LJCDataGrid DataProcessGrid;
		private System.Windows.Forms.ContextMenuStrip DataProcessMenu;
		private System.Windows.Forms.ToolStripMenuItem DataProcessMenuNew;
		private System.Windows.Forms.ToolStripMenuItem DataProcessMenuEdit;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem DataProcessMenuDelete;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem DataProcessMenuRefresh;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem DataProcessMenuClose;
		private System.Windows.Forms.ToolStripMenuItem DataProcessMenuSelect;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
  }
}