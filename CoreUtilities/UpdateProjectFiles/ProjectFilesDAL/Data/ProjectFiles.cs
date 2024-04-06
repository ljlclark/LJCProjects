﻿// Copyright(c) Lester J. Clark and Contributors.
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
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public ProjectFiles()
    {
      mPrevCount = -1;
    }
    #endregion

    #region Collection Methods

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public ProjectFiles Clone()
    {
      var retValue = MemberwiseClone() as ProjectFiles;
      return retValue;
    }

    // Gets a custom collection from List<T>.
    /// <include path='items/GetCollection/*' file='../../LJCGenDoc/Common/Collection.xml'/>
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

    // Creates and adds the object with the provided values.
    /// <include path='items/Add/*' file='Doc/ProjectFiles.xml'/>
    public ProjectFile Add(ProjectFileParentKey parentKey
      , ProjectFileParentKey sourceKey, string fileName
      , string targetFilePath, string sourceFilePath)
    {
      ProjectFile retValue;

      // Do not add duplicate of existing item.
      retValue = LJCRetrieve(parentKey, fileName);
      if (null == retValue)
      {
        retValue = new ProjectFile()
        {
          TargetCodeLine = parentKey.CodeLine,
          TargetCodeGroup = parentKey.CodeGroup,
          TargetSolution = parentKey.Solution,
          TargetProject = parentKey.Project,
          FileName = fileName,
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
    /// <include path='items/LJCDelete/*' file='Doc/ProjectFiles.xml'/>
    public void LJCDelete(ProjectFileParentKey parentKey, string fileName)
    {
      ProjectFile item = LJCRetrieve(parentKey, fileName);
      if (item != null)
      {
        Remove(item);
      }
    }

    // Retrieves items that match the supplied values.
    /// <include path='items/LJCLoad/*' file='Doc/ProjectFiles.xml'/>
    public ProjectFiles LJCLoad(ProjectFileParentKey parentKey = null)
    {
      ProjectFiles retValue = null;

      if (null == parentKey)
      {
        retValue = Clone();
      }
      else
      {
        string message = "";
        ProjectFile.ParentKeyValues(ref message, parentKey);
        NetString.ThrowArgError(message);

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
    /// <include path='items/LJCRetrieve/*' file='Doc/ProjectFiles.xml'/>
    public ProjectFile LJCRetrieve(ProjectFileParentKey parentKey, string fileName)
    {
      ProjectFile retValue = null;

      string message = "";
      string context = ClassContext + "LJCRetrieve()";
      NetString.ArgError(ref message, parentKey, "parentKey", context);
      NetString.ArgError(ref message, fileName, "fileName");
      ProjectFile.ParentKeyValues(ref message, parentKey);
      NetString.ThrowArgError(message);

      LJCSortUnique();
      ProjectFile searchItem = new ProjectFile()
      {
        TargetCodeLine = parentKey.CodeLine,
        TargetCodeGroup = parentKey.CodeGroup,
        TargetSolution = parentKey.Solution,
        TargetProject = parentKey.Project,
        FileName = fileName
      };
      int index = BinarySearch(searchItem);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }

    // Finds and updates the collection item.
    /// <include path='items/LJCUpdate/*' file='Doc/ProjectFiles.xml'/>
    public void LJCUpdate(ProjectFile projectFile)
    {
      if (NetCommon.HasItems(this))
      {
        string message = "";
        string context = ClassContext + "LJCUpdate()";
        NetString.ArgError(ref message, projectFile, "projectFile", context);
        ProjectFile.ItemValues(ref message, projectFile);
        NetString.ThrowArgError(message);

        var parentKey = GetParentKey(projectFile);
        var item = LJCRetrieve(parentKey, projectFile.FileName);
        if (item != null)
        {
          item.TargetPathCodeGroup = projectFile.TargetPathCodeGroup;
          item.TargetPathSolution = projectFile.TargetPathSolution;
          item.TargetPathProject = projectFile.TargetPathProject;
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

    // Retrieves the ParentKey from the object.
    /// <include path='items/GetParentKey/*' file='Doc/ProjectFiles.xml'/>
    public ProjectFileParentKey GetParentKey(ProjectFile projectFile)
    {
      string message = "";
      string context = ClassContext + "GetParentKey()";
      NetString.ArgError(ref message, projectFile, "projectFile", context);
      ProjectFile.ItemParentValues(ref message, projectFile);
      NetString.ThrowArgError(message);

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
    private const string ClassContext = "ProjectFilesDAL.ProjectFiles.";
    #endregion
  }
}
