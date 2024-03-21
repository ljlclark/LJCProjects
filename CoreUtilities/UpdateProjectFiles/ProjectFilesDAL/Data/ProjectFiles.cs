// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProjectFiles.cs
using LJCNetCommon;
using System.Collections.Generic;

namespace ProjectFilesDAL
{
  /// <summary>Represents a collection of ProjectFile Data Objects.</summary>
  public class ProjectFiles : List<ProjectFile>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ProjectFiles()
    {
      mPrevCount = -1;
    }
    #endregion

    #region Collection Methods

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ProjectFiles Clone()
    {
      var retValue = MemberwiseClone() as ProjectFiles;
      return retValue;
    }

    // Gets a custom collection from List<T>.
    /// <include path='items/GetCollection/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public ProjectFiles GetCollection(List<ProjectFile> list)
    {
      ProjectFiles retValue = null;

      if (NetCommon.HasItems(list))
      {
        retValue = new ProjectFiles();
        foreach (ProjectFile item in list)
        {
          retValue.Add(item);
        }
      }
      return retValue;
    }
    #endregion

    #region Data Methods

    // Creates and adds the object from the provided values.
    /// <include path='items/Add/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public ProjectFile Add(ProjectFileParentKey parentKey
      , ProjectFileParentKey sourceKey, string sourceFileName
      , string targetFilePath, string sourceFilePath)
    {
      ProjectFile retValue;

      string message = "";
      NetString.AddMissingArgument(message, parentKey);
      AddMissingValues(message, parentKey);
      NetString.AddMissingArgument(message, sourceKey);
      NetString.ThrowInvalidArgument(message);

      retValue = LJCRetrieve(parentKey, sourceFileName);
      if (null == retValue)
      {
        retValue = new ProjectFile()
        {
          TargetCodeLine = parentKey.CodeLine,
          TargetCodeGroup = parentKey.CodeGroup,
          TargetSolution = parentKey.Solution,
          TargetProject = parentKey.Project,
          SourceFileName = sourceFileName,
          TargetFilePath = targetFilePath,
          SourceCodeLine = sourceKey.CodeLine,
          SourceCodeGroup = sourceKey.CodeGroup,
          SourceSolution = sourceKey.Solution,
          SourceProject = sourceKey.Project,
          SourceFilePath = sourceFilePath
        };
        Add(retValue);
      }
      return retValue;
    }

    // Removes an item by unique values.
    /// <summary>
    /// Removes an item by unique values.
    /// </summary>
    /// <param name="parentKey">The ParentKey value.</param>
    public void LJCDelete(ProjectFileParentKey parentKey, string sourceFileName)
    {
      ProjectFile item = LJCRetrieve(parentKey, sourceFileName);
      if (item != null)
      {
        Remove(item);
      }
    }

    // Retrieves items that match the supplied values.
    /// <summary>
    /// Retrieves a collection that match the supplied values.
    /// </summary>
    /// <param name="parentKey">The ParentKey value.</param>
    /// <returns>The collection object.</returns>
    public ProjectFiles LJCLoad(ProjectFileParentKey parentKey)
    {
      ProjectFiles retValue = null;

      if (parentKey != null)
      {
        var items = FindAll(x =>
          x.TargetCodeLine == parentKey.CodeLine
          && x.TargetCodeGroup == parentKey.CodeGroup
          && x.TargetSolution == parentKey.Solution
          && x.TargetProject == parentKey.Project);
        retValue = GetCollection(items);
      }
      return retValue;
    }

    // Retrieves the collection element with unique values.
    /// <summary>
    /// Retrieves the collection element with unique values.
    /// </summary>
    /// <param name="parentKey">The ParentKey value.</param>
    /// <param name="sourceFileName">The SourceFile name.</param>
    /// <returns>A reference to the matching item.</returns>
    public ProjectFile LJCRetrieve(ProjectFileParentKey parentKey, string sourceFileName)
    {
      ProjectFile retValue = null;

      LJCSortUnique();
      ProjectFile searchItem = new ProjectFile()
      {
        TargetCodeLine = parentKey.CodeLine,
        TargetCodeGroup = parentKey.CodeGroup,
        TargetSolution = parentKey.Solution,
        TargetProject = parentKey.Project,
        SourceFileName = sourceFileName
      };
      int index = BinarySearch(searchItem);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }

    // Finds and updates the collection item.
    /// <summary>
    /// Finds and updates the collection item.
    /// </summary>
    /// <param name="codeGroup">The ProjectFile object.</param>
    public void LJCUpdate(ProjectFile projectFile)
    {
      if (NetCommon.HasItems(this))
      {
        var parentKey = GetParentKey(projectFile);
        var item = LJCRetrieve(parentKey, projectFile.SourceFileName);
        if (item != null)
        {
          item.SourceCodeLine = projectFile.SourceCodeLine;
          item.SourceCodeGroup = projectFile.SourceCodeGroup;
          item.SourceSolution = projectFile.SourceSolution;
          item.SourceProject = projectFile.SourceProject;
          item.TargetFilePath = projectFile.TargetFilePath;
          item.SourceFilePath = projectFile.SourceFilePath;
        }
      }
    }
    #endregion

    #region Public Methods

    // Adds the missing ParentKey values messages.
    /// <summary>
    /// Adds the missing ParentKey values messages.
    /// </summary>
    /// <param name="message">The message value.</param>
    /// <param name="parentKey">The ParentKey object.</param>
    public void AddMissingValues(string message, ProjectFileParentKey parentKey)
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

    // Retrieves the ParentKey from the object.
    /// <summary>Retrieves the ParentKey from the object.</summary>
    /// <param name="project">The ProjectFile object.</param>
    /// <returns>The ParentKey object.</returns>
    public ProjectFileParentKey GetParentKey(ProjectFile projectFile)
    {
      var retValue = new ProjectFileParentKey()
      {
        CodeLine = projectFile.TargetCodeLine,
        CodeGroup = projectFile.TargetCodeGroup,
        Solution = projectFile.TargetSolution,
        Project = projectFile.TargetProject
      };
      return retValue;
    }

    /// <summary>Sorts on Unique values.</summary>
    public void LJCSortUnique()
    {
      if (Count != mPrevCount)
      {
        mPrevCount = Count;
        Sort();
      }
    }
    #endregion

    #region Class Data

    private int mPrevCount;
    #endregion
  }
}
