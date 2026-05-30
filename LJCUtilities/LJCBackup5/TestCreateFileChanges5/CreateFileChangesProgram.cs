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
        var createFileChanges = new LJCCreateFileChanges(profile.SourceRoot
          , profile.TargetRoot, profile.ChangesFilespec)
        {
          IncludeFilters = profile.IncludeFilters,
          SkipFiles = profile.SkipFiles,
        };
        if (createFileChanges != null)
        {
          var value = profile.IncludeMissingTargetFolders;
          result = value.ToString();
        }
      }

      var compare = "True";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
