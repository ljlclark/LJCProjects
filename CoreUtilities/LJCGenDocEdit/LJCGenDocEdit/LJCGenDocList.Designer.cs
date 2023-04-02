namespace LJCGenDocEdit
{
  partial class LJCGenDocList
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
      this.TabsSplit = new System.Windows.Forms.SplitContainer();
      this.MainTabs = new LJCWinFormControls.LJCTabControl(this.components);
      this.AssemblyTab = new System.Windows.Forms.TabPage();
      this.AssemblySplit = new System.Windows.Forms.SplitContainer();
      this.AssemblyGrid = new LJCWinFormControls.LJCDataGrid(this.components);
      this.AssemblyHeading = new LJCWinFormControls.LJCHeaderBox();
      this.AssemblyItemGrid = new LJCWinFormControls.LJCDataGrid(this.components);
      this.AssemblyItemHeading = new LJCWinFormControls.LJCHeaderBox();
      this.ClassTab = new System.Windows.Forms.TabPage();
      this.AssemblyCombo = new LJCWinFormControls.LJCItemCombo();
      this.AssemblyLabel = new System.Windows.Forms.Label();
      this.ClassSplit = new System.Windows.Forms.SplitContainer();
      this.ClassGrid = new LJCWinFormControls.LJCDataGrid(this.components);
      this.ClassHeading = new LJCWinFormControls.LJCHeaderBox();
      this.ClassItemGrid = new LJCWinFormControls.LJCDataGrid(this.components);
      this.ClassItemHeading = new LJCWinFormControls.LJCHeaderBox();
      this.TileTabs = new LJCWinFormControls.LJCTabControl(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.TabsSplit)).BeginInit();
      this.TabsSplit.Panel1.SuspendLayout();
      this.TabsSplit.Panel2.SuspendLayout();
      this.TabsSplit.SuspendLayout();
      this.MainTabs.SuspendLayout();
      this.AssemblyTab.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.AssemblySplit)).BeginInit();
      this.AssemblySplit.Panel1.SuspendLayout();
      this.AssemblySplit.Panel2.SuspendLayout();
      this.AssemblySplit.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.AssemblyGrid)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.AssemblyItemGrid)).BeginInit();
      this.ClassTab.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.ClassSplit)).BeginInit();
      this.ClassSplit.Panel1.SuspendLayout();
      this.ClassSplit.Panel2.SuspendLayout();
      this.ClassSplit.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.ClassGrid)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.ClassItemGrid)).BeginInit();
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
      this.TabsSplit.SplitterDistance = 647;
      this.TabsSplit.TabIndex = 0;
      // 
      // MainTabs
      // 
      this.MainTabs.Controls.Add(this.AssemblyTab);
      this.MainTabs.Controls.Add(this.ClassTab);
      this.MainTabs.Dock = System.Windows.Forms.DockStyle.Fill;
      this.MainTabs.Location = new System.Drawing.Point(0, 0);
      this.MainTabs.Name = "MainTabs";
      this.MainTabs.SelectedIndex = 0;
      this.MainTabs.Size = new System.Drawing.Size(647, 450);
      this.MainTabs.TabIndex = 0;
      // 
      // AssemblyTab
      // 
      this.AssemblyTab.Controls.Add(this.AssemblySplit);
      this.AssemblyTab.Location = new System.Drawing.Point(4, 29);
      this.AssemblyTab.Name = "AssemblyTab";
      this.AssemblyTab.Padding = new System.Windows.Forms.Padding(3);
      this.AssemblyTab.Size = new System.Drawing.Size(639, 417);
      this.AssemblyTab.TabIndex = 0;
      this.AssemblyTab.Text = "Assembly";
      this.AssemblyTab.UseVisualStyleBackColor = true;
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
      this.AssemblySplit.Panel1.Controls.Add(this.AssemblyGrid);
      this.AssemblySplit.Panel1.Controls.Add(this.AssemblyHeading);
      // 
      // AssemblySplit.Panel2
      // 
      this.AssemblySplit.Panel2.Controls.Add(this.AssemblyItemGrid);
      this.AssemblySplit.Panel2.Controls.Add(this.AssemblyItemHeading);
      this.AssemblySplit.Size = new System.Drawing.Size(633, 411);
      this.AssemblySplit.SplitterDistance = 104;
      this.AssemblySplit.TabIndex = 0;
      // 
      // AssemblyGrid
      // 
      this.AssemblyGrid.AllowUserToAddRows = false;
      this.AssemblyGrid.AllowUserToDeleteRows = false;
      this.AssemblyGrid.AllowUserToResizeRows = false;
      this.AssemblyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.AssemblyGrid.BackgroundColor = System.Drawing.Color.AliceBlue;
      this.AssemblyGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.AssemblyGrid.DefaultCellStyle = dataGridViewCellStyle1;
      this.AssemblyGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
      this.AssemblyGrid.LJCAllowSelectionChange = false;
      this.AssemblyGrid.LJCDragDataName = null;
      this.AssemblyGrid.LJCLastRowIndex = -1;
      this.AssemblyGrid.LJCRowHeight = 0;
      this.AssemblyGrid.Location = new System.Drawing.Point(0, 31);
      this.AssemblyGrid.MultiSelect = false;
      this.AssemblyGrid.Name = "AssemblyGrid";
      this.AssemblyGrid.RowHeadersVisible = false;
      this.AssemblyGrid.RowHeadersWidth = 62;
      this.AssemblyGrid.RowTemplate.Height = 28;
      this.AssemblyGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.AssemblyGrid.ShowCellToolTips = false;
      this.AssemblyGrid.Size = new System.Drawing.Size(632, 73);
      this.AssemblyGrid.TabIndex = 2;
      this.AssemblyGrid.Text = "LJCDataGrid";
      this.AssemblyGrid.SelectionChanged += new System.EventHandler(this.AssemblyGrid_SelectionChanged);
      this.AssemblyGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AssemblyGrid_MouseDown);
      // 
      // AssemblyHeading
      // 
      this.AssemblyHeading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.AssemblyHeading.LJCBeginColor = System.Drawing.Color.AliceBlue;
      this.AssemblyHeading.LJCEndColor = System.Drawing.Color.LightSkyBlue;
      this.AssemblyHeading.Location = new System.Drawing.Point(0, 0);
      this.AssemblyHeading.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.AssemblyHeading.Name = "AssemblyHeading";
      this.AssemblyHeading.Size = new System.Drawing.Size(632, 31);
      this.AssemblyHeading.TabIndex = 1;
      this.AssemblyHeading.TabStop = false;
      this.AssemblyHeading.Text = "Assembly Group";
      // 
      // AssemblyItemGrid
      // 
      this.AssemblyItemGrid.AllowUserToAddRows = false;
      this.AssemblyItemGrid.AllowUserToDeleteRows = false;
      this.AssemblyItemGrid.AllowUserToResizeRows = false;
      this.AssemblyItemGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.AssemblyItemGrid.BackgroundColor = System.Drawing.Color.AliceBlue;
      this.AssemblyItemGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
      dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
      dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.AssemblyItemGrid.DefaultCellStyle = dataGridViewCellStyle2;
      this.AssemblyItemGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
      this.AssemblyItemGrid.LJCAllowSelectionChange = false;
      this.AssemblyItemGrid.LJCDragDataName = null;
      this.AssemblyItemGrid.LJCLastRowIndex = -1;
      this.AssemblyItemGrid.LJCRowHeight = 0;
      this.AssemblyItemGrid.Location = new System.Drawing.Point(0, 31);
      this.AssemblyItemGrid.MultiSelect = false;
      this.AssemblyItemGrid.Name = "AssemblyItemGrid";
      this.AssemblyItemGrid.RowHeadersVisible = false;
      this.AssemblyItemGrid.RowHeadersWidth = 62;
      this.AssemblyItemGrid.RowTemplate.Height = 28;
      this.AssemblyItemGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.AssemblyItemGrid.ShowCellToolTips = false;
      this.AssemblyItemGrid.Size = new System.Drawing.Size(632, 270);
      this.AssemblyItemGrid.TabIndex = 3;
      this.AssemblyItemGrid.Text = "LJCDataGrid";
      this.AssemblyItemGrid.SelectionChanged += new System.EventHandler(this.AssemblyItemGrid_SelectionChanged);
      this.AssemblyItemGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AssemblyItemGrid_MouseDown);
      // 
      // AssemblyItemHeading
      // 
      this.AssemblyItemHeading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.AssemblyItemHeading.LJCBeginColor = System.Drawing.Color.AliceBlue;
      this.AssemblyItemHeading.LJCEndColor = System.Drawing.Color.LightSkyBlue;
      this.AssemblyItemHeading.Location = new System.Drawing.Point(0, 0);
      this.AssemblyItemHeading.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.AssemblyItemHeading.Name = "AssemblyItemHeading";
      this.AssemblyItemHeading.Size = new System.Drawing.Size(632, 31);
      this.AssemblyItemHeading.TabIndex = 2;
      this.AssemblyItemHeading.TabStop = false;
      this.AssemblyItemHeading.Text = "Assembly Item";
      // 
      // ClassTab
      // 
      this.ClassTab.Controls.Add(this.AssemblyCombo);
      this.ClassTab.Controls.Add(this.AssemblyLabel);
      this.ClassTab.Controls.Add(this.ClassSplit);
      this.ClassTab.Location = new System.Drawing.Point(4, 29);
      this.ClassTab.Name = "ClassTab";
      this.ClassTab.Padding = new System.Windows.Forms.Padding(3);
      this.ClassTab.Size = new System.Drawing.Size(639, 417);
      this.ClassTab.TabIndex = 1;
      this.ClassTab.Text = "Class";
      this.ClassTab.UseVisualStyleBackColor = true;
      // 
      // AssemblyCombo
      // 
      this.AssemblyCombo.Location = new System.Drawing.Point(107, 6);
      this.AssemblyCombo.Name = "AssemblyCombo";
      this.AssemblyCombo.Size = new System.Drawing.Size(400, 28);
      this.AssemblyCombo.TabIndex = 2;
      // 
      // AssemblyLabel
      // 
      this.AssemblyLabel.Location = new System.Drawing.Point(0, 9);
      this.AssemblyLabel.Name = "AssemblyLabel";
      this.AssemblyLabel.Size = new System.Drawing.Size(100, 23);
      this.AssemblyLabel.TabIndex = 1;
      this.AssemblyLabel.Text = "Assembly";
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
      this.ClassSplit.Panel1.Controls.Add(this.ClassGrid);
      this.ClassSplit.Panel1.Controls.Add(this.ClassHeading);
      // 
      // ClassSplit.Panel2
      // 
      this.ClassSplit.Panel2.Controls.Add(this.ClassItemGrid);
      this.ClassSplit.Panel2.Controls.Add(this.ClassItemHeading);
      this.ClassSplit.Size = new System.Drawing.Size(633, 375);
      this.ClassSplit.SplitterDistance = 83;
      this.ClassSplit.TabIndex = 0;
      // 
      // ClassGrid
      // 
      this.ClassGrid.AllowUserToAddRows = false;
      this.ClassGrid.AllowUserToDeleteRows = false;
      this.ClassGrid.AllowUserToResizeRows = false;
      this.ClassGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.ClassGrid.BackgroundColor = System.Drawing.Color.AliceBlue;
      this.ClassGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.ClassGrid.DefaultCellStyle = dataGridViewCellStyle3;
      this.ClassGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
      this.ClassGrid.LJCAllowSelectionChange = false;
      this.ClassGrid.LJCDragDataName = null;
      this.ClassGrid.LJCLastRowIndex = -1;
      this.ClassGrid.LJCRowHeight = 0;
      this.ClassGrid.Location = new System.Drawing.Point(0, 31);
      this.ClassGrid.MultiSelect = false;
      this.ClassGrid.Name = "ClassGrid";
      this.ClassGrid.RowHeadersVisible = false;
      this.ClassGrid.RowHeadersWidth = 62;
      this.ClassGrid.RowTemplate.Height = 28;
      this.ClassGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.ClassGrid.ShowCellToolTips = false;
      this.ClassGrid.Size = new System.Drawing.Size(632, 52);
      this.ClassGrid.TabIndex = 3;
      this.ClassGrid.Text = "LJCDataGrid";
      // 
      // ClassHeading
      // 
      this.ClassHeading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.ClassHeading.LJCBeginColor = System.Drawing.Color.AliceBlue;
      this.ClassHeading.LJCEndColor = System.Drawing.Color.LightSkyBlue;
      this.ClassHeading.Location = new System.Drawing.Point(0, 0);
      this.ClassHeading.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.ClassHeading.Name = "ClassHeading";
      this.ClassHeading.Size = new System.Drawing.Size(632, 31);
      this.ClassHeading.TabIndex = 2;
      this.ClassHeading.TabStop = false;
      this.ClassHeading.Text = "Class Group";
      // 
      // ClassItemGrid
      // 
      this.ClassItemGrid.AllowUserToAddRows = false;
      this.ClassItemGrid.AllowUserToDeleteRows = false;
      this.ClassItemGrid.AllowUserToResizeRows = false;
      this.ClassItemGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.ClassItemGrid.BackgroundColor = System.Drawing.Color.AliceBlue;
      this.ClassItemGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
      dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
      dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.ClassItemGrid.DefaultCellStyle = dataGridViewCellStyle4;
      this.ClassItemGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
      this.ClassItemGrid.LJCAllowSelectionChange = false;
      this.ClassItemGrid.LJCDragDataName = null;
      this.ClassItemGrid.LJCLastRowIndex = -1;
      this.ClassItemGrid.LJCRowHeight = 0;
      this.ClassItemGrid.Location = new System.Drawing.Point(0, 31);
      this.ClassItemGrid.MultiSelect = false;
      this.ClassItemGrid.Name = "ClassItemGrid";
      this.ClassItemGrid.RowHeadersVisible = false;
      this.ClassItemGrid.RowHeadersWidth = 62;
      this.ClassItemGrid.RowTemplate.Height = 28;
      this.ClassItemGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.ClassItemGrid.ShowCellToolTips = false;
      this.ClassItemGrid.Size = new System.Drawing.Size(632, 257);
      this.ClassItemGrid.TabIndex = 4;
      this.ClassItemGrid.Text = "LJCDataGrid";
      // 
      // ClassItemHeading
      // 
      this.ClassItemHeading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.ClassItemHeading.LJCBeginColor = System.Drawing.Color.AliceBlue;
      this.ClassItemHeading.LJCEndColor = System.Drawing.Color.LightSkyBlue;
      this.ClassItemHeading.Location = new System.Drawing.Point(0, 0);
      this.ClassItemHeading.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.ClassItemHeading.Name = "ClassItemHeading";
      this.ClassItemHeading.Size = new System.Drawing.Size(632, 31);
      this.ClassItemHeading.TabIndex = 2;
      this.ClassItemHeading.TabStop = false;
      this.ClassItemHeading.Text = "Class Item";
      // 
      // TileTabs
      // 
      this.TileTabs.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TileTabs.Location = new System.Drawing.Point(0, 0);
      this.TileTabs.Name = "TileTabs";
      this.TileTabs.SelectedIndex = 0;
      this.TileTabs.Size = new System.Drawing.Size(149, 450);
      this.TileTabs.TabIndex = 0;
      // 
      // LJCGenDocList
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this.TabsSplit);
      this.Name = "LJCGenDocList";
      this.Text = "GenDoc Edit";
      this.Load += new System.EventHandler(this.LJCGenDocEdit_Load);
      this.TabsSplit.Panel1.ResumeLayout(false);
      this.TabsSplit.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.TabsSplit)).EndInit();
      this.TabsSplit.ResumeLayout(false);
      this.MainTabs.ResumeLayout(false);
      this.AssemblyTab.ResumeLayout(false);
      this.AssemblySplit.Panel1.ResumeLayout(false);
      this.AssemblySplit.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.AssemblySplit)).EndInit();
      this.AssemblySplit.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.AssemblyGrid)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.AssemblyItemGrid)).EndInit();
      this.ClassTab.ResumeLayout(false);
      this.ClassSplit.Panel1.ResumeLayout(false);
      this.ClassSplit.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.ClassSplit)).EndInit();
      this.ClassSplit.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.ClassGrid)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.ClassItemGrid)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer TabsSplit;
    private LJCWinFormControls.LJCTabControl MainTabs;
    private System.Windows.Forms.TabPage AssemblyTab;
    private System.Windows.Forms.TabPage ClassTab;
    private LJCWinFormControls.LJCTabControl TileTabs;
    private System.Windows.Forms.SplitContainer AssemblySplit;
    internal LJCWinFormControls.LJCItemCombo AssemblyCombo;
    private System.Windows.Forms.Label AssemblyLabel;
    private System.Windows.Forms.SplitContainer ClassSplit;
    private LJCWinFormControls.LJCHeaderBox AssemblyHeading;
    private LJCWinFormControls.LJCHeaderBox AssemblyItemHeading;
    private LJCWinFormControls.LJCHeaderBox ClassHeading;
    private LJCWinFormControls.LJCHeaderBox ClassItemHeading;
    internal LJCWinFormControls.LJCDataGrid AssemblyGrid;
    internal LJCWinFormControls.LJCDataGrid AssemblyItemGrid;
    internal LJCWinFormControls.LJCDataGrid ClassGrid;
    internal LJCWinFormControls.LJCDataGrid ClassItemGrid;
  }
}

