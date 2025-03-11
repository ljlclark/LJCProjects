// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCPictureBox.cs
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using LJCNetCommon;
using LJCWinFormCommon;

namespace LJCAppManager
{
	// Provides additional functionality for a PictureBox control.
	/// <include path='items/LJCPictureBox/*' file='Doc/LJCPictureBox.xml'/>
	public class LJCPictureBox : PictureBox
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public LJCPictureBox()
		{
			DefaultImagePath = Path.Combine(Directory.GetCurrentDirectory(), "Images");
		}
		#endregion

		#region Overriden Events

		// Override for the MouseDown event.
		/// <include path='items/OnMouseDown/*' file='Doc/LJCPictureBox.xml'/>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			if (MouseButtons.Left == e.Button)
			{
				if (AllowCrop)
				{
					mIsSelecting = true;
					mSelection = new Rectangle(new Point(e.X, e.Y), new Size());
				}
			}
		}

		// Override for the MouseMove event.
		/// <include path='items/OnMouseMove/*' file='Doc/LJCPictureBox.xml'/>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			if (AllowCrop && mIsSelecting)
			{
				mSelection.Width = e.X - mSelection.X;
				mSelection.Height = e.Y - mSelection.Y;
				Refresh();
			}
		}

		// Override for the MouseUp event.
		/// <include path='items/OnMouseUp/*' file='Doc/LJCPictureBox.xml'/>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);

			if (AllowCrop && mIsSelecting)
			{
				Image selectionImage = Image;
				Rectangle cropRectangle = FormCommon.TransformCrop(mSelection, selectionImage
					, mOriginalImage);
				Image image = FormCommon.CropImage(mOriginalImage, cropRectangle.Location
					, cropRectangle.Size);
				mOriginalImage = image;
				image = FormCommon.ResizeImage(image, Size, true);
				Image = image;
				mIsSelecting = false;
				AllowCrop = false;
			}
		}

		// Override for the Paint event.
		/// <include path='items/OnPaint/*' file='Doc/LJCPictureBox.xml'/>
		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);

			if (mIsSelecting)
			{
				pe.Graphics.DrawRectangle(Pens.Black, mSelection);
			}
			else
			{
				if (Image != null)
				{
					if (Image.Height < mMaxSize.Height)
					{
						Height = Image.Height;
					}
					else
					{
						Height = mMaxSize.Height;
					}
					if (Image.Width < mMaxSize.Width)
					{
						Width = Image.Width;
					}
					else
					{
						Width = mMaxSize.Width;
					}
				}
			}
		}
		#endregion

		#region Methods

		// Releases the active resources.
		/// <include path='items/ReleaseResources/*' file='Doc/LJCPictureBox.xml'/>
		public void ReleaseResources()
		{
			mOriginalImage?.Dispose();
		}

		// Loads the specified image file.
		/// <include path='items/LoadFromFile/*' file='Doc/LJCPictureBox.xml'/>
		public void LoadFromFile(string imageFileName, string defaultImagePath = null)
		{
			if (null == defaultImagePath)
			{
				defaultImagePath = DefaultImagePath;
			}
			string fileSpec = Path.Combine(defaultImagePath, imageFileName);
			if (File.Exists(fileSpec))
			{
				using (Image image = Image.FromFile(fileSpec))
				{
					Load(image);
				}
			}
		}

		// Loads the specified image.
		/// <include path='items/Load/*' file='Doc/LJCPictureBox.xml'/>
		public void Load(Image image)
		{
			if (0 == mMaxSize.Width && 0 == mMaxSize.Height)
			{
				mMaxSize = Size;
			}

			// Reset standard picture size.
			Width = mMaxSize.Width;
			Height = mMaxSize.Height;

			using (image)
			{
				mOriginalImage = image.Clone() as Image;
			}
			Image = mOriginalImage;
			Image = FormCommon.ResizeImage(mOriginalImage, Size);
		}

		// Displays a file selection dialog and loads the selected image file.
		/// <include path='items/SelectImageFile/*' file='Doc/LJCPictureBox.xml'/>
		public void SelectImageFile(string defaultImagePath = null)
		{
			string filter;
			string fileSpec;

			if (null == defaultImagePath)
			{
				defaultImagePath = DefaultImagePath;
			}

			filter = "*.jpg|*.jpg|*.png|*.png|*.bmp|*.bmp|All Files(*.*)|*.*";
			fileSpec = FormCommon.SelectFile(filter, defaultImagePath);
			if (NetString.HasValue(fileSpec))
			{
				using (Image image = Image.FromFile(fileSpec))
				{
					Load(image);
				}
			}
		}

		// Saves the image to a file.
		/// <include path='items/SaveImageFile/*' file='Doc/LJCPictureBox.xml'/>
		public void SaveImageFile(string imageFileName, string defaultImagePath = null)
		{
			string filter;
			string defaultFileSpec;
			string fileSpec;

			if (null == defaultImagePath)
			{
				defaultImagePath = DefaultImagePath;
			}

			filter = "*.jpg|*.jpg|*.png|*.png|*.bmp|*.bmp|All Files(*.*)|*.*";
			defaultFileSpec = Path.Combine(defaultImagePath, imageFileName);
			fileSpec = FormCommon.SaveFile(filter, null, defaultFileSpec);
			if (NetString.HasValue(fileSpec))
			{
				Image.Save(fileSpec);
			}
		}

		// Rotates the image 90 degrees left.
		/// <include path='items/RotateLeft/*' file='Doc/LJCPictureBox.xml'/>
		public void RotateLeft()
		{
			Image = mOriginalImage;
			Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
			Image = FormCommon.ResizeImage(Image, mMaxSize);
			Refresh();
		}

		// Rotates the image 90 degrees right.
		/// <include path='items/RotateRight/*' file='Doc/LJCPictureBox.xml'/>
		public void RotateRight()
		{
			Image = mOriginalImage;
			Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
			Image = FormCommon.ResizeImage(Image, mMaxSize);
			Refresh();
		}
		#endregion

		#region Properties

		/// <summary>The default image path.</summary>
		public string DefaultImagePath
		{
			get
			{
				return mDefaultImagePath;
			}
			set
			{
				mDefaultImagePath = NetString.InitString(value);
			}
		}
		private string mDefaultImagePath;

		/// <summary>Indicates that a crop action is allowed.</summary>
		public bool AllowCrop { get; set; }
		#endregion

		#region Class Data

		// Image resize values.
		private Image mOriginalImage;
		private Size mMaxSize;

		// Image crop values.
		private bool mIsSelecting;
		private Rectangle mSelection;
		#endregion
	}
}
