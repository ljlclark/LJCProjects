// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CodeLineManager.cs
using LJCNetCommon;
using LJCTextDataReaderLib;
using System.IO;

namespace ProjectFilesDAL
{
  /// <summary>The CodeLine data manager.</summary>
  public class CodeLineManager
  {
    #region Constructors

    /// <summary>
    /// Initializes an object instance.
    /// </summary>
    /// <param name="fileName">The data file name.</param>
    public CodeLineManager(string fileName = "CodeLine.txt")
    {
      FileName = fileName;
      Reader = new TextDataReader();
      Reader.LJCSetFile(FileName);
    }
    #endregion

    #region Data Methods

    /// <summary>
    /// Adds a CodeLine file record
    /// </summary>
    /// <param name="name">The Name value.</param>
    /// <param name="path">The Path value.</param>
    /// <returns>The added CodeLine data object.</returns>
    public CodeLine Add(string name, string path)
    {
      CodeLine retValue = null;

      if (NetString.HasValue(name))
      {
        var codeLine = CreateDataObject(name, path);
        Reader.Close();
        File.AppendAllText(FileName, CreateRecord(codeLine));
        Reader.LJCOpen();
        retValue = Retrieve(name);
      }
      return retValue;
    }

    /// <summary>
    /// Deletes a CodeLine record.
    /// </summary>
    /// <param name="name">The Name value.</param>
    public void Delete(string name)
    {
      if (NetString.HasValue(name))
      {
        var current = CurrentDataObject();
        var codeLines = LoadAllExcept(name);
        if (codeLines != null)
        {
          WriteFileWithBackup(codeLines);
        }
        if (current != null)
        {
          Retrieve(current.Name);
        }
      }
    }

    /// <summary>
    /// Retrieves a collection of CodeLine records.
    /// </summary>
    /// <param name="name">The Name value.</param>
    /// <returns>The CodeLines collection if available; otherwise null.</returns>
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
          if (IsMatch(codeLine))
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
    /// <param name="name">The Name value.</param>
    /// <returns>The CodeLines collection if available; otherwise null.</returns>
    public CodeLines LoadAllExcept(string name)
    {
      CodeLines retValue = null;

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

    /// <summary>
    /// Retrieves a CodeLine record.
    /// </summary>
    /// <param name="name">The Name value.</param>
    /// <returns>The CodeLine Data Object if found; otherwise null.</returns>
    public CodeLine Retrieve(string name = null)
    {
      CodeLine retValue = null;

      Reader.LJCOpen();
      while (Reader.Read())
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
          retValue = codeLine;
          break;
        }
      }
      Reader.LJCOpen();
      return retValue;
    }

    /// <summary>
    /// Updates a CodeLine file record.
    /// </summary>
    /// <param name="codeLine">The CodeLine Data Object.</param>
    public CodeLine Update(CodeLine codeLine)
    {
      CodeLine retValue = null;

      if (NetString.HasValue(codeLine.Name))
      {
        var current = CurrentDataObject();
        var codeLines = LoadAllExcept(codeLine.Name);
        if (codeLines != null)
        {
          WriteFileWithBackup(codeLines);
        }
        Reader.Close();
        var text = CreateRecord(codeLine);
        File.AppendAllText(FileName, text);
        Reader.LJCOpen();
        Retrieve(current.Name);
      }
      return retValue;
    }
    #endregion

    #region Public Methods

    /// <summary>
    /// Write the text file from a CodeLines collection.
    /// </summary>
    /// <param name="fileName">The File name.</param>
    /// <param name="codeLines">The CodeLines collection</param>
    public void CreateFile(string fileName, CodeLines codeLines)
    {
      File.WriteAllText(fileName, "Name, Path\r\n");
      foreach (CodeLine codeLine in codeLines)
      {
        var text = CreateRecord(codeLine);
        File.AppendAllText(fileName, text);
      }
    }

    /// <summary>
    /// Creates a record string.
    /// </summary>
    /// <param name="codeLine">The CodeLine Data Object.</param>
    /// <returns>The record string.</returns>
    public string CreateRecord(CodeLine codeLine)
    {
      string retValue = null;

      if (codeLine != null && NetString.HasValue(codeLine.Name))
      {
        retValue = $"{codeLine.Name}, {codeLine.Path}\r\n";
      }
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

    /// <summary>Sorts the file on unique values.</summary>
    public void SortFile()
    {
      var codeLines = Load();
      codeLines.LJCSortUnique();
      WriteFileWithBackup(codeLines);
    }

    /// <summary>
    /// Write the text file from a CodeLines collection and create a backup.
    /// </summary>
    /// <param name="codeLines">The CodeLines collection</param>
    public void WriteFileWithBackup(CodeLines codeLines)
    {
      var fileName = Path.GetFileNameWithoutExtension(FileName);
      var backupFile = $"{fileName}Backup.txt";
      CreateFile(backupFile, Load());
      Reader.Close();
      if (File.Exists(FileName))
      {
        File.Delete(FileName);
      }
      CreateFile(FileName, codeLines);
      Reader.LJCOpen();
    }
    #endregion

    #region Private Methods

    private CodeLine CreateDataObject(string name, string pathName)
    {
      var retValue = new CodeLine()
      {
        Name = name,
        Path = pathName
      };
      return retValue;
    }

    private bool IsMatch(CodeLine codeLine)
    {
      var retValue = false;

      var current = CurrentDataObject();
      if (current.Name == codeLine.Name)
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
