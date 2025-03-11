namespace LJCAppManager
{
	partial class ModuleDetail
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
			this.FormCancelButton = new System.Windows.Forms.Button();
			this.OKButton = new System.Windows.Forms.Button();
			this.ParentNameTextbox = new System.Windows.Forms.TextBox();
			this.ParentNameLabel = new System.Windows.Forms.Label();
			this.TitleTextbox = new System.Windows.Forms.TextBox();
			this.TitleLabel = new System.Windows.Forms.Label();
			this.ModuleNameTextbox = new System.Windows.Forms.TextBox();
			this.ModuleNameLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// FormCancelButton
			// 
			this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.FormCancelButton.Location = new System.Drawing.Point(456, 127);
			this.FormCancelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.FormCancelButton.Name = "FormCancelButton";
			this.FormCancelButton.Size = new System.Drawing.Size(112, 35);
			this.FormCancelButton.TabIndex = 7;
			this.FormCancelButton.Text = "Cancel";
			this.FormCancelButton.UseVisualStyleBackColor = true;
			this.FormCancelButton.Click += new System.EventHandler(this.FormCancelButton_Click);
			// 
			// OKButton
			// 
			this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OKButton.Location = new System.Drawing.Point(335, 127);
			this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(112, 35);
			this.OKButton.TabIndex = 6;
			this.OKButton.Text = "&OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
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
			// ParentNameLabel
			// 
			this.ParentNameLabel.Location = new System.Drawing.Point(10, 23);
			this.ParentNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ParentNameLabel.Name = "ParentNameLabel";
			this.ParentNameLabel.Size = new System.Drawing.Size(123, 20);
			this.ParentNameLabel.TabIndex = 0;
			this.ParentNameLabel.Text = "App Program";
			// 
			// TitleTextbox
			// 
			this.TitleTextbox.Location = new System.Drawing.Point(135, 90);
			this.TitleTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.TitleTextbox.Name = "TitleTextbox";
			this.TitleTextbox.Size = new System.Drawing.Size(433, 26);
			this.TitleTextbox.TabIndex = 5;
			// 
			// TitleLabel
			// 
			this.TitleLabel.Location = new System.Drawing.Point(10, 95);
			this.TitleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.TitleLabel.Name = "TitleLabel";
			this.TitleLabel.Size = new System.Drawing.Size(123, 20);
			this.TitleLabel.TabIndex = 4;
			this.TitleLabel.Text = "Title";
			// 
			// ModuleNameTextbox
			// 
			this.ModuleNameTextbox.Location = new System.Drawing.Point(135, 54);
			this.ModuleNameTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ModuleNameTextbox.Name = "ModuleNameTextbox";
			this.ModuleNameTextbox.Size = new System.Drawing.Size(433, 26);
			this.ModuleNameTextbox.TabIndex = 3;
			// 
			// ModuleNameLabel
			// 
			this.ModuleNameLabel.Location = new System.Drawing.Point(10, 59);
			this.ModuleNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ModuleNameLabel.Name = "ModuleNameLabel";
			this.ModuleNameLabel.Size = new System.Drawing.Size(123, 20);
			this.ModuleNameLabel.TabIndex = 2;
			this.ModuleNameLabel.Text = "Module Name";
			// 
			// ModuleDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(580, 172);
			this.Controls.Add(this.FormCancelButton);
			this.Controls.Add(this.OKButton);
			this.Controls.Add(this.ParentNameTextbox);
			this.Controls.Add(this.ParentNameLabel);
			this.Controls.Add(this.TitleTextbox);
			this.Controls.Add(this.TitleLabel);
			this.Controls.Add(this.ModuleNameTextbox);
			this.Controls.Add(this.ModuleNameLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.Name = "ModuleDetail";
			this.Text = "Module Detail";
			this.Load += new System.EventHandler(this.ModuleDetail_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ModuleDetail_KeyDown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button FormCancelButton;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.TextBox ParentNameTextbox;
		private System.Windows.Forms.Label ParentNameLabel;
		private System.Windows.Forms.TextBox TitleTextbox;
		private System.Windows.Forms.Label TitleLabel;
		private System.Windows.Forms.TextBox ModuleNameTextbox;
		private System.Windows.Forms.Label ModuleNameLabel;
	}
}