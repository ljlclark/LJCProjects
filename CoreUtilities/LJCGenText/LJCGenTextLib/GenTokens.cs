// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// GenTokens.cs
using System.Collections.Generic;

namespace LJCGenTextLib
{
  // Represents a collection of replacement token strings.
  /// <include file='Doc/GenTokens.xml'
  ///  path='items/GenTokens/*'/>
  public class GenTokens : List<string>
  {
    #region Constructors

    //Initializes an object instance.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='items/DefaultConstructor/*'/>
    public GenTokens()
    {
      mPrevCount = -1;
    }
    #endregion

    #region Methods

    // Sets the Token elements from the specified text.
    /// <include file='Doc/GenTokens.xml'
    ///  path='items/SetTokens/*'/>
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
    /// <include file='Doc/GenTokens.xml'
    ///  path='items/GetNextToken/*'/>
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
    /// <include file='Doc/GenTokens.xml'
    ///  path='items/BinarySearch/*'/>
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
