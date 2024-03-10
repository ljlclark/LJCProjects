// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CodeLine.cs
using System;

namespace ProjectFilesDAL
{
  /// <summary>The CodeLine Data Object.</summary>
  public class CodeLine : IComparable<CodeLine>
  {
    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public CodeLine Clone()
    {
      var retValue = MemberwiseClone() as CodeLine;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(CodeLine other)
    {
      int retValue;

      if (null == other)
      {
        // This value is greater than null.
        retValue = 1;
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
}
