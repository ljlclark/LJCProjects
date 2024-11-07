namespace LJCDataUtility
{
  partial class DataUtilityList
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
      this.ModuleMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.ModuleHeading = new System.Windows.Forms.ToolStripMenuItem();
      this.ModuleRefresh = new System.Windows.Forms.ToolStripMenuItem();
      this.ModuleExit = new System.Windows.Forms.ToolStripMenuItem();
      this.ljcTabControl1 = new LJCWinFormControls.LJCTabControl(this.components);
      this.ModulePage = new System.Windows.Forms.TabPage();
      this.ModuleGrid = new LJCWinFormControls.LJCDataGrid(this.components);
      this.TablePage = new System.Windows.Forms.TabPage();
      this.TableGrid = new LJCWinFormControls.LJCDataGrid(this.components);
      this.TableMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.TableHeading = new System.Windows.Forms.ToolStripMenuItem();
      this.TableRefresh = new System.Windows.Forms.ToolStripMenuItem();
      this.TableExit = new System.Windows.Forms.ToolStripMenuItem();
      this.ColymnPage = new System.Windows.Forms.TabPage();
      this.ColumnGrid = new LJCWinFormControls.LJCDataGrid(this.components);
      this.ColumnMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.ColumnHeading = new System.Windows.Forms.ToolStripMenuItem();
      this.ColumnRefresh = new System.Windows.Forms.ToolStripMenuItem();
      this.ColumnExit = new System.Windows.Forms.ToolStripMenuItem();
      this.KeyPage = new System.Windows.Forms.TabPage();
      this.KeyGrid = new LJCWinFormControls.LJCDataGrid(this.components);
      this.KeyMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.keyMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.KeyRefresh = new System.Windows.Forms.ToolStripMenuItem();
      this.KeyExit = new System.Windows.Forms.ToolStripMenuItem();
      this.MapTablePage = new System.Windows.Forms.TabPage();
      this.MapTableGrid = new LJCWinFormControls.LJCDataGrid(this.components);
      this.MapTableMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.mapTableMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.MapTableRefresh = new System.Windows.Forms.ToolStripMenuItem();
      this.MapTableExit = new System.Windows.Forms.ToolStripMenuItem();
      this.MapColumnPage = new System.Windows.Forms.TabPage();
      this.MapColumnGrid = new LJCWinFormControls.LJCDataGrid(this.components);
      this.MapColumnMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.mapColumnMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.MapColumnRefresh = new System.Windows.Forms.ToolStripMenuItem();
      this.MapColumnExit = new System.Windows.Forms.ToolStripMenuItem();
      this.ModuleMenu.SuspendLayout();
      this.ljcTabControl1.SuspendLayout();
      this.ModulePage.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.ModuleGrid)).BeginInit();
      this.TablePage.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.TableGrid)).BeginInit();
      this.TableMenu.SuspendLayout();
      this.ColymnPage.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.ColumnGrid)).BeginInit();
      this.ColumnMenu.SuspendLayout();
      this.KeyPage.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.KeyGrid)).BeginInit();
      this.KeyMenu.SuspendLayout();
      this.MapTablePage.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.MapTableGrid)).BeginInit();
      this.MapTableMenu.SuspendLayout();
      this.MapColumnPage.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.MapColumnGrid)).BeginInit();
      this.MapColumnMenu.SuspendLayout();
      this.SuspendLayout();
      // 
      // ModuleMenu
      // 
      this.ModuleMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.ModuleMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ModuleHeading,
            this.ModuleRefresh,
            this.ModuleExit});
      this.ModuleMenu.Name = "ModuleMenu";
      this.ModuleMenu.Size = new System.Drawing.Size(196, 100);
      // 
      // ModuleHeading
      // 
      this.ModuleHeading.BackColor = System.Drawing.SystemColors.Highlight;
      this.ModuleHeading.Name = "ModuleHeading";
      this.ModuleHeading.Size = new System.Drawing.Size(195, 32);
      this.ModuleHeading.Text = "Module Menu";
      // 
      // ModuleRefresh
      // 
      this.ModuleRefresh.Name = "ModuleRefresh";
      this.ModuleRefresh.Size = new System.Drawing.Size(195, 32);
      this.ModuleRefresh.Text = "&Refresh";
      this.ModuleRefresh.Click += new System.EventHandler(this.ModuleRefresh_Click);
      // 
      // ModuleExit
      // 
      this.ModuleExit.Name = "ModuleExit";
      this.ModuleExit.Size = new System.Drawing.Size(195, 32);
      this.ModuleExit.Text = "E&xit";
      this.ModuleExit.Click += new System.EventHandler(this.ModuleExit_Click);
      // 
      // ljcTabControl1
      // 
      this.ljcTabControl1.Controls.Add(this.ModulePage);
      this.ljcTabControl1.Controls.Add(this.TablePage);
      this.ljcTabControl1.Controls.Add(this.ColymnPage);
      this.ljcTabControl1.Controls.Add(this.KeyPage);
      this.ljcTabControl1.Controls.Add(this.MapTablePage);
      this.ljcTabControl1.Controls.Add(this.MapColumnPage);
      this.ljcTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ljcTabControl1.Location = new System.Drawing.Point(0, 0);
      this.ljcTabControl1.Name = "ljcTabControl1";
      this.ljcTabControl1.SelectedIndex = 0;
      this.ljcTabControl1.Size = new System.Drawing.Size(800, 450);
      this.ljcTabControl1.TabIndex = 1;
      // 
      // ModulePage
      // 
      this.ModulePage.Controls.Add(this.ModuleGrid);
      this.ModulePage.Location = new System.Drawing.Point(4, 29);
      this.ModulePage.Name = "ModulePage";
      this.ModulePage.Padding = new System.Windows.Forms.Padding(3);
      this.ModulePage.Size = new System.Drawing.Size(792, 417);
      this.ModulePage.TabIndex = 0;
      this.ModulePage.Text = "Module";
      this.ModulePage.UseVisualStyleBackColor = true;
      // 
      // ModuleGrid
      // 
      this.ModuleGrid.AllowUserToAddRows = false;
      this.ModuleGrid.AllowUserToDeleteRows = false;
      this.ModuleGrid.AllowUserToResizeRows = false;
      this.ModuleGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.ModuleGrid.BackgroundColor = System.Drawing.SystemColors.Control;
      this.ModuleGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.ModuleGrid.ContextMenuStrip = this.ModuleMenu;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.ModuleGrid.DefaultCellStyle = dataGridViewCellStyle1;
      this.ModuleGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
      this.ModuleGrid.LJCAllowSelectionChange = false;
      this.ModuleGrid.LJCDragDataName = null;
      this.ModuleGrid.LJCLastRowIndex = -1;
      this.ModuleGrid.LJCRowHeight = 0;
      this.ModuleGrid.Location = new System.Drawing.Point(0, 0);
      this.ModuleGrid.MultiSelect = false;
      this.ModuleGrid.Name = "ModuleGrid";
      this.ModuleGrid.RowHeadersVisible = false;
      this.ModuleGrid.RowHeadersWidth = 62;
      this.ModuleGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.ModuleGrid.ShowCellToolTips = false;
      this.ModuleGrid.Size = new System.Drawing.Size(786, 411);
      this.ModuleGrid.TabIndex = 1;
      this.ModuleGrid.Text = "LJCDataGrid";
      // 
      // TablePage
      // 
      this.TablePage.Controls.Add(this.TableGrid);
      this.TablePage.Location = new System.Drawing.Point(4, 29);
      this.TablePage.Name = "TablePage";
      this.TablePage.Padding = new System.Windows.Forms.Padding(3);
      this.TablePage.Size = new System.Drawing.Size(792, 417);
      this.TablePage.TabIndex = 1;
      this.TablePage.Text = "Table";
      this.TablePage.UseVisualStyleBackColor = true;
      // 
      // TableGrid
      // 
      this.TableGrid.AllowUserToAddRows = false;
      this.TableGrid.AllowUserToDeleteRows = false;
      this.TableGrid.AllowUserToResizeRows = false;
      this.TableGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.TableGrid.BackgroundColor = System.Drawing.SystemColors.Control;
      this.TableGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.TableGrid.ContextMenuStrip = this.TableMenu;
      dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
      dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
      dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.TableGrid.DefaultCellStyle = dataGridViewCellStyle2;
      this.TableGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
      this.TableGrid.LJCAllowSelectionChange = false;
      this.TableGrid.LJCDragDataName = null;
      this.TableGrid.LJCLastRowIndex = -1;
      this.TableGrid.LJCRowHeight = 0;
      this.TableGrid.Location = new System.Drawing.Point(3, 3);
      this.TableGrid.MultiSelect = false;
      this.TableGrid.Name = "TableGrid";
      this.TableGrid.RowHeadersVisible = false;
      this.TableGrid.RowHeadersWidth = 62;
      this.TableGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.TableGrid.ShowCellToolTips = false;
      this.TableGrid.Size = new System.Drawing.Size(786, 411);
      this.TableGrid.TabIndex = 2;
      this.TableGrid.Text = "LJCDataGrid";
      // 
      // TableMenu
      // 
      this.TableMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.TableMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TableHeading,
            this.TableRefresh,
            this.TableExit});
      this.TableMenu.Name = "TableMenu";
      this.TableMenu.Size = new System.Drawing.Size(175, 100);
      // 
      // TableHeading
      // 
      this.TableHeading.BackColor = System.Drawing.SystemColors.Highlight;
      this.TableHeading.Name = "TableHeading";
      this.TableHeading.Size = new System.Drawing.Size(174, 32);
      this.TableHeading.Text = "Table Menu";
      // 
      // TableRefresh
      // 
      this.TableRefresh.Name = "TableRefresh";
      this.TableRefresh.Size = new System.Drawing.Size(174, 32);
      this.TableRefresh.Text = "&Refresh";
      this.TableRefresh.Click += new System.EventHandler(this.TableRefresh_Click);
      // 
      // TableExit
      // 
      this.TableExit.Name = "TableExit";
      this.TableExit.Size = new System.Drawing.Size(174, 32);
      this.TableExit.Text = "E&xit";
      this.TableExit.Click += new System.EventHandler(this.ModuleExit_Click);
      // 
      // ColymnPage
      // 
      this.ColymnPage.Controls.Add(this.ColumnGrid);
      this.ColymnPage.Location = new System.Drawing.Point(4, 29);
      this.ColymnPage.Name = "ColymnPage";
      this.ColymnPage.Size = new System.Drawing.Size(792, 417);
      this.ColymnPage.TabIndex = 2;
      this.ColymnPage.Text = "Column";
      this.ColymnPage.UseVisualStyleBackColor = true;
      // 
      // ColumnGrid
      // 
      this.ColumnGrid.AllowUserToAddRows = false;
      this.ColumnGrid.AllowUserToDeleteRows = false;
      this.ColumnGrid.AllowUserToResizeRows = false;
      this.ColumnGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.ColumnGrid.BackgroundColor = System.Drawing.SystemColors.Control;
      this.ColumnGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.ColumnGrid.ContextMenuStrip = this.ColumnMenu;
      dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
      dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
      dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.ColumnGrid.DefaultCellStyle = dataGridViewCellStyle3;
      this.ColumnGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
      this.ColumnGrid.LJCAllowSelectionChange = false;
      this.ColumnGrid.LJCDragDataName = null;
      this.ColumnGrid.LJCLastRowIndex = -1;
      this.ColumnGrid.LJCRowHeight = 0;
      this.ColumnGrid.Location = new System.Drawing.Point(3, 3);
      this.ColumnGrid.MultiSelect = false;
      this.ColumnGrid.Name = "ColumnGrid";
      this.ColumnGrid.RowHeadersVisible = false;
      this.ColumnGrid.RowHeadersWidth = 62;
      this.ColumnGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.ColumnGrid.ShowCellToolTips = false;
      this.ColumnGrid.Size = new System.Drawing.Size(786, 411);
      this.ColumnGrid.TabIndex = 2;
      this.ColumnGrid.Text = "LJCDataGrid";
      // 
      // ColumnMenu
      // 
      this.ColumnMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.ColumnMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ColumnHeading,
            this.ColumnRefresh,
            this.ColumnExit});
      this.ColumnMenu.Name = "ColumnMenu";
      this.ColumnMenu.Size = new System.Drawing.Size(197, 100);
      // 
      // ColumnHeading
      // 
      this.ColumnHeading.BackColor = System.Drawing.SystemColors.Highlight;
      this.ColumnHeading.Name = "ColumnHeading";
      this.ColumnHeading.Size = new System.Drawing.Size(196, 32);
      this.ColumnHeading.Text = "Column Menu";
      // 
      // ColumnRefresh
      // 
      this.ColumnRefresh.Name = "ColumnRefresh";
      this.ColumnRefresh.Size = new System.Drawing.Size(196, 32);
      this.ColumnRefresh.Text = "&Refresh";
      this.ColumnRefresh.Click += new System.EventHandler(this.ColumnRefresh_Click);
      // 
      // ColumnExit
      // 
      this.ColumnExit.Name = "ColumnExit";
      this.ColumnExit.Size = new System.Drawing.Size(196, 32);
      this.ColumnExit.Text = "E&xit";
      this.ColumnExit.Click += new System.EventHandler(this.ModuleExit_Click);
      // 
      // KeyPage
      // 
      this.KeyPage.Controls.Add(this.KeyGrid);
      this.KeyPage.Location = new System.Drawing.Point(4, 29);
      this.KeyPage.Name = "KeyPage";
      this.KeyPage.Size = new System.Drawing.Size(792, 417);
      this.KeyPage.TabIndex = 3;
      this.KeyPage.Text = "Key";
      this.KeyPage.UseVisualStyleBackColor = true;
      // 
      // KeyGrid
      // 
      this.KeyGrid.AllowUserToAddRows = false;
      this.KeyGrid.AllowUserToDeleteRows = false;
      this.KeyGrid.AllowUserToResizeRows = false;
      this.KeyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.KeyGrid.BackgroundColor = System.Drawing.SystemColors.Control;
      this.KeyGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.KeyGrid.ContextMenuStrip = this.KeyMenu;
      dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
      dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
      dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.KeyGrid.DefaultCellStyle = dataGridViewCellStyle4;
      this.KeyGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
      this.KeyGrid.LJCAllowSelectionChange = false;
      this.KeyGrid.LJCDragDataName = null;
      this.KeyGrid.LJCLastRowIndex = -1;
      this.KeyGrid.LJCRowHeight = 0;
      this.KeyGrid.Location = new System.Drawing.Point(3, 3);
      this.KeyGrid.MultiSelect = false;
      this.KeyGrid.Name = "KeyGrid";
      this.KeyGrid.RowHeadersVisible = false;
      this.KeyGrid.RowHeadersWidth = 62;
      this.KeyGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.KeyGrid.ShowCellToolTips = false;
      this.KeyGrid.Size = new System.Drawing.Size(786, 411);
      this.KeyGrid.TabIndex = 2;
      this.KeyGrid.Text = "LJCDataGrid";
      // 
      // KeyMenu
      // 
      this.KeyMenu.BackColor = System.Drawing.SystemColors.Highlight;
      this.KeyMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.KeyMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.keyMenuToolStripMenuItem,
            this.KeyRefresh,
            this.KeyExit});
      this.KeyMenu.Name = "KeyMenu";
      this.KeyMenu.Size = new System.Drawing.Size(163, 100);
      // 
      // keyMenuToolStripMenuItem
      // 
      this.keyMenuToolStripMenuItem.Name = "keyMenuToolStripMenuItem";
      this.keyMenuToolStripMenuItem.Size = new System.Drawing.Size(162, 32);
      this.keyMenuToolStripMenuItem.Text = "Key Menu";
      // 
      // KeyRefresh
      // 
      this.KeyRefresh.BackColor = System.Drawing.SystemColors.Control;
      this.KeyRefresh.Name = "KeyRefresh";
      this.KeyRefresh.Size = new System.Drawing.Size(162, 32);
      this.KeyRefresh.Text = "&Refresh";
      this.KeyRefresh.Click += new System.EventHandler(this.KeyRefresh_Click);
      // 
      // KeyExit
      // 
      this.KeyExit.BackColor = System.Drawing.SystemColors.Control;
      this.KeyExit.Name = "KeyExit";
      this.KeyExit.Size = new System.Drawing.Size(162, 32);
      this.KeyExit.Text = "E&xit";
      this.KeyExit.Click += new System.EventHandler(this.ModuleExit_Click);
      // 
      // MapTablePage
      // 
      this.MapTablePage.Controls.Add(this.MapTableGrid);
      this.MapTablePage.Location = new System.Drawing.Point(4, 29);
      this.MapTablePage.Name = "MapTablePage";
      this.MapTablePage.Size = new System.Drawing.Size(792, 417);
      this.MapTablePage.TabIndex = 4;
      this.MapTablePage.Text = "MapTable";
      this.MapTablePage.UseVisualStyleBackColor = true;
      // 
      // MapTableGrid
      // 
      this.MapTableGrid.AllowUserToAddRows = false;
      this.MapTableGrid.AllowUserToDeleteRows = false;
      this.MapTableGrid.AllowUserToResizeRows = false;
      this.MapTableGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.MapTableGrid.BackgroundColor = System.Drawing.SystemColors.Control;
      this.MapTableGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.MapTableGrid.ContextMenuStrip = this.MapTableMenu;
      dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
      dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
      dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.MapTableGrid.DefaultCellStyle = dataGridViewCellStyle5;
      this.MapTableGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
      this.MapTableGrid.LJCAllowSelectionChange = false;
      this.MapTableGrid.LJCDragDataName = null;
      this.MapTableGrid.LJCLastRowIndex = -1;
      this.MapTableGrid.LJCRowHeight = 0;
      this.MapTableGrid.Location = new System.Drawing.Point(3, 3);
      this.MapTableGrid.MultiSelect = false;
      this.MapTableGrid.Name = "MapTableGrid";
      this.MapTableGrid.RowHeadersVisible = false;
      this.MapTableGrid.RowHeadersWidth = 62;
      this.MapTableGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.MapTableGrid.ShowCellToolTips = false;
      this.MapTableGrid.Size = new System.Drawing.Size(786, 411);
      this.MapTableGrid.TabIndex = 2;
      this.MapTableGrid.Text = "LJCDataGrid";
      // 
      // MapTableMenu
      // 
      this.MapTableMenu.BackColor = System.Drawing.SystemColors.Highlight;
      this.MapTableMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.MapTableMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mapTableMenuToolStripMenuItem,
            this.MapTableRefresh,
            this.MapTableExit});
      this.MapTableMenu.Name = "MapTableMenu";
      this.MapTableMenu.Size = new System.Drawing.Size(216, 100);
      // 
      // mapTableMenuToolStripMenuItem
      // 
      this.mapTableMenuToolStripMenuItem.Name = "mapTableMenuToolStripMenuItem";
      this.mapTableMenuToolStripMenuItem.Size = new System.Drawing.Size(215, 32);
      this.mapTableMenuToolStripMenuItem.Text = "Map Table Menu";
      // 
      // MapTableRefresh
      // 
      this.MapTableRefresh.BackColor = System.Drawing.SystemColors.Control;
      this.MapTableRefresh.Name = "MapTableRefresh";
      this.MapTableRefresh.Size = new System.Drawing.Size(215, 32);
      this.MapTableRefresh.Text = "&Refresh";
      this.MapTableRefresh.Click += new System.EventHandler(this.MapTableRefresh_Click);
      // 
      // MapTableExit
      // 
      this.MapTableExit.BackColor = System.Drawing.SystemColors.Control;
      this.MapTableExit.Name = "MapTableExit";
      this.MapTableExit.Size = new System.Drawing.Size(215, 32);
      this.MapTableExit.Text = "E&xit";
      this.MapTableExit.Click += new System.EventHandler(this.ModuleExit_Click);
      // 
      // MapColumnPage
      // 
      this.MapColumnPage.Controls.Add(this.MapColumnGrid);
      this.MapColumnPage.Location = new System.Drawing.Point(4, 29);
      this.MapColumnPage.Name = "MapColumnPage";
      this.MapColumnPage.Size = new System.Drawing.Size(792, 417);
      this.MapColumnPage.TabIndex = 5;
      this.MapColumnPage.Text = "MapColumn";
      this.MapColumnPage.UseVisualStyleBackColor = true;
      // 
      // MapColumnGrid
      // 
      this.MapColumnGrid.AllowUserToAddRows = false;
      this.MapColumnGrid.AllowUserToDeleteRows = false;
      this.MapColumnGrid.AllowUserToResizeRows = false;
      this.MapColumnGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.MapColumnGrid.BackgroundColor = System.Drawing.SystemColors.Control;
      this.MapColumnGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.MapColumnGrid.ContextMenuStrip = this.MapColumnMenu;
      dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
      dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
      dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.MapColumnGrid.DefaultCellStyle = dataGridViewCellStyle6;
      this.MapColumnGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
      this.MapColumnGrid.LJCAllowSelectionChange = false;
      this.MapColumnGrid.LJCDragDataName = null;
      this.MapColumnGrid.LJCLastRowIndex = -1;
      this.MapColumnGrid.LJCRowHeight = 0;
      this.MapColumnGrid.Location = new System.Drawing.Point(3, 3);
      this.MapColumnGrid.MultiSelect = false;
      this.MapColumnGrid.Name = "MapColumnGrid";
      this.MapColumnGrid.RowHeadersVisible = false;
      this.MapColumnGrid.RowHeadersWidth = 62;
      this.MapColumnGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.MapColumnGrid.ShowCellToolTips = false;
      this.MapColumnGrid.Size = new System.Drawing.Size(786, 411);
      this.MapColumnGrid.TabIndex = 2;
      this.MapColumnGrid.Text = "LJCDataGrid";
      // 
      // MapColumnMenu
      // 
      this.MapColumnMenu.BackColor = System.Drawing.SystemColors.Highlight;
      this.MapColumnMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.MapColumnMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mapColumnMenuToolStripMenuItem,
            this.MapColumnRefresh,
            this.MapColumnExit});
      this.MapColumnMenu.Name = "MapColumnMenu";
      this.MapColumnMenu.Size = new System.Drawing.Size(238, 100);
      // 
      // mapColumnMenuToolStripMenuItem
      // 
      this.mapColumnMenuToolStripMenuItem.Name = "mapColumnMenuToolStripMenuItem";
      this.mapColumnMenuToolStripMenuItem.Size = new System.Drawing.Size(237, 32);
      this.mapColumnMenuToolStripMenuItem.Text = "Map Column Menu";
      // 
      // MapColumnRefresh
      // 
      this.MapColumnRefresh.BackColor = System.Drawing.SystemColors.Control;
      this.MapColumnRefresh.Name = "MapColumnRefresh";
      this.MapColumnRefresh.Size = new System.Drawing.Size(237, 32);
      this.MapColumnRefresh.Text = "&Refresh";
      this.MapColumnRefresh.Click += new System.EventHandler(this.MapColumnRefresh_Click);
      // 
      // MapColumnExit
      // 
      this.MapColumnExit.BackColor = System.Drawing.SystemColors.Control;
      this.MapColumnExit.Name = "MapColumnExit";
      this.MapColumnExit.Size = new System.Drawing.Size(237, 32);
      this.MapColumnExit.Text = "E&xit";
      this.MapColumnExit.Click += new System.EventHandler(this.ModuleExit_Click);
      // 
      // DataUtilityList
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this.ljcTabControl1);
      this.Name = "DataUtilityList";
      this.Text = "Form1";
      this.ModuleMenu.ResumeLayout(false);
      this.ljcTabControl1.ResumeLayout(false);
      this.ModulePage.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.ModuleGrid)).EndInit();
      this.TablePage.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.TableGrid)).EndInit();
      this.TableMenu.ResumeLayout(false);
      this.ColymnPage.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.ColumnGrid)).EndInit();
      this.ColumnMenu.ResumeLayout(false);
      this.KeyPage.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.KeyGrid)).EndInit();
      this.KeyMenu.ResumeLayout(false);
      this.MapTablePage.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.MapTableGrid)).EndInit();
      this.MapTableMenu.ResumeLayout(false);
      this.MapColumnPage.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.MapColumnGrid)).EndInit();
      this.MapColumnMenu.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion
    internal System.Windows.Forms.ContextMenuStrip ModuleMenu;
    private System.Windows.Forms.ToolStripMenuItem ModuleExit;
    internal LJCWinFormControls.LJCTabControl ljcTabControl1;
    private System.Windows.Forms.TabPage ModulePage;
    private System.Windows.Forms.TabPage TablePage;
    private System.Windows.Forms.TabPage ColymnPage;
    private System.Windows.Forms.TabPage KeyPage;
    private System.Windows.Forms.TabPage MapTablePage;
    private System.Windows.Forms.TabPage MapColumnPage;
    internal LJCWinFormControls.LJCDataGrid ModuleGrid;
    internal LJCWinFormControls.LJCDataGrid TableGrid;
    internal LJCWinFormControls.LJCDataGrid ColumnGrid;
    internal LJCWinFormControls.LJCDataGrid KeyGrid;
    internal LJCWinFormControls.LJCDataGrid MapTableGrid;
    internal LJCWinFormControls.LJCDataGrid MapColumnGrid;
    private System.Windows.Forms.ToolStripMenuItem ModuleRefresh;
    internal System.Windows.Forms.ContextMenuStrip TableMenu;
    private System.Windows.Forms.ToolStripMenuItem TableRefresh;
    private System.Windows.Forms.ToolStripMenuItem TableExit;
    internal System.Windows.Forms.ContextMenuStrip ColumnMenu;
    private System.Windows.Forms.ToolStripMenuItem ColumnHeading;
    private System.Windows.Forms.ToolStripMenuItem ColumnRefresh;
    private System.Windows.Forms.ToolStripMenuItem ColumnExit;
    private System.Windows.Forms.ToolStripMenuItem TableHeading;
    private System.Windows.Forms.ToolStripMenuItem ModuleHeading;
    internal System.Windows.Forms.ContextMenuStrip KeyMenu;
    private System.Windows.Forms.ToolStripMenuItem keyMenuToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem KeyRefresh;
    private System.Windows.Forms.ToolStripMenuItem KeyExit;
    internal System.Windows.Forms.ContextMenuStrip MapTableMenu;
    private System.Windows.Forms.ToolStripMenuItem mapTableMenuToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem MapTableRefresh;
    private System.Windows.Forms.ToolStripMenuItem MapTableExit;
    internal System.Windows.Forms.ContextMenuStrip MapColumnMenu;
    private System.Windows.Forms.ToolStripMenuItem mapColumnMenuToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem MapColumnRefresh;
    private System.Windows.Forms.ToolStripMenuItem MapColumnExit;
  }
}

