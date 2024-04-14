// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DocMethodGroupManager.cs
using LJCDataAccess;
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using System.Collections.Generic;
using System.Data;

namespace LJCGenDocDAL
{
  #region Constructors

  /// <summary>Provides table specific data methods.</summary>
  public class DocMethodGroupManager
  {
    // Initializes an object instance.
    /// <include path='items/DataManagerC/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DocMethodGroupManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "DocMethodGroup", string schemaName = null)
    {
      InitArgError(dbServiceRef, dataConfigName);

      Manager = new DataManager(dbServiceRef, dataConfigName, tableName
        , schemaName);
      ResultConverter = new ResultConverter<DocMethodGroup, DocMethodGroups>();

      // Map table names with property names or captions
      // that differ from the column names.
      Manager.MapNames(DocMethodGroup.ColumnID, caption: "DocMethodGroup ID");

      // Add Calculated and Join columns.
      // Enables adding Calculated and Join columns to a grid configuration.
      //Manager.DataDefinition.Add(DocClassGroupHeading.ColumnHeading);
      Manager.DataDefinition.Add(DocMethodGroupHeading.ColumnHeading);

      // Create the list of database assigned columns.
      Manager.SetDbAssignedColumns(new string[]
      {
        DocMethodGroup.ColumnID
      });

      // Create the list of lookup column names.
      Manager.SetLookupColumns(new string[]
      {
        DocMethodGroup.ColumnDocClassID,
        //DocMethodGroup.ColumnDocMethodGroupHeadingID
        DocMethodGroup.ColumnHeadingName
      });
    }

    //  Initialize argument error handline.
    private void InitArgError(DbServiceRef dbServiceRef, string dataConfigName)
    {
      ArgError = new ArgError("LJCGenDocDAL.DocMethodGroupManager")
      {
        MethodName = "DocMethodGroupManager(dbServiceRef, dataConfigName)"
      };
      ArgError.Add(DbServiceRef.ItemValues(dbServiceRef));
      ArgError.Add(dataConfigName, "dataConfigName");
      NetString.ThrowArgError(ArgError.ToString());
    }
    #endregion

    #region Data Methods

    // Adds a record to the database.
    /// <include path='items/Add/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DocMethodGroup Add(DocMethodGroup dataObject
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
    public DocMethodGroups Load(DbColumns keyColumns = null
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
    public DocMethodGroup Retrieve(DbColumns keyColumns
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
    public void Update(DocMethodGroup dataObject, DbColumns keyColumns
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

    // Loads a collection of Data Records with the supplied value.
    /// <include path='items/LoadWithParentID/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DocMethodGroups LoadWithParentID(short parentID
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
    public DocMethodGroup RetrieveWithID(short id
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
    public DocMethodGroup RetrieveWithUnique(short parentID
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
        { DocMethodGroup.ColumnID, id }
      };
      return retValue;
    }

    // Gets the Parent key columns.
    /// <include path='items/GetParentIDKey/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DbColumns GetParentIDKey(short parentID)
    {
      // Needs object cast for string to select the correct Add overload.
      var retValue = new DbColumns()
      {
        { DocMethodGroup.ColumnDocClassID, parentID },
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
        { DocMethodGroup.ColumnDocClassID, parentID},
        //{ DocMethodGroup.ColumnDocMethodGroupHeadingID, docMethodGroupHeadingID }
        { DocMethodGroup.ColumnHeadingName, (object)name }
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

      // DocMethodGroupHeading.Heading,
      // left join DocMethodGroupHeading
      // on ((DocMethodGroup.DocMethodGroupHeadingID = DocMethodGroupHeading.ID))
      dbJoin = new DbJoin
      {
        TableName = "DocMethodGroupHeading",
        JoinType = "left",
        JoinOns = new DbJoinOns()
        {
          { DocMethodGroup.ColumnDocMethodGroupHeadingID
            , DocMethodGroupHeading.ColumnID }
        },
        Columns = new DbColumns()
        {
          // columnName, propertyName = null, renameAs = null
          //   , dataTypeName = "String", caption = null
          { DocMethodGroupHeading.ColumnHeading }
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
      if (ClassID > 0)
      {
        var where = $"DocClassID = {ClassID}";
        var parms = new ProcedureParameters()
        {
          { "@table", SqlDbType.VarChar, 100, "DocMethodGroup" },
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
    public bool IsDuplicate(DocMethodGroup lookupRecord
      , DocMethodGroup currentRecord, bool isUpdate = false)
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
      if (ClassID > 0)
      {
        var where = $"DocClassID = {ClassID}";
        var parms = new ProcedureParameters()
        {
          { "@table", SqlDbType.VarChar, 100, "DocMethodGroup" },
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

    /// <summary>Gets or sets the ClassID value.</summary>
    public int ClassID { get; set; }

    /// <summary>Gets or sets the DataManager reference.</summary>
    public DataManager Manager { get; set; }

    /// <summary>Gets or sets the ResultConverter reference.</summary>
    public ResultConverter<DocMethodGroup, DocMethodGroups> ResultConverter { get; set; }

    /// <summary>Gets or sets the SourceSequence value.</summary>
    public int SourceSequence { get; set; }

    /// <summary>Gets or sets the TargetSequence value.</summary>
    public int TargetSequence { get; set; }

    // Represents Argument errors.
    private ArgError ArgError { get; set; }
    #endregion
  }
}

