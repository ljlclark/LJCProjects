// Copyright (c) Lester J. Clark 2017-2019- All Rights Reserved
using System.Collections.Generic;

namespace LJCRegionDAL
{
  /// <summary>Represents a collection of Province objects.</summary>
  public class Provinces : List<Province>
  {
    #region Public Methods

    // Creates and adds the object from the provided values.
    /// <include path='items/Add/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public Province Add(int id, string name)
    {
      Province retValue = new Province()
      {
        ID = id,
        Name = name
      };
      Add(retValue);
      return retValue;
    }

    // Retrieve the collection element with name.
    /// <include path='items/LJCSearchName/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public Province LJCSearchName(string name)
    {
      Province retValue = null;

      if (Count != mPrevCount)
      {
        mPrevCount = Count;
        Sort();
      }

      Province searchItem = new Province()
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
