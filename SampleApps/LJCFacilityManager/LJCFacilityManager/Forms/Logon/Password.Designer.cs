namespace LJCFacilityManager
{
	partial class Password
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
			this.NewTextbox = new System.Windows.Forms.TextBox();
			this.NewLabel = new System.Windows.Forms.Label();
			this.OldTextbox = new System.Windows.Forms.TextBox();
			this.OldLabel = new System.Windows.Forms.Label();
			this.FormCancelButton = new System.Windows.Forms.Button();
			this.OKButton = new System.Windows.Forms.Button();
			this.ReEnterTextbox = new System.Windows.Forms.TextBox();
			this.ReEnterLabel = new System.Windows.Forms.Label();
			this.UserIDTextbox = new System.Windows.Forms.TextBox();
			this.UserIDLabel = new System.Windows.Forms.Label();
			this.AdminCheckbox = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// NewTextbox
			// 
			this.NewTextbox.Location = new System.Drawing.Point(158, 138);
			this.NewTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.NewTextbox.Name = "NewTextbox";
			this.NewTextbox.PasswordChar = '*';
			this.NewTextbox.Size = new System.Drawing.Size(232, 26);
			this.NewTextbox.TabIndex = 6;
			// 
			// NewLabel
			// 
			this.NewLabel.Location = new System.Drawing.Point(18, 143);
			this.NewLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.NewLabel.Name = "NewLabel";
			this.NewLabel.Size = new System.Drawing.Size(138, 20);
			this.NewLabel.TabIndex = 5;
			this.NewLabel.Text = "New Password";
			// 
			// OldTextbox
			// 
			this.OldTextbox.Location = new System.Drawing.Point(158, 98);
			this.OldTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.OldTextbox.Name = "OldTextbox";
			this.OldTextbox.PasswordChar = '*';
			this.OldTextbox.Size = new System.Drawing.Size(232, 26);
			this.OldTextbox.TabIndex = 4;
			// 
			// OldLabel
			// 
			this.OldLabel.Location = new System.Drawing.Point(18, 103);
			this.OldLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.OldLabel.Name = "OldLabel";
			this.OldLabel.Size = new System.Drawing.Size(138, 20);
			this.OldLabel.TabIndex = 3;
			this.OldLabel.Text = "Password";
			// 
			// FormCancelButton
			// 
			this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.FormCancelButton.Location = new System.Drawing.Point(279, 220);
			this.FormCancelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.FormCancelButton.Name = "FormCancelButton";
			this.FormCancelButton.Size = new System.Drawing.Size(112, 35);
			this.FormCancelButton.TabIndex = 10;
			this.FormCancelButton.Text = "Cancel";
			this.FormCancelButton.UseVisualStyleBackColor = true;
			// 
			// OKButton
			// 
			this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OKButton.Location = new System.Drawing.Point(158, 220);
			this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(112, 35);
			this.OKButton.TabIndex = 9;
			this.OKButton.Text = "&OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// ReEnterTextbox
			// 
			this.ReEnterTextbox.Location = new System.Drawing.Point(158, 178);
			this.ReEnterTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ReEnterTextbox.Name = "ReEnterTextbox";
			this.ReEnterTextbox.PasswordChar = '*';
			this.ReEnterTextbox.Size = new System.Drawing.Size(232, 26);
			this.ReEnterTextbox.TabIndex = 8;
			// 
			// ReEnterLabel
			// 
			this.ReEnterLabel.Location = new System.Drawing.Point(18, 183);
			this.ReEnterLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ReEnterLabel.Name = "ReEnterLabel";
			this.ReEnterLabel.Size = new System.Drawing.Size(138, 20);
			this.ReEnterLabel.TabIndex = 7;
			this.ReEnterLabel.Text = "Re-enter New";
			// 
			// UserIDTextbox
			// 
			this.UserIDTextbox.Location = new System.Drawing.Point(158, 18);
			this.UserIDTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.UserIDTextbox.Name = "UserIDTextbox";
			this.UserIDTextbox.ReadOnly = true;
			this.UserIDTextbox.Size = new System.Drawing.Size(232, 26);
			this.UserIDTextbox.TabIndex = 1;
			// 
			// UserIDLabel
			// 
			this.UserIDLabel.Location = new System.Drawing.Point(18, 23);
			this.UserIDLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.UserIDLabel.Name = "UserIDLabel";
			this.UserIDLabel.Size = new System.Drawing.Size(138, 20);
			this.UserIDLabel.TabIndex = 0;
			this.UserIDLabel.Text = "User ID";
			// 
			// AdminCheckbox
			// 
			this.AdminCheckbox.AutoSize = true;
			this.AdminCheckbox.Enabled = false;
			this.AdminCheckbox.Location = new System.Drawing.Point(165, 58);
			this.AdminCheckbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.AdminCheckbox.Name = "AdminCheckbox";
			this.AdminCheckbox.Size = new System.Drawing.Size(129, 24);
			this.AdminCheckbox.TabIndex = 2;
			this.AdminCheckbox.Text = "Administrator";
			this.AdminCheckbox.UseVisualStyleBackColor = true;
			// 
			// Password
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(410, 266);
			this.Controls.Add(this.AdminCheckbox);
			this.Controls.Add(this.UserIDTextbox);
			this.Controls.Add(this.UserIDLabel);
			this.Controls.Add(this.ReEnterTextbox);
			this.Controls.Add(this.ReEnterLabel);
			this.Controls.Add(this.NewTextbox);
			this.Controls.Add(this.NewLabel);
			this.Controls.Add(this.OldTextbox);
			this.Controls.Add(this.OldLabel);
			this.Controls.Add(this.FormCancelButton);
			this.Controls.Add(this.OKButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Password";
			this.ShowInTaskbar = false;
			this.Text = "Password";
			this.Load += new System.EventHandler(this.Password_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox NewTextbox;
		private System.Windows.Forms.Label NewLabel;
		private System.Windows.Forms.TextBox OldTextbox;
		private System.Windows.Forms.Label OldLabel;
		private System.Windows.Forms.Button FormCancelButton;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.TextBox ReEnterTextbox;
		private System.Windows.Forms.Label ReEnterLabel;
		private System.Windows.Forms.TextBox UserIDTextbox;
		private System.Windows.Forms.Label UserIDLabel;
		private System.Windows.Forms.CheckBox AdminCheckbox;
	}
}