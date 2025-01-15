namespace LJCViewEditor
{
	partial class ViewDataDetail
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
      this.ParentTextbox = new System.Windows.Forms.TextBox();
      this.ParentLabel = new System.Windows.Forms.Label();
      this.NameTextbox = new System.Windows.Forms.TextBox();
      this.NameLabel = new System.Windows.Forms.Label();
      this.DescriptionTextbox = new System.Windows.Forms.TextBox();
      this.DescriptionLabel = new System.Windows.Forms.Label();
      this.ViewDetailMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.ViewDetailHelp = new System.Windows.Forms.ToolStripMenuItem();
      this.ViewDetailMenu.SuspendLayout();
      this.SuspendLayout();
      // 
      // FormCancelButton
      // 
      this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.FormCancelButton.Location = new System.Drawing.Point(594, 136);
      this.FormCancelButton.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
      this.FormCancelButton.Name = "FormCancelButton";
      this.FormCancelButton.Size = new System.Drawing.Size(137, 38);
      this.FormCancelButton.TabIndex = 7;
      this.FormCancelButton.Text = "Cancel";
      this.FormCancelButton.UseVisualStyleBackColor = true;
      this.FormCancelButton.Click += new System.EventHandler(this.FormCancelButton_Click);
      // 
      // OKButton
      // 
      this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.OKButton.Location = new System.Drawing.Point(446, 136);
      this.OKButton.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
      this.OKButton.Name = "OKButton";
      this.OKButton.Size = new System.Drawing.Size(137, 38);
      this.OKButton.TabIndex = 6;
      this.OKButton.Text = "&OK";
      this.OKButton.UseVisualStyleBackColor = true;
      this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
      // 
      // ParentTextbox
      // 
      this.ParentTextbox.Location = new System.Drawing.Point(202, 15);
      this.ParentTextbox.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
      this.ParentTextbox.Name = "ParentTextbox";
      this.ParentTextbox.ReadOnly = true;
      this.ParentTextbox.Size = new System.Drawing.Size(528, 28);
      this.ParentTextbox.TabIndex = 1;
      this.ParentTextbox.TabStop = false;
      // 
      // ParentLabel
      // 
      this.ParentLabel.Location = new System.Drawing.Point(13, 18);
      this.ParentLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
      this.ParentLabel.Name = "ParentLabel";
      this.ParentLabel.Size = new System.Drawing.Size(187, 32);
      this.ParentLabel.TabIndex = 0;
      this.ParentLabel.Text = "Table";
      // 
      // NameTextbox
      // 
      this.NameTextbox.Location = new System.Drawing.Point(202, 55);
      this.NameTextbox.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
      this.NameTextbox.Name = "NameTextbox";
      this.NameTextbox.Size = new System.Drawing.Size(528, 28);
      this.NameTextbox.TabIndex = 3;
      // 
      // NameLabel
      // 
      this.NameLabel.Location = new System.Drawing.Point(13, 58);
      this.NameLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
      this.NameLabel.Name = "NameLabel";
      this.NameLabel.Size = new System.Drawing.Size(187, 32);
      this.NameLabel.TabIndex = 2;
      this.NameLabel.Text = "Name";
      // 
      // DescriptionTextbox
      // 
      this.DescriptionTextbox.Location = new System.Drawing.Point(202, 95);
      this.DescriptionTextbox.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
      this.DescriptionTextbox.Name = "DescriptionTextbox";
      this.DescriptionTextbox.Size = new System.Drawing.Size(528, 28);
      this.DescriptionTextbox.TabIndex = 5;
      // 
      // DescriptionLabel
      // 
      this.DescriptionLabel.Location = new System.Drawing.Point(13, 98);
      this.DescriptionLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
      this.DescriptionLabel.Name = "DescriptionLabel";
      this.DescriptionLabel.Size = new System.Drawing.Size(187, 32);
      this.DescriptionLabel.TabIndex = 4;
      this.DescriptionLabel.Text = "Description";
      // 
      // ViewDetailMenu
      // 
      this.ViewDetailMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.ViewDetailMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewDetailHelp});
      this.ViewDetailMenu.Name = "ViewDetailMenu";
      this.ViewDetailMenu.Size = new System.Drawing.Size(153, 36);
      // 
      // ViewDetailHelp
      // 
      this.ViewDetailHelp.Name = "ViewDetailHelp";
      this.ViewDetailHelp.ShortcutKeyDisplayString = "F1";
      this.ViewDetailHelp.Size = new System.Drawing.Size(152, 32);
      this.ViewDetailHelp.Text = "&Help";
      this.ViewDetailHelp.Click += new System.EventHandler(this.ViewDetailHelp_Click);
      // 
      // ViewDataDetail
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(747, 186);
      this.ContextMenuStrip = this.ViewDetailMenu;
      this.Controls.Add(this.DescriptionTextbox);
      this.Controls.Add(this.DescriptionLabel);
      this.Controls.Add(this.NameTextbox);
      this.Controls.Add(this.NameLabel);
      this.Controls.Add(this.ParentTextbox);
      this.Controls.Add(this.ParentLabel);
      this.Controls.Add(this.FormCancelButton);
      this.Controls.Add(this.OKButton);
      this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.KeyPreview = true;
      this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "ViewDataDetail";
      this.Text = "ViewData Detail";
      this.Load += new System.EventHandler(this.ViewDataDetail_Load);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ViewDataDetail_KeyDown);
      this.ViewDetailMenu.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button FormCancelButton;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.TextBox ParentTextbox;
		private System.Windows.Forms.Label ParentLabel;
		private System.Windows.Forms.TextBox NameTextbox;
		private System.Windows.Forms.Label NameLabel;
		private System.Windows.Forms.TextBox DescriptionTextbox;
		private System.Windows.Forms.Label DescriptionLabel;
		private System.Windows.Forms.ContextMenuStrip ViewDetailMenu;
		private System.Windows.Forms.ToolStripMenuItem ViewDetailHelp;
	}
}