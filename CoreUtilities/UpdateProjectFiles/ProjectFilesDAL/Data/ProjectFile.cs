// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CodeLine.cs
using LJCNetCommon;
using System;

namespace ProjectFilesDAL
{
  /// <summary>The ProjectFile Data Object.</summary>
  public class ProjectFile : IComparable<ProjectFile>
  {
    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ProjectFile Clone()
    {
      var retValue = MemberwiseClone() as ProjectFile;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(ProjectFile other)
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
        retValue = TargetCodeLine.CompareTo(other.TargetCodeLine);
        if (NetString.CompareEqual == retValue)
        {
          retValue = TargetCodeGroup.CompareTo(other.TargetCodeGroup);
          if (NetString.CompareEqual == retValue)
          {
            retValue = TargetSolution.CompareTo(other.TargetSolution);
            if (NetString.CompareEqual == retValue)
            {
              retValue = TargetProject.CompareTo(other.TargetProject);
              if (NetString.CompareEqual == retValue)
              {
                retValue = SourceFileName.CompareTo(other.SourceFileName);
              }
            }
          }
        }
      }
      return retValue;
    }
    #endregion

    #region Data Properties

    /// <summary>Gets or sets the Target CodeLine name.</summary>
    public string TargetCodeLine { get; set; }

    /// <summary>Gets or sets the Target CodeGroup name.</summary>
    public string TargetCodeGroup { get; set; }

    /// <summary>Gets or sets the Target Solution name.</summary>
    public string TargetSolution { get; set; }

    /// <summary>Gets or sets the Target Project name.</summary>
    public string TargetProject { get; set; }

    /// <summary>Gets or sets the Source file name.</summary>
    public string SourceFileName { get; set; }

    /// <summary>Gets or sets the Source CodeLine name.</summary>
    public string SourceCodeLine { get; set; }

    /// <summary>Gets or sets the Source CodeGroup name.</summary>
    public string SourceCodeGroup { get; set; }

    /// <summary>Gets or sets the Source Solution name.</summary>
    public string SourceSolution { get; set; }

    /// <summary>Gets or sets the Source Project name.</summary>
    public string SourceProject { get; set; }

    /// <summary>Gets or sets the Source file path.</summary>
    public string SourceFilePath { get; set; }

    /// <summary>Gets or sets the Target file path.</summary>
    public string TargetFilePath { get; set; }
    #endregion
  }

  /// <summary></summary>
  public class ProjectFileParentKey
  {
    /// <summary>Gets or sets the Target CodeLine name.</summary>
    public string CodeLine { get; set; }

    /// <summary>Gets or sets the Target CodeGroup name.</summary>
    public string CodeGroup { get; set; }

    /// <summary>Gets or sets the Target Solution name.</summary>
    public string Solution { get; set; }

    /// <summary>Gets or sets the Target Project name.</summary>
    public string Project { get; set; }

    /// <summary>Gets or sets the Source file name.</summary>
    public string SourceFileName { get; set; }
  }
}
