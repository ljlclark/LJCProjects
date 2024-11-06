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
      this.ModuleExit = new System.Windows.Forms.ToolStripMenuItem();
      this.ljcTabControl1 = new LJCWinFormControls.LJCTabControl(this.components);
      this.ModulePage = new System.Windows.Forms.TabPage();
      this.ModuleGrid = new LJCWinFormControls.LJCDataGrid(this.components);
      this.TablePage = new System.Windows.Forms.TabPage();
      this.TableGrid = new LJCWinFormControls.LJCDataGrid(this.components);
      this.ColymnPage = new System.Windows.Forms.TabPage();
      this.ColumnGrid = new LJCWinFormControls.LJCDataGrid(this.components);
      this.KeyPage = new System.Windows.Forms.TabPage();
      this.KeyGrid = new LJCWinFormControls.LJCDataGrid(this.components);
      this.MapTablePage = new System.Windows.Forms.TabPage();
      this.MapTableGrid = new LJCWinFormControls.LJCDataGrid(this.components);
      this.MapColumnPage = new System.Windows.Forms.TabPage();
      this.MapColumnGrid = new LJCWinFormControls.LJCDataGrid(this.components);
      this.ModuleMenu.SuspendLayout();
      this.ljcTabControl1.SuspendLayout();
      this.ModulePage.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.ModuleGrid)).BeginInit();
      this.TablePage.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.TableGrid)).BeginInit();
      this.ColymnPage.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.ColumnGrid)).BeginInit();
      this.KeyPage.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.KeyGrid)).BeginInit();
      this.MapTablePage.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.MapTableGrid)).BeginInit();
      this.MapColumnPage.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.MapColumnGrid)).BeginInit();
      this.SuspendLayout();
      // 
      // ModuleMenu
      // 
      this.ModuleMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.ModuleMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ModuleExit});
      this.ModuleMenu.Name = "ModuleMenu";
      this.ModuleMenu.Size = new System.Drawing.Size(112, 36);
      // 
      // ModuleExit
      // 
      this.ModuleExit.Name = "ModuleExit";
      this.ModuleExit.Size = new System.Drawing.Size(111, 32);
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
      this.TableGrid.ContextMenuStrip = this.ModuleMenu;
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
      this.ColumnGrid.ContextMenuStrip = this.ModuleMenu;
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
      this.KeyGrid.ContextMenuStrip = this.ModuleMenu;
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
      this.MapTableGrid.ContextMenuStrip = this.ModuleMenu;
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
      this.MapColumnGrid.ContextMenuStrip = this.ModuleMenu;
      dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
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
      this.ColymnPage.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.ColumnGrid)).EndInit();
      this.KeyPage.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.KeyGrid)).EndInit();
      this.MapTablePage.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.MapTableGrid)).EndInit();
      this.MapColumnPage.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.MapColumnGrid)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.ContextMenuStrip ModuleMenu;
    private System.Windows.Forms.ToolStripMenuItem ModuleExit;
    private LJCWinFormControls.LJCTabControl ljcTabControl1;
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
  }
}

