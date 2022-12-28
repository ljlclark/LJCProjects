namespace LJCWinFormControls
{
	partial class Calendar
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
			this.CalendarControl = new System.Windows.Forms.MonthCalendar();
			this.CalendarMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.CalendarMenuToday = new System.Windows.Forms.ToolStripMenuItem();
			this.CalendarMenuClose = new System.Windows.Forms.ToolStripMenuItem();
			this.CalendarMenuDate = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.CalendarMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// CalendarControl
			// 
			this.CalendarControl.ContextMenuStrip = this.CalendarMenu;
			this.CalendarControl.Location = new System.Drawing.Point(0, 0);
			this.CalendarControl.Margin = new System.Windows.Forms.Padding(14);
			this.CalendarControl.Name = "CalendarControl";
			this.CalendarControl.TabIndex = 0;
			this.CalendarControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CalendarControl_KeyDown);
			this.CalendarControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CalendarControl_MouseDown);
			// 
			// CalendarMenu
			// 
			this.CalendarMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.CalendarMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CalendarMenuDate,
            this.toolStripSeparator1,
            this.CalendarMenuToday,
            this.toolStripSeparator2,
            this.CalendarMenuClose});
			this.CalendarMenu.Name = "mMenu";
			this.CalendarMenu.Size = new System.Drawing.Size(241, 139);
			// 
			// CalendarMenuToday
			// 
			this.CalendarMenuToday.Name = "CalendarMenuToday";
			this.CalendarMenuToday.Size = new System.Drawing.Size(240, 30);
			this.CalendarMenuToday.Text = "&Select Today";
			this.CalendarMenuToday.Click += new System.EventHandler(this.MenuTodayItem_Click);
			// 
			// CalendarMenuClose
			// 
			this.CalendarMenuClose.Name = "CalendarMenuClose";
			this.CalendarMenuClose.Size = new System.Drawing.Size(240, 30);
			this.CalendarMenuClose.Text = "&Close";
			this.CalendarMenuClose.Click += new System.EventHandler(this.MenuCloseItem_Click);
			// 
			// CalendarMenuDate
			// 
			this.CalendarMenuDate.Name = "CalendarMenuDate";
			this.CalendarMenuDate.Size = new System.Drawing.Size(240, 30);
			this.CalendarMenuDate.Text = "Select &Date";
			this.CalendarMenuDate.Click += new System.EventHandler(this.CalendarMenuDate_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(237, 6);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(237, 6);
			// 
			// Calendar
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(339, 248);
			this.Controls.Add(this.CalendarControl);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.KeyPreview = true;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "Calendar";
			this.ShowInTaskbar = false;
			this.Text = "Calendar";
			this.Load += new System.EventHandler(this.Calendar_Load);
			this.CalendarMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.MonthCalendar CalendarControl;
		private System.Windows.Forms.ContextMenuStrip CalendarMenu;
		private System.Windows.Forms.ToolStripMenuItem CalendarMenuToday;
		private System.Windows.Forms.ToolStripMenuItem CalendarMenuClose;
		private System.Windows.Forms.ToolStripMenuItem CalendarMenuDate;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
	}
}