namespace LJCAppManager
{
	partial class Logon
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
			this.PasswordTextbox = new System.Windows.Forms.TextBox();
			this.PasswordLabel = new System.Windows.Forms.Label();
			this.UserIDTextbox = new System.Windows.Forms.TextBox();
			this.UserIDLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// FormCancelButton
			// 
			this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.FormCancelButton.Location = new System.Drawing.Point(264, 100);
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
			this.OKButton.Location = new System.Drawing.Point(142, 100);
			this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(112, 35);
			this.OKButton.TabIndex = 4;
			this.OKButton.Text = "&OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// PasswordTextbox
			// 
			this.PasswordTextbox.Location = new System.Drawing.Point(142, 58);
			this.PasswordTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.PasswordTextbox.Name = "PasswordTextbox";
			this.PasswordTextbox.PasswordChar = '*';
			this.PasswordTextbox.Size = new System.Drawing.Size(232, 26);
			this.PasswordTextbox.TabIndex = 3;
			// 
			// PasswordLabel
			// 
			this.PasswordLabel.Location = new System.Drawing.Point(18, 63);
			this.PasswordLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.PasswordLabel.Name = "PasswordLabel";
			this.PasswordLabel.Size = new System.Drawing.Size(123, 20);
			this.PasswordLabel.TabIndex = 2;
			this.PasswordLabel.Text = "Password";
			// 
			// UserIDTextbox
			// 
			this.UserIDTextbox.Location = new System.Drawing.Point(142, 18);
			this.UserIDTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.UserIDTextbox.Name = "UserIDTextbox";
			this.UserIDTextbox.Size = new System.Drawing.Size(232, 26);
			this.UserIDTextbox.TabIndex = 1;
			// 
			// UserIDLabel
			// 
			this.UserIDLabel.Location = new System.Drawing.Point(18, 23);
			this.UserIDLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.UserIDLabel.Name = "UserIDLabel";
			this.UserIDLabel.Size = new System.Drawing.Size(123, 20);
			this.UserIDLabel.TabIndex = 0;
			this.UserIDLabel.Text = "User ID";
			// 
			// Logon
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(394, 146);
			this.Controls.Add(this.PasswordTextbox);
			this.Controls.Add(this.PasswordLabel);
			this.Controls.Add(this.UserIDTextbox);
			this.Controls.Add(this.UserIDLabel);
			this.Controls.Add(this.FormCancelButton);
			this.Controls.Add(this.OKButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Logon";
			this.ShowInTaskbar = false;
			this.Text = "Application Manager Logon";
			this.Load += new System.EventHandler(this.Logon_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button FormCancelButton;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.TextBox PasswordTextbox;
		private System.Windows.Forms.Label PasswordLabel;
		private System.Windows.Forms.TextBox UserIDTextbox;
		private System.Windows.Forms.Label UserIDLabel;
	}
}