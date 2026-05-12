// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// BackupProgram.cs
using LJCNetCommon5;

// <include path="members/LJCBackup/*" file="Doc/LJCBackup.xml"/>
// LibName: LJCBackup

namespace LJCBackup
{
  // The program entry class.
  /// <include path="members/BackupProgram/*" file="Doc/BackupProgram.xml"/>
  internal class BackupProgram
  {
    // The program entry method.
    /// <include path="members/Main/*" file="Doc/BackupProgram.xml"/>
    static void Main()
    {
      var profilesFileSpec = "BackupProfiles.txt";
      if (!File.Exists(profilesFileSpec))
      {
        CreateSampleProfilesFile(profilesFileSpec);
      }

      var profiles = CreateProfiles(profilesFileSpec);
      ShowSelections(profilesFileSpec, profiles);

      if (LJC.HasListItems(profiles))
      {
        var count = profiles.Count;
        var selection = ProfileSelection(count);
        if (selection >= 0
          && selection <= count)
        {
        }
      }
    }

    // Shows the profile and exit selections.
    private static void ShowSelections(string profilesFileSpec
      , LJCBackupProfiles? profiles)
    {
      if (!LJC.HasListItems(profiles))
      {
        Console.WriteLine($"No profiles were found in {profilesFileSpec}.");
        Console.WriteLine("X - Exit");
        ProfileSelection(0, true);
      }

      if (LJC.HasListItems(profiles))
      {
        var count = profiles.Count;
        for (int index = 0; index < count; index++)
        {
          var profile = profiles[index];
          Console.WriteLine($"{index} - {profile.Name}");
        }
        Console.WriteLine("X - Exit");
      }
    }

    // Creates the profiles collection.
    private static LJCBackupProfiles? CreateProfiles(string profilesFileSpec)
    {
      LJCBackupProfiles? retProfiles = null;
      LJCBackupProfile? profile = null;

      var lines = File.ReadAllLines(profilesFileSpec);
      if (LJC.HasArrayElements(lines))
      {
        retProfiles = [];
        foreach (var line in lines)
        {
          var lineSearch = line.Trim().ToLower();

          var values = line.Split(',');
          if (LJC.HasArrayElements(values))
          {
            var property = values[0].Trim();
            var value = values[1].Trim();

            var comparisonType = StringComparison.OrdinalIgnoreCase;
            if (property.Equals("Backup", comparisonType))
            {
              if (profile != null)
              {
                retProfiles.Add(profile);
              }
              profile = new LJCBackupProfile(value);
            }

            if (profile != null)
            {
              switch (property.ToLower())
              {
                case "sourceroot":
                  profile.SourceRoot = value;
                  break;
                case "targetroot":
                  profile.TargetRoot = value;
                  break;
                case "changesfilespec":
                  profile.ChangesFileSpec = value;
                  break;
                case "includefilters":
                  var filters = value.Split("|");
                  foreach (var filter in filters)
                  {
                    profile.IncludeFilters.Add(filter.Trim());
                  }
                  break;
                case "skipfiles":
                  var skipFiles = value.Split("|");
                  foreach (var skipFile in skipFiles)
                  {
                    profile.SkipFiles.Add(skipFile.Trim());
                  }
                  break;
              }
            }
          }
        }
        if (profile != null)
        {
          retProfiles.Add(profile);
        }
      }
      return retProfiles;
    }

    // Creates a sample profiles file.
    private static void CreateSampleProfilesFile(string profilesFileSpec)
    {
      var values = new List<string>
      {
        "Backup, LJCProjectsDev to LJCProjects",
      };
      var codeRoot = @"C:\Users\Les\Documents\Visual Studio 2022";

      var property = "SourceRoot";
      var root = $"{property}, {codeRoot}";
      var text = $"{root}\\LJCProjectsDev";
      values.Add(text);

      property = "TargetRoot";
      root = $"{property}, {codeRoot}";
      text = $"{root}\\LJCProjects";
      values.Add(text);

      property = "ChangesFileSpec";
      text = $"{property}, ChangesLJCProjects.txt";
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

    // Gets the profile selection.
    private static int ProfileSelection(int profileCount, bool exitOnly = false)
    {
      var retValue = -1;

      var success = false;
      while (!success)
      {
        var key = Console.ReadKey();
        var ch = (char)key.KeyChar;
        if ('x' == ch
          || 'X' == ch)
        {
          success = true;
        }

        if (!success)
        {
          if (!exitOnly)
          {
            if (ch >= '0'
              && ch <= '9')
            {
              if ((int)ch - 48 <= profileCount - 1)
              {
                success = true;
                retValue = (int)ch;
              }
            }
          }

          if (!success)
          {
            Console.Write("\b");
          }
        }
      }
      return retValue;
    }
  }
}
