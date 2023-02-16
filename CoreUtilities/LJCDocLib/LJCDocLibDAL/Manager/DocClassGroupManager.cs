// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DocClassGroupManager.cs
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using System.Collections.Generic;

namespace LJCDocLibDAL
{
  /// <summary>Provides table specific data methods.</summary>
  public class DocClassGroupManager
  {
    // Initializes an object instance.
    /// <include path='items/DataManagerC/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DocClassGroupManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "DocClassGroup", string schemaName = null)
    {
      Manager = new DataManager(dbServiceRef, dataConfigName, tableName
        , schemaName);
      ResultConverter = new ResultConverter<DocClassGroup, DocClassGroups>();

      // Map table names with property names or captions
      // that differ from the column names.
      Manager.MapNames(DocClassGroup.ColumnID, caption: "DocClassGroup ID");

      // Create the list of database assigned columns.
      Manager.SetDbAssignedColumns(new string[]
      {
        DocClassGroup.ColumnID
      });

      // Create the list of lookup column names.
      Manager.SetLookupColumns(new string[]
      {
        DocClassGroup.ColumnDocAssemblyID,
        DocClassGroup.ColumnDocClassGroupHeadingID
      });
    }

    #region Data Methods

    // Adds a record to the database.
    /// <include path='items/Add/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DocClassGroup Add(DocClassGroup dataObject
      , List<string> propertyNames = null)
    {
      DocClassGroup retValue;

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
    public DocClassGroups Load(DbColumns keyColumns = null
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      DocClassGroups retValue;

      var dbResult = Manager.Load(keyColumns, propertyNames, filters, joins);
      retValue = ResultConverter.CreateCollection(dbResult);
      return retValue;
    }

    // Retrieves a record from the database.
    /// <include path='items/Retrieve/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DocClassGroup Retrieve(DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      DocClassGroup retValue;

      var dbResult = Manager.Retrieve(keyColumns, propertyNames, filters, joins);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Updates the record.
    /// <include path='items/Update/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public void Update(DocClassGroup dataObject, DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null)
    {
      Manager.Update(dataObject, keyColumns, propertyNames, filters);
    }
    #endregion

    #region Load/Retrieve Methods

    // Retrieves a record with the supplied value.
    /// <include path='items/RetrieveWithID/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DocClassGroup RetrieveWithID(int id, List<string> propertyNames = null)
    {
      DocClassGroup retValue;

      var keyColumns = GetIDKey(id);
      var dbResult = Manager.Retrieve(keyColumns, propertyNames);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Retrieves a record with the supplied unique values.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="docAssemblyID"></param>
    /// <param name="docClassGroupHeadingID"></param>
    /// <param name="propertyNames"></param>
    /// <returns></returns>
    public DocClassGroup RetrieveWithUnique(short docAssemblyID
      , short docClassGroupHeadingID, List<string> propertyNames = null)
    {
      DocClassGroup retValue;

      var keyColumns = GetUniqueKey(docAssemblyID, docClassGroupHeadingID);
      var dbResult = Manager.Retrieve(keyColumns, propertyNames);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }
    #endregion

    #region GetKey Methods

    // Gets the ID key columns.
    /// <include path='items/GetIDKey/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DbColumns GetIDKey(int id)
    {
      // Add(columnName, propertyName = null, renameAs = null
      //   , datatypeName = "String", caption = null);
      // Add(columnName, object value, dataTypeName = "String");
      var retValue = new DbColumns()
      {
        { DocClassGroup.ColumnID, id }
      };
      return retValue;
    }

    // Gets the ID key columns.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="docAssemblyID"></param>
    /// <param name="docClassGroupHeadingID"></param>
    /// <returns></returns>
    public DbColumns GetUniqueKey(short docAssemblyID
      , short docClassGroupHeadingID)
    {
      // Needs cast for string to select the correct Add overload.
      var retValue = new DbColumns()
      {
        { DocClassGroup.ColumnDocAssemblyID, docAssemblyID },
        { DocClassGroup.ColumnDocClassGroupHeadingID, docClassGroupHeadingID}
      };
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the DataManager reference.</summary>
    public DataManager Manager { get; set; }

    /// <summary>Gets or sets the ResultConverter reference.</summary>
    public ResultConverter<DocClassGroup, DocClassGroups> ResultConverter { get; set; }
    #endregion
  }
}
