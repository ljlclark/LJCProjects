// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCBackupProgram.cs
using LJCBackupChangesLib5;
using LJCBackupCommonLib5;
using LJCCreateFileChangesLib5;
using LJCNetCommon5;

// Contains classes for code backup.
// Assembly level XML comments are in the first class XML.
// <include file="Doc/LJCBackup.xml"
//  path="members/LJCBackup5/*"/>
// Assembly: LJCBackup5

namespace LJCBackup5
{
  // A code Backup program.
  /// <include file="Doc/LJCBackupProgram.xml"
  ///  path="members/LJCBackupProgram/*"/>
  internal class LJCBackupProgram
  {
    // The program entry method.
    /// <include file="Doc/LJCBackupProgram.xml"
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

    // Returns Not Available if TargetPath is not found.
    private static string AvailableText(LJCBackupProfile profile)
    {
      var retText = "";

      if (!IsAvailable(profile))
      {
        retText = " *** Not Available";
      }
      return retText;
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
        var sourceCodeline = profile.SourceCodeline;
        var changesFilespec = profile.ChangesFilespec;
        var backupChanges = new LJCBackupChanges(sourceCodeline
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

    // Checks if TargetPath is found.
    private static bool IsAvailable(LJCBackupProfile profile)
    {
      var retValue = true;

      if (!Directory.Exists(profile.TargetPath))
      {
        retValue = false;
      }
      return retValue;
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
                if (LJC.HasListItems(profiles))
                {
                  var profile = profiles[selection];
                  if (profile != null
                    && IsAvailable(profile))
                  {
                    success = true;
                    retValue = selection;
                  }
                }
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

          var available = AvailableText(profile);
          Console.WriteLine($"{index} - {profile.Name} {available}");
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
