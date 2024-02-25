// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CodeGroupManager.cs
using LJCNetCommon;
using LJCTextDataReaderLib;
using System.IO;

namespace ProjectFilesDAL
{
  /// <summary>The CodeGroup data manager.</summary>
  public class CodeGroupManager
  {
    #region Constructors

    /// <summary>
    /// Initializes an object instance.
    /// </summary>
    /// <param name="fileName">The data file name.</param>
    public CodeGroupManager(string fileName = "CodeGroup.txt")
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
    /// <param name="codeLineName">The CodeLine name.</param>
    /// <param name="name">The Name value.</param>
    /// <param name="path">The Path value.</param>
    /// <returns>The added CodeLine data object.</returns>
    public CodeGroup Add(string codeLineName, string name, string path)
    {
      CodeGroup retValue = null;

      if (NetString.HasValue(codeLineName)
        && NetString.HasValue(name))
      {
        var codeGroup = new CodeGroup()
        {
          CodeLine = codeLineName,
          Name = name,
          Path = path
        };
        Reader.Close();
        File.AppendAllText(FileName, CreateRecord(codeGroup));
        Reader.LJCOpen();
        retValue = Retrieve(codeLineName, name);
      }
      return retValue;
    }

    /// <summary>
    /// Deletes a CodeLine record.
    /// </summary>
    /// <param name="name">The Name value.</param>
    public void Delete(string codeLineName, string name)
    {
      if (NetString.HasValue(name))
      {
        var current = DataObject();
        var codeLines = LoadAllExcept(codeLineName, name);
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
    /// <returns>The CodeGroups collection if available; otherwise null.</returns>
    public CodeGroups Load(string name = null)
    {
      CodeGroups retValue = null;

      Reader.LJCOpen();
      if (Reader.Read())
      {
        retValue = new CodeGroups();
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
    public CodeGroups LoadAllExcept(string codeLineName, string name)
    {
      CodeGroups retValue = null;

      Reader.LJCOpen();
      if (Reader.Read())
      {
        retValue = new CodeGroups();
        do
        {
          var codeLineValue = Reader.GetString("CodeLine");
          codeLineValue = codeLineValue?.Trim();
          var nameValue = Reader.GetString("Name");
          nameValue = nameValue.Trim();
          if (codeLineValue != codeLineName
            && nameValue != name)
          {
            var codeGroup = DataObject();
            retValue.Add(codeGroup);
          }
        } while (Reader.Read());
        Reader.LJCOpen();
      }
      return retValue;
    }

    /// <summary>
    /// Retrieves a CodeLine record.
    /// </summary>
    /// <param name="codeLineName">The CodeLine name.</param>
    /// <param name="name">The Name value.</param>
    /// <returns>The CodeLine Data Object if found; otherwise null.</returns>
    public CodeGroup Retrieve(string codeLineName, string name = null)
    {
      CodeGroup retValue = null;

      if (NetString.HasValue(codeLineName))
      {
        Reader.LJCOpen();
        while (Reader.Read())
        {
          var codeLineValue = Reader.GetString("CodeLine");
          codeLineValue = codeLineValue?.Trim();
          var nameValue = Reader.GetString("Name");
          nameValue = nameValue?.Trim();
          if (codeLineValue == codeLineName)
          {
            if (NetString.HasValue(name))
            {
              if (nameValue == name)
              {
                retValue = DataObject();
                break;
              }
            }
            else
            {
              retValue = DataObject();
              break;
            }
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
    public CodeGroup Update(CodeGroup codeGroup)
    {
      CodeGroup retValue = null;

      if (NetString.HasValue(codeGroup.Name))
      {
        var current = DataObject();
        var codeLines = LoadAllExcept(codeGroup.CodeLine, codeGroup.Name);
        if (codeLines != null)
        {
          WriteFileWithBackup(codeLines);
        }
        Reader.Close();
        var text = CreateRecord(codeGroup);
        File.AppendAllText(FileName, text);
        Reader.LJCOpen();
        Retrieve(current.Name);
      }
      return retValue;
    }
    #endregion

    #region Public Methods

    /// <summary>
    /// Write the text file from a CodeGroups collection.
    /// </summary>
    /// <param name="fileName">The File name.</param>
    /// <param name="codeGroups">The CodeGroups collection</param>
    public void CreateFile(string fileName, CodeGroups codeGroups)
    {
      File.WriteAllText(fileName, "CodeLine, Name, Path\r\n");
      foreach (CodeGroup codeGroup in codeGroups)
      {
        var text = CreateRecord(codeGroup);
        File.AppendAllText(fileName, text);
      }
    }

    /// <summary>
    /// Creates a record string.
    /// </summary>
    /// <param name="codeGroup">The CodeLine Data Object.</param>
    /// <returns>The record string.</returns>
    public string CreateRecord(CodeGroup codeGroup)
    {
      string retValue = null;

      if (codeGroup != null && NetString.HasValue(codeGroup.Name))
      {
        retValue = $"{codeGroup.CodeLine}, {codeGroup.Name}"
          + $", {codeGroup.Path}\r\n";
      }
      return retValue;
    }

    /// <summary>Creates a Data Object from the current values.</summary>
    public CodeGroup DataObject()
    {
      var retValue = new CodeGroup()
      {
        CodeLine = Reader.GetString("CodeLine"),
        Name = Reader.GetString("Name"),
        Path = Reader.GetString("Path")
      };
      return retValue;
    }

    /// <summary>Sorts the file on unique values.</summary>
    public void SortFile()
    {
      var codeGroups = Load();
      codeGroups.LJCSortUnique();
      WriteFileWithBackup(codeGroups);
    }

    /// <summary>
    /// Write the text file from a CodeLines collection and create a backup.
    /// </summary>
    /// <param name="codeGroups">The CodeGroups collection</param>
    public void WriteFileWithBackup(CodeGroups codeGroups)
    {
      var fileName = Path.GetFileNameWithoutExtension(FileName);
      var backupFile = $"{fileName}Backup.txt";
      CreateFile(backupFile, Load());
      Reader.Close();
      if (File.Exists(FileName))
      {
        File.Delete(FileName);
      }
      CreateFile(FileName, codeGroups);
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
