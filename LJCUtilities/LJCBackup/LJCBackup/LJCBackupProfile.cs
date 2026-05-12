// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// BackupProfile.cs

namespace LJCBackup
{
  // Represents a backup profile.
  /// <include path="members/LJCBackupProfile/*" file="Doc/LJCBackupProfile.xml"/>
  public class LJCBackupProfile
  {
    // The constructor without parameters.
    /// <include path="members/ConstructorDefault/*" file="Doc/LJCBackupProfile.xml"/>
    public LJCBackupProfile()
    {
      Name = "";
      SourceRoot = "";
      TargetRoot = "";
      ChangesFileSpec = "";
      IncludeFilters = new List<string>();
      SkipFiles = new List<string>();
    }

    // The constructor with parameters.
    /// <include path="members/ConstructorParam/*" file="Doc/LJCBackupProfile.xml"/>
    public LJCBackupProfile(string name) : this()
    {
      Name = name;
    }
    
    // Gets or sets the profile name.
    /// <include path="members/Name/*" file="Doc/LJCBackupProfile.xml"/>
    public string Name { get; set; }

    // Gets or sets the source root value.
    /// <include path="members/SourceRoot/*" file="Doc/LJCBackupProfile.xml"/>
    public string SourceRoot { get; set; }

    // Gets or sets the target root value.
    /// <include path="members/TargetRoot/*" file="Doc/LJCBackupProfile.xml"/>
    public string TargetRoot { get; set; }

    // Gets or sets the changes filespec.
    /// <include path="members/ChangesFileSpec/*" file="Doc/LJCBackupProfile.xml"/>
    public string ChangesFileSpec { get; set; }

    // Gets or sets the included file filters.
    /// <include path="members/IncludeFilters/*" file="Doc/LJCBackupProfile.xml"/>
    public List<string> IncludeFilters { get; set; }

    // Gets or sets the skipped files.
    /// <include path="members/SkipFiles/*" file="Doc/LJCBackupProfile.xml"/>
    public List<string> SkipFiles { get; set; }
  }
}
