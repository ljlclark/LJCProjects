// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCBackupProfiles.cs
using LJCNetCommon5;

namespace LJCBackupCommonLib5
{
  // Represents a collection of backup profiles.
  /// <include file="Doc/LJCBackupProfiles.xml"
  ///  path="members/LJCBackupProfiles/*"/>
  public class LJCBackupProfiles : List<LJCBackupProfile>
  {
    #region Static Methods

    // Creates a sample profiles file.
    /// <include file="Doc/LJCBackupProfiles.xml"
    ///  path="members/CreateSampleProfilesFile/*"/>
    public static void CreateSampleProfilesFile(string profilesFilespec)
    {
      var values = new List<string>
      {
        "Backup, CLJCProjects",
      };
      var codelineRoot = @"C:\Users\User\Documents\Visual Studio 2022";

      var property = "SourceRoot";
      var text = $"{property}, {codelineRoot}";
      values.Add(text);

      property = "SourceCodeline";
      text = $"{property}, LJCProjectsDev";
      values.Add(text);

      property = "TargetRoot";
      text = $"{property}, {codelineRoot}";
      values.Add(text);

      property = "TargetCodeline";
      text = $"{property}, LJCProjects";
      values.Add(text);

      property = "ChangesFilespec";
      text = $@"{property}, LJCProjects\ChangesLJCProjects.txt";
      values.Add(text);

      property = "SkipFoldersFilespec";
      text = $@"{property}, LJCProjects\SkipFoldersLJCProjects.txt";
      values.Add(text);

      property = "IncludeFilters";
      text = $"{property}, *.cs|*.sln|*.csproj|*.Designer.cs|*.resx|*.config";
      values.Add(text);
      text = $@"{property}, Doc\*.xml|*.cmd|*.txt|*.sql";
      values.Add(text);

      property = "SkipFiles";
      text = $@"{property}, ChangesFile.txt|BackupLog.txt|MissingFolders.txt";
      values.Add(text);
      text = $"{property},?Build*.cmd|ClearBuild.cmd|UpdateAll.cmd";
      values.Add(text);

      var lines = values.ToArray();
      if (!File.Exists(profilesFilespec))
      {
        File.WriteAllLines(profilesFilespec, lines);
      }
    }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file="Doc/LJCBackupProfiles.xml"
    ///  path="members/Constructor/*"/>
    public LJCBackupProfiles()
    {
      mFilespec = "";
    }

    // Initializes an object instance with the supplied values.
    /// <include file="Doc/LJCBackupProfiles.xml"
    ///  path="members/ConstructorParam/*"/>
    public LJCBackupProfiles(string filespec) : this()
    {
      Filespec = filespec;
    }

    // Loads the profiles collection data.
    /// <include file="Doc/LJCBackupProfiles.xml"
    ///  path="members/LoadProfiles/*"/>
    public void LoadProfiles(string profilesFilespec)
    {
      LJCBackupProfile? profile = null;

      var lines = File.ReadAllLines(profilesFilespec);
      if (LJC.HasArrayElements(lines))
      {
        foreach (var line in lines)
        {
          var lineSearch = line.Trim().ToLower();

          var values = line.Split(',');
          if (LJC.HasArrayElements(values)
            && LJC.HasText(values[0]))
          {
            var property = values[0].Trim();
            var value = values[1].Trim();

            if (LJC.Equals("Backup", property))
            {
              if (profile != null)
              {
                Add(profile);
              }
              profile = new LJCBackupProfile(value);
            }

            if (profile != null)
            {
              switch (property.ToLower())
              {
                case "changesfilespec":
                  profile.ChangesFilespec = value;
                  break;

                case "includefilters":
                  var filters = value.Split("|");
                  foreach (var filter in filters)
                  {
                    profile.IncludeFilters.Add(filter.Trim());
                  }
                  break;

                case "includemissingtargetfolders":
                  if (bool.TryParse(value, out bool include))
                  {
                    profile.IncludeMissingTargetFolders = include;
                  }
                  break;

                case "skipfiles":
                  var skipFiles = value.Split("|");
                  foreach (var skipFile in skipFiles)
                  {
                    profile.SkipFiles.Add(skipFile.Trim());
                  }
                  break;

                case "skipfoldersfilespec":
                  profile.SkipFoldersFilespec = value;
                  break;

                case "sourcecodeline":
                  profile.SourceCodeline = value;
                  break;

                case "sourceroot":
                  profile.SourceRoot = value;
                  break;

                case "targetcodeline":
                  profile.TargetCodeline = value;
                  break;

                case "targetroot":
                  profile.TargetRoot = value;
                  break;
              }
            }
          }
        }
        if (profile != null)
        {
          Add(profile);
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and returns a clone of the object.
    /// <include file="Doc/LJCBackupProfiles.xml"
    ///  path="members/Clone/*"/>
    public LJCBackupProfiles? Clone()
    {
      var retValue = MemberwiseClone() as LJCBackupProfiles;
      return retValue;
    }

    // Checks if the collection has items.
    /// <include file="Doc/LJCBackupProfiles.xml"
    ///  path="members/LJCHasItems/*"/>
    public bool LJCHasItems()
    {
      bool retValue = false;

      if (Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }

    // Removes an item by keys.
    /// <include file="Doc/LJCBackupProfiles.xml"
    ///  path="members/LJCRemove/*"/>
    public void LJCRemove(string name)
    {
      LJCBackupProfile? item = Find(x => x.Name == name);
      if (item != null)
      {
        Remove(item);
      }
    }

    // Retrieve the collection element.
    /// <include file="Doc/LJCBackupProfiles.xml"
    ///  path="members/LJCRetrieve/*"/>
    public LJCBackupProfile? LJCRetrieve(string name)
    {
      LJCBackupProfile? retValue = null;

      LJCSort();
      LJCBackupProfile searchItem = new()
      {
        Name = name,
      };
      int index = BinarySearch(searchItem);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }
    #endregion

    #region Search and Sort Methods

    // Sort on the unique key.
    /// <include file="Doc/LJCBackupProfiles.xml"
    ///  path="members/LJCSortPrimary/*"/>
    public void LJCSort()
    {
      if (Count != mPrevCount)
      {
        mPrevCount = Count;
        Sort();
      }
    }
    #endregion

    #region Properties

    // Gets or sets the content filespec.
    /// <include file="Doc/LJCBackupProfiles.xml"
    ///  path="members/Filespec/*"/>
    public string Filespec
    {
      get { return mFilespec; }
      set
      {
        if (LJC.HasText(value))
        {
          mFilespec = value.Trim();
          if (!File.Exists(mFilespec))
          {
            CreateSampleProfilesFile(mFilespec);
          }
          LoadProfiles(mFilespec);
        }
      }
    }
    private string mFilespec;

    // The item for the specified name.
    /// <include file="Doc/LJCBackupProfiles.xml"
    ///  path="members/NameIndexer/*"/>
    public LJCBackupProfile? this[string name]
    {
      get { return LJCRetrieve(name); }
    }
    #endregion

    #region Class Data

    private int mPrevCount;
    #endregion
  }
}
