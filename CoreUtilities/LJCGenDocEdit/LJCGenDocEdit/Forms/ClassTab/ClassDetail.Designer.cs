namespace LJCGenDocEdit
{
  partial class ClassDetail
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClassDetail));
      this.NameText = new System.Windows.Forms.TextBox();
      this.NameLabel = new System.Windows.Forms.Label();
      this.FormCancelButton = new System.Windows.Forms.Button();
      this.OKButton = new System.Windows.Forms.Button();
      this.DescriptionText = new System.Windows.Forms.TextBox();
      this.DescriptionLabel = new System.Windows.Forms.Label();
      this.AssemblyText = new System.Windows.Forms.TextBox();
      this.AssemblyLabel = new System.Windows.Forms.Label();
      this.ParentText = new System.Windows.Forms.TextBox();
      this.ParentLabel = new System.Windows.Forms.Label();
      this.SequenceText = new System.Windows.Forms.TextBox();
      this.SequenceLabel = new System.Windows.Forms.Label();
      this.ActiveCheckbox = new System.Windows.Forms.CheckBox();
      this.DialogMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.DialogNew = new System.Windows.Forms.ToolStripMenuItem();
      this.DialogPrevious = new System.Windows.Forms.ToolStripMenuItem();
      this.DialogNext = new System.Windows.Forms.ToolStripMenuItem();
      this.NameButton = new System.Windows.Forms.Button();
      this.ButtonImages = new System.Windows.Forms.ImageList(this.components);
      this.DialogMenu.SuspendLayout();
      this.SuspendLayout();
      // 
      // NameText
      // 
      this.NameText.Location = new System.Drawing.Point(145, 86);
      this.NameText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.NameText.Name = "NameText";
      this.NameText.ReadOnly = true;
      this.NameText.Size = new System.Drawing.Size(377, 26);
      this.NameText.TabIndex = 5;
      // 
      // NameLabel
      // 
      this.NameLabel.Location = new System.Drawing.Point(18, 90);
      this.NameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.NameLabel.Name = "NameLabel";
      this.NameLabel.Size = new System.Drawing.Size(124, 20);
      this.NameLabel.TabIndex = 4;
      this.NameLabel.Text = "Name";
      // 
      // FormCancelButton
      // 
      this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.FormCancelButton.Location = new System.Drawing.Point(448, 197);
      this.FormCancelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.FormCancelButton.Name = "FormCancelButton";
      this.FormCancelButton.Size = new System.Drawing.Size(112, 35);
      this.FormCancelButton.TabIndex = 13;
      this.FormCancelButton.Text = "Cancel";
      this.FormCancelButton.UseVisualStyleBackColor = true;
      // 
      // OKButton
      // 
      this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.OKButton.Location = new System.Drawing.Point(326, 197);
      this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.OKButton.Name = "OKButton";
      this.OKButton.Size = new System.Drawing.Size(112, 35);
      this.OKButton.TabIndex = 12;
      this.OKButton.Text = "&OK";
      this.OKButton.UseVisualStyleBackColor = true;
      this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
      // 
      // DescriptionText
      // 
      this.DescriptionText.Location = new System.Drawing.Point(145, 122);
      this.DescriptionText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.DescriptionText.Name = "DescriptionText";
      this.DescriptionText.Size = new System.Drawing.Size(413, 26);
      this.DescriptionText.TabIndex = 8;
      // 
      // DescriptionLabel
      // 
      this.DescriptionLabel.Location = new System.Drawing.Point(18, 126);
      this.DescriptionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.DescriptionLabel.Name = "DescriptionLabel";
      this.DescriptionLabel.Size = new System.Drawing.Size(124, 20);
      this.DescriptionLabel.TabIndex = 7;
      this.DescriptionLabel.Text = "Description";
      // 
      // AssemblyText
      // 
      this.AssemblyText.Location = new System.Drawing.Point(145, 14);
      this.AssemblyText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.AssemblyText.Name = "AssemblyText";
      this.AssemblyText.ReadOnly = true;
      this.AssemblyText.Size = new System.Drawing.Size(413, 26);
      this.AssemblyText.TabIndex = 1;
      // 
      // AssemblyLabel
      // 
      this.AssemblyLabel.Location = new System.Drawing.Point(18, 18);
      this.AssemblyLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.AssemblyLabel.Name = "AssemblyLabel";
      this.AssemblyLabel.Size = new System.Drawing.Size(124, 20);
      this.AssemblyLabel.TabIndex = 0;
      this.AssemblyLabel.Text = "Assembly";
      // 
      // ParentText
      // 
      this.ParentText.Location = new System.Drawing.Point(145, 50);
      this.ParentText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.ParentText.Name = "ParentText";
      this.ParentText.ReadOnly = true;
      this.ParentText.Size = new System.Drawing.Size(413, 26);
      this.ParentText.TabIndex = 3;
      // 
      // ParentLabel
      // 
      this.ParentLabel.Location = new System.Drawing.Point(18, 54);
      this.ParentLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.ParentLabel.Name = "ParentLabel";
      this.ParentLabel.Size = new System.Drawing.Size(124, 20);
      this.ParentLabel.TabIndex = 2;
      this.ParentLabel.Text = "Class Group";
      // 
      // SequenceText
      // 
      this.SequenceText.Location = new System.Drawing.Point(145, 158);
      this.SequenceText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.SequenceText.Name = "SequenceText";
      this.SequenceText.Size = new System.Drawing.Size(45, 26);
      this.SequenceText.TabIndex = 10;
      // 
      // SequenceLabel
      // 
      this.SequenceLabel.Location = new System.Drawing.Point(18, 162);
      this.SequenceLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.SequenceLabel.Name = "SequenceLabel";
      this.SequenceLabel.Size = new System.Drawing.Size(124, 20);
      this.SequenceLabel.TabIndex = 9;
      this.SequenceLabel.Text = "Sequence";
      // 
      // ActiveCheckbox
      // 
      this.ActiveCheckbox.AutoSize = true;
      this.ActiveCheckbox.Location = new System.Drawing.Point(277, 159);
      this.ActiveCheckbox.Name = "ActiveCheckbox";
      this.ActiveCheckbox.Size = new System.Drawing.Size(78, 24);
      this.ActiveCheckbox.TabIndex = 11;
      this.ActiveCheckbox.Text = "Active";
      this.ActiveCheckbox.UseVisualStyleBackColor = true;
      // 
      // DialogMenu
      // 
      this.DialogMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.DialogMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DialogNew,
            this.DialogPrevious,
            this.DialogNext});
      this.DialogMenu.Name = "DialogMenu";
      this.DialogMenu.Size = new System.Drawing.Size(303, 100);
      // 
      // DialogNew
      // 
      this.DialogNew.Name = "DialogNew";
      this.DialogNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
      this.DialogNew.Size = new System.Drawing.Size(302, 32);
      this.DialogNew.Text = "Save and &New";
      this.DialogNew.Click += new System.EventHandler(this.DialogNew_Click);
      // 
      // DialogPrevious
      // 
      this.DialogPrevious.Name = "DialogPrevious";
      this.DialogPrevious.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)));
      this.DialogPrevious.Size = new System.Drawing.Size(302, 32);
      this.DialogPrevious.Text = "Save and &Previous";
      this.DialogPrevious.Click += new System.EventHandler(this.DialogPrevious_Click);
      // 
      // DialogNext
      // 
      this.DialogNext.Name = "DialogNext";
      this.DialogNext.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)));
      this.DialogNext.Size = new System.Drawing.Size(302, 32);
      this.DialogNext.Text = "&Save and Next";
      this.DialogNext.Click += new System.EventHandler(this.DialogNext_Click);
      // 
      // NameButton
      // 
      this.NameButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.NameButton.ImageKey = "Ellipse.bmp";
      this.NameButton.ImageList = this.ButtonImages;
      this.NameButton.Location = new System.Drawing.Point(530, 84);
      this.NameButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.NameButton.Name = "NameButton";
      this.NameButton.Size = new System.Drawing.Size(28, 28);
      this.NameButton.TabIndex = 6;
      this.NameButton.UseVisualStyleBackColor = true;
      this.NameButton.Click += new System.EventHandler(this.NameButton_Click);
      // 
      // ButtonImages
      // 
      this.ButtonImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ButtonImages.ImageStream")));
      this.ButtonImages.TransparentColor = System.Drawing.Color.Magenta;
      this.ButtonImages.Images.SetKeyName(0, "Ellipse.bmp");
      this.ButtonImages.Images.SetKeyName(1, "Calendar.bmp");
      // 
      // ClassDetail
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(578, 244);
      this.ContextMenuStrip = this.DialogMenu;
      this.Controls.Add(this.NameButton);
      this.Controls.Add(this.SequenceText);
      this.Controls.Add(this.SequenceLabel);
      this.Controls.Add(this.ActiveCheckbox);
      this.Controls.Add(this.ParentText);
      this.Controls.Add(this.ParentLabel);
      this.Controls.Add(this.AssemblyText);
      this.Controls.Add(this.AssemblyLabel);
      this.Controls.Add(this.NameText);
      this.Controls.Add(this.NameLabel);
      this.Controls.Add(this.FormCancelButton);
      this.Controls.Add(this.OKButton);
      this.Controls.Add(this.DescriptionText);
      this.Controls.Add(this.DescriptionLabel);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "ClassDetail";
      this.ShowInTaskbar = false;
      this.Text = "Class Detail";
      this.Load += new System.EventHandler(this.ClassDetail_Load);
      this.DialogMenu.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox NameText;
    private System.Windows.Forms.Label NameLabel;
    private System.Windows.Forms.Button FormCancelButton;
    private System.Windows.Forms.Button OKButton;
    private System.Windows.Forms.TextBox DescriptionText;
    private System.Windows.Forms.Label DescriptionLabel;
    private System.Windows.Forms.TextBox AssemblyText;
    private System.Windows.Forms.Label AssemblyLabel;
    private System.Windows.Forms.TextBox ParentText;
    private System.Windows.Forms.Label ParentLabel;
    private System.Windows.Forms.TextBox SequenceText;
    private System.Windows.Forms.Label SequenceLabel;
    private System.Windows.Forms.CheckBox ActiveCheckbox;
    private System.Windows.Forms.ContextMenuStrip DialogMenu;
    private System.Windows.Forms.ToolStripMenuItem DialogNew;
    private System.Windows.Forms.ToolStripMenuItem DialogPrevious;
    private System.Windows.Forms.ToolStripMenuItem DialogNext;
    private System.Windows.Forms.Button NameButton;
    private System.Windows.Forms.ImageList ButtonImages;
  }
}