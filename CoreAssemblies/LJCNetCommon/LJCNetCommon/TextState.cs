// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TextState.cs

namespace LJCNetCommon
{
  /// <summary>Represents the text state.</summary>
  public class TextState
  {
    #region Constructors
    /// <summary>
    /// Initializes an object instance with the supplied values.
    /// </summary>
    public TextState(int indentCount = 0)
    {
      IndentCount = indentCount;
      NewIndentCount = 0;
    }
    #endregion

    #region Properties

    /// <summary>The current indent count to sync called method.</summary>
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

    /// <summary>The new indent count to sync calling method.</summary>
    public int NewIndentCount
    {
      get { return mNewIndentCount; }
      set
      {
        mNewIndentCount = 0;
        if (value >= 0)
        {
          mNewIndentCount = value;
        }
      }
    }
    private int mNewIndentCount;
    #endregion
  }
}
