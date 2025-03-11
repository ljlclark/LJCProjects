namespace LJCTransformManager
{
	partial class TableViewer
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TableViewer));
			this.RecordsGrid = new LJCWinFormControls.LJCDataGrid(this.components);
			this.TableMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.TableMenuDrop = new System.Windows.Forms.ToolStripMenuItem();
			this.RecordsToolPanel = new System.Windows.Forms.Panel();
			this.RecordsCounter = new System.Windows.Forms.Label();
			this.RecordsTool = new System.Windows.Forms.ToolStrip();
			this.DataProcessToolNew = new System.Windows.Forms.ToolStripButton();
			this.DataProcessToolEdit = new System.Windows.Forms.ToolStripButton();
			this.DataProcessToolDelete = new System.Windows.Forms.ToolStripButton();
			this.TableMenuExit = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			((System.ComponentModel.ISupportInitialize)(this.RecordsGrid)).BeginInit();
			this.TableMenu.SuspendLayout();
			this.RecordsToolPanel.SuspendLayout();
			this.RecordsTool.SuspendLayout();
			this.SuspendLayout();
			// 
			// RecordsGrid
			// 
			this.RecordsGrid.AllowUserToAddRows = false;
			this.RecordsGrid.AllowUserToDeleteRows = false;
			this.RecordsGrid.AllowUserToResizeRows = false;
			this.RecordsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.RecordsGrid.BackgroundColor = System.Drawing.Color.AliceBlue;
			this.RecordsGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.RecordsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.RecordsGrid.ContextMenuStrip = this.TableMenu;
			this.RecordsGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.RecordsGrid.LJCAllowSelectionChange = false;
			this.RecordsGrid.LJCLastRowIndex = -1;
			this.RecordsGrid.Location = new System.Drawing.Point(0, 39);
			this.RecordsGrid.MultiSelect = false;
			this.RecordsGrid.Name = "RecordsGrid";
			this.RecordsGrid.RowHeadersVisible = false;
			this.RecordsGrid.RowTemplate.Height = 28;
			this.RecordsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.RecordsGrid.ShowCellToolTips = false;
			this.RecordsGrid.Size = new System.Drawing.Size(778, 505);
			this.RecordsGrid.TabIndex = 3;
			this.RecordsGrid.Text = "LJCDataGrid";
			this.RecordsGrid.SelectionChanged += new System.EventHandler(this.RecordsGrid_SelectionChanged);
			this.RecordsGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RecordsGrid_KeyDown);
			this.RecordsGrid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.RecordsGrid_MouseDoubleClick);
			this.RecordsGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RecordsGrid_MouseDown);
			// 
			// TableMenu
			// 
			this.TableMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.TableMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TableMenuDrop,
            this.toolStripSeparator1,
            this.TableMenuExit});
			this.TableMenu.Name = "TableMenu";
			this.TableMenu.Size = new System.Drawing.Size(241, 103);
			// 
			// TableMenuDrop
			// 
			this.TableMenuDrop.Name = "TableMenuDrop";
			this.TableMenuDrop.Size = new System.Drawing.Size(240, 30);
			this.TableMenuDrop.Text = "Drop &Table";
			this.TableMenuDrop.Click += new System.EventHandler(this.TableMenuDrop_Click);
			// 
			// RecordsToolPanel
			// 
			this.RecordsToolPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.RecordsToolPanel.BackColor = System.Drawing.SystemColors.Control;
			this.RecordsToolPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.RecordsToolPanel.Controls.Add(this.RecordsCounter);
			this.RecordsToolPanel.Controls.Add(this.RecordsTool);
			this.RecordsToolPanel.Location = new System.Drawing.Point(0, 0);
			this.RecordsToolPanel.Name = "RecordsToolPanel";
			this.RecordsToolPanel.Size = new System.Drawing.Size(778, 40);
			this.RecordsToolPanel.TabIndex = 2;
			// 
			// RecordsCounter
			// 
			this.RecordsCounter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.RecordsCounter.Location = new System.Drawing.Point(523, 13);
			this.RecordsCounter.Name = "RecordsCounter";
			this.RecordsCounter.Size = new System.Drawing.Size(250, 23);
			this.RecordsCounter.TabIndex = 1;
			this.RecordsCounter.Text = "Row 0 of 0";
			this.RecordsCounter.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// RecordsTool
			// 
			this.RecordsTool.Dock = System.Windows.Forms.DockStyle.None;
			this.RecordsTool.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.RecordsTool.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.RecordsTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DataProcessToolNew,
            this.DataProcessToolEdit,
            this.DataProcessToolDelete});
			this.RecordsTool.Location = new System.Drawing.Point(3, 2);
			this.RecordsTool.Name = "RecordsTool";
			this.RecordsTool.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
			this.RecordsTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.RecordsTool.Size = new System.Drawing.Size(73, 25);
			this.RecordsTool.TabIndex = 0;
			this.RecordsTool.Text = "FirstTool";
			// 
			// DataProcessToolNew
			// 
			this.DataProcessToolNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.DataProcessToolNew.Image = ((System.Drawing.Image)(resources.GetObject("DataProcessToolNew.Image")));
			this.DataProcessToolNew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.DataProcessToolNew.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.DataProcessToolNew.Name = "DataProcessToolNew";
			this.DataProcessToolNew.Size = new System.Drawing.Size(23, 22);
			this.DataProcessToolNew.Text = "New";
			// 
			// DataProcessToolEdit
			// 
			this.DataProcessToolEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.DataProcessToolEdit.Image = ((System.Drawing.Image)(resources.GetObject("DataProcessToolEdit.Image")));
			this.DataProcessToolEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.DataProcessToolEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.DataProcessToolEdit.Name = "DataProcessToolEdit";
			this.DataProcessToolEdit.Size = new System.Drawing.Size(23, 22);
			this.DataProcessToolEdit.Text = "Edit";
			// 
			// DataProcessToolDelete
			// 
			this.DataProcessToolDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.DataProcessToolDelete.Image = ((System.Drawing.Image)(resources.GetObject("DataProcessToolDelete.Image")));
			this.DataProcessToolDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.DataProcessToolDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.DataProcessToolDelete.Name = "DataProcessToolDelete";
			this.DataProcessToolDelete.Size = new System.Drawing.Size(23, 22);
			this.DataProcessToolDelete.Text = "Delete";
			// 
			// TableMenuExit
			// 
			this.TableMenuExit.Name = "TableMenuExit";
			this.TableMenuExit.Size = new System.Drawing.Size(240, 30);
			this.TableMenuExit.Text = "&Exit";
			this.TableMenuExit.Click += new System.EventHandler(this.TableMenuExit_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(237, 6);
			// 
			// TableViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(778, 544);
			this.Controls.Add(this.RecordsGrid);
			this.Controls.Add(this.RecordsToolPanel);
			this.Name = "TableViewer";
			this.Text = "Table Viewer";
			this.Load += new System.EventHandler(this.TableViewer_Load);
			((System.ComponentModel.ISupportInitialize)(this.RecordsGrid)).EndInit();
			this.TableMenu.ResumeLayout(false);
			this.RecordsToolPanel.ResumeLayout(false);
			this.RecordsToolPanel.PerformLayout();
			this.RecordsTool.ResumeLayout(false);
			this.RecordsTool.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private LJCWinFormControls.LJCDataGrid RecordsGrid;
		private System.Windows.Forms.Panel RecordsToolPanel;
		private System.Windows.Forms.Label RecordsCounter;
		private System.Windows.Forms.ToolStrip RecordsTool;
		private System.Windows.Forms.ToolStripButton DataProcessToolNew;
		private System.Windows.Forms.ToolStripButton DataProcessToolEdit;
		private System.Windows.Forms.ToolStripButton DataProcessToolDelete;
		private System.Windows.Forms.ContextMenuStrip TableMenu;
		private System.Windows.Forms.ToolStripMenuItem TableMenuDrop;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem TableMenuExit;
	}
}