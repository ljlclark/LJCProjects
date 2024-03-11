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

    /// <summary>
    /// Initializes an object instance.
    /// </summary>
    /// <param name="fileName">The data file name.</param>
    public CodeGroupManager(string fileName = @"DataFiles\CodeGroup.txt")
    {
      FileName = fileName;
      CreateBaseColumns();
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
        var codeGroup = CreateDataObject(codeLineName, name, path);
        var newRecord = CreateRecord(codeGroup);
        Reader.Close();
        File.AppendAllText(FileName, newRecord);
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
      if (NetString.HasValue(codeLineName)
        && NetString.HasValue(name))
      {
        var current = CurrentDataObject();
        var codeLines = LoadAllExcept(codeLineName, name);
        if (codeLines != null)
        {
          WriteFileWithBackup(codeLines);
        }
        if (current != null)
        {
          Retrieve(codeLineName, current.Name);
        }
      }
    }

    /// <summary>
    /// Retrieves a collection of CodeLine records.
    /// </summary>
    /// <param name="codeLineName">The CodeLine name.</param>
    /// <param name="name">The Name value.</param>
    /// <returns>The CodeGroups collection if available; otherwise null.</returns>
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

    /// <summary>
    /// Retrieves a collection of records that do NOT match the supplied Name
    /// value.
    /// </summary>
    /// <param name="codeLineName">The CodeLine name.</param>
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
      }
      return retValue;
    }

    /// <summary>
    /// Updates a record from the DataObject.
    /// </summary>
    /// <param name="codeGroup">The DataObject value.</param>
    public CodeGroup Update(CodeGroup codeGroup)
    {
      CodeGroup retValue = null;

      if (NetString.HasValue(codeGroup.CodeLine)
        && NetString.HasValue(codeGroup.Name))
      {
        var current = CurrentDataObject();
        var codeLines = LoadAllExcept(codeGroup.CodeLine, codeGroup.Name);
        if (codeLines != null)
        {
          WriteFileWithBackup(codeLines);
        }
        Reader.Close();
        var text = CreateRecord(codeGroup);
        File.AppendAllText(FileName, text);
        Reader.LJCOpen();
        Retrieve(codeGroup.CodeLine, current.Name);
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

    /// <summary>
    /// Creates a record string.
    /// </summary>
    /// <param name="codeGroup">The CodeLine Data Object.</param>
    /// <returns>The record string.</returns>
    public string CreateRecord(CodeGroup codeGroup)
    {
      string retValue = null;

      if (codeGroup != null
        && NetString.HasValue(codeGroup.Name))
      {
        var builder = new StringBuilder(128);
        if (!Reader.LJCEndsWithNewLine())
        {
          builder.AppendLine();
        }
        builder.Append($"{codeGroup.CodeLine}");
        builder.Append($", {codeGroup.Name}");
        builder.AppendLine($", {codeGroup.Path}");
        retValue = builder.ToString();
      }
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

      // *** Next Statement *** Change - 3/11/24
      if (NetString.HasValue(codeLineName))
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

      var current = CurrentDataObject();
      if (current.CodeLine == codeGroup.CodeLine)
      {
        // *** Next Statement *** Add - 3/11/24
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
    public string FileName { get; set; }
    #endregion
  }
}
