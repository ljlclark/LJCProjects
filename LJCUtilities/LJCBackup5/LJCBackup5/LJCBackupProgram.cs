// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCBackupProgram.cs
using LJCNetCommon5;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

      var selection = SelectOption(profiles);
      if (selection >= 0
        && selection <= profiles.Count)
      {
        var profile = CreateChanges(profiles, selection);
        ViewProposedChanges(profile);
        ApplyChanges(profile);
        Console.WriteLine();
      }
    }

    // Creates the changes file.
    private static LJCBackupProfile CreateChanges(LJCBackupProfiles profiles
      , int selection)
    {
      LJCBackupProfile retValue = profiles[selection];

      Console.WriteLine();
      Console.Write("Creating Changes File...");
      var profile = profiles[selection];
      var createChanges = new LJCCreateFileChanges(profile.SourcePath
        , profile.TargetPath, profile.ChangesFilespec)
      {
        IncludeFilters = profile.IncludeFilters,
        SkipFiles = profile.SkipFiles,
        IncludeMissingTargetFolders = profile.IncludeMissingTargetFolders,
      };
      createChanges.Run();
      return retValue;
    }

    // Select to apply changes.
    private static void ApplyChanges(LJCBackupProfile profile)
    {
      Console.WriteLine();
      Console.WriteLine();
      Console.Write("Apply Changes Y/(N): ");
      var key = Console.ReadKey();
      var ch = (char)key.KeyChar;
      if ("Yy".Contains(ch))
      {
        Console.WriteLine();
        Console.Write("Applying Changes...");
        var sourceCodeLine = profile.SourceCodeline;
        var changesFilespec = profile.ChangesFilespec;
        var backupChanges = new LJCBackupChanges(sourceCodeLine
          , changesFilespec);
        backupChanges.Run(profile.TargetPath);
        Console.WriteLine();
        Console.Write("Changes Applied");
        Console.WriteLine();
        Console.WriteLine();
        Console.Write("View changes log (Y/(N): ");
        key = Console.ReadKey();
        ch = (char)key.KeyChar;
        if ("Yy".Contains(ch))
        {
          LJCNetFile.ShellProgram("BackupLog.txt");
        }
      }
    }

    // Gets the profile selection.
    private static int SelectOption(LJCBackupProfiles? profiles
      , bool exitOnly = false)
    {
      var retValue = -1;

      Console.Write("Select Option: ");

      var count = 0;
      if (LJC.HasListItems(profiles))
      {
        count = profiles.Count;
      }

      var success = false;
      while (!success)
      {
        var key = Console.ReadKey();
        var ch = (char)key.KeyChar;
        if ("Xx".Contains(ch))
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
              if (selection <= count - 1)
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
        //SelectOption(null, true);
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

    // Select to view proposed changes.
    private static void ViewProposedChanges(LJCBackupProfile profile)
    {
      Console.WriteLine();
      Console.WriteLine();
      Console.Write("View Proposed Changes Y/(N): ");
      var key = Console.ReadKey();
      var ch = (char)key.KeyChar;
      if ("Yy".Contains(ch))
      {
        Console.WriteLine();
        Console.Write($"File: {profile.ChangesFilespec}");
        LJCNetFile.ShellProgram(profile.ChangesFilespec);
      }
    }
  }
}
