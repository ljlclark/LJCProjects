// Copyright (c) Lester J. Clark 2020 - All Rights Reserved
using System;
using System.Windows.Forms;
using LJCNetCommon;

namespace CVRManager
{
	/// <summary>The custom MaskTextBox.</summary>
	public partial class LJCMaskBox : MaskedTextBox
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
		public LJCMaskBox()
		{
			InitializeComponent();
			LJCMaskValue = LJCDateMaskValue;
		}
		#endregion

		#region Override Event Methods

		/// <summary>
		/// Handles the Enter event to set the Mask.
		/// </summary>
		/// <param name="e">The event arguments.</param>
		protected override void OnEnter(EventArgs e)
		{
			base.OnEnter(e);

			if (false == NetString.HasValue(Mask))
			{
				Mask = LJCMaskValue;
			}
		}

		/// <summary>
		/// Handles the Leave event to clear the Mask if empty.
		/// </summary>
		/// <param name="e">The event arguments.</param>
		protected override void OnLeave(EventArgs e)
		{
			base.OnLeave(e);

			if (LJCIsEmpty())
			{
				Mask = "";
			}
		}

		/// <summary>
		/// Handles the KeyPress event.
		/// </summary>
		/// <param name="e">The event arguments.</param>
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			//base.OnKeyPress(e);

			if (LJCTimeMaskValue == LJCMaskValue)
			{
				if (false == IsAMPM(ref e))
				{
					e.Handled = true;
					e.KeyChar = 'A';
				}
			}
			base.OnKeyPress(e);
		}
		#endregion

		#region Methods

		/// <summary>Checks if the control Text matches TextCheckString.</summary>
		public bool LJCIsEmpty()
		{
			bool retValue = false;

			if (LJCTextCheckString == Text.Trim()
				|| false == NetString.HasValue(Text))
			{
				retValue = true;
			}
			return retValue;
		}

		// Checks Mask "00:00 >LM" for AM or PM.
		private bool IsAMPM(ref KeyPressEventArgs e)
		{
			bool retValue = true;

			if (SelectionStart >= 5
				&& SelectionStart <= 7)
			{
				if (e.KeyChar != 'P'
					&& e.KeyChar != 'p')
				{
					retValue = false;
				}
			}
			return retValue;
		}
		#endregion

		#region Properties

		/// <summary>Gets or sets the Mask and Text value.</summary>
		public string LJCText
		{
			get { return Text; }
			set
			{
				if (false == NetString.HasValue(value))
				{
					Mask = "";
				}
				else
				{
					Mask = LJCMaskValue;
				}
				Text = value;
			}
		}

		/// <summary>Gets or sets the MaskValue.</summary>
		public string LJCMaskValue
		{
			get { return mMaskValue; }
			set
			{
				mMaskValue = value;
				switch (value)
				{
					case LJCDateMaskValue:
						LJCTextCheckString = LJCDateCheckString;
						break;
					case LJCTimeMaskValue:
						LJCTextCheckString = LJCTimeCheckString;
						break;
					default:
						LJCTextCheckString = null;
						break;
				}
			}
		}
		private string mMaskValue;

		/// <summary>Gets or sets the TextCheckString value.</summary>
		public string LJCTextCheckString { get; set; }
		#endregion

		#region Class Data

		/// <summary>The Date Mask Value.</summary>
		public const string LJCDateMaskValue = "00/00/0000";

		/// <summary>The Time Mask Value.</summary>
		public const string LJCTimeMaskValue = "00:00 >LM";

		/// <summary>The Date check string.</summary>
		public const string LJCDateCheckString = "/  /";

		/// <summary>The Time check string.</summary>
		public const string LJCTimeCheckString = ":    M";
		#endregion
	}
}
