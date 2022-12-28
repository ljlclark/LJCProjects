namespace CVRManager
{
	partial class VisitFilterDetail
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VisitFilterDetail));
			this.BeginDateButton = new System.Windows.Forms.Button();
			this.ButtonImages = new System.Windows.Forms.ImageList(this.components);
			this.BeginDateMask = new CVRManager.LJCMaskBox();
			this.BeginDateLabel = new System.Windows.Forms.Label();
			this.EndDateButton = new System.Windows.Forms.Button();
			this.EndDateMask = new CVRManager.LJCMaskBox();
			this.EndDateLabel = new System.Windows.Forms.Label();
			this.MiddleNameTextBox = new System.Windows.Forms.TextBox();
			this.MiddleNameLabel = new System.Windows.Forms.Label();
			this.LastNameTextBox = new System.Windows.Forms.TextBox();
			this.LastNameLabel = new System.Windows.Forms.Label();
			this.FirstNameTextBox = new System.Windows.Forms.TextBox();
			this.FirstNameLabel = new System.Windows.Forms.Label();
			this.FormCancelButton = new System.Windows.Forms.Button();
			this.OKButton = new System.Windows.Forms.Button();
			this.DialogMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.DialogMenuHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.DialogMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// BeginDateButton
			// 
			this.BeginDateButton.ImageKey = "Calendar.bmp";
			this.BeginDateButton.ImageList = this.ButtonImages;
			this.BeginDateButton.Location = new System.Drawing.Point(230, 11);
			this.BeginDateButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.BeginDateButton.Name = "BeginDateButton";
			this.BeginDateButton.Size = new System.Drawing.Size(30, 30);
			this.BeginDateButton.TabIndex = 2;
			this.BeginDateButton.UseVisualStyleBackColor = true;
			this.BeginDateButton.Click += new System.EventHandler(this.BeginDateButton_Click);
			// 
			// ButtonImages
			// 
			this.ButtonImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ButtonImages.ImageStream")));
			this.ButtonImages.TransparentColor = System.Drawing.Color.Magenta;
			this.ButtonImages.Images.SetKeyName(0, "Ellipse.bmp");
			this.ButtonImages.Images.SetKeyName(1, "Calendar.bmp");
			// 
			// BeginDateMask
			// 
			this.BeginDateMask.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BeginDateMask.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
			this.BeginDateMask.LJCMaskValue = "00/00/0000";
			this.BeginDateMask.LJCText = "";
			this.BeginDateMask.LJCTextCheckString = "/  /";
			this.BeginDateMask.Location = new System.Drawing.Point(110, 11);
			this.BeginDateMask.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.BeginDateMask.Name = "BeginDateMask";
			this.BeginDateMask.Size = new System.Drawing.Size(112, 30);
			this.BeginDateMask.TabIndex = 1;
			// 
			// BeginDateLabel
			// 
			this.BeginDateLabel.Location = new System.Drawing.Point(12, 15);
			this.BeginDateLabel.Name = "BeginDateLabel";
			this.BeginDateLabel.Size = new System.Drawing.Size(96, 23);
			this.BeginDateLabel.TabIndex = 0;
			this.BeginDateLabel.Text = "Begin Date";
			// 
			// EndDateButton
			// 
			this.EndDateButton.ImageKey = "Calendar.bmp";
			this.EndDateButton.ImageList = this.ButtonImages;
			this.EndDateButton.Location = new System.Drawing.Point(230, 51);
			this.EndDateButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.EndDateButton.Name = "EndDateButton";
			this.EndDateButton.Size = new System.Drawing.Size(30, 30);
			this.EndDateButton.TabIndex = 5;
			this.EndDateButton.UseVisualStyleBackColor = true;
			this.EndDateButton.Click += new System.EventHandler(this.EndDateButton_Click);
			// 
			// EndDateMask
			// 
			this.EndDateMask.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.EndDateMask.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
			this.EndDateMask.LJCMaskValue = "00/00/0000";
			this.EndDateMask.LJCText = "";
			this.EndDateMask.LJCTextCheckString = "/  /";
			this.EndDateMask.Location = new System.Drawing.Point(110, 51);
			this.EndDateMask.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.EndDateMask.Name = "EndDateMask";
			this.EndDateMask.Size = new System.Drawing.Size(112, 30);
			this.EndDateMask.TabIndex = 4;
			// 
			// EndDateLabel
			// 
			this.EndDateLabel.Location = new System.Drawing.Point(12, 54);
			this.EndDateLabel.Name = "EndDateLabel";
			this.EndDateLabel.Size = new System.Drawing.Size(96, 23);
			this.EndDateLabel.TabIndex = 3;
			this.EndDateLabel.Text = "End Date";
			// 
			// MiddleNameTextBox
			// 
			this.MiddleNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MiddleNameTextBox.Location = new System.Drawing.Point(110, 131);
			this.MiddleNameTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MiddleNameTextBox.Name = "MiddleNameTextBox";
			this.MiddleNameTextBox.Size = new System.Drawing.Size(179, 30);
			this.MiddleNameTextBox.TabIndex = 9;
			// 
			// MiddleNameLabel
			// 
			this.MiddleNameLabel.Location = new System.Drawing.Point(12, 135);
			this.MiddleNameLabel.Name = "MiddleNameLabel";
			this.MiddleNameLabel.Size = new System.Drawing.Size(96, 23);
			this.MiddleNameLabel.TabIndex = 8;
			this.MiddleNameLabel.Text = "Middle Name";
			// 
			// LastNameTextBox
			// 
			this.LastNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.LastNameTextBox.Location = new System.Drawing.Point(110, 91);
			this.LastNameTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.LastNameTextBox.Name = "LastNameTextBox";
			this.LastNameTextBox.Size = new System.Drawing.Size(179, 30);
			this.LastNameTextBox.TabIndex = 7;
			// 
			// LastNameLabel
			// 
			this.LastNameLabel.Location = new System.Drawing.Point(12, 94);
			this.LastNameLabel.Name = "LastNameLabel";
			this.LastNameLabel.Size = new System.Drawing.Size(96, 23);
			this.LastNameLabel.TabIndex = 6;
			this.LastNameLabel.Text = "Last Name";
			// 
			// FirstNameTextBox
			// 
			this.FirstNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FirstNameTextBox.Location = new System.Drawing.Point(110, 171);
			this.FirstNameTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.FirstNameTextBox.Name = "FirstNameTextBox";
			this.FirstNameTextBox.Size = new System.Drawing.Size(179, 30);
			this.FirstNameTextBox.TabIndex = 11;
			// 
			// FirstNameLabel
			// 
			this.FirstNameLabel.Location = new System.Drawing.Point(12, 175);
			this.FirstNameLabel.Name = "FirstNameLabel";
			this.FirstNameLabel.Size = new System.Drawing.Size(96, 23);
			this.FirstNameLabel.TabIndex = 10;
			this.FirstNameLabel.Text = "First Name";
			// 
			// FormCancelButton
			// 
			this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.FormCancelButton.Location = new System.Drawing.Point(178, 212);
			this.FormCancelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.FormCancelButton.Name = "FormCancelButton";
			this.FormCancelButton.Size = new System.Drawing.Size(112, 35);
			this.FormCancelButton.TabIndex = 13;
			this.FormCancelButton.Text = "Cancel";
			this.FormCancelButton.UseVisualStyleBackColor = true;
			this.FormCancelButton.Click += new System.EventHandler(this.FormCancelButton_Click);
			// 
			// OKButton
			// 
			this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OKButton.Location = new System.Drawing.Point(58, 212);
			this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(112, 35);
			this.OKButton.TabIndex = 12;
			this.OKButton.Text = "&OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// DialogMenu
			// 
			this.DialogMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.DialogMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DialogMenuHelp});
			this.DialogMenu.Name = "DialogMenu";
			this.DialogMenu.Size = new System.Drawing.Size(122, 34);
			// 
			// DialogMenuHelp
			// 
			this.DialogMenuHelp.Name = "DialogMenuHelp";
			this.DialogMenuHelp.Size = new System.Drawing.Size(121, 30);
			this.DialogMenuHelp.Text = "&Help";
			this.DialogMenuHelp.Click += new System.EventHandler(this.DialogMenuHelp_Click);
			// 
			// VisitFilterDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(300, 256);
			this.ContextMenuStrip = this.DialogMenu;
			this.Controls.Add(this.FormCancelButton);
			this.Controls.Add(this.OKButton);
			this.Controls.Add(this.FirstNameTextBox);
			this.Controls.Add(this.FirstNameLabel);
			this.Controls.Add(this.MiddleNameTextBox);
			this.Controls.Add(this.MiddleNameLabel);
			this.Controls.Add(this.LastNameTextBox);
			this.Controls.Add(this.LastNameLabel);
			this.Controls.Add(this.EndDateButton);
			this.Controls.Add(this.EndDateMask);
			this.Controls.Add(this.EndDateLabel);
			this.Controls.Add(this.BeginDateButton);
			this.Controls.Add(this.BeginDateMask);
			this.Controls.Add(this.BeginDateLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "VisitFilterDetail";
			this.ShowInTaskbar = false;
			this.Text = "Filter Detail";
			this.Load += new System.EventHandler(this.FilterDetail_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FilterDetail_KeyDown);
			this.DialogMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button BeginDateButton;
		private LJCMaskBox BeginDateMask;
		private System.Windows.Forms.Label BeginDateLabel;
		private System.Windows.Forms.Button EndDateButton;
		private LJCMaskBox EndDateMask;
		private System.Windows.Forms.Label EndDateLabel;
		private System.Windows.Forms.TextBox MiddleNameTextBox;
		private System.Windows.Forms.Label MiddleNameLabel;
		private System.Windows.Forms.TextBox LastNameTextBox;
		private System.Windows.Forms.Label LastNameLabel;
		private System.Windows.Forms.ImageList ButtonImages;
		private System.Windows.Forms.TextBox FirstNameTextBox;
		private System.Windows.Forms.Label FirstNameLabel;
		private System.Windows.Forms.Button FormCancelButton;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.ContextMenuStrip DialogMenu;
		private System.Windows.Forms.ToolStripMenuItem DialogMenuHelp;
	}
}