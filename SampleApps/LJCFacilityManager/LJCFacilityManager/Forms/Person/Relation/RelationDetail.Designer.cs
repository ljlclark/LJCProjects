namespace LJCFacilityManager
{
	partial class RelationDetail
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RelationDetail));
			this.FormCancelButton = new System.Windows.Forms.Button();
			this.OKButton = new System.Windows.Forms.Button();
			this.TypeLabel = new System.Windows.Forms.Label();
			this.RelationTextbox = new System.Windows.Forms.TextBox();
			this.RelationLabel = new System.Windows.Forms.Label();
			this.ParentNameTextbox = new System.Windows.Forms.TextBox();
			this.ParentNameLabel = new System.Windows.Forms.Label();
			this.RelationButton = new System.Windows.Forms.Button();
			this.mButtonImages = new System.Windows.Forms.ImageList(this.components);
			this.TypeCombo = new LJCFacilityManager.CodeTypeCombo();
			this.DialogMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.DialogMenuHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.DialogMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// FormCancelButton
			// 
			this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.FormCancelButton.Location = new System.Drawing.Point(472, 140);
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
			this.OKButton.Location = new System.Drawing.Point(351, 140);
			this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(112, 35);
			this.OKButton.TabIndex = 7;
			this.OKButton.Text = "&OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// TypeLabel
			// 
			this.TypeLabel.Location = new System.Drawing.Point(18, 103);
			this.TypeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.TypeLabel.Name = "TypeLabel";
			this.TypeLabel.Size = new System.Drawing.Size(130, 20);
			this.TypeLabel.TabIndex = 5;
			this.TypeLabel.Text = "Relation Type";
			// 
			// RelationTextbox
			// 
			this.RelationTextbox.Location = new System.Drawing.Point(150, 58);
			this.RelationTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.RelationTextbox.Name = "RelationTextbox";
			this.RelationTextbox.ReadOnly = true;
			this.RelationTextbox.Size = new System.Drawing.Size(396, 26);
			this.RelationTextbox.TabIndex = 3;
			// 
			// RelationLabel
			// 
			this.RelationLabel.Location = new System.Drawing.Point(18, 63);
			this.RelationLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.RelationLabel.Name = "RelationLabel";
			this.RelationLabel.Size = new System.Drawing.Size(130, 20);
			this.RelationLabel.TabIndex = 2;
			this.RelationLabel.Text = "Relation";
			// 
			// ParentNameTextbox
			// 
			this.ParentNameTextbox.Location = new System.Drawing.Point(150, 18);
			this.ParentNameTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ParentNameTextbox.Name = "ParentNameTextbox";
			this.ParentNameTextbox.ReadOnly = true;
			this.ParentNameTextbox.Size = new System.Drawing.Size(433, 26);
			this.ParentNameTextbox.TabIndex = 1;
			// 
			// ParentNameLabel
			// 
			this.ParentNameLabel.Location = new System.Drawing.Point(18, 23);
			this.ParentNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ParentNameLabel.Name = "ParentNameLabel";
			this.ParentNameLabel.Size = new System.Drawing.Size(130, 20);
			this.ParentNameLabel.TabIndex = 0;
			this.ParentNameLabel.Text = "Person";
			// 
			// RelationButton
			// 
			this.RelationButton.ImageKey = "Ellipse.bmp";
			this.RelationButton.ImageList = this.mButtonImages;
			this.RelationButton.Location = new System.Drawing.Point(550, 54);
			this.RelationButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.RelationButton.Name = "RelationButton";
			this.RelationButton.Size = new System.Drawing.Size(36, 37);
			this.RelationButton.TabIndex = 4;
			this.RelationButton.UseVisualStyleBackColor = true;
			this.RelationButton.Click += new System.EventHandler(this.RelationButton_Click);
			// 
			// mButtonImages
			// 
			this.mButtonImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("mButtonImages.ImageStream")));
			this.mButtonImages.TransparentColor = System.Drawing.Color.Magenta;
			this.mButtonImages.Images.SetKeyName(0, "Ellipse.bmp");
			this.mButtonImages.Images.SetKeyName(1, "Calendar.bmp");
			// 
			// TypeCombo
			// 
			this.TypeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.TypeCombo.FormattingEnabled = true;
			this.TypeCombo.Location = new System.Drawing.Point(150, 98);
			this.TypeCombo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.TypeCombo.Name = "TypeCombo";
			this.TypeCombo.Size = new System.Drawing.Size(433, 28);
			this.TypeCombo.TabIndex = 6;
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
			// RelationDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(603, 186);
			this.ContextMenuStrip = this.DialogMenu;
			this.Controls.Add(this.RelationButton);
			this.Controls.Add(this.ParentNameTextbox);
			this.Controls.Add(this.ParentNameLabel);
			this.Controls.Add(this.FormCancelButton);
			this.Controls.Add(this.OKButton);
			this.Controls.Add(this.TypeLabel);
			this.Controls.Add(this.RelationTextbox);
			this.Controls.Add(this.RelationLabel);
			this.Controls.Add(this.TypeCombo);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "RelationDetail";
			this.Text = "Relation Detail";
			this.Load += new System.EventHandler(this.RelationDetail_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RelationDetail_KeyDown);
			this.DialogMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button FormCancelButton;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.Label TypeLabel;
		private System.Windows.Forms.TextBox RelationTextbox;
		private System.Windows.Forms.Label RelationLabel;
		private CodeTypeCombo TypeCombo;
		private System.Windows.Forms.TextBox ParentNameTextbox;
		private System.Windows.Forms.Label ParentNameLabel;
		private System.Windows.Forms.Button RelationButton;
		private System.Windows.Forms.ImageList mButtonImages;
		private System.Windows.Forms.ContextMenuStrip DialogMenu;
		private System.Windows.Forms.ToolStripMenuItem DialogMenuHelp;
	}
}