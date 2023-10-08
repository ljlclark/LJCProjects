namespace LJCGenDocEdit
{
  partial class MethodSelect
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
      this.MethodGrid = new LJCWinFormControls.LJCDataGrid(this.components);
      this.MethodMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.ClassHeading = new System.Windows.Forms.ToolStripMenuItem();
      this.MethodSelectItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.MethodText = new System.Windows.Forms.ToolStripMenuItem();
      this.MethodCSV = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.MethodClose = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.MethodHelp = new System.Windows.Forms.ToolStripMenuItem();
      ((System.ComponentModel.ISupportInitialize)(this.MethodGrid)).BeginInit();
      this.MethodMenu.SuspendLayout();
      this.SuspendLayout();
      // 
      // MethodGrid
      // 
      this.MethodGrid.AllowUserToAddRows = false;
      this.MethodGrid.AllowUserToDeleteRows = false;
      this.MethodGrid.AllowUserToResizeRows = false;
      this.MethodGrid.BackgroundColor = System.Drawing.Color.AliceBlue;
      this.MethodGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.MethodGrid.ContextMenuStrip = this.MethodMenu;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.MethodGrid.DefaultCellStyle = dataGridViewCellStyle1;
      this.MethodGrid.Dock = System.Windows.Forms.DockStyle.Fill;
      this.MethodGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
      this.MethodGrid.LJCAllowSelectionChange = false;
      this.MethodGrid.LJCDragDataName = null;
      this.MethodGrid.LJCLastRowIndex = -1;
      this.MethodGrid.LJCRowHeight = 0;
      this.MethodGrid.Location = new System.Drawing.Point(0, 0);
      this.MethodGrid.MultiSelect = false;
      this.MethodGrid.Name = "MethodGrid";
      this.MethodGrid.RowHeadersVisible = false;
      this.MethodGrid.RowHeadersWidth = 62;
      this.MethodGrid.RowTemplate.Height = 28;
      this.MethodGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.MethodGrid.ShowCellToolTips = false;
      this.MethodGrid.Size = new System.Drawing.Size(578, 344);
      this.MethodGrid.TabIndex = 2;
      this.MethodGrid.Text = "LJCDataGrid";
      this.MethodGrid.SelectionChanged += new System.EventHandler(this.MethodGrid_SelectionChanged);
      this.MethodGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MethodGrid_KeyDown);
      this.MethodGrid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.MethodGrid_MouseDoubleClick);
      this.MethodGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MethodGrid_MouseDown);
      // 
      // MethodMenu
      // 
      this.MethodMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.MethodMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ClassHeading,
            this.MethodSelectItem,
            this.toolStripSeparator1,
            this.MethodText,
            this.MethodCSV,
            this.toolStripSeparator2,
            this.MethodClose,
            this.toolStripSeparator4,
            this.MethodHelp});
      this.MethodMenu.Name = "AssemblyMenu";
      this.MethodMenu.Size = new System.Drawing.Size(249, 214);
      this.MethodMenu.Text = "Assembly Group Menu";
      // 
      // ClassHeading
      // 
      this.ClassHeading.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
      this.ClassHeading.Name = "ClassHeading";
      this.ClassHeading.Size = new System.Drawing.Size(248, 32);
      this.ClassHeading.Text = "Method Select Menu";
      // 
      // MethodSelectItem
      // 
      this.MethodSelectItem.Name = "MethodSelectItem";
      this.MethodSelectItem.ShortcutKeyDisplayString = "";
      this.MethodSelectItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
      this.MethodSelectItem.Size = new System.Drawing.Size(248, 32);
      this.MethodSelectItem.Text = "&Select";
      this.MethodSelectItem.Click += new System.EventHandler(this.MethodSelectItem_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(245, 6);
      // 
      // MethodText
      // 
      this.MethodText.Name = "MethodText";
      this.MethodText.Size = new System.Drawing.Size(248, 32);
      this.MethodText.Text = "Export &Text";
      this.MethodText.Click += new System.EventHandler(this.MethodText_Click);
      // 
      // MethodCSV
      // 
      this.MethodCSV.Name = "MethodCSV";
      this.MethodCSV.Size = new System.Drawing.Size(248, 32);
      this.MethodCSV.Text = "Export &CSV";
      this.MethodCSV.Click += new System.EventHandler(this.MethodCSV_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(245, 6);
      // 
      // MethodClose
      // 
      this.MethodClose.Name = "MethodClose";
      this.MethodClose.Size = new System.Drawing.Size(248, 32);
      this.MethodClose.Text = "&Close";
      this.MethodClose.Click += new System.EventHandler(this.MethodClose_Click);
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(245, 6);
      // 
      // MethodHelp
      // 
      this.MethodHelp.Name = "MethodHelp";
      this.MethodHelp.Size = new System.Drawing.Size(248, 32);
      this.MethodHelp.Text = "&Help";
      this.MethodHelp.Click += new System.EventHandler(this.MethodHelp_Click);
      // 
      // MethodSelect
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(578, 344);
      this.Controls.Add(this.MethodGrid);
      this.Name = "MethodSelect";
      this.Text = "Method Select";
      this.Load += new System.EventHandler(this.MethodSelect_Load);
      ((System.ComponentModel.ISupportInitialize)(this.MethodGrid)).EndInit();
      this.MethodMenu.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    internal LJCWinFormControls.LJCDataGrid MethodGrid;
    private System.Windows.Forms.ContextMenuStrip MethodMenu;
    private System.Windows.Forms.ToolStripMenuItem ClassHeading;
    private System.Windows.Forms.ToolStripMenuItem MethodSelectItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem MethodText;
    private System.Windows.Forms.ToolStripMenuItem MethodCSV;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripMenuItem MethodClose;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    private System.Windows.Forms.ToolStripMenuItem MethodHelp;
  }
}