// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ForeignKeys.cs
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.Text;

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

      var loadStatement = SQLStatement(Manager.TableName);
      var dbResult = Manager.ExecuteClientSql(RequestType.ExecuteSQL
        , loadStatement);
      retValue = ResultConverter.CreateCollection(dbResult);
      return retValue;
    }

    // Gets the SQL Retrieve statement.
    private string SQLStatement(string tableName)
    {
      StringBuilder b = new StringBuilder(256);
      b.AppendLine("SELECT");
      b.AppendLine(" [Constraint_Column_Usage].[TABLE_CATALOG] as DBName");
      b.AppendLine(" , [Constraint_Column_Usage].[TABLE_SCHEMA] as TableSchema");
      b.AppendLine(" , [Constraint_Column_Usage].[TABLE_NAME] as TableName");
      b.AppendLine(" , [Constraint_Column_Usage].[COLUMN_NAME] as ColumnName");
      b.AppendLine(" , [Constraint_Column_Usage].[CONSTRAINT_CATALOG] as ConstraintDBName");
      b.AppendLine(" , [Constraint_Column_Usage].[CONSTRAINT_SCHEMA] as ConstrainsSchema");
      b.AppendLine(" , [Constraint_Column_Usage].[CONSTRAINT_NAME] as ConstraintName");
      b.AppendLine(" , [Referential_Constraints].[unique_constraint_name] as UniqueConstraintName");
      b.AppendLine(" , [Referential_Constraints].[update_rule] as UpdateRule");
      b.AppendLine(" , [Referential_Constraints].[delete_rule] as DeleteRule");
      b.AppendLine(" , [Key_Column_Usage].[table_name] as TargetTable");
      b.AppendLine(" , [Key_Column_Usage].[column_name] as TargetColumn");
      b.AppendLine(" , [Key_Column_Usage].[ordinal_position] as OrdinalPosition");
      b.AppendLine("from[Information_Schema].[Constraint_Column_Usage] ");
      b.AppendLine("left join[Information_Schema].[Referential_Constraints]");
      b.AppendLine(" on[Constraint_Column_Usage].[constraint_name]");
      b.AppendLine("  = [Referential_Constraints].[constraint_name] ");
      b.AppendLine("left join[Information_Schema].[Key_Column_Usage]");
      b.AppendLine(" on[Referential_Constraints].[unique_constraint_name]");
      b.AppendLine("  = [Key_Column_Usage].[constraint_name] ");
      b.AppendLine("where([Key_Column_Usage].[column_name] is not null)");
      b.AppendLine($" and([Constraint_Column_Usage].[TABLE_NAME] = '{tableName}'");
      b.AppendLine($"  or [Key_Column_Usage].[table_name] = '{tableName}')");
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
