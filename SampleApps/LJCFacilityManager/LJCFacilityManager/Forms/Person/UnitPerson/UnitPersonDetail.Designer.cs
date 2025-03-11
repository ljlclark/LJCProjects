namespace LJCFacilityManager
{
	partial class UnitPersonDetail
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UnitPersonDetail));
			this.LocationGroup = new System.Windows.Forms.GroupBox();
			this.FacilityLabel = new System.Windows.Forms.Label();
			this.FacilityCombo = new LJCFacilityManager.FacilityCombo();
			this.UnitLabel = new System.Windows.Forms.Label();
			this.UnitCombo = new LJCFacilityManager.UnitCombo();
			this.FormCancelButton = new System.Windows.Forms.Button();
			this.OKButton = new System.Windows.Forms.Button();
			this.ParentNameTextbox = new System.Windows.Forms.TextBox();
			this.ParentNameLabel = new System.Windows.Forms.Label();
			this.EndMask = new System.Windows.Forms.MaskedTextBox();
			this.BeginMask = new System.Windows.Forms.MaskedTextBox();
			this.EndButton = new System.Windows.Forms.Button();
			this.ButtonImages = new System.Windows.Forms.ImageList(this.components);
			this.BeginButton = new System.Windows.Forms.Button();
			this.BeginLabel = new System.Windows.Forms.Label();
			this.EndLabel = new System.Windows.Forms.Label();
			this.DialogMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.LocationGroup.SuspendLayout();
			this.DialogMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// LocationGroup
			// 
			this.LocationGroup.Controls.Add(this.FacilityLabel);
			this.LocationGroup.Controls.Add(this.FacilityCombo);
			this.LocationGroup.Controls.Add(this.UnitLabel);
			this.LocationGroup.Controls.Add(this.UnitCombo);
			this.LocationGroup.Location = new System.Drawing.Point(8, 55);
			this.LocationGroup.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.LocationGroup.Name = "LocationGroup";
			this.LocationGroup.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.LocationGroup.Size = new System.Drawing.Size(610, 114);
			this.LocationGroup.TabIndex = 2;
			this.LocationGroup.TabStop = false;
			this.LocationGroup.Text = "Location";
			// 
			// FacilityLabel
			// 
			this.FacilityLabel.Location = new System.Drawing.Point(10, 32);
			this.FacilityLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.FacilityLabel.Name = "FacilityLabel";
			this.FacilityLabel.Size = new System.Drawing.Size(152, 20);
			this.FacilityLabel.TabIndex = 0;
			this.FacilityLabel.Text = "Facility";
			// 
			// FacilityCombo
			// 
			this.FacilityCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.FacilityCombo.FormattingEnabled = true;
			this.FacilityCombo.Location = new System.Drawing.Point(165, 28);
			this.FacilityCombo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.FacilityCombo.Name = "FacilityCombo";
			this.FacilityCombo.Size = new System.Drawing.Size(433, 28);
			this.FacilityCombo.TabIndex = 1;
			this.FacilityCombo.SelectedIndexChanged += new System.EventHandler(this.FacilityCombo_SelectedIndexChanged);
			// 
			// UnitLabel
			// 
			this.UnitLabel.Location = new System.Drawing.Point(10, 72);
			this.UnitLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.UnitLabel.Name = "UnitLabel";
			this.UnitLabel.Size = new System.Drawing.Size(152, 20);
			this.UnitLabel.TabIndex = 2;
			this.UnitLabel.Text = "Unit";
			// 
			// UnitCombo
			// 
			this.UnitCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.UnitCombo.FormattingEnabled = true;
			this.UnitCombo.Location = new System.Drawing.Point(165, 68);
			this.UnitCombo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.UnitCombo.Name = "UnitCombo";
			this.UnitCombo.Size = new System.Drawing.Size(433, 28);
			this.UnitCombo.TabIndex = 3;
			// 
			// FormCancelButton
			// 
			this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.FormCancelButton.Location = new System.Drawing.Point(495, 263);
			this.FormCancelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.FormCancelButton.Name = "FormCancelButton";
			this.FormCancelButton.Size = new System.Drawing.Size(112, 35);
			this.FormCancelButton.TabIndex = 10;
			this.FormCancelButton.Text = "Cancel";
			this.FormCancelButton.UseVisualStyleBackColor = true;
			this.FormCancelButton.Click += new System.EventHandler(this.FormCancelButton_Click);
			// 
			// OKButton
			// 
			this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OKButton.Location = new System.Drawing.Point(374, 263);
			this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(112, 35);
			this.OKButton.TabIndex = 9;
			this.OKButton.Text = "&OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// ParentNameTextbox
			// 
			this.ParentNameTextbox.Location = new System.Drawing.Point(172, 18);
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
			this.ParentNameLabel.Size = new System.Drawing.Size(153, 20);
			this.ParentNameLabel.TabIndex = 0;
			this.ParentNameLabel.Text = "Person";
			// 
			// EndMask
			// 
			this.EndMask.Location = new System.Drawing.Point(172, 222);
			this.EndMask.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.EndMask.Mask = "00/00/0000";
			this.EndMask.Name = "EndMask";
			this.EndMask.Size = new System.Drawing.Size(100, 26);
			this.EndMask.TabIndex = 7;
			this.EndMask.ValidatingType = typeof(System.DateTime);
			// 
			// BeginMask
			// 
			this.BeginMask.Location = new System.Drawing.Point(172, 182);
			this.BeginMask.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.BeginMask.Mask = "00/00/0000";
			this.BeginMask.Name = "BeginMask";
			this.BeginMask.Size = new System.Drawing.Size(100, 26);
			this.BeginMask.TabIndex = 4;
			this.BeginMask.ValidatingType = typeof(System.DateTime);
			// 
			// EndButton
			// 
			this.EndButton.ImageKey = "Calendar.bmp";
			this.EndButton.ImageList = this.ButtonImages;
			this.EndButton.Location = new System.Drawing.Point(280, 217);
			this.EndButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.EndButton.Name = "EndButton";
			this.EndButton.Size = new System.Drawing.Size(36, 37);
			this.EndButton.TabIndex = 8;
			this.EndButton.UseVisualStyleBackColor = true;
			this.EndButton.Click += new System.EventHandler(this.EndButton_Click);
			// 
			// ButtonImages
			// 
			this.ButtonImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ButtonImages.ImageStream")));
			this.ButtonImages.TransparentColor = System.Drawing.Color.Magenta;
			this.ButtonImages.Images.SetKeyName(0, "Ellipse.bmp");
			this.ButtonImages.Images.SetKeyName(1, "Calendar.bmp");
			// 
			// BeginButton
			// 
			this.BeginButton.ImageKey = "Calendar.bmp";
			this.BeginButton.ImageList = this.ButtonImages;
			this.BeginButton.Location = new System.Drawing.Point(280, 177);
			this.BeginButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.BeginButton.Name = "BeginButton";
			this.BeginButton.Size = new System.Drawing.Size(36, 37);
			this.BeginButton.TabIndex = 5;
			this.BeginButton.UseVisualStyleBackColor = true;
			this.BeginButton.Click += new System.EventHandler(this.BeginButton_Click);
			// 
			// BeginLabel
			// 
			this.BeginLabel.Location = new System.Drawing.Point(18, 186);
			this.BeginLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.BeginLabel.Name = "BeginLabel";
			this.BeginLabel.Size = new System.Drawing.Size(153, 20);
			this.BeginLabel.TabIndex = 3;
			this.BeginLabel.Text = "Begin Date";
			// 
			// EndLabel
			// 
			this.EndLabel.Location = new System.Drawing.Point(18, 226);
			this.EndLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.EndLabel.Name = "EndLabel";
			this.EndLabel.Size = new System.Drawing.Size(153, 20);
			this.EndLabel.TabIndex = 6;
			this.EndLabel.Text = "End Date";
			// 
			// DialogMenu
			// 
			this.DialogMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.DialogMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem});
			this.DialogMenu.Name = "DialogMenu";
			this.DialogMenu.Size = new System.Drawing.Size(122, 34);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(121, 30);
			this.helpToolStripMenuItem.Text = "&Help";
			this.helpToolStripMenuItem.Click += new System.EventHandler(this.DialogMenuHelp_Click);
			// 
			// UnitPersonDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(626, 309);
			this.ContextMenuStrip = this.DialogMenu;
			this.Controls.Add(this.EndMask);
			this.Controls.Add(this.BeginMask);
			this.Controls.Add(this.EndButton);
			this.Controls.Add(this.BeginButton);
			this.Controls.Add(this.BeginLabel);
			this.Controls.Add(this.EndLabel);
			this.Controls.Add(this.ParentNameTextbox);
			this.Controls.Add(this.ParentNameLabel);
			this.Controls.Add(this.FormCancelButton);
			this.Controls.Add(this.OKButton);
			this.Controls.Add(this.LocationGroup);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "UnitPersonDetail";
			this.ShowInTaskbar = false;
			this.Text = "Person Unit";
			this.Load += new System.EventHandler(this.UnitPersonDetail_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UnitPersonDetail_KeyDown);
			this.LocationGroup.ResumeLayout(false);
			this.DialogMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox LocationGroup;
		private System.Windows.Forms.Label FacilityLabel;
		private FacilityCombo FacilityCombo;
		private System.Windows.Forms.Label UnitLabel;
		private UnitCombo UnitCombo;
		private System.Windows.Forms.Button FormCancelButton;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.TextBox ParentNameTextbox;
		private System.Windows.Forms.Label ParentNameLabel;
		private System.Windows.Forms.MaskedTextBox EndMask;
		private System.Windows.Forms.MaskedTextBox BeginMask;
		private System.Windows.Forms.Button EndButton;
		private System.Windows.Forms.Button BeginButton;
		private System.Windows.Forms.Label BeginLabel;
		private System.Windows.Forms.Label EndLabel;
		private System.Windows.Forms.ImageList ButtonImages;
		private System.Windows.Forms.ContextMenuStrip DialogMenu;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
	}
}