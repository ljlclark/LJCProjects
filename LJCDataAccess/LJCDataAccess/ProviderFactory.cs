// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
using System;
using System.Data;
using System.Data.Common;
using System.Text;
using LJCNetCommon;

namespace LJCDataAccess
{
  // Implements a data provider factory.
  /// <include path='items/ProviderFactory/*' file='Doc/ProviderFactory.xml'/>
  public class ProviderFactory
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ProviderFactory()
    {
    }

    // Initializes an object instance with the provided values,
    /// <include path='items/ProviderFactoryC/*' file='Doc/ProviderFactory.xml'/>
    public ProviderFactory(string connectionString, string providerName)
    {
      ConnectionString = connectionString;
      ProviderName = providerName;
      CreateDbProviderFactory();
    }
    #endregion

    #region Public Methods

    // Closes the database connection.
    /// <include path='items/CloseConnection/*' file='Doc/ProviderFactory.xml'/>
    public void CloseConnection()
    {
      if (mDbConnection != null)
      {
        mDbConnection.Close();
      }
    }

    // Retrieves the DbCommand object.
    /// <include path='items/CreateCommand/*' file='Doc/ProviderFactory.xml'/>
    public DbCommand CreateCommand(string commandText
      , CommandType commandType = CommandType.Text)
    {
      string errorText;
      DbCommand retVal;

      CreateConnection();
      if (mDbConnection == null)
      {
        errorText = "The DbConnection object value is not set.";
        throw new MissingFieldException(errorText);
      }
      else
      {
        if (mDbConnection.State != ConnectionState.Closed)
        {
          errorText = "The DbConnection object state is ";
          errorText += $"'{mDbConnection.State}'. It must be 'Closed'.";
          throw new InvalidOperationException(errorText);
        }
        else
        {
          retVal = DbProviderFactory.CreateCommand();
          retVal.Connection = mDbConnection;
          retVal.CommandType = commandType;
          retVal.CommandText = commandText;
        }
      }
      return retVal;
    }

    // Retrieves the DbConnection object.
    /// <include path='items/CreateConnection/*' file='Doc/ProviderFactory.xml'/>
    public DbConnection CreateConnection()
    {
      DbConnection retValue = null;

      if (false == NetString.HasValue(ConnectionString))
      {
        string errorText = "The ProviderFactory.ConnectionString value"
          + " is not set.";
        throw new MissingMemberException(errorText);
      }
      else
      {
        if (mDbConnection == null)
        {
          mDbConnection = DbProviderFactory.CreateConnection();
          mDbConnection.ConnectionString = ConnectionString;
        }
      }
      return retValue;
    }

    // Creates the DbDataAdapter object.
    /// <include path='items/CreateDataAdapter/*' file='Doc/ProviderFactory.xml'/>
    public DbDataAdapter CreateDataAdapter()
    {
      return DbProviderFactory.CreateDataAdapter();
    }

    // Opens the database connection.
    /// <include path='items/OpenConnection/*' file='Doc/ProviderFactory.xml'/>
    public void OpenConnection()
    {
      mDbConnection.Open();
    }
    #endregion

    #region Private Methods

    // Creates the DbProviderFactory object.
    /// <include path='items/CreateDbProviderFactory/*' file='Doc/ProviderFactory.xml'/>
    private void CreateDbProviderFactory()
    {
      StringBuilder builder;

      if (false == NetString.HasValue(ProviderName))
      {
        builder = new StringBuilder(64);
        builder.AppendLine("The Provider name is missing or the");
        builder.Append($"App.config '{ConfigProvider}' key is empty or missing.");
        string errorText = builder.ToString();
        throw new MissingMemberException(errorText);
      }
      else
      {
        try
        {
          DbProviderFactory = DbProviderFactories.GetFactory(ProviderName);
        }
        catch (ArgumentException ex)
        {
          builder = new StringBuilder(64);
          builder.AppendLine("The Provider name may be invalid or the");
          builder.Append($"App.config '{ConfigProvider}' key may be invalid.");
          builder.AppendLine();
          builder.Append(ex.Message);
          string errorText = builder.ToString();
          throw new InvalidOperationException(errorText);
        }
      }
    }
    #endregion

    #region Properties

    /// <summary>Gets the ConnectionString value.</summary>
    public string ConnectionString { get; private set; }

    /// <summary>Gets a reference to the DbProviderFactory object.</summary>
    public DbProviderFactory DbProviderFactory { get; private set; }

    /// <summary>Gets the ProviderName value.</summary>
    public string ProviderName { get; private set; }
    #endregion

    #region Class Data

    //private const string ConfigConnectionString = "DefaultConnectionString";
    private const string ConfigProvider = "DefaultProvider";

    private DbConnection mDbConnection;
    #endregion
  }
}
