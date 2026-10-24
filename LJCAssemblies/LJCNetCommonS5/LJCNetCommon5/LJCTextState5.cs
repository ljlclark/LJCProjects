// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCTextState.cs

namespace LJCNetCommon5
{
  // Represents the text state.
  /// <include file="Doc/ProjectTextState.xml"
  ///  path="members/LJCTextState/*"/>
  public class LJCTextState
  {
    #region Properties

    // Gets or sets the current indent count to sync called method.
    /// <include file='Doc/TextState.xml'
    ///  path='members/IndentCount/*'/>
    public int IndentCount
    {
      get => mIndentCount;
      set
      {
        mIndentCount = 0;
        if (value >= 0)
        {
          mIndentCount = value;
        }
      }
    }
    private int mIndentCount;

    // Gets or sets the new indent count to sync calling method.
    /// <include file='Doc/TextState.xml'
    ///  path='members/ChildIndentCount/*'/>
    public int ChildIndentCount
    {
      get => mChildIndentCount;
      set
      {
        mChildIndentCount = 0;
        if (value >= 0)
        {
          mChildIndentCount = value;
        }
      }
    }
    private int mChildIndentCount;
    #endregion

    #region Constructors
    // Initializes an object instance with the supplied values.
    /// <include file='Doc/TextState.xml'
    ///  path='members/Constructor/*'/>
    public LJCTextState(int indentCount = 0)
    {
      IndentCount = indentCount;
      ChildIndentCount = 0;
    }
    #endregion
  }
}
