﻿namespace ModuleHost
{
	partial class ModuleHostForm
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
			this.FormTabs = new System.Windows.Forms.TabControl();
			this.SuspendLayout();
			// 
			// FormTabs
			// 
			this.FormTabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.FormTabs.Location = new System.Drawing.Point(0, 0);
			this.FormTabs.Name = "FormTabs";
			this.FormTabs.SelectedIndex = 0;
			this.FormTabs.Size = new System.Drawing.Size(801, 601);
			this.FormTabs.TabIndex = 0;
			// 
			// ModuleHostForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(801, 601);
			this.Controls.Add(this.FormTabs);
			this.Name = "ModuleHostForm";
			this.Text = "App Manager Host";
			this.Load += new System.EventHandler(this.ModuleHostForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl FormTabs;
	}
}

