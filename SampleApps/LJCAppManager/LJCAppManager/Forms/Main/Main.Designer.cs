namespace LJCAppManager
{
	partial class Main
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
			this.MenuSplit = new System.Windows.Forms.SplitContainer();
			this.MenuTree = new System.Windows.Forms.TreeView();
			this.TreeMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.TreeMenuShow = new System.Windows.Forms.ToolStripMenuItem();
			this.TreeMenuExit = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.TreeMenuAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.TreeImageList = new System.Windows.Forms.ImageList(this.components);
			this.MenuHeader = new LJCWinFormControls.LJCHeaderBox();
			this.TabSplit = new System.Windows.Forms.SplitContainer();
			this.MainTabs = new LJCWinFormControls.LJCTabControl(this.components);
			this.MainTabMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.MainTabMenuShow = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.MainTabMenuMove = new System.Windows.Forms.ToolStripMenuItem();
			this.TileTabs = new LJCWinFormControls.LJCTabControl(this.components);
			this.TileTabMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.TileTabMenuShow = new System.Windows.Forms.ToolStripMenuItem();
			this.TileTabMenuMove = new System.Windows.Forms.ToolStripMenuItem();
			this.TreeMenuHelp = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.MenuSplit)).BeginInit();
			this.MenuSplit.Panel1.SuspendLayout();
			this.MenuSplit.Panel2.SuspendLayout();
			this.MenuSplit.SuspendLayout();
			this.TreeMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.TabSplit)).BeginInit();
			this.TabSplit.Panel1.SuspendLayout();
			this.TabSplit.Panel2.SuspendLayout();
			this.TabSplit.SuspendLayout();
			this.MainTabMenu.SuspendLayout();
			this.TileTabMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// MenuSplit
			// 
			this.MenuSplit.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MenuSplit.Location = new System.Drawing.Point(0, 0);
			this.MenuSplit.Name = "MenuSplit";
			// 
			// MenuSplit.Panel1
			// 
			this.MenuSplit.Panel1.Controls.Add(this.MenuTree);
			this.MenuSplit.Panel1.Controls.Add(this.MenuHeader);
			// 
			// MenuSplit.Panel2
			// 
			this.MenuSplit.Panel2.Controls.Add(this.TabSplit);
			this.MenuSplit.Size = new System.Drawing.Size(1257, 648);
			this.MenuSplit.SplitterDistance = 308;
			this.MenuSplit.TabIndex = 0;
			// 
			// MenuTree
			// 
			this.MenuTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MenuTree.ContextMenuStrip = this.TreeMenu;
			this.MenuTree.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
			this.MenuTree.HideSelection = false;
			this.MenuTree.ImageIndex = 0;
			this.MenuTree.ImageList = this.TreeImageList;
			this.MenuTree.Location = new System.Drawing.Point(1, 32);
			this.MenuTree.Name = "MenuTree";
			this.MenuTree.SelectedImageIndex = 0;
			this.MenuTree.Size = new System.Drawing.Size(303, 611);
			this.MenuTree.TabIndex = 1;
			this.MenuTree.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.MenuTree_DrawNode);
			this.MenuTree.DoubleClick += new System.EventHandler(this.MenuTree_DoubleClick);
			this.MenuTree.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MenuTree_KeyDown);
			this.MenuTree.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MenuTree_MouseDown);
			// 
			// TreeMenu
			// 
			this.TreeMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.TreeMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TreeMenuShow,
            this.TreeMenuExit,
            this.toolStripSeparator1,
            this.TreeMenuHelp,
            this.TreeMenuAbout});
			this.TreeMenu.Name = "TreeMenu";
			this.TreeMenu.Size = new System.Drawing.Size(241, 163);
			// 
			// TreeMenuShow
			// 
			this.TreeMenuShow.Name = "TreeMenuShow";
			this.TreeMenuShow.Size = new System.Drawing.Size(240, 30);
			this.TreeMenuShow.Text = "&Show Module";
			this.TreeMenuShow.Click += new System.EventHandler(this.TreeMenuShow_Click);
			// 
			// TreeMenuExit
			// 
			this.TreeMenuExit.Name = "TreeMenuExit";
			this.TreeMenuExit.Size = new System.Drawing.Size(240, 30);
			this.TreeMenuExit.Text = "E&xit";
			this.TreeMenuExit.Click += new System.EventHandler(this.TreeMenuExit_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(237, 6);
			// 
			// TreeMenuAbout
			// 
			this.TreeMenuAbout.Name = "TreeMenuAbout";
			this.TreeMenuAbout.Size = new System.Drawing.Size(240, 30);
			this.TreeMenuAbout.Text = "&About";
			this.TreeMenuAbout.Click += new System.EventHandler(this.TreeMenuAbout_Click);
			// 
			// TreeImageList
			// 
			this.TreeImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("TreeImageList.ImageStream")));
			this.TreeImageList.TransparentColor = System.Drawing.Color.Transparent;
			this.TreeImageList.Images.SetKeyName(0, "Program20.bmp");
			this.TreeImageList.Images.SetKeyName(1, "Module20.bmp");
			// 
			// MenuHeader
			// 
			this.MenuHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MenuHeader.LJCBeginColor = System.Drawing.Color.AliceBlue;
			this.MenuHeader.LJCEndColor = System.Drawing.Color.LightSkyBlue;
			this.MenuHeader.LJCShowCloseButton = true;
			this.MenuHeader.Location = new System.Drawing.Point(1, 2);
			this.MenuHeader.Name = "MenuHeader";
			this.MenuHeader.Size = new System.Drawing.Size(304, 31);
			this.MenuHeader.TabIndex = 0;
			this.MenuHeader.Text = "Menu";
			// 
			// TabSplit
			// 
			this.TabSplit.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TabSplit.Location = new System.Drawing.Point(0, 0);
			this.TabSplit.Name = "TabSplit";
			// 
			// TabSplit.Panel1
			// 
			this.TabSplit.Panel1.Controls.Add(this.MainTabs);
			// 
			// TabSplit.Panel2
			// 
			this.TabSplit.Panel2.Controls.Add(this.TileTabs);
			this.TabSplit.Size = new System.Drawing.Size(945, 648);
			this.TabSplit.SplitterDistance = 483;
			this.TabSplit.TabIndex = 0;
			// 
			// MainTabs
			// 
			this.MainTabs.AllowDrop = true;
			this.MainTabs.ContextMenuStrip = this.MainTabMenu;
			this.MainTabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainTabs.LJCAllowDrag = true;
			this.MainTabs.Location = new System.Drawing.Point(0, 0);
			this.MainTabs.Name = "MainTabs";
			this.MainTabs.SelectedIndex = 0;
			this.MainTabs.Size = new System.Drawing.Size(483, 648);
			this.MainTabs.TabIndex = 0;
			// 
			// MainTabMenu
			// 
			this.MainTabMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.MainTabMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainTabMenuShow,
            this.toolStripSeparator2,
            this.MainTabMenuMove});
			this.MainTabMenu.Name = "contextMenuStrip1";
			this.MainTabMenu.Size = new System.Drawing.Size(209, 70);
			// 
			// MainTabMenuShow
			// 
			this.MainTabMenuShow.Name = "MainTabMenuShow";
			this.MainTabMenuShow.Size = new System.Drawing.Size(208, 30);
			this.MainTabMenuShow.Text = "Hide Menu";
			this.MainTabMenuShow.Click += new System.EventHandler(this.MainTabMenuShow_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(205, 6);
			// 
			// MainTabMenuMove
			// 
			this.MainTabMenuMove.Name = "MainTabMenuMove";
			this.MainTabMenuMove.Size = new System.Drawing.Size(208, 30);
			this.MainTabMenuMove.Text = "Move Tab Right";
			this.MainTabMenuMove.Click += new System.EventHandler(this.MainTabMenuMove_Click);
			// 
			// TileTabs
			// 
			this.TileTabs.AllowDrop = true;
			this.TileTabs.ContextMenuStrip = this.TileTabMenu;
			this.TileTabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TileTabs.LJCAllowDrag = true;
			this.TileTabs.Location = new System.Drawing.Point(0, 0);
			this.TileTabs.Name = "TileTabs";
			this.TileTabs.SelectedIndex = 0;
			this.TileTabs.Size = new System.Drawing.Size(458, 648);
			this.TileTabs.TabIndex = 0;
			// 
			// TileTabMenu
			// 
			this.TileTabMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.TileTabMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TileTabMenuShow,
            this.TileTabMenuMove});
			this.TileTabMenu.Name = "TileTabMenu";
			this.TileTabMenu.Size = new System.Drawing.Size(196, 64);
			// 
			// TileTabMenuShow
			// 
			this.TileTabMenuShow.Name = "TileTabMenuShow";
			this.TileTabMenuShow.Size = new System.Drawing.Size(195, 30);
			this.TileTabMenuShow.Text = "Hide Menu";
			this.TileTabMenuShow.Click += new System.EventHandler(this.TileTabMenuShow_Click);
			// 
			// TileTabMenuMove
			// 
			this.TileTabMenuMove.Name = "TileTabMenuMove";
			this.TileTabMenuMove.Size = new System.Drawing.Size(195, 30);
			this.TileTabMenuMove.Text = "Move Tab Left";
			this.TileTabMenuMove.Click += new System.EventHandler(this.TileTabMenuMove_Click);
			// 
			// TreeMenuHelp
			// 
			this.TreeMenuHelp.Name = "TreeMenuHelp";
			this.TreeMenuHelp.Size = new System.Drawing.Size(240, 30);
			this.TreeMenuHelp.Text = "&Help";
			this.TreeMenuHelp.Click += new System.EventHandler(this.TreeMenuHelp_Click);
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1257, 648);
			this.Controls.Add(this.MenuSplit);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Main";
			this.Text = "Application Manager";
			this.Load += new System.EventHandler(this.Main_Load);
			this.MenuSplit.Panel1.ResumeLayout(false);
			this.MenuSplit.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.MenuSplit)).EndInit();
			this.MenuSplit.ResumeLayout(false);
			this.TreeMenu.ResumeLayout(false);
			this.TabSplit.Panel1.ResumeLayout(false);
			this.TabSplit.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.TabSplit)).EndInit();
			this.TabSplit.ResumeLayout(false);
			this.MainTabMenu.ResumeLayout(false);
			this.TileTabMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer MenuSplit;
		private System.Windows.Forms.SplitContainer TabSplit;
		private LJCWinFormControls.LJCHeaderBox MenuHeader;
		private System.Windows.Forms.TreeView MenuTree;
		private LJCWinFormControls.LJCTabControl MainTabs;
		private LJCWinFormControls.LJCTabControl TileTabs;
		private System.Windows.Forms.ContextMenuStrip TreeMenu;
		private System.Windows.Forms.ToolStripMenuItem TreeMenuShow;
		private System.Windows.Forms.ToolStripMenuItem TreeMenuExit;
		private System.Windows.Forms.ImageList TreeImageList;
		private System.Windows.Forms.ToolStripMenuItem TreeMenuAbout;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ContextMenuStrip MainTabMenu;
		private System.Windows.Forms.ToolStripMenuItem MainTabMenuShow;
		private System.Windows.Forms.ToolStripMenuItem MainTabMenuMove;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ContextMenuStrip TileTabMenu;
		private System.Windows.Forms.ToolStripMenuItem TileTabMenuShow;
		private System.Windows.Forms.ToolStripMenuItem TileTabMenuMove;
		private System.Windows.Forms.ToolStripMenuItem TreeMenuHelp;
	}
}

