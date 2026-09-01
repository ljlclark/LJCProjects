namespace LJCDataUtility5
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
      FormCancelButton = new Button();
      OKButton = new Button();
      DescriptionText = new TextBox();
      DescriptionLabel = new Label();
      NameText = new TextBox();
      NameLabel = new Label();
      SuspendLayout();
      // 
      // FormCancelButton
      // 
      FormCancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      FormCancelButton.Location = new Point(553, 104);
      FormCancelButton.Margin = new Padding(6);
      FormCancelButton.Name = "FormCancelButton";
      FormCancelButton.Size = new Size(140, 40);
      FormCancelButton.TabIndex = 5;
      FormCancelButton.Text = "Cancel";
      FormCancelButton.UseVisualStyleBackColor = true;
      // 
      // OKButton
      // 
      OKButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      OKButton.Location = new Point(401, 104);
      OKButton.Margin = new Padding(6);
      OKButton.Name = "OKButton";
      OKButton.Size = new Size(140, 40);
      OKButton.TabIndex = 4;
      OKButton.Text = "&OK";
      OKButton.UseVisualStyleBackColor = true;
      OKButton.Click += OKButton_Click;
      // 
      // DescriptionText
      // 
      DescriptionText.Location = new Point(193, 61);
      DescriptionText.Margin = new Padding(6);
      DescriptionText.Name = "DescriptionText";
      DescriptionText.Size = new Size(500, 31);
      DescriptionText.TabIndex = 3;
      // 
      // DescriptionLabel
      // 
      DescriptionLabel.AutoSize = true;
      DescriptionLabel.Location = new Point(12, 67);
      DescriptionLabel.Name = "DescriptionLabel";
      DescriptionLabel.Size = new Size(102, 25);
      DescriptionLabel.TabIndex = 2;
      DescriptionLabel.Text = "Description";
      // 
      // NameText
      // 
      NameText.Location = new Point(193, 18);
      NameText.Margin = new Padding(6);
      NameText.Name = "NameText";
      NameText.Size = new Size(500, 31);
      NameText.TabIndex = 1;
      // 
      // NameLabel
      // 
      NameLabel.AutoSize = true;
      NameLabel.Location = new Point(12, 24);
      NameLabel.Name = "NameLabel";
      NameLabel.Size = new Size(59, 25);
      NameLabel.TabIndex = 0;
      NameLabel.Text = "Name";
      // 
      // DataModuleDetail
      // 
      AutoScaleDimensions = new SizeF(144F, 144F);
      AutoScaleMode = AutoScaleMode.Dpi;
      ClientSize = new Size(706, 155);
      Controls.Add(DescriptionText);
      Controls.Add(DescriptionLabel);
      Controls.Add(NameText);
      Controls.Add(NameLabel);
      Controls.Add(FormCancelButton);
      Controls.Add(OKButton);
      Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
      FormBorderStyle = FormBorderStyle.FixedDialog;
      Name = "DataModuleDetail";
      Text = "DataModule Detai";
      Load += DataModuleDetail_Load;
      ResumeLayout(false);
      PerformLayout();

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