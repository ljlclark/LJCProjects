// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CodeLine.cs
using System;

namespace ProjectFilesDAL
{
  public class CodeLline : IComparable<Solution>
  {
    #region Data Methods

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(Solution other)
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
