namespace LJCDataUtility
{
  partial class DataMapTableDetail
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
      this.ParentNameTextbox = new System.Windows.Forms.TextBox();
      this.ParentNameLabel = new System.Windows.Forms.Label();
      this.NameTextbox = new System.Windows.Forms.TextBox();
      this.NameLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // FormCancelButton
      // 
      this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.FormCancelButton.Location = new System.Drawing.Point(578, 125);
      this.FormCancelButton.Margin = new System.Windows.Forms.Padding(9, 8, 9, 8);
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
      this.OKButton.Margin = new System.Windows.Forms.Padding(9, 8, 9, 8);
      this.OKButton.Name = "OKButton";
      this.OKButton.Size = new System.Drawing.Size(174, 46);
      this.OKButton.TabIndex = 4;
      this.OKButton.Text = "&OK";
      this.OKButton.UseVisualStyleBackColor = true;
      this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
      // 
      // ParentNameTextbox
      // 
      this.ParentNameTextbox.Location = new System.Drawing.Point(254, 22);
      this.ParentNameTextbox.Margin = new System.Windows.Forms.Padding(6);
      this.ParentNameTextbox.Name = "ParentNameTextbox";
      this.ParentNameTextbox.ReadOnly = true;
      this.ParentNameTextbox.Size = new System.Drawing.Size(500, 32);
      this.ParentNameTextbox.TabIndex = 1;
      // 
      // ParentNameLabel
      // 
      this.ParentNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ParentNameLabel.Location = new System.Drawing.Point(23, 23);
      this.ParentNameLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.ParentNameLabel.Name = "ParentNameLabel";
      this.ParentNameLabel.Size = new System.Drawing.Size(225, 26);
      this.ParentNameLabel.TabIndex = 0;
      this.ParentNameLabel.Text = "Table";
      // 
      // NameTextbox
      // 
      this.NameTextbox.Location = new System.Drawing.Point(254, 69);
      this.NameTextbox.Margin = new System.Windows.Forms.Padding(6);
      this.NameTextbox.Name = "NameTextbox";
      this.NameTextbox.Size = new System.Drawing.Size(500, 32);
      this.NameTextbox.TabIndex = 3;
      // 
      // NameLabel
      // 
      this.NameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.NameLabel.Location = new System.Drawing.Point(23, 70);
      this.NameLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.NameLabel.Name = "NameLabel";
      this.NameLabel.Size = new System.Drawing.Size(225, 26);
      this.NameLabel.TabIndex = 2;
      this.NameLabel.Text = "Column";
      // 
      // DataMapTableDetail
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(778, 185);
      this.Controls.Add(this.ParentNameTextbox);
      this.Controls.Add(this.ParentNameLabel);
      this.Controls.Add(this.NameTextbox);
      this.Controls.Add(this.NameLabel);
      this.Controls.Add(this.FormCancelButton);
      this.Controls.Add(this.OKButton);
      this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Margin = new System.Windows.Forms.Padding(4);
      this.Name = "DataMapTableDetail";
      this.Text = "DataMapTable Detail";
      this.Load += new System.EventHandler(this.DataMapTableDetail_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button FormCancelButton;
    private System.Windows.Forms.Button OKButton;
    private System.Windows.Forms.TextBox ParentNameTextbox;
    private System.Windows.Forms.Label ParentNameLabel;
    private System.Windows.Forms.TextBox NameTextbox;
    private System.Windows.Forms.Label NameLabel;
  }
}