// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// RegionManagers.cs
using System;
using LJCDBClientLib;

namespace LJCRegionDAL
{
  /// <summary>Creates the RegionManagerDAL Manager objects.</summary>
  public class RegionManagers
  {
    #region Constructors

    /// <summary>
    /// Initializes an object instance.
    /// </summary>
    /// <param name="dbServiceRef">The DbServiceRef object.</param>
    /// <param name="dataConfigName">The DataConfigName value.</param>
    public RegionManagers(DbServiceRef dbServiceRef, string dataConfigName)
    {
      mDbServiceRef = dbServiceRef;
      mDataConfigName = dataConfigName;
    }
    #endregion

    #region Properties

    /// <summary>Gets the RegionDataManager object.</summary>
    public RegionDataManager RegionDataManager
    {
      get
      {
        if (null == mRegionDataManager)
        {
          RegionDataManager
            = new RegionDataManager(mDbServiceRef, mDataConfigName);
        }
        return mRegionDataManager;
      }

      private set
      {
        if (value != null)
        {
          mRegionDataManager = value;
        }
      }
    }

    /// <summary>Gets the ProvinceManager object.</summary>
    public ProvinceManager ProvinceManager
    {
      get
      {
        if (null == mProvinceManager)
        {
          ProvinceManager
            = new ProvinceManager(mDbServiceRef, mDataConfigName);
        }
        return mProvinceManager;
      }

      private set
      {
        if (value != null)
        {
          mProvinceManager = value;
        }
      }
    }

    /// <summary>Gets the CityManager object.</summary>
    public CityManager CityManager
    {
      get
      {
        if (null == mCityManager)
        {
          CityManager
            = new CityManager(mDbServiceRef, mDataConfigName);
        }
        return mCityManager;
      }

      private set
      {
        if (value != null)
        {
          mCityManager = value;
        }
      }
    }

    /// <summary>Gets the CitySectionManager object.</summary>
    public CitySectionManager CitySectionManager
    {
      get
      {
        if (null == mCitySectionManager)
        {
          CitySectionManager
            = new CitySectionManager(mDbServiceRef, mDataConfigName);
        }
        return mCitySectionManager;
      }

      private set
      {
        if (value != null)
        {
          mCitySectionManager = value;
        }
      }
    }
    #endregion

    #region Class Data

    private readonly DbServiceRef mDbServiceRef;
    private readonly string mDataConfigName;

    private RegionDataManager mRegionDataManager;
    private ProvinceManager mProvinceManager;
    private CityManager mCityManager;
    private CitySectionManager mCitySectionManager;
    #endregion
  }
}
