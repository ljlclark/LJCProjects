namespace LJCDataDetail
{
  partial class TabDetail
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
      this.ParentNameText = new System.Windows.Forms.TextBox();
      this.ParentNameLabel = new System.Windows.Forms.Label();
      this.FormCancelButton = new System.Windows.Forms.Button();
      this.OKButton = new System.Windows.Forms.Button();
      this.TabIndexText = new System.Windows.Forms.TextBox();
      this.TabIndexLabel = new System.Windows.Forms.Label();
      this.TabTextText = new System.Windows.Forms.TextBox();
      this.TabTextLabel = new System.Windows.Forms.Label();
      this.DescriptionText = new System.Windows.Forms.TextBox();
      this.DescriptionLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // ParentNameText
      // 
      this.ParentNameText.Location = new System.Drawing.Point(165, 14);
      this.ParentNameText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.ParentNameText.Name = "ParentNameText";
      this.ParentNameText.ReadOnly = true;
      this.ParentNameText.Size = new System.Drawing.Size(433, 26);
      this.ParentNameText.TabIndex = 1;
      this.ParentNameText.TabStop = false;
      // 
      // ParentNameLabel
      // 
      this.ParentNameLabel.Location = new System.Drawing.Point(11, 17);
      this.ParentNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.ParentNameLabel.Name = "ParentNameLabel";
      this.ParentNameLabel.Size = new System.Drawing.Size(153, 20);
      this.ParentNameLabel.TabIndex = 0;
      this.ParentNameLabel.Text = "Control Detail";
      // 
      // FormCancelButton
      // 
      this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.FormCancelButton.Location = new System.Drawing.Point(487, 161);
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
      this.OKButton.Location = new System.Drawing.Point(366, 161);
      this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.OKButton.Name = "OKButton";
      this.OKButton.Size = new System.Drawing.Size(112, 35);
      this.OKButton.TabIndex = 8;
      this.OKButton.Text = "&OK";
      this.OKButton.UseVisualStyleBackColor = true;
      this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
      // 
      // TabIndexText
      // 
      this.TabIndexText.Location = new System.Drawing.Point(165, 50);
      this.TabIndexText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.TabIndexText.Name = "TabIndexText";
      this.TabIndexText.Size = new System.Drawing.Size(40, 26);
      this.TabIndexText.TabIndex = 3;
      // 
      // TabIndexLabel
      // 
      this.TabIndexLabel.Location = new System.Drawing.Point(11, 53);
      this.TabIndexLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.TabIndexLabel.Name = "TabIndexLabel";
      this.TabIndexLabel.Size = new System.Drawing.Size(153, 20);
      this.TabIndexLabel.TabIndex = 2;
      this.TabIndexLabel.Text = "Tab Index";
      // 
      // TabTextText
      // 
      this.TabTextText.Location = new System.Drawing.Point(165, 86);
      this.TabTextText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.TabTextText.Name = "TabTextText";
      this.TabTextText.Size = new System.Drawing.Size(433, 26);
      this.TabTextText.TabIndex = 5;
      // 
      // TabTextLabel
      // 
      this.TabTextLabel.Location = new System.Drawing.Point(11, 89);
      this.TabTextLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.TabTextLabel.Name = "TabTextLabel";
      this.TabTextLabel.Size = new System.Drawing.Size(153, 20);
      this.TabTextLabel.TabIndex = 4;
      this.TabTextLabel.Text = "Tab Text";
      // 
      // DescriptionText
      // 
      this.DescriptionText.Location = new System.Drawing.Point(165, 122);
      this.DescriptionText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.DescriptionText.Name = "DescriptionText";
      this.DescriptionText.Size = new System.Drawing.Size(433, 26);
      this.DescriptionText.TabIndex = 7;
      // 
      // DescriptionLabel
      // 
      this.DescriptionLabel.Location = new System.Drawing.Point(11, 125);
      this.DescriptionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.DescriptionLabel.Name = "DescriptionLabel";
      this.DescriptionLabel.Size = new System.Drawing.Size(153, 20);
      this.DescriptionLabel.TabIndex = 6;
      this.DescriptionLabel.Text = "Description";
      // 
      // TabDetail
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(612, 205);
      this.Controls.Add(this.DescriptionText);
      this.Controls.Add(this.DescriptionLabel);
      this.Controls.Add(this.TabTextText);
      this.Controls.Add(this.TabTextLabel);
      this.Controls.Add(this.TabIndexText);
      this.Controls.Add(this.TabIndexLabel);
      this.Controls.Add(this.ParentNameText);
      this.Controls.Add(this.ParentNameLabel);
      this.Controls.Add(this.FormCancelButton);
      this.Controls.Add(this.OKButton);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "TabDetail";
      this.Text = "Tab Detail";
      this.Load += new System.EventHandler(this.TabDetail_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

        #endregion

        private System.Windows.Forms.TextBox ParentNameText;
        private System.Windows.Forms.Label ParentNameLabel;
        private System.Windows.Forms.Button FormCancelButton;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.TextBox TabIndexText;
        private System.Windows.Forms.Label TabIndexLabel;
        private System.Windows.Forms.TextBox TabTextText;
        private System.Windows.Forms.Label TabTextLabel;
        private System.Windows.Forms.TextBox DescriptionText;
        private System.Windows.Forms.Label DescriptionLabel;
    }
}