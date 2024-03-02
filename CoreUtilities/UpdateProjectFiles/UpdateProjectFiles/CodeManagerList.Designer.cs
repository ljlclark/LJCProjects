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
      this.TabSplit = new System.Windows.Forms.SplitContainer();
      this.MainTabs = new LJCWinFormControls.LJCTabControl(this.components);
      this.CodeLineTab = new System.Windows.Forms.TabPage();
      this.AssemblySplit = new System.Windows.Forms.SplitContainer();
      this.CodeLineGrid = new LJCWinFormControls.LJCDataGrid(this.components);
      this.CodeGroupGrid = new LJCWinFormControls.LJCDataGrid(this.components);
      this.AssemblyItemHeader = new LJCWinFormControls.LJCHeaderBox();
      this.SolutionTab = new System.Windows.Forms.TabPage();
      this.GroupCombo = new LJCWinFormControls.LJCItemCombo();
      this.GroupLabel = new System.Windows.Forms.Label();
      this.ClassSplit = new System.Windows.Forms.SplitContainer();
      this.SolutionGrid = new LJCWinFormControls.LJCDataGrid(this.components);
      this.ProjectGrid = new LJCWinFormControls.LJCDataGrid(this.components);
      this.ClassItemHeader = new LJCWinFormControls.LJCHeaderBox();
      this.FileTab = new System.Windows.Forms.TabPage();
      this.FileGrid = new LJCWinFormControls.LJCDataGrid(this.components);
      this.ProjectCombo = new LJCWinFormControls.LJCItemCombo();
      this.ProjectLabel = new System.Windows.Forms.Label();
      this.TileTabs = new LJCWinFormControls.LJCTabControl(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.TabSplit)).BeginInit();
      this.TabSplit.Panel1.SuspendLayout();
      this.TabSplit.Panel2.SuspendLayout();
      this.TabSplit.SuspendLayout();
      this.MainTabs.SuspendLayout();
      this.CodeLineTab.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.AssemblySplit)).BeginInit();
      this.AssemblySplit.Panel1.SuspendLayout();
      this.AssemblySplit.Panel2.SuspendLayout();
      this.AssemblySplit.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.CodeLineGrid)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.CodeGroupGrid)).BeginInit();
      this.SolutionTab.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.ClassSplit)).BeginInit();
      this.ClassSplit.Panel1.SuspendLayout();
      this.ClassSplit.Panel2.SuspendLayout();
      this.ClassSplit.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.SolutionGrid)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.ProjectGrid)).BeginInit();
      this.FileTab.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.FileGrid)).BeginInit();
      this.SuspendLayout();
      // 
      // TabSplit
      // 
      this.TabSplit.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TabSplit.Location = new System.Drawing.Point(0, 0);
      this.TabSplit.Name = "TabSplit";
      // 
      // TabSplit.Panel1
      // 
      this.TabSplit.Panel1.Controls.Add(this.MainTabs);
      // 
      // TabSplit.Panel2
      // 
      this.TabSplit.Panel2.Controls.Add(this.TileTabs);
      this.TabSplit.Size = new System.Drawing.Size(800, 450);
      this.TabSplit.SplitterDistance = 624;
      this.TabSplit.TabIndex = 0;
      // 
      // MainTabs
      // 
      this.MainTabs.AllowDrop = true;
      this.MainTabs.Controls.Add(this.CodeLineTab);
      this.MainTabs.Controls.Add(this.SolutionTab);
      this.MainTabs.Controls.Add(this.FileTab);
      this.MainTabs.Dock = System.Windows.Forms.DockStyle.Fill;
      this.MainTabs.LJCAllowDrag = true;
      this.MainTabs.Location = new System.Drawing.Point(0, 0);
      this.MainTabs.Name = "MainTabs";
      this.MainTabs.SelectedIndex = 0;
      this.MainTabs.Size = new System.Drawing.Size(624, 450);
      this.MainTabs.TabIndex = 1;
      // 
      // CodeLineTab
      // 
      this.CodeLineTab.Controls.Add(this.AssemblySplit);
      this.CodeLineTab.Location = new System.Drawing.Point(4, 29);
      this.CodeLineTab.Name = "CodeLineTab";
      this.CodeLineTab.Padding = new System.Windows.Forms.Padding(3);
      this.CodeLineTab.Size = new System.Drawing.Size(616, 417);
      this.CodeLineTab.TabIndex = 0;
      this.CodeLineTab.Text = "Code Line";
      this.CodeLineTab.UseVisualStyleBackColor = true;
      // 
      // AssemblySplit
      // 
      this.AssemblySplit.Dock = System.Windows.Forms.DockStyle.Fill;
      this.AssemblySplit.Location = new System.Drawing.Point(3, 3);
      this.AssemblySplit.Name = "AssemblySplit";
      this.AssemblySplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // AssemblySplit.Panel1
      // 
      this.AssemblySplit.Panel1.Controls.Add(this.CodeLineGrid);
      // 
      // AssemblySplit.Panel2
      // 
      this.AssemblySplit.Panel2.Controls.Add(this.CodeGroupGrid);
      this.AssemblySplit.Panel2.Controls.Add(this.AssemblyItemHeader);
      this.AssemblySplit.Size = new System.Drawing.Size(610, 411);
      this.AssemblySplit.SplitterDistance = 104;
      this.AssemblySplit.TabIndex = 0;
      // 
      // CodeLineGrid
      // 
      this.CodeLineGrid.AllowDrop = true;
      this.CodeLineGrid.AllowUserToAddRows = false;
      this.CodeLineGrid.AllowUserToDeleteRows = false;
      this.CodeLineGrid.AllowUserToResizeRows = false;
      this.CodeLineGrid.BackgroundColor = System.Drawing.Color.AliceBlue;
      this.CodeLineGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
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
      this.CodeLineGrid.Size = new System.Drawing.Size(610, 104);
      this.CodeLineGrid.TabIndex = 1;
      this.CodeLineGrid.Text = "LJCDataGrid";
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
      this.CodeGroupGrid.Size = new System.Drawing.Size(609, 270);
      this.CodeGroupGrid.TabIndex = 1;
      this.CodeGroupGrid.Text = "LJCDataGrid";
      // 
      // AssemblyItemHeader
      // 
      this.AssemblyItemHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.AssemblyItemHeader.LJCBeginColor = System.Drawing.Color.AliceBlue;
      this.AssemblyItemHeader.LJCEndColor = System.Drawing.Color.LightSkyBlue;
      this.AssemblyItemHeader.Location = new System.Drawing.Point(0, 0);
      this.AssemblyItemHeader.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.AssemblyItemHeader.Name = "AssemblyItemHeader";
      this.AssemblyItemHeader.Size = new System.Drawing.Size(609, 33);
      this.AssemblyItemHeader.TabIndex = 0;
      this.AssemblyItemHeader.TabStop = false;
      this.AssemblyItemHeader.Text = "Code Group";
      // 
      // SolutionTab
      // 
      this.SolutionTab.Controls.Add(this.GroupCombo);
      this.SolutionTab.Controls.Add(this.GroupLabel);
      this.SolutionTab.Controls.Add(this.ClassSplit);
      this.SolutionTab.Location = new System.Drawing.Point(4, 29);
      this.SolutionTab.Name = "SolutionTab";
      this.SolutionTab.Padding = new System.Windows.Forms.Padding(3);
      this.SolutionTab.Size = new System.Drawing.Size(616, 417);
      this.SolutionTab.TabIndex = 1;
      this.SolutionTab.Text = "Solution";
      this.SolutionTab.UseVisualStyleBackColor = true;
      // 
      // GroupCombo
      // 
      this.GroupCombo.Location = new System.Drawing.Point(107, 6);
      this.GroupCombo.Name = "GroupCombo";
      this.GroupCombo.Size = new System.Drawing.Size(500, 28);
      this.GroupCombo.TabIndex = 1;
      // 
      // GroupLabel
      // 
      this.GroupLabel.Location = new System.Drawing.Point(7, 9);
      this.GroupLabel.Name = "GroupLabel";
      this.GroupLabel.Size = new System.Drawing.Size(100, 23);
      this.GroupLabel.TabIndex = 0;
      this.GroupLabel.Text = "Code Group";
      // 
      // ClassSplit
      // 
      this.ClassSplit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.ClassSplit.Location = new System.Drawing.Point(3, 37);
      this.ClassSplit.Name = "ClassSplit";
      this.ClassSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // ClassSplit.Panel1
      // 
      this.ClassSplit.Panel1.Controls.Add(this.SolutionGrid);
      // 
      // ClassSplit.Panel2
      // 
      this.ClassSplit.Panel2.Controls.Add(this.ProjectGrid);
      this.ClassSplit.Panel2.Controls.Add(this.ClassItemHeader);
      this.ClassSplit.Size = new System.Drawing.Size(610, 375);
      this.ClassSplit.SplitterDistance = 83;
      this.ClassSplit.TabIndex = 2;
      // 
      // SolutionGrid
      // 
      this.SolutionGrid.AllowDrop = true;
      this.SolutionGrid.AllowUserToAddRows = false;
      this.SolutionGrid.AllowUserToDeleteRows = false;
      this.SolutionGrid.AllowUserToResizeRows = false;
      this.SolutionGrid.BackgroundColor = System.Drawing.Color.AliceBlue;
      this.SolutionGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
      dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
      dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.SolutionGrid.DefaultCellStyle = dataGridViewCellStyle3;
      this.SolutionGrid.Dock = System.Windows.Forms.DockStyle.Fill;
      this.SolutionGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
      this.SolutionGrid.LJCAllowDrag = true;
      this.SolutionGrid.LJCAllowSelectionChange = false;
      this.SolutionGrid.LJCDragDataName = null;
      this.SolutionGrid.LJCLastRowIndex = -1;
      this.SolutionGrid.LJCRowHeight = 0;
      this.SolutionGrid.Location = new System.Drawing.Point(0, 0);
      this.SolutionGrid.MultiSelect = false;
      this.SolutionGrid.Name = "SolutionGrid";
      this.SolutionGrid.RowHeadersVisible = false;
      this.SolutionGrid.RowHeadersWidth = 62;
      this.SolutionGrid.RowTemplate.Height = 28;
      this.SolutionGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.SolutionGrid.ShowCellToolTips = false;
      this.SolutionGrid.Size = new System.Drawing.Size(610, 83);
      this.SolutionGrid.TabIndex = 1;
      this.SolutionGrid.Text = "LJCDataGrid";
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
      this.ProjectGrid.Location = new System.Drawing.Point(0, 32);
      this.ProjectGrid.MultiSelect = false;
      this.ProjectGrid.Name = "ProjectGrid";
      this.ProjectGrid.RowHeadersVisible = false;
      this.ProjectGrid.RowHeadersWidth = 62;
      this.ProjectGrid.RowTemplate.Height = 28;
      this.ProjectGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.ProjectGrid.ShowCellToolTips = false;
      this.ProjectGrid.Size = new System.Drawing.Size(609, 257);
      this.ProjectGrid.TabIndex = 1;
      this.ProjectGrid.Text = "LJCDataGrid";
      // 
      // ClassItemHeader
      // 
      this.ClassItemHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.ClassItemHeader.LJCBeginColor = System.Drawing.Color.AliceBlue;
      this.ClassItemHeader.LJCEndColor = System.Drawing.Color.LightSkyBlue;
      this.ClassItemHeader.Location = new System.Drawing.Point(0, 0);
      this.ClassItemHeader.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.ClassItemHeader.Name = "ClassItemHeader";
      this.ClassItemHeader.Size = new System.Drawing.Size(609, 32);
      this.ClassItemHeader.TabIndex = 0;
      this.ClassItemHeader.TabStop = false;
      this.ClassItemHeader.Text = "Project";
      // 
      // FileTab
      // 
      this.FileTab.Controls.Add(this.FileGrid);
      this.FileTab.Controls.Add(this.ProjectCombo);
      this.FileTab.Controls.Add(this.ProjectLabel);
      this.FileTab.Location = new System.Drawing.Point(4, 29);
      this.FileTab.Name = "FileTab";
      this.FileTab.Size = new System.Drawing.Size(616, 417);
      this.FileTab.TabIndex = 2;
      this.FileTab.Text = "File";
      this.FileTab.UseVisualStyleBackColor = true;
      // 
      // FileGrid
      // 
      this.FileGrid.AllowDrop = true;
      this.FileGrid.AllowUserToAddRows = false;
      this.FileGrid.AllowUserToDeleteRows = false;
      this.FileGrid.AllowUserToResizeRows = false;
      this.FileGrid.BackgroundColor = System.Drawing.Color.AliceBlue;
      this.FileGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
      dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
      dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.FileGrid.DefaultCellStyle = dataGridViewCellStyle5;
      this.FileGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
      this.FileGrid.LJCAllowDrag = true;
      this.FileGrid.LJCAllowSelectionChange = false;
      this.FileGrid.LJCDragDataName = null;
      this.FileGrid.LJCLastRowIndex = -1;
      this.FileGrid.LJCRowHeight = 0;
      this.FileGrid.Location = new System.Drawing.Point(0, 38);
      this.FileGrid.MultiSelect = false;
      this.FileGrid.Name = "FileGrid";
      this.FileGrid.RowHeadersVisible = false;
      this.FileGrid.RowHeadersWidth = 62;
      this.FileGrid.RowTemplate.Height = 28;
      this.FileGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.FileGrid.ShowCellToolTips = false;
      this.FileGrid.Size = new System.Drawing.Size(616, 379);
      this.FileGrid.TabIndex = 3;
      this.FileGrid.Text = "LJCDataGrid";
      // 
      // ProjectCombo
      // 
      this.ProjectCombo.Location = new System.Drawing.Point(107, 6);
      this.ProjectCombo.Name = "ProjectCombo";
      this.ProjectCombo.Size = new System.Drawing.Size(500, 28);
      this.ProjectCombo.TabIndex = 1;
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
      this.TileTabs.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TileTabs.Location = new System.Drawing.Point(0, 0);
      this.TileTabs.Name = "TileTabs";
      this.TileTabs.SelectedIndex = 0;
      this.TileTabs.Size = new System.Drawing.Size(172, 450);
      this.TileTabs.TabIndex = 0;
      // 
      // CodeManagerList
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this.TabSplit);
      this.Name = "CodeManagerList";
      this.Text = "Code Manager";
      this.TabSplit.Panel1.ResumeLayout(false);
      this.TabSplit.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.TabSplit)).EndInit();
      this.TabSplit.ResumeLayout(false);
      this.MainTabs.ResumeLayout(false);
      this.CodeLineTab.ResumeLayout(false);
      this.AssemblySplit.Panel1.ResumeLayout(false);
      this.AssemblySplit.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.AssemblySplit)).EndInit();
      this.AssemblySplit.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.CodeLineGrid)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.CodeGroupGrid)).EndInit();
      this.SolutionTab.ResumeLayout(false);
      this.ClassSplit.Panel1.ResumeLayout(false);
      this.ClassSplit.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.ClassSplit)).EndInit();
      this.ClassSplit.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.SolutionGrid)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.ProjectGrid)).EndInit();
      this.FileTab.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.FileGrid)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer TabSplit;
    private LJCWinFormControls.LJCTabControl TileTabs;
    private LJCWinFormControls.LJCTabControl MainTabs;
    private System.Windows.Forms.TabPage CodeLineTab;
    private System.Windows.Forms.SplitContainer AssemblySplit;
    internal LJCWinFormControls.LJCDataGrid CodeLineGrid;
    internal LJCWinFormControls.LJCDataGrid CodeGroupGrid;
    private LJCWinFormControls.LJCHeaderBox AssemblyItemHeader;
    private System.Windows.Forms.TabPage SolutionTab;
    internal LJCWinFormControls.LJCItemCombo GroupCombo;
    private System.Windows.Forms.Label GroupLabel;
    private System.Windows.Forms.SplitContainer ClassSplit;
    internal LJCWinFormControls.LJCDataGrid SolutionGrid;
    internal LJCWinFormControls.LJCDataGrid ProjectGrid;
    private LJCWinFormControls.LJCHeaderBox ClassItemHeader;
    private System.Windows.Forms.TabPage FileTab;
    internal LJCWinFormControls.LJCItemCombo ProjectCombo;
    private System.Windows.Forms.Label ProjectLabel;
    internal LJCWinFormControls.LJCDataGrid FileGrid;
  }
}

