// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// RepeatItems.cs
using System.Collections.Generic;

namespace LJCGenTextLib
{
  // Represents a collection of RepeatItem objects.
  /// <include path='items/RepeatItems/*' file='Doc/RepeatItems.xml'/>
  public class RepeatItems : List<RepeatItem>
  {
    #region Constructors

    //Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public RepeatItems()
    {
    }
    #endregion

    #region Methods

    // Creates the RepeateItem object and adds it to the end of the collection.
    /// <include path='items/Add/*' file='Doc/RepeatItems.xml'/>
    public RepeatItem Add(string name)
    {
      RepeatItem retValue;

      //if (LJCNetString.HasValue(name))
      //{
      //	retValue = LJCSearchByName(name);
      //	if (null == retValue)
      //	{
      retValue = new RepeatItem()
      {
        Name = name
      };
      Add(retValue);
      //	}
      //}
      return retValue;
    }

    // Retrieve the collection element with name.
    /// <include path='items/LJCSearchName/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public RepeatItem Retrieve(string name)
    {
      RepeatItem repeatItem;
      int index;
      RepeatItem retValue = null;

      if (Count != mPrevCount)
      {
        mPrevCount = Count;
        Sort();
      }

      repeatItem = new RepeatItem()
      {
        Name = name
      };
      index = base.BinarySearch(repeatItem);
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
