// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CodeLine.cs
using System;

namespace ProjectFilesDAL
{
  public class ProjectFile : IComparable<ProjectFile>
  {
    #region Data Methods

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(ProjectFile other)
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
        retValue = TargetSolution.CompareTo(other.TargetSolution);
        if (0 == retValue)
        {
          retValue = TargetProject.CompareTo(other.TargetProject);
          if (0 == retValue)
          {
            retValue = SourceFileName.CompareTo(other.SourceFileName);
          }
        }
      }
      return retValue;
    }
    #endregion

    #region Data Properties

    /// <summary>Gets or sets the Target Solution name.</summary>
    public string TargetSolution { get; set; }

    /// <summary>Gets or sets the Target Project name.</summary>
    public string TargetProject { get; set; }

    /// <summary>Gets or sets the Source file name.</summary>
    public string SourceFileName { get; set; }

    /// <summary>Gets or sets the Source Project name.</summary>
    public string SourceProject { get; set; }

    /// <summary>Gets or sets the Source file spec.</summary>
    public string SourceFileSpec { get; set; }

    /// <summary>Gets or sets the Target file spec.</summary>
    public string TargetFileSpec { get; set; }
    #endregion
  }
}
