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
    #region Static Methods

    // Checks for the required item values.
    /// <summary>
    /// Checks for the required item values.
    /// </summary>
    /// <param name="message">The message value.</param>
    /// <param name="projectFile">The Solution objct.</param>
    public static void ItemValues(ref string message, ProjectFile projectFile)
    {
      if (projectFile != null)
      {
        ItemParentValues(ref message, projectFile);
        if (!NetString.HasValue(projectFile.FileName))
        {
          message += $"{projectFile.FileName}";
        }
      }
    }

    // Checks for the required ParentKey values.
    /// <summary>
    /// Checks for the required ParentKey values.
    /// </summary>
    /// <param name="message">The message value.</param>
    /// <param name="projectFile">The Solution objct.</param>
    public static void ItemParentValues(ref string message, ProjectFile projectFile)
    {
      if (projectFile != null)
      {
        if (!NetString.HasValue(projectFile.TargetCodeLine))
        {
          message += $"{projectFile.TargetCodeLine}";
        }
        if (!NetString.HasValue(projectFile.TargetCodeGroup))
        {
          message += $"{projectFile.TargetCodeGroup}";
        }
        if (!NetString.HasValue(projectFile.TargetSolution))
        {
          message += $"{projectFile.TargetSolution}";
        }
        if (!NetString.HasValue(projectFile.TargetProject))
        {
          message += $"{projectFile.TargetProject}";
        }
      }
    }

    // Checks for the required ParentKey values.
    /// <summary>
    /// Checks for the required ParentKey values.
    /// </summary>
    /// <param name="message">The message value.</param>
    /// <param name="projectFile">The Solution objct.</param>
    public static void ItemSourceValues(ref string message
      , ProjectFile projectFile)
    {
      if (projectFile != null)
      {
        if (!NetString.HasValue(projectFile.SourceCodeLine))
        {
          message += $"{projectFile.SourceCodeLine}";
        }
        if (!NetString.HasValue(projectFile.SourceCodeGroup))
        {
          message += $"{projectFile.SourceCodeGroup}";
        }
        if (!NetString.HasValue(projectFile.SourceSolution))
        {
          message += $"{projectFile.SourceSolution}";
        }
        if (!NetString.HasValue(projectFile.SourceFilePath))
        {
          message += $"{projectFile.SourceFilePath}";
        }
        if (!NetString.HasValue(projectFile.FileName))
        {
          message += $"{projectFile.FileName}";
        }
      }
    }

    // Checks the ParentKey for values.
    /// <summary>
    /// Checks the ParentKey for values.
    /// </summary>
    /// <param name="message">The message value.</param>
    /// <param name="parentKey">The ParentKey object.</param>
    public static void ParentKeyValues(ref string message
      , ProjectFileParentKey parentKey)
    {
      if (parentKey != null)
      {
        if (!NetString.HasValue(parentKey.CodeLine))
        {
          message += $"{parentKey.CodeLine} is missing.";
        }
        if (!NetString.HasValue(parentKey.CodeGroup))
        {
          message += $"{parentKey.CodeGroup} is missing.";
        }
        if (!NetString.HasValue(parentKey.Solution))
        {
          message += $"{parentKey.Solution} is missing.";
        }
        if (!NetString.HasValue(parentKey.Project))
        {
          message += $"{parentKey.Project} is missing.";
        }
      }
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public ProjectFile Clone()
    {
      var retValue = MemberwiseClone() as ProjectFile;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCGenDoc/Common/Data.xml'/>
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
              if (NetString.HasValue(TargetProject))
              {
                retValue = TargetProject.CompareTo(other.TargetProject);
                if (NetString.CompareEqual == retValue)
                {
                  retValue = FileName.CompareTo(other.FileName);
                }
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

    /// <summary>Gets or sets the TargetPath CodeGroup name.</summary>
    public string TargetPathCodeGroup { get; set; }

    /// <summary>Gets or sets the TargetPath Solution name.</summary>
    public string TargetPathSolution { get; set; }

    /// <summary>Gets or sets the TargetPath Project name.</summary>
    public string TargetPathProject { get; set; }

    /// <summary>Gets or sets the file name.</summary>
    public string FileName { get; set; }

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
    public string FileName { get; set; }
  }
}
