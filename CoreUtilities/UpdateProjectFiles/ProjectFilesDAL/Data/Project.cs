// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Project.cs
using LJCNetCommon;
using System;
using System.Collections.Generic;

namespace ProjectFilesDAL
{
  /// <summary>The Project Data Object.</summary>
  public class Project : IComparable<Project>
  {
    #region Static Functions

    // Checks for the required object values.
    /// <include path='items/ItemValues/*' file='Doc/Project.xml'/>
    public static string ItemValues(Project project)
    {
      var retValue = ItemParentValues(project);
      if (!NetString.HasValue(project.Name))
      {
        retValue += $"{project.Name}";
      }
      return retValue;
    }

    // Checks for the required ParentKey values.
    /// <include path='items/ItemParentValues/*' file='Doc/Project.xml'/>
    public static string ItemParentValues(Project project)
    {
      string retValue = "";

      if (project != null)
      {
        if (!NetString.HasValue(project.CodeLine))
        {
          retValue += $"{project.CodeLine}";
        }
        if (!NetString.HasValue(project.CodeGroup))
        {
          retValue += $"{project.CodeGroup}";
        }
        if (!NetString.HasValue(project.Solution))
        {
          retValue += $"{project.Solution}";
        }
      }
      return retValue;
    }

    // Checks the ParentKey for values.
    /// <include path='items/ParentKeyValues/*' file='Doc/Project.xml'/>
    public static string ParentKeyValues(ProjectParentKey parentKey)
    {
      string retValue = "";

      if (!NetString.HasValue(parentKey.CodeLine))
      {
        retValue += $"{parentKey.CodeLine}";
      }
      if (!NetString.HasValue(parentKey.CodeGroup))
      {
        retValue += $"{parentKey.CodeGroup}";
      }
      if (!NetString.HasValue(parentKey.Solution))
      {
        retValue += $"{parentKey.Solution}";
      }
      return retValue;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public Project Clone()
    {
      var retValue = MemberwiseClone() as Project;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public int CompareTo(Project other)
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
            retValue = Solution.CompareTo(other.Solution);
            if (NetString.CompareEqual == retValue)
            {
              retValue = Name.CompareTo(other.Name);
            }
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

    /// <summary>Gets or sets the Solution name.</summary>
    public string Solution { get; set; }

    /// <summary>Gets or sets the name.</summary>
    public string Name { get; set; }

    /// <summary>Gets or sets the path.</summary>
    public string Path { get; set; }
    #endregion
  }

  /// <summary>
  /// 
  /// </summary>
  public class ProjectParentKey
  {
    /// <summary>Gets or sets the CodeLine name.</summary>
    public string CodeLine { get; set; }

    /// <summary>Gets or sets the CodeGroup name.</summary>
    public string CodeGroup { get; set; }

    /// <summary>Gets or sets the Solution name.</summary>
    public string Solution { get; set; }
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class ProjectPath : IComparer<Project>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public int Compare(Project x, Project y)
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
            retValue = x.CodeGroup.CompareTo(y.CodeGroup);
            if (NetString.CompareEqual == retValue)
            {
              retValue = x.Solution.CompareTo(y.Solution);
              if (NetString.CompareEqual == retValue)
              {
                retValue = x.Path.CompareTo(y.Path);
              }
            }
          }
        }
      }
      return retValue;
    }
  }
  #endregion
}
