// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// RegionItem.cs
using LJCDBClientLib;
using LJCDBDataAccess;
using LJCNetCommon;
using LJCRegionDAL;
using System;

namespace LJCRegionItem
{
  /// <summary>Provides ItemKey methods.</summary>
  public class RegionItem
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public RegionItem(string dataConfigName)
    {
      DataConfigName = dataConfigName;
      SetDataConfig();
    }

    // Sets the Data Configuration.
    private void SetDataConfig()
    {
      // Add reference to LJCDBClientLib, DbDataAccessLib and LJCDBMessage.
      mDbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(DataConfigName)
      };
      mRegionManagers = new RegionManagers(mDbServiceRef, DataConfigName);
    }
    #endregion

    #region Public Methods

    /// <summary>
    /// Creates and returns the KeyItem object. 
    /// </summary>
    /// <param name="dataColumns">The Parent DataColumns.</param>
    /// <param name="propertyName">The Parent DataColumn PropertyName.</param>
    /// <param name="staticKey">true if a StaticKey; otherwise, false.</param>
    /// <returns>The KeyItem object.</returns>
    public KeyItem KeyItem(DbColumns dataColumns, string propertyName
      , bool staticKey = false)
    {
      KeyItem retValue = null;

      int id = dataColumns.LJCGetInt32(propertyName);
      if (id > 0)
      {
        var manager = mRegionManagers.RegionDataManager;
        retValue = manager.GetKeyItem(propertyName, id);
        if (staticKey)
        {
          retValue.TableName = null;
        }
      }
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the DataConfigName value.</summary>
    public string DataConfigName
    {
      get { return mDataConfigName; }
      set
      {
        mDataConfigName = NetString.InitString(value);
        if (!NetString.HasValue(mDataConfigName))
        {
          throw new NullReferenceException("DataConfigName is null.");
        }
        SetDataConfig();
      }
    }
    private string mDataConfigName;
    #endregion

    #region Class Data

    private DbServiceRef mDbServiceRef;
    private RegionManagers mRegionManagers;
    #endregion
  }
}
