using LJCNetCommon;

namespace BackupWatcher
{
  /// <summary></summary>
  public class FileChange
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="changeType"></param>
    /// <param name="fileSpec"></param>
    /// <param name="toFileName"></param>
    public FileChange(string changeType, string fileSpec, string toFileName = null)
    {
      ChangeType = changeType;
      FileSpec = fileSpec;
      ToFileSpec = toFileName;
    }

    /// <summary></summary>
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

    /// <summary></summary>
    public string ChangeType { get; set; }

    /// <summary></summary>
    public string FileSpec { get; set; }

    /// <summary></summary>
    public string ToFileSpec { get; set; }
  }
}
