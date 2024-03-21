// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProjectFileManager.cs
using LJCNetCommon;
using LJCTextDataReaderLib;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace ProjectFilesDAL
{
  /// <summary>The ProjectFile data manager.</summary>
  public class ProjectFileManager
  {
    #region Constructors

    /// <summary>
    /// Initializes an object instance.
    /// </summary>
    /// <param name="fileName">The data file name.</param>
    public ProjectFileManager(string fileName = @"DataFiles\ProjectFile.txt")
    {
      CreateBaseColumns();
      FileName = fileName;
      Reader = new TextDataReader();
      Reader.LJCSetFile(FileName);
    }
    #endregion

    #region Data Methods

    /// <summary>
    /// Adds a ProjectFile record
    /// </summary>
    /// <param name="parentKey">The Target Key values.</param>
    /// <param name="sourceKey">The Source Key values.</param>
    /// <param name="sourceFileName">The Source File name.</param>
    /// <param name="sourceFilePath">The Source FileSpec.</param>
    /// <param name="targetFilePath">The Target FileSpec.</param>
    /// <returns>The added Project data object.</returns>
    public ProjectFile Add(ProjectFileParentKey parentKey, ProjectFileParentKey sourceKey
      , string sourceFileName, string sourceFilePath, string targetFilePath)
    {
      ProjectFile retValue = null;

      if (HasParentKey(parentKey)
        && HasParentKey(sourceKey)
        && NetString.HasValue(sourceFileName)
        && NetString.HasValue(sourceFilePath)
        && NetString.HasValue(targetFilePath))
      {
        var projectFile = CreateDataObject(parentKey, sourceKey
          , sourceFilePath, targetFilePath);
        var newRecord = CreateRecord(projectFile);
        Reader.Close();
        File.AppendAllText(FileName, newRecord);
        Reader.LJCOpen();
        retValue = Retrieve(parentKey, sourceFileName);
      }
      return retValue;
    }

    /// <summary>
    /// Deletes a ProjectFile record.
    /// </summary>
    /// <param name="parentKey">The Target Key values.</param>
    /// <param name="sourceFileName">The Name value.</param>
    public void Delete(ProjectFileParentKey parentKey, string sourceFileName)
    {
      if (HasParentKey(parentKey)
        && NetString.HasValue(sourceFileName))
      {
        var current = CurrentDataObject();
        //var projectFiles = LoadAllExcept(CurrentParentKey(), sourceFileName);
        var projectFiles = LoadAllExcept(parentKey, sourceFileName);
        if (projectFiles != null)
        {
          WriteBackup();
          RecreateFile(projectFiles);
        }
        if (current != null)
        {
          //Retrieve(CurrentParentKey(), sourceFileName);
          Retrieve(parentKey, current.SourceFileName);
        }
      }
    }

    /// <summary>
    /// Retrieves a collection of ProjectFile records.
    /// </summary>
    /// <param name="parentKey">The ParentKey value.</param>
    /// <param name="sourceFileName">The Name value.</param>
    /// <returns>The Solutions collection if available; otherwise null.</returns>
    public ProjectFiles Load(ProjectFileParentKey parentKey = null
      , string sourceFileName = null)
    {
      ProjectFiles retValue = null;

      var sourceKey = CurrentSourceKey();
      var project = CreateDataObject(parentKey, sourceKey, sourceFileName, null);
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

    /// <summary>
    /// Retrieves a collection of records that do NOT match the supplied Name
    /// value.
    /// </summary>
    /// <param name="parentKey">The ParentKey value.</param>
    /// <param name="sourceFileName">The Name value.</param>
    /// <returns>The ProjectFiles collection if available; otherwise null.</returns>
    public ProjectFiles LoadAllExcept(ProjectFileParentKey parentKey
      , string sourceFileName)
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
            || projectFile.SourceFileName != sourceFileName)
          {
            retValue.Add(projectFile);
          }
        } while (Reader.Read());
        Reader.LJCOpen();
      }
      return retValue;
    }

    /// <summary>
    /// Retrieves a ProjectFile record.
    /// </summary>
    /// <param name="parentKey">The ParentKey value.</param>
    /// <param name="sourceFileName">The SourceFileName value.</param>
    /// <returns>The Solution Data Object if found; otherwise null.</returns>
    public ProjectFile Retrieve(ProjectFileParentKey parentKey, string sourceFileName)
    {
      ProjectFile retValue = null;

      if (HasParentKey(parentKey))
      {
        if (NetString.HasValue(sourceFileName))
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
            if (NetString.HasValue(sourceFileName))
            {
              if (projectFile.SourceFileName == sourceFileName)
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

    /// <summary>
    /// Updates a record from the DataObject.
    /// </summary>
    /// <param name="projectFile">The DataObject value.</param>
    public Project Update(ProjectFile projectFile)
    {
      Project retValue = null;

      var parentKey = CreateParentKey(projectFile);
      if (HasParentKey(parentKey)
        && NetString.HasValue(projectFile.SourceFileName))
      {
        var current = CurrentDataObject();
        var projectFiles = LoadAllExcept(parentKey, projectFile.SourceFileName);
        if (projectFiles != null)
        {
          WriteBackup();
          RecreateFile(projectFiles);
        }
        Reader.Close();
        var text = CreateRecord(projectFile);
        File.AppendAllText(FileName, text);
        Reader.LJCOpen();
        Retrieve(parentKey, current.SourceFileName);
      }
      return retValue;
    }
    #endregion

    #region Public Methods

    /// <summary>
    /// Write the text file from a Solutions collection.
    /// </summary>
    /// <param name="fileName">The File name.</param>
    /// <param name="projectFiles">The ProjectFiles collection</param>
    public void CreateFile(string fileName, ProjectFiles projectFiles)
    {
      var builder = new StringBuilder(256);
      builder.Append("TargetCodeLine");
      builder.Append(", TargetCodeGroup");
      builder.Append(", TargetSolution");
      builder.Append(", TargetProject");
      builder.Append(", SourceFileName");
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

    /// <summary>
    /// Creates a record string.
    /// </summary>
    /// <param name="projectFile">The ProjectFile Data Object.</param>
    /// <returns>The record string.</returns>
    public string CreateRecord(ProjectFile projectFile)
    {
      string retValue = null;

      if (projectFile != null
        && NetString.HasValue(projectFile.SourceFileName))
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
        builder.Append($", {projectFile.SourceFileName}");
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
        SourceFileName = Reader.GetTrimValue("SourceFileName"),
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

    /// <summary>Creates a DbColumns object from propertyNames.</summary>
    /// <param name="propertyNames">The list of property names.</param>
    /// <returns>A DbColumns collection.</returns>
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
    /// <summary>
    /// Recreates a file.
    /// </summary>
    /// <param name="projectFiles">The ProjectFiles collection</param>
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

    /// <summary>
    /// Write a backup file.
    /// </summary>
    /// <param name="projectFiles">The Projects collection</param>
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
        { "SourceFileName" },
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
          SourceFileName = targetKey.SourceFileName,
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
        && current.SourceFileName == projectFile.SourceFileName)
      {
        // *** Next Statement *** Add - 3/11/24
        if (null == projectFile.SourceFileName
          || current.SourceFileName == projectFile.SourceFileName)
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
