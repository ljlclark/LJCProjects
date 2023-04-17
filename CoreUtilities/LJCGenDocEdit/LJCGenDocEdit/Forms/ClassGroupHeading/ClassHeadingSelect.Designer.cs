namespace LJCGenDocEdit
{
  partial class ClassHeadingSelect
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
      this.ClassHeadingGrid = new LJCWinFormControls.LJCDataGrid(this.components);
      this.HeadingMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.GroupHeadingHeading = new System.Windows.Forms.ToolStripMenuItem();
      this.HeadingEdit = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.HeadingRefresh = new System.Windows.Forms.ToolStripMenuItem();
      this.HeadingSelectSeparator = new System.Windows.Forms.ToolStripSeparator();
      this.HeadingSelect = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.HeadingText = new System.Windows.Forms.ToolStripMenuItem();
      this.HeadingCSV = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.HeadingClose = new System.Windows.Forms.ToolStripMenuItem();
      ((System.ComponentModel.ISupportInitialize)(this.ClassHeadingGrid)).BeginInit();
      this.HeadingMenu.SuspendLayout();
      this.SuspendLayout();
      // 
      // ClassHeadingGrid
      // 
      this.ClassHeadingGrid.AllowUserToAddRows = false;
      this.ClassHeadingGrid.AllowUserToDeleteRows = false;
      this.ClassHeadingGrid.AllowUserToResizeRows = false;
      this.ClassHeadingGrid.BackgroundColor = System.Drawing.Color.AliceBlue;
      this.ClassHeadingGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.ClassHeadingGrid.ContextMenuStrip = this.HeadingMenu;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.ClassHeadingGrid.DefaultCellStyle = dataGridViewCellStyle1;
      this.ClassHeadingGrid.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ClassHeadingGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
      this.ClassHeadingGrid.LJCAllowSelectionChange = false;
      this.ClassHeadingGrid.LJCDragDataName = null;
      this.ClassHeadingGrid.LJCLastRowIndex = -1;
      this.ClassHeadingGrid.LJCRowHeight = 0;
      this.ClassHeadingGrid.Location = new System.Drawing.Point(0, 0);
      this.ClassHeadingGrid.MultiSelect = false;
      this.ClassHeadingGrid.Name = "ClassHeadingGrid";
      this.ClassHeadingGrid.RowHeadersVisible = false;
      this.ClassHeadingGrid.RowHeadersWidth = 62;
      this.ClassHeadingGrid.RowTemplate.Height = 28;
      this.ClassHeadingGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.ClassHeadingGrid.ShowCellToolTips = false;
      this.ClassHeadingGrid.Size = new System.Drawing.Size(578, 344);
      this.ClassHeadingGrid.TabIndex = 0;
      this.ClassHeadingGrid.Text = "LJCDataGrid";
      this.ClassHeadingGrid.SelectionChanged += new System.EventHandler(this.ClassHeadingGrid_SelectionChanged);
      this.ClassHeadingGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ClassHeadingGrid_KeyDown);
      this.ClassHeadingGrid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ClassHeadingGrid_MouseDoubleClick);
      this.ClassHeadingGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ClassHeadingGrid_MouseDown);
      // 
      // HeadingMenu
      // 
      this.HeadingMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.HeadingMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GroupHeadingHeading,
            this.HeadingEdit,
            this.toolStripSeparator3,
            this.HeadingRefresh,
            this.HeadingSelectSeparator,
            this.HeadingSelect,
            this.toolStripSeparator1,
            this.HeadingText,
            this.HeadingCSV,
            this.toolStripSeparator2,
            this.HeadingClose});
      this.HeadingMenu.Name = "AssemblyMenu";
      this.HeadingMenu.Size = new System.Drawing.Size(302, 285);
      this.HeadingMenu.Text = "Assembly Group Menu";
      // 
      // GroupHeadingHeading
      // 
      this.GroupHeadingHeading.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
      this.GroupHeadingHeading.Name = "GroupHeadingHeading";
      this.GroupHeadingHeading.Size = new System.Drawing.Size(301, 32);
      this.GroupHeadingHeading.Text = "Class Group Heading Menu";
      // 
      // HeadingEdit
      // 
      this.HeadingEdit.Name = "HeadingEdit";
      this.HeadingEdit.ShortcutKeyDisplayString = "ENTER";
      this.HeadingEdit.Size = new System.Drawing.Size(301, 32);
      this.HeadingEdit.Text = "&Edit";
      this.HeadingEdit.Click += new System.EventHandler(this.HeadingEdit_Click);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(298, 6);
      // 
      // HeadingRefresh
      // 
      this.HeadingRefresh.Name = "HeadingRefresh";
      this.HeadingRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
      this.HeadingRefresh.Size = new System.Drawing.Size(301, 32);
      this.HeadingRefresh.Text = "&Refresh";
      this.HeadingRefresh.Click += new System.EventHandler(this.HeadingRefresh_Click);
      // 
      // HeadingSelectSeparator
      // 
      this.HeadingSelectSeparator.Name = "HeadingSelectSeparator";
      this.HeadingSelectSeparator.Size = new System.Drawing.Size(298, 6);
      // 
      // HeadingSelect
      // 
      this.HeadingSelect.Name = "HeadingSelect";
      this.HeadingSelect.ShortcutKeyDisplayString = "ENTER";
      this.HeadingSelect.Size = new System.Drawing.Size(301, 32);
      this.HeadingSelect.Text = "&Select";
      this.HeadingSelect.Click += new System.EventHandler(this.HeadingSelect_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(298, 6);
      // 
      // HeadingText
      // 
      this.HeadingText.Name = "HeadingText";
      this.HeadingText.Size = new System.Drawing.Size(301, 32);
      this.HeadingText.Text = "Export &Text";
      this.HeadingText.Click += new System.EventHandler(this.HeadingText_Click);
      // 
      // HeadingCSV
      // 
      this.HeadingCSV.Name = "HeadingCSV";
      this.HeadingCSV.Size = new System.Drawing.Size(301, 32);
      this.HeadingCSV.Text = "Export &CSV";
      this.HeadingCSV.Click += new System.EventHandler(this.HeadingCSV_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(298, 6);
      // 
      // HeadingClose
      // 
      this.HeadingClose.Name = "HeadingClose";
      this.HeadingClose.Size = new System.Drawing.Size(301, 32);
      this.HeadingClose.Text = "&Close";
      this.HeadingClose.Click += new System.EventHandler(this.HeadingClose_Click);
      // 
      // ClassHeadingSelect
      // 
      this.ClientSize = new System.Drawing.Size(578, 344);
      this.Controls.Add(this.ClassHeadingGrid);
      this.Name = "ClassHeadingSelect";
      this.ShowInTaskbar = false;
      this.Text = "Class Group Heading Select";
      this.Load += new System.EventHandler(this.ClassHeadingSelect_Load);
      ((System.ComponentModel.ISupportInitialize)(this.ClassHeadingGrid)).EndInit();
      this.HeadingMenu.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    internal LJCWinFormControls.LJCDataGrid ClassHeadingGrid;
    private System.Windows.Forms.ContextMenuStrip HeadingMenu;
    private System.Windows.Forms.ToolStripMenuItem GroupHeadingHeading;
    private System.Windows.Forms.ToolStripMenuItem HeadingEdit;
    private System.Windows.Forms.ToolStripMenuItem HeadingRefresh;
    private System.Windows.Forms.ToolStripMenuItem HeadingText;
    private System.Windows.Forms.ToolStripMenuItem HeadingCSV;
    private System.Windows.Forms.ToolStripMenuItem HeadingClose;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripMenuItem HeadingSelect;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripSeparator HeadingSelectSeparator;
  }
}