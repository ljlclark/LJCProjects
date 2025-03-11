namespace LJCFacilityManager
{
	partial class UnitDetail
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
			this.UnitTypeLabel = new System.Windows.Forms.Label();
			this.DescriptionTextbox = new System.Windows.Forms.TextBox();
			this.DescriptionLabel = new System.Windows.Forms.Label();
			this.UnitTypeCombo = new LJCFacilityManager.CodeTypeCombo();
			this.CodeTextbox = new System.Windows.Forms.TextBox();
			this.CodeLabel = new System.Windows.Forms.Label();
			this.ParentNameLabel = new System.Windows.Forms.Label();
			this.ParentNameTextbox = new System.Windows.Forms.TextBox();
			this.ExtensionTextbox = new System.Windows.Forms.TextBox();
			this.ExtensionLabel = new System.Windows.Forms.Label();
			this.PhoneTextbox = new System.Windows.Forms.TextBox();
			this.PhoneLabel = new System.Windows.Forms.Label();
			this.BedsCombo = new System.Windows.Forms.ComboBox();
			this.BathsCombo = new System.Windows.Forms.ComboBox();
			this.BedsLabel = new System.Windows.Forms.Label();
			this.BathsLabel = new System.Windows.Forms.Label();
			this.DialogMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.DialogMenuHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.DialogMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// FormCancelButton
			// 
			this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.FormCancelButton.Location = new System.Drawing.Point(472, 262);
			this.FormCancelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.FormCancelButton.Name = "FormCancelButton";
			this.FormCancelButton.Size = new System.Drawing.Size(112, 35);
			this.FormCancelButton.TabIndex = 17;
			this.FormCancelButton.Text = "Cancel";
			this.FormCancelButton.UseVisualStyleBackColor = true;
			this.FormCancelButton.Click += new System.EventHandler(this.FormCancelButton_Click);
			// 
			// OKButton
			// 
			this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OKButton.Location = new System.Drawing.Point(351, 262);
			this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(112, 35);
			this.OKButton.TabIndex = 16;
			this.OKButton.Text = "&OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// UnitTypeLabel
			// 
			this.UnitTypeLabel.Location = new System.Drawing.Point(10, 143);
			this.UnitTypeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.UnitTypeLabel.Name = "UnitTypeLabel";
			this.UnitTypeLabel.Size = new System.Drawing.Size(123, 20);
			this.UnitTypeLabel.TabIndex = 6;
			this.UnitTypeLabel.Text = "Unit Type";
			// 
			// DescriptionTextbox
			// 
			this.DescriptionTextbox.Location = new System.Drawing.Point(135, 98);
			this.DescriptionTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.DescriptionTextbox.Name = "DescriptionTextbox";
			this.DescriptionTextbox.Size = new System.Drawing.Size(433, 26);
			this.DescriptionTextbox.TabIndex = 5;
			// 
			// DescriptionLabel
			// 
			this.DescriptionLabel.Location = new System.Drawing.Point(10, 103);
			this.DescriptionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.DescriptionLabel.Name = "DescriptionLabel";
			this.DescriptionLabel.Size = new System.Drawing.Size(123, 20);
			this.DescriptionLabel.TabIndex = 4;
			this.DescriptionLabel.Text = "Description";
			// 
			// UnitTypeCombo
			// 
			this.UnitTypeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.UnitTypeCombo.FormattingEnabled = true;
			this.UnitTypeCombo.Location = new System.Drawing.Point(135, 138);
			this.UnitTypeCombo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.UnitTypeCombo.Name = "UnitTypeCombo";
			this.UnitTypeCombo.Size = new System.Drawing.Size(433, 28);
			this.UnitTypeCombo.TabIndex = 7;
			// 
			// CodeTextbox
			// 
			this.CodeTextbox.Location = new System.Drawing.Point(135, 58);
			this.CodeTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.CodeTextbox.Name = "CodeTextbox";
			this.CodeTextbox.Size = new System.Drawing.Size(180, 26);
			this.CodeTextbox.TabIndex = 3;
			// 
			// CodeLabel
			// 
			this.CodeLabel.Location = new System.Drawing.Point(10, 63);
			this.CodeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.CodeLabel.Name = "CodeLabel";
			this.CodeLabel.Size = new System.Drawing.Size(123, 20);
			this.CodeLabel.TabIndex = 2;
			this.CodeLabel.Text = "Code";
			// 
			// ParentNameLabel
			// 
			this.ParentNameLabel.Location = new System.Drawing.Point(10, 23);
			this.ParentNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ParentNameLabel.Name = "ParentNameLabel";
			this.ParentNameLabel.Size = new System.Drawing.Size(123, 20);
			this.ParentNameLabel.TabIndex = 0;
			this.ParentNameLabel.Text = "Facility";
			// 
			// ParentNameTextbox
			// 
			this.ParentNameTextbox.Location = new System.Drawing.Point(135, 18);
			this.ParentNameTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ParentNameTextbox.Name = "ParentNameTextbox";
			this.ParentNameTextbox.ReadOnly = true;
			this.ParentNameTextbox.Size = new System.Drawing.Size(433, 26);
			this.ParentNameTextbox.TabIndex = 1;
			this.ParentNameTextbox.TabStop = false;
			// 
			// ExtensionTextbox
			// 
			this.ExtensionTextbox.Location = new System.Drawing.Point(441, 218);
			this.ExtensionTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ExtensionTextbox.Name = "ExtensionTextbox";
			this.ExtensionTextbox.Size = new System.Drawing.Size(55, 26);
			this.ExtensionTextbox.TabIndex = 15;
			// 
			// ExtensionLabel
			// 
			this.ExtensionLabel.Location = new System.Drawing.Point(316, 223);
			this.ExtensionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ExtensionLabel.Name = "ExtensionLabel";
			this.ExtensionLabel.Size = new System.Drawing.Size(123, 20);
			this.ExtensionLabel.TabIndex = 14;
			this.ExtensionLabel.Text = "Extension";
			// 
			// PhoneTextbox
			// 
			this.PhoneTextbox.Location = new System.Drawing.Point(135, 218);
			this.PhoneTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.PhoneTextbox.Name = "PhoneTextbox";
			this.PhoneTextbox.Size = new System.Drawing.Size(151, 26);
			this.PhoneTextbox.TabIndex = 13;
			// 
			// PhoneLabel
			// 
			this.PhoneLabel.Location = new System.Drawing.Point(10, 223);
			this.PhoneLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.PhoneLabel.Name = "PhoneLabel";
			this.PhoneLabel.Size = new System.Drawing.Size(123, 20);
			this.PhoneLabel.TabIndex = 12;
			this.PhoneLabel.Text = "Phone";
			// 
			// BedsCombo
			// 
			this.BedsCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.BedsCombo.FormattingEnabled = true;
			this.BedsCombo.Location = new System.Drawing.Point(135, 178);
			this.BedsCombo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.BedsCombo.Name = "BedsCombo";
			this.BedsCombo.Size = new System.Drawing.Size(55, 28);
			this.BedsCombo.TabIndex = 9;
			// 
			// BathsCombo
			// 
			this.BathsCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.BathsCombo.FormattingEnabled = true;
			this.BathsCombo.Location = new System.Drawing.Point(441, 178);
			this.BathsCombo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.BathsCombo.Name = "BathsCombo";
			this.BathsCombo.Size = new System.Drawing.Size(55, 28);
			this.BathsCombo.TabIndex = 11;
			// 
			// BedsLabel
			// 
			this.BedsLabel.Location = new System.Drawing.Point(10, 183);
			this.BedsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.BedsLabel.Name = "BedsLabel";
			this.BedsLabel.Size = new System.Drawing.Size(123, 20);
			this.BedsLabel.TabIndex = 8;
			this.BedsLabel.Text = "Beds";
			// 
			// BathsLabel
			// 
			this.BathsLabel.Location = new System.Drawing.Point(316, 183);
			this.BathsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.BathsLabel.Name = "BathsLabel";
			this.BathsLabel.Size = new System.Drawing.Size(123, 20);
			this.BathsLabel.TabIndex = 10;
			this.BathsLabel.Text = "Baths";
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
			// UnitDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(596, 308);
			this.ContextMenuStrip = this.DialogMenu;
			this.Controls.Add(this.BathsLabel);
			this.Controls.Add(this.BedsLabel);
			this.Controls.Add(this.BathsCombo);
			this.Controls.Add(this.BedsCombo);
			this.Controls.Add(this.ExtensionTextbox);
			this.Controls.Add(this.ExtensionLabel);
			this.Controls.Add(this.PhoneTextbox);
			this.Controls.Add(this.PhoneLabel);
			this.Controls.Add(this.ParentNameTextbox);
			this.Controls.Add(this.ParentNameLabel);
			this.Controls.Add(this.FormCancelButton);
			this.Controls.Add(this.OKButton);
			this.Controls.Add(this.UnitTypeLabel);
			this.Controls.Add(this.DescriptionTextbox);
			this.Controls.Add(this.DescriptionLabel);
			this.Controls.Add(this.UnitTypeCombo);
			this.Controls.Add(this.CodeTextbox);
			this.Controls.Add(this.CodeLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "UnitDetail";
			this.ShowInTaskbar = false;
			this.Text = "Unit Detail";
			this.Load += new System.EventHandler(this.UnitDetail_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UnitDetail_KeyDown);
			this.DialogMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button FormCancelButton;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.Label UnitTypeLabel;
		private System.Windows.Forms.TextBox DescriptionTextbox;
		private System.Windows.Forms.Label DescriptionLabel;
		private LJCFacilityManager.CodeTypeCombo UnitTypeCombo;
		private System.Windows.Forms.TextBox CodeTextbox;
		private System.Windows.Forms.Label CodeLabel;
		private System.Windows.Forms.Label ParentNameLabel;
		private System.Windows.Forms.TextBox ParentNameTextbox;
		private System.Windows.Forms.TextBox ExtensionTextbox;
		private System.Windows.Forms.Label ExtensionLabel;
		private System.Windows.Forms.TextBox PhoneTextbox;
		private System.Windows.Forms.Label PhoneLabel;
		private System.Windows.Forms.ComboBox BedsCombo;
		private System.Windows.Forms.ComboBox BathsCombo;
		private System.Windows.Forms.Label BedsLabel;
		private System.Windows.Forms.Label BathsLabel;
		private System.Windows.Forms.ContextMenuStrip DialogMenu;
		private System.Windows.Forms.ToolStripMenuItem DialogMenuHelp;
	}
}