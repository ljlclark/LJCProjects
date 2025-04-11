// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJC.cs

namespace LJCNetCommon
{
  /// <summary>LJC specific methods.</summary>
  public class LJCHTML
  {
    #region Static Functions

    /// <summary>Returns default author.</summary>
    public static string Author()
    {
      return "Lester J. Clark";
    }

    // Returns default HTML Beginning.
    /// <summary>
    /// Returns default HTML Beginning.
    /// </summary>
    /// <param name="textState"></param>
    /// <param name="fileName">The HTML document file name.</param>
    /// <returns>The HTML begin text.</returns>
    public static string GetHTMLBegin(TextState textState
      , string fileName = null)
    {
      // Begin "Get String" method.
      var hb = new HTMLBuilder();
      hb.AddIndent(textState.IndentCount);
      var syncState = hb.TextState;

      string[] copyright = new string[]
      {
        "Copyright(c) Lester J. Clark and Contributors",
        "Licensed under the MIT License",
      };
      hb.HTMLBegin(syncState, copyright, fileName);

      // End "Create String" method.
      textState.IndentCount = syncState.IndentCount;
      var retValue = hb.ToString();
      return retValue;
    }
    #endregion
  }
}
