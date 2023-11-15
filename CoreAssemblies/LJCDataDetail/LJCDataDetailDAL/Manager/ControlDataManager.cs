// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ControlDataManager.cs
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using System.Collections.Generic;

namespace LJCDataDetailDAL
{
  /// <summary>Provides table specific data methods.</summary>
  public class ControlDataManager
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DataManagerC/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public ControlDataManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "ControlData", string schemaName = null)
    {
      Manager = new DataManager(dbServiceRef, dataConfigName, tableName
        , schemaName);
      ResultConverter = new ResultConverter<ControlData, ControlDataItems>();

      // Map table names with property names or captions
      // that differ from the column names.
      //Manager.MapNames(ControlDetail.ColumnID, caption: "ControlDetail ID");

      // Create the list of database assigned columns.
      Manager.SetDbAssignedColumns(new string[]
      {
        ControlData.ColumnID
      });

      // Create the list of lookup column names.
      Manager.SetLookupColumns(new string[]
      {
        ControlData.ColumnControlDetailID,
        DbColumn.ColumnPropertyName
      });
    }
    #endregion

    #region Data Methods

    // Adds a record to the database.
    /// <include path='items/Add/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public ControlData Add(ControlDetail dataObject, List<string> propertyNames = null)
    {
      ControlData retValue;

      var dbResult = Manager.Add(dataObject, propertyNames);
      retValue = ResultConverter.CreateData(dbResult);
      if (retValue != null)
      {
        dataObject.ID = retValue.ID;
      }
      return retValue;
    }

    // Deletes the records with the specified key values.
    /// <include path='items/Delete/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public void Delete(DbColumns keyColumns, DbFilters filters = null)
    {
      Manager.Delete(keyColumns, filters);
    }

    // Retrieves a collection of data records.
    /// <include path='items/Load/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public ControlDataItems Load(DbColumns keyColumns = null
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      ControlDataItems retValue;

      var dbResult = Manager.Load(keyColumns, propertyNames, filters, joins);
      retValue = ResultConverter.CreateCollection(dbResult);
      return retValue;
    }

    // Retrieves a record from the database.
    /// <include path='items/Retrieve/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public ControlData Retrieve(DbColumns keyColumns, List<string> propertyNames = null
      , DbFilters filters = null, DbJoins joins = null)
    {
      ControlData retValue;

      var dbResult = Manager.Retrieve(keyColumns, propertyNames, filters, joins);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Updates the record.
    /// <include path='items/Update/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public void Update(ControlData dataObject, DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null)
    {
      Manager.Update(dataObject, keyColumns, propertyNames, filters);
    }
    #endregion

    #region Custom Load/Retrieve Methods

    // Retrieves a record with the supplied value.
    /// <include path='items/RetrieveWithParentID/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public ControlDataItems LoadWithParentID(long id
      , List<string> propertyNames = null)
    {
      var keyColumns = GetParentKey(id);
      var retValue = Load(keyColumns, propertyNames);
      return retValue;
    }

    // Retrieves a record with the supplied value.
    /// <include path='items/RetrieveWithID/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public ControlData RetrieveWithID(long id, List<string> propertyNames = null)
    {
      var keyColumns = GetIDKey(id);
      var retValue = Retrieve(keyColumns, propertyNames);
      return retValue;
    }

    // Retrieves a record with the supplied value.
    /// <include path='items/RetrieveWithParentID/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public ControlData RetrieveWithParentID(long id, List<string> propertyNames = null)
    {
      var keyColumns = GetParentKey(id);
      var retValue = Retrieve(keyColumns, propertyNames);
      return retValue;
    }

    // Retrieves a record with the supplied values.
    /// <include path='items/RetrieveWithUnique/*' file='../Doc/ControlDataManager.xml'/>
    public ControlData RetrieveWithUnique(long controlDetailID, string propertyName
      , List<string> propertyNames = null)
    {
      var keyColumns = GetUniqueKey(controlDetailID, propertyName);
      var retValue = Retrieve(keyColumns, propertyNames);
      return retValue;
    }
    #endregion

    #region GetKey Methods

    // Gets the ID key columns.
    /// <include path='items/GetIDKey/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public DbColumns GetIDKey(long id)
    {
      // Add(columnName, propertyName = null, renameAs = null
      //   , datatypeName = "String", caption = null);
      // Add(columnName, object value, dataTypeName = "String");
      // Needs (object cast for string value to select correct Add overload.
      var retValue = new DbColumns()
      {
        { ControlData.ColumnID, id }
      };
      return retValue;
    }

    // Gets the ID key columns.
    /// <include path='items/GetParentKey/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public DbColumns GetParentKey(long controlDetailID)
    {
      var retValue = new DbColumns()
      {
        { ControlData.ColumnControlDetailID, controlDetailID }
      };
      return retValue;
    }

    // Gets the unique key columns.
    /// <include path='items/GetUniqueKey/*' file='../Doc/ControlDataManager.xml'/>
    public DbColumns GetUniqueKey(long controlDetailID, string propertyName)
    {
      var retValue = new DbColumns()
      {
        { ControlData.ColumnControlDetailID, controlDetailID },
        { DbColumn.ColumnPropertyName, propertyName }
      };
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the non-select affected record count.
    /// </summary>
    public int AffectedCount
    {
      get { return Manager.AffectedCount; }
    }

    /// <summary>Gets or sets the DataManager reference.</summary>
    public DataManager Manager { get; set; }

    /// <summary>Gets or sets the ResultConverter reference.</summary>
    public ResultConverter<ControlData, ControlDataItems> ResultConverter
    { get; set; }
    #endregion
  }
}
