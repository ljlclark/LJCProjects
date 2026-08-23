namespace LJCDataUtility5
{
    partial class DataUtilityList
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      components = new System.ComponentModel.Container();
      DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
      DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
      DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
      splitContainer1 = new SplitContainer();
      TableGrid = new LJCControls5.LJCDataGrid(components);
      TableMenu = new ContextMenuStrip(components);
      TableExit = new ToolStripMenuItem();
      ColumnsSplit = new SplitContainer();
      tabControl1 = new TabControl();
      tabPage1 = new TabPage();
      ColumnGrid = new LJCControls5.LJCDataGrid(components);
      ColumnMenu = new ContextMenuStrip(components);
      ColumnExit = new ToolStripMenuItem();
      tabPage2 = new TabPage();
      KeyGrid = new LJCControls5.LJCDataGrid(components);
      KeyMenu = new ContextMenuStrip(components);
      KeyExit = new ToolStripMenuItem();
      tabControl2 = new TabControl();
      ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
      splitContainer1.Panel1.SuspendLayout();
      splitContainer1.Panel2.SuspendLayout();
      splitContainer1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)TableGrid).BeginInit();
      TableMenu.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)ColumnsSplit).BeginInit();
      ColumnsSplit.Panel1.SuspendLayout();
      ColumnsSplit.Panel2.SuspendLayout();
      ColumnsSplit.SuspendLayout();
      tabControl1.SuspendLayout();
      tabPage1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)ColumnGrid).BeginInit();
      ColumnMenu.SuspendLayout();
      tabPage2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)KeyGrid).BeginInit();
      KeyMenu.SuspendLayout();
      SuspendLayout();
      // 
      // splitContainer1
      // 
      splitContainer1.Dock = DockStyle.Fill;
      splitContainer1.Location = new Point(0, 0);
      splitContainer1.Name = "splitContainer1";
      splitContainer1.Orientation = Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      splitContainer1.Panel1.Controls.Add(TableGrid);
      // 
      // splitContainer1.Panel2
      // 
      splitContainer1.Panel2.Controls.Add(ColumnsSplit);
      splitContainer1.Size = new Size(800, 450);
      splitContainer1.SplitterDistance = 162;
      splitContainer1.TabIndex = 1;
      // 
      // TableGrid
      // 
      TableGrid.AllowUserToAddRows = false;
      TableGrid.AllowUserToDeleteRows = false;
      TableGrid.AllowUserToResizeRows = false;
      TableGrid.BackgroundColor = Color.AliceBlue;
      TableGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      TableGrid.ContextMenuStrip = TableMenu;
      dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle4.BackColor = SystemColors.Window;
      dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F);
      dataGridViewCellStyle4.ForeColor = SystemColors.ControlText;
      dataGridViewCellStyle4.SelectionBackColor = SystemColors.ControlLight;
      dataGridViewCellStyle4.SelectionForeColor = Color.Black;
      dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
      TableGrid.DefaultCellStyle = dataGridViewCellStyle4;
      TableGrid.Dock = DockStyle.Fill;
      TableGrid.EditMode = DataGridViewEditMode.EditOnEnter;
      TableGrid.LJCAllowSelectionChange = false;
      TableGrid.LJCDragDataName = "";
      TableGrid.LJCLastRowIndex = -1;
      TableGrid.LJCRowHeight = 0;
      TableGrid.Location = new Point(0, 0);
      TableGrid.MultiSelect = false;
      TableGrid.Name = "TableGrid";
      TableGrid.RowHeadersVisible = false;
      TableGrid.RowHeadersWidth = 62;
      TableGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      TableGrid.ShowCellToolTips = false;
      TableGrid.Size = new Size(800, 162);
      TableGrid.TabIndex = 1;
      // 
      // TableMenu
      // 
      TableMenu.ImageScalingSize = new Size(24, 24);
      TableMenu.Items.AddRange(new ToolStripItem[] { TableExit });
      TableMenu.Name = "ColumnMenu";
      TableMenu.Size = new Size(112, 36);
      // 
      // TableExit
      // 
      TableExit.Name = "TableExit";
      TableExit.Size = new Size(111, 32);
      TableExit.Text = "E&xit";
      TableExit.Click += Exit_Click;
      // 
      // ColumnsSplit
      // 
      ColumnsSplit.Dock = DockStyle.Fill;
      ColumnsSplit.Location = new Point(0, 0);
      ColumnsSplit.Name = "ColumnsSplit";
      // 
      // ColumnsSplit.Panel1
      // 
      ColumnsSplit.Panel1.Controls.Add(tabControl1);
      // 
      // ColumnsSplit.Panel2
      // 
      ColumnsSplit.Panel2.Controls.Add(tabControl2);
      ColumnsSplit.Size = new Size(800, 284);
      ColumnsSplit.SplitterDistance = 653;
      ColumnsSplit.TabIndex = 0;
      // 
      // tabControl1
      // 
      tabControl1.Controls.Add(tabPage1);
      tabControl1.Controls.Add(tabPage2);
      tabControl1.Dock = DockStyle.Fill;
      tabControl1.Location = new Point(0, 0);
      tabControl1.Name = "tabControl1";
      tabControl1.SelectedIndex = 0;
      tabControl1.Size = new Size(653, 284);
      tabControl1.TabIndex = 0;
      // 
      // tabPage1
      // 
      tabPage1.Controls.Add(ColumnGrid);
      tabPage1.Location = new Point(4, 34);
      tabPage1.Name = "tabPage1";
      tabPage1.Padding = new Padding(3);
      tabPage1.Size = new Size(645, 246);
      tabPage1.TabIndex = 0;
      tabPage1.Text = "tabPage1";
      tabPage1.UseVisualStyleBackColor = true;
      // 
      // ColumnGrid
      // 
      ColumnGrid.AllowUserToAddRows = false;
      ColumnGrid.AllowUserToDeleteRows = false;
      ColumnGrid.AllowUserToResizeRows = false;
      ColumnGrid.BackgroundColor = Color.AliceBlue;
      ColumnGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      ColumnGrid.ContextMenuStrip = ColumnMenu;
      dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = SystemColors.Window;
      dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
      dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
      dataGridViewCellStyle1.SelectionBackColor = SystemColors.ControlLight;
      dataGridViewCellStyle1.SelectionForeColor = Color.Black;
      dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
      ColumnGrid.DefaultCellStyle = dataGridViewCellStyle1;
      ColumnGrid.Dock = DockStyle.Fill;
      ColumnGrid.EditMode = DataGridViewEditMode.EditOnEnter;
      ColumnGrid.LJCAllowSelectionChange = false;
      ColumnGrid.LJCDragDataName = "";
      ColumnGrid.LJCLastRowIndex = -1;
      ColumnGrid.LJCRowHeight = 0;
      ColumnGrid.Location = new Point(3, 3);
      ColumnGrid.MultiSelect = false;
      ColumnGrid.Name = "ColumnGrid";
      ColumnGrid.RowHeadersVisible = false;
      ColumnGrid.RowHeadersWidth = 62;
      ColumnGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      ColumnGrid.ShowCellToolTips = false;
      ColumnGrid.Size = new Size(639, 240);
      ColumnGrid.TabIndex = 1;
      // 
      // ColumnMenu
      // 
      ColumnMenu.ImageScalingSize = new Size(24, 24);
      ColumnMenu.Items.AddRange(new ToolStripItem[] { ColumnExit });
      ColumnMenu.Name = "ColumnMenu";
      ColumnMenu.Size = new Size(112, 36);
      // 
      // ColumnExit
      // 
      ColumnExit.Name = "ColumnExit";
      ColumnExit.Size = new Size(111, 32);
      ColumnExit.Text = "E&xit";
      // 
      // tabPage2
      // 
      tabPage2.Controls.Add(KeyGrid);
      tabPage2.Location = new Point(4, 34);
      tabPage2.Name = "tabPage2";
      tabPage2.Padding = new Padding(3);
      tabPage2.Size = new Size(645, 246);
      tabPage2.TabIndex = 1;
      tabPage2.Text = "tabPage2";
      tabPage2.UseVisualStyleBackColor = true;
      // 
      // KeyGrid
      // 
      KeyGrid.AllowUserToAddRows = false;
      KeyGrid.AllowUserToDeleteRows = false;
      KeyGrid.AllowUserToResizeRows = false;
      KeyGrid.BackgroundColor = Color.AliceBlue;
      KeyGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      KeyGrid.ContextMenuStrip = KeyMenu;
      dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle2.BackColor = SystemColors.Window;
      dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
      dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
      dataGridViewCellStyle2.SelectionBackColor = SystemColors.ControlLight;
      dataGridViewCellStyle2.SelectionForeColor = Color.Black;
      dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
      KeyGrid.DefaultCellStyle = dataGridViewCellStyle2;
      KeyGrid.Dock = DockStyle.Fill;
      KeyGrid.EditMode = DataGridViewEditMode.EditOnEnter;
      KeyGrid.LJCAllowSelectionChange = false;
      KeyGrid.LJCDragDataName = "";
      KeyGrid.LJCLastRowIndex = -1;
      KeyGrid.LJCRowHeight = 0;
      KeyGrid.Location = new Point(3, 3);
      KeyGrid.MultiSelect = false;
      KeyGrid.Name = "KeyGrid";
      KeyGrid.RowHeadersVisible = false;
      KeyGrid.RowHeadersWidth = 62;
      KeyGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      KeyGrid.ShowCellToolTips = false;
      KeyGrid.Size = new Size(639, 240);
      KeyGrid.TabIndex = 1;
      // 
      // KeyMenu
      // 
      KeyMenu.ImageScalingSize = new Size(24, 24);
      KeyMenu.Items.AddRange(new ToolStripItem[] { KeyExit });
      KeyMenu.Name = "ColumnMenu";
      KeyMenu.Size = new Size(112, 36);
      // 
      // KeyExit
      // 
      KeyExit.Name = "KeyExit";
      KeyExit.Size = new Size(111, 32);
      KeyExit.Text = "E&xit";
      // 
      // tabControl2
      // 
      tabControl2.Dock = DockStyle.Fill;
      tabControl2.Location = new Point(0, 0);
      tabControl2.Name = "tabControl2";
      tabControl2.SelectedIndex = 0;
      tabControl2.Size = new Size(143, 284);
      tabControl2.TabIndex = 0;
      // 
      // DataUtilityList
      // 
      AutoScaleDimensions = new SizeF(10F, 25F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(800, 450);
      Controls.Add(splitContainer1);
      Name = "DataUtilityList";
      Text = "Form1";
      Load += Form1_Load;
      splitContainer1.Panel1.ResumeLayout(false);
      splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
      splitContainer1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)TableGrid).EndInit();
      TableMenu.ResumeLayout(false);
      ColumnsSplit.Panel1.ResumeLayout(false);
      ColumnsSplit.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)ColumnsSplit).EndInit();
      ColumnsSplit.ResumeLayout(false);
      tabControl1.ResumeLayout(false);
      tabPage1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)ColumnGrid).EndInit();
      ColumnMenu.ResumeLayout(false);
      tabPage2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)KeyGrid).EndInit();
      KeyMenu.ResumeLayout(false);
      ResumeLayout(false);
    }

    #endregion

    internal LJCControls5.LJCDataGrid ColumnGrid;
    internal LJCControls5.LJCDataGrid KeyGrid;
    private SplitContainer splitContainer1;
    internal LJCControls5.LJCDataGrid TableGrid;
    private SplitContainer ColumnsSplit;
    private TabControl tabControl1;
    private TabPage tabPage1;
    private TabPage tabPage2;
    private TabControl tabControl2;
    internal ContextMenuStrip TableMenu;
    internal ToolStripMenuItem TableExit;
    internal ContextMenuStrip ColumnMenu;
    internal ToolStripMenuItem ColumnExit;
    internal ContextMenuStrip KeyMenu;
    internal ToolStripMenuItem KeyExit;
  }
}
