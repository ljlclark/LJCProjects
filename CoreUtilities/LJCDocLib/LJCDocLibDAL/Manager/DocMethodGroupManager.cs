// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DocMethodGroupManager.cs
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using System.Collections.Generic;

namespace LJCDocLibDAL
{
  /// <summary>Provides table specific data methods.</summary>
  public class DocMethodGroupManager
  {
    // Initializes an object instance.
    /// <include path='items/DataManagerC/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DocMethodGroupManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "DocMethodGroup", string schemaName = null)
    {
      Manager = new DataManager(dbServiceRef, dataConfigName, tableName
        , schemaName);
      ResultConverter = new ResultConverter<DocMethodGroup, DocMethodGroups>();

      // Map table names with property names or captions
      // that differ from the column names.
      Manager.MapNames(DocMethodGroup.ColumnID, caption: "DocMethodGroup ID");

      // Add Calculated and Join columns.
      // Enables adding Calculated and Join columns to a grid configuration.
      Manager.DataDefinition.Add(DocClassGroupHeading.ColumnHeading);

      // Create the list of database assigned columns.
      Manager.SetDbAssignedColumns(new string[]
      {
        DocMethodGroup.ColumnID
      });

      // Create the list of lookup column names.
      Manager.SetLookupColumns(new string[]
      {
        DocMethodGroup.ColumnDocClassID,
        DocMethodGroup.ColumnDocMethodGroupHeadingID
      });
    }

    #region Data Methods

    // Adds a record to the database.
    /// <include path='items/Add/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DocMethodGroup Add(DocMethodGroup dataObject
      , List<string> propertyNames = null)
    {
      DocMethodGroup retValue;

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
    public DocMethodGroups Load(DbColumns keyColumns = null
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      DocMethodGroups retValue;

      var dbResult = Manager.Load(keyColumns, propertyNames, filters, joins);
      retValue = ResultConverter.CreateCollection(dbResult);
      return retValue;
    }

    // Retrieves a record from the database.
    /// <include path='items/Retrieve/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DocMethodGroup Retrieve(DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      DocMethodGroup retValue;

      var dbResult = Manager.Retrieve(keyColumns, propertyNames, filters, joins);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Updates the record.
    /// <include path='items/Update/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public void Update(DocMethodGroup dataObject, DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null)
    {
      Manager.Update(dataObject, keyColumns, propertyNames, filters);
    }
    #endregion

    #region Load/Retrieve Methods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="parentID"></param>
    /// <returns></returns>
    public DocMethodGroups LoadWithParent(short parentID)
    {
      var keyColumns = GetParentID(parentID);
      var joins = GetJoins();
      var dbResult = Manager.Load(keyColumns, joins: joins);
      var retValue = ResultConverter.CreateCollection(dbResult);
      return retValue;
    }

    // Retrieves a record with the supplied value.
    /// <include path='items/RetrieveWithID/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DocMethodGroup RetrieveWithID(short id, List<string> propertyNames = null)
    {
      DocMethodGroup retValue;

      var keyColumns = GetIDKey(id);
      var joins = GetJoins();
      var dbResult = Manager.Retrieve(keyColumns, propertyNames, joins: joins);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Retrieves a record with the supplied unique values.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="docClassID"></param>
    /// <param name="headingName"></param>
    /// <param name="propertyNames"></param>
    /// <returns></returns>
    public DocMethodGroup RetrieveWithUnique(short docClassID
      , string headingName, List<string> propertyNames = null)
    {
      DocMethodGroup retValue;

      var keyColumns = GetUniqueKey(docClassID, headingName);
      var joins = GetJoins();
      var dbResult = Manager.Retrieve(keyColumns, propertyNames, joins: joins);
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
        { DocMethodGroup.ColumnID, id }
      };
      return retValue;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="parentID"></param>
    /// <returns></returns>
    public DbColumns GetParentID(short parentID)
    {
      // Needs object cast for string to select the correct Add overload.
      var retValue = new DbColumns()
      {
        { DocMethodGroup.ColumnDocClassID, parentID },
      };
      return retValue;
    }

    // Gets the ID key columns.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="docClassID"></param>
    /// <param name="headingName"></param>
    /// <returns></returns>
    //public DbColumns GetUniqueKey(short docClassID
    //, short docMethodGroupHeadingID)
    public DbColumns GetUniqueKey(short docClassID, string headingName)
    {
      // Needs cast for string to select the correct Add overload.
      var retValue = new DbColumns()
      {
        { DocMethodGroup.ColumnDocClassID, docClassID},
        //{ DocMethodGroup.ColumnDocMethodGroupHeadingID, docMethodGroupHeadingID }
        { DocMethodGroup.ColumnHeadingName, (object)headingName }
      };
      return retValue;
    }
    #endregion

    #region Joins

    // Creates and returns the Load Joins object.
    /// <include path='items/GetLoadJoins/*' file='../../LJCDocLib/Common/Manager.xml'/>
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

    // Check for duplicate unique key.
    /// <include path='items/IsDuplicate/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
    public bool IsDuplicate(DocMethodGroup lookupRecord
      , DocMethodGroup currentRecord, bool isUpdate = false)
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
    public ResultConverter<DocMethodGroup, DocMethodGroups> ResultConverter { get; set; }
    #endregion
  }
}

