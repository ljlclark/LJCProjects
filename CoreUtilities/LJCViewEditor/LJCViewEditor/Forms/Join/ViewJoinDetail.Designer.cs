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
      this.JoinTypeTextbox.Location = new System.Drawing.Point(202, 95);
      this.JoinTypeTextbox.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
      this.JoinTypeTextbox.Name = "JoinTypeTextbox";
      this.JoinTypeTextbox.Size = new System.Drawing.Size(528, 28);
      this.JoinTypeTextbox.TabIndex = 5;
      // 
      // JoinTypeLabel
      // 
      this.JoinTypeLabel.Location = new System.Drawing.Point(13, 98);
      this.JoinTypeLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
      this.JoinTypeLabel.Name = "JoinTypeLabel";
      this.JoinTypeLabel.Size = new System.Drawing.Size(185, 32);
      this.JoinTypeLabel.TabIndex = 4;
      this.JoinTypeLabel.Text = "Join Type";
      // 
      // JoinTableCombo
      // 
      this.JoinTableCombo.Location = new System.Drawing.Point(202, 55);
      this.JoinTableCombo.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
      this.JoinTableCombo.Name = "JoinTableCombo";
      this.JoinTableCombo.Size = new System.Drawing.Size(528, 30);
      this.JoinTableCombo.TabIndex = 3;
      // 
      // JoinTableLabel
      // 
      this.JoinTableLabel.Location = new System.Drawing.Point(13, 58);
      this.JoinTableLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
      this.JoinTableLabel.Name = "JoinTableLabel";
      this.JoinTableLabel.Size = new System.Drawing.Size(185, 32);
      this.JoinTableLabel.TabIndex = 2;
      this.JoinTableLabel.Text = "Table Name";
      // 
      // ParentTextbox
      // 
      this.ParentTextbox.Location = new System.Drawing.Point(202, 15);
      this.ParentTextbox.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
      this.ParentTextbox.Name = "ParentTextbox";
      this.ParentTextbox.ReadOnly = true;
      this.ParentTextbox.Size = new System.Drawing.Size(528, 28);
      this.ParentTextbox.TabIndex = 1;
      this.ParentTextbox.TabStop = false;
      // 
      // ParentLabel
      // 
      this.ParentLabel.Location = new System.Drawing.Point(13, 18);
      this.ParentLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
      this.ParentLabel.Name = "ParentLabel";
      this.ParentLabel.Size = new System.Drawing.Size(185, 32);
      this.ParentLabel.TabIndex = 0;
      this.ParentLabel.Text = "View Name";
      // 
      // FormCancelButton
      // 
      this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.FormCancelButton.Location = new System.Drawing.Point(595, 176);
      this.FormCancelButton.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
      this.FormCancelButton.Name = "FormCancelButton";
      this.FormCancelButton.Size = new System.Drawing.Size(137, 38);
      this.FormCancelButton.TabIndex = 9;
      this.FormCancelButton.Text = "Cancel";
      this.FormCancelButton.UseVisualStyleBackColor = true;
      this.FormCancelButton.Click += new System.EventHandler(this.FormCancelButton_Click);
      // 
      // OKButton
      // 
      this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.OKButton.Location = new System.Drawing.Point(447, 176);
      this.OKButton.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
      this.OKButton.Name = "OKButton";
      this.OKButton.Size = new System.Drawing.Size(137, 38);
      this.OKButton.TabIndex = 8;
      this.OKButton.Text = "&OK";
      this.OKButton.UseVisualStyleBackColor = true;
      this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
      // 
      // AliasTextbox
      // 
      this.AliasTextbox.Location = new System.Drawing.Point(202, 134);
      this.AliasTextbox.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
      this.AliasTextbox.Name = "AliasTextbox";
      this.AliasTextbox.Size = new System.Drawing.Size(366, 28);
      this.AliasTextbox.TabIndex = 7;
      // 
      // AliasLabel
      // 
      this.AliasLabel.Location = new System.Drawing.Point(16, 137);
      this.AliasLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
      this.AliasLabel.Name = "AliasLabel";
      this.AliasLabel.Size = new System.Drawing.Size(185, 32);
      this.AliasLabel.TabIndex = 6;
      this.AliasLabel.Text = "Alias";
      // 
      // JoinMenu
      // 
      this.JoinMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.JoinMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.JoinHelp});
      this.JoinMenu.Name = "JoinMenu";
      this.JoinMenu.Size = new System.Drawing.Size(153, 36);
      // 
      // JoinHelp
      // 
      this.JoinHelp.Name = "JoinHelp";
      this.JoinHelp.ShortcutKeyDisplayString = "F1";
      this.JoinHelp.Size = new System.Drawing.Size(152, 32);
      this.JoinHelp.Text = "&Help";
      this.JoinHelp.Click += new System.EventHandler(this.JoinHelp_Click);
      // 
      // ViewJoinDetail
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(748, 224);
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
      this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.KeyPreview = true;
      this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
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