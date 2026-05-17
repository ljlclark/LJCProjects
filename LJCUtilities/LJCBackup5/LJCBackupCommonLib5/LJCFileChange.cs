// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCFileChange.cs
using LJCNetCommon5;

namespace LJCBackupCommonLib5
{
  // Represents the File change object.
  /// <include file='Doc/LJCFileChange.xml'
  ///  path='members/LJCFileChange/*'/>
  public class LJCFileChange
  {
    #region Constructor Methods

    // Initializes the object instance.
    /// <include file='Doc/LJCFileChange.xml'
    ///  path='members/Constructor/*'/>
    public LJCFileChange()
    {
      mChangeType = "";
      mFileSpec = "";
    }

    // Initializes the object instance with the supplied values.
    /// <include file='Doc/LJCFileChange.xml'
    ///  path='members/ConstructorParam/*'/>
    public LJCFileChange(string changeType, string fileSpec
      , string? toFileName = null) : this()
    {
      ChangeType = changeType;
      Filespec = fileSpec;
      ToFilespec = toFileName;
    }
    #endregion

    #region Methods

    // Returns the Change text.
    /// <include file='Doc/LJCFileChange.xml'
    ///  path='members/Text/*'/>
    public string Text()
    {
      string retValue = ChangeType;
      retValue += $",{Filespec}";
      if (LJC.HasText(ToFilespec))
      {
        retValue += $",{ToFilespec}";
      }
      return retValue;
    }
    #endregion

    #region Properties

    // Gets or sets the Change type.
    /// <include file='Doc/LJCFileChange.xml'
    ///  path='members/ChangeType/*'/>
    public string ChangeType
    {
      get { return mChangeType; }
      set
      {
        if (LJC.HasText(value))
        {
          mChangeType = value.Trim();
        }
      }
    }
    private string mChangeType;

    // Gets or sets the FileSpec.
    /// <include file='Doc/LJCFileChange.xml'
    ///  path='members/FileSpec/*'/>
    public string Filespec
    {
      get { return mFileSpec; }
      set
      {
        if (LJC.HasText(value))
        {
          mFileSpec = value.Trim();
        }
      }
    }
    private string mFileSpec;

    // Gets or sets the To FileSpec.
    /// <include file='Doc/LJCFileChange.xml'
    ///  path='members/ToFileSpec/*'/>
    public string? ToFilespec
    {
      get { return mToFileSpec; }
      set { mToFileSpec = LJCNetString.InitString(value); }
    }
    private string? mToFileSpec;
    #endregion
  }
}
