using LJCControls5;

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
      DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
      DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
      DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
      MainSplit = new SplitContainer();
      TableGrid = new LJCDataGrid(components);
      TableMenu = new ContextMenuStrip(components);
      Table = new ToolStripMenuItem();
      TableNew = new ToolStripMenuItem();
      TableEdit = new ToolStripMenuItem();
      toolStripSeparator1 = new ToolStripSeparator();
      TableDelete = new ToolStripMenuItem();
      toolStripSeparator2 = new ToolStripSeparator();
      TableRefresh = new ToolStripMenuItem();
      toolStripSeparator3 = new ToolStripSeparator();
      TableExit = new ToolStripMenuItem();
      ColumnsSplit = new SplitContainer();
      ColumnTabs = new LJCTabControl(components);
      ColumnTabMenu = new ContextMenuStrip(components);
      ColumnTabMove = new ToolStripMenuItem();
      ColumnPage = new TabPage();
      ColumnGrid = new LJCDataGrid(components);
      ColumnMenu = new ContextMenuStrip(components);
      toolStripMenuItem1 = new ToolStripMenuItem();
      ColumnNew = new ToolStripMenuItem();
      ColumnEdit = new ToolStripMenuItem();
      toolStripSeparator4 = new ToolStripSeparator();
      ColumnDelete = new ToolStripMenuItem();
      toolStripSeparator5 = new ToolStripSeparator();
      ColumnRefresh = new ToolStripMenuItem();
      toolStripSeparator6 = new ToolStripSeparator();
      ColumnExit = new ToolStripMenuItem();
      KeyPage = new TabPage();
      KeyGrid = new LJCDataGrid(components);
      KeyMenu = new ContextMenuStrip(components);
      toolStripMenuItem2 = new ToolStripMenuItem();
      KeyNew = new ToolStripMenuItem();
      KeyEdit = new ToolStripMenuItem();
      toolStripSeparator7 = new ToolStripSeparator();
      KeyDelete = new ToolStripMenuItem();
      toolStripSeparator8 = new ToolStripSeparator();
      KeyRefresh = new ToolStripMenuItem();
      toolStripSeparator9 = new ToolStripSeparator();
      KeyExit = new ToolStripMenuItem();
      TileTabs = new LJCTabControl(components);
      KeyTabMenu = new ContextMenuStrip(components);
      KeyTabMove = new ToolStripMenuItem();
      ModuleCombo = new LJCItemCombo();
      ModuleMenu = new ContextMenuStrip(components);
      Module = new ToolStripMenuItem();
      ModuleNew = new ToolStripMenuItem();
      ModuleEdit = new ToolStripMenuItem();
      toolStripSeparator10 = new ToolStripSeparator();
      ModuleDelete = new ToolStripMenuItem();
      toolStripSeparator11 = new ToolStripSeparator();
      ModuleRefresh = new ToolStripMenuItem();
      toolStripSeparator12 = new ToolStripSeparator();
      ModuleExit = new ToolStripMenuItem();
      ModuleLabel = new Label();
      ConfigLabel = new Label();
      ConfigCombo = new LJCItemCombo();
      TableUpdate = new ToolStripMenuItem();
      ((System.ComponentModel.ISupportInitialize)MainSplit).BeginInit();
      MainSplit.Panel1.SuspendLayout();
      MainSplit.Panel2.SuspendLayout();
      MainSplit.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)TableGrid).BeginInit();
      TableMenu.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)ColumnsSplit).BeginInit();
      ColumnsSplit.Panel1.SuspendLayout();
      ColumnsSplit.Panel2.SuspendLayout();
      ColumnsSplit.SuspendLayout();
      ColumnTabs.SuspendLayout();
      ColumnTabMenu.SuspendLayout();
      ColumnPage.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)ColumnGrid).BeginInit();
      ColumnMenu.SuspendLayout();
      KeyPage.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)KeyGrid).BeginInit();
      KeyMenu.SuspendLayout();
      KeyTabMenu.SuspendLayout();
      ModuleMenu.SuspendLayout();
      SuspendLayout();
      // 
      // MainSplit
      // 
      MainSplit.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      MainSplit.Location = new Point(0, 56);
      MainSplit.Name = "MainSplit";
      MainSplit.Orientation = Orientation.Horizontal;
      // 
      // MainSplit.Panel1
      // 
      MainSplit.Panel1.Controls.Add(TableGrid);
      // 
      // MainSplit.Panel2
      // 
      MainSplit.Panel2.Controls.Add(ColumnsSplit);
      MainSplit.Size = new Size(800, 394);
      MainSplit.SplitterDistance = 140;
      MainSplit.SplitterWidth = 6;
      MainSplit.TabIndex = 4;
      // 
      // TableGrid
      // 
      TableGrid.AllowUserToAddRows = false;
      TableGrid.AllowUserToDeleteRows = false;
      TableGrid.AllowUserToResizeRows = false;
      TableGrid.BackgroundColor = Color.AliceBlue;
      TableGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      TableGrid.ContextMenuStrip = TableMenu;
      dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = SystemColors.Window;
      dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
      dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
      dataGridViewCellStyle1.SelectionBackColor = SystemColors.ControlLight;
      dataGridViewCellStyle1.SelectionForeColor = Color.Black;
      dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
      TableGrid.DefaultCellStyle = dataGridViewCellStyle1;
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
      TableGrid.Size = new Size(800, 140);
      TableGrid.TabIndex = 5;
      // 
      // TableMenu
      // 
      TableMenu.ImageScalingSize = new Size(24, 24);
      TableMenu.Items.AddRange(new ToolStripItem[] { Table, TableNew, TableEdit, toolStripSeparator1, TableDelete, toolStripSeparator2, TableRefresh, toolStripSeparator3, TableUpdate, TableExit });
      TableMenu.Name = "ColumnMenu";
      TableMenu.Size = new Size(241, 279);
      // 
      // Table
      // 
      Table.BackColor = SystemColors.GradientActiveCaption;
      Table.Name = "Table";
      Table.Size = new Size(240, 32);
      Table.Text = "Table";
      // 
      // TableNew
      // 
      TableNew.Name = "TableNew";
      TableNew.ShortcutKeys = Keys.Control | Keys.N;
      TableNew.Size = new Size(240, 32);
      TableNew.Text = "&New";
      // 
      // TableEdit
      // 
      TableEdit.Name = "TableEdit";
      TableEdit.ShortcutKeyDisplayString = "ENTER";
      TableEdit.Size = new Size(240, 32);
      TableEdit.Text = "&Edit";
      // 
      // toolStripSeparator1
      // 
      toolStripSeparator1.Name = "toolStripSeparator1";
      toolStripSeparator1.Size = new Size(237, 6);
      // 
      // TableDelete
      // 
      TableDelete.Name = "TableDelete";
      TableDelete.ShortcutKeys = Keys.Delete;
      TableDelete.Size = new Size(240, 32);
      TableDelete.Text = "&Delete";
      // 
      // toolStripSeparator2
      // 
      toolStripSeparator2.Name = "toolStripSeparator2";
      toolStripSeparator2.Size = new Size(237, 6);
      // 
      // TableRefresh
      // 
      TableRefresh.Name = "TableRefresh";
      TableRefresh.ShortcutKeys = Keys.F5;
      TableRefresh.Size = new Size(240, 32);
      TableRefresh.Text = "&Refresh";
      // 
      // toolStripSeparator3
      // 
      toolStripSeparator3.Name = "toolStripSeparator3";
      toolStripSeparator3.Size = new Size(237, 6);
      // 
      // TableExit
      // 
      TableExit.Name = "TableExit";
      TableExit.Size = new Size(240, 32);
      TableExit.Text = "E&xit";
      // 
      // ColumnsSplit
      // 
      ColumnsSplit.Dock = DockStyle.Fill;
      ColumnsSplit.Location = new Point(0, 0);
      ColumnsSplit.Name = "ColumnsSplit";
      // 
      // ColumnsSplit.Panel1
      // 
      ColumnsSplit.Panel1.Controls.Add(ColumnTabs);
      // 
      // ColumnsSplit.Panel2
      // 
      ColumnsSplit.Panel2.Controls.Add(TileTabs);
      ColumnsSplit.Size = new Size(800, 248);
      ColumnsSplit.SplitterDistance = 653;
      ColumnsSplit.TabIndex = 6;
      // 
      // ColumnTabs
      // 
      ColumnTabs.AllowDrop = true;
      ColumnTabs.ContextMenuStrip = ColumnTabMenu;
      ColumnTabs.Controls.Add(ColumnPage);
      ColumnTabs.Controls.Add(KeyPage);
      ColumnTabs.Dock = DockStyle.Fill;
      ColumnTabs.LJCAllowDrag = true;
      ColumnTabs.Location = new Point(0, 0);
      ColumnTabs.Name = "ColumnTabs";
      ColumnTabs.SelectedIndex = 0;
      ColumnTabs.Size = new Size(653, 248);
      ColumnTabs.TabIndex = 7;
      // 
      // ColumnTabMenu
      // 
      ColumnTabMenu.ImageScalingSize = new Size(24, 24);
      ColumnTabMenu.Items.AddRange(new ToolStripItem[] { ColumnTabMove });
      ColumnTabMenu.Name = "ColumnTabMenu";
      ColumnTabMenu.Size = new Size(209, 36);
      // 
      // ColumnTabMove
      // 
      ColumnTabMove.Name = "ColumnTabMove";
      ColumnTabMove.Size = new Size(208, 32);
      ColumnTabMove.Text = "Move Tab Right";
      // 
      // ColumnPage
      // 
      ColumnPage.Controls.Add(ColumnGrid);
      ColumnPage.Location = new Point(4, 34);
      ColumnPage.Name = "ColumnPage";
      ColumnPage.Padding = new Padding(3);
      ColumnPage.Size = new Size(645, 210);
      ColumnPage.TabIndex = 0;
      ColumnPage.Text = "Column";
      ColumnPage.UseVisualStyleBackColor = true;
      // 
      // ColumnGrid
      // 
      ColumnGrid.AllowUserToAddRows = false;
      ColumnGrid.AllowUserToDeleteRows = false;
      ColumnGrid.AllowUserToResizeRows = false;
      ColumnGrid.BackgroundColor = Color.AliceBlue;
      ColumnGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      ColumnGrid.ContextMenuStrip = ColumnMenu;
      dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle2.BackColor = SystemColors.Window;
      dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
      dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
      dataGridViewCellStyle2.SelectionBackColor = SystemColors.ControlLight;
      dataGridViewCellStyle2.SelectionForeColor = Color.Black;
      dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
      ColumnGrid.DefaultCellStyle = dataGridViewCellStyle2;
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
      ColumnGrid.Size = new Size(639, 204);
      ColumnGrid.TabIndex = 8;
      // 
      // ColumnMenu
      // 
      ColumnMenu.ImageScalingSize = new Size(24, 24);
      ColumnMenu.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1, ColumnNew, ColumnEdit, toolStripSeparator4, ColumnDelete, toolStripSeparator5, ColumnRefresh, toolStripSeparator6, ColumnExit });
      ColumnMenu.Name = "ColumnMenu";
      ColumnMenu.Size = new Size(184, 214);
      // 
      // toolStripMenuItem1
      // 
      toolStripMenuItem1.BackColor = SystemColors.GradientActiveCaption;
      toolStripMenuItem1.Name = "toolStripMenuItem1";
      toolStripMenuItem1.Size = new Size(183, 32);
      toolStripMenuItem1.Text = "Column";
      // 
      // ColumnNew
      // 
      ColumnNew.Name = "ColumnNew";
      ColumnNew.ShortcutKeys = Keys.Control | Keys.N;
      ColumnNew.Size = new Size(183, 32);
      ColumnNew.Text = "&New";
      // 
      // ColumnEdit
      // 
      ColumnEdit.Name = "ColumnEdit";
      ColumnEdit.ShortcutKeyDisplayString = "ENTER";
      ColumnEdit.Size = new Size(183, 32);
      ColumnEdit.Text = "&Edit";
      // 
      // toolStripSeparator4
      // 
      toolStripSeparator4.Name = "toolStripSeparator4";
      toolStripSeparator4.Size = new Size(180, 6);
      // 
      // ColumnDelete
      // 
      ColumnDelete.Name = "ColumnDelete";
      ColumnDelete.ShortcutKeys = Keys.Delete;
      ColumnDelete.Size = new Size(183, 32);
      ColumnDelete.Text = "&Delete";
      // 
      // toolStripSeparator5
      // 
      toolStripSeparator5.Name = "toolStripSeparator5";
      toolStripSeparator5.Size = new Size(180, 6);
      // 
      // ColumnRefresh
      // 
      ColumnRefresh.Name = "ColumnRefresh";
      ColumnRefresh.ShortcutKeys = Keys.F5;
      ColumnRefresh.Size = new Size(183, 32);
      ColumnRefresh.Text = "&Refresh";
      // 
      // toolStripSeparator6
      // 
      toolStripSeparator6.Name = "toolStripSeparator6";
      toolStripSeparator6.Size = new Size(180, 6);
      // 
      // ColumnExit
      // 
      ColumnExit.Name = "ColumnExit";
      ColumnExit.Size = new Size(183, 32);
      ColumnExit.Text = "E&xit";
      // 
      // KeyPage
      // 
      KeyPage.Controls.Add(KeyGrid);
      KeyPage.Location = new Point(4, 34);
      KeyPage.Name = "KeyPage";
      KeyPage.Padding = new Padding(3);
      KeyPage.Size = new Size(645, 210);
      KeyPage.TabIndex = 1;
      KeyPage.Text = "Key";
      KeyPage.UseVisualStyleBackColor = true;
      // 
      // KeyGrid
      // 
      KeyGrid.AllowUserToAddRows = false;
      KeyGrid.AllowUserToDeleteRows = false;
      KeyGrid.AllowUserToResizeRows = false;
      KeyGrid.BackgroundColor = Color.AliceBlue;
      KeyGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      KeyGrid.ContextMenuStrip = KeyMenu;
      dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle3.BackColor = SystemColors.Window;
      dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
      dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
      dataGridViewCellStyle3.SelectionBackColor = SystemColors.ControlLight;
      dataGridViewCellStyle3.SelectionForeColor = Color.Black;
      dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
      KeyGrid.DefaultCellStyle = dataGridViewCellStyle3;
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
      KeyGrid.Size = new Size(639, 204);
      KeyGrid.TabIndex = 9;
      // 
      // KeyMenu
      // 
      KeyMenu.ImageScalingSize = new Size(24, 24);
      KeyMenu.Items.AddRange(new ToolStripItem[] { toolStripMenuItem2, KeyNew, KeyEdit, toolStripSeparator7, KeyDelete, toolStripSeparator8, KeyRefresh, toolStripSeparator9, KeyExit });
      KeyMenu.Name = "ColumnMenu";
      KeyMenu.Size = new Size(184, 214);
      // 
      // toolStripMenuItem2
      // 
      toolStripMenuItem2.BackColor = SystemColors.GradientActiveCaption;
      toolStripMenuItem2.Name = "toolStripMenuItem2";
      toolStripMenuItem2.Size = new Size(183, 32);
      toolStripMenuItem2.Text = "Key";
      // 
      // KeyNew
      // 
      KeyNew.Name = "KeyNew";
      KeyNew.ShortcutKeys = Keys.Control | Keys.N;
      KeyNew.Size = new Size(183, 32);
      KeyNew.Text = "&New";
      // 
      // KeyEdit
      // 
      KeyEdit.Name = "KeyEdit";
      KeyEdit.ShortcutKeyDisplayString = "ENTER";
      KeyEdit.Size = new Size(183, 32);
      KeyEdit.Text = "&Edit";
      // 
      // toolStripSeparator7
      // 
      toolStripSeparator7.Name = "toolStripSeparator7";
      toolStripSeparator7.Size = new Size(180, 6);
      // 
      // KeyDelete
      // 
      KeyDelete.Name = "KeyDelete";
      KeyDelete.ShortcutKeys = Keys.Delete;
      KeyDelete.Size = new Size(183, 32);
      KeyDelete.Text = "&Delete";
      // 
      // toolStripSeparator8
      // 
      toolStripSeparator8.Name = "toolStripSeparator8";
      toolStripSeparator8.Size = new Size(180, 6);
      // 
      // KeyRefresh
      // 
      KeyRefresh.Name = "KeyRefresh";
      KeyRefresh.ShortcutKeys = Keys.F5;
      KeyRefresh.Size = new Size(183, 32);
      KeyRefresh.Text = "&Refresh";
      // 
      // toolStripSeparator9
      // 
      toolStripSeparator9.Name = "toolStripSeparator9";
      toolStripSeparator9.Size = new Size(180, 6);
      // 
      // KeyExit
      // 
      KeyExit.Name = "KeyExit";
      KeyExit.Size = new Size(183, 32);
      KeyExit.Text = "E&xit";
      // 
      // TileTabs
      // 
      TileTabs.AllowDrop = true;
      TileTabs.ContextMenuStrip = KeyTabMenu;
      TileTabs.Dock = DockStyle.Fill;
      TileTabs.LJCAllowDrag = true;
      TileTabs.Location = new Point(0, 0);
      TileTabs.Name = "TileTabs";
      TileTabs.SelectedIndex = 0;
      TileTabs.Size = new Size(143, 248);
      TileTabs.TabIndex = 10;
      // 
      // KeyTabMenu
      // 
      KeyTabMenu.ImageScalingSize = new Size(24, 24);
      KeyTabMenu.Items.AddRange(new ToolStripItem[] { KeyTabMove });
      KeyTabMenu.Name = "KeyTabMenu";
      KeyTabMenu.Size = new Size(196, 36);
      // 
      // KeyTabMove
      // 
      KeyTabMove.Name = "KeyTabMove";
      KeyTabMove.Size = new Size(195, 32);
      KeyTabMove.Text = "Move Tab Left";
      // 
      // ModuleCombo
      // 
      ModuleCombo.ContextMenuStrip = ModuleMenu;
      ModuleCombo.FormattingEnabled = true;
      ModuleCombo.Location = new Point(132, 12);
      ModuleCombo.Name = "ModuleCombo";
      ModuleCombo.Size = new Size(230, 33);
      ModuleCombo.TabIndex = 1;
      // 
      // ModuleMenu
      // 
      ModuleMenu.ImageScalingSize = new Size(24, 24);
      ModuleMenu.Items.AddRange(new ToolStripItem[] { Module, ModuleNew, ModuleEdit, toolStripSeparator10, ModuleDelete, toolStripSeparator11, ModuleRefresh, toolStripSeparator12, ModuleExit });
      ModuleMenu.Name = "ModuleMenu";
      ModuleMenu.Size = new Size(184, 214);
      // 
      // Module
      // 
      Module.BackColor = SystemColors.GradientActiveCaption;
      Module.Name = "Module";
      Module.Size = new Size(183, 32);
      Module.Text = "Module";
      // 
      // ModuleNew
      // 
      ModuleNew.Name = "ModuleNew";
      ModuleNew.ShortcutKeys = Keys.Control | Keys.N;
      ModuleNew.Size = new Size(183, 32);
      ModuleNew.Text = "&New";
      // 
      // ModuleEdit
      // 
      ModuleEdit.Name = "ModuleEdit";
      ModuleEdit.ShortcutKeyDisplayString = "ENTER";
      ModuleEdit.Size = new Size(183, 32);
      ModuleEdit.Text = "&Edit";
      // 
      // toolStripSeparator10
      // 
      toolStripSeparator10.Name = "toolStripSeparator10";
      toolStripSeparator10.Size = new Size(180, 6);
      // 
      // ModuleDelete
      // 
      ModuleDelete.Name = "ModuleDelete";
      ModuleDelete.ShortcutKeys = Keys.Delete;
      ModuleDelete.Size = new Size(183, 32);
      ModuleDelete.Text = "&Delete";
      // 
      // toolStripSeparator11
      // 
      toolStripSeparator11.Name = "toolStripSeparator11";
      toolStripSeparator11.Size = new Size(180, 6);
      // 
      // ModuleRefresh
      // 
      ModuleRefresh.Name = "ModuleRefresh";
      ModuleRefresh.ShortcutKeys = Keys.F5;
      ModuleRefresh.Size = new Size(183, 32);
      ModuleRefresh.Text = "&Refresh";
      // 
      // toolStripSeparator12
      // 
      toolStripSeparator12.Name = "toolStripSeparator12";
      toolStripSeparator12.Size = new Size(180, 6);
      // 
      // ModuleExit
      // 
      ModuleExit.Name = "ModuleExit";
      ModuleExit.Size = new Size(183, 32);
      ModuleExit.Text = "E&xit";
      // 
      // ModuleLabel
      // 
      ModuleLabel.Location = new Point(12, 15);
      ModuleLabel.Name = "ModuleLabel";
      ModuleLabel.Size = new Size(114, 25);
      ModuleLabel.TabIndex = 0;
      ModuleLabel.Text = "Module";
      // 
      // ConfigLabel
      // 
      ConfigLabel.Location = new Point(390, 15);
      ConfigLabel.Name = "ConfigLabel";
      ConfigLabel.Size = new Size(165, 25);
      ConfigLabel.TabIndex = 2;
      ConfigLabel.Text = "Data Configuration";
      // 
      // ConfigCombo
      // 
      ConfigCombo.FormattingEnabled = true;
      ConfigCombo.Location = new Point(561, 12);
      ConfigCombo.Name = "ConfigCombo";
      ConfigCombo.Size = new Size(230, 33);
      ConfigCombo.TabIndex = 3;
      // 
      // TableUpdate
      // 
      TableUpdate.Name = "TableUpdate";
      TableUpdate.Size = new Size(240, 32);
      TableUpdate.Text = "Update From Table";
      // 
      // DataUtilityList
      // 
      AutoScaleDimensions = new SizeF(10F, 25F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(800, 450);
      Controls.Add(ConfigLabel);
      Controls.Add(ConfigCombo);
      Controls.Add(ModuleLabel);
      Controls.Add(ModuleCombo);
      Controls.Add(MainSplit);
      Name = "DataUtilityList";
      Text = "Data Utility";
      Load += DataUtilityList_Load;
      MainSplit.Panel1.ResumeLayout(false);
      MainSplit.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)MainSplit).EndInit();
      MainSplit.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)TableGrid).EndInit();
      TableMenu.ResumeLayout(false);
      ColumnsSplit.Panel1.ResumeLayout(false);
      ColumnsSplit.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)ColumnsSplit).EndInit();
      ColumnsSplit.ResumeLayout(false);
      ColumnTabs.ResumeLayout(false);
      ColumnTabMenu.ResumeLayout(false);
      ColumnPage.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)ColumnGrid).EndInit();
      ColumnMenu.ResumeLayout(false);
      KeyPage.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)KeyGrid).EndInit();
      KeyMenu.ResumeLayout(false);
      KeyTabMenu.ResumeLayout(false);
      ModuleMenu.ResumeLayout(false);
      ResumeLayout(false);
    }

    #endregion

    internal LJCControls5.LJCDataGrid ColumnGrid;
    internal LJCControls5.LJCDataGrid KeyGrid;
    private SplitContainer MainSplit;
    internal LJCControls5.LJCDataGrid TableGrid;
    private SplitContainer ColumnsSplit;
    internal LJCTabControl ColumnTabs;
    private TabPage ColumnPage;
    private TabPage KeyPage;
    private LJCTabControl TileTabs;
    internal ContextMenuStrip TableMenu;
    internal ToolStripMenuItem TableExit;
    internal ContextMenuStrip ColumnMenu;
    internal ToolStripMenuItem ColumnExit;
    internal ContextMenuStrip KeyMenu;
    internal ToolStripMenuItem KeyExit;
    internal ToolStripMenuItem TableNew;
    internal ToolStripMenuItem TableEdit;
    internal ToolStripMenuItem TableDelete;
    internal ToolStripMenuItem TableRefresh;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripSeparator toolStripSeparator3;
    internal ToolStripMenuItem ColumnNew;
    internal ToolStripMenuItem ColumnEdit;
    private ToolStripSeparator toolStripSeparator4;
    internal ToolStripMenuItem ColumnDelete;
    private ToolStripSeparator toolStripSeparator5;
    internal ToolStripMenuItem ColumnRefresh;
    private ToolStripSeparator toolStripSeparator6;
    internal ToolStripMenuItem KeyNew;
    internal ToolStripMenuItem KeyEdit;
    private ToolStripSeparator toolStripSeparator7;
    internal ToolStripMenuItem KeyDelete;
    private ToolStripSeparator toolStripSeparator8;
    internal ToolStripMenuItem KeyRefresh;
    private ToolStripSeparator toolStripSeparator9;
    internal LJCItemCombo ModuleCombo;
    private Label ModuleLabel;
    private Label ConfigLabel;
    internal LJCItemCombo ConfigCombo;
    internal ContextMenuStrip ModuleMenu;
    internal ToolStripMenuItem ModuleNew;
    internal ToolStripMenuItem ModuleEdit;
    private ToolStripSeparator toolStripSeparator10;
    internal ToolStripMenuItem ModuleDelete;
    private ToolStripSeparator toolStripSeparator11;
    internal ToolStripMenuItem ModuleRefresh;
    private ToolStripSeparator toolStripSeparator12;
    internal ToolStripMenuItem ModuleExit;
    private ToolStripMenuItem Table;
    private ToolStripMenuItem toolStripMenuItem1;
    private ToolStripMenuItem toolStripMenuItem2;
    private ToolStripMenuItem Module;
    private ContextMenuStrip ColumnTabMenu;
    private ToolStripMenuItem ColumnTabMove;
    private ContextMenuStrip KeyTabMenu;
    private ToolStripMenuItem KeyTabMove;
    internal ToolStripMenuItem TableUpdate;
  }
}
