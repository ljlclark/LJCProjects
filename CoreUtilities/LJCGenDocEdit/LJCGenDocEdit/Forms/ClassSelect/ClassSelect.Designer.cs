namespace LJCGenDocEdit
{
  partial class ClassSelect
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
      this.ClassMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.ClassHeading = new System.Windows.Forms.ToolStripMenuItem();
      this.ClassSelectItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.ClassText = new System.Windows.Forms.ToolStripMenuItem();
      this.ClassCSV = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.ClassClose = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.ClassHelp = new System.Windows.Forms.ToolStripMenuItem();
      this.ClassGrid = new LJCWinFormControls.LJCDataGrid(this.components);
      this.ClassMenu.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.ClassGrid)).BeginInit();
      this.SuspendLayout();
      // 
      // ClassMenu
      // 
      this.ClassMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.ClassMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ClassHeading,
            this.ClassSelectItem,
            this.toolStripSeparator1,
            this.ClassText,
            this.ClassCSV,
            this.toolStripSeparator2,
            this.ClassClose,
            this.toolStripSeparator4,
            this.ClassHelp});
      this.ClassMenu.Name = "AssemblyMenu";
      this.ClassMenu.Size = new System.Drawing.Size(226, 214);
      this.ClassMenu.Text = "Assembly Group Menu";
      // 
      // ClassHeading
      // 
      this.ClassHeading.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
      this.ClassHeading.Name = "ClassHeading";
      this.ClassHeading.Size = new System.Drawing.Size(225, 32);
      this.ClassHeading.Text = "Class Select Menu";
      // 
      // ClassSelectItem
      // 
      this.ClassSelectItem.Name = "ClassSelectItem";
      this.ClassSelectItem.ShortcutKeyDisplayString = "";
      this.ClassSelectItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
      this.ClassSelectItem.Size = new System.Drawing.Size(225, 32);
      this.ClassSelectItem.Text = "&Select";
      this.ClassSelectItem.Click += new System.EventHandler(this.ClassSelectItem_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(222, 6);
      // 
      // ClassText
      // 
      this.ClassText.Name = "ClassText";
      this.ClassText.Size = new System.Drawing.Size(225, 32);
      this.ClassText.Text = "Export &Text";
      this.ClassText.Click += new System.EventHandler(this.ClassText_Click);
      // 
      // ClassCSV
      // 
      this.ClassCSV.Name = "ClassCSV";
      this.ClassCSV.Size = new System.Drawing.Size(225, 32);
      this.ClassCSV.Text = "Export &CSV";
      this.ClassCSV.Click += new System.EventHandler(this.ClassCSV_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(222, 6);
      // 
      // ClassClose
      // 
      this.ClassClose.Name = "ClassClose";
      this.ClassClose.Size = new System.Drawing.Size(225, 32);
      this.ClassClose.Text = "&Close";
      this.ClassClose.Click += new System.EventHandler(this.ClassClose_Click);
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(222, 6);
      // 
      // ClassHelp
      // 
      this.ClassHelp.Name = "ClassHelp";
      this.ClassHelp.Size = new System.Drawing.Size(225, 32);
      this.ClassHelp.Text = "&Help";
      this.ClassHelp.Click += new System.EventHandler(this.ClassHelp_Click);
      // 
      // ClassGrid
      // 
      this.ClassGrid.AllowUserToAddRows = false;
      this.ClassGrid.AllowUserToDeleteRows = false;
      this.ClassGrid.AllowUserToResizeRows = false;
      this.ClassGrid.BackgroundColor = System.Drawing.Color.AliceBlue;
      this.ClassGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.ClassGrid.ContextMenuStrip = this.ClassMenu;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.ClassGrid.DefaultCellStyle = dataGridViewCellStyle1;
      this.ClassGrid.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ClassGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
      this.ClassGrid.LJCAllowSelectionChange = false;
      this.ClassGrid.LJCDragDataName = null;
      this.ClassGrid.LJCLastRowIndex = -1;
      this.ClassGrid.LJCRowHeight = 0;
      this.ClassGrid.Location = new System.Drawing.Point(0, 0);
      this.ClassGrid.MultiSelect = false;
      this.ClassGrid.Name = "ClassGrid";
      this.ClassGrid.RowHeadersVisible = false;
      this.ClassGrid.RowHeadersWidth = 62;
      this.ClassGrid.RowTemplate.Height = 28;
      this.ClassGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.ClassGrid.ShowCellToolTips = false;
      this.ClassGrid.Size = new System.Drawing.Size(578, 344);
      this.ClassGrid.TabIndex = 1;
      this.ClassGrid.Text = "LJCDataGrid";
      this.ClassGrid.SelectionChanged += new System.EventHandler(this.ClassGrid_SelectionChanged);
      this.ClassGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ClassGrid_KeyDown);
      this.ClassGrid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ClassGrid_MouseDoubleClick);
      this.ClassGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ClassGrid_MouseDown);
      // 
      // ClassSelect
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(578, 344);
      this.Controls.Add(this.ClassGrid);
      this.Name = "ClassSelect";
      this.Text = "Class Select";
      this.Load += new System.EventHandler(this.ClassSelect_Load);
      this.ClassMenu.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.ClassGrid)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ContextMenuStrip ClassMenu;
    private System.Windows.Forms.ToolStripMenuItem ClassHeading;
    private System.Windows.Forms.ToolStripMenuItem ClassSelectItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem ClassText;
    private System.Windows.Forms.ToolStripMenuItem ClassCSV;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripMenuItem ClassClose;
    internal LJCWinFormControls.LJCDataGrid ClassGrid;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    private System.Windows.Forms.ToolStripMenuItem ClassHelp;
  }
}