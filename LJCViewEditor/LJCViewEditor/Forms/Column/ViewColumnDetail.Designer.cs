﻿namespace LJCViewEditor
{
	partial class ViewColumnDetail
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
			this.DataTypeLabel = new System.Windows.Forms.Label();
			this.ColumnNameTextbox = new System.Windows.Forms.TextBox();
			this.ColumnNameLabel = new System.Windows.Forms.Label();
			this.ParentTextbox = new System.Windows.Forms.TextBox();
			this.ParentLabel = new System.Windows.Forms.Label();
			this.FormCancelButton = new System.Windows.Forms.Button();
			this.OKButton = new System.Windows.Forms.Button();
			this.CaptionTextbox = new System.Windows.Forms.TextBox();
			this.CaptionLabel = new System.Windows.Forms.Label();
			this.ValueTextbox = new System.Windows.Forms.TextBox();
			this.ValueLabel = new System.Windows.Forms.Label();
			this.RenameTextbox = new System.Windows.Forms.TextBox();
			this.RenameLabel = new System.Windows.Forms.Label();
			this.PropertyTextbox = new System.Windows.Forms.TextBox();
			this.PropertyLabel = new System.Windows.Forms.Label();
			this.DataTypeCombo = new System.Windows.Forms.ComboBox();
			this.TemplateColumnCombo = new System.Windows.Forms.ComboBox();
			this.TemplateColumnLabel = new System.Windows.Forms.Label();
			this.PrimaryKeyCheckBox = new System.Windows.Forms.CheckBox();
			this.ViewColumnMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ViewColumnHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.ViewColumnMenu.SuspendLayout();
			this.SuspendLayout();
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
			this.ParentLabel.Text = "View Name";
			// 
			// FormCancelButton
			// 
			this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.FormCancelButton.Location = new System.Drawing.Point(489, 331);
			this.FormCancelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.FormCancelButton.Name = "FormCancelButton";
			this.FormCancelButton.Size = new System.Drawing.Size(112, 35);
			this.FormCancelButton.TabIndex = 18;
			this.FormCancelButton.Text = "Cancel";
			this.FormCancelButton.UseVisualStyleBackColor = true;
			this.FormCancelButton.Click += new System.EventHandler(this.FormCancelButton_Click);
			// 
			// OKButton
			// 
			this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OKButton.Location = new System.Drawing.Point(369, 332);
			this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(112, 34);
			this.OKButton.TabIndex = 17;
			this.OKButton.Text = "&OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
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
			// ValueTextbox
			// 
			this.ValueTextbox.Location = new System.Drawing.Point(167, 266);
			this.ValueTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ValueTextbox.Name = "ValueTextbox";
			this.ValueTextbox.Size = new System.Drawing.Size(433, 26);
			this.ValueTextbox.TabIndex = 15;
			// 
			// ValueLabel
			// 
			this.ValueLabel.Location = new System.Drawing.Point(13, 269);
			this.ValueLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ValueLabel.Name = "ValueLabel";
			this.ValueLabel.Size = new System.Drawing.Size(153, 20);
			this.ValueLabel.TabIndex = 14;
			this.ValueLabel.Text = "Value";
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
			// DataTypeCombo
			// 
			this.DataTypeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.DataTypeCombo.FormattingEnabled = true;
			this.DataTypeCombo.Location = new System.Drawing.Point(167, 229);
			this.DataTypeCombo.Name = "DataTypeCombo";
			this.DataTypeCombo.Size = new System.Drawing.Size(433, 28);
			this.DataTypeCombo.TabIndex = 13;
			// 
			// TemplateColumnCombo
			// 
			this.TemplateColumnCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.TemplateColumnCombo.FormattingEnabled = true;
			this.TemplateColumnCombo.Location = new System.Drawing.Point(167, 49);
			this.TemplateColumnCombo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
			this.TemplateColumnCombo.Name = "TemplateColumnCombo";
			this.TemplateColumnCombo.Size = new System.Drawing.Size(300, 28);
			this.TemplateColumnCombo.TabIndex = 3;
			this.TemplateColumnCombo.SelectedIndexChanged += new System.EventHandler(this.TemplateCombo_SelectedIndexChanged);
			// 
			// TemplateColumnLabel
			// 
			this.TemplateColumnLabel.Location = new System.Drawing.Point(13, 53);
			this.TemplateColumnLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.TemplateColumnLabel.Name = "TemplateColumnLabel";
			this.TemplateColumnLabel.Size = new System.Drawing.Size(153, 20);
			this.TemplateColumnLabel.TabIndex = 2;
			this.TemplateColumnLabel.Text = "Template Column";
			// 
			// PrimaryKeyCheckBox
			// 
			this.PrimaryKeyCheckBox.AutoSize = true;
			this.PrimaryKeyCheckBox.Location = new System.Drawing.Point(167, 300);
			this.PrimaryKeyCheckBox.Name = "PrimaryKeyCheckBox";
			this.PrimaryKeyCheckBox.Size = new System.Drawing.Size(117, 24);
			this.PrimaryKeyCheckBox.TabIndex = 16;
			this.PrimaryKeyCheckBox.Text = "Primary Key";
			this.PrimaryKeyCheckBox.UseVisualStyleBackColor = true;
			// 
			// ViewColumnMenu
			// 
			this.ViewColumnMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.ViewColumnMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewColumnHelp});
			this.ViewColumnMenu.Name = "ViewColumnMenu";
			this.ViewColumnMenu.Size = new System.Drawing.Size(153, 34);
			// 
			// ViewColumnHelp
			// 
			this.ViewColumnHelp.Name = "ViewColumnHelp";
			this.ViewColumnHelp.ShortcutKeyDisplayString = "F1";
			this.ViewColumnHelp.Size = new System.Drawing.Size(152, 30);
			this.ViewColumnHelp.Text = "&Help";
			this.ViewColumnHelp.Click += new System.EventHandler(this.ViewColumnHelp_Click);
			// 
			// ViewColumnDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(614, 380);
			this.ContextMenuStrip = this.ViewColumnMenu;
			this.Controls.Add(this.PrimaryKeyCheckBox);
			this.Controls.Add(this.TemplateColumnLabel);
			this.Controls.Add(this.TemplateColumnCombo);
			this.Controls.Add(this.DataTypeCombo);
			this.Controls.Add(this.RenameTextbox);
			this.Controls.Add(this.RenameLabel);
			this.Controls.Add(this.PropertyTextbox);
			this.Controls.Add(this.PropertyLabel);
			this.Controls.Add(this.CaptionTextbox);
			this.Controls.Add(this.CaptionLabel);
			this.Controls.Add(this.ValueTextbox);
			this.Controls.Add(this.ValueLabel);
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
			this.Name = "ViewColumnDetail";
			this.Text = "ViewColumn Detail";
			this.Load += new System.EventHandler(this.ViewColumnDetail_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ViewColumnDetail_KeyDown);
			this.ViewColumnMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label DataTypeLabel;
		private System.Windows.Forms.TextBox ColumnNameTextbox;
		private System.Windows.Forms.Label ColumnNameLabel;
		private System.Windows.Forms.TextBox ParentTextbox;
		private System.Windows.Forms.Label ParentLabel;
		private System.Windows.Forms.Button FormCancelButton;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.TextBox CaptionTextbox;
		private System.Windows.Forms.Label CaptionLabel;
		private System.Windows.Forms.TextBox ValueTextbox;
		private System.Windows.Forms.Label ValueLabel;
		private System.Windows.Forms.TextBox RenameTextbox;
		private System.Windows.Forms.Label RenameLabel;
		private System.Windows.Forms.TextBox PropertyTextbox;
		private System.Windows.Forms.Label PropertyLabel;
		private System.Windows.Forms.ComboBox DataTypeCombo;
		private System.Windows.Forms.ComboBox TemplateColumnCombo;
		private System.Windows.Forms.Label TemplateColumnLabel;
		private System.Windows.Forms.CheckBox PrimaryKeyCheckBox;
		private System.Windows.Forms.ContextMenuStrip ViewColumnMenu;
		private System.Windows.Forms.ToolStripMenuItem ViewColumnHelp;
	}
}