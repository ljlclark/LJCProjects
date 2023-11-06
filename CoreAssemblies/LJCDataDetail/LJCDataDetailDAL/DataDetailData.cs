﻿// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataDetailData.cs
using LJCDataDetailDAL;
using LJCDBClientLib;
using LJCDBDataAccess;
using LJCNetCommon;
using System.Collections.Generic;

namespace LJCDataDetailDAL
{
  /// <summary>Contains methods for using DataDetail data.</summary>
  public class DataDetailData
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DataDetailDataC/*' file='Doc/DataDetailData.xml'/>
    public DataDetailData(string dataConfigName, string tableName
      , string userID = null)
    {
      // Initialize property values.
      DataConfigName = dataConfigName;
      DbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName)
      };
      Managers = new DataDetailManagers();
      Managers.SetDBProperties(DbServiceRef, dataConfigName);
      TableName = tableName;
      UserID = userID;
    }
    #endregion

    #region ControlDetail

    // Gets the ControlDetail data.
    /// <include path='items/GetControlDetail/*' file='Doc/DataDetailData.xml'/>
    public ControlDetail GetControlDetail()
    {
      ControlDetail retValue;

      var configManager = Managers.ControlDetailManager;
      retValue = configManager.RetrieveWithUniqueTable(UserID
        , DataConfigName, TableName);

      if (null == retValue)
      {
        retValue = DefaultControlDetail();
      }
      else
      {
        // Load ControlTabItems.
        var tabManager = Managers.ControlTabManager;
        var controlTabItems = tabManager.LoadWithParentID(retValue.ID);
        if (NetCommon.HasItems(controlTabItems))
        {
          retValue.ControlTabItems = controlTabItems;
          var columnManager = Managers.ControlColumnManager;
          var rowManager = Managers.ControlRowManager;
          foreach (ControlTab controlTab in retValue.ControlTabItems)
          {
            // Load ControlColumns.
            var controlColumns = columnManager.LoadWithParentID(controlTab.ID);
            if (NetCommon.HasItems(controlColumns))
            {
              controlTab.ControlColumns = controlColumns;
              foreach (ControlColumn controlColumn in controlColumns)
              {
                // Load ControlRows
                controlColumn.ControlRows
                  = rowManager.LoadWithParentID(controlColumn.ID);
              }
            }
          }
        }
      }
      return retValue;
    }

    // Updates the ControlDetail data object.
    /// <include path='items/UpdateControlDetail/*' file='Doc/DataDetailData.xml'/>
    public int UpdateControlDetail(ControlDetail dataObject
      , DbColumns keyColumns = null)
    {
      int retValue = 0;

      var manager = Managers.ControlDetailManager;
      if (null == keyColumns
        && dataObject.ID > 0)
      {
        keyColumns = manager.GetIDKey(dataObject.ID);
      }
      if (keyColumns != null)
      {
        manager.Update(dataObject, keyColumns);
        retValue = manager.AffectedCount;
      }
      return retValue;
    }

    // Creates the Default ControlDetail data.
    private ControlDetail DefaultControlDetail()
    {
      ControlDetail retValue;

      // Set default values.
      retValue = new ControlDetail()
      {
        Name = $"Detail{TableName}Standard",
        Description = $"{TableName} Detail Standard",
        DataConfigName = DataConfigName,
        TableName = TableName,
        UserID = UserID,
        BorderHorizontal = 5,
        BorderVertical = 8,
        CharacterPixels = 6,
        ColumnRowsLimit = 8,
        ControlRowHeight = 21,
        ControlRowSpacing = 5,
        ContentHeight = 21 + (8 * 2),
        DataValueCount = 1,
        MaxControlCharacters = 40,
        PageColumnsLimit = 2,
      };
      retValue.ColumnRowCount = retValue.ColumnRowsLimit;

      // Add DetailConfig with default data.
      var addedRecord = Managers.ControlDetailManager.Add(retValue);
      if (addedRecord != null)
      {
        retValue.ID = addedRecord.ID;
      }
      return retValue;
    }
    #endregion

    #region ControlTab

    // Adds the ControlColumn data object.
    /// <include path='items/AddControlTab/*' file='Doc/DataDetailData.xml'/>
    public void AddControlTab(ControlTab dataObject
      , List<string> propertyNames = null)
    {
      var manager = Managers.ControlTabManager;
      var addedItem = manager.Add(dataObject, propertyNames);
      if (addedItem != null)
      {
        dataObject.ID = addedItem.ID;
      }
    }

    // Gets the ControlTab data object.
    /// <include path='items/SetControlTab/*' file='Doc/DataDetailData.xml'/>
    public ControlTab SetControlTab(ControlTab dataObject)
    {
      ControlTab retValue;

      var manager = Managers.ControlTabManager;
      retValue = manager.RetrieveWithUnique(dataObject.ControlDetailID
        , dataObject.TabIndex);
      if (null == retValue)
      {
        retValue = dataObject;
        AddControlTab(retValue);
      }
      return retValue;
    }

    // Updates the ControlTab data object.
    /// <include path='items/UpdateControlTab/*' file='Doc/DataDetailData.xml'/>
    public int UpdateControlTab(ControlTab dataObject
      , DbColumns keyColumns = null)
    {
      int retValue = 0;

      var manager = Managers.ControlTabManager;
      if (null == keyColumns
        && dataObject.ID > 0)
      {
        keyColumns = manager.GetIDKey(dataObject.ID);
      }
      if (keyColumns != null)
      {
        manager.Update(dataObject, keyColumns);
        retValue = manager.AffectedCount;
      }
      return retValue;
    }
    #endregion

    #region ControlColumn

    // Adds the ControlColumn data object.
    /// <include path='items/AddControlColumn/*' file='Doc/DataDetailData.xml'/>
    public void AddControlColumn(ControlColumn dataObject
      , List<string> propertyNames = null)
    {
      var manager = Managers.ControlColumnManager;
      var addedItem = manager.Add(dataObject, propertyNames);
      if (addedItem != null)
      {
        dataObject.ID = addedItem.ID;
      }
    }

    // Gets the ControlColumn data object.
    /// <include path='items/SetControlColumn/*' file='Doc/DataDetailData.xml'/>
    public ControlColumn SetControlColumn(ControlColumn dataObject)
    {
      ControlColumn retValue;

      var manager = Managers.ControlColumnManager;
      retValue = manager.RetrieveWithUnique(dataObject.ControlTabID
        , dataObject.ColumnIndex);
      if (null == retValue)
      {
        retValue = dataObject;
        AddControlColumn(retValue);
      }
      return retValue;
    }

    // Updates the ControlColumn data object.
    /// <include path='items/UpdateControlColumn/*' file='Doc/DataDetailData.xml'/>
    public int UpdateControlColumn(ControlColumn dataObject
      , DbColumns keyColumns = null)
    {
      int retValue = 0;

      var manager = Managers.ControlColumnManager;
      if (null == keyColumns
        && dataObject.ID > 0)
      {
        keyColumns = manager.GetIDKey(dataObject.ID);
      }
      if (keyColumns != null)
      {
        manager.Update(dataObject, keyColumns);
        retValue = manager.AffectedCount;
      }
      return retValue;
    }
    #endregion

    #region ControlRow

    // Adds the ControlRow data object.
    /// <include path='items/AddControlRow/*' file='Doc/DataDetailData.xml'/>
    public void AddControlRow(ControlRow dataObject
      , List<string> propertyNames = null)
    {
      var manager = Managers.ControlRowManager;
      var addedItem = manager.Add(dataObject, propertyNames);
      if (addedItem != null)
      {
        dataObject.ID = addedItem.ID;
      }
    }

    // Updates the ControlRow data object.
    /// <include path='items/UpdateControlRow/*' file='Doc/DataDetailData.xml'/>
    public int UpdateControlRow(ControlRow dataObject
      , DbColumns keyColumns = null)
    {
      int retValue = 0;

      var manager = Managers.ControlRowManager;
      if (null == keyColumns
        && dataObject.ID > 0)
      {
        keyColumns = manager.GetIDKey(dataObject.ID);
      }
      if (keyColumns != null)
      {
        manager.Update(dataObject, keyColumns);
        retValue = manager.AffectedCount;
      }
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the DataConfig Name.</summary>
    public string DataConfigName { get; set; }

    /// <summary>Gets or sets DbServiceRef.</summary>
    public DbServiceRef DbServiceRef { get; set; }

    /// <summary>Gets or sets DataDetailManagers.</summary>
    public DataDetailManagers Managers { get; set; }

    /// <summary>Gets or sets the Table Name.</summary>
    public string TableName { get; set; }

    /// <summary>Gets or sets the User ID.</summary>
    public string UserID { get; set; }
    #endregion
  }
}
