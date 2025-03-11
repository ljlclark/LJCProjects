// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// FacilityForm.cs
using System;
using System.Windows.Forms;
using LJCFacilityManager;

namespace LJCFacilityForm
{
	/// <summary>The Facility test form.</summary>
	public partial class FacilityForm : Form
	{
		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public FacilityForm()
		{
			InitializeComponent();
		}

		// Configures the form and loads the initial control data.
		private void FacilityForm_Load(object sender, EventArgs e)
		{
			FacilityModule facilityModule = new FacilityModule();
			TabControl moduleTabs = facilityModule.LJCTabs();
			moduleTabs.Parent = this;

			// Add the tab pages the first time it is requested.
			foreach (TabPage tabPage in moduleTabs.TabPages)
			{
				if (!FormTabs.Contains(tabPage))
				{
					tabPage.Parent = FormTabs;
				}
			}
			facilityModule.LJCInit();
			facilityModule.PageClose += FacilityModule_PageClose;
		}

		// Handles the PageClose event.
		private void FacilityModule_PageClose(object sender, EventArgs e)
		{
			Close();
		}
	}
}
