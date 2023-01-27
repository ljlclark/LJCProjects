// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// RegionDataManager.cs
using System.Collections.Generic;
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;

namespace LJCRegionDAL
{
  /// <summary>Provides Table specific data manipulation methods.</summary>
  public class RegionDataManager : ObjectManager<RegionData, Regions>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/ObjectManagerC/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
    public RegionDataManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "Region") : base(dbServiceRef, dataConfigName, tableName)
    {
      // Create the list of database assigned columns.
      SetDbAssignedColumns(new string[]
        {
          RegionData.ColumnID
        });

      // Create the list of lookup column names.
      SetLookupColumns(new string[]
        {
          RegionData.ColumnName
        });
    }
    #endregion

    #region Retrieve/Load Methods

    // Retrieves a Data Record with the supplied value.
    /// <include path='items/RetrieveWithID/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
    public RegionData RetrieveWithID(int id, List<string> propertyNames = null)
    {
      var keyColumns = GetIDKey(id);
      return Retrieve(keyColumns, propertyNames);
    }
    #endregion

    #region GetKey Methods

    // Gets the ID key record.
    /// <include path='items/GetIDKey/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
    public DbColumns GetIDKey(int id)
    {
      var retValue = new DbColumns()
      {
        { RegionData.ColumnID, id }
      };
      return retValue;
    }
    #endregion

    #region KeyItem Methods

    // Creates the RecordColumns object.
    /// <include path='items/DataColumns/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
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
    /// <include path='items/GetKeyItem/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
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
          MaxLength = RegionData.LengthName,
          PrimaryKeyName = RegionData.ColumnID,
          PropertyName = propertyName,
          TableName = RegionData.TableName
        };
      }
      return retValue;
    }

    // Creates the KeyItems collection.
    /// <include path='items/GetKeyItems/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
    public KeyItems GetKeyItems(string propertyName
      , DbColumns keyColumns = null)
    {
      KeyItems retValue = null;

      var records = Load(keyColumns);
      if (records != null)
      {
        retValue = new KeyItems();
        foreach (RegionData record in records)
        {
          retValue.Add(propertyName, record.ID, record.Name
            , RegionData.LengthName);
        }
      }
      return retValue;
    }
    #endregion

    #region OrderBys

    /// <summary>Sets the current OrderBy names.</summary>
    public void SetOrderByName() => DataManager.OrderByNames = new List<string>()
    {
      RegionData.ColumnName
    };
    #endregion
  }
}
