// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProjectManager.cs
using LJCNetCommon;
using LJCTextDataReaderLib;
using System.IO;
using System.Text;

namespace ProjectFilesDAL
{
  /// <summary>The Project data manager.</summary>
  public class ProjectManager
  {
    #region Constructors

    /// <summary>
    /// Initializes an object instance.
    /// </summary>
    /// <param name="fileName">The data file name.</param>
    public ProjectManager(string fileName = "Project.txt")
    {
      FileName = fileName;
      Reader = new TextDataReader();
      Reader.LJCSetFile(FileName);
    }
    #endregion

    #region Data Methods

    /// <summary>
    /// Adds a Project file record
    /// </summary>
    /// <param name="codeLineName">The CodeLine name.</param>
    /// <param name="codeGroupName">The CodeGroup name.</param>
    /// <param name="solutionName">The Solution name.</param>
    /// <param name="name">The Name value.</param>
    /// <param name="path">The Path value.</param>
    /// <returns>The added Project data object.</returns>
    public Project Add(string codeLineName, string codeGroupName
      , string solutionName, string name, string path)
    {
      Project retValue = null;

      if (NetString.HasValue(codeLineName)
        && NetString.HasValue(codeGroupName)
        && NetString.HasValue(solutionName)
        && NetString.HasValue(name))
      {
        var project = CreateDataObject(codeLineName, codeGroupName, solutionName
          , name, path);
        Reader.Close();
        File.AppendAllText(FileName, CreateRecord(project));
        Reader.LJCOpen();
        retValue = Retrieve(codeLineName, codeGroupName, solutionName, name);
      }
      return retValue;
    }

    /// <summary>
    /// Deletes a Project record.
    /// </summary>
    /// <param name="codeLineName">The CodeLine name.</param>
    /// <param name="codeGroupName">The CodeGroup name.</param>
    /// <param name="solutionName">The Solution name.</param>
    /// <param name="name">The Name value.</param>
    public void Delete(string codeLineName, string codeGroupName
      , string solutionName, string name)
    {
      if (NetString.HasValue(name))
      {
        var current = CurrentDataObject();
        var projects = LoadAllExcept(codeLineName, codeGroupName, solutionName
          , name);
        if (projects != null)
        {
          WriteFileWithBackup(projects);
        }
        if (current != null)
        {
          Retrieve(current.CodeLine, current.CodeGroup, current.Solution
            , current.Name);
        }
      }
    }

    /// <summary>
    /// Retrieves a collection of Project records.
    /// </summary>
    /// <param name="codeLineName">The CodeLine name.</param>
    /// <param name="codeGroupName">The CodeGroup name.</param>
    /// <param name="solutionName">The Solution name.</param>
    /// <param name="name">The Name value.</param>
    /// <returns>The Solutions collection if available; otherwise null.</returns>
    public Projects Load(string codeLineName = null
      , string codeGroupName = null, string solutionName = null
      , string name = null)
    {
      Projects retValue = null;

      var project = CreateDataObject(codeLineName, codeGroupName,solutionName
        , name, null);
      Reader.LJCOpen();
      if (Reader.Read())
      {
        retValue = new Projects();
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
    /// <param name="codeLineName">The CodeLine name.</param>
    /// <param name="codeGroupName">The CodeGroup name.</param>
    /// <param name="solutionName">The Soluton name.</param>
    /// <param name="name">The Name value.</param>
    /// <returns>The Solutions collection if available; otherwise null.</returns>
    public Projects LoadAllExcept(string codeLineName, string codeGroupName
      , string solutionName, string name)
    {
      Projects retValue = null;

      Reader.LJCOpen();
      if (Reader.Read())
      {
        retValue = new Projects();
        do
        {
          var project = CurrentDataObject();
          if (project.CodeLine != codeLineName
            && project.CodeGroup != codeGroupName
            && project.Solution != solutionName
            && project.Name != name)
          {
            retValue.Add(project);
          }
        } while (Reader.Read());
        Reader.LJCOpen();
      }
      return retValue;
    }

    /// <summary>
    /// Retrieves a Project record.
    /// </summary>
    /// <param name="codeLineName">The CodeLine name.</param>
    /// <param name="codeGroupName">The Solution name.</param>
    /// <param name="solutionName">The Solution name.</param>
    /// <param name="name">The Name value.</param>
    /// <returns>The Solution Data Object if found; otherwise null.</returns>
    public Project Retrieve(string codeLineName, string codeGroupName
      , string solutionName = null, string name = null)
    {
      Project retValue = null;

      if (NetString.HasValue(codeLineName)
        && NetString.HasValue(codeGroupName)
        && NetString.HasValue(solutionName))
      {
        Reader.LJCOpen();
        while (Reader.Read())
        {
          var project = CurrentDataObject();
          if (project.CodeLine == codeLineName
            && project.CodeGroup == codeGroupName
            && project.Solution == solutionName)
          {
            if (NetString.HasValue(name))
            {
              if (project.Name == name)
              {
                retValue = project;
                break;
              }
            }
            else
            {
              retValue = project;
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
    /// <param name="project">The Project Data Object.</param>
    public Project Update(Project project)
    {
      Project retValue = null;

      if (NetString.HasValue(project.Name))
      {
        var current = CurrentDataObject();
        var projects = LoadAllExcept(project.CodeLine, project.CodeGroup
          , project.Solution, project.Name);
        if (projects != null)
        {
          WriteFileWithBackup(projects);
        }
        Reader.Close();
        var text = CreateRecord(project);
        File.AppendAllText(FileName, text);
        Reader.LJCOpen();
        Retrieve(current.CodeLine, current.CodeGroup, current.Solution
          , current.Name);
      }
      return retValue;
    }
    #endregion

    #region Public Methods

    /// <summary>
    /// Write the text file from a Solutions collection.
    /// </summary>
    /// <param name="fileName">The File name.</param>
    /// <param name="projects">The Projects collection</param>
    public void CreateFile(string fileName, Projects projects)
    {
      var header = "CodeLine, CodeGroup, Solution, Name";
      header += ", Path\r\n";
      File.WriteAllText(fileName, header);
      foreach (Project project in projects)
      {
        var text = CreateRecord(project);
        File.AppendAllText(fileName, text);
      }
    }

    /// <summary>
    /// Creates a record string.
    /// </summary>
    /// <param name="project">The Project Data Object.</param>
    /// <returns>The record string.</returns>
    public string CreateRecord(Project project)
    {
      string retValue = null;

      if (project != null && NetString.HasValue(project.Name))
      {
        var builder = new StringBuilder(1024);
        builder.Append($"{project.CodeLine}, {project.CodeGroup}");
        builder.Append($"{project.Solution}, {project.Name}");
        builder.AppendLine($", {project.Path}");
        retValue = builder.ToString();
      }
      return retValue;
    }

    /// <summary>Creates a Data Object from the current values.</summary>
    public Project CurrentDataObject()
    {
      var retValue = new Project()
      {
        CodeLine = Reader.GetTrimValue("CodeLine"),
        CodeGroup = Reader.GetTrimValue("CodeGroup"),
        Solution = Reader.GetTrimValue("Solution"),
        Name = Reader.GetTrimValue("Name"),
        Path = Reader.GetTrimValue("Path")
      };
      return retValue;
    }

    /// <summary>Sorts the file on unique values.</summary>
    public void SortFile()
    {
      var projects = Load();
      projects.LJCSortUnique();
      WriteFileWithBackup(projects);
    }

    /// <summary>
    /// Write the text file from a Solutions collection and create a backup.
    /// </summary>
    /// <param name="projects">The Projects collection</param>
    public void WriteFileWithBackup(Projects projects)
    {
      var fileName = Path.GetFileNameWithoutExtension(FileName);
      var backupFile = $"{fileName}Backup.txt";
      CreateFile(backupFile, Load());
      Reader.Close();
      if (File.Exists(FileName))
      {
        File.Delete(FileName);
      }
      CreateFile(FileName, projects);
      Reader.LJCOpen();
    }
    #endregion

    #region Private Methods

    private Project CreateDataObject(string codeLineName, string codeGroupName
      , string solutionName, string name, string pathName)
    {
      var retValue = new Project()
      {
        CodeLine = codeLineName,
        CodeGroup = codeGroupName,
        Solution = solutionName,
        Name = name,
        Path = pathName
      };
      return retValue;
    }

    private bool IsMatch(Project project)
    {
      var retValue = false;

      var current = CurrentDataObject();
      if (current.CodeLine == project.CodeLine
        && current.CodeGroup == project.CodeGroup
        && current.Solution == project.Solution
        && current.Name == project.Name)
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
