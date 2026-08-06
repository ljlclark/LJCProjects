// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDataConfigs.cs
using LJCNetCommon5;
using System.Reflection;
using System.Xml.Serialization;

namespace LJCDataAccessConfig5
{
  // Represents a collection of LJCDataConfig objects.
  /// <include file='Doc/LJCDataConfigs.xml'
  ///  path='members/LJCDataConfigs/*'/>
  [XmlRoot("LJCDataConfigs")]
  public class LJCDataConfigs : List<LJCDataConfig>
  {
    #region Static Methods

    // Gets a DataConfig from the DataConfigs.xml file.
    /// <include file='Doc/LJCDataConfigs.xml'
    ///  path='members/DataConfig/*'/>
    public static LJCDataConfig? DataConfig(string configName)
    {
      LJCDataConfig retConfig = null;

      if (LJC.HasText(configName))
      {
        var dataConfigs = new LJCDataConfigs();
        dataConfigs.LoadData();
        retConfig = dataConfigs.Retrieve(configName);
      }
      return retConfig;
    }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/LJCDataConfigs.xml'
    ///  path='members/Constructor/*'/>
    public LJCDataConfigs()
    {
      mConfigFileName = "DataConfigs.xml";
      string localAssembly = Assembly.GetExecutingAssembly().Location;
#pragma warning disable CS8604 // Possible null reference argument.
      ConfigFileSpec = Path.Combine(Path.GetDirectoryName(localAssembly)
        , mConfigFileName);
#pragma warning restore CS8604 // Possible null reference argument.
    }

    // Retrieves the config data.
    /// <include file='Doc/LJCDataConfigs.xml'
    ///  path='members/LoadData/*'/>
    public void LoadData()
    {
      if (!File.Exists(ConfigFileSpec))
      {
        WriteDefaultData();
      }

      if (LJC.XmlDeserialize(typeof(LJCDataConfigs)
        , ConfigFileSpec) is LJCDataConfigs dataConfigs)
      {
        Clear();
        foreach (LJCDataConfig dataConfig in dataConfigs)
        {
          Add(dataConfig);
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and adds the object from the provided valus.
    /// <include file='Doc/LJCDataConfigs.xml'
    ///  path='members/Add/*'/>
    public LJCDataConfig Add(string name, string dbServer, string database
      , string connectionType)
    {
      var retValue = new LJCDataConfig(connectionType)
      {
        Name = name,
        DbServer = dbServer,
        Database = database,
        ConnectionType = connectionType,
      };
      Add(retValue);
      return retValue;
    }

    // Retrieve the data configuration.
    /// <include file='Doc/LJCDataConfigs.xml'
    ///  path='members/Retrieve/*'/>
    public LJCDataConfig Retrieve(string name)
    {
      LJCDataConfig retValue;

      if (Count != mPrevCount)
      {
        mPrevCount = Count;
        Sort();
      }

      var dataConfig = new LJCDataConfig(ConnectionType)
      {
        Name = name,
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
    /// <include file='Doc/LJCDataConfigs.xml'
    ///  path='members/Save/*'/>
    public void Save()
    {
      LJC.XmlSerialize(this.GetType(), this, null, ConfigFileSpec);
    }
    #endregion

    #region Private Methods

    // Create the default data file.
    private void WriteDefaultData()
    {
      var tb = new LJCTextBuilder();
      tb.AddLine("<?xml version='1.0'?>");
      tb.AddLine("<LJCDataConfigs xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"");
      tb.AddLine("  xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">");
      tb.AddLine("  <LJCDataConfig>");
      tb.AddLine("    <Name>DataConfig</Name>");
      tb.AddLine("    <DbServer>Machine_Name\\SQL_Instance_Name</DbServer>");
      tb.AddLine("    <Database>Database_Name</Database>");
      tb.AddLine("    <ID>User_ID</ID>");
      tb.AddLine("    <Pswd>Password</Pswd>");
      tb.AddLine("    <ConnectionType>SQLServer</ConnectionType>");
      tb.AddLine("  </LJCDataConfig>");
      tb.AddLine("</LJCDataConfigs>");
      var dataConfigs = tb.ToString();
      if (LJC.HasText(ConfigFileSpec))
      {
        File.WriteAllText(ConfigFileSpec, dataConfigs);
      }
    }
    #endregion

    #region Properties

    // Gets or sets the ConnectionType name.
    /// <include file='Doc/LJCDataConfigs.xml'
    ///  path='members/ConnectionType/*'/>
    public string? ConnectionType
    {
      get { return mConnectionType; }
      set { mConnectionType = value?.Trim(); }
    }
    private string? mConnectionType;

    // The configuration file path.
    /// <include file='Doc/LJCDataConfigs.xml'
    ///  path='members/ConfigFileSpec/*'/>
    public string ConfigFileSpec { get; set; }
    #endregion

    #region Class Data

    private int mPrevCount;
    private readonly string mConfigFileName;
    #endregion
  }
}
