// Copyright (c) Lester J. Clark 2017-2020 - All Rights Reserved
using System;
using System.Windows.Forms;

namespace DataHelper
{
	/// <summary>Provides Change Processing functionality.</summary>
	public class ChangeTimer
	{
		#region Methods

		/// <summary>
		/// Starts the change processing.
		/// </summary>
		/// <param name="changeName">The ChangeName value.</param>
		public void StartChange(string changeName)
		{
			mChangeTimer = new Timer()
			{
				Interval = 100
			};
			mChangeTimer.Tick += ChangeTimer_Tick;
			DoChange(changeName);
		}

		/// <summary>
		/// Uses a timer to allow the list to process outstanding messages.
		/// </summary>
		/// <param name="changeName">The ChangeName value.</param>
		public void DoChange(string changeName)
		{
			ChangeName = changeName;
			mChangeTimer.Start();
		}
		#endregion

		#region Control Event Handlers

		/// <summary>
		/// Fires the ItemChange event.
		/// </summary>
		protected void OnItemChange()
		{
			ItemChange?.Invoke(this, new EventArgs());
		}

		// The Timer event handler.
		private void ChangeTimer_Tick(object sender, EventArgs e)
		{
			mChangeTimer.Stop();
			OnItemChange();
		}
		#endregion

		#region Properties

		/// <summary>Gets or sets the ChangeName value.</summary>
		public string ChangeName { get; set; }
		#endregion

		#region Class Data

		private Timer mChangeTimer;

		/// <summary>The Item Change event.</summary>
		public event EventHandler<EventArgs> ItemChange;
		#endregion
	}
}
