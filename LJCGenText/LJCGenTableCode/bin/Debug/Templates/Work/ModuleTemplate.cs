// Copyright (c) Lester J. Clark 2018-2021 - All Rights Reserved
using System;
using System.Windows.Forms;
using LJC.Net.Common;
using LJC.WinForm.Common;
using LJC.WinForm.Controls;
using LJC.DBServiceLib;
using LJC.QueryGridLib;
using LJC.DBView.DAL;
// #SectionBegin Class
using _FullAppName_.DAL;

namespace _Namespace_
{
	// The tab composite user control.
	/// <include path='items/Module/*' file='../../CommonModule.xml'/>
	public partial class _ClassName_Module : Form
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../CommonData.xml'/>
		public _ClassName_Module()
		{
			InitializeComponent();

			// Set default class data.
			//LJCHelpPageList = "_ClassName_List.htm";
			//LJCHelpPageDetail = "_ClassName_Detail.htm";
		}
		#endregion

		// A Module has no Load event.

		#region Action Methods

		#region _ClassName_

		/// <summary>
		/// Sets the selected item and returns to the parent form.
		/// </summary>
		private void DoSelect_ClassName_()
		{
			// Add at end of ListTemplate code.
			if (_ClassName_Grid.LJCAllowSelectionChange)
			{
				OnPageClose();
			}
		}
		#endregion
		#endregion

		#region AppModule Implementation

		// Initializes the module.
		/// <include path='items/LJCInit/*' file='../../CommonModule.xml'/>
		public void LJCInit()
		{
			InitializeControls();
		}

		// Returns a reference to the module tab control.
		/// <include path='items/LJCTabs/*' file='../../CommonModule.xml'/>
		public TabControl LJCTabs()
		{
			return _ClassName_Tabs;
		}

		/// <summary>Gets the module assembly name.</summary>
		public string LJCProgramName
		{
			get { return "_FullAppName_.exe"; }
		}

		// Closes the current page.
		/// <include path='items/ClosePage/*' file='../../CommonModule.xml'/>
		public void ClosePage(TabPage tabPage)
		{
			LJCTabControl parentTabControl;

			// Set current page and invoke event.
			CloseTabPage = tabPage;
			OnPageClose();

			// Collapse tile panel if no tab pages left.
			parentTabControl = tabPage.Parent as LJCTabControl;
			if (parentTabControl != null)
			{
				parentTabControl.LJCCloseEmptyPanel();
			}
		}

		/// <summary>Gets or sets the close tab flag.</summary>
		public TabPage CloseTabPage { get; set; }

		/// <summary>Calls the PageClose event handlers.</summary>
		protected void OnPageClose()
		{
			PageClose?.Invoke(this, new EventArgs());
		}

		/// <summary>The page close event.</summary>
		public event EventHandler<EventArgs> PageClose;
		#endregion

		#region Action Event Handlers

		#region _ClassName_

		// <summary>Performs the Close function.</summary>
		private void _ClassName_MenuClose_Click(object sender, EventArgs e)
		{
			SaveControlValues();
			ClosePage(_ClassName_Page);
		}
		#endregion
		#endregion

		#region Control Event Handlers

		#region _ClassName_

		// <summary>Handles the form keys.</summary>
		private void _ClassName_Grid_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F1:
					Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
						, "_ClassName_List.htm");
					break;
			}
		}
		#endregion
		#endregion

		#region Properties

		///// <summary>The List help page name.</summary>
		//public string LJCHelpPageList
		//{
		//	get { return mHelpPageList; }
		//	set { mHelpPageList = LJCNetCommon.InitString(value); }
		//}
		//private string mHelpPageList;

		///// <summary>The Detail help page name.</summary>
		//public string LJCHelpPageDetail
		//{
		//	get { return mHelpPageDetail; }
		//	set { mHelpPageDetail = LJCNetCommon.InitString(value); }
		//}
		//private string mHelpPageDetail;
		#endregion
	}
}
// #SectionEnd Class
