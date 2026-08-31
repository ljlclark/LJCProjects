// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataKeyManager.cs
using LJCDBClientLib5;
using LJCDBMessage5;
using LJCNetCommon5;

namespace LJCDataUtilityDAL5
{
  // Provides table specific data methods.
  /// <include file='Doc/DataKeyManager.xml'
  ///  path='members/DataKeyManager/*'/>
  public class DataKeyManager
  {
    #region Static Methods

    // Check for duplicate unique key.
    /// <include file='../../LJCGenDoc5/Common/Manager.xml'
    ///  path='items/IsDuplicate/*'/>
    public static bool IsDuplicate(DataKey lookupRecord, DataKey currentRecord
      , bool isUpdate = false)
    {
      bool retValue = false;

      if (lookupRecord != null)
      {
        if (!isUpdate)
        {
          // Duplicate for "New" record that already exists.
          retValue = true;
        }
        else
        {
          // If not the current record.
          if (lookupRecord.DbId != currentRecord.DbId
            && lookupRecord.Id != currentRecord.Id)
          {
            // Duplicate for "Update" where unique key is modified.
            retValue = true;
          }
        }
      }
      return retValue;
    }
    #endregion


    #region Constructors

    // Initializes an object instance.
    /// <include file='../../LJCGenDoc5/Common/Manager.xml'
    ///  path='members/Constructor/*'/>
    public DataKeyManager()
    {
      Manager = null;
      ResultConverter = new LJCResultConverter<DataKey, DataKeys>();
      //EntryManager = null;
    }

    // Initializes an object instance with the supplied values.
    /// <include file='Doc/DataKeyManager.xml'
    ///  path='members/ParamConstructor/*'/>
    public DataKeyManager(LJCDbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "DataKey", string? schemaName = null) : this()
    {
      Manager = new LJCDataManager(dbServiceRef, dataConfigName, tableName
        , schemaName);

      // Map table names with property names or captions
      // that differ from the column names.
      Manager.MapNames(DataKey.ColumnId, caption: "DataKey ID");
      //Manager.MapNames(DataKey.ColumnSourceColumnName
      //  , DataKey.PropertySourceColumnNames, caption: "Columns");
      //Manager.MapNames(DataKey.ColumnTargetColumnName
      //  , DataKey.PropertyTargetColumnNames, caption: "Target Columns");


      // Add Calculated and Join columns.
      // Enables adding Calculated and Join columns to a grid configuration.
      var propertyName = "TableName";
      Manager.DataDefinition.Add(DataUtilTable.ColumnName
        , propertyName, propertyName, caption: "Table Name");

      // Create the list of database assigned columns.
      Manager.SetDbAssignedColumns(
      [
        DataKey.ColumnId
      ]);

      // Create the list of lookup column names.
      Manager.SetLookupColumns(
      [
        DataKey.ColumnDataTableId,
        DataKey.ColumnName
      ]);

      var values = ValuesDataUtility.Instance;
      //var ManagersDataSite = values.SiteManagers;
      //DbId = ManagersDataSite.DbGroupManager.DbId;
      DbId = values.DbGroupId;
      //EntryManager = ManagersDataSite.DataEntryManager;
    }
    #endregion

    #region Manager Methods

    // Creates a set of columns that match the supplied list.
    /// <include file='../../LJCGenDoc5/Common/Manager.xml'
    ///  path='members/Columns/*'/>
    public LJCDataColumns? Columns(List<string> propertyNames)
    {
      var retColumns = Manager?.DataDefinition;
      if (retColumns != null
        && LJC.HasListItems(propertyNames))
      {
        retColumns = Manager?.DataDefinition.LJCColumns(propertyNames);
      }
      return retColumns;
    }

    // Creates a list of BaseDefinition property names.
    /// <include file='../../LJCGenDoc5/Common/Manager.xml'
    ///  path='members/PropertyNames/*'/>
    public List<string>? PropertyNames()
    {
      return Manager?.GetPropertyNames();
    }
    #endregion

    #region Data Methods

    // Adds a Data Record to the database.
    /// <include file='Doc/DataKeyManager.xml'
    ///  path='members/Add/*'/>
    public DataKey? Add(DataKey dataObject
      , List<string>? propertyNames = null, bool includeNull = false)
    {
      DataKey? retValue = null;

      var dbResult = Manager?.Add(dataObject, propertyNames
        , includeNull);
      if (dbResult != null)
      {
        retValue = ResultConverter.CreateData(dbResult);
        if (retValue != null)
        {
          dataObject.Id = retValue.Id;
          //EntryManager.WriteDataEntry(Manager.SQLStatement);
        }
      }
      return retValue;
    }

    // Deletes the records with the specified key values.
    /// <include file='../../LJCGenDoc5/Common/Manager.xml'
    ///  path='members/Delete/*'/>
    public void Delete(LJCDataColumns keyColumns, LJCDBFilters? filters = null)
    {
      Manager?.Delete(keyColumns, filters);
      //EntryManager.WriteDataEntry(Manager.SQLStatement);
    }

    // Retrieves a collection of data records.
    /// <include file='../../LJCGenDoc5/Common/Manager.xml'
    ///  path='members/Load/*'/>
    public DataKeys? Load(LJCDataColumns? keyColumns = null
      , List<string>? propertyNames = null, LJCDBFilters? filters = null
      , LJCDBJoins? joins = null)
    {
      DataKeys? retValue = null;

      if (null == joins)
      {
        joins = GetJoins();
      }
      var dbResult = Manager?.Load(keyColumns, propertyNames, filters, joins);
      if (dbResult != null)
      {
        retValue = ResultConverter.CreateCollection(dbResult);
      }
      return retValue;
    }

    // Retrieves a record from the database.
    /// <include file='../../LJCGenDoc5/Common/Manager.xml'
    ///  path='members/Retrieve/*'/>
    public DataKey? Retrieve(LJCDataColumns keyColumns
      , List<string>? propertyNames = null, LJCDBFilters? filters = null
      , LJCDBJoins? joins = null)
    {
      DataKey? retValue = null;

      if (null == joins)
      {
        joins = GetJoins();
      }
      var dbResult = Manager?.Retrieve(keyColumns, propertyNames, filters, joins);
      if (dbResult != null)
      {
        retValue = ResultConverter.CreateData(dbResult);
      }
      return retValue;
    }

    // Updates the record.
    /// <include file='../../LJCGenDoc5/Common/Manager.xml'
    ///  path='members/Update/*'/>
    public void Update(DataKey dataObject, LJCDataColumns keyColumns
      , List<string>? propertyNames = null, LJCDBFilters? filters = null)
    {
      Manager?.Update(dataObject, keyColumns, propertyNames, filters);
      //EntryManager.WriteDataEntry(Manager.SQLStatement);
    }
    #endregion

    #region Custom Data Methods

    // Loads records with the supplied values.
    /// <include file='Doc/DataKeyManager.xml'
    ///  path='members/LoadWithForeign/*'/>
    public DataKeys? LoadWithForeign(string tableName
      , List<string>? propertyNames = null)
    {
      var keyColumns = ForeignKey(tableName);
      var retKeys = Load(keyColumns, propertyNames);
      return retKeys;
    }

    // Loads records with the supplied values.
    /// <include file='Doc/DataKeyManager.xml'
    ///  path='members/LoadWithParent/*'/>
    public DataKeys? LoadWithParent(short parentDbId, long parentId 
      , List<string>? propertyNames = null)
    {
      var keyColumns = ParentKey(parentDbId, parentId);
      var retKeys = Load(keyColumns, propertyNames);
      return retKeys;
    }

    // Loads records with the supplied values.
    /// <include file='Doc/DataKeyManager.xml'
    ///  path='members/LoadWithParentType/*'/>
    public DataKeys? LoadWithParentType(short parentDbId, long parentId
      , int keyType, List<string>? propertyNames = null)
    {
      var keyColumns = ParentTypeKey(parentDbId, parentId, keyType);
      var retKeys = Load(keyColumns, propertyNames);
      return retKeys;
    }

    // Loads records with the supplied values.
    /// <include file='Doc/DataKeyManager.xml'
    ///  path='members/LoadWithType/*'/>
    public DataKeys? LoadWithType(short parentDbId, long parentId, short keyType
      , List<string>? propertyNames = null)
    {
      var keyColumns = TypeKey(parentDbId, parentId, keyType);
      var retKeys = Load(keyColumns, propertyNames);
      return retKeys;
    }

    // Retrieves a record with the supplied value.
    /// <include file='Doc/DataKeyManager.xml'
    ///  path='members/RetrieveWithIds/*'/>
    public DataKey? RetrieveWithId(short dbId, long id
      , List<string>? propertyNames = null)
    {
      DataKey? retValue = null;

      var keyColumns = IdKey(dbId, id);
      var joins = GetJoins();
      var dbResult = Manager?.Retrieve(keyColumns, propertyNames
        , joins: joins);
      if (dbResult != null)
      {
        retValue = ResultConverter.CreateData(dbResult);
      }
      return retValue;
    }

    // Retrieves a record with the supplied values.
    /// <include file='Doc/DataKeyManager.xml'
    ///  path='members/RetrieveWithParentType/*'/>
    public DataKey? RetrieveWithParentType(short parentDbId, long parentId
      , short keyType, List<string>? propertyNames = null)
    {
      DataKey? retValue = null;

      var keyColumns = ParentTypeKey(parentDbId, parentId, keyType);
      var joins = GetJoins();
      var dbResult = Manager?.Retrieve(keyColumns, propertyNames
        , joins: joins);
      if (dbResult != null)
      {
        retValue = ResultConverter.CreateData(dbResult);
      }
      return retValue;
    }

    // Retrieves a record with the supplied unique values.
    /// <include file='Doc/DataKeyManager.xml'
    ///  path='members/RetrieveUnique/*'/>
    public DataKey? RetrieveUnique(short parentDbId, long parentId
      , string name, List<string>? propertyNames = null)
    {
      DataKey? retValue = null;

      var keyColumns = UniqueKey(parentDbId, parentId, name);
      var joins = GetJoins();
      var dbResult = Manager?.Retrieve(keyColumns, propertyNames
        , joins: joins);
      if (dbResult != null)
      {
        retValue = ResultConverter.CreateData(dbResult);
      }
      return retValue;
    }
    #endregion

    #region GetKey Methods

    // Gets the foreign key columns.
    /// <include file='Doc/DataKeyManager.xml'
    ///  path='members/ForeignKey/*'/>
    public static LJCDataColumns ForeignKey(string targetTableName)
    {
      // Add(columnName, object value, dataTypeName = "String");
      var foreignKeyType = 3;
      var retValue = new LJCDataColumns()
      {
        { DataKey.ColumnTargetTableName, targetTableName },
        { DataKey.ColumnKeyType, foreignKeyType },
      };
      return retValue;
    }

    // Gets the primary key columns.
    /// <include file='Doc/DataKeyManager.xml'
    ///  path='members/IdKeys/*'/>
    public static LJCDataColumns IdKey(short dbId, long id)
    {
      // Add(columnName, object value, dataTypeName = "String");
      var retValue = new LJCDataColumns()
      {
        { DataKey.ColumnId, id },
      };
      if (dbId > 0)
      {
        retValue.Add(DataKey.ColumnDbId, dbId);
      }
      return retValue;
    }

    // Gets the parent key columns.
    /// <include file='Doc/DataKeyManager.xml'
    ///  path='members/ParentKey/*'/>
    public static LJCDataColumns ParentKey(short parentDbId, long parentId)
    {
      // Add(columnName, object value, dataTypeName = "String");
      var retValue = new LJCDataColumns()
      {
        { DataKey.ColumnDataTableDbId, parentDbId },
        { DataKey.ColumnDataTableId, parentId },
      };
      return retValue;
    }

    // Gets the parent by type key columns.
    /// <include file='Doc/DataKeyManager.xml'
    ///  path='members/ParentTypeKey/*'/>
    public static LJCDataColumns ParentTypeKey(short parentDbId, long parentId
      , int keyType)
    {
      // Add(columnName, object value, dataTypeName = "String");
      var retValue = new LJCDataColumns()
      {
        { DataKey.ColumnDataTableDbId, parentDbId },
        { DataKey.ColumnDataTableId, parentId },
        { DataKey.ColumnKeyType, keyType },
      };
      return retValue;
    }

    // Gets the parent by type key columns.
    /// <include file='Doc/DataKeyManager.xml'
    ///  path='members/TypeKey/*'/>
    public static LJCDataColumns TypeKey(short parentDbId, long parentId, int keyType)
    {
      // Add(columnName, object value, dataTypeName = "String");
      var retValue = new LJCDataColumns()
      {
        { DataKey.ColumnDataTableDbId, parentDbId },
        { DataKey.ColumnDataTableId, parentId },
        { DataKey.ColumnKeyType, keyType },
      };
      return retValue;
    }

    // Gets the unique key columns.
    /// <include file='Doc/DataKeyManager.xml'
    ///  path='members/UniqueKey/*'/>
    public static LJCDataColumns UniqueKey(short parentDbId, long parentId
      , string name)
    {
      // Add(columnName, object value, dataTypeName = "String");
      // Needs cast for string to select the correct Add overload.
      var retValue = new LJCDataColumns()
      {
        { DataKey.ColumnDataTableDbId, parentDbId },
        { DataKey.ColumnDataTableId, parentId },
        { DataKey.ColumnName, name },
      };
      return retValue;
    }
    #endregion

    #region Joins

    // Creates and returns the Load Joins object.
    /// <include file='../../LJCGenDoc5/Common/Manager.xml'
    ///  path='members/GetLoadJoins/*'/>
    public static LJCDBJoins GetJoins()
    {
      LJCDBJoins retValue = [];

      // Note: JoinOn Columns must have properties in the DataObject
      // to receive the join values.
      // The RenameAs property is required if there is another table column
      // with the same name.
      // dbColumns.Add(string columnName, string propertyName = null
      //   , string renameAs = null, string dataTypeName = "String"
      //   , string caption = null) 
      // dbColumns.Add(columnName, object value, dataTypeName = "String");

      // Example SQL additions
      // //select
      //   JoinTable.Name as JoinTableName
      // //from MainTable
      // left join JoinTable
      //  on ((MainTable.ParentId = JoinTable.Id))

      LJCDBJoin dbJoin;
      dbJoin = new LJCDBJoin
      {
        TableName = "DataTable",
        JoinType = "left",
        JoinOns = new LJCDBJoinOns()
        {
          { DataKey.ColumnDataTableId, DataUtilTable.ColumnId }
        },
        Columns = new LJCDataColumns()
        {
          // columnName, propertyName = null, renameAs = null
          //   , dataTypeName = "String", caption = null
          { DataUtilTable.ColumnName, "DataTableName", "DataTableName" }
        }
      };
      retValue.Add(dbJoin);
      return retValue;
    }
    #endregion

    #region Properties

    // Gets the affected record count.
    /// <include file='Doc/DataKeyManager.xml'
    ///  path='members/AffectedCount/*'/>
    public int AffectedCount
    {
      get
      {
        var retValue = 0;
        if (Manager != null)
        {
          retValue = Manager.AffectedCount;
        }
        return retValue;
      }
    }

    // Gets or sets the Database ID.
    /// <include file='Doc/DataKeyManager.xml'
    ///  path='members/DbId/*'/>
    public short DbId { get; set; }

    // Gets or sets the DataManager reference.
    /// <include file='Doc/DataKeyManager.xml'
    ///  path='members/Manager/*'/>
    public LJCDataManager? Manager { get; set; }

    // Gets or sets the ResultConverter reference.
    /// <include file='Doc/DataKeyManager.xml'
    ///  path='members/ResultConverter/*'/>
    public LJCResultConverter<DataKey, DataKeys> ResultConverter { get; set; }

    //// Gets or sets the EntryManager reference.
    ///// <include file='Doc/DataKeyManager.xml'
    /////  path='members/EntryManager/*'/>
    //private DataEntryManager EntryManager { get; set; }
    #endregion
  }
}
