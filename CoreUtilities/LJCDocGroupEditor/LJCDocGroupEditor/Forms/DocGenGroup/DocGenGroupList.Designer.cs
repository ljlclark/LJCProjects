namespace LJCDocGroupEditor
{
	partial class DocGenGroupList
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      this.GroupMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.GroupMenuNew = new System.Windows.Forms.ToolStripMenuItem();
      this.GroupMenuEdit = new System.Windows.Forms.ToolStripMenuItem();
      this.DocGenGroupDeleteSeparator = new System.Windows.Forms.ToolStripSeparator();
      this.GroupMenuDelete = new System.Windows.Forms.ToolStripMenuItem();
      this.DocGenGroupRefreshSeparator = new System.Windows.Forms.ToolStripSeparator();
      this.GroupMenuRefresh = new System.Windows.Forms.ToolStripMenuItem();
      this.GroupMenuSelect = new System.Windows.Forms.ToolStripMenuItem();
      this.GroupMenuFileEdit = new System.Windows.Forms.ToolStripMenuItem();
      this.GroupMenuSequence = new System.Windows.Forms.ToolStripMenuItem();
      this.DocGenGroupCloseSeparator = new System.Windows.Forms.ToolStripSeparator();
      this.GroupMenuClose = new System.Windows.Forms.ToolStripMenuItem();
      this.MainSplit = new System.Windows.Forms.SplitContainer();
      this.DocGenGroupHeading = new LJCWinFormControls.LJCHeaderBox();
      this.GroupGrid = new LJCWinFormControls.LJCDataGrid(this.components);
      this.DocAssemblyHeader = new LJCWinFormControls.LJCHeaderBox();
      this.DocAssemblyGrid = new LJCWinFormControls.LJCDataGrid(this.components);
      this.DocAssemblyMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
      this.DocAssemblyMenuNew = new System.Windows.Forms.ToolStripMenuItem();
      this.DocAssemblyMenuEdit = new System.Windows.Forms.ToolStripMenuItem();
      this.DocAssemblyDeleteSeparater = new System.Windows.Forms.ToolStripSeparator();
      this.DocAssemblyMenuDelete = new System.Windows.Forms.ToolStripMenuItem();
      this.DocAssemblyRefreshSeparator = new System.Windows.Forms.ToolStripSeparator();
      this.DocAssemblyMenuRefresh = new System.Windows.Forms.ToolStripMenuItem();
      this.DocAssemblyMenuSelect = new System.Windows.Forms.ToolStripMenuItem();
      this.DocAssemblyMenuSequence = new System.Windows.Forms.ToolStripMenuItem();
      this.FileLabel = new System.Windows.Forms.Label();
      this.FileCombo = new System.Windows.Forms.ComboBox();
      this.GroupMenu.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.MainSplit)).BeginInit();
      this.MainSplit.Panel1.SuspendLayout();
      this.MainSplit.Panel2.SuspendLayout();
      this.MainSplit.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.GroupGrid)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.DocAssemblyGrid)).BeginInit();
      this.DocAssemblyMenu.SuspendLayout();
      this.SuspendLayout();
      // 
      // GroupMenu
      // 
      this.GroupMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.GroupMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.GroupMenuNew,
            this.GroupMenuEdit,
            this.DocGenGroupDeleteSeparator,
            this.GroupMenuDelete,
            this.DocGenGroupRefreshSeparator,
            this.GroupMenuRefresh,
            this.GroupMenuSelect,
            this.GroupMenuFileEdit,
            this.GroupMenuSequence,
            this.DocGenGroupCloseSeparator,
            this.GroupMenuClose});
      this.GroupMenu.Name = "mFacilityMenu";
      this.GroupMenu.Size = new System.Drawing.Size(185, 310);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(184, 32);
      this.toolStripMenuItem1.Text = "Group Menu";
      // 
      // GroupMenuNew
      // 
      this.GroupMenuNew.Name = "GroupMenuNew";
      this.GroupMenuNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
      this.GroupMenuNew.Size = new System.Drawing.Size(184, 32);
      this.GroupMenuNew.Text = "&New";
      this.GroupMenuNew.Click += new System.EventHandler(this.GroupMenuNew_Click);
      // 
      // GroupMenuEdit
      // 
      this.GroupMenuEdit.Name = "GroupMenuEdit";
      this.GroupMenuEdit.ShortcutKeyDisplayString = "Enter";
      this.GroupMenuEdit.Size = new System.Drawing.Size(184, 32);
      this.GroupMenuEdit.Text = "&Edit";
      this.GroupMenuEdit.Click += new System.EventHandler(this.GroupMenuEdit_Click);
      // 
      // DocGenGroupDeleteSeparator
      // 
      this.DocGenGroupDeleteSeparator.Name = "DocGenGroupDeleteSeparator";
      this.DocGenGroupDeleteSeparator.Size = new System.Drawing.Size(181, 6);
      // 
      // GroupMenuDelete
      // 
      this.GroupMenuDelete.Name = "GroupMenuDelete";
      this.GroupMenuDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
      this.GroupMenuDelete.Size = new System.Drawing.Size(184, 32);
      this.GroupMenuDelete.Text = "&Delete";
      this.GroupMenuDelete.Click += new System.EventHandler(this.GroupMenuDelete_Click);
      // 
      // DocGenGroupRefreshSeparator
      // 
      this.DocGenGroupRefreshSeparator.Name = "DocGenGroupRefreshSeparator";
      this.DocGenGroupRefreshSeparator.Size = new System.Drawing.Size(181, 6);
      // 
      // GroupMenuRefresh
      // 
      this.GroupMenuRefresh.Name = "GroupMenuRefresh";
      this.GroupMenuRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
      this.GroupMenuRefresh.Size = new System.Drawing.Size(184, 32);
      this.GroupMenuRefresh.Text = "&Refresh";
      this.GroupMenuRefresh.Click += new System.EventHandler(this.GroupMenuRefresh_Click);
      // 
      // GroupMenuSelect
      // 
      this.GroupMenuSelect.Name = "GroupMenuSelect";
      this.GroupMenuSelect.Size = new System.Drawing.Size(184, 32);
      this.GroupMenuSelect.Text = "&Select";
      this.GroupMenuSelect.Click += new System.EventHandler(this.GroupMenuSelect_Click);
      // 
      // GroupMenuFileEdit
      // 
      this.GroupMenuFileEdit.Name = "GroupMenuFileEdit";
      this.GroupMenuFileEdit.Size = new System.Drawing.Size(184, 32);
      this.GroupMenuFileEdit.Text = "F&ile Edit";
      this.GroupMenuFileEdit.Click += new System.EventHandler(this.GroupMenuFileEdit_Click);
      // 
      // GroupMenuSequence
      // 
      this.GroupMenuSequence.Name = "GroupMenuSequence";
      this.GroupMenuSequence.Size = new System.Drawing.Size(184, 32);
      this.GroupMenuSequence.Text = "Sequence";
      this.GroupMenuSequence.Click += new System.EventHandler(this.GroupMenuSequence_Click);
      // 
      // DocGenGroupCloseSeparator
      // 
      this.DocGenGroupCloseSeparator.Name = "DocGenGroupCloseSeparator";
      this.DocGenGroupCloseSeparator.Size = new System.Drawing.Size(181, 6);
      // 
      // GroupMenuClose
      // 
      this.GroupMenuClose.Name = "GroupMenuClose";
      this.GroupMenuClose.Size = new System.Drawing.Size(184, 32);
      this.GroupMenuClose.Text = "E&xit";
      this.GroupMenuClose.Click += new System.EventHandler(this.GroupMenuClose_Click);
      // 
      // MainSplit
      // 
      this.MainSplit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.MainSplit.Location = new System.Drawing.Point(0, 44);
      this.MainSplit.Name = "MainSplit";
      this.MainSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // MainSplit.Panel1
      // 
      this.MainSplit.Panel1.Controls.Add(this.DocGenGroupHeading);
      this.MainSplit.Panel1.Controls.Add(this.GroupGrid);
      // 
      // MainSplit.Panel2
      // 
      this.MainSplit.Panel2.Controls.Add(this.DocAssemblyHeader);
      this.MainSplit.Panel2.Controls.Add(this.DocAssemblyGrid);
      this.MainSplit.Size = new System.Drawing.Size(778, 500);
      this.MainSplit.SplitterDistance = 257;
      this.MainSplit.TabIndex = 2;
      // 
      // DocGenGroupHeading
      // 
      this.DocGenGroupHeading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.DocGenGroupHeading.LJCBeginColor = System.Drawing.Color.AliceBlue;
      this.DocGenGroupHeading.LJCEndColor = System.Drawing.Color.LightSkyBlue;
      this.DocGenGroupHeading.Location = new System.Drawing.Point(0, 0);
      this.DocGenGroupHeading.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.DocGenGroupHeading.Name = "DocGenGroupHeading";
      this.DocGenGroupHeading.Size = new System.Drawing.Size(778, 31);
      this.DocGenGroupHeading.TabIndex = 0;
      this.DocGenGroupHeading.TabStop = false;
      this.DocGenGroupHeading.Text = "Doc Group";
      // 
      // GroupGrid
      // 
      this.GroupGrid.AllowDrop = true;
      this.GroupGrid.AllowUserToAddRows = false;
      this.GroupGrid.AllowUserToDeleteRows = false;
      this.GroupGrid.AllowUserToResizeRows = false;
      this.GroupGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.GroupGrid.BackgroundColor = System.Drawing.Color.AliceBlue;
      this.GroupGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.GroupGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.GroupGrid.ContextMenuStrip = this.GroupMenu;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.GroupGrid.DefaultCellStyle = dataGridViewCellStyle1;
      this.GroupGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
      this.GroupGrid.LJCAllowSelectionChange = false;
      this.GroupGrid.LJCDragDataName = null;
      this.GroupGrid.LJCLastRowIndex = -1;
      this.GroupGrid.LJCRowHeight = 0;
      this.GroupGrid.Location = new System.Drawing.Point(0, 30);
      this.GroupGrid.MultiSelect = false;
      this.GroupGrid.Name = "GroupGrid";
      this.GroupGrid.RowHeadersVisible = false;
      this.GroupGrid.RowHeadersWidth = 62;
      this.GroupGrid.RowTemplate.Height = 28;
      this.GroupGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.GroupGrid.ShowCellToolTips = false;
      this.GroupGrid.Size = new System.Drawing.Size(778, 226);
      this.GroupGrid.TabIndex = 1;
      this.GroupGrid.Text = "LJCDataGrid";
      this.GroupGrid.SelectionChanged += new System.EventHandler(this.GroupGrid_SelectionChanged);
      this.GroupGrid.DragDrop += new System.Windows.Forms.DragEventHandler(this.GroupGrid_DragDrop);
      this.GroupGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GroupGrid_KeyDown);
      this.GroupGrid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.GroupGrid_MouseDoubleClick);
      this.GroupGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GroupGrid_MouseDown);
      // 
      // DocAssemblyHeader
      // 
      this.DocAssemblyHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.DocAssemblyHeader.LJCBeginColor = System.Drawing.Color.AliceBlue;
      this.DocAssemblyHeader.LJCEndColor = System.Drawing.Color.LightSkyBlue;
      this.DocAssemblyHeader.Location = new System.Drawing.Point(0, 0);
      this.DocAssemblyHeader.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.DocAssemblyHeader.Name = "DocAssemblyHeader";
      this.DocAssemblyHeader.Size = new System.Drawing.Size(778, 31);
      this.DocAssemblyHeader.TabIndex = 0;
      this.DocAssemblyHeader.TabStop = false;
      this.DocAssemblyHeader.Text = "Doc Assembly";
      // 
      // DocAssemblyGrid
      // 
      this.DocAssemblyGrid.AllowDrop = true;
      this.DocAssemblyGrid.AllowUserToAddRows = false;
      this.DocAssemblyGrid.AllowUserToDeleteRows = false;
      this.DocAssemblyGrid.AllowUserToResizeRows = false;
      this.DocAssemblyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.DocAssemblyGrid.BackgroundColor = System.Drawing.Color.AliceBlue;
      this.DocAssemblyGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.DocAssemblyGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.DocAssemblyGrid.ContextMenuStrip = this.DocAssemblyMenu;
      dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
      dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
      dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.DocAssemblyGrid.DefaultCellStyle = dataGridViewCellStyle2;
      this.DocAssemblyGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
      this.DocAssemblyGrid.LJCAllowSelectionChange = false;
      this.DocAssemblyGrid.LJCDragDataName = null;
      this.DocAssemblyGrid.LJCLastRowIndex = -1;
      this.DocAssemblyGrid.LJCRowHeight = 0;
      this.DocAssemblyGrid.Location = new System.Drawing.Point(0, 30);
      this.DocAssemblyGrid.MultiSelect = false;
      this.DocAssemblyGrid.Name = "DocAssemblyGrid";
      this.DocAssemblyGrid.RowHeadersVisible = false;
      this.DocAssemblyGrid.RowHeadersWidth = 62;
      this.DocAssemblyGrid.RowTemplate.Height = 28;
      this.DocAssemblyGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.DocAssemblyGrid.ShowCellToolTips = false;
      this.DocAssemblyGrid.Size = new System.Drawing.Size(778, 208);
      this.DocAssemblyGrid.TabIndex = 1;
      this.DocAssemblyGrid.Text = "LJCDataGrid";
      this.DocAssemblyGrid.SelectionChanged += new System.EventHandler(this.DocAssemblyGrid_SelectionChanged);
      this.DocAssemblyGrid.DragDrop += new System.Windows.Forms.DragEventHandler(this.DocAssemblyGrid_DragDrop);
      this.DocAssemblyGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DocAssemblyGrid_KeyDown);
      this.DocAssemblyGrid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DocAssemblyGrid_MouseDoubleClick);
      this.DocAssemblyGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DocAssemblyGrid_MouseDown);
      // 
      // DocAssemblyMenu
      // 
      this.DocAssemblyMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.DocAssemblyMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.DocAssemblyMenuNew,
            this.DocAssemblyMenuEdit,
            this.DocAssemblyDeleteSeparater,
            this.DocAssemblyMenuDelete,
            this.DocAssemblyRefreshSeparator,
            this.DocAssemblyMenuRefresh,
            this.DocAssemblyMenuSelect,
            this.DocAssemblyMenuSequence});
      this.DocAssemblyMenu.Name = "mFacilityMenu";
      this.DocAssemblyMenu.Size = new System.Drawing.Size(212, 240);
      // 
      // toolStripMenuItem2
      // 
      this.toolStripMenuItem2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
      this.toolStripMenuItem2.Name = "toolStripMenuItem2";
      this.toolStripMenuItem2.Size = new System.Drawing.Size(211, 32);
      this.toolStripMenuItem2.Text = "Assembly Menu";
      // 
      // DocAssemblyMenuNew
      // 
      this.DocAssemblyMenuNew.Name = "DocAssemblyMenuNew";
      this.DocAssemblyMenuNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
      this.DocAssemblyMenuNew.Size = new System.Drawing.Size(211, 32);
      this.DocAssemblyMenuNew.Text = "&New";
      this.DocAssemblyMenuNew.Click += new System.EventHandler(this.AssemblyMenuNew_Click);
      // 
      // DocAssemblyMenuEdit
      // 
      this.DocAssemblyMenuEdit.Name = "DocAssemblyMenuEdit";
      this.DocAssemblyMenuEdit.ShortcutKeyDisplayString = "Enter";
      this.DocAssemblyMenuEdit.Size = new System.Drawing.Size(211, 32);
      this.DocAssemblyMenuEdit.Text = "&Edit";
      this.DocAssemblyMenuEdit.Click += new System.EventHandler(this.AssemblyMenuEdit_Click);
      // 
      // DocAssemblyDeleteSeparater
      // 
      this.DocAssemblyDeleteSeparater.Name = "DocAssemblyDeleteSeparater";
      this.DocAssemblyDeleteSeparater.Size = new System.Drawing.Size(208, 6);
      // 
      // DocAssemblyMenuDelete
      // 
      this.DocAssemblyMenuDelete.Name = "DocAssemblyMenuDelete";
      this.DocAssemblyMenuDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
      this.DocAssemblyMenuDelete.Size = new System.Drawing.Size(211, 32);
      this.DocAssemblyMenuDelete.Text = "&Delete";
      this.DocAssemblyMenuDelete.Click += new System.EventHandler(this.AssemblyMenuDelete_Click);
      // 
      // DocAssemblyRefreshSeparator
      // 
      this.DocAssemblyRefreshSeparator.Name = "DocAssemblyRefreshSeparator";
      this.DocAssemblyRefreshSeparator.Size = new System.Drawing.Size(208, 6);
      // 
      // DocAssemblyMenuRefresh
      // 
      this.DocAssemblyMenuRefresh.Name = "DocAssemblyMenuRefresh";
      this.DocAssemblyMenuRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
      this.DocAssemblyMenuRefresh.Size = new System.Drawing.Size(211, 32);
      this.DocAssemblyMenuRefresh.Text = "&Refresh";
      this.DocAssemblyMenuRefresh.Click += new System.EventHandler(this.AssemblyMenuRefresh_Click);
      // 
      // DocAssemblyMenuSelect
      // 
      this.DocAssemblyMenuSelect.Name = "DocAssemblyMenuSelect";
      this.DocAssemblyMenuSelect.Size = new System.Drawing.Size(211, 32);
      this.DocAssemblyMenuSelect.Text = "&Select";
      this.DocAssemblyMenuSelect.Click += new System.EventHandler(this.AssemblyMenuSelect_Click);
      // 
      // DocAssemblyMenuSequence
      // 
      this.DocAssemblyMenuSequence.Name = "DocAssemblyMenuSequence";
      this.DocAssemblyMenuSequence.Size = new System.Drawing.Size(211, 32);
      this.DocAssemblyMenuSequence.Text = "Sequence";
      this.DocAssemblyMenuSequence.Click += new System.EventHandler(this.DocAssemblyMenuSequence_Click);
      // 
      // FileLabel
      // 
      this.FileLabel.AutoSize = true;
      this.FileLabel.Location = new System.Drawing.Point(12, 12);
      this.FileLabel.Name = "FileLabel";
      this.FileLabel.Size = new System.Drawing.Size(71, 20);
      this.FileLabel.TabIndex = 0;
      this.FileLabel.Text = "XML File";
      // 
      // FileCombo
      // 
      this.FileCombo.FormattingEnabled = true;
      this.FileCombo.Location = new System.Drawing.Point(119, 9);
      this.FileCombo.Name = "FileCombo";
      this.FileCombo.Size = new System.Drawing.Size(240, 28);
      this.FileCombo.TabIndex = 1;
      this.FileCombo.SelectedIndexChanged += new System.EventHandler(this.FileCombo_SelectedIndexChanged);
      // 
      // DocGenGroupList
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(778, 544);
      this.Controls.Add(this.FileLabel);
      this.Controls.Add(this.FileCombo);
      this.Controls.Add(this.MainSplit);
      this.Name = "DocGenGroupList";
      this.Text = "DocGroup List";
      this.Load += new System.EventHandler(this.DocGenGroupList_Load);
      this.GroupMenu.ResumeLayout(false);
      this.MainSplit.Panel1.ResumeLayout(false);
      this.MainSplit.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.MainSplit)).EndInit();
      this.MainSplit.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.GroupGrid)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.DocAssemblyGrid)).EndInit();
      this.DocAssemblyMenu.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ContextMenuStrip GroupMenu;
		private System.Windows.Forms.ToolStripMenuItem GroupMenuNew;
		private System.Windows.Forms.ToolStripMenuItem GroupMenuEdit;
		private System.Windows.Forms.ToolStripSeparator DocGenGroupDeleteSeparator;
		private System.Windows.Forms.ToolStripMenuItem GroupMenuDelete;
		private System.Windows.Forms.ToolStripSeparator DocGenGroupRefreshSeparator;
		private System.Windows.Forms.ToolStripMenuItem GroupMenuRefresh;
		private System.Windows.Forms.ToolStripSeparator DocGenGroupCloseSeparator;
		private System.Windows.Forms.ToolStripMenuItem GroupMenuClose;
		private System.Windows.Forms.ToolStripMenuItem GroupMenuSelect;
		private System.Windows.Forms.SplitContainer MainSplit;
		private LJCWinFormControls.LJCDataGrid GroupGrid;
		private LJCWinFormControls.LJCDataGrid DocAssemblyGrid;
		private LJCWinFormControls.LJCHeaderBox DocGenGroupHeading;
		private LJCWinFormControls.LJCHeaderBox DocAssemblyHeader;
		private System.Windows.Forms.ContextMenuStrip DocAssemblyMenu;
		private System.Windows.Forms.ToolStripMenuItem DocAssemblyMenuNew;
		private System.Windows.Forms.ToolStripMenuItem DocAssemblyMenuEdit;
		private System.Windows.Forms.ToolStripSeparator DocAssemblyDeleteSeparater;
		private System.Windows.Forms.ToolStripMenuItem DocAssemblyMenuDelete;
		private System.Windows.Forms.ToolStripSeparator DocAssemblyRefreshSeparator;
		private System.Windows.Forms.ToolStripMenuItem DocAssemblyMenuRefresh;
		private System.Windows.Forms.ToolStripMenuItem DocAssemblyMenuSelect;
		private System.Windows.Forms.ToolStripMenuItem GroupMenuFileEdit;
		private System.Windows.Forms.Label FileLabel;
		private System.Windows.Forms.ComboBox FileCombo;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
    private System.Windows.Forms.ToolStripMenuItem GroupMenuSequence;
    private System.Windows.Forms.ToolStripMenuItem DocAssemblyMenuSequence;
  }
}