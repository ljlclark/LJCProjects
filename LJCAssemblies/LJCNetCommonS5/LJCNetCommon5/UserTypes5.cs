// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// UserTypes.cs

namespace LJCNetCommon5
{
  // Represents a collection of User Type names.
  /// <include file='Doc/UserTypes.xml'
  ///  path='members/UserTypes/*'/>
  public class UserTypes : List<string>
  {
    #region Class Data

    // The previous count value.
    private int mPrevCount;
    #endregion

    #region Methods

    // Retrieve the collection element by name.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
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
