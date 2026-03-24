// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// LJCNetString.cs
using System.Diagnostics.CodeAnalysis;

namespace LJCNetCommon
{
  // Contains common string related static functions.
  /// <include path="members/LJCNetString/*" file="Doc/LJCNetString.xml"/>
  public class LJCNetString
  {
    #region Checking String Values

    // Checks if a text value exists.
    /// <include path="members/HasValue/*" file="Doc/LJCNetString.xml"/>
    public static bool HasValue([NotNullWhen(true)] string? text)
    {
      return !string.IsNullOrWhiteSpace(text);
    }

    // Do an Ignore Case string compare.
    /// <include path="members/IsEqual/*" file="Doc/LJCNetString.xml"/>
    public static bool IsEqual(string? stringA, string? stringB)
    {
      bool retValue = false;

      if (stringA != null)
      {
        retValue = stringA.Equals(stringB
          , System.StringComparison.InvariantCultureIgnoreCase);
      }

      if (null == stringA
        && null == stringB)
      {
        retValue = true;
      }
      return retValue;
    }
    #endregion
  }
}
