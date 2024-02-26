// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProjectFileManager.cs
using LJCNetCommon;
using LJCTextDataReaderLib;
using System.IO;
using System.Text;

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
    public ProjectFileManager(string fileName = "ProjectFile.txt")
    {
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
    /// <param name="sourceFileSpec">The Source FileSpec.</param>
    /// <param name="targetFileSpec">The Target FileSpec.</param>
    /// <returns>The added Project data object.</returns>
    public ProjectFile Add(ProjectFileKey parentKey, ProjectFileKey sourceKey
      , string sourceFileName, string sourceFileSpec, string targetFileSpec)
    {
      ProjectFile retValue = null;

      if (HasParentKey(parentKey)
        && HasParentKey(sourceKey)
        && NetString.HasValue(sourceFileName)
        && NetString.HasValue(sourceFileSpec)
        && NetString.HasValue(targetFileSpec))
      {
        var projectFile = CreateDataObject(parentKey, sourceKey
          , sourceFileSpec, targetFileSpec);
        Reader.Close();
        File.AppendAllText(FileName, CreateRecord(projectFile));
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
    public void Delete(ProjectFileKey parentKey, string sourceFileName)
    {
      if (HasParentKey(parentKey)
        && NetString.HasValue(sourceFileName))
      {
        var current = CurrentDataObject();
        //var projectFiles = LoadAllExcept(CurrentParentKey(), sourceFileName);
        var projectFiles = LoadAllExcept(parentKey, sourceFileName);
        if (projectFiles != null)
        {
          WriteFileWithBackup(projectFiles);
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
    /// <param name="name">The Name value.</param>
    /// <returns>The Solutions collection if available; otherwise null.</returns>
    public ProjectFiles Load(ProjectFileKey parentKey = null
      , string name = null)
    {
      ProjectFiles retValue = null;

      var sourceKey = CurrentSourceKey();
      var project = CreateDataObject(parentKey, sourceKey, name, null);
      Reader.LJCOpen();
      if (Reader.Read())
      {
        retValue = new ProjectFiles();
        do
        {
          if (IsMatch(project))
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
    public ProjectFiles LoadAllExcept(ProjectFileKey parentKey
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
            && projectFile.TargetCodeGroup != parentKey.CodeGroup
            && projectFile.TargetSolution != parentKey.Solution
            && projectFile.TargetProject != parentKey.Project
            && projectFile.SourceFileName != sourceFileName)
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
    public ProjectFile Retrieve(ProjectFileKey parentKey, string sourceFileName)
    {
      ProjectFile retValue = null;

      if (HasParentKey(parentKey))
      {
        Reader.LJCOpen();
        while (Reader.Read())
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
              retValue = projectFile;
              break;
            }
          }
        }
        Reader.LJCOpen();
      }
      return retValue;
    }

    /// <summary>
    /// Updates a Project file record.
    /// </summary>
    /// <param name="projectFile">The ProjectFile Data Object.</param>
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
          WriteFileWithBackup(projectFiles);
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
      builder.Append("TargetCodeLine, TargetCodeGroup");
      builder.Append(", TargetSolution, SourceFileName");
      builder.Append(", SourceCodeLine, SourceCodeGroup");
      builder.Append(", SourceSolution, SourceFileSpec");
      builder.AppendLine(", TargetFileSpec");
      var header = builder.ToString();
      File.WriteAllText(fileName, header);
      foreach (ProjectFile projectFile in projectFiles)
      {
        var text = CreateRecord(projectFile);
        File.AppendAllText(fileName, text);
      }
    }

    public ProjectFileKey CreateParentKey(ProjectFile projectFile)
    {
      var retValue = new ProjectFileKey()
      {
        CodeLine = projectFile.TargetCodeLine,
        CodeGroup = projectFile.TargetCodeGroup,
        Solution = projectFile.TargetSolution
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
        builder.Append($"{projectFile.TargetCodeLine}");
        builder.Append($", {projectFile.TargetCodeGroup}");
        builder.Append($", {projectFile.TargetSolution}");
        builder.Append($", {projectFile.TargetProject}");
        builder.Append($", {projectFile.SourceFileName}");
        builder.Append($", {projectFile.SourceCodeLine}");
        builder.Append($", {projectFile.SourceCodeGroup}");
        builder.Append($", {projectFile.SourceSolution}");
        builder.Append($", {projectFile.SourceProject}");
        builder.Append($", {projectFile.SourceFileSpec}");
        builder.AppendLine($", {projectFile.TargetFileSpec}");
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
        SourceFileSpec = Reader.GetTrimValue("SourceFileSpec"),
        TargetFileSpec = Reader.GetTrimValue("TargetFileSpec"),
      };
      return retValue;
    }

    public ProjectFileKey CurrentParentKey()
    {
      var retValue = new ProjectFileKey()
      {
        CodeLine = Reader.GetTrimValue("TargetCodeLine"),
        CodeGroup = Reader.GetTrimValue("TargetCodeGroup"),
        Solution = Reader.GetTrimValue("TargetSolution"),
        Project = Reader.GetTrimValue("TargetProject"),
      };
      return retValue;
    }

    public ProjectFileKey CurrentSourceKey()
    {
      var retValue = new ProjectFileKey()
      {
        CodeLine = Reader.GetTrimValue("SourceCodeLine"),
        CodeGroup = Reader.GetTrimValue("SourceCodeGroup"),
        Solution = Reader.GetTrimValue("SourceSolution"),
        Project = Reader.GetTrimValue("SourceProject"),
      };
      return retValue;
    }

    /// <summary>Sorts the file on unique values.</summary>
    public void SortFile()
    {
      var projectFiles = Load();
      projectFiles.LJCSortUnique();
      WriteFileWithBackup(projectFiles);
    }

    /// <summary>
    /// Write the text file from a Solutions collection and create a backup.
    /// </summary>
    /// <param name="projectFiles">The Projects collection</param>
    public void WriteFileWithBackup(ProjectFiles projectFiles)
    {
      var fileName = Path.GetFileNameWithoutExtension(FileName);
      var backupFile = $"{fileName}Backup.txt";
      CreateFile(backupFile, Load());
      Reader.Close();
      if (File.Exists(FileName))
      {
        File.Delete(FileName);
      }
      CreateFile(FileName, projectFiles);
      Reader.LJCOpen();
    }
    #endregion

    #region Private Methods

    private ProjectFile CreateDataObject(ProjectFileKey targetKey
      , ProjectFileKey sourceKey, string sourceFileSpec, string targetFileSpec)
    {
      var retValue = new ProjectFile()
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
        SourceFileSpec = sourceFileSpec,
        TargetFileSpec = targetFileSpec,
      };
      return retValue;
    }

    private bool HasParentKey(ProjectFileKey parentKey)
    {
      var retValue = true;
      if (NetString.HasValue(parentKey.CodeLine)
        && NetString.HasValue(parentKey.CodeGroup)
        && NetString.HasValue(parentKey.Solution)
        && NetString.HasValue(parentKey.Project))
      {
        retValue = true;
      }
      return retValue;
    }

    private bool IsMatch(ProjectFile projectFile)
    {
      var retValue = false;

      var current = CurrentDataObject();
      if (current.TargetCodeLine == projectFile.TargetCodeLine
        && current.TargetCodeGroup == projectFile.TargetCodeGroup
        && current.TargetSolution == projectFile.TargetSolution
        && current.SourceFileName == projectFile.SourceFileName)
      {
        retValue = true;
      }
      return retValue;
    }
    #endregion

    #region Public Properties

    /// <summary>Gets or sets the TextDataReader.</summary>
    public TextDataReader Reader { get; private set; }

    /// <summary>Gets or sets the File name.</summary>
    public string FileName { get; set; }
    #endregion
  }
}
