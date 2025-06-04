// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataConfigs.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using LJCNetCommon;

namespace LJCDataAccessConfig
{
  // Represents a collection of DataConfig objects.
  /// <include path='items/DataConfigs/*' file='Doc/DataConfigsDoc.xml'/>
  [XmlRoot("DataConfigs")]
  public class DataConfigs : List<DataConfig>
  {
    #region Static Functions

    /// <summary>Gets a DataConfig from the DataConfigs.xml file.</summary>
    /// <param name="configName">The DataConfig name.</param>
    /// <returns>The DataConfig object.</returns>
    public static DataConfig GetDataConfig(string configName)
    {
      DataConfig retConfig = null;

      if (NetString.HasValue(configName))
      {
        var dataConfigs = new DataConfigs();
        dataConfigs.LJCLoadData();
        retConfig = dataConfigs.LJCGetByName(configName);
      }
      return retConfig;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DataConfigsC/*' file='Doc/DataConfigsDoc.xml'/>
    public DataConfigs()
    {
      mConfigFileName = "DataConfigs.xml";
      string localAssembly = Assembly.GetExecutingAssembly().Location;
      LJCConfigFileSpec = Path.Combine(Path.GetDirectoryName(localAssembly)
        , mConfigFileName);
    }
    #endregion

    #region Public Methods

    // Retrieves the config data.
    /// <include path='items/LJCLoadData/*' file='Doc/DataConfigsDoc.xml'/>
    public void LJCLoadData()
    {
      if (!File.Exists(LJCConfigFileSpec))
      {
        WriteDefaultData();
      }

      if (NetCommon.XmlDeserialize(typeof(DataConfigs)
        , LJCConfigFileSpec) is DataConfigs dataConfigs)
      {
        Clear();
        foreach (DataConfig dataConfig in dataConfigs)
        {
          Add(dataConfig);
        }
      }
    }

    // Creates and adds the object from the provided valus.
    /// <include path='items/Add/*' file='Doc/DataConfigsDoc.xml'/>
    public DataConfig Add(string name, string dbServer, string database)
    {
      DataConfig retValue = new DataConfig
      {
        Name = name,
        //retValue.DbServer = retValue.B(dbServer);
        //retValue.Database = retValue.B(database);
        DbServer = dbServer,
        Database = database
      };
      Add(retValue);
      return retValue;
    }

    // Retrieve the data configuration.
    /// <include path='items/LJCGetByName/*' file='Doc/DataConfigsDoc.xml'/>
    public DataConfig LJCGetByName(string name)
    {
      DataConfig retValue;

      if (Count != mPrevCount)
      {
        mPrevCount = Count;
        Sort();
      }

      DataConfig dataConfig = new DataConfig()
      {
        Name = name
      };
      int index = BinarySearch(dataConfig);
      if (index < 0)
      {
        var errorText = $"Data Configuration '{name}' was not found.";
        throw new Exception(errorText);
      }
      else
      {
        retValue = this[index];
      }
      return retValue;
    }

    // Saves the config data.
    /// <include path='items/LJCSave/*' file='Doc/DataConfigsDoc.xml'/>
    public void LJCSave()
    {
      NetCommon.XmlSerialize(this.GetType(), this, null, LJCConfigFileSpec);
    }
    #endregion

    #region Private Methods

    // Create the default data file.
    private void WriteDefaultData()
    {
      List<string> configs = new List<string>
        {
          "<?xml version='1.0'?>",
           "<DataConfigs xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>",
            "<DataConfig>",
              "<Name>PersonData</Name>",
              "<DbServer>Machine_Name\\SQL_Instance_Name</DbServer>",
              "<Database>Database_Name</Database>",
              "<ID>User_ID</ID>",
              "<Pswd>Password</Pswd>",
              "<ConnectionType>SQLServer</ConnectionType>",
            "</DataConfig>",
          "</DataConfigs>"
        };
      File.WriteAllLines(LJCConfigFileSpec, configs);
    }
    #endregion

    #region Properties

    /// <summary>The configuration file path.</summary>
    public string LJCConfigFileSpec { get; set; }
    #endregion

    #region Class Data

    private int mPrevCount;
    private readonly string mConfigFileName;
    #endregion
  }
}
