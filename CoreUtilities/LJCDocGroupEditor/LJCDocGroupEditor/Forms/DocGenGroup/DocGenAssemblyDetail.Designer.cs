namespace LJCDocGroupEditor
{
	partial class DocGenAssemblyDetail
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocGenAssemblyDetail));
			this.DescriptionTextbox = new System.Windows.Forms.TextBox();
			this.DescriptionLabel = new System.Windows.Forms.Label();
			this.NameTextbox = new System.Windows.Forms.TextBox();
			this.NameLabel = new System.Windows.Forms.Label();
			this.FormCancelButton = new System.Windows.Forms.Button();
			this.OKButton = new System.Windows.Forms.Button();
			this.ImageFileButton = new System.Windows.Forms.Button();
			this.ButtonImages = new System.Windows.Forms.ImageList(this.components);
			this.XmlFileTextbox = new System.Windows.Forms.TextBox();
			this.XmlFileLabel = new System.Windows.Forms.Label();
			this.ImageFileTextbox = new System.Windows.Forms.TextBox();
			this.ImageFileLabel = new System.Windows.Forms.Label();
			this.XmlFileButton = new System.Windows.Forms.Button();
			this.ParentNameTextbox = new System.Windows.Forms.TextBox();
			this.ParentNameLabel = new System.Windows.Forms.Label();
			this.SequenceTextbox = new System.Windows.Forms.TextBox();
			this.SequenceLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// DescriptionTextbox
			// 
			this.DescriptionTextbox.Location = new System.Drawing.Point(149, 83);
			this.DescriptionTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.DescriptionTextbox.Name = "DescriptionTextbox";
			this.DescriptionTextbox.Size = new System.Drawing.Size(433, 26);
			this.DescriptionTextbox.TabIndex = 5;
			// 
			// DescriptionLabel
			// 
			this.DescriptionLabel.Location = new System.Drawing.Point(15, 88);
			this.DescriptionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.DescriptionLabel.Name = "DescriptionLabel";
			this.DescriptionLabel.Size = new System.Drawing.Size(131, 20);
			this.DescriptionLabel.TabIndex = 4;
			this.DescriptionLabel.Text = "Description";
			// 
			// NameTextbox
			// 
			this.NameTextbox.Location = new System.Drawing.Point(149, 47);
			this.NameTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.NameTextbox.Name = "NameTextbox";
			this.NameTextbox.Size = new System.Drawing.Size(433, 26);
			this.NameTextbox.TabIndex = 3;
			this.NameTextbox.TextChanged += new System.EventHandler(this.NameTextbox_TextChanged);
			this.NameTextbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NameTextbox_KeyPress);
			// 
			// NameLabel
			// 
			this.NameLabel.Location = new System.Drawing.Point(15, 52);
			this.NameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.NameLabel.Name = "NameLabel";
			this.NameLabel.Size = new System.Drawing.Size(131, 20);
			this.NameLabel.TabIndex = 2;
			this.NameLabel.Text = "Name";
			// 
			// FormCancelButton
			// 
			this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.FormCancelButton.Location = new System.Drawing.Point(469, 227);
			this.FormCancelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.FormCancelButton.Name = "FormCancelButton";
			this.FormCancelButton.Size = new System.Drawing.Size(112, 35);
			this.FormCancelButton.TabIndex = 15;
			this.FormCancelButton.Text = "Cancel";
			this.FormCancelButton.UseVisualStyleBackColor = true;
			this.FormCancelButton.Click += new System.EventHandler(this.FormCancelButton_Click);
			// 
			// OKButton
			// 
			this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OKButton.Location = new System.Drawing.Point(348, 227);
			this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(112, 35);
			this.OKButton.TabIndex = 14;
			this.OKButton.Text = "&OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// ImageFileButton
			// 
			this.ImageFileButton.ImageKey = "Ellipse.bmp";
			this.ImageFileButton.ImageList = this.ButtonImages;
			this.ImageFileButton.Location = new System.Drawing.Point(554, 154);
			this.ImageFileButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ImageFileButton.Name = "ImageFileButton";
			this.ImageFileButton.Size = new System.Drawing.Size(28, 28);
			this.ImageFileButton.TabIndex = 11;
			this.ImageFileButton.UseVisualStyleBackColor = true;
			this.ImageFileButton.Click += new System.EventHandler(this.ImageFileButton_Click);
			// 
			// ButtonImages
			// 
			this.ButtonImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ButtonImages.ImageStream")));
			this.ButtonImages.TransparentColor = System.Drawing.Color.Magenta;
			this.ButtonImages.Images.SetKeyName(0, "Ellipse.bmp");
			this.ButtonImages.Images.SetKeyName(1, "Calendar.bmp");
			// 
			// XmlFileTextbox
			// 
			this.XmlFileTextbox.Location = new System.Drawing.Point(149, 119);
			this.XmlFileTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.XmlFileTextbox.Name = "XmlFileTextbox";
			this.XmlFileTextbox.Size = new System.Drawing.Size(397, 26);
			this.XmlFileTextbox.TabIndex = 7;
			this.XmlFileTextbox.TabStop = false;
			// 
			// XmlFileLabel
			// 
			this.XmlFileLabel.Location = new System.Drawing.Point(15, 124);
			this.XmlFileLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.XmlFileLabel.Name = "XmlFileLabel";
			this.XmlFileLabel.Size = new System.Drawing.Size(131, 20);
			this.XmlFileLabel.TabIndex = 6;
			this.XmlFileLabel.Text = "XML File Path";
			// 
			// ImageFileTextbox
			// 
			this.ImageFileTextbox.Location = new System.Drawing.Point(149, 155);
			this.ImageFileTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ImageFileTextbox.Name = "ImageFileTextbox";
			this.ImageFileTextbox.Size = new System.Drawing.Size(397, 26);
			this.ImageFileTextbox.TabIndex = 10;
			this.ImageFileTextbox.TabStop = false;
			// 
			// ImageFileLabel
			// 
			this.ImageFileLabel.Location = new System.Drawing.Point(15, 160);
			this.ImageFileLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ImageFileLabel.Name = "ImageFileLabel";
			this.ImageFileLabel.Size = new System.Drawing.Size(131, 20);
			this.ImageFileLabel.TabIndex = 9;
			this.ImageFileLabel.Text = "Image File Path";
			// 
			// XmlFileButton
			// 
			this.XmlFileButton.ImageKey = "Ellipse.bmp";
			this.XmlFileButton.ImageList = this.ButtonImages;
			this.XmlFileButton.Location = new System.Drawing.Point(554, 118);
			this.XmlFileButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.XmlFileButton.Name = "XmlFileButton";
			this.XmlFileButton.Size = new System.Drawing.Size(28, 28);
			this.XmlFileButton.TabIndex = 8;
			this.XmlFileButton.UseVisualStyleBackColor = true;
			this.XmlFileButton.Click += new System.EventHandler(this.XmlFileButton_Click);
			// 
			// ParentNameTextbox
			// 
			this.ParentNameTextbox.Location = new System.Drawing.Point(149, 14);
			this.ParentNameTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ParentNameTextbox.Name = "ParentNameTextbox";
			this.ParentNameTextbox.ReadOnly = true;
			this.ParentNameTextbox.Size = new System.Drawing.Size(433, 26);
			this.ParentNameTextbox.TabIndex = 1;
			this.ParentNameTextbox.TabStop = false;
			// 
			// ParentNameLabel
			// 
			this.ParentNameLabel.Location = new System.Drawing.Point(15, 19);
			this.ParentNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ParentNameLabel.Name = "ParentNameLabel";
			this.ParentNameLabel.Size = new System.Drawing.Size(131, 20);
			this.ParentNameLabel.TabIndex = 0;
			this.ParentNameLabel.Text = "Group Name";
			// 
			// SequenceTextbox
			// 
			this.SequenceTextbox.Location = new System.Drawing.Point(149, 191);
			this.SequenceTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.SequenceTextbox.Name = "SequenceTextbox";
			this.SequenceTextbox.Size = new System.Drawing.Size(54, 26);
			this.SequenceTextbox.TabIndex = 13;
			this.SequenceTextbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SequenceTextbox_KeyPress);
			// 
			// SequenceLabel
			// 
			this.SequenceLabel.Location = new System.Drawing.Point(15, 196);
			this.SequenceLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.SequenceLabel.Name = "SequenceLabel";
			this.SequenceLabel.Size = new System.Drawing.Size(131, 20);
			this.SequenceLabel.TabIndex = 12;
			this.SequenceLabel.Text = "Sequence";
			// 
			// DocGenAssemblyDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(599, 273);
			this.Controls.Add(this.SequenceTextbox);
			this.Controls.Add(this.SequenceLabel);
			this.Controls.Add(this.ParentNameTextbox);
			this.Controls.Add(this.ParentNameLabel);
			this.Controls.Add(this.XmlFileButton);
			this.Controls.Add(this.ImageFileTextbox);
			this.Controls.Add(this.ImageFileLabel);
			this.Controls.Add(this.XmlFileTextbox);
			this.Controls.Add(this.XmlFileLabel);
			this.Controls.Add(this.ImageFileButton);
			this.Controls.Add(this.DescriptionTextbox);
			this.Controls.Add(this.DescriptionLabel);
			this.Controls.Add(this.NameTextbox);
			this.Controls.Add(this.NameLabel);
			this.Controls.Add(this.FormCancelButton);
			this.Controls.Add(this.OKButton);
			this.Name = "DocGenAssemblyDetail";
			this.Text = "DocAssembly Detail";
			this.Load += new System.EventHandler(this.DocGenAssemblyDetail_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DocGenAssemblyDetail_KeyDown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.TextBox DescriptionTextbox;
		private System.Windows.Forms.Label DescriptionLabel;
		private System.Windows.Forms.TextBox NameTextbox;
		private System.Windows.Forms.Label NameLabel;
		private System.Windows.Forms.Button FormCancelButton;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.Button ImageFileButton;
		private System.Windows.Forms.TextBox XmlFileTextbox;
		private System.Windows.Forms.Label XmlFileLabel;
		private System.Windows.Forms.TextBox ImageFileTextbox;
		private System.Windows.Forms.Label ImageFileLabel;
		private System.Windows.Forms.Button XmlFileButton;
		private System.Windows.Forms.ImageList ButtonImages;
		private System.Windows.Forms.TextBox ParentNameTextbox;
		private System.Windows.Forms.Label ParentNameLabel;
		private System.Windows.Forms.TextBox SequenceTextbox;
		private System.Windows.Forms.Label SequenceLabel;
	}
}