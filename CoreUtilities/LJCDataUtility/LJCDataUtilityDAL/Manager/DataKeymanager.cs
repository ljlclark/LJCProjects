// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataKeyManager.cs
using LJCDataUtilityDAL;
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using System.Collections.Generic;

namespace LJCDataUtilityDAL
{
  /// <summary>Provides table specific data methods.</summary>
  public class DataKeyManager
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DataManagerC/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DataKeyManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "DataKey", string schemaName = null)
    {
      Manager = new DataManager(dbServiceRef, dataConfigName, tableName
        , schemaName);
      ResultConverter = new ResultConverter<DataKey, DataKeys>();

      // Map table names with property names or captions
      // that differ from the column names.
      Manager.MapNames(DataKey.ColumnID, caption: "DataKey ID");
      //Manager.MapNames(DataKey.ColumnSourceColumnName
      //  , DataKey.PropertySourceColumnNames, caption: "Columns");
      //Manager.MapNames(DataKey.ColumnTargetColumnName
      //  , DataKey.PropertyTargetColumnNames, caption: "Target Columns");

      // Add Calculated and Join columns.
      // Enables adding Calculated and Join columns to a grid configuration.

      // Create the list of database assigned columns.
      Manager.SetDbAssignedColumns(new string[]
      {
        DataKey.ColumnID
      });

      // Create the list of lookup column names.
      Manager.SetLookupColumns(new string[]
      {
        DataKey.ColumnDataTableID,
        DataKey.ColumnName
      });
    }
    #endregion

    #region Data Methods

    // Adds a record to the database.
    /// <include path='items/Add/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DataKey Add(DataKey dataObject
      , List<string> propertyNames = null, bool includeNull = false)
    {
      DataKey retValue;

      var dbResult = Manager.Add(dataObject, propertyNames
        , includeNull);
      retValue = ResultConverter.CreateData(dbResult);
      if (retValue != null)
      {
        dataObject.ID = retValue.ID;
      }
      return retValue;
    }

    // Deletes the records with the specified key values.
    /// <include path='items/Delete/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public void Delete(DbColumns keyColumns, DbFilters filters = null)
    {
      Manager.Delete(keyColumns, filters);
    }

    // Retrieves a collection of data records.
    /// <include path='items/Load/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DataKeys Load(DbColumns keyColumns = null
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      DataKeys retValue;

      if (null == joins)
      {
        joins = GetJoins();
      }
      var dbResult = Manager.Load(keyColumns, propertyNames, filters, joins);
      retValue = ResultConverter.CreateCollection(dbResult);
      return retValue;
    }

    // Retrieves a record from the database.
    /// <include path='items/Retrieve/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DataKey Retrieve(DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      DataKey retValue;

      if (null == joins)
      {
        joins = GetJoins();
      }
      var dbResult = Manager.Retrieve(keyColumns, propertyNames, filters, joins);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Updates the record.
    /// <include path='items/Update/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public void Update(DataKey dataObject, DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null)
    {
      Manager.Update(dataObject, keyColumns, propertyNames, filters);
    }

    // Creates a set of columns that match the supplied list.
    public DbColumns Columns(List<string> propertyNames)
    {
      return Manager.DataDefinition.LJCGetColumns(propertyNames);
    }

    // Creates a list of BaseDefinition property names.
    public List<string> PropertyNames()
    {
      return Manager.GetPropertyNames();
    }
    #endregion

    #region Load/Retrieve Methods

    // Retrieves a record with the supplied value.
    /// <include path='items/RetrieveWithID/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DataKey RetrieveWithID(int id, List<string> propertyNames = null)
    {
      DataKey retValue;

      var keyColumns = IDKey(id);
      var joins = GetJoins();
      var dbResult = Manager.Retrieve(keyColumns, propertyNames
        , joins: joins);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Retrieves a record with the supplied unique values.
    /// <include path='items/RetrieveWithName/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DataKey RetrieveWithUnique(int dataTableID, string name
      , List<string> propertyNames = null)
    {
      DataKey retValue;

      var joins = GetJoins();
      var keyColumns = UniqueKey(dataTableID, name);
      var dbResult = Manager.Retrieve(keyColumns, propertyNames
        , joins: joins);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }
    #endregion

    #region GetKey Methods

    // Gets the ID key columns.
    /// <include path='items/GetIDKey/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DbColumns IDKey(int id)
    {
      // Add(columnName, propertyName = null, renameAs = null
      //   , datatypeName = "String", caption = null);
      // Add(columnName, object value, dataTypeName = "String");
      var retValue = new DbColumns()
      {
        { DataKey.ColumnID, id }
      };
      return retValue;
    }

    // Gets the ID key columns.
    /// <include path='items/GetIDKey/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DbColumns ParentKey(int parentID)
    {
      var retValue = new DbColumns()
      {
        { DataKey.ColumnDataTableID, parentID }
      };
      return retValue;
    }

    // Gets the ID key columns.
    /// <include path='items/GetNameKey/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DbColumns UniqueKey(int dataTableID, string name)
    {
      // Needs cast for string to select the correct Add overload.
      var retValue = new DbColumns()
      {
        { DataKey.ColumnDataTableID, dataTableID },
        { DataKey.ColumnName, (object)name }
      };
      return retValue;
    }
    #endregion

    #region Joins

    // Creates and returns the Load Joins object.
    // *** Add *** 12/16/24
    /// <include path='items/GetLoadJoins/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DbJoins GetJoins()
    {
      DbJoins retValue = new DbJoins();

      // Note: JoinOn Columns must have properties in the DataObject
      // to receive the join values.
      // The RenameAs property is required if there is another table column
      // with the same name.
      // dbColumns.Add(string columnName, string propertyName = null
      //   , string renameAs = null, string dataTypeName = "String"
      //   , string caption = null) 

      // Example SQL additions
      // DataTable.Name
      //left join DataTable
      // on ((DataKey.TableID = DataTable.ID))

      DbJoin dbJoin;
      dbJoin = new DbJoin
      {
        TableName = "DataTable",
        JoinType = "left",
        JoinOns = new DbJoinOns()
        {
          { DataKey.ColumnDataTableID, DataUtilTable.ColumnID }
        },
        Columns = new DbColumns()
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

    /// <summary>Gets the affected record count.</summary>
    public int AffectedCount
    {
      get { return Manager.AffectedCount; }
    }

    /// <summary>Gets or sets the DataManager reference.</summary>
    public DataManager Manager { get; set; }

    /// <summary>Gets or sets the ResultConverter reference.</summary>
    public ResultConverter<DataKey, DataKeys> ResultConverter { get; set; }
    #endregion
  }
}
