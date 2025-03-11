namespace LJCAppManager
{
	partial class ProgramDetail
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
			this.TitleTextbox = new System.Windows.Forms.TextBox();
			this.TitleLabel = new System.Windows.Forms.Label();
			this.FileNameTextbox = new System.Windows.Forms.TextBox();
			this.FileNameLabel = new System.Windows.Forms.Label();
			this.FormCancelButton = new System.Windows.Forms.Button();
			this.OKButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// TitleTextbox
			// 
			this.TitleTextbox.Location = new System.Drawing.Point(135, 54);
			this.TitleTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.TitleTextbox.Name = "TitleTextbox";
			this.TitleTextbox.Size = new System.Drawing.Size(433, 26);
			this.TitleTextbox.TabIndex = 3;
			// 
			// TitleLabel
			// 
			this.TitleLabel.Location = new System.Drawing.Point(10, 59);
			this.TitleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.TitleLabel.Name = "TitleLabel";
			this.TitleLabel.Size = new System.Drawing.Size(123, 20);
			this.TitleLabel.TabIndex = 2;
			this.TitleLabel.Text = "Title";
			// 
			// FileNameTextbox
			// 
			this.FileNameTextbox.Location = new System.Drawing.Point(135, 18);
			this.FileNameTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.FileNameTextbox.Name = "FileNameTextbox";
			this.FileNameTextbox.Size = new System.Drawing.Size(180, 26);
			this.FileNameTextbox.TabIndex = 1;
			// 
			// FileNameLabel
			// 
			this.FileNameLabel.Location = new System.Drawing.Point(10, 23);
			this.FileNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.FileNameLabel.Name = "FileNameLabel";
			this.FileNameLabel.Size = new System.Drawing.Size(123, 20);
			this.FileNameLabel.TabIndex = 0;
			this.FileNameLabel.Text = "File Name";
			// 
			// FormCancelButton
			// 
			this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.FormCancelButton.Location = new System.Drawing.Point(457, 91);
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
			this.OKButton.Location = new System.Drawing.Point(336, 91);
			this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(112, 35);
			this.OKButton.TabIndex = 4;
			this.OKButton.Text = "&OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// ProgramDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(580, 136);
			this.Controls.Add(this.FormCancelButton);
			this.Controls.Add(this.OKButton);
			this.Controls.Add(this.TitleTextbox);
			this.Controls.Add(this.TitleLabel);
			this.Controls.Add(this.FileNameTextbox);
			this.Controls.Add(this.FileNameLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ProgramDetail";
			this.Text = "Program Detail";
			this.Load += new System.EventHandler(this.ProgramDetail_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProgramDetail_KeyDown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.TextBox TitleTextbox;
		private System.Windows.Forms.Label TitleLabel;
		private System.Windows.Forms.TextBox FileNameTextbox;
		private System.Windows.Forms.Label FileNameLabel;
		private System.Windows.Forms.Button FormCancelButton;
		private System.Windows.Forms.Button OKButton;
	}
}