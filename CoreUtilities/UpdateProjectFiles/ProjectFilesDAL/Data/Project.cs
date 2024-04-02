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
    #region Static Methods

    // Checks for the required object values.
    /// <summary>
    /// Checks for the required object values.
    /// </summary>
    /// <param name="message">The message value.</param>
    /// <param name="project">The Project objct.</param>
    /// <param name="name">The name value.</param>
    public static void ItemValues(ref string message, Project project
      , string name)
    {
      if (project != null)
      {
        ItemParentValues(ref message, project);
        if (!NetString.HasValue(name))
        {
          message += $"{name}";
        }
      }
    }

    // Checks for the required ParentKey values.
    /// <summary>
    /// Checks for the required ParentKey values.
    /// </summary>
    /// <param name="message">The message value.</param>
    /// <param name="project">The Project objct.</param>
    public static void ItemParentValues(ref string message, Project project)
    {
      if (project != null)
      {
        if (!NetString.HasValue(project.CodeLine))
        {
          message += $"{project.CodeLine}";
        }
        if (!NetString.HasValue(project.CodeGroup))
        {
          message += $"{project.CodeGroup}";
        }
        if (!NetString.HasValue(project.Solution))
        {
          message += $"{project.Solution}";
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
      , ProjectParentKey parentKey)
    {
      if (parentKey != null)
      {
        if (!NetString.HasValue(parentKey.CodeLine))
        {
          message += $"{parentKey.CodeLine}";
        }
        if (!NetString.HasValue(parentKey.CodeGroup))
        {
          message += $"{parentKey.CodeGroup}";
        }
        if (!NetString.HasValue(parentKey.Solution))
        {
          message += $"{parentKey.Solution}";
        }
      }
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
}
