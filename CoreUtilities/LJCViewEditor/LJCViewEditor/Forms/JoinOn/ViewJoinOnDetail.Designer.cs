namespace LJCViewEditor
{
	partial class ViewJoinOnDetail
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
      this.ToColumnCombo = new System.Windows.Forms.ComboBox();
      this.ToColumnLabel = new System.Windows.Forms.Label();
      this.FromColumnCombo = new System.Windows.Forms.ComboBox();
      this.FromColumnLabel = new System.Windows.Forms.Label();
      this.ParentTextbox = new System.Windows.Forms.TextBox();
      this.ParentLabel = new System.Windows.Forms.Label();
      this.OperatorTextbox = new System.Windows.Forms.TextBox();
      this.OperatorLabel = new System.Windows.Forms.Label();
      this.JoinOnMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.JoinOnHelp = new System.Windows.Forms.ToolStripMenuItem();
      this.JoinOnMenu.SuspendLayout();
      this.SuspendLayout();
      // 
      // FormCancelButton
      // 
      this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.FormCancelButton.Location = new System.Drawing.Point(606, 176);
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
      this.OKButton.Location = new System.Drawing.Point(458, 176);
      this.OKButton.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
      this.OKButton.Name = "OKButton";
      this.OKButton.Size = new System.Drawing.Size(137, 38);
      this.OKButton.TabIndex = 8;
      this.OKButton.Text = "&OK";
      this.OKButton.UseVisualStyleBackColor = true;
      this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
      // 
      // ToColumnCombo
      // 
      this.ToColumnCombo.Location = new System.Drawing.Point(213, 95);
      this.ToColumnCombo.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
      this.ToColumnCombo.Name = "ToColumnCombo";
      this.ToColumnCombo.Size = new System.Drawing.Size(528, 30);
      this.ToColumnCombo.TabIndex = 5;
      // 
      // ToColumnLabel
      // 
      this.ToColumnLabel.Location = new System.Drawing.Point(13, 98);
      this.ToColumnLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
      this.ToColumnLabel.Name = "ToColumnLabel";
      this.ToColumnLabel.Size = new System.Drawing.Size(193, 32);
      this.ToColumnLabel.TabIndex = 4;
      this.ToColumnLabel.Text = "To Column Name";
      // 
      // FromColumnCombo
      // 
      this.FromColumnCombo.Location = new System.Drawing.Point(213, 55);
      this.FromColumnCombo.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
      this.FromColumnCombo.Name = "FromColumnCombo";
      this.FromColumnCombo.Size = new System.Drawing.Size(528, 30);
      this.FromColumnCombo.TabIndex = 3;
      // 
      // FromColumnLabel
      // 
      this.FromColumnLabel.Location = new System.Drawing.Point(13, 58);
      this.FromColumnLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
      this.FromColumnLabel.Name = "FromColumnLabel";
      this.FromColumnLabel.Size = new System.Drawing.Size(193, 32);
      this.FromColumnLabel.TabIndex = 2;
      this.FromColumnLabel.Text = "From Column Name";
      // 
      // ParentTextbox
      // 
      this.ParentTextbox.Location = new System.Drawing.Point(213, 15);
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
      this.ParentLabel.Size = new System.Drawing.Size(193, 32);
      this.ParentLabel.TabIndex = 0;
      this.ParentLabel.Text = "Join Name";
      // 
      // OperatorTextbox
      // 
      this.OperatorTextbox.Location = new System.Drawing.Point(213, 134);
      this.OperatorTextbox.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
      this.OperatorTextbox.Name = "OperatorTextbox";
      this.OperatorTextbox.Size = new System.Drawing.Size(528, 28);
      this.OperatorTextbox.TabIndex = 7;
      // 
      // OperatorLabel
      // 
      this.OperatorLabel.Location = new System.Drawing.Point(13, 137);
      this.OperatorLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
      this.OperatorLabel.Name = "OperatorLabel";
      this.OperatorLabel.Size = new System.Drawing.Size(193, 32);
      this.OperatorLabel.TabIndex = 6;
      this.OperatorLabel.Text = "Join On Operator";
      // 
      // JoinOnMenu
      // 
      this.JoinOnMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.JoinOnMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.JoinOnHelp});
      this.JoinOnMenu.Name = "JoinOnMenu";
      this.JoinOnMenu.Size = new System.Drawing.Size(153, 36);
      // 
      // JoinOnHelp
      // 
      this.JoinOnHelp.Name = "JoinOnHelp";
      this.JoinOnHelp.ShortcutKeyDisplayString = "F1";
      this.JoinOnHelp.Size = new System.Drawing.Size(152, 32);
      this.JoinOnHelp.Text = "&Help";
      this.JoinOnHelp.Click += new System.EventHandler(this.JoinOnHelp_Click);
      // 
      // ViewJoinOnDetail
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(759, 224);
      this.ContextMenuStrip = this.JoinOnMenu;
      this.Controls.Add(this.OperatorTextbox);
      this.Controls.Add(this.OperatorLabel);
      this.Controls.Add(this.FormCancelButton);
      this.Controls.Add(this.OKButton);
      this.Controls.Add(this.ToColumnCombo);
      this.Controls.Add(this.ToColumnLabel);
      this.Controls.Add(this.FromColumnCombo);
      this.Controls.Add(this.FromColumnLabel);
      this.Controls.Add(this.ParentTextbox);
      this.Controls.Add(this.ParentLabel);
      this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.KeyPreview = true;
      this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "ViewJoinOnDetail";
      this.Text = "ViewJoinOn Detail";
      this.Load += new System.EventHandler(this.ViewJoinOnDetail_Load);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ViewJoinOnDetail_KeyDown);
      this.JoinOnMenu.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button FormCancelButton;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.ComboBox ToColumnCombo;
		private System.Windows.Forms.Label ToColumnLabel;
		private System.Windows.Forms.ComboBox FromColumnCombo;
		private System.Windows.Forms.Label FromColumnLabel;
		private System.Windows.Forms.TextBox ParentTextbox;
		private System.Windows.Forms.Label ParentLabel;
		private System.Windows.Forms.TextBox OperatorTextbox;
		private System.Windows.Forms.Label OperatorLabel;
		private System.Windows.Forms.ContextMenuStrip JoinOnMenu;
		private System.Windows.Forms.ToolStripMenuItem JoinOnHelp;
	}
}