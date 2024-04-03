// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Solution.cs
using LJCNetCommon;
using System;
using System.Collections.Generic;

namespace ProjectFilesDAL
{
  /// <summary>The Solution Data Object.</summary>
  public class Solution : IComparable<Solution>
  {
    #region Static Functions

    // Checks for the required item values.
    /// <include path='items/ItemValues/*' file='Doc/Solution.xml'/>
    public static void ItemValues(ref string message, Solution solution)
    {
      if (solution != null)
      {
        ItemParentValues(ref message, solution);
        if (!NetString.HasValue(solution.Name))
        {
          message += $"{solution.Name}";
        }
      }
    }

    // Checks for the required ParentKey values.
    /// <include path='items/ItemParentValues/*' file='Doc/Solution.xml'/>
    public static void ItemParentValues(ref string message, Solution solution)
    {
      if (solution != null)
      {
        if (!NetString.HasValue(solution.CodeLine))
        {
          message += $"{solution.CodeLine}";
        }
        if (!NetString.HasValue(solution.CodeGroup))
        {
          message += $"{solution.CodeGroup}";
        }
      }
    }

    // Checks the ParentKey for values.
    /// <include path='items/ParentKeyValues/*' file='Doc/Solution.xml'/>
    public static void ParentKeyValues(ref string message
      , SolutionParentKey parentKey)
    {
      if (parentKey != null)
      {
        if (!NetString.HasValue(parentKey.CodeLine))
        {
          message += $"{parentKey.CodeLine}";
        }
        if (!NetString.HasValue(parentKey.CodeGroup))
        {
          message += $"{parentKey.CodeGroup}";
        }
      }
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public Solution Clone()
    {
      var retValue = MemberwiseClone() as Solution;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public int CompareTo(Solution other)
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
          retValue = CodeGroup.CompareTo(other.CodeGroup);
          if (NetString.CompareEqual == retValue)
          {
            retValue = Name.CompareTo(other.Name);
          }
        }
      }
      return retValue;
    }
    #endregion

    #region Data Properties

    /// <summary>Gets or sets the CodeLine name.</summary>
    public string CodeLine { get; set; }

    /// <summary>Gets or sets the CodeGroup name.</summary>
    public string CodeGroup { get; set; }

    /// <summary>Gets or sets the name.</summary>
    public string Name { get; set; }

    /// <summary>Gets or sets the sequence.</summary>
    public int Sequence { get; set; }

    /// <summary>Gets or sets the path.</summary>
    public string Path { get; set; }
    #endregion
  }

  /// <summary>
  /// 
  /// </summary>
  public class SolutionParentKey
  {
    /// <summary>Gets or sets the CodeLine name.</summary>
    public string CodeLine { get; set; }

    /// <summary>Gets or sets the CodeGroup name.</summary>
    public string CodeGroup { get; set; }
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class SolutionPath : IComparer<Solution>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public int Compare(Solution x, Solution y)
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

  /// <summary>Sort and search on Name value.</summary>
  public class SolutionSequence : IComparer<Solution>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public int Compare(Solution x, Solution y)
    {
      int retValue;

      retValue = NetCommon.CompareNull(x, y);
      if (NetString.CompareNotNullOrEqual == retValue)
      {
        // Case sensitive.
        retValue = x.CodeLine.CompareTo(y.CodeLine);
        if (NetString.CompareEqual == retValue)
        {
          retValue = x.CodeGroup.CompareTo(y.CodeGroup);
          if (NetString.CompareEqual == retValue)
          {
            // Not case sensitive.
            retValue = x.Sequence.CompareTo(y.Sequence);
          }
        }
      }
      return retValue;
    }
  }
  #endregion
}
