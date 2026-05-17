// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCBackupProfiles.cs
using LJCNetCommon5;

namespace LJCBackupCommonLib
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
    public static void CreateSampleProfilesFile(string profilesFileSpec)
    {
      var values = new List<string>
      {
        "Backup, CLJCProjects",
      };
      var codelineRoot = @"C:\Users\Les\Documents\Visual Studio 2022";

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
      File.WriteAllLines(profilesFileSpec, lines);
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
    public LJCBackupProfiles(string fileSpec) : this()
    {
      Filespec = fileSpec;
    }
    #endregion

    #region Collection Methods

    // Loads the profiles collection data.
    /// <include file="Doc/LJCBackupProfiles.xml"
    ///  path="members/LoadProfiles/*"/>
    public void LoadProfiles(string profilesFileSpec)
    {
      LJCBackupProfile? profile = null;

      var lines = File.ReadAllLines(profilesFileSpec);
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
    #endregion
  }
}
