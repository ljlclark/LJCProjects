// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// FileChange.cs
using LJCNetCommon;

namespace LJCBackupCommonLib
{
  /// <summary>Represents the File change object.</summary>
  public class FileChange
  {
    // Initializes the object instance.
    /// <include path='items/FileChangeC/*' file='Doc/FileChange.xml'/>
    public FileChange(string changeType, string fileSpec, string toFileName = null)
    {
      ChangeType = changeType;
      FileSpec = fileSpec;
      ToFileSpec = toFileName;
    }

    /// <summary>Returns the Change text.</summary>
    public string Text()
    {
      string retValue = ChangeType;
      retValue += $",{FileSpec}";
      if (NetString.HasValue(ToFileSpec))
      {
        retValue += $",{ToFileSpec}";
      }
      return retValue;
    }

    #region Public Properties

    /// <summary>Gets or sets the Change type.</summary>
    public string ChangeType
    {
      get { return mChangeType; }
      set { mChangeType = NetString.InitString(value); }
    }
    private string mChangeType;

    /// <summary>Gets or sets the FileSpec.</summary>
    public string FileSpec
    {
      get { return mFileSpec; }
      set { mFileSpec = NetString.InitString(value); }
    }
    private string mFileSpec;

    /// <summary>Gets or sets the To FileSpec.</summary>
    public string ToFileSpec
    {
      get { return mToFileSpec; }
      set { mToFileSpec = NetString.InitString(value); }
    }
    private string mToFileSpec;
    #endregion
  }
}
