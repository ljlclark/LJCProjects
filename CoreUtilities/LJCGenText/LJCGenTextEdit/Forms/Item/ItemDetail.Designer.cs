namespace LJCGenTextEdit
{
	partial class ItemDetail
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
			this.NameTextbox = new System.Windows.Forms.TextBox();
			this.NameLabel = new System.Windows.Forms.Label();
			this.ParentNameTextbox = new System.Windows.Forms.TextBox();
			this.ParentNameLabel = new System.Windows.Forms.Label();
			this.DetailMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.DetailMenuHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.DetailMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// FormCancelButton
			// 
			this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.FormCancelButton.Location = new System.Drawing.Point(448, 92);
			this.FormCancelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.FormCancelButton.Name = "FormCancelButton";
			this.FormCancelButton.Size = new System.Drawing.Size(112, 35);
			this.FormCancelButton.TabIndex = 3;
			this.FormCancelButton.Text = "Cancel";
			this.FormCancelButton.UseVisualStyleBackColor = true;
			this.FormCancelButton.Click += new System.EventHandler(this.FormCancelButton_Click);
			// 
			// OKButton
			// 
			this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OKButton.Location = new System.Drawing.Point(326, 92);
			this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(112, 35);
			this.OKButton.TabIndex = 2;
			this.OKButton.Text = "&OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// NameTextbox
			// 
			this.NameTextbox.Location = new System.Drawing.Point(145, 53);
			this.NameTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.NameTextbox.Name = "NameTextbox";
			this.NameTextbox.Size = new System.Drawing.Size(413, 26);
			this.NameTextbox.TabIndex = 1;
			// 
			// NameLabel
			// 
			this.NameLabel.Location = new System.Drawing.Point(18, 58);
			this.NameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.NameLabel.Name = "NameLabel";
			this.NameLabel.Size = new System.Drawing.Size(124, 20);
			this.NameLabel.TabIndex = 0;
			this.NameLabel.Text = "Name";
			// 
			// ParentNameTextbox
			// 
			this.ParentNameTextbox.Location = new System.Drawing.Point(145, 17);
			this.ParentNameTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ParentNameTextbox.Name = "ParentNameTextbox";
			this.ParentNameTextbox.ReadOnly = true;
			this.ParentNameTextbox.Size = new System.Drawing.Size(413, 26);
			this.ParentNameTextbox.TabIndex = 5;
			// 
			// ParentNameLabel
			// 
			this.ParentNameLabel.Location = new System.Drawing.Point(18, 22);
			this.ParentNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ParentNameLabel.Name = "ParentNameLabel";
			this.ParentNameLabel.Size = new System.Drawing.Size(124, 20);
			this.ParentNameLabel.TabIndex = 4;
			this.ParentNameLabel.Text = "Section Name";
			// 
			// DetailMenu
			// 
			this.DetailMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.DetailMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DetailMenuHelp});
			this.DetailMenu.Name = "DetailMenu";
			this.DetailMenu.Size = new System.Drawing.Size(241, 67);
			// 
			// DetailMenuHelp
			// 
			this.DetailMenuHelp.Name = "DetailMenuHelp";
			this.DetailMenuHelp.ShortcutKeyDisplayString = "F1";
			this.DetailMenuHelp.Size = new System.Drawing.Size(240, 30);
			this.DetailMenuHelp.Text = "&Help";
			this.DetailMenuHelp.Click += new System.EventHandler(this.DetailMenuHelp_Click);
			// 
			// ItemDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(578, 139);
			this.ContextMenuStrip = this.DetailMenu;
			this.Controls.Add(this.ParentNameTextbox);
			this.Controls.Add(this.ParentNameLabel);
			this.Controls.Add(this.FormCancelButton);
			this.Controls.Add(this.OKButton);
			this.Controls.Add(this.NameTextbox);
			this.Controls.Add(this.NameLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ItemDetail";
			this.Text = "Item Detail";
			this.Load += new System.EventHandler(this.ItemDetail_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ItemDetail_KeyDown);
			this.DetailMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button FormCancelButton;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.TextBox NameTextbox;
		private System.Windows.Forms.Label NameLabel;
		private System.Windows.Forms.TextBox ParentNameTextbox;
		private System.Windows.Forms.Label ParentNameLabel;
		private System.Windows.Forms.ContextMenuStrip DetailMenu;
		private System.Windows.Forms.ToolStripMenuItem DetailMenuHelp;
	}
}