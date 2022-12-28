// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// Replacements.cs
using LJCNetCommon;
using System.Collections.Generic;

namespace LJCGenTextLib
{
  // Represents a collection of Replacement objects.
  /// <include path='items/Replacements/*' file='Doc/Replacements.xml'/>
  public class Replacements : List<Replacement>
  {
    #region Constructors

    //Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public Replacements()
    {
      mPrevCount = -1;
    }
    #endregion

    #region Methods

    // Creates the Replacement object with the supplied values
    /// <include path='items/Add/*' file='Doc/Replacements.xml'/>
    public Replacement Add(string name, string value)
    {
      Replacement retValue = null;

      if (NetString.HasValue(name))
      {
        //retValue = LJCSearchByName(name);
        //if (null == retValue)
        //{
        retValue = new Replacement(name, value)
        {
          Name = name
        };
        Add(retValue);
        //}
      }
      return retValue;
    }

    // Retrieve the collection element with name.
    /// <include path='items/LJCSearchName/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public Replacement LJCSearchName(string name)
    {
      Replacement replacement;
      int index;
      Replacement retValue = null;

      if (Count != mPrevCount)
      {
        mPrevCount = Count;
        Sort();
      }

      replacement = new Replacement(name, null);
      index = base.BinarySearch(replacement);
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
