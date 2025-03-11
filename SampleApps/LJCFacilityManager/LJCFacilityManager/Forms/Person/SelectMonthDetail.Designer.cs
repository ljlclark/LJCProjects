namespace LJCFacilityManager
{
	partial class SelectMonthDetail
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
			this.mMonthCombo = new System.Windows.Forms.ComboBox();
			this.mMonthLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// FormCancelButton
			// 
			this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.FormCancelButton.Location = new System.Drawing.Point(237, 60);
			this.FormCancelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.FormCancelButton.Name = "FormCancelButton";
			this.FormCancelButton.Size = new System.Drawing.Size(112, 35);
			this.FormCancelButton.TabIndex = 19;
			this.FormCancelButton.Text = "Cancel";
			this.FormCancelButton.UseVisualStyleBackColor = true;
			this.FormCancelButton.Click += new System.EventHandler(this.FormCancelButton_Click);
			// 
			// OKButton
			// 
			this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OKButton.Location = new System.Drawing.Point(116, 60);
			this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(112, 35);
			this.OKButton.TabIndex = 18;
			this.OKButton.Text = "&OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// mMonthCombo
			// 
			this.mMonthCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.mMonthCombo.FormattingEnabled = true;
			this.mMonthCombo.Location = new System.Drawing.Point(166, 18);
			this.mMonthCombo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.mMonthCombo.Name = "mMonthCombo";
			this.mMonthCombo.Size = new System.Drawing.Size(180, 28);
			this.mMonthCombo.TabIndex = 20;
			// 
			// mMonthLabel
			// 
			this.mMonthLabel.Location = new System.Drawing.Point(18, 23);
			this.mMonthLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.mMonthLabel.Name = "mMonthLabel";
			this.mMonthLabel.Size = new System.Drawing.Size(147, 20);
			this.mMonthLabel.TabIndex = 21;
			this.mMonthLabel.Text = "Month";
			// 
			// SelectMonthDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(360, 106);
			this.Controls.Add(this.mMonthLabel);
			this.Controls.Add(this.mMonthCombo);
			this.Controls.Add(this.FormCancelButton);
			this.Controls.Add(this.OKButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "SelectMonthDetail";
			this.Text = "Select Month";
			this.Load += new System.EventHandler(this.SelectMonthDetail_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button FormCancelButton;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.ComboBox mMonthCombo;
		private System.Windows.Forms.Label mMonthLabel;
	}
}