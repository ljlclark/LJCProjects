// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCBackupProfile.cs

// Contains common backup code.
// Assembly level XML comments are in the first class XML.
// <include file="LJCBackupProfile.xml"
//  path="members/LJCBackupCommonLib5/*"/>
// Assembly: LJCBackupCommonLib5

using LJCNetCommon5;

namespace LJCBackupCommonLib5
{
  // Represents a backup profile.
  /// <include file="Doc/LJCBackupProfile.xml"
  ///  path="members/LJCBackupProfile/*"/>
  public class LJCBackupProfile : IComparable<LJCBackupProfile>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include file="Doc/LJCBackupProfile.xml"
    ///  path="members/Constructor/*"/>
    public LJCBackupProfile()
    {
      ChangesFilespec = "";
      IncludeFilters = [];
      Name = "";
      SkipCodePathspec = "";
      SkipFiles = [];
      SkipSubfolders = [];
      SourceCodeline = "";
      SourceRoot = "";
      TargetCodeline = "";
      TargetRoot = "";
    }

    // Initializes an object instance with the supplied values.
    /// <include file="Doc/LJCBackupProfile.xml"
    ///  path="members/ConstructorParam/*"/>
    public LJCBackupProfile(string name) : this()
    {
      Name = name;
    }
    #endregion

    #region Data Methods

    // Provides the default Sort functionality.
    /// <include file="Doc/LJCBackupProfile.xml"
    ///  path="members/CompareTo/*"/>
    public int CompareTo(LJCBackupProfile? other)
    {
      int retValue;

      if (null == other)
      {
        // This value is greater than null.
        retValue = LJCNetString.CompareGreater;
      }
      else
      {
        retValue = LJC.CompareNull(Name, other.Name);
        if (LJCNetString.CompareNotNullOrEqual == retValue)
        {
          // Not case sensitive.
          retValue = string.Compare(Name, other.Name, true);
        }
      }
      return retValue;
    }
    #endregion

    #region Properties

    // Gets or sets the changes filespec.
    /// <include file="Doc/LJCBackupProfile.xml"
    ///  path="members/ChangesFileSpec/*"/>
    public string ChangesFilespec { get; set; }

    // Gets or sets the included file filters.
    /// <include file="Doc/LJCBackupProfile.xml"
    ///  path="members/IncludeFilters/*"/>
    public List<string> IncludeFilters { get; set; }

    // Gets or sets the include missing target folders flag.
    /// <include file="Doc/LJCBackupProfile.xml"
    ///  path="members/IncludeMissingTargetFolders/*"/>
    public bool IncludeMissingTargetFolders { get; set; }

    // Gets or sets the profile name.
    /// <include file="Doc/LJCBackupProfile.xml"
    ///  path="members/Name/*"/>
    public string Name { get; set; }

    // Gets or sets the skipped code path file.
    /// <include file="Doc/LJCBackupProfile.xml"
    ///  path="members/SkipCodePathspec/*"/>
    public string SkipCodePathspec { get; set; }

    // Gets or sets the skipped files.
    /// <include file="Doc/LJCBackupProfile.xml"
    ///  path="members/SkipFiles/*"/>
    public List<string> SkipFiles { get; set; }

    // Gets or sets the skipped subfolders.
    /// <include file="Doc/LJCBackupProfile.xml"
    ///  path="members/SkipSubfolders/*"/>
    public List<string> SkipSubfolders { get; set; }

    // Gets or sets the source codeline value.
    /// <include file="Doc/LJCBackupProfile.xml"
    ///  path="members/SourceCodeline/*"/>
    public string SourceCodeline { get; set; }

    // Gets or sets the source root value.
    /// <include file="Doc/LJCBackupProfile.xml"
    ///  path="members/SourceRoot/*"/>
    public string SourceRoot { get; set; }

    // Gets or sets the target codeline value.
    /// <include file="Doc/LJCBackupProfile.xml"
    ///  path="members/TargetCodeline/*"/>
    public string TargetCodeline { get; set; }

    // Gets or sets the target root value.
    /// <include file="Doc/LJCBackupProfile.xml"
    ///  path="members/TargetRoot/*"/>
    public string TargetRoot { get; set; }

    // Gets the full source path.
    /// <include file="Doc/LJCBackupProfile.xml"
    ///  path="members/SourcePath/*"/>
    public string SourcePath
    {
      get { return $@"{SourceRoot}\{SourceCodeline}"; }
    }

    // Gets the full target path.
    /// <include file="Doc/LJCBackupProfile.xml"
    ///  path="members/TargetPath/*"/>
    public string TargetPath
    {
      get { return $@"{TargetRoot}\{TargetCodeline}"; }
    }
    #endregion
  }
}
