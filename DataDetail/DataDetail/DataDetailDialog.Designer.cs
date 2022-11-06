namespace DataDetail
{
	partial class DataDetailDialog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataDetailDialog));
      this.FormCancelButton = new System.Windows.Forms.Button();
      this.OKButton = new System.Windows.Forms.Button();
      this.MainTabs = new LJCWinFormControls.LJCTabControl();
      this.DetailMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.DetailTabEdit = new System.Windows.Forms.ToolStripMenuItem();
      this.s = new System.Windows.Forms.TabPage();
      this.ButtonImages = new System.Windows.Forms.ImageList(this.components);
      this.DetailTabAdd = new System.Windows.Forms.ToolStripMenuItem();
      this.MainTabs.SuspendLayout();
      this.DetailMenu.SuspendLayout();
      this.SuspendLayout();
      // 
      // FormCancelButton
      // 
      this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.FormCancelButton.Location = new System.Drawing.Point(653, 495);
      this.FormCancelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.FormCancelButton.Name = "FormCancelButton";
      this.FormCancelButton.Size = new System.Drawing.Size(112, 35);
      this.FormCancelButton.TabIndex = 2;
      this.FormCancelButton.Text = "Cancel";
      this.FormCancelButton.UseVisualStyleBackColor = true;
      this.FormCancelButton.Click += new System.EventHandler(this.FormCancelButton_Click);
      // 
      // OKButton
      // 
      this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.OKButton.Location = new System.Drawing.Point(533, 495);
      this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.OKButton.Name = "OKButton";
      this.OKButton.Size = new System.Drawing.Size(112, 35);
      this.OKButton.TabIndex = 1;
      this.OKButton.Text = "&OK";
      this.OKButton.UseVisualStyleBackColor = true;
      this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
      // 
      // MainTabs
      // 
      this.MainTabs.ContextMenuStrip = this.DetailMenu;
      this.MainTabs.Controls.Add(this.s);
      this.MainTabs.Location = new System.Drawing.Point(0, 0);
      this.MainTabs.Name = "MainTabs";
      this.MainTabs.SelectedIndex = 0;
      this.MainTabs.Size = new System.Drawing.Size(782, 487);
      this.MainTabs.TabIndex = 0;
      // 
      // DetailMenu
      // 
      this.DetailMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.DetailMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DetailTabAdd,
            this.DetailTabEdit});
      this.DetailMenu.Name = "DetailMenu";
      this.DetailMenu.Size = new System.Drawing.Size(241, 101);
      // 
      // DetailTabEdit
      // 
      this.DetailTabEdit.Name = "DetailTabEdit";
      this.DetailTabEdit.Size = new System.Drawing.Size(240, 32);
      this.DetailTabEdit.Text = "Tab Edit";
      this.DetailTabEdit.Click += new System.EventHandler(this.DetailTabEdit_Click);
      // 
      // s
      // 
      this.s.Location = new System.Drawing.Point(4, 29);
      this.s.Name = "s";
      this.s.Padding = new System.Windows.Forms.Padding(3);
      this.s.Size = new System.Drawing.Size(774, 454);
      this.s.TabIndex = 0;
      this.s.Text = "Page 1";
      this.s.UseVisualStyleBackColor = true;
      // 
      // ButtonImages
      // 
      this.ButtonImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ButtonImages.ImageStream")));
      this.ButtonImages.TransparentColor = System.Drawing.Color.Magenta;
      this.ButtonImages.Images.SetKeyName(0, "Ellipse.bmp");
      this.ButtonImages.Images.SetKeyName(1, "Calendar.bmp");
      // 
      // DetailTabAdd
      // 
      this.DetailTabAdd.Name = "DetailTabAdd";
      this.DetailTabAdd.Size = new System.Drawing.Size(240, 32);
      this.DetailTabAdd.Text = "Tab Add";
      this.DetailTabAdd.Click += new System.EventHandler(this.DetailTabAdd_Click);
      // 
      // DataDetailDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(778, 544);
      this.Controls.Add(this.MainTabs);
      this.Controls.Add(this.FormCancelButton);
      this.Controls.Add(this.OKButton);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "DataDetailDialog";
      this.ShowInTaskbar = false;
      this.Text = "Dynamic Detail Dialog";
      this.Load += new System.EventHandler(this.DataDetailDialog_Load);
      this.MainTabs.ResumeLayout(false);
      this.DetailMenu.ResumeLayout(false);
      this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button FormCancelButton;
		private System.Windows.Forms.Button OKButton;
		private LJCWinFormControls.LJCTabControl MainTabs;
		private System.Windows.Forms.TabPage s;
		private System.Windows.Forms.ImageList ButtonImages;
		private System.Windows.Forms.ContextMenuStrip DetailMenu;
		private System.Windows.Forms.ToolStripMenuItem DetailTabEdit;
        private System.Windows.Forms.ToolStripMenuItem DetailTabAdd;
    }
}

