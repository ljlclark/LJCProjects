using System;
using System.Windows.Forms;
using LJCRegionManager;

namespace LJCRegionForm
{
  // Copyright (c) Lester J. Clark 2017-2019 - All Rights Reserved
  /// <summary>Displays the RegionModule form.</summary>
  public partial class RegionTabForm : Form
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public RegionTabForm()
    {
      InitializeComponent();
    }
    #endregion

    #region Form Event Handlers

    private void RegionTabForm_Load(object sender, EventArgs e)
    {
      RegionModule = new RegionModule()
      {
        LJCIsSelect = true
      };

      TabControl moduleTabs = RegionModule.LJCTabs();
      moduleTabs.Parent = this;

      // Add the tab pages the first time it is requested.
      foreach (TabPage tabPage in moduleTabs.TabPages)
      {
        if (false == FormTabs.Contains(tabPage))
        {
          tabPage.Parent = FormTabs;
        }
      }
      RegionModule.LJCInit();
      RegionModule.PageClose += RegionModule_PageClose;

      RegionModule.LJCRegionID = LJCRegionID;
      RegionModule.LJCProvinceID = LJCProvinceID;
      RegionModule.LJCCityID = LJCCityID;
      RegionModule.LJCCitySectionID = LJCCitySectionID;
      RegionModule.LJCRegionName = LJCRegionName;
      RegionModule.LJCProvinceCode = LJCProvinceCode;
      RegionModule.LJCCityName = LJCCityName;
      RegionModule.LJCCitySectionName = LJCCitySectionName;

      CenterToScreen();
    }

    private void RegionModule_PageClose(object sender, EventArgs e)
    {
      if (IsChildWindow
        && RegionModule.LJCSelectedRegion != null)
      {
        DialogResult = DialogResult.OK;
      }
      else
      {
        Close();
      }
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the RegionModule reference.</summary>
    public RegionModule RegionModule { get; set; }

    /// <summary>The startup RegionID value.</summary>
    public int LJCRegionID { get; set; }

    /// <summary>The startup ProvinceID value.</summary>
    public int LJCProvinceID { get; set; }

    /// <summary>The startup CityID value.</summary>
    public int LJCCityID { get; set; }

    /// <summary>The startup CitySectionID value.</summary>
    public int LJCCitySectionID { get; set; }

    /// <summary>Gets or sets the Region name.</summary>
    public string LJCRegionName { get; set; }

    /// <summary>Gets or sets the Province abbreviation.</summary>
    public string LJCProvinceCode { get; set; }

    /// <summary>Gets or sets the City/Municipality name.</summary>
    public string LJCCityName { get; set; }

    /// <summary>Gets or sets the Barangay name.</summary>
    public string LJCCitySectionName { get; set; }

    /// <summary>Gets or sets the child window indicator.</summary>
    public bool IsChildWindow { get; set; }
    #endregion
  }
}
