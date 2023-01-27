// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// UserTypes.cs
using System.Collections.Generic;

namespace LJCDocGenLib
{
  /// <summary>Represents a collection of User Type names.</summary>
  public class UserTypes : List<string>
  {
    #region Methods

    // Retrieve the collection element by name.
    /// <include path='items/LJCSearchName/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public string LJCSearchName(string name)
    {
      string retValue = null;

      if (Count != mPrevCount)
      {
        mPrevCount = Count;
        Sort();
      }

      int index = BinarySearch(name);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }
    #endregion

    #region Class Data

    // The previous count value.
    private int mPrevCount;
    #endregion
  }
}
