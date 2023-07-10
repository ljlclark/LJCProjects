namespace LJCGenDocEdit
{
  partial class MethodDetail
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MethodDetail));
      this.NameText = new System.Windows.Forms.TextBox();
      this.NameLabel = new System.Windows.Forms.Label();
      this.FormCancelButton = new System.Windows.Forms.Button();
      this.OKButton = new System.Windows.Forms.Button();
      this.DescriptionText = new System.Windows.Forms.TextBox();
      this.DescriptionLabel = new System.Windows.Forms.Label();
      this.ClassText = new System.Windows.Forms.TextBox();
      this.ClassLabel = new System.Windows.Forms.Label();
      this.GroupText = new System.Windows.Forms.TextBox();
      this.GroupLabel = new System.Windows.Forms.Label();
      this.SequenceText = new System.Windows.Forms.TextBox();
      this.SequenceLabel = new System.Windows.Forms.Label();
      this.ActiveCheckbox = new System.Windows.Forms.CheckBox();
      this.DialogMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.DialogNew = new System.Windows.Forms.ToolStripMenuItem();
      this.DialogPrevious = new System.Windows.Forms.ToolStripMenuItem();
      this.DialogNext = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.DialogHelp = new System.Windows.Forms.ToolStripMenuItem();
      this.NameButton = new System.Windows.Forms.Button();
      this.ButtonImages = new System.Windows.Forms.ImageList(this.components);
      this.OverloadText = new System.Windows.Forms.TextBox();
      this.OverloadLabel = new System.Windows.Forms.Label();
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
      this.FormCancelButton.Location = new System.Drawing.Point(448, 233);
      this.FormCancelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.FormCancelButton.Name = "FormCancelButton";
      this.FormCancelButton.Size = new System.Drawing.Size(112, 35);
      this.FormCancelButton.TabIndex = 15;
      this.FormCancelButton.Text = "Cancel";
      this.FormCancelButton.UseVisualStyleBackColor = true;
      // 
      // OKButton
      // 
      this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.OKButton.Location = new System.Drawing.Point(326, 233);
      this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.OKButton.Name = "OKButton";
      this.OKButton.Size = new System.Drawing.Size(112, 35);
      this.OKButton.TabIndex = 14;
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
      // ClassText
      // 
      this.ClassText.Location = new System.Drawing.Point(145, 14);
      this.ClassText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.ClassText.Name = "ClassText";
      this.ClassText.ReadOnly = true;
      this.ClassText.Size = new System.Drawing.Size(413, 26);
      this.ClassText.TabIndex = 1;
      // 
      // ClassLabel
      // 
      this.ClassLabel.Location = new System.Drawing.Point(18, 18);
      this.ClassLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.ClassLabel.Name = "ClassLabel";
      this.ClassLabel.Size = new System.Drawing.Size(124, 20);
      this.ClassLabel.TabIndex = 0;
      this.ClassLabel.Text = "Class";
      // 
      // GroupText
      // 
      this.GroupText.Location = new System.Drawing.Point(145, 50);
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
      this.GroupLabel.Size = new System.Drawing.Size(124, 20);
      this.GroupLabel.TabIndex = 2;
      this.GroupLabel.Text = "Method Group";
      // 
      // SequenceText
      // 
      this.SequenceText.Location = new System.Drawing.Point(145, 194);
      this.SequenceText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.SequenceText.Name = "SequenceText";
      this.SequenceText.Size = new System.Drawing.Size(45, 26);
      this.SequenceText.TabIndex = 12;
      // 
      // SequenceLabel
      // 
      this.SequenceLabel.Location = new System.Drawing.Point(18, 198);
      this.SequenceLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.SequenceLabel.Name = "SequenceLabel";
      this.SequenceLabel.Size = new System.Drawing.Size(124, 20);
      this.SequenceLabel.TabIndex = 11;
      this.SequenceLabel.Text = "Sequence";
      // 
      // ActiveCheckbox
      // 
      this.ActiveCheckbox.AutoSize = true;
      this.ActiveCheckbox.Location = new System.Drawing.Point(277, 195);
      this.ActiveCheckbox.Name = "ActiveCheckbox";
      this.ActiveCheckbox.Size = new System.Drawing.Size(78, 24);
      this.ActiveCheckbox.TabIndex = 13;
      this.ActiveCheckbox.Text = "Active";
      this.ActiveCheckbox.UseVisualStyleBackColor = true;
      // 
      // DialogMenu
      // 
      this.DialogMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.DialogMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DialogNew,
            this.DialogPrevious,
            this.DialogNext,
            this.toolStripSeparator1,
            this.DialogHelp});
      this.DialogMenu.Name = "DialogMenu";
      this.DialogMenu.Size = new System.Drawing.Size(303, 138);
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
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(299, 6);
      // 
      // DialogHelp
      // 
      this.DialogHelp.Name = "DialogHelp";
      this.DialogHelp.Size = new System.Drawing.Size(302, 32);
      this.DialogHelp.Text = "&Help";
      this.DialogHelp.Click += new System.EventHandler(this.DialogHelp_Click);
      // 
      // NameButton
      // 
      this.NameButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.NameButton.ImageKey = "Ellipse.bmp";
      this.NameButton.ImageList = this.ButtonImages;
      this.NameButton.Location = new System.Drawing.Point(530, 85);
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
      // OverloadText
      // 
      this.OverloadText.Location = new System.Drawing.Point(145, 158);
      this.OverloadText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.OverloadText.Name = "OverloadText";
      this.OverloadText.Size = new System.Drawing.Size(413, 26);
      this.OverloadText.TabIndex = 10;
      this.OverloadText.Leave += new System.EventHandler(this.OverloadText_Leave);
      // 
      // OverloadLabel
      // 
      this.OverloadLabel.Location = new System.Drawing.Point(18, 162);
      this.OverloadLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.OverloadLabel.Name = "OverloadLabel";
      this.OverloadLabel.Size = new System.Drawing.Size(124, 20);
      this.OverloadLabel.TabIndex = 9;
      this.OverloadLabel.Text = "Overload Name";
      // 
      // MethodDetail
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(578, 280);
      this.ContextMenuStrip = this.DialogMenu;
      this.Controls.Add(this.OverloadText);
      this.Controls.Add(this.OverloadLabel);
      this.Controls.Add(this.NameButton);
      this.Controls.Add(this.SequenceText);
      this.Controls.Add(this.SequenceLabel);
      this.Controls.Add(this.ActiveCheckbox);
      this.Controls.Add(this.GroupText);
      this.Controls.Add(this.GroupLabel);
      this.Controls.Add(this.ClassText);
      this.Controls.Add(this.ClassLabel);
      this.Controls.Add(this.NameText);
      this.Controls.Add(this.NameLabel);
      this.Controls.Add(this.FormCancelButton);
      this.Controls.Add(this.OKButton);
      this.Controls.Add(this.DescriptionText);
      this.Controls.Add(this.DescriptionLabel);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "MethodDetail";
      this.ShowInTaskbar = false;
      this.Text = "Method Detail";
      this.Load += new System.EventHandler(this.MethodDetail_Load);
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
    private System.Windows.Forms.TextBox ClassText;
    private System.Windows.Forms.Label ClassLabel;
    private System.Windows.Forms.TextBox GroupText;
    private System.Windows.Forms.Label GroupLabel;
    private System.Windows.Forms.TextBox SequenceText;
    private System.Windows.Forms.Label SequenceLabel;
    private System.Windows.Forms.CheckBox ActiveCheckbox;
    private System.Windows.Forms.ContextMenuStrip DialogMenu;
    private System.Windows.Forms.ToolStripMenuItem DialogNew;
    private System.Windows.Forms.ToolStripMenuItem DialogPrevious;
    private System.Windows.Forms.ToolStripMenuItem DialogNext;
    private System.Windows.Forms.Button NameButton;
    private System.Windows.Forms.ImageList ButtonImages;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem DialogHelp;
    private System.Windows.Forms.TextBox OverloadText;
    private System.Windows.Forms.Label OverloadLabel;
  }
}