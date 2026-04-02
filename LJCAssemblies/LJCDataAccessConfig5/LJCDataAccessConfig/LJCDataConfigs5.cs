// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDataConfigs5.cs
using LJCNetCommon5;
using System.Reflection;

namespace LJCDataAccessConfig5
{
  // Represents a collection of LJCDataConfig objects.
  /// <include path="members/LJCDataConfigs/*" file="Doc/LJCDataConfigs.xml"/>
  public class LJCDataConfigs5 : List<LJCDataConfig5>
  {
    #region Static Methods

    // Gets a DataConfig from the DataConfigs.xml file.
    /// <include path="members/DataConfig/*" file="Doc/LJCDataConfigs.xml"/>
    public static LJCDataConfig5? DataConfig(string configName)
    {
      LJCDataConfig5 retConfig = null;

      if (LJC5.HasValue(configName))
      {
        var dataConfigs = new LJCDataConfigs5();
        dataConfigs.LoadData();
        retConfig = dataConfigs.Retrieve(configName);
      }
      return retConfig;
    }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include path="members/Constructor/*" file="Doc/LJCDataConfigs.xml"/>
    public LJCDataConfigs5()
    {
      mConfigFileName = "DataConfigs.xml";
      string localAssembly = Assembly.GetExecutingAssembly().Location;
#pragma warning disable CS8604 // Possible null reference argument.
      ConfigFileSpec = Path.Combine(Path.GetDirectoryName(localAssembly)
        , mConfigFileName);
#pragma warning restore CS8604 // Possible null reference argument.
    }

    // Retrieves the config data.
    /// <include path="members/LoadData/*" file="Doc/LJCDataConfigs.xml"/>
    public void LoadData()
    {
      if (!File.Exists(ConfigFileSpec))
      {
        WriteDefaultData();
      }

      if (LJC5.XmlDeserialize(typeof(LJCDataConfigs5)
        , ConfigFileSpec) is LJCDataConfigs5 dataConfigs)
      {
        Clear();
        foreach (LJCDataConfig5 dataConfig in dataConfigs)
        {
          Add(dataConfig);
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and adds the object from the provided valus.
    /// <include path="members/Add/*" file="Doc/LJCDataConfigs.xml"/>
    public LJCDataConfig5 Add(string name, string dbServer, string database
      , string connectionType)
    {
      var retValue = new LJCDataConfig5(connectionType)
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
    /// <include path="members/Retrieve/*" file="Doc/LJCDataConfigs.xml"/>
    public LJCDataConfig5 Retrieve(string name)
    {
      LJCDataConfig5 retValue;

      if (Count != mPrevCount)
      {
        mPrevCount = Count;
        Sort();
      }

      var dataConfig = new LJCDataConfig5(ConnectionType)
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
    /// <include path="members/Save/*" file="Doc/LJCDataConfigs.xml"/>
    public void Save()
    {
      LJC5.XmlSerialize(this.GetType(), this, null, ConfigFileSpec);
    }
    #endregion

    #region Private Methods

    // Create the default data file.
    private void WriteDefaultData()
    {
      var tb = new LJCTextBuilder5();
      tb.AddLine("<?xml version='1.0'?>");
      tb.AddLine("<DataConfigs xmlns: xsi\"http://www.w3.org/2001/XMLSchema-instance\"");
      tb.AddLine(" xmlns: xsd=\"http://www.w3.org/2001/XMLSchema\">");
      tb.AddLine("  <DataConfig>");
      tb.AddLine("    <Name>PersonData</Name>");
      tb.AddLine("    <DbServer>Machine_Name\\SQL_Instance_Name</DbServer>");
      tb.AddLine("    <Database>Database_Name</Database>");
      tb.AddLine("    <ID>User_ID</ID>");
      tb.AddLine("    <Pswd>Password</Pswd>");
      tb.AddLine("    <ConnectionType>SQLServer</ConnectionType>");
      tb.AddLine("  </DataConfig>");
      tb.AddLine("</DataConfigs>");
      var dataConfigs = tb.ToString().Split('\n');
      if (LJC5.HasValue(ConfigFileSpec))
      {
        File.WriteAllLines(ConfigFileSpec, dataConfigs);
      }
    }
    #endregion

    #region Properties

    // Gets or sets the ConnectionType name.
    /// <include path="members/ConnectionType/*" file="Doc/LJCDataConfigs.xml"/>
    public string? ConnectionType
    {
      get { return mConnectionType; }
      set { mConnectionType = value?.Trim(); }
    }
    private string? mConnectionType;

    // The configuration file path.
    /// <include path="members/ConfigFileSpec/*" file="Doc/LJCDataConfigs.xml"/>
    public string ConfigFileSpec { get; set; }
    #endregion

    #region Class Data

    private int mPrevCount;
    private readonly string mConfigFileName;
    #endregion
  }
}
