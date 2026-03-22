
using System.Diagnostics.CodeAnalysis;

namespace LJCNetCommon
{
  // Contains common string related static functions.
  /// <include path='items/LJCNetString/*' file='Doc/LJCNetString.xml'/>
  public class LJCNetString
  {
    #region Checking String Values

    // Checks if a text value exists.
    /// <include path='items/HasValue/*' file='Doc/LJCNetString.xml'/>
    public static bool HasValue([NotNullWhen(true)] string? text)
    {
      return !string.IsNullOrWhiteSpace(text);
    }
    #endregion
  }
}
