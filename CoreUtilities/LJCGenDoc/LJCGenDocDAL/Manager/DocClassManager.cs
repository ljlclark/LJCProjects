﻿// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DocClassManager.cs
using LJCDataAccess;
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using System.Collections.Generic;
using System.Data;

namespace LJCGenDocDAL
{
  /// <summary>Provides table specific data methods.</summary>
  public class DocClassManager
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DataManagerC/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DocClassManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "DocClass", string schemaName = null)
    {
      InitArgError(dbServiceRef, dataConfigName);

      Manager = new DataManager(dbServiceRef, dataConfigName, tableName
        , schemaName);
      ResultConverter = new ResultConverter<DocClass, DocClasses>();

      // Map table names with property names or captions
      // that differ from the column names.
      Manager.MapNames(DocClass.ColumnID, caption: "DocClass ID");

      // Create the list of database assigned columns.
      Manager.SetDbAssignedColumns(new string[]
      {
        DocClass.ColumnID
      });

      // Create the list of lookup column names.
      Manager.SetLookupColumns(new string[]
      {
        DocClass.ColumnDocAssemblyID,
        DocClass.ColumnName
      });
    }

    //  Initialize argument error handline.
    private void InitArgError(DbServiceRef dbServiceRef, string dataConfigName)
    {
      ArgError = new ArgError("LJCGenDocDAL.DocClassManager")
      {
        MethodName = "DocClassManager(dbServiceRef, dataConfigName)"
      };
      ArgError.Add(DbServiceRef.ItemValues(dbServiceRef));
      ArgError.Add(dataConfigName, "dataConfigName");
      NetString.ThrowArgError(ArgError.ToString());
    }
    #endregion

    #region Data Methods

    // Adds a record to the database.
    /// <include path='items/Add/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DocClass Add(DocClass dataObject
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
    public DocClasses Load(DbColumns keyColumns = null
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
    public DocClass Retrieve(DbColumns keyColumns
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
    public void Update(DocClass dataObject, DbColumns keyColumns
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
    /// <include path='items/LoadWithGroup/*' file='Doc/DocClassManager.xml'/>
    public DocClasses LoadWithGroup(short groupID)
    {
      ArgError.MethodName = "LoadWithGroup(groupID)";
      if (groupID <= 0)
      {
        ArgError.Add("groupID must be greater than zero.");
      }
      NetString.ThrowArgError(ArgError.ToString());

      var keyColumns = GetForeignKey(groupID);
      var dbResult = Manager.Load(keyColumns);
      var retValue = ResultConverter.CreateCollection(dbResult);
      return retValue;
    }

    // Retrieves a collection of data records with the supplied values.
    /// <include path='items/LoadWithParentID/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DocClasses LoadWithParentID(short parentID
      , List<string> propertyNames = null)
    {
      ArgError.MethodName = "LoadWithParentID(parentID)";
      if (parentID <= 0)
      {
        ArgError.Add("parentID must be greater than zero.");
      }
      NetString.ThrowArgError(ArgError.ToString());

      var keyColumns = GetParentIDKey(parentID);
      var dbResult = Manager.Load(keyColumns, propertyNames: propertyNames);
      var retValue = ResultConverter.CreateCollection(dbResult);
      return retValue;
    }

    // Retrieves a record with the supplied value.
    /// <include path='items/RetrieveWithID/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DocClass RetrieveWithID(int id, List<string> propertyNames = null)
    {
      ArgError.MethodName = "RetrieveWithID(id)";
      if (id <= 0)
      {
        ArgError.Add("id must be greater than zero.");
      }
      NetString.ThrowArgError(ArgError.ToString());

      var keyColumns = GetIDKey(id);
      var dbResult = Manager.Retrieve(keyColumns, propertyNames);
      var retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Retrieves a record with the supplied unique values.
    /// <include path='items/RetrieveWithUnique/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DocClass RetrieveWithUnique(short parentID, string name
      , List<string> propertyNames = null)
    {
      ArgError.MethodName = "RetrieveWithUnique(name)";
      ArgError.Add(name, "name");
      NetString.ThrowArgError(ArgError.ToString());

      var keyColumns = GetUniqueKey(parentID, name);
      var dbResult = Manager.Retrieve(keyColumns, propertyNames);
      var retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }
    #endregion

    #region GetKey Methods

    // Gets the ID key columns.
    /// <include path='items/GetForeignKey/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DbColumns GetForeignKey(short foreignID)
    {
      // Needs cast for string to select the correct Add overload.
      var retValue = new DbColumns()
      {
        { DocClass.ColumnDocClassGroupID, foreignID }
      };
      return retValue;
    }

    // Gets the ID key columns.
    /// <include path='items/GetIDKey/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DbColumns GetIDKey(int id)
    {
      // Add(columnName, propertyName = null, renameAs = null
      //   , datatypeName = "String", caption = null);
      // Add(columnName, object value, dataTypeName = "String");
      var retValue = new DbColumns()
      {
        { DocClass.ColumnID, id }
      };
      return retValue;
    }

    // Gets the Parent key columns.
    /// <include path='items/GetParentIDKey/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DbColumns GetParentIDKey(short parentID)
    {
      // Needs cast for string to select the correct Add overload.
      var retValue = new DbColumns()
      {
        { DocClass.ColumnDocAssemblyID, parentID }
      };
      return retValue;
    }

    // Gets the ID key columns.
    /// <include path='items/GetUniqueKey/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DbColumns GetUniqueKey(short parentID, string name)
    {
      // Needs cast for string to select the correct Add overload.
      var retValue = new DbColumns()
      {
        { DocClass.ColumnDocAssemblyID, parentID },
        { DocClass.ColumnName, (object)name }
      };
      return retValue;
    }
    #endregion

    #region Other Methods

    // Changes the moved sequence values.
    /// <include path='items/ChangeSequence/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public void ChangeSequence(int sourceSequence, int targetSequence)
    {
      if (ClassGroupID > 0)
      {
        var where = $"DocClassGroupID = {ClassGroupID}";
        var parms = new ProcedureParameters()
        {
          { "@table", SqlDbType.VarChar, 100, "DocClass" },
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
    public bool IsDuplicate(DocClass lookupRecord
      , DocClass currentRecord, bool isUpdate = false)
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
      if (ClassGroupID > 0)
      {
        var where = $"DocClassGroupID = {ClassGroupID}";
        var parms = new ProcedureParameters()
        {
          { "@table", SqlDbType.VarChar, 100, "DocClass" },
          { "@idColumn", SqlDbType.VarChar, 100, "ID" },
          { "@sequenceColumn", SqlDbType.VarChar, 100, "Sequence" },
          { "@where", SqlDbType.VarChar, 200, where}
        };
        Manager.LoadProcedure("sp_ResetSequence", parms);
      }
    }

    // Sets the current OrderBy names.
    /// <include path='items/SetOrderBy/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public void SetOrderBy(List<string> names)
    {
      Manager.OrderByNames = names;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the GroupID value.</summary>
    public int ClassGroupID { get; set; }

    /// <summary>Gets or sets the DataManager reference.</summary>
    public DataManager Manager { get; set; }

    /// <summary>Gets or sets the ResultConverter reference.</summary>
    public ResultConverter<DocClass, DocClasses> ResultConverter { get; set; }

    /// <summary>Gets or sets the SourceSequence value.</summary>
    public int SourceSequence { get; set; }

    /// <summary>Gets or sets the TargetSequence value.</summary>
    public int TargetSequence { get; set; }

    // Represents Argument errors.
    private ArgError ArgError { get; set; }
    #endregion
  }
}
