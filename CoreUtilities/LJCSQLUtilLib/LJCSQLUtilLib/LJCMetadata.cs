// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCMetadata.cs
using LJCDBClientLib;
using LJCDBDataAccess;
using LJCDBMessage;
using LJCNetCommon;
using LJCSQLUtilLibDAL;
using LJCWinFormCommon;
using System;
using System.Data;
using System.Text;

namespace LJCSQLUtilLib
{
	// Contains Metadata helper methods.
	/// <include path='items/LJCMetadata/*' file='Doc/ProjectSQLUtilLib.xml'/>
	public class LJCMetadata
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/LJCMetadataC/*' file='Doc/LJCMetadata.xml'/>
		public LJCMetadata(DbServiceRef dbServiceRef, string dataConfigName)
		{
			if (dbServiceRef != null)
			{
				mDbServiceRef = dbServiceRef;
			}
			else
			{
				// 7/23
				mDbServiceRef = new DbServiceRef()
				{
					DbDataAccess = new DbDataAccess(dataConfigName)
				};
			}
			//mDataConfigName = dataConfigName;
			InitializeData();
		}
		#endregion

		#region Create Metadata Table Methods

		// Builds and returns the Create Table SQL statement from the Metadata.
		/// <include path='items/GetCreateTableSql1/*' file='Doc/LJCMetadata.xml'/>
		public string GetCreateTableSql(string tableName, bool includeForeignKeys = true)
		{
			string retValue;

			DbMetaDataTable mdTable = MdTableManager.RetrieveWithUniqueKey(tableName);
			if (null == mdTable)
			{
				retValue = $"Table '{tableName}' was not found.";
			}
			else
			{
				DbMetaDataColumns mdColumns = MdColumnManager.LoadByTableID(mdTable.ID);
				retValue = GetCreateTableSql(mdTable.TableName, mdColumns, includeForeignKeys);
			}
			return retValue;
		}

		// Builds and returns the Create Table SQL statement with the specified
		// Metadata columns.
		/// <include path='items/GetCreateTableSql2/*' file='Doc/LJCMetadata.xml'/>
		public string GetCreateTableSql(string tableName, DbMetaDataColumns mdColumns
			, bool includeForeignKeys = true)
		{
			StringBuilder builder;
			string primaryKeyConstraintSql;
			string retValue = null;

			string columnSql = GetCreateColumnSql(mdColumns);
			if (columnSql != null)
			{
				builder = new StringBuilder(128);
				builder.AppendLine("IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES");
				builder.AppendLine($" WHERE TABLE_NAME = N'{tableName}')");
				builder.AppendLine("BEGIN");
				builder.AppendLine($"CREATE TABLE[dbo].[{tableName}](");
				builder.Append(columnSql);

				primaryKeyConstraintSql = GetPrimaryKeyConstraintSql(tableName);
				if (primaryKeyConstraintSql != null)
				{
					builder.Append(primaryKeyConstraintSql);
				}
				builder.AppendLine(")");

				if (includeForeignKeys)
				{
					mdColumns = MdColumnManager.LoadByTableID(2);
					builder.Append(GetForeignKeyConstraintSql(tableName, mdColumns));
				}
				builder.AppendLine("END");
				retValue = builder.ToString();
			}
			return retValue;
		}

		// Creates and returns the Column Definitions SQL for the Create Table SQL
		/// <include path='items/GetCreateColumnSql/*' file='Doc/LJCMetadata.xml'/>
		public string GetCreateColumnSql(DbMetaDataColumns mdColumns)
		{
			StringBuilder builder;
			string retValue = null;

			if (mdColumns != null && mdColumns.Count > 0)
			{
				builder = new StringBuilder(128);
				foreach (DbMetaDataColumn mdColumn in mdColumns)
				{
					string sqlDbTypeName = GetSqlDbTypeName(mdColumn.DataTypeName);
					builder.Append($"	[{mdColumn.ColumnName}]");
					if (mdColumn.AutoIncrement)
					{
						builder.Append($" [{sqlDbTypeName}] IDENTITY(1, 1)");
					}
					else
					{
						builder.Append($" [{sqlDbTypeName}]");
					}
					if ("nvarchar" == sqlDbTypeName
						|| "varchar" == sqlDbTypeName)
					{
						builder.Append($"({mdColumn.MaxLength})");
					}
					if (!mdColumn.AllowDBNull)
					{
						builder.Append(" NOT");
					}
					builder.AppendLine(" NULL,");
				}
				retValue = builder.ToString();
			}
			return retValue;
		}

		// Get the .NET to SQL data type names.
		/// <include path='items/GetSqlDbTypeName/*' file='Doc/LJCMetadata.xml'/>
		public string GetSqlDbTypeName(string netTypeName)
		{
			string retValue;

			switch (netTypeName)
			{
				case "Boolean":
					retValue = Enum.GetName(typeof(SqlDbType), SqlDbType.Bit);
					break;
				case "Byte":
					retValue = Enum.GetName(typeof(SqlDbType), SqlDbType.TinyInt);
					break;
				case "DateTime":
					retValue = Enum.GetName(typeof(SqlDbType), SqlDbType.DateTime);
					break;
				case "Decimal":
					retValue = Enum.GetName(typeof(SqlDbType), SqlDbType.Decimal);
					break;
				case "Double":
					retValue = Enum.GetName(typeof(SqlDbType), SqlDbType.Float);
					break;
				case "Int16":
					retValue = Enum.GetName(typeof(SqlDbType), SqlDbType.SmallInt);
					break;
				case "Int32":
					retValue = Enum.GetName(typeof(SqlDbType), SqlDbType.Int);
					break;
				case "Int64":
					retValue = Enum.GetName(typeof(SqlDbType), SqlDbType.BigInt);
					break;
				case "Single":
					retValue = Enum.GetName(typeof(SqlDbType), SqlDbType.Real);
					break;
				case "String":
				default:
					retValue = Enum.GetName(typeof(SqlDbType), SqlDbType.NVarChar);
					break;
			}
			return retValue.ToLower();
		}

		// Creates and returns the Primary Key Constraint SQL for the Create Table
		// SQL statement from the Metadata.
		/// <include path='items/GetPrimaryKeyConstraintSql/*' file='Doc/LJCMetadata.xml'/>
		public string GetPrimaryKeyConstraintSql(string tableName)
		{
			StringBuilder builder = null;
			string retValue = null;

			DbMetaDataKeys mdKeys = MdKeyManager.LoadPrimaryKeys();
			if (mdKeys != null && mdKeys.Count > 0)
			{
				if (null == builder)
				{
					builder = new StringBuilder(128);
					builder.AppendLine($"	CONSTRAINT [PK_{tableName}]");
					builder.AppendLine("	PRIMARY KEY CLUSTERED(");
					builder.Append("		[");
				}
				bool first = true;
				foreach (DbMetaDataKey mdKey in mdKeys)
				{
					if (!first)
					{
						builder.Append(",\r\n");
					}
					first = false;
					builder.Append(mdKey.ColumnName);
				}
				builder.AppendLine("] ASC)");
				retValue = builder.ToString();
			}
			return retValue;
		}

		// Creates and returns the Foreign Key Constraing SQL for the foreign keys.
		/// <include path='items/GetForeignKeyConstraintSql/*' file='Doc/LJCMetadata.xml'/>
		public string GetForeignKeyConstraintSql(string tableName, DbMetaDataColumns mdColumns)
		{
			StringBuilder builder = null;
			string retValue = null;

			foreach (DbMetaDataColumn mdColumn in mdColumns)
			{
				DbMetaDataKeys mdKeys = MdKeyManager.LoadForeignKey(mdColumn.ID);
				if (mdKeys != null && mdKeys.Count > 0)
				{
					if (null == builder)
					{
						builder = new StringBuilder(128);
						builder.AppendLine($"ALTER TABLE[dbo].[{tableName}] WITH CHECK");
					}
					bool first = true;
					foreach (DbMetaDataKey mdKey in mdKeys)
					{
						if (!first)
						{
							builder.Append(",\r\n");
						}
						first = false;
						builder.AppendLine($"	ADD CONSTRAINT [FK_{tableName}_{mdKey.ToTableName}]");
						builder.AppendLine($"	FOREIGN KEY([{mdKey.ColumnName}])");
						builder.Append($"	REFERENCES [dbo].[{mdKey.ToTableName}] ([{mdKey.ToColumnName}])");
					}
					builder.AppendLine();
					retValue = builder.ToString();
				}
			}
			return retValue;
		}

		/// <summary>
		/// Remake table with new column names and/or column sequences.
		/// </summary>
		/// <param name="tableName">The table name.</param>
		/// <param name="fromMdColumns">The source columns.</param>
		/// <param name="toMdColumns">The target columns.</param>
		public string GetRemakeTableScript(string tableName
			, DbMetaDataColumns fromMdColumns, DbMetaDataColumns toMdColumns)
		{
			StringBuilder builder;
			ForeignKeys primaryKeys;
			ForeignKeys foreignKeys;
			string format;
			string retValue = null;

			builder = new StringBuilder(128);

			builder.AppendLine("-- Drop table FK constraints. ");
			foreignKeys = LoadSchemaForeignKeys(tableName);
			if (null != foreignKeys && foreignKeys.Count > 0)
			{
				foreach (ForeignKey foreignKey in foreignKeys)
				{
					format = "ALTER TABLE [dbo].[{0}] DROP CONSTRAINT {1} \r\n";
					builder.AppendFormat(format, foreignKey.SourceTable
						, foreignKey.SourceConstraint);
				}
				builder.AppendLine("GO");
			}
			builder.AppendLine();

			builder.AppendLine("-- Drop table PK constraints. ");
			primaryKeys = LoadSchemaPrimaryKeys(tableName);
			if (null != primaryKeys && primaryKeys.Count > 0)
			{
				foreach (ForeignKey foreignKey in primaryKeys)
				{
					format = "ALTER TABLE [dbo].[{0}] DROP CONSTRAINT {1} \r\n";
					builder.AppendFormat(format, foreignKey.SourceTable
						, foreignKey.SourceConstraint);
				}
				builder.AppendLine("GO");
			}
			builder.AppendLine();

			builder.AppendLine("-- Rename table to backup. ");
			string backupTableName = $"{tableName}Backup";
			builder.AppendLine($"EXEC sp_rename '{tableName}', '{backupTableName}'");
			builder.AppendLine("GO");
			builder.AppendLine();

			builder.AppendLine("-- Create new table. ");
			builder.Append(GetCreateTableSql(tableName, toMdColumns, false));
			builder.AppendLine("GO");
			builder.AppendLine();

			builder.AppendLine("-- Copy to new table. ");

			// To columns.
			builder.AppendLine($"SET IDENTITY_INSERT {tableName} ON; ");
			builder.AppendLine("insert into {tableName} ");
			builder.Append(" (");
			bool first = true;
			foreach (DbMetaDataColumn toMdColumn in toMdColumns)
			{
				if (!first)
				{
					builder.Append(", ");
				}
				first = false;
				builder.Append(toMdColumn.ColumnName);
			}
			builder.AppendLine(") ");

			// Select matching "From" columns by "To" columns ID.
			DbMetaDataColumn fromMdColumn;
			builder.AppendLine("select ");
			builder.Append("  ");
			first = true;
			foreach (DbMetaDataColumn toMdColumn in toMdColumns)
			{
				if (!first)
				{
					builder.Append(", ");
				}
				first = false;

				fromMdColumn = fromMdColumns.Find(x => x.ID == toMdColumn.ID);
				if (null == fromMdColumn)
				{
					// Add default value.
					builder.Append(toMdColumn.DefaultValue);
				}
				else
				{
					builder.Append(fromMdColumn.ColumnName);
				}
			}

			builder.AppendLine();
			builder.AppendLine($"from {backupTableName} ");
			builder.AppendLine("SET IDENTITY_INSERT {tableName} OFF; ");
			builder.AppendLine("GO");
			builder.AppendLine();

			builder.Append("-- Create new table FK constraints. ");
			if (null != foreignKeys && foreignKeys.Count > 0)
			{
				foreach (ForeignKey foreignKey in foreignKeys)
				{
					builder.AppendLine();
					builder.AppendLine($"ALTER TABLE[dbo].[{foreignKey.SourceTable}]"
						+ $" WITH CHECK ");
					builder.AppendLine($"	ADD CONSTRAINT [FK_{foreignKey.SourceTable}_"
						+ $"{foreignKey.TargetTable}] ");
					builder.AppendLine($"	FOREIGN KEY([{foreignKey.SourceColumn}]) ");
					builder.Append($"	REFERENCES [dbo].[{foreignKey.TargetTable}]"
						+ $" ([{foreignKey.TargetColumn}])");
				}
				builder.AppendLine();
				builder.AppendLine("GO");
			}
			retValue = builder.ToString();
			return retValue;
		}
		#endregion

		#region Save Metadata from Schema Methods

		// Creates/Updates the Metadata from the Schema data.
		/// <include path='items/UpdateMetadataFromSchema/*' file='Doc/LJCMetadata.xml'/>
		public bool UpdateMetadataFromSchema(string dataConfigName, string tableName)
		{
			ForeignKeys foreignKeys;
			DbMetaDataColumn mdColumn;
			DbMetaDataKey mdKey;
			bool retValue = true;

			DataManager dataManager = new DataManager(mDbServiceRef, dataConfigName
				, tableName);
			DbResult dbResult = dataManager.GetSchemaOnly();
			if (null != dbResult)
			{
				DbMetaDataTable mdTable = SaveMdTable(tableName);

				int sequence = 0;
				DbColumns dbColumns = dbResult.Columns;
				foreach (DbColumn dbColumn in dbColumns)
				{
					sequence++;
					mdColumn = SaveMdColumn(mdTable.ID, sequence, dbColumn);
					if (null == mdColumn)
					{
						retValue = false;
						break;
					}
				}

				if (retValue)
				{
					foreignKeys = LoadSchemaPrimaryKeys(tableName);
					if (null == foreignKeys)
					{
						retValue = false;
					}
					else
					{
						foreach (ForeignKey foreignKey in foreignKeys)
						{
							mdKey = SaveMdKey(foreignKey);
							if (null == mdKey)
							{
								retValue = false;
								break;
							}
						}
					}
				}

				if (retValue)
				{
					foreignKeys = LoadSchemaForeignKeys(tableName);
					if (null == foreignKeys)
					{
						retValue = false;
					}
					else
					{
						foreach (ForeignKey foreignKey in foreignKeys)
						{
							mdKey = SaveMdKey(foreignKey);
							if (null == mdKey)
							{
								retValue = false;
								break;
							}
						}
					}
				}
			}
			return retValue;
		}

		// Saves the MD Table from the table name.
		/// <include path='items/SaveMdTable/*' file='Doc/LJCMetadata.xml'/>
		public DbMetaDataTable SaveMdTable(string tableName)
		{
			DbMetaDataTable retValue;

			// Get Update record.
			retValue = MdTableManager.RetrieveWithUniqueKey(tableName);
			if (null == retValue)
			{
				// Get Create record.
				retValue = new DbMetaDataTable()
				{
					TableName = tableName,
					Name = tableName,
					PluralName = tableName,
					Caption = tableName
				};
			}

			// Set updatable values and save.
			MdTableManager.SaveData(retValue);
			return retValue;
		}

		// Saves the MD Column from the DbColumn object.
		/// <include path='items/SaveMdColumn/*' file='Doc/LJCMetadata.xml'/>
		public DbMetaDataColumn SaveMdColumn(int tableID, int sequence, DbColumn dbColumn)
		{
			DbMetaDataColumn retValue;

			// Get Update record.
			retValue = MdColumnManager.RetrieveWithUniqueKey(tableID, dbColumn.ColumnName);
			if (null == retValue)
			{
				// Get Create record.
				retValue = new DbMetaDataColumn()
				{
					DbMetaDataTableID = tableID,
				};
			}

			// Set updatable values and save.
			retValue.DbMetaDataTableID = tableID;
			retValue.Sequence = sequence;
			retValue.ColumnName = dbColumn.ColumnName;
			retValue.PropertyName = dbColumn.PropertyName;
			retValue.Name = dbColumn.ColumnName;
			retValue.ShortCaption = dbColumn.Caption;
			retValue.Caption = dbColumn.Caption;
			retValue.DataTypeName = dbColumn.DataTypeName;
			retValue.MaxLength = dbColumn.MaxLength;
			retValue.AllowDBNull = dbColumn.AllowDBNull;
			retValue.AutoIncrement = dbColumn.AutoIncrement;
			//retValue.IsPrimaryKey = dbColumn.IsPrimaryKey;
			MdColumnManager.SaveData(retValue);
			return retValue;
		}

		// Saves the MD Key from the ForeignKey object.
		/// <include path='items/SaveMdKey/*' file='Doc/LJCMetadata.xml'/>
		public DbMetaDataKey SaveMdKey(ForeignKey foreignKey)
		{
			DbMetaDataTable mdSourceTable;
			DbMetaDataTable mdTargetTable;
			DbMetaDataColumn mdSourceColumn = null;
			DbMetaDataColumn mdTargetColumn;
			DbMetaDataKey retValue = null;

			// Get Update record.
			int keyType = 2;
			if (null == foreignKey.TargetTable)
			{
				keyType = 1;
			}

			// Get Source table ID.
			mdSourceTable = MdTableManager.RetrieveWithUniqueKey(foreignKey.SourceTable);
			if (mdSourceTable != null)
			{
				mdSourceColumn = MdColumnManager.RetrieveWithUniqueKey(mdSourceTable.ID
					, foreignKey.SourceColumn);
			}

			if (mdSourceColumn != null)
			{
				retValue = MdKeyManager.RetrieveWithUniqueKey(mdSourceColumn.ID, keyType);
				if (null == retValue)
				{
					// Get Create record.
					retValue = new DbMetaDataKey()
					{
						DbMetaDataColumnID = mdSourceColumn.ID,
						DbMetaDataKeyTypeID = keyType
					};
				}

				// Set updatable values and save.
				if (null != foreignKey.TargetTable)
				{
					mdTargetTable = MdTableManager.RetrieveWithUniqueKey(foreignKey.TargetTable);
					if (mdTargetTable != null)
					{
						mdTargetColumn = MdColumnManager.RetrieveWithUniqueKey(mdTargetTable.ID
							, foreignKey.TargetColumn);

						// ToDo: What if mdTargetColumn is null?
						if (mdTargetColumn != null)
						{
							retValue.ToColumnID = mdTargetColumn.ID;
							MdKeyManager.SaveData(retValue);
						}
					}
				}
				else
				{
					MdKeyManager.SaveData(retValue);
				}
			}
			return retValue;
		}
		#endregion

		#region Schema Methods

		// Loads a collection of Primary Key data records.
		/// <include path='items/LoadSchemaPrimaryKeys/*' file='Doc/LJCMetadata.xml'/>
		public ForeignKeys LoadSchemaPrimaryKeys(string tableName = null)
		{
			ForeignKeys retValue;

			retValue = ForeignKeyManager.LoadPrimaryKeys(tableName);
			return retValue;
		}

		// Loads a collection of Foreign Key data records. 
		/// <include path='items/LoadSchemaForeignKeys/*' file='Doc/LJCMetadata.xml'/>
		public ForeignKeys LoadSchemaForeignKeys(string tableName = null)
		{
			ForeignKeys retValue;

			retValue = ForeignKeyManager.LoadForeignKeys(tableName);

			// How to use a stored procedure.
			//mForeignKeyManager.DataManager.DataConfigName = "FacilityManager";
			//DbJoins joins = mForeignKeyManager.GetLoadJoins();
			//Parameters parameters = new Parameters
			//{
			//	{ "@TableName", SqlDbType.NVarChar, 20, "Unit" }
			//};
			//ForeignKeys foreignKeys = mForeignKeyManager.LoadProcedure("sp_GetForeignKeys"
			//	, parameters, joins);
			//mForeignKeyManager.DataManager.DataConfigName = "AppManager";
			return retValue;
		}
		#endregion

		#region Setup Methods

		// Configures the controls and loads the selection control data.
		/// <include path='items/InitializeControls/*' file='../../LJCGenDoc/Common/List.xml'/>
		private void InitializeData()
		{
			//string dataConfigName = "AppManager";
			string dataConfigName = "FacilityManager";

			// Initialize Class Data.
			ForeignKeyManager = new ForeignKeyManager(mDbServiceRef, dataConfigName);
			if (ForeignKeyManager != null)
			{
				try
				{
					MdTableManager = new DbMetaDataTableManager(mDbServiceRef, dataConfigName);
				}
				catch (SystemException e)
				{
					CreateTables(e, dataConfigName);
					MdTableManager = new DbMetaDataTableManager(mDbServiceRef, dataConfigName);
				}
				MdColumnManager = new DbMetaDataColumnManager(mDbServiceRef, dataConfigName);
				MdKeyManager = new DbMetaDataKeyManager(mDbServiceRef, dataConfigName);
			}
		}

		// Create the application tables.
		internal static void CreateTables(SystemException e, string dataConfigName)
		{
			string[] fileSpecs = {
				@"CreateDBMetaDataTables.sql"
			};

			int errorCode = ManagerCommon.GetMissingTableErrorCode(dataConfigName);
			if (e.HResult == errorCode)
			{
				if (FormCommon.CreateTablesPrompt(e.Message, fileSpecs))
				{
					if (!ManagerCommon.CreateTables(dataConfigName, fileSpecs))
					{
						throw new SystemException(e.Message);
					}
				}
			}
		}
		#endregion

		#region Properties

		/// <summary>Gets or sets the MdTableManager value.</summary>
		public DbMetaDataTableManager MdTableManager { get; set; }

		/// <summary>Gets or sets the MdColumnManager value.</summary>
		public DbMetaDataColumnManager MdColumnManager { get; set; }

		/// <summary>Gets or sets the MdKeyManager value.</summary>
		public DbMetaDataKeyManager MdKeyManager { get; set; }

		/// <summary>Gets or sets the ForeignKeyManager value.</summary>
		public ForeignKeyManager ForeignKeyManager { get; set; }
		#endregion

		#region Class Data

		private readonly DbServiceRef mDbServiceRef;
		//private readonly string mDataConfigName;
		#endregion
	}
}
