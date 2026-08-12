// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DbGroupManager.cs
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using System.Collections.Generic;

namespace LJCDataSiteDAL
{
  /// <summary>Provides table specific data methods.</summary>
  public class DbGroupManager
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='members/DataManagerC/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DbGroupManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "DataSite", string schemaName = null)
    {
      Manager = new DataManager(dbServiceRef, dataConfigName, tableName
        , schemaName);
      ResultConverter = new ResultConverter<DbGroup, DbGroups>();

      // Create the list of database assigned columns.
      Manager.SetDbAssignedColumns(new string[]
      {
        DbGroup.ColumnID
      });

      // Create the list of lookup column names.
      Manager.SetLookupColumns(new string[]
      {
        DbGroup.ColumnName
      });

      DbID = RetrieveDbID();
    }
    #endregion

    #region Data Methods

    // Adds a record to the database.
    /// <include path='members/Add/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DbGroup Add(DbGroup dataObject
      , List<string> propertyNames = null)
    {
      DbGroup retValue;

      var dbResult = Manager.Add(dataObject, propertyNames);
      retValue = ResultConverter.CreateData(dbResult);
      if (retValue != null)
      {
        dataObject.ID = retValue.ID;
      }
      return retValue;
    }

    // Deletes the records with the specified key values.
    /// <include path='members/Delete/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public void Delete(LJCDataColumns keyColumns, DbFilters filters = null)
    {
      Manager.Delete(keyColumns, filters);
    }

    // Retrieves a collection of data records.
    /// <include path='members/Load/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DbGroups Load(LJCDataColumns keyColumns = null
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      DbGroups retValue;

      var dbResult = Manager.Load(keyColumns, propertyNames, filters, joins);
      retValue = ResultConverter.CreateCollection(dbResult);
      return retValue;
    }

    // Retrieves a record from the database.
    /// <include path='members/Retrieve/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DbGroup Retrieve(LJCDataColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      DbGroup retValue;

      var dbResult = Manager.Retrieve(keyColumns, propertyNames, filters
        , joins);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Updates the record.
    /// <include path='members/Update/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public void Update(DbGroup dataObject, LJCDataColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null)
    {
      Manager.Update(dataObject, keyColumns, propertyNames, filters);
    }
    #endregion

    #region Custom Data Methods

    /// <include file='Doc/DbGroupManager.xml'
    ///  path='members/RetrieveDbGroupID/*'/>
    public short RetrieveDbID()
    {
      short retValue = -1;

      var sql = "select DbID ";
      sql += "from DbGroupIdentifier ";
      sql += "where ID = 1;";
      DbResult result = Manager.ExecuteClientSql(RequestType.RetrieveSQL, sql);
      if (result.HasRows())
      {
        var row = result.Rows[0];
        retValue = row.Values.LJCGetInt16("DbID");
      }
      return retValue;
    }

    // Retrieves a record with the supplied value.
    /// <include path='members/RetrieveWithID/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DbGroup RetrieveWithID(long id, List<string> propertyNames = null)
    {
      DbGroup retValue;

      var keyColumns = IDKey(id);
      var dbResult = Manager.Retrieve(keyColumns, propertyNames);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Retrieves a record with the supplied unique values.
    /// <include path='members/RetrieveWithName/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DbGroup RetrieveWithName(string name
      , List<string> propertyNames = null)
    {
      DbGroup retValue;

      var keyColumns = NameKey(name);
      var dbResult = Manager.Retrieve(keyColumns, propertyNames);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }
    #endregion

    #region GetKey Methods

    // Gets the ID key columns.
    /// <include path='members/IDKey/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public LJCDataColumns IDKey(long id)
    {
      // Add(columnName, propertyName = null, renameAs = null
      //   , datatypeName = "String", caption = null);
      // Add(columnName, object value, dataTypeName = "String");
      var retValue = new LJCDataColumns()
      {
        { DbGroup.ColumnID, id }
      };
      return retValue;
    }

    // Gets the ID key columns.
    /// <include path='members/NameKey/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public LJCDataColumns NameKey(string name)
    {
      // Needs cast for string to select the correct Add overload.
      var retValue = new LJCDataColumns()
      {
        { DbGroup.ColumnName, (object)name }
      };
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the DbGroup Identifier.</summary>
    public short DbID { get; set; }

    /// <summary>Gets or sets the DataManager reference.</summary>
    public DataManager Manager { get; set; }

    /// <summary>Gets or sets the (ORM) ResultConverter reference.</summary>
    public ResultConverter<DbGroup, DbGroups> ResultConverter { get; set; }
    #endregion
  }
}
