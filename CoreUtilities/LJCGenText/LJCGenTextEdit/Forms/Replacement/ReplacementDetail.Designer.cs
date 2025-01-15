namespace LJCGenTextEdit
{
	partial class ReplacementDetail
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
      this.FormCancelButton = new System.Windows.Forms.Button();
      this.OKButton = new System.Windows.Forms.Button();
      this.NameText = new System.Windows.Forms.TextBox();
      this.NameLabel = new System.Windows.Forms.Label();
      this.ValueTextbox = new System.Windows.Forms.TextBox();
      this.ValueLabel = new System.Windows.Forms.Label();
      this.ParentNameText = new System.Windows.Forms.TextBox();
      this.ParentNameLabel = new System.Windows.Forms.Label();
      this.DetailMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.DetailMenuHelp = new System.Windows.Forms.ToolStripMenuItem();
      this.DetailMenu.SuspendLayout();
      this.SuspendLayout();
      // 
      // FormCancelButton
      // 
      this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.FormCancelButton.Location = new System.Drawing.Point(447, 126);
      this.FormCancelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.FormCancelButton.Name = "FormCancelButton";
      this.FormCancelButton.Size = new System.Drawing.Size(112, 35);
      this.FormCancelButton.TabIndex = 7;
      this.FormCancelButton.Text = "Cancel";
      this.FormCancelButton.UseVisualStyleBackColor = true;
      this.FormCancelButton.Click += new System.EventHandler(this.FormCancelButton_Click);
      // 
      // OKButton
      // 
      this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.OKButton.Location = new System.Drawing.Point(325, 126);
      this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.OKButton.Name = "OKButton";
      this.OKButton.Size = new System.Drawing.Size(112, 35);
      this.OKButton.TabIndex = 6;
      this.OKButton.Text = "&OK";
      this.OKButton.UseVisualStyleBackColor = true;
      this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
      // 
      // NameText
      // 
      this.NameText.Location = new System.Drawing.Point(175, 51);
      this.NameText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.NameText.Name = "NameText";
      this.NameText.Size = new System.Drawing.Size(383, 26);
      this.NameText.TabIndex = 3;
      // 
      // NameLabel
      // 
      this.NameLabel.Location = new System.Drawing.Point(18, 56);
      this.NameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.NameLabel.Name = "NameLabel";
      this.NameLabel.Size = new System.Drawing.Size(154, 20);
      this.NameLabel.TabIndex = 2;
      this.NameLabel.Text = "Replace Name";
      // 
      // ValueTextbox
      // 
      this.ValueTextbox.Location = new System.Drawing.Point(175, 87);
      this.ValueTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.ValueTextbox.Name = "ValueTextbox";
      this.ValueTextbox.Size = new System.Drawing.Size(383, 26);
      this.ValueTextbox.TabIndex = 5;
      // 
      // ValueLabel
      // 
      this.ValueLabel.Location = new System.Drawing.Point(18, 92);
      this.ValueLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.ValueLabel.Name = "ValueLabel";
      this.ValueLabel.Size = new System.Drawing.Size(124, 20);
      this.ValueLabel.TabIndex = 4;
      this.ValueLabel.Text = "Replace Value";
      // 
      // ParentNameText
      // 
      this.ParentNameText.Location = new System.Drawing.Point(175, 15);
      this.ParentNameText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.ParentNameText.Name = "ParentNameText";
      this.ParentNameText.ReadOnly = true;
      this.ParentNameText.Size = new System.Drawing.Size(383, 26);
      this.ParentNameText.TabIndex = 1;
      // 
      // ParentNameLabel
      // 
      this.ParentNameLabel.Location = new System.Drawing.Point(18, 20);
      this.ParentNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.ParentNameLabel.Name = "ParentNameLabel";
      this.ParentNameLabel.Size = new System.Drawing.Size(124, 20);
      this.ParentNameLabel.TabIndex = 0;
      this.ParentNameLabel.Text = "Item Name";
      // 
      // DetailMenu
      // 
      this.DetailMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.DetailMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DetailMenuHelp});
      this.DetailMenu.Name = "DetailMenu";
      this.DetailMenu.Size = new System.Drawing.Size(153, 36);
      // 
      // DetailMenuHelp
      // 
      this.DetailMenuHelp.Name = "DetailMenuHelp";
      this.DetailMenuHelp.ShortcutKeyDisplayString = "F1";
      this.DetailMenuHelp.Size = new System.Drawing.Size(152, 32);
      this.DetailMenuHelp.Text = "&Help";
      this.DetailMenuHelp.Click += new System.EventHandler(this.DetailMenuHelp_Click);
      // 
      // ReplacementDetail
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(578, 175);
      this.ContextMenuStrip = this.DetailMenu;
      this.Controls.Add(this.ParentNameText);
      this.Controls.Add(this.ParentNameLabel);
      this.Controls.Add(this.ValueTextbox);
      this.Controls.Add(this.ValueLabel);
      this.Controls.Add(this.FormCancelButton);
      this.Controls.Add(this.OKButton);
      this.Controls.Add(this.NameText);
      this.Controls.Add(this.NameLabel);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "ReplacementDetail";
      this.Text = "Replacement Detail";
      this.Load += new System.EventHandler(this.ReplacementDetail_Load);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ReplacementDetail_KeyDown);
      this.DetailMenu.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button FormCancelButton;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.TextBox NameText;
		private System.Windows.Forms.Label NameLabel;
		private System.Windows.Forms.TextBox ValueTextbox;
		private System.Windows.Forms.Label ValueLabel;
		private System.Windows.Forms.TextBox ParentNameText;
		private System.Windows.Forms.Label ParentNameLabel;
		private System.Windows.Forms.ContextMenuStrip DetailMenu;
		private System.Windows.Forms.ToolStripMenuItem DetailMenuHelp;
	}
}