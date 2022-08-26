// Copyright (c) Lester J. Clark 2017-2019- All Rights Reserved
using System.Collections.Generic;

namespace LJCRegionDAL
{
  /// <summary>Represents a collection of Region objects.</summary>
  public class Regions : List<RegionData>
  {
    #region Public Methods

    // Creates and adds the object from the provided values.
    /// <include path='items/Add/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public RegionData Add(int id, string name)
    {
      RegionData retValue = new RegionData()
      {
        ID = id,
        Name = name
      };
      Add(retValue);
      return retValue;
    }

    // Retrieve the collection element with name.
    /// <include path='items/LJCSearchName/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public RegionData LJCSearchName(string name)
    {
      RegionData retValue = null;

      if (Count != mPrevCount)
      {
        mPrevCount = Count;
        Sort();
      }

      RegionData searchItem = new RegionData()
      {
        Name = name
      };
      int index = BinarySearch(searchItem);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }
    #endregion

    #region Class Data

    private int mPrevCount;
    #endregion
  }
}
