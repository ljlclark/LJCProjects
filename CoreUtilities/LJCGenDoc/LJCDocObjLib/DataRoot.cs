// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataRoot.cs
using LJCGenDocDAL;
using LJCNetCommon;
using System;
using System.IO;

namespace LJCDocObjLib
{
  // Represents the Root documentation data.
  /// <include path='items/DataRoot/*' file='Doc/DataRoot.xml'/>
  public class DataRoot
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DataRootC/*' file='Doc/DataRoot.xml'/>
    //public DataRoot(DocGenGroups assemblyGroups)
    public DataRoot(DocAssemblyGroups assemblyGroups
      , string configFileName = null)
    {
      AssemblyGroups = assemblyGroups;
      var configValues = ValuesGenDoc.Instance;
      SetConfig(configValues, configFileName);
      Managers = ValuesGenDoc.Instance.Managers;
      File.WriteAllText("LJCDocObjLib.log", "");
      CreateAssembliesData();
    }

    // Sets the config file name.
    private static void SetConfig(ValuesGenDoc configValues
      , string configFileName)
    {
      var logName = "LJCDocObjLib.Log";
      try
      {
        if (NetString.HasValue(configFileName))
        {
          configValues.SetConfigFile(configFileName);
        }
        else
        {
          configValues.SetConfigFile();
        }
        var errors = configValues.Errors;
        if (NetString.HasValue(errors))
        {
          var fileName = configValues.FileSpec;
          var message = $"ConfigError: {fileName} - {errors}";
          File.WriteAllText(logName, message);
          Exception ex = new Exception(message);
          throw ex;
        }
      }
      catch (Exception ex)
      {
        var fileName = configValues.FileSpec;
        var message = $"ConfigError: {fileName} - {ex.Message}";
        File.WriteAllText(logName, message);
        throw ex;
      }
    }
    #endregion

    #region Methods

    // Creates the assembly data.
    /// <include path='items/CreateAssembliesData/*' file='Doc/DataRoot.xml'/>
    private void CreateAssembliesData()
    {
      bool success;

      // Process each DocAssemblyGroup.
      DataAssemblies = new DataAssemblies();
      var assemblyManager = Managers.DocAssemblyManager;
      foreach (DocAssemblyGroup assemblyGroup in AssemblyGroups)
      {
        // Process each group DocAssembly.
        var groupAssemblies = assemblyManager.LoadWithParentID(assemblyGroup.ID);
        foreach (DocAssembly groupAssembly in groupAssemblies)
        {
          // Check if assembly file is HTML.
          // *** Next Statement *** Change 1/31/25
          success = true;
          bool isHtml = ".html" == Path.GetExtension(groupAssembly.FileSpec).ToLower();
          if (!isHtml)
          {
            // Check if assembly XML file exists.
            if (!File.Exists(groupAssembly.FileSpec))
            {
              success = false;
              string errorText = $"{DateTime.Now} - File"
                + $" '{groupAssembly.FileSpec}' was not found.\r\n";
              File.AppendAllText("LJCDocObjLib.log", errorText);
            }
          }

          if (success)
          {
            // Create the DataAssembly data.
            DataAssembly dataAssembly = new DataAssembly(this
              , groupAssembly.FileSpec, groupAssembly.Description
              , groupAssembly.MainImage);
            if (dataAssembly.Name != null)
            {
              DataAssemblies.Add(dataAssembly);
            }
          }
        }
      }
    }
    #endregion

    #region Data Properties

    /// <summary>Gets or sets the DataAssemblies list.</summary>
    public DataAssemblies DataAssemblies { get; set; }
    #endregion

    #region Class Properties

    /// <summary>Gets or sets the AssemblyGroups list.</summary>
    public DocAssemblyGroups AssemblyGroups { get; set; }

    /// <summary></summary>
    public ManagersGenDoc Managers { get; set; }
    #endregion
  }
}
