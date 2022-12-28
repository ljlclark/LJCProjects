// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DbSqlBuilder.cs
using System.Text;
using LJCNetCommon;

namespace LJCDBMessage
{
  // Provides SQL builder methods.
  /// <include path='items/DbSqlBuilder/*' file='Doc/DbSqlBuilder.xml'/>
  public class DbSqlBuilder
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DbSqlBuilderC/*' file='Doc/DbSqlBuilder.xml'/>
    public DbSqlBuilder(DbRequest dbRequest)
    {
      mDbRequest = dbRequest;
    }
    #endregion

    #region Public Methods

    // Creates the list of included table columns.
    /// <include path='items/ColumnList/*' file='Doc/DbSqlBuilder.xml'/>
    public string ColumnList(DbRequest dbRequest = null, bool listOnly = true)
    {
      string retValue;

      if (null == dbRequest)
      {
        dbRequest = mDbRequest;
      }

      if (null == dbRequest.Columns)
      {
        retValue = $" {dbRequest.TableName}.* \r\n";
      }
      else
      {
        StringBuilder builder = new StringBuilder(64);
        foreach (DbColumn dbColumn in dbRequest.Columns)
        {
          if (0 == builder.Length)
          {
            if (false == listOnly)
            {
              builder.AppendLine("(");
            }
          }
          else
          {
            builder.AppendLine(", ");
          }

          builder.Append($" {dbRequest.TableName}.{dbColumn.ColumnName}");
          if (dbColumn.RenameAs != null)
          {
            builder.Append($" as {dbColumn.RenameAs}");
          }
        }

        if (dbRequest.Joins != null)
        {
          foreach (DbJoin join in dbRequest.Joins)
          {
            if (join.Columns != null)
            {
              foreach (DbColumn column in join.Columns)
              {
                string qualifier = join.TableName;
                if (join.TableAlias != null)
                {
                  qualifier = join.TableAlias;
                }

                builder.Append($", \r\n {qualifier}.{column.ColumnName}");
                if (column.RenameAs != null)
                {
                  builder.Append($" as {column.RenameAs}");
                }
              }
            }
          }
        }
        builder.AppendLine(" ");

        if (false == listOnly)
        {
          builder.AppendLine(")");
        }
        retValue = builder.ToString();
      }
      return retValue;
    }

    // Creates the SQL Insert statement.
    /// <include path='items/CreateAddSql/*' file='Doc/DbSqlBuilder.xml'/>
    public string CreateAddSql()
    {
      string retValue;

      StringBuilder builder = new StringBuilder(64);
      builder.Append($"insert into {mDbRequest.TableName}\r\n");
      builder.Append(ColumnList(listOnly: false));
      builder.Append(InsertValueList(false));
      retValue = builder.ToString();
      return retValue;
    }

    // Creates the SQL Delete statement.
    /// <include path='items/CreateDeleteSql/*' file='Doc/DbSqlBuilder.xml'/>
    public string CreateDeleteSql()
    {
      string retValue;

      StringBuilder builder = new StringBuilder(64);
      builder.Append($"delete from {mDbRequest.TableName}\r\n");
      builder.Append(WhereClause());
      retValue = builder.ToString();
      return retValue;
    }

    // Creates the SQL Select statement for multiple records.
    /// <include path='items/CreateLoadSql/*' file='Doc/DbSqlBuilder.xml'/>
    public string CreateLoadSql(DbRequest dbRequest = null)
    {
      string retValue;

      if (null == dbRequest)
      {
        dbRequest = mDbRequest;
      }

      StringBuilder builder = new StringBuilder(64);
      builder.AppendLine("select ");
      builder.Append(ColumnList());
      builder.Append("from ");
      if (NetString.HasValue(dbRequest.SchemaName))
      {
        builder.Append($"{dbRequest.SchemaName}.");
      }
      builder.Append($"{dbRequest.TableName} \r\n");
      builder.Append(JoinStatement(dbRequest.Joins, dbRequest.SchemaName));
      builder.Append(WhereClause());
      builder.Append(OrderBy());
      builder.Append(PageFetch());
      retValue = builder.ToString();
      return retValue;
    }

    // Creates the SQL Select statement for one record.
    /// <include path='items/CreateRetrieveSql/*' file='Doc/DbSqlBuilder.xml'/>
    public string CreateRetrieveSql(DbRequest dbRequest = null)
    {
      string retValue;

      if (null == dbRequest)
      {
        dbRequest = mDbRequest;
      }

      StringBuilder builder = new StringBuilder(64);
      builder.AppendLine("select");
      builder.Append(ColumnList(dbRequest));
      builder.Append("from ");
      if (NetString.HasValue(dbRequest.SchemaName))
      {
        builder.Append($"{dbRequest.SchemaName}.");
      }
      builder.Append($"{dbRequest.TableName} \r\n");
      builder.Append(JoinStatement(dbRequest.Joins, dbRequest.SchemaName));
      builder.Append(WhereClause());
      retValue = builder.ToString();
      return retValue;
    }

    // Creates the SQL Update statement.
    /// <include path='items/CreateUpdateSql/*' file='Doc/DbSqlBuilder.xml'/>
    public string CreateUpdateSql()
    {
      string retValue;

      StringBuilder builder = new StringBuilder(64);
      builder.Append($"update {mDbRequest.TableName} \r\n");
      builder.AppendLine("set");
      builder.Append(UpdateValueList());
      builder.Append(WhereClause());
      retValue = builder.ToString();
      return retValue;
    }

    // Creates the where clause from the filters.
    /// <include path='items/FilterWhereClause/*' file='Doc/DbSqlBuilder.xml'/>
    public string FilterWhereClause(DbFilters dbFilters, bool recursive = false)
    {
      string retValue = null;

      if (dbFilters != null && dbFilters.Count > 0)
      {
        StringBuilder builder = new StringBuilder(64);
        bool first = true;
        foreach (DbFilter filter in dbFilters)
        {
          // Begin the filter grouping
          if (first && false == recursive)
          {
            builder.Append("where ");
          }
          else
          {
            if (recursive)
            {
              builder.AppendLine(" ");
            }

            // Each filter except the first will begin with the Boolean Operator.
            // If recursive, it will already be inside of a filter group.
            builder.Append($" {filter.BooleanOperator} ");
          }
          first = false;
          if (false == recursive)
          {
            // Begin the filter group.
            builder.Append("(");
          }

          // Add conditions.
          DbConditionSet conditionSet = filter.ConditionSet;
          var conditions = filter.ConditionSet.Conditions;
          if (conditions != null && conditions.Count > 0)
          {
            // Begin the conditions group.
            builder.Append("(");

            for (int index = 0; index < conditions.Count; index++)
            {
              DbCondition condition = conditions[index];
              if (index > 0 && 0 == index % 2)
              {
                builder.AppendLine();
                builder.Append("  ");
              }

              if (index > 0)
              {
                // Each condition except the first will begin with the ConditionSet -
                // Boolean Operator.
                builder.Append($" {conditionSet.BooleanOperator} ");
              }
              if (NetString.IsEqual("is null", condition.SecondValue))
              {
                builder.Append("{condition.FirstValue} is null");
              }
              else
              {
                builder.Append($"{condition.FirstValue} {condition.ComparisonOperator}");
                builder.Append($" {condition.SecondValue}");
              }
            }

            // End the conditions grouping.
            builder.Append(")");
          }

          // Recursive filters.
          if (filter.Filters != null && filter.Filters.Count > 0)
          {
            builder.Append(FilterWhereClause(filter.Filters, true));
          }

          // End the filter grouping.
          if (false == recursive)
          {
            builder.AppendLine(")");
          }
          retValue = builder.ToString();
        }
      }
      return retValue;
    }

    // Get the JoinOn statements.
    private string GetJoinOns(DbJoin dbJoin, DbJoinOns dbJoinOns
      , bool recursive = false)
    {
      string retValue;

      StringBuilder builder = new StringBuilder(64);
      bool first = true;
      foreach (DbJoinOn dbJoinOn in dbJoinOns)
      {
        // Begin the Join grouping.
        if (first && false == recursive)
        {
          builder.Append("(");
        }
        else
        {
          if (false == recursive)
          {
            builder.Append(")");
          }
          builder.Append($"\r\n {dbJoinOn.BooleanOperator} ");
          if (false == recursive)
          {
            builder.Append("(");
          }
        }
        first = false;

        // Begin the JoinOn grouping.
        builder.Append("(");

        string fromColumnName = QualifyColumnName(dbJoinOn.FromColumnName
          , mDbRequest.TableName);
        string toColumnName = QualifyColumnName(dbJoinOn.ToColumnName
          , dbJoin.TableName, dbJoin.TableAlias);

        builder.Append($"{fromColumnName} {dbJoinOn.JoinOnOperator} {toColumnName}");

        // End the JoinOn grouping.
        builder.Append(")");

        // Recursive JoinOns.
        if (dbJoinOn.JoinOns.Count > 0)
        {
          builder.Append(GetJoinOns(dbJoin, dbJoinOn.JoinOns, true));
        }
      }

      // End the Join grouping.
      if (false == recursive)
      {
        builder.Append(")");
      }
      retValue = builder.ToString();
      return retValue;
    }

    // Get the full join table string. 
    private string GetJoinTableString(string schemaName, DbJoin dbJoin)
    {
      string retValue;

      StringBuilder builder = new StringBuilder(64);
      builder.Append(" ");
      if (NetString.HasValue(schemaName))
      {
        // Note: use schemaName from params?
        builder.Append($"{mDbRequest.SchemaName}.");
      }
      builder.Append($"{dbJoin.TableName}");
      if (dbJoin.TableAlias != null)
      {
        builder.Append($" as {dbJoin.TableAlias}");
      }
      builder.AppendLine(" ");
      retValue = builder.ToString();
      return retValue;
    }

    // Creates a list of record values.
    /// <include path='items/InsertValueList/*' file='Doc/DbSqlBuilder.xml'/>
    public string InsertValueList(bool listOnly = true)
    {
      string retValue = null;

      if (mDbRequest.Columns != null && mDbRequest.Columns.Count > 0)
      {
        StringBuilder builder = new StringBuilder(64);
        foreach (DbColumn dbColumn in mDbRequest.Columns)
        {
          if (0 == builder.Length)
          {
            if (false == listOnly)
            {
              builder.AppendLine("values(");
            }
          }
          else
          {
            builder.AppendLine(",");
          }
          builder.Append($" {dbColumn.SQLFormatValue()}");
        }
        builder.AppendLine();

        if (false == listOnly)
        {
          builder.Append(")");
        }
        retValue = builder.ToString();
      }
      return retValue;
    }

    // Creates the join statement.
    /// <include path='items/JoinStatement/*' file='Doc/DbSqlBuilder.xml'/>
    public string JoinStatement(DbJoins dbJoins, string schemaName = null)
    {
      string retValue = null;

      if (dbJoins != null && dbJoins.Count > 0)
      {
        StringBuilder builder = new StringBuilder(64);
        foreach (DbJoin dbJoin in dbJoins)
        {
          // Begin the Join.
          if (builder.Length > 0)
          {
            builder.AppendLine(" ");
          }
          builder.Append($"{dbJoin.JoinType} join");
          builder.Append(GetJoinTableString(schemaName, dbJoin));
          builder.Append("  on ");
          builder.Append(GetJoinOns(dbJoin, dbJoin.JoinOns));
        }
        builder.AppendLine();
        retValue = builder.ToString();
      }
      return retValue;
    }

    // Creates the where clause from the key values.
    /// <include path='items/KeyWhereClause/*' file='Doc/DbSqlBuilder.xml'/>
    public string KeyWhereClause()
    {
      string retValue = null;

      DbColumns keyColumns = mDbRequest.KeyColumns;
      if (keyColumns != null && keyColumns.Count > 0)
      {
        StringBuilder builder = new StringBuilder(64);
        foreach (DbColumn dbColumn in keyColumns)
        {
          // Do not include null or empty values.
          if (null == dbColumn.Value
            || false == NetString.HasValue(dbColumn.Value.ToString()))
          {
            continue;
          }

          string value = dbColumn.SQLFormatValue();
          bool isZero = "0" == dbColumn.Value.ToString();

          // Do not include AutoIncrement if the value is "0".
          if (dbColumn.AutoIncrement && isZero)
          {
            continue;
          }

          if (0 == builder.Length)
          {
            builder.Append("where ");
          }
          else
          {
            builder.Append(" and ");
          }

          // Allow user to qualify column name to a table other than the
          // primary table.
          string tableName = mDbRequest.TableName;
          string columnName = dbColumn.ColumnName;
          if (columnName.IndexOf(".") > -1)
          {
            string[] values = columnName.Split('.');
            if (values.Length > 1)
            {
              tableName = values[0];
              columnName = values[1];
            }
          }

          // Create the standard condition.
          string text = $"{tableName}.{columnName} = {value}\r\n";

          // Create the "is null" condition.
          bool isNull = false;
          if (dbColumn.DataTypeName != "String"
            && dbColumn.DataTypeName != "Boolean")
          {
            if (dbColumn.AllowDBNull && isZero)
            {
              isNull = true;
            }
          }
          if ("'-null'" == value
            || "'-'" == value)
          {
            isNull = true;
          }
          if (isNull)
          {
            text = $"{tableName}.{columnName} is null\r\n";
          }

          builder.Append(text);
        }
        retValue = builder.ToString();
      }
      return retValue;
    }

    // Creates the order by statement from the order by column list.
    /// <include path='items/OrderBy/*' file='Doc/DbSqlBuilder.xml'/>
    public string OrderBy()
    {
      string retValue = null;

      if (mDbRequest.OrderByNames.Count > 0)
      {
        StringBuilder builder = new StringBuilder(64);
        builder.Append("order by ");
        bool first = true;
        foreach (string name in mDbRequest.OrderByNames)
        {
          if (false == first)
          {
            builder.Append(", ");
          }
          first = false;
          builder.Append(name);
        }
        builder.AppendLine(" ");
        retValue = builder.ToString();
      }
      return retValue;
    }

    // Creates the offset/fetch next statement.
    /// <include path='items/PageFetch/*' file='Doc/DbSqlBuilder.xml'/>
    public string PageFetch()
    {
      string retValue = null;

      if (mDbRequest.OrderByNames.Count > 0 && mDbRequest.PageSize > 0)
      {
        StringBuilder builder = new StringBuilder(64);
        builder.Append($"offset {mDbRequest.PageStartIndex} rows\r\n");
        builder.Append($"fetch next {mDbRequest.PageSize} rows only");
        builder.AppendLine();
        retValue = builder.ToString();
      }
      return retValue;
    }

    // Qualify with the table name unless already qualified.
    private string QualifyColumnName(string columnName, string tableName
      , string alias = null)
    {
      bool qualify = true;
      string retValue;

      retValue = columnName;

      if (columnName.Trim().StartsWith("|"))
      {
        // Value is a constant delimited with "|".
        qualify = false;
        retValue = retValue.Trim();
        retValue = retValue.Substring(1, retValue.Length - 2);
      }

      if (qualify)
      {
        // Allow user to qualify column name to another table.
        if (columnName.IndexOf(".") > -1)
        {
          string[] values = columnName.Split('.');
          if (values.Length > 1)
          {
            tableName = values[0];
            columnName = values[1];
          }
        }
        else
        {
          if (NetString.HasValue(alias))
          {
            tableName = alias;
          }
        }
        retValue = $"{tableName}.{columnName}";
      }
      return retValue;
    }

    // Creates a list of record update values.
    /// <include path='items/UpdateList/*' file='Doc/DbSqlBuilder.xml'/>
    public string UpdateValueList()
    {
      string retValue = null;

      if (mDbRequest.Columns != null && mDbRequest.Columns.Count > 0)
      {
        StringBuilder builder = new StringBuilder(64);
        foreach (DbColumn dbColumn in mDbRequest.Columns)
        {
          if (builder.Length > 0)
          {
            builder.AppendLine(",");
          }
          builder.Append($" {dbColumn.ColumnName} = {dbColumn.SQLFormatValue()}");
        }
        builder.AppendLine();
        retValue = builder.ToString();
      }
      return retValue;
    }

    // Create the where clause.
    /// <include path='items/WhereClause/*' file='Doc/DbSqlBuilder.xml'/>
    public string WhereClause()
    {
      string retValue = null;

      if (mDbRequest != null)
      {
        if (null == mDbRequest.Filters || 0 == mDbRequest.Filters.Count)
        {
          retValue = KeyWhereClause();
        }
        else
        {
          retValue = FilterWhereClause(mDbRequest.Filters);
        }
      }
      return retValue;
    }
    #endregion

    private readonly DbRequest mDbRequest;
  }
}
