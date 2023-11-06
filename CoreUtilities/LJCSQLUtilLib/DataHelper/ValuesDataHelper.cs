// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ValuesDataHelper.cs
using LJCDBClientLib;
using LJCNetCommon;
using System.IO;

namespace DataHelper
{
	/// <summary>
	/// 
	/// </summary>
	public sealed class ValuesDataHelper
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
		public ValuesDataHelper()
		{
			StandardSettings = new StandardUISettings();
      var fileName = "DataHelper.exe.config";
      if (File.Exists(fileName))
      {
        SetConfigFile(fileName);
      }
    }

    /// <summary>Configures the settings.</summary>
    /// <param name="fileName">The config file name.</param>
    public void SetConfigFile(string fileName)
    {
      if (NetString.HasValue(fileName))
      {
        // No config file set or new file name.
        if (false == NetString.HasValue(ConfigFileName)
          || fileName.Trim().ToLower() != ConfigFileName.ToLower())
        {
          ConfigFileName = fileName.Trim();
          StandardSettings.SetProperties(fileName);
        }
      }
    }
    #endregion

    #region Properties

    /// <summary>Gets the ConfigFile name.</summary>
    public string ConfigFileName { get; private set; }

    /// <summary>Gets the StandardSettings value.</summary>
    public StandardUISettings StandardSettings { get; private set; }

    /// <summary>Gets the singleton instance.</summary>
    public static ValuesDataHelper Instance
    {
      get { return mInstance; }
    }
    #endregion

    #region Class Data

    // The singleton instance.
    private static readonly ValuesDataHelper mInstance
      = new ValuesDataHelper();
    #endregion
  }
}
