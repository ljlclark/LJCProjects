namespace LJCViewEditor
{
	partial class ViewOrderByDetail
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
      this.ColumnNameTextbox = new System.Windows.Forms.ComboBox();
      this.ColumnNameLabel = new System.Windows.Forms.Label();
      this.ParentTextbox = new System.Windows.Forms.TextBox();
      this.ParentLabel = new System.Windows.Forms.Label();
      this.OrderByMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.OrderByHelp = new System.Windows.Forms.ToolStripMenuItem();
      this.OrderByMenu.SuspendLayout();
      this.SuspendLayout();
      // 
      // FormCancelButton
      // 
      this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.FormCancelButton.Location = new System.Drawing.Point(595, 97);
      this.FormCancelButton.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
      this.FormCancelButton.Name = "FormCancelButton";
      this.FormCancelButton.Size = new System.Drawing.Size(137, 38);
      this.FormCancelButton.TabIndex = 5;
      this.FormCancelButton.Text = "Cancel";
      this.FormCancelButton.UseVisualStyleBackColor = true;
      this.FormCancelButton.Click += new System.EventHandler(this.FormCancelButton_Click);
      // 
      // OKButton
      // 
      this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.OKButton.Location = new System.Drawing.Point(447, 97);
      this.OKButton.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
      this.OKButton.Name = "OKButton";
      this.OKButton.Size = new System.Drawing.Size(137, 38);
      this.OKButton.TabIndex = 4;
      this.OKButton.Text = "&OK";
      this.OKButton.UseVisualStyleBackColor = true;
      this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
      // 
      // ColumnNameTextbox
      // 
      this.ColumnNameTextbox.Location = new System.Drawing.Point(202, 55);
      this.ColumnNameTextbox.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
      this.ColumnNameTextbox.Name = "ColumnNameTextbox";
      this.ColumnNameTextbox.Size = new System.Drawing.Size(528, 30);
      this.ColumnNameTextbox.TabIndex = 3;
      // 
      // ColumnNameLabel
      // 
      this.ColumnNameLabel.Location = new System.Drawing.Point(13, 58);
      this.ColumnNameLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
      this.ColumnNameLabel.Name = "ColumnNameLabel";
      this.ColumnNameLabel.Size = new System.Drawing.Size(185, 32);
      this.ColumnNameLabel.TabIndex = 2;
      this.ColumnNameLabel.Text = "Column Name";
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
      // OrderByMenu
      // 
      this.OrderByMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.OrderByMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OrderByHelp});
      this.OrderByMenu.Name = "OrderByMenu";
      this.OrderByMenu.Size = new System.Drawing.Size(153, 36);
      // 
      // OrderByHelp
      // 
      this.OrderByHelp.Name = "OrderByHelp";
      this.OrderByHelp.ShortcutKeyDisplayString = "F1";
      this.OrderByHelp.Size = new System.Drawing.Size(152, 32);
      this.OrderByHelp.Text = "&Help";
      this.OrderByHelp.Click += new System.EventHandler(this.OrderByHelp_Click);
      // 
      // ViewOrderByDetail
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(748, 145);
      this.ContextMenuStrip = this.OrderByMenu;
      this.Controls.Add(this.ColumnNameTextbox);
      this.Controls.Add(this.ColumnNameLabel);
      this.Controls.Add(this.ParentTextbox);
      this.Controls.Add(this.ParentLabel);
      this.Controls.Add(this.FormCancelButton);
      this.Controls.Add(this.OKButton);
      this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.KeyPreview = true;
      this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "ViewOrderByDetail";
      this.Text = "ViewOrderBy Detail";
      this.Load += new System.EventHandler(this.ViewOrderByDetail_Load);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ViewOrderByDetail_KeyDown);
      this.OrderByMenu.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button FormCancelButton;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.ComboBox ColumnNameTextbox;
		private System.Windows.Forms.Label ColumnNameLabel;
		private System.Windows.Forms.TextBox ParentTextbox;
		private System.Windows.Forms.Label ParentLabel;
		private System.Windows.Forms.ContextMenuStrip OrderByMenu;
		private System.Windows.Forms.ToolStripMenuItem OrderByHelp;
	}
}