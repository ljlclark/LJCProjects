// Copyright (c) Lester J. Clark 2017-2019- All Rights Reserved
using System.Collections.Generic;
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;

namespace LJCRegionDAL
{
  /// <summary>Provides Table specific data manipulation methods.</summary>
  public class CitySectionManager : ObjectManager<CitySection, CitySections>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/ObjectManagerC/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
    public CitySectionManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "CitySection") : base(dbServiceRef, dataConfigName, tableName)
    {
      // Create the list of database assigned columns.
      SetDbAssignedColumns(new string[]
        {
          CitySection.ColumnID
        });

      // Create the list of lookup column names.
      SetLookupColumns(new string[]
        {
          CitySection.ColumnCityID,
          CitySection.ColumnName
        });
    }
    #endregion

    #region Retrieve/Load Methods

    // Retrieves a Data Record with the supplied value.
    /// <include path='items/RetrieveWithID/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
    public CitySection RetrieveWithID(int id, List<string> propertyNames = null)
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
        { CitySection.ColumnID, id }
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
          MaxLength = CitySection.LengthName,
          PrimaryKeyName = CitySection.ColumnID,
          PropertyName = propertyName,
          TableName = CitySection.TableName
        };
      }
      return retValue;
    }
    #endregion

    #region OrderBys

    /// <summary>Sets the current OrderBy names.</summary>
    public void SetOrderByName() => DataManager.OrderByNames = new List<string>()
    {
      CitySection.ColumnName
    };
    #endregion
  }
}
