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
      this.ValueTextbox = new System.Windows.Forms.TextBox();
      this.ValueLabel = new System.Windows.Forms.Label();
      this.NameTextbox = new System.Windows.Forms.TextBox();
      this.NameLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // FormCancelButton
      // 
      this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.FormCancelButton.Location = new System.Drawing.Point(578, 125);
      this.FormCancelButton.Margin = new System.Windows.Forms.Padding(6);
      this.FormCancelButton.Name = "FormCancelButton";
      this.FormCancelButton.Size = new System.Drawing.Size(174, 46);
      this.FormCancelButton.TabIndex = 5;
      this.FormCancelButton.Text = "Cancel";
      this.FormCancelButton.UseVisualStyleBackColor = true;
      this.FormCancelButton.Click += new System.EventHandler(this.FormCancelButton_Click);
      // 
      // OKButton
      // 
      this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.OKButton.Location = new System.Drawing.Point(387, 125);
      this.OKButton.Margin = new System.Windows.Forms.Padding(6);
      this.OKButton.Name = "OKButton";
      this.OKButton.Size = new System.Drawing.Size(174, 46);
      this.OKButton.TabIndex = 4;
      this.OKButton.Text = "&OK";
      this.OKButton.UseVisualStyleBackColor = true;
      this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
      // 
      // ValueTextbox
      // 
      this.ValueTextbox.Location = new System.Drawing.Point(254, 69);
      this.ValueTextbox.Margin = new System.Windows.Forms.Padding(6);
      this.ValueTextbox.Name = "ValueTextbox";
      this.ValueTextbox.Size = new System.Drawing.Size(500, 26);
      this.ValueTextbox.TabIndex = 3;
      // 
      // ValueLabel
      // 
      this.ValueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ValueLabel.Location = new System.Drawing.Point(23, 75);
      this.ValueLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.ValueLabel.Name = "ValueLabel";
      this.ValueLabel.Size = new System.Drawing.Size(225, 26);
      this.ValueLabel.TabIndex = 2;
      this.ValueLabel.Text = "Description";
      // 
      // NameTextbox
      // 
      this.NameTextbox.Location = new System.Drawing.Point(254, 22);
      this.NameTextbox.Margin = new System.Windows.Forms.Padding(6);
      this.NameTextbox.Name = "NameTextbox";
      this.NameTextbox.Size = new System.Drawing.Size(500, 26);
      this.NameTextbox.TabIndex = 1;
      // 
      // NameLabel
      // 
      this.NameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.NameLabel.Location = new System.Drawing.Point(23, 28);
      this.NameLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.NameLabel.Name = "NameLabel";
      this.NameLabel.Size = new System.Drawing.Size(225, 26);
      this.NameLabel.TabIndex = 0;
      this.NameLabel.Text = "Name";
      // 
      // DataModuleDetail
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(778, 185);
      this.Controls.Add(this.ValueTextbox);
      this.Controls.Add(this.ValueLabel);
      this.Controls.Add(this.NameTextbox);
      this.Controls.Add(this.NameLabel);
      this.Controls.Add(this.FormCancelButton);
      this.Controls.Add(this.OKButton);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Name = "DataModuleDetail";
      this.Text = "DataModule Detai";
      this.Load += new System.EventHandler(this.DataModuleDetai_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button FormCancelButton;
    private System.Windows.Forms.Button OKButton;
    private System.Windows.Forms.TextBox ValueTextbox;
    private System.Windows.Forms.Label ValueLabel;
    private System.Windows.Forms.TextBox NameTextbox;
    private System.Windows.Forms.Label NameLabel;
  }
}