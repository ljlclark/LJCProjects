// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CodeGroupManager.cs
using LJCNetCommon;
using LJCTextDataReaderLib;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProjectFilesDAL
{
  /// <summary>The CodeGroup data manager.</summary>
  public class CodeGroupManager
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/CodeGroupManagerC/*' file='Doc/CodeGroupManager.xml'/>
    public CodeGroupManager(string fileSpec = "CodeGroup.txt")
    {
      string message = "";
      string context = ClassContext + "CodeGroupManager()";
      NetString.ArgError(ref message, fileSpec, "fileSpec", context);
      NetString.ThrowArgError(message);

      FileSpec = fileSpec;
      CreateBaseColumns();
      Reader = new TextDataReader();
      Reader.LJCSetFile(FileSpec);
    }
    #endregion

    #region Data Methods

    // Adds a CodeLine file record
    /// <include path='items/Add/*' file='Doc/CodeGroupManager.xml'/>
    public CodeGroup Add(string codeLineName, string name, string path)
    {
      string message = "";
      string context = ClassContext + "Add()";
      NetString.ArgError(ref message, codeLineName, "codeLineName", context);
      NetString.ArgError(ref message, name, "name");
      NetString.ThrowArgError(message);

      var codeGroup = CreateDataObject(codeLineName, name, path);
      var newRecord = CreateRecord(codeGroup);
      Reader.Close();
      File.AppendAllText(FileSpec, newRecord);
      Reader.LJCOpen();
      var retValue = Retrieve(codeLineName, name);
      return retValue;
    }

    // Deletes a CodeLine record.
    /// <include path='items/Delete/*' file='Doc/CodeGroupManager.xml'/>
    public void Delete(string codeLineName, string name)
    {
      string message = "";
      string context = ClassContext + "Delete()";
      NetString.ArgError(ref message, codeLineName, "codeLineName", context);
      NetString.ArgError(ref message, name, "name");
      NetString.ThrowArgError(message);

      var current = CurrentDataObject();
      var codeLines = LoadAllExcept(codeLineName, name);
      if (codeLines != null)
      {
        WriteBackup();
        RecreateFile(codeLines);
      }
      if (current != null)
      {
        Retrieve(codeLineName, current.Name);
      }
    }

    // Loads a collection of CodeLine records.
    /// <include path='items/Load/*' file='Doc/CodeGroupManager.xml'/>
    public CodeGroups Load(string codeLineName = null, string name = null)
    {
      CodeGroups retValue = null;

      var codeGroup = CreateDataObject(codeLineName, name, null);
      Reader.LJCOpen();
      if (Reader.Read())
      {
        retValue = new CodeGroups();
        do
        {
          if (null == codeGroup
            || IsMatch(codeGroup))
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
    /// <include path='items/LoadAllExcept/*' file='Doc/CodeGroupManager.xml'/>
    public CodeGroups LoadAllExcept(string codeLineName, string name)
    {
      CodeGroups retValue = null;

      string message = "";
      string context = ClassContext + "LoadAllExcept()";
      NetString.ArgError(ref message, name, "name", context);
      NetString.ThrowArgError(message);

      Reader.LJCOpen();
      if (Reader.Read())
      {
        retValue = new CodeGroups();
        do
        {
          var codeLine = CurrentDataObject();
          if (codeLine.CodeLine != codeLineName
            || codeLine.Name != name)
          {
            retValue.Add(codeLine);
          }
        } while (Reader.Read());
        Reader.LJCOpen();
      }
      return retValue;
    }

    // Retrieves a CodeLine record.
    /// <include path='items/LoadAllExcept/*' file='Doc/CodeGroupManager.xml'/>
    public CodeGroup Retrieve(string codeLineName, string name = null)
    {
      CodeGroup retValue = null;

      string message = "";
      string context = ClassContext + "Retrieve()";
      NetString.ArgError(ref message, codeLineName, "codeLineName", context);
      NetString.ThrowArgError(message);

      if (NetString.HasValue(name))
      {
        Reader.LJCOpen();
      }
      bool success;
      while (success = Reader.Read())
      {
        var codeGroup = CurrentDataObject();
        if (codeGroup.CodeLine == codeLineName)
        {
          if (NetString.HasValue(name))
          {
            if (codeGroup.Name == name)
            {
              retValue = codeGroup;
              break;
            }
          }
          else
          {
            // Get next item in Parent key.
            retValue = codeGroup;
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
      return retValue;
    }

    // Updates a record from the DataObject.
    /// <include path='items/Update/*' file='Doc/CodeGroupManager.xml'/>
    public CodeGroup Update(CodeGroup codeGroup)
    {
      string message = "";
      string context = ClassContext + "Update()";
      NetString.ArgError(ref message, codeGroup, "codeGroup", context);
      CodeGroup.ItemValues(ref message, codeGroup);
      NetString.ThrowArgError(message);

      var current = CurrentDataObject();
      var codeLines = LoadAllExcept(codeGroup.CodeLine, codeGroup.Name);
      if (codeLines != null)
      {
        WriteBackup();
        RecreateFile(codeLines);
      }
      Reader.Close();
      var text = CreateRecord(codeGroup);
      File.AppendAllText(FileSpec, text);
      Reader.LJCOpen();
      var retValue = Retrieve(codeGroup.CodeLine, current.Name);
      return retValue;
    }
    #endregion

    #region Public Methods

    // Write the text file from a CodeGroups collection.
    /// <include path='items/CreateFile/*' file='Doc/CodeGroupManager.xml'/>
    public void CreateFile(string fileName, CodeGroups codeGroups)
    {
      string message = "";
      string context = ClassContext + "CreateFile()";
      NetString.ArgError(ref message, fileName, "fileName", context);
      NetString.ThrowArgError(message);

      var builder = new StringBuilder(128);
      builder.Append("CodeLine, Name");
      builder.AppendLine(", Path");
      var header = builder.ToString();
      File.WriteAllText(fileName, header);
      foreach (CodeGroup codeGroup in codeGroups)
      {
        var text = CreateRecord(codeGroup);
        File.AppendAllText(fileName, text);
      }
    }

    // Creates a record string.
    /// <include path='items/CreateRecord/*' file='Doc/CodeGroupManager.xml'/>
    public string CreateRecord(CodeGroup codeGroup)
    {
      string message = "";
      string context = ClassContext + "CreateRecord()";
      NetString.ArgError(ref message, codeGroup, "codeGroup", context);
      CodeGroup.ItemParentValues(ref message, codeGroup);
      NetString.ThrowArgError(message);

      var builder = new StringBuilder(128);
      if (!Reader.LJCEndsWithNewLine())
      {
        builder.AppendLine();
      }
      builder.Append($"{codeGroup.CodeLine}");
      builder.Append($", {codeGroup.Name}");
      builder.AppendLine($", {codeGroup.Path}");
      var retValue = builder.ToString();
      return retValue;
    }

    /// <summary>Creates a Data Object from the current values.</summary>
    public CodeGroup CurrentDataObject()
    {
      var retValue = new CodeGroup()
      {
        CodeLine = Reader.GetTrimValue("CodeLine"),
        Name = Reader.GetTrimValue("Name"),
        Path = Reader.GetTrimValue("Path")
      };
      return retValue;
    }

    // Creates a DbColumns object from propertyNames.
    /// <include path='items/GetColumns/*' file='Doc/CodeGroupManager.xml'/>
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
    /// <include path='items/RecreateFile/*' file='Doc/CodeGroupManager.xml'/>
    public void RecreateFile(CodeGroups codeGroups)
    {
      string message = "";
      string context = ClassContext + "RecreateFile()";
      NetString.ArgError(ref message, FileSpec, "FileName", context);
      NetString.ThrowArgError(message);

      Reader.Close();
      if (File.Exists(FileSpec))
      {
        File.Delete(FileSpec);
      }
      CreateFile(FileSpec, codeGroups);
      Reader.LJCOpen();
    }

    /// <summary>Sorts the file on unique values.</summary>
    public void SortFile()
    {
      var codeGroups = Load();
      codeGroups.LJCSortUnique();
      WriteBackup();
      RecreateFile(codeGroups);
    }

    /// <summary>Write a backup file.</summary>
    public void WriteBackup()
    {
      string message = "";
      string context = ClassContext + "Add()";
      NetString.ArgError(ref message, FileSpec, "FileName", context);
      NetString.ThrowArgError(message);

      var fileName = Path.GetFileNameWithoutExtension(FileSpec);
      var backupFile = $"{fileName}Backup.txt";
      CreateFile(backupFile, Load());
    }
    #endregion

    #region Private Methods

    // Creates the  Base column definitions.
    private void CreateBaseColumns()
    {
      BaseColumns = new DbColumns()
      {
        { "CodeLine" },
        { "Name" },
        { "Path" }
      };
    }

    // Creates a DataObject from the supplied values.
    private CodeGroup CreateDataObject(string codeLineName, string name
      , string pathName = null)
    {
      CodeGroup retValue = null;

      // A null return value is allowed.
      if (codeLineName != null)
      {
        retValue = new CodeGroup()
        {
          CodeLine = codeLineName,
          Name = name,
          Path = pathName
        };
      }
      return retValue;
    }

    // Checks if the DataObject keys match the current values.
    private bool IsMatch(CodeGroup codeGroup)
    {
      var retValue = false;

      string message = "";
      string context = ClassContext + "Add()";
      NetString.ArgError(ref message, codeGroup, "codeGroup", context);
      CodeGroup.ItemParentValues(ref message, codeGroup);
      NetString.ThrowArgError(message);

      var current = CurrentDataObject();
      if (current.CodeLine == codeGroup.CodeLine)
      {
        if (null == codeGroup.Name
          || current.Name == codeGroup.Name)
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
    public string FileSpec { get; set; }
    #endregion

    #region Class Data

    private const string ClassContext = "DataProjectFilesDAL.CodeGroupManager.";
    #endregion
  }
}
