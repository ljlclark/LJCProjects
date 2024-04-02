// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// SolutionManager.cs
using LJCNetCommon;
using LJCTextDataReaderLib;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProjectFilesDAL
{
  /// <summary>The Solution data manager.</summary>
  public class SolutionManager
  {
    #region Constructors

    /// <summary>Initializes an object instance.</summary>
    /// <param name="fileName">The data file name.</param>
    public SolutionManager(string fileName = @"DataFiles\Solution.txt")
    {
      CreateBaseColumns();
      FileName = fileName;
      Reader = new TextDataReader();
      Reader.LJCSetFile(FileName);
    }
    #endregion

    #region Data Methods

    /// <summary>
    /// Adds a Solution file record
    /// </summary>
    /// <param name="parentKey">The ParentKey value.</param>
    /// <param name="name">The Name value.</param>
    /// <param name="sequence">The Sequence value.</param>
    /// <param name="path">The Path value.</param>
    /// <returns>The added Solution data object.</returns>
    public Solution Add(SolutionParentKey parentKey, string name, int sequence
      , string path)
    {
      var message = NetString.ArgError(null, parentKey, name);
      Solution.ParentKeyValues(ref message, parentKey);
      NetString.ThrowArgError(message);

      var solution = CreateDataObject(parentKey, name, sequence, path);
      var newRecord = CreateRecord(solution);
      Reader.Close();
      File.AppendAllText(FileName, newRecord);
      Reader.LJCOpen();
      var retValue = Retrieve(parentKey, name);
      return retValue;
    }

    /// <summary>
    /// Deletes a Solution record.
    /// </summary>
    /// <param name="parentKey">The ParentKey value.</param>
    /// <param name="name">The Name value.</param>
    public void Delete(SolutionParentKey parentKey, string name)
    {
      var message = NetString.ArgError(null, parentKey, name);
      Solution.ParentKeyValues(ref message, parentKey);
      NetString.ThrowArgError(message);

      var current = CurrentDataObject();
      var solutions = LoadAllExcept(parentKey, name);
      if (solutions != null)
      {
        WriteBackup();
        RecreateFile(solutions);
      }
      if (current != null)
      {
        Retrieve(parentKey, current.Name);
      }
    }

    /// <summary>
    /// Retrieves a collection of Solution records.
    /// </summary>
    /// <param name="parentKey">The ParentKey value.</param>
    /// <param name="name">The Name value.</param>
    /// <returns>The Solutions collection if available; otherwise null.</returns>
    public Solutions Load(SolutionParentKey parentKey = null, string name = null)
    {
      Solutions retValue = null;

      var solution = CreateDataObject(parentKey, name, 0, null);
      Reader.LJCOpen();
      if (Reader.Read())
      {
        retValue = new Solutions();
        do
        {
          if (null == solution
            || IsMatch(solution))
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
    /// <returns>The Solutions collection if available; otherwise null.</returns>
    public Solutions LoadAllExcept(SolutionParentKey parentKey, string name)
    {
      Solutions retValue = null;

      Reader.LJCOpen();
      if (Reader.Read())
      {
        retValue = new Solutions();
        do
        {
          var solution = CurrentDataObject();
          if (solution.CodeLine != parentKey.CodeLine
            || solution.CodeGroup != parentKey.CodeGroup
            || solution.Name != name)
          {
            retValue.Add(solution);
          }
        } while (Reader.Read());
        Reader.LJCOpen();
      }
      return retValue;
    }

    /// <summary>
    /// Retrieves a Solution record.
    /// </summary>
    /// <param name="parentKey">The ParentKey value.</param>
    /// <param name="name">The Name value.</param>
    /// <returns>The Solution Data Object if found; otherwise null.</returns>
    public Solution Retrieve(SolutionParentKey parentKey, string name = null)
    {
      Solution retValue = null;

      var message = NetString.ArgError(null, parentKey);
      Solution.ParentKeyValues(ref message, parentKey);
      NetString.ThrowArgError(message);

      if (NetString.HasValue(name))
      {
        Reader.LJCOpen();
      }
      bool success;
      while (success = Reader.Read())
      {
        var solution = CurrentDataObject();
        if (solution.CodeLine == parentKey.CodeLine
          && solution.CodeGroup == parentKey.CodeGroup)
        {
          if (NetString.HasValue(name))
          {
            if (solution.Name == name)
            {
              retValue = solution;
              break;
            }
          }
          else
          {
            // Get next item in Parent key.
            retValue = solution;
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

    /// <summary>
    /// Updates a record from the DataObject.
    /// </summary>
    /// <param name="solution">The DataObject value.</param>
    public Solution Update(Solution solution)
    {
      var message = NetString.ArgError(null, solution);
      Solution.ItemValues(ref message, solution);
      NetString.ThrowArgError(message);

      var parentKey = CreateParentKey(solution);
      var current = CurrentDataObject();
      var solutions = LoadAllExcept(parentKey, solution.Name);
      if (solutions != null)
      {
        WriteBackup();
        RecreateFile(solutions);
      }
      Reader.Close();
      var text = CreateRecord(solution);
      File.AppendAllText(FileName, text);
      Reader.LJCOpen();
      var retValue = Retrieve(parentKey, current.Name);
      return retValue;
    }
    #endregion

    #region Public Methods

    /// <summary>
    /// Write the text file from a Solutions collection.
    /// </summary>
    /// <param name="fileName">The File name.</param>
    /// <param name="solutions">The Solutions collection</param>
    public void CreateFile(string fileName, Solutions solutions)
    {
      var builder = new StringBuilder(128);
      builder.Append("CodeLine, CodeGroup");
      builder.Append(", Name, Sequence");
      builder.AppendLine(", Path");
      var header = builder.ToString();
      File.WriteAllText(fileName, header);
      foreach (Solution solution in solutions)
      {
        var text = CreateRecord(solution);
        File.AppendAllText(fileName, text);
      }
    }

    /// <summary>Creates a ParentKey from the supplied DataObject.</summary>
    /// <param name="solution"></param>
    /// <returns></returns>
    public SolutionParentKey CreateParentKey(Solution solution)
    {
      var retValue = new SolutionParentKey()
      {
        CodeLine = solution.CodeLine,
        CodeGroup = solution.CodeGroup
      };
      return retValue;
    }

    /// <summary>
    /// Creates a record string.
    /// </summary>
    /// <param name="solution">The Solution Data Object.</param>
    /// <returns>The record string.</returns>
    public string CreateRecord(Solution solution)
    {
      var message = NetString.ArgError(null, solution);
      Solution.ItemValues(ref message, solution);
      NetString.ThrowArgError(message);

      var builder = new StringBuilder(128);
      if (!Reader.LJCEndsWithNewLine())
      {
        builder.AppendLine();
      }
      builder.Append($"{solution.CodeLine}");
      builder.Append($", {solution.CodeGroup}");
      builder.Append($", {solution.Name}");
      builder.Append($", {solution.Sequence}");
      builder.AppendLine($", {solution.Path}");
      var retValue = builder.ToString();
      return retValue;
    }

    /// <summary>Creates a Data Object from the current values.</summary>
    public Solution CurrentDataObject()
    {
      var retValue = new Solution()
      {
        CodeLine = Reader.GetTrimValue("CodeLine"),
        CodeGroup = Reader.GetTrimValue("CodeGroup"),
        Name = Reader.GetTrimValue("Name"),
        Sequence = Reader.GetInt32("Sequence"),
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
    /// <param name="solutions">The Solutions collection</param>
    public void RecreateFile(Solutions solutions)
    {
      Reader.Close();
      if (File.Exists(FileName))
      {
        File.Delete(FileName);
      }
      CreateFile(FileName, solutions);
      Reader.LJCOpen();
    }

    /// <summary>Sorts the file on unique values.</summary>
    public void SortFile()
    {
      var solutions = Load();
      solutions.LJCSortUnique();
      WriteBackup();
      RecreateFile(solutions);
    }

    /// <summary>Write the text file from a Solutions collection and create a backup.</summary>
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
        { "Name" },
        { "Path" }
      };
    }

    // Creates a DataObject from the supplied values.
    private Solution CreateDataObject(SolutionParentKey parentKey, string name
      , int sequence = 0, string pathName = null)
    {
      Solution retValue = null;

      if (parentKey != null)
      {
        retValue = new Solution()
        {
          CodeLine = parentKey.CodeLine,
          CodeGroup = parentKey.CodeGroup,
          Name = name,
          Sequence = sequence,
          Path = pathName
        };
      }
      return retValue;
    }

    // Checks if the DataObject keys match the current values.
    private bool IsMatch(Solution solution)
    {
      var retValue = false;

      var message = NetString.ArgError(null, solution);
      Solution.ItemParentValues(ref message, solution);
      NetString.ThrowArgError(message);

      var current = CurrentDataObject();
      if (current.CodeLine == solution.CodeLine
        && current.CodeGroup == solution.CodeGroup)
      {
        if (null == solution.Name
          || current.Name == solution.Name)
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
