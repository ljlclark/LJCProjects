namespace LJCTransformManager
{
	partial class TaskDetail
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
			this.TaskStatusCombo = new System.Windows.Forms.ComboBox();
			this.TaskStatusLabel = new System.Windows.Forms.Label();
			this.SequenceTextbox = new System.Windows.Forms.TextBox();
			this.SequenceLabel = new System.Windows.Forms.Label();
			this.ParentNameTextbox = new System.Windows.Forms.TextBox();
			this.ParentNameLabel = new System.Windows.Forms.Label();
			this.TaskDescriptionTextbox = new System.Windows.Forms.TextBox();
			this.TaskDescriptionLabel = new System.Windows.Forms.Label();
			this.TaskNameTextbox = new System.Windows.Forms.TextBox();
			this.TaskNameLabel = new System.Windows.Forms.Label();
			this.FormCancelButton = new System.Windows.Forms.Button();
			this.OKButton = new System.Windows.Forms.Button();
			this.TaskTypeCombo = new LJCWinFormControls.LJCItemCombo();
			this.TaskTypeLabel = new System.Windows.Forms.Label();
			this.ActionItemTextbox = new System.Windows.Forms.TextBox();
			this.ActionItemLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// TaskStatusCombo
			// 
			this.TaskStatusCombo.FormattingEnabled = true;
			this.TaskStatusCombo.Location = new System.Drawing.Point(137, 190);
			this.TaskStatusCombo.Name = "TaskStatusCombo";
			this.TaskStatusCombo.Size = new System.Drawing.Size(243, 28);
			this.TaskStatusCombo.TabIndex = 11;
			this.TaskStatusCombo.SelectedIndexChanged += new System.EventHandler(this.StatusCombo_SelectedIndexChanged);
			this.TaskStatusCombo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.StatusCombo_KeyDown);
			this.TaskStatusCombo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StatusCombo_KeyPress);
			// 
			// TaskStatusLabel
			// 
			this.TaskStatusLabel.Location = new System.Drawing.Point(12, 193);
			this.TaskStatusLabel.Name = "TaskStatusLabel";
			this.TaskStatusLabel.Size = new System.Drawing.Size(123, 20);
			this.TaskStatusLabel.TabIndex = 10;
			this.TaskStatusLabel.Text = "Status";
			// 
			// SequenceTextbox
			// 
			this.SequenceTextbox.Location = new System.Drawing.Point(137, 226);
			this.SequenceTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.SequenceTextbox.Name = "SequenceTextbox";
			this.SequenceTextbox.Size = new System.Drawing.Size(60, 26);
			this.SequenceTextbox.TabIndex = 13;
			this.SequenceTextbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SequenceTextbox_KeyPress);
			// 
			// SequenceLabel
			// 
			this.SequenceLabel.Location = new System.Drawing.Point(12, 229);
			this.SequenceLabel.Name = "SequenceLabel";
			this.SequenceLabel.Size = new System.Drawing.Size(123, 20);
			this.SequenceLabel.TabIndex = 12;
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
			this.ParentNameLabel.Text = "Step";
			// 
			// TaskDescriptionTextbox
			// 
			this.TaskDescriptionTextbox.Location = new System.Drawing.Point(137, 86);
			this.TaskDescriptionTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.TaskDescriptionTextbox.Name = "TaskDescriptionTextbox";
			this.TaskDescriptionTextbox.Size = new System.Drawing.Size(417, 26);
			this.TaskDescriptionTextbox.TabIndex = 5;
			// 
			// TaskDescriptionLabel
			// 
			this.TaskDescriptionLabel.Location = new System.Drawing.Point(13, 89);
			this.TaskDescriptionLabel.Name = "TaskDescriptionLabel";
			this.TaskDescriptionLabel.Size = new System.Drawing.Size(123, 20);
			this.TaskDescriptionLabel.TabIndex = 4;
			this.TaskDescriptionLabel.Text = "Description";
			// 
			// TaskNameTextbox
			// 
			this.TaskNameTextbox.Location = new System.Drawing.Point(137, 50);
			this.TaskNameTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.TaskNameTextbox.MaxLength = 100;
			this.TaskNameTextbox.Name = "TaskNameTextbox";
			this.TaskNameTextbox.Size = new System.Drawing.Size(417, 26);
			this.TaskNameTextbox.TabIndex = 3;
			this.TaskNameTextbox.TextChanged += new System.EventHandler(this.TaskNameTextbox_TextChanged);
			this.TaskNameTextbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TaskNameTextbox_KeyPress);
			// 
			// TaskNameLabel
			// 
			this.TaskNameLabel.Location = new System.Drawing.Point(13, 53);
			this.TaskNameLabel.Name = "TaskNameLabel";
			this.TaskNameLabel.Size = new System.Drawing.Size(123, 20);
			this.TaskNameLabel.TabIndex = 2;
			this.TaskNameLabel.Text = "Task";
			// 
			// FormCancelButton
			// 
			this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.FormCancelButton.Location = new System.Drawing.Point(442, 262);
			this.FormCancelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.FormCancelButton.Name = "FormCancelButton";
			this.FormCancelButton.Size = new System.Drawing.Size(112, 35);
			this.FormCancelButton.TabIndex = 15;
			this.FormCancelButton.Text = "Cancel";
			this.FormCancelButton.UseVisualStyleBackColor = true;
			this.FormCancelButton.Click += new System.EventHandler(this.FormCancelButton_Click);
			// 
			// OKButton
			// 
			this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OKButton.Location = new System.Drawing.Point(322, 262);
			this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(112, 35);
			this.OKButton.TabIndex = 14;
			this.OKButton.Text = "&OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// TaskTypeCombo
			// 
			this.TaskTypeCombo.FormattingEnabled = true;
			this.TaskTypeCombo.Location = new System.Drawing.Point(137, 120);
			this.TaskTypeCombo.Name = "TaskTypeCombo";
			this.TaskTypeCombo.Size = new System.Drawing.Size(243, 28);
			this.TaskTypeCombo.TabIndex = 7;
			this.TaskTypeCombo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TaskTypeCombo_KeyDown);
			this.TaskTypeCombo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TaskTypeCombo_KeyPress);
			// 
			// TaskTypeLabel
			// 
			this.TaskTypeLabel.Location = new System.Drawing.Point(12, 124);
			this.TaskTypeLabel.Name = "TaskTypeLabel";
			this.TaskTypeLabel.Size = new System.Drawing.Size(123, 20);
			this.TaskTypeLabel.TabIndex = 6;
			this.TaskTypeLabel.Text = "Task Type";
			// 
			// ActionItemTextbox
			// 
			this.ActionItemTextbox.Location = new System.Drawing.Point(137, 156);
			this.ActionItemTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ActionItemTextbox.Name = "ActionItemTextbox";
			this.ActionItemTextbox.Size = new System.Drawing.Size(417, 26);
			this.ActionItemTextbox.TabIndex = 9;
			this.ActionItemTextbox.TextChanged += new System.EventHandler(this.ActionItemTextbox_TextChanged);
			this.ActionItemTextbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ActionItemTextbox_KeyPress);
			// 
			// ActionItemLabel
			// 
			this.ActionItemLabel.Location = new System.Drawing.Point(12, 159);
			this.ActionItemLabel.Name = "ActionItemLabel";
			this.ActionItemLabel.Size = new System.Drawing.Size(123, 20);
			this.ActionItemLabel.TabIndex = 8;
			this.ActionItemLabel.Text = "Action Item";
			// 
			// TaskDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(563, 310);
			this.Controls.Add(this.ActionItemTextbox);
			this.Controls.Add(this.ActionItemLabel);
			this.Controls.Add(this.TaskTypeCombo);
			this.Controls.Add(this.TaskTypeLabel);
			this.Controls.Add(this.TaskStatusCombo);
			this.Controls.Add(this.TaskStatusLabel);
			this.Controls.Add(this.SequenceTextbox);
			this.Controls.Add(this.SequenceLabel);
			this.Controls.Add(this.ParentNameTextbox);
			this.Controls.Add(this.ParentNameLabel);
			this.Controls.Add(this.TaskDescriptionTextbox);
			this.Controls.Add(this.TaskDescriptionLabel);
			this.Controls.Add(this.TaskNameTextbox);
			this.Controls.Add(this.TaskNameLabel);
			this.Controls.Add(this.FormCancelButton);
			this.Controls.Add(this.OKButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "TaskDetail";
			this.ShowInTaskbar = false;
			this.Text = "Task Detail";
			this.Load += new System.EventHandler(this.TaskDetail_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox TaskStatusCombo;
		private System.Windows.Forms.Label TaskStatusLabel;
		private System.Windows.Forms.TextBox SequenceTextbox;
		private System.Windows.Forms.Label SequenceLabel;
		private System.Windows.Forms.TextBox ParentNameTextbox;
		private System.Windows.Forms.Label ParentNameLabel;
		private System.Windows.Forms.TextBox TaskDescriptionTextbox;
		private System.Windows.Forms.Label TaskDescriptionLabel;
		private System.Windows.Forms.TextBox TaskNameTextbox;
		private System.Windows.Forms.Label TaskNameLabel;
		private System.Windows.Forms.Button FormCancelButton;
		private System.Windows.Forms.Button OKButton;
		private LJCWinFormControls.LJCItemCombo TaskTypeCombo;
		private System.Windows.Forms.Label TaskTypeLabel;
		private System.Windows.Forms.TextBox ActionItemTextbox;
		private System.Windows.Forms.Label ActionItemLabel;
	}
}