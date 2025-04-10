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
    public TextState(bool hasText = false, int indentCount = 0)
    {
      HasText = hasText;
      IndentCount = indentCount;
    }
    #endregion

    #region Properties

    /// <summary>Indicates if the builder has text.</summary>
    public bool HasText { get; set; }

    /// <summary>The current indent count.</summary>
    public int IndentCount { get; set; }
    #endregion
  }
}
