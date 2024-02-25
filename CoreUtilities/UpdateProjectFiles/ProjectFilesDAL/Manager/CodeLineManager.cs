using LJCNetCommon;
using LJCTextDataReaderLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
    public Solution Add(string name, string path)
    {
      Solution retValue = null;

      if (NetString.HasValue(name))
      {
        var codeLine = new Solution()
        {
          Name = name,
          Path = path
        };
        //var current = DataObject();
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
        var current = DataObject();
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

      Reader.LJCOpen();
      if (Reader.Read())
      {
        retValue = new CodeLines();
        do
        {
          if (!NetString.HasValue(name))
          {
            retValue.Add(DataObject());
          }
          else
          {
            var value = Reader.GetString("Name");
            if (value == name)
            {
              retValue.Add(DataObject());
              break;
            }
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
          var value = Reader.GetString("Name");
          if (value != name)
          {
            var codeLine = DataObject();
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
    public Solution Retrieve(string name = null)
    {
      Solution retValue = null;

      if (!NetString.HasValue(name))
      {
        if (Reader.Read())
        {
          retValue = DataObject();
        }
      }
      else
      {
        Reader.LJCOpen();
        while (Reader.Read())
        {
          var value = Reader.GetString("Name");
          if (value == name)
          {
            retValue = DataObject();
            break;
          }
        }
        Reader.LJCOpen();
      }
      return retValue;
    }

    /// <summary>
    /// Updates a CodeLine file record.
    /// </summary>
    /// <param name="codeLine">The CodeLine Data Object.</param>
    public Solution Update(Solution codeLine)
    {
      Solution retValue = null;

      if (NetString.HasValue(codeLine.Name))
      {
        var current = DataObject();
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
    /// <param name="codeLines">The CodeLines collection</param>
    public void CreateFile(string fileName, CodeLines codeLines)
    {
      File.WriteAllText(fileName, "Name, Path\r\n");
      foreach (Solution codeLine in codeLines)
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
    public string CreateRecord(Solution codeLine)
    {
      string retValue = null;

      if (codeLine != null && NetString.HasValue(codeLine.Name))
      {
        retValue = $"{codeLine.Name}, {codeLine.Path}\r\n";
      }
      return retValue;
    }

    /// <summary>Creates a Data Object from the current values.</summary>
    public Solution DataObject()
    {
      var retValue = new Solution()
      {
        Name = Reader.GetString("Name"),
        Path = Reader.GetString("Path")
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

    #region Public Properties

    /// <summary>Gets or sets the TextDataReader.</summary>
    public TextDataReader Reader { get; private set; }

    /// <summary>Gets or sets the File name.</summary>
    public string FileName { get; set; }
    #endregion
  }
}
