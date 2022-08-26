// ControlRowManager.cs
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using System.Collections.Generic;
using System.Xml.Linq;

namespace DataDetailDAL
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

    #region Custom Retrieve/Load Methods

    // Retrieves a record with the supplied value.
    /// <include path='items/RetrieveWithID/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public ControlRow RetrieveWithID(int id, List<string> propertyNames = null)
    {
      ControlRow retValue;

      var keyColumns = GetIDKey(id);
      var dbResult = Manager.Retrieve(keyColumns, propertyNames);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Retrieves a record with the supplied name value.
    /// <include path='items/RetrieveWithName/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public ControlRow RetrieveWithUnique(int controlColumnID, string dataValueName
      , List<string> propertyNames = null)
    {
      ControlRow retValue;

      var keyColumns = GetUniqueKey(controlColumnID, dataValueName);
      var dbResult = Manager.Retrieve(keyColumns, propertyNames);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Loads the parent records.
    /// <summary>
    /// Loads the parent records.
    /// </summary>
    /// <param name="detailConfigID">The DetailConfig parent ID.</param>
    /// <returns>The ControlColumns related to the parent.</returns>
    public ControlRows LoadWithParentID(long detailConfigID)
    {
      ControlRows retValue;

      var keyColumns = GetParentKey(detailConfigID);
      var dbResult = Manager.Load(keyColumns);
      retValue = ResultConverter.CreateCollection(dbResult);
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
        { ControlRow.ColumnID, id }
      };
      return retValue;
    }

    /// <summary>
    /// Gets the Parent ID key columns.
    /// </summary>
    /// <param name="controlColumnID">The parent ID value.</param>
    /// <returns>The Parent KeyColumns object.</returns>
    public DbColumns GetParentKey(long controlColumnID)
    {
      var retValue = new DbColumns()
      {
        { ControlRow.ColumnControlColumnID, controlColumnID }
      };
      return retValue;
    }

    // Gets the ID key columns.
    /// <include path='items/GetNameKey/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DbColumns GetUniqueKey(int controlColumnID, string dataValueName)
    {
      // Needs cast to select the correct Add overload.
      var retValue = new DbColumns()
      {
        { ControlRow.ColumnControlColumnID, controlColumnID },
        { ControlRow.ColumnDataValueName, (object)dataValueName }
      };
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the DataManager reference.</summary>
    public DataManager Manager { get; set; }

    /// <summary>Gets or sets the ResultConverter reference.</summary>
    public ResultConverter<ControlRow, ControlRows> ResultConverter { get; set; }
    #endregion
  }
}
