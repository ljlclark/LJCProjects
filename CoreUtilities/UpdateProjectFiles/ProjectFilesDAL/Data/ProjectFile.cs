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
    /// <include path='items/ItemValues/*' file='Doc/ProjectFile.xml'/>
    public static string ItemValues(ProjectFile projectFile)
    {
      var retValue = ItemParentValues(projectFile);
      if (!NetString.HasValue(projectFile.FileName))
      {
        retValue += "projectFile.FileName\r\n";
      }
      return retValue;
    }

    // Checks for the required ParentKey values.
    /// <include path='items/ItemParentValues/*' file='Doc/ProjectFile.xml'/>
    public static string ItemParentValues(ProjectFile projectFile)
    {
      string retValue = "";

      if (null == projectFile)
      {
        retValue += "projectFile\r\n";
      }
      else
      {
        if (!NetString.HasValue(projectFile.TargetCodeLine))
        {
          retValue += "projectFile.TargetCodeLine\r\n";
        }
        if (!NetString.HasValue(projectFile.TargetCodeGroup))
        {
          retValue += "projectFile.TargetCodeGroup\r\n";
        }
        if (!NetString.HasValue(projectFile.TargetSolution))
        {
          retValue += "projectFile.TargetSolution\r\n";
        }
        if (!NetString.HasValue(projectFile.TargetProject))
        {
          retValue += "projectFile.TargetProject\r\n";
        }
      }
      return retValue;
    }

    // Checks for the required ParentKey values.
    /// <include path='items/ItemSourceValues/*' file='Doc/ProjectFile.xml'/>
    public static string ItemSourceValues(ProjectFile projectFile)
    {
      string retValue = "";

      if (null == projectFile)
      {
        retValue += "projectFile\r\n";
      }
      else
      {
        if (!NetString.HasValue(projectFile.SourceCodeLine))
        {
          retValue += "projectFile.SourceCodeLine\r\n";
        }
        if (!NetString.HasValue(projectFile.SourceCodeGroup))
        {
          retValue += "projectFile.SourceCodeGroup\r\n";
        }
        if (!NetString.HasValue(projectFile.SourceSolution))
        {
          retValue += "projectFile.SourceSolution\r\n";
        }
        if (!NetString.HasValue(projectFile.SourceFilePath))
        {
          retValue += "projectFile.SourceFilePath\r\n";
        }
        if (!NetString.HasValue(projectFile.FileName))
        {
          retValue += "projectFile.FileName\r\n";
        }
      }
      return retValue;
    }

    // Checks the ParentKey for values.
    /// <include path='items/ParentKeyValues/*' file='Doc/ProjectFile.xml'/>
    public static string ParentKeyValues(ProjectFileParentKey parentKey)
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
        if (!NetString.HasValue(parentKey.Solution))
        {
          retValue += "parentKey.Solution\r\n";
        }
        if (!NetString.HasValue(parentKey.Project))
        {
          retValue += "parentKey.Project\r\n";
        }
      }
      return retValue;
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
