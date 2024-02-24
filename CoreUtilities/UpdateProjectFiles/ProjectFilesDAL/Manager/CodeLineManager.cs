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
    public CodeLine Add(string name, string path)
    {
      CodeLine retValue = null;

      if (NetString.HasValue(name))
      {
        var codeLine = new CodeLine()
        {
          Name = name,
          Path = path
        };
        File.AppendAllText(FileName, CreateRecord(codeLine));
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
        var codeLines = LoadAllExcept(name);
        if (codeLines != null)
        {
          WriteFileWithBackup(codeLines);
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

      Reader.LJCSetFile(FileName);
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
        Reader.LJCSetFile(FileName);
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

      Reader.LJCSetFile(FileName);
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
        Reader.LJCSetFile(FileName);
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

      if (!NetString.HasValue(name))
      {
        if (Reader.Read())
        {
          retValue = DataObject();
        }
      }
      else
      {
        Reader.LJCSetFile(FileName);
        while (Reader.Read())
        {
          var value = Reader.GetString("Name");
          if (value == name)
          {
            retValue = DataObject();
            break;
          }
        }
        Reader.LJCSetFile(FileName);
      }
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
        var codeLines = LoadAllExcept(codeLine.Name);
        if (codeLines != null)
        {
          WriteFileWithBackup(codeLines);
        }
        var text = CreateRecord(codeLine);
        File.AppendAllText(FileName, text);
      }
      return retValue;
    }
    #endregion

    #region Public Methods

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
        retValue = $"{codeLine.Name}, {codeLine.Path}";
      }
      return retValue;
    }

    /// <summary>Creates a Data Object from the current values.</summary>
    public CodeLine DataObject()
    {
      var retValue = new CodeLine()
      {
        Name = Reader.GetString("Name"),
        Path = Reader.GetString("Path")
      };
      return retValue;
    }

    /// <summary>
    /// Write the text file from a CodeLines collection.
    /// </summary>
    /// <param name="codeLines">The CodeLines collection</param>
    public void WriteFile(string fileName, CodeLines codeLines)
    {
      File.WriteAllText(fileName, "Name, Path");
      foreach (CodeLine codeLine in codeLines)
      {
        var text = CreateRecord(codeLine);
        File.AppendAllText(fileName, text);
      }
    }

    /// <summary>
    /// Write the text file from a CodeLines collection and create a backup.
    /// </summary>
    /// <param name="codeLines">The CodeLines collection</param>
    public void WriteFileWithBackup(CodeLines codeLines)
    {
      var backupFile = $"{FileName}Backup.txt";
      WriteFile(backupFile, codeLines);
      if (File.Exists(FileName))
      {
        File.Delete(FileName);
      }
      File.Copy(backupFile, FileName);
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
