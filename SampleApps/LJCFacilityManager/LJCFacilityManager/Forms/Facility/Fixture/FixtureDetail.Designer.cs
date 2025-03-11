namespace LJCFacilityManager
{
	partial class FixtureDetail
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
			this.FixtureTypeLabel = new System.Windows.Forms.Label();
			this.DescriptionTextbox = new System.Windows.Forms.TextBox();
			this.DescriptionLabel = new System.Windows.Forms.Label();
			this.CodeTextbox = new System.Windows.Forms.TextBox();
			this.CodeLabel = new System.Windows.Forms.Label();
			this.ParentNameTextbox = new System.Windows.Forms.TextBox();
			this.ParentNameLabel = new System.Windows.Forms.Label();
			this.MakeTextbox = new System.Windows.Forms.TextBox();
			this.MakeLabel = new System.Windows.Forms.Label();
			this.ModelTextbox = new System.Windows.Forms.TextBox();
			this.ModelLabel = new System.Windows.Forms.Label();
			this.SerialNumberTextbox = new System.Windows.Forms.TextBox();
			this.SerialNumberLabel = new System.Windows.Forms.Label();
			this.FixtureTypeCombo = new LJCFacilityManager.CodeTypeCombo();
			this.DialogMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.DialogMenuHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.DialogMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// FormCancelButton
			// 
			this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.FormCancelButton.Location = new System.Drawing.Point(472, 300);
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
			this.OKButton.Location = new System.Drawing.Point(351, 300);
			this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(112, 35);
			this.OKButton.TabIndex = 14;
			this.OKButton.Text = "&OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// FixtureTypeLabel
			// 
			this.FixtureTypeLabel.Location = new System.Drawing.Point(18, 143);
			this.FixtureTypeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.FixtureTypeLabel.Name = "FixtureTypeLabel";
			this.FixtureTypeLabel.Size = new System.Drawing.Size(129, 20);
			this.FixtureTypeLabel.TabIndex = 6;
			this.FixtureTypeLabel.Text = "Fixture Type";
			// 
			// DescriptionTextbox
			// 
			this.DescriptionTextbox.Location = new System.Drawing.Point(150, 98);
			this.DescriptionTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.DescriptionTextbox.Name = "DescriptionTextbox";
			this.DescriptionTextbox.Size = new System.Drawing.Size(433, 26);
			this.DescriptionTextbox.TabIndex = 5;
			// 
			// DescriptionLabel
			// 
			this.DescriptionLabel.Location = new System.Drawing.Point(18, 103);
			this.DescriptionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.DescriptionLabel.Name = "DescriptionLabel";
			this.DescriptionLabel.Size = new System.Drawing.Size(130, 20);
			this.DescriptionLabel.TabIndex = 4;
			this.DescriptionLabel.Text = "Description";
			// 
			// CodeTextbox
			// 
			this.CodeTextbox.Location = new System.Drawing.Point(150, 58);
			this.CodeTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.CodeTextbox.Name = "CodeTextbox";
			this.CodeTextbox.Size = new System.Drawing.Size(180, 26);
			this.CodeTextbox.TabIndex = 3;
			// 
			// CodeLabel
			// 
			this.CodeLabel.Location = new System.Drawing.Point(18, 63);
			this.CodeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.CodeLabel.Name = "CodeLabel";
			this.CodeLabel.Size = new System.Drawing.Size(130, 20);
			this.CodeLabel.TabIndex = 2;
			this.CodeLabel.Text = "Code";
			// 
			// ParentNameTextbox
			// 
			this.ParentNameTextbox.Location = new System.Drawing.Point(150, 18);
			this.ParentNameTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ParentNameTextbox.Name = "ParentNameTextbox";
			this.ParentNameTextbox.ReadOnly = true;
			this.ParentNameTextbox.Size = new System.Drawing.Size(433, 26);
			this.ParentNameTextbox.TabIndex = 1;
			this.ParentNameTextbox.TabStop = false;
			// 
			// ParentNameLabel
			// 
			this.ParentNameLabel.Location = new System.Drawing.Point(18, 23);
			this.ParentNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ParentNameLabel.Name = "ParentNameLabel";
			this.ParentNameLabel.Size = new System.Drawing.Size(130, 20);
			this.ParentNameLabel.TabIndex = 0;
			this.ParentNameLabel.Text = "Unit";
			// 
			// MakeTextbox
			// 
			this.MakeTextbox.Location = new System.Drawing.Point(150, 180);
			this.MakeTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MakeTextbox.Name = "MakeTextbox";
			this.MakeTextbox.Size = new System.Drawing.Size(180, 26);
			this.MakeTextbox.TabIndex = 9;
			// 
			// MakeLabel
			// 
			this.MakeLabel.Location = new System.Drawing.Point(18, 185);
			this.MakeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.MakeLabel.Name = "MakeLabel";
			this.MakeLabel.Size = new System.Drawing.Size(130, 20);
			this.MakeLabel.TabIndex = 8;
			this.MakeLabel.Text = "Make";
			// 
			// ModelTextbox
			// 
			this.ModelTextbox.Location = new System.Drawing.Point(150, 220);
			this.ModelTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ModelTextbox.Name = "ModelTextbox";
			this.ModelTextbox.Size = new System.Drawing.Size(180, 26);
			this.ModelTextbox.TabIndex = 11;
			// 
			// ModelLabel
			// 
			this.ModelLabel.Location = new System.Drawing.Point(18, 225);
			this.ModelLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ModelLabel.Name = "ModelLabel";
			this.ModelLabel.Size = new System.Drawing.Size(130, 20);
			this.ModelLabel.TabIndex = 10;
			this.ModelLabel.Text = "Model";
			// 
			// SerialNumberTextbox
			// 
			this.SerialNumberTextbox.Location = new System.Drawing.Point(150, 260);
			this.SerialNumberTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.SerialNumberTextbox.Name = "SerialNumberTextbox";
			this.SerialNumberTextbox.Size = new System.Drawing.Size(180, 26);
			this.SerialNumberTextbox.TabIndex = 13;
			// 
			// SerialNumberLabel
			// 
			this.SerialNumberLabel.Location = new System.Drawing.Point(18, 265);
			this.SerialNumberLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.SerialNumberLabel.Name = "SerialNumberLabel";
			this.SerialNumberLabel.Size = new System.Drawing.Size(130, 20);
			this.SerialNumberLabel.TabIndex = 12;
			this.SerialNumberLabel.Text = "Serial Number";
			// 
			// FixtureTypeCombo
			// 
			this.FixtureTypeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.FixtureTypeCombo.FormattingEnabled = true;
			this.FixtureTypeCombo.Location = new System.Drawing.Point(150, 138);
			this.FixtureTypeCombo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.FixtureTypeCombo.Name = "FixtureTypeCombo";
			this.FixtureTypeCombo.Size = new System.Drawing.Size(433, 28);
			this.FixtureTypeCombo.TabIndex = 7;
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
			// FixtureDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(603, 346);
			this.ContextMenuStrip = this.DialogMenu;
			this.Controls.Add(this.SerialNumberTextbox);
			this.Controls.Add(this.SerialNumberLabel);
			this.Controls.Add(this.ModelTextbox);
			this.Controls.Add(this.ModelLabel);
			this.Controls.Add(this.MakeTextbox);
			this.Controls.Add(this.MakeLabel);
			this.Controls.Add(this.ParentNameTextbox);
			this.Controls.Add(this.ParentNameLabel);
			this.Controls.Add(this.FormCancelButton);
			this.Controls.Add(this.OKButton);
			this.Controls.Add(this.FixtureTypeLabel);
			this.Controls.Add(this.DescriptionTextbox);
			this.Controls.Add(this.DescriptionLabel);
			this.Controls.Add(this.FixtureTypeCombo);
			this.Controls.Add(this.CodeTextbox);
			this.Controls.Add(this.CodeLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FixtureDetail";
			this.ShowInTaskbar = false;
			this.Text = "Fixture Detail";
			this.Load += new System.EventHandler(this.FixtureDetail_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FixtureDetail_KeyDown);
			this.DialogMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button FormCancelButton;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.Label FixtureTypeLabel;
		private System.Windows.Forms.TextBox DescriptionTextbox;
		private System.Windows.Forms.Label DescriptionLabel;
		private LJCFacilityManager.CodeTypeCombo FixtureTypeCombo;
		private System.Windows.Forms.TextBox CodeTextbox;
		private System.Windows.Forms.Label CodeLabel;
		private System.Windows.Forms.TextBox ParentNameTextbox;
		private System.Windows.Forms.Label ParentNameLabel;
		private System.Windows.Forms.TextBox MakeTextbox;
		private System.Windows.Forms.Label MakeLabel;
		private System.Windows.Forms.TextBox ModelTextbox;
		private System.Windows.Forms.Label ModelLabel;
		private System.Windows.Forms.TextBox SerialNumberTextbox;
		private System.Windows.Forms.Label SerialNumberLabel;
		private System.Windows.Forms.ContextMenuStrip DialogMenu;
		private System.Windows.Forms.ToolStripMenuItem DialogMenuHelp;
	}
}