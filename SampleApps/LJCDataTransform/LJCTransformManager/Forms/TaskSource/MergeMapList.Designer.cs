namespace LJCTransformManager
{
	partial class MergeMapList
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
			this.MainSplit = new System.Windows.Forms.SplitContainer();
			this.SourceHeader = new LJCWinFormControls.LJCHeaderBox();
			this.SourceGrid = new LJCWinFormControls.LJCDataGrid(this.components);
			this.SourceMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.SourceMenuNew = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.SourceMenuDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.SourceMenuMerge = new System.Windows.Forms.ToolStripMenuItem();
			this.SourceMenuOverwrite = new System.Windows.Forms.ToolStripMenuItem();
			this.SourceMenuInsert = new System.Windows.Forms.ToolStripMenuItem();
			this.TargetHeader = new LJCWinFormControls.LJCHeaderBox();
			this.TargetGrid = new LJCWinFormControls.LJCDataGrid(this.components);
			this.TargetMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.TargetMenuNew = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.TargetMenuDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.TargetMenuMerge = new System.Windows.Forms.ToolStripMenuItem();
			this.TargetMenuOverwrite = new System.Windows.Forms.ToolStripMenuItem();
			this.TargetMenuInsert = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.MainSplit)).BeginInit();
			this.MainSplit.Panel1.SuspendLayout();
			this.MainSplit.Panel2.SuspendLayout();
			this.MainSplit.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.SourceGrid)).BeginInit();
			this.SourceMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.TargetGrid)).BeginInit();
			this.TargetMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// FormCancelButton
			// 
			this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.FormCancelButton.Location = new System.Drawing.Point(649, 491);
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
			this.OKButton.Location = new System.Drawing.Point(529, 491);
			this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(112, 35);
			this.OKButton.TabIndex = 1;
			this.OKButton.Text = "&OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// MainSplit
			// 
			this.MainSplit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MainSplit.Location = new System.Drawing.Point(0, 0);
			this.MainSplit.Name = "MainSplit";
			// 
			// MainSplit.Panel1
			// 
			this.MainSplit.Panel1.Controls.Add(this.SourceHeader);
			this.MainSplit.Panel1.Controls.Add(this.SourceGrid);
			// 
			// MainSplit.Panel2
			// 
			this.MainSplit.Panel2.Controls.Add(this.TargetHeader);
			this.MainSplit.Panel2.Controls.Add(this.TargetGrid);
			this.MainSplit.Size = new System.Drawing.Size(775, 479);
			this.MainSplit.SplitterDistance = 380;
			this.MainSplit.SplitterWidth = 6;
			this.MainSplit.TabIndex = 0;
			// 
			// SourceHeader
			// 
			this.SourceHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.SourceHeader.LJCBeginColor = System.Drawing.Color.AliceBlue;
			this.SourceHeader.LJCEndColor = System.Drawing.Color.SkyBlue;
			this.SourceHeader.Location = new System.Drawing.Point(0, 0);
			this.SourceHeader.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.SourceHeader.Name = "SourceHeader";
			this.SourceHeader.Size = new System.Drawing.Size(379, 31);
			this.SourceHeader.TabIndex = 0;
			this.SourceHeader.TabStop = false;
			this.SourceHeader.Text = "Source Layout";
			// 
			// SourceGrid
			// 
			this.SourceGrid.AllowUserToAddRows = false;
			this.SourceGrid.AllowUserToDeleteRows = false;
			this.SourceGrid.AllowUserToResizeRows = false;
			this.SourceGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.SourceGrid.BackgroundColor = System.Drawing.Color.AliceBlue;
			this.SourceGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.SourceGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.SourceGrid.ContextMenuStrip = this.SourceMenu;
			this.SourceGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.SourceGrid.LJCAllowSelectionChange = false;
			this.SourceGrid.LJCLastRowIndex = -1;
			this.SourceGrid.Location = new System.Drawing.Point(0, 30);
			this.SourceGrid.MultiSelect = false;
			this.SourceGrid.Name = "SourceGrid";
			this.SourceGrid.RowHeadersVisible = false;
			this.SourceGrid.RowTemplate.Height = 28;
			this.SourceGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.SourceGrid.ShowCellToolTips = false;
			this.SourceGrid.Size = new System.Drawing.Size(380, 449);
			this.SourceGrid.TabIndex = 1;
			this.SourceGrid.Text = "LJCDataGrid";
			this.SourceGrid.SelectionChanged += new System.EventHandler(this.SourceGrid_SelectionChanged);
			this.SourceGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SourceGrid_KeyDown);
			this.SourceGrid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.SourceGrid_MouseDoubleClick);
			this.SourceGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SourceGrid_MouseDown);
			// 
			// SourceMenu
			// 
			this.SourceMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.SourceMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SourceMenuNew,
            this.toolStripSeparator1,
            this.SourceMenuDelete,
            this.toolStripSeparator3,
            this.SourceMenuMerge,
            this.SourceMenuOverwrite,
            this.SourceMenuInsert});
			this.SourceMenu.Name = "SourceMenu";
			this.SourceMenu.Size = new System.Drawing.Size(241, 199);
			// 
			// SourceMenuNew
			// 
			this.SourceMenuNew.Name = "SourceMenuNew";
			this.SourceMenuNew.Size = new System.Drawing.Size(240, 30);
			this.SourceMenuNew.Text = "&New";
			this.SourceMenuNew.Click += new System.EventHandler(this.SourceMenuNew_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(237, 6);
			// 
			// SourceMenuDelete
			// 
			this.SourceMenuDelete.Name = "SourceMenuDelete";
			this.SourceMenuDelete.Size = new System.Drawing.Size(240, 30);
			this.SourceMenuDelete.Text = "&Delete";
			this.SourceMenuDelete.Click += new System.EventHandler(this.SourceMenuDelete_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(237, 6);
			// 
			// SourceMenuMerge
			// 
			this.SourceMenuMerge.Name = "SourceMenuMerge";
			this.SourceMenuMerge.Size = new System.Drawing.Size(240, 30);
			this.SourceMenuMerge.Text = "&Merge";
			this.SourceMenuMerge.Click += new System.EventHandler(this.SourceMenuMerge_Click);
			// 
			// SourceMenuOverwrite
			// 
			this.SourceMenuOverwrite.Name = "SourceMenuOverwrite";
			this.SourceMenuOverwrite.Size = new System.Drawing.Size(240, 30);
			this.SourceMenuOverwrite.Text = "&Overwrite";
			this.SourceMenuOverwrite.Click += new System.EventHandler(this.SourceMenuOverwrite_Click);
			// 
			// SourceMenuInsert
			// 
			this.SourceMenuInsert.Name = "SourceMenuInsert";
			this.SourceMenuInsert.Size = new System.Drawing.Size(240, 30);
			this.SourceMenuInsert.Text = "&Insert Only";
			this.SourceMenuInsert.Click += new System.EventHandler(this.SourceMenuInsert_Click);
			// 
			// TargetHeader
			// 
			this.TargetHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TargetHeader.LJCBeginColor = System.Drawing.Color.AliceBlue;
			this.TargetHeader.LJCEndColor = System.Drawing.Color.SkyBlue;
			this.TargetHeader.Location = new System.Drawing.Point(1, 0);
			this.TargetHeader.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.TargetHeader.Name = "TargetHeader";
			this.TargetHeader.Size = new System.Drawing.Size(377, 31);
			this.TargetHeader.TabIndex = 0;
			this.TargetHeader.TabStop = false;
			this.TargetHeader.Text = "Target Layout";
			// 
			// TargetGrid
			// 
			this.TargetGrid.AllowUserToAddRows = false;
			this.TargetGrid.AllowUserToDeleteRows = false;
			this.TargetGrid.AllowUserToResizeRows = false;
			this.TargetGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TargetGrid.BackgroundColor = System.Drawing.Color.AliceBlue;
			this.TargetGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.TargetGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.TargetGrid.ContextMenuStrip = this.TargetMenu;
			this.TargetGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.TargetGrid.LJCAllowSelectionChange = false;
			this.TargetGrid.LJCLastRowIndex = -1;
			this.TargetGrid.Location = new System.Drawing.Point(0, 30);
			this.TargetGrid.MultiSelect = false;
			this.TargetGrid.Name = "TargetGrid";
			this.TargetGrid.RowHeadersVisible = false;
			this.TargetGrid.RowTemplate.Height = 28;
			this.TargetGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.TargetGrid.ShowCellToolTips = false;
			this.TargetGrid.Size = new System.Drawing.Size(378, 449);
			this.TargetGrid.TabIndex = 1;
			this.TargetGrid.Text = "LJCDataGrid";
			this.TargetGrid.SelectionChanged += new System.EventHandler(this.TargetGrid_SelectionChanged);
			this.TargetGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TargetGrid_KeyDown);
			this.TargetGrid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TargetGrid_MouseDoubleClick);
			this.TargetGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TargetGrid_MouseDown);
			// 
			// TargetMenu
			// 
			this.TargetMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.TargetMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TargetMenuNew,
            this.toolStripSeparator2,
            this.TargetMenuDelete,
            this.toolStripSeparator4,
            this.TargetMenuMerge,
            this.TargetMenuOverwrite,
            this.TargetMenuInsert});
			this.TargetMenu.Name = "TargetMenu";
			this.TargetMenu.Size = new System.Drawing.Size(171, 166);
			// 
			// TargetMenuNew
			// 
			this.TargetMenuNew.Name = "TargetMenuNew";
			this.TargetMenuNew.Size = new System.Drawing.Size(170, 30);
			this.TargetMenuNew.Text = "&New";
			this.TargetMenuNew.Click += new System.EventHandler(this.TargetMenuNew_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(167, 6);
			// 
			// TargetMenuDelete
			// 
			this.TargetMenuDelete.Name = "TargetMenuDelete";
			this.TargetMenuDelete.Size = new System.Drawing.Size(170, 30);
			this.TargetMenuDelete.Text = "&Delete";
			this.TargetMenuDelete.Click += new System.EventHandler(this.TargetMenuDelete_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(167, 6);
			// 
			// TargetMenuMerge
			// 
			this.TargetMenuMerge.Name = "TargetMenuMerge";
			this.TargetMenuMerge.Size = new System.Drawing.Size(170, 30);
			this.TargetMenuMerge.Text = "&Merge";
			this.TargetMenuMerge.Click += new System.EventHandler(this.TargetMenuMerge_Click);
			// 
			// TargetMenuOverwrite
			// 
			this.TargetMenuOverwrite.Name = "TargetMenuOverwrite";
			this.TargetMenuOverwrite.Size = new System.Drawing.Size(170, 30);
			this.TargetMenuOverwrite.Text = "&Overwrite";
			this.TargetMenuOverwrite.Click += new System.EventHandler(this.TargetMenuOverwrite_Click);
			// 
			// TargetMenuInsert
			// 
			this.TargetMenuInsert.Name = "TargetMenuInsert";
			this.TargetMenuInsert.Size = new System.Drawing.Size(170, 30);
			this.TargetMenuInsert.Text = "&Insert Only";
			this.TargetMenuInsert.Click += new System.EventHandler(this.TargetMenuInsert_Click);
			// 
			// MergeMapList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(774, 540);
			this.Controls.Add(this.FormCancelButton);
			this.Controls.Add(this.OKButton);
			this.Controls.Add(this.MainSplit);
			this.Name = "MergeMapList";
			this.ShowInTaskbar = false;
			this.Text = "Source Map List";
			this.Load += new System.EventHandler(this.MergeMapList_Load);
			this.MainSplit.Panel1.ResumeLayout(false);
			this.MainSplit.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.MainSplit)).EndInit();
			this.MainSplit.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.SourceGrid)).EndInit();
			this.SourceMenu.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.TargetGrid)).EndInit();
			this.TargetMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button FormCancelButton;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.SplitContainer MainSplit;
		private LJCWinFormControls.LJCHeaderBox SourceHeader;
		private LJCWinFormControls.LJCDataGrid SourceGrid;
		private LJCWinFormControls.LJCHeaderBox TargetHeader;
		private LJCWinFormControls.LJCDataGrid TargetGrid;
		private System.Windows.Forms.ContextMenuStrip SourceMenu;
		private System.Windows.Forms.ToolStripMenuItem SourceMenuNew;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem SourceMenuDelete;
		private System.Windows.Forms.ContextMenuStrip TargetMenu;
		private System.Windows.Forms.ToolStripMenuItem TargetMenuNew;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem TargetMenuDelete;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem SourceMenuMerge;
		private System.Windows.Forms.ToolStripMenuItem SourceMenuOverwrite;
		private System.Windows.Forms.ToolStripMenuItem SourceMenuInsert;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem TargetMenuMerge;
		private System.Windows.Forms.ToolStripMenuItem TargetMenuOverwrite;
		private System.Windows.Forms.ToolStripMenuItem TargetMenuInsert;
	}
}