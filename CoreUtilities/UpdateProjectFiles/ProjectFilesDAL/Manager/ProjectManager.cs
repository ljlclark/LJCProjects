// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProjectManager.cs
using LJCNetCommon;
using LJCTextDataReaderLib;
using System.Collections.Generic;
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
    public ProjectManager(string fileName = @"DataFiles\Project.txt")
    {
      CreateBaseColumns();
      FileName = fileName;
      Reader = new TextDataReader();
      Reader.LJCSetFile(FileName);
    }
    #endregion

    #region Data Methods

    /// <summary>
    /// Adds a Project file record
    /// </summary>
    /// <param name="parentKey">The ParentKey value.</param>
    /// <param name="name">The Name value.</param>
    /// <param name="path">The Path value.</param>
    /// <returns>The added Project data object.</returns>
    public Project Add(ProjectParentKey parentKey, string name, string path)
    {
      Project retValue = null;

      if (HasParentKey(parentKey)
        && NetString.HasValue(name))
      {
        var project = CreateDataObject(parentKey, name, path);
        var newRecord = CreateRecord(project);
        Reader.Close();
        File.AppendAllText(FileName, newRecord);
        Reader.LJCOpen();
        retValue = Retrieve(parentKey, name);
      }
      return retValue;
    }

    /// <summary>
    /// Deletes a Project record.
    /// </summary>
    /// <param name="parentKey">The ParentKey value.</param>
    /// <param name="name">The Name value.</param>
    public void Delete(ProjectParentKey parentKey, string name)
    {
      if (HasParentKey(parentKey)
        && NetString.HasValue(name))
      {
        var current = CurrentDataObject();
        var projects = LoadAllExcept(parentKey, name);
        if (projects != null)
        {
          WriteBackup();
          RecreateFile(projects);
        }
        if (current != null)
        {
          Retrieve(parentKey, current.Name);
        }
      }
    }

    /// <summary>
    /// Retrieves a collection of Project records.
    /// </summary>
    /// <param name="parentKey">The ParentKey value.</param>
    /// <param name="name">The Name value.</param>
    /// <returns>The Solutions collection if available; otherwise null.</returns>
    public Projects Load(ProjectParentKey parentKey = null, string name = null)
    {
      Projects retValue = null;

      var project = CreateDataObject(parentKey, name, null);
      Reader.LJCOpen();
      if (Reader.Read())
      {
        retValue = new Projects();
        do
        {
          if (null == project
            || IsMatch(project))
          {
            var currentObject = CurrentDataObject();
            retValue.Add(currentObject);
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
    /// <param name="name">The Name value.</param>
    /// <returns>The Projects collection if available; otherwise null.</returns>
    public Projects LoadAllExcept(ProjectParentKey parentKey, string name)
    {
      Projects retValue = null;

      Reader.LJCOpen();
      if (Reader.Read())
      {
        retValue = new Projects();
        do
        {
          var project = CurrentDataObject();
          if (project.CodeLine != parentKey.CodeLine
            || project.CodeGroup != parentKey.CodeGroup
            || project.Solution != parentKey.Solution
            || project.Name != name)
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
    /// <param name="parentKey">The ParentKey value.</param>
    /// <param name="name">The Name value.</param>
    /// <returns>The Project Data Object if found; otherwise null.</returns>
    public Project Retrieve(ProjectParentKey parentKey, string name = null)
    {
      Project retValue = null;

      if (HasParentKey(parentKey))
      {
        if (NetString.HasValue(name))
        {
          Reader.LJCOpen();
        }
        bool success;
        while (success = Reader.Read())
        {
          var project = CurrentDataObject();
          if (project.CodeLine == parentKey.CodeLine
            && project.CodeGroup == parentKey.CodeGroup
            && project.Solution == parentKey.Solution)
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
              // Get next item in Parent key.
              retValue = project;
              break;
            }
          }
          //else
          //{
          //  success = false;
          //  break;
          //}
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
    /// <param name="project">The DataObject value.</param>
    public Project Update(Project project)
    {
      Project retValue = null;

      var parentKey = CreateParentKey(project);
      if (HasParentKey(parentKey)
        && NetString.HasValue(project.Name))
      {
        var current = CurrentDataObject();
        var projects = LoadAllExcept(parentKey, project.Name);
        if (projects != null)
        {
          WriteBackup();
          RecreateFile(projects);
        }
        Reader.Close();
        var text = CreateRecord(project);
        File.AppendAllText(FileName, text);
        Reader.LJCOpen();
        Retrieve(parentKey, current.Name);
      }
      return retValue;
    }
    #endregion

    #region Public Methods

    /// <summary>
    /// Write the text file from a Projects collection.
    /// </summary>
    /// <param name="fileName">The File name.</param>
    /// <param name="projects">The Projects collection</param>
    public void CreateFile(string fileName, Projects projects)
    {
      var builder = new StringBuilder(128);
      builder.Append("CodeLine, CodeGroup");
      builder.Append(", Solution, Name");
      builder.AppendLine(", Path");
      var header = builder.ToString();
      File.WriteAllText(fileName, header);
      foreach (Project project in projects)
      {
        var text = CreateRecord(project);
        File.AppendAllText(fileName, text);
      }
    }

    // <summary>Creates a ParentKey from the supplied DataObject.</summary>
    public ProjectParentKey CreateParentKey(Project project)
    {
      var retValue = new ProjectParentKey()
      {
        CodeLine = project.CodeLine,
        CodeGroup = project.CodeGroup,
        Solution = project.Solution
      };
      return retValue;
    }

    /// <summary>
    /// Creates a record string.
    /// </summary>
    /// <param name="project">The Project Data Object.</param>
    /// <returns>The record string.</returns>
    public string CreateRecord(Project project)
    {
      string retValue = null;

      if (project != null
        && NetString.HasValue(project.Name))
      {
        var builder = new StringBuilder(1024);
        if (!Reader.LJCEndsWithNewLine())
        {
          builder.AppendLine();
        }
        builder.Append($"{project.CodeLine}");
        builder.Append($", {project.CodeGroup}");
        builder.Append($", {project.Solution}");
        builder.Append($", {project.Name}");
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
    /// <param name="projects">The Projects collection</param>
    public void RecreateFile(Projects projects)
    {
      Reader.Close();
      if (File.Exists(FileName))
      {
        File.Delete(FileName);
      }
      CreateFile(FileName, projects);
      Reader.LJCOpen();
    }

    /// <summary>Sorts the file on unique values.</summary>
    public void SortFile()
    {
      var projects = Load();
      projects.LJCSortUnique();
      WriteBackup();
      RecreateFile(projects);
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
        { "CodeLine" },
        { "CodeGroup" },
        { "Solution" },
        { "Name" },
        { "Path" }
      };
    }

    // Creates a DataObject from the supplied values.
    private Project CreateDataObject(ProjectParentKey parentKey, string name
      , string pathName = null)
    {
      Project retValue = null;

      // *** Next Statement *** Change - 3/11/24
      if (HasParentKey(parentKey))
      {
        retValue = new Project()
        {
          CodeLine = parentKey.CodeLine,
          CodeGroup = parentKey.CodeGroup,
          Solution = parentKey.Solution,
          Name = name,
          Path = pathName
        };
      }
      return retValue;
    }

    // Checks for the existance of the ParentKey values.
    private bool HasParentKey(ProjectParentKey parentKey)
    {
      var retValue = false;

      if (parentKey != null
        && NetString.HasValue(parentKey.CodeLine)
        && NetString.HasValue(parentKey.CodeGroup)
        && NetString.HasValue(parentKey.Solution))
      {
        retValue = true;
      }
      return retValue;
    }

    // Checks if the DataObject keys match the current values.
    private bool IsMatch(Project project)
    {
      var retValue = false;

      var current = CurrentDataObject();
      if (current.CodeLine == project.CodeLine
        && current.CodeGroup == project.CodeGroup
        && current.Solution == project.Solution)
      {
        // *** Next Statement *** Add - 3/11/24
        if (null == project.Name
          || current.Name == project.Name)
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
