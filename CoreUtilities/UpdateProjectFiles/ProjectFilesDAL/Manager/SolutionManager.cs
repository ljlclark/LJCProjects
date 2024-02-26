﻿// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// SolutionManager.cs
using LJCNetCommon;
using LJCTextDataReaderLib;
using System.IO;

namespace ProjectFilesDAL
{
  /// <summary>The Solution data manager.</summary>
  public class SolutionManager
  {
    #region Constructors

    /// <summary>
    /// Initializes an object instance.
    /// </summary>
    /// <param name="fileName">The data file name.</param>
    public SolutionManager(string fileName = "Solution.txt")
    {
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
    /// <param name="path">The Path value.</param>
    /// <returns>The added Solution data object.</returns>
    public Solution Add(SolutionParentKey parentKey, string name, string path)
    {
      Solution retValue = null;

      if (HasParentKey(parentKey)
        && NetString.HasValue(name))
      {
        var solution = CreateDataObject(parentKey, name, path);
        Reader.Close();
        File.AppendAllText(FileName, CreateRecord(solution));
        Reader.LJCOpen();
        retValue = Retrieve(parentKey, name);
      }
      return retValue;
    }

    /// <summary>
    /// Deletes a Solution record.
    /// </summary>
    /// <param name="parentKey">The ParentKey value.</param>
    /// <param name="name">The Name value.</param>
    public void Delete(SolutionParentKey parentKey, string name)
    {
      if (HasParentKey(parentKey)
        && NetString.HasValue(name))
      {
        var current = CurrentDataObject();
        var solutions = LoadAllExcept(parentKey, name);
        if (solutions != null)
        {
          WriteFileWithBackup(solutions);
        }
        if (current != null)
        {
          Retrieve(parentKey, current.Name);
        }
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

      var solution = CreateDataObject(parentKey, name, null);
      Reader.LJCOpen();
      if (Reader.Read())
      {
        retValue = new Solutions();
        do
        {
          if (IsMatch(solution))
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
            && solution.CodeGroup != parentKey.CodeGroup
            && solution.Name != name)
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

      if (HasParentKey(parentKey))
      {
        Reader.LJCOpen();
        while (Reader.Read())
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
              retValue = solution;
              break;
            }
          }
        }
        Reader.LJCOpen();
      }
      return retValue;
    }

    /// <summary>
    /// Updates a Solution file record.
    /// </summary>
    /// <param name="solution">The Solution Data Object.</param>
    public Solution Update(Solution solution)
    {
      Solution retValue = null;

      var parentKey = CreateParentKey(solution);
      if (HasParentKey(parentKey)
        && NetString.HasValue(solution.Name))
      {
        var current = CurrentDataObject();
        var Solutions = LoadAllExcept(parentKey, solution.Name);
        if (Solutions != null)
        {
          WriteFileWithBackup(Solutions);
        }
        Reader.Close();
        var text = CreateRecord(solution);
        File.AppendAllText(FileName, text);
        Reader.LJCOpen();
        Retrieve(parentKey, current.Name);
      }
      return retValue;
    }
    #endregion

    #region Public Methods

    /// <summary>
    /// Write the text file from a Solutions collection.
    /// </summary>
    /// <param name="fileName">The File name.</param>
    /// <param name="Solutions">The Solutions collection</param>
    public void CreateFile(string fileName, Solutions Solutions)
    {
      File.WriteAllText(fileName, "CodeLine, CodeGroup, Name, Path\r\n");
      foreach (Solution solution in Solutions)
      {
        var text = CreateRecord(solution);
        File.AppendAllText(fileName, text);
      }
    }

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
    /// <param name="Solution">The Solution Data Object.</param>
    /// <returns>The record string.</returns>
    public string CreateRecord(Solution Solution)
    {
      string retValue = null;

      if (Solution != null && NetString.HasValue(Solution.Name))
      {
        retValue = $"{Solution.CodeLine}, {Solution.CodeGroup}";
        retValue += $", {Solution.Name}, {Solution.Path}\r\n";
      }
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
        Path = Reader.GetTrimValue("Path")
      };
      return retValue;
    }

    /// <summary>Sorts the file on unique values.</summary>
    public void SortFile()
    {
      var solutions = Load();
      solutions.LJCSortUnique();
      WriteFileWithBackup(solutions);
    }

    /// <summary>
    /// Write the text file from a Solutions collection and create a backup.
    /// </summary>
    /// <param name="Solutions">The Solutions collection</param>
    public void WriteFileWithBackup(Solutions Solutions)
    {
      var fileName = Path.GetFileNameWithoutExtension(FileName);
      var backupFile = $"{fileName}Backup.txt";
      CreateFile(backupFile, Load());
      Reader.Close();
      if (File.Exists(FileName))
      {
        File.Delete(FileName);
      }
      CreateFile(FileName, Solutions);
      Reader.LJCOpen();
    }
    #endregion

    #region Private Methods

    private Solution CreateDataObject(SolutionParentKey parentKey, string name
      , string pathName)
    {
      var retValue = new Solution()
      {
        CodeLine = parentKey.CodeLine,
        CodeGroup = parentKey.CodeGroup,
        Name = name,
        Path = pathName
      };
      return retValue;
    }

    private bool HasParentKey(SolutionParentKey parentKey)
    {
      var retValue = true;
      if (NetString.HasValue(parentKey.CodeLine)
        && NetString.HasValue(parentKey.CodeGroup))
      {
        retValue = true;
      }
      return retValue;
    }

    private bool IsMatch(Solution solution)
    {
      var retValue = false;

      var current = CurrentDataObject();
      if (current.CodeLine == solution.CodeLine
        && current.CodeGroup == solution.CodeGroup
        && current.Name == solution.Name)
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
