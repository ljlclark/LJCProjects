// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CVPersonManager.cs
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using System.Collections.Generic;

namespace CVRDAL
{
  /// <summary>Provides Table specific data manipulation methods.</summary>
  public class CVPersonManager
    : ObjectManager<CVPerson, CVPersons>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/ObjectManagerC/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
    public CVPersonManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "CVPerson")
      : base(dbServiceRef, dataConfigName, tableName)
    {
      // Create the list of database assigned columns.
      SetDbAssignedColumns(new string[]
      {
        CVPerson.ColumnID
      });

      // Create the list of lookup column names.
      SetLookupColumns(new string[]
      {
        CVPerson.ColumnFirstName,
        CVPerson.ColumnMiddleName,
        CVPerson.ColumnLastName
      });
    }
    #endregion

    #region Load/Retrieve Methods

    // Deletes a Data Record with the supplied value.
    /// <include path='items/DeleteWithID/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
    public void DeleteWithID(long id)
    {
      var keyColumns = GetIDKey(id);
      Delete(keyColumns);
    }

    // Retrieves a Data Record with the supplied value.
    /// <include path='items/RetrieveWithID/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
    public CVPerson RetrieveWithID(long id, List<string> propertyNames = null)
    {
      var keyColumns = GetIDKey(id);
      return Retrieve(keyColumns, propertyNames);
    }
    #endregion

    #region GetKey Methods

    // Gets the ID key record.
    /// <include path='items/GetIDKey/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
    public DbColumns GetIDKey(long id)
    {
      DbColumns retValue = new DbColumns()
      {
        { CVPerson.ColumnID, id }
      };
      return retValue;
    }
    #endregion

    #region KeyItem Methods

    // Creates the DataColumns object.
    /// <include path='items/DataColumns/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
    public DbColumns DataColumns(long id)
    {
      DbColumns retValue = null;

      bool useObject = false;
      if (useObject)
      {
        // Use data object.
        var cvPerson = RetrieveWithID(id);
        if (cvPerson != null)
        {
          retValue = DbColumns.LJCCreateObjectColumns(cvPerson
            , DataDefinition);
        }
      }
      else
      {
        // Use common data definitions.
        DbColumns keyColumns = GetIDKey(id);
        var dbResult = DataManager.Retrieve(keyColumns);
        if (DbResult.HasRows(dbResult))
        {
          retValue = dbResult.GetValueColumns(dbResult.Rows[0].Values);
        }
      }
      return retValue;
    }

    // Creates the KeyItem object.
    /// <include path='items/GetKeyItem/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
    public KeyItem GetKeyItem(string propertyName, long id)
    {
      KeyItem retValue = null;

      var record = RetrieveWithID(id);
      if (record != null)
      {
        retValue = new KeyItem
        {
          Description = record.FullName,
          ID = id,
          MaxLength = CVPerson.LengthFullName,
          PrimaryKeyName = CVPerson.ColumnID,
          PropertyName = propertyName,
          TableName = CVPerson.TableName
        };
      }
      return retValue;
    }
    #endregion
  }
}
