namespace LJCTransformManager
{
	partial class DataProcessDetail
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
			this.ProcessDescriptionTextbox = new System.Windows.Forms.TextBox();
			this.ProcessDescriptionLabel = new System.Windows.Forms.Label();
			this.ProcessNameTextbox = new System.Windows.Forms.TextBox();
			this.ProcessNameLabel = new System.Windows.Forms.Label();
			this.FormCancelButton = new System.Windows.Forms.Button();
			this.OKButton = new System.Windows.Forms.Button();
			this.ParentNameTextbox = new System.Windows.Forms.TextBox();
			this.ParentNameLabel = new System.Windows.Forms.Label();
			this.SequenceTextbox = new System.Windows.Forms.TextBox();
			this.SequenceLabel = new System.Windows.Forms.Label();
			this.StatusLabel = new System.Windows.Forms.Label();
			this.StatusCombo = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// ProcessDescriptionTextbox
			// 
			this.ProcessDescriptionTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.ProcessDescriptionTextbox.Location = new System.Drawing.Point(136, 85);
			this.ProcessDescriptionTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ProcessDescriptionTextbox.Name = "ProcessDescriptionTextbox";
			this.ProcessDescriptionTextbox.Size = new System.Drawing.Size(417, 26);
			this.ProcessDescriptionTextbox.TabIndex = 5;
			// 
			// ProcessDescriptionLabel
			// 
			this.ProcessDescriptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.ProcessDescriptionLabel.Location = new System.Drawing.Point(11, 90);
			this.ProcessDescriptionLabel.Name = "ProcessDescriptionLabel";
			this.ProcessDescriptionLabel.Size = new System.Drawing.Size(123, 20);
			this.ProcessDescriptionLabel.TabIndex = 4;
			this.ProcessDescriptionLabel.Text = "Description";
			// 
			// ProcessNameTextbox
			// 
			this.ProcessNameTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.ProcessNameTextbox.Location = new System.Drawing.Point(136, 49);
			this.ProcessNameTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ProcessNameTextbox.MaxLength = 100;
			this.ProcessNameTextbox.Name = "ProcessNameTextbox";
			this.ProcessNameTextbox.Size = new System.Drawing.Size(417, 26);
			this.ProcessNameTextbox.TabIndex = 3;
			this.ProcessNameTextbox.TextChanged += new System.EventHandler(this.ProcessNameTextbox_TextChanged);
			this.ProcessNameTextbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ProcessNameTextbox_KeyPress);
			// 
			// ProcessNameLabel
			// 
			this.ProcessNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.ProcessNameLabel.Location = new System.Drawing.Point(11, 54);
			this.ProcessNameLabel.Name = "ProcessNameLabel";
			this.ProcessNameLabel.Size = new System.Drawing.Size(123, 20);
			this.ProcessNameLabel.TabIndex = 2;
			this.ProcessNameLabel.Text = "Data Process";
			// 
			// FormCancelButton
			// 
			this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.FormCancelButton.Location = new System.Drawing.Point(442, 194);
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
			this.OKButton.Location = new System.Drawing.Point(322, 194);
			this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(112, 35);
			this.OKButton.TabIndex = 10;
			this.OKButton.Text = "&OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
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
			this.ParentNameLabel.Location = new System.Drawing.Point(12, 19);
			this.ParentNameLabel.Name = "ParentNameLabel";
			this.ParentNameLabel.Size = new System.Drawing.Size(123, 20);
			this.ParentNameLabel.TabIndex = 0;
			this.ParentNameLabel.Text = "Process Group";
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
			this.SequenceLabel.Location = new System.Drawing.Point(11, 163);
			this.SequenceLabel.Name = "SequenceLabel";
			this.SequenceLabel.Size = new System.Drawing.Size(123, 20);
			this.SequenceLabel.TabIndex = 8;
			this.SequenceLabel.Text = "Sequence";
			// 
			// StatusLabel
			// 
			this.StatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.StatusLabel.Location = new System.Drawing.Point(12, 126);
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Size = new System.Drawing.Size(123, 20);
			this.StatusLabel.TabIndex = 6;
			this.StatusLabel.Text = "Status";
			// 
			// StatusCombo
			// 
			this.StatusCombo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.StatusCombo.FormattingEnabled = true;
			this.StatusCombo.Location = new System.Drawing.Point(136, 121);
			this.StatusCombo.Name = "StatusCombo";
			this.StatusCombo.Size = new System.Drawing.Size(243, 28);
			this.StatusCombo.TabIndex = 7;
			this.StatusCombo.SelectedIndexChanged += new System.EventHandler(this.StatusCombo_SelectedIndexChanged);
			this.StatusCombo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.StatusCombo_KeyDown);
			this.StatusCombo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StatusCombo_KeyPress);
			// 
			// DataProcessDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(563, 242);
			this.Controls.Add(this.StatusCombo);
			this.Controls.Add(this.StatusLabel);
			this.Controls.Add(this.SequenceTextbox);
			this.Controls.Add(this.SequenceLabel);
			this.Controls.Add(this.ParentNameTextbox);
			this.Controls.Add(this.ParentNameLabel);
			this.Controls.Add(this.ProcessDescriptionTextbox);
			this.Controls.Add(this.ProcessDescriptionLabel);
			this.Controls.Add(this.ProcessNameTextbox);
			this.Controls.Add(this.ProcessNameLabel);
			this.Controls.Add(this.FormCancelButton);
			this.Controls.Add(this.OKButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DataProcessDetail";
			this.ShowInTaskbar = false;
			this.Text = "Data Process Detail";
			this.Load += new System.EventHandler(this.DataProcessDetail_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox ProcessDescriptionTextbox;
		private System.Windows.Forms.Label ProcessDescriptionLabel;
		private System.Windows.Forms.TextBox ProcessNameTextbox;
		private System.Windows.Forms.Label ProcessNameLabel;
		private System.Windows.Forms.Button FormCancelButton;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.TextBox ParentNameTextbox;
		private System.Windows.Forms.Label ParentNameLabel;
		private System.Windows.Forms.TextBox SequenceTextbox;
		private System.Windows.Forms.Label SequenceLabel;
		private System.Windows.Forms.Label StatusLabel;
		private System.Windows.Forms.ComboBox StatusCombo;
	}
}