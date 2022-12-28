namespace LJCViewEditor
{
	partial class ViewJoinDetail
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
			this.JoinTypeTextbox = new System.Windows.Forms.TextBox();
			this.JoinTypeLabel = new System.Windows.Forms.Label();
			this.JoinTableCombo = new LJCWinFormControls.LJCItemCombo();
			this.JoinTableLabel = new System.Windows.Forms.Label();
			this.ParentTextbox = new System.Windows.Forms.TextBox();
			this.ParentLabel = new System.Windows.Forms.Label();
			this.FormCancelButton = new System.Windows.Forms.Button();
			this.OKButton = new System.Windows.Forms.Button();
			this.AliasTextbox = new System.Windows.Forms.TextBox();
			this.AliasLabel = new System.Windows.Forms.Label();
			this.JoinMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.JoinHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.JoinMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// JoinTypeTextbox
			// 
			this.JoinTypeTextbox.Location = new System.Drawing.Point(165, 86);
			this.JoinTypeTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.JoinTypeTextbox.Name = "JoinTypeTextbox";
			this.JoinTypeTextbox.Size = new System.Drawing.Size(433, 26);
			this.JoinTypeTextbox.TabIndex = 5;
			// 
			// JoinTypeLabel
			// 
			this.JoinTypeLabel.Location = new System.Drawing.Point(11, 89);
			this.JoinTypeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.JoinTypeLabel.Name = "JoinTypeLabel";
			this.JoinTypeLabel.Size = new System.Drawing.Size(151, 20);
			this.JoinTypeLabel.TabIndex = 4;
			this.JoinTypeLabel.Text = "Join Type";
			// 
			// JoinTableCombo
			// 
			this.JoinTableCombo.Location = new System.Drawing.Point(165, 50);
			this.JoinTableCombo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.JoinTableCombo.Name = "JoinTableCombo";
			this.JoinTableCombo.Size = new System.Drawing.Size(433, 28);
			this.JoinTableCombo.TabIndex = 3;
			this.JoinTableCombo.TextChanged += new System.EventHandler(this.TableNameTextbox_TextChanged);
			this.JoinTableCombo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TableNameTextbox_KeyPress);
			// 
			// JoinTableLabel
			// 
			this.JoinTableLabel.Location = new System.Drawing.Point(11, 55);
			this.JoinTableLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.JoinTableLabel.Name = "JoinTableLabel";
			this.JoinTableLabel.Size = new System.Drawing.Size(151, 20);
			this.JoinTableLabel.TabIndex = 2;
			this.JoinTableLabel.Text = "Table Name";
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
			// FormCancelButton
			// 
			this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.FormCancelButton.Location = new System.Drawing.Point(487, 160);
			this.FormCancelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.FormCancelButton.Name = "FormCancelButton";
			this.FormCancelButton.Size = new System.Drawing.Size(112, 35);
			this.FormCancelButton.TabIndex = 9;
			this.FormCancelButton.Text = "Cancel";
			this.FormCancelButton.UseVisualStyleBackColor = true;
			this.FormCancelButton.Click += new System.EventHandler(this.FormCancelButton_Click);
			// 
			// OKButton
			// 
			this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OKButton.Location = new System.Drawing.Point(366, 160);
			this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(112, 35);
			this.OKButton.TabIndex = 8;
			this.OKButton.Text = "&OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// AliasTextbox
			// 
			this.AliasTextbox.Location = new System.Drawing.Point(165, 122);
			this.AliasTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.AliasTextbox.Name = "AliasTextbox";
			this.AliasTextbox.Size = new System.Drawing.Size(300, 26);
			this.AliasTextbox.TabIndex = 7;
			// 
			// AliasLabel
			// 
			this.AliasLabel.Location = new System.Drawing.Point(13, 125);
			this.AliasLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.AliasLabel.Name = "AliasLabel";
			this.AliasLabel.Size = new System.Drawing.Size(151, 20);
			this.AliasLabel.TabIndex = 6;
			this.AliasLabel.Text = "Alias";
			// 
			// JoinMenu
			// 
			this.JoinMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.JoinMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.JoinHelp});
			this.JoinMenu.Name = "JoinMenu";
			this.JoinMenu.Size = new System.Drawing.Size(241, 67);
			// 
			// JoinHelp
			// 
			this.JoinHelp.Name = "JoinHelp";
			this.JoinHelp.ShortcutKeyDisplayString = "F1";
			this.JoinHelp.Size = new System.Drawing.Size(240, 30);
			this.JoinHelp.Text = "&Help";
			this.JoinHelp.Click += new System.EventHandler(this.JoinHelp_Click);
			// 
			// ViewJoinDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(612, 204);
			this.ContextMenuStrip = this.JoinMenu;
			this.Controls.Add(this.AliasLabel);
			this.Controls.Add(this.AliasTextbox);
			this.Controls.Add(this.FormCancelButton);
			this.Controls.Add(this.OKButton);
			this.Controls.Add(this.JoinTypeTextbox);
			this.Controls.Add(this.JoinTypeLabel);
			this.Controls.Add(this.JoinTableCombo);
			this.Controls.Add(this.JoinTableLabel);
			this.Controls.Add(this.ParentTextbox);
			this.Controls.Add(this.ParentLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ViewJoinDetail";
			this.Text = "ViewJoin Detail";
			this.Load += new System.EventHandler(this.ViewJoinDetail_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ViewJoinDetail_KeyDown);
			this.JoinMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox JoinTypeTextbox;
		private System.Windows.Forms.Label JoinTypeLabel;
		private LJCWinFormControls.LJCItemCombo JoinTableCombo;
		private System.Windows.Forms.Label JoinTableLabel;
		private System.Windows.Forms.TextBox ParentTextbox;
		private System.Windows.Forms.Label ParentLabel;
		private System.Windows.Forms.Button FormCancelButton;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.TextBox AliasTextbox;
		private System.Windows.Forms.Label AliasLabel;
		private System.Windows.Forms.ContextMenuStrip JoinMenu;
		private System.Windows.Forms.ToolStripMenuItem JoinHelp;
	}
}