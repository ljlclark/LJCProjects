namespace LJCDataUtility5
{
  partial class DataTableDetail
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
      DescriptionText = new TextBox();
      DescriptonLabel = new Label();
      NameText = new TextBox();
      NameLabel = new Label();
      ParentNameText = new TextBox();
      ParentNameLabel = new Label();
      NewNameText = new TextBox();
      NewNameLabel = new Label();
      SequenceText = new TextBox();
      SequenceLabel = new Label();
      SchemaLabel = new Label();
      SchemaText = new TextBox();
      SuspendLayout();
      // 
      // FormCancelButton
      // 
      FormCancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      FormCancelButton.Location = new Point(532, 278);
      FormCancelButton.Margin = new Padding(9, 8, 9, 8);
      FormCancelButton.Name = "FormCancelButton";
      FormCancelButton.Size = new Size(140, 40);
      FormCancelButton.TabIndex = 13;
      FormCancelButton.Text = "Cancel";
      FormCancelButton.UseVisualStyleBackColor = true;
      // 
      // OKButton
      // 
      OKButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      OKButton.Location = new Point(373, 278);
      OKButton.Margin = new Padding(9, 8, 9, 8);
      OKButton.Name = "OKButton";
      OKButton.Size = new Size(140, 40);
      OKButton.TabIndex = 12;
      OKButton.Text = "&OK";
      OKButton.UseVisualStyleBackColor = true;
      OKButton.Click += OKButton_Click;
      // 
      // DescriptionText
      // 
      DescriptionText.Location = new Point(172, 104);
      DescriptionText.Margin = new Padding(6);
      DescriptionText.Name = "DescriptionText";
      DescriptionText.Size = new Size(500, 31);
      DescriptionText.TabIndex = 5;
      // 
      // DescriptonLabel
      // 
      DescriptonLabel.AutoSize = true;
      DescriptonLabel.Location = new Point(12, 107);
      DescriptonLabel.Name = "DescriptonLabel";
      DescriptonLabel.Size = new Size(102, 25);
      DescriptonLabel.TabIndex = 4;
      DescriptonLabel.Text = "Description";
      // 
      // NameText
      // 
      NameText.Location = new Point(171, 61);
      NameText.Margin = new Padding(6);
      NameText.Name = "NameText";
      NameText.Size = new Size(500, 31);
      NameText.TabIndex = 3;
      // 
      // NameLabel
      // 
      NameLabel.AutoSize = true;
      NameLabel.Location = new Point(12, 64);
      NameLabel.Name = "NameLabel";
      NameLabel.Size = new Size(59, 25);
      NameLabel.TabIndex = 2;
      NameLabel.Text = "Name";
      // 
      // ParentNameText
      // 
      ParentNameText.Location = new Point(171, 18);
      ParentNameText.Margin = new Padding(6);
      ParentNameText.Name = "ParentNameText";
      ParentNameText.ReadOnly = true;
      ParentNameText.Size = new Size(500, 31);
      ParentNameText.TabIndex = 1;
      // 
      // ParentNameLabel
      // 
      ParentNameLabel.AutoSize = true;
      ParentNameLabel.Location = new Point(12, 21);
      ParentNameLabel.Name = "ParentNameLabel";
      ParentNameLabel.Size = new Size(73, 25);
      ParentNameLabel.TabIndex = 0;
      ParentNameLabel.Text = "Module";
      // 
      // NewNameText
      // 
      NewNameText.Location = new Point(172, 233);
      NewNameText.Margin = new Padding(6);
      NewNameText.Name = "NewNameText";
      NewNameText.Size = new Size(500, 31);
      NewNameText.TabIndex = 11;
      // 
      // NewNameLabel
      // 
      NewNameLabel.AutoSize = true;
      NewNameLabel.Location = new Point(12, 236);
      NewNameLabel.Name = "NewNameLabel";
      NewNameLabel.Size = new Size(99, 25);
      NewNameLabel.TabIndex = 10;
      NewNameLabel.Text = "New Name";
      // 
      // SequenceText
      // 
      SequenceText.Location = new Point(172, 147);
      SequenceText.Margin = new Padding(6);
      SequenceText.Name = "SequenceText";
      SequenceText.Size = new Size(55, 31);
      SequenceText.TabIndex = 7;
      // 
      // SequenceLabel
      // 
      SequenceLabel.AutoSize = true;
      SequenceLabel.Location = new Point(12, 150);
      SequenceLabel.Name = "SequenceLabel";
      SequenceLabel.Size = new Size(88, 25);
      SequenceLabel.TabIndex = 6;
      SequenceLabel.Text = "Sequence";
      // 
      // SchemaLabel
      // 
      SchemaLabel.AutoSize = true;
      SchemaLabel.Location = new Point(12, 193);
      SchemaLabel.Name = "SchemaLabel";
      SchemaLabel.Size = new Size(74, 25);
      SchemaLabel.TabIndex = 8;
      SchemaLabel.Text = "Schema";
      // 
      // SchemaText
      // 
      SchemaText.Location = new Point(172, 190);
      SchemaText.Margin = new Padding(6);
      SchemaText.Name = "SchemaText";
      SchemaText.Size = new Size(55, 31);
      SchemaText.TabIndex = 9;
      // 
      // DataTableDetail
      // 
      AutoScaleDimensions = new SizeF(144F, 144F);
      AutoScaleMode = AutoScaleMode.Dpi;
      ClientSize = new Size(686, 329);
      Controls.Add(SchemaLabel);
      Controls.Add(SchemaText);
      Controls.Add(SequenceLabel);
      Controls.Add(SequenceText);
      Controls.Add(NewNameText);
      Controls.Add(NewNameLabel);
      Controls.Add(ParentNameText);
      Controls.Add(ParentNameLabel);
      Controls.Add(DescriptionText);
      Controls.Add(DescriptonLabel);
      Controls.Add(NameText);
      Controls.Add(NameLabel);
      Controls.Add(FormCancelButton);
      Controls.Add(OKButton);
      Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
      FormBorderStyle = FormBorderStyle.FixedDialog;
      Margin = new Padding(4);
      Name = "DataTableDetail";
      Text = "DataTable Detail";
      Load += DataTableDetail_Load;
      ResumeLayout(false);
      PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button FormCancelButton;
    private System.Windows.Forms.Button OKButton;
    private System.Windows.Forms.TextBox DescriptionText;
    private System.Windows.Forms.Label DescriptonLabel;
    private System.Windows.Forms.TextBox NameText;
    private System.Windows.Forms.Label NameLabel;
    private System.Windows.Forms.TextBox ParentNameText;
    private System.Windows.Forms.Label ParentNameLabel;
    private System.Windows.Forms.TextBox NewNameText;
    private System.Windows.Forms.Label NewNameLabel;
    private System.Windows.Forms.TextBox SequenceText;
    private System.Windows.Forms.Label SequenceLabel;
    private System.Windows.Forms.Label SchemaLabel;
    private System.Windows.Forms.TextBox SchemaText;
  }
}