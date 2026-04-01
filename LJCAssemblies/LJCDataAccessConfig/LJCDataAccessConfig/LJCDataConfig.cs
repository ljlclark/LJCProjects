// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDataConfig.cs
using LJCNetCommon;
using System.Data.Common;

namespace LJCDataAccessConfig
{
  // Represents a data location configuration.
  /// <include path="members/LJCDataConfig/*" file="Doc/LJCDataConfig.xml"/>
  public class LJCDataConfig
  {
    #region Static Methods

    // Retrieves the provider name value. 
    /// <include path="members/ProviderName/*" file="Doc/LJCDataConfig.xml"/>
    public static string ProviderName(string connectionType)
    {
      string retVal = "System.Data.SqlClient";

      if (connectionType != null)
      {
        if (LJCNetString.IsEqual(connectionType, "OleDB"))
        {
          retVal = "SQLOLEDB";
        }
        if (LJCNetString.IsEqual(connectionType, "ODBC"))
        {
          retVal = "MSDASQL.1";
        }
        if (LJCNetString.IsEqual(connectionType, "SQLServer"))
        {
          retVal = "System.Data.SqlClient";
        }
        if (LJCNetString.IsEqual(connectionType, "MySQL"))
        {
          retVal = "MySql.Data.MySqlClient";
        }
        if (LJCNetString.IsEqual(connectionType, "Access"))
        {
          retVal = "Microsoft.Jet.OLEDB.4.0";
        }
      }
      return retVal;
    }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include path="members/Constructor/*" file="Doc/LJCDataConfig.xml"/>
    public LJCDataConfig(string? connectionType = null)
    {
      if (LJC.HasValue(connectionType))
      {
        ConnectionType = connectionType;
      }
    }
    #endregion

    #region Data Class Methods

    // The object string value.
    /// <include path="members/ToString/*" file="Doc/LJCDataConfig.xml"/>
    public override string? ToString()
    {
      return mName;
    }
    #endregion

    #region Public Methods

    /// <include path="members/ConnectionString/*" file="Doc/LJCDataConfig.xml"/>
    public string? ConnectionString(string? connectionType)
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
    /// <include path="members/ConnectionStringFromTemplate/*" file="Doc/LJCDataConfig.xml"/>
    public string? ConnectionStringFromTemplate(string? templateText)
    {
      DbConnectionStringBuilder connectionBuilder;
      string? replacementValue;
      string? retValue = null;

      if (LJC.HasValue(templateText))
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
    /// <include path="items/SQLIntegratedConnectionString/*" file="Doc/LJCDataConfig.xml"/>
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
          if (LJC.HasValue(DbServer))
          {
            retValue = DbServer;
          }
          break;

        case "database":
          if (LJC.HasValue(Database))
          {
            retValue = Database;
          }
          break;

        case "uid":
          if (LJC.HasValue(UserID))
          {
            retValue = UserID;
          }
          break;

        case "pswd":
          if (LJC.HasValue(Pswd))
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
    /// <include path="members/Name/*" file="Doc/LJCDataConfig.xml"/>
    public string? Name
    {
      get { return mName; }
      set { mName = value?.Trim(); }
    }
    private string? mName;

    // Gets or sets the DbServer instance name.
    /// <include path="mmebers/DbServer/*" file="Doc/LJCDataConfig.xml"/>
    public string? DbServer
    {
      get { return mDbServer; }
      set { mDbServer = value?.Trim(); }
    }
    private string? mDbServer;

    // Gets or sets the Database name.
    /// <include path="members/Database/*" file="Doc/LJCDataConfig.xml"/>
    public string? Database
    {
      get { return mDatabase; }
      set { mDatabase = value?.Trim(); }
    }
    private string? mDatabase;

    // Gets or sets the ConnectionType name.
    /// <include path="members/ConnectionType/*" file="Doc/LJCDataConfig.xml"/>
    public string? ConnectionType
    {
      get { return mConnectionType; }
      set { mConnectionType = value?.Trim(); }
    }
    private string? mConnectionType;

    // Gets or sets the UserID name.
    /// <include path="members/UserID/*" file="Doc/LJCDataConfig.xml"/>
    public string? UserID
    {
      get { return mUserID; }
      set { mUserID = value?.Trim(); }
    }
    private string? mUserID;

    // Gets or sets the Pswd name.
    /// <include path="members/Pswd/*" file="Doc/LJCDataConfig.xml"/>
    public string? Pswd
    {
      get { return mPswd; }
      set { mPswd = value?.Trim(); }
    }
    private string? mPswd;
    #endregion
  }
}
