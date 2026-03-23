// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCTextState.cs

namespace LJCNetCommon
{
  /// <summary>Represents the text state.</summary>
  /// <include path='members/LJCTextState/*' file='Doc/LJCTextState.xml'/>
  public class LJCTextState
  {
    #region Constructors
    // Initializes an object instance with the supplied values.
    /// <include path='members/Constructor/*' file='Doc/LJCTextState.xml'/>
    public LJCTextState(int indentCount = 0)
    {
      IndentCount = indentCount;
      ChildIndentCount = 0;
    }
    #endregion

    #region Properties

    // Gets or sets the current indent count to sync called method.
    /// <include path='members/IndentCount/*' file='Doc/LJCTextState.xml'/>
    public int IndentCount
    {
      get { return mIndentCount; }
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
    /// <include path='members/ChildIndentCount/*' file='Doc/LJCTextState.xml'/>
    public int ChildIndentCount
    {
      get { return mChildIndentCount; }
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
  }
}
