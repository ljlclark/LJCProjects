// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ValuesDataUtility.cs
using LJCDataUtilityDAL;
using LJCDBClientLib;
using LJCNetCommon;
using System.IO;

namespace LJCDataUtilityDAL
{
  /// <summary>The Application values singleton class.</summary>
  public sealed class ValuesDataUtility
  {
    #region Constructors

    // Initializes an instance of the object.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public ValuesDataUtility()
    {
      ArgError = new ArgError("LJCDataUtilityDAL.ValuesDataUtility");
      Errors = "";
      StandardSettings = new StandardUISettings();
    }

    /// <summary>Configures the settings.</summary>
    /// <param name="fileSpec">The config file name.</param>
    public void SetConfigFile(string fileSpec = "LJCDataUtility.exe.config")
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
          Managers = new ManagersDataUtility();
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
    public static ValuesDataUtility Instance
    {
      get { return mInstance; }
    }

    /// <summary>Gets or sets the Managers class reference.</summary>
    public ManagersDataUtility Managers { get; set; }

    /// <summary>Gets the StandardSettings value.</summary>
    public StandardUISettings StandardSettings { get; private set; }

    // Represents Argument errors.
    private ArgError ArgError { get; set; }
    #endregion

    #region Class Data

    /// <summary>Initialize Singleton.</summary>
    private static readonly ValuesDataUtility mInstance
      = new ValuesDataUtility();
    #endregion
  }
}
