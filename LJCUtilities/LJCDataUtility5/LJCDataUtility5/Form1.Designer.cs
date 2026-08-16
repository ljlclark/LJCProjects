namespace LJCDataUtility5
{
    partial class Form1
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
      MainGrid = new LJCControls5.LJCDataGrid5();
      ((System.ComponentModel.ISupportInitialize)MainGrid).BeginInit();
      SuspendLayout();
      // 
      // MainGrid
      // 
      MainGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      MainGrid.Dock = DockStyle.Fill;
      MainGrid.Location = new Point(0, 0);
      MainGrid.Name = "MainGrid";
      MainGrid.RowHeadersWidth = 62;
      MainGrid.Size = new Size(800, 450);
      MainGrid.TabIndex = 0;
      // 
      // Form1
      // 
      AutoScaleDimensions = new SizeF(10F, 25F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(800, 450);
      Controls.Add(MainGrid);
      Name = "Form1";
      Text = "Form1";
      Load += Form1_Load;
      ((System.ComponentModel.ISupportInitialize)MainGrid).EndInit();
      ResumeLayout(false);
    }

    #endregion

    internal LJCControls5.LJCDataGrid5 MainGrid;
  }
}
