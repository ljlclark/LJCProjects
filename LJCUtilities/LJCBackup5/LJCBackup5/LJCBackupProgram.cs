// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCBackupProgram.cs
using LJCNetCommon5;

/// <include file="Doc/LJCBackup.xml"
///  path="members/LJCBackup/*"/>
// LibName: LJCBackup

namespace LJCBackup5
{
  // The program entry class.
  /// <include file="Doc/BackupProgram.xml"
  ///  path="members/BackupProgram/*"/>
  internal class LJCBackupProgram
  {
    // The program entry method.
    /// <include file="Doc/BackupProgram.xml"
    ///  path="members/Main/*"/>
    static void Main()
    {
      var profilesFileSpec = "BackupProfiles.txt";
      var profiles = new LJCBackupProfiles(profilesFileSpec);
      ShowSelections(profilesFileSpec, profiles);

      if (LJC.HasListItems(profiles))
      {
        var count = profiles.Count;
        Console.Write("Select Option: ");
        var selection = SelectOption(count);
        if (selection >= 0
          && selection <= count)
        {
          Console.WriteLine();
          Console.Write("Running...");
          var profile = profiles[selection];
          var createChanges = new LJCCreateFileChanges(profile.SourcePath
            , profile.TargetPath, profile.ChangesFilespec)
          {
            IncludeFilters = profile.IncludeFilters,
            SkipFiles = profile.SkipFiles
          };
          createChanges.Run();
        }
      }
    }

    // Gets the profile selection.
    private static int SelectOption(int profileCount, bool exitOnly = false)
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
              var selection = (int)ch - 48;
              if (selection <= profileCount - 1)
              {
                success = true;
                retValue = selection;
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

    // Shows the profile and exit selections.
    private static void ShowSelections(string profilesFileSpec
      , LJCBackupProfiles? profiles)
    {
      if (!LJC.HasListItems(profiles))
      {
        Console.WriteLine($"No profiles were found in {profilesFileSpec}.");
        Console.WriteLine("X - Exit");
        SelectOption(0, true);
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
  }
}
