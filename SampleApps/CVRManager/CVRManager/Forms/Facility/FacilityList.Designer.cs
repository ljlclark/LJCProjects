namespace CVRManager
{
	partial class FacilityList
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
			this.FacilityGrid = new LJCWinFormControls.LJCDataGrid(this.components);
			this.MainMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.MainMenuNew = new System.Windows.Forms.ToolStripMenuItem();
			this.MainMenuEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.MainMenuDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.MainMenuRefresh = new System.Windows.Forms.ToolStripMenuItem();
			this.MainMenuExportText = new System.Windows.Forms.ToolStripMenuItem();
			this.MainMenuExportCSV = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.MainMenuClose = new System.Windows.Forms.ToolStripMenuItem();
			this.MainMenuHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			((System.ComponentModel.ISupportInitialize)(this.FacilityGrid)).BeginInit();
			this.MainMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// FacilityGrid
			// 
			this.FacilityGrid.AllowUserToAddRows = false;
			this.FacilityGrid.AllowUserToDeleteRows = false;
			this.FacilityGrid.AllowUserToResizeRows = false;
			this.FacilityGrid.BackgroundColor = System.Drawing.Color.AliceBlue;
			this.FacilityGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.FacilityGrid.ContextMenuStrip = this.MainMenu;
			this.FacilityGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.FacilityGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.FacilityGrid.LJCAllowSelectionChange = false;
			this.FacilityGrid.LJCLastRowIndex = -1;
			this.FacilityGrid.LJCRowHeight = 22;
			this.FacilityGrid.Location = new System.Drawing.Point(0, 0);
			this.FacilityGrid.MultiSelect = false;
			this.FacilityGrid.Name = "FacilityGrid";
			this.FacilityGrid.RowHeadersVisible = false;
			this.FacilityGrid.RowTemplate.Height = 28;
			this.FacilityGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.FacilityGrid.ShowCellToolTips = false;
			this.FacilityGrid.Size = new System.Drawing.Size(578, 644);
			this.FacilityGrid.TabIndex = 0;
			this.FacilityGrid.Text = "LJCDataGrid";
			this.FacilityGrid.SelectionChanged += new System.EventHandler(this.FacilityGrid_SelectionChanged);
			this.FacilityGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FacilityGrid_KeyDown);
			this.FacilityGrid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.FacilityGrid_MouseDoubleClick);
			this.FacilityGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FacilityGrid_MouseDown);
			// 
			// MainMenu
			// 
			this.MainMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenuNew,
            this.MainMenuEdit,
            this.toolStripSeparator3,
            this.MainMenuDelete,
            this.toolStripSeparator2,
            this.MainMenuRefresh,
            this.MainMenuExportText,
            this.MainMenuExportCSV,
            this.toolStripSeparator4,
            this.MainMenuClose,
            this.toolStripSeparator1,
            this.MainMenuHelp});
			this.MainMenu.Name = "MainMenu";
			this.MainMenu.Size = new System.Drawing.Size(184, 254);
			// 
			// MainMenuNew
			// 
			this.MainMenuNew.AutoSize = false;
			this.MainMenuNew.Name = "MainMenuNew";
			this.MainMenuNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.MainMenuNew.Size = new System.Drawing.Size(240, 28);
			this.MainMenuNew.Text = "&New";
			this.MainMenuNew.Click += new System.EventHandler(this.MainMenuNew_Click);
			// 
			// MainMenuEdit
			// 
			this.MainMenuEdit.AutoSize = false;
			this.MainMenuEdit.Name = "MainMenuEdit";
			this.MainMenuEdit.ShortcutKeyDisplayString = "Enter";
			this.MainMenuEdit.Size = new System.Drawing.Size(240, 28);
			this.MainMenuEdit.Text = "&Edit";
			this.MainMenuEdit.Click += new System.EventHandler(this.MainMenuEdit_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(180, 6);
			// 
			// MainMenuDelete
			// 
			this.MainMenuDelete.AutoSize = false;
			this.MainMenuDelete.Name = "MainMenuDelete";
			this.MainMenuDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.MainMenuDelete.Size = new System.Drawing.Size(240, 28);
			this.MainMenuDelete.Text = "&Delete";
			this.MainMenuDelete.Click += new System.EventHandler(this.MainMenuDelete_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(180, 6);
			// 
			// MainMenuRefresh
			// 
			this.MainMenuRefresh.AutoSize = false;
			this.MainMenuRefresh.Name = "MainMenuRefresh";
			this.MainMenuRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
			this.MainMenuRefresh.Size = new System.Drawing.Size(240, 28);
			this.MainMenuRefresh.Text = "&Refresh";
			this.MainMenuRefresh.Click += new System.EventHandler(this.MainMenuRefresh_Click);
			// 
			// MainMenuExportText
			// 
			this.MainMenuExportText.AutoSize = false;
			this.MainMenuExportText.Name = "MainMenuExportText";
			this.MainMenuExportText.Size = new System.Drawing.Size(240, 28);
			this.MainMenuExportText.Text = "Export &Text";
			this.MainMenuExportText.Click += new System.EventHandler(this.MainMenuExportText_Click);
			// 
			// MainMenuExportCSV
			// 
			this.MainMenuExportCSV.AutoSize = false;
			this.MainMenuExportCSV.Name = "MainMenuExportCSV";
			this.MainMenuExportCSV.Size = new System.Drawing.Size(240, 28);
			this.MainMenuExportCSV.Text = "E&xport CSV";
			this.MainMenuExportCSV.Click += new System.EventHandler(this.MainMenuExportCSV_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(180, 6);
			// 
			// MainMenuClose
			// 
			this.MainMenuClose.AutoSize = false;
			this.MainMenuClose.Name = "MainMenuClose";
			this.MainMenuClose.ShortcutKeyDisplayString = "";
			this.MainMenuClose.Size = new System.Drawing.Size(240, 28);
			this.MainMenuClose.Text = "C&lose";
			this.MainMenuClose.Click += new System.EventHandler(this.MainMenuClose_Click);
			// 
			// MainMenuHelp
			// 
			this.MainMenuHelp.Name = "MainMenuHelp";
			this.MainMenuHelp.Size = new System.Drawing.Size(183, 30);
			this.MainMenuHelp.Text = "&Help";
			this.MainMenuHelp.Click += new System.EventHandler(this.MainMenuHelp_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(180, 6);
			// 
			// FacilityList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(578, 644);
			this.Controls.Add(this.FacilityGrid);
			this.Name = "FacilityList";
			this.Text = "Facility List";
			this.Load += new System.EventHandler(this.FacilityList_Load);
			((System.ComponentModel.ISupportInitialize)(this.FacilityGrid)).EndInit();
			this.MainMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		internal LJCWinFormControls.LJCDataGrid FacilityGrid;
		private System.Windows.Forms.ContextMenuStrip MainMenu;
		private System.Windows.Forms.ToolStripMenuItem MainMenuNew;
		private System.Windows.Forms.ToolStripMenuItem MainMenuEdit;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem MainMenuDelete;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem MainMenuRefresh;
		private System.Windows.Forms.ToolStripMenuItem MainMenuExportText;
		private System.Windows.Forms.ToolStripMenuItem MainMenuExportCSV;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem MainMenuClose;
		private System.Windows.Forms.ToolStripMenuItem MainMenuHelp;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
	}
}