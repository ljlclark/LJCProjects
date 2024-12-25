// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ForeignKeys.cs
using LJCDataAccess;
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml.Linq;

namespace LJCDataUtilityDAL
{
  /// <summary>Provides table specific data methods.</summary>
  public class ForeignKeyManager
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DataManagerC/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public ForeignKeyManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "", string schemaName = null)
    {
      Manager = new DataManager(dbServiceRef, dataConfigName, tableName
        , schemaName);
      ResultConverter = new ResultConverter<ForeignKey, ForeignKeys>();
    }
    #endregion

    #region Data Methods

    // Retrieves a collection of data records.
    /// <include path='items/Load/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public ForeignKeys Load(List<string> propertyNames = null)
    {
      ForeignKeys retValue;

      //var sql = CreateProcedure(Manager.TableName);
      //var dbResult = Manager.ExecuteClientSql(RequestType.ExecuteSQL, sql);

      var parms = new ProcedureParameters();
      var parm = new ProcedureParameter
      {
        ParameterName = "@TableName",
        SqlDbType = SqlDbType.NVarChar,
        Size = 20,
        Value = Manager.TableName
      };
      parms.Add(parm);

      var headSql = "USE[LJCDataUtility] \r\n";
      headSql += "SET ANSI_NULLS ON \r\n";
      headSql += "SET QUOTED_IDENTIFIER ON \r\n";
      var result = Manager.ExecuteClientSql(RequestType.ExecuteSQL, headSql);

      var dbResult = Manager.LoadProcedure("sp_GetForeignKeys", parms);
      retValue = ResultConverter.CreateCollection(dbResult);
      return retValue;
    }

    // Gets the SQL Retrieve statement.
    private string CreateProcedure(string tableName)
    {
      StringBuilder b = new StringBuilder(256);
      b.AppendLine("IF OBJECT_ID('[dbo].[sp_GetForeignKeys]', N'p')");
      b.AppendLine(" IS NULL");
      b.AppendLine("CREATE PROCEDURE[dbo].[sp_GetForeignKeys]");
      b.AppendLine("  @TableName nvarchar(20)");
      b.AppendLine("AS");
      b.AppendLine("BEGIN");
      b.AppendLine("SELECT");
      b.AppendLine(" ccu.[TABLE_CATALOG] as DBName, ");
      b.AppendLine(" ccu.[TABLE_SCHEMA] as TableSchema, ");
      b.AppendLine(" ccu.[TABLE_NAME] as TableName, ");
      b.AppendLine(" ccu.[COLUMN_NAME] as ColumnName, ");
      b.AppendLine(" ccu.[CONSTRAINT_CATALOG] as ConstraintDBName, ");
      b.AppendLine(" ccu.[CONSTRAINT_SCHEMA] as ConstrainsSchema, ");
      b.AppendLine(" ccu.[CONSTRAINT_NAME] as ConstraintName, ");
      b.AppendLine(" rc.[unique_constraint_name] as UniqueConstraintName, ");
      b.AppendLine(" rc.[update_rule] as UpdateRule, ");
      b.AppendLine(" rc.[delete_rule] as DeleteRule, ");
      b.AppendLine(" kcu.[table_name] as TargetTable, ");
      b.AppendLine(" kcu.[column_name] as TargetColumn, ");
      b.AppendLine(" kcu.[ordinal_position] as OrdinalPosition ");
      b.AppendLine("from [Information_Schema].[Constraint_Column_Usage] as ccu ");
      b.AppendLine("left join[Information_Schema].[Referential_Constraints] as rc ");
      b.AppendLine(" on ccu.[constraint_name] ");
      b.AppendLine("  = rc.[constraint_name] ");
      b.AppendLine("left join[Information_Schema].[Key_Column_Usage] as kcu ");
      b.AppendLine(" on rc.[unique_constraint_name] ");
      b.AppendLine("  = kcu.[constraint_name] ");
      b.AppendLine("where (kcu.[column_name] is not null ");
      b.AppendLine($" and (ccu.[TABLE_NAME] = '{tableName}' ");
      b.AppendLine($"  or kcu.[table_name] = '{tableName}')) ");
      b.AppendLine("END");
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
    public ResultConverter<ForeignKey, ForeignKeys> ResultConverter { get; set; }
    #endregion
  }
}
