﻿// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CodeLine.cs
using LJCNetCommon;
using System;
using System.Collections.Generic;

namespace ProjectFilesDAL
{
  /// <summary>The CodeLine Data Object.</summary>
  public class CodeLine : IComparable<CodeLine>
  {
    #region Static Methods

    // Checks for the required object values.
    /// <summary>
    /// Checks for the required object values.
    /// </summary>
    /// <param name="message">The message value.</param>
    /// <param name="codeLine">The CodeLine value.</param>
    public static void ItemValues(ref string message, CodeLine codeLine)
    {
      if (codeLine != null)
      {
        if (!NetString.HasValue(codeLine.Name))
        {
          message += $"{codeLine.Name}";
        }
      }
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public CodeLine Clone()
    {
      var retValue = MemberwiseClone() as CodeLine;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public int CompareTo(CodeLine other)
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
        retValue = Name.CompareTo(other.Name);
      }
      return retValue;
    }
    #endregion

    #region Data Properties

    /// <summary>Gets or sets the name.</summary>
    public string Name { get; set; }

    /// <summary>Gets or sets the path</summary>
    public string Path { get; set; }
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class CodeLinePathComparer : IComparer<CodeLine>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public int Compare(CodeLine x, CodeLine y)
    {
      int retValue;

      retValue = NetCommon.CompareNull(x, y);
      if (NetString.CompareNotNullOrEqual == retValue)
      {
        retValue = NetCommon.CompareNull(x.Path, y.Path);
        if (NetString.CompareNotNullOrEqual == retValue)
        {
          // Case sensitive.
          retValue = x.Path.CompareTo(y.Path);
        }
      }
      return retValue;
    }
  }
  #endregion
}
