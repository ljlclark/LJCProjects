namespace LJCTransformManager
{
	partial class SourceLayoutModule
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SourceLayoutModule));
      this.SourceLayoutTabs = new LJCWinFormControls.LJCTabControl(this.components);
      this.SourceLayoutPage = new System.Windows.Forms.TabPage();
      this.SourceLayoutLabel = new System.Windows.Forms.Label();
      this.SourceLayoutTextbox = new System.Windows.Forms.TextBox();
      this.LayoutColumnGrid = new LJCWinFormControls.LJCDataGrid(this.components);
      this.LayoutColumnMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.LayoutColumnMenuNew = new System.Windows.Forms.ToolStripMenuItem();
      this.LayoutColumnMenuEdit = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.LayoutColumnMenuDelete = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.LayoutColumnMenuRefresh = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.LayoutColumnMenuClose = new System.Windows.Forms.ToolStripMenuItem();
      this.LayoutColumnHeader = new LJCWinFormControls.LJCHeaderBox();
      this.LayoutColumnToolPanel = new System.Windows.Forms.Panel();
      this.LayoutColumnCounter = new System.Windows.Forms.Label();
      this.LayoutColumnTool = new System.Windows.Forms.ToolStrip();
      this.LayoutColumnToolNew = new System.Windows.Forms.ToolStripButton();
      this.LayoutColumnToolEdit = new System.Windows.Forms.ToolStripButton();
      this.LayoutColumnToolDelete = new System.Windows.Forms.ToolStripButton();
      this.SourceLayoutButton = new System.Windows.Forms.Button();
      this.ButtonImages = new System.Windows.Forms.ImageList(this.components);
      this.DataSourceLabel = new System.Windows.Forms.Label();
      this.DataSourceCombo = new LJCWinFormControls.LJCItemCombo();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.SourceLayoutTabs.SuspendLayout();
      this.SourceLayoutPage.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.LayoutColumnGrid)).BeginInit();
      this.LayoutColumnMenu.SuspendLayout();
      this.LayoutColumnToolPanel.SuspendLayout();
      this.LayoutColumnTool.SuspendLayout();
      this.SuspendLayout();
      // 
      // SourceLayoutTabs
      // 
      this.SourceLayoutTabs.Controls.Add(this.SourceLayoutPage);
      this.SourceLayoutTabs.Dock = System.Windows.Forms.DockStyle.Fill;
      this.SourceLayoutTabs.Location = new System.Drawing.Point(0, 0);
      this.SourceLayoutTabs.Name = "SourceLayoutTabs";
      this.SourceLayoutTabs.SelectedIndex = 0;
      this.SourceLayoutTabs.Size = new System.Drawing.Size(800, 600);
      this.SourceLayoutTabs.TabIndex = 0;
      // 
      // SourceLayoutPage
      // 
      this.SourceLayoutPage.Controls.Add(this.SourceLayoutLabel);
      this.SourceLayoutPage.Controls.Add(this.SourceLayoutTextbox);
      this.SourceLayoutPage.Controls.Add(this.LayoutColumnGrid);
      this.SourceLayoutPage.Controls.Add(this.LayoutColumnHeader);
      this.SourceLayoutPage.Controls.Add(this.LayoutColumnToolPanel);
      this.SourceLayoutPage.Controls.Add(this.SourceLayoutButton);
      this.SourceLayoutPage.Controls.Add(this.DataSourceLabel);
      this.SourceLayoutPage.Controls.Add(this.DataSourceCombo);
      this.SourceLayoutPage.Location = new System.Drawing.Point(4, 29);
      this.SourceLayoutPage.Name = "SourceLayoutPage";
      this.SourceLayoutPage.Padding = new System.Windows.Forms.Padding(3);
      this.SourceLayoutPage.Size = new System.Drawing.Size(792, 567);
      this.SourceLayoutPage.TabIndex = 0;
      this.SourceLayoutPage.Text = "Source Layout";
      this.SourceLayoutPage.UseVisualStyleBackColor = true;
      // 
      // SourceLayoutLabel
      // 
      this.SourceLayoutLabel.Location = new System.Drawing.Point(12, 51);
      this.SourceLayoutLabel.Name = "SourceLayoutLabel";
      this.SourceLayoutLabel.Size = new System.Drawing.Size(135, 20);
      this.SourceLayoutLabel.TabIndex = 2;
      this.SourceLayoutLabel.Text = "Source Layout";
      // 
      // SourceLayoutTextbox
      // 
      this.SourceLayoutTextbox.Location = new System.Drawing.Point(151, 48);
      this.SourceLayoutTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.SourceLayoutTextbox.Name = "SourceLayoutTextbox";
      this.SourceLayoutTextbox.Size = new System.Drawing.Size(330, 26);
      this.SourceLayoutTextbox.TabIndex = 3;
      // 
      // LayoutColumnGrid
      // 
      this.LayoutColumnGrid.AllowUserToAddRows = false;
      this.LayoutColumnGrid.AllowUserToDeleteRows = false;
      this.LayoutColumnGrid.AllowUserToResizeRows = false;
      this.LayoutColumnGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.LayoutColumnGrid.BackgroundColor = System.Drawing.Color.AliceBlue;
      this.LayoutColumnGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.LayoutColumnGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.LayoutColumnGrid.ContextMenuStrip = this.LayoutColumnMenu;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.LayoutColumnGrid.DefaultCellStyle = dataGridViewCellStyle1;
      this.LayoutColumnGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
      this.LayoutColumnGrid.LJCAllowSelectionChange = false;
      this.LayoutColumnGrid.LJCLastRowIndex = -1;
      this.LayoutColumnGrid.LJCRowHeight = 0;
      this.LayoutColumnGrid.Location = new System.Drawing.Point(1, 154);
      this.LayoutColumnGrid.MultiSelect = false;
      this.LayoutColumnGrid.Name = "LayoutColumnGrid";
      this.LayoutColumnGrid.RowHeadersVisible = false;
      this.LayoutColumnGrid.RowHeadersWidth = 62;
      this.LayoutColumnGrid.RowTemplate.Height = 28;
      this.LayoutColumnGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.LayoutColumnGrid.ShowCellToolTips = false;
      this.LayoutColumnGrid.Size = new System.Drawing.Size(791, 411);
      this.LayoutColumnGrid.TabIndex = 7;
      this.LayoutColumnGrid.Text = "LJCDataGrid";
      // 
      // LayoutColumnMenu
      // 
      this.LayoutColumnMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.LayoutColumnMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.LayoutColumnMenuNew,
            this.LayoutColumnMenuEdit,
            this.toolStripSeparator1,
            this.LayoutColumnMenuDelete,
            this.toolStripSeparator2,
            this.LayoutColumnMenuRefresh,
            this.toolStripSeparator3,
            this.LayoutColumnMenuClose});
      this.LayoutColumnMenu.Name = "mFacilityMenu";
      this.LayoutColumnMenu.Size = new System.Drawing.Size(250, 247);
      // 
      // LayoutColumnMenuNew
      // 
      this.LayoutColumnMenuNew.Name = "LayoutColumnMenuNew";
      this.LayoutColumnMenuNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
      this.LayoutColumnMenuNew.Size = new System.Drawing.Size(249, 32);
      this.LayoutColumnMenuNew.Text = "&New";
      this.LayoutColumnMenuNew.Click += new System.EventHandler(this.LayoutColumnMenuNew_Click);
      // 
      // LayoutColumnMenuEdit
      // 
      this.LayoutColumnMenuEdit.Name = "LayoutColumnMenuEdit";
      this.LayoutColumnMenuEdit.ShortcutKeyDisplayString = "Enter";
      this.LayoutColumnMenuEdit.Size = new System.Drawing.Size(249, 32);
      this.LayoutColumnMenuEdit.Text = "&Edit";
      this.LayoutColumnMenuEdit.Click += new System.EventHandler(this.LayoutColumnMenuEdit_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(246, 6);
      // 
      // LayoutColumnMenuDelete
      // 
      this.LayoutColumnMenuDelete.Name = "LayoutColumnMenuDelete";
      this.LayoutColumnMenuDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
      this.LayoutColumnMenuDelete.Size = new System.Drawing.Size(249, 32);
      this.LayoutColumnMenuDelete.Text = "&Delete";
      this.LayoutColumnMenuDelete.Click += new System.EventHandler(this.LayoutColumnMenuDelete_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(246, 6);
      // 
      // LayoutColumnMenuRefresh
      // 
      this.LayoutColumnMenuRefresh.Name = "LayoutColumnMenuRefresh";
      this.LayoutColumnMenuRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
      this.LayoutColumnMenuRefresh.Size = new System.Drawing.Size(249, 32);
      this.LayoutColumnMenuRefresh.Text = "&Refresh";
      this.LayoutColumnMenuRefresh.Click += new System.EventHandler(this.LayoutColumnMenuRefresh_Click);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(246, 6);
      // 
      // LayoutColumnMenuClose
      // 
      this.LayoutColumnMenuClose.Name = "LayoutColumnMenuClose";
      this.LayoutColumnMenuClose.Size = new System.Drawing.Size(249, 32);
      this.LayoutColumnMenuClose.Text = "&Close";
      this.LayoutColumnMenuClose.Click += new System.EventHandler(this.LayoutColumnMenuClose_Click);
      // 
      // LayoutColumnHeader
      // 
      this.LayoutColumnHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.LayoutColumnHeader.LJCBeginColor = System.Drawing.Color.AliceBlue;
      this.LayoutColumnHeader.LJCEndColor = System.Drawing.Color.SkyBlue;
      this.LayoutColumnHeader.Location = new System.Drawing.Point(1, 85);
      this.LayoutColumnHeader.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.LayoutColumnHeader.Name = "LayoutColumnHeader";
      this.LayoutColumnHeader.Size = new System.Drawing.Size(791, 31);
      this.LayoutColumnHeader.TabIndex = 5;
      this.LayoutColumnHeader.TabStop = false;
      this.LayoutColumnHeader.Text = "Layout Column";
      // 
      // LayoutColumnToolPanel
      // 
      this.LayoutColumnToolPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.LayoutColumnToolPanel.BackColor = System.Drawing.SystemColors.Control;
      this.LayoutColumnToolPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.LayoutColumnToolPanel.Controls.Add(this.LayoutColumnCounter);
      this.LayoutColumnToolPanel.Controls.Add(this.LayoutColumnTool);
      this.LayoutColumnToolPanel.Location = new System.Drawing.Point(1, 115);
      this.LayoutColumnToolPanel.Name = "LayoutColumnToolPanel";
      this.LayoutColumnToolPanel.Size = new System.Drawing.Size(791, 40);
      this.LayoutColumnToolPanel.TabIndex = 6;
      // 
      // LayoutColumnCounter
      // 
      this.LayoutColumnCounter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.LayoutColumnCounter.Location = new System.Drawing.Point(536, 13);
      this.LayoutColumnCounter.Name = "LayoutColumnCounter";
      this.LayoutColumnCounter.Size = new System.Drawing.Size(250, 23);
      this.LayoutColumnCounter.TabIndex = 1;
      this.LayoutColumnCounter.Text = "Row 0 of 0";
      this.LayoutColumnCounter.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // LayoutColumnTool
      // 
      this.LayoutColumnTool.Dock = System.Windows.Forms.DockStyle.None;
      this.LayoutColumnTool.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.LayoutColumnTool.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.LayoutColumnTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LayoutColumnToolNew,
            this.LayoutColumnToolEdit,
            this.LayoutColumnToolDelete});
      this.LayoutColumnTool.Location = new System.Drawing.Point(3, 2);
      this.LayoutColumnTool.Name = "LayoutColumnTool";
      this.LayoutColumnTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.LayoutColumnTool.Size = new System.Drawing.Size(106, 27);
      this.LayoutColumnTool.TabIndex = 0;
      this.LayoutColumnTool.Text = "FirstTool";
      // 
      // LayoutColumnToolNew
      // 
      this.LayoutColumnToolNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.LayoutColumnToolNew.Image = ((System.Drawing.Image)(resources.GetObject("LayoutColumnToolNew.Image")));
      this.LayoutColumnToolNew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.LayoutColumnToolNew.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.LayoutColumnToolNew.Name = "LayoutColumnToolNew";
      this.LayoutColumnToolNew.Size = new System.Drawing.Size(34, 22);
      this.LayoutColumnToolNew.Text = "New";
      // 
      // LayoutColumnToolEdit
      // 
      this.LayoutColumnToolEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.LayoutColumnToolEdit.Image = ((System.Drawing.Image)(resources.GetObject("LayoutColumnToolEdit.Image")));
      this.LayoutColumnToolEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.LayoutColumnToolEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.LayoutColumnToolEdit.Name = "LayoutColumnToolEdit";
      this.LayoutColumnToolEdit.Size = new System.Drawing.Size(34, 22);
      this.LayoutColumnToolEdit.Text = "Edit";
      // 
      // LayoutColumnToolDelete
      // 
      this.LayoutColumnToolDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.LayoutColumnToolDelete.Image = ((System.Drawing.Image)(resources.GetObject("LayoutColumnToolDelete.Image")));
      this.LayoutColumnToolDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.LayoutColumnToolDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.LayoutColumnToolDelete.Name = "LayoutColumnToolDelete";
      this.LayoutColumnToolDelete.Size = new System.Drawing.Size(34, 22);
      this.LayoutColumnToolDelete.Text = "Delete";
      // 
      // SourceLayoutButton
      // 
      this.SourceLayoutButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.SourceLayoutButton.ImageKey = "Ellipse.bmp";
      this.SourceLayoutButton.ImageList = this.ButtonImages;
      this.SourceLayoutButton.Location = new System.Drawing.Point(489, 46);
      this.SourceLayoutButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.SourceLayoutButton.Name = "SourceLayoutButton";
      this.SourceLayoutButton.Size = new System.Drawing.Size(28, 28);
      this.SourceLayoutButton.TabIndex = 4;
      this.SourceLayoutButton.UseVisualStyleBackColor = true;
      this.SourceLayoutButton.Click += new System.EventHandler(this.SourceLayoutButton_Click);
      // 
      // ButtonImages
      // 
      this.ButtonImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ButtonImages.ImageStream")));
      this.ButtonImages.TransparentColor = System.Drawing.Color.Magenta;
      this.ButtonImages.Images.SetKeyName(0, "Ellipse.bmp");
      this.ButtonImages.Images.SetKeyName(1, "Calendar.bmp");
      // 
      // DataSourceLabel
      // 
      this.DataSourceLabel.Location = new System.Drawing.Point(12, 15);
      this.DataSourceLabel.Name = "DataSourceLabel";
      this.DataSourceLabel.Size = new System.Drawing.Size(135, 20);
      this.DataSourceLabel.TabIndex = 0;
      this.DataSourceLabel.Text = "Data Source";
      // 
      // DataSourceCombo
      // 
      this.DataSourceCombo.FormattingEnabled = true;
      this.DataSourceCombo.Location = new System.Drawing.Point(151, 12);
      this.DataSourceCombo.Name = "DataSourceCombo";
      this.DataSourceCombo.Size = new System.Drawing.Size(330, 28);
      this.DataSourceCombo.TabIndex = 1;
      this.DataSourceCombo.SelectedIndexChanged += new System.EventHandler(this.DataSourceCombo_SelectedIndexChanged);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
      this.toolStripMenuItem1.Enabled = false;
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(249, 32);
      this.toolStripMenuItem1.Text = "LayoutColumn Menu";
      // 
      // SourceLayoutModule
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.SourceLayoutTabs);
      this.Name = "SourceLayoutModule";
      this.Size = new System.Drawing.Size(800, 600);
      this.SourceLayoutTabs.ResumeLayout(false);
      this.SourceLayoutPage.ResumeLayout(false);
      this.SourceLayoutPage.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.LayoutColumnGrid)).EndInit();
      this.LayoutColumnMenu.ResumeLayout(false);
      this.LayoutColumnToolPanel.ResumeLayout(false);
      this.LayoutColumnToolPanel.PerformLayout();
      this.LayoutColumnTool.ResumeLayout(false);
      this.LayoutColumnTool.PerformLayout();
      this.ResumeLayout(false);

		}

		#endregion

		private LJCWinFormControls.LJCTabControl SourceLayoutTabs;
		private System.Windows.Forms.TabPage SourceLayoutPage;
		private System.Windows.Forms.Label DataSourceLabel;
		private LJCWinFormControls.LJCItemCombo DataSourceCombo;
		private System.Windows.Forms.ContextMenuStrip LayoutColumnMenu;
		private System.Windows.Forms.ToolStripMenuItem LayoutColumnMenuNew;
		private System.Windows.Forms.ToolStripMenuItem LayoutColumnMenuEdit;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem LayoutColumnMenuDelete;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem LayoutColumnMenuRefresh;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem LayoutColumnMenuClose;
		private System.Windows.Forms.Button SourceLayoutButton;
		private System.Windows.Forms.ImageList ButtonImages;
		internal System.Windows.Forms.TextBox SourceLayoutTextbox;
		internal LJCWinFormControls.LJCDataGrid LayoutColumnGrid;
		private LJCWinFormControls.LJCHeaderBox LayoutColumnHeader;
		private System.Windows.Forms.Panel LayoutColumnToolPanel;
		private System.Windows.Forms.Label LayoutColumnCounter;
		private System.Windows.Forms.ToolStrip LayoutColumnTool;
		private System.Windows.Forms.ToolStripButton LayoutColumnToolNew;
		private System.Windows.Forms.ToolStripButton LayoutColumnToolEdit;
		private System.Windows.Forms.ToolStripButton LayoutColumnToolDelete;
		private System.Windows.Forms.Label SourceLayoutLabel;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
  }
}
