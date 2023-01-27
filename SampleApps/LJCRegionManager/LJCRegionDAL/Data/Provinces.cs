// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Provinces.cs
using System.Collections.Generic;

namespace LJCRegionDAL
{
  /// <summary>Represents a collection of Province objects.</summary>
  public class Provinces : List<Province>
  {
    #region Collection Methods

    // Creates and adds the object from the provided values.
    /// <include path='items/Add/*' file='../../../CoreUtilities/LJCDocLib/Common/Collection.xml'/>
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

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public Provinces Clone()
    {
      var retValue = MemberwiseClone() as Provinces;
      return retValue;
    }
    #endregion

    #region Search and Sort Methods

    // Retrieve the collection element with name.
    /// <include path='items/LJCSearchName/*' file='../../../CoreUtilities/LJCDocLib/Common/Collection.xml'/>
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
