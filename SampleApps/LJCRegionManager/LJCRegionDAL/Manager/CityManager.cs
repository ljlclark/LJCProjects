// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CityManager.cs
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using System.Collections.Generic;

namespace LJCRegionDAL
{
  /// <summary>Provides Table specific data manipulation methods.</summary>
  public class CityManager
    : ObjectManager<City, Cities>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/ObjectManagerC/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public CityManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "City")
      : base(dbServiceRef, dataConfigName, tableName)
    {
      // Create the list of database assigned columns.
      SetDbAssignedColumns(new string[]
      {
        City.ColumnID
      });

      // Create the list of lookup column names.
      SetLookupColumns(new string[]
      {
        City.ColumnProvinceID,
        City.ColumnName
      });
    }
    #endregion

    #region Load/Retrieve Methods

    // Retrieves a Data Record with the supplied value.
    /// <include path='items/RetrieveWithID/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public City RetrieveWithID(int id, List<string> propertyNames = null)
    {
      var keyColumns = GetIDKey(id);
      return Retrieve(keyColumns, propertyNames);
    }
    #endregion

    #region GetKey Methods

    // Get the ID key record.
    /// <include path='items/GetIDKey/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public DbColumns GetIDKey(int id)
    {
      var retValue = new DbColumns()
      {
        { City.ColumnID, id }
      };
      return retValue;
    }
    #endregion

    #region KeyItem Methods

    // Creates the RecordColumns object.
    /// <include path='items/DataColumns/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public DbColumns DataColumns(long id)
    {
      DbColumns retValue = null;

      // Use common data definitions.
      var keyColumns = GetIDKey((int)id);
      var dbResult = DataManager.Retrieve(keyColumns);
      if (DbResult.HasRows(dbResult))
      {
        retValue = dbResult.GetValueColumns(dbResult.Rows[0].Values);
      }
      return retValue;
    }

    // Creates the KeyItem object.
    /// <include path='items/GetKeyItem/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public KeyItem GetKeyItem(string propertyName, long id)
    {
      KeyItem retValue = null;

      var record = RetrieveWithID((int)id);
      if (record != null)
      {
        retValue = new KeyItem
        {
          Description = record.Name,
          ID = id,
          MaxLength = City.LengthName,
          PrimaryKeyName = City.ColumnID,
          PropertyName = propertyName,
          TableName = City.TableName
        };
      }
      return retValue;
    }
    #endregion

    #region OrderBys

    /// <summary>Sets the current OrderBy names.</summary>
    public void SetOrderByName() => DataManager.OrderByNames = new List<string>()
    {
      City.ColumnName
    };
    #endregion
  }
}
