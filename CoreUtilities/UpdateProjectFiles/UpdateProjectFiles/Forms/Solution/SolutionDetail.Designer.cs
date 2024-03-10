namespace UpdateProjectFiles
{
  partial class SolutionDetail
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
      this.CodeLineText = new System.Windows.Forms.TextBox();
      this.CodeLineLabel = new System.Windows.Forms.Label();
      this.FormCancelButton = new System.Windows.Forms.Button();
      this.OKButton = new System.Windows.Forms.Button();
      this.NameText = new System.Windows.Forms.TextBox();
      this.NameLabel = new System.Windows.Forms.Label();
      this.GroupText = new System.Windows.Forms.TextBox();
      this.GroupLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // PathText
      // 
      this.PathText.Location = new System.Drawing.Point(151, 122);
      this.PathText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.PathText.Name = "PathText";
      this.PathText.Size = new System.Drawing.Size(413, 26);
      this.PathText.TabIndex = 7;
      // 
      // PathLabel
      // 
      this.PathLabel.Location = new System.Drawing.Point(18, 126);
      this.PathLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.PathLabel.Name = "PathLabel";
      this.PathLabel.Size = new System.Drawing.Size(130, 20);
      this.PathLabel.TabIndex = 6;
      this.PathLabel.Text = "Path";
      // 
      // CodeLineText
      // 
      this.CodeLineText.Location = new System.Drawing.Point(151, 14);
      this.CodeLineText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.CodeLineText.Name = "CodeLineText";
      this.CodeLineText.ReadOnly = true;
      this.CodeLineText.Size = new System.Drawing.Size(413, 26);
      this.CodeLineText.TabIndex = 1;
      // 
      // CodeLineLabel
      // 
      this.CodeLineLabel.Location = new System.Drawing.Point(18, 18);
      this.CodeLineLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.CodeLineLabel.Name = "CodeLineLabel";
      this.CodeLineLabel.Size = new System.Drawing.Size(130, 20);
      this.CodeLineLabel.TabIndex = 0;
      this.CodeLineLabel.Text = "CodeL Line";
      // 
      // FormCancelButton
      // 
      this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.FormCancelButton.Location = new System.Drawing.Point(454, 162);
      this.FormCancelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.FormCancelButton.Name = "FormCancelButton";
      this.FormCancelButton.Size = new System.Drawing.Size(112, 35);
      this.FormCancelButton.TabIndex = 9;
      this.FormCancelButton.Text = "Cancel";
      this.FormCancelButton.UseVisualStyleBackColor = true;
      // 
      // OKButton
      // 
      this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.OKButton.Location = new System.Drawing.Point(332, 162);
      this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.OKButton.Name = "OKButton";
      this.OKButton.Size = new System.Drawing.Size(112, 35);
      this.OKButton.TabIndex = 8;
      this.OKButton.Text = "&OK";
      this.OKButton.UseVisualStyleBackColor = true;
      this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
      // 
      // NameText
      // 
      this.NameText.Location = new System.Drawing.Point(151, 86);
      this.NameText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.NameText.Name = "NameText";
      this.NameText.Size = new System.Drawing.Size(413, 26);
      this.NameText.TabIndex = 5;
      // 
      // NameLabel
      // 
      this.NameLabel.Location = new System.Drawing.Point(18, 90);
      this.NameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.NameLabel.Name = "NameLabel";
      this.NameLabel.Size = new System.Drawing.Size(130, 20);
      this.NameLabel.TabIndex = 4;
      this.NameLabel.Text = "Name";
      // 
      // GroupText
      // 
      this.GroupText.Location = new System.Drawing.Point(151, 50);
      this.GroupText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.GroupText.Name = "GroupText";
      this.GroupText.ReadOnly = true;
      this.GroupText.Size = new System.Drawing.Size(413, 26);
      this.GroupText.TabIndex = 3;
      // 
      // GroupLabel
      // 
      this.GroupLabel.Location = new System.Drawing.Point(18, 54);
      this.GroupLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.GroupLabel.Name = "GroupLabel";
      this.GroupLabel.Size = new System.Drawing.Size(130, 20);
      this.GroupLabel.TabIndex = 2;
      this.GroupLabel.Text = "Code Group";
      // 
      // SolutionDetail
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(584, 209);
      this.Controls.Add(this.GroupText);
      this.Controls.Add(this.GroupLabel);
      this.Controls.Add(this.PathText);
      this.Controls.Add(this.PathLabel);
      this.Controls.Add(this.CodeLineText);
      this.Controls.Add(this.CodeLineLabel);
      this.Controls.Add(this.FormCancelButton);
      this.Controls.Add(this.OKButton);
      this.Controls.Add(this.NameText);
      this.Controls.Add(this.NameLabel);
      this.Name = "SolutionDetail";
      this.Text = "Solution Detail";
      this.Load += new System.EventHandler(this.SolutionDetail_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox PathText;
    private System.Windows.Forms.Label PathLabel;
    private System.Windows.Forms.TextBox CodeLineText;
    private System.Windows.Forms.Label CodeLineLabel;
    private System.Windows.Forms.Button FormCancelButton;
    private System.Windows.Forms.Button OKButton;
    private System.Windows.Forms.TextBox NameText;
    private System.Windows.Forms.Label NameLabel;
    private System.Windows.Forms.TextBox GroupText;
    private System.Windows.Forms.Label GroupLabel;
  }
}