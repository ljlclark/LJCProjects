// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DocMethodManager.cs
using LJCDataAccess;
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using System.Collections.Generic;
using System.Data;

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
        DocMethod.ColumnDocMethodGroupID,
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
      ChangeSequence(SourceSequence, TargetSequence);
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

    #region Other Methods

    /// <summary>
    /// Changes the moved sequence values.
    /// </summary>
    /// <param name="sourceSequence">The source sequence.</param>
    /// <param name="targetSequence">The target sequence.</param>
    public void ChangeSequence(int sourceSequence, int targetSequence)
    {
      if (MethodGroupID > 0)
      {
        var where = $"DocMethodGroupID = {MethodGroupID}";
        var parms = new ProcedureParameters()
        {
          { "@table", SqlDbType.VarChar, 100, "DocMethod" },
          { "@column", SqlDbType.VarChar, 100, "Sequence" },
          { "@sourceSequence", SqlDbType.Int, 0, sourceSequence },
          { "@targetSequence", SqlDbType.Int, 0, targetSequence },
          { "@where", SqlDbType.VarChar, 200, where }
        };
        Manager.LoadProcedure("sp_ChangeSequence", parms);
      }
    }

    // Check for duplicate unique key.
    /// <include path='items/IsDuplicate/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
    public bool IsDuplicate(DocMethod lookupRecord
      , DocMethod currentRecord, bool isUpdate = false)
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
      if (MethodGroupID > 0)
      {
        var where = $"DocMethodGroupID = {MethodGroupID}";
        var parms = new ProcedureParameters()
        {
          { "@table", SqlDbType.VarChar, 100, "DocMethod" },
          { "@idColumn", SqlDbType.VarChar, 100, "ID" },
          { "@sequenceColumn", SqlDbType.VarChar, 100, "Sequence" },
          { "@where", SqlDbType.VarChar, 200, where}
        };
        Manager.LoadProcedure("sp_ResetSequence", parms);
      }
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

    /// <summary>Gets or sets the MethodGroupID value.</summary>
    public int MethodGroupID { get; set; }

    /// <summary>Gets or sets the DataManager reference.</summary>
    public DataManager Manager { get; set; }

    /// <summary>Gets or sets the ResultConverter reference.</summary>
    public ResultConverter<DocMethod, DocMethods> ResultConverter { get; set; }

    /// <summary>Gets or sets the SourceSequence value.</summary>
    public int SourceSequence { get; set; }

    /// <summary>Gets or sets the TargetSequence value.</summary>
    public int TargetSequence { get; set; }
    #endregion
  }
}
