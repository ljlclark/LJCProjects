// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDataConfig5.cs
using LJCNetCommon5;
using System.Data.Common;

namespace LJCDBConfig5
{
  // Represents a data location configuration.
  /// <include file='Doc/ProjectDataConfig.xml'
  ///  path='members/LJCDataConfig/*'/>
  public class LJCDataConfig : IComparable<LJCDataConfig>
  {
    #region Static Methods

    // Retrieves the provider name value. 
    /// <include file='Doc/DataConfig.xml'
    ///  path='members/ProviderName/*'/>
    public static string ProviderName(string? connectionType = "SQLServer")
    {
      string retName = "Microsoft.Data.SqlClient";

      if (connectionType != null)
      {
        if (LJCNetString.IsEqual(connectionType, "Access"))
        {
          retName = "Microsoft.Jet.OLEDB.4.0";
        }
        if (LJCNetString.IsEqual(connectionType, "ODBC"))
        {
          retName = "MSDASQL.1";
        }
        if (LJCNetString.IsEqual(connectionType, "OleDB"))
        {
          retName = "SQLOLEDB";
        }
        if (LJCNetString.IsEqual(connectionType, "MySQL"))
        {
          retName = "MySql.Data.MySqlClient";
        }
        if (LJCNetString.IsEqual(connectionType, "SQLServer"))
        {
          retName = "Microsoft.Data.SqlClient";
        }
      }
      return retName;
    }
    #endregion

    #region Properties

    // Gets or sets the DataConfig name.
    /// <include file='Doc/DataConfig.xml'
    ///  path='members/Name/*'/>
    public string? Name
    {
      get => mName;
      set => mName = value?.Trim();
    }
    private string? mName;

    // Gets or sets the DbServer instance name.
    /// <include file='Doc/DataConfig.xml'
    ///  path='mmebers/DbServer/*'/>
    public string? DbServer
    {
      get => mDbServer;
      set => mDbServer = value?.Trim();
    }
    private string? mDbServer;

    // Gets or sets the Database name.
    /// <include file='Doc/DataConfig.xml'
    ///  path='members/Database/*'/>
    public string? Database
    {
      get => mDatabase;
      set => mDatabase = value?.Trim();
    }
    private string? mDatabase;

    // Gets or sets the ConnectionType name.
    /// <include file='Doc/DataConfig.xml'
    ///  path='members/ConnectionType/*'/>
    public string? ConnectionType
    {
      get => mConnectionType;
      set => mConnectionType = value?.Trim();
    }
    private string? mConnectionType;

    // Gets or sets the UserID name.
    /// <include file='Doc/DataConfig.xml'
    ///  path='members/UserID/*'/>
    public string? UserID
    {
      get => mUserID;
      set => mUserID = value?.Trim();
    }
    private string? mUserID;

    // Gets or sets the Pswd name.
    /// <include file='Doc/DataConfig.xml'
    ///  path='members/Pswd/*'/>
    public string? Pswd
    {
      get => mPswd;
      set => mPswd = value?.Trim();
    }
    private string? mPswd;
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/DataConfig.xml'
    ///  path='members/Constructor/*'/>
    public LJCDataConfig()
    {
    }

    // Initializes an object instance with the supplied values.
    /// <include file='Doc/DataConfig.xml'
    ///  path='members/ConstructorParam/*'/>
    public LJCDataConfig(string? connectionType = null) : this()
    {
      if (LJC.HasText(connectionType))
      {
        ConnectionType = connectionType;
      }
    }
    #endregion

    #region Data Class Methods

    // The object string value.
    /// <include file='Doc/DataConfig.xml'
    ///  path='members/ToString/*'/>
    public override string? ToString()
    {
      return mName;
    }

    /// <include file='Doc/DataConfig.xml'
    ///  path='members/CompareTo/*'/>
    public int CompareTo(LJCDataConfig? other)
    {
      int retValue;

      if (null == other)
      {
        // This object is greater than the "other" object.
        retValue = LJCNetString.CompareGreater;
      }
      else
      {
        retValue = LJC.CompareNull(Name, other.Name);
        if (LJCNetString.CompareNotNullOrEqual == retValue)
        {
          // Case sensitive.
          retValue = Name!.CompareTo(other.Name);
        }
      }
      return retValue;
    }
    #endregion

    #region Public Methods

    /// <include file='Doc/DataConfig.xml'
    ///  path='members/ConnectionString/*'/>
    public string? ConnectionString(string? connectionType = "SQLServer"
      , bool trusted = false)
    {
      string? retValue;

      LJCConnectionTemplates connectionTemplates = [];
      connectionTemplates.LoadData();
      LJCConnectionTemplate? connectionTemplate;
      connectionTemplate = connectionTemplates.Retrieve(connectionType);
      retValue = ConnectionStringFromTemplate(connectionTemplate?.Template
        , trusted);
      return retValue;
    }

    // Creates the populated connection string from the template text.
    /// <include file='Doc/DataConfig.xml'
    ///  path='members/ConnectionStringFromTemplate/*'/>
    public string? ConnectionStringFromTemplate(string? templateText
      , bool trusted = false)
    {
      DbConnectionStringBuilder connectionBuilder;
      string? replacementValue;
      string? retValue = null;

      if (LJC.HasText(templateText))
      {
        string[] items = templateText.Split([';']
          , StringSplitOptions.RemoveEmptyEntries);

        connectionBuilder = [];
        foreach (string item in items)
        {
          string[] values = item.Split(['=']
            , StringSplitOptions.RemoveEmptyEntries);
          if (2 == values.Length)
          {
            string keyword = values[0].Trim();
            string value = values[1];

            var textParser = new LJCTextParser();
            string? marker = textParser.DelimitedString(value, "{", "}");

            if (marker != null)
            {
              replacementValue = ReplacementValue(marker);
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
        if (trusted)
        {
          retValue += ";TrustServerCertificate=True;";
        }
      }
      return retValue;
    }

    // Creates the SQL integrated connection string from an internal value.
    /// <include file='Doc/DataConfig.xml'
    ///  path='items/SQLIntegratedConnectionString/*'/>
    public string? SQLIntegratedConnectionString(bool trusted = false)
    {
      string? retValue;

      string connectionText = "Data Source={DbServer}; Initial Catalog={Database}; "
        + "Integrated Security=True";
      retValue = ConnectionStringFromTemplate(connectionText, trusted);
      return retValue;
    }
    #endregion

    #region Private Methods

    // Get the replacement value for the specified marker.
    private string? ReplacementValue(string marker)
    {
      string? retValue = null;

      switch (marker.ToLower())
      {
        case "dbserver":
          if (LJC.HasText(DbServer))
          {
            retValue = DbServer;
          }
          break;

        case "database":
          if (LJC.HasText(Database))
          {
            retValue = Database;
          }
          break;

        case "uid":
          if (LJC.HasText(UserID))
          {
            retValue = UserID;
          }
          break;

        case "pswd":
          if (LJC.HasText(Pswd))
          {
            retValue = Pswd;
          }
          break;
      }
      return retValue;
    }
    #endregion
  }
}
