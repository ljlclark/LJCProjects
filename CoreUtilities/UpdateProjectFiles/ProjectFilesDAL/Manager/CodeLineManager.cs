// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CodeLineManager.cs
using LJCNetCommon;
using LJCTextDataReaderLib;
using System.Collections.Generic;
using System.IO;

namespace ProjectFilesDAL
{
  /// <summary>The CodeLine data manager.</summary>
  public class CodeLineManager
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/CodeLineManagerC/*' file='Doc/CodeLineManager.xml'/>
    public CodeLineManager(string fileSpec = "CodeLine.txt")
    {
      string message = "";
      string context = ClassContext + "CodeLineManager()";
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
    /// <include path='items/Add/*' file='Doc/CodeLineManager.xml'/>
    public CodeLine Add(string name, string path)
    {
      string message = "";
      string context = ClassContext + "Add()";
      NetString.ArgError(ref message, name, "name", context);
      NetString.ThrowArgError(message);

      var codeLine = CreateDataObject(name, path);
      var newRecord = CreateRecord(codeLine);
      Reader.Close();
      File.AppendAllText(FileSpec, newRecord);
      Reader.LJCOpen();
      var retValue = Retrieve(name);
      return retValue;
    }

    // Deletes a CodeLine record.
    /// <include path='items/Delete/*' file='Doc/CodeLineManager.xml'/>
    public void Delete(string name)
    {
      string message = "";
      string context = ClassContext + "Delete()";
      NetString.ArgError(ref message, name, "name", context);
      NetString.ThrowArgError(message);

      var current = CurrentDataObject();
      var codeLines = LoadAllExcept(name);
      if (codeLines != null)
      {
        WriteBackup();
        RecreateFile(codeLines);
      }
      if (current != null)
      {
        Retrieve(current.Name);
      }
    }

    // Loads a collection of CodeLine records.
    /// <include path='items/Load/*' file='Doc/CodeLineManager.xml'/>
    public CodeLines Load(string name = null)
    {
      CodeLines retValue = null;

      var codeLine = CreateDataObject(name, null);
      Reader.LJCOpen();
      if (Reader.Read())
      {
        retValue = new CodeLines();
        do
        {
          if (null == codeLine
            || IsMatch(codeLine))
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
    /// <include path='items/LoadAllExcept/*' file='Doc/CodeLineManager.xml'/>
    public CodeLines LoadAllExcept(string name)
    {
      CodeLines retValue = null;

      string message = "";
      string context = ClassContext + "LoadAllExcept()";
      NetString.ArgError(ref message, name, "name", context);
      NetString.ThrowArgError(message);

      Reader.LJCOpen();
      if (Reader.Read())
      {
        retValue = new CodeLines();
        do
        {
          var codeLine = CurrentDataObject();
          if (codeLine.Name != name)
          {
            retValue.Add(codeLine);
          }
        } while (Reader.Read());
        Reader.LJCOpen();
      }
      return retValue;
    }

    // Retrieves a CodeLine record.
    /// <include path='items/Retrieve/*' file='Doc/CodeLineManager.xml'/>
    public CodeLine Retrieve(string name = null)
    {
      CodeLine retValue = null;

      if (NetString.HasValue(name))
      {
        Reader.LJCOpen();
      }
      bool success;
      while (success = Reader.Read())
      {
        var codeLine = CurrentDataObject();
        if (NetString.HasValue(name))
        {
          if (codeLine.Name == name)
          {
            retValue = codeLine;
            break;
          }
        }
        else
        {
          // Get next item.
          retValue = codeLine;
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
    /// <include path='items/Update/*' file='Doc/CodeLineManager.xml'/>
    public CodeLine Update(CodeLine codeLine)
    {
      string message = "";
      string context = ClassContext + "Update()";
      NetString.ArgError(ref message, codeLine, "codeLine", context);
      CodeLine.ItemValues(ref message, codeLine);
      NetString.ThrowArgError(message);

      var current = CurrentDataObject();
      var codeLines = LoadAllExcept(codeLine.Name);
      if (codeLines != null)
      {
        WriteBackup();
        RecreateFile(codeLines);
      }
      Reader.Close();
      var text = CreateRecord(codeLine);
      File.AppendAllText(FileSpec, text);
      Reader.LJCOpen();
      var retValue = Retrieve(current.Name);
      return retValue;
    }
    #endregion

    #region Public Methods

    // Write the text file from a CodeLines collection.
    /// <include path='items/CreateFile/*' file='Doc/CodeLineManager.xml'/>
    public void CreateFile(string fileName, CodeLines codeLines)
    {
      string message = "";
      string context = ClassContext + "CreateFile()";
      NetString.ArgError(ref message, fileName, "fileName", context);
      NetString.ThrowArgError(message);

      File.WriteAllText(fileName, "Name, Path\r\n");
      foreach (CodeLine codeLine in codeLines)
      {
        var text = CreateRecord(codeLine);
        File.AppendAllText(fileName, text);
      }
    }

    // Creates a record string.
    /// <include path='items/CreateRecord/*' file='Doc/CodeLineManager.xml'/>
    public string CreateRecord(CodeLine codeLine)
    {
      string message = "";
      string context = ClassContext + "CreateRecord()";
      NetString.ArgError(ref message, codeLine, "codeLine", context);
      CodeLine.ItemValues(ref message, codeLine);
      NetString.ThrowArgError(message);

      var retValue = "";
      if (!Reader.LJCEndsWithNewLine())
      {
        retValue = "\r\n";
      }
      retValue += $"{codeLine.Name}, {codeLine.Path}\r\n";
      return retValue;
    }

    /// <summary>Creates a Data Object from the current values.</summary>
    public CodeLine CurrentDataObject()
    {
      var retValue = new CodeLine()
      {
        Name = Reader.GetTrimValue("Name"),
        Path = Reader.GetTrimValue("Path")
      };
      return retValue;
    }

    // Creates a DbColumns object 
    /// <include path='items/GetColumns/*' file='Doc/CodeLineManager.xml'/>
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
    /// <include path='items/RecreateFile/*' file='Doc/CodeLineManager.xml'/>
    public void RecreateFile(CodeLines codeLines)
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
      CreateFile(FileSpec, codeLines);
      Reader.LJCOpen();
    }

    /// <summary>Sorts the file on unique values.</summary>
    public void SortFile()
    {
      var codeLines = Load();
      codeLines.LJCSortUnique();
      WriteBackup();
      RecreateFile(codeLines);
    }

    /// <summary>Writes a backup file.</summary>
    public void WriteBackup()
    {
      string message = "";
      string context = ClassContext + "WriteBackup()";
      NetString.ArgError(ref message, FileSpec, "FileName", context);
      NetString.ThrowArgError(message);

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
        { "Name" },
        { "Path" }
      };
    }

    // Creates a DataObject from the supplied values.
    private CodeLine CreateDataObject(string name, string pathName = null)
    {
      var retValue = new CodeLine()
      {
        Name = name,
        Path = pathName
      };
      return retValue;
    }

    // Checks if the DataObject keys match the current values.
    private bool IsMatch(CodeLine codeLine)
    {
      var retValue = false;

      string message = "";
      string context = ClassContext + "IsMatch()";
      NetString.ArgError(ref message, codeLine, "codeLine", context);
      NetString.ThrowArgError(message);

      var current = CurrentDataObject();
      if (null == codeLine.Name
        || current.Name == codeLine.Name)
      {
        retValue = true;
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

    private const string ClassContext = "DataProjectFilesDAL.CodeLineManager.";
    #endregion
  }
}
