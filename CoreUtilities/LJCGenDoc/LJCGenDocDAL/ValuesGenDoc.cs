// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ValuesGenDoc.cs
using LJCDBClientLib;
using LJCNetCommon;
using System.IO;

namespace LJCGenDocDAL
{
  /// <summary>The Application values singleton class.</summary>
  public sealed class ValuesGenDoc
  {
    #region Constructors

    // Initializes an instance of the object.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public ValuesGenDoc()
    {
      ArgError = new ArgError("LJCGenDocDAL.ValuesGenDoc");
      Errors = "";
      StandardSettings = new StandardUISettings();
    }

    /// <summary>Configures the settings.</summary>
    /// <param name="fileSpec">The config file name.</param>
    public void SetConfigFile(string fileSpec = "LJCGenDoc.exe.config")
    {
      if (!File.Exists(fileSpec))
      {
        ArgError.MethodName = "SetConfigFile(fileSpec)";
        var message = ArgError.ToString();
        message += $"File {fileSpec} was not found.\r\n";
        Errors += message;
      }
      else
      {
        // Update for changed file name.
        fileSpec = fileSpec.Trim();
        if (!NetString.IsEqual(fileSpec, FileSpec))
        {
          FileSpec = fileSpec;
          StandardSettings.SetProperties(fileSpec);

          var settings = StandardSettings;
          Managers = new ManagersGenDoc();
          Managers.SetDBProperties(settings.DbServiceRef
            , settings.DataConfigName);
        }
      }
    }
    #endregion

    #region Properties

    /// <summary>Gets the Error message</summary>
    public string Errors { get; private set; }

    /// <summary>Gets the config FileSpec.</summary>
    public string FileSpec { get; private set; }

    /// <summary>Gets or sets the generated page count.</summary>
    public int GenPageCount { get; set; }

    /// <summary>Gets the singleton instance.</summary>
    public static ValuesGenDoc Instance
    {
      get { return mInstance; }
    }

    /// <summary>Gets or sets the Managers class reference.</summary>
    public ManagersGenDoc Managers { get; set; }

    /// <summary>Gets the StandardSettings value.</summary>
    public StandardUISettings StandardSettings { get; private set; }

    // Represents Argument errors.
    private ArgError ArgError { get; set; }
    #endregion

    #region Class Data

    /// <summary>Initialize Singleton.</summary>
    private static readonly ValuesGenDoc mInstance
      = new ValuesGenDoc();
    #endregion
  }
}
