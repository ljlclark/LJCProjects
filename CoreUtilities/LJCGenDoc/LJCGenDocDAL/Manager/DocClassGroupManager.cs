﻿// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DocClassGroupManager.cs
using LJCDataAccess;
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using System.Collections.Generic;
using System.Data;

namespace LJCGenDocDAL
{
  /// <summary>Provides table specific data methods.</summary>
  public class DocClassGroupManager
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DataManagerC/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DocClassGroupManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "DocClassGroup", string schemaName = null)
    {
      InitArgError(dbServiceRef, dataConfigName);

      Manager = new DataManager(dbServiceRef, dataConfigName, tableName
        , schemaName);
      ResultConverter = new ResultConverter<DocClassGroup, DocClassGroups>();

      // Map table names with property names or captions
      // that differ from the column names.
      Manager.MapNames(DocClassGroup.ColumnID, caption: "DocClassGroup ID");

      // Add Calculated and Join columns.
      // Enables adding Calculated and Join columns to a grid configuration.
      Manager.DataDefinition.Add(DocClassGroupHeading.ColumnHeading);

      // Create the list of database assigned columns.
      Manager.SetDbAssignedColumns(new string[]
      {
        DocClassGroup.ColumnID
      });

      // Create the list of lookup column names.
      Manager.SetLookupColumns(new string[]
      {
        DocClassGroup.ColumnDocAssemblyID,
        DocClassGroup.ColumnHeadingName
      });
    }

    //  Initialize argument error handline.
    private void InitArgError(DbServiceRef dbServiceRef, string dataConfigName)
    {
      ArgError = new ArgError("LJCGenDocDAL.DocClassGroupManager")
      {
        MethodName = "DocClassGroupManager(dbServiceRef, dataConfigName)"
      };
      ArgError.Add(DbServiceRef.ItemValues(dbServiceRef));
      ArgError.Add(dataConfigName, "dataConfigName");
      NetString.ThrowArgError(ArgError.ToString());
    }
    #endregion

    #region Data Methods

    // Adds a record to the database.
    /// <include path='items/Add/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DocClassGroup Add(DocClassGroup dataObject
      , List<string> propertyNames = null)
    {
      ArgError.MethodName = "Add(dataObject)";
      ArgError.Add(dataObject, "dataObject");
      NetString.ThrowArgError(ArgError.ToString());

      ChangeSequence(0, TargetSequence);
      var dbResult = Manager.Add(dataObject, propertyNames);
      var retValue = ResultConverter.CreateData(dbResult);
      if (retValue != null)
      {
        dataObject.ID = retValue.ID;
      }
      return retValue;
    }

    // Deletes the records with the specified key values.
    /// <include path='items/Delete/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public void Delete(DbColumns keyColumns, DbFilters filters = null)
    {
      ArgError.MethodName = "Delete(keyColumns, filters)";
      if (null == keyColumns
        && null == filters)
      {
        ArgError.Add("Must have keyColumns or filters.");
      }
      NetString.ThrowArgError(ArgError.ToString());

      ChangeSequence(-1, TargetSequence);
      Manager.Delete(keyColumns, filters);
    }

    // Gets the collection of specified columns.
    /// <include path='items/GetColumns/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DbColumns GetColumns(List<string> propertyNames)
    {
      return Manager.DataDefinition.LJCGetColumns(propertyNames);
    }

    // Retrieves a collection of data records.
    /// <include path='items/Load/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DocClassGroups Load(DbColumns keyColumns = null
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      var dbResult = Manager.Load(keyColumns, propertyNames, filters, joins);
      var retValue = ResultConverter.CreateCollection(dbResult);
      return retValue;
    }

    // Retrieves a collection of data records.
    /// <include path='items/Load/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DbResult LoadResult(DbColumns keyColumns = null
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      var retValue = Manager.Load(keyColumns, propertyNames, filters, joins);
      return retValue;
    }

    // Retrieves a record from the database.
    /// <include path='items/Retrieve/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DocClassGroup Retrieve(DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      ArgError.MethodName = "Retrieve(keyColumns, filters)";
      if (null == keyColumns
        && null == filters)
      {
        ArgError.Add("Must have keyColumns or filters.");
      }
      NetString.ThrowArgError(ArgError.ToString());

      var dbResult = Manager.Retrieve(keyColumns, propertyNames, filters, joins);
      var retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Updates the record.
    /// <include path='items/Update/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public void Update(DocClassGroup dataObject, DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null)
    {
      ArgError.MethodName = "Update(dataObjecct, keyColumns, filters)";
      ArgError.Add(dataObject, "dataObject");
      if (null == keyColumns
        && null == filters)
      {
        ArgError.Add("Must have keyColumns or filters.");
      }
      NetString.ThrowArgError(ArgError.ToString());

      ChangeSequence(SourceSequence, TargetSequence);
      Manager.Update(dataObject, keyColumns, propertyNames, filters);
    }
    #endregion

    #region Load/Retrieve Methods

    // Retrieves a collection of data records with the supplied values.
    /// <include path='items/LoadWithParentID/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DocClassGroups LoadWithParentID(short parentID
      , List<string> propertyNames = null)
    {
      ArgError.MethodName = "LoadWithParentID(parentID)";
      if (parentID <= 0)
      {
        ArgError.Add("parentID must be greater than zero.");
      }
      NetString.ThrowArgError(ArgError.ToString());

      var keyColumns = GetParentIDKey(parentID);
      var joins = GetJoins();
      var dbResult = Manager.Load(keyColumns, propertyNames, joins: joins);
      var retValue = ResultConverter.CreateCollection(dbResult);
      return retValue;
    }

    // Retrieves a record with the supplied value.
    /// <include path='items/RetrieveWithID/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DocClassGroup RetrieveWithID(short id
      , List<string> propertyNames = null)
    {
      ArgError.MethodName = "RetrieveWithID(id)";
      if (id <= 0)
      {
        ArgError.Add("id must be greater than zero.");
      }
      NetString.ThrowArgError(ArgError.ToString());

      var keyColumns = GetIDKey(id);
      var joins = GetJoins();
      var dbResult = Manager.Retrieve(keyColumns, propertyNames, joins: joins);
      var retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Retrieves a record with the supplied unique values.
    /// <include path='items/RetrieveWithUnique/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DocClassGroup RetrieveWithUnique(short parentID
      , string name, List<string> propertyNames = null)
    {
      ArgError.MethodName = "RetrieveWithUnique(name)";
      ArgError.Add(name, "name");
      NetString.ThrowArgError(ArgError.ToString());

      var keyColumns = GetUniqueKey(parentID, name);
      var joins = GetJoins();
      var dbResult = Manager.Retrieve(keyColumns, propertyNames, joins: joins);
      var retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }
    #endregion

    #region GetKey Methods

    // Gets the ID key columns.
    /// <include path='items/GetIDKey/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DbColumns GetIDKey(short id)
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

    // Gets the Parent key columns.
    /// <include path='items/GetParentIDKey/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DbColumns GetParentIDKey(int parentID)
    {
      // Needs object cast for string to select the correct Add overload.
      var retValue = new DbColumns()
      {
        { DocClassGroup.ColumnDocAssemblyID, parentID },
      };
      return retValue;
    }

    // Gets the Unique key columns.
    /// <include path='items/GetUniqueKey/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DbColumns GetUniqueKey(short parentID
      , string name)
    {
      // Needs object cast for string to select the correct Add overload.
      var retValue = new DbColumns()
      {
        { DocClassGroup.ColumnDocAssemblyID, parentID },
        { DocClassGroup.ColumnHeadingName, (object)name}
      };
      return retValue;
    }
    #endregion

    #region Joins

    // Creates and returns the Load Joins object.
    /// <include path='items/GetLoadJoins/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DbJoins GetJoins()
    {
      DbJoin dbJoin;
      DbJoins retValue = new DbJoins();

      // Note: JoinOn Columns must have properties in the DataObject
      // to receive the join values.
      // The RenameAs property is required if there is another table column
      // with the same name.
      // Note: dbColumns.Add(string columnName, string propertyName = null
      // , string renameAs = null, string dataTypeName = "String"
      // , string caption = null)

      // DocClassGroupHeading.Heading,
      // left join DocClassGroupHeading
      // on ((DocClassGroup.DocClassGroupHeadingID = DocClassGroupHeading.ID))
      dbJoin = new DbJoin
      {
        TableName = "DocClassGroupHeading",
        JoinType = "left",
        JoinOns = new DbJoinOns()
        {
          { DocClassGroup.ColumnDocClassGroupHeadingID
            , DocClassGroupHeading.ColumnID }
        },
        Columns = new DbColumns()
        {
          // columnName, propertyName = null, renameAs = null
          //   , dataTypeName = "String", caption = null
          { DocClassGroupHeading.ColumnHeading }
        }
      };
      retValue.Add(dbJoin);
      return retValue;
    }
    #endregion

    #region Other Methods

    // Changes the moved sequence values.
    /// <include path='items/ChangeSequence/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public void ChangeSequence(int sourceSequence, int targetSequence)
    {
      if (AssemblyID > 0)
      {
        var where = $"DocAssemblyID = {AssemblyID}";
        var parms = new ProcedureParameters()
        {
          { "@table", SqlDbType.VarChar, 100, "DocClassGroup" },
          { "@column", SqlDbType.VarChar, 100, "Sequence" },
          { "@sourceSequence", SqlDbType.Int, 0, sourceSequence },
          { "@targetSequence", SqlDbType.Int, 0, targetSequence },
          { "@where", SqlDbType.VarChar, 200, where }
        };
        Manager.LoadProcedure("sp_ChangeSequence", parms);
      }
    }

    // Check for duplicate unique key.
    /// <include path='items/IsDuplicate/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public bool IsDuplicate(DocClassGroup lookupRecord
      , DocClassGroup currentRecord, bool isUpdate = false)
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
          if (lookupRecord.ID != currentRecord.ID)
          {
            // Duplicate for "Update" where unique key is modified.
            retValue = true;
          }
        }
      }
      return retValue;
    }

    /// <summary>Resets the sequence values.</summary>
    public void ResetSequence()
    {
      var where = $"DocAssemblyID = {AssemblyID}";
      var parms = new ProcedureParameters()
      {
        { "@table", SqlDbType.VarChar, 100, "DocClassGroup" },
        { "@idColumn", SqlDbType.VarChar, 100, "ID" },
        { "@sequenceColumn", SqlDbType.VarChar, 100, "Sequence" },
        { "@where", SqlDbType.VarChar, 200, where}
      };
      Manager.LoadProcedure("sp_ResetSequence", parms);
    }

    // Sets the current OrderBy names.
    /// <include path='items/SetOrderBy/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public void SetOrderBy(List<string> names)
    {
      Manager.OrderByNames = names;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the AssemblyID value.</summary>
    public int AssemblyID { get; set; }

    /// <summary>Gets or sets the DataManager reference.</summary>
    public DataManager Manager { get; set; }

    /// <summary>Gets or sets the ResultConverter reference.</summary>
    public ResultConverter<DocClassGroup, DocClassGroups> ResultConverter { get; set; }

    /// <summary>Gets or sets the SourceSequence value.</summary>
    public int SourceSequence { get; set; }

    /// <summary>Gets or sets the TargetSequence value.</summary>
    public int TargetSequence { get; set; }

    // Represents Argument errors.
    private ArgError ArgError { get; set; }
    #endregion
  }
}
