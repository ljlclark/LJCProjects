namespace LJCRegionManager
{
	partial class CityDetail
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
			this.DescriptionTextbox = new System.Windows.Forms.TextBox();
			this.DescriptionLabel = new System.Windows.Forms.Label();
			this.NameTextbox = new System.Windows.Forms.TextBox();
			this.NameLabel = new System.Windows.Forms.Label();
			this.ParentNameTextbox = new System.Windows.Forms.TextBox();
			this.ParentNameLabel = new System.Windows.Forms.Label();
			this.FormCancelButton = new System.Windows.Forms.Button();
			this.OKButton = new System.Windows.Forms.Button();
			this.CityCheckbox = new System.Windows.Forms.CheckBox();
			this.DialogMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.DialogMenuHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.DialogMenu.SuspendLayout();
			this.SuspendLayout();
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
			// NameTextbox
			// 
			this.NameTextbox.Location = new System.Drawing.Point(165, 50);
			this.NameTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.NameTextbox.Name = "NameTextbox";
			this.NameTextbox.Size = new System.Drawing.Size(433, 26);
			this.NameTextbox.TabIndex = 3;
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
			// ParentNameTextbox
			// 
			this.ParentNameTextbox.Location = new System.Drawing.Point(165, 14);
			this.ParentNameTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ParentNameTextbox.Name = "ParentNameTextbox";
			this.ParentNameTextbox.ReadOnly = true;
			this.ParentNameTextbox.Size = new System.Drawing.Size(433, 26);
			this.ParentNameTextbox.TabIndex = 1;
			this.ParentNameTextbox.TabStop = false;
			// 
			// ParentNameLabel
			// 
			this.ParentNameLabel.Location = new System.Drawing.Point(11, 19);
			this.ParentNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ParentNameLabel.Name = "ParentNameLabel";
			this.ParentNameLabel.Size = new System.Drawing.Size(153, 20);
			this.ParentNameLabel.TabIndex = 0;
			this.ParentNameLabel.Text = "Province";
			// 
			// FormCancelButton
			// 
			this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.FormCancelButton.Location = new System.Drawing.Point(486, 160);
			this.FormCancelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.FormCancelButton.Name = "FormCancelButton";
			this.FormCancelButton.Size = new System.Drawing.Size(112, 35);
			this.FormCancelButton.TabIndex = 8;
			this.FormCancelButton.Text = "Cancel";
			this.FormCancelButton.UseVisualStyleBackColor = true;
			this.FormCancelButton.Click += new System.EventHandler(this.FormCancelButton_Click);
			// 
			// OKButton
			// 
			this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OKButton.Location = new System.Drawing.Point(365, 160);
			this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(112, 35);
			this.OKButton.TabIndex = 7;
			this.OKButton.Text = "&OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// CityCheckbox
			// 
			this.CityCheckbox.AutoSize = true;
			this.CityCheckbox.Location = new System.Drawing.Point(165, 122);
			this.CityCheckbox.Name = "CityCheckbox";
			this.CityCheckbox.Size = new System.Drawing.Size(61, 24);
			this.CityCheckbox.TabIndex = 6;
			this.CityCheckbox.Text = "City";
			this.CityCheckbox.UseVisualStyleBackColor = true;
			// 
			// DialogMenu
			// 
			this.DialogMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.DialogMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DialogMenuHelp});
			this.DialogMenu.Name = "DialogMenu";
			this.DialogMenu.Size = new System.Drawing.Size(122, 34);
			// 
			// DialogMenuHelp
			// 
			this.DialogMenuHelp.Name = "DialogMenuHelp";
			this.DialogMenuHelp.Size = new System.Drawing.Size(240, 30);
			this.DialogMenuHelp.Text = "&Help";
			this.DialogMenuHelp.Click += new System.EventHandler(this.DialogMenuHelp_Click);
			// 
			// CityDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(611, 205);
			this.ContextMenuStrip = this.DialogMenu;
			this.Controls.Add(this.CityCheckbox);
			this.Controls.Add(this.DescriptionTextbox);
			this.Controls.Add(this.DescriptionLabel);
			this.Controls.Add(this.NameTextbox);
			this.Controls.Add(this.NameLabel);
			this.Controls.Add(this.ParentNameTextbox);
			this.Controls.Add(this.ParentNameLabel);
			this.Controls.Add(this.FormCancelButton);
			this.Controls.Add(this.OKButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CityDetail";
			this.Text = "City Detail";
			this.Load += new System.EventHandler(this.CityDetail_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CityDetail_KeyDown);
			this.DialogMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.TextBox DescriptionTextbox;
		private System.Windows.Forms.Label DescriptionLabel;
		private System.Windows.Forms.TextBox NameTextbox;
		private System.Windows.Forms.Label NameLabel;
		private System.Windows.Forms.TextBox ParentNameTextbox;
		private System.Windows.Forms.Label ParentNameLabel;
		private System.Windows.Forms.Button FormCancelButton;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.CheckBox CityCheckbox;
		private System.Windows.Forms.ContextMenuStrip DialogMenu;
		private System.Windows.Forms.ToolStripMenuItem DialogMenuHelp;
	}
}