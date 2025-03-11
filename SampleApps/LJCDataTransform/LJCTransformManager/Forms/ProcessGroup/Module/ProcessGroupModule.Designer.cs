namespace LJCTransformManager
{
	partial class ProcessGroupModule
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessGroupModule));
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      this.ProcessGroupTabs = new LJCWinFormControls.LJCTabControl(this.components);
      this.ProcessGroupPage = new System.Windows.Forms.TabPage();
      this.GroupSplit = new System.Windows.Forms.SplitContainer();
      this.ProcessGroupGrid = new LJCWinFormControls.LJCDataGrid(this.components);
      this.ProcessGroupMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.ProcessGroupNew = new System.Windows.Forms.ToolStripMenuItem();
      this.ProcessGroupEdit = new System.Windows.Forms.ToolStripMenuItem();
      this.AddressDeleteSeparator = new System.Windows.Forms.ToolStripSeparator();
      this.ProcessGroupDelete = new System.Windows.Forms.ToolStripMenuItem();
      this.AddressRefreshSeparator = new System.Windows.Forms.ToolStripSeparator();
      this.ProcessGroupRefresh = new System.Windows.Forms.ToolStripMenuItem();
      this.AddressCloseSeparator = new System.Windows.Forms.ToolStripSeparator();
      this.ProcessGroupExecute = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.ProcessGroupLog = new System.Windows.Forms.ToolStripMenuItem();
      this.ProcessGroupFileEdit = new System.Windows.Forms.ToolStripMenuItem();
      this.ProcessGroupClose = new System.Windows.Forms.ToolStripMenuItem();
      this.ProcessGroupToolPanel = new System.Windows.Forms.Panel();
      this.ProcessGroupCounter = new System.Windows.Forms.Label();
      this.ProcessGroupTool = new System.Windows.Forms.ToolStrip();
      this.ProcessGroupToolNew = new System.Windows.Forms.ToolStripButton();
      this.ProcessGroupToolEdit = new System.Windows.Forms.ToolStripButton();
      this.ProcessGroupToolDelete = new System.Windows.Forms.ToolStripButton();
      this.DataProcessGrid = new LJCWinFormControls.LJCDataGrid(this.components);
      this.DataProcessMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.DataProcessMenuNew = new System.Windows.Forms.ToolStripMenuItem();
      this.DataProcessEdit = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.DataProcessDelete = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.DataProcessRefresh = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.DataProcessLog = new System.Windows.Forms.ToolStripMenuItem();
      this.DataProcessClose = new System.Windows.Forms.ToolStripMenuItem();
      this.ProcessHeader = new LJCWinFormControls.LJCHeaderBox();
      this.DataProcessToolPanel = new System.Windows.Forms.Panel();
      this.DataProcessCounter = new System.Windows.Forms.Label();
      this.DataProcessTool = new System.Windows.Forms.ToolStrip();
      this.DataProcessToolNew = new System.Windows.Forms.ToolStripButton();
      this.DataProcessToolEdit = new System.Windows.Forms.ToolStripButton();
      this.DataProcessToolDelete = new System.Windows.Forms.ToolStripButton();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
      this.ProcessGroupTabs.SuspendLayout();
      this.ProcessGroupPage.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.GroupSplit)).BeginInit();
      this.GroupSplit.Panel1.SuspendLayout();
      this.GroupSplit.Panel2.SuspendLayout();
      this.GroupSplit.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.ProcessGroupGrid)).BeginInit();
      this.ProcessGroupMenu.SuspendLayout();
      this.ProcessGroupToolPanel.SuspendLayout();
      this.ProcessGroupTool.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.DataProcessGrid)).BeginInit();
      this.DataProcessMenu.SuspendLayout();
      this.DataProcessToolPanel.SuspendLayout();
      this.DataProcessTool.SuspendLayout();
      this.SuspendLayout();
      // 
      // ProcessGroupTabs
      // 
      this.ProcessGroupTabs.Controls.Add(this.ProcessGroupPage);
      this.ProcessGroupTabs.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ProcessGroupTabs.Location = new System.Drawing.Point(0, 0);
      this.ProcessGroupTabs.Name = "ProcessGroupTabs";
      this.ProcessGroupTabs.SelectedIndex = 0;
      this.ProcessGroupTabs.Size = new System.Drawing.Size(800, 600);
      this.ProcessGroupTabs.TabIndex = 0;
      // 
      // ProcessGroupPage
      // 
      this.ProcessGroupPage.Controls.Add(this.GroupSplit);
      this.ProcessGroupPage.Location = new System.Drawing.Point(4, 29);
      this.ProcessGroupPage.Name = "ProcessGroupPage";
      this.ProcessGroupPage.Padding = new System.Windows.Forms.Padding(3);
      this.ProcessGroupPage.Size = new System.Drawing.Size(792, 567);
      this.ProcessGroupPage.TabIndex = 0;
      this.ProcessGroupPage.Text = "Process Group";
      this.ProcessGroupPage.UseVisualStyleBackColor = true;
      // 
      // GroupSplit
      // 
      this.GroupSplit.Dock = System.Windows.Forms.DockStyle.Fill;
      this.GroupSplit.Location = new System.Drawing.Point(3, 3);
      this.GroupSplit.Name = "GroupSplit";
      this.GroupSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // GroupSplit.Panel1
      // 
      this.GroupSplit.Panel1.Controls.Add(this.ProcessGroupGrid);
      this.GroupSplit.Panel1.Controls.Add(this.ProcessGroupToolPanel);
      // 
      // GroupSplit.Panel2
      // 
      this.GroupSplit.Panel2.Controls.Add(this.DataProcessGrid);
      this.GroupSplit.Panel2.Controls.Add(this.ProcessHeader);
      this.GroupSplit.Panel2.Controls.Add(this.DataProcessToolPanel);
      this.GroupSplit.Size = new System.Drawing.Size(786, 561);
      this.GroupSplit.SplitterDistance = 267;
      this.GroupSplit.TabIndex = 1;
      // 
      // ProcessGroupGrid
      // 
      this.ProcessGroupGrid.AllowUserToAddRows = false;
      this.ProcessGroupGrid.AllowUserToDeleteRows = false;
      this.ProcessGroupGrid.AllowUserToResizeRows = false;
      this.ProcessGroupGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.ProcessGroupGrid.BackgroundColor = System.Drawing.Color.AliceBlue;
      this.ProcessGroupGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.ProcessGroupGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.ProcessGroupGrid.ContextMenuStrip = this.ProcessGroupMenu;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.ProcessGroupGrid.DefaultCellStyle = dataGridViewCellStyle1;
      this.ProcessGroupGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
      this.ProcessGroupGrid.LJCAllowSelectionChange = false;
      this.ProcessGroupGrid.LJCLastRowIndex = -1;
      this.ProcessGroupGrid.LJCRowHeight = 0;
      this.ProcessGroupGrid.Location = new System.Drawing.Point(0, 39);
      this.ProcessGroupGrid.MultiSelect = false;
      this.ProcessGroupGrid.Name = "ProcessGroupGrid";
      this.ProcessGroupGrid.RowHeadersVisible = false;
      this.ProcessGroupGrid.RowHeadersWidth = 62;
      this.ProcessGroupGrid.RowTemplate.Height = 28;
      this.ProcessGroupGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.ProcessGroupGrid.ShowCellToolTips = false;
      this.ProcessGroupGrid.Size = new System.Drawing.Size(786, 227);
      this.ProcessGroupGrid.TabIndex = 2;
      this.ProcessGroupGrid.Text = "LJCDataGrid";
      this.ProcessGroupGrid.SelectionChanged += new System.EventHandler(this.ProcessGroupGrid_SelectionChanged);
      this.ProcessGroupGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProcessGroupGrid_KeyDown);
      this.ProcessGroupGrid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ProcessGroupGrid_MouseDoubleClick);
      this.ProcessGroupGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ProcessGroupGrid_MouseDown);
      // 
      // ProcessGroupMenu
      // 
      this.ProcessGroupMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.ProcessGroupMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.ProcessGroupNew,
            this.ProcessGroupEdit,
            this.AddressDeleteSeparator,
            this.ProcessGroupDelete,
            this.AddressRefreshSeparator,
            this.ProcessGroupRefresh,
            this.AddressCloseSeparator,
            this.ProcessGroupExecute,
            this.toolStripSeparator4,
            this.ProcessGroupLog,
            this.ProcessGroupFileEdit,
            this.ProcessGroupClose});
      this.ProcessGroupMenu.Name = "mFacilityMenu";
      this.ProcessGroupMenu.Size = new System.Drawing.Size(264, 316);
      // 
      // ProcessGroupNew
      // 
      this.ProcessGroupNew.Name = "ProcessGroupNew";
      this.ProcessGroupNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
      this.ProcessGroupNew.Size = new System.Drawing.Size(263, 32);
      this.ProcessGroupNew.Text = "&New";
      this.ProcessGroupNew.Click += new System.EventHandler(this.ProcessGroupNew_Click);
      // 
      // ProcessGroupEdit
      // 
      this.ProcessGroupEdit.Name = "ProcessGroupEdit";
      this.ProcessGroupEdit.ShortcutKeyDisplayString = "Enter";
      this.ProcessGroupEdit.Size = new System.Drawing.Size(263, 32);
      this.ProcessGroupEdit.Text = "&Edit";
      this.ProcessGroupEdit.Click += new System.EventHandler(this.ProcessGroupEdit_Click);
      // 
      // AddressDeleteSeparator
      // 
      this.AddressDeleteSeparator.Name = "AddressDeleteSeparator";
      this.AddressDeleteSeparator.Size = new System.Drawing.Size(260, 6);
      // 
      // ProcessGroupDelete
      // 
      this.ProcessGroupDelete.Name = "ProcessGroupDelete";
      this.ProcessGroupDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
      this.ProcessGroupDelete.Size = new System.Drawing.Size(263, 32);
      this.ProcessGroupDelete.Text = "&Delete";
      this.ProcessGroupDelete.Click += new System.EventHandler(this.ProcessGroupDelete_Click);
      // 
      // AddressRefreshSeparator
      // 
      this.AddressRefreshSeparator.Name = "AddressRefreshSeparator";
      this.AddressRefreshSeparator.Size = new System.Drawing.Size(260, 6);
      // 
      // ProcessGroupRefresh
      // 
      this.ProcessGroupRefresh.Name = "ProcessGroupRefresh";
      this.ProcessGroupRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
      this.ProcessGroupRefresh.Size = new System.Drawing.Size(263, 32);
      this.ProcessGroupRefresh.Text = "&Refresh";
      this.ProcessGroupRefresh.Click += new System.EventHandler(this.ProcessGroupRefresh_Click);
      // 
      // AddressCloseSeparator
      // 
      this.AddressCloseSeparator.Name = "AddressCloseSeparator";
      this.AddressCloseSeparator.Size = new System.Drawing.Size(260, 6);
      // 
      // ProcessGroupExecute
      // 
      this.ProcessGroupExecute.Name = "ProcessGroupExecute";
      this.ProcessGroupExecute.Size = new System.Drawing.Size(263, 32);
      this.ProcessGroupExecute.Text = "Execute Process Group";
      this.ProcessGroupExecute.Click += new System.EventHandler(this.ProcessGroupExecute_Click);
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(260, 6);
      // 
      // ProcessGroupLog
      // 
      this.ProcessGroupLog.Name = "ProcessGroupLog";
      this.ProcessGroupLog.Size = new System.Drawing.Size(263, 32);
      this.ProcessGroupLog.Text = "Show Group Log";
      this.ProcessGroupLog.Click += new System.EventHandler(this.ProcessGroupLog_Click);
      // 
      // ProcessGroupFileEdit
      // 
      this.ProcessGroupFileEdit.Name = "ProcessGroupFileEdit";
      this.ProcessGroupFileEdit.Size = new System.Drawing.Size(263, 32);
      this.ProcessGroupFileEdit.Text = "F&ile Edit";
      this.ProcessGroupFileEdit.Click += new System.EventHandler(this.ProcessGroupFileEdit_Click);
      // 
      // ProcessGroupClose
      // 
      this.ProcessGroupClose.Name = "ProcessGroupClose";
      this.ProcessGroupClose.Size = new System.Drawing.Size(263, 32);
      this.ProcessGroupClose.Text = "&Close";
      this.ProcessGroupClose.Click += new System.EventHandler(this.ProcessGroupClose_Click);
      // 
      // ProcessGroupToolPanel
      // 
      this.ProcessGroupToolPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.ProcessGroupToolPanel.BackColor = System.Drawing.SystemColors.Control;
      this.ProcessGroupToolPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.ProcessGroupToolPanel.Controls.Add(this.ProcessGroupCounter);
      this.ProcessGroupToolPanel.Controls.Add(this.ProcessGroupTool);
      this.ProcessGroupToolPanel.Location = new System.Drawing.Point(0, 0);
      this.ProcessGroupToolPanel.Name = "ProcessGroupToolPanel";
      this.ProcessGroupToolPanel.Size = new System.Drawing.Size(786, 40);
      this.ProcessGroupToolPanel.TabIndex = 1;
      // 
      // ProcessGroupCounter
      // 
      this.ProcessGroupCounter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.ProcessGroupCounter.Location = new System.Drawing.Point(531, 13);
      this.ProcessGroupCounter.Name = "ProcessGroupCounter";
      this.ProcessGroupCounter.Size = new System.Drawing.Size(250, 23);
      this.ProcessGroupCounter.TabIndex = 1;
      this.ProcessGroupCounter.Text = "Row 0 of 0";
      this.ProcessGroupCounter.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // ProcessGroupTool
      // 
      this.ProcessGroupTool.Dock = System.Windows.Forms.DockStyle.None;
      this.ProcessGroupTool.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.ProcessGroupTool.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.ProcessGroupTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProcessGroupToolNew,
            this.ProcessGroupToolEdit,
            this.ProcessGroupToolDelete});
      this.ProcessGroupTool.Location = new System.Drawing.Point(3, 2);
      this.ProcessGroupTool.Name = "ProcessGroupTool";
      this.ProcessGroupTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.ProcessGroupTool.Size = new System.Drawing.Size(106, 27);
      this.ProcessGroupTool.TabIndex = 0;
      this.ProcessGroupTool.Text = "FirstTool";
      // 
      // ProcessGroupToolNew
      // 
      this.ProcessGroupToolNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.ProcessGroupToolNew.Image = ((System.Drawing.Image)(resources.GetObject("ProcessGroupToolNew.Image")));
      this.ProcessGroupToolNew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.ProcessGroupToolNew.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.ProcessGroupToolNew.Name = "ProcessGroupToolNew";
      this.ProcessGroupToolNew.Size = new System.Drawing.Size(34, 22);
      this.ProcessGroupToolNew.Text = "New";
      this.ProcessGroupToolNew.Click += new System.EventHandler(this.ProcessGroupToolNew_Click);
      // 
      // ProcessGroupToolEdit
      // 
      this.ProcessGroupToolEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.ProcessGroupToolEdit.Image = ((System.Drawing.Image)(resources.GetObject("ProcessGroupToolEdit.Image")));
      this.ProcessGroupToolEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.ProcessGroupToolEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.ProcessGroupToolEdit.Name = "ProcessGroupToolEdit";
      this.ProcessGroupToolEdit.Size = new System.Drawing.Size(34, 22);
      this.ProcessGroupToolEdit.Text = "Edit";
      this.ProcessGroupToolEdit.Click += new System.EventHandler(this.ProcessGroupToolEdit_Click);
      // 
      // ProcessGroupToolDelete
      // 
      this.ProcessGroupToolDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.ProcessGroupToolDelete.Image = ((System.Drawing.Image)(resources.GetObject("ProcessGroupToolDelete.Image")));
      this.ProcessGroupToolDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.ProcessGroupToolDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.ProcessGroupToolDelete.Name = "ProcessGroupToolDelete";
      this.ProcessGroupToolDelete.Size = new System.Drawing.Size(34, 22);
      this.ProcessGroupToolDelete.Text = "Delete";
      this.ProcessGroupToolDelete.Click += new System.EventHandler(this.ProcessGroupToolDelete_Click);
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
      dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
      dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
      dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.DataProcessGrid.DefaultCellStyle = dataGridViewCellStyle2;
      this.DataProcessGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
      this.DataProcessGrid.LJCAllowSelectionChange = false;
      this.DataProcessGrid.LJCLastRowIndex = -1;
      this.DataProcessGrid.LJCRowHeight = 0;
      this.DataProcessGrid.Location = new System.Drawing.Point(0, 70);
      this.DataProcessGrid.MultiSelect = false;
      this.DataProcessGrid.Name = "DataProcessGrid";
      this.DataProcessGrid.RowHeadersVisible = false;
      this.DataProcessGrid.RowHeadersWidth = 62;
      this.DataProcessGrid.RowTemplate.Height = 28;
      this.DataProcessGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.DataProcessGrid.ShowCellToolTips = false;
      this.DataProcessGrid.Size = new System.Drawing.Size(786, 220);
      this.DataProcessGrid.TabIndex = 5;
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
            this.toolStripMenuItem2,
            this.DataProcessMenuNew,
            this.DataProcessEdit,
            this.toolStripSeparator1,
            this.DataProcessDelete,
            this.toolStripSeparator2,
            this.DataProcessRefresh,
            this.toolStripSeparator3,
            this.DataProcessLog,
            this.DataProcessClose});
      this.DataProcessMenu.Name = "mFacilityMenu";
      this.DataProcessMenu.Size = new System.Drawing.Size(232, 246);
      // 
      // DataProcessMenuNew
      // 
      this.DataProcessMenuNew.Name = "DataProcessMenuNew";
      this.DataProcessMenuNew.Size = new System.Drawing.Size(231, 32);
      this.DataProcessMenuNew.Text = "&Add";
      this.DataProcessMenuNew.Click += new System.EventHandler(this.DataProcessMenuNew_Click);
      // 
      // DataProcessEdit
      // 
      this.DataProcessEdit.Name = "DataProcessEdit";
      this.DataProcessEdit.ShortcutKeyDisplayString = "Enter";
      this.DataProcessEdit.Size = new System.Drawing.Size(231, 32);
      this.DataProcessEdit.Text = "&Edit";
      this.DataProcessEdit.Click += new System.EventHandler(this.DataProcessEdit_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(228, 6);
      // 
      // DataProcessDelete
      // 
      this.DataProcessDelete.Name = "DataProcessDelete";
      this.DataProcessDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
      this.DataProcessDelete.Size = new System.Drawing.Size(231, 32);
      this.DataProcessDelete.Text = "Re&move";
      this.DataProcessDelete.Click += new System.EventHandler(this.DataProcessDelete_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(228, 6);
      // 
      // DataProcessRefresh
      // 
      this.DataProcessRefresh.Name = "DataProcessRefresh";
      this.DataProcessRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
      this.DataProcessRefresh.Size = new System.Drawing.Size(231, 32);
      this.DataProcessRefresh.Text = "&Refresh";
      this.DataProcessRefresh.Click += new System.EventHandler(this.DataProcessRefresh_Click);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(228, 6);
      // 
      // DataProcessLog
      // 
      this.DataProcessLog.Name = "DataProcessLog";
      this.DataProcessLog.Size = new System.Drawing.Size(231, 32);
      this.DataProcessLog.Text = "Show Process &Log";
      this.DataProcessLog.Click += new System.EventHandler(this.DataProcessLog_Click);
      // 
      // DataProcessClose
      // 
      this.DataProcessClose.Name = "DataProcessClose";
      this.DataProcessClose.Size = new System.Drawing.Size(231, 32);
      this.DataProcessClose.Text = "&Close";
      this.DataProcessClose.Click += new System.EventHandler(this.DataProcessClose_Click);
      // 
      // ProcessHeader
      // 
      this.ProcessHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.ProcessHeader.LJCBeginColor = System.Drawing.Color.AliceBlue;
      this.ProcessHeader.LJCEndColor = System.Drawing.Color.SkyBlue;
      this.ProcessHeader.Location = new System.Drawing.Point(0, 1);
      this.ProcessHeader.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.ProcessHeader.Name = "ProcessHeader";
      this.ProcessHeader.Size = new System.Drawing.Size(786, 31);
      this.ProcessHeader.TabIndex = 3;
      this.ProcessHeader.TabStop = false;
      this.ProcessHeader.Text = "Transform Process";
      // 
      // DataProcessToolPanel
      // 
      this.DataProcessToolPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.DataProcessToolPanel.BackColor = System.Drawing.SystemColors.Control;
      this.DataProcessToolPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.DataProcessToolPanel.Controls.Add(this.DataProcessCounter);
      this.DataProcessToolPanel.Controls.Add(this.DataProcessTool);
      this.DataProcessToolPanel.Location = new System.Drawing.Point(0, 31);
      this.DataProcessToolPanel.Name = "DataProcessToolPanel";
      this.DataProcessToolPanel.Size = new System.Drawing.Size(786, 40);
      this.DataProcessToolPanel.TabIndex = 4;
      // 
      // DataProcessCounter
      // 
      this.DataProcessCounter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.DataProcessCounter.Location = new System.Drawing.Point(531, 13);
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
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
      this.toolStripMenuItem1.Enabled = false;
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(263, 32);
      this.toolStripMenuItem1.Text = "ProcessGroup Menu";
      // 
      // toolStripMenuItem2
      // 
      this.toolStripMenuItem2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
      this.toolStripMenuItem2.Enabled = false;
      this.toolStripMenuItem2.Name = "toolStripMenuItem2";
      this.toolStripMenuItem2.Size = new System.Drawing.Size(231, 32);
      this.toolStripMenuItem2.Text = "DataProcess Menu";
      // 
      // ProcessGroupModule
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.ProcessGroupTabs);
      this.Name = "ProcessGroupModule";
      this.Size = new System.Drawing.Size(800, 600);
      this.ProcessGroupTabs.ResumeLayout(false);
      this.ProcessGroupPage.ResumeLayout(false);
      this.GroupSplit.Panel1.ResumeLayout(false);
      this.GroupSplit.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.GroupSplit)).EndInit();
      this.GroupSplit.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.ProcessGroupGrid)).EndInit();
      this.ProcessGroupMenu.ResumeLayout(false);
      this.ProcessGroupToolPanel.ResumeLayout(false);
      this.ProcessGroupToolPanel.PerformLayout();
      this.ProcessGroupTool.ResumeLayout(false);
      this.ProcessGroupTool.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.DataProcessGrid)).EndInit();
      this.DataProcessMenu.ResumeLayout(false);
      this.DataProcessToolPanel.ResumeLayout(false);
      this.DataProcessToolPanel.PerformLayout();
      this.DataProcessTool.ResumeLayout(false);
      this.DataProcessTool.PerformLayout();
      this.ResumeLayout(false);

		}

		#endregion

		private LJCWinFormControls.LJCTabControl ProcessGroupTabs;
		private System.Windows.Forms.TabPage ProcessGroupPage;
		private System.Windows.Forms.SplitContainer GroupSplit;
		internal LJCWinFormControls.LJCDataGrid ProcessGroupGrid;
		private System.Windows.Forms.Panel ProcessGroupToolPanel;
		private System.Windows.Forms.Label ProcessGroupCounter;
		private System.Windows.Forms.ToolStrip ProcessGroupTool;
		private System.Windows.Forms.ToolStripButton ProcessGroupToolNew;
		private System.Windows.Forms.ToolStripButton ProcessGroupToolEdit;
		private System.Windows.Forms.ToolStripButton ProcessGroupToolDelete;
		internal LJCWinFormControls.LJCDataGrid DataProcessGrid;
		private LJCWinFormControls.LJCHeaderBox ProcessHeader;
		private System.Windows.Forms.Panel DataProcessToolPanel;
		private System.Windows.Forms.Label DataProcessCounter;
		private System.Windows.Forms.ToolStrip DataProcessTool;
		private System.Windows.Forms.ToolStripButton DataProcessToolNew;
		private System.Windows.Forms.ToolStripButton DataProcessToolEdit;
		private System.Windows.Forms.ToolStripButton DataProcessToolDelete;
		private System.Windows.Forms.ContextMenuStrip ProcessGroupMenu;
		private System.Windows.Forms.ToolStripMenuItem ProcessGroupNew;
		private System.Windows.Forms.ToolStripMenuItem ProcessGroupEdit;
		private System.Windows.Forms.ToolStripSeparator AddressDeleteSeparator;
		private System.Windows.Forms.ToolStripMenuItem ProcessGroupDelete;
		private System.Windows.Forms.ToolStripSeparator AddressRefreshSeparator;
		private System.Windows.Forms.ToolStripMenuItem ProcessGroupRefresh;
		private System.Windows.Forms.ToolStripSeparator AddressCloseSeparator;
		private System.Windows.Forms.ToolStripMenuItem ProcessGroupClose;
		private System.Windows.Forms.ContextMenuStrip DataProcessMenu;
		private System.Windows.Forms.ToolStripMenuItem DataProcessMenuNew;
		private System.Windows.Forms.ToolStripMenuItem DataProcessEdit;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem DataProcessDelete;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem DataProcessRefresh;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem DataProcessClose;
		private System.Windows.Forms.ToolStripMenuItem ProcessGroupExecute;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem DataProcessLog;
		private System.Windows.Forms.ToolStripMenuItem ProcessGroupLog;
		private System.Windows.Forms.ToolStripMenuItem ProcessGroupFileEdit;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
  }
}
