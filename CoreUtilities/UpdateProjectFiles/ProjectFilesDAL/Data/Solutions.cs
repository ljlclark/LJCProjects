// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Solutions.cs
using System.Collections.Generic;

namespace ProjectFilesDAL
{
  public class Solutions : List<Solution>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public Solutions()
    {
      mPrevCount = -1;
    }
    #endregion

    #region Search and Sort Methods

    // Retrieve the collection element with unique values.
    /// <summary>
    /// Retrieve the collection element with unique values.
    /// </summary>
    /// <param name="codeGroup">The CodeGroup name.</param>
    /// <param name="name">The item name.</param>
    /// <returns>A reference to the matching item.</returns>
    public Solution LJCSearchUnique(string codeGroup, string name)
    {
      Solution retValue = null;

      LJCSortUnique();
      Solution searchItem = new Solution()
      {
        CodeGroup = codeGroup,
        Name = name
      };
      int index = BinarySearch(searchItem);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }

    /// <summary>Sort on Unique values.</summary>
    /// <param name="comparer">The Comparer object.</param>
    public void LJCSortUnique()
    {
      if (Count != mPrevCount)
      {
        mPrevCount = Count;
        Sort();
      }
    }
    #endregion

    #region Class Data

    private int mPrevCount;
    #endregion
  }
}
