// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJC.cs

namespace LJCDataUtility
{
  /// <summary>LJC specific methods.</summary>
  public class LJC
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
    /// <param name="fileName">The HTML document file name.</param>
    /// <returns>The HTML begin text.</returns>
    public static string HTMLBegin(string fileName = null)
    {
      var hb = new HTMLBuilder();
      string[] copyright = new string[]
      {
        "Copyright(c) Lester J. Clark and Contributors",
        "Licensed under the MIT License",
      };
      hb.CreateHTMLBegin(copyright, fileName);
      var retValue = hb.ToString();
      return retValue;
    }
    #endregion
  }
}
