namespace LJCTransformManager
{
	partial class ProcessStepModule
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessStepModule));
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      this.ProcessStepTabs = new LJCWinFormControls.LJCTabControl(this.components);
      this.ProcessStepPage = new System.Windows.Forms.TabPage();
      this.DataProcessLabel = new System.Windows.Forms.Label();
      this.DataProcessCombo = new LJCWinFormControls.LJCItemCombo();
      this.StepSplit = new System.Windows.Forms.SplitContainer();
      this.StepHeader = new LJCWinFormControls.LJCHeaderBox();
      this.StepGrid = new LJCWinFormControls.LJCDataGrid(this.components);
      this.StepMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.StepMenuNew = new System.Windows.Forms.ToolStripMenuItem();
      this.StepMenuEdit = new System.Windows.Forms.ToolStripMenuItem();
      this.DeleteSeparator = new System.Windows.Forms.ToolStripSeparator();
      this.StepMenuDelete = new System.Windows.Forms.ToolStripMenuItem();
      this.RefreshSeparator = new System.Windows.Forms.ToolStripSeparator();
      this.StepMenuRefresh = new System.Windows.Forms.ToolStripMenuItem();
      this.CloseSeparator = new System.Windows.Forms.ToolStripSeparator();
      this.StepMenuClose = new System.Windows.Forms.ToolStripMenuItem();
      this.StepToolPanel = new System.Windows.Forms.Panel();
      this.StepCounter = new System.Windows.Forms.Label();
      this.StepTool = new System.Windows.Forms.ToolStrip();
      this.StepToolNew = new System.Windows.Forms.ToolStripButton();
      this.StepToolEdit = new System.Windows.Forms.ToolStripButton();
      this.StepToolDelete = new System.Windows.Forms.ToolStripButton();
      this.TaskGrid = new LJCWinFormControls.LJCDataGrid(this.components);
      this.TaskMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.TaskMenuNew = new System.Windows.Forms.ToolStripMenuItem();
      this.TaskMenuEdit = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.TaskMenuDelete = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.TaskMenuRefresh = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.TaskMenuClose = new System.Windows.Forms.ToolStripMenuItem();
      this.TaskHeader = new LJCWinFormControls.LJCHeaderBox();
      this.TaskToolPanel = new System.Windows.Forms.Panel();
      this.TaskCounter = new System.Windows.Forms.Label();
      this.TaskTool = new System.Windows.Forms.ToolStrip();
      this.TaskToolNew = new System.Windows.Forms.ToolStripButton();
      this.TaskToolEdit = new System.Windows.Forms.ToolStripButton();
      this.TaskToolDelete = new System.Windows.Forms.ToolStripButton();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
      this.ProcessStepTabs.SuspendLayout();
      this.ProcessStepPage.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.StepSplit)).BeginInit();
      this.StepSplit.Panel1.SuspendLayout();
      this.StepSplit.Panel2.SuspendLayout();
      this.StepSplit.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.StepGrid)).BeginInit();
      this.StepMenu.SuspendLayout();
      this.StepToolPanel.SuspendLayout();
      this.StepTool.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.TaskGrid)).BeginInit();
      this.TaskMenu.SuspendLayout();
      this.TaskToolPanel.SuspendLayout();
      this.TaskTool.SuspendLayout();
      this.SuspendLayout();
      // 
      // ProcessStepTabs
      // 
      this.ProcessStepTabs.Controls.Add(this.ProcessStepPage);
      this.ProcessStepTabs.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ProcessStepTabs.Location = new System.Drawing.Point(0, 0);
      this.ProcessStepTabs.Name = "ProcessStepTabs";
      this.ProcessStepTabs.SelectedIndex = 0;
      this.ProcessStepTabs.Size = new System.Drawing.Size(800, 600);
      this.ProcessStepTabs.TabIndex = 0;
      // 
      // ProcessStepPage
      // 
      this.ProcessStepPage.Controls.Add(this.DataProcessLabel);
      this.ProcessStepPage.Controls.Add(this.DataProcessCombo);
      this.ProcessStepPage.Controls.Add(this.StepSplit);
      this.ProcessStepPage.Location = new System.Drawing.Point(4, 29);
      this.ProcessStepPage.Name = "ProcessStepPage";
      this.ProcessStepPage.Padding = new System.Windows.Forms.Padding(3);
      this.ProcessStepPage.Size = new System.Drawing.Size(792, 567);
      this.ProcessStepPage.TabIndex = 0;
      this.ProcessStepPage.Text = "Process Step";
      this.ProcessStepPage.UseVisualStyleBackColor = true;
      // 
      // DataProcessLabel
      // 
      this.DataProcessLabel.AutoSize = true;
      this.DataProcessLabel.Location = new System.Drawing.Point(12, 15);
      this.DataProcessLabel.Name = "DataProcessLabel";
      this.DataProcessLabel.Size = new System.Drawing.Size(105, 20);
      this.DataProcessLabel.TabIndex = 0;
      this.DataProcessLabel.Text = "Data Process";
      // 
      // DataProcessCombo
      // 
      this.DataProcessCombo.FormattingEnabled = true;
      this.DataProcessCombo.Location = new System.Drawing.Point(151, 12);
      this.DataProcessCombo.Name = "DataProcessCombo";
      this.DataProcessCombo.Size = new System.Drawing.Size(330, 28);
      this.DataProcessCombo.TabIndex = 1;
      this.DataProcessCombo.SelectedIndexChanged += new System.EventHandler(this.DataProcessCombo_SelectedIndexChanged);
      // 
      // StepSplit
      // 
      this.StepSplit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.StepSplit.Location = new System.Drawing.Point(0, 48);
      this.StepSplit.Name = "StepSplit";
      this.StepSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // StepSplit.Panel1
      // 
      this.StepSplit.Panel1.Controls.Add(this.StepHeader);
      this.StepSplit.Panel1.Controls.Add(this.StepGrid);
      this.StepSplit.Panel1.Controls.Add(this.StepToolPanel);
      // 
      // StepSplit.Panel2
      // 
      this.StepSplit.Panel2.Controls.Add(this.TaskGrid);
      this.StepSplit.Panel2.Controls.Add(this.TaskHeader);
      this.StepSplit.Panel2.Controls.Add(this.TaskToolPanel);
      this.StepSplit.Size = new System.Drawing.Size(792, 518);
      this.StepSplit.SplitterDistance = 195;
      this.StepSplit.TabIndex = 5;
      // 
      // StepHeader
      // 
      this.StepHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.StepHeader.LJCBeginColor = System.Drawing.Color.AliceBlue;
      this.StepHeader.LJCEndColor = System.Drawing.Color.SkyBlue;
      this.StepHeader.Location = new System.Drawing.Point(0, 1);
      this.StepHeader.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.StepHeader.Name = "StepHeader";
      this.StepHeader.Size = new System.Drawing.Size(792, 31);
      this.StepHeader.TabIndex = 0;
      this.StepHeader.TabStop = false;
      this.StepHeader.Text = "Step";
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
      this.StepGrid.Location = new System.Drawing.Point(0, 70);
      this.StepGrid.MultiSelect = false;
      this.StepGrid.Name = "StepGrid";
      this.StepGrid.RowHeadersVisible = false;
      this.StepGrid.RowHeadersWidth = 62;
      this.StepGrid.RowTemplate.Height = 28;
      this.StepGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.StepGrid.ShowCellToolTips = false;
      this.StepGrid.Size = new System.Drawing.Size(792, 124);
      this.StepGrid.TabIndex = 2;
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
            this.StepMenuClose});
      this.StepMenu.Name = "mFacilityMenu";
      this.StepMenu.Size = new System.Drawing.Size(184, 214);
      // 
      // StepMenuNew
      // 
      this.StepMenuNew.Name = "StepMenuNew";
      this.StepMenuNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
      this.StepMenuNew.Size = new System.Drawing.Size(183, 32);
      this.StepMenuNew.Text = "&New";
      this.StepMenuNew.Click += new System.EventHandler(this.StepMenuNew_Click);
      // 
      // StepMenuEdit
      // 
      this.StepMenuEdit.Name = "StepMenuEdit";
      this.StepMenuEdit.ShortcutKeyDisplayString = "Enter";
      this.StepMenuEdit.Size = new System.Drawing.Size(183, 32);
      this.StepMenuEdit.Text = "&Edit";
      this.StepMenuEdit.Click += new System.EventHandler(this.StepMenuEdit_Click);
      // 
      // DeleteSeparator
      // 
      this.DeleteSeparator.Name = "DeleteSeparator";
      this.DeleteSeparator.Size = new System.Drawing.Size(180, 6);
      // 
      // StepMenuDelete
      // 
      this.StepMenuDelete.Name = "StepMenuDelete";
      this.StepMenuDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
      this.StepMenuDelete.Size = new System.Drawing.Size(183, 32);
      this.StepMenuDelete.Text = "&Delete";
      this.StepMenuDelete.Click += new System.EventHandler(this.StepMenuDelete_Click);
      // 
      // RefreshSeparator
      // 
      this.RefreshSeparator.Name = "RefreshSeparator";
      this.RefreshSeparator.Size = new System.Drawing.Size(180, 6);
      // 
      // StepMenuRefresh
      // 
      this.StepMenuRefresh.Name = "StepMenuRefresh";
      this.StepMenuRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
      this.StepMenuRefresh.Size = new System.Drawing.Size(183, 32);
      this.StepMenuRefresh.Text = "&Refresh";
      this.StepMenuRefresh.Click += new System.EventHandler(this.StepMenuRefresh_Click);
      // 
      // CloseSeparator
      // 
      this.CloseSeparator.Name = "CloseSeparator";
      this.CloseSeparator.Size = new System.Drawing.Size(180, 6);
      // 
      // StepMenuClose
      // 
      this.StepMenuClose.Name = "StepMenuClose";
      this.StepMenuClose.Size = new System.Drawing.Size(183, 32);
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
      this.StepToolPanel.Location = new System.Drawing.Point(0, 31);
      this.StepToolPanel.Name = "StepToolPanel";
      this.StepToolPanel.Size = new System.Drawing.Size(792, 40);
      this.StepToolPanel.TabIndex = 1;
      // 
      // StepCounter
      // 
      this.StepCounter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.StepCounter.Location = new System.Drawing.Point(537, 13);
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
      this.StepToolDelete.Click += new System.EventHandler(this.StepToolDelete_Click);
      // 
      // TaskGrid
      // 
      this.TaskGrid.AllowUserToAddRows = false;
      this.TaskGrid.AllowUserToDeleteRows = false;
      this.TaskGrid.AllowUserToResizeRows = false;
      this.TaskGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.TaskGrid.BackgroundColor = System.Drawing.Color.AliceBlue;
      this.TaskGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.TaskGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.TaskGrid.ContextMenuStrip = this.TaskMenu;
      dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
      dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
      dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.TaskGrid.DefaultCellStyle = dataGridViewCellStyle2;
      this.TaskGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
      this.TaskGrid.LJCAllowSelectionChange = false;
      this.TaskGrid.LJCLastRowIndex = -1;
      this.TaskGrid.LJCRowHeight = 0;
      this.TaskGrid.Location = new System.Drawing.Point(0, 70);
      this.TaskGrid.MultiSelect = false;
      this.TaskGrid.Name = "TaskGrid";
      this.TaskGrid.RowHeadersVisible = false;
      this.TaskGrid.RowHeadersWidth = 62;
      this.TaskGrid.RowTemplate.Height = 28;
      this.TaskGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.TaskGrid.ShowCellToolTips = false;
      this.TaskGrid.Size = new System.Drawing.Size(792, 249);
      this.TaskGrid.TabIndex = 2;
      this.TaskGrid.Text = "LJCDataGrid";
      this.TaskGrid.SelectionChanged += new System.EventHandler(this.TaskGrid_SelectionChanged);
      this.TaskGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TaskGrid_KeyDown);
      this.TaskGrid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TaskGrid_MouseDoubleClick);
      this.TaskGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TaskGrid_MouseDown);
      // 
      // TaskMenu
      // 
      this.TaskMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.TaskMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.TaskMenuNew,
            this.TaskMenuEdit,
            this.toolStripSeparator1,
            this.TaskMenuDelete,
            this.toolStripSeparator2,
            this.TaskMenuRefresh,
            this.toolStripSeparator3,
            this.TaskMenuClose});
      this.TaskMenu.Name = "mFacilityMenu";
      this.TaskMenu.Size = new System.Drawing.Size(184, 214);
      // 
      // TaskMenuNew
      // 
      this.TaskMenuNew.Name = "TaskMenuNew";
      this.TaskMenuNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
      this.TaskMenuNew.Size = new System.Drawing.Size(183, 32);
      this.TaskMenuNew.Text = "&New";
      this.TaskMenuNew.Click += new System.EventHandler(this.TaskMenuNew_Click);
      // 
      // TaskMenuEdit
      // 
      this.TaskMenuEdit.Name = "TaskMenuEdit";
      this.TaskMenuEdit.ShortcutKeyDisplayString = "Enter";
      this.TaskMenuEdit.Size = new System.Drawing.Size(183, 32);
      this.TaskMenuEdit.Text = "&Edit";
      this.TaskMenuEdit.Click += new System.EventHandler(this.TaskMenuEdit_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(180, 6);
      // 
      // TaskMenuDelete
      // 
      this.TaskMenuDelete.Name = "TaskMenuDelete";
      this.TaskMenuDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
      this.TaskMenuDelete.Size = new System.Drawing.Size(183, 32);
      this.TaskMenuDelete.Text = "&Delete";
      this.TaskMenuDelete.Click += new System.EventHandler(this.TaskMenuDelete_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(180, 6);
      // 
      // TaskMenuRefresh
      // 
      this.TaskMenuRefresh.Name = "TaskMenuRefresh";
      this.TaskMenuRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
      this.TaskMenuRefresh.Size = new System.Drawing.Size(183, 32);
      this.TaskMenuRefresh.Text = "&Refresh";
      this.TaskMenuRefresh.Click += new System.EventHandler(this.TaskMenuRefresh_Click);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(180, 6);
      // 
      // TaskMenuClose
      // 
      this.TaskMenuClose.Name = "TaskMenuClose";
      this.TaskMenuClose.Size = new System.Drawing.Size(183, 32);
      this.TaskMenuClose.Text = "&Close";
      this.TaskMenuClose.Click += new System.EventHandler(this.TaskMenuClose_Click);
      // 
      // TaskHeader
      // 
      this.TaskHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.TaskHeader.LJCBeginColor = System.Drawing.Color.AliceBlue;
      this.TaskHeader.LJCEndColor = System.Drawing.Color.SkyBlue;
      this.TaskHeader.Location = new System.Drawing.Point(0, 1);
      this.TaskHeader.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.TaskHeader.Name = "TaskHeader";
      this.TaskHeader.Size = new System.Drawing.Size(792, 31);
      this.TaskHeader.TabIndex = 0;
      this.TaskHeader.TabStop = false;
      this.TaskHeader.Text = "Task";
      // 
      // TaskToolPanel
      // 
      this.TaskToolPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.TaskToolPanel.BackColor = System.Drawing.SystemColors.Control;
      this.TaskToolPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.TaskToolPanel.Controls.Add(this.TaskCounter);
      this.TaskToolPanel.Controls.Add(this.TaskTool);
      this.TaskToolPanel.Location = new System.Drawing.Point(0, 31);
      this.TaskToolPanel.Name = "TaskToolPanel";
      this.TaskToolPanel.Size = new System.Drawing.Size(792, 40);
      this.TaskToolPanel.TabIndex = 1;
      // 
      // TaskCounter
      // 
      this.TaskCounter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.TaskCounter.Location = new System.Drawing.Point(537, 13);
      this.TaskCounter.Name = "TaskCounter";
      this.TaskCounter.Size = new System.Drawing.Size(250, 23);
      this.TaskCounter.TabIndex = 1;
      this.TaskCounter.Text = "Row 0 of 0";
      this.TaskCounter.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // TaskTool
      // 
      this.TaskTool.Dock = System.Windows.Forms.DockStyle.None;
      this.TaskTool.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.TaskTool.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.TaskTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TaskToolNew,
            this.TaskToolEdit,
            this.TaskToolDelete});
      this.TaskTool.Location = new System.Drawing.Point(3, 2);
      this.TaskTool.Name = "TaskTool";
      this.TaskTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.TaskTool.Size = new System.Drawing.Size(106, 27);
      this.TaskTool.TabIndex = 0;
      this.TaskTool.Text = "FirstTool";
      // 
      // TaskToolNew
      // 
      this.TaskToolNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.TaskToolNew.Image = ((System.Drawing.Image)(resources.GetObject("TaskToolNew.Image")));
      this.TaskToolNew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.TaskToolNew.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.TaskToolNew.Name = "TaskToolNew";
      this.TaskToolNew.Size = new System.Drawing.Size(34, 22);
      this.TaskToolNew.Text = "New";
      this.TaskToolNew.Click += new System.EventHandler(this.TaskToolNew_Click);
      // 
      // TaskToolEdit
      // 
      this.TaskToolEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.TaskToolEdit.Image = ((System.Drawing.Image)(resources.GetObject("TaskToolEdit.Image")));
      this.TaskToolEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.TaskToolEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.TaskToolEdit.Name = "TaskToolEdit";
      this.TaskToolEdit.Size = new System.Drawing.Size(34, 22);
      this.TaskToolEdit.Text = "Edit";
      this.TaskToolEdit.Click += new System.EventHandler(this.TaskToolEdit_Click);
      // 
      // TaskToolDelete
      // 
      this.TaskToolDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.TaskToolDelete.Image = ((System.Drawing.Image)(resources.GetObject("TaskToolDelete.Image")));
      this.TaskToolDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.TaskToolDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.TaskToolDelete.Name = "TaskToolDelete";
      this.TaskToolDelete.Size = new System.Drawing.Size(34, 22);
      this.TaskToolDelete.Text = "Delete";
      this.TaskToolDelete.Click += new System.EventHandler(this.TaskToolDelete_Click);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
      this.toolStripMenuItem1.Enabled = false;
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(183, 32);
      this.toolStripMenuItem1.Text = "Step Menu";
      // 
      // toolStripMenuItem2
      // 
      this.toolStripMenuItem2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
      this.toolStripMenuItem2.Enabled = false;
      this.toolStripMenuItem2.Name = "toolStripMenuItem2";
      this.toolStripMenuItem2.Size = new System.Drawing.Size(183, 32);
      this.toolStripMenuItem2.Text = "Task Menu";
      // 
      // ProcessStepModule
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.ProcessStepTabs);
      this.Name = "ProcessStepModule";
      this.Size = new System.Drawing.Size(800, 600);
      this.ProcessStepTabs.ResumeLayout(false);
      this.ProcessStepPage.ResumeLayout(false);
      this.ProcessStepPage.PerformLayout();
      this.StepSplit.Panel1.ResumeLayout(false);
      this.StepSplit.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.StepSplit)).EndInit();
      this.StepSplit.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.StepGrid)).EndInit();
      this.StepMenu.ResumeLayout(false);
      this.StepToolPanel.ResumeLayout(false);
      this.StepToolPanel.PerformLayout();
      this.StepTool.ResumeLayout(false);
      this.StepTool.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.TaskGrid)).EndInit();
      this.TaskMenu.ResumeLayout(false);
      this.TaskToolPanel.ResumeLayout(false);
      this.TaskToolPanel.PerformLayout();
      this.TaskTool.ResumeLayout(false);
      this.TaskTool.PerformLayout();
      this.ResumeLayout(false);

		}

		#endregion

		private LJCWinFormControls.LJCTabControl ProcessStepTabs;
		private System.Windows.Forms.TabPage ProcessStepPage;
		private System.Windows.Forms.Label DataProcessLabel;
		internal LJCWinFormControls.LJCItemCombo DataProcessCombo;
		private System.Windows.Forms.SplitContainer StepSplit;
		private LJCWinFormControls.LJCHeaderBox StepHeader;
		internal LJCWinFormControls.LJCDataGrid StepGrid;
		private System.Windows.Forms.Panel StepToolPanel;
		private System.Windows.Forms.Label StepCounter;
		private System.Windows.Forms.ToolStrip StepTool;
		private System.Windows.Forms.ToolStripButton StepToolNew;
		private System.Windows.Forms.ToolStripButton StepToolEdit;
		private System.Windows.Forms.ToolStripButton StepToolDelete;
		internal LJCWinFormControls.LJCDataGrid TaskGrid;
		private LJCWinFormControls.LJCHeaderBox TaskHeader;
		private System.Windows.Forms.Panel TaskToolPanel;
		private System.Windows.Forms.Label TaskCounter;
		private System.Windows.Forms.ToolStrip TaskTool;
		private System.Windows.Forms.ToolStripButton TaskToolNew;
		private System.Windows.Forms.ToolStripButton TaskToolEdit;
		private System.Windows.Forms.ToolStripButton TaskToolDelete;
		private System.Windows.Forms.ContextMenuStrip StepMenu;
		private System.Windows.Forms.ToolStripMenuItem StepMenuNew;
		private System.Windows.Forms.ToolStripMenuItem StepMenuEdit;
		private System.Windows.Forms.ToolStripSeparator DeleteSeparator;
		private System.Windows.Forms.ToolStripMenuItem StepMenuDelete;
		private System.Windows.Forms.ToolStripSeparator RefreshSeparator;
		private System.Windows.Forms.ToolStripMenuItem StepMenuRefresh;
		private System.Windows.Forms.ToolStripSeparator CloseSeparator;
		private System.Windows.Forms.ToolStripMenuItem StepMenuClose;
		private System.Windows.Forms.ContextMenuStrip TaskMenu;
		private System.Windows.Forms.ToolStripMenuItem TaskMenuNew;
		private System.Windows.Forms.ToolStripMenuItem TaskMenuEdit;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem TaskMenuDelete;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem TaskMenuRefresh;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem TaskMenuClose;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
  }
}
