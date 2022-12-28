namespace CVRManager
{
	partial class CVPersonDetail
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CVPersonDetail));
			this.RegionLabel = new System.Windows.Forms.Label();
			this.FirstNameTextbox = new System.Windows.Forms.TextBox();
			this.FirstNameLabel = new System.Windows.Forms.Label();
			this.LastNameLabel = new System.Windows.Forms.Label();
			this.MiddleNameLabel = new System.Windows.Forms.Label();
			this.FormCancelButton = new System.Windows.Forms.Button();
			this.OKButton = new System.Windows.Forms.Button();
			this.RegionTextbox = new System.Windows.Forms.TextBox();
			this.RegionButton = new System.Windows.Forms.Button();
			this.ButtonImages = new System.Windows.Forms.ImageList(this.components);
			this.MiddleNameTextbox = new System.Windows.Forms.TextBox();
			this.LastNameTextbox = new System.Windows.Forms.TextBox();
			this.AddressTextbox = new System.Windows.Forms.TextBox();
			this.AddressLabel = new System.Windows.Forms.Label();
			this.SexCombo = new LJCWinFormControls.LJCItemCombo();
			this.SexLabel = new System.Windows.Forms.Label();
			this.PhoneTextbox = new System.Windows.Forms.TextBox();
			this.PhoneLabel = new System.Windows.Forms.Label();
			this.DialogMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.DialogMenuHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.DialogMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// RegionLabel
			// 
			this.RegionLabel.Location = new System.Drawing.Point(15, 219);
			this.RegionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.RegionLabel.Name = "RegionLabel";
			this.RegionLabel.Size = new System.Drawing.Size(141, 25);
			this.RegionLabel.TabIndex = 10;
			this.RegionLabel.Text = "City, Province  Zip";
			// 
			// FirstNameTextbox
			// 
			this.FirstNameTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FirstNameTextbox.Location = new System.Drawing.Point(159, 14);
			this.FirstNameTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.FirstNameTextbox.Name = "FirstNameTextbox";
			this.FirstNameTextbox.Size = new System.Drawing.Size(203, 30);
			this.FirstNameTextbox.TabIndex = 1;
			// 
			// FirstNameLabel
			// 
			this.FirstNameLabel.Location = new System.Drawing.Point(15, 17);
			this.FirstNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.FirstNameLabel.Name = "FirstNameLabel";
			this.FirstNameLabel.Size = new System.Drawing.Size(141, 25);
			this.FirstNameLabel.TabIndex = 0;
			this.FirstNameLabel.Text = "First Name";
			// 
			// LastNameLabel
			// 
			this.LastNameLabel.Location = new System.Drawing.Point(15, 97);
			this.LastNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.LastNameLabel.Name = "LastNameLabel";
			this.LastNameLabel.Size = new System.Drawing.Size(141, 25);
			this.LastNameLabel.TabIndex = 4;
			this.LastNameLabel.Text = "Last Name";
			// 
			// MiddleNameLabel
			// 
			this.MiddleNameLabel.Location = new System.Drawing.Point(15, 57);
			this.MiddleNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.MiddleNameLabel.Name = "MiddleNameLabel";
			this.MiddleNameLabel.Size = new System.Drawing.Size(141, 25);
			this.MiddleNameLabel.TabIndex = 2;
			this.MiddleNameLabel.Text = "Middle Name";
			// 
			// FormCancelButton
			// 
			this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.FormCancelButton.Location = new System.Drawing.Point(480, 296);
			this.FormCancelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.FormCancelButton.Name = "FormCancelButton";
			this.FormCancelButton.Size = new System.Drawing.Size(112, 35);
			this.FormCancelButton.TabIndex = 16;
			this.FormCancelButton.Text = "Cancel";
			this.FormCancelButton.UseVisualStyleBackColor = true;
			// 
			// OKButton
			// 
			this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OKButton.Location = new System.Drawing.Point(360, 296);
			this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(112, 35);
			this.OKButton.TabIndex = 15;
			this.OKButton.Text = "&OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// RegionTextbox
			// 
			this.RegionTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.RegionTextbox.Location = new System.Drawing.Point(159, 216);
			this.RegionTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.RegionTextbox.Name = "RegionTextbox";
			this.RegionTextbox.Size = new System.Drawing.Size(397, 30);
			this.RegionTextbox.TabIndex = 11;
			// 
			// RegionButton
			// 
			this.RegionButton.ImageKey = "Ellipse.bmp";
			this.RegionButton.ImageList = this.ButtonImages;
			this.RegionButton.Location = new System.Drawing.Point(563, 216);
			this.RegionButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.RegionButton.Name = "RegionButton";
			this.RegionButton.Size = new System.Drawing.Size(30, 30);
			this.RegionButton.TabIndex = 12;
			this.RegionButton.UseVisualStyleBackColor = true;
			this.RegionButton.Click += new System.EventHandler(this.RegionButton_Click);
			// 
			// ButtonImages
			// 
			this.ButtonImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ButtonImages.ImageStream")));
			this.ButtonImages.TransparentColor = System.Drawing.Color.Magenta;
			this.ButtonImages.Images.SetKeyName(0, "Ellipse.bmp");
			this.ButtonImages.Images.SetKeyName(1, "Calendar.bmp");
			// 
			// MiddleNameTextbox
			// 
			this.MiddleNameTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MiddleNameTextbox.Location = new System.Drawing.Point(159, 54);
			this.MiddleNameTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MiddleNameTextbox.Name = "MiddleNameTextbox";
			this.MiddleNameTextbox.Size = new System.Drawing.Size(203, 30);
			this.MiddleNameTextbox.TabIndex = 3;
			// 
			// LastNameTextbox
			// 
			this.LastNameTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.LastNameTextbox.Location = new System.Drawing.Point(159, 94);
			this.LastNameTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.LastNameTextbox.Name = "LastNameTextbox";
			this.LastNameTextbox.Size = new System.Drawing.Size(203, 30);
			this.LastNameTextbox.TabIndex = 5;
			// 
			// AddressTextbox
			// 
			this.AddressTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.AddressTextbox.Location = new System.Drawing.Point(159, 176);
			this.AddressTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.AddressTextbox.Name = "AddressTextbox";
			this.AddressTextbox.Size = new System.Drawing.Size(397, 30);
			this.AddressTextbox.TabIndex = 9;
			// 
			// AddressLabel
			// 
			this.AddressLabel.Location = new System.Drawing.Point(15, 179);
			this.AddressLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.AddressLabel.Name = "AddressLabel";
			this.AddressLabel.Size = new System.Drawing.Size(141, 25);
			this.AddressLabel.TabIndex = 8;
			this.AddressLabel.Text = "Address";
			// 
			// SexCombo
			// 
			this.SexCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SexCombo.FormattingEnabled = true;
			this.SexCombo.Location = new System.Drawing.Point(159, 134);
			this.SexCombo.Name = "SexCombo";
			this.SexCombo.Size = new System.Drawing.Size(203, 33);
			this.SexCombo.TabIndex = 7;
			// 
			// SexLabel
			// 
			this.SexLabel.Location = new System.Drawing.Point(15, 137);
			this.SexLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.SexLabel.Name = "SexLabel";
			this.SexLabel.Size = new System.Drawing.Size(141, 26);
			this.SexLabel.TabIndex = 6;
			this.SexLabel.Text = "Sex";
			// 
			// PhoneTextbox
			// 
			this.PhoneTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.PhoneTextbox.Location = new System.Drawing.Point(159, 256);
			this.PhoneTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.PhoneTextbox.Name = "PhoneTextbox";
			this.PhoneTextbox.Size = new System.Drawing.Size(160, 30);
			this.PhoneTextbox.TabIndex = 14;
			// 
			// PhoneLabel
			// 
			this.PhoneLabel.Location = new System.Drawing.Point(15, 259);
			this.PhoneLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.PhoneLabel.Name = "PhoneLabel";
			this.PhoneLabel.Size = new System.Drawing.Size(141, 25);
			this.PhoneLabel.TabIndex = 13;
			this.PhoneLabel.Text = "Phone Number";
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
			// CVPersonDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(605, 341);
			this.ContextMenuStrip = this.DialogMenu;
			this.Controls.Add(this.PhoneTextbox);
			this.Controls.Add(this.PhoneLabel);
			this.Controls.Add(this.SexLabel);
			this.Controls.Add(this.SexCombo);
			this.Controls.Add(this.AddressLabel);
			this.Controls.Add(this.AddressTextbox);
			this.Controls.Add(this.LastNameTextbox);
			this.Controls.Add(this.MiddleNameTextbox);
			this.Controls.Add(this.RegionButton);
			this.Controls.Add(this.RegionTextbox);
			this.Controls.Add(this.RegionLabel);
			this.Controls.Add(this.FirstNameTextbox);
			this.Controls.Add(this.FirstNameLabel);
			this.Controls.Add(this.LastNameLabel);
			this.Controls.Add(this.MiddleNameLabel);
			this.Controls.Add(this.FormCancelButton);
			this.Controls.Add(this.OKButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CVPersonDetail";
			this.ShowInTaskbar = false;
			this.Text = "Person Detail";
			this.Load += new System.EventHandler(this.CVPersonDetail_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CVPersonDetail_KeyDown);
			this.DialogMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label RegionLabel;
		private System.Windows.Forms.TextBox FirstNameTextbox;
		private System.Windows.Forms.Label FirstNameLabel;
		private System.Windows.Forms.Label LastNameLabel;
		private System.Windows.Forms.Label MiddleNameLabel;
		private System.Windows.Forms.Button FormCancelButton;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.TextBox RegionTextbox;
		private System.Windows.Forms.Button RegionButton;
		private System.Windows.Forms.TextBox MiddleNameTextbox;
		private System.Windows.Forms.TextBox LastNameTextbox;
		private System.Windows.Forms.TextBox AddressTextbox;
		private System.Windows.Forms.Label AddressLabel;
		private LJCWinFormControls.LJCItemCombo SexCombo;
		private System.Windows.Forms.Label SexLabel;
		private System.Windows.Forms.TextBox PhoneTextbox;
		private System.Windows.Forms.Label PhoneLabel;
		private System.Windows.Forms.ImageList ButtonImages;
		private System.Windows.Forms.ContextMenuStrip DialogMenu;
		private System.Windows.Forms.ToolStripMenuItem DialogMenuHelp;
	}
}