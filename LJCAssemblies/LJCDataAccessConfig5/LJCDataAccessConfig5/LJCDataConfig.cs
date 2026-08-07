// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDataConfig.cs
using LJCNetCommon5;
using System.Data.Common;

namespace LJCDataAccessConfig5
{
  // Represents a data location configuration.
  /// <include file='Doc/LJCDataConfig.xml'
  ///  path='members/LJCDataConfig/*'/>
  public class LJCDataConfig : IComparable<LJCDataConfig>
  {
    #region Static Methods

    // Retrieves the provider name value. 
    /// <include file='Doc/LJCDataConfig.xml'
    ///  path='members/ProviderName/*'/>
    public static string ProviderName(string? connectionType = "SQLServer")
    {
      //string retVal = "System.Data.SqlClient";
      string retVal = "Microsoft.Data.SqlClient";

      if (connectionType != null)
      {
        if (LJCNetString.IsEqual(connectionType, "Access"))
        {
          retVal = "Microsoft.Jet.OLEDB.4.0";
        }
        if (LJCNetString.IsEqual(connectionType, "ODBC"))
        {
          retVal = "MSDASQL.1";
        }
        if (LJCNetString.IsEqual(connectionType, "OleDB"))
        {
          retVal = "SQLOLEDB";
        }
        if (LJCNetString.IsEqual(connectionType, "MySQL"))
        {
          retVal = "MySql.Data.MySqlClient";
        }
        if (LJCNetString.IsEqual(connectionType, "SQLServer"))
        {
          //retVal = "System.Data.SqlClient";
          retVal = "Microsoft.Data.SqlClient";
        }
      }
      return retVal;
    }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/LJCDataConfig.xml'
    ///  path='members/Constructor/*'/>
    public LJCDataConfig()
    {
    }

    // Initializes an object instance with the supplied values.
    /// <include file='Doc/LJCDataConfig.xml'
    ///  path='members/ConstructorParam/*'/>
    public LJCDataConfig(string? connectionType = null)
    {
      if (LJC.HasText(connectionType))
      {
        ConnectionType = connectionType;
      }
    }
    #endregion

    #region Data Class Methods

    // The object string value.
    /// <include file='Doc/LJCDataConfig.xml'
    ///  path='members/ToString/*'/>
    public override string? ToString()
    {
      return mName;
    }

    /// <include file='Doc/LJCDataConfig.xml'
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

    /// <include file='Doc/LJCDataConfig.xml'
    ///  path='members/ConnectionString/*'/>
    public string? ConnectionString(string? connectionType = "SQLServer")
    {
      LJCConnectionTemplates connectionTemplates;
      LJCConnectionTemplate connectionTemplate;
      string? retValue;

      connectionTemplates = [];
      connectionTemplates.LoadData();
      connectionTemplate = connectionTemplates.Retrieve(connectionType);
      retValue = ConnectionStringFromTemplate(connectionTemplate?.Template);
      return retValue;
    }

    // Creates the populated connection string from the template text.
    /// <include file='Doc/LJCDataConfig.xml'
    ///  path='members/ConnectionStringFromTemplate/*'/>
    public string? ConnectionStringFromTemplate(string? templateText)
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
      }
      return retValue;
    }

    // Creates the SQL integrated connection string from an internal value.
    /// <include file='Doc/LJCDataConfig.xml'
    ///  path='items/SQLIntegratedConnectionString/*'/>
    public string? SQLIntegratedConnectionString()
    {
      string retValue;

      string connectionText = "Data Source={DbServer}; Initial Catalog={Database}; "
        + "Integrated Security=True";
      retValue = ConnectionStringFromTemplate(connectionText);
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

    #region Properties

    // Gets or sets the DataConfig name.
    /// <include file='Doc/LJCDataConfig.xml'
    ///  path='members/Name/*'/>
    public string? Name
    {
      get { return mName; }
      set { mName = value?.Trim(); }
    }
    private string? mName;

    // Gets or sets the DbServer instance name.
    /// <include file='Doc/LJCDataConfig.xml'
    ///  path='mmebers/DbServer/*'/>
    public string? DbServer
    {
      get { return mDbServer; }
      set { mDbServer = value?.Trim(); }
    }
    private string? mDbServer;

    // Gets or sets the Database name.
    /// <include file='Doc/LJCDataConfig.xml'
    ///  path='members/Database/*'/>
    public string? Database
    {
      get { return mDatabase; }
      set { mDatabase = value?.Trim(); }
    }
    private string? mDatabase;

    // Gets or sets the ConnectionType name.
    /// <include file='Doc/LJCDataConfig.xml'
    ///  path='members/ConnectionType/*'/>
    public string? ConnectionType
    {
      get { return mConnectionType; }
      set { mConnectionType = value?.Trim(); }
    }
    private string? mConnectionType;

    // Gets or sets the UserID name.
    /// <include file='Doc/LJCDataConfig.xml'
    ///  path='members/UserID/*'/>
    public string? UserID
    {
      get { return mUserID; }
      set { mUserID = value?.Trim(); }
    }
    private string? mUserID;

    // Gets or sets the Pswd name.
    /// <include file='Doc/LJCDataConfig.xml'
    ///  path='members/Pswd/*'/>
    public string? Pswd
    {
      get { return mPswd; }
      set { mPswd = value?.Trim(); }
    }
    private string? mPswd;
    #endregion
  }
}
