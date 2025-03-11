namespace LJCTransformManager
{
	partial class ProcessGroupDetail
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
			this.GroupDescriptionTextbox = new System.Windows.Forms.TextBox();
			this.GroupDescriptionLabel = new System.Windows.Forms.Label();
			this.GroupNameTextbox = new System.Windows.Forms.TextBox();
			this.GroupNameLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
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
			// GroupDescriptionTextbox
			// 
			this.GroupDescriptionTextbox.Location = new System.Drawing.Point(137, 50);
			this.GroupDescriptionTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.GroupDescriptionTextbox.Name = "GroupDescriptionTextbox";
			this.GroupDescriptionTextbox.Size = new System.Drawing.Size(417, 26);
			this.GroupDescriptionTextbox.TabIndex = 3;
			// 
			// GroupDescriptionLabel
			// 
			this.GroupDescriptionLabel.Location = new System.Drawing.Point(12, 53);
			this.GroupDescriptionLabel.Name = "GroupDescriptionLabel";
			this.GroupDescriptionLabel.Size = new System.Drawing.Size(123, 20);
			this.GroupDescriptionLabel.TabIndex = 2;
			this.GroupDescriptionLabel.Text = "Description";
			// 
			// GroupNameTextbox
			// 
			this.GroupNameTextbox.Location = new System.Drawing.Point(137, 14);
			this.GroupNameTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.GroupNameTextbox.MaxLength = 100;
			this.GroupNameTextbox.Name = "GroupNameTextbox";
			this.GroupNameTextbox.Size = new System.Drawing.Size(417, 26);
			this.GroupNameTextbox.TabIndex = 1;
			this.GroupNameTextbox.TextChanged += new System.EventHandler(this.GroupNameTextbox_TextChanged);
			this.GroupNameTextbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GroupNameTextbox_KeyPress);
			// 
			// GroupNameLabel
			// 
			this.GroupNameLabel.Location = new System.Drawing.Point(12, 17);
			this.GroupNameLabel.Name = "GroupNameLabel";
			this.GroupNameLabel.Size = new System.Drawing.Size(123, 20);
			this.GroupNameLabel.TabIndex = 0;
			this.GroupNameLabel.Text = "Process Group";
			// 
			// ProcessGroupDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(563, 135);
			this.Controls.Add(this.GroupDescriptionTextbox);
			this.Controls.Add(this.GroupDescriptionLabel);
			this.Controls.Add(this.GroupNameTextbox);
			this.Controls.Add(this.GroupNameLabel);
			this.Controls.Add(this.FormCancelButton);
			this.Controls.Add(this.OKButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ProcessGroupDetail";
			this.ShowInTaskbar = false;
			this.Text = "Group Process Detail";
			this.Load += new System.EventHandler(this.ProcessGroupDetail_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button FormCancelButton;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.TextBox GroupDescriptionTextbox;
		private System.Windows.Forms.Label GroupDescriptionLabel;
		private System.Windows.Forms.TextBox GroupNameTextbox;
		private System.Windows.Forms.Label GroupNameLabel;
	}
}