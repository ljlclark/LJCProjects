namespace LJCTransformManager
{
	partial class TransformDetail
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
			this.TargetDataCombo = new LJCWinFormControls.LJCItemCombo();
			this.TargetMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.TargetMenuView = new System.Windows.Forms.ToolStripMenuItem();
			this.TargetDataLabel = new System.Windows.Forms.Label();
			this.SourceDataCombo = new LJCWinFormControls.LJCItemCombo();
			this.SourceMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.SourceMenuView = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.SourceMenuLayoutSelect = new System.Windows.Forms.ToolStripMenuItem();
			this.SourceDataLabel = new System.Windows.Forms.Label();
			this.ParentNameTextbox = new System.Windows.Forms.TextBox();
			this.ParentNameLabel = new System.Windows.Forms.Label();
			this.TransformDescriptionTextbox = new System.Windows.Forms.TextBox();
			this.TransformDescriptionLabel = new System.Windows.Forms.Label();
			this.TransformNameTextbox = new System.Windows.Forms.TextBox();
			this.TransformNameLabel = new System.Windows.Forms.Label();
			this.FormCancelButton = new System.Windows.Forms.Button();
			this.OKButton = new System.Windows.Forms.Button();
			this.TargetMenuLayoutSelect = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.TargetMenu.SuspendLayout();
			this.SourceMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// TargetDataCombo
			// 
			this.TargetDataCombo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.TargetDataCombo.ContextMenuStrip = this.TargetMenu;
			this.TargetDataCombo.Location = new System.Drawing.Point(167, 156);
			this.TargetDataCombo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.TargetDataCombo.Name = "TargetDataCombo";
			this.TargetDataCombo.Size = new System.Drawing.Size(417, 28);
			this.TargetDataCombo.TabIndex = 9;
			this.TargetDataCombo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TargetDataCombo_KeyDown);
			this.TargetDataCombo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TargetDataCombo_KeyPress);
			// 
			// TargetMenu
			// 
			this.TargetMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.TargetMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TargetMenuView,
            this.toolStripSeparator2,
            this.TargetMenuLayoutSelect});
			this.TargetMenu.Name = "SourceMenu";
			this.TargetMenu.Size = new System.Drawing.Size(241, 103);
			// 
			// TargetMenuView
			// 
			this.TargetMenuView.Name = "TargetMenuView";
			this.TargetMenuView.Size = new System.Drawing.Size(240, 30);
			this.TargetMenuView.Text = "&View Data";
			this.TargetMenuView.Click += new System.EventHandler(this.TargetView_Click);
			// 
			// TargetDataLabel
			// 
			this.TargetDataLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.TargetDataLabel.Location = new System.Drawing.Point(13, 159);
			this.TargetDataLabel.Name = "TargetDataLabel";
			this.TargetDataLabel.Size = new System.Drawing.Size(147, 20);
			this.TargetDataLabel.TabIndex = 8;
			this.TargetDataLabel.Text = "Data Target";
			// 
			// SourceDataCombo
			// 
			this.SourceDataCombo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.SourceDataCombo.ContextMenuStrip = this.SourceMenu;
			this.SourceDataCombo.Location = new System.Drawing.Point(167, 120);
			this.SourceDataCombo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.SourceDataCombo.Name = "SourceDataCombo";
			this.SourceDataCombo.Size = new System.Drawing.Size(417, 28);
			this.SourceDataCombo.TabIndex = 7;
			this.SourceDataCombo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SourceDataCombo_KeyDown);
			this.SourceDataCombo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SourceDataCombo_KeyPress);
			// 
			// SourceMenu
			// 
			this.SourceMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.SourceMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SourceMenuView,
            this.toolStripSeparator1,
            this.SourceMenuLayoutSelect});
			this.SourceMenu.Name = "SourceMenu";
			this.SourceMenu.Size = new System.Drawing.Size(189, 70);
			// 
			// SourceMenuView
			// 
			this.SourceMenuView.Name = "SourceMenuView";
			this.SourceMenuView.Size = new System.Drawing.Size(188, 30);
			this.SourceMenuView.Text = "&View Data";
			this.SourceMenuView.Click += new System.EventHandler(this.SourceMenuView_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(185, 6);
			// 
			// SourceMenuLayoutSelect
			// 
			this.SourceMenuLayoutSelect.Name = "SourceMenuLayoutSelect";
			this.SourceMenuLayoutSelect.Size = new System.Drawing.Size(188, 30);
			this.SourceMenuLayoutSelect.Text = "&Select Layout";
			this.SourceMenuLayoutSelect.Click += new System.EventHandler(this.SourceMenuLayoutSelect_Click);
			// 
			// SourceDataLabel
			// 
			this.SourceDataLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.SourceDataLabel.Location = new System.Drawing.Point(13, 123);
			this.SourceDataLabel.Name = "SourceDataLabel";
			this.SourceDataLabel.Size = new System.Drawing.Size(147, 20);
			this.SourceDataLabel.TabIndex = 6;
			this.SourceDataLabel.Text = "Data Source";
			// 
			// ParentNameTextbox
			// 
			this.ParentNameTextbox.Location = new System.Drawing.Point(167, 14);
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
			this.ParentNameLabel.Text = "Task";
			// 
			// TransformDescriptionTextbox
			// 
			this.TransformDescriptionTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.TransformDescriptionTextbox.Location = new System.Drawing.Point(167, 86);
			this.TransformDescriptionTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.TransformDescriptionTextbox.Name = "TransformDescriptionTextbox";
			this.TransformDescriptionTextbox.Size = new System.Drawing.Size(417, 26);
			this.TransformDescriptionTextbox.TabIndex = 5;
			// 
			// TransformDescriptionLabel
			// 
			this.TransformDescriptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.TransformDescriptionLabel.Location = new System.Drawing.Point(13, 89);
			this.TransformDescriptionLabel.Name = "TransformDescriptionLabel";
			this.TransformDescriptionLabel.Size = new System.Drawing.Size(123, 20);
			this.TransformDescriptionLabel.TabIndex = 4;
			this.TransformDescriptionLabel.Text = "Description";
			// 
			// TransformNameTextbox
			// 
			this.TransformNameTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.TransformNameTextbox.Location = new System.Drawing.Point(167, 50);
			this.TransformNameTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.TransformNameTextbox.MaxLength = 100;
			this.TransformNameTextbox.Name = "TransformNameTextbox";
			this.TransformNameTextbox.Size = new System.Drawing.Size(417, 26);
			this.TransformNameTextbox.TabIndex = 3;
			this.TransformNameTextbox.TextChanged += new System.EventHandler(this.TransformNameTextbox_TextChanged);
			this.TransformNameTextbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TransformNameTextbox_KeyPress);
			// 
			// TransformNameLabel
			// 
			this.TransformNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.TransformNameLabel.Location = new System.Drawing.Point(13, 53);
			this.TransformNameLabel.Name = "TransformNameLabel";
			this.TransformNameLabel.Size = new System.Drawing.Size(123, 20);
			this.TransformNameLabel.TabIndex = 2;
			this.TransformNameLabel.Text = "Task Transform";
			// 
			// FormCancelButton
			// 
			this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.FormCancelButton.Location = new System.Drawing.Point(473, 193);
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
			this.OKButton.Location = new System.Drawing.Point(353, 193);
			this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(112, 35);
			this.OKButton.TabIndex = 10;
			this.OKButton.Text = "&OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// TargetMenuLayoutSelect
			// 
			this.TargetMenuLayoutSelect.Name = "TargetMenuLayoutSelect";
			this.TargetMenuLayoutSelect.Size = new System.Drawing.Size(240, 30);
			this.TargetMenuLayoutSelect.Text = "&Select Layout";
			this.TargetMenuLayoutSelect.Click += new System.EventHandler(this.TargetMenuLayoutSelect_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(237, 6);
			// 
			// TransformDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(593, 241);
			this.Controls.Add(this.TargetDataCombo);
			this.Controls.Add(this.TargetDataLabel);
			this.Controls.Add(this.SourceDataCombo);
			this.Controls.Add(this.SourceDataLabel);
			this.Controls.Add(this.ParentNameTextbox);
			this.Controls.Add(this.ParentNameLabel);
			this.Controls.Add(this.TransformDescriptionTextbox);
			this.Controls.Add(this.TransformDescriptionLabel);
			this.Controls.Add(this.TransformNameTextbox);
			this.Controls.Add(this.TransformNameLabel);
			this.Controls.Add(this.FormCancelButton);
			this.Controls.Add(this.OKButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "TransformDetail";
			this.ShowInTaskbar = false;
			this.Text = "Task Transform Detail";
			this.Load += new System.EventHandler(this.TransformDetail_Load);
			this.TargetMenu.ResumeLayout(false);
			this.SourceMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private LJCWinFormControls.LJCItemCombo TargetDataCombo;
		private System.Windows.Forms.Label TargetDataLabel;
		private LJCWinFormControls.LJCItemCombo SourceDataCombo;
		private System.Windows.Forms.Label SourceDataLabel;
		private System.Windows.Forms.TextBox ParentNameTextbox;
		private System.Windows.Forms.Label ParentNameLabel;
		private System.Windows.Forms.TextBox TransformDescriptionTextbox;
		private System.Windows.Forms.Label TransformDescriptionLabel;
		private System.Windows.Forms.TextBox TransformNameTextbox;
		private System.Windows.Forms.Label TransformNameLabel;
		private System.Windows.Forms.Button FormCancelButton;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.ContextMenuStrip SourceMenu;
		private System.Windows.Forms.ToolStripMenuItem SourceMenuView;
		private System.Windows.Forms.ContextMenuStrip TargetMenu;
		private System.Windows.Forms.ToolStripMenuItem TargetMenuView;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem SourceMenuLayoutSelect;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem TargetMenuLayoutSelect;
	}
}