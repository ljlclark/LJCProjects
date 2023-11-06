namespace LJCDataDetail
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
      this.MainTabs = new LJCWinFormControls.LJCTabControl(this.components);
      this.DetailMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.DetailTabAdd = new System.Windows.Forms.ToolStripMenuItem();
      this.DetailTabEdit = new System.Windows.Forms.ToolStripMenuItem();
      this.DetailTabDelete = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.DetailSelectMoveRow = new System.Windows.Forms.ToolStripMenuItem();
      this.DetailPasteRow = new System.Windows.Forms.ToolStripMenuItem();
      this.DetailClearSelect = new System.Windows.Forms.ToolStripMenuItem();
      this.Page1 = new System.Windows.Forms.TabPage();
      this.ButtonImages = new System.Windows.Forms.ImageList(this.components);
      this.StatusLabel = new System.Windows.Forms.Label();
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
      this.FormCancelButton.TabIndex = 3;
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
      this.OKButton.TabIndex = 2;
      this.OKButton.Text = "&OK";
      this.OKButton.UseVisualStyleBackColor = true;
      this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
      // 
      // MainTabs
      // 
      this.MainTabs.AllowDrop = true;
      this.MainTabs.ContextMenuStrip = this.DetailMenu;
      this.MainTabs.Controls.Add(this.Page1);
      this.MainTabs.LJCAllowDrag = true;
      this.MainTabs.Location = new System.Drawing.Point(0, 0);
      this.MainTabs.Name = "MainTabs";
      this.MainTabs.SelectedIndex = 0;
      this.MainTabs.Size = new System.Drawing.Size(782, 487);
      this.MainTabs.TabIndex = 0;
      this.MainTabs.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainTabs_MouseDown);
      // 
      // DetailMenu
      // 
      this.DetailMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.DetailMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DetailTabAdd,
            this.DetailTabEdit,
            this.DetailTabDelete,
            this.toolStripSeparator1,
            this.DetailSelectMoveRow,
            this.DetailPasteRow,
            this.DetailClearSelect});
      this.DetailMenu.Name = "DetailMenu";
      this.DetailMenu.Size = new System.Drawing.Size(220, 202);
      // 
      // DetailTabAdd
      // 
      this.DetailTabAdd.Name = "DetailTabAdd";
      this.DetailTabAdd.Size = new System.Drawing.Size(219, 32);
      this.DetailTabAdd.Text = "Tab Add";
      this.DetailTabAdd.Click += new System.EventHandler(this.DetailTabAdd_Click);
      // 
      // DetailTabEdit
      // 
      this.DetailTabEdit.Name = "DetailTabEdit";
      this.DetailTabEdit.Size = new System.Drawing.Size(219, 32);
      this.DetailTabEdit.Text = "Tab Edit";
      this.DetailTabEdit.Click += new System.EventHandler(this.DetailTabEdit_Click);
      // 
      // DetailTabDelete
      // 
      this.DetailTabDelete.Name = "DetailTabDelete";
      this.DetailTabDelete.Size = new System.Drawing.Size(219, 32);
      this.DetailTabDelete.Text = "Tab Delete";
      this.DetailTabDelete.Click += new System.EventHandler(this.DetailTabDelete_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(216, 6);
      // 
      // DetailSelectMoveRow
      // 
      this.DetailSelectMoveRow.Name = "DetailSelectMoveRow";
      this.DetailSelectMoveRow.Size = new System.Drawing.Size(219, 32);
      this.DetailSelectMoveRow.Text = "Select Move Row";
      this.DetailSelectMoveRow.Click += new System.EventHandler(this.DetailSelectMoveRow_Click);
      // 
      // DetailPasteRow
      // 
      this.DetailPasteRow.Name = "DetailPasteRow";
      this.DetailPasteRow.Size = new System.Drawing.Size(219, 32);
      this.DetailPasteRow.Text = "Paste Row";
      this.DetailPasteRow.Click += new System.EventHandler(this.DetailPasteRow_Click);
      // 
      // DetailClearSelect
      // 
      this.DetailClearSelect.Name = "DetailClearSelect";
      this.DetailClearSelect.Size = new System.Drawing.Size(219, 32);
      this.DetailClearSelect.Text = "Clear Select Row";
      this.DetailClearSelect.Click += new System.EventHandler(this.DetailClearSelect_Click);
      // 
      // Page1
      // 
      this.Page1.Location = new System.Drawing.Point(4, 29);
      this.Page1.Name = "Page1";
      this.Page1.Padding = new System.Windows.Forms.Padding(3);
      this.Page1.Size = new System.Drawing.Size(774, 454);
      this.Page1.TabIndex = 0;
      this.Page1.Text = "Page 1";
      this.Page1.UseVisualStyleBackColor = true;
      // 
      // ButtonImages
      // 
      this.ButtonImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ButtonImages.ImageStream")));
      this.ButtonImages.TransparentColor = System.Drawing.Color.Magenta;
      this.ButtonImages.Images.SetKeyName(0, "Ellipse.bmp");
      this.ButtonImages.Images.SetKeyName(1, "Calendar.bmp");
      // 
      // StatusLabel
      // 
      this.StatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.StatusLabel.Location = new System.Drawing.Point(12, 499);
      this.StatusLabel.Name = "StatusLabel";
      this.StatusLabel.Size = new System.Drawing.Size(514, 26);
      this.StatusLabel.TabIndex = 1;
      // 
      // DataDetailDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(778, 544);
      this.Controls.Add(this.StatusLabel);
      this.Controls.Add(this.MainTabs);
      this.Controls.Add(this.FormCancelButton);
      this.Controls.Add(this.OKButton);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
      this.KeyPreview = true;
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
    private System.Windows.Forms.TabPage Page1;
    private System.Windows.Forms.ImageList ButtonImages;
    private System.Windows.Forms.ContextMenuStrip DetailMenu;
    private System.Windows.Forms.ToolStripMenuItem DetailTabEdit;
    private System.Windows.Forms.ToolStripMenuItem DetailTabAdd;
    private System.Windows.Forms.ToolStripMenuItem DetailTabDelete;
    private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem DetailSelectMoveRow;
        private System.Windows.Forms.ToolStripMenuItem DetailPasteRow;
        private System.Windows.Forms.ToolStripMenuItem DetailClearSelect;
    }
}

