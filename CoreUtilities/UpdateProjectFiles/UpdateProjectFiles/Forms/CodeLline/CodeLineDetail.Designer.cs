namespace UpdateProjectFiles
{
  partial class CodeLineDetail
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
      this.PathText = new System.Windows.Forms.TextBox();
      this.PathLabel = new System.Windows.Forms.Label();
      this.FormCancelButton = new System.Windows.Forms.Button();
      this.OKButton = new System.Windows.Forms.Button();
      this.NameText = new System.Windows.Forms.TextBox();
      this.NameLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // PathText
      // 
      this.PathText.Location = new System.Drawing.Point(151, 50);
      this.PathText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.PathText.Name = "PathText";
      this.PathText.Size = new System.Drawing.Size(413, 26);
      this.PathText.TabIndex = 3;
      // 
      // PathLabel
      // 
      this.PathLabel.Location = new System.Drawing.Point(18, 54);
      this.PathLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.PathLabel.Name = "PathLabel";
      this.PathLabel.Size = new System.Drawing.Size(124, 20);
      this.PathLabel.TabIndex = 2;
      this.PathLabel.Text = "Path";
      // 
      // FormCancelButton
      // 
      this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.FormCancelButton.Location = new System.Drawing.Point(454, 90);
      this.FormCancelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.FormCancelButton.Name = "FormCancelButton";
      this.FormCancelButton.Size = new System.Drawing.Size(112, 35);
      this.FormCancelButton.TabIndex = 5;
      this.FormCancelButton.Text = "Cancel";
      this.FormCancelButton.UseVisualStyleBackColor = true;
      // 
      // OKButton
      // 
      this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.OKButton.Location = new System.Drawing.Point(332, 90);
      this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.OKButton.Name = "OKButton";
      this.OKButton.Size = new System.Drawing.Size(112, 35);
      this.OKButton.TabIndex = 4;
      this.OKButton.Text = "&OK";
      this.OKButton.UseVisualStyleBackColor = true;
      this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
      // 
      // NameText
      // 
      this.NameText.Location = new System.Drawing.Point(151, 14);
      this.NameText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.NameText.Name = "NameText";
      this.NameText.Size = new System.Drawing.Size(413, 26);
      this.NameText.TabIndex = 1;
      // 
      // NameLabel
      // 
      this.NameLabel.Location = new System.Drawing.Point(18, 18);
      this.NameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.NameLabel.Name = "NameLabel";
      this.NameLabel.Size = new System.Drawing.Size(124, 20);
      this.NameLabel.TabIndex = 0;
      this.NameLabel.Text = "Name";
      // 
      // CodeLineDetail
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(584, 138);
      this.Controls.Add(this.PathText);
      this.Controls.Add(this.PathLabel);
      this.Controls.Add(this.FormCancelButton);
      this.Controls.Add(this.OKButton);
      this.Controls.Add(this.NameText);
      this.Controls.Add(this.NameLabel);
      this.Name = "CodeLineDetail";
      this.Text = "Code Line Detail";
      this.Load += new System.EventHandler(this.CodeLineDetail_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox PathText;
    private System.Windows.Forms.Label PathLabel;
    private System.Windows.Forms.Button FormCancelButton;
    private System.Windows.Forms.Button OKButton;
    private System.Windows.Forms.TextBox NameText;
    private System.Windows.Forms.Label NameLabel;
  }
}