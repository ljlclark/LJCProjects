namespace LJCDocGroupEditor
{
	partial class DocGenGroupDetail
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
			this.NameTextbox = new System.Windows.Forms.TextBox();
			this.NameLabel = new System.Windows.Forms.Label();
			this.DescriptionTextbox = new System.Windows.Forms.TextBox();
			this.DescriptionLabel = new System.Windows.Forms.Label();
			this.SequenceTextbox = new System.Windows.Forms.TextBox();
			this.SequenceLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// FormCancelButton
			// 
			this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.FormCancelButton.Location = new System.Drawing.Point(464, 125);
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
			this.OKButton.Location = new System.Drawing.Point(343, 125);
			this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(112, 35);
			this.OKButton.TabIndex = 6;
			this.OKButton.Text = "&OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// NameTextbox
			// 
			this.NameTextbox.Location = new System.Drawing.Point(142, 17);
			this.NameTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.NameTextbox.Name = "NameTextbox";
			this.NameTextbox.Size = new System.Drawing.Size(433, 26);
			this.NameTextbox.TabIndex = 1;
			this.NameTextbox.TextChanged += new System.EventHandler(this.NameTextbox_TextChanged);
			this.NameTextbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NameTextbox_KeyPress);
			// 
			// NameLabel
			// 
			this.NameLabel.Location = new System.Drawing.Point(18, 22);
			this.NameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.NameLabel.Name = "NameLabel";
			this.NameLabel.Size = new System.Drawing.Size(123, 20);
			this.NameLabel.TabIndex = 0;
			this.NameLabel.Text = "Name";
			// 
			// DescriptionTextbox
			// 
			this.DescriptionTextbox.Location = new System.Drawing.Point(142, 53);
			this.DescriptionTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.DescriptionTextbox.Name = "DescriptionTextbox";
			this.DescriptionTextbox.Size = new System.Drawing.Size(433, 26);
			this.DescriptionTextbox.TabIndex = 3;
			// 
			// DescriptionLabel
			// 
			this.DescriptionLabel.Location = new System.Drawing.Point(18, 58);
			this.DescriptionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.DescriptionLabel.Name = "DescriptionLabel";
			this.DescriptionLabel.Size = new System.Drawing.Size(123, 20);
			this.DescriptionLabel.TabIndex = 2;
			this.DescriptionLabel.Text = "Description";
			// 
			// SequenceTextbox
			// 
			this.SequenceTextbox.Location = new System.Drawing.Point(142, 89);
			this.SequenceTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.SequenceTextbox.Name = "SequenceTextbox";
			this.SequenceTextbox.Size = new System.Drawing.Size(54, 26);
			this.SequenceTextbox.TabIndex = 5;
			this.SequenceTextbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SequenceTextbox_KeyPress);
			// 
			// SequenceLabel
			// 
			this.SequenceLabel.Location = new System.Drawing.Point(18, 94);
			this.SequenceLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.SequenceLabel.Name = "SequenceLabel";
			this.SequenceLabel.Size = new System.Drawing.Size(123, 20);
			this.SequenceLabel.TabIndex = 4;
			this.SequenceLabel.Text = "Sequence";
			// 
			// DocGenGroupDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(596, 171);
			this.Controls.Add(this.SequenceTextbox);
			this.Controls.Add(this.SequenceLabel);
			this.Controls.Add(this.DescriptionTextbox);
			this.Controls.Add(this.DescriptionLabel);
			this.Controls.Add(this.NameTextbox);
			this.Controls.Add(this.NameLabel);
			this.Controls.Add(this.FormCancelButton);
			this.Controls.Add(this.OKButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DocGenGroupDetail";
			this.ShowInTaskbar = false;
			this.Text = "DocGroup Detail";
			this.Load += new System.EventHandler(this.DocGenGroupDetail_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DocGenGroupDetail_KeyDown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button FormCancelButton;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.TextBox NameTextbox;
		private System.Windows.Forms.Label NameLabel;
		private System.Windows.Forms.TextBox DescriptionTextbox;
		private System.Windows.Forms.Label DescriptionLabel;
		private System.Windows.Forms.TextBox SequenceTextbox;
		private System.Windows.Forms.Label SequenceLabel;
	}
}
