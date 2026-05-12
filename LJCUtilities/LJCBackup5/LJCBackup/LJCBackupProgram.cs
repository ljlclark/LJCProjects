// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCBackupProgram.cs
using LJCNetCommon5;

// <include path="members/LJCBackup/*" file="Doc/LJCBackup.xml"/>
// LibName: LJCBackup

namespace LJCBackup5
{
  // The program entry class.
  /// <include path="members/BackupProgram/*" file="Doc/BackupProgram.xml"/>
  internal class LJCBackupProgram
  {
    // The program entry method.
    /// <include path="members/Main/*" file="Doc/BackupProgram.xml"/>
    static void Main()
    {
      var profilesFileSpec = "BackupProfiles.txt";
      var profiles = new LJCBackupProfiles(profilesFileSpec);
      ShowSelections(profilesFileSpec, profiles);

      if (LJC.HasListItems(profiles))
      {
        var count = profiles.Count;
        var selection = ProfileSelection(count);
        if (selection >= 0
          && selection <= count)
        {
          var profile = profiles[selection];
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
  }
}
