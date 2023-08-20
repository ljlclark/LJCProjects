// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ManagerSetup.cs
using LJCDBClientLib;
using LJCDBMessage;
using LJCGridDataLib;
using LJCNetCommon;
using LJCWinFormControls;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LJCGridDataTests
{
  // Represents the LJCDataGrid setup.
  internal class ManagerSetup
  {
    // Initializes the object instance.
    public ManagerSetup(LJCDataGrid ljcGrid, DataManager dataManager)
    {
      mLJCGrid = ljcGrid;
      mDataManager = dataManager;
    }

    // Creates the DbColumns for LJCGrid setup.
    public DbColumns CreateColumns()
    {
      DbColumns retValue = null;

      List<string> propertyNames = new List<string>()
      {
        "Name",
        "Description",
        "Abbreviation"
      };

      // *** Test Setting ***
      var managerSetupCase = ManagerSetupCase.FromAdd;
      switch (managerSetupCase)
      {
        case ManagerSetupCase.FromAdd:
          retValue = ColumnsFromAdd();
          break;

        case ManagerSetupCase.FromResult:
          retValue = ColumnsFromResult(propertyNames);
          break;

        case ManagerSetupCase.FromRequest:
          retValue = ColumnsFromRequest(propertyNames);
          break;

        case ManagerSetupCase.FromDataObject:
          retValue = ColumnsFromDataObject(propertyNames);
          break;
      }
      return retValue;
    }

    // Configure the Grid Columns using DbColumns.Add().
    private DbColumns ColumnsFromAdd()
    {
      DbColumns retValue = new DbColumns();
      retValue.Add("Name", caption: "Name Caption", maxLength: 60);
      retValue.Add("Description", null, maxLength: 100);
      retValue.Add("Abbreviation", null, maxLength: 15);
      return retValue;
    }

    // Configure the Grid Columns from the DbColumns definition.
    private DbColumns ColumnsFromResult(List<string> propertyNames)
    {
      DbColumns retValue = null;

      var dbResult = mDataManager.Load(propertyNames: propertyNames);
      if (DbResult.HasData(dbResult))
      {
        // Create the Grid column definitions.
        retValue = dbResult.Columns.LJCGetColumns(propertyNames);
      }
      return retValue;
    }

    // Configure the Grid Columns from the DbRequest object definition.
    private DbColumns ColumnsFromRequest(List<string> propertyNames)
    {
      DbColumns retValue;

      // Get a View Request.
      //var dbRequest = ViewHelper.GetViewRequest("TableName", "ViewDataName");

      // Or Create the Request.
      var dbRequest = new DbRequest()
      {
        Columns = mDataManager.DataDefinition,
        DataConfigName = mDataManager.DataConfigName,
        RequestTypeName = RequestType.Load.ToString(),
        TableName = mDataManager.TableName
      };

      // Create the Grid column definitions.
      retValue = TableData.GetGridColumns(dbRequest, propertyNames);
      return retValue;
    }

    // Configure the Grid Columns from the Data object properties.
    private DbColumns ColumnsFromDataObject(List<string> propertyNames)
    {
      DbColumns retValue;

      var dataObject = new Province();

      // Create the Grid column definitions.
      retValue = DbColumns.LJCGetColumns(dataObject, propertyNames);
      return retValue;
    }

    private readonly DataManager mDataManager;
    private readonly LJCDataGrid mLJCGrid;

    private enum ManagerSetupCase
    {
      FromAdd,
      FromResult,
      FromRequest,
      FromDataObject
    }
  }
}
