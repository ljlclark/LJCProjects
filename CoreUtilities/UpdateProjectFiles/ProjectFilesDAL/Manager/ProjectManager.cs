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

    // Initializes an object instance.
    /// <include path='items/ProjectManagerC/*' file='Doc/ProjectManager.xml'/>
    public ProjectManager(string fileSpec = "Project.txt")
    {
      ArgError = new ArgError("ProjectFilesDAL.ProjectManager")
      {
        MethodName = "ProjectManager(fileSpec)"
      };
      ArgError.Add(fileSpec, "fileSpec");
      NetString.ThrowArgError(ArgError.ToString());

      FileSpec = fileSpec;
      CreateBaseColumns();
      Reader = new TextDataReader();
      Reader.LJCSetFile(FileSpec);
    }
    #endregion

    #region Data Methods

    // Adds a Project file record
    /// <include path='items/Add/*' file='Doc/ProjectManager.xml'/>
    public Project Add(ProjectParentKey parentKey, string name, string path)
    {
      ArgError.MethodName = "Add(parentKey, name, path)";
      ArgError.Add(Project.ParentKeyValues(parentKey));
      ArgError.Add(name, "name");
      NetString.ThrowArgError(ArgError.ToString());

      var project = CreateDataObject(parentKey, name, path);
      var newRecord = CreateRecord(project);
      Reader.Close();
      File.AppendAllText(FileSpec, newRecord);
      Reader.LJCOpen();
      var retValue = Retrieve(parentKey, name);
      return retValue;
    }

    // Deletes a Project record.
    /// <include path='items/Delete/*' file='Doc/ProjectManager.xml'/>
    public void Delete(ProjectParentKey parentKey, string name)
    {
      ArgError.MethodName = "Delete(parentKey, name)";
      ArgError.Add(Project.ParentKeyValues(parentKey));
      ArgError.Add(name, "name");
      NetString.ThrowArgError(ArgError.ToString());

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

    // Loads a collection of Project records.
    /// <include path='items/Load/*' file='Doc/ProjectManager.xml'/>
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

    // Loads a collection of records that do NOT match the supplied Name
    /// <include path='items/LoadAllExcept/*' file='Doc/ProjectManager.xml'/>
    public Projects LoadAllExcept(ProjectParentKey parentKey, string name)
    {
      Projects retValue = null;

      ArgError.MethodName = "LoadAllExcept(parentKey, name)";
      ArgError.Add(name, "name");
      NetString.ThrowArgError(ArgError.ToString());

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

    // Retrieves a Project record.
    /// <include path='items/Retrieve/*' file='Doc/ProjectManager.xml'/>
    public Project Retrieve(ProjectParentKey parentKey, string name = null)
    {
      Project retValue = null;

      ArgError.MethodName = "Retrieve(parentKey, name)";
      ArgError.Add(Project.ParentKeyValues(parentKey));
      NetString.ThrowArgError(ArgError.ToString());

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
      }
      if (!success)
      {
        Reader.LJCOpen();
      }
      return retValue;
    }

    // Updates a record from the DataObject.
    /// <include path='items/Update/*' file='Doc/ProjectManager.xml'/>
    public Project Update(Project project)
    {
      ArgError.MethodName = "Update(project)";
      ArgError.Add(Project.ItemValues(project));
      NetString.ThrowArgError(ArgError.ToString());

      var parentKey = CreateParentKey(project);
      var current = CurrentDataObject();
      var projects = LoadAllExcept(parentKey, project.Name);
      if (projects != null)
      {
        WriteBackup();
        RecreateFile(projects);
      }
      Reader.Close();
      var text = CreateRecord(project);
      File.AppendAllText(FileSpec, text);
      Reader.LJCOpen();
      var retValue = Retrieve(parentKey, current.Name);
      return retValue;
    }
    #endregion

    #region Public Methods

    // Write the text file from a Projects collection.
    /// <include path='items/CreateFile/*' file='Doc/ProjectManager.xml'/>
    public void CreateFile(string fileName, Projects projects)
    {
      ArgError.MethodName = "CreateFile(fileName, projects)";
      ArgError.Add(fileName, "fileName");
      NetString.ThrowArgError(ArgError.ToString());

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

    // Creates a ParentKey from the supplied DataObject.
    /// <include path='items/CreateParentKey/*' file='Doc/ProjectManager.xml'/>
    public ProjectParentKey CreateParentKey(Project project)
    {
      ArgError.MethodName = "CreateParentKey(project)";
      ArgError.Add(Project.ItemParentValues(project));
      NetString.ThrowArgError(ArgError.ToString());

      var retValue = new ProjectParentKey()
      {
        CodeLine = project.CodeLine,
        CodeGroup = project.CodeGroup,
        Solution = project.Solution
      };
      return retValue;
    }

    // Creates a record string.
    /// <include path='items/CreateRecord/*' file='Doc/ProjectManager.xml'/>
    public string CreateRecord(Project project)
    {
      ArgError.MethodName = "CreateRecord(project)";
      ArgError.Add(Project.ItemValues(project));
      NetString.ThrowArgError(ArgError.ToString());

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
      var retValue = builder.ToString();
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

    // Creates a DbColumns object from propertyNames.
    /// <include path='items/GetColumns/*' file='Doc/ProjectManager.xml'/>
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
    /// <include path='items/RecreateFile/*' file='Doc/ProjectManager.xml'/>
    public void RecreateFile(Projects projects)
    {
      ArgError.MethodName = "RecreateFile(projects)";
      ArgError.Add(FileSpec, "FileName");
      NetString.ThrowArgError(ArgError.ToString());

      Reader.Close();
      if (File.Exists(FileSpec))
      {
        File.Delete(FileSpec);
      }
      CreateFile(FileSpec, projects);
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
      ArgError.MethodName = "WriteBackup()";
      ArgError.Add(FileSpec, "FileName");
      NetString.ThrowArgError(ArgError.ToString());

      var fileName = Path.GetFileNameWithoutExtension(FileSpec);
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

      // A null return value is allowed.
      if (parentKey != null)
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

    // Checks if the DataObject keys match the current values.
    private bool IsMatch(Project project)
    {
      var retValue = false;

      ArgError.MethodName = "IsMatch(project)";
      ArgError.Add(Project.ItemParentValues(project));
      NetString.ThrowArgError(ArgError.ToString());

      var current = CurrentDataObject();
      if (current.CodeLine == project.CodeLine
        && current.CodeGroup == project.CodeGroup
        && current.Solution == project.Solution)
      {
        if (null == project.Name
          || current.Name == project.Name)
        {
          retValue = true;
        }
      }
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets the base data definition columns collection.</summary>
    public DbColumns BaseColumns { get; set; }

    /// <summary>Gets or sets the TextDataReader.</summary>
    public TextDataReader Reader { get; private set; }

    /// <summary>Gets or sets the File name.</summary>
    public string FileSpec { get; set; }

    // Represents Argument errors.
    private ArgError ArgError { get; set; }
    #endregion
  }
}
