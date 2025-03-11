namespace LJCTransformManager
{
	partial class DataSourceDetail
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataSourceDetail));
			this.DataConfigNameTextbox = new System.Windows.Forms.TextBox();
			this.DataConfigNameLabel = new System.Windows.Forms.Label();
			this.SourceTypeCombo = new LJCWinFormControls.LJCItemCombo();
			this.SourceTypeLabel = new System.Windows.Forms.Label();
			this.SourceStatusCombo = new System.Windows.Forms.ComboBox();
			this.SourceStatusLabel = new System.Windows.Forms.Label();
			this.ParentNameTextbox = new System.Windows.Forms.TextBox();
			this.ParentNameLabel = new System.Windows.Forms.Label();
			this.SourceDescriptionTextbox = new System.Windows.Forms.TextBox();
			this.SourceDescriptionLabel = new System.Windows.Forms.Label();
			this.SourceNameTextbox = new System.Windows.Forms.TextBox();
			this.SourceNameLabel = new System.Windows.Forms.Label();
			this.FormCancelButton = new System.Windows.Forms.Button();
			this.OKButton = new System.Windows.Forms.Button();
			this.SourceItemNameTextbox = new System.Windows.Forms.TextBox();
			this.SourceItemNameLabel = new System.Windows.Forms.Label();
			this.LayoutCombo = new LJCWinFormControls.LJCItemCombo();
			this.LayoutLabel = new System.Windows.Forms.Label();
			this.SourceItemButton = new System.Windows.Forms.Button();
			this.ButtonImages = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// DataConfigNameTextbox
			// 
			this.DataConfigNameTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.DataConfigNameTextbox.Location = new System.Drawing.Point(167, 192);
			this.DataConfigNameTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.DataConfigNameTextbox.Name = "DataConfigNameTextbox";
			this.DataConfigNameTextbox.Size = new System.Drawing.Size(417, 26);
			this.DataConfigNameTextbox.TabIndex = 11;
			this.DataConfigNameTextbox.TextChanged += new System.EventHandler(this.ActionItemTextbox_TextChanged);
			this.DataConfigNameTextbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ActionItemTextbox_KeyPress);
			// 
			// DataConfigNameLabel
			// 
			this.DataConfigNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.DataConfigNameLabel.Location = new System.Drawing.Point(13, 195);
			this.DataConfigNameLabel.Name = "DataConfigNameLabel";
			this.DataConfigNameLabel.Size = new System.Drawing.Size(147, 20);
			this.DataConfigNameLabel.TabIndex = 10;
			this.DataConfigNameLabel.Text = "Data Config Name";
			// 
			// SourceTypeCombo
			// 
			this.SourceTypeCombo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.SourceTypeCombo.FormattingEnabled = true;
			this.SourceTypeCombo.Location = new System.Drawing.Point(167, 156);
			this.SourceTypeCombo.Name = "SourceTypeCombo";
			this.SourceTypeCombo.Size = new System.Drawing.Size(243, 28);
			this.SourceTypeCombo.TabIndex = 9;
			this.SourceTypeCombo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TaskTypeCombo_KeyDown);
			this.SourceTypeCombo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TaskTypeCombo_KeyPress);
			// 
			// SourceTypeLabel
			// 
			this.SourceTypeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.SourceTypeLabel.Location = new System.Drawing.Point(12, 160);
			this.SourceTypeLabel.Name = "SourceTypeLabel";
			this.SourceTypeLabel.Size = new System.Drawing.Size(123, 20);
			this.SourceTypeLabel.TabIndex = 8;
			this.SourceTypeLabel.Text = "Source Type";
			// 
			// SourceStatusCombo
			// 
			this.SourceStatusCombo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.SourceStatusCombo.FormattingEnabled = true;
			this.SourceStatusCombo.Location = new System.Drawing.Point(167, 262);
			this.SourceStatusCombo.Name = "SourceStatusCombo";
			this.SourceStatusCombo.Size = new System.Drawing.Size(243, 28);
			this.SourceStatusCombo.TabIndex = 16;
			this.SourceStatusCombo.SelectedIndexChanged += new System.EventHandler(this.StatusCombo_SelectedIndexChanged);
			this.SourceStatusCombo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.StatusCombo_KeyDown);
			this.SourceStatusCombo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StatusCombo_KeyPress);
			// 
			// SourceStatusLabel
			// 
			this.SourceStatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.SourceStatusLabel.Location = new System.Drawing.Point(13, 265);
			this.SourceStatusLabel.Name = "SourceStatusLabel";
			this.SourceStatusLabel.Size = new System.Drawing.Size(123, 20);
			this.SourceStatusLabel.TabIndex = 15;
			this.SourceStatusLabel.Text = "Source Status";
			// 
			// ParentNameTextbox
			// 
			this.ParentNameTextbox.Location = new System.Drawing.Point(167, 14);
			this.ParentNameTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ParentNameTextbox.MaxLength = 100;
			this.ParentNameTextbox.Name = "ParentNameTextbox";
			this.ParentNameTextbox.ReadOnly = true;
			this.ParentNameTextbox.Size = new System.Drawing.Size(417, 26);
			this.ParentNameTextbox.TabIndex = 1;
			this.ParentNameTextbox.TabStop = false;
			// 
			// ParentNameLabel
			// 
			this.ParentNameLabel.Location = new System.Drawing.Point(12, 17);
			this.ParentNameLabel.Name = "ParentNameLabel";
			this.ParentNameLabel.Size = new System.Drawing.Size(123, 20);
			this.ParentNameLabel.TabIndex = 0;
			this.ParentNameLabel.Text = "Transform";
			// 
			// SourceDescriptionTextbox
			// 
			this.SourceDescriptionTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.SourceDescriptionTextbox.Location = new System.Drawing.Point(167, 86);
			this.SourceDescriptionTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.SourceDescriptionTextbox.Name = "SourceDescriptionTextbox";
			this.SourceDescriptionTextbox.Size = new System.Drawing.Size(417, 26);
			this.SourceDescriptionTextbox.TabIndex = 5;
			// 
			// SourceDescriptionLabel
			// 
			this.SourceDescriptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.SourceDescriptionLabel.Location = new System.Drawing.Point(13, 89);
			this.SourceDescriptionLabel.Name = "SourceDescriptionLabel";
			this.SourceDescriptionLabel.Size = new System.Drawing.Size(123, 20);
			this.SourceDescriptionLabel.TabIndex = 4;
			this.SourceDescriptionLabel.Text = "Description";
			// 
			// SourceNameTextbox
			// 
			this.SourceNameTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.SourceNameTextbox.Location = new System.Drawing.Point(167, 50);
			this.SourceNameTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.SourceNameTextbox.MaxLength = 100;
			this.SourceNameTextbox.Name = "SourceNameTextbox";
			this.SourceNameTextbox.Size = new System.Drawing.Size(417, 26);
			this.SourceNameTextbox.TabIndex = 3;
			this.SourceNameTextbox.TextChanged += new System.EventHandler(this.TaskNameTextbox_TextChanged);
			this.SourceNameTextbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TaskNameTextbox_KeyPress);
			// 
			// SourceNameLabel
			// 
			this.SourceNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.SourceNameLabel.Location = new System.Drawing.Point(13, 53);
			this.SourceNameLabel.Name = "SourceNameLabel";
			this.SourceNameLabel.Size = new System.Drawing.Size(123, 20);
			this.SourceNameLabel.TabIndex = 2;
			this.SourceNameLabel.Text = "Data Source";
			// 
			// FormCancelButton
			// 
			this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.FormCancelButton.Location = new System.Drawing.Point(473, 301);
			this.FormCancelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.FormCancelButton.Name = "FormCancelButton";
			this.FormCancelButton.Size = new System.Drawing.Size(112, 35);
			this.FormCancelButton.TabIndex = 18;
			this.FormCancelButton.Text = "Cancel";
			this.FormCancelButton.UseVisualStyleBackColor = true;
			this.FormCancelButton.Click += new System.EventHandler(this.FormCancelButton_Click);
			// 
			// OKButton
			// 
			this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OKButton.Location = new System.Drawing.Point(353, 301);
			this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(112, 35);
			this.OKButton.TabIndex = 17;
			this.OKButton.Text = "&OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// SourceItemNameTextbox
			// 
			this.SourceItemNameTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.SourceItemNameTextbox.Location = new System.Drawing.Point(167, 228);
			this.SourceItemNameTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.SourceItemNameTextbox.Name = "SourceItemNameTextbox";
			this.SourceItemNameTextbox.Size = new System.Drawing.Size(381, 26);
			this.SourceItemNameTextbox.TabIndex = 13;
			this.SourceItemNameTextbox.TextChanged += new System.EventHandler(this.ItemNameTextbox_TextChanged);
			this.SourceItemNameTextbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ItemNameTextbox_KeyPress);
			// 
			// SourceItemNameLabel
			// 
			this.SourceItemNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.SourceItemNameLabel.Location = new System.Drawing.Point(13, 231);
			this.SourceItemNameLabel.Name = "SourceItemNameLabel";
			this.SourceItemNameLabel.Size = new System.Drawing.Size(147, 20);
			this.SourceItemNameLabel.TabIndex = 12;
			this.SourceItemNameLabel.Text = "Source Item Name";
			// 
			// LayoutCombo
			// 
			this.LayoutCombo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.LayoutCombo.FormattingEnabled = true;
			this.LayoutCombo.Location = new System.Drawing.Point(167, 120);
			this.LayoutCombo.Name = "LayoutCombo";
			this.LayoutCombo.Size = new System.Drawing.Size(417, 28);
			this.LayoutCombo.TabIndex = 7;
			// 
			// LayoutLabel
			// 
			this.LayoutLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.LayoutLabel.Location = new System.Drawing.Point(13, 123);
			this.LayoutLabel.Name = "LayoutLabel";
			this.LayoutLabel.Size = new System.Drawing.Size(123, 20);
			this.LayoutLabel.TabIndex = 6;
			this.LayoutLabel.Text = "Layout";
			// 
			// SourceItemButton
			// 
			this.SourceItemButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.SourceItemButton.ImageKey = "Ellipse.bmp";
			this.SourceItemButton.ImageList = this.ButtonImages;
			this.SourceItemButton.Location = new System.Drawing.Point(556, 227);
			this.SourceItemButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.SourceItemButton.Name = "SourceItemButton";
			this.SourceItemButton.Size = new System.Drawing.Size(28, 28);
			this.SourceItemButton.TabIndex = 14;
			this.SourceItemButton.UseVisualStyleBackColor = true;
			this.SourceItemButton.Click += new System.EventHandler(this.SourceItemButton_Click);
			// 
			// ButtonImages
			// 
			this.ButtonImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ButtonImages.ImageStream")));
			this.ButtonImages.TransparentColor = System.Drawing.Color.Magenta;
			this.ButtonImages.Images.SetKeyName(0, "Ellipse.bmp");
			this.ButtonImages.Images.SetKeyName(1, "Calendar.bmp");
			// 
			// DataSourceDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(593, 349);
			this.Controls.Add(this.SourceItemButton);
			this.Controls.Add(this.LayoutCombo);
			this.Controls.Add(this.LayoutLabel);
			this.Controls.Add(this.SourceItemNameTextbox);
			this.Controls.Add(this.SourceItemNameLabel);
			this.Controls.Add(this.DataConfigNameTextbox);
			this.Controls.Add(this.DataConfigNameLabel);
			this.Controls.Add(this.SourceTypeCombo);
			this.Controls.Add(this.SourceTypeLabel);
			this.Controls.Add(this.SourceStatusCombo);
			this.Controls.Add(this.SourceStatusLabel);
			this.Controls.Add(this.ParentNameTextbox);
			this.Controls.Add(this.ParentNameLabel);
			this.Controls.Add(this.SourceDescriptionTextbox);
			this.Controls.Add(this.SourceDescriptionLabel);
			this.Controls.Add(this.SourceNameTextbox);
			this.Controls.Add(this.SourceNameLabel);
			this.Controls.Add(this.FormCancelButton);
			this.Controls.Add(this.OKButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DataSourceDetail";
			this.ShowInTaskbar = false;
			this.Text = "Data Source Detail";
			this.Load += new System.EventHandler(this.DataSourceDetail_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox DataConfigNameTextbox;
		private System.Windows.Forms.Label DataConfigNameLabel;
		private LJCWinFormControls.LJCItemCombo SourceTypeCombo;
		private System.Windows.Forms.Label SourceTypeLabel;
		private System.Windows.Forms.ComboBox SourceStatusCombo;
		private System.Windows.Forms.Label SourceStatusLabel;
		private System.Windows.Forms.TextBox ParentNameTextbox;
		private System.Windows.Forms.Label ParentNameLabel;
		private System.Windows.Forms.TextBox SourceDescriptionTextbox;
		private System.Windows.Forms.Label SourceDescriptionLabel;
		private System.Windows.Forms.TextBox SourceNameTextbox;
		private System.Windows.Forms.Label SourceNameLabel;
		private System.Windows.Forms.Button FormCancelButton;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.TextBox SourceItemNameTextbox;
		private System.Windows.Forms.Label SourceItemNameLabel;
		private LJCWinFormControls.LJCItemCombo LayoutCombo;
		private System.Windows.Forms.Label LayoutLabel;
		private System.Windows.Forms.Button SourceItemButton;
		private System.Windows.Forms.ImageList ButtonImages;
	}
}