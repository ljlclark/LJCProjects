namespace LJCFacilityManager
{
	partial class FacilityDetail
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
			this.CodeLabel = new System.Windows.Forms.Label();
			this.CodeTextbox = new System.Windows.Forms.TextBox();
			this.FacilityTypeCombo = new LJCFacilityManager.CodeTypeCombo();
			this.DescriptionTextbox = new System.Windows.Forms.TextBox();
			this.DescriptionLabel = new System.Windows.Forms.Label();
			this.FacilityTypeLabel = new System.Windows.Forms.Label();
			this.OKButton = new System.Windows.Forms.Button();
			this.FormCancelButton = new System.Windows.Forms.Button();
			this.DialogMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.DialogMenuHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.DialogMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// CodeLabel
			// 
			this.CodeLabel.Location = new System.Drawing.Point(18, 23);
			this.CodeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.CodeLabel.Name = "CodeLabel";
			this.CodeLabel.Size = new System.Drawing.Size(123, 20);
			this.CodeLabel.TabIndex = 0;
			this.CodeLabel.Text = "Code";
			// 
			// CodeTextbox
			// 
			this.CodeTextbox.Location = new System.Drawing.Point(142, 18);
			this.CodeTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.CodeTextbox.Name = "CodeTextbox";
			this.CodeTextbox.Size = new System.Drawing.Size(180, 26);
			this.CodeTextbox.TabIndex = 1;
			// 
			// FacilityTypeCombo
			// 
			this.FacilityTypeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.FacilityTypeCombo.FormattingEnabled = true;
			this.FacilityTypeCombo.Location = new System.Drawing.Point(142, 98);
			this.FacilityTypeCombo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.FacilityTypeCombo.Name = "FacilityTypeCombo";
			this.FacilityTypeCombo.Size = new System.Drawing.Size(433, 28);
			this.FacilityTypeCombo.TabIndex = 5;
			// 
			// DescriptionTextbox
			// 
			this.DescriptionTextbox.Location = new System.Drawing.Point(142, 58);
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
			this.DescriptionLabel.Size = new System.Drawing.Size(123, 20);
			this.DescriptionLabel.TabIndex = 2;
			this.DescriptionLabel.Text = "Description";
			// 
			// FacilityTypeLabel
			// 
			this.FacilityTypeLabel.Location = new System.Drawing.Point(18, 103);
			this.FacilityTypeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.FacilityTypeLabel.Name = "FacilityTypeLabel";
			this.FacilityTypeLabel.Size = new System.Drawing.Size(123, 20);
			this.FacilityTypeLabel.TabIndex = 4;
			this.FacilityTypeLabel.Text = "Facility Type";
			// 
			// OKButton
			// 
			this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OKButton.Location = new System.Drawing.Point(344, 142);
			this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(112, 35);
			this.OKButton.TabIndex = 6;
			this.OKButton.Text = "&OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// FormCancelButton
			// 
			this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.FormCancelButton.Location = new System.Drawing.Point(465, 142);
			this.FormCancelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.FormCancelButton.Name = "FormCancelButton";
			this.FormCancelButton.Size = new System.Drawing.Size(112, 35);
			this.FormCancelButton.TabIndex = 7;
			this.FormCancelButton.Text = "Cancel";
			this.FormCancelButton.UseVisualStyleBackColor = true;
			this.FormCancelButton.Click += new System.EventHandler(this.FormCancelButton_Click);
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
			// FacilityDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(596, 188);
			this.ContextMenuStrip = this.DialogMenu;
			this.Controls.Add(this.FormCancelButton);
			this.Controls.Add(this.OKButton);
			this.Controls.Add(this.FacilityTypeLabel);
			this.Controls.Add(this.DescriptionTextbox);
			this.Controls.Add(this.DescriptionLabel);
			this.Controls.Add(this.FacilityTypeCombo);
			this.Controls.Add(this.CodeTextbox);
			this.Controls.Add(this.CodeLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FacilityDetail";
			this.ShowInTaskbar = false;
			this.Text = "Facility Detail";
			this.Load += new System.EventHandler(this.FacilityDetail_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FacilityDetail_KeyDown);
			this.DialogMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label CodeLabel;
		private System.Windows.Forms.TextBox CodeTextbox;
		private LJCFacilityManager.CodeTypeCombo FacilityTypeCombo;
		private System.Windows.Forms.TextBox DescriptionTextbox;
		private System.Windows.Forms.Label DescriptionLabel;
		private System.Windows.Forms.Label FacilityTypeLabel;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.Button FormCancelButton;
		private System.Windows.Forms.ContextMenuStrip DialogMenu;
		private System.Windows.Forms.ToolStripMenuItem DialogMenuHelp;
	}
}