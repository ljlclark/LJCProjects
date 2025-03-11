namespace LJCTransformManager
{
	partial class LayoutDetail
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
			this.LayoutDescriptionTextbox = new System.Windows.Forms.TextBox();
			this.LayoutDescriptionLabel = new System.Windows.Forms.Label();
			this.LayoutNameTextbox = new System.Windows.Forms.TextBox();
			this.LayoutNameLabel = new System.Windows.Forms.Label();
			this.FormCancelButton = new System.Windows.Forms.Button();
			this.OKButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// LayoutDescriptionTextbox
			// 
			this.LayoutDescriptionTextbox.Location = new System.Drawing.Point(137, 50);
			this.LayoutDescriptionTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.LayoutDescriptionTextbox.Name = "LayoutDescriptionTextbox";
			this.LayoutDescriptionTextbox.Size = new System.Drawing.Size(417, 26);
			this.LayoutDescriptionTextbox.TabIndex = 3;
			// 
			// LayoutDescriptionLabel
			// 
			this.LayoutDescriptionLabel.Location = new System.Drawing.Point(12, 53);
			this.LayoutDescriptionLabel.Name = "LayoutDescriptionLabel";
			this.LayoutDescriptionLabel.Size = new System.Drawing.Size(123, 20);
			this.LayoutDescriptionLabel.TabIndex = 2;
			this.LayoutDescriptionLabel.Text = "Description";
			// 
			// LayoutNameTextbox
			// 
			this.LayoutNameTextbox.Location = new System.Drawing.Point(137, 14);
			this.LayoutNameTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.LayoutNameTextbox.MaxLength = 100;
			this.LayoutNameTextbox.Name = "LayoutNameTextbox";
			this.LayoutNameTextbox.Size = new System.Drawing.Size(417, 26);
			this.LayoutNameTextbox.TabIndex = 1;
			this.LayoutNameTextbox.TextChanged += new System.EventHandler(this.LayoutNameTextbox_TextChanged);
			this.LayoutNameTextbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LayoutNameTextbox_KeyPress);
			// 
			// LayoutNameLabel
			// 
			this.LayoutNameLabel.Location = new System.Drawing.Point(12, 17);
			this.LayoutNameLabel.Name = "LayoutNameLabel";
			this.LayoutNameLabel.Size = new System.Drawing.Size(123, 20);
			this.LayoutNameLabel.TabIndex = 0;
			this.LayoutNameLabel.Text = "Layout";
			// 
			// FormCancelButton
			// 
			this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.FormCancelButton.Location = new System.Drawing.Point(442, 86);
			this.FormCancelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.FormCancelButton.Name = "FormCancelButton";
			this.FormCancelButton.Size = new System.Drawing.Size(112, 35);
			this.FormCancelButton.TabIndex = 5;
			this.FormCancelButton.Text = "Cancel";
			this.FormCancelButton.UseVisualStyleBackColor = true;
			this.FormCancelButton.Click += new System.EventHandler(this.FormCancelButton_Click);
			// 
			// OKButton
			// 
			this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OKButton.Location = new System.Drawing.Point(322, 86);
			this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(112, 35);
			this.OKButton.TabIndex = 4;
			this.OKButton.Text = "&OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// LayoutDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(563, 135);
			this.Controls.Add(this.LayoutDescriptionTextbox);
			this.Controls.Add(this.LayoutDescriptionLabel);
			this.Controls.Add(this.LayoutNameTextbox);
			this.Controls.Add(this.LayoutNameLabel);
			this.Controls.Add(this.FormCancelButton);
			this.Controls.Add(this.OKButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "LayoutDetail";
			this.Text = "Layout Detail";
			this.Load += new System.EventHandler(this.LayoutDetail_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox LayoutDescriptionTextbox;
		private System.Windows.Forms.Label LayoutDescriptionLabel;
		private System.Windows.Forms.TextBox LayoutNameTextbox;
		private System.Windows.Forms.Label LayoutNameLabel;
		private System.Windows.Forms.Button FormCancelButton;
		private System.Windows.Forms.Button OKButton;
	}
}