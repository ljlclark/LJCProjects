// Copyright (c) Lester J Clark 2017-2019 - All Rights Reserved
using System.Collections.Generic;

namespace LJCRegionDAL
{
  /// <summary>Represents a collection of CitySection objects.</summary>
  public class CitySections : List<CitySection>
  {
    #region Collection Methods

    // Creates and adds the object from the provided values.
    /// <include path='items/Add/*' file='../../../CoreUtilities/LJCDocLib/Common/Collection.xml'/>
    public CitySection Add(int id, string name)
    {
      CitySection retValue = new CitySection()
      {
        ID = id,
        Name = name
      };
      Add(retValue);
      return retValue;
    }

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public CitySections Clone()
    {
      var retValue = MemberwiseClone() as CitySections;
      return retValue;
    }
    #endregion

    #region Search and Sort Methods

    // Retrieve the collection element with name.
    /// <include path='items/LJCSearchName/*' file='../../../CoreUtilities/LJCDocLib/Common/Collection.xml'/>
    public CitySection LJCSearchName(string name)
    {
      CitySection retValue = null;

      if (Count != mPrevCount)
      {
        mPrevCount = Count;
        Sort();
      }

      CitySection searchItem = new CitySection()
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
