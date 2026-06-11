// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCBackupProgram.cs
using LJCBackupChangesLib5;
using LJCBackupCommonLib5;
using LJCCreateFileChangesLib5;
using LJCNetCommon5;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

      while (true)
      {
        ShowSelections(profilesFileSpec, profiles);

        var selection = SelectOption(profiles);
        if (0 == selection)
        {
          break;
        }

        var profile = CreateChanges(profiles, selection - 1);
        ViewProposedChanges(profile);
        ApplyChanges(profile);
      }
    }

    // Select to apply changes.
    private static void ApplyChanges(LJCBackupProfile profile)
    {
      Console.WriteLine();
      var prompt = "Apply Changes Y|N: ";
      Console.Write(prompt);

      var value = LJC.KeyOption(prompt, "YyNn");
      if ("Yy".Contains(value))
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
        ViewLog();
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
        SkipCodePathspec = profile.SkipCodePathspec,
        SkipFiles = profile.SkipFiles,
        SkipSubfolders = profile.SkipSubfolders,
        IncludeMissingTargetFolders = profile.IncludeMissingTargetFolders,
      };
      createChanges.Run();
      Console.WriteLine();
      return retValue;
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
    private static int SelectOption(LJCBackupProfiles? profiles)
    {
      var retValue = 0;

      var prompt = "Select Option: ";
      Console.Write(prompt);

      var count = 0;
      if (LJC.HasListItems(profiles))
      {
        count = profiles.Count;
      }

      while (true)
      {
        var value = Console.ReadLine();

        if (!LJC.HasText(value))
        {
          LJC.ClearReadLine(prompt, "");
          continue;
        }

        var checkValue = value!.Trim();
        if ("Xx".Contains(checkValue))
        {
          break;
        }

        // Get integer profile selection.
        if (!int.TryParse(value, out int selection))
        {
          LJC.ClearReadLine(prompt, value);
          continue;
        }

        // Invalid selection value.
        if (selection < 1
          || selection > count)
        {
          LJC.ClearReadLine(prompt, value);
          continue;
        }

        // Get selection.
        if (LJC.HasListItems(profiles))
        {
          var profile = profiles[selection - 1];
          if (profile != null
            && IsAvailable(profile))
          {
            retValue = selection;
            break;
          }
        }

        // Clear unavailable selection.
        LJC.ClearReadLine(prompt, value);
      }
      return retValue;
    }

    // Shows the profile and exit selections.
    private static void ShowSelections(string profilesFileSpec
      , LJCBackupProfiles? profiles)
    {
      Console.WriteLine();
      if (!LJC.HasListItems(profiles))
      {
        Console.WriteLine($"No profiles were found in {profilesFileSpec}.");
        Console.WriteLine("X - Exit");
      }

      if (LJC.HasListItems(profiles))
      {
        var count = profiles.Count;
        for (int index = 0; index < count; index++)
        {
          var profile = profiles[index];

          var available = AvailableText(profile);
          Console.WriteLine($"{index + 1} - {profile.Name} {available}");
        }
        Console.WriteLine("X - Exit");
      }
    }

    // Select to view the backup log.
    private static void ViewLog()
    {
      Console.WriteLine();
      var prompt = "View changes log (Y/(N): ";
      Console.Write(prompt);

      var value = LJC.KeyOption(prompt, "YyNn");
      if ("Yy".Contains(value))
      {
        LJCNetFile.ShellProgram("BackupLog.txt");
      }
    }

    // Select to view proposed changes.
    private static void ViewProposedChanges(LJCBackupProfile profile)
    {
      Console.WriteLine();
      var prompt = "View Proposed Changes Y|N: ";
      Console.Write(prompt);

      var value = LJC.KeyOption(prompt, "YyNn");
      if ("Yy".Contains(value))
      {
        Console.WriteLine($"File: {profile.ChangesFilespec}");
        LJCNetFile.ShellProgram(profile.ChangesFilespec);
      }
    }
  }
}
