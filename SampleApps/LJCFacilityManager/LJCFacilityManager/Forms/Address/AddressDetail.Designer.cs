namespace LJCFacilityManager
{
	partial class AddressDetail
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddressDetail));
			this.FormCancelButton = new System.Windows.Forms.Button();
			this.OKButton = new System.Windows.Forms.Button();
			this.TypeLabel = new System.Windows.Forms.Label();
			this.StreetTextbox = new System.Windows.Forms.TextBox();
			this.StreetLabel = new System.Windows.Forms.Label();
			this.PostalCodeTextbox = new System.Windows.Forms.TextBox();
			this.PostalCodeLabel = new System.Windows.Forms.Label();
			this.ParentNameTextbox = new System.Windows.Forms.TextBox();
			this.ParentNameLabel = new System.Windows.Forms.Label();
			this.CitySectionButton = new System.Windows.Forms.Button();
			this.ButtonImages = new System.Windows.Forms.ImageList(this.components);
			this.CitySectionTextbox = new System.Windows.Forms.TextBox();
			this.CitySectionLabel = new System.Windows.Forms.Label();
			this.CityTextbox = new System.Windows.Forms.TextBox();
			this.CityLabel = new System.Windows.Forms.Label();
			this.ProvinceTextbox = new System.Windows.Forms.TextBox();
			this.ProvinceLabel = new System.Windows.Forms.Label();
			this.TypeCombo = new LJCFacilityManager.CodeTypeCombo();
			this.DialogMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.DialogMenuHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.DialogMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// FormCancelButton
			// 
			this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.FormCancelButton.Location = new System.Drawing.Point(481, 273);
			this.FormCancelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.FormCancelButton.Name = "FormCancelButton";
			this.FormCancelButton.Size = new System.Drawing.Size(112, 35);
			this.FormCancelButton.TabIndex = 16;
			this.FormCancelButton.Text = "Cancel";
			this.FormCancelButton.UseVisualStyleBackColor = true;
			this.FormCancelButton.Click += new System.EventHandler(this.FormCancelButton_Click);
			// 
			// OKButton
			// 
			this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OKButton.Location = new System.Drawing.Point(360, 273);
			this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(112, 35);
			this.OKButton.TabIndex = 15;
			this.OKButton.Text = "&OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// TypeLabel
			// 
			this.TypeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.TypeLabel.Location = new System.Drawing.Point(18, 239);
			this.TypeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.TypeLabel.Name = "TypeLabel";
			this.TypeLabel.Size = new System.Drawing.Size(123, 20);
			this.TypeLabel.TabIndex = 13;
			this.TypeLabel.Text = "Address Type";
			// 
			// StreetTextbox
			// 
			this.StreetTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.StreetTextbox.Location = new System.Drawing.Point(143, 162);
			this.StreetTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.StreetTextbox.Name = "StreetTextbox";
			this.StreetTextbox.Size = new System.Drawing.Size(336, 26);
			this.StreetTextbox.TabIndex = 10;
			// 
			// StreetLabel
			// 
			this.StreetLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.StreetLabel.Location = new System.Drawing.Point(18, 167);
			this.StreetLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.StreetLabel.Name = "StreetLabel";
			this.StreetLabel.Size = new System.Drawing.Size(123, 20);
			this.StreetLabel.TabIndex = 9;
			this.StreetLabel.Text = "Street";
			// 
			// PostalCodeTextbox
			// 
			this.PostalCodeTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.PostalCodeTextbox.Location = new System.Drawing.Point(143, 198);
			this.PostalCodeTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.PostalCodeTextbox.Name = "PostalCodeTextbox";
			this.PostalCodeTextbox.Size = new System.Drawing.Size(336, 26);
			this.PostalCodeTextbox.TabIndex = 12;
			// 
			// PostalCodeLabel
			// 
			this.PostalCodeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.PostalCodeLabel.Location = new System.Drawing.Point(18, 203);
			this.PostalCodeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.PostalCodeLabel.Name = "PostalCodeLabel";
			this.PostalCodeLabel.Size = new System.Drawing.Size(123, 20);
			this.PostalCodeLabel.TabIndex = 11;
			this.PostalCodeLabel.Text = "Postal Code";
			// 
			// ParentNameTextbox
			// 
			this.ParentNameTextbox.Location = new System.Drawing.Point(143, 18);
			this.ParentNameTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ParentNameTextbox.Name = "ParentNameTextbox";
			this.ParentNameTextbox.ReadOnly = true;
			this.ParentNameTextbox.Size = new System.Drawing.Size(417, 26);
			this.ParentNameTextbox.TabIndex = 1;
			this.ParentNameTextbox.TabStop = false;
			// 
			// ParentNameLabel
			// 
			this.ParentNameLabel.Location = new System.Drawing.Point(18, 23);
			this.ParentNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ParentNameLabel.Name = "ParentNameLabel";
			this.ParentNameLabel.Size = new System.Drawing.Size(123, 20);
			this.ParentNameLabel.TabIndex = 0;
			this.ParentNameLabel.Text = "Addressee";
			// 
			// CitySectionButton
			// 
			this.CitySectionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.CitySectionButton.ImageKey = "Ellipse.bmp";
			this.CitySectionButton.ImageList = this.ButtonImages;
			this.CitySectionButton.Location = new System.Drawing.Point(566, 125);
			this.CitySectionButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.CitySectionButton.Name = "CitySectionButton";
			this.CitySectionButton.Size = new System.Drawing.Size(28, 28);
			this.CitySectionButton.TabIndex = 8;
			this.CitySectionButton.UseVisualStyleBackColor = true;
			this.CitySectionButton.Click += new System.EventHandler(this.CitySectionButton_Click);
			// 
			// ButtonImages
			// 
			this.ButtonImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ButtonImages.ImageStream")));
			this.ButtonImages.TransparentColor = System.Drawing.Color.Magenta;
			this.ButtonImages.Images.SetKeyName(0, "Ellipse.bmp");
			this.ButtonImages.Images.SetKeyName(1, "Calendar.bmp");
			// 
			// CitySectionTextbox
			// 
			this.CitySectionTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.CitySectionTextbox.Location = new System.Drawing.Point(143, 126);
			this.CitySectionTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.CitySectionTextbox.Name = "CitySectionTextbox";
			this.CitySectionTextbox.Size = new System.Drawing.Size(417, 26);
			this.CitySectionTextbox.TabIndex = 7;
			// 
			// CitySectionLabel
			// 
			this.CitySectionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.CitySectionLabel.Location = new System.Drawing.Point(18, 131);
			this.CitySectionLabel.Name = "CitySectionLabel";
			this.CitySectionLabel.Size = new System.Drawing.Size(123, 20);
			this.CitySectionLabel.TabIndex = 6;
			this.CitySectionLabel.Text = "Barangay";
			// 
			// CityTextbox
			// 
			this.CityTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.CityTextbox.Location = new System.Drawing.Point(143, 90);
			this.CityTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.CityTextbox.Name = "CityTextbox";
			this.CityTextbox.Size = new System.Drawing.Size(417, 26);
			this.CityTextbox.TabIndex = 5;
			// 
			// CityLabel
			// 
			this.CityLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.CityLabel.Location = new System.Drawing.Point(18, 95);
			this.CityLabel.Name = "CityLabel";
			this.CityLabel.Size = new System.Drawing.Size(123, 20);
			this.CityLabel.TabIndex = 4;
			this.CityLabel.Text = "City";
			// 
			// ProvinceTextbox
			// 
			this.ProvinceTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.ProvinceTextbox.Location = new System.Drawing.Point(143, 54);
			this.ProvinceTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ProvinceTextbox.MaxLength = 100;
			this.ProvinceTextbox.Name = "ProvinceTextbox";
			this.ProvinceTextbox.Size = new System.Drawing.Size(417, 26);
			this.ProvinceTextbox.TabIndex = 3;
			// 
			// ProvinceLabel
			// 
			this.ProvinceLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.ProvinceLabel.Location = new System.Drawing.Point(18, 59);
			this.ProvinceLabel.Name = "ProvinceLabel";
			this.ProvinceLabel.Size = new System.Drawing.Size(123, 20);
			this.ProvinceLabel.TabIndex = 2;
			this.ProvinceLabel.Text = "Province";
			// 
			// TypeCombo
			// 
			this.TypeCombo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.TypeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.TypeCombo.FormattingEnabled = true;
			this.TypeCombo.Location = new System.Drawing.Point(143, 234);
			this.TypeCombo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.TypeCombo.Name = "TypeCombo";
			this.TypeCombo.Size = new System.Drawing.Size(416, 28);
			this.TypeCombo.TabIndex = 14;
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
			this.DialogMenuHelp.Size = new System.Drawing.Size(121, 30);
			this.DialogMenuHelp.Text = "&Help";
			this.DialogMenuHelp.Click += new System.EventHandler(this.DialogMenuHelp_Click);
			// 
			// AddressDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(601, 319);
			this.ContextMenuStrip = this.DialogMenu;
			this.Controls.Add(this.CitySectionButton);
			this.Controls.Add(this.CitySectionTextbox);
			this.Controls.Add(this.CitySectionLabel);
			this.Controls.Add(this.CityTextbox);
			this.Controls.Add(this.CityLabel);
			this.Controls.Add(this.ProvinceTextbox);
			this.Controls.Add(this.ProvinceLabel);
			this.Controls.Add(this.ParentNameTextbox);
			this.Controls.Add(this.ParentNameLabel);
			this.Controls.Add(this.PostalCodeTextbox);
			this.Controls.Add(this.PostalCodeLabel);
			this.Controls.Add(this.FormCancelButton);
			this.Controls.Add(this.OKButton);
			this.Controls.Add(this.TypeLabel);
			this.Controls.Add(this.TypeCombo);
			this.Controls.Add(this.StreetTextbox);
			this.Controls.Add(this.StreetLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AddressDetail";
			this.ShowInTaskbar = false;
			this.Text = "Address Detail";
			this.Load += new System.EventHandler(this.AddressDetail_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AddressDetail_KeyDown);
			this.DialogMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button FormCancelButton;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.Label TypeLabel;
		private CodeTypeCombo TypeCombo;
		private System.Windows.Forms.TextBox StreetTextbox;
		private System.Windows.Forms.Label StreetLabel;
		private System.Windows.Forms.TextBox PostalCodeTextbox;
		private System.Windows.Forms.Label PostalCodeLabel;
		private System.Windows.Forms.TextBox ParentNameTextbox;
		private System.Windows.Forms.Label ParentNameLabel;
		private System.Windows.Forms.Button CitySectionButton;
		private System.Windows.Forms.TextBox CitySectionTextbox;
		private System.Windows.Forms.Label CitySectionLabel;
		private System.Windows.Forms.TextBox CityTextbox;
		private System.Windows.Forms.Label CityLabel;
		private System.Windows.Forms.TextBox ProvinceTextbox;
		private System.Windows.Forms.Label ProvinceLabel;
		private System.Windows.Forms.ImageList ButtonImages;
		private System.Windows.Forms.ContextMenuStrip DialogMenu;
		private System.Windows.Forms.ToolStripMenuItem DialogMenuHelp;
	}
}