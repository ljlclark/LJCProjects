// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CodeGroup.cs
using LJCNetCommon;
using System;
using System.Collections.Generic;

namespace ProjectFilesDAL
{
  /// <summary>The CodeGroup Data Object.</summary>
  public class CodeGroup : IComparable<CodeGroup>
  {
    #region Static Functions

    // Checks for the required object values.
    /// <include path='items/ItemValues/*' file='Doc/CodeGroup.xml'/>
    public static string ItemValues(CodeGroup codeGroup)
    {
      var retValue = ItemParentValues(codeGroup);
      if (!NetString.HasValue(codeGroup.Name))
      {
        retValue += $"{codeGroup.Name}";
      }
      return retValue;
    }

    // Checks for the required object values.
    /// <include path='items/ItemParentValues/*' file='Doc/CodeGroup.xml'/>
    public static string ItemParentValues(CodeGroup codeGroup)
    {
      string retValue = "";

      if (codeGroup != null)
      {
        if (!NetString.HasValue(codeGroup.CodeLine))
        {
          retValue += $"{codeGroup.CodeLine}";
        }
      }
      return retValue;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public CodeGroup Clone()
    {
      var retValue = MemberwiseClone() as CodeGroup;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public int CompareTo(CodeGroup other)
    {
      int retValue;

      if (null == other)
      {
        // This value is greater than null.
        retValue = NetString.CompareGreater;
      }
      else
      {
        // Case sensitive.
        retValue = CodeLine.CompareTo(other.CodeLine);
        if (NetString.CompareEqual == retValue)
        {
          retValue = Name.CompareTo(other.Name);
        }
      }
      return retValue;
    }
    #endregion

    #region Data Properties

    /// <summary>Gets or sets the CodeLine name.</summary>
    public string CodeLine { get; set; }

    /// <summary>Gets or sets the name.</summary>
    public string Name { get; set; }

    /// <summary>Gets or sets the path.</summary>
    public string Path { get; set; }
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Path value.</summary>
  public class CodeGroupPathComparer : IComparer<CodeGroup>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public int Compare(CodeGroup x, CodeGroup y)
    {
      int retValue;

      retValue = NetCommon.CompareNull(x, y);
      if (NetString.CompareNotNullOrEqual == retValue)
      {
        retValue = NetCommon.CompareNull(x.Path, y.Path);
        if (NetString.CompareNotNullOrEqual == retValue)
        {
          // Case sensitive.
          retValue = x.CodeLine.CompareTo(y.CodeLine);
          if (NetString.CompareEqual == retValue)
          {
            retValue = x.Path.CompareTo(y.Path);
          }
        }
      }
      return retValue;
    }
  }
  #endregion
}
