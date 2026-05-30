// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// BackupChangesProgram.cs
using LJCBackupChangesLib5;
using LJCBackupCommonLib5;
using LJCNetCommon5;

namespace TestBackupChanges5
{
  // The entry class.
  internal class BackupChangesProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCBackupChanges");
      Console.WriteLine();
      Console.WriteLine("*** LJCBackupChanges ***");

      // Static Methods
      CreateTargetFilespec();

      // Constructor Methods
      ConstructorParam();

      // Methods
      Run();
    }

    #region Static Methods

    // using LJCBackupChangesLib5;

    // Creates the Target Filespec.
    private static void CreateTargetFilespec()
    {
      var methodName = "CreateTargetFilespec()";

      // Test Setup
      var sourceRoot = @"C:\Users\User\SourceRoot";
      var sourceCodeline = "CodeLineDev";
      var sourceFilespec = $@"{sourceRoot}\{sourceCodeline}\File.cs";
      var targetRoot = @"C:\Users\User\TargetRoot";
      var targetCodeline = "CodeLine";
      var targetPath = $@"{targetRoot}\{targetCodeline}\";

      // Live Test Setup
      sourceRoot = @"C:\Users\Les\Documents\Visual Studio 2022";
      sourceCodeline = "LJCProjectsDev";
      sourceFilespec = $@"{sourceRoot}\{sourceCodeline}\1BuildAll.cmd";
      targetRoot = @"C:\Users\Les\Documents\Visual Studio 2022";
      targetCodeline = "LJCProjects";
      targetPath = $@"{targetRoot}\{targetCodeline}\";

      // Test Method
      // Source file must exist.
      var targetFilespec = LJCBackupChanges.CreateTargetFilespec
        (sourceFilespec, sourceCodeline, targetPath);

      var result = targetFilespec;
      var compare = @"C:\Users\User\TargetRoot";

      // Live Compare
      compare = @"C:\Users\Les\Documents\Visual Studio 2022";

      compare += @"\LJCProjects\1BuildAll.cmd";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Constructor Methods

    // using LJCBackupChangesLib5;
    // using LJCBackupCommonLib5;

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
        var backupChanges = new LJCBackupChanges(profile.SourceCodeline
          , profile.ChangesFilespec);
        if (backupChanges != null)
        {
          result = profile.SourceCodeline;
        }
      }

      var compare = "LJCProjectsDev";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Methods

    // using LJCBackupChangesLib5;
    // using LJCBackupCommonLib5;

    // Applies the change commands.
    private static void Run()
    {
      var methodName = "Run()";

      var profilesFilespec = "BackupProfiles.txt";
      var profiles = new LJCBackupProfiles(profilesFilespec);
      var profile = profiles[0];
      // or
      profile = profiles["LocalLJCProjects"];
      // or
      profile = profiles.LJCRetrieve("LocalLJCProjects");

      if (profile != null)
      {
        // Changes file must exist.
        if (!File.Exists(profile.ChangesFilespec))
        {
          LJCNetFile.CreateFolder(profile.ChangesFilespec);
          File.WriteAllText(profile.ChangesFilespec, "");
        }

        var backupChanges = new LJCBackupChanges(profile.SourceCodeline
          , profile.ChangesFilespec);

        // Test Method
        backupChanges.Run(profile.TargetPath);

        var result = backupChanges.TargetPath;

        var compare = @"C:\Users\User\Documents\Visual Studio 2022\LJCProjects";

        // Live Test
        compare = @"C:\Users\Les\Documents\Visual Studio 2022\LJCProjects";

        TestCommon?.Write($"{methodName}", result, compare);
      }
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
