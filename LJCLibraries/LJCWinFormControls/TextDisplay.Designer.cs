namespace LJCWinFormControls
{
	partial class TextDisplay
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
			this.TextRichTextbox = new LJCWinFormControls.LJCRtControl();
			this.TextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.MenuCopyAllItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuSelectAllItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuCopyItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuPasteItem = new System.Windows.Forms.ToolStripMenuItem();
			this.TextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// TextRichTextbox
			// 
			this.TextRichTextbox.ContextMenuStrip = this.TextMenu;
			this.TextRichTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TextRichTextbox.Location = new System.Drawing.Point(0, 0);
			this.TextRichTextbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.TextRichTextbox.Name = "TextRichTextbox";
			this.TextRichTextbox.Size = new System.Drawing.Size(627, 674);
			this.TextRichTextbox.TabIndex = 0;
			this.TextRichTextbox.Text = "";
			// 
			// TextMenu
			// 
			this.TextMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.TextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuCopyAllItem,
            this.MenuSelectAllItem,
            this.MenuCopyItem,
            this.MenuPasteItem});
			this.TextMenu.Name = "mMenu";
			this.TextMenu.Size = new System.Drawing.Size(214, 124);
			// 
			// MenuCopyAllItem
			// 
			this.MenuCopyAllItem.Name = "MenuCopyAllItem";
			this.MenuCopyAllItem.Size = new System.Drawing.Size(213, 30);
			this.MenuCopyAllItem.Text = "Copy All";
			this.MenuCopyAllItem.Click += new System.EventHandler(this.MenuCopyAllItem_Click);
			// 
			// MenuSelectAllItem
			// 
			this.MenuSelectAllItem.Name = "MenuSelectAllItem";
			this.MenuSelectAllItem.ShortcutKeyDisplayString = "Ctrl-A";
			this.MenuSelectAllItem.Size = new System.Drawing.Size(213, 30);
			this.MenuSelectAllItem.Text = "Select All";
			this.MenuSelectAllItem.Click += new System.EventHandler(this.MenuSelectAllItem_Click);
			// 
			// MenuCopyItem
			// 
			this.MenuCopyItem.Name = "MenuCopyItem";
			this.MenuCopyItem.ShortcutKeyDisplayString = "Ctrl-C";
			this.MenuCopyItem.Size = new System.Drawing.Size(213, 30);
			this.MenuCopyItem.Text = "Copy";
			this.MenuCopyItem.Click += new System.EventHandler(this.MenuCopyItem_Click);
			// 
			// MenuPasteItem
			// 
			this.MenuPasteItem.Name = "MenuPasteItem";
			this.MenuPasteItem.ShortcutKeyDisplayString = "Ctrl-V";
			this.MenuPasteItem.Size = new System.Drawing.Size(213, 30);
			this.MenuPasteItem.Text = "Paste";
			this.MenuPasteItem.Click += new System.EventHandler(this.MenuPasteItem_Click);
			// 
			// TextDisplay
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(627, 674);
			this.Controls.Add(this.TextRichTextbox);
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "TextDisplay";
			this.Text = "Text Display";
			this.TextMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private LJCWinFormControls.LJCRtControl TextRichTextbox;
		private System.Windows.Forms.ContextMenuStrip TextMenu;
		private System.Windows.Forms.ToolStripMenuItem MenuSelectAllItem;
		private System.Windows.Forms.ToolStripMenuItem MenuCopyItem;
		private System.Windows.Forms.ToolStripMenuItem MenuPasteItem;
		private System.Windows.Forms.ToolStripMenuItem MenuCopyAllItem;
	}
}