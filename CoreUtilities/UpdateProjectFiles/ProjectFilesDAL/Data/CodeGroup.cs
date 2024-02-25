// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CodeGroup.cs
using System;

namespace ProjectFilesDAL
{
  /// <summary>The CodeGroup Data Object.</summary>
  public class CodeGroup : IComparable<CodeGroup> 
  {
    #region Data Methods

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(CodeGroup other)
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
        retValue = CodeLine.CompareTo(other.CodeLine);
        if (0 == retValue)
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
}
