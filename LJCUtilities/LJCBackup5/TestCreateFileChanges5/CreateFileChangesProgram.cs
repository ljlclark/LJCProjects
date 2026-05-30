// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CreateFileChangesProgram.cs
using LJCBackupCommonLib5;
using LJCCreateFileChangesLib5;
using LJCNetCommon5;

namespace TestCreateFileChanges5
{
  // The entry class.
  internal class CreateFileChangesProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCCreateFileChanges");
      Console.WriteLine();
      Console.WriteLine("*** LJCCreateFileChanges ***");

      // Constructor Methods
      ConstructorParam();

      // Method
      Run();
    }

    #region Constructor Methods

    // using LJCBackupCommonLib5;
    // using LJCBackupChangesLib5;

    // Initializes an object instance with the supplied values.
    private static void ConstructorParam()
    {
      var methodName = "ConstructorParam()";

      var profilesFilespec = "BackupProfiles.txt";
      var profiles = new LJCBackupProfiles(profilesFilespec);
      var profile = profiles[0];
      // or
      profile = profiles["LocalLJCProjects"];
      // or
      profile = profiles.LJCRetrieve("LocalLJCProjects");

      string? result = null;
      if (profile != null)
      {
        // Test Method
        var createChanges = new LJCCreateFileChanges(profile.SourceRoot
          , profile.TargetRoot, profile.ChangesFilespec)
        {
          IncludeFilters = profile.IncludeFilters,
          SkipFiles = profile.SkipFiles,
          IncludeMissingTargetFolders = profile.IncludeMissingTargetFolders,
        };
        if (createChanges != null)
        {
          var value = profile.IncludeMissingTargetFolders;
          result = value.ToString();
        }
      }

      var compare = "False";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Runs the create "Changes" file process.
    private static void Run()
    {
      //var methodName = "Run()";

      var profilesFilespec = "BackupProfiles.txt";
      var profiles = new LJCBackupProfiles(profilesFilespec);
      var profile = profiles[0];
      // or
      profile = profiles["LocalLJCProjects"];
      // or
      profile = profiles.LJCRetrieve("LocalLJCProjects");

      if (profile != null)
      {
        var createChanges = new LJCCreateFileChanges(profile.SourceRoot
          , profile.TargetRoot, profile.ChangesFilespec)
        {
          IncludeFilters = profile.IncludeFilters,
          SkipFiles = profile.SkipFiles,
          IncludeMissingTargetFolders = profile.IncludeMissingTargetFolders,
        };

        if (createChanges != null)
        {
          // Test Method
          createChanges.Run();
        }
      }
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
