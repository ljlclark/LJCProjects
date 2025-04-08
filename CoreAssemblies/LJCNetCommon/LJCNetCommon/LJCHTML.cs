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
      textState.HasText = false;
      var hb = new HTMLBuilder(textState);
      string[] copyright = new string[]
      {
        "Copyright(c) Lester J. Clark and Contributors",
        "Licensed under the MIT License",
      };
      hb.CreateHTMLBegin(hb.TextState, copyright, fileName);
      var retValue = hb.ToString();
      return retValue;
    }
    #endregion
  }
}
