// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// RegionModule.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using LJCDBClientLib;
using LJCGridDataLib;
using LJCNetCommon;
using LJCRegionDAL;
using LJCWinFormCommon;
using LJCWinFormControls;

namespace LJCRegionManager
{
  // Note: Modules are loaded dynamically with reflection. Any changes to
  //       this module must be compiled and copied to the host program folder
  //       before they are available. See the build UpdatePost.cmd file.

  // The Region tab composite user control.
  /// <include path='items/ModuleA/*' file='../../../CoreUtilities/LJCDocLib/Common/Module.xml'/>
  /// <link file="RegionTables.html">--Region DB Diagram</link>
  public partial class RegionModule : UserControl
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public RegionModule()
    {
      Cursor = Cursors.WaitCursor;
      InitializeComponent();
      string errorText = CheckDependencies();
      if (false == string.IsNullOrWhiteSpace(errorText))
      {
        MessageBox.Show(errorText);
      }

      // Set default class data.
      LJCHelpFile = "LJCRegionManager.chm";
      Cursor = Cursors.Default;
    }

    // Check for Depencency files.
    private string CheckDependencies()
    {
      string retValue = "";

      string fileTypeName = "Dependency";

      string fileSpec = "LJCNetCommon.dll";
      if (false == File.Exists(fileSpec))
      {
        retValue += $"{fileTypeName} '{fileSpec}' is not found.";
      }
      retValue += CheckFile("LJCRegionDAL.dll", fileTypeName);
      return retValue;
    }

    // Checks for file.
    private string CheckFile(string fileSpec, string fileTypeName = "File")
    {
      string retValue = null;

      if (false == NetString.HasValue(fileSpec))
      {
        retValue = $"{fileTypeName} name is missing.";
      }
      else
      {
        if (false == File.Exists(fileSpec))
        {
          retValue = $"{fileTypeName} '{fileSpec}' is not found.";
        }
      }
      return retValue;
    }
    #endregion

    #region Data Methods

    #region Region

    // Retrieves the list rows.
    /// <include path='items/DataRetrieve/*' file='../../../CoreUtilities/LJCDocLib/Common/List.xml'/>
    private void DataRetrieveRegion()
    {
      Regions records;

      Cursor = Cursors.WaitCursor;
      RegionCombo.Items.Clear();

      var regionManager = Managers.RegionDataManager;
      regionManager.SetOrderByName();
      records = regionManager.Load();

      if (records != null && records.Count > 0)
      {
        foreach (RegionData regionData in records)
        {
          RegionCombo.Items.Add(regionData);
        }
        if (LJCRegionID < 1 && false == NetString.HasValue(LJCRegionName))
        {
          RegionCombo.SelectedIndex = RegionCombo.Items.Count - 1;
        }
      }
      Cursor = Cursors.Default;
    }

    // Sets the selected item from the name value.
    /// <include path='items/SetRegion/*' file='Doc/RegionModule.xml'/>
    public void SetRegion(int regionID, string regionName)
    {
      if (regionID > 0)
      {
        int index = -1;
        foreach (RegionData dataRegion in RegionCombo.Items)
        {
          index++;
          if (dataRegion.ID == regionID)
          {
            RegionCombo.SelectedIndex = index;
            break;
          }
        }
      }
      else
      {
        if (regionName != null)
        {
          RegionCombo.SelectedIndex = RegionCombo.FindString(regionName);
        }
      }
    }
    #endregion

    #region Province

    // Retrieves the list rows.
    /// <include path='items/DataRetrieve/*' file='../../../CoreUtilities/LJCDocLib/Common/List.xml'/>
    public void DataRetrieveProvince()
    {
      Cursor = Cursors.WaitCursor;
      ProvinceGrid.LJCRowsClear();

      if (NetString.HasValue(RegionCombo.Text)
        && RegionCombo.SelectedItem is RegionData regionData)
      {
        var keyColumns = new DbColumns()
        {
          { Province.ColumnRegionID, regionData.ID }
        };
        var provinceManager = Managers.ProvinceManager;
        provinceManager.SetOrderByName();
        Provinces records = provinceManager.Load(keyColumns);

        if (records != null && records.Count > 0)
        {
          SetupGridProvince(records[0]);  // Region
          foreach (Province province in records)
          {
            RowAddProvince(province);
          }
        }
      }
      Cursor = Cursors.Default;
      DoChange(Change.Province);
    }

    // Adds a grid row and updates it with the record values.
    /// <include path='items/RowAdd/*' file='../../../CoreUtilities/LJCDocLib/Common/List.xml'/>
    private LJCGridRow RowAddProvince(Province dataRecord)
    {
      LJCGridRow retValue;

      retValue = ProvinceGrid.LJCRowAdd();
      SetStoredValuesProvince(retValue, dataRecord);

      // Sets the row values from a data object.
      retValue.LJCSetValues(ProvinceGrid, dataRecord);
      return retValue;
    }

    // Updates the current row with the record values.
    /// <include path='items/RowUpdate/*' file='../../../CoreUtilities/LJCDocLib/Common/List.xml'/>
    private void RowUpdateProvince(Province dataRecord)
    {
      if (ProvinceGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValuesProvince(row, dataRecord);
        row.LJCSetValues(ProvinceGrid, dataRecord);
      }
    }

    // Sets the row stored values.
    /// <include path='items/SetStoredValues/*' file='../../../CoreUtilities/LJCDocLib/Common/List.xml'/>
    private void SetStoredValuesProvince(LJCGridRow row, Province dataRecord)
    {
      row.LJCSetInt32(Province.ColumnID, dataRecord.ID);
      row.LJCSetString(Province.ColumnName, dataRecord.Name);
    }

    // Selects a row based on the key record values.
    /// <include path='items/RowSelect/*' file='../../../CoreUtilities/LJCDocLib/Common/List.xml'/>
    private bool RowSelectProvince(Province dataRecord)
    {
      int rowID;
      bool retValue = false;

      if (dataRecord != null)
      {
        Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in ProvinceGrid.Rows)
        {
          rowID = row.LJCGetInt32(Province.ColumnID);
          if (rowID == dataRecord.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            ProvinceGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Selects a row based on the supplied province code.
    /// <include path='items/SetProvince/*' file='Doc/RegionModule.xml'/>
    public void SetProvince(int provinceID, string provinceCode)
    {
      DbColumns keyColumns;

      if (provinceID > 0 || provinceCode != null)
      {
        if (provinceID > 0)
        {
          keyColumns = new DbColumns()
          {
            { Province.ColumnID, provinceID }
          };
        }
        else
        {
          keyColumns = new DbColumns()
          {
            { Province.ColumnAbbreviation, (object)provinceCode }
          };
        }
        Province lookupRecord = Managers.ProvinceManager.Retrieve(keyColumns);
        if (lookupRecord != null)
        {
          RowSelectProvince(lookupRecord);
        }
      }
    }
    #endregion

    #region City

    // Retrieves the list rows.
    /// <include path='items/DataRetrieve/*' file='../../../CoreUtilities/LJCDocLib/Common/List.xml'/>
    public void DataRetrieveCity()
    {
      Cursor = Cursors.WaitCursor;
      CityGrid.LJCRowsClear();

      if (ProvinceGrid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from list items.
        int provinceID = parentRow.LJCGetInt32(Province.ColumnID);

        var keyColumns = new DbColumns()
        {
          { City.ColumnProvinceID, provinceID },
        };
        var cityManager = Managers.CityManager;
        cityManager.SetOrderByName();
        Cities cities = cityManager.Load(keyColumns);

        if (cities != null && cities.Count > 0)
        {
          SetupGridCity(cities[0]);  // Region
          foreach (City city in cities)
          {
            RowAddCity(city);
          }
        }
      }
      Cursor = Cursors.Default;
      DoChange(Change.City);
    }

    // Adds a grid row and updates it with the record values.
    /// <include path='items/RowAdd/*' file='../../../CoreUtilities/LJCDocLib/Common/List.xml'/>
    private LJCGridRow RowAddCity(City dataRecord)
    {
      LJCGridRow retValue;

      retValue = CityGrid.LJCRowAdd();
      SetStoredValuesCity(retValue, dataRecord);

      // Sets the row values from a data object.
      retValue.LJCSetValues(CityGrid, dataRecord);
      return retValue;
    }

    // Updates the current row with the record values.
    /// <include path='items/RowUpdate/*' file='../../../CoreUtilities/LJCDocLib/Common/List.xml'/>
    private void RowUpdateCity(City dataRecord)
    {
      if (CityGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValuesCity(row, dataRecord);
        row.LJCSetValues(CityGrid, dataRecord);
      }
    }

    // Sets the row stored values.
    /// <include path='items/SetStoredValues/*' file='../../../CoreUtilities/LJCDocLib/Common/List.xml'/>
    private void SetStoredValuesCity(LJCGridRow row, City dataRecord)
    {
      row.LJCSetInt32(City.ColumnID, dataRecord.ID);
      row.LJCSetString(City.ColumnName, dataRecord.Name);
    }

    // Selects a row based on the key record values.
    /// <include path='items/RowSelect/*' file='../../../CoreUtilities/LJCDocLib/Common/List.xml'/>
    private bool RowSelectCity(City dataRecord)
    {
      int rowID;
      bool retValue = false;

      if (dataRecord != null)
      {
        Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in CityGrid.Rows)
        {
          rowID = row.LJCGetInt32(City.ColumnID);
          if (rowID == dataRecord.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            CityGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Selects a row based on the supplied value.
    /// <include path='items/SetCity/*' file='Doc/RegionModule.xml'/>
    public void SetCity(int cityID, string cityName)
    {
      DbColumns keyColumns;

      if (cityID > 0 || cityName != null)
      {
        if (cityID > 0)
        {
          keyColumns = new DbColumns()
          {
            { City.ColumnID, cityID }
          };
        }
        else
        {
          keyColumns = new DbColumns()
          {
            { City.ColumnName, (object)cityName }
          };
        }
        City lookupRecord = Managers.CityManager.Retrieve(keyColumns);
        if (lookupRecord != null)
        {
          RowSelectCity(lookupRecord);
        }
      }
    }
    #endregion

    #region CitySection

    // Retrieves the list rows.
    /// <include path='items/DataRetrieve/*' file='../../../CoreUtilities/LJCDocLib/Common/List.xml'/>
    public void DataRetrieveCitySection()
    {
      Cursor = Cursors.WaitCursor;
      CitySectionGrid.LJCRowsClear();

      if (CityGrid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from list items.
        int cityID = parentRow.LJCGetInt32(City.ColumnID);

        var keyColumns = new DbColumns()
        {
          { CitySection.ColumnCityID, cityID }
        };
        var citySectionManager = Managers.CitySectionManager;
        citySectionManager.SetOrderByName();
        CitySections list = citySectionManager.Load(keyColumns);

        if (list != null && list.Count > 0)
        {
          SetupGridCitySection(list[0]);  // Region
          foreach (CitySection city in list)
          {
            RowAddCitySection(city);
          }
        }
      }
      Cursor = Cursors.Default;
      DoChange(Change.CitySection);
    }

    // Adds a grid row and updates it with the record values.
    /// <include path='items/RowAdd/*' file='../../../CoreUtilities/LJCDocLib/Common/List.xml'/>
    private LJCGridRow RowAddCitySection(CitySection dataRecord)
    {
      LJCGridRow retValue;

      retValue = CitySectionGrid.LJCRowAdd();
      SetStoredValuesCitySection(retValue, dataRecord);

      // Sets the row values from a data object.
      retValue.LJCSetValues(CitySectionGrid, dataRecord);
      return retValue;
    }

    // Updates the current row with the record values.
    /// <include path='items/RowUpdate/*' file='../../../CoreUtilities/LJCDocLib/Common/List.xml'/>
    private void RowUpdateCitySection(CitySection dataRecord)
    {
      if (CitySectionGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValuesCitySection(row, dataRecord);
        row.LJCSetValues(CitySectionGrid, dataRecord);
      }
    }

    // Sets the row stored values.
    /// <include path='items/SetStoredValues/*' file='../../../CoreUtilities/LJCDocLib/Common/List.xml'/>
    private void SetStoredValuesCitySection(LJCGridRow row, CitySection dataRecord)
    {
      row.LJCSetInt32(CitySection.ColumnID, dataRecord.ID);
    }

    // Selects a row based on the key record values.
    /// <include path='items/RowSelect/*' file='../../../CoreUtilities/LJCDocLib/Common/List.xml'/>
    private bool RowSelectCitySection(CitySection dataRecord)
    {
      int rowID;
      bool retValue = false;

      if (dataRecord != null)
      {
        Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in CitySectionGrid.Rows)
        {
          rowID = row.LJCGetInt32(CitySection.ColumnID);
          if (rowID == dataRecord.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            CitySectionGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Sets the selected item from the name value.
    /// <include path='items/SetCitySection/*' file='Doc/RegionModule.xml'/>
    public void SetCitySection(int citySectionID, string citySectionName)
    {
      DbColumns keyColumns;

      if (citySectionID > 0 || citySectionName != null)
      {
        if (citySectionID > 0)
        {
          keyColumns = new DbColumns()
          {
            { CitySection.ColumnID, citySectionID }
          };
        }
        else
        {
          keyColumns = new DbColumns()
          {
            { CitySection.ColumnName, (object)citySectionName }
          };
        }
        CitySection lookupRecord = Managers.CitySectionManager.Retrieve(keyColumns);
        if (lookupRecord != null)
        {
          RowSelectCitySection(lookupRecord);
        }
      }
    }
    #endregion
    #endregion

    #region Action Methods

    #region Province

    // Performs the default list action.
    /// <include path='items/DoDefault/*' file='../../../CoreUtilities/LJCDocLib/Common/List.xml'/>
    private void DoDefaultProvince()
    {
      if (LJCIsSelect)
      {
        DoSelectProvince();
      }
      else
      {
        DoEditProvince();
      }
    }

    // Displays a detail dialog for a new record.
    /// <include path='items/DoNew/*' file='../../../CoreUtilities/LJCDocLib/Common/List.xml'/>
    private void DoNewProvince()
    {
      ProvinceDetail detail;

      if (RegionCombo.SelectedItem is RegionData regionData)
      {
        detail = new ProvinceDetail
        {
          LJCParentID = regionData.ID,
          LJCParentName = regionData.Name,
          LJCHelpFileName = LJCHelpFile,
          LJCHelpPageName = @"Province\ProvinceDetail.html"
        };
        detail.LJCChange += ProvinceDetail_Change;
        detail.ShowDialog();
      }
    }

    // Displays a detail dialog to edit an existing record.
    /// <include path='items/DoEdit/*' file='../../../CoreUtilities/LJCDocLib/Common/List.xml'/>
    private void DoEditProvince()
    {
      ProvinceDetail detail;

      if (RegionCombo.SelectedItem is RegionData parentData
        && ProvinceGrid.CurrentRow is LJCGridRow row)
      {
        // Data from list items.
        int id = row.LJCGetInt32(Province.ColumnID);

        detail = new ProvinceDetail()
        {
          LJCID = id,
          LJCParentID = parentData.ID,
          LJCParentName = parentData.Name,
          LJCHelpFileName = LJCHelpFile,
          LJCHelpPageName = @"Province\ProvinceDetail.html"
        };
        detail.LJCChange += ProvinceDetail_Change;
        detail.ShowDialog();
      }
    }

    // Adds new row or updates existing row with changes from the detail dialog.
    /// <include path='items/Detail_Change/*' file='../../../CoreUtilities/LJCDocLib/Common/List.xml'/>
    private void ProvinceDetail_Change(object sender, EventArgs e)
    {
      ProvinceDetail detail;
      Province record;
      LJCGridRow row;

      detail = sender as ProvinceDetail;
      record = detail.LJCRecord;
      if (detail.LJCIsUpdate)
      {
        RowUpdateProvince(record);
      }
      else
      {
        SetupGridProvince(record);  // Region

        // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
        row = RowAddProvince(record);
        ProvinceGrid.LJCSetCurrentRow(row, true);
        TimedChange(Change.Province);
      }
    }

    // Deletes the selected row.
    /// <include path='items/DoDelete/*' file='../../../CoreUtilities/LJCDocLib/Common/List.xml'/>
    private void DoDeleteProvince()
    {
      string title;
      string message;

      if (ProvinceGrid.CurrentRow is LJCGridRow row)
      {
        title = "Delete Confirmation";
        message = FormCommon.DeleteConfirm;
        if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
          , MessageBoxIcon.Question) == DialogResult.Yes)
        {
          // Data from list items.
          int id = row.LJCGetInt32(Province.ColumnID);

          var keyColumns = new DbColumns()
          {
            { Province.ColumnID, id }
          };
          var provinceManager = Managers.ProvinceManager;
          provinceManager.Delete(keyColumns);
          if (provinceManager.AffectedCount < 1)
          {
            message = FormCommon.DeleteError;
            MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
              , MessageBoxIcon.Exclamation);
          }
          else
          {
            ProvinceGrid.Rows.Remove(row);
            TimedChange(Change.Province);
          }
        }
      }
    }

    // Refreshes the list.
    /// <include path='items/DoRefresh/*' file='../../../CoreUtilities/LJCDocLib/Common/List.xml'/>
    private void DoRefreshProvince()
    {
      Province record;
      int id = 0;

      if (ProvinceGrid.CurrentRow is LJCGridRow row)
      {
        id = row.LJCGetInt32(Province.ColumnID);
      }
      DataRetrieveProvince();

      // Select the original row.
      if (id > 0)
      {
        record = new Province()
        {
          ID = id
        };
        RowSelectProvince(record);
      }
    }

    // Sets the selected item and returns to the parent form.
    /// <include path='items/DoSelect/*' file='../../../CoreUtilities/LJCDocLib/Common/List.xml'/>
    private void DoSelectProvince()
    {
      Province record;
      int id;

      LJCSelectedProvince = null;
      LJCSelectedCity = null;
      LJCSelectedCitySection = null;
      if (ProvinceGrid.CurrentRow is LJCGridRow row)
      {
        Cursor = Cursors.WaitCursor;
        id = row.LJCGetInt32(Province.ColumnID);

        var keyColumns = new DbColumns()
        {
          { Province.ColumnID, id }
        };
        record = Managers.ProvinceManager.Retrieve(keyColumns);
        if (record != null)
        {
          LJCSelectedProvince = record;
        }
        Cursor = Cursors.Default;
      }
      if (ProvinceGrid.LJCAllowSelectionChange)
      {
        OnPageClose();  // Module
      }
    }
    #endregion

    #region City

    // Find the List item.
    private void DoFindCity()
    {
      foreach (LJCGridRow gridRow in CityGrid.Rows)
      {
        string findText = CityFindTextBox.Text.Trim().ToLower();
        string nameText = gridRow.LJCGetCellText(City.ColumnName).ToLower();
        if (nameText.StartsWith(findText))
        {
          CityGrid.LJCSetCurrentRow(gridRow, true);
          break;
        }
      }
    }

    // Performs the default list action.
    /// <include path='items/DoDefault/*' file='../../../CoreUtilities/LJCDocLib/Common/List.xml'/>
    private void DoDefaultCity()
    {
      if (LJCIsSelect)
      {
        DoSelectCity();
      }
      else
      {
        DoEditCity();
      }
    }

    // Displays a detail dialog for a new record.
    /// <include path='items/DoNew/*' file='../../../CoreUtilities/LJCDocLib/Common/List.xml'/>
    private void DoNewCity()
    {
      CityDetail detail;

      if (ProvinceGrid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from list items.
        int parentID = parentRow.LJCGetInt32(Province.ColumnID);
        string parentName = parentRow.LJCGetString(Province.ColumnName);

        detail = new CityDetail
        {
          LJCParentID = parentID,
          LJCParentName = parentName,
          LJCHelpFileName = LJCHelpFile,
          LJCHelpPageName = @"City\CityDetail.html"
        };
        detail.LJCChange += CityDetail_Change;
        detail.ShowDialog();
      }
    }

    // Displays a detail dialog to edit an existing record.
    /// <include path='items/DoEdit/*' file='../../../CoreUtilities/LJCDocLib/Common/List.xml'/>
    private void DoEditCity()
    {
      CityDetail detail;

      if (ProvinceGrid.CurrentRow is LJCGridRow parentRow
        && CityGrid.CurrentRow is LJCGridRow row)
      {
        // Data from list items.
        int id = row.LJCGetInt32(City.ColumnID);
        int parentID = parentRow.LJCGetInt32(Province.ColumnID);
        string parentName = parentRow.LJCGetString(Province.ColumnName);

        detail = new CityDetail()
        {
          LJCID = id,
          LJCParentID = parentID,
          LJCParentName = parentName,
          LJCHelpFileName = LJCHelpFile,
          LJCHelpPageName = @"City\CityDetail.html"
        };
        detail.LJCChange += CityDetail_Change;
        detail.ShowDialog();
      }
    }

    // Adds new row or updates existing row with changes from the detail dialog.
    /// <include path='items/Detail_Change/*' file='../../../CoreUtilities/LJCDocLib/Common/List.xml'/>
    private void CityDetail_Change(object sender, EventArgs e)
    {
      CityDetail detail;
      City record;
      LJCGridRow row;

      detail = sender as CityDetail;
      record = detail.LJCRecord;
      if (detail.LJCIsUpdate)
      {
        RowUpdateCity(record);
      }
      else
      {
        SetupGridCity(record); // Region

        // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
        row = RowAddCity(record);
        CityGrid.LJCSetCurrentRow(row, true);
        TimedChange(Change.City);
      }
    }

    // Deletes the selected row.
    /// <include path='items/DoDelete/*' file='../../../CoreUtilities/LJCDocLib/Common/List.xml'/>
    private void DoDeleteCity()
    {
      string title;
      string message;

      if (CityGrid.CurrentRow is LJCGridRow row)
      {
        title = "Delete Confirmation";
        message = FormCommon.DeleteConfirm;
        if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
          , MessageBoxIcon.Question) == DialogResult.Yes)
        {
          // Data from list items.
          int id = row.LJCGetInt32(City.ColumnID);

          var keyColumns = new DbColumns()
          {
            { City.ColumnID, id }
          };
          var cityManager = Managers.CityManager;
          cityManager.Delete(keyColumns);
          if (cityManager.AffectedCount < 1)
          {
            message = FormCommon.DeleteError;
            MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
              , MessageBoxIcon.Exclamation);
          }
          else
          {
            CityGrid.Rows.Remove(row);
            TimedChange(Change.City);
          }
        }
      }
    }

    // Refreshes the list.
    /// <include path='items/DoRefresh/*' file='../../../CoreUtilities/LJCDocLib/Common/List.xml'/>
    private void DoRefreshCity()
    {
      City record;
      int id = 0;

      if (CityGrid.CurrentRow is LJCGridRow row)
      {
        id = row.LJCGetInt32(City.ColumnID);
      }
      DataRetrieveCity();

      // Select the original row.
      if (id > 0)
      {
        record = new City()
        {
          ID = id
        };
        RowSelectCity(record);
      }
    }

    // Sets the selected item and returns to the parent form.
    /// <include path='items/DoSelect/*' file='../../../CoreUtilities/LJCDocLib/Common/List.xml'/>
    private void DoSelectCity()
    {
      City record;
      int id;

      // Module
      ProvinceGrid.LJCAllowSelectionChange = false;
      DoSelectProvince();
      ProvinceGrid.LJCAllowSelectionChange = true;

      LJCSelectedCity = null;
      LJCSelectedCitySection = null;
      if (CityGrid.CurrentRow is LJCGridRow row)
      {
        Cursor = Cursors.WaitCursor;
        id = row.LJCGetInt32(City.ColumnID);

        record = Managers.CityManager.RetrieveWithID(id);
        if (record != null)
        {
          LJCSelectedCity = record;
        }
        Cursor = Cursors.Default;
      }
      if (CityGrid.LJCAllowSelectionChange)
      {
        OnPageClose();  // Module
      }
    }
    #endregion

    #region CitySection

    // Find the List item.
    private void DoFindCitySection()
    {
      foreach (LJCGridRow gridRow in CitySectionGrid.Rows)
      {
        string findText = CitySectionFindTextBox.Text.Trim().ToLower();
        string nameText = gridRow.LJCGetCellText(CitySection.ColumnName).ToLower();
        if (nameText.StartsWith(findText))
        {
          CitySectionGrid.LJCSetCurrentRow(gridRow);
          break;
        }
      }
    }

    // Performs the default list action.
    /// <include path='items/DoDefault/*' file='../../../CoreUtilities/LJCDocLib/Common/List.xml'/>
    private void DoDefaultCitySection()
    {
      if (LJCIsSelect)
      {
        DoSelectCitySection();
      }
      else
      {
        DoEditCitySection();
      }
    }

    // Displays a detail dialog for a new record.
    /// <include path='items/DoNew/*' file='../../../CoreUtilities/LJCDocLib/Common/List.xml'/>
    private void DoNewCitySection()
    {
      CitySectionDetail detail;

      if (CityGrid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from list items.
        int parentID = parentRow.LJCGetInt32(City.ColumnID);
        string parentName = parentRow.LJCGetString(City.ColumnName);

        detail = new CitySectionDetail
        {
          LJCParentID = parentID,
          LJCParentName = parentName,
          LJCHelpFileName = LJCHelpFile,
          LJCHelpPageName = @"CitySection\CitySectionDetail.html"
        };
        detail.LJCChange += CitySectionDetail_Change;
        detail.ShowDialog();
      }
    }

    // Displays a detail dialog to edit an existing record.
    /// <include path='items/DoEdit/*' file='../../../CoreUtilities/LJCDocLib/Common/List.xml'/>
    private void DoEditCitySection()
    {
      CitySectionDetail detail;

      if (CityGrid.CurrentRow is LJCGridRow parentRow
        && CitySectionGrid.CurrentRow is LJCGridRow row)
      {
        // Data from list items.
        int id = row.LJCGetInt32(CitySection.ColumnID);
        int parentID = parentRow.LJCGetInt32(City.ColumnID);
        string parentName = parentRow.LJCGetString(City.ColumnName);

        detail = new CitySectionDetail()
        {
          LJCID = id,
          LJCParentID = parentID,
          LJCParentName = parentName,
          LJCHelpFileName = LJCHelpFile,
          LJCHelpPageName = @"CitySection\CitySectionDetail.html"
        };
        detail.LJCChange += CitySectionDetail_Change;
        detail.ShowDialog();
      }
    }

    // Adds new row or updates existing row with changes from the detail dialog.
    /// <include path='items/Detail_Change/*' file='../../../CoreUtilities/LJCDocLib/Common/List.xml'/>
    private void CitySectionDetail_Change(object sender, EventArgs e)
    {
      CitySectionDetail detail;
      CitySection record;
      LJCGridRow row;

      detail = sender as CitySectionDetail;
      record = detail.LJCRecord;
      if (detail.LJCIsUpdate)
      {
        RowUpdateCitySection(record);
      }
      else
      {
        SetupGridCitySection(record);  // Region

        // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
        row = RowAddCitySection(record);
        CitySectionGrid.LJCSetCurrentRow(row, true);
        TimedChange(Change.CitySection);
      }
    }

    // Deletes the selected row.
    /// <include path='items/DoDelete/*' file='../../../CoreUtilities/LJCDocLib/Common/List.xml'/>
    private void DoDeleteCitySection()
    {
      string title;
      string message;

      if (CitySectionGrid.CurrentRow is LJCGridRow row)
      {
        title = "Delete Confirmation";
        message = FormCommon.DeleteConfirm;
        if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
          , MessageBoxIcon.Question) == DialogResult.Yes)
        {
          // Data from list items.
          int id = row.LJCGetInt32(CitySection.ColumnID);

          var keyColumns = new DbColumns()
          {
            { CitySection.ColumnID, id }
          };
          var citySectionManager = Managers.CitySectionManager;
          citySectionManager.Delete(keyColumns);
          if (citySectionManager.AffectedCount < 1)
          {
            message = FormCommon.DeleteError;
            MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
              , MessageBoxIcon.Exclamation);
          }
          else
          {
            CitySectionGrid.Rows.Remove(row);
            TimedChange(Change.CitySection);
          }
        }
      }
    }

    // Refreshes the list.
    /// <include path='items/DoRefresh/*' file='../../../CoreUtilities/LJCDocLib/Common/List.xml'/>
    private void DoRefreshCitySection()
    {
      CitySection record;
      int id = 0;

      if (CitySectionGrid.CurrentRow is LJCGridRow row)
      {
        id = row.LJCGetInt32(CitySection.ColumnID);
      }
      DataRetrieveCitySection();

      // Select the original row.
      if (id > 0)
      {
        record = new CitySection()
        {
          ID = id
        };
        RowSelectCitySection(record);
      }
    }

    // Sets the selected item and returns to the parent form.
    /// <include path='items/DoSelect/*' file='../../../CoreUtilities/LJCDocLib/Common/List.xml'/>
    private void DoSelectCitySection()
    {
      CitySection record;
      int id;

      // Module
      CityGrid.LJCAllowSelectionChange = false;
      DoSelectCity();
      CityGrid.LJCAllowSelectionChange = true;

      LJCSelectedCitySection = null;
      if (CitySectionGrid.CurrentRow is LJCGridRow row)
      {
        Cursor = Cursors.WaitCursor;
        id = row.LJCGetInt32(CitySection.ColumnID);

        var keyColumns = new DbColumns()
        {
          { CitySection.ColumnID, id }
        };
        record = Managers.CitySectionManager.Retrieve(keyColumns);
        if (record != null)
        {
          LJCSelectedCitySection = record;
        }
        Cursor = Cursors.Default;
      }
      if (CitySectionGrid.LJCAllowSelectionChange)
      {
        OnPageClose();  // Module
      }
    }
    #endregion
    #endregion

    #region Setup Methods

    // Configure the initial control settings.
    private void ConfigureControls()
    {
      // Make sure lists scroll vertically and counter labels show.
      if (AutoScaleMode == AutoScaleMode.Font)
      {
        ProvinceSplit.SplitterWidth = 5;
        CitySplit.SplitterWidth = 5;
        int oneThird = ProvinceSplit.Height / 3;
        ProvinceSplit.SplitterDistance = oneThird;
        CitySplit.SplitterDistance = oneThird;
        RegionCombo.Top = RegionLabel.Top - 5;

        // Modify MainSplit.Panel1 controls.
        ListHelper.SetPanelControls(ProvinceSplit.Panel1, ProvinceHeading
          , ProvinceToolPanel, ProvinceGrid);

        // Modify MainSplit.Panel2, ChildSplit.
        ListHelper.SetPanelSplitControl(ProvinceSplit.Panel2, CitySplit);

        // Modify ChildSplit.Panel1 controls.
        ListHelper.SetPanelControls(CitySplit.Panel1, CityHeading
          , CityToolPanel, CityGrid);

        CityFindTextBox.Top -= 1;
        CityFindButton.Height = CityFindTextBox.Height;
        CityFindButton.Top = CityFindTextBox.Top;

        // Modify ChildSplit.Panel2 controls.
        ListHelper.SetPanelControls(CitySplit.Panel2, CitySectionHeading
          , CitySectionToolPanel, CitySectionGrid);

        CitySectionFindTextBox.Top -= 1;
        CitySectionFindButton.Height = CitySectionFindTextBox.Height;
        CitySectionFindButton.Top = CitySectionFindTextBox.Top;
      }
    }

    // Configures the controls and loads the selection control data.
    private void InitializeControls()
    {
      // Get singleton values.
      Cursor = Cursors.WaitCursor;
      var values = ValuesRegion.Instance;
      mSettings = values.StandardSettings;

      // Initialize Class Data.
      Managers = new RegionManagers(mSettings.DbServiceRef
        , mSettings.DataConfigName);

      // Configure controls.
      if (RegionTabs.Parent != null
        && "RegionModule" == RegionTabs.Parent.GetType().Name)
      {
        ProvinceMenuExit.Text = "Close";
      }

      if (LJCIsSelect)
      {
        // This is a Selection List.
        Text = "Region Selection";
      }
      else
      {
        // This is a display list.
        Text = "Region List";
        ProvinceMenuSelect.Visible = false;
        CityMenuSelect.Visible = false;
        CitySectionSelect.Visible = false;
      }

      // Set initial control values.
      NetFile.CreateFolder("ControlValues");
      mControlValuesFileName = @"ControlValues\Region.xml";

      ProvinceToolPanel.BackColor = mSettings.BeginColor;
      ProvinceTool.BackColor = mSettings.BeginColor;
      CityToolPanel.BackColor = mSettings.BeginColor;
      CityTool.BackColor = mSettings.BeginColor;
      CitySectionToolPanel.BackColor = mSettings.BeginColor;
      CitySectionTool.BackColor = mSettings.BeginColor;

      SetupGrids();
      StartItemChange();
      Cursor = Cursors.Default;
    }

    #region Setup Support

    // Create the application tables.
    internal static void CreateTables(SystemException e, string dataConfigName)
    {
      ManagerCommon.GetConfigValues(dataConfigName, out string connectionType
        , out string _, out string _);

      string[] fileSpecs;
      switch (connectionType)
      {
        case "MySQL":
          string[] fileSpecs1 = {
            @"MySQLScript\2RegionTables.sql",
            @"MySQLScript\3RegionData.sql",
            @"MySQLScript\4ProvinceData.sql",
            @"MySQLScript\5CityData.sql",
            @"MySQLScript\6CitySectionData.sql"
          };
          fileSpecs = fileSpecs1;
          break;

        default:
          string[] fileSpecs2 = {
            @"SQLScript\MS2RegionTables.sql",
            @"SQLScript\MS3RegionData.sql",
            @"SQLScript\MS4ProvinceData.sql",
            @"SQLScript\MS5CityData.sql",
            @"SQLScript\MS6CitySectionData.sql"
          };
          fileSpecs = fileSpecs2;
          break;
      }

      int errorCode = ManagerCommon.GetMissingTableErrorCode(dataConfigName);
      if (e.HResult == errorCode)
      {
        if (FormCommon.CreateTablesPrompt(e.Message, fileSpecs))
        {
          if (false == ManagerCommon.CreateTables(dataConfigName, fileSpecs))
          {
            throw new SystemException(e.Message);
          }
        }
      }
    }

    // Restores the control values.
    private void RestoreControlValues()
    {
      Control parent = RegionTabs.Parent;
      ControlValue controlValue;

      if (File.Exists(mControlValuesFileName))
      {
        ControlValues = NetCommon.XmlDeserialize(typeof(ControlValues)
          , mControlValuesFileName) as ControlValues;

        if (ControlValues != null)
        {
          // Restore Window values.
          controlValue = ControlValues.LJCSearchName(Name);

          if (controlValue != null)
          {
            // Tabs Parent is not this module form.
            if (parent != null
                && parent.GetType().Name != Name)
            {
              parent.Left = controlValue.Left;
              parent.Top = controlValue.Top;
              parent.Width = controlValue.Width;
              parent.Height = controlValue.Height;
            }
          }

          // Restore Splitter and other values.
          FormCommon.RestoreSplitDistance(ProvinceSplit, ControlValues);
          FormCommon.RestoreSplitDistance(CitySplit, ControlValues);
        }
      }
    }

    // Saves the control values. 
    private void SaveControlValues()
    {
      Control parent = RegionTabs.Parent;
      ControlValues controlValues = new ControlValues();

      // Save Grid Column values.
      ProvinceGrid.LJCSaveColumnValues(controlValues);
      CityGrid.LJCSaveColumnValues(controlValues);
      CitySectionGrid.LJCSaveColumnValues(controlValues);

      // Save Splitter values.
      controlValues.Add("ProvinceSplit.SplitterDistance", 0, 0, 0, ProvinceSplit.SplitterDistance);
      controlValues.Add("CitySplit.SplitterDistance", 0, 0, 0, CitySplit.SplitterDistance);

      // Save Window values.
      // Tabs Parent is not this module form.
      if (parent != null
        && parent.GetType().Name != Name)
      {
        controlValues.Add(Name, parent.Left, parent.Top
          , parent.Width, parent.Height);
      }

      NetCommon.XmlSerialize(controlValues.GetType(), controlValues, null
        , mControlValuesFileName);
    }

    // Setup the data grids.
    private void SetupGrids()
    {
      try
      {
        SetupGridProvince();
      }
      catch (SystemException e)
      {
        CreateTables(e, mSettings.DataConfigName);
        SetupGridProvince();
      }
      SetupGridCity();
      SetupGridCitySection();
    }

    // Setup the Province grid.
    private void SetupGridProvince(Province record = null)
    {
      //ProvinceGrid.BackgroundColor = Color.AliceBlue;

      // Setup default display columns if no columns are defined.
      if (record != null
        && 0 == ProvinceGrid.Columns.Count)
      {
        List<string> displayColumns = new List<string>()
        {
          Province.ColumnAbbreviation,
          Province.ColumnName,
          Province.ColumnDescription,
        };

        // Get the display columns from the record.
        ResultGridData resultGridData = new ResultGridData();
        resultGridData.SetGridColumns(record, displayColumns);
        //mProvinceDisplayColumns = resultGridData.DisplayColumns;

        // Setup the grid display columns.
        ProvinceGrid.LJCAddColumns(resultGridData.GridColumns);
        ProvinceGrid.LJCRestoreColumnValues(ControlValues);
      }
    }
    //private DbColumns mProvinceDisplayColumns;

    // Setup the City grid.
    private void SetupGridCity(City record = null)
    {
      //CityGrid.BackgroundColor = Color.AliceBlue;

      // Setup default display columns if no columns are defined.
      if (record != null
        && 0 == CityGrid.Columns.Count)
      {
        List<string> displayColumns = new List<string>()
        {
          City.ColumnName,
          City.ColumnDescription,
          City.ColumnZipCode,
          City.ColumnDistrict
        };

        // Get the display columns from the record.
        ResultGridData resultGridData = new ResultGridData();
        resultGridData.SetGridColumns(record, displayColumns);
        //mCityDisplayColumns = resultGridData.DisplayColumns;

        // Setup the grid display columns.
        CityGrid.LJCAddColumns(resultGridData.GridColumns);
        CityGrid.LJCRestoreColumnValues(ControlValues);
      }
    }
    //private DbColumns mCityDisplayColumns;

    // Setup the CitySection grid.
    private void SetupGridCitySection(CitySection record = null)
    {
      //CitySectionGrid.BackgroundColor = Color.AliceBlue;

      // Setup default display columns if no columns are defined.
      if (record != null
        && 0 == CitySectionGrid.Columns.Count)
      {
        List<string> displayColumns = new List<string>()
        {
          CitySection.ColumnName,
          CitySection.ColumnDescription,
          "ZoneType",
          "Contact"
        };

        // Get the display columns from the record.
        ResultGridData resultGridData = new ResultGridData();
        resultGridData.SetGridColumns(record, displayColumns);
        //mCitySectionDisplayColumns = resultGridData.DisplayColumns;

        // Setup the grid display columns.
        CitySectionGrid.LJCAddColumns(resultGridData.GridColumns);
        CitySectionGrid.LJCRestoreColumnValues(ControlValues);
      }
    }
    //private DbColumns mCitySectionDisplayColumns;

    /// <summary>Gets or sets the Allow Change value.</summary>
    public ControlValues ControlValues { get; set; }
    #endregion
    #endregion

    #region AppModule Implementation

    /// <summary>
    /// Initializes the module.
    /// </summary>
    public void LJCInit()
    {
      InitializeControls();
    }

    /// <summary>
    /// Returns a reference to the module tab control.
    /// </summary>
    /// <returns>A reference to the module tab control.</returns>
    public TabControl LJCTabs()
    {
      return RegionTabs;
    }

    /// <summary>Gets the module assembly name.</summary>
    public string LJCProgramName
    {
      get { return "LJCRegionManager.exe"; }
    }

    /// <summary>
    /// Closes the current page.
    /// </summary>
    /// <param name="tabPage">The current tab page.</param>
    public void ClosePage(TabPage tabPage)
    {
      LJCTabControl parentTabControl;

      // Set current page and invoke event.
      CloseTabPage = tabPage;
      OnPageClose();

      // Collapse tile panel if no tab pages left.
      parentTabControl = tabPage.Parent as LJCTabControl;
      parentTabControl?.LJCCloseEmptyPanel();
    }

    /// <summary>Gets or sets the close tab page.</summary>
    public TabPage CloseTabPage { get; set; }

    /// <summary>Calls the PageClose event handlers.</summary>
    /// <remarks><para>Syntax: protected void OnPageClose()</para></remarks>
    protected void OnPageClose()
    {
      PageClose?.Invoke(this, new EventArgs());
    }

    /// <summary>The page close event.</summary>
    public event EventHandler<EventArgs> PageClose;
    #endregion

    #region Item Change Processing

    // Execute the list and related item functions.
    internal void DoChange(Change change)
    {
      Cursor = Cursors.WaitCursor;
      switch (change)
      {
        case Change.Startup:
          ConfigureControls();

          // Load first List.
          DataRetrieveRegion();

          SetRegion(LJCRegionID, LJCRegionName);
          LJCRegionName = null;
          break;

        case Change.Region:
          RestoreControlValues();  // Region
          DataRetrieveProvince();

          SetProvince(LJCProvinceID, LJCProvinceCode);
          LJCProvinceCode = null;
          break;

        case Change.Province:
          DataRetrieveCity();
          ProvinceGrid.LJCSetLastRow();
          ProvinceGrid.LJCSetCounter(ProvinceCounter);

          SetCity(LJCCityID, LJCCityName);
          LJCCityName = null;
          break;

        case Change.City:
          DataRetrieveCitySection();
          CityGrid.LJCSetLastRow();
          CityGrid.LJCSetCounter(CityCounter);

          SetCitySection(LJCCitySectionID, LJCCitySectionName);
          LJCCitySectionName = null;
          break;

        case Change.CitySection:
          CitySectionGrid.LJCSetLastRow();
          CitySectionGrid.LJCSetCounter(CitySectionCounter);
          break;
      }
      SetControlState();
      Cursor = Cursors.Default;
    }

    // The ChangeType values.
    internal enum Change
    {
      Startup,
      Region,
      Province,
      City,
      CitySection
    }

    #region Item Change Support

    // Start the Change processing.
    private void StartItemChange()
    {
      ChangeTimer = new ChangeTimer();
      ChangeTimer.ItemChange += ChangeTimer_ItemChange;
      TimedChange(Change.Startup);
    }

    // Change Event Handler
    private void ChangeTimer_ItemChange(object sender, EventArgs e)
    {
      Change changeType;

      changeType = (Change)Enum.Parse(typeof(Change)
        , ChangeTimer.ChangeName);
      DoChange(changeType);
    }

    // Starts the Timer with the Change value.
    internal void TimedChange(Change change)
    {
      ChangeTimer.DoChange(change.ToString());
    }

    // Gets or sets the ChangeTimer object.
    internal ChangeTimer ChangeTimer { get; set; }
    #endregion
    #endregion

    #region Private Methods

    // Sets the control states based on the current control values.
    private void SetControlState()
    {
      bool enableNew;
      bool enableEdit;

      enableNew = RegionCombo.SelectedIndex != -1;
      enableEdit = ProvinceGrid.CurrentRow != null;
      FormCommon.SetToolState(ProvinceTool, enableNew, enableEdit);
      FormCommon.SetMenuState(ProvinceMenu, enableNew, enableEdit);
      ProvinceFileEdit.Enabled = true;
      ProvinceMenuHelp.Enabled = enableNew;

      enableNew = ProvinceGrid.CurrentRow != null;
      enableEdit = CityGrid.CurrentRow != null;
      FormCommon.SetToolState(CityTool, enableNew, enableEdit);
      FormCommon.SetMenuState(CityMenu, enableNew, enableEdit);
      CityMenuHelp.Enabled = enableNew;

      enableNew = CityGrid.CurrentRow != null;
      enableEdit = CitySectionGrid.CurrentRow != null;
      FormCommon.SetToolState(CitySectionTool, enableNew, enableEdit);
      FormCommon.SetMenuState(CitySectionMenu, enableNew, enableEdit);
      CitySectionMenuHelp.Enabled = enableNew;
    }
    #endregion

    #region Action Event Handlers

    #region Province

    // Call the New method.
    private void ProvinceToolNew_Click(object sender, EventArgs e)
    {
      DoNewProvince();
    }

    // Call the Edit method.
    private void ProvinceToolEdit_Click(object sender, EventArgs e)
    {
      DoEditProvince();
    }

    // Call the Delete method.
    private void ProvinceToolDelete_Click(object sender, EventArgs e)
    {
      DoDeleteProvince();
    }

    // Call the Select method.
    private void ProvinceToolSelect_Click(object sender, EventArgs e)
    {
      DoSelectProvince();
    }

    // Call the New method.
    private void ProvinceMenuNew_Click(object sender, EventArgs e)
    {
      DoNewProvince();
    }

    // Call the Edit method.
    private void ProvinceMenuEdit_Click(object sender, EventArgs e)
    {
      DoEditProvince();
    }

    // Call the Delete method.
    private void ProvinceMenuDelete_Click(object sender, EventArgs e)
    {
      DoDeleteProvince();
    }

    // Call the Refresh method.
    private void ProvinceMenuRefresh_Click(object sender, EventArgs e)
    {
      DoRefreshProvince();
    }

    // Allows display and edit of text file.
    private void ProvinceFileEdit_Click(object sender, EventArgs e)
    {
      FormCommon.ShellFile("NotePad.exe");
    }

    // Call the Select method.
    private void ProvinceMenuSelect_Click(object sender, EventArgs e)
    {
      DoSelectProvince();
    }

    // Perform the Close function.
    private void ProvinceMenuExit_Click(object sender, EventArgs e)
    {
      //LJCRegionID = 0;
      LJCSelectedRegion = null;
      SaveControlValues();
      ClosePage(RegionPage);
    }

    // Show the help page.
    private void ProvinceMenuHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"Province\ProvinceList.html");
    }
    #endregion

    #region City

    // Call the New method.
    private void CityToolNew_Click(object sender, EventArgs e)
    {
      DoNewCity();
    }

    // Call the Edit method.
    private void CityToolEdit_Click(object sender, EventArgs e)
    {
      DoEditCity();
    }

    // Call the Delete method.
    private void CityToolDelete_Click(object sender, EventArgs e)
    {
      DoDeleteCity();
    }

    // Call the Select method.
    private void CityToolSelect_Click(object sender, EventArgs e)
    {
      DoSelectCity();
    }

    // Call the New method.
    private void CityMenuNew_Click(object sender, EventArgs e)
    {
      DoNewCity();
    }

    // Call the Edit method.
    private void CityMenuEdit_Click(object sender, EventArgs e)
    {
      DoEditCity();
    }

    // Call the Delete method.
    private void CityMenuDelete_Click(object sender, EventArgs e)
    {
      DoDeleteCity();
    }

    // Call the Refresh method.
    private void CityMenuRefresh_Click(object sender, EventArgs e)
    {
      DoRefreshCity();
    }

    // Call the Select method.
    private void CityMenuSelect_Click(object sender, EventArgs e)
    {
      DoSelectCity();
    }

    // Show the help page.
    private void CityMenuHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"City\CityList.html");
    }
    #endregion

    #region CitySection

    // Call the New method.
    private void CitySectionToolNew_Click(object sender, EventArgs e)
    {
      DoNewCitySection();
    }

    // Call the Edit method.
    private void CitySectionToolEdit_Click(object sender, EventArgs e)
    {
      DoEditCitySection();
    }

    // Call the Delete method.
    private void CitySectionToolDelete_Click(object sender, EventArgs e)
    {
      DoDeleteCitySection();
    }

    // Call the Select method.
    private void CitySectionToolSelect_Click(object sender, EventArgs e)
    {
      DoSelectCitySection();
    }

    // Call the New method.
    private void CitySectionNew_Click(object sender, EventArgs e)
    {
      DoNewCitySection();
    }

    // Call the Edit method.
    private void CitySectionEdit_Click(object sender, EventArgs e)
    {
      DoEditCitySection();
    }

    // Call the Delete method.
    private void CitySectionDelete_Click(object sender, EventArgs e)
    {
      DoDeleteCitySection();
    }

    // Call the Refresh method.
    private void CitySectionRefresh_Click(object sender, EventArgs e)
    {
      DoRefreshCitySection();
    }

    // Call the Select method.
    private void CitySectionSelect_Click(object sender, EventArgs e)
    {
      DoSelectCitySection();
    }

    // Show the help page.
    private void CitySectionMenuHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"CitySection\CitySectionList.html");
    }
    #endregion
    #endregion

    #region Control Event Handlers

    #region Region

    // Handles the SelectedIndexChanged event.
    private void RegionCombo_SelectedIndexChanged(object sender, EventArgs e)
    {
      LJCSelectedRegion = RegionCombo.SelectedItem as RegionData;
      TimedChange(Change.Region);
    }
    #endregion

    #region Province

    // Handles the form keys.
    private void ProvinceGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          DoEditProvince();
          e.Handled = true;
          break;

        case Keys.F1:
          Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
            , "ProvinceList.html");
          e.Handled = true;
          break;

        case Keys.F5:
          DoRefreshProvince();
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(ProvinceGrid
              , MousePosition);
            ProvinceMenu.Show(position);
            ProvinceMenu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            ProvinceTool.Select();
          }
          else
          {
            CityTool.Select();
          }
          e.Handled = true;
          break;

        case Keys.F10:
          ProvinceMenu.Show(MousePosition);
          ProvinceMenu.Select();
          break;
      }
    }

    // Handles the MouseDown event.
    private void ProvinceGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right
        && ProvinceGrid.LJCIsDifferentRow(e))
      {
        // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
        ProvinceGrid.LJCSetCurrentRow(e);
        TimedChange(Change.Province);
      }
    }

    // Handles the SelectionChanged event.
    private void ProvinceGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (ProvinceGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.Province);
      }
      ProvinceGrid.LJCAllowSelectionChange = true;
    }

    // Handles the MouseDoubleClick event.
    private void ProvinceGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (ProvinceGrid.LJCGetMouseRow(e) != null)
      {
        DoDefaultProvince();
      }
    }
    #endregion

    #region City

    // Handles the ButtonClick event.
    private void CityFindButton_Click(object sender, EventArgs e)
    {
      DoFindCity();
    }

    // Handles the Grid keys.
    private void CityGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          DoEditCity();
          e.Handled = true;
          break;

        case Keys.F1:
          Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
            , "CityList.html");
          e.Handled = true;
          break;

        case Keys.F5:
          DoRefreshCity();
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(CityGrid
              , MousePosition);
            CityMenu.Show(position);
            CityMenu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            CityTool.Select();
          }
          else
          {
            CitySectionTool.Select();
          }
          e.Handled = true;
          break;

        case Keys.F10:
          CityMenu.Show(MousePosition);
          CityMenu.Select();
          break;
      }
    }

    // Handles the MouseDown event.
    private void CityGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right
        && CityGrid.LJCIsDifferentRow(e))
      {
        // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
        CityGrid.LJCSetCurrentRow(e);
        TimedChange(Change.City);
      }
    }

    // Handles the SelectionChanged event.
    private void CityGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (CityGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.City);
      }
      CityGrid.LJCAllowSelectionChange = true;
    }

    // Handles the MouseDoubleClick event.
    private void CityGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (CityGrid.LJCGetMouseRow(e) != null)
      {
        DoDefaultCity();
      }
    }
    #endregion

    #region CitySection

    // Handles the ButtonClick event.
    private void CitySectionFindButton_Click(object sender, EventArgs e)
    {
      DoFindCitySection();
    }

    // Handles the Grid keys.
    private void CitySectionGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          DoEditCitySection();
          e.Handled = true;
          break;

        case Keys.F1:
          Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
            , "CitySectionList.html");
          e.Handled = true;
          break;

        case Keys.F5:
          DoRefreshCitySection();
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(CitySectionGrid
              , MousePosition);
            CitySectionMenu.Show(position);
            CitySectionMenu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            CitySectionTool.Select();
          }
          else
          {
            RegionCombo.Select();
          }
          e.Handled = true;
          break;

        case Keys.F10:
          CitySectionMenu.Show(MousePosition);
          CitySectionMenu.Select();
          break;
      }
    }

    // Handles the MouseDown event.
    private void CitySectionGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right
        && CitySectionGrid.LJCIsDifferentRow(e))
      {
        // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
        CitySectionGrid.LJCSetCurrentRow(e);
        TimedChange(Change.CitySection);
      }
    }

    // Handles the SelectionChanged event.
    private void CitySectionGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (CitySectionGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.CitySection);
      }
      CitySectionGrid.LJCAllowSelectionChange = true;
    }

    // Handles the MouseDoubleClick event.
    private void CitySectionGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (CitySectionGrid.LJCGetMouseRow(e) != null)
      {
        DoDefaultCitySection();
      }
    }
    #endregion
    #endregion

    #region Properties

    /// <summary>Gets or sets the LJCIsSelect value.</summary>
    public bool LJCIsSelect { get; set; }

    /// <summary>Gets a reference to the selected record.</summary>
    public RegionData LJCSelectedRegion { get; private set; }

    /// <summary>Gets a reference to the selected record.</summary>
    public Province LJCSelectedProvince { get; private set; }

    /// <summary>Gets a reference to the selected record.</summary>
    public City LJCSelectedCity { get; private set; }

    /// <summary>Gets a reference to the selected record.</summary>
    public CitySection LJCSelectedCitySection { get; private set; }

    /// <summary> The help file name.</summary>
    public string LJCHelpFile { get; set; }

    /// <summary>The startup RegionID value.</summary>
    public int LJCRegionID { get; set; }

    /// <summary>The startup ProvinceID value.</summary>
    public int LJCProvinceID { get; set; }

    /// <summary>The startup CityID value.</summary>
    public int LJCCityID { get; set; }

    /// <summary>The startup CitySectionID value.</summary>
    public int LJCCitySectionID { get; set; }

    /// <summary>The startup RegionName value.</summary>
    public string LJCRegionName { get; set; }

    /// <summary>The startup ProvinceCode value.</summary>
    public string LJCProvinceCode { get; set; }

    /// <summary>The startup CityName value.</summary>
    public string LJCCityName { get; set; }

    /// <summary>The startup CitySectionName value.</summary>
    public string LJCCitySectionName { get; set; }

    // The Managers object.
    internal RegionManagers Managers { get; set; }
    #endregion

    #region Class Data

    private StandardUISettings mSettings;
    private string mControlValuesFileName;
    #endregion
  }
}
