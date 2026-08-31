namespace LJCDataUtility5
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
      FormCancelButton = new Button();
      OKButton = new Button();
      ClusteredCheck = new CheckBox();
      TargetTableText = new TextBox();
      TargetTableLabel = new Label();
      TargetColumnText = new TextBox();
      TargetColumnLabel = new Label();
      SourceColumnText = new TextBox();
      SourceColumnLabel = new Label();
      ParentNameText = new TextBox();
      ParentNameLabel = new Label();
      KeyTypeCombo = new LJCControls5.LJCItemCombo();
      KeyTypeLabel = new Label();
      NameText = new TextBox();
      NameLabel = new Label();
      AscendingCheck = new CheckBox();
      SuspendLayout();
      // 
      // FormCancelButton
      // 
      FormCancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      FormCancelButton.Location = new Point(578, 356);
      FormCancelButton.Margin = new Padding(9, 8, 9, 8);
      FormCancelButton.Name = "FormCancelButton";
      FormCancelButton.Size = new Size(140, 40);
      FormCancelButton.TabIndex = 15;
      FormCancelButton.Text = "Cancel";
      FormCancelButton.UseVisualStyleBackColor = true;
      // 
      // OKButton
      // 
      OKButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      OKButton.Location = new Point(420, 356);
      OKButton.Margin = new Padding(9, 8, 9, 8);
      OKButton.Name = "OKButton";
      OKButton.Size = new Size(140, 40);
      OKButton.TabIndex = 14;
      OKButton.Text = "&OK";
      OKButton.UseVisualStyleBackColor = true;
      OKButton.Click += OKButton_Click;
      // 
      // ClusteredCheck
      // 
      ClusteredCheck.AutoSize = true;
      ClusteredCheck.Location = new Point(218, 276);
      ClusteredCheck.Margin = new Padding(4);
      ClusteredCheck.Name = "ClusteredCheck";
      ClusteredCheck.Size = new Size(112, 29);
      ClusteredCheck.TabIndex = 12;
      ClusteredCheck.Text = "Clustered";
      ClusteredCheck.UseVisualStyleBackColor = true;
      // 
      // TargetTableText
      // 
      TargetTableText.Location = new Point(218, 190);
      TargetTableText.Margin = new Padding(9, 8, 9, 8);
      TargetTableText.Name = "TargetTableText";
      TargetTableText.Size = new Size(500, 31);
      TargetTableText.TabIndex = 9;
      // 
      // TargetTableLabel
      // 
      TargetTableLabel.AutoSize = true;
      TargetTableLabel.Location = new Point(12, 196);
      TargetTableLabel.Name = "TargetTableLabel";
      TargetTableLabel.Size = new Size(105, 25);
      TargetTableLabel.TabIndex = 8;
      TargetTableLabel.Text = "Target Table";
      // 
      // TargetColumnText
      // 
      TargetColumnText.Location = new Point(218, 233);
      TargetColumnText.Margin = new Padding(9, 8, 9, 8);
      TargetColumnText.Name = "TargetColumnText";
      TargetColumnText.Size = new Size(500, 31);
      TargetColumnText.TabIndex = 11;
      // 
      // TargetColumnLabel
      // 
      TargetColumnLabel.AutoSize = true;
      TargetColumnLabel.Location = new Point(12, 239);
      TargetColumnLabel.Name = "TargetColumnLabel";
      TargetColumnLabel.Size = new Size(127, 25);
      TargetColumnLabel.TabIndex = 10;
      TargetColumnLabel.Text = "Target Column";
      // 
      // SourceColumnText
      // 
      SourceColumnText.Location = new Point(218, 147);
      SourceColumnText.Margin = new Padding(9, 8, 9, 8);
      SourceColumnText.Name = "SourceColumnText";
      SourceColumnText.Size = new Size(500, 31);
      SourceColumnText.TabIndex = 7;
      // 
      // SourceColumnLabel
      // 
      SourceColumnLabel.AutoSize = true;
      SourceColumnLabel.Location = new Point(12, 153);
      SourceColumnLabel.Name = "SourceColumnLabel";
      SourceColumnLabel.Size = new Size(133, 25);
      SourceColumnLabel.TabIndex = 6;
      SourceColumnLabel.Text = "Source Column";
      // 
      // ParentNameText
      // 
      ParentNameText.Location = new Point(218, 18);
      ParentNameText.Margin = new Padding(9, 8, 9, 8);
      ParentNameText.Name = "ParentNameText";
      ParentNameText.ReadOnly = true;
      ParentNameText.Size = new Size(500, 31);
      ParentNameText.TabIndex = 1;
      // 
      // ParentNameLabel
      // 
      ParentNameLabel.AutoSize = true;
      ParentNameLabel.Location = new Point(12, 24);
      ParentNameLabel.Name = "ParentNameLabel";
      ParentNameLabel.Size = new Size(94, 25);
      ParentNameLabel.TabIndex = 0;
      ParentNameLabel.Text = "Data Table";
      // 
      // KeyTypeCombo
      // 
      KeyTypeCombo.DropDownStyle = ComboBoxStyle.DropDownList;
      KeyTypeCombo.Location = new Point(218, 104);
      KeyTypeCombo.Margin = new Padding(9, 8, 9, 8);
      KeyTypeCombo.Name = "KeyTypeCombo";
      KeyTypeCombo.Size = new Size(200, 33);
      KeyTypeCombo.TabIndex = 5;
      // 
      // KeyTypeLabel
      // 
      KeyTypeLabel.AutoSize = true;
      KeyTypeLabel.Location = new Point(12, 110);
      KeyTypeLabel.Name = "KeyTypeLabel";
      KeyTypeLabel.Size = new Size(77, 25);
      KeyTypeLabel.TabIndex = 4;
      KeyTypeLabel.Text = "KeyType";
      // 
      // NameText
      // 
      NameText.Location = new Point(218, 61);
      NameText.Margin = new Padding(9, 8, 9, 8);
      NameText.Name = "NameText";
      NameText.Size = new Size(500, 31);
      NameText.TabIndex = 3;
      // 
      // NameLabel
      // 
      NameLabel.AutoSize = true;
      NameLabel.Location = new Point(12, 67);
      NameLabel.Name = "NameLabel";
      NameLabel.Size = new Size(59, 25);
      NameLabel.TabIndex = 2;
      NameLabel.Text = "Name";
      // 
      // AscendingCheck
      // 
      AscendingCheck.AutoSize = true;
      AscendingCheck.Location = new Point(218, 315);
      AscendingCheck.Margin = new Padding(4);
      AscendingCheck.Name = "AscendingCheck";
      AscendingCheck.Size = new Size(121, 29);
      AscendingCheck.TabIndex = 13;
      AscendingCheck.Text = "Ascending";
      AscendingCheck.UseVisualStyleBackColor = true;
      // 
      // DataKeyDetail
      // 
      AutoScaleDimensions = new SizeF(144F, 144F);
      AutoScaleMode = AutoScaleMode.Dpi;
      ClientSize = new Size(733, 408);
      Controls.Add(AscendingCheck);
      Controls.Add(ClusteredCheck);
      Controls.Add(TargetTableText);
      Controls.Add(TargetTableLabel);
      Controls.Add(TargetColumnText);
      Controls.Add(TargetColumnLabel);
      Controls.Add(SourceColumnText);
      Controls.Add(SourceColumnLabel);
      Controls.Add(ParentNameText);
      Controls.Add(ParentNameLabel);
      Controls.Add(KeyTypeCombo);
      Controls.Add(KeyTypeLabel);
      Controls.Add(NameText);
      Controls.Add(NameLabel);
      Controls.Add(FormCancelButton);
      Controls.Add(OKButton);
      Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
      FormBorderStyle = FormBorderStyle.FixedDialog;
      Margin = new Padding(4);
      MaximizeBox = false;
      MinimizeBox = false;
      Name = "DataKeyDetail";
      Text = "DataKey Detail";
      Load += DataKeyDetail_Load;
      ResumeLayout(false);
      PerformLayout();

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
    private LJCControls5.LJCItemCombo KeyTypeCombo;
    private System.Windows.Forms.Label KeyTypeLabel;
    private System.Windows.Forms.TextBox NameText;
    private System.Windows.Forms.Label NameLabel;
    private System.Windows.Forms.CheckBox AscendingCheck;
  }
}