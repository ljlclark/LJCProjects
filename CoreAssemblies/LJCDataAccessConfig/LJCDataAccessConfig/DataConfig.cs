// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataConfig.cs
using System;
using System.Data.Common;
using System.Security.Cryptography;
using LJCNetCommon;

namespace LJCDataAccessConfig
{
  // Represents a data location configuration.
  /// <include file='Doc/ProjectDataAccessConfig.xml'
  ///  path='items/DataConfig/*'/>
  public partial class DataConfig : IComparable<DataConfig>
  {
    #region Static Functions

    // Retrieves the provider name value. 
    /// <include file='Doc/DataConfig.xml'
    ///  path='items/GetProviderName/*'/>
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
    /// <include file='Doc/DataConfig.xml'
    ///  path='items/DataConfigC/*'/>
    public DataConfig()
    {
      G = SymmetricAlgorithm.Create("Rijndael");
    }
    #endregion

    #region Public Methods

    // The object string identifier.
    /// <include file='Doc/DataConfig.xml'
    ///  path='items/ToString/*'/>
    public override string ToString()
    {
      return mName;
    }

    // Retrieves the provider name value.
    /// <include file='Doc/DataConfig.xml'
    ///  path='items/GetProviderName2/*'/>
    public string GetProviderName()
    {
      return GetProviderName(ConnectionType);
    }

    // Creates the populated connection string.
    /// <include file='Doc/DataConfig.xml'
    ///  path='items/GetConnectionString1/*'/>
    public string GetConnectionString()
    {
      return GetConnectionString(ConnectionType);
    }

    // Creates the populated connection string from the ConnectionType name.
    /// <include file='Doc/DataConfig.xml'
    ///  path='items/GetConnectionString2/*'/>
    public string GetConnectionString(string connectionType)
    {
      ConnectionTemplates connectionTemplates;
      ConnectionTemplate connectionTemplate;
      string retValue;

      connectionTemplates = new ConnectionTemplates();
      connectionTemplates.LJCLoadData();
      connectionTemplate = connectionTemplates.LJCGetByName(connectionType);
      retValue = GetConnectionStringFromText(connectionTemplate.Template);
      return retValue;
    }

    // Creates the populated connection string from the template text.
    /// <include file='Doc/DataConfig.xml'
    ///  path='items/GetConnectionStringFromText/*'/>
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

          //int startIndex = 0;
          //string marker = NetString.GetDelimitedString(value, "{"
          //  , ref startIndex, "}");
          var textParser = new LJCParser();
          string marker = textParser.DelimitedString(value, "{", "}");

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
    /// <include file='Doc/DataConfig.xml'
    ///  path='items/SQLIntegratedConnectionString/*'/>
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
    /// <include file='Doc/DataConfig.xml'
    ///  path='items/CompareTo/*'/>
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
    public string ConnectionType
    {
      get { return mConnectionType; }
      set { mConnectionType = NetString.InitString(value); }
    }
    private string mConnectionType;

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
