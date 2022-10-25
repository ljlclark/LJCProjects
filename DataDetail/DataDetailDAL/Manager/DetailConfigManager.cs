// DetailDialogManager.cs
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using System.Collections.Generic;

namespace DataDetailDAL
{
  /// <summary>Provides table specific data methods.</summary>
  public class DetailConfigManager
  {
    // Initializes an object instance.
    /// <include path='items/DataManagerC/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DetailConfigManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "DetailConfig", string schemaName = null)
    {
      Manager = new DataManager(dbServiceRef, dataConfigName, tableName
        , schemaName);
      ResultConverter = new ResultConverter<DetailConfig, DetailConfigs>();

      // Map table names with property names or captions
      // that differ from the column names.
      Manager.MapNames(DetailConfig.ColumnID, caption: "DetailConfig ID");

      // Create the list of database assigned columns.
      Manager.SetDbAssignedColumns(new string[]
      {
        DetailConfig.ColumnID
      });

      // Create the list of lookup column names.
      Manager.SetLookupColumns(new string[]
      {
        DetailConfig.ColumnName
      });
    }

    #region Data Methods

    // Adds a record to the database.
    /// <include path='items/Add/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DetailConfig Add(DetailConfig dataObject, List<string> propertyNames = null)
    {
      DetailConfig retValue;

      var dbResult = Manager.Add(dataObject, propertyNames);
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
    public DetailConfigs Load(DbColumns keyColumns = null
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      DetailConfigs retValue;

      var dbResult = Manager.Load(keyColumns, propertyNames, filters, joins);
      retValue = ResultConverter.CreateCollection(dbResult);
      return retValue;
    }

    // Retrieves a record from the database.
    /// <include path='items/Retrieve/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DetailConfig Retrieve(DbColumns keyColumns, List<string> propertyNames = null
      , DbFilters filters = null, DbJoins joins = null)
    {
      DetailConfig retValue;

      var dbResult = Manager.Retrieve(keyColumns, propertyNames, filters, joins);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Updates the record.
    /// <include path='items/Update/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public void Update(DetailConfig dataObject, DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null)
    {
      Manager.Update(dataObject, keyColumns, propertyNames, filters);
    }
    #endregion

    #region Retrieve/Load Methods

    // Retrieves a record with the supplied value.
    /// <include path='items/RetrieveWithID/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DetailConfig RetrieveWithID(long id, List<string> propertyNames = null)
    {
      DetailConfig retValue;

      var keyColumns = GetIDKey(id);
      var dbResult = Manager.Retrieve(keyColumns, propertyNames);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Retrieves a record with the supplied name value.
    /// <include path='items/RetrieveWithName/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DetailConfig RetrieveWithUnique(string name
      , List<string> propertyNames = null)
    {
      DetailConfig retValue;

      var keyColumns = GetUniqueKey(name);
      var dbResult = Manager.Retrieve(keyColumns, propertyNames);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Retrieves a record with the supplied name value.
    /// <include path='items/RetrieveWithUniqueTable/*' file='Doc/DetailConfigManager.xml'/>
    public DetailConfig RetrieveWithUniqueTable(string userID, string dataConfigName
      , string tableName, List<string> propertyNames = null)
    {
      DetailConfig retValue;

      var keyColumns = GetUniqueTableKey(userID, dataConfigName, tableName);
      var dbResult = Manager.Retrieve(keyColumns, propertyNames);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }
    #endregion

    #region GetKey Methods

    // Gets the ID key columns.
    /// <include path='items/GetIDKey/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DbColumns GetIDKey(long id)
    {
      // Add(columnName, propertyName = null, renameAs = null
      //   , datatypeName = "String", caption = null);
      // Add(columnName, object value, dataTypeName = "String");
      var retValue = new DbColumns()
      {
        { DetailConfig.ColumnID, id }
      };
      return retValue;
    }

    // Gets the ID key columns.
    /// <include path='items/GetNameKey/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DbColumns GetUniqueKey(string name)
    {
      // Needs cast to select the correct Add overload.
      var retValue = new DbColumns()
      {
        { DetailConfig.ColumnName, (object)name }
      };
      return retValue;
    }

    // Gets the ID key columns.
    /// <include path='items/GetUniqueTableKey/*' file='Doc/DetailConfigManager.xml'/>
    public DbColumns GetUniqueTableKey(string userID, string dataConfigName
      , string tableName)
    {
      // Needs cast to select the correct Add overload.
      var retValue = new DbColumns()
      {
        { DetailConfig.ColumnUserID, (object)userID },
        { DetailConfig.ColumnDataConfigName, (object)dataConfigName },
        { DetailConfig.ColumnTableName, (object)tableName }
      };
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the DataManager reference.</summary>
    public DataManager Manager { get; set; }

    /// <summary>Gets or sets the ResultConverter reference.</summary>
    public ResultConverter<DetailConfig, DetailConfigs> ResultConverter { get; set; }
    #endregion
  }
}
