// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataConfig.cs
using System;
using System.Data.Common;
using System.Security.Cryptography;
using LJCNetCommon;

namespace LJCDataAccessConfig
{
  // Represents a data location configuration.
  /// <include path='items/DataConfig/*' file='Doc/ProjectDataAccessConfig.xml'/>
  public partial class DataConfig : IComparable<DataConfig>
  {
    #region Static Functions

    // Retrieves the provider name value. 
    /// <include path='items/GetProviderName/*' file='Doc/DataConfig.xml'/>
    public static string GetProviderName(string connectionTypeName)
    {
      string retVal = "System.Data.SqlClient";

      if (connectionTypeName != null)
      {
        if (NetString.IsEqual(connectionTypeName, "OleDB"))
        {
          retVal = "SQLOLEDB";
        }
        if (NetString.IsEqual(connectionTypeName, "ODBC"))
        {
          retVal = "MSDASQL.1";
        }
        if (NetString.IsEqual(connectionTypeName, "SQLServer"))
        {
          retVal = "System.Data.SqlClient";
        }
        if (NetString.IsEqual(connectionTypeName, "MySQL"))
        {
          retVal = "MySql.Data.MySqlClient";
        }
        if (NetString.IsEqual(connectionTypeName, "Access"))
        {
          retVal = "Microsoft.Jet.OLEDB.4.0";
        }
      }
      return retVal;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DataConfigC/*' file='Doc/DataConfig.xml'/>
    public DataConfig()
    {
      G = SymmetricAlgorithm.Create("Rijndael");
    }
    #endregion

    #region Public Methods

    // The object string identifier.
    /// <include path='items/ToString/*' file='Doc/DataConfig.xml'/>
    public override string ToString()
    {
      return mName;
    }

    // Retrieves the provider name value.
    /// <include path='items/GetProviderName2/*' file='Doc/DataConfig.xml'/>
    public string GetProviderName()
    {
      return GetProviderName(ConnectionTypeName);
    }

    // Creates the populated connection string.
    /// <include path='items/GetConnectionString1/*' file='Doc/DataConfig.xml'/>
    public string GetConnectionString()
    {
      return GetConnectionString(ConnectionTypeName);
    }

    // Creates the populated connection string from the ConnectionType name.
    /// <include path='items/GetConnectionString2/*' file='Doc/DataConfig.xml'/>
    public string GetConnectionString(string connectionTypeName)
    {
      ConnectionTemplates connectionTemplates;
      ConnectionTemplate connectionTemplate;
      string retValue;

      connectionTemplates = new ConnectionTemplates();
      connectionTemplates.LJCLoadData();
      connectionTemplate = connectionTemplates.LJCGetByName(connectionTypeName);
      retValue = GetConnectionStringFromText(connectionTemplate.Template);
      return retValue;
    }

    // Creates the populated connection string from the template text.
    /// <include path='items/GetConnectionStringFromText/*' file='Doc/DataConfig.xml'/>
    public string GetConnectionStringFromText(string templateText)
    {
      DbConnectionStringBuilder connectionBuilder;
      string replacementValue;
      string retValue;

      string[] items = templateText.Split(new char[] { ';' }
        , StringSplitOptions.RemoveEmptyEntries);

      connectionBuilder = new DbConnectionStringBuilder();
      foreach (string item in items)
      {
        string[] values = item.Split(new char[] { '=' }
          , StringSplitOptions.RemoveEmptyEntries);
        if (2 == values.Length)
        {
          string keyword = values[0].Trim();
          string value = values[1];
          int startIndex = 0;
          string marker = NetString.GetDelimitedString(value, "{"
            , ref startIndex, "}");
          if (marker != null)
          {
            replacementValue = GetReplacementValue(marker);
            if (replacementValue != null)
            {
              connectionBuilder.Add(keyword, replacementValue);
            }
          }
          else
          {
            connectionBuilder.Add(keyword, value);
          }
        }
      }
      retValue = connectionBuilder.ToString();
      return retValue;
    }

    // Creates the SQL integrated connection string from an internal value.
    /// <include path='items/SQLIntegratedConnectionString/*' file='Doc/DataConfig.xml'/>
    public string SQLIntegratedConnectionString()
    {
      string retValue;

      string connectionText = "Data Source={DbServer}; Initial Catalog={Database}; "
        + "Integrated Security=True";
      retValue = GetConnectionStringFromText(connectionText);
      return retValue;
    }
    #endregion

    #region Private Methods

    // Get the replacement value for the specified marker.
    private string GetReplacementValue(string marker)
    {
      string retValue = null;

      switch (marker.ToLower())
      {
        case "dbserver":
          if (NetString.HasValue(DbServer))
          {
            retValue = DbServer;
          }
          break;

        case "database":
          if (NetString.HasValue(Database))
          {
            retValue = Database;
          }
          break;

        case "uid":
          if (NetString.HasValue(UserID))
          {
            retValue = UserID;
          }
          break;

        case "pswd":
          if (NetString.HasValue(Pswd))
          {
            retValue = Pswd;
          }
          break;
      }
      return retValue;
    }
    #endregion

    #region IComparable Methods

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='Doc/DataConfig.xml'/>
    public int CompareTo(DataConfig other)
    {
      int retValue;

      if (null == other)
      {
        retValue = 1;
      }
      else
      {
        // Case sensitive.
        //retValue = Name.CompareTo(other.Name);

        // Not case sensitive.
        retValue = string.Compare(Name, other.Name, true);
      }
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the DataConfig name.</summary>
    public string Name
    {
      get { return mName; }
      set { mName = NetString.InitString(value); }
    }
    private string mName;

    /// <summary>Gets or sets the DbServer instance name.</summary>
    public string DbServer
    {
      get { return mDbServer; }
      set { mDbServer = NetString.InitString(value); }
    }
    private string mDbServer;

    /// <summary>Gets or sets the Database name.</summary>
    public string Database
    {
      get { return mDatabase; }
      set { mDatabase = NetString.InitString(value); }
    }
    private string mDatabase;

    /// <summary>Gets or sets the ConnectionType value.</summary>
    public string ConnectionTypeName
    {
      get { return mConnectionTypeName; }
      set { mConnectionTypeName = NetString.InitString(value); }
    }
    private string mConnectionTypeName;

    /// <summary>Gets or sets the User ID.</summary>
    public string UserID
    {
      get { return mUserID; }
      set { mUserID = NetString.InitString(value); }
    }
    private string mUserID;

    /// <summary>Gets or sets the Password.</summary>
    public string Pswd
    {
      get { return mPswd; }
      set { mPswd = NetString.InitString(value); }
    }
    private string mPswd;
    #endregion
  }
}
