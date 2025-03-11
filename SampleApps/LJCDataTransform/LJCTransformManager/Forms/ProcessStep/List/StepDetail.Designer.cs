namespace LJCTransformManager
{
	partial class StepDetail
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
			this.SequenceTextbox = new System.Windows.Forms.TextBox();
			this.SequenceLabel = new System.Windows.Forms.Label();
			this.ParentNameTextbox = new System.Windows.Forms.TextBox();
			this.ParentNameLabel = new System.Windows.Forms.Label();
			this.StepDescriptionTextbox = new System.Windows.Forms.TextBox();
			this.StepDescriptionLabel = new System.Windows.Forms.Label();
			this.StepNameTextbox = new System.Windows.Forms.TextBox();
			this.StepNameLabel = new System.Windows.Forms.Label();
			this.FormCancelButton = new System.Windows.Forms.Button();
			this.OKButton = new System.Windows.Forms.Button();
			this.TaskStatusCombo = new System.Windows.Forms.ComboBox();
			this.TaskStatusLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// SequenceTextbox
			// 
			this.SequenceTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.SequenceTextbox.Location = new System.Drawing.Point(136, 158);
			this.SequenceTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.SequenceTextbox.Name = "SequenceTextbox";
			this.SequenceTextbox.Size = new System.Drawing.Size(60, 26);
			this.SequenceTextbox.TabIndex = 9;
			this.SequenceTextbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SequenceTextbox_KeyPress);
			// 
			// SequenceLabel
			// 
			this.SequenceLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.SequenceLabel.Location = new System.Drawing.Point(11, 161);
			this.SequenceLabel.Name = "SequenceLabel";
			this.SequenceLabel.Size = new System.Drawing.Size(123, 30);
			this.SequenceLabel.TabIndex = 8;
			this.SequenceLabel.Text = "Sequence";
			// 
			// ParentNameTextbox
			// 
			this.ParentNameTextbox.Location = new System.Drawing.Point(137, 14);
			this.ParentNameTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ParentNameTextbox.MaxLength = 100;
			this.ParentNameTextbox.Name = "ParentNameTextbox";
			this.ParentNameTextbox.ReadOnly = true;
			this.ParentNameTextbox.Size = new System.Drawing.Size(417, 26);
			this.ParentNameTextbox.TabIndex = 1;
			this.ParentNameTextbox.TabStop = false;
			// 
			// ParentNameLabel
			// 
			this.ParentNameLabel.Location = new System.Drawing.Point(12, 17);
			this.ParentNameLabel.Name = "ParentNameLabel";
			this.ParentNameLabel.Size = new System.Drawing.Size(123, 20);
			this.ParentNameLabel.TabIndex = 0;
			this.ParentNameLabel.Text = "Data Process";
			// 
			// StepDescriptionTextbox
			// 
			this.StepDescriptionTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.StepDescriptionTextbox.Location = new System.Drawing.Point(136, 88);
			this.StepDescriptionTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.StepDescriptionTextbox.Name = "StepDescriptionTextbox";
			this.StepDescriptionTextbox.Size = new System.Drawing.Size(417, 26);
			this.StepDescriptionTextbox.TabIndex = 5;
			// 
			// StepDescriptionLabel
			// 
			this.StepDescriptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.StepDescriptionLabel.Location = new System.Drawing.Point(11, 91);
			this.StepDescriptionLabel.Name = "StepDescriptionLabel";
			this.StepDescriptionLabel.Size = new System.Drawing.Size(123, 20);
			this.StepDescriptionLabel.TabIndex = 4;
			this.StepDescriptionLabel.Text = "Description";
			// 
			// StepNameTextbox
			// 
			this.StepNameTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.StepNameTextbox.Location = new System.Drawing.Point(136, 52);
			this.StepNameTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.StepNameTextbox.MaxLength = 100;
			this.StepNameTextbox.Name = "StepNameTextbox";
			this.StepNameTextbox.Size = new System.Drawing.Size(417, 26);
			this.StepNameTextbox.TabIndex = 3;
			this.StepNameTextbox.TextChanged += new System.EventHandler(this.StepNameTextbox_TextChanged);
			this.StepNameTextbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StepNameTextbox_KeyPress);
			// 
			// StepNameLabel
			// 
			this.StepNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.StepNameLabel.Location = new System.Drawing.Point(11, 55);
			this.StepNameLabel.Name = "StepNameLabel";
			this.StepNameLabel.Size = new System.Drawing.Size(123, 20);
			this.StepNameLabel.TabIndex = 2;
			this.StepNameLabel.Text = "Step";
			// 
			// FormCancelButton
			// 
			this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.FormCancelButton.Location = new System.Drawing.Point(442, 193);
			this.FormCancelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.FormCancelButton.Name = "FormCancelButton";
			this.FormCancelButton.Size = new System.Drawing.Size(112, 35);
			this.FormCancelButton.TabIndex = 11;
			this.FormCancelButton.Text = "Cancel";
			this.FormCancelButton.UseVisualStyleBackColor = true;
			this.FormCancelButton.Click += new System.EventHandler(this.FormCancelButton_Click);
			// 
			// OKButton
			// 
			this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OKButton.Location = new System.Drawing.Point(322, 193);
			this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(112, 35);
			this.OKButton.TabIndex = 10;
			this.OKButton.Text = "&OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// TaskStatusCombo
			// 
			this.TaskStatusCombo.FormattingEnabled = true;
			this.TaskStatusCombo.Location = new System.Drawing.Point(137, 122);
			this.TaskStatusCombo.Name = "TaskStatusCombo";
			this.TaskStatusCombo.Size = new System.Drawing.Size(243, 28);
			this.TaskStatusCombo.TabIndex = 7;
			// 
			// TaskStatusLabel
			// 
			this.TaskStatusLabel.Location = new System.Drawing.Point(12, 125);
			this.TaskStatusLabel.Name = "TaskStatusLabel";
			this.TaskStatusLabel.Size = new System.Drawing.Size(123, 20);
			this.TaskStatusLabel.TabIndex = 6;
			this.TaskStatusLabel.Text = "Status";
			// 
			// StepDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(563, 242);
			this.Controls.Add(this.TaskStatusCombo);
			this.Controls.Add(this.TaskStatusLabel);
			this.Controls.Add(this.SequenceTextbox);
			this.Controls.Add(this.SequenceLabel);
			this.Controls.Add(this.ParentNameTextbox);
			this.Controls.Add(this.ParentNameLabel);
			this.Controls.Add(this.StepDescriptionTextbox);
			this.Controls.Add(this.StepDescriptionLabel);
			this.Controls.Add(this.StepNameTextbox);
			this.Controls.Add(this.StepNameLabel);
			this.Controls.Add(this.FormCancelButton);
			this.Controls.Add(this.OKButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "StepDetail";
			this.ShowInTaskbar = false;
			this.Text = "Step Detail";
			this.Load += new System.EventHandler(this.StepDetail_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.TextBox SequenceTextbox;
		private System.Windows.Forms.Label SequenceLabel;
		private System.Windows.Forms.TextBox ParentNameTextbox;
		private System.Windows.Forms.Label ParentNameLabel;
		private System.Windows.Forms.TextBox StepDescriptionTextbox;
		private System.Windows.Forms.Label StepDescriptionLabel;
		private System.Windows.Forms.TextBox StepNameTextbox;
		private System.Windows.Forms.Label StepNameLabel;
		private System.Windows.Forms.Button FormCancelButton;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.ComboBox TaskStatusCombo;
		private System.Windows.Forms.Label TaskStatusLabel;
	}
}