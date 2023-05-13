namespace LJCGenDocEdit
{
  partial class MethodHeadingSelect
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
      this.MethodHeadingGrid = new LJCWinFormControls.LJCDataGrid(this.components);
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
      this.HeadingHelp = new System.Windows.Forms.ToolStripMenuItem();
      ((System.ComponentModel.ISupportInitialize)(this.MethodHeadingGrid)).BeginInit();
      this.HeadingMenu.SuspendLayout();
      this.SuspendLayout();
      // 
      // MethodHeadingGrid
      // 
      this.MethodHeadingGrid.AllowUserToAddRows = false;
      this.MethodHeadingGrid.AllowUserToDeleteRows = false;
      this.MethodHeadingGrid.AllowUserToResizeRows = false;
      this.MethodHeadingGrid.BackgroundColor = System.Drawing.Color.AliceBlue;
      this.MethodHeadingGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.MethodHeadingGrid.ContextMenuStrip = this.HeadingMenu;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.MethodHeadingGrid.DefaultCellStyle = dataGridViewCellStyle1;
      this.MethodHeadingGrid.Dock = System.Windows.Forms.DockStyle.Fill;
      this.MethodHeadingGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
      this.MethodHeadingGrid.LJCAllowSelectionChange = false;
      this.MethodHeadingGrid.LJCDragDataName = null;
      this.MethodHeadingGrid.LJCLastRowIndex = -1;
      this.MethodHeadingGrid.LJCRowHeight = 0;
      this.MethodHeadingGrid.Location = new System.Drawing.Point(0, 0);
      this.MethodHeadingGrid.MultiSelect = false;
      this.MethodHeadingGrid.Name = "MethodHeadingGrid";
      this.MethodHeadingGrid.RowHeadersVisible = false;
      this.MethodHeadingGrid.RowHeadersWidth = 62;
      this.MethodHeadingGrid.RowTemplate.Height = 28;
      this.MethodHeadingGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.MethodHeadingGrid.ShowCellToolTips = false;
      this.MethodHeadingGrid.Size = new System.Drawing.Size(578, 344);
      this.MethodHeadingGrid.TabIndex = 1;
      this.MethodHeadingGrid.Text = "LJCDataGrid";
      this.MethodHeadingGrid.SelectionChanged += new System.EventHandler(this.MethodHeadingGrid_SelectionChanged);
      this.MethodHeadingGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MethodHeadingGrid_KeyDown);
      this.MethodHeadingGrid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.MethodHeadingGrid_MouseDoubleClick);
      this.MethodHeadingGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MethodHeadingGrid_MouseDown);
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
            this.HeadingClose,
            this.HeadingHelp});
      this.HeadingMenu.Name = "AssemblyMenu";
      this.HeadingMenu.Size = new System.Drawing.Size(325, 284);
      this.HeadingMenu.Text = "Assembly Group Menu";
      // 
      // GroupHeadingHeading
      // 
      this.GroupHeadingHeading.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
      this.GroupHeadingHeading.Name = "GroupHeadingHeading";
      this.GroupHeadingHeading.Size = new System.Drawing.Size(324, 32);
      this.GroupHeadingHeading.Text = "Method Group Heading Menu";
      // 
      // HeadingEdit
      // 
      this.HeadingEdit.Name = "HeadingEdit";
      this.HeadingEdit.ShortcutKeyDisplayString = "ENTER";
      this.HeadingEdit.Size = new System.Drawing.Size(324, 32);
      this.HeadingEdit.Text = "&Edit";
      this.HeadingEdit.Click += new System.EventHandler(this.HeadingEdit_Click);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(321, 6);
      // 
      // HeadingRefresh
      // 
      this.HeadingRefresh.Name = "HeadingRefresh";
      this.HeadingRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
      this.HeadingRefresh.Size = new System.Drawing.Size(324, 32);
      this.HeadingRefresh.Text = "&Refresh";
      this.HeadingRefresh.Click += new System.EventHandler(this.HeadingRefresh_Click);
      // 
      // HeadingSelectSeparator
      // 
      this.HeadingSelectSeparator.Name = "HeadingSelectSeparator";
      this.HeadingSelectSeparator.Size = new System.Drawing.Size(321, 6);
      // 
      // HeadingSelect
      // 
      this.HeadingSelect.Name = "HeadingSelect";
      this.HeadingSelect.ShortcutKeyDisplayString = "ENTER";
      this.HeadingSelect.Size = new System.Drawing.Size(324, 32);
      this.HeadingSelect.Text = "&Select";
      this.HeadingSelect.Click += new System.EventHandler(this.HeadingSelect_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(321, 6);
      // 
      // HeadingText
      // 
      this.HeadingText.Name = "HeadingText";
      this.HeadingText.Size = new System.Drawing.Size(324, 32);
      this.HeadingText.Text = "Export &Text";
      this.HeadingText.Click += new System.EventHandler(this.HeadingText_Click);
      // 
      // HeadingCSV
      // 
      this.HeadingCSV.Name = "HeadingCSV";
      this.HeadingCSV.Size = new System.Drawing.Size(324, 32);
      this.HeadingCSV.Text = "Export &CSV";
      this.HeadingCSV.Click += new System.EventHandler(this.HeadingCSV_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(321, 6);
      // 
      // HeadingClose
      // 
      this.HeadingClose.Name = "HeadingClose";
      this.HeadingClose.Size = new System.Drawing.Size(324, 32);
      this.HeadingClose.Text = "&Close";
      this.HeadingClose.Click += new System.EventHandler(this.HeadingClose_Click);
      // 
      // HeadingHelp
      // 
      this.HeadingHelp.Name = "HeadingHelp";
      this.HeadingHelp.Size = new System.Drawing.Size(324, 32);
      this.HeadingHelp.Text = "&Help";
      this.HeadingHelp.Click += new System.EventHandler(this.HeadingHelp_Click);
      // 
      // MethodHeadingSelect
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(578, 344);
      this.Controls.Add(this.MethodHeadingGrid);
      this.Name = "MethodHeadingSelect";
      this.Text = "Method Group Heading Select";
      this.Load += new System.EventHandler(this.MethodHeadingSelect_Load);
      ((System.ComponentModel.ISupportInitialize)(this.MethodHeadingGrid)).EndInit();
      this.HeadingMenu.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    internal LJCWinFormControls.LJCDataGrid MethodHeadingGrid;
    private System.Windows.Forms.ContextMenuStrip HeadingMenu;
    private System.Windows.Forms.ToolStripMenuItem GroupHeadingHeading;
    private System.Windows.Forms.ToolStripMenuItem HeadingEdit;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripMenuItem HeadingRefresh;
    private System.Windows.Forms.ToolStripMenuItem HeadingSelect;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem HeadingText;
    private System.Windows.Forms.ToolStripMenuItem HeadingCSV;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripMenuItem HeadingClose;
    private System.Windows.Forms.ToolStripSeparator HeadingSelectSeparator;
    private System.Windows.Forms.ToolStripMenuItem HeadingHelp;
  }
}