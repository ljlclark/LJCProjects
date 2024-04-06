using System.Security.Principal;

namespace UpdateProjectFiles
{
  partial class CodeManagerList
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
      this.TabsSplit = new System.Windows.Forms.SplitContainer();
      this.MainTabs = new LJCWinFormControls.LJCTabControl(this.components);
      this.MainTabsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.MainTabsMove = new System.Windows.Forms.ToolStripMenuItem();
      this.CodeLineTab = new System.Windows.Forms.TabPage();
      this.CodeLineSplit = new System.Windows.Forms.SplitContainer();
      this.CodeLineGrid = new LJCWinFormControls.LJCDataGrid(this.components);
      this.CodeLineMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.CodeLineHeading = new System.Windows.Forms.ToolStripMenuItem();
      this.CodeLineNew = new System.Windows.Forms.ToolStripMenuItem();
      this.CodeLineEdit = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
      this.CodeLineDelete = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
      this.CodeLineRefresh = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.CodeLineExit = new System.Windows.Forms.ToolStripMenuItem();
      this.CodeGroupGrid = new LJCWinFormControls.LJCDataGrid(this.components);
      this.CodeGroupMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.CodeGroupHeading = new System.Windows.Forms.ToolStripMenuItem();
      this.CodeGroupNew = new System.Windows.Forms.ToolStripMenuItem();
      this.CodeGroupEdit = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
      this.CodeGroupDelete = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
      this.CodeGroupRefresh = new System.Windows.Forms.ToolStripMenuItem();
      this.CodeGroupHeader = new LJCWinFormControls.LJCHeaderBox();
      this.SolutionTab = new System.Windows.Forms.TabPage();
      this.GroupText = new System.Windows.Forms.TextBox();
      this.GroupLabel = new System.Windows.Forms.Label();
      this.SolutionSplit = new System.Windows.Forms.SplitContainer();
      this.SolutionHeader = new LJCWinFormControls.LJCHeaderBox();
      this.SolutionGrid = new LJCWinFormControls.LJCDataGrid(this.components);
      this.SolutionMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.SolutionHeading = new System.Windows.Forms.ToolStripMenuItem();
      this.SolutionNew = new System.Windows.Forms.ToolStripMenuItem();
      this.SolutionEdit = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.SolutionDelete = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.SolutionRefresh = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
      this.SolutionClear = new System.Windows.Forms.ToolStripMenuItem();
      this.SolutionUpdate = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.SolutionExit = new System.Windows.Forms.ToolStripMenuItem();
      this.ProjectGrid = new LJCWinFormControls.LJCDataGrid(this.components);
      this.ProjectMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.ProjectHeading = new System.Windows.Forms.ToolStripMenuItem();
      this.ProjectNew = new System.Windows.Forms.ToolStripMenuItem();
      this.ProjectEdit = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
      this.ProjectDelete = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
      this.ProjectRefresh = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
      this.ProjectClear = new System.Windows.Forms.ToolStripMenuItem();
      this.ProjectUpdate = new System.Windows.Forms.ToolStripMenuItem();
      this.ProjectHeader = new LJCWinFormControls.LJCHeaderBox();
      this.FileTab = new System.Windows.Forms.TabPage();
      this.ljcHeaderBox1 = new LJCWinFormControls.LJCHeaderBox();
      this.ProjectFileGrid = new LJCWinFormControls.LJCDataGrid(this.components);
      this.ProjectFileMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.ProjectFileHeading = new System.Windows.Forms.ToolStripMenuItem();
      this.ProjectFileNew = new System.Windows.Forms.ToolStripMenuItem();
      this.ProjectFileEdit = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
      this.ProjectFileDelete = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
      this.ProjectFileRefresh = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
      this.ProjectFileExit = new System.Windows.Forms.ToolStripMenuItem();
      this.ProjectText = new System.Windows.Forms.TextBox();
      this.ProjectLabel = new System.Windows.Forms.Label();
      this.TileTabs = new LJCWinFormControls.LJCTabControl(this.components);
      this.TileTabsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.TileTabsMove = new System.Windows.Forms.ToolStripMenuItem();
      ((System.ComponentModel.ISupportInitialize)(this.TabsSplit)).BeginInit();
      this.TabsSplit.Panel1.SuspendLayout();
      this.TabsSplit.Panel2.SuspendLayout();
      this.TabsSplit.SuspendLayout();
      this.MainTabs.SuspendLayout();
      this.MainTabsMenu.SuspendLayout();
      this.CodeLineTab.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.CodeLineSplit)).BeginInit();
      this.CodeLineSplit.Panel1.SuspendLayout();
      this.CodeLineSplit.Panel2.SuspendLayout();
      this.CodeLineSplit.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.CodeLineGrid)).BeginInit();
      this.CodeLineMenu.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.CodeGroupGrid)).BeginInit();
      this.CodeGroupMenu.SuspendLayout();
      this.SolutionTab.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.SolutionSplit)).BeginInit();
      this.SolutionSplit.Panel1.SuspendLayout();
      this.SolutionSplit.Panel2.SuspendLayout();
      this.SolutionSplit.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.SolutionGrid)).BeginInit();
      this.SolutionMenu.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.ProjectGrid)).BeginInit();
      this.ProjectMenu.SuspendLayout();
      this.FileTab.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.ProjectFileGrid)).BeginInit();
      this.ProjectFileMenu.SuspendLayout();
      this.TileTabsMenu.SuspendLayout();
      this.SuspendLayout();
      // 
      // TabsSplit
      // 
      this.TabsSplit.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TabsSplit.Location = new System.Drawing.Point(0, 0);
      this.TabsSplit.Name = "TabsSplit";
      // 
      // TabsSplit.Panel1
      // 
      this.TabsSplit.Panel1.Controls.Add(this.MainTabs);
      // 
      // TabsSplit.Panel2
      // 
      this.TabsSplit.Panel2.Controls.Add(this.TileTabs);
      this.TabsSplit.Size = new System.Drawing.Size(800, 450);
      this.TabsSplit.SplitterDistance = 695;
      this.TabsSplit.TabIndex = 0;
      // 
      // MainTabs
      // 
      this.MainTabs.AllowDrop = true;
      this.MainTabs.ContextMenuStrip = this.MainTabsMenu;
      this.MainTabs.Controls.Add(this.CodeLineTab);
      this.MainTabs.Controls.Add(this.SolutionTab);
      this.MainTabs.Controls.Add(this.FileTab);
      this.MainTabs.Dock = System.Windows.Forms.DockStyle.Fill;
      this.MainTabs.LJCAllowDrag = true;
      this.MainTabs.Location = new System.Drawing.Point(0, 0);
      this.MainTabs.Name = "MainTabs";
      this.MainTabs.SelectedIndex = 0;
      this.MainTabs.Size = new System.Drawing.Size(695, 450);
      this.MainTabs.TabIndex = 1;
      this.MainTabs.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainTabs_MouseDown);
      // 
      // MainTabsMenu
      // 
      this.MainTabsMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.MainTabsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainTabsMove});
      this.MainTabsMenu.Name = "MainTabMenu";
      this.MainTabsMenu.Size = new System.Drawing.Size(177, 36);
      // 
      // MainTabsMove
      // 
      this.MainTabsMove.Name = "MainTabsMove";
      this.MainTabsMove.Size = new System.Drawing.Size(176, 32);
      this.MainTabsMove.Text = "Move Right";
      this.MainTabsMove.Click += new System.EventHandler(this.MainTabsMove_Click);
      // 
      // CodeLineTab
      // 
      this.CodeLineTab.Controls.Add(this.CodeLineSplit);
      this.CodeLineTab.Location = new System.Drawing.Point(4, 29);
      this.CodeLineTab.Name = "CodeLineTab";
      this.CodeLineTab.Padding = new System.Windows.Forms.Padding(3);
      this.CodeLineTab.Size = new System.Drawing.Size(687, 417);
      this.CodeLineTab.TabIndex = 0;
      this.CodeLineTab.Text = "Code Line";
      this.CodeLineTab.UseVisualStyleBackColor = true;
      // 
      // CodeLineSplit
      // 
      this.CodeLineSplit.Dock = System.Windows.Forms.DockStyle.Fill;
      this.CodeLineSplit.Location = new System.Drawing.Point(3, 3);
      this.CodeLineSplit.Name = "CodeLineSplit";
      this.CodeLineSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // CodeLineSplit.Panel1
      // 
      this.CodeLineSplit.Panel1.Controls.Add(this.CodeLineGrid);
      // 
      // CodeLineSplit.Panel2
      // 
      this.CodeLineSplit.Panel2.Controls.Add(this.CodeGroupGrid);
      this.CodeLineSplit.Panel2.Controls.Add(this.CodeGroupHeader);
      this.CodeLineSplit.Size = new System.Drawing.Size(681, 411);
      this.CodeLineSplit.SplitterDistance = 104;
      this.CodeLineSplit.TabIndex = 0;
      // 
      // CodeLineGrid
      // 
      this.CodeLineGrid.AllowDrop = true;
      this.CodeLineGrid.AllowUserToAddRows = false;
      this.CodeLineGrid.AllowUserToDeleteRows = false;
      this.CodeLineGrid.AllowUserToResizeRows = false;
      this.CodeLineGrid.BackgroundColor = System.Drawing.Color.AliceBlue;
      this.CodeLineGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.CodeLineGrid.ContextMenuStrip = this.CodeLineMenu;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.CodeLineGrid.DefaultCellStyle = dataGridViewCellStyle1;
      this.CodeLineGrid.Dock = System.Windows.Forms.DockStyle.Fill;
      this.CodeLineGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
      this.CodeLineGrid.LJCAllowDrag = true;
      this.CodeLineGrid.LJCAllowSelectionChange = false;
      this.CodeLineGrid.LJCDragDataName = null;
      this.CodeLineGrid.LJCLastRowIndex = -1;
      this.CodeLineGrid.LJCRowHeight = 0;
      this.CodeLineGrid.Location = new System.Drawing.Point(0, 0);
      this.CodeLineGrid.MultiSelect = false;
      this.CodeLineGrid.Name = "CodeLineGrid";
      this.CodeLineGrid.RowHeadersVisible = false;
      this.CodeLineGrid.RowHeadersWidth = 62;
      this.CodeLineGrid.RowTemplate.Height = 28;
      this.CodeLineGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.CodeLineGrid.ShowCellToolTips = false;
      this.CodeLineGrid.Size = new System.Drawing.Size(681, 104);
      this.CodeLineGrid.TabIndex = 1;
      this.CodeLineGrid.Text = "LJCDataGrid";
      this.CodeLineGrid.SelectionChanged += new System.EventHandler(this.CodeLineGrid_SelectionChanged);
      this.CodeLineGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CodeLineGrid_KeyDown);
      this.CodeLineGrid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.CodeLineGrid_MouseDoubleClick);
      this.CodeLineGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CodeLineGrid_MouseDown);
      // 
      // CodeLineMenu
      // 
      this.CodeLineMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.CodeLineMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CodeLineHeading,
            this.CodeLineNew,
            this.CodeLineEdit,
            this.toolStripSeparator10,
            this.CodeLineDelete,
            this.toolStripSeparator15,
            this.CodeLineRefresh,
            this.toolStripSeparator3,
            this.CodeLineExit});
      this.CodeLineMenu.Name = "AssemblyMenu";
      this.CodeLineMenu.Size = new System.Drawing.Size(213, 214);
      this.CodeLineMenu.Text = "Assembly Group Menu";
      // 
      // CodeLineHeading
      // 
      this.CodeLineHeading.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
      this.CodeLineHeading.Name = "CodeLineHeading";
      this.CodeLineHeading.Size = new System.Drawing.Size(212, 32);
      this.CodeLineHeading.Text = "Code Line Menu";
      // 
      // CodeLineNew
      // 
      this.CodeLineNew.Name = "CodeLineNew";
      this.CodeLineNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
      this.CodeLineNew.Size = new System.Drawing.Size(212, 32);
      this.CodeLineNew.Text = "&New";
      this.CodeLineNew.Click += new System.EventHandler(this.CodeLineNew_Click);
      // 
      // CodeLineEdit
      // 
      this.CodeLineEdit.Name = "CodeLineEdit";
      this.CodeLineEdit.ShortcutKeyDisplayString = "ENTER";
      this.CodeLineEdit.Size = new System.Drawing.Size(212, 32);
      this.CodeLineEdit.Text = "&Edit";
      this.CodeLineEdit.Click += new System.EventHandler(this.CodeLineEdit_Click);
      // 
      // toolStripSeparator10
      // 
      this.toolStripSeparator10.Name = "toolStripSeparator10";
      this.toolStripSeparator10.Size = new System.Drawing.Size(209, 6);
      // 
      // CodeLineDelete
      // 
      this.CodeLineDelete.Name = "CodeLineDelete";
      this.CodeLineDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
      this.CodeLineDelete.Size = new System.Drawing.Size(212, 32);
      this.CodeLineDelete.Text = "&Delete";
      this.CodeLineDelete.Click += new System.EventHandler(this.CodeLineDelete_Click);
      // 
      // toolStripSeparator15
      // 
      this.toolStripSeparator15.Name = "toolStripSeparator15";
      this.toolStripSeparator15.Size = new System.Drawing.Size(209, 6);
      // 
      // CodeLineRefresh
      // 
      this.CodeLineRefresh.Name = "CodeLineRefresh";
      this.CodeLineRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
      this.CodeLineRefresh.Size = new System.Drawing.Size(212, 32);
      this.CodeLineRefresh.Text = "&Refresh";
      this.CodeLineRefresh.Click += new System.EventHandler(this.CodeLineRefresh_Click);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(209, 6);
      // 
      // CodeLineExit
      // 
      this.CodeLineExit.Name = "CodeLineExit";
      this.CodeLineExit.Size = new System.Drawing.Size(212, 32);
      this.CodeLineExit.Text = "E&xit";
      this.CodeLineExit.Click += new System.EventHandler(this.CodeLineExit_Click);
      // 
      // CodeGroupGrid
      // 
      this.CodeGroupGrid.AllowDrop = true;
      this.CodeGroupGrid.AllowUserToAddRows = false;
      this.CodeGroupGrid.AllowUserToDeleteRows = false;
      this.CodeGroupGrid.AllowUserToResizeRows = false;
      this.CodeGroupGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.CodeGroupGrid.BackgroundColor = System.Drawing.Color.AliceBlue;
      this.CodeGroupGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.CodeGroupGrid.ContextMenuStrip = this.CodeGroupMenu;
      dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
      dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
      dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.CodeGroupGrid.DefaultCellStyle = dataGridViewCellStyle2;
      this.CodeGroupGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
      this.CodeGroupGrid.LJCAllowDrag = true;
      this.CodeGroupGrid.LJCAllowSelectionChange = false;
      this.CodeGroupGrid.LJCDragDataName = null;
      this.CodeGroupGrid.LJCLastRowIndex = -1;
      this.CodeGroupGrid.LJCRowHeight = 0;
      this.CodeGroupGrid.Location = new System.Drawing.Point(0, 32);
      this.CodeGroupGrid.MultiSelect = false;
      this.CodeGroupGrid.Name = "CodeGroupGrid";
      this.CodeGroupGrid.RowHeadersVisible = false;
      this.CodeGroupGrid.RowHeadersWidth = 62;
      this.CodeGroupGrid.RowTemplate.Height = 28;
      this.CodeGroupGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.CodeGroupGrid.ShowCellToolTips = false;
      this.CodeGroupGrid.Size = new System.Drawing.Size(680, 271);
      this.CodeGroupGrid.TabIndex = 1;
      this.CodeGroupGrid.Text = "LJCDataGrid";
      this.CodeGroupGrid.SelectionChanged += new System.EventHandler(this.CodeGroupGrid_SelectionChanged);
      this.CodeGroupGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CodeGroupGrid_KeyDown);
      this.CodeGroupGrid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.CodeGroupGrid_MouseDoubleClick);
      this.CodeGroupGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CodeGroupGrid_MouseDown);
      // 
      // CodeGroupMenu
      // 
      this.CodeGroupMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.CodeGroupMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CodeGroupHeading,
            this.CodeGroupNew,
            this.CodeGroupEdit,
            this.toolStripSeparator9,
            this.CodeGroupDelete,
            this.toolStripSeparator11,
            this.CodeGroupRefresh});
      this.CodeGroupMenu.Name = "AssemblyMenu";
      this.CodeGroupMenu.Size = new System.Drawing.Size(232, 176);
      this.CodeGroupMenu.Text = "Assembly Group Menu";
      // 
      // CodeGroupHeading
      // 
      this.CodeGroupHeading.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
      this.CodeGroupHeading.Name = "CodeGroupHeading";
      this.CodeGroupHeading.Size = new System.Drawing.Size(231, 32);
      this.CodeGroupHeading.Text = "Code Group Menu";
      // 
      // CodeGroupNew
      // 
      this.CodeGroupNew.Name = "CodeGroupNew";
      this.CodeGroupNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
      this.CodeGroupNew.Size = new System.Drawing.Size(231, 32);
      this.CodeGroupNew.Text = "&New";
      this.CodeGroupNew.Click += new System.EventHandler(this.CodeGroupNew_Click);
      // 
      // CodeGroupEdit
      // 
      this.CodeGroupEdit.Name = "CodeGroupEdit";
      this.CodeGroupEdit.ShortcutKeyDisplayString = "ENTER";
      this.CodeGroupEdit.Size = new System.Drawing.Size(231, 32);
      this.CodeGroupEdit.Text = "&Edit";
      this.CodeGroupEdit.Click += new System.EventHandler(this.CodeGroupEdit_Click);
      // 
      // toolStripSeparator9
      // 
      this.toolStripSeparator9.Name = "toolStripSeparator9";
      this.toolStripSeparator9.Size = new System.Drawing.Size(228, 6);
      // 
      // CodeGroupDelete
      // 
      this.CodeGroupDelete.Name = "CodeGroupDelete";
      this.CodeGroupDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
      this.CodeGroupDelete.Size = new System.Drawing.Size(231, 32);
      this.CodeGroupDelete.Text = "&Delete";
      this.CodeGroupDelete.Click += new System.EventHandler(this.CodeGroupDelete_Click);
      // 
      // toolStripSeparator11
      // 
      this.toolStripSeparator11.Name = "toolStripSeparator11";
      this.toolStripSeparator11.Size = new System.Drawing.Size(228, 6);
      // 
      // CodeGroupRefresh
      // 
      this.CodeGroupRefresh.Name = "CodeGroupRefresh";
      this.CodeGroupRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
      this.CodeGroupRefresh.Size = new System.Drawing.Size(231, 32);
      this.CodeGroupRefresh.Text = "&Refresh";
      this.CodeGroupRefresh.Click += new System.EventHandler(this.CodeGroupRefresh_Click);
      // 
      // CodeGroupHeader
      // 
      this.CodeGroupHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.CodeGroupHeader.LJCBeginColor = System.Drawing.Color.AliceBlue;
      this.CodeGroupHeader.LJCEndColor = System.Drawing.Color.LightSkyBlue;
      this.CodeGroupHeader.Location = new System.Drawing.Point(0, 0);
      this.CodeGroupHeader.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.CodeGroupHeader.Name = "CodeGroupHeader";
      this.CodeGroupHeader.Size = new System.Drawing.Size(680, 33);
      this.CodeGroupHeader.TabIndex = 0;
      this.CodeGroupHeader.TabStop = false;
      this.CodeGroupHeader.Text = "Code Group";
      // 
      // SolutionTab
      // 
      this.SolutionTab.Controls.Add(this.GroupText);
      this.SolutionTab.Controls.Add(this.GroupLabel);
      this.SolutionTab.Controls.Add(this.SolutionSplit);
      this.SolutionTab.Location = new System.Drawing.Point(4, 29);
      this.SolutionTab.Name = "SolutionTab";
      this.SolutionTab.Padding = new System.Windows.Forms.Padding(3);
      this.SolutionTab.Size = new System.Drawing.Size(687, 417);
      this.SolutionTab.TabIndex = 1;
      this.SolutionTab.Text = "Solution";
      this.SolutionTab.UseVisualStyleBackColor = true;
      // 
      // GroupText
      // 
      this.GroupText.Location = new System.Drawing.Point(107, 6);
      this.GroupText.Name = "GroupText";
      this.GroupText.Size = new System.Drawing.Size(500, 26);
      this.GroupText.TabIndex = 1;
      // 
      // GroupLabel
      // 
      this.GroupLabel.Location = new System.Drawing.Point(7, 9);
      this.GroupLabel.Name = "GroupLabel";
      this.GroupLabel.Size = new System.Drawing.Size(100, 23);
      this.GroupLabel.TabIndex = 0;
      this.GroupLabel.Text = "Code Group";
      // 
      // SolutionSplit
      // 
      this.SolutionSplit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.SolutionSplit.Location = new System.Drawing.Point(3, 41);
      this.SolutionSplit.Name = "SolutionSplit";
      this.SolutionSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // SolutionSplit.Panel1
      // 
      this.SolutionSplit.Panel1.Controls.Add(this.SolutionHeader);
      this.SolutionSplit.Panel1.Controls.Add(this.SolutionGrid);
      // 
      // SolutionSplit.Panel2
      // 
      this.SolutionSplit.Panel2.Controls.Add(this.ProjectGrid);
      this.SolutionSplit.Panel2.Controls.Add(this.ProjectHeader);
      this.SolutionSplit.Size = new System.Drawing.Size(681, 375);
      this.SolutionSplit.SplitterDistance = 123;
      this.SolutionSplit.TabIndex = 2;
      // 
      // SolutionHeader
      // 
      this.SolutionHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.SolutionHeader.LJCBeginColor = System.Drawing.Color.AliceBlue;
      this.SolutionHeader.LJCEndColor = System.Drawing.Color.LightSkyBlue;
      this.SolutionHeader.Location = new System.Drawing.Point(0, 0);
      this.SolutionHeader.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.SolutionHeader.Name = "SolutionHeader";
      this.SolutionHeader.Size = new System.Drawing.Size(680, 32);
      this.SolutionHeader.TabIndex = 2;
      this.SolutionHeader.TabStop = false;
      this.SolutionHeader.Text = "Solution";
      // 
      // SolutionGrid
      // 
      this.SolutionGrid.AllowDrop = true;
      this.SolutionGrid.AllowUserToAddRows = false;
      this.SolutionGrid.AllowUserToDeleteRows = false;
      this.SolutionGrid.AllowUserToResizeRows = false;
      this.SolutionGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.SolutionGrid.BackgroundColor = System.Drawing.Color.AliceBlue;
      this.SolutionGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.SolutionGrid.ContextMenuStrip = this.SolutionMenu;
      dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
      dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
      dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.SolutionGrid.DefaultCellStyle = dataGridViewCellStyle3;
      this.SolutionGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
      this.SolutionGrid.LJCAllowDrag = true;
      this.SolutionGrid.LJCAllowSelectionChange = false;
      this.SolutionGrid.LJCDragDataName = null;
      this.SolutionGrid.LJCLastRowIndex = -1;
      this.SolutionGrid.LJCRowHeight = 0;
      this.SolutionGrid.Location = new System.Drawing.Point(0, 31);
      this.SolutionGrid.MultiSelect = false;
      this.SolutionGrid.Name = "SolutionGrid";
      this.SolutionGrid.RowHeadersVisible = false;
      this.SolutionGrid.RowHeadersWidth = 62;
      this.SolutionGrid.RowTemplate.Height = 28;
      this.SolutionGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.SolutionGrid.ShowCellToolTips = false;
      this.SolutionGrid.Size = new System.Drawing.Size(681, 91);
      this.SolutionGrid.TabIndex = 1;
      this.SolutionGrid.Text = "LJCDataGrid";
      this.SolutionGrid.SelectionChanged += new System.EventHandler(this.SolutionGrid_SelectionChanged);
      this.SolutionGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SolutionGrid_KeyDown);
      this.SolutionGrid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.SolutionGrid_MouseDoubleClick);
      this.SolutionGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SolutionGrid_MouseDown);
      // 
      // SolutionMenu
      // 
      this.SolutionMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.SolutionMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SolutionHeading,
            this.SolutionNew,
            this.SolutionEdit,
            this.toolStripSeparator2,
            this.SolutionDelete,
            this.toolStripSeparator4,
            this.SolutionRefresh,
            this.toolStripSeparator5,
            this.SolutionClear,
            this.SolutionUpdate,
            this.toolStripSeparator1,
            this.SolutionExit});
      this.SolutionMenu.Name = "AssemblyMenu";
      this.SolutionMenu.Size = new System.Drawing.Size(259, 284);
      this.SolutionMenu.Text = "Assembly Group Menu";
      // 
      // SolutionHeading
      // 
      this.SolutionHeading.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
      this.SolutionHeading.Name = "SolutionHeading";
      this.SolutionHeading.Size = new System.Drawing.Size(258, 32);
      this.SolutionHeading.Text = "Solution Menu";
      // 
      // SolutionNew
      // 
      this.SolutionNew.Name = "SolutionNew";
      this.SolutionNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
      this.SolutionNew.Size = new System.Drawing.Size(258, 32);
      this.SolutionNew.Text = "&New";
      this.SolutionNew.Click += new System.EventHandler(this.SolutionNew_Click);
      // 
      // SolutionEdit
      // 
      this.SolutionEdit.Name = "SolutionEdit";
      this.SolutionEdit.ShortcutKeyDisplayString = "ENTER";
      this.SolutionEdit.Size = new System.Drawing.Size(258, 32);
      this.SolutionEdit.Text = "&Edit";
      this.SolutionEdit.Click += new System.EventHandler(this.SolutionEdit_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(255, 6);
      // 
      // SolutionDelete
      // 
      this.SolutionDelete.Name = "SolutionDelete";
      this.SolutionDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
      this.SolutionDelete.Size = new System.Drawing.Size(258, 32);
      this.SolutionDelete.Text = "&Delete";
      this.SolutionDelete.Click += new System.EventHandler(this.SolutionDelete_Click);
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(255, 6);
      // 
      // SolutionRefresh
      // 
      this.SolutionRefresh.Name = "SolutionRefresh";
      this.SolutionRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
      this.SolutionRefresh.Size = new System.Drawing.Size(258, 32);
      this.SolutionRefresh.Text = "&Refresh";
      this.SolutionRefresh.Click += new System.EventHandler(this.SolutionRefresh_Click);
      // 
      // toolStripSeparator5
      // 
      this.toolStripSeparator5.Name = "toolStripSeparator5";
      this.toolStripSeparator5.Size = new System.Drawing.Size(255, 6);
      // 
      // SolutionClear
      // 
      this.SolutionClear.Name = "SolutionClear";
      this.SolutionClear.Size = new System.Drawing.Size(258, 32);
      this.SolutionClear.Text = "Clear Dependencies";
      this.SolutionClear.Click += new System.EventHandler(this.SolutionClear_Click);
      // 
      // SolutionUpdate
      // 
      this.SolutionUpdate.Name = "SolutionUpdate";
      this.SolutionUpdate.Size = new System.Drawing.Size(258, 32);
      this.SolutionUpdate.Text = "Update Dependencies";
      this.SolutionUpdate.Click += new System.EventHandler(this.SolutionUpdate_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(255, 6);
      // 
      // SolutionExit
      // 
      this.SolutionExit.Name = "SolutionExit";
      this.SolutionExit.Size = new System.Drawing.Size(258, 32);
      this.SolutionExit.Text = "E&xit";
      // 
      // ProjectGrid
      // 
      this.ProjectGrid.AllowDrop = true;
      this.ProjectGrid.AllowUserToAddRows = false;
      this.ProjectGrid.AllowUserToDeleteRows = false;
      this.ProjectGrid.AllowUserToResizeRows = false;
      this.ProjectGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.ProjectGrid.BackgroundColor = System.Drawing.Color.AliceBlue;
      this.ProjectGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.ProjectGrid.ContextMenuStrip = this.ProjectMenu;
      dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
      dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
      dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.ProjectGrid.DefaultCellStyle = dataGridViewCellStyle4;
      this.ProjectGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
      this.ProjectGrid.LJCAllowDrag = true;
      this.ProjectGrid.LJCAllowSelectionChange = false;
      this.ProjectGrid.LJCDragDataName = null;
      this.ProjectGrid.LJCLastRowIndex = -1;
      this.ProjectGrid.LJCRowHeight = 0;
      this.ProjectGrid.Location = new System.Drawing.Point(0, 31);
      this.ProjectGrid.MultiSelect = false;
      this.ProjectGrid.Name = "ProjectGrid";
      this.ProjectGrid.RowHeadersVisible = false;
      this.ProjectGrid.RowHeadersWidth = 62;
      this.ProjectGrid.RowTemplate.Height = 28;
      this.ProjectGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.ProjectGrid.ShowCellToolTips = false;
      this.ProjectGrid.Size = new System.Drawing.Size(680, 216);
      this.ProjectGrid.TabIndex = 1;
      this.ProjectGrid.Text = "LJCDataGrid";
      this.ProjectGrid.SelectionChanged += new System.EventHandler(this.ProjectGrid_SelectionChanged);
      this.ProjectGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProjectGrid_KeyDown);
      this.ProjectGrid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ProjectGrid_MouseDoubleClick);
      this.ProjectGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ProjectGrid_MouseDown);
      // 
      // ProjectMenu
      // 
      this.ProjectMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.ProjectMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProjectHeading,
            this.ProjectNew,
            this.ProjectEdit,
            this.toolStripSeparator12,
            this.ProjectDelete,
            this.toolStripSeparator13,
            this.ProjectRefresh,
            this.toolStripSeparator14,
            this.ProjectClear,
            this.ProjectUpdate});
      this.ProjectMenu.Name = "AssemblyMenu";
      this.ProjectMenu.Size = new System.Drawing.Size(259, 246);
      this.ProjectMenu.Text = "Assembly Group Menu";
      // 
      // ProjectHeading
      // 
      this.ProjectHeading.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
      this.ProjectHeading.Name = "ProjectHeading";
      this.ProjectHeading.Size = new System.Drawing.Size(258, 32);
      this.ProjectHeading.Text = "Project Menu";
      // 
      // ProjectNew
      // 
      this.ProjectNew.Name = "ProjectNew";
      this.ProjectNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
      this.ProjectNew.Size = new System.Drawing.Size(258, 32);
      this.ProjectNew.Text = "&New";
      this.ProjectNew.Click += new System.EventHandler(this.ProjectNew_Click);
      // 
      // ProjectEdit
      // 
      this.ProjectEdit.Name = "ProjectEdit";
      this.ProjectEdit.ShortcutKeyDisplayString = "ENTER";
      this.ProjectEdit.Size = new System.Drawing.Size(258, 32);
      this.ProjectEdit.Text = "&Edit";
      this.ProjectEdit.Click += new System.EventHandler(this.ProjectEdit_Click);
      // 
      // toolStripSeparator12
      // 
      this.toolStripSeparator12.Name = "toolStripSeparator12";
      this.toolStripSeparator12.Size = new System.Drawing.Size(255, 6);
      // 
      // ProjectDelete
      // 
      this.ProjectDelete.Name = "ProjectDelete";
      this.ProjectDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
      this.ProjectDelete.Size = new System.Drawing.Size(258, 32);
      this.ProjectDelete.Text = "&Delete";
      this.ProjectDelete.Click += new System.EventHandler(this.ProjectDelete_Click);
      // 
      // toolStripSeparator13
      // 
      this.toolStripSeparator13.Name = "toolStripSeparator13";
      this.toolStripSeparator13.Size = new System.Drawing.Size(255, 6);
      // 
      // ProjectRefresh
      // 
      this.ProjectRefresh.Name = "ProjectRefresh";
      this.ProjectRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
      this.ProjectRefresh.Size = new System.Drawing.Size(258, 32);
      this.ProjectRefresh.Text = "&Refresh";
      this.ProjectRefresh.Click += new System.EventHandler(this.ProjectRefresh_Click);
      // 
      // toolStripSeparator14
      // 
      this.toolStripSeparator14.Name = "toolStripSeparator14";
      this.toolStripSeparator14.Size = new System.Drawing.Size(255, 6);
      // 
      // ProjectClear
      // 
      this.ProjectClear.Name = "ProjectClear";
      this.ProjectClear.Size = new System.Drawing.Size(258, 32);
      this.ProjectClear.Text = "Clear Dependencies";
      this.ProjectClear.Click += new System.EventHandler(this.ProjectClear_Click);
      // 
      // ProjectUpdate
      // 
      this.ProjectUpdate.Name = "ProjectUpdate";
      this.ProjectUpdate.Size = new System.Drawing.Size(258, 32);
      this.ProjectUpdate.Text = "Update Dependencies";
      this.ProjectUpdate.Click += new System.EventHandler(this.ProjectUpdate_Click);
      // 
      // ProjectHeader
      // 
      this.ProjectHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.ProjectHeader.LJCBeginColor = System.Drawing.Color.AliceBlue;
      this.ProjectHeader.LJCEndColor = System.Drawing.Color.LightSkyBlue;
      this.ProjectHeader.Location = new System.Drawing.Point(0, 0);
      this.ProjectHeader.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.ProjectHeader.Name = "ProjectHeader";
      this.ProjectHeader.Size = new System.Drawing.Size(680, 32);
      this.ProjectHeader.TabIndex = 0;
      this.ProjectHeader.TabStop = false;
      this.ProjectHeader.Text = "Project";
      // 
      // FileTab
      // 
      this.FileTab.Controls.Add(this.ljcHeaderBox1);
      this.FileTab.Controls.Add(this.ProjectFileGrid);
      this.FileTab.Controls.Add(this.ProjectText);
      this.FileTab.Controls.Add(this.ProjectLabel);
      this.FileTab.Location = new System.Drawing.Point(4, 29);
      this.FileTab.Name = "FileTab";
      this.FileTab.Size = new System.Drawing.Size(687, 417);
      this.FileTab.TabIndex = 2;
      this.FileTab.Text = "File";
      this.FileTab.UseVisualStyleBackColor = true;
      // 
      // ljcHeaderBox1
      // 
      this.ljcHeaderBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.ljcHeaderBox1.LJCBeginColor = System.Drawing.Color.AliceBlue;
      this.ljcHeaderBox1.LJCEndColor = System.Drawing.Color.LightSkyBlue;
      this.ljcHeaderBox1.Location = new System.Drawing.Point(0, 41);
      this.ljcHeaderBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.ljcHeaderBox1.Name = "ljcHeaderBox1";
      this.ljcHeaderBox1.Size = new System.Drawing.Size(680, 32);
      this.ljcHeaderBox1.TabIndex = 4;
      this.ljcHeaderBox1.TabStop = false;
      this.ljcHeaderBox1.Text = "ProjectFile";
      // 
      // ProjectFileGrid
      // 
      this.ProjectFileGrid.AllowDrop = true;
      this.ProjectFileGrid.AllowUserToAddRows = false;
      this.ProjectFileGrid.AllowUserToDeleteRows = false;
      this.ProjectFileGrid.AllowUserToResizeRows = false;
      this.ProjectFileGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.ProjectFileGrid.BackgroundColor = System.Drawing.Color.AliceBlue;
      this.ProjectFileGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.ProjectFileGrid.ContextMenuStrip = this.ProjectFileMenu;
      dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
      dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
      dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.ProjectFileGrid.DefaultCellStyle = dataGridViewCellStyle5;
      this.ProjectFileGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
      this.ProjectFileGrid.LJCAllowDrag = true;
      this.ProjectFileGrid.LJCAllowSelectionChange = false;
      this.ProjectFileGrid.LJCDragDataName = null;
      this.ProjectFileGrid.LJCLastRowIndex = -1;
      this.ProjectFileGrid.LJCRowHeight = 0;
      this.ProjectFileGrid.Location = new System.Drawing.Point(0, 72);
      this.ProjectFileGrid.MultiSelect = false;
      this.ProjectFileGrid.Name = "ProjectFileGrid";
      this.ProjectFileGrid.RowHeadersVisible = false;
      this.ProjectFileGrid.RowHeadersWidth = 62;
      this.ProjectFileGrid.RowTemplate.Height = 28;
      this.ProjectFileGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.ProjectFileGrid.ShowCellToolTips = false;
      this.ProjectFileGrid.Size = new System.Drawing.Size(687, 345);
      this.ProjectFileGrid.TabIndex = 3;
      this.ProjectFileGrid.Text = "LJCDataGrid";
      this.ProjectFileGrid.SelectionChanged += new System.EventHandler(this.FileGrid_SelectionChanged);
      this.ProjectFileGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FileGrid_KeyDown);
      this.ProjectFileGrid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.FileGrid_MouseDoubleClick);
      this.ProjectFileGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FileGrid_MouseDown);
      // 
      // ProjectFileMenu
      // 
      this.ProjectFileMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.ProjectFileMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProjectFileHeading,
            this.ProjectFileNew,
            this.ProjectFileEdit,
            this.toolStripSeparator6,
            this.ProjectFileDelete,
            this.toolStripSeparator7,
            this.ProjectFileRefresh,
            this.toolStripSeparator8,
            this.ProjectFileExit});
      this.ProjectFileMenu.Name = "AssemblyMenu";
      this.ProjectFileMenu.Size = new System.Drawing.Size(220, 214);
      this.ProjectFileMenu.Text = "Assembly Group Menu";
      // 
      // ProjectFileHeading
      // 
      this.ProjectFileHeading.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
      this.ProjectFileHeading.Name = "ProjectFileHeading";
      this.ProjectFileHeading.Size = new System.Drawing.Size(219, 32);
      this.ProjectFileHeading.Text = "Project File Menu";
      // 
      // ProjectFileNew
      // 
      this.ProjectFileNew.Name = "ProjectFileNew";
      this.ProjectFileNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
      this.ProjectFileNew.Size = new System.Drawing.Size(219, 32);
      this.ProjectFileNew.Text = "&New";
      this.ProjectFileNew.Click += new System.EventHandler(this.ProjectFileNew_Click);
      // 
      // ProjectFileEdit
      // 
      this.ProjectFileEdit.Name = "ProjectFileEdit";
      this.ProjectFileEdit.ShortcutKeyDisplayString = "ENTER";
      this.ProjectFileEdit.Size = new System.Drawing.Size(219, 32);
      this.ProjectFileEdit.Text = "&Edit";
      this.ProjectFileEdit.Click += new System.EventHandler(this.ProjectFileEdit_Click);
      // 
      // toolStripSeparator6
      // 
      this.toolStripSeparator6.Name = "toolStripSeparator6";
      this.toolStripSeparator6.Size = new System.Drawing.Size(216, 6);
      // 
      // ProjectFileDelete
      // 
      this.ProjectFileDelete.Name = "ProjectFileDelete";
      this.ProjectFileDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
      this.ProjectFileDelete.Size = new System.Drawing.Size(219, 32);
      this.ProjectFileDelete.Text = "&Delete";
      this.ProjectFileDelete.Click += new System.EventHandler(this.ProjectFileDelete_Click);
      // 
      // toolStripSeparator7
      // 
      this.toolStripSeparator7.Name = "toolStripSeparator7";
      this.toolStripSeparator7.Size = new System.Drawing.Size(216, 6);
      // 
      // ProjectFileRefresh
      // 
      this.ProjectFileRefresh.Name = "ProjectFileRefresh";
      this.ProjectFileRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
      this.ProjectFileRefresh.Size = new System.Drawing.Size(219, 32);
      this.ProjectFileRefresh.Text = "&Refresh";
      this.ProjectFileRefresh.Click += new System.EventHandler(this.ProjectFileRefresh_Click);
      // 
      // toolStripSeparator8
      // 
      this.toolStripSeparator8.Name = "toolStripSeparator8";
      this.toolStripSeparator8.Size = new System.Drawing.Size(216, 6);
      // 
      // ProjectFileExit
      // 
      this.ProjectFileExit.Name = "ProjectFileExit";
      this.ProjectFileExit.Size = new System.Drawing.Size(219, 32);
      this.ProjectFileExit.Text = "E&xit";
      // 
      // ProjectText
      // 
      this.ProjectText.Location = new System.Drawing.Point(107, 6);
      this.ProjectText.Name = "ProjectText";
      this.ProjectText.Size = new System.Drawing.Size(500, 26);
      this.ProjectText.TabIndex = 1;
      // 
      // ProjectLabel
      // 
      this.ProjectLabel.Location = new System.Drawing.Point(7, 9);
      this.ProjectLabel.Name = "ProjectLabel";
      this.ProjectLabel.Size = new System.Drawing.Size(100, 23);
      this.ProjectLabel.TabIndex = 0;
      this.ProjectLabel.Text = "Project";
      // 
      // TileTabs
      // 
      this.TileTabs.AllowDrop = true;
      this.TileTabs.ContextMenuStrip = this.TileTabsMenu;
      this.TileTabs.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TileTabs.LJCAllowDrag = true;
      this.TileTabs.Location = new System.Drawing.Point(0, 0);
      this.TileTabs.Name = "TileTabs";
      this.TileTabs.SelectedIndex = 0;
      this.TileTabs.Size = new System.Drawing.Size(101, 450);
      this.TileTabs.TabIndex = 0;
      this.TileTabs.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TileTabs_MouseDown);
      // 
      // TileTabsMenu
      // 
      this.TileTabsMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.TileTabsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TileTabsMove});
      this.TileTabsMenu.Name = "TileTabsMenu";
      this.TileTabsMenu.Size = new System.Drawing.Size(164, 36);
      // 
      // TileTabsMove
      // 
      this.TileTabsMove.Name = "TileTabsMove";
      this.TileTabsMove.Size = new System.Drawing.Size(163, 32);
      this.TileTabsMove.Text = "Move Left";
      this.TileTabsMove.Click += new System.EventHandler(this.TileTabsMove_Click);
      // 
      // CodeManagerList
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this.TabsSplit);
      this.Name = "CodeManagerList";
      this.Text = "Code Manager";
      this.Load += new System.EventHandler(this.CodeManagerList_Load);
      this.TabsSplit.Panel1.ResumeLayout(false);
      this.TabsSplit.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.TabsSplit)).EndInit();
      this.TabsSplit.ResumeLayout(false);
      this.MainTabs.ResumeLayout(false);
      this.MainTabsMenu.ResumeLayout(false);
      this.CodeLineTab.ResumeLayout(false);
      this.CodeLineSplit.Panel1.ResumeLayout(false);
      this.CodeLineSplit.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.CodeLineSplit)).EndInit();
      this.CodeLineSplit.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.CodeLineGrid)).EndInit();
      this.CodeLineMenu.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.CodeGroupGrid)).EndInit();
      this.CodeGroupMenu.ResumeLayout(false);
      this.SolutionTab.ResumeLayout(false);
      this.SolutionTab.PerformLayout();
      this.SolutionSplit.Panel1.ResumeLayout(false);
      this.SolutionSplit.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.SolutionSplit)).EndInit();
      this.SolutionSplit.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.SolutionGrid)).EndInit();
      this.SolutionMenu.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.ProjectGrid)).EndInit();
      this.ProjectMenu.ResumeLayout(false);
      this.FileTab.ResumeLayout(false);
      this.FileTab.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.ProjectFileGrid)).EndInit();
      this.ProjectFileMenu.ResumeLayout(false);
      this.TileTabsMenu.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer TabsSplit;
    private LJCWinFormControls.LJCTabControl TileTabs;
    private LJCWinFormControls.LJCTabControl MainTabs;
    private System.Windows.Forms.TabPage CodeLineTab;
    private System.Windows.Forms.SplitContainer CodeLineSplit;
    internal LJCWinFormControls.LJCDataGrid CodeLineGrid;
    internal LJCWinFormControls.LJCDataGrid CodeGroupGrid;
    private LJCWinFormControls.LJCHeaderBox CodeGroupHeader;
    private System.Windows.Forms.TabPage SolutionTab;
    internal System.Windows.Forms.TextBox GroupText;
    private System.Windows.Forms.Label GroupLabel;
    private System.Windows.Forms.SplitContainer SolutionSplit;
    internal LJCWinFormControls.LJCDataGrid SolutionGrid;
    internal LJCWinFormControls.LJCDataGrid ProjectGrid;
    private LJCWinFormControls.LJCHeaderBox ProjectHeader;
    private System.Windows.Forms.TabPage FileTab;
    internal System.Windows.Forms.TextBox ProjectText;
    private System.Windows.Forms.Label ProjectLabel;
    internal LJCWinFormControls.LJCDataGrid ProjectFileGrid;
    private System.Windows.Forms.ContextMenuStrip CodeLineMenu;
    private System.Windows.Forms.ToolStripMenuItem CodeLineHeading;
    private System.Windows.Forms.ToolStripMenuItem CodeLineNew;
    private System.Windows.Forms.ToolStripMenuItem CodeLineEdit;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
    private System.Windows.Forms.ToolStripMenuItem CodeLineDelete;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
    private System.Windows.Forms.ToolStripMenuItem CodeLineRefresh;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripMenuItem CodeLineExit;
    private System.Windows.Forms.ContextMenuStrip SolutionMenu;
    private System.Windows.Forms.ToolStripMenuItem SolutionHeading;
    private System.Windows.Forms.ToolStripMenuItem SolutionNew;
    private System.Windows.Forms.ToolStripMenuItem SolutionEdit;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripMenuItem SolutionDelete;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    private System.Windows.Forms.ToolStripMenuItem SolutionRefresh;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    private System.Windows.Forms.ToolStripMenuItem SolutionExit;
    private System.Windows.Forms.ContextMenuStrip ProjectFileMenu;
    private System.Windows.Forms.ToolStripMenuItem ProjectFileHeading;
    private System.Windows.Forms.ToolStripMenuItem ProjectFileNew;
    private System.Windows.Forms.ToolStripMenuItem ProjectFileEdit;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
    private System.Windows.Forms.ToolStripMenuItem ProjectFileDelete;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
    private System.Windows.Forms.ToolStripMenuItem ProjectFileRefresh;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
    private System.Windows.Forms.ToolStripMenuItem ProjectFileExit;
    private System.Windows.Forms.ContextMenuStrip CodeGroupMenu;
    private System.Windows.Forms.ToolStripMenuItem CodeGroupHeading;
    private System.Windows.Forms.ToolStripMenuItem CodeGroupNew;
    private System.Windows.Forms.ToolStripMenuItem CodeGroupEdit;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
    private System.Windows.Forms.ToolStripMenuItem CodeGroupDelete;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
    private System.Windows.Forms.ToolStripMenuItem CodeGroupRefresh;
    private System.Windows.Forms.ContextMenuStrip ProjectMenu;
    private System.Windows.Forms.ToolStripMenuItem ProjectHeading;
    private System.Windows.Forms.ToolStripMenuItem ProjectNew;
    private System.Windows.Forms.ToolStripMenuItem ProjectEdit;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
    private System.Windows.Forms.ToolStripMenuItem ProjectDelete;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
    private System.Windows.Forms.ToolStripMenuItem ProjectRefresh;
    private System.Windows.Forms.ContextMenuStrip MainTabsMenu;
    private System.Windows.Forms.ToolStripMenuItem MainTabsMove;
    private System.Windows.Forms.ContextMenuStrip TileTabsMenu;
    private System.Windows.Forms.ToolStripMenuItem TileTabsMove;
    private System.Windows.Forms.ToolStripMenuItem SolutionClear;
    private System.Windows.Forms.ToolStripMenuItem SolutionUpdate;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem ProjectClear;
    private System.Windows.Forms.ToolStripMenuItem ProjectUpdate;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
    private LJCWinFormControls.LJCHeaderBox SolutionHeader;
    private LJCWinFormControls.LJCHeaderBox ljcHeaderBox1;
  }
}

