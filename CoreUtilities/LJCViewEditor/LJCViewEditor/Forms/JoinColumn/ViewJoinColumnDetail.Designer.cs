namespace LJCViewEditor
{
	partial class ViewJoinColumnDetail
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
			this.TemplateLabel = new System.Windows.Forms.Label();
			this.TemplateCombo = new System.Windows.Forms.ComboBox();
			this.DataTypeCombo = new System.Windows.Forms.ComboBox();
			this.RenameTextbox = new System.Windows.Forms.TextBox();
			this.RenameLabel = new System.Windows.Forms.Label();
			this.PropertyTextbox = new System.Windows.Forms.TextBox();
			this.PropertyLabel = new System.Windows.Forms.Label();
			this.CaptionTextbox = new System.Windows.Forms.TextBox();
			this.CaptionLabel = new System.Windows.Forms.Label();
			this.DataTypeLabel = new System.Windows.Forms.Label();
			this.ColumnNameTextbox = new System.Windows.Forms.TextBox();
			this.ColumnNameLabel = new System.Windows.Forms.Label();
			this.ParentTextbox = new System.Windows.Forms.TextBox();
			this.ParentLabel = new System.Windows.Forms.Label();
			this.FormCancelButton = new System.Windows.Forms.Button();
			this.OKButton = new System.Windows.Forms.Button();
			this.JoinColumnMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.JoinColumnHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.JoinColumnMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// TemplateLabel
			// 
			this.TemplateLabel.Location = new System.Drawing.Point(13, 53);
			this.TemplateLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.TemplateLabel.Name = "TemplateLabel";
			this.TemplateLabel.Size = new System.Drawing.Size(153, 20);
			this.TemplateLabel.TabIndex = 2;
			this.TemplateLabel.Text = "Template Column";
			// 
			// TemplateCombo
			// 
			this.TemplateCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.TemplateCombo.FormattingEnabled = true;
			this.TemplateCombo.Location = new System.Drawing.Point(167, 49);
			this.TemplateCombo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
			this.TemplateCombo.Name = "TemplateCombo";
			this.TemplateCombo.Size = new System.Drawing.Size(300, 28);
			this.TemplateCombo.TabIndex = 3;
			this.TemplateCombo.SelectedIndexChanged += new System.EventHandler(this.TemplateCombo_SelectedIndexChanged);
			// 
			// DataTypeCombo
			// 
			this.DataTypeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.DataTypeCombo.FormattingEnabled = true;
			this.DataTypeCombo.Location = new System.Drawing.Point(167, 229);
			this.DataTypeCombo.Name = "DataTypeCombo";
			this.DataTypeCombo.Size = new System.Drawing.Size(433, 28);
			this.DataTypeCombo.TabIndex = 13;
			// 
			// RenameTextbox
			// 
			this.RenameTextbox.Location = new System.Drawing.Point(167, 158);
			this.RenameTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.RenameTextbox.Name = "RenameTextbox";
			this.RenameTextbox.Size = new System.Drawing.Size(433, 26);
			this.RenameTextbox.TabIndex = 9;
			this.RenameTextbox.TextChanged += new System.EventHandler(this.RenameTextbox_TextChanged);
			this.RenameTextbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RenameTextbox_KeyPress);
			// 
			// RenameLabel
			// 
			this.RenameLabel.Location = new System.Drawing.Point(13, 161);
			this.RenameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.RenameLabel.Name = "RenameLabel";
			this.RenameLabel.Size = new System.Drawing.Size(153, 20);
			this.RenameLabel.TabIndex = 8;
			this.RenameLabel.Text = "Rename As";
			// 
			// PropertyTextbox
			// 
			this.PropertyTextbox.Location = new System.Drawing.Point(167, 122);
			this.PropertyTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.PropertyTextbox.Name = "PropertyTextbox";
			this.PropertyTextbox.Size = new System.Drawing.Size(433, 26);
			this.PropertyTextbox.TabIndex = 7;
			this.PropertyTextbox.TextChanged += new System.EventHandler(this.PropertyTextbox_TextChanged);
			this.PropertyTextbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PropertyTextbox_KeyPress);
			// 
			// PropertyLabel
			// 
			this.PropertyLabel.Location = new System.Drawing.Point(13, 125);
			this.PropertyLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.PropertyLabel.Name = "PropertyLabel";
			this.PropertyLabel.Size = new System.Drawing.Size(153, 20);
			this.PropertyLabel.TabIndex = 6;
			this.PropertyLabel.Text = "Property Name";
			// 
			// CaptionTextbox
			// 
			this.CaptionTextbox.Location = new System.Drawing.Point(167, 194);
			this.CaptionTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.CaptionTextbox.Name = "CaptionTextbox";
			this.CaptionTextbox.Size = new System.Drawing.Size(433, 26);
			this.CaptionTextbox.TabIndex = 11;
			// 
			// CaptionLabel
			// 
			this.CaptionLabel.Location = new System.Drawing.Point(13, 197);
			this.CaptionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.CaptionLabel.Name = "CaptionLabel";
			this.CaptionLabel.Size = new System.Drawing.Size(153, 20);
			this.CaptionLabel.TabIndex = 10;
			this.CaptionLabel.Text = "Caption";
			// 
			// DataTypeLabel
			// 
			this.DataTypeLabel.Location = new System.Drawing.Point(13, 233);
			this.DataTypeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.DataTypeLabel.Name = "DataTypeLabel";
			this.DataTypeLabel.Size = new System.Drawing.Size(153, 20);
			this.DataTypeLabel.TabIndex = 12;
			this.DataTypeLabel.Text = "Data Type Name";
			// 
			// ColumnNameTextbox
			// 
			this.ColumnNameTextbox.Location = new System.Drawing.Point(167, 86);
			this.ColumnNameTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ColumnNameTextbox.Name = "ColumnNameTextbox";
			this.ColumnNameTextbox.Size = new System.Drawing.Size(433, 26);
			this.ColumnNameTextbox.TabIndex = 5;
			this.ColumnNameTextbox.TextChanged += new System.EventHandler(this.ColumnNameTextbox_TextChanged);
			this.ColumnNameTextbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ColumnNameTextbox_KeyPress);
			// 
			// ColumnNameLabel
			// 
			this.ColumnNameLabel.Location = new System.Drawing.Point(13, 89);
			this.ColumnNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ColumnNameLabel.Name = "ColumnNameLabel";
			this.ColumnNameLabel.Size = new System.Drawing.Size(153, 20);
			this.ColumnNameLabel.TabIndex = 4;
			this.ColumnNameLabel.Text = "Column Name";
			// 
			// ParentTextbox
			// 
			this.ParentTextbox.Location = new System.Drawing.Point(167, 14);
			this.ParentTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ParentTextbox.Name = "ParentTextbox";
			this.ParentTextbox.ReadOnly = true;
			this.ParentTextbox.Size = new System.Drawing.Size(433, 26);
			this.ParentTextbox.TabIndex = 1;
			this.ParentTextbox.TabStop = false;
			// 
			// ParentLabel
			// 
			this.ParentLabel.Location = new System.Drawing.Point(13, 17);
			this.ParentLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ParentLabel.Name = "ParentLabel";
			this.ParentLabel.Size = new System.Drawing.Size(153, 20);
			this.ParentLabel.TabIndex = 0;
			this.ParentLabel.Text = "View Join Name";
			// 
			// FormCancelButton
			// 
			this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.FormCancelButton.Location = new System.Drawing.Point(489, 266);
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
			this.OKButton.Location = new System.Drawing.Point(369, 266);
			this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(112, 35);
			this.OKButton.TabIndex = 14;
			this.OKButton.Text = "&OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// JoinColumnMenu
			// 
			this.JoinColumnMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.JoinColumnMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.JoinColumnHelp});
			this.JoinColumnMenu.Name = "JoinColumnMenu";
			this.JoinColumnMenu.Size = new System.Drawing.Size(241, 67);
			// 
			// JoinColumnHelp
			// 
			this.JoinColumnHelp.Name = "JoinColumnHelp";
			this.JoinColumnHelp.ShortcutKeyDisplayString = "F1";
			this.JoinColumnHelp.Size = new System.Drawing.Size(240, 30);
			this.JoinColumnHelp.Text = "&Help";
			this.JoinColumnHelp.Click += new System.EventHandler(this.JoinColumnHelp_Click);
			// 
			// ViewJoinColumnDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(613, 315);
			this.ContextMenuStrip = this.JoinColumnMenu;
			this.Controls.Add(this.TemplateLabel);
			this.Controls.Add(this.TemplateCombo);
			this.Controls.Add(this.DataTypeCombo);
			this.Controls.Add(this.RenameTextbox);
			this.Controls.Add(this.RenameLabel);
			this.Controls.Add(this.PropertyTextbox);
			this.Controls.Add(this.PropertyLabel);
			this.Controls.Add(this.CaptionTextbox);
			this.Controls.Add(this.CaptionLabel);
			this.Controls.Add(this.DataTypeLabel);
			this.Controls.Add(this.ColumnNameTextbox);
			this.Controls.Add(this.ColumnNameLabel);
			this.Controls.Add(this.ParentTextbox);
			this.Controls.Add(this.ParentLabel);
			this.Controls.Add(this.FormCancelButton);
			this.Controls.Add(this.OKButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ViewJoinColumnDetail";
			this.Text = "ViewJoinColumn Detail";
			this.Load += new System.EventHandler(this.ViewJoinColumnDetail_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ViewJoinColumnDetail_KeyDown);
			this.JoinColumnMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label TemplateLabel;
		private System.Windows.Forms.ComboBox TemplateCombo;
		private System.Windows.Forms.ComboBox DataTypeCombo;
		private System.Windows.Forms.TextBox RenameTextbox;
		private System.Windows.Forms.Label RenameLabel;
		private System.Windows.Forms.TextBox PropertyTextbox;
		private System.Windows.Forms.Label PropertyLabel;
		private System.Windows.Forms.TextBox CaptionTextbox;
		private System.Windows.Forms.Label CaptionLabel;
		private System.Windows.Forms.Label DataTypeLabel;
		private System.Windows.Forms.TextBox ColumnNameTextbox;
		private System.Windows.Forms.Label ColumnNameLabel;
		private System.Windows.Forms.TextBox ParentTextbox;
		private System.Windows.Forms.Label ParentLabel;
		private System.Windows.Forms.Button FormCancelButton;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.ContextMenuStrip JoinColumnMenu;
		private System.Windows.Forms.ToolStripMenuItem JoinColumnHelp;
	}
}