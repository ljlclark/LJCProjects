// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CodeGroups.cs
using System.Collections.Generic;

namespace ProjectFilesDAL
{
  /// <summary>Represents a collection of CodeGroup Data Objects.</summary>
  public class CodeGroups : List<CodeGroup>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public CodeGroups()
    {
      mPrevCount = -1;
    }
    #endregion

    #region Search and Sort Methods

    // Retrieve the collection element with unique values.
    /// <summary>
    /// Retrieve the collection element with unique values.
    /// </summary>
    /// <param name="codeLine">The CodeLine name.</param>
    /// <param name="name">The item name.</param>
    /// <returns>A reference to the matching item.</returns>
    public CodeGroup LJCSearchUnique(string codeLine, string name)
    {
      CodeGroup retValue = null;

      LJCSortUnique();
      CodeGroup searchItem = new CodeGroup()
      {
        CodeLine = codeLine,
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
