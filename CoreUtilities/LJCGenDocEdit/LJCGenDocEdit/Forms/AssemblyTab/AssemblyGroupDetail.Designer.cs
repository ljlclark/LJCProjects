namespace LJCGenDocEdit
{
  partial class AssemblyGroupDetail
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
      this.NameText = new System.Windows.Forms.TextBox();
      this.NameLabel = new System.Windows.Forms.Label();
      this.FormCancelButton = new System.Windows.Forms.Button();
      this.OKButton = new System.Windows.Forms.Button();
      this.HeadingText = new System.Windows.Forms.TextBox();
      this.HeadingLabel = new System.Windows.Forms.Label();
      this.SequenceText = new System.Windows.Forms.TextBox();
      this.SequenceLabel = new System.Windows.Forms.Label();
      this.ActiveCheckbox = new System.Windows.Forms.CheckBox();
      this.DialogMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.DialogNew = new System.Windows.Forms.ToolStripMenuItem();
      this.DialogPrevious = new System.Windows.Forms.ToolStripMenuItem();
      this.DialogNext = new System.Windows.Forms.ToolStripMenuItem();
      this.DialogHelp = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.DialogMenu.SuspendLayout();
      this.SuspendLayout();
      // 
      // NameText
      // 
      this.NameText.Location = new System.Drawing.Point(145, 14);
      this.NameText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.NameText.Name = "NameText";
      this.NameText.Size = new System.Drawing.Size(413, 26);
      this.NameText.TabIndex = 1;
      // 
      // NameLabel
      // 
      this.NameLabel.Location = new System.Drawing.Point(18, 18);
      this.NameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.NameLabel.Name = "NameLabel";
      this.NameLabel.Size = new System.Drawing.Size(124, 20);
      this.NameLabel.TabIndex = 0;
      this.NameLabel.Text = "Group Name";
      // 
      // FormCancelButton
      // 
      this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.FormCancelButton.Location = new System.Drawing.Point(448, 125);
      this.FormCancelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.FormCancelButton.Name = "FormCancelButton";
      this.FormCancelButton.Size = new System.Drawing.Size(112, 35);
      this.FormCancelButton.TabIndex = 5;
      this.FormCancelButton.Text = "Cancel";
      this.FormCancelButton.UseVisualStyleBackColor = true;
      // 
      // OKButton
      // 
      this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.OKButton.Location = new System.Drawing.Point(326, 125);
      this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.OKButton.Name = "OKButton";
      this.OKButton.Size = new System.Drawing.Size(112, 35);
      this.OKButton.TabIndex = 7;
      this.OKButton.Text = "&OK";
      this.OKButton.UseVisualStyleBackColor = true;
      this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
      // 
      // HeadingText
      // 
      this.HeadingText.Location = new System.Drawing.Point(145, 50);
      this.HeadingText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.HeadingText.Name = "HeadingText";
      this.HeadingText.Size = new System.Drawing.Size(413, 26);
      this.HeadingText.TabIndex = 3;
      // 
      // HeadingLabel
      // 
      this.HeadingLabel.Location = new System.Drawing.Point(18, 54);
      this.HeadingLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.HeadingLabel.Name = "HeadingLabel";
      this.HeadingLabel.Size = new System.Drawing.Size(124, 20);
      this.HeadingLabel.TabIndex = 2;
      this.HeadingLabel.Text = "Heading";
      // 
      // SequenceText
      // 
      this.SequenceText.Location = new System.Drawing.Point(145, 86);
      this.SequenceText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.SequenceText.Name = "SequenceText";
      this.SequenceText.Size = new System.Drawing.Size(45, 26);
      this.SequenceText.TabIndex = 5;
      // 
      // SequenceLabel
      // 
      this.SequenceLabel.Location = new System.Drawing.Point(18, 90);
      this.SequenceLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.SequenceLabel.Name = "SequenceLabel";
      this.SequenceLabel.Size = new System.Drawing.Size(124, 20);
      this.SequenceLabel.TabIndex = 4;
      this.SequenceLabel.Text = "Sequence";
      // 
      // ActiveCheckbox
      // 
      this.ActiveCheckbox.AutoSize = true;
      this.ActiveCheckbox.Location = new System.Drawing.Point(277, 87);
      this.ActiveCheckbox.Name = "ActiveCheckbox";
      this.ActiveCheckbox.Size = new System.Drawing.Size(78, 24);
      this.ActiveCheckbox.TabIndex = 6;
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
      this.DialogMenu.Size = new System.Drawing.Size(303, 171);
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
      // DialogHelp
      // 
      this.DialogHelp.Name = "DialogHelp";
      this.DialogHelp.Size = new System.Drawing.Size(302, 32);
      this.DialogHelp.Text = "&Help";
      this.DialogHelp.Click += new System.EventHandler(this.DialogHelp_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(299, 6);
      // 
      // AssemblyGroupDetail
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(578, 172);
      this.ContextMenuStrip = this.DialogMenu;
      this.Controls.Add(this.SequenceText);
      this.Controls.Add(this.SequenceLabel);
      this.Controls.Add(this.ActiveCheckbox);
      this.Controls.Add(this.NameText);
      this.Controls.Add(this.NameLabel);
      this.Controls.Add(this.FormCancelButton);
      this.Controls.Add(this.OKButton);
      this.Controls.Add(this.HeadingText);
      this.Controls.Add(this.HeadingLabel);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "AssemblyGroupDetail";
      this.ShowInTaskbar = false;
      this.Text = "Assembly Group Detail";
      this.Load += new System.EventHandler(this.AssemblyGroupDetail_Load);
      this.DialogMenu.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox NameText;
    private System.Windows.Forms.Label NameLabel;
    private System.Windows.Forms.Button FormCancelButton;
    private System.Windows.Forms.Button OKButton;
    private System.Windows.Forms.TextBox HeadingText;
    private System.Windows.Forms.Label HeadingLabel;
    private System.Windows.Forms.TextBox SequenceText;
    private System.Windows.Forms.Label SequenceLabel;
    private System.Windows.Forms.CheckBox ActiveCheckbox;
    private System.Windows.Forms.ContextMenuStrip DialogMenu;
    private System.Windows.Forms.ToolStripMenuItem DialogNew;
    private System.Windows.Forms.ToolStripMenuItem DialogPrevious;
    private System.Windows.Forms.ToolStripMenuItem DialogNext;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem DialogHelp;
  }
}