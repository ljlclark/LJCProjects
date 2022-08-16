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
			this.FormCancelButton.Location = new System.Drawing.Point(486, 124);
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
			this.OKButton.Location = new System.Drawing.Point(365, 124);
			this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(112, 35);
			this.OKButton.TabIndex = 6;
			this.OKButton.Text = "&OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// ParentTextbox
			// 
			this.ParentTextbox.Location = new System.Drawing.Point(165, 14);
			this.ParentTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ParentTextbox.Name = "ParentTextbox";
			this.ParentTextbox.ReadOnly = true;
			this.ParentTextbox.Size = new System.Drawing.Size(433, 26);
			this.ParentTextbox.TabIndex = 1;
			this.ParentTextbox.TabStop = false;
			// 
			// ParentLabel
			// 
			this.ParentLabel.Location = new System.Drawing.Point(11, 19);
			this.ParentLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ParentLabel.Name = "ParentLabel";
			this.ParentLabel.Size = new System.Drawing.Size(153, 20);
			this.ParentLabel.TabIndex = 0;
			this.ParentLabel.Text = "Table";
			// 
			// NameTextbox
			// 
			this.NameTextbox.Location = new System.Drawing.Point(165, 50);
			this.NameTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.NameTextbox.Name = "NameTextbox";
			this.NameTextbox.Size = new System.Drawing.Size(433, 26);
			this.NameTextbox.TabIndex = 3;
			this.NameTextbox.TextChanged += new System.EventHandler(this.NameTextbox_TextChanged);
			this.NameTextbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NameTextbox_KeyPress);
			// 
			// NameLabel
			// 
			this.NameLabel.Location = new System.Drawing.Point(11, 55);
			this.NameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.NameLabel.Name = "NameLabel";
			this.NameLabel.Size = new System.Drawing.Size(153, 20);
			this.NameLabel.TabIndex = 2;
			this.NameLabel.Text = "Name";
			// 
			// DescriptionTextbox
			// 
			this.DescriptionTextbox.Location = new System.Drawing.Point(165, 86);
			this.DescriptionTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.DescriptionTextbox.Name = "DescriptionTextbox";
			this.DescriptionTextbox.Size = new System.Drawing.Size(433, 26);
			this.DescriptionTextbox.TabIndex = 5;
			// 
			// DescriptionLabel
			// 
			this.DescriptionLabel.Location = new System.Drawing.Point(11, 91);
			this.DescriptionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.DescriptionLabel.Name = "DescriptionLabel";
			this.DescriptionLabel.Size = new System.Drawing.Size(153, 20);
			this.DescriptionLabel.TabIndex = 4;
			this.DescriptionLabel.Text = "Description";
			// 
			// ViewDetailMenu
			// 
			this.ViewDetailMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.ViewDetailMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewDetailHelp});
			this.ViewDetailMenu.Name = "ViewDetailMenu";
			this.ViewDetailMenu.Size = new System.Drawing.Size(241, 67);
			// 
			// ViewDetailHelp
			// 
			this.ViewDetailHelp.Name = "ViewDetailHelp";
			this.ViewDetailHelp.ShortcutKeyDisplayString = "F1";
			this.ViewDetailHelp.Size = new System.Drawing.Size(240, 30);
			this.ViewDetailHelp.Text = "&Help";
			this.ViewDetailHelp.Click += new System.EventHandler(this.ViewDetailHelp_Click);
			// 
			// ViewDataDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(611, 169);
			this.ContextMenuStrip = this.ViewDetailMenu;
			this.Controls.Add(this.DescriptionTextbox);
			this.Controls.Add(this.DescriptionLabel);
			this.Controls.Add(this.NameTextbox);
			this.Controls.Add(this.NameLabel);
			this.Controls.Add(this.ParentTextbox);
			this.Controls.Add(this.ParentLabel);
			this.Controls.Add(this.FormCancelButton);
			this.Controls.Add(this.OKButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
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