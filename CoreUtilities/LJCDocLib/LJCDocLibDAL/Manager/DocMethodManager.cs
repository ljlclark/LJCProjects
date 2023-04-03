// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DocMethodManager.cs
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using System.Collections.Generic;

namespace LJCDocLibDAL
{
  /// <summary>Provides table specific data methods.</summary>
  public class DocMethodManager
  {
    // Initializes an object instance.
    /// <include path='items/DataManagerC/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DocMethodManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "DocMethod", string schemaName = null)
    {
      Manager = new DataManager(dbServiceRef, dataConfigName, tableName
        , schemaName);
      ResultConverter = new ResultConverter<DocMethod, DocMethods>();

      // Map table names with property names or captions
      // that differ from the column names.
      Manager.MapNames(DocMethod.ColumnID, caption: "DocMethod ID");

      // Create the list of database assigned columns.
      Manager.SetDbAssignedColumns(new string[]
      {
        DocMethod.ColumnID
      });

      // Create the list of lookup column names.
      Manager.SetLookupColumns(new string[]
      {
        DocMethod.ColumnName
      });
    }

    #region Data Methods

    // Adds a record to the database.
    /// <include path='items/Add/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DocMethod Add(DocMethod dataObject
      , List<string> propertyNames = null)
    {
      DocMethod retValue;

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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="propertyNames"></param>
    /// <returns></returns>
    public DbColumns GetColumns(List<string> propertyNames)
    {
      return Manager.DataDefinition.LJCGetColumns(propertyNames);
    }

    // Retrieves a collection of data records.
    /// <include path='items/Load/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DocMethods Load(DbColumns keyColumns = null
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      DocMethods retValue;

      var dbResult = Manager.Load(keyColumns, propertyNames, filters, joins);
      retValue = ResultConverter.CreateCollection(dbResult);
      return retValue;
    }

    // Retrieves a record from the database.
    /// <include path='items/Retrieve/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DocMethod Retrieve(DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      DocMethod retValue;

      var dbResult = Manager.Retrieve(keyColumns, propertyNames, filters, joins);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Updates the record.
    /// <include path='items/Update/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public void Update(DocMethod dataObject, DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null)
    {
      Manager.Update(dataObject, keyColumns, propertyNames, filters);
    }
    #endregion

    #region Load/Retrieve Methods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="groupID"></param>
    /// <returns></returns>
    public DocMethods LoadWithGroup(short groupID)
    {
      var keyColumns = GetGroupKey(groupID);
      var dbResult = Manager.Load(keyColumns);
      var retValue = ResultConverter.CreateCollection(dbResult);
      return retValue;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="parentID"></param>
    /// <returns></returns>
    public DocMethods LoadWithParent(short parentID)
    {
      var keyColumns = GetGroupKey(parentID);
      var dbResult = Manager.Load(keyColumns);
      var retValue = ResultConverter.CreateCollection(dbResult);
      return retValue;
    }

    // Retrieves a record with the supplied value.
    /// <include path='items/RetrieveWithID/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DocMethod RetrieveWithID(int id, List<string> propertyNames = null)
    {
      DocMethod retValue;

      var keyColumns = GetIDKey(id);
      var dbResult = Manager.Retrieve(keyColumns, propertyNames);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Retrieves a record with the supplied unique values.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="classID"></param>
    /// <param name="name"></param>
    /// <param name="propertyNames"></param>
    /// <returns></returns>
    public DocMethod RetrieveWithUnique(short classID, string name
      , List<string> propertyNames = null)
    {
      DocMethod retValue;

      var keyColumns = GetUniqueKey(classID, name);
      var dbResult = Manager.Retrieve(keyColumns, propertyNames);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }
    #endregion

    #region GetKey Methods

    // Gets the ID key columns.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="groupID"></param>
    /// <returns></returns>
    public DbColumns GetGroupKey(short groupID)
    {
      // Needs cast for string to select the correct Add overload.
      var retValue = new DbColumns()
      {
        { DocMethod.ColumnDocMethodGroupID, groupID }
      };
      return retValue;
    }

    // Gets the ID key columns.
    /// <include path='items/GetIDKey/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DbColumns GetIDKey(int id)
    {
      // Add(columnName, propertyName = null, renameAs = null
      //   , datatypeName = "String", caption = null);
      // Add(columnName, object value, dataTypeName = "String");
      var retValue = new DbColumns()
      {
        { DocMethod.ColumnID, id }
      };
      return retValue;
    }

    // Gets the ID key columns.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="parentID"></param>
    /// <returns></returns>
    public DbColumns GetParentKey(short parentID)
    {
      // Needs cast for string to select the correct Add overload.
      var retValue = new DbColumns()
      {
        { DocMethod.ColumnDocClassID, parentID }
      };
      return retValue;
    }

    // Gets the ID key columns.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="classID"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public DbColumns GetUniqueKey(short classID, string name)
    {
      // Needs cast for string to select the correct Add overload.
      var retValue = new DbColumns()
      {
        { DocMethod.ColumnDocClassID, classID },
        { DocMethod.ColumnName, (object)name }
      };
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the DataManager reference.</summary>
    public DataManager Manager { get; set; }

    /// <summary>Gets or sets the ResultConverter reference.</summary>
    public ResultConverter<DocMethod, DocMethods> ResultConverter { get; set; }
    #endregion
  }
}
