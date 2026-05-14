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
      IncludeFilters = [];
      Name = "";
      SkipFiles = [];
      SkipFoldersFilespec = "";
      SourceCodeline = "";
      SourceRoot = "";
      TargetCodeline = "";
      TargetRoot = "";
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

    // Gets or sets the source codeline value.
    /// <include path="members/SourceCodeline/*" file="Doc/LJCBackupProfile.xml"/>
    public string SourceCodeline { get; set; }

    // Gets or sets the source root value.
    /// <include path="members/SourceRoot/*" file="Doc/LJCBackupProfile.xml"/>
    public string SourceRoot { get; set; }

    // Gets or sets the target codeline value.
    /// <include path="members/TargetCodeline/*" file="Doc/LJCBackupProfile.xml"/>
    public string TargetCodeline { get; set; }

    // Gets or sets the target root value.
    /// <include path="members/TargetRoot/*" file="Doc/LJCBackupProfile.xml"/>
    public string TargetRoot { get; set; }
    #endregion

    // Gets or sets the full source path.
    /// <include path="members/SourcePath/*" file="Doc/LJCBackupProfile.xml"/>
    public string SourcePath
    {
      get { return $@"{SourceRoot}\{SourceCodeline}"; }
    }

    // Gets or sets the full target path.
    /// <include path="members/TargetPath/*" file="Doc/LJCBackupProfile.xml"/>
    public string TargetPath
    {
      get { return $@"{TargetRoot}\{TargetCodeline}"; }
    }
  }
}
