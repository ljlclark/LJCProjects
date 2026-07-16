// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// TableKeyManager.cs
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using System.Xml.Linq;

namespace LJCDataUtilityDAL
{
  /// <summary>Provides table specific data methods.</summary>
  public class TableKeyManager
  {
    #region Constructors

    // Initializes an object instance.
    /// <include file='../../LJCGenDoc/Common/Manager.xml'
    ///  path='members/Constructor/*'/>
    public TableKeyManager()
    {
      Manager = null;
      ResultConverter = new ResultConverter<TableKey, TableKeys>();
    }

    // Initializes an object instance.
    /// <include file='../../LJCGenDoc/Common/Manager.xml'
    ///  path='items/DataManagerC/*'/>
    public TableKeyManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "", string schemaName = null) : this()
    {
      Manager = new DataManager(dbServiceRef, dataConfigName, tableName
        , schemaName);
    }
    #endregion

    #region Data Methods

    // Retrieves a collection of data records.
    /// <include file='../../LJCGenDoc/Common/Manager.xml'
    ///  path='members/LoadForeignKeys/*'/>
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

      var sql = ForeignKeySql(Manager.TableName);
      var dbResult = Manager.ExecuteClientSql(RequestType.LoadSQL, sql);
      var retValue = ResultConverter.CreateCollection(dbResult);
      return retValue;
    }

    // Retrieves a collection of Key records.
    /// <include file='../../LJCGenDoc/Common/Manager.xml'
    ///  path='members/LoadTableKeys/*'/>
    public TableKeys LoadTableKeys(string keyType = "PRIMARY KEY"
      , string constraintName = null)
    {
      var sql = TableKeySql(Manager.TableName, keyType, constraintName);
      if ("FOREIGN KEY" == keyType)
      {
        sql = ForeignKeySql(Manager.TableName);
      }
      var dbResult = Manager.ExecuteClientSql(RequestType.LoadSQL, sql);
      var retValue = ResultConverter.CreateCollection(dbResult);
      return retValue;
    }

    // Gets the table foreign key values SQL.
    private string ForeignKeySql(string tableName)
    {
      TextBuilder tb = new TextBuilder();

      tb.Line("select");
      tb.Line("rc.CONSTRAINT_CATALOG as DBName, ");
      tb.Line("rc.CONSTRAINT_NAME AS ConstraintName, ");
      tb.Line("rc.UNIQUE_CONSTRAINT_NAME as UniqueConstraintName, ");
      tb.Line("tc.CONSTRAINT_TYPE as KeyType, ");
      tb.Line("kcus.COLUMN_NAME AS ColumnName, ");
      tb.Line("kcus.ORDINAL_POSITION as OrdinalPosition, ");
      tb.Line("kcus.TABLE_NAME AS TableName, ");
      tb.Line("kcus.TABLE_SCHEMA AS TableSchema, ");
      tb.Line("kcut.COLUMN_NAME AS TargetColumn, ");
      tb.Line("kcut.TABLE_NAME AS TargetTable ");
      tb.Line("from INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS rc ");
      tb.Line("left join INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc ");
      tb.Line("  on rc.CONSTRAINT_NAME = tc.CONSTRAINT_NAME ");
      tb.Line("left join INFORMATION_SCHEMA.KEY_COLUMN_USAGE kcus ");
      tb.Line("  on rc.CONSTRAINT_NAME = kcus.CONSTRAINT_NAME ");
      tb.Line("left join INFORMATION_SCHEMA.KEY_COLUMN_USAGE kcut ");
      tb.Line("  on rc.UNIQUE_CONSTRAINT_NAME = kcut.CONSTRAINT_NAME ");
      tb.Line("  and kcus.ORDINAL_POSITION = kcut.ORDINAL_POSITION ");
      tb.Line($"where kcus.Table_Name = '{tableName}' ");
      tb.Line("order by rc.[CONSTRAINT_NAME], kcus.[ORDINAL_POSITION];");

      var retValue = tb.ToString();
      return retValue;
    }

    // Gets the table key values SQL.
    private string TableKeySql(string tableName
      , string keyType = "PRIMARY KEY", string uniqueConstraintName = null)
    {
      TextBuilder tb = new TextBuilder();

      tb.Line("SELECT");
      tb.Line(" tc.[CONSTRAINT_NAME] as ConstraintName, ");
      tb.Line(" tc.[TABLE_CATALOG] as DBName, ");
      tb.Line(" tc.[CONSTRAINT_TYPE] as KeyType, ");
      tb.Line(" tc.[TABLE_NAME] as TableName, ");
      tb.Line(" tc.[TABLE_SCHEMA] as TableSchema, ");
      tb.Line(" kcu.[COLUMN_NAME] as ColumnName, ");
      tb.Line(" kcu.[ORDINAL_POSITION] as OrdinalPosition, ");
      tb.Line(" rc.[UNIQUE_CONSTRAINT_NAME] as UniqueConstraintName ");
      tb.Line("FROM [INFORMATION_SCHEMA].[TABLE_CONSTRAINTS] as tc ");
      tb.Line("LEFT JOIN [INFORMATION_SCHEMA].[KEY_COLUMN_USAGE] as kcu ");
      tb.Line(" ON tc.[CONSTRAINT_NAME] = kcu.[CONSTRAINT_NAME] ");
      tb.Line("LEFT JOIN INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS as rc ");
      tb.Line(" ON tc.CONSTRAINT_NAME = rc.CONSTRAINT_NAME");
      if (NetString.HasValue(uniqueConstraintName))
      {
        tb.Line($"WHERE tc.[CONSTRAINT_NAME] = '{uniqueConstraintName}'");
      }
      else
      {
        tb.Line($"WHERE tc.[TABLE_NAME] = '{tableName}'");
        tb.Line($" AND tc.[CONSTRAINT_TYPE] = '{keyType}'");
      }
      tb.Line("ORDER BY tc.[CONSTRAINT_NAME], kcu.[ORDINAL_POSITION];");

      var retValue = tb.ToString();
      return retValue;
    }

    //// Create the sp_GetForeignKeys procedure.
    //private void CreateForeignKeysProcedure()
    //{
    //  var sql = "DROP PROCEDURE IF EXISTS dbo.sp_GetForeignKeys;";
    //  Manager.ExecuteClientSql(RequestType.ExecuteSQL, sql);

    //  sql = ForeignKeysProcedureSql(Manager.TableName);
    //  try
    //  {
    //    Manager.ExecuteClientSql(RequestType.ExecuteSQL, sql);
    //  }
    //  catch (Exception e)
    //  {
    //    var isHandled = false;
    //    if (e is SqlException)
    //    {
    //      // ErrorCode -2146232060
    //      if (e.Message.StartsWith("There is already an object"))
    //      {
    //        isHandled = true;
    //      }
    //    }
    //    if (!isHandled)
    //    {
    //      throw e;
    //    }
    //  }
    //}

    //// Gets the sp_GetForeignKeys procedure SQL.
    //private string ForeignKeysProcedureSql(string tableName)
    //{
    //  TextBuilder b = new TextBuilder();
    //  b.Line("CREATE PROCEDURE[dbo].[sp_GetForeignKeys]");
    //  b.Line("  @TableName nvarchar(20)");
    //  b.Line("AS");
    //  b.Line("BEGIN");
    //  b.Line("SELECT");
    //  b.Line(" tc.[TABLE_CATALOG] as DBName, ");
    //  b.Line(" tc.[TABLE_SCHEMA] as TableSchema, ");
    //  b.Line(" tc.[TABLE_NAME] as TableName, ");
    //  b.Line(" tc.[CONSTRAINT_Name] as ConstraintName, ");
    //  b.Line(" tc.[CONSTRAINT_TYPE] as KeyType, ");
    //  b.Line(" ccu.[COLUMN_NAME] as ColumnName, ");
    //  b.Line(" kcu.[ORDINAL_POSITION] as OrdinalPosition, ");
    //  b.Line(" rc.[UPDATE_RULE] as UpdateRule, ");
    //  b.Line(" rc.[DELETE_RULE] as DeleteRule ");
    //  b.Line("from [INFORMATION_SCHEMA].[TABLE_CONSTRAINTS] as tc ");
    //  b.Line("left join[INFORMATION_SCHEMA].[CONSTRAINT_COLUMN_USAGE] as ccu ");
    //  b.Line(" on tc.[CONSTRAINT_NAME] = ccu.[CONSTRAINT_NAME] ");
    //  b.Line("left join[INFORMATION_SCHEMA].[KEY_COLUMN_USAGE] as kcu ");
    //  b.Line(" on tc.[CONSTRAINT_NAME] = kcu.[CONSTRAINT_NAME] ");
    //  b.Line("left join[INFORMATION_SCHEMA].[REFERENTIAL_CONSTRAINTS] as rc ");
    //  b.Line(" on ccu.[CONSTRAINT_NAME] = rc.[CONSTRAINT_NAME] ");
    //  b.Line($"where tc.[TABLE_NAME] = '{tableName}'");
    //  b.Line($" and tc.[CONSTRAINT_TYPE] = 'FOREIGN KEY'");
    //  b.Line("END");
    //  var retValue = b.ToString();
    //  return retValue;
    //}
    #endregion

    #region Properties

    // Gets the affected record count.
    /// <include file='Doc/TableKeyManager.xml'
    ///  path='members/AffectedCount/*'/>
    public int AffectedCount
    {
      get => Manager.AffectedCount;
    }

    // Gets or sets the DataManager reference.
    /// <include file='Doc/TableKeyManager.xml'
    ///  path='members/Manager/*'/>
    public DataManager Manager { get; set; }

    // Gets or sets the ResultConverter reference.
    /// <include file='Doc/TableKeyManager.xml'
    ///  path='members/ResultConverter/*'/>
    public ResultConverter<TableKey, TableKeys> ResultConverter { get; set; }

    // Gets or sets the table name.
    /// <include file='Doc/TableKeyManager.xml'
    ///  path='members/TableName/*'/>
    public string TableName
    {
      get => _TableName;
      set
      {
        var newValue = value?.Trim();
        _TableName = newValue;
        Manager.TableName = newValue;
      }
    }
    private string _TableName;
    #endregion
  }
}
