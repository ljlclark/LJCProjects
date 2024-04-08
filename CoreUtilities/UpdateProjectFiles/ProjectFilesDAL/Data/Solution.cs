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
    public static string ItemValues(Solution solution)
    {
      var retValue = ItemParentValues(solution);
      if (NetString.HasValue(retValue))
      {
        if (!NetString.HasValue(solution.Name))
        {
          retValue += "solution.Name\r\n";
        }
      }
      return retValue;
    }

    // Checks for the required ParentKey values.
    /// <include path='items/ItemParentValues/*' file='Doc/Solution.xml'/>
    public static string ItemParentValues(Solution solution)
    {
      string retValue = "";

      if (null == solution)
      {
        retValue += "solution\r\n";
      }
      else
      {
        if (!NetString.HasValue(solution.CodeLine))
        {
          retValue += "solution.CodeLine\r\n";
        }
        if (!NetString.HasValue(solution.CodeGroup))
        {
          retValue += "solution.CodeGroup\r\n";
        }
      }
      return retValue;
    }

    // Checks the ParentKey for values.
    /// <include path='items/ParentKeyValues/*' file='Doc/Solution.xml'/>
    public static string ParentKeyValues(SolutionParentKey parentKey)
    {
      string retValue = "";

      if (null == parentKey)
      {
        retValue += "parentKey\r\n";
      }
      else
      {
        if (!NetString.HasValue(parentKey.CodeLine))
        {
          retValue += "parentKey.CodeLine\r\n";
        }
        if (!NetString.HasValue(parentKey.CodeGroup))
        {
          retValue += "parentKey.CodeGroup\r\n";
        }
      }
      return retValue;
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
