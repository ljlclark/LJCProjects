namespace LJCDataUtility
{
  partial class DataKeyDetail
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
      this.ClusteredCheck = new System.Windows.Forms.CheckBox();
      this.TargetTableText = new System.Windows.Forms.TextBox();
      this.TargetTableLabel = new System.Windows.Forms.Label();
      this.TargetColumnText = new System.Windows.Forms.TextBox();
      this.TargetColumnLabel = new System.Windows.Forms.Label();
      this.SourceColumnText = new System.Windows.Forms.TextBox();
      this.SourceColumnLabel = new System.Windows.Forms.Label();
      this.ParentNameText = new System.Windows.Forms.TextBox();
      this.ParentNameLabel = new System.Windows.Forms.Label();
      this.KeyTypeText = new LJCWinFormControls.LJCItemCombo();
      this.KeyTypeLabel = new System.Windows.Forms.Label();
      this.NameText = new System.Windows.Forms.TextBox();
      this.NameLabel = new System.Windows.Forms.Label();
      this.AscendingCheck = new System.Windows.Forms.CheckBox();
      this.SuspendLayout();
      // 
      // FormCancelButton
      // 
      this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.FormCancelButton.Location = new System.Drawing.Point(578, 407);
      this.FormCancelButton.Margin = new System.Windows.Forms.Padding(9, 8, 9, 8);
      this.FormCancelButton.Name = "FormCancelButton";
      this.FormCancelButton.Size = new System.Drawing.Size(174, 46);
      this.FormCancelButton.TabIndex = 15;
      this.FormCancelButton.Text = "Cancel";
      this.FormCancelButton.UseVisualStyleBackColor = true;
      // 
      // OKButton
      // 
      this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.OKButton.Location = new System.Drawing.Point(387, 407);
      this.OKButton.Margin = new System.Windows.Forms.Padding(9, 8, 9, 8);
      this.OKButton.Name = "OKButton";
      this.OKButton.Size = new System.Drawing.Size(174, 46);
      this.OKButton.TabIndex = 14;
      this.OKButton.Text = "&OK";
      this.OKButton.UseVisualStyleBackColor = true;
      this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
      // 
      // ClusteredCheck
      // 
      this.ClusteredCheck.AutoSize = true;
      this.ClusteredCheck.Location = new System.Drawing.Point(254, 305);
      this.ClusteredCheck.Margin = new System.Windows.Forms.Padding(4);
      this.ClusteredCheck.Name = "ClusteredCheck";
      this.ClusteredCheck.Size = new System.Drawing.Size(131, 30);
      this.ClusteredCheck.TabIndex = 12;
      this.ClusteredCheck.Text = "Clustered";
      this.ClusteredCheck.UseVisualStyleBackColor = true;
      // 
      // TargetTableText
      // 
      this.TargetTableText.Location = new System.Drawing.Point(254, 210);
      this.TargetTableText.Margin = new System.Windows.Forms.Padding(9, 8, 9, 8);
      this.TargetTableText.Name = "TargetTableText";
      this.TargetTableText.Size = new System.Drawing.Size(500, 32);
      this.TargetTableText.TabIndex = 9;
      // 
      // TargetTableLabel
      // 
      this.TargetTableLabel.AutoSize = true;
      this.TargetTableLabel.Location = new System.Drawing.Point(23, 216);
      this.TargetTableLabel.Name = "TargetTableLabel";
      this.TargetTableLabel.Size = new System.Drawing.Size(132, 26);
      this.TargetTableLabel.TabIndex = 8;
      this.TargetTableLabel.Text = "Target Table";
      // 
      // TargetColumnText
      // 
      this.TargetColumnText.Location = new System.Drawing.Point(254, 257);
      this.TargetColumnText.Margin = new System.Windows.Forms.Padding(9, 8, 9, 8);
      this.TargetColumnText.Name = "TargetColumnText";
      this.TargetColumnText.Size = new System.Drawing.Size(500, 32);
      this.TargetColumnText.TabIndex = 11;
      // 
      // TargetColumnLabel
      // 
      this.TargetColumnLabel.AutoSize = true;
      this.TargetColumnLabel.Location = new System.Drawing.Point(23, 263);
      this.TargetColumnLabel.Name = "TargetColumnLabel";
      this.TargetColumnLabel.Size = new System.Drawing.Size(155, 26);
      this.TargetColumnLabel.TabIndex = 10;
      this.TargetColumnLabel.Text = "Target Column";
      // 
      // SourceColumnText
      // 
      this.SourceColumnText.Location = new System.Drawing.Point(254, 163);
      this.SourceColumnText.Margin = new System.Windows.Forms.Padding(9, 8, 9, 8);
      this.SourceColumnText.Name = "SourceColumnText";
      this.SourceColumnText.Size = new System.Drawing.Size(500, 32);
      this.SourceColumnText.TabIndex = 7;
      // 
      // SourceColumnLabel
      // 
      this.SourceColumnLabel.AutoSize = true;
      this.SourceColumnLabel.Location = new System.Drawing.Point(23, 169);
      this.SourceColumnLabel.Name = "SourceColumnLabel";
      this.SourceColumnLabel.Size = new System.Drawing.Size(163, 26);
      this.SourceColumnLabel.TabIndex = 6;
      this.SourceColumnLabel.Text = "Source Column";
      // 
      // ParentNameText
      // 
      this.ParentNameText.Location = new System.Drawing.Point(254, 22);
      this.ParentNameText.Margin = new System.Windows.Forms.Padding(9, 8, 9, 8);
      this.ParentNameText.Name = "ParentNameText";
      this.ParentNameText.ReadOnly = true;
      this.ParentNameText.Size = new System.Drawing.Size(500, 32);
      this.ParentNameText.TabIndex = 1;
      // 
      // ParentNameLabel
      // 
      this.ParentNameLabel.AutoSize = true;
      this.ParentNameLabel.Location = new System.Drawing.Point(23, 28);
      this.ParentNameLabel.Name = "ParentNameLabel";
      this.ParentNameLabel.Size = new System.Drawing.Size(117, 26);
      this.ParentNameLabel.TabIndex = 0;
      this.ParentNameLabel.Text = "Data Table";
      // 
      // KeyTypeText
      // 
      this.KeyTypeText.Location = new System.Drawing.Point(254, 116);
      this.KeyTypeText.Margin = new System.Windows.Forms.Padding(9, 8, 9, 8);
      this.KeyTypeText.Name = "KeyTypeText";
      this.KeyTypeText.Size = new System.Drawing.Size(200, 34);
      this.KeyTypeText.TabIndex = 5;
      // 
      // KeyTypeLabel
      // 
      this.KeyTypeLabel.AutoSize = true;
      this.KeyTypeLabel.Location = new System.Drawing.Point(23, 122);
      this.KeyTypeLabel.Name = "KeyTypeLabel";
      this.KeyTypeLabel.Size = new System.Drawing.Size(97, 26);
      this.KeyTypeLabel.TabIndex = 4;
      this.KeyTypeLabel.Text = "KeyType";
      // 
      // NameText
      // 
      this.NameText.Location = new System.Drawing.Point(254, 69);
      this.NameText.Margin = new System.Windows.Forms.Padding(9, 8, 9, 8);
      this.NameText.Name = "NameText";
      this.NameText.Size = new System.Drawing.Size(500, 32);
      this.NameText.TabIndex = 3;
      // 
      // NameLabel
      // 
      this.NameLabel.AutoSize = true;
      this.NameLabel.Location = new System.Drawing.Point(23, 75);
      this.NameLabel.Name = "NameLabel";
      this.NameLabel.Size = new System.Drawing.Size(71, 26);
      this.NameLabel.TabIndex = 2;
      this.NameLabel.Text = "Name";
      // 
      // AscendingCheck
      // 
      this.AscendingCheck.AutoSize = true;
      this.AscendingCheck.Location = new System.Drawing.Point(254, 352);
      this.AscendingCheck.Margin = new System.Windows.Forms.Padding(4);
      this.AscendingCheck.Name = "AscendingCheck";
      this.AscendingCheck.Size = new System.Drawing.Size(140, 30);
      this.AscendingCheck.TabIndex = 13;
      this.AscendingCheck.Text = "Ascending";
      this.AscendingCheck.UseVisualStyleBackColor = true;
      // 
      // DataKeyDetail
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(778, 467);
      this.Controls.Add(this.AscendingCheck);
      this.Controls.Add(this.ClusteredCheck);
      this.Controls.Add(this.TargetTableText);
      this.Controls.Add(this.TargetTableLabel);
      this.Controls.Add(this.TargetColumnText);
      this.Controls.Add(this.TargetColumnLabel);
      this.Controls.Add(this.SourceColumnText);
      this.Controls.Add(this.SourceColumnLabel);
      this.Controls.Add(this.ParentNameText);
      this.Controls.Add(this.ParentNameLabel);
      this.Controls.Add(this.KeyTypeText);
      this.Controls.Add(this.KeyTypeLabel);
      this.Controls.Add(this.NameText);
      this.Controls.Add(this.NameLabel);
      this.Controls.Add(this.FormCancelButton);
      this.Controls.Add(this.OKButton);
      this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Margin = new System.Windows.Forms.Padding(4);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "DataKeyDetail";
      this.Text = "DataKey Detail";
      this.Load += new System.EventHandler(this.DataKeyDetail_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button FormCancelButton;
    private System.Windows.Forms.Button OKButton;
    private System.Windows.Forms.CheckBox ClusteredCheck;
    private System.Windows.Forms.TextBox TargetTableText;
    private System.Windows.Forms.Label TargetTableLabel;
    private System.Windows.Forms.TextBox TargetColumnText;
    private System.Windows.Forms.Label TargetColumnLabel;
    private System.Windows.Forms.TextBox SourceColumnText;
    private System.Windows.Forms.Label SourceColumnLabel;
    private System.Windows.Forms.TextBox ParentNameText;
    private System.Windows.Forms.Label ParentNameLabel;
    private LJCWinFormControls.LJCItemCombo KeyTypeText;
    private System.Windows.Forms.Label KeyTypeLabel;
    private System.Windows.Forms.TextBox NameText;
    private System.Windows.Forms.Label NameLabel;
    private System.Windows.Forms.CheckBox AscendingCheck;
  }
}