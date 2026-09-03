namespace LJCControls5
{
	/// <summary>
	/// 
	/// </summary>
	partial class InfoWindow
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
      InfoRTBox = new RichTextBox();
      OKButton = new Button();
      ExecuteButton = new Button();
      SuspendLayout();
      // 
      // InfoRTBox
      // 
      InfoRTBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      InfoRTBox.Location = new Point(0, 0);
      InfoRTBox.Margin = new Padding(3, 4, 3, 4);
      InfoRTBox.Name = "InfoRTBox";
      InfoRTBox.Size = new Size(753, 485);
      InfoRTBox.TabIndex = 0;
      InfoRTBox.Text = "";
      // 
      // OKButton
      // 
      OKButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      OKButton.Location = new Point(614, 496);
      OKButton.Margin = new Padding(4, 6, 4, 6);
      OKButton.Name = "OKButton";
      OKButton.Size = new Size(124, 44);
      OKButton.TabIndex = 5;
      OKButton.Text = "Close";
      OKButton.UseVisualStyleBackColor = true;
      OKButton.Click += OKButton_Click;
      // 
      // ExecuteButton
      // 
      ExecuteButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      ExecuteButton.Location = new Point(481, 496);
      ExecuteButton.Margin = new Padding(4, 6, 4, 6);
      ExecuteButton.Name = "ExecuteButton";
      ExecuteButton.Size = new Size(124, 44);
      ExecuteButton.TabIndex = 6;
      ExecuteButton.Text = "&Execute";
      ExecuteButton.UseVisualStyleBackColor = true;
      ExecuteButton.Click += ExecuteButton_Click;
      // 
      // InfoWindow
      // 
      AutoScaleDimensions = new SizeF(10F, 25F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(753, 555);
      Controls.Add(ExecuteButton);
      Controls.Add(OKButton);
      Controls.Add(InfoRTBox);
      Margin = new Padding(3, 4, 3, 4);
      Name = "InfoWindow";
      Text = "Info Window";
      Load += InfoWindow_Load;
      ResumeLayout(false);

    }

    #endregion

    internal System.Windows.Forms.Button ExecuteButton;
    private System.Windows.Forms.RichTextBox InfoRTBox;
		private System.Windows.Forms.Button OKButton;
  }
}