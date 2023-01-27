// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// GenTokens.cs
using System.Collections.Generic;

namespace LJCGenTextLib
{
  // Represents a collection of replacement token strings.
  /// <include path='items/GenTokens/*' file='Doc/GenTokens.xml'/>
  public class GenTokens : List<string>
  {
    #region Constructors

    //Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public GenTokens()
    {
      mPrevCount = -1;
    }
    #endregion

    #region Methods

    // Sets the Token elements from the specified text.
    /// <include path='items/SetTokens/*' file='Doc/GenTokens.xml'/>
    public void SetTokens(string text)
    {
      string tokenValue;
      string existingToken;
      int currentIndex;

      Clear();
      currentIndex = -1;
      tokenValue = GetNextToken(text, ref currentIndex);
      while (tokenValue != null)
      {
        existingToken = BinarySearch(tokenValue);
        if (null == existingToken)
        {
          Add(tokenValue);
        }
        tokenValue = GetNextToken(text, ref currentIndex);
      }
    }

    // Retrieves the next valid token.
    /// <include path='items/GetNextToken/*' file='Doc/GenTokens.xml'/>
    public string GetNextToken(string text, ref int currentIndex)
    {
      int startIndex = 0;
      bool isSearching = true;
      string retValue = null;

      currentIndex++;
      if (currentIndex >= text.Length)
      {
        isSearching = false;
      }
      while (isSearching)
      {
        currentIndex = text.IndexOf('_', currentIndex);
        if (-1 == currentIndex)
        {
          isSearching = false;
        }
        if (isSearching)
        {
          startIndex = currentIndex;
          currentIndex = text.IndexOf('_', currentIndex + 1);
          if (-1 == currentIndex)
          {
            isSearching = false;
          }
        }
        if (isSearching)
        {
          retValue = text.Substring(startIndex, currentIndex - startIndex + 1);
          if (-1 == retValue.IndexOf(' ')
            && -1 == retValue.IndexOf(">")
            && -1 == retValue.IndexOf("'"))
          {
            // The token contains no spaces so it is valid.
            isSearching = false;
          }
        }
      }
      return retValue;
    }

    // Searches the entire sorted collection for an element with the specified value
    /// <include path='items/BinarySearch/*' file='Doc/GenTokens.xml'/>
    public new string BinarySearch(string tokenValue)
    {
      int index;
      string retValue = null;

      if (Count != mPrevCount)
      {
        mPrevCount = Count;
        Sort();
      }

      index = base.BinarySearch(tokenValue);
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
