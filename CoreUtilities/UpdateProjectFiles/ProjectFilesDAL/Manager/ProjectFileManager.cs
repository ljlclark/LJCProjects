﻿// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProjectFileManager.cs
using LJCNetCommon;
using LJCTextDataReaderLib;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProjectFilesDAL
{
  /// <summary>The ProjectFile data manager.</summary>
  public class ProjectFileManager
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/ProjectFileManagerC/*' file='Doc/ProjectFileManager.xml'/>
    public ProjectFileManager(string fileName = @"DataFiles\ProjectFile.txt")
    {
      CreateBaseColumns();
      FileName = fileName;
      Reader = new TextDataReader();
      Reader.LJCSetFile(FileName);
    }
    #endregion

    #region Data Methods

    // Adds a ProjectFile record
    /// <include path='items/Add/*' file='Doc/ProjectFileManager.xml'/>
    public ProjectFile Add(ProjectFileParentKey parentKey, ProjectFileParentKey sourceKey
      , string fileName, string sourceFilePath, string targetFilePath)
    {
      ProjectFile retValue = null;

      if (HasParentKey(parentKey)
        && HasParentKey(sourceKey)
        && NetString.HasValue(fileName)
        && NetString.HasValue(sourceFilePath)
        && NetString.HasValue(targetFilePath))
      {
        var projectFile = CreateDataObject(parentKey, sourceKey
          , sourceFilePath, targetFilePath);
        var newRecord = CreateRecord(projectFile);
        Reader.Close();
        File.AppendAllText(FileName, newRecord);
        Reader.LJCOpen();
        retValue = Retrieve(parentKey, fileName);
      }
      return retValue;
    }

    // Deletes a ProjectFile record.
    /// <include path='items/Delete/*' file='Doc/ProjectFileManager.xml'/>
    public void Delete(ProjectFileParentKey parentKey, string fileName)
    {
      if (HasParentKey(parentKey)
        && NetString.HasValue(fileName))
      {
        var current = CurrentDataObject();
        var projectFiles = LoadAllExcept(parentKey, fileName);
        if (projectFiles != null)
        {
          WriteBackup();
          RecreateFile(projectFiles);
        }
        if (current != null)
        {
          Retrieve(parentKey, current.FileName);
        }
      }
    }

    // Retrieves a collection of ProjectFile records.
    /// <include path='items/Load/*' file='Doc/ProjectFileManager.xml'/>
    public ProjectFiles Load(ProjectFileParentKey parentKey = null
      , string fileName = null)
    {
      ProjectFiles retValue = null;

      var sourceKey = CurrentSourceKey();
      var project = CreateDataObject(parentKey, sourceKey, fileName, null);
      Reader.LJCOpen();
      if (Reader.Read())
      {
        retValue = new ProjectFiles();
        do
        {
          if (null == project
            || IsMatch(project))
          {
            retValue.Add(CurrentDataObject());
          }
        } while (Reader.Read());
        Reader.LJCOpen();
      }
      return retValue;
    }

    // Retrieves a collection of records that do NOT match the supplied Name
    /// <include path='items/LoadAllExcept/*' file='Doc/ProjectFileManager.xml'/>
    public ProjectFiles LoadAllExcept(ProjectFileParentKey parentKey
      , string fileName)
    {
      ProjectFiles retValue = null;

      Reader.LJCOpen();
      if (Reader.Read())
      {
        retValue = new ProjectFiles();
        do
        {
          var projectFile = CurrentDataObject();
          if (projectFile.TargetCodeLine != parentKey.CodeLine
            || projectFile.TargetCodeGroup != parentKey.CodeGroup
            || projectFile.TargetSolution != parentKey.Solution
            || projectFile.TargetProject != parentKey.Project
            || projectFile.FileName != fileName)
          {
            retValue.Add(projectFile);
          }
        } while (Reader.Read());
        Reader.LJCOpen();
      }
      return retValue;
    }

    // Retrieves a ProjectFile record.
    /// <include path='items/Retrieve/*' file='Doc/ProjectFileManager.xml'/>
    public ProjectFile Retrieve(ProjectFileParentKey parentKey, string fileName)
    {
      ProjectFile retValue = null;

      if (HasParentKey(parentKey))
      {
        if (NetString.HasValue(fileName))
        {
          Reader.LJCOpen();
        }
        bool success;
        while (success = Reader.Read())
        {
          var projectFile = CurrentDataObject();
          if (projectFile.TargetCodeLine == parentKey.CodeLine
            && projectFile.TargetCodeGroup == parentKey.CodeGroup
            && projectFile.TargetSolution == parentKey.Solution)
          {
            if (NetString.HasValue(fileName))
            {
              if (projectFile.FileName == fileName)
              {
                retValue = projectFile;
                break;
              }
            }
            else
            {
              // Get next item in Parent key.
              retValue = projectFile;
              break;
            }
          }
          else
          {
            success = false;
            break;
          }
        }
        if (!success)
        {
          Reader.LJCOpen();
        }
      }
      return retValue;
    }

    // Updates a record from the DataObject.
    /// <include path='items/Update/*' file='Doc/ProjectFileManager.xml'/>
    public Project Update(ProjectFile projectFile)
    {
      Project retValue = null;

      var parentKey = CreateParentKey(projectFile);
      if (HasParentKey(parentKey)
        && NetString.HasValue(projectFile.FileName))
      {
        var current = CurrentDataObject();
        var projectFiles = LoadAllExcept(parentKey, projectFile.FileName);
        if (projectFiles != null)
        {
          WriteBackup();
          RecreateFile(projectFiles);
        }
        Reader.Close();
        var text = CreateRecord(projectFile);
        File.AppendAllText(FileName, text);
        Reader.LJCOpen();
        Retrieve(parentKey, current.FileName);
      }
      return retValue;
    }
    #endregion

    #region Public Methods

    // Write the text file from a Solutions collection.
    /// <include path='items/CreateFile/*' file='Doc/ProjectFileManager.xml'/>
    public void CreateFile(string fileName, ProjectFiles projectFiles)
    {
      var builder = new StringBuilder(256);
      builder.Append("TargetCodeLine");
      builder.Append(", TargetCodeGroup");
      builder.Append(", TargetSolution");
      builder.Append(", TargetProject");
      builder.Append(", TargetPathCodeGroup");
      builder.Append(", TargetPathSolution");
      builder.Append(", TargetPathProject");
      builder.Append(", FileName");
      builder.Append(", SourceCodeLine");
      builder.Append(", SourceCodeGroup");
      builder.Append(", SourceSolution");
      builder.Append(", SourceProject");
      builder.Append(", SourceFilePath");
      builder.AppendLine(", TargetFilePath");
      var header = builder.ToString();
      File.WriteAllText(fileName, header);
      foreach (ProjectFile projectFile in projectFiles)
      {
        var text = CreateRecord(projectFile);
        File.AppendAllText(fileName, text);
      }
    }

    // <summary>Creates a ParentKey from the supplied DataObject.</summary>
    /// <include path='items/CreateParentKey/*' file='Doc/ProjectFileManager.xml'/>
    public ProjectFileParentKey CreateParentKey(ProjectFile projectFile)
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

    // Creates a record string.
    /// <include path='items/CreateRecord/*' file='Doc/ProjectFileManager.xml'/>
    public string CreateRecord(ProjectFile projectFile)
    {
      string retValue = null;

      if (projectFile != null
        && NetString.HasValue(projectFile.FileName))
      {
        var builder = new StringBuilder(256);
        if (!Reader.LJCEndsWithNewLine())
        {
          builder.AppendLine();
        }
        builder.Append($"{projectFile.TargetCodeLine}");
        builder.Append($", {projectFile.TargetCodeGroup}");
        builder.Append($", {projectFile.TargetSolution}");
        builder.Append($", {projectFile.TargetProject}");
        builder.Append($", {projectFile.TargetPathCodeGroup}");
        builder.Append($", {projectFile.TargetPathSolution}");
        builder.Append($", {projectFile.TargetPathProject}");
        builder.Append($", {projectFile.FileName}");
        builder.Append($", {projectFile.SourceCodeLine}");
        builder.Append($", {projectFile.SourceCodeGroup}");
        builder.Append($", {projectFile.SourceSolution}");
        builder.Append($", {projectFile.SourceProject}");
        builder.Append($", {projectFile.SourceFilePath}");
        builder.AppendLine($", {projectFile.TargetFilePath}");
        retValue = builder.ToString();
      }
      return retValue;
    }

    /// <summary>Creates a Data Object from the current values.</summary>
    public ProjectFile CurrentDataObject()
    {
      var retValue = new ProjectFile()
      {
        TargetCodeLine = Reader.GetTrimValue("TargetCodeLine"),
        TargetCodeGroup = Reader.GetTrimValue("TargetCodeGroup"),
        TargetSolution = Reader.GetTrimValue("TargetSolution"),
        TargetProject = Reader.GetTrimValue("TargetProject"),
        TargetPathCodeGroup = Reader.GetTrimValue("TargetPathCodeGroup"),
        TargetPathSolution = Reader.GetTrimValue("TargetPathSolution"),
        TargetPathProject = Reader.GetTrimValue("TargetPathProject"),
        FileName = Reader.GetTrimValue("FileName"),
        SourceCodeLine = Reader.GetTrimValue("SourceCodeLine"),
        SourceCodeGroup = Reader.GetTrimValue("SourceCodeGroup"),
        SourceSolution = Reader.GetTrimValue("SourceSolution"),
        SourceProject = Reader.GetTrimValue("SourceProject"),
        SourceFilePath = Reader.GetTrimValue("SourceFilePath"),
        TargetFilePath = Reader.GetTrimValue("TargetFilePath"),
      };
      return retValue;
    }

    /// <summary>Creates a ParentKey from the current values.</summary>
    public ProjectFileParentKey CurrentParentKey()
    {
      var retValue = new ProjectFileParentKey()
      {
        CodeLine = Reader.GetTrimValue("TargetCodeLine"),
        CodeGroup = Reader.GetTrimValue("TargetCodeGroup"),
        Solution = Reader.GetTrimValue("TargetSolution"),
        Project = Reader.GetTrimValue("TargetProject"),
      };
      return retValue;
    }

    /// <summary>Creates a source key from the current values.</summary>
    public ProjectFileParentKey CurrentSourceKey()
    {
      var retValue = new ProjectFileParentKey()
      {
        CodeLine = Reader.GetTrimValue("SourceCodeLine"),
        CodeGroup = Reader.GetTrimValue("SourceCodeGroup"),
        Solution = Reader.GetTrimValue("SourceSolution"),
        Project = Reader.GetTrimValue("SourceProject"),
      };
      return retValue;
    }

    // Creates a DbColumns object from propertyNames.
    /// <include path='items/GetColumns/*' file='Doc/ProjectFileManager.xml'/>
    public DbColumns GetColumns(List<string> propertyNames = null)
    {
      DbColumns retValue = BaseColumns;

      if (NetCommon.HasItems(propertyNames))
      {
        retValue = BaseColumns?.LJCGetColumns(propertyNames);
      }
      return retValue;
    }

    /// <summary>Creates a list of property names.</summary>
    public List<string> GetPropertyNames()
    {
      var retValue = BaseColumns?.LJCGetPropertyNames();
      return retValue;
    }

    // Recreates a file.
    /// <include path='items/RecreateFile/*' file='Doc/ProjectFileManager.xml'/>
    public void RecreateFile(ProjectFiles projectFiles)
    {
      Reader.Close();
      if (File.Exists(FileName))
      {
        File.Delete(FileName);
      }
      CreateFile(FileName, projectFiles);
      Reader.LJCOpen();
    }

    /// <summary>Sorts the file on unique values.</summary>
    public void SortFile()
    {
      var projectFiles = Load();
      projectFiles.LJCSortUnique();
      WriteBackup();
      RecreateFile(projectFiles);
    }

    /// <summary>Writes a backup file.</summary>
    public void WriteBackup()
    {
      var fileName = Path.GetFileNameWithoutExtension(FileName);
      var backupFile = $"{fileName}Backup.txt";
      CreateFile(backupFile, Load());
    }
    #endregion

    #region Private Methods

    // Creates the Base column definitions.
    private void CreateBaseColumns()
    {
      BaseColumns = new DbColumns()
      {
        { "TargetCodeLine" },
        { "TargetCodeGroup" },
        { "TargetSolution" },
        { "TargetProject" },
        { "TargetPathCodeGroup" },
        { "TargetPathSolution" },
        { "TargetPathProject" },
        { "FileName" },
        { "SourceCodeLine" },
        { "SourceCodeGroup" },
        { "SourceSolution" },
        { "SourceProject" },
        { "sourceFilePath" },
        { "targetFilePath" }
      };
    }

    // Creates a DataObject from the supplied values.
    private ProjectFile CreateDataObject(ProjectFileParentKey targetKey
      , ProjectFileParentKey sourceKey, string sourceFilePath, string targetFilePath)
    {
      ProjectFile retValue = null;

      // *** Next Statement *** Change - 3/11/24
      if (HasParentKey(targetKey)
        && HasParentKey(sourceKey))
      {
        retValue = new ProjectFile()
        {
          TargetCodeLine = targetKey.CodeLine,
          TargetCodeGroup = targetKey.CodeGroup,
          TargetSolution = targetKey.Solution,
          TargetProject = targetKey.Project,
          FileName = targetKey.FileName,
          SourceCodeLine = sourceKey.CodeLine,
          SourceCodeGroup = sourceKey.CodeGroup,
          SourceSolution = sourceKey.Solution,
          SourceProject = sourceKey.Project,
          SourceFilePath = sourceFilePath,
          TargetFilePath = targetFilePath,
        };
      }
      return retValue;
    }

    // Checks for the existance of the ParentKey values.
    private bool HasParentKey(ProjectFileParentKey parentKey)
    {
      var retValue = false;

      if (parentKey != null
        && NetString.HasValue(parentKey.CodeLine)
        && NetString.HasValue(parentKey.CodeGroup)
        && NetString.HasValue(parentKey.Solution)
        && NetString.HasValue(parentKey.Project))
      {
        retValue = true;
      }
      return retValue;
    }

    // Checks if the DataObject keys match the current values.
    private bool IsMatch(ProjectFile projectFile)
    {
      var retValue = false;

      var current = CurrentDataObject();
      if (current.TargetCodeLine == projectFile.TargetCodeLine
        && current.TargetCodeGroup == projectFile.TargetCodeGroup
        && current.TargetSolution == projectFile.TargetSolution
        && current.FileName == projectFile.FileName)
      {
        // *** Next Statement *** Add - 3/11/24
        if (null == projectFile.FileName
          || current.FileName == projectFile.FileName)
        {
          retValue = true;
        }
      }
      return retValue;
    }
    #endregion

    #region Public Properties

    /// <summary>Gets the base data definition columns collection.</summary>
    public DbColumns BaseColumns { get; set; }

    /// <summary>Gets or sets the TextDataReader.</summary>
    public TextDataReader Reader { get; private set; }

    /// <summary>Gets or sets the File name.</summary>
    public string FileName { get; set; }
    #endregion
  }
}
