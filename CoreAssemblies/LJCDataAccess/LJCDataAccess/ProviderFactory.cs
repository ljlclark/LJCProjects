// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProviderFactory.cs
using System;
using System.Data;
using System.Data.Common;
using System.Text;
using LJCNetCommon;

namespace LJCDataAccess
{
  // Implements a data provider factory.
  /// <include file='Doc/ProviderFactory.xml'
  ///  path='items/ProviderFactory/*'/>
  public class ProviderFactory
  {
    #region Constructors

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/DefaultConstructor/*'/>
    public ProviderFactory()
    {
    }

    // Initializes an object instance with the provided values,
    /// <include file='Doc/ProviderFactory.xml'
    ///  path='items/ProviderFactoryC/*'/>
    public ProviderFactory(string connectionString, string providerName)
    {
      ConnectionString = connectionString;
      ProviderName = providerName;
      CreateDbProviderFactory();
    }
    #endregion

    #region Public Methods

    // Closes the database connection.
    /// <include file='Doc/ProviderFactory.xml'
    ///  path='items/CloseConnection/*'/>
    public void CloseConnection()
    {
      mDbConnection?.Close();
    }

    // Retrieves the DbCommand object.
    /// <include file='Doc/ProviderFactory.xml'
    ///  path='items/CreateCommand/*'/>
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
    /// <include file='Doc/ProviderFactory.xml'
    ///  path='items/CreateConnection/*'/>
    public DbConnection CreateConnection()
    {
      DbConnection retValue = null;

      if (!NetString.HasValue(ConnectionString))
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
    /// <include file='Doc/ProviderFactory.xml'
    ///  path='items/CreateDataAdapter/*'/>
    public DbDataAdapter CreateDataAdapter()
    {
      return DbProviderFactory.CreateDataAdapter();
    }

    // Opens the database connection.
    /// <include file='Doc/ProviderFactory.xml'
    ///  path='items/OpenConnection/*'/>
    public void OpenConnection()
    {
      mDbConnection.Open();
    }
    #endregion

    #region Private Methods

    // Creates the DbProviderFactory object.
    /// <include file='Doc/ProviderFactory.xml'
    ///  path='items/CreateDbProviderFactory/*'/>
    private void CreateDbProviderFactory()
    {
      StringBuilder builder;

      if (!NetString.HasValue(ProviderName))
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
