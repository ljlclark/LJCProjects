// DataDetailData.cs
using LJCDataDetailDAL;
using LJCDBClientLib;
using LJCDBDataAccess;
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
        if (controlTabItems != null && controlTabItems.Count > 0)
        {
          retValue.ControlTabItems = controlTabItems;
          var columnManager = Managers.ControlColumnManager;
          var rowManager = Managers.ControlRowManager;
          foreach (ControlTab controlTab in retValue.ControlTabItems)
          {
            // Load ControlColumns.
            controlTab.ControlColumns
              = columnManager.LoadWithParentID(controlTab.ID);

            foreach (ControlColumn controlColumn in controlTab.ControlColumns)
            {
              // Load ControlRows
              controlColumn.ControlRows
                = rowManager.LoadWithParentID(controlColumn.ID);
            }
          }
        }
      }
      return retValue;
    }

    // Updates the ControlDetail data object.
    /// <include path='items/UpdateControlDetail/*' file='Doc/DataDetailData.xml'/>
    public void UpdateControlDetail(ControlDetail dataObject)
    {
      if (dataObject.ID > 0)
      {
        var manager = Managers.ControlDetailManager;
        var keyColumns = manager.GetIDKey(dataObject.ID);
        manager.Update(dataObject, keyColumns);
      }
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
        var addedItem = manager.Add(dataObject);
        if (addedItem != null)
        {
          retValue.ID = addedItem.ID;
        }
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
