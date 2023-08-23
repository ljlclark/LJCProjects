namespace LJCGridDataTests
{
    partial class TestForm
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
      this.TestDataGrid = new LJCWinFormControls.LJCDataGrid(this.components);
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.TestDataGrid)).BeginInit();
      this.SuspendLayout();
      // 
      // TestDataGrid
      // 
      this.TestDataGrid.AllowUserToAddRows = false;
      this.TestDataGrid.AllowUserToDeleteRows = false;
      this.TestDataGrid.AllowUserToResizeRows = false;
      this.TestDataGrid.BackgroundColor = System.Drawing.Color.AliceBlue;
      this.TestDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
      dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
      dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.TestDataGrid.DefaultCellStyle = dataGridViewCellStyle3;
      this.TestDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TestDataGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
      this.TestDataGrid.LJCAllowSelectionChange = false;
      this.TestDataGrid.LJCDragDataName = null;
      this.TestDataGrid.LJCLastRowIndex = -1;
      this.TestDataGrid.LJCRowHeight = 0;
      this.TestDataGrid.Location = new System.Drawing.Point(0, 0);
      this.TestDataGrid.MultiSelect = false;
      this.TestDataGrid.Name = "TestDataGrid";
      this.TestDataGrid.RowHeadersVisible = false;
      this.TestDataGrid.RowHeadersWidth = 62;
      this.TestDataGrid.RowTemplate.Height = 28;
      this.TestDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.TestDataGrid.ShowCellToolTips = false;
      this.TestDataGrid.Size = new System.Drawing.Size(800, 450);
      this.TestDataGrid.TabIndex = 0;
      this.TestDataGrid.Text = "LJCDataGrid";
      // 
      // TestForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this.TestDataGrid);
      this.Name = "TestForm";
      this.Text = "Test Form";
      this.Load += new System.EventHandler(this.Form1_Load);
      ((System.ComponentModel.ISupportInitialize)(this.TestDataGrid)).EndInit();
      this.ResumeLayout(false);

        }

        #endregion

        private LJCWinFormControls.LJCDataGrid TestDataGrid;
    private System.Windows.Forms.ToolTip toolTip1;
  }
}

