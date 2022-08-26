namespace CVRManager
{
	partial class CVVisitDetail
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CVVisitDetail));
			this.ContactNameText = new System.Windows.Forms.TextBox();
			this.ContactNameLabel = new System.Windows.Forms.Label();
			this.ButtonImages = new System.Windows.Forms.ImageList(this.components);
			this.EnterDateMask = new CVRManager.LJCMaskBox();
			this.EnterTimeLabel = new System.Windows.Forms.Label();
			this.RegisterTimeLabel = new System.Windows.Forms.Label();
			this.FormCancelButton = new System.Windows.Forms.Button();
			this.OKButton = new System.Windows.Forms.Button();
			this.ContactButton = new System.Windows.Forms.Button();
			this.ExitDateMask = new CVRManager.LJCMaskBox();
			this.ExitTimeLabel = new System.Windows.Forms.Label();
			this.RegisterDateMask = new CVRManager.LJCMaskBox();
			this.RegisterTimeMask = new CVRManager.LJCMaskBox();
			this.EnterTimeMask = new CVRManager.LJCMaskBox();
			this.ExitTimeMask = new CVRManager.LJCMaskBox();
			this.RegisterDateButton = new System.Windows.Forms.Button();
			this.EnterDateButton = new System.Windows.Forms.Button();
			this.ExitDateButton = new System.Windows.Forms.Button();
			this.ParentNameText = new System.Windows.Forms.TextBox();
			this.ParentNameLabel = new System.Windows.Forms.Label();
			this.DialogMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.DialogMenuHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.TemperatureLabel = new System.Windows.Forms.Label();
			this.TemperatureText = new System.Windows.Forms.TextBox();
			this.TemperatureCombo = new LJCWinFormControls.LJCItemCombo();
			this.ContactIndexText = new System.Windows.Forms.TextBox();
			this.ContactIndexLabel = new System.Windows.Forms.Label();
			this.ContactIndexButton = new System.Windows.Forms.Button();
			this.DialogMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// ContactNameText
			// 
			this.ContactNameText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ContactNameText.Location = new System.Drawing.Point(149, 54);
			this.ContactNameText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ContactNameText.Name = "ContactNameText";
			this.ContactNameText.ReadOnly = true;
			this.ContactNameText.Size = new System.Drawing.Size(397, 30);
			this.ContactNameText.TabIndex = 3;
			this.ContactNameText.TabStop = false;
			// 
			// ContactNameLabel
			// 
			this.ContactNameLabel.Location = new System.Drawing.Point(15, 61);
			this.ContactNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ContactNameLabel.Name = "ContactNameLabel";
			this.ContactNameLabel.Size = new System.Drawing.Size(131, 20);
			this.ContactNameLabel.TabIndex = 2;
			this.ContactNameLabel.Text = "Contact Name";
			// 
			// ButtonImages
			// 
			this.ButtonImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ButtonImages.ImageStream")));
			this.ButtonImages.TransparentColor = System.Drawing.Color.Magenta;
			this.ButtonImages.Images.SetKeyName(0, "Ellipse.bmp");
			this.ButtonImages.Images.SetKeyName(1, "Calendar.bmp");
			// 
			// EnterDateMask
			// 
			this.EnterDateMask.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.EnterDateMask.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
			this.EnterDateMask.LJCMaskValue = "00/00/0000";
			this.EnterDateMask.LJCText = "";
			this.EnterDateMask.LJCTextCheckString = "/  /";
			this.EnterDateMask.Location = new System.Drawing.Point(149, 174);
			this.EnterDateMask.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.EnterDateMask.Name = "EnterDateMask";
			this.EnterDateMask.Size = new System.Drawing.Size(112, 30);
			this.EnterDateMask.TabIndex = 13;
			// 
			// EnterTimeLabel
			// 
			this.EnterTimeLabel.Location = new System.Drawing.Point(15, 181);
			this.EnterTimeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.EnterTimeLabel.Name = "EnterTimeLabel";
			this.EnterTimeLabel.Size = new System.Drawing.Size(131, 20);
			this.EnterTimeLabel.TabIndex = 12;
			this.EnterTimeLabel.Text = "Enter Time";
			// 
			// RegisterTimeLabel
			// 
			this.RegisterTimeLabel.Location = new System.Drawing.Point(15, 141);
			this.RegisterTimeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.RegisterTimeLabel.Name = "RegisterTimeLabel";
			this.RegisterTimeLabel.Size = new System.Drawing.Size(131, 20);
			this.RegisterTimeLabel.TabIndex = 8;
			this.RegisterTimeLabel.Text = "Register Time";
			// 
			// FormCancelButton
			// 
			this.FormCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.FormCancelButton.Location = new System.Drawing.Point(470, 290);
			this.FormCancelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.FormCancelButton.Name = "FormCancelButton";
			this.FormCancelButton.Size = new System.Drawing.Size(112, 35);
			this.FormCancelButton.TabIndex = 24;
			this.FormCancelButton.Text = "Cancel";
			this.FormCancelButton.UseVisualStyleBackColor = true;
			// 
			// OKButton
			// 
			this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OKButton.Location = new System.Drawing.Point(350, 290);
			this.OKButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(112, 35);
			this.OKButton.TabIndex = 23;
			this.OKButton.Text = "&OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// ContactButton
			// 
			this.ContactButton.ImageKey = "Ellipse.bmp";
			this.ContactButton.ImageList = this.ButtonImages;
			this.ContactButton.Location = new System.Drawing.Point(554, 54);
			this.ContactButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ContactButton.Name = "ContactButton";
			this.ContactButton.Size = new System.Drawing.Size(30, 30);
			this.ContactButton.TabIndex = 4;
			this.ContactButton.UseVisualStyleBackColor = true;
			this.ContactButton.Click += new System.EventHandler(this.ContactButton_Click);
			// 
			// ExitDateMask
			// 
			this.ExitDateMask.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ExitDateMask.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
			this.ExitDateMask.LJCMaskValue = "00/00/0000";
			this.ExitDateMask.LJCText = "";
			this.ExitDateMask.LJCTextCheckString = "/  /";
			this.ExitDateMask.Location = new System.Drawing.Point(149, 212);
			this.ExitDateMask.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ExitDateMask.Name = "ExitDateMask";
			this.ExitDateMask.Size = new System.Drawing.Size(112, 30);
			this.ExitDateMask.TabIndex = 17;
			// 
			// ExitTimeLabel
			// 
			this.ExitTimeLabel.Location = new System.Drawing.Point(15, 219);
			this.ExitTimeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ExitTimeLabel.Name = "ExitTimeLabel";
			this.ExitTimeLabel.Size = new System.Drawing.Size(131, 20);
			this.ExitTimeLabel.TabIndex = 16;
			this.ExitTimeLabel.Text = "Exit Time";
			// 
			// RegisterDateMask
			// 
			this.RegisterDateMask.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.RegisterDateMask.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
			this.RegisterDateMask.LJCMaskValue = "00/00/0000";
			this.RegisterDateMask.LJCText = "";
			this.RegisterDateMask.LJCTextCheckString = "/  /";
			this.RegisterDateMask.Location = new System.Drawing.Point(149, 134);
			this.RegisterDateMask.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.RegisterDateMask.Name = "RegisterDateMask";
			this.RegisterDateMask.Size = new System.Drawing.Size(112, 30);
			this.RegisterDateMask.TabIndex = 9;
			// 
			// RegisterTimeMask
			// 
			this.RegisterTimeMask.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.RegisterTimeMask.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
			this.RegisterTimeMask.LJCMaskValue = "00/00/0000";
			this.RegisterTimeMask.LJCText = "";
			this.RegisterTimeMask.LJCTextCheckString = "/  /";
			this.RegisterTimeMask.Location = new System.Drawing.Point(316, 134);
			this.RegisterTimeMask.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.RegisterTimeMask.Name = "RegisterTimeMask";
			this.RegisterTimeMask.Size = new System.Drawing.Size(93, 30);
			this.RegisterTimeMask.TabIndex = 11;
			// 
			// EnterTimeMask
			// 
			this.EnterTimeMask.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.EnterTimeMask.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
			this.EnterTimeMask.LJCMaskValue = "00/00/0000";
			this.EnterTimeMask.LJCText = "";
			this.EnterTimeMask.LJCTextCheckString = "/  /";
			this.EnterTimeMask.Location = new System.Drawing.Point(316, 174);
			this.EnterTimeMask.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.EnterTimeMask.Name = "EnterTimeMask";
			this.EnterTimeMask.Size = new System.Drawing.Size(93, 30);
			this.EnterTimeMask.TabIndex = 15;
			// 
			// ExitTimeMask
			// 
			this.ExitTimeMask.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ExitTimeMask.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
			this.ExitTimeMask.LJCMaskValue = "00/00/0000";
			this.ExitTimeMask.LJCText = "";
			this.ExitTimeMask.LJCTextCheckString = "/  /";
			this.ExitTimeMask.Location = new System.Drawing.Point(316, 212);
			this.ExitTimeMask.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ExitTimeMask.Name = "ExitTimeMask";
			this.ExitTimeMask.Size = new System.Drawing.Size(93, 30);
			this.ExitTimeMask.TabIndex = 19;
			// 
			// RegisterDateButton
			// 
			this.RegisterDateButton.ImageKey = "Calendar.bmp";
			this.RegisterDateButton.ImageList = this.ButtonImages;
			this.RegisterDateButton.Location = new System.Drawing.Point(269, 134);
			this.RegisterDateButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.RegisterDateButton.Name = "RegisterDateButton";
			this.RegisterDateButton.Size = new System.Drawing.Size(30, 30);
			this.RegisterDateButton.TabIndex = 10;
			this.RegisterDateButton.UseVisualStyleBackColor = true;
			this.RegisterDateButton.Click += new System.EventHandler(this.RegisterDateButton_Click);
			// 
			// EnterDateButton
			// 
			this.EnterDateButton.ImageKey = "Calendar.bmp";
			this.EnterDateButton.ImageList = this.ButtonImages;
			this.EnterDateButton.Location = new System.Drawing.Point(269, 174);
			this.EnterDateButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.EnterDateButton.Name = "EnterDateButton";
			this.EnterDateButton.Size = new System.Drawing.Size(30, 30);
			this.EnterDateButton.TabIndex = 14;
			this.EnterDateButton.UseVisualStyleBackColor = true;
			this.EnterDateButton.Click += new System.EventHandler(this.EnterDateButton_Click);
			// 
			// ExitDateButton
			// 
			this.ExitDateButton.ImageKey = "Calendar.bmp";
			this.ExitDateButton.ImageList = this.ButtonImages;
			this.ExitDateButton.Location = new System.Drawing.Point(269, 212);
			this.ExitDateButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ExitDateButton.Name = "ExitDateButton";
			this.ExitDateButton.Size = new System.Drawing.Size(30, 30);
			this.ExitDateButton.TabIndex = 18;
			this.ExitDateButton.UseVisualStyleBackColor = true;
			this.ExitDateButton.Click += new System.EventHandler(this.ExitDateButton_Click);
			// 
			// ParentNameText
			// 
			this.ParentNameText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ParentNameText.Location = new System.Drawing.Point(149, 14);
			this.ParentNameText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ParentNameText.Name = "ParentNameText";
			this.ParentNameText.ReadOnly = true;
			this.ParentNameText.Size = new System.Drawing.Size(397, 30);
			this.ParentNameText.TabIndex = 1;
			this.ParentNameText.TabStop = false;
			// 
			// ParentNameLabel
			// 
			this.ParentNameLabel.Location = new System.Drawing.Point(15, 21);
			this.ParentNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ParentNameLabel.Name = "ParentNameLabel";
			this.ParentNameLabel.Size = new System.Drawing.Size(131, 20);
			this.ParentNameLabel.TabIndex = 0;
			this.ParentNameLabel.Text = "Facility Name";
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
			// TemperatureLabel
			// 
			this.TemperatureLabel.Location = new System.Drawing.Point(15, 257);
			this.TemperatureLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.TemperatureLabel.Name = "TemperatureLabel";
			this.TemperatureLabel.Size = new System.Drawing.Size(131, 20);
			this.TemperatureLabel.TabIndex = 20;
			this.TemperatureLabel.Text = "Temperature";
			// 
			// TemperatureText
			// 
			this.TemperatureText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TemperatureText.Location = new System.Drawing.Point(149, 250);
			this.TemperatureText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.TemperatureText.Name = "TemperatureText";
			this.TemperatureText.Size = new System.Drawing.Size(60, 30);
			this.TemperatureText.TabIndex = 21;
			this.TemperatureText.TabStop = false;
			this.TemperatureText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TemperatureText_KeyPress);
			this.TemperatureText.Leave += new System.EventHandler(this.TemperatureText_Leave);
			// 
			// TemperatureCombo
			// 
			this.TemperatureCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.TemperatureCombo.FormattingEnabled = true;
			this.TemperatureCombo.ItemHeight = 20;
			this.TemperatureCombo.Location = new System.Drawing.Point(217, 251);
			this.TemperatureCombo.Name = "TemperatureCombo";
			this.TemperatureCombo.Size = new System.Drawing.Size(139, 28);
			this.TemperatureCombo.TabIndex = 22;
			this.TemperatureCombo.SelectedIndexChanged += new System.EventHandler(this.TemperatureCombo_SelectedIndexChanged);
			// 
			// ContactIndexText
			// 
			this.ContactIndexText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ContactIndexText.Location = new System.Drawing.Point(149, 94);
			this.ContactIndexText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ContactIndexText.Name = "ContactIndexText";
			this.ContactIndexText.Size = new System.Drawing.Size(60, 30);
			this.ContactIndexText.TabIndex = 6;
			this.ContactIndexText.TabStop = false;
			// 
			// ContactIndexLabel
			// 
			this.ContactIndexLabel.Location = new System.Drawing.Point(15, 101);
			this.ContactIndexLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ContactIndexLabel.Name = "ContactIndexLabel";
			this.ContactIndexLabel.Size = new System.Drawing.Size(131, 20);
			this.ContactIndexLabel.TabIndex = 5;
			this.ContactIndexLabel.Text = "Contact Index";
			// 
			// ContactIndexButton
			// 
			this.ContactIndexButton.ImageKey = "Ellipse.bmp";
			this.ContactIndexButton.ImageList = this.ButtonImages;
			this.ContactIndexButton.Location = new System.Drawing.Point(217, 94);
			this.ContactIndexButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ContactIndexButton.Name = "ContactIndexButton";
			this.ContactIndexButton.Size = new System.Drawing.Size(30, 30);
			this.ContactIndexButton.TabIndex = 7;
			this.ContactIndexButton.UseVisualStyleBackColor = true;
			this.ContactIndexButton.Click += new System.EventHandler(this.ContactIndexButton_Click);
			// 
			// CVVisitDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(595, 335);
			this.ContextMenuStrip = this.DialogMenu;
			this.Controls.Add(this.ContactIndexButton);
			this.Controls.Add(this.ContactIndexText);
			this.Controls.Add(this.ContactIndexLabel);
			this.Controls.Add(this.TemperatureCombo);
			this.Controls.Add(this.TemperatureText);
			this.Controls.Add(this.TemperatureLabel);
			this.Controls.Add(this.ParentNameText);
			this.Controls.Add(this.ParentNameLabel);
			this.Controls.Add(this.ExitDateButton);
			this.Controls.Add(this.EnterDateButton);
			this.Controls.Add(this.RegisterDateButton);
			this.Controls.Add(this.ExitTimeMask);
			this.Controls.Add(this.EnterTimeMask);
			this.Controls.Add(this.RegisterTimeMask);
			this.Controls.Add(this.RegisterDateMask);
			this.Controls.Add(this.ExitDateMask);
			this.Controls.Add(this.ExitTimeLabel);
			this.Controls.Add(this.ContactButton);
			this.Controls.Add(this.ContactNameText);
			this.Controls.Add(this.ContactNameLabel);
			this.Controls.Add(this.EnterDateMask);
			this.Controls.Add(this.EnterTimeLabel);
			this.Controls.Add(this.RegisterTimeLabel);
			this.Controls.Add(this.FormCancelButton);
			this.Controls.Add(this.OKButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CVVisitDetail";
			this.ShowInTaskbar = false;
			this.Text = "Contact Visit Detail";
			this.Load += new System.EventHandler(this.CVVisitDetail_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CVVisitDetail_KeyDown);
			this.DialogMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.TextBox ContactNameText;
		private System.Windows.Forms.Label ContactNameLabel;
		private LJCMaskBox EnterDateMask;
		private System.Windows.Forms.Label EnterTimeLabel;
		private System.Windows.Forms.Label RegisterTimeLabel;
		private System.Windows.Forms.Button FormCancelButton;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.Button ContactButton;
		private LJCMaskBox ExitDateMask;
		private System.Windows.Forms.Label ExitTimeLabel;
		private System.Windows.Forms.ImageList ButtonImages;
		private LJCMaskBox RegisterDateMask;
		private LJCMaskBox RegisterTimeMask;
		private LJCMaskBox EnterTimeMask;
		private LJCMaskBox ExitTimeMask;
		private System.Windows.Forms.Button RegisterDateButton;
		private System.Windows.Forms.Button EnterDateButton;
		private System.Windows.Forms.Button ExitDateButton;
		private System.Windows.Forms.TextBox ParentNameText;
		private System.Windows.Forms.Label ParentNameLabel;
		private System.Windows.Forms.ContextMenuStrip DialogMenu;
		private System.Windows.Forms.ToolStripMenuItem DialogMenuHelp;
		private System.Windows.Forms.Label TemperatureLabel;
		private System.Windows.Forms.TextBox TemperatureText;
		private LJCWinFormControls.LJCItemCombo TemperatureCombo;
		private System.Windows.Forms.TextBox ContactIndexText;
		private System.Windows.Forms.Label ContactIndexLabel;
		private System.Windows.Forms.Button ContactIndexButton;
	}
}