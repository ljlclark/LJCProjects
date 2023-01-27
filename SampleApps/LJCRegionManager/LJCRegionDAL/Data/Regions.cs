// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Regions.cs
using System.Collections.Generic;

namespace LJCRegionDAL
{
  /// <summary>Represents a collection of Region objects.</summary>
  public class Regions : List<RegionData>
  {
    #region Collection Methods

    // Creates and adds the object from the provided values.
    /// <include path='items/Add/*' file='../../../CoreUtilities/LJCDocLib/Common/Collection.xml'/>
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

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public Regions Clone()
    {
      var retValue = MemberwiseClone() as Regions;
      return retValue;
    }
    #endregion

    #region Search and Sort Methods

    // Retrieve the collection element with name.
    /// <include path='items/LJCSearchName/*' file='../../../CoreUtilities/LJCDocLib/Common/Collection.xml'/>
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
