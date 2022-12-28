namespace CVRManager
{
	partial class CVPersonList
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CVPersonList));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			this.MainToolPanel = new System.Windows.Forms.Panel();
			this.ShowButton = new System.Windows.Forms.Button();
			this.FirstNameTextBox = new System.Windows.Forms.TextBox();
			this.FirstNameLabel = new System.Windows.Forms.Label();
			this.LastNameTextBox = new System.Windows.Forms.TextBox();
			this.LastNameLabel = new System.Windows.Forms.Label();
			this.MainTools = new System.Windows.Forms.ToolStrip();
			this.MainToolsNew = new System.Windows.Forms.ToolStripButton();
			this.MainToolsEdit = new System.Windows.Forms.ToolStripButton();
			this.MainToolsBar1 = new System.Windows.Forms.ToolStripSeparator();
			this.MainToolsSelect = new System.Windows.Forms.ToolStripButton();
			this.PersonMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.PersonMenuNew = new System.Windows.Forms.ToolStripMenuItem();
			this.PersonMenuEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.FacilityDeleteSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.PersonMenuDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.FacilityRefreshSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.PersonMenuRefresh = new System.Windows.Forms.ToolStripMenuItem();
			this.PersonMenuExportText = new System.Windows.Forms.ToolStripMenuItem();
			this.PersonMenuExportCSV = new System.Windows.Forms.ToolStripMenuItem();
			this.SelectSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.PersonMenuSelect = new System.Windows.Forms.ToolStripMenuItem();
			this.FacilityCloseSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.PersonMenuClose = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.PersonMenuHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.CVPersonGrid = new LJCWinFormControls.LJCDataGrid(this.components);
			this.MainToolPanel.SuspendLayout();
			this.MainTools.SuspendLayout();
			this.PersonMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.CVPersonGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// MainToolPanel
			// 
			this.MainToolPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MainToolPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.MainToolPanel.Controls.Add(this.ShowButton);
			this.MainToolPanel.Controls.Add(this.FirstNameTextBox);
			this.MainToolPanel.Controls.Add(this.FirstNameLabel);
			this.MainToolPanel.Controls.Add(this.LastNameTextBox);
			this.MainToolPanel.Controls.Add(this.LastNameLabel);
			this.MainToolPanel.Controls.Add(this.MainTools);
			this.MainToolPanel.Location = new System.Drawing.Point(0, 0);
			this.MainToolPanel.Name = "MainToolPanel";
			this.MainToolPanel.Size = new System.Drawing.Size(592, 83);
			this.MainToolPanel.TabIndex = 0;
			// 
			// ShowButton
			// 
			this.ShowButton.Location = new System.Drawing.Point(504, 6);
			this.ShowButton.Name = "ShowButton";
			this.ShowButton.Size = new System.Drawing.Size(75, 30);
			this.ShowButton.TabIndex = 3;
			this.ShowButton.Text = "Show";
			this.ShowButton.UseVisualStyleBackColor = true;
			this.ShowButton.Click += new System.EventHandler(this.ShowButton_Click);
			// 
			// FirstNameTextBox
			// 
			this.FirstNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FirstNameTextBox.Location = new System.Drawing.Point(339, 43);
			this.FirstNameTextBox.Name = "FirstNameTextBox";
			this.FirstNameTextBox.Size = new System.Drawing.Size(159, 30);
			this.FirstNameTextBox.TabIndex = 5;
			// 
			// FirstNameLabel
			// 
			this.FirstNameLabel.Location = new System.Drawing.Point(241, 47);
			this.FirstNameLabel.Name = "FirstNameLabel";
			this.FirstNameLabel.Size = new System.Drawing.Size(96, 23);
			this.FirstNameLabel.TabIndex = 4;
			this.FirstNameLabel.Text = "First Name";
			// 
			// LastNameTextBox
			// 
			this.LastNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.LastNameTextBox.Location = new System.Drawing.Point(339, 7);
			this.LastNameTextBox.Name = "LastNameTextBox";
			this.LastNameTextBox.Size = new System.Drawing.Size(159, 30);
			this.LastNameTextBox.TabIndex = 2;
			// 
			// LastNameLabel
			// 
			this.LastNameLabel.Location = new System.Drawing.Point(241, 11);
			this.LastNameLabel.Name = "LastNameLabel";
			this.LastNameLabel.Size = new System.Drawing.Size(96, 23);
			this.LastNameLabel.TabIndex = 1;
			this.LastNameLabel.Text = "Last Name";
			// 
			// MainTools
			// 
			this.MainTools.AutoSize = false;
			this.MainTools.Dock = System.Windows.Forms.DockStyle.None;
			this.MainTools.ImageScalingSize = new System.Drawing.Size(36, 47);
			this.MainTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainToolsNew,
            this.MainToolsEdit,
            this.MainToolsBar1,
            this.MainToolsSelect});
			this.MainTools.Location = new System.Drawing.Point(0, 0);
			this.MainTools.Name = "MainTools";
			this.MainTools.ShowItemToolTips = false;
			this.MainTools.Size = new System.Drawing.Size(220, 83);
			this.MainTools.TabIndex = 0;
			this.MainTools.Text = "toolStrip1";
			// 
			// MainToolsNew
			// 
			this.MainToolsNew.AutoSize = false;
			this.MainToolsNew.Image = ((System.Drawing.Image)(resources.GetObject("MainToolsNew.Image")));
			this.MainToolsNew.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.MainToolsNew.Margin = new System.Windows.Forms.Padding(2);
			this.MainToolsNew.Name = "MainToolsNew";
			this.MainToolsNew.Size = new System.Drawing.Size(38, 48);
			this.MainToolsNew.Text = "New";
			this.MainToolsNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.MainToolsNew.Click += new System.EventHandler(this.MainToolsNew_Click);
			// 
			// MainToolsEdit
			// 
			this.MainToolsEdit.AutoSize = false;
			this.MainToolsEdit.Image = ((System.Drawing.Image)(resources.GetObject("MainToolsEdit.Image")));
			this.MainToolsEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.MainToolsEdit.Margin = new System.Windows.Forms.Padding(2);
			this.MainToolsEdit.Name = "MainToolsEdit";
			this.MainToolsEdit.Size = new System.Drawing.Size(38, 48);
			this.MainToolsEdit.Text = "Edit";
			this.MainToolsEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.MainToolsEdit.Click += new System.EventHandler(this.MainToolsEdit_Click);
			// 
			// MainToolsBar1
			// 
			this.MainToolsBar1.Name = "MainToolsBar1";
			this.MainToolsBar1.Size = new System.Drawing.Size(6, 83);
			// 
			// MainToolsSelect
			// 
			this.MainToolsSelect.AutoSize = false;
			this.MainToolsSelect.Image = ((System.Drawing.Image)(resources.GetObject("MainToolsSelect.Image")));
			this.MainToolsSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.MainToolsSelect.Margin = new System.Windows.Forms.Padding(2);
			this.MainToolsSelect.Name = "MainToolsSelect";
			this.MainToolsSelect.Size = new System.Drawing.Size(38, 48);
			this.MainToolsSelect.Text = "Select";
			this.MainToolsSelect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.MainToolsSelect.Click += new System.EventHandler(this.MainToolsSelect_Click);
			// 
			// PersonMenu
			// 
			this.PersonMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.PersonMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PersonMenuNew,
            this.PersonMenuEdit,
            this.FacilityDeleteSeparator,
            this.PersonMenuDelete,
            this.FacilityRefreshSeparator,
            this.PersonMenuRefresh,
            this.PersonMenuExportText,
            this.PersonMenuExportCSV,
            this.SelectSeparator,
            this.PersonMenuSelect,
            this.FacilityCloseSeparator,
            this.PersonMenuClose,
            this.toolStripSeparator1,
            this.PersonMenuHelp});
			this.PersonMenu.Name = "mFacilityMenu";
			this.PersonMenu.Size = new System.Drawing.Size(204, 292);
			// 
			// PersonMenuNew
			// 
			this.PersonMenuNew.AutoSize = false;
			this.PersonMenuNew.Name = "PersonMenuNew";
			this.PersonMenuNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.PersonMenuNew.Size = new System.Drawing.Size(240, 28);
			this.PersonMenuNew.Text = "&New";
			this.PersonMenuNew.Click += new System.EventHandler(this.PersonMenuNew_Click);
			// 
			// PersonMenuEdit
			// 
			this.PersonMenuEdit.AutoSize = false;
			this.PersonMenuEdit.Name = "PersonMenuEdit";
			this.PersonMenuEdit.ShortcutKeyDisplayString = "Enter";
			this.PersonMenuEdit.Size = new System.Drawing.Size(240, 28);
			this.PersonMenuEdit.Text = "&Edit";
			this.PersonMenuEdit.Click += new System.EventHandler(this.PersonMenuEdit_Click);
			// 
			// FacilityDeleteSeparator
			// 
			this.FacilityDeleteSeparator.Name = "FacilityDeleteSeparator";
			this.FacilityDeleteSeparator.Size = new System.Drawing.Size(200, 6);
			// 
			// PersonMenuDelete
			// 
			this.PersonMenuDelete.AutoSize = false;
			this.PersonMenuDelete.Name = "PersonMenuDelete";
			this.PersonMenuDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.PersonMenuDelete.Size = new System.Drawing.Size(240, 28);
			this.PersonMenuDelete.Text = "&Delete";
			this.PersonMenuDelete.Click += new System.EventHandler(this.PersonMenuDelete_Click);
			// 
			// FacilityRefreshSeparator
			// 
			this.FacilityRefreshSeparator.Name = "FacilityRefreshSeparator";
			this.FacilityRefreshSeparator.Size = new System.Drawing.Size(200, 6);
			// 
			// PersonMenuRefresh
			// 
			this.PersonMenuRefresh.AutoSize = false;
			this.PersonMenuRefresh.Name = "PersonMenuRefresh";
			this.PersonMenuRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
			this.PersonMenuRefresh.Size = new System.Drawing.Size(240, 28);
			this.PersonMenuRefresh.Text = "&Refresh";
			this.PersonMenuRefresh.Click += new System.EventHandler(this.PersonMenuRefresh_Click);
			// 
			// PersonMenuExportText
			// 
			this.PersonMenuExportText.Name = "PersonMenuExportText";
			this.PersonMenuExportText.Size = new System.Drawing.Size(203, 30);
			this.PersonMenuExportText.Text = "Export &Text File";
			this.PersonMenuExportText.Click += new System.EventHandler(this.PersonMenuExportText_Click);
			// 
			// PersonMenuExportCSV
			// 
			this.PersonMenuExportCSV.Name = "PersonMenuExportCSV";
			this.PersonMenuExportCSV.Size = new System.Drawing.Size(203, 30);
			this.PersonMenuExportCSV.Text = "E&xport CSV File";
			this.PersonMenuExportCSV.Click += new System.EventHandler(this.PersonMenuExportCSV_Click);
			// 
			// SelectSeparator
			// 
			this.SelectSeparator.Name = "SelectSeparator";
			this.SelectSeparator.Size = new System.Drawing.Size(200, 6);
			// 
			// PersonMenuSelect
			// 
			this.PersonMenuSelect.AutoSize = false;
			this.PersonMenuSelect.Name = "PersonMenuSelect";
			this.PersonMenuSelect.ShortcutKeyDisplayString = "";
			this.PersonMenuSelect.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
			this.PersonMenuSelect.Size = new System.Drawing.Size(240, 28);
			this.PersonMenuSelect.Text = "&Select";
			this.PersonMenuSelect.Click += new System.EventHandler(this.PersonMenuSelect_Click);
			// 
			// FacilityCloseSeparator
			// 
			this.FacilityCloseSeparator.Name = "FacilityCloseSeparator";
			this.FacilityCloseSeparator.Size = new System.Drawing.Size(200, 6);
			// 
			// PersonMenuClose
			// 
			this.PersonMenuClose.AutoSize = false;
			this.PersonMenuClose.Name = "PersonMenuClose";
			this.PersonMenuClose.Size = new System.Drawing.Size(240, 28);
			this.PersonMenuClose.Text = "&Close";
			this.PersonMenuClose.Click += new System.EventHandler(this.PersonMenuClose_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(200, 6);
			// 
			// PersonMenuHelp
			// 
			this.PersonMenuHelp.Name = "PersonMenuHelp";
			this.PersonMenuHelp.ShortcutKeyDisplayString = "F1";
			this.PersonMenuHelp.Size = new System.Drawing.Size(203, 30);
			this.PersonMenuHelp.Text = "&Help";
			this.PersonMenuHelp.Click += new System.EventHandler(this.PersonMenuHelp_Click);
			// 
			// CVPersonGrid
			// 
			this.CVPersonGrid.AllowUserToAddRows = false;
			this.CVPersonGrid.AllowUserToDeleteRows = false;
			this.CVPersonGrid.AllowUserToResizeRows = false;
			this.CVPersonGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.CVPersonGrid.BackgroundColor = System.Drawing.Color.AliceBlue;
			this.CVPersonGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.CVPersonGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.CVPersonGrid.ContextMenuStrip = this.PersonMenu;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.CVPersonGrid.DefaultCellStyle = dataGridViewCellStyle1;
			this.CVPersonGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.CVPersonGrid.LJCAllowSelectionChange = false;
			this.CVPersonGrid.LJCLastRowIndex = -1;
			this.CVPersonGrid.LJCRowHeight = 22;
			this.CVPersonGrid.Location = new System.Drawing.Point(0, 82);
			this.CVPersonGrid.MultiSelect = false;
			this.CVPersonGrid.Name = "CVPersonGrid";
			this.CVPersonGrid.RowHeadersVisible = false;
			this.CVPersonGrid.RowTemplate.Height = 28;
			this.CVPersonGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.CVPersonGrid.ShowCellToolTips = false;
			this.CVPersonGrid.Size = new System.Drawing.Size(594, 662);
			this.CVPersonGrid.TabIndex = 1;
			this.CVPersonGrid.Text = "LJCDataGrid";
			this.CVPersonGrid.SelectionChanged += new System.EventHandler(this.CVPersonGrid_SelectionChanged);
			this.CVPersonGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CVPersonGrid_KeyDown);
			this.CVPersonGrid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.CVPersonGrid_MouseDoubleClick);
			this.CVPersonGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CVPersonGrid_MouseDown);
			// 
			// CVPersonList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(592, 744);
			this.Controls.Add(this.CVPersonGrid);
			this.Controls.Add(this.MainToolPanel);
			this.Name = "CVPersonList";
			this.Text = "Person List";
			this.Load += new System.EventHandler(this.CVPersonList_Load);
			this.MainToolPanel.ResumeLayout(false);
			this.MainToolPanel.PerformLayout();
			this.MainTools.ResumeLayout(false);
			this.MainTools.PerformLayout();
			this.PersonMenu.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.CVPersonGrid)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel MainToolPanel;
		private System.Windows.Forms.ToolStrip MainTools;
		private System.Windows.Forms.ToolStripButton MainToolsNew;
		private System.Windows.Forms.ToolStripButton MainToolsEdit;
		private System.Windows.Forms.ToolStripSeparator MainToolsBar1;
		private System.Windows.Forms.ToolStripButton MainToolsSelect;
		private System.Windows.Forms.ContextMenuStrip PersonMenu;
		private System.Windows.Forms.ToolStripMenuItem PersonMenuNew;
		private System.Windows.Forms.ToolStripMenuItem PersonMenuEdit;
		private System.Windows.Forms.ToolStripSeparator FacilityDeleteSeparator;
		private System.Windows.Forms.ToolStripMenuItem PersonMenuDelete;
		private System.Windows.Forms.ToolStripSeparator FacilityRefreshSeparator;
		private System.Windows.Forms.ToolStripMenuItem PersonMenuRefresh;
		private System.Windows.Forms.ToolStripSeparator SelectSeparator;
		private System.Windows.Forms.ToolStripMenuItem PersonMenuSelect;
		private System.Windows.Forms.ToolStripSeparator FacilityCloseSeparator;
		private System.Windows.Forms.ToolStripMenuItem PersonMenuClose;
		internal LJCWinFormControls.LJCDataGrid CVPersonGrid;
		internal System.Windows.Forms.TextBox FirstNameTextBox;
		private System.Windows.Forms.Label FirstNameLabel;
		internal System.Windows.Forms.TextBox LastNameTextBox;
		private System.Windows.Forms.Label LastNameLabel;
		private System.Windows.Forms.Button ShowButton;
		private System.Windows.Forms.ToolStripMenuItem PersonMenuExportText;
		private System.Windows.Forms.ToolStripMenuItem PersonMenuExportCSV;
		private System.Windows.Forms.ToolStripMenuItem PersonMenuHelp;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
	}
}