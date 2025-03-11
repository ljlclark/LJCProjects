namespace LJCAppManager
{
	partial class UserDetail
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
			this.UserNameTextbox = new System.Windows.Forms.TextBox();
			this.UserNameLabel = new System.Windows.Forms.Label();
			this.UserIDTextbox = new System.Windows.Forms.TextBox();
			this.UserIDLabel = new System.Windows.Forms.Label();
			this.FormCancelButton = new System.Windows.Forms.Button();
			this.OKButton = new System.Windows.Forms.Button();
			this.PersonPicture = new LJCAppManager.LJCPictureBox();
			this.PictureMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.PictureMenuImport = new System.Windows.Forms.ToolStripMenuItem();
			this.PictureMenuSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.PictureMenuRotate = new System.Windows.Forms.ToolStripMenuItem();
			this.PictureMenuRotateLeft = new System.Windows.Forms.ToolStripMenuItem();
			this.PictureMenuRotateRight = new System.Windows.Forms.ToolStripMenuItem();
			this.PictureMenuCrop = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.PictureMenuSave = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.PersonPicture)).BeginInit();
			this.PictureMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// UserNameTextbox
			// 
			this.UserNameTextbox.Location = new System.Drawing.Point(172, 18);
			this.UserNameTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.UserNameTextbox.Name = "UserNameTextbox";
			this.UserNameTextbox.Size = new System.Drawing.Size(433, 26);
			this.UserNameTextbox.TabIndex = 1;
			// 
			// UserNameLabel
			// 
			this.UserNameLabel.Location = new System.Drawing.Point(18, 23);
			this.UserNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.UserNameLabel.Name = "UserNameLabel";
			this.UserNameLabel.Size = new System.Drawing.Size(153, 20);
			this.UserNameLabel.TabIndex = 0;
			this.UserNameLabel.Text = "User Name";
			// 
			// UserIDTextbox
			// 
			this.UserIDTextbox.Location = new System.Drawing.Point(172, 58);
			this.UserIDTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.UserIDTextbox.Name = "UserIDTextbox";
			this.UserIDTextbox.Size = new System.Drawing.Size(433, 26);
			this.UserIDTextbox.TabIndex = 3;
			// 
			// UserIDLabel
			// 
			this.UserIDLabel.Location = new System.Drawing.Point(18, 63);
			this.UserIDLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.UserIDLabel.Name = "UserIDLabel";
			this.UserIDLabel.Size = new System.Drawing.Size(153, 20);
			this.UserIDLabel.TabIndex = 2;
			this.UserIDLabel.Text = "User ID";
			// 
			// FormCancelButton
			// 
			this.FormCancelButton.Location = new System.Drawing.Point(493, 96);
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
			this.OKButton.Location = new System.Drawing.Point(372, 96);
			this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(112, 35);
			this.OKButton.TabIndex = 4;
			this.OKButton.Text = "&OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// PersonPicture
			// 
			this.PersonPicture.ContextMenuStrip = this.PictureMenu;
			this.PersonPicture.Location = new System.Drawing.Point(620, 18);
			this.PersonPicture.Name = "PersonPicture";
			this.PersonPicture.Size = new System.Drawing.Size(267, 412);
			this.PersonPicture.TabIndex = 6;
			this.PersonPicture.TabStop = false;
			// 
			// PictureMenu
			// 
			this.PictureMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.PictureMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PictureMenuImport,
            this.PictureMenuSeparator,
            this.PictureMenuRotate,
            this.PictureMenuCrop,
            this.toolStripSeparator1,
            this.PictureMenuSave});
			this.PictureMenu.Name = "PictureMenu";
			this.PictureMenu.Size = new System.Drawing.Size(140, 136);
			// 
			// PictureMenuImport
			// 
			this.PictureMenuImport.Name = "PictureMenuImport";
			this.PictureMenuImport.Size = new System.Drawing.Size(139, 30);
			this.PictureMenuImport.Text = "&Import";
			this.PictureMenuImport.Click += new System.EventHandler(this.PictureMenuImport_Click);
			// 
			// PictureMenuSeparator
			// 
			this.PictureMenuSeparator.Name = "PictureMenuSeparator";
			this.PictureMenuSeparator.Size = new System.Drawing.Size(136, 6);
			// 
			// PictureMenuRotate
			// 
			this.PictureMenuRotate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PictureMenuRotateLeft,
            this.PictureMenuRotateRight});
			this.PictureMenuRotate.Name = "PictureMenuRotate";
			this.PictureMenuRotate.Size = new System.Drawing.Size(139, 30);
			this.PictureMenuRotate.Text = "&Rotate";
			// 
			// PictureMenuRotateLeft
			// 
			this.PictureMenuRotateLeft.Name = "PictureMenuRotateLeft";
			this.PictureMenuRotateLeft.Size = new System.Drawing.Size(233, 30);
			this.PictureMenuRotateLeft.Text = "90 Degrees &Left";
			this.PictureMenuRotateLeft.Click += new System.EventHandler(this.PictureMenuRotateLeft_Click);
			// 
			// PictureMenuRotateRight
			// 
			this.PictureMenuRotateRight.Name = "PictureMenuRotateRight";
			this.PictureMenuRotateRight.Size = new System.Drawing.Size(233, 30);
			this.PictureMenuRotateRight.Text = "90 Degrees &Right";
			this.PictureMenuRotateRight.Click += new System.EventHandler(this.PictureMenuRotateRight_Click);
			// 
			// PictureMenuCrop
			// 
			this.PictureMenuCrop.Name = "PictureMenuCrop";
			this.PictureMenuCrop.Size = new System.Drawing.Size(139, 30);
			this.PictureMenuCrop.Text = "&Crop";
			this.PictureMenuCrop.Click += new System.EventHandler(this.PictureMenuCrop_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(136, 6);
			// 
			// PictureMenuSave
			// 
			this.PictureMenuSave.Name = "PictureMenuSave";
			this.PictureMenuSave.Size = new System.Drawing.Size(139, 30);
			this.PictureMenuSave.Text = "&Save";
			this.PictureMenuSave.Click += new System.EventHandler(this.PictureMenuSave_Click);
			// 
			// UserDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(896, 438);
			this.Controls.Add(this.PersonPicture);
			this.Controls.Add(this.FormCancelButton);
			this.Controls.Add(this.OKButton);
			this.Controls.Add(this.UserNameTextbox);
			this.Controls.Add(this.UserNameLabel);
			this.Controls.Add(this.UserIDTextbox);
			this.Controls.Add(this.UserIDLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "UserDetail";
			this.Text = "User Detail";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UserDetail_FormClosing);
			this.Load += new System.EventHandler(this.UserDetail_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UserDetail_KeyDown);
			((System.ComponentModel.ISupportInitialize)(this.PersonPicture)).EndInit();
			this.PictureMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox UserNameTextbox;
		private System.Windows.Forms.Label UserNameLabel;
		private System.Windows.Forms.TextBox UserIDTextbox;
		private System.Windows.Forms.Label UserIDLabel;
		private System.Windows.Forms.Button FormCancelButton;
		private System.Windows.Forms.Button OKButton;
		private LJCAppManager.LJCPictureBox PersonPicture;
		private System.Windows.Forms.ContextMenuStrip PictureMenu;
		private System.Windows.Forms.ToolStripMenuItem PictureMenuRotate;
		private System.Windows.Forms.ToolStripMenuItem PictureMenuRotateLeft;
		private System.Windows.Forms.ToolStripMenuItem PictureMenuRotateRight;
		private System.Windows.Forms.ToolStripMenuItem PictureMenuCrop;
		private System.Windows.Forms.ToolStripMenuItem PictureMenuImport;
		private System.Windows.Forms.ToolStripSeparator PictureMenuSeparator;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem PictureMenuSave;
	}
}