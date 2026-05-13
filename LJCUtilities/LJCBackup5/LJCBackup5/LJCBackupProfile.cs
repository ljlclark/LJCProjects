// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCBackupProfile.cs

namespace LJCBackup5
{
  // Represents a backup profile.
  /// <include path="members/LJCBackupProfile/*" file="Doc/LJCBackupProfile.xml"/>
  public class LJCBackupProfile
  {
    // Initializes an object instance.
    /// <include path="members/ConstructorDefault/*" file="Doc/LJCBackupProfile.xml"/>
    public LJCBackupProfile()
    {
      ChangesFilespec = "";
      CodelineRoot = "";
      IncludeFilters = [];
      Name = "";
      SkipFiles = [];
      SkipFoldersFilespec = "";
      SourceCodeline = "";
      TargetCodeline = "";
    }

    // Initializes an object instance with the supplied values.
    /// <include path="members/ConstructorParam/*" file="Doc/LJCBackupProfile.xml"/>
    public LJCBackupProfile(string name) : this()
    {
      Name = name;
    }

    #region Properties

    // Gets or sets the changes filespec.
    /// <include path="members/ChangesFileSpec/*" file="Doc/LJCBackupProfile.xml"/>
    public string ChangesFilespec { get; set; }

    // Gets or sets the changes filespec.
    /// <include path="members/CodelineRoot/*" file="Doc/LJCBackupProfile.xml"/>
    public string CodelineRoot { get; set; }

    // Gets or sets the included file filters.
    /// <include path="members/IncludeFilters/*" file="Doc/LJCBackupProfile.xml"/>
    public List<string> IncludeFilters { get; set; }

    // Gets or sets the profile name.
    /// <include path="members/Name/*" file="Doc/LJCBackupProfile.xml"/>
    public string Name { get; set; }

    // Gets or sets the skipped files.
    /// <include path="members/SkipFiles/*" file="Doc/LJCBackupProfile.xml"/>
    public List<string> SkipFiles { get; set; }

    // Gets or sets the skipped files.
    /// <include path="members/SkipFoldersFilespec/*" file="Doc/LJCBackupProfile.xml"/>
    public string SkipFoldersFilespec { get; set; }

    // Gets or sets the source root value.
    /// <include path="members/SourceCodeline/*" file="Doc/LJCBackupProfile.xml"/>
    public string SourceCodeline { get; set; }

    // Gets or sets the target root value.
    /// <include path="members/TargetCodeline/*" file="Doc/LJCBackupProfile.xml"/>
    public string TargetCodeline { get; set; }
    #endregion
  }
}
