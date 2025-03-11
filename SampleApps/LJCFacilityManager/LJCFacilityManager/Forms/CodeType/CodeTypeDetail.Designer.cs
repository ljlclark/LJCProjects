namespace LJCFacilityManager
{
	partial class CodeTypeDetail
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
			this.CodeTypeClassLabel = new System.Windows.Forms.Label();
			this.DescriptionTextbox = new System.Windows.Forms.TextBox();
			this.DescriptionLabel = new System.Windows.Forms.Label();
			this.CodeTypeClassCombo = new LJCFacilityManager.CodeTypeClassCombo();
			this.CodeTextbox = new System.Windows.Forms.TextBox();
			this.CodeLabel = new System.Windows.Forms.Label();
			this.DialogMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.DialogMenuHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.DialogMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// FormCancelButton
			// 
			this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.FormCancelButton.Location = new System.Drawing.Point(488, 140);
			this.FormCancelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.FormCancelButton.Name = "FormCancelButton";
			this.FormCancelButton.Size = new System.Drawing.Size(112, 35);
			this.FormCancelButton.TabIndex = 15;
			this.FormCancelButton.Text = "Cancel";
			this.FormCancelButton.UseVisualStyleBackColor = true;
			// 
			// OKButton
			// 
			this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OKButton.Location = new System.Drawing.Point(366, 140);
			this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(112, 35);
			this.OKButton.TabIndex = 14;
			this.OKButton.Text = "&OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// CodeTypeClassLabel
			// 
			this.CodeTypeClassLabel.Location = new System.Drawing.Point(18, 103);
			this.CodeTypeClassLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.CodeTypeClassLabel.Name = "CodeTypeClassLabel";
			this.CodeTypeClassLabel.Size = new System.Drawing.Size(146, 20);
			this.CodeTypeClassLabel.TabIndex = 12;
			this.CodeTypeClassLabel.Text = "Code Type Class";
			// 
			// DescriptionTextbox
			// 
			this.DescriptionTextbox.Location = new System.Drawing.Point(165, 58);
			this.DescriptionTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.DescriptionTextbox.Name = "DescriptionTextbox";
			this.DescriptionTextbox.Size = new System.Drawing.Size(433, 26);
			this.DescriptionTextbox.TabIndex = 11;
			// 
			// DescriptionLabel
			// 
			this.DescriptionLabel.Location = new System.Drawing.Point(18, 63);
			this.DescriptionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.DescriptionLabel.Name = "DescriptionLabel";
			this.DescriptionLabel.Size = new System.Drawing.Size(146, 20);
			this.DescriptionLabel.TabIndex = 10;
			this.DescriptionLabel.Text = "Description";
			// 
			// CodeTypeClassCombo
			// 
			this.CodeTypeClassCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CodeTypeClassCombo.FormattingEnabled = true;
			this.CodeTypeClassCombo.Location = new System.Drawing.Point(165, 98);
			this.CodeTypeClassCombo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.CodeTypeClassCombo.Name = "CodeTypeClassCombo";
			this.CodeTypeClassCombo.Size = new System.Drawing.Size(433, 28);
			this.CodeTypeClassCombo.TabIndex = 13;
			// 
			// CodeTextbox
			// 
			this.CodeTextbox.Location = new System.Drawing.Point(165, 18);
			this.CodeTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.CodeTextbox.Name = "CodeTextbox";
			this.CodeTextbox.Size = new System.Drawing.Size(180, 26);
			this.CodeTextbox.TabIndex = 9;
			// 
			// CodeLabel
			// 
			this.CodeLabel.Location = new System.Drawing.Point(18, 23);
			this.CodeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.CodeLabel.Name = "CodeLabel";
			this.CodeLabel.Size = new System.Drawing.Size(146, 20);
			this.CodeLabel.TabIndex = 8;
			this.CodeLabel.Text = "Code";
			// 
			// DialogMenu
			// 
			this.DialogMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.DialogMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DialogMenuHelp});
			this.DialogMenu.Name = "DialogMenu";
			this.DialogMenu.Size = new System.Drawing.Size(241, 67);
			// 
			// DialogMenuHelp
			// 
			this.DialogMenuHelp.Name = "DialogMenuHelp";
			this.DialogMenuHelp.Size = new System.Drawing.Size(240, 30);
			this.DialogMenuHelp.Text = "&Help";
			this.DialogMenuHelp.Click += new System.EventHandler(this.DialogMenuHelp_Click);
			// 
			// CodeTypeDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(618, 186);
			this.ContextMenuStrip = this.DialogMenu;
			this.Controls.Add(this.FormCancelButton);
			this.Controls.Add(this.OKButton);
			this.Controls.Add(this.CodeTypeClassLabel);
			this.Controls.Add(this.DescriptionTextbox);
			this.Controls.Add(this.DescriptionLabel);
			this.Controls.Add(this.CodeTypeClassCombo);
			this.Controls.Add(this.CodeTextbox);
			this.Controls.Add(this.CodeLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CodeTypeDetail";
			this.ShowInTaskbar = false;
			this.Text = "Code Type Detail";
			this.Load += new System.EventHandler(this.CodeTypeDetail_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CodeTypeDetail_KeyDown);
			this.DialogMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button FormCancelButton;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.Label CodeTypeClassLabel;
		private System.Windows.Forms.TextBox DescriptionTextbox;
		private System.Windows.Forms.Label DescriptionLabel;
		private CodeTypeClassCombo CodeTypeClassCombo;
		private System.Windows.Forms.TextBox CodeTextbox;
		private System.Windows.Forms.Label CodeLabel;
		private System.Windows.Forms.ContextMenuStrip DialogMenu;
		private System.Windows.Forms.ToolStripMenuItem DialogMenuHelp;
	}
}