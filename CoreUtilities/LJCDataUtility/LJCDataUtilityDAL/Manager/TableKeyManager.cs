// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// TableKeyManager.cs
using LJCDataAccess;
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Xml.Linq;

namespace LJCDataUtilityDAL
{
  /// <summary>Provides table specific data methods.</summary>
  public class TableKeyManager
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DataManagerC/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public TableKeyManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "", string schemaName = null)
    {
      Manager = new DataManager(dbServiceRef, dataConfigName, tableName
        , schemaName);
      ResultConverter = new ResultConverter<TableKey, TableKeys>();
    }
    #endregion

    #region Data Methods

    // Retrieves a collection of data records.
    /// <include path='items/Load/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public TableKeys LoadForeignKeys()
    {
      //TableKeys retValue;

      ////var sql = "OBJECT_ID('[dbo].[sp_GetForeignKeys]', N'p')";
      ////var dbResult = Manager.ExecuteClientSql(RequestType.ExecuteSQL, sql);
      //CreateForeignKeysProcedure();

      //var parms = new ProcedureParameters();
      //var parm = new ProcedureParameter("@TableName", SqlDbType.NVarChar
      //  , 20, Manager.TableName);
      //parms.Add(parm);

      ////// ?
      ////var headSql = "USE[LJCDataUtility] \r\n";
      ////headSql += "SET ANSI_NULLS ON \r\n";
      ////headSql += "SET QUOTED_IDENTIFIER ON \r\n";
      ////var result = Manager.ExecuteClientSql(RequestType.ExecuteSQL, headSql);

      //var dbResult = Manager.LoadProcedure("sp_GetForeignKeys", parms);
      //retValue = ResultConverter.CreateCollection(dbResult);
      //return retValue;

      var sql = TableKeySql(Manager.TableName, "FOREIGN KEY");
      var dbResult = Manager.ExecuteClientSql(RequestType.LoadSQL, sql);
      var retValue = ResultConverter.CreateCollection(dbResult);
      return retValue;
    }

    // Retrieves a collection of Primary key records.
    public TableKeys LoadTableKeys(string keyType = "PRIMARY KEY"
      , string constraintName = null)
    {
      var sql = TableKeySql(Manager.TableName, keyType, constraintName);
      var dbResult = Manager.ExecuteClientSql(RequestType.LoadSQL, sql);
      var retValue = ResultConverter.CreateCollection(dbResult);
      return retValue;
    }

    // Create the sp_GetForeignKeys procedure.
    private void CreateForeignKeysProcedure()
    {
      var sql = "DROP PROCEDURE IF EXISTS dbo.sp_GetForeignKeys;";
      Manager.ExecuteClientSql(RequestType.ExecuteSQL, sql);

      sql = ForeignKeysProcedureSql(Manager.TableName);
      try
      {
        Manager.ExecuteClientSql(RequestType.ExecuteSQL, sql);
      }
      catch (Exception e)
      {
        var isHandled = false;
        if (e is SqlException)
        {
          // ErrorCode -2146232060
          if (e.Message.StartsWith("There is already an object"))
          {
            isHandled = true;
          }
        }
        if (!isHandled)
        {
          throw e;
        }
      }
    }

    // Gets the table key values SQL.
    private string TableKeySql(string tableName
      , string keyType = "PRIMARY KEY", string constraintName = null)
    {
      TextBuilder b = new TextBuilder(256);
      b.Line("SELECT");
      b.Line(" tc.[TABLE_CATALOG] as DBName, ");
      b.Line(" tc.[TABLE_SCHEMA] as TableSchema, ");
      b.Line(" tc.[TABLE_NAME] as TableName, ");
      b.Line(" tc.[CONSTRAINT_TYPE] as KeyType, ");
      b.Line(" tc.[CONSTRAINT_Name] as ConstraintName, ");
      b.Line(" kcu.[COLUMN_NAME] as ColumnName, ");
      b.Line(" kcu.[ORDINAL_POSITION] as OrdinalPosition ");
      b.Line("FROM [INFORMATION_SCHEMA].[TABLE_CONSTRAINTS] as tc ");
      b.Line("LEFT JOIN [INFORMATION_SCHEMA].[KEY_COLUMN_USAGE] as kcu ");
      b.Line(" ON tc.[CONSTRAINT_NAME] = kcu.[CONSTRAINT_NAME] ");
      if (NetString.HasValue(constraintName))
      {
        b.Line("WHERE tc.[CONSTRAINT_NAME] = constraintName");
      }
      else
      {
        b.Line($"WHERE tc.[TABLE_NAME] = '{tableName}'");
        b.Line($" AND tc.[CONSTRAINT_TYPE] = '{keyType}'");
      }
      b.Line("ORDER BY kcu.[ORDINAL_POSITION];");
      var retValue = b.ToString();
      return retValue;
    }

    // Gets the sp_GetForeignKeys procedure SQL.
    private string ForeignKeysProcedureSql(string tableName)
    {
      TextBuilder b = new TextBuilder(256);
      b.Line("CREATE PROCEDURE[dbo].[sp_GetForeignKeys]");
      b.Line("  @TableName nvarchar(20)");
      b.Line("AS");
      b.Line("BEGIN");
      b.Line("SELECT");
      b.Line(" tc.[TABLE_CATALOG] as DBName, ");
      b.Line(" tc.[TABLE_SCHEMA] as TableSchema, ");
      b.Line(" tc.[TABLE_NAME] as TableName, ");
      b.Line(" tc.[CONSTRAINT_Name] as ConstraintName, ");
      b.Line(" tc.[CONSTRAINT_TYPE] as KeyType, ");
      b.Line(" ccu.[COLUMN_NAME] as ColumnName, ");
      b.Line(" kcu.[ORDINAL_POSITION] as OrdinalPosition, ");
      b.Line(" rc.[UPDATE_RULE] as UpdateRule, ");
      b.Line(" rc.[DELETE_RULE] as DeleteRule ");
      b.Line("from [INFORMATION_SCHEMA].[TABLE_CONSTRAINTS] as tc ");
      b.Line("left join[INFORMATION_SCHEMA].[CONSTRAINT_COLUMN_USAGE] as ccu ");
      b.Line(" on tc.[CONSTRAINT_NAME] = ccu.[CONSTRAINT_NAME] ");
      b.Line("left join[INFORMATION_SCHEMA].[KEY_COLUMN_USAGE] as kcu ");
      b.Line(" on tc.[CONSTRAINT_NAME] = kcu.[CONSTRAINT_NAME] ");
      b.Line("left join[INFORMATION_SCHEMA].[REFERENTIAL_CONSTRAINTS] as rc ");
      b.Line(" on ccu.[CONSTRAINT_NAME] = rc.[CONSTRAINT_NAME] ");
      b.Line($"where tc.[TABLE_NAME] = '{tableName}'");
      b.Line($" and tc.[CONSTRAINT_TYPE] = 'FOREIGN KEY'");
      b.Line("END");
      var retValue = b.ToString();
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets the affected record count.</summary>
    public int AffectedCount
    {
      get { return Manager.AffectedCount; }
    }

    /// <summary>Gets or sets the DataManager reference.</summary>
    public DataManager Manager { get; set; }

    /// <summary>Gets or sets the ResultConverter reference.</summary>
    public ResultConverter<TableKey, TableKeys> ResultConverter { get; set; }
    #endregion
  }
}
