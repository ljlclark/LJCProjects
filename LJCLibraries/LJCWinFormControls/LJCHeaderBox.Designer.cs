namespace LJCWinFormControls
{
	partial class LJCHeaderBox
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

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LJCHeaderBox));
			this.CloseImageList = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// CloseImageList
			// 
			this.CloseImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("CloseImageList.ImageStream")));
			this.CloseImageList.TransparentColor = System.Drawing.Color.Magenta;
			this.CloseImageList.Images.SetKeyName(0, "Close.bmp");
			this.CloseImageList.Images.SetKeyName(1, "CloseHover.bmp");
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ImageList CloseImageList;
	}
}
