// Copyright(c) Lester J. Clark and Contributors.
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
    /// <param name="codeLineName">The CodeLine name.</param>
    /// <param name="codeGroupName">The CodeGroup name.</param>
    /// <param name="name">The Name value.</param>
    /// <param name="path">The Path value.</param>
    /// <returns>The added Solution data object.</returns>
    public Solution Add(string codeLineName, string codeGroupName, string name
      , string path)
    {
      Solution retValue = null;

      if ( NetString.HasValue(codeLineName)
        && NetString.HasValue(codeGroupName)
        && NetString.HasValue(name))
      {
        var Solution = new Solution()
        {
          CodeLine = codeLineName.Trim(),
          CodeGroup = codeGroupName.Trim(),
          Name = name.Trim(),
          Path = path
        };
        Reader.Close();
        File.AppendAllText(FileName, CreateRecord(Solution));
        Reader.LJCOpen();
        retValue = Retrieve(codeGroupName, name);
      }
      return retValue;
    }

    /// <summary>
    /// Deletes a Solution record.
    /// </summary>
    /// <param name="codeLineName">The CodeLine name.</param>
    /// <param name="codeGroupName">The CodeGroup name.</param>
    /// <param name="name">The Name value.</param>
    public void Delete(string codeLineName, string codeGroupName, string name)
    {
      if (NetString.HasValue(name))
      {
        var current = DataObject();
        var Solutions = LoadAllExcept(codeLineName, codeGroupName, name);
        if (Solutions != null)
        {
          WriteFileWithBackup(Solutions);
        }
        if (current != null)
        {
          Retrieve(current.CodeLine, current.CodeGroup, current.Name);
        }
      }
    }

    /// <summary>
    /// Retrieves a collection of Solution records.
    /// </summary>
    /// <param name="codeLineName">The CodeLine name.</param>
    /// <param name="codeGroupName">The CodeGroup name.</param>
    /// <param name="name">The Name value.</param>
    /// <returns>The Solutions collection if available; otherwise null.</returns>
    public Solutions Load(string codeLineName = null
      , string codeGroupName = null, string name = null)
    {
      Solutions retValue = null;

      var solution = CreateObject(codeLineName, codeGroupName, name, null);
      Reader.LJCOpen();
      if (Reader.Read())
      {
        retValue = new Solutions();
        do
        {
          if (IsMatch(solution))
          {
            retValue.Add(DataObject());
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
    /// <param name="codeGroupName">The CodeGroup name.</param>
    /// <param name="name">The Name value.</param>
    /// <returns>The Solutions collection if available; otherwise null.</returns>
    public Solutions LoadAllExcept(string codeLineName,string codeGroupName
      , string name)
    {
      Solutions retValue = null;

      Reader.LJCOpen();
      if (Reader.Read())
      {
        retValue = new Solutions();
        do
        {
          var CodeLineValue = Reader.GetTrimValue("CodeLine");
          var CodeGroupValue = Reader.GetTrimValue("CodeGroup");
          var nameValue = Reader.GetTrimValue("Name");
          if (CodeLineValue  != codeLineName
            && CodeGroupValue != codeGroupName
            && nameValue != name)
          {
            var Solution = DataObject();
            retValue.Add(Solution);
          }
        } while (Reader.Read());
        Reader.LJCOpen();
      }
      return retValue;
    }

    /// <summary>
    /// Retrieves a Solution record.
    /// </summary>
    /// <param name="codeLineName">The CodeLine name.</param>
    /// <param name="codeGroupName">The Solution name.</param>
    /// <param name="name">The Name value.</param>
    /// <returns>The Solution Data Object if found; otherwise null.</returns>
    public Solution Retrieve(string codeLineName, string codeGroupName
      , string name = null)
    {
      Solution retValue = null;

      if (NetString.HasValue(codeGroupName))
      {
        Reader.LJCOpen();
        while (Reader.Read())
        {
          var codeLineValue = Reader.GetTrimValue("CodeLine");
          var codeGroupValue = Reader.GetTrimValue("CodeGroup");
          var nameValue = Reader.GetTrimValue("Name");
          if (codeLineValue == codeLineName
            && codeGroupValue == codeGroupName)
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
    /// Updates a Solution file record.
    /// </summary>
    /// <param name="Solution">The Solution Data Object.</param>
    public Solution Update(Solution Solution)
    {
      Solution retValue = null;

      if (NetString.HasValue(Solution.Name))
      {
        var current = DataObject();
        var Solutions = LoadAllExcept(Solution.CodeLine,Solution.CodeGroup
          , Solution.Name);
        if (Solutions != null)
        {
          WriteFileWithBackup(Solutions);
        }
        Reader.Close();
        var text = CreateRecord(Solution);
        File.AppendAllText(FileName, text);
        Reader.LJCOpen();
        Retrieve(current.CodeLine, current.CodeGroup, current.Name);
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
      foreach (Solution Solution in Solutions)
      {
        var text = CreateRecord(Solution);
        File.AppendAllText(fileName, text);
      }
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
    public Solution DataObject()
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
      var Solutions = Load();
      Solutions.LJCSortUnique();
      WriteFileWithBackup(Solutions);
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

    private Solution CreateObject(string codeLineName, string codeGroupName
      , string name, string pathName)
    {
      var retValue = new Solution()
      {
        CodeLine = codeLineName,
        CodeGroup = codeGroupName,
        Name = name,
        Path = pathName
      };
      return retValue;
    }

    private bool IsMatch(Solution solution)
    {
      var retValue = false;

      var codeLineValue = Reader.GetTrimValue("CodeLine");
      var codeGroupValue = Reader.GetTrimValue("CodeGroup");
      var nameValue = Reader.GetTrimValue("Name");
      if (codeLineValue == solution.CodeLine
        && codeGroupValue == solution.CodeGroup
        && nameValue == solution.Name)
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
