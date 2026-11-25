// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCUserTypes.cs

namespace LJCCSParser5
{
  // Represents a collection of User Type names.
  /// <include file='Doc/LJCUserTypes.xml'
  ///  path='members/UserTypes/*'/>
  public class LJCUserTypes : List<string>
  {
    #region Class Data

    // The previous count value.
    private int mPrevCount;
    #endregion

    #region Methods

    // Retrieve the collection element by name.
    /// <include file='../../../LJCUtilities/LJCGenDoc5/Common/Collection.xml'
    ///  path='members/LJCSearchName/*'/>
    public string? LJCSearchName(string name)
    {
      string? retValue = null;

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
  }
}
