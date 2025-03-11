namespace LJCTransformManager
{
	partial class StepList
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StepList));
      this.StepGrid = new LJCWinFormControls.LJCDataGrid(this.components);
      this.StepMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.StepMenuNew = new System.Windows.Forms.ToolStripMenuItem();
      this.StepMenuEdit = new System.Windows.Forms.ToolStripMenuItem();
      this.DeleteSeparator = new System.Windows.Forms.ToolStripSeparator();
      this.StepMenuDelete = new System.Windows.Forms.ToolStripMenuItem();
      this.RefreshSeparator = new System.Windows.Forms.ToolStripSeparator();
      this.StepMenuRefresh = new System.Windows.Forms.ToolStripMenuItem();
      this.CloseSeparator = new System.Windows.Forms.ToolStripSeparator();
      this.StepMenuSelect = new System.Windows.Forms.ToolStripMenuItem();
      this.StepMenuSeparator = new System.Windows.Forms.ToolStripSeparator();
      this.StepMenuClose = new System.Windows.Forms.ToolStripMenuItem();
      this.StepToolPanel = new System.Windows.Forms.Panel();
      this.StepCounter = new System.Windows.Forms.Label();
      this.StepTool = new System.Windows.Forms.ToolStrip();
      this.StepToolNew = new System.Windows.Forms.ToolStripButton();
      this.StepToolEdit = new System.Windows.Forms.ToolStripButton();
      this.StepToolDelete = new System.Windows.Forms.ToolStripButton();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      ((System.ComponentModel.ISupportInitialize)(this.StepGrid)).BeginInit();
      this.StepMenu.SuspendLayout();
      this.StepToolPanel.SuspendLayout();
      this.StepTool.SuspendLayout();
      this.SuspendLayout();
      // 
      // StepGrid
      // 
      this.StepGrid.AllowUserToAddRows = false;
      this.StepGrid.AllowUserToDeleteRows = false;
      this.StepGrid.AllowUserToResizeRows = false;
      this.StepGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.StepGrid.BackgroundColor = System.Drawing.Color.AliceBlue;
      this.StepGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.StepGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.StepGrid.ContextMenuStrip = this.StepMenu;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.StepGrid.DefaultCellStyle = dataGridViewCellStyle1;
      this.StepGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
      this.StepGrid.LJCAllowSelectionChange = false;
      this.StepGrid.LJCLastRowIndex = -1;
      this.StepGrid.LJCRowHeight = 0;
      this.StepGrid.Location = new System.Drawing.Point(0, 39);
      this.StepGrid.MultiSelect = false;
      this.StepGrid.Name = "StepGrid";
      this.StepGrid.RowHeadersVisible = false;
      this.StepGrid.RowHeadersWidth = 62;
      this.StepGrid.RowTemplate.Height = 28;
      this.StepGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.StepGrid.ShowCellToolTips = false;
      this.StepGrid.Size = new System.Drawing.Size(778, 505);
      this.StepGrid.TabIndex = 1;
      this.StepGrid.Text = "LJCDataGrid";
      this.StepGrid.SelectionChanged += new System.EventHandler(this.StepGrid_SelectionChanged);
      this.StepGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.StepGrid_KeyDown);
      this.StepGrid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.StepGrid_MouseDoubleClick);
      this.StepGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.StepGrid_MouseDown);
      // 
      // StepMenu
      // 
      this.StepMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.StepMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.StepMenuNew,
            this.StepMenuEdit,
            this.DeleteSeparator,
            this.StepMenuDelete,
            this.RefreshSeparator,
            this.StepMenuRefresh,
            this.CloseSeparator,
            this.StepMenuSelect,
            this.StepMenuSeparator,
            this.StepMenuClose});
      this.StepMenu.Name = "mFacilityMenu";
      this.StepMenu.Size = new System.Drawing.Size(241, 285);
      // 
      // StepMenuNew
      // 
      this.StepMenuNew.Name = "StepMenuNew";
      this.StepMenuNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
      this.StepMenuNew.Size = new System.Drawing.Size(240, 32);
      this.StepMenuNew.Text = "&New";
      this.StepMenuNew.Click += new System.EventHandler(this.StepMenuNew_Click);
      // 
      // StepMenuEdit
      // 
      this.StepMenuEdit.Name = "StepMenuEdit";
      this.StepMenuEdit.ShortcutKeyDisplayString = "Enter";
      this.StepMenuEdit.Size = new System.Drawing.Size(240, 32);
      this.StepMenuEdit.Text = "&Edit";
      this.StepMenuEdit.Click += new System.EventHandler(this.StepMenuEdit_Click);
      // 
      // DeleteSeparator
      // 
      this.DeleteSeparator.Name = "DeleteSeparator";
      this.DeleteSeparator.Size = new System.Drawing.Size(237, 6);
      // 
      // StepMenuDelete
      // 
      this.StepMenuDelete.Name = "StepMenuDelete";
      this.StepMenuDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
      this.StepMenuDelete.Size = new System.Drawing.Size(240, 32);
      this.StepMenuDelete.Text = "&Delete";
      this.StepMenuDelete.Click += new System.EventHandler(this.StepMenuDelete_Click);
      // 
      // RefreshSeparator
      // 
      this.RefreshSeparator.Name = "RefreshSeparator";
      this.RefreshSeparator.Size = new System.Drawing.Size(237, 6);
      // 
      // StepMenuRefresh
      // 
      this.StepMenuRefresh.Name = "StepMenuRefresh";
      this.StepMenuRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
      this.StepMenuRefresh.Size = new System.Drawing.Size(240, 32);
      this.StepMenuRefresh.Text = "&Refresh";
      this.StepMenuRefresh.Click += new System.EventHandler(this.StepMenuRefresh_Click);
      // 
      // CloseSeparator
      // 
      this.CloseSeparator.Name = "CloseSeparator";
      this.CloseSeparator.Size = new System.Drawing.Size(237, 6);
      // 
      // StepMenuSelect
      // 
      this.StepMenuSelect.Name = "StepMenuSelect";
      this.StepMenuSelect.Size = new System.Drawing.Size(240, 32);
      this.StepMenuSelect.Text = "&Select";
      // 
      // StepMenuSeparator
      // 
      this.StepMenuSeparator.Name = "StepMenuSeparator";
      this.StepMenuSeparator.Size = new System.Drawing.Size(237, 6);
      // 
      // StepMenuClose
      // 
      this.StepMenuClose.Name = "StepMenuClose";
      this.StepMenuClose.Size = new System.Drawing.Size(240, 32);
      this.StepMenuClose.Text = "&Close";
      this.StepMenuClose.Click += new System.EventHandler(this.StepMenuClose_Click);
      // 
      // StepToolPanel
      // 
      this.StepToolPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.StepToolPanel.BackColor = System.Drawing.SystemColors.Control;
      this.StepToolPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.StepToolPanel.Controls.Add(this.StepCounter);
      this.StepToolPanel.Controls.Add(this.StepTool);
      this.StepToolPanel.Location = new System.Drawing.Point(0, 0);
      this.StepToolPanel.Name = "StepToolPanel";
      this.StepToolPanel.Size = new System.Drawing.Size(778, 40);
      this.StepToolPanel.TabIndex = 0;
      // 
      // StepCounter
      // 
      this.StepCounter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.StepCounter.Location = new System.Drawing.Point(523, 13);
      this.StepCounter.Name = "StepCounter";
      this.StepCounter.Size = new System.Drawing.Size(250, 23);
      this.StepCounter.TabIndex = 1;
      this.StepCounter.Text = "Row 0 of 0";
      this.StepCounter.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // StepTool
      // 
      this.StepTool.Dock = System.Windows.Forms.DockStyle.None;
      this.StepTool.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.StepTool.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.StepTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StepToolNew,
            this.StepToolEdit,
            this.StepToolDelete});
      this.StepTool.Location = new System.Drawing.Point(3, 2);
      this.StepTool.Name = "StepTool";
      this.StepTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.StepTool.Size = new System.Drawing.Size(106, 27);
      this.StepTool.TabIndex = 0;
      this.StepTool.Text = "FirstTool";
      // 
      // StepToolNew
      // 
      this.StepToolNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.StepToolNew.Image = ((System.Drawing.Image)(resources.GetObject("StepToolNew.Image")));
      this.StepToolNew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.StepToolNew.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.StepToolNew.Name = "StepToolNew";
      this.StepToolNew.Size = new System.Drawing.Size(34, 22);
      this.StepToolNew.Text = "New";
      this.StepToolNew.Click += new System.EventHandler(this.StepToolNew_Click);
      // 
      // StepToolEdit
      // 
      this.StepToolEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.StepToolEdit.Image = ((System.Drawing.Image)(resources.GetObject("StepToolEdit.Image")));
      this.StepToolEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.StepToolEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.StepToolEdit.Name = "StepToolEdit";
      this.StepToolEdit.Size = new System.Drawing.Size(34, 22);
      this.StepToolEdit.Text = "Edit";
      this.StepToolEdit.Click += new System.EventHandler(this.StepToolEdit_Click);
      // 
      // StepToolDelete
      // 
      this.StepToolDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.StepToolDelete.Image = ((System.Drawing.Image)(resources.GetObject("StepToolDelete.Image")));
      this.StepToolDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.StepToolDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.StepToolDelete.Name = "StepToolDelete";
      this.StepToolDelete.Size = new System.Drawing.Size(34, 22);
      this.StepToolDelete.Text = "Delete";
      this.StepToolDelete.Click += new System.EventHandler(this.StepDelete_Click);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
      this.toolStripMenuItem1.Enabled = false;
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(240, 32);
      this.toolStripMenuItem1.Text = "Step Menu";
      // 
      // StepList
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(778, 544);
      this.Controls.Add(this.StepGrid);
      this.Controls.Add(this.StepToolPanel);
      this.Name = "StepList";
      this.Text = "Step List";
      this.Load += new System.EventHandler(this.StepList_Load);
      ((System.ComponentModel.ISupportInitialize)(this.StepGrid)).EndInit();
      this.StepMenu.ResumeLayout(false);
      this.StepToolPanel.ResumeLayout(false);
      this.StepToolPanel.PerformLayout();
      this.StepTool.ResumeLayout(false);
      this.StepTool.PerformLayout();
      this.ResumeLayout(false);

		}

		#endregion

		internal LJCWinFormControls.LJCDataGrid StepGrid;
		private System.Windows.Forms.Panel StepToolPanel;
		private System.Windows.Forms.Label StepCounter;
		private System.Windows.Forms.ToolStrip StepTool;
		private System.Windows.Forms.ToolStripButton StepToolNew;
		private System.Windows.Forms.ToolStripButton StepToolEdit;
		private System.Windows.Forms.ToolStripButton StepToolDelete;
		private System.Windows.Forms.ContextMenuStrip StepMenu;
		private System.Windows.Forms.ToolStripMenuItem StepMenuNew;
		private System.Windows.Forms.ToolStripMenuItem StepMenuEdit;
		private System.Windows.Forms.ToolStripSeparator DeleteSeparator;
		private System.Windows.Forms.ToolStripMenuItem StepMenuDelete;
		private System.Windows.Forms.ToolStripSeparator RefreshSeparator;
		private System.Windows.Forms.ToolStripMenuItem StepMenuRefresh;
		private System.Windows.Forms.ToolStripSeparator CloseSeparator;
		private System.Windows.Forms.ToolStripMenuItem StepMenuClose;
		private System.Windows.Forms.ToolStripMenuItem StepMenuSelect;
		private System.Windows.Forms.ToolStripSeparator StepMenuSeparator;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
  }
}