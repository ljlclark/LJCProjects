namespace LJCFacilityManager
{
	partial class EquipmentDetail
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
			this.SerialNumberTextbox = new System.Windows.Forms.TextBox();
			this.SerialNumberLabel = new System.Windows.Forms.Label();
			this.ModelTextbox = new System.Windows.Forms.TextBox();
			this.ModelLabel = new System.Windows.Forms.Label();
			this.MakeTextbox = new System.Windows.Forms.TextBox();
			this.MakeLabel = new System.Windows.Forms.Label();
			this.FormCancelButton = new System.Windows.Forms.Button();
			this.OKButton = new System.Windows.Forms.Button();
			this.EquipmentTypeLabel = new System.Windows.Forms.Label();
			this.DescriptionTextbox = new System.Windows.Forms.TextBox();
			this.DescriptionLabel = new System.Windows.Forms.Label();
			this.EquipmentTypeCombo = new LJCFacilityManager.CodeTypeCombo();
			this.CodeTextbox = new System.Windows.Forms.TextBox();
			this.CodeLabel = new System.Windows.Forms.Label();
			this.LocationGroup = new System.Windows.Forms.GroupBox();
			this.FacilityLabel = new System.Windows.Forms.Label();
			this.FacilityCombo = new LJCFacilityManager.FacilityCombo();
			this.UnitLabel = new System.Windows.Forms.Label();
			this.UnitCombo = new LJCFacilityManager.UnitCombo();
			this.DialogMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.DialogMenuHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.LocationGroup.SuspendLayout();
			this.DialogMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// SerialNumberTextbox
			// 
			this.SerialNumberTextbox.Location = new System.Drawing.Point(150, 342);
			this.SerialNumberTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.SerialNumberTextbox.Name = "SerialNumberTextbox";
			this.SerialNumberTextbox.Size = new System.Drawing.Size(180, 26);
			this.SerialNumberTextbox.TabIndex = 12;
			// 
			// SerialNumberLabel
			// 
			this.SerialNumberLabel.Location = new System.Drawing.Point(18, 346);
			this.SerialNumberLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.SerialNumberLabel.Name = "SerialNumberLabel";
			this.SerialNumberLabel.Size = new System.Drawing.Size(130, 20);
			this.SerialNumberLabel.TabIndex = 11;
			this.SerialNumberLabel.Text = "Serial Number";
			// 
			// ModelTextbox
			// 
			this.ModelTextbox.Location = new System.Drawing.Point(150, 302);
			this.ModelTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ModelTextbox.Name = "ModelTextbox";
			this.ModelTextbox.Size = new System.Drawing.Size(180, 26);
			this.ModelTextbox.TabIndex = 10;
			// 
			// ModelLabel
			// 
			this.ModelLabel.Location = new System.Drawing.Point(18, 306);
			this.ModelLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ModelLabel.Name = "ModelLabel";
			this.ModelLabel.Size = new System.Drawing.Size(130, 20);
			this.ModelLabel.TabIndex = 9;
			this.ModelLabel.Text = "Model";
			// 
			// MakeTextbox
			// 
			this.MakeTextbox.Location = new System.Drawing.Point(150, 262);
			this.MakeTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MakeTextbox.Name = "MakeTextbox";
			this.MakeTextbox.Size = new System.Drawing.Size(180, 26);
			this.MakeTextbox.TabIndex = 8;
			// 
			// MakeLabel
			// 
			this.MakeLabel.Location = new System.Drawing.Point(18, 266);
			this.MakeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.MakeLabel.Name = "MakeLabel";
			this.MakeLabel.Size = new System.Drawing.Size(130, 20);
			this.MakeLabel.TabIndex = 7;
			this.MakeLabel.Text = "Make";
			// 
			// FormCancelButton
			// 
			this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.FormCancelButton.Location = new System.Drawing.Point(472, 382);
			this.FormCancelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.FormCancelButton.Name = "FormCancelButton";
			this.FormCancelButton.Size = new System.Drawing.Size(112, 35);
			this.FormCancelButton.TabIndex = 14;
			this.FormCancelButton.Text = "Cancel";
			this.FormCancelButton.UseVisualStyleBackColor = true;
			this.FormCancelButton.Click += new System.EventHandler(this.FormCancelButton_Click);
			// 
			// OKButton
			// 
			this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OKButton.Location = new System.Drawing.Point(351, 382);
			this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(112, 35);
			this.OKButton.TabIndex = 13;
			this.OKButton.Text = "&OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// EquipmentTypeLabel
			// 
			this.EquipmentTypeLabel.Location = new System.Drawing.Point(18, 103);
			this.EquipmentTypeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.EquipmentTypeLabel.Name = "EquipmentTypeLabel";
			this.EquipmentTypeLabel.Size = new System.Drawing.Size(129, 20);
			this.EquipmentTypeLabel.TabIndex = 4;
			this.EquipmentTypeLabel.Text = "Equipment Type";
			// 
			// DescriptionTextbox
			// 
			this.DescriptionTextbox.Location = new System.Drawing.Point(150, 58);
			this.DescriptionTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.DescriptionTextbox.Name = "DescriptionTextbox";
			this.DescriptionTextbox.Size = new System.Drawing.Size(433, 26);
			this.DescriptionTextbox.TabIndex = 3;
			// 
			// DescriptionLabel
			// 
			this.DescriptionLabel.Location = new System.Drawing.Point(18, 63);
			this.DescriptionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.DescriptionLabel.Name = "DescriptionLabel";
			this.DescriptionLabel.Size = new System.Drawing.Size(130, 20);
			this.DescriptionLabel.TabIndex = 2;
			this.DescriptionLabel.Text = "Description";
			// 
			// EquipmentTypeCombo
			// 
			this.EquipmentTypeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.EquipmentTypeCombo.FormattingEnabled = true;
			this.EquipmentTypeCombo.Location = new System.Drawing.Point(150, 98);
			this.EquipmentTypeCombo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.EquipmentTypeCombo.Name = "EquipmentTypeCombo";
			this.EquipmentTypeCombo.Size = new System.Drawing.Size(433, 28);
			this.EquipmentTypeCombo.TabIndex = 5;
			// 
			// CodeTextbox
			// 
			this.CodeTextbox.Location = new System.Drawing.Point(150, 18);
			this.CodeTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.CodeTextbox.Name = "CodeTextbox";
			this.CodeTextbox.Size = new System.Drawing.Size(180, 26);
			this.CodeTextbox.TabIndex = 1;
			// 
			// CodeLabel
			// 
			this.CodeLabel.Location = new System.Drawing.Point(18, 23);
			this.CodeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.CodeLabel.Name = "CodeLabel";
			this.CodeLabel.Size = new System.Drawing.Size(130, 20);
			this.CodeLabel.TabIndex = 0;
			this.CodeLabel.Text = "Code";
			// 
			// LocationGroup
			// 
			this.LocationGroup.Controls.Add(this.FacilityLabel);
			this.LocationGroup.Controls.Add(this.FacilityCombo);
			this.LocationGroup.Controls.Add(this.UnitLabel);
			this.LocationGroup.Controls.Add(this.UnitCombo);
			this.LocationGroup.Location = new System.Drawing.Point(8, 137);
			this.LocationGroup.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.LocationGroup.Name = "LocationGroup";
			this.LocationGroup.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.LocationGroup.Size = new System.Drawing.Size(588, 114);
			this.LocationGroup.TabIndex = 6;
			this.LocationGroup.TabStop = false;
			this.LocationGroup.Text = "Location";
			// 
			// FacilityLabel
			// 
			this.FacilityLabel.Location = new System.Drawing.Point(10, 32);
			this.FacilityLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.FacilityLabel.Name = "FacilityLabel";
			this.FacilityLabel.Size = new System.Drawing.Size(129, 20);
			this.FacilityLabel.TabIndex = 0;
			this.FacilityLabel.Text = "Facility";
			// 
			// FacilityCombo
			// 
			this.FacilityCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.FacilityCombo.FormattingEnabled = true;
			this.FacilityCombo.Location = new System.Drawing.Point(142, 28);
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
			this.UnitLabel.Size = new System.Drawing.Size(129, 20);
			this.UnitLabel.TabIndex = 2;
			this.UnitLabel.Text = "Unit";
			// 
			// UnitCombo
			// 
			this.UnitCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.UnitCombo.FormattingEnabled = true;
			this.UnitCombo.Location = new System.Drawing.Point(142, 68);
			this.UnitCombo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.UnitCombo.Name = "UnitCombo";
			this.UnitCombo.Size = new System.Drawing.Size(433, 28);
			this.UnitCombo.TabIndex = 3;
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
			// EquipmentDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(603, 428);
			this.ContextMenuStrip = this.DialogMenu;
			this.Controls.Add(this.LocationGroup);
			this.Controls.Add(this.SerialNumberTextbox);
			this.Controls.Add(this.SerialNumberLabel);
			this.Controls.Add(this.ModelTextbox);
			this.Controls.Add(this.ModelLabel);
			this.Controls.Add(this.MakeTextbox);
			this.Controls.Add(this.MakeLabel);
			this.Controls.Add(this.FormCancelButton);
			this.Controls.Add(this.OKButton);
			this.Controls.Add(this.EquipmentTypeLabel);
			this.Controls.Add(this.DescriptionTextbox);
			this.Controls.Add(this.DescriptionLabel);
			this.Controls.Add(this.EquipmentTypeCombo);
			this.Controls.Add(this.CodeTextbox);
			this.Controls.Add(this.CodeLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "EquipmentDetail";
			this.ShowInTaskbar = false;
			this.Text = "Equipment Detail";
			this.Load += new System.EventHandler(this.EquipmentDetail_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EquipmentDetail_KeyDown);
			this.LocationGroup.ResumeLayout(false);
			this.DialogMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox SerialNumberTextbox;
		private System.Windows.Forms.Label SerialNumberLabel;
		private System.Windows.Forms.TextBox ModelTextbox;
		private System.Windows.Forms.Label ModelLabel;
		private System.Windows.Forms.TextBox MakeTextbox;
		private System.Windows.Forms.Label MakeLabel;
		private System.Windows.Forms.Button FormCancelButton;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.Label EquipmentTypeLabel;
		private System.Windows.Forms.TextBox DescriptionTextbox;
		private System.Windows.Forms.Label DescriptionLabel;
		private CodeTypeCombo EquipmentTypeCombo;
		private System.Windows.Forms.TextBox CodeTextbox;
		private System.Windows.Forms.Label CodeLabel;
		private System.Windows.Forms.GroupBox LocationGroup;
		private System.Windows.Forms.Label FacilityLabel;
		private LJCFacilityManager.FacilityCombo FacilityCombo;
		private System.Windows.Forms.Label UnitLabel;
		private UnitCombo UnitCombo;
		private System.Windows.Forms.ContextMenuStrip DialogMenu;
		private System.Windows.Forms.ToolStripMenuItem DialogMenuHelp;
	}
}