namespace LJCViewEditor
{
	partial class ViewFilterDetail
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
			this.FormCancelButton = new System.Windows.Forms.Button();
			this.OKButton = new System.Windows.Forms.Button();
			this.ParentTextbox = new System.Windows.Forms.TextBox();
			this.ParentLabel = new System.Windows.Forms.Label();
			this.NameTextbox = new System.Windows.Forms.TextBox();
			this.NameLabel = new System.Windows.Forms.Label();
			this.OperatorCombo = new System.Windows.Forms.ComboBox();
			this.OperatorLabel = new System.Windows.Forms.Label();
			this.FilterMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.FilterHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.FilterMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// FormCancelButton
			// 
			this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.FormCancelButton.Location = new System.Drawing.Point(487, 124);
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
			this.OKButton.Location = new System.Drawing.Point(366, 124);
			this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(112, 35);
			this.OKButton.TabIndex = 4;
			this.OKButton.Text = "&OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// ParentTextbox
			// 
			this.ParentTextbox.Location = new System.Drawing.Point(165, 14);
			this.ParentTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ParentTextbox.Name = "ParentTextbox";
			this.ParentTextbox.ReadOnly = true;
			this.ParentTextbox.Size = new System.Drawing.Size(433, 26);
			this.ParentTextbox.TabIndex = 1;
			this.ParentTextbox.TabStop = false;
			// 
			// ParentLabel
			// 
			this.ParentLabel.Location = new System.Drawing.Point(11, 19);
			this.ParentLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ParentLabel.Name = "ParentLabel";
			this.ParentLabel.Size = new System.Drawing.Size(151, 20);
			this.ParentLabel.TabIndex = 0;
			this.ParentLabel.Text = "View Name";
			// 
			// NameTextbox
			// 
			this.NameTextbox.Location = new System.Drawing.Point(166, 50);
			this.NameTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.NameTextbox.Name = "NameTextbox";
			this.NameTextbox.Size = new System.Drawing.Size(433, 26);
			this.NameTextbox.TabIndex = 3;
			// 
			// NameLabel
			// 
			this.NameLabel.Location = new System.Drawing.Point(12, 55);
			this.NameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.NameLabel.Name = "NameLabel";
			this.NameLabel.Size = new System.Drawing.Size(151, 20);
			this.NameLabel.TabIndex = 2;
			this.NameLabel.Text = "Name";
			// 
			// OperatorCombo
			// 
			this.OperatorCombo.Location = new System.Drawing.Point(166, 86);
			this.OperatorCombo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.OperatorCombo.Name = "OperatorCombo";
			this.OperatorCombo.Size = new System.Drawing.Size(70, 28);
			this.OperatorCombo.TabIndex = 7;
			// 
			// OperatorLabel
			// 
			this.OperatorLabel.Location = new System.Drawing.Point(12, 91);
			this.OperatorLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.OperatorLabel.Name = "OperatorLabel";
			this.OperatorLabel.Size = new System.Drawing.Size(153, 20);
			this.OperatorLabel.TabIndex = 6;
			this.OperatorLabel.Text = "Boolean Operator";
			// 
			// FilterMenu
			// 
			this.FilterMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.FilterMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FilterHelp});
			this.FilterMenu.Name = "FilterMenu";
			this.FilterMenu.Size = new System.Drawing.Size(153, 34);
			// 
			// FilterHelp
			// 
			this.FilterHelp.Name = "FilterHelp";
			this.FilterHelp.ShortcutKeyDisplayString = "F1";
			this.FilterHelp.Size = new System.Drawing.Size(152, 30);
			this.FilterHelp.Text = "&Help";
			this.FilterHelp.Click += new System.EventHandler(this.FilterHelp_Click);
			// 
			// ViewFilterDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(612, 168);
			this.ContextMenuStrip = this.FilterMenu;
			this.Controls.Add(this.OperatorCombo);
			this.Controls.Add(this.OperatorLabel);
			this.Controls.Add(this.NameTextbox);
			this.Controls.Add(this.NameLabel);
			this.Controls.Add(this.ParentTextbox);
			this.Controls.Add(this.ParentLabel);
			this.Controls.Add(this.FormCancelButton);
			this.Controls.Add(this.OKButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ViewFilterDetail";
			this.Text = "ViewFilter Detail";
			this.Load += new System.EventHandler(this.ViewFilterDetail_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ViewFilterDetail_KeyDown);
			this.FilterMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button FormCancelButton;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.TextBox ParentTextbox;
		private System.Windows.Forms.Label ParentLabel;
		private System.Windows.Forms.TextBox NameTextbox;
		private System.Windows.Forms.Label NameLabel;
		private System.Windows.Forms.ComboBox OperatorCombo;
		private System.Windows.Forms.Label OperatorLabel;
		private System.Windows.Forms.ContextMenuStrip FilterMenu;
		private System.Windows.Forms.ToolStripMenuItem FilterHelp;
	}
}