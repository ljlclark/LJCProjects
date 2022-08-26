// Copyright (c) Lester J. Clark 2017-2019- All Rights Reserved
using System.Collections.Generic;

namespace LJCRegionDAL
{
  // Represents a collection of City objects.
  /// <include path='items/Cities/*' file='Doc/ProjectRegionManagerDAL.xml'/>
  public class Cities : List<City>
  {
    #region Public Methods

    // Creates and adds the object from the provided values.
    /// <include path='items/Add/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public City Add(int id, string name)
    {
      City retValue = new City()
      {
        ID = id,
        Name = name
      };
      Add(retValue);
      return retValue;
    }

    // Retrieve the collection element with name.
    /// <include path='items/LJCSearchName/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public City LJCSearchName(string name)
    {
      City retValue = null;

      if (Count != mPrevCount)
      {
        mPrevCount = Count;
        Sort();
      }

      City searchItem = new City()
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
