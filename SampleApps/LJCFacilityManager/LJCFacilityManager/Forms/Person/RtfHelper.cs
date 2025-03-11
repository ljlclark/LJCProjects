// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// RtfHelper.cs
using System;
using System.Windows.Forms;
using System.Drawing;

namespace LJCFacilityManager
{
	/// <summary>Contains RichTextBox helper methods.</summary>
	public class RtfHelper
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/RtfHelperC/*' file='Doc/RtfHelper.xml'/>
		public RtfHelper(RichTextBox rtfTextBox)
		{
			mRtfTextBox = rtfTextBox;
		}
		#endregion

		#region Methods

		// Appends text to the RTF control.
		/// <include path='items/Append1/*' file='Doc/RtfHelper.xml'/>
		public void Append(string text)
		{
			if (mRtfTextBox != null
				&& text != null)
			{
				mRtfTextBox.AppendText(text);
			}
		}

		// Appends text to the RTF control with the specified font.
		/// <include path='items/Append2/*' file='Doc/RtfHelper.xml'/>
		public void Append(string text, FontStyle fontStyle)
		{
			mRtfTextBox.SelectionFont = SetFontStyle(fontStyle);
			Append(text);
			mRtfTextBox.SelectionFont = SetFontStyle(FontStyle.Regular);
		}

		// Appends text to the RTF control with the specified Font and color.
		/// <include path='items/Append3/*' file='Doc/RtfHelper.xml'/>
		public void Append(string text, FontStyle fontStyle, Color color)
		{
			Color saveColor = Color.White;

			saveColor = mRtfTextBox.SelectionColor;
			mRtfTextBox.SelectionColor = color;

			mRtfTextBox.SelectionFont = SetFontStyle(fontStyle);
			Append(text);
			mRtfTextBox.SelectionFont = SetFontStyle(FontStyle.Regular);

			mRtfTextBox.SelectionColor = saveColor;
		}

		/// <summary>Appends a blank line to the RTF control.</summary>
		public void AppendLine()
		{
			Append("\r\n");
		}

		// Appends a text line to the RTF control.
		/// <include path='items/AppendLine1/*' file='Doc/RtfHelper.xml'/>
		public void AppendLine(string text)
		{
			Append(text + "\r\n");
		}

		// Appends a text line to the RTF control with the specified font.
		/// <include path='items/AppendLine2/*' file='Doc/RtfHelper.xml'/>
		public void AppendLine(string text, FontStyle fontStyle)
		{
			Append(text + "\r\n", fontStyle);
		}

		// Appends a text line to the RTF control with the specified Font and color.
		/// <include path='items/AppendLine3/*' file='Doc/RtfHelper.xml'/>
		public void AppendLine(string text, FontStyle fontStyle, Color color)
		{
			Append(text + "\r\n", fontStyle, color);
		}

		// Set the Font style.
		/// <include path='items/SetFontStyle/*' file='Doc/RtfHelper.xml'/>
		private Font SetFontStyle(FontStyle style)
		{
			return new Font(mRtfTextBox.Font.FontFamily, mRtfTextBox.Font.Size, style);
		}
		#endregion

		#region Member Data

		private RichTextBox mRtfTextBox;
		#endregion
	}
}
