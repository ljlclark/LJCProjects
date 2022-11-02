// DetailDialogManager.cs
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using System.Collections.Generic;

namespace LJCDataDetailDAL
{
  /// <summary>Provides table specific data methods.</summary>
  public class ControlDetailManager
  {
    // Initializes an object instance.
    /// <include path='items/DataManagerC/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public ControlDetailManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "ControlDetail", string schemaName = null)
    {
      Manager = new DataManager(dbServiceRef, dataConfigName, tableName
        , schemaName);
      ResultConverter = new ResultConverter<ControlDetail, ControlDetails>();

      // Map table names with property names or captions
      // that differ from the column names.
      Manager.MapNames(ControlDetail.ColumnID, caption: "ControlDetail ID");
      Manager.MapNames(ControlDetail.ColumnContentHeight, "ContentHeight");

      // Create the list of database assigned columns.
      Manager.SetDbAssignedColumns(new string[]
      {
        ControlDetail.ColumnID
      });

      // Create the list of lookup column names.
      Manager.SetLookupColumns(new string[]
      {
        ControlDetail.ColumnName
      });
    }

    #region Data Methods

    // Adds a record to the database.
    /// <include path='items/Add/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public ControlDetail Add(ControlDetail dataObject, List<string> propertyNames = null)
    {
      ControlDetail retValue;

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
    public ControlDetails Load(DbColumns keyColumns = null
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      ControlDetails retValue;

      var dbResult = Manager.Load(keyColumns, propertyNames, filters, joins);
      retValue = ResultConverter.CreateCollection(dbResult);
      return retValue;
    }

    // Retrieves a record from the database.
    /// <include path='items/Retrieve/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public ControlDetail Retrieve(DbColumns keyColumns, List<string> propertyNames = null
      , DbFilters filters = null, DbJoins joins = null)
    {
      ControlDetail retValue;

      var dbResult = Manager.Retrieve(keyColumns, propertyNames, filters, joins);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Updates the record.
    /// <include path='items/Update/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public void Update(ControlDetail dataObject, DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null)
    {
      Manager.Update(dataObject, keyColumns, propertyNames, filters);
    }
    #endregion

    #region Load/Retrieve Methods

    // Retrieves a record with the supplied value.
    /// <include path='items/RetrieveWithID/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public ControlDetail RetrieveWithID(long id, List<string> propertyNames = null)
    {
      var keyColumns = GetIDKey(id);
      var retValue = Retrieve(keyColumns, propertyNames);
      return retValue;
    }

    // Retrieves a record with the supplied name value.
    /// <include path='items/RetrieveWithName/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public ControlDetail RetrieveWithUnique(string name
      , List<string> propertyNames = null)
    {
      var keyColumns = GetUniqueKey(name);
      var retValue = Retrieve(keyColumns, propertyNames);
      return retValue;
    }

    // Retrieves a record with the supplied name value.
    /// <include path='items/RetrieveWithUniqueTable/*' file='Doc/DetailConfigManager.xml'/>
    public ControlDetail RetrieveWithUniqueTable(string userID, string dataConfigName
      , string tableName, List<string> propertyNames = null)
    {
      var keyColumns = GetUniqueTableKey(userID, dataConfigName, tableName);
      var retValue = Retrieve(keyColumns, propertyNames);
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
      // Needs (object cast for string value to select correct Add overload.
      var retValue = new DbColumns()
      {
        { ControlDetail.ColumnID, id }
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
        { ControlDetail.ColumnName, (object)name }
      };
      return retValue;
    }

    // Gets the ID key columns.
    /// <include path='items/GetUniqueTableKey/*' file='Doc/DetailConfigManager.xml'/>
    public DbColumns GetUniqueTableKey(string userID, string dataConfigName
      , string tableName)
    {
      var retValue = new DbColumns()
      {
        { ControlDetail.ColumnUserID, (object)userID },
        { ControlDetail.ColumnDataConfigName, (object)dataConfigName },
        { ControlDetail.ColumnTableName, (object)tableName }
      };
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the DataManager reference.</summary>
    public DataManager Manager { get; set; }

    /// <summary>Gets or sets the ResultConverter reference.</summary>
    public ResultConverter<ControlDetail, ControlDetails> ResultConverter
    { get; set; }
    #endregion
  }
}
