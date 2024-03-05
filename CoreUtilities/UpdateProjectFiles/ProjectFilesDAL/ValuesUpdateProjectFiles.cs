using LJCNetCommon;
using System.Drawing;
using System.IO;

namespace ProjectFilesDAL
{
  public sealed class ValuesUpdateProjectFiles
  {
    #region Constructors

    // Initializes an object instance.
    public ValuesUpdateProjectFiles()
    {
      SetConfigFileSpec("UpdateProjectFiles.exe.config");
    }
    #endregion

    #region Public Methods

    public void SetConfigFileSpec(string fileSpec)
    {
      bool success = true;
      if (!NetString.HasValue(fileSpec))
      {
        // Do not continue if no fileSpec.
        success = false;
      }

      if (success)
      {
        fileSpec = fileSpec.Trim();
        if (NetString.HasValue(FileSpec)
          && !NetString.IsEqual(fileSpec, FileSpec))
        {
          // Do not continue if fileSpec equals FileSpec.
          success = false;
        }
      }

      if (success
        && File.Exists(fileSpec))
      {
        // Process if changed fileName exists.
        FileSpec = fileSpec;
        SetProperties(FileSpec);
      }
    }
    #endregion

    #region Private Methods

    // Sets the Settings properties.
    public void SetProperties(string fileSpec)
    {
      AppSettings = new AppSettings(fileSpec);
      BeginColor = AppSettings.GetColor("BeginColor", Color.AliceBlue);
      EndColor = AppSettings.GetColor("EndColor", Color.LightSkyBlue);
      Managers = new ManagersProjectFiles();
    }
    #endregion

    #region Properties

    /// <summary>The begin gradient color.</summary>
    public Color BeginColor { get; private set; }

    /// <summary>Gets or sets the config FileSpec.</summary>
    public string FileSpec { get; private set; }

    /// <summary>The end gradient color.</summary>
    public Color EndColor { get; private set; }

    // Gets the singleton instance.
    public static ValuesUpdateProjectFiles Instance
    {
      get { return mInstance; }
    }

    /// <summary>Gets or sets the Managers class reference.</summary>
    public ManagersProjectFiles Managers { get; set; }

    // Gets or sets the AppSettings value.
    internal AppSettings AppSettings { get; set; }
    #endregion

    #region Class Data

    // The singleton instance.
    private static readonly ValuesUpdateProjectFiles mInstance
      = new ValuesUpdateProjectFiles();
    #endregion
  }
}
