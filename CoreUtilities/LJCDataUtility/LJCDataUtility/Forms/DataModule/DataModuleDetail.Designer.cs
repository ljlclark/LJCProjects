namespace LJCDataUtility
{
  partial class DataModuleDetail
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
      this.FormCancelButton = new System.Windows.Forms.Button();
      this.OKButton = new System.Windows.Forms.Button();
      this.DescriptionText = new System.Windows.Forms.TextBox();
      this.DescriptionLabel = new System.Windows.Forms.Label();
      this.NameText = new System.Windows.Forms.TextBox();
      this.NameLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // FormCancelButton
      // 
      this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.FormCancelButton.Location = new System.Drawing.Point(578, 124);
      this.FormCancelButton.Margin = new System.Windows.Forms.Padding(6);
      this.FormCancelButton.Name = "FormCancelButton";
      this.FormCancelButton.Size = new System.Drawing.Size(174, 46);
      this.FormCancelButton.TabIndex = 5;
      this.FormCancelButton.Text = "Cancel";
      this.FormCancelButton.UseVisualStyleBackColor = true;
      // 
      // OKButton
      // 
      this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.OKButton.Location = new System.Drawing.Point(387, 124);
      this.OKButton.Margin = new System.Windows.Forms.Padding(6);
      this.OKButton.Name = "OKButton";
      this.OKButton.Size = new System.Drawing.Size(174, 46);
      this.OKButton.TabIndex = 4;
      this.OKButton.Text = "&OK";
      this.OKButton.UseVisualStyleBackColor = true;
      this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
      // 
      // DescriptionText
      // 
      this.DescriptionText.Location = new System.Drawing.Point(254, 69);
      this.DescriptionText.Margin = new System.Windows.Forms.Padding(6);
      this.DescriptionText.Name = "DescriptionText";
      this.DescriptionText.Size = new System.Drawing.Size(500, 32);
      this.DescriptionText.TabIndex = 3;
      // 
      // DescriptionLabel
      // 
      this.DescriptionLabel.AutoSize = true;
      this.DescriptionLabel.Location = new System.Drawing.Point(23, 75);
      this.DescriptionLabel.Name = "DescriptionLabel";
      this.DescriptionLabel.Size = new System.Drawing.Size(121, 26);
      this.DescriptionLabel.TabIndex = 2;
      this.DescriptionLabel.Text = "Description";
      // 
      // NameText
      // 
      this.NameText.Location = new System.Drawing.Point(254, 22);
      this.NameText.Margin = new System.Windows.Forms.Padding(6);
      this.NameText.Name = "NameText";
      this.NameText.Size = new System.Drawing.Size(500, 32);
      this.NameText.TabIndex = 1;
      // 
      // NameLabel
      // 
      this.NameLabel.AutoSize = true;
      this.NameLabel.Location = new System.Drawing.Point(23, 28);
      this.NameLabel.Name = "NameLabel";
      this.NameLabel.Size = new System.Drawing.Size(71, 26);
      this.NameLabel.TabIndex = 0;
      this.NameLabel.Text = "Name";
      // 
      // DataModuleDetail
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(778, 184);
      this.Controls.Add(this.DescriptionText);
      this.Controls.Add(this.DescriptionLabel);
      this.Controls.Add(this.NameText);
      this.Controls.Add(this.NameLabel);
      this.Controls.Add(this.FormCancelButton);
      this.Controls.Add(this.OKButton);
      this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Name = "DataModuleDetail";
      this.Text = "DataModule Detai";
      this.Load += new System.EventHandler(this.DataModuleDetail_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button FormCancelButton;
    private System.Windows.Forms.Button OKButton;
    private System.Windows.Forms.TextBox DescriptionText;
    private System.Windows.Forms.Label DescriptionLabel;
    private System.Windows.Forms.TextBox NameText;
    private System.Windows.Forms.Label NameLabel;
  }
}