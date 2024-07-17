namespace CalcPad
{
  partial class Form1
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
      this.CalcsRTB = new System.Windows.Forms.RichTextBox();
      this.SuspendLayout();
      // 
      // CalcsRTB
      // 
      this.CalcsRTB.Dock = System.Windows.Forms.DockStyle.Fill;
      this.CalcsRTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.CalcsRTB.Location = new System.Drawing.Point(0, 0);
      this.CalcsRTB.Margin = new System.Windows.Forms.Padding(5);
      this.CalcsRTB.Name = "CalcsRTB";
      this.CalcsRTB.Size = new System.Drawing.Size(1422, 720);
      this.CalcsRTB.TabIndex = 0;
      this.CalcsRTB.Text = "";
      this.CalcsRTB.DoubleClick += new System.EventHandler(this.CalcRTB_DoubleClick);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 32F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1422, 720);
      this.Controls.Add(this.CalcsRTB);
      this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Margin = new System.Windows.Forms.Padding(5);
      this.Name = "Form1";
      this.Text = "Form1";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.RichTextBox CalcsRTB;
  }
}

