namespace LJCDataDetail
{
	partial class SelectList
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
			this.DataGrid = new LJCWinFormControls.LJCDataGrid(this.components);
			this.DataMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.DataMenuClose = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.DataGrid)).BeginInit();
			this.DataMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// DataGrid
			// 
			this.DataGrid.AllowUserToAddRows = false;
			this.DataGrid.AllowUserToDeleteRows = false;
			this.DataGrid.AllowUserToResizeRows = false;
			this.DataGrid.BackgroundColor = System.Drawing.Color.AliceBlue;
			this.DataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DataGrid.ContextMenuStrip = this.DataMenu;
			this.DataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DataGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.DataGrid.LJCAllowSelectionChange = false;
			this.DataGrid.LJCLastRowIndex = -1;
			this.DataGrid.LJCRowHeight = 0;
			this.DataGrid.Location = new System.Drawing.Point(0, 0);
			this.DataGrid.MultiSelect = false;
			this.DataGrid.Name = "DataGrid";
			this.DataGrid.RowHeadersVisible = false;
			this.DataGrid.RowTemplate.Height = 28;
			this.DataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.DataGrid.ShowCellToolTips = false;
			this.DataGrid.Size = new System.Drawing.Size(778, 544);
			this.DataGrid.TabIndex = 0;
			this.DataGrid.Text = "LJCDataGrid";
			// 
			// DataMenu
			// 
			this.DataMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.DataMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DataMenuClose});
			this.DataMenu.Name = "DataMenu";
			this.DataMenu.Size = new System.Drawing.Size(128, 34);
			// 
			// DataMenuClose
			// 
			this.DataMenuClose.Name = "DataMenuClose";
			this.DataMenuClose.Size = new System.Drawing.Size(127, 30);
			this.DataMenuClose.Text = "&Close";
			this.DataMenuClose.Click += new System.EventHandler(this.DataMenuClose_Click);
			// 
			// SelectList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(778, 544);
			this.Controls.Add(this.DataGrid);
			this.Name = "SelectList";
			this.ShowInTaskbar = false;
			this.Text = "Select List";
			this.Load += new System.EventHandler(this.SelectList_Load);
			((System.ComponentModel.ISupportInitialize)(this.DataGrid)).EndInit();
			this.DataMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private LJCWinFormControls.LJCDataGrid DataGrid;
		private System.Windows.Forms.ContextMenuStrip DataMenu;
		private System.Windows.Forms.ToolStripMenuItem DataMenuClose;
	}
}