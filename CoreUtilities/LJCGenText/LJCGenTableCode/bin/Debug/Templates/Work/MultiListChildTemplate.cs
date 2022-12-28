// Copyright (c) Lester J. Clark 2018-2021 - All Rights Reserved
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LJC.WinForm.Controls;

namespace LJC.FacilityManager
{
	/// <summary>
	/// The Parent list form.
	/// </summary>
	/// <remarks>
	/// 
	/// <para>-- Library Level Remarks</para>
	/// </remarks>
	public partial class ParentList : Form
	{
		/// <summary>
		/// Initializes an object instance.
		/// </summary>
		/// <remarks><para>Syntax: public ParentList()</para></remarks>
		public ParentList()
		{
			InitializeComponent();
		}

		#region Setup Methods

		/// <summary>
		/// Configures the controls and loads the selection control data.
		/// </summary>
		private void InitializeControls()
		{
			// Configure controls.

			// Setup panel manager for child tab splitter.
			mLJCPanelManager = new LJCPanelManager(TabSplit, ChildTabs, ChildTileTabs);
		}
		private LJCPanelManager mLJCPanelManager;
		#endregion
	}
}
