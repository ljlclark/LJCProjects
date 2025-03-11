// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// SelectMonthDetail.cs
using System;
using System.Drawing;
using System.Windows.Forms;
using LJCWinFormCommon;

namespace LJCFacilityManager
{
	/// <summary>The month selection detail dialog.</summary>
	public partial class SelectMonthDetail : Form
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
		public SelectMonthDetail()
		{
			InitializeComponent();
		}
		#endregion

		#region Form Event Handlers

		// Configures the form and loads the initial control data.
		private void SelectMonthDetail_Load(object sender, EventArgs e)
		{
			AcceptButton = OKButton;
			CancelButton = FormCancelButton;

			InitializeControls();

			CenterToParent();
		}

		// Paint the form background.
		/// <include path='items/OnPaintBackground/*' file='../../LJCDocLib/Common/Detail.xml'/>
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			Color beginColor = Color.AliceBlue;
			Color endColor = Color.LightSkyBlue;

			base.OnPaintBackground(e);

			FormCommon.CreateGradient(e.Graphics, ClientRectangle, beginColor, endColor);
		}
		#endregion

		#region Setup Methods

		// Configures the controls and loads the selection control data.
		/// <include path='items/InitializeControls/*' file='../../LJCDocLib/Common/Detail.xml'/>
		private void InitializeControls()
		{
			int month;
		
			// Initialize Class Data.

			// Load control data.
			mMonthCombo.Items.Add("Entire Year");
			mMonthCombo.Items.Add("January");
			mMonthCombo.Items.Add("February");
			mMonthCombo.Items.Add("March");
			mMonthCombo.Items.Add("April");
			mMonthCombo.Items.Add("May");
			mMonthCombo.Items.Add("June");
			mMonthCombo.Items.Add("July");
			mMonthCombo.Items.Add("August");
			mMonthCombo.Items.Add("September");
			mMonthCombo.Items.Add("October");
			mMonthCombo.Items.Add("November");
			mMonthCombo.Items.Add("December");

			// Get next month.
			month = DateTime.Now.Month + 1;
			if (month == 13)
			{
				month = 1;
			}

			// Adjust to index value.
			mMonthCombo.SelectedIndex = month;
		}
		#endregion

		#region Control Event Handlers

		// Saves the data and closes the form.
		private void OKButton_Click(object sender, EventArgs e)
		{
			mMonthNumber = mMonthCombo.SelectedIndex;
			DialogResult = DialogResult.OK;
		}

		// Closes the form without saving the data.
		private void FormCancelButton_Click(object sender, EventArgs e)
		{
			Close();
		}
		#endregion

		#region Properties

		/// <summary>Gets or sets the MonthNumber value.</summary>
		public int MonthNumber
		{
			get
			{
				return mMonthNumber;
			}
			set
			{
				mMonthNumber = value;
			}
		}
		#endregion

		#region Member Data

		// Property values.
		private int mMonthNumber;
		#endregion
	}
}
