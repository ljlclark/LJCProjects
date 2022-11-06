// ControlRowManager.cs
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using System.Collections.Generic;
using System.Xml.Linq;

namespace LJCDataDetailDAL
{
  /// <summary>Provides table specific data methods.</summary>
  public class ControlRowManager
  {
    // Initializes an object instance.
    /// <include path='items/DataManagerC/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public ControlRowManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "ControlRow", string schemaName = null)
    {
      Manager = new DataManager(dbServiceRef, dataConfigName, tableName
        , schemaName);
      ResultConverter = new ResultConverter<ControlRow, ControlRows>();

      // Map table names with property names or captions
      // that differ from the column names.
      Manager.MapNames(ControlRow.ColumnID, caption: "ControlRow ID");

      // Create the list of database assigned columns.
      Manager.SetDbAssignedColumns(new string[]
      {
        ControlRow.ColumnID
      });

      // Create the list of lookup column names.
      Manager.SetLookupColumns(new string[]
      {
        ControlRow.ColumnControlColumnID,
        ControlRow.ColumnDataValueName
      });
    }

    #region Data Methods

    // Adds a record to the database.
    /// <include path='items/Add/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public ControlRow Add(ControlRow dataObject, List<string> propertyNames = null)
    {
      ControlRow retValue;

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
    public ControlRows Load(DbColumns keyColumns = null
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      ControlRows retValue;

      var dbResult = Manager.Load(keyColumns, propertyNames, filters, joins);
      retValue = ResultConverter.CreateCollection(dbResult);
      return retValue;
    }

    // Retrieves a record from the database.
    /// <include path='items/Retrieve/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public ControlRow Retrieve(DbColumns keyColumns, List<string> propertyNames = null
      , DbFilters filters = null, DbJoins joins = null)
    {
      ControlRow retValue;

      var dbResult = Manager.Retrieve(keyColumns, propertyNames, filters, joins);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Updates the record.
    /// <include path='items/Update/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public void Update(ControlRow dataObject, DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null)
    {
      Manager.Update(dataObject, keyColumns, propertyNames, filters);
    }
    #endregion

    #region Custom Load/Retrieve Methods

    // Loads the parent records.
    /// <include path='items/LoadWithParentID/*' file='Doc/ControlRowManager.xml'/>
    public ControlRows LoadWithParentID(long detailConfigID)
    {
      var keyColumns = GetParentKey(detailConfigID);
      var retValue = Load(keyColumns);
      return retValue;
    }

    // Retrieves a record with the supplied value.
    /// <include path='items/RetrieveWithID/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public ControlRow RetrieveWithID(long id, List<string> propertyNames = null)
    {
      var keyColumns = GetIDKey(id);
      var retValue = Retrieve(keyColumns, propertyNames);
      return retValue;
    }

    // Retrieves a record with the supplied name value.
    /// <include path='items/RetrieveWithUnique/*' file='Doc/ControlRowManager.xml'/>
    public ControlRow RetrieveWithUnique(int controlColumnID, string dataValueName
      , List<string> propertyNames = null)
    {
      var keyColumns = GetUniqueKey(controlColumnID, dataValueName);
      var retValue = Retrieve(keyColumns, propertyNames);
      return retValue;
    }
    #endregion

    #region GetKey Methods

    // Gets the ID key columns.
    /// <include path='items/GetIDKey/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DbColumns GetIDKey(long id)
    {
      // Add(columnName, propertyName = null, renameAs = null
      //   , datatypeName = "String", caption = null);
      // Add(columnName, object value, dataTypeName = "String");
      // Needs (object) cast for string value to select correct Add overload.
      var retValue = new DbColumns()
      {
        { ControlRow.ColumnID, id }
      };
      return retValue;
    }

    // Gets the Parent ID key columns.
    /// <include path='items/GetParentKey/*' file='Doc/ControlRowManager.xml'/>
    public DbColumns GetParentKey(long controlColumnID)
    {
      var retValue = new DbColumns()
      {
        { ControlRow.ColumnControlColumnID, controlColumnID }
      };
      return retValue;
    }

    // Gets the ID key columns.
    /// <include path='items/GetUniqueKey/*' file='Doc/ControlRowManager.xml'/>
    public DbColumns GetUniqueKey(int controlColumnID, string dataValueName)
    {
      var retValue = new DbColumns()
      {
        { ControlRow.ColumnControlColumnID, controlColumnID },
        { ControlRow.ColumnDataValueName, (object)dataValueName }
      };
      return retValue;
    }
    #endregion

    #region Other Public Methods

    // Check for duplicate unique key.
    /// <include path='items/IsDuplicate/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public bool IsDuplicate(ControlRow lookupRecord, ControlRow currentRecord
      , bool isUpdate = false)
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

    /// <summary>
    /// Gets or sets the non-select affected record count.
    /// </summary>
    public int AffectedCount
    {
      get { return Manager.AffectedCount; }
    }

    /// <summary>Gets or sets the DataManager reference.</summary>
    public DataManager Manager { get; set; }

    /// <summary>Gets or sets the ResultConverter reference.</summary>
    public ResultConverter<ControlRow, ControlRows> ResultConverter
    { get; set; }
    #endregion
  }
}
