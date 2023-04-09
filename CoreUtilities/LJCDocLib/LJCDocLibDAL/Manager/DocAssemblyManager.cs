// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DocAssemblyManager.cs
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using System.Collections.Generic;

namespace LJCDocLibDAL
{
  /// <summary>Provides table specific data methods.</summary>
  public class DocAssemblyManager
  {
    // Initializes an object instance.
    /// <include path='items/DataManagerC/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DocAssemblyManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "DocAssembly", string schemaName = null)
    {
      Manager = new DataManager(dbServiceRef, dataConfigName, tableName
        , schemaName);
      ResultConverter = new ResultConverter<DocAssembly, DocAssemblies>();

      // Map table names with property names or captions
      // that differ from the column names.
      Manager.MapNames(DocAssembly.ColumnID, caption: "DocAssembly ID");

      // Create the list of database assigned columns.
      Manager.SetDbAssignedColumns(new string[]
      {
        DocAssembly.ColumnID
      });

      // Create the list of lookup column names.
      Manager.SetLookupColumns(new string[]
      {
        DocAssembly.ColumnDocAssemblyGroupID,
        DocAssembly.ColumnName
      });
    }

    #region Data Methods

    // Adds a record to the database.
    /// <include path='items/Add/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DocAssembly Add(DocAssembly dataObject
      , List<string> propertyNames = null)
    {
      DocAssembly retValue;

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
    public DocAssemblies Load(DbColumns keyColumns = null
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      DocAssemblies retValue;

      var dbResult = Manager.Load(keyColumns, propertyNames, filters, joins);
      retValue = ResultConverter.CreateCollection(dbResult);
      return retValue;
    }

    // Retrieves a record from the database.
    /// <include path='items/Retrieve/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DocAssembly Retrieve(DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      DocAssembly retValue;

      var dbResult = Manager.Retrieve(keyColumns, propertyNames, filters, joins);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Updates the record.
    /// <include path='items/Update/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public void Update(DocAssembly dataObject, DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null)
    {
      Manager.Update(dataObject, keyColumns, propertyNames, filters);
    }
    #endregion

    #region Load/Retrieve Methods

    // Retrieves a collection of data records with the supplied values.
    /// <summary>
    /// Retrieves a collection of data records with the supplied values.
    /// </summary>
    /// <param name="parentID"></param>
    /// <param name="propertyNames"></param>
    /// <returns></returns>
    public DocAssemblies LoadWithParent(short parentID, List<string> propertyNames = null)
    {
      DocAssemblies retValue;

      var keyColumns = GetParentKey(parentID);
      var dbResult = Manager.Load(keyColumns, propertyNames);
      retValue = ResultConverter.CreateCollection(dbResult);
      return retValue;
    }

    // Retrieves a record with the supplied values.
    /// <include path='items/RetrieveWithID/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DocAssembly RetrieveWithID(int id, List<string> propertyNames = null)
    {
      var keyColumns = GetIDKey(id);
      var dbResult = Manager.Retrieve(keyColumns, propertyNames);
      var retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="propertyNames"></param>
    /// <returns></returns>
    public DocAssembly RetrieveWithName(string name
      , List<string> propertyNames = null)
    {
      var keyColumns = GetNameKey(name);
      var dbResult = Manager.Retrieve(keyColumns, propertyNames);
      var retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Retrieves a record with the supplied unique values.
    /// <summary>
    /// Retrieves a record with the supplied unique values.
    /// </summary>
    /// <param name="parentID"></param>
    /// <param name="name"></param>
    /// <param name="propertyNames"></param>
    /// <returns></returns>
    public DocAssembly RetrieveWithUnique(short parentID, string name
      , List<string> propertyNames = null)
    {
      var keyColumns = GetUniqueKey(parentID, name);
      var dbResult = Manager.Retrieve(keyColumns, propertyNames);
      var retValue = ResultConverter.CreateData(dbResult);
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
        { DocAssembly.ColumnID, id }
      };
      return retValue;
    }

    /// <summary>
    /// Gets the Name key columns.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public DbColumns GetNameKey(string name)
    {
      // Needs cast for string to select the correct Add overload.
      var retValue = new DbColumns()
      {
        { DocAssembly.ColumnName, (object)name }
      };
      return retValue;
    }

    // Gets the Parent key columns.
    /// <summary>
    /// Gets the Parent key columns.
    /// </summary>
    /// <param name="parentID"></param>
    /// <returns></returns>
    public DbColumns GetParentKey(short parentID)
    {
      var retValue = new DbColumns()
      {
        { DocAssembly.ColumnDocAssemblyGroupID, parentID}
      };
      return retValue;
    }

    // Gets the Unique key columns.
    /// <summary>
    /// Gets the Unique key columns.
    /// </summary>
    /// <param name="parentID"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public DbColumns GetUniqueKey(short parentID, string name)
    {
      // Needs cast for string to select the correct Add overload.
      var retValue = new DbColumns()
      {
        { DocAssembly.ColumnDocAssemblyGroupID, parentID},
        { DocAssembly.ColumnName, (object)name }
      };
      return retValue;
    }
    #endregion

    #region Other Methods

    // Check for duplicate unique key.
    /// <include path='items/IsDuplicate/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
    public bool IsDuplicate(DocAssembly lookupRecord
      , DocAssembly currentRecord, bool isUpdate = false)
    {
      bool retValue = false;

      if (lookupRecord != null)
      {
        if (false == isUpdate)
        {
          // Duplicate for "New" record that already exists.
          retValue = true;
        }
        else
        {
          if (lookupRecord.ID != currentRecord.ID)
          {
            // Duplicate for "Update" where unique key is modified.
            retValue = true;
          }
        }
      }
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the DataManager reference.</summary>
    public DataManager Manager { get; set; }

    /// <summary>Gets or sets the ResultConverter reference.</summary>
    public ResultConverter<DocAssembly, DocAssemblies> ResultConverter { get; set; }
    #endregion
  }
}
