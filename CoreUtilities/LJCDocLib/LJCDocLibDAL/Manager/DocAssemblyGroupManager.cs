// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DocAssemblyGroupManager.cs
using LJCDataAccess;
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using System.Collections.Generic;
using System.Data;

namespace LJCDocLibDAL
{
  /// <summary>Provides table specific data methods.</summary>
  public class DocAssemblyGroupManager
  {
    // Initializes an object instance.
    /// <include path='items/DataManagerC/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DocAssemblyGroupManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "DocAssemblyGroup", string schemaName = null)
    {
      Manager = new DataManager(dbServiceRef, dataConfigName, tableName
        , schemaName);
      ResultConverter = new ResultConverter<DocAssemblyGroup, DocAssemblyGroups>();

      // Map table names with property names or captions
      // that differ from the column names.
      Manager.MapNames(DocAssemblyGroup.ColumnID, caption: "DocAssemblyGroup ID");

      // Create the list of database assigned columns.
      Manager.SetDbAssignedColumns(new string[]
      {
        DocAssemblyGroup.ColumnID
      });

      // Create the list of lookup column names.
      Manager.SetLookupColumns(new string[]
      {
        DocAssemblyGroup.ColumnName
      });
    }

    #region Data Methods

    // Adds a record to the database.
    /// <include path='items/Add/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DocAssemblyGroup Add(DocAssemblyGroup dataObject
      , List<string> propertyNames = null)
    {
      DocAssemblyGroup retValue;

      ChangeSequence(0, TargetSequence);
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
      ChangeSequence(-1, TargetSequence);
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
    public DocAssemblyGroups Load(DbColumns keyColumns = null
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      DocAssemblyGroups retValue;

      var dbResult = Manager.Load(keyColumns, propertyNames, filters, joins);
      retValue = ResultConverter.CreateCollection(dbResult);
      return retValue;
    }

    // Retrieves a record from the database.
    /// <include path='items/Retrieve/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DocAssemblyGroup Retrieve(DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      DocAssemblyGroup retValue;

      var dbResult = Manager.Retrieve(keyColumns, propertyNames, filters, joins);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Updates the record.
    /// <include path='items/Update/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public void Update(DocAssemblyGroup dataObject, DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null)
    {
      ChangeSequence(SourceSequence, TargetSequence);
      Manager.Update(dataObject, keyColumns, propertyNames, filters);
    }
    #endregion

    #region Load/Retrieve Methods

    // Retrieves a record with the supplied value.
    /// <include path='items/RetrieveWithID/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DocAssemblyGroup RetrieveWithID(short id
      , List<string> propertyNames = null)
    {
      DocAssemblyGroup retValue;

      var keyColumns = GetIDKey(id);
      var dbResult = Manager.Retrieve(keyColumns, propertyNames);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Retrieves a record with the supplied unique values.
    /// <include path='items/RetrieveWithName/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DocAssemblyGroup RetrieveWithUnique(string name
      , List<string> propertyNames = null)
    {
      DocAssemblyGroup retValue;

      var keyColumns = GetUniqueKey(name);
      var dbResult = Manager.Retrieve(keyColumns, propertyNames);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }
    #endregion

    #region GetKey Methods

    // Gets the ID key columns.
    /// <include path='items/GetIDKey/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DbColumns GetIDKey(short id)
    {
      // Add(columnName, propertyName = null, renameAs = null
      //   , datatypeName = "String", caption = null);
      // Add(columnName, object value, dataTypeName = "String");
      var retValue = new DbColumns()
      {
        { DocAssemblyGroup.ColumnID, id }
      };
      return retValue;
    }

    // Gets the ID key columns.
    /// <include path='items/GetNameKey/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DbColumns GetUniqueKey(string name)
    {
      // Needs object cast for string to select the correct Add overload.
      var retValue = new DbColumns()
      {
        { DocAssemblyGroup.ColumnName, (object)name }
      };
      return retValue;
    }
    #endregion

    #region Other Methods

    /// <summary>
    /// Changes the moved sequence values.
    /// </summary>
    /// <param name="sourceSequence">The source sequence.</param>
    /// <param name="targetSequence">The target sequence.</param>
    public void ChangeSequence(int sourceSequence, int targetSequence)
    {
      var parms = new ProcedureParameters()
      {
        { "@table", SqlDbType.VarChar, 100, "DocAssemblyGroup" },
        { "@column", SqlDbType.VarChar, 100, "Sequence" },
        { "@sourceSequence", SqlDbType.Int, 0, sourceSequence },
        { "@targetSequence", SqlDbType.Int, 0, targetSequence }
      };
      Manager.LoadProcedure("sp_ChangeSequence", parms);
    }

    // Check for duplicate unique key.
    /// <include path='items/IsDuplicate/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
    public bool IsDuplicate(DocAssemblyGroup lookupRecord
      , DocAssemblyGroup currentRecord, bool isUpdate = false)
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

    /// <summary>
    /// Resets the sequence values.
    /// </summary>
    public void ResetSequence()
    {
      var parms = new ProcedureParameters()
      {
        { "@table", SqlDbType.VarChar, 100, "DocAssemblyGroup" },
        { "@idColumn", SqlDbType.VarChar, 100, "ID" },
        { "@sequenceColumn", SqlDbType.VarChar, 100, "Sequence" }
      };
      Manager.LoadProcedure("sp_ResetSequence", parms);
    }

    /// <summary>
    /// Sets the order by names.
    /// </summary>
    /// <param name="names">The name list.</param>
    public void SetOrderBy(List<string> names)
    {
      Manager.OrderByNames = names;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the DataManager reference.</summary>
    public DataManager Manager { get; set; }

    /// <summary>Gets or sets the ResultConverter reference.</summary>
    public ResultConverter<DocAssemblyGroup, DocAssemblyGroups> ResultConverter { get; set; }

    /// <summary>Gets or sets the SourceSequence value.</summary>
    public int SourceSequence { get; set; }

    /// <summary>Gets or sets the TargetSequence value.</summary>
    public int TargetSequence { get; set; }
    #endregion
  }
}
