namespace LJCUnitMeasure
{
	partial class UnitTypeDetail
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
			this.CodeText = new System.Windows.Forms.TextBox();
			this.CodeLabel = new System.Windows.Forms.Label();
			this.FormCancelButton = new System.Windows.Forms.Button();
			this.OKButton = new System.Windows.Forms.Button();
			this.NameText = new System.Windows.Forms.TextBox();
			this.NameLabel = new System.Windows.Forms.Label();
			this.CategoryText = new System.Windows.Forms.TextBox();
			this.CategoryLabel = new System.Windows.Forms.Label();
			this.DialogMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.DialogMenuHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.DialogMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// CodeText
			// 
			this.CodeText.Location = new System.Drawing.Point(150, 54);
			this.CodeText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.CodeText.Name = "CodeText";
			this.CodeText.Size = new System.Drawing.Size(77, 26);
			this.CodeText.TabIndex = 13;
			this.CodeText.TextChanged += new System.EventHandler(this.CodeText_TextChanged);
			this.CodeText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CodeText_KeyPress);
			// 
			// CodeLabel
			// 
			this.CodeLabel.Location = new System.Drawing.Point(18, 57);
			this.CodeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.CodeLabel.Name = "CodeLabel";
			this.CodeLabel.Size = new System.Drawing.Size(130, 20);
			this.CodeLabel.TabIndex = 12;
			this.CodeLabel.Text = "Code";
			// 
			// FormCancelButton
			// 
			this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.FormCancelButton.Location = new System.Drawing.Point(453, 126);
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
			this.OKButton.Location = new System.Drawing.Point(332, 126);
			this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(112, 35);
			this.OKButton.TabIndex = 16;
			this.OKButton.Text = "&OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// NameText
			// 
			this.NameText.Location = new System.Drawing.Point(150, 90);
			this.NameText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.NameText.Name = "NameText";
			this.NameText.Size = new System.Drawing.Size(415, 26);
			this.NameText.TabIndex = 15;
			// 
			// NameLabel
			// 
			this.NameLabel.Location = new System.Drawing.Point(18, 93);
			this.NameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.NameLabel.Name = "NameLabel";
			this.NameLabel.Size = new System.Drawing.Size(130, 20);
			this.NameLabel.TabIndex = 14;
			this.NameLabel.Text = "Name";
			// 
			// CategoryText
			// 
			this.CategoryText.Location = new System.Drawing.Point(150, 18);
			this.CategoryText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.CategoryText.Name = "CategoryText";
			this.CategoryText.ReadOnly = true;
			this.CategoryText.Size = new System.Drawing.Size(415, 26);
			this.CategoryText.TabIndex = 19;
			// 
			// CategoryLabel
			// 
			this.CategoryLabel.Location = new System.Drawing.Point(18, 21);
			this.CategoryLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.CategoryLabel.Name = "CategoryLabel";
			this.CategoryLabel.Size = new System.Drawing.Size(130, 20);
			this.CategoryLabel.TabIndex = 18;
			this.CategoryLabel.Text = "Category";
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
			this.DialogMenuHelp.Size = new System.Drawing.Size(121, 30);
			this.DialogMenuHelp.Text = "&Help";
			this.DialogMenuHelp.Click += new System.EventHandler(this.DialogMenuHelp_Click);
			// 
			// UnitTypeDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(578, 171);
			this.ContextMenuStrip = this.DialogMenu;
			this.Controls.Add(this.CategoryText);
			this.Controls.Add(this.CategoryLabel);
			this.Controls.Add(this.CodeText);
			this.Controls.Add(this.CodeLabel);
			this.Controls.Add(this.FormCancelButton);
			this.Controls.Add(this.OKButton);
			this.Controls.Add(this.NameText);
			this.Controls.Add(this.NameLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "UnitTypeDetail";
			this.ShowInTaskbar = false;
			this.Text = "UnitTypeDetail";
			this.Load += new System.EventHandler(this.UnitTypeDetail_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UnitTypeDetail_KeyDown);
			this.DialogMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox CodeText;
		private System.Windows.Forms.Label CodeLabel;
		private System.Windows.Forms.Button FormCancelButton;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.TextBox NameText;
		private System.Windows.Forms.Label NameLabel;
		private System.Windows.Forms.TextBox CategoryText;
		private System.Windows.Forms.Label CategoryLabel;
		private System.Windows.Forms.ContextMenuStrip DialogMenu;
		private System.Windows.Forms.ToolStripMenuItem DialogMenuHelp;
	}
}