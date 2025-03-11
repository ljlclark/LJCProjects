// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CommonModule.cs
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using LJCNetCommon;
using LJCDBMessage;
using LJCDBClientLib;
using LJCDBServiceLib;
using LJCDataAccessConfig;
using LJCTextDataReaderLib;
using LJCAddressParserLib;
using LJCDataTransformDAL;

namespace LJCCommonModuleLib
{
	// Executes an Action for all sources of a task.
	/// <include path='items/CommonModule/*' file='Doc/CommonModuleLib.xml'/>
	public partial class CommonModule
	{
		#region Public Methods

		// Executes the specified method.
		/// <include path='items/Init/*' file='Doc/CommonModuleLib.xml'/>
		public bool Init(int processID, int taskID, string actionName
			, string dataConfigName)
		{
			bool retValue = true;

			mLogFileSpec = GetProcessLogFileSpec(processID, true);
			string errorText = CheckDependencies();
			if (NetString.HasValue(errorText))
			{
				retValue = false;
				File.AppendAllText(mLogFileSpec, errorText);
			}

			if (retValue)
			{
				// Reset the configuration if the processID changes.
				ResetConfiguration(processID, dataConfigName);
				mLogFileSpec = GetProcessLogFileSpec(processID);

				retValue = DoTransforms(taskID, actionName);
				if (retValue)
				{
					retValue = DoSourceActions(taskID, actionName);
				}
			}
			return retValue;
		}

		// Check for Depencency files.
		private string CheckDependencies()
		{
			string retValue = "";

			string fileTypeName = "Dependency";

			string fileSpec = "LJC.Net.Common.dll";
			if (!File.Exists(fileSpec))
			{
				retValue += $"{fileTypeName} '{fileSpec}' is not found.";
			}
			retValue += CheckFile("LJC.DataTransformProcess.exe", fileTypeName);
			retValue += CheckFile("LJCDataTransformDAL.dll", fileTypeName);
			retValue += CheckFile("LJC.TextDataReaderLib.dll", fileTypeName);
			return retValue;
		}

		// Checks for file.
		private string CheckFile(string fileSpec, string fileTypeName = "File")
		{
			string retValue = null;

			if (!NetString.HasValue(fileSpec))
			{
				retValue = $"{fileTypeName} name is missing.";
			}
			else
			{
				if (!File.Exists(fileSpec))
				{
					retValue = $"{fileTypeName} '{fileSpec}' was not found.";
				}
			}
			return retValue;
		}

		// Resets the process data configuration.
		private bool ResetConfiguration(int processID, string dataConfigName)
		{
			bool retValue = true;

			if (processID != mProcessID)
			{
				// Set the work variables.
				mProcessID = processID;
				mDbServiceRef = new DbServiceRef()
				{
					DbService = new DbService()
				};
				mDataConfigName = dataConfigName;

				// Set the manager work variables.
				mProcessManager = new DataProcessManager(mDbServiceRef, mDataConfigName);

				mSourceManager = new DataSourceManager(mDbServiceRef, mDataConfigName);
				mLayoutManager = new SourceLayoutManager(mDbServiceRef, mDataConfigName);
				mLayoutColumnManager = new LayoutColumnManager(mDbServiceRef
					, mDataConfigName);
				mDataTypeManager = new DataTypeManager(mDbServiceRef, mDataConfigName);
				mTaskTransformManager = new TaskTransformManager(mDbServiceRef
					, mDataConfigName);
				mTransformMatchManager = new TransformMatchManager(mDbServiceRef
					, mDataConfigName);
				mTransformMapManager = new TransformMapManager(mDbServiceRef
					, mDataConfigName);
			}
			return retValue;
		}
		#endregion

		#region Action Methods

		// Executes the DataSource actions.
		private bool DoSourceActions(int taskID, string actionName)
		{
			string itemName;
			string message;
			bool retValue = true;

			DataSources dataSources = mSourceManager.LoadWithTaskID(taskID);
			if (dataSources != null)
			{
				foreach (DataSource dataSource in dataSources)
				{
					itemName = GetTextItemName(dataSource.SourceItemName);

					switch (actionName)
					{
						case "VerifyTextColumns":
							retValue = VerifyTextColumns(dataSource);
							if (retValue)
							{
								message = "Verify Text Columns for '{0}' successful.";
								WriteLogLine(message, itemName);
							}
							break;

						case "VerifyTextData":
							retValue = VerifyTextData(dataSource);
							if (retValue)
							{
								message = "Verify Text Data for file '{0}' successful.";
								WriteLogLine(message, itemName);
							}
							break;

						case "CreateTable":
							retValue = CreateTable(dataSource);
							if (retValue)
							{
								message = "Create Table '{0}' successful.";
								WriteLogLine(message, dataSource.SourceItemName);
							}
							break;

						case "ParseAddress":
							retValue = ParseAddress(dataSource);
							if (retValue)
							{
								message = "Parse Address for Table '{0}' successful.";
								WriteLogLine(message, dataSource.SourceItemName);
							}
							break;
					}
				}
			}
			return retValue;
		}

		// Executes the Transform actions.
		private bool DoTransforms(int taskID, string actionName)
		{
			DataSource sourceData;
			DataSource targetData;
			string itemName;
			string message;
			bool retValue = true;

			TaskTransforms taskTransforms = mTaskTransformManager.LoadWithTaskID(taskID);
			if (taskTransforms != null)
			{
				foreach (TaskTransform taskTransform in taskTransforms)
				{
					sourceData = RetrieveDataSource(taskTransform.SourceDataID);
					targetData = RetrieveDataSource(taskTransform.TargetDataID);

					switch (actionName)
					{
						case "UploadTable":
							retValue = UploadData(taskTransform);
							if (retValue)
							{
								itemName = GetTextItemName(sourceData.SourceItemName);
								message = "Upload table '{0}' from file '{1}' successful.";
								WriteLogLine(message, targetData.SourceItemName, itemName);
							}
							break;

						case "MatchTable":
							retValue = MatchTable(taskTransform);
							if (retValue)
							{
								message = "Match table '{0}' to '{1}' successful.";
								WriteLogLine(message, sourceData.SourceItemName
									, targetData.SourceItemName);
							}
							break;

						case "MergeTable":
							retValue = MergeTable(taskTransform);
							if (retValue)
							{
								message = "Merge table '{0}' to '{1}' successful.";
								WriteLogLine(message, sourceData.SourceItemName
									, targetData.SourceItemName);
							}
							break;
					}
				}
			}
			return retValue;
		}
		#endregion

		#region Transform Actions

		// Matches the data tables.
		private bool MatchTable(TaskTransform taskTransform)
		{
			bool retValue = true;

			while (retValue)
			{
				retValue = GetTransformData(taskTransform, out DataSource _
					, out DataSource targetDataSource);
				if (!retValue)
				{
					break;
				}

				string matchSql = CreateMatchFlagSql(taskTransform, out retValue);
				if (NetString.HasValue(matchSql))
				{
					DataManager dataManager = new DataManager(mDbServiceRef
						, targetDataSource.DataConfigName, null);

					try
					{
						dataManager.ExecuteClientSql(RequestType.ExecuteSQL, matchSql);
					}
					catch (Exception e)
					{
						retValue = false;
						WriteLogLine(e.Message);
						WriteLogLine("Match SQL:\r\n{0}", matchSql);
					}
				}
				break;
			}
			return retValue;
		}

		// Merges the data tables.
		private bool MergeTable(TaskTransform taskTransform)
		{
			bool retValue = true;

			while (retValue)
			{
				retValue = GetTransformData(taskTransform, out DataSource _
					, out DataSource targetDataSource);
				if (!retValue)
				{
					break;
				}

				string mergeSql = CreateMergeSql(taskTransform, out retValue);
				if (NetString.HasValue(mergeSql))
				{
					DataManager dataManager = new DataManager(mDbServiceRef
						, targetDataSource.DataConfigName, null);

					try
					{
						dataManager.ExecuteClientSql(RequestType.ExecuteSQL, mergeSql);
					}
					catch (Exception e)
					{
						retValue = false;
						WriteLogLine(e.Message);
						WriteLogLine("Merge SQL:\r\n{0}", mergeSql);
						break;
					}

					string insertSql = CreateInsertSql(taskTransform, out retValue);
					if (NetString.HasValue(mergeSql))
					{
						try
						{
							dataManager.ExecuteClientSql(RequestType.ExecuteSQL, insertSql);
						}
						catch (Exception e)
						{
							retValue = false;
							WriteLogLine(e.Message);
							WriteLogLine("Insert SQL:\r\n{0}", insertSql);
							break;
						}
					}
					break;
				}
			}
			return retValue;
		}

		// Uploads the file data to a table.
		private bool UploadData(TaskTransform taskTransform)
		{
			Dictionary<string, string> transformMappings = null;
			bool retValue = true;

			while (retValue)
			{
				retValue = GetTransformData(taskTransform, out DataSource sourceDataSource
					, out DataSource targetDataSource);
				if (!retValue)
				{
					break;
				}

				string connectionString = GetConnectionString(targetDataSource.DataConfigName
					, out string errorText);
				if (NetString.HasValue(errorText))
				{
					retValue = false;
					WriteLogLine(errorText);
					break;
				}

				ColumnMappings columnMappings
					= CreateTransformMappings(taskTransform.TransformID, out retValue);
				if (null == columnMappings)
				{
					retValue = false;
					WriteLogLine("Missing Column Mapping for '{0}'.", taskTransform.Name);
				}

				if (retValue)
				{
					transformMappings = new Dictionary<string, string>();
					foreach (ColumnMapping columnMapping in columnMappings)
					{
						transformMappings.Add(columnMapping.SourceColumn.Name
							, columnMapping.TargetColumn.Name);
					}
				}

				if (retValue)
				{
					retValue = BulkCopy(sourceDataSource.SourceItemName
						, targetDataSource.SourceItemName, connectionString, out errorText
						, transformMappings);
					if (NetString.HasValue(errorText))
					{
						retValue = false;
						WriteLogLine(errorText);
					}
				}
				break;
			}
			return retValue;
		}
		#endregion

		#region DataSource Actions.

		// Creates the Source target table.
		private bool CreateTable(DataSource dataSource)
		{
			bool retValue = true;

			DataManager dataManager = new DataManager(mDbServiceRef
				, dataSource.DataConfigName, null);
			string createSql = GetCreateTableSql(dataSource);

			try
			{
				dataManager.ExecuteClientSql(RequestType.ExecuteSQL, createSql);
			}
			catch (Exception e)
			{
				retValue = false;
				WriteLogLine(e.Message);
				WriteLogLine("Create Table SQL:\r\n{0}", createSql);
			}
			return retValue;
		}

		// Parses the Address Data columns.
		private bool ParseAddress(DataSource dataSource)
		{
			bool retValue = true;

			string tableName = dataSource.SourceItemName;
			DataManager dataManager = new DataManager(mDbServiceRef
				, dataSource.DataConfigName, tableName);

			DbResult dbResult = GetAddressData(dataManager);

			if (DbResult.HasRows(dbResult))
			{
				StandardAddress standardAddress = new StandardAddress();
				foreach (DbRow dbRow in dbResult.Rows)
				{
					DbValues dbValues = dbRow.Values;
					string addressLine1 = dbValues.LJCGetString("AddressLine1").ToString();
					standardAddress.ParseDeliveryAddressLine(addressLine1);

					UpdateAddress(dataManager, standardAddress, addressLine1, tableName);
				}
			}
			return retValue;
		}

		// Verifies the text source columns.
		private bool VerifyTextColumns(DataSource dataSource)
		{
			LayoutColumns layoutColumns;
			LayoutColumn layoutColumn;
			string message;
			bool retValue = true;

			while (true)
			{
				if (null == dataSource)
				{
					retValue = false;
					WriteLogLine("VerifyTextColumns() source is null.");
					break;
				}

				string sourceName = dataSource.SourceItemName;
				string errorText = CheckFile(sourceName, "Source File");
				if (NetString.HasValue(errorText))
				{
					WriteLogLine(errorText);
					break;
				}

				SourceLayout layout = RetrieveLayout(dataSource.SourceLayoutID);
				if (null == layout)
				{
					retValue = false;
					break;
				}

				layoutColumns = LoadLayoutColumns(dataSource.SourceLayoutID);
				if (null == layoutColumns)
				{
					retValue = false;
					break;
				}

				// Get the source columns.
				mTextDataReader = new TextDataReader();
				mTextDataReader.LJCSetFile(sourceName);
				string[] fieldNames = mTextDataReader.LJCGetFieldNames();
				if (fieldNames.Length != layoutColumns.Count)
				{
					retValue = false;
					message = "File '{0}' field count ({1}) does not equal layout '{2}'"
						+ " column count ({3}).";
					WriteLogLine(message, sourceName, fieldNames.Length
						, layout.Name, layoutColumns.Count);
				}

				// This code does not match file sequence to LayoutColumn sequence.
				//for (int index = 0; index < fieldNames.Length; index++)
				foreach (string fieldName in mTextDataReader.LJCGetFieldNames())
				{
					layoutColumn = layoutColumns.LJCSearchName(fieldName);
					if (null == layoutColumn)
					{
						retValue = false;
						message = "File '{0}' field '{1}' is not a column in Layout '{2}'.";
						WriteLogLine(message, sourceName, fieldName, layout.Name);
					}
				}
				mTextDataReader.Close();
				break;
			}
			return retValue;
		}

		// Verifies the text source data.
		private bool VerifyTextData(DataSource dataSource)
		{
			LayoutColumns layoutColumns;
			LayoutColumn layoutColumn;
			string message;
			bool retValue = true;

			while (true)
			{
				if (null == dataSource)
				{
					retValue = false;
					WriteLogLine("VerifyTextData() source is null.");
					break;
				}

				string sourceName = dataSource.SourceItemName;
				string errorText = CheckFile(sourceName, "Source File");
				if (NetString.HasValue(errorText))
				{
					WriteLogLine(errorText);
					break;
				}

				SourceLayout layout = RetrieveLayout(dataSource.SourceLayoutID);
				if (null == layout)
				{
					retValue = false;
					break;
				}

				layoutColumns = LoadLayoutColumns(dataSource.SourceLayoutID);
				if (null == layoutColumns)
				{
					retValue = false;
					break;
				}

				// Get the source columns.
				int rowNumber = 0;
				mTextDataReader = new TextDataReader();
				mTextDataReader.LJCSetFile(sourceName);
				string[] fieldNames = mTextDataReader.LJCGetFieldNames();
				if (fieldNames.Length != layoutColumns.Count)
				{
					retValue = false;
					message = "File '{0}' field count ({1}) does not equal "
						+ "layout '{2}' column count ({3}).";
					WriteLogLine(message, sourceName, fieldNames.Length
						, layout.Name, layoutColumns.Count);
				}

				try
				{
					while (mTextDataReader.Read())
					{
						// This code does not match file sequence to LayoutColumn sequence.
						rowNumber++;
						foreach (string fieldName in mTextDataReader.LJCGetFieldNames())
						{
							layoutColumn = layoutColumns.LJCSearchName(fieldName);
							if (layoutColumn != null)
							{
								if (!CheckTextData(fieldName, layoutColumn, rowNumber))
								{
									retValue = false;
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					message = "File: {0} Row {1}\r\n  {2}";
					rowNumber++;
					WriteLogLine(message, sourceName, rowNumber, ex.Message);
				}
				mTextDataReader.Close();
				break;
			}
			return retValue;
		}
		#endregion

		#region Supporting Methods

		// Performs the text to table bulk copy.
		private bool BulkCopy(string textFileSpec, string targetTableName
			, string connString, out string errorText
			, Dictionary<string, string> mappings = null)
		{
			bool retValue = true;

			errorText = null;
			SqlBulkCopyOptions copyOptions = SqlBulkCopyOptions.TableLock
				| SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.UseInternalTransaction;

			try
			{
				TextDataReader textDataReader = new TextDataReader();
				textDataReader.LJCSetFile(textFileSpec);

				using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connString, copyOptions))
				{
					bulkCopy.DestinationTableName = targetTableName;
					CreateBulkCopyMappings(bulkCopy.ColumnMappings, mappings);
					try
					{
						bulkCopy.WriteToServer(textDataReader);
					}
					catch (Exception ex)
					{
						retValue = false;
						errorText = ex.Message;
					}
					finally
					{
						bulkCopy.Close();
						textDataReader.Close();
					}
				}
			}
			catch (Exception e)
			{
				retValue = false;
				errorText = e.Message;
			}
			return retValue;
		}

		// Creates the ColumnMappings.
		private void CreateBulkCopyMappings(SqlBulkCopyColumnMappingCollection columnMappings
			, Dictionary<string, string> transformMappings)
		{
			if (transformMappings != null)
			{
				KeyValuePair<string, string> valuePair;
				for (int index = 0; index < transformMappings.Count; index++)
				{
					valuePair = transformMappings.ElementAt(index);
					columnMappings.Add(valuePair.Key, valuePair.Value);
				}
			}
		}

		// Creates and returns the Column list.
		private string CreateColumnList(ColumnMappings columnMappings
			, bool includeParens = false, bool useToSource = false)
		{
			string retValue;

			StringBuilder builder = new StringBuilder(128);
			bool first = true;
			foreach (ColumnMapping columnMapping in columnMappings)
			{
				string sourceColumnName = columnMapping.SourceColumn.Name;
				string targetColumnName = columnMapping.TargetColumn.Name;
				string columnName = sourceColumnName;
				if (useToSource)
				{
					columnName = targetColumnName;
				}

				if ((int)MapType.Merge == columnMapping.MapTypeID
					|| (int)MapType.Overwrite == columnMapping.MapTypeID
					|| (int)MapType.InsertInclude == columnMapping.MapTypeID)
				{
					if (first)
					{
						builder.Append(" ");
						if (includeParens)
						{
							builder.Append("(");
						}
					}
					else
					{
						builder.Append(", ");
					}
					first = false;

					builder.Append($"{columnName}");
				}
			}
			if (includeParens)
			{
				builder.Append(")");
			}
			builder.AppendLine();
			retValue = builder.ToString();
			return retValue;
		}

		// Creates and returns the Insert SQL.
		private string CreateInsertSql(TaskTransform taskTransform, out bool success)
		{
			string retValue = null;

			success = true;
			while (success)
			{
				success = GetTransformData(taskTransform, out DataSource sourceDataSource
					, out DataSource targetDataSource);
				if (!success)
				{
					break;
				}

				ColumnMappings columnMappings
					= CreateTransformMappings(taskTransform.TransformID, out success);

				StringBuilder builder = new StringBuilder(128);
				builder.AppendLine($"insert into {targetDataSource.SourceItemName}");
				builder.Append(CreateColumnList(columnMappings, true, true));
				builder.AppendLine("select");
				builder.Append(CreateColumnList(columnMappings));
				builder.AppendLine($"from {sourceDataSource.SourceItemName}");
				builder.AppendLine("where MatchFlag = 0 or MatchFlag is null");
				retValue = builder.ToString();
				break;
			}
			return retValue;
		}

		// Creates and returns the MatchFlag SQL.
		private string CreateMatchFlagSql(TaskTransform taskTransform
			, out bool success)
		{
			string retValue = null;

			success = true;
			while (success)
			{
				success = GetTransformData(taskTransform, out DataSource sourceDataSource
					, out DataSource targetDataSource);
				if (!success)
				{
					break;
				}

				ColumnMappings matchMappings
					= CreateMatchMappings(taskTransform.TransformID, out success);

				if (success && matchMappings != null)
				{
					StringBuilder builder = new StringBuilder(64);
					builder.AppendLine($"update {targetDataSource.SourceItemName} set");
					builder.AppendLine(" MatchFlag = 1");
					builder.AppendLine($"from {sourceDataSource.SourceItemName} s");
					builder.AppendLine($"left join {targetDataSource.SourceItemName} t");

					bool first = true;
					foreach (ColumnMapping matchMapping in matchMappings)
					{
						builder.Append(first ? " on " : " and ");
						first = false;
						builder.Append($"t.{matchMapping.SourceColumn} ="
							+ $" s.{matchMapping.TargetColumn}");
						builder.AppendLine();
					}
					retValue = builder.ToString();
				}
				break;
			}
			return retValue;
		}

		// Creates the Match mappings.
		private ColumnMappings CreateMatchMappings(int transformID
			, out bool success)
		{
			LayoutColumn sourceColumn;
			LayoutColumn targetColumn;
			ColumnMappings retValue = null;

			success = true;
			TransformMatches transformMatches
				= mTransformMatchManager.LoadWithTransformID(transformID);
			if (transformMatches != null)
			{
				retValue = new ColumnMappings();
				foreach (TransformMatch transformMatch in transformMatches)
				{
					sourceColumn = RetrieveLayoutColumn(transformMatch.SourceColumnID);
					if (null == sourceColumn)
					{
						success = false;
						break;
					}
					targetColumn = RetrieveLayoutColumn(transformMatch.TargetColumnID);
					if (null == targetColumn)
					{
						success = false;
						break;
					}
					if (null == sourceColumn || null == targetColumn)
					{
						success = false;
						WriteLogLine("Source Column: {0} or Target Column: {1} is null."
							, transformMatch.SourceColumnID, transformMatch.TargetColumnID);
						break;
					}
					retValue.Add(sourceColumn, targetColumn);
				}
			}
			return retValue;
		}

		// Creates and returns the Merge SQL.
		private string CreateMergeSql(TaskTransform taskTransform, out bool success)
		{
			string retValue = null;

			success = true;
			while (success)
			{
				success = GetTransformData(taskTransform, out DataSource sourceDataSource
					, out DataSource targetDataSource);
				if (!success)
				{
					break;
				}

				ColumnMappings columnMappings
					= CreateTransformMappings(taskTransform.TransformID, out success);

				StringBuilder builder = new StringBuilder(128);
				builder.AppendLine($"update {targetDataSource.SourceItemName} set");
				bool first = true;
				foreach (ColumnMapping columnMapping in columnMappings)
				{
					string sourceColumnName = columnMapping.SourceColumn.Name;
					string targetColumnName = columnMapping.TargetColumn.Name;

					if ((int)MapType.Merge == columnMapping.MapTypeID
						|| (int)MapType.Overwrite == columnMapping.MapTypeID)
					{
						if (!first)
						{
							builder.AppendLine(",");
						}
						first = false;
					}

					switch (columnMapping.MapTypeID)
					{
						case (int)MapType.Merge:
							// if Target is empty then use Source else Target.
							builder.AppendLine($" {targetColumnName} = case when t.{targetColumnName}"
								+ $" is null or Len(t.{targetColumnName}) = 0 ");
							builder.Append($"  then s.{sourceColumnName} else t.{targetColumnName} end");
							break;

						case (int)MapType.Overwrite:
							// Is Source is not empty, use Source, else Target.
							builder.AppendLine($" {targetColumnName} = case when"
								+ $" len(s.{sourceColumnName}) > 0 ");
							builder.Append($"  then s.{sourceColumnName} else t.{targetColumnName} end");
							break;
					}
				}
				builder.AppendLine();
				builder.Append($"FROM {sourceDataSource.SourceItemName} s");
				builder.AppendLine();
				builder.Append($"left join {targetDataSource.SourceItemName} t");
				builder.AppendLine();

				ColumnMappings matchMappings
					= CreateMatchMappings(taskTransform.TransformID, out success);

				first = true;
				foreach (ColumnMapping matchMapping in matchMappings)
				{
					builder.Append(first ? " on " : " and ");
					first = false;
					builder.Append($"t.{matchMapping.SourceColumn} ="
						+ $" s.{matchMapping.TargetColumn} ");
					builder.AppendLine();
				}
				retValue = builder.ToString();
				break;
			}
			return retValue;
		}

		// Checks the data field for a correct value.
		private bool CheckTextData(string fieldName, LayoutColumn layoutColumn
			, int rowNumber)
		{
			string message;
			bool retValue = true;

			TextDataReader textDataReader = mTextDataReader;
			int index = textDataReader.GetOrdinal(fieldName);
			try
			{
				switch (layoutColumn.DataTypeID)
				{
					case (short)LJCFieldDataType.Boolean:
						bool boolValue = textDataReader.GetBoolean(index);
						break;
					case (short)LJCFieldDataType.Byte:
						byte byteValue = textDataReader.GetByte(index);
						break;
					case (short)LJCFieldDataType.DateTime:
						DateTime datetimeValue = textDataReader.GetDateTime(index);
						break;
					case (short)LJCFieldDataType.Decimal:
						decimal decimalValue = textDataReader.GetDecimal(index);
						break;
					case (short)LJCFieldDataType.Double:
						double doubleValue = textDataReader.GetDouble(index);
						break;
					case (short)LJCFieldDataType.Int16:
						short shortValue = textDataReader.GetInt16(index);
						break;
					case (short)LJCFieldDataType.Int32:
						int intValue = textDataReader.GetInt32(index);
						break;
					case (short)LJCFieldDataType.Int64:
						long longValue = textDataReader.GetInt64(index);
						break;
					case (short)LJCFieldDataType.Single:
						float floatValue = textDataReader.GetFloat(index);
						break;
					case (short)LJCFieldDataType.String:
						string stringValue = textDataReader.GetString(index);
						if (NetString.HasValue(stringValue)
							&& stringValue.Length > layoutColumn.Length)
						{
							retValue = false;
							message = "Row: {0}, Field: '{1}' "
								+ "Data length ({2}) exceeds layout column length ({3}).";
							WriteLogLine(message, rowNumber, fieldName, stringValue.Length
								, layoutColumn.Length);
						}
						break;
				}
			}
			catch (SystemException ex)
			{
				retValue = false;
				message = "Row: {0}, Field: '{1}' ";
				message += ex.Message;
				WriteLogLine(message, rowNumber, fieldName);
			}
			return retValue;
		}

		// Creates the mappings object.
		private ColumnMappings CreateTransformMappings(int transformID
			, out bool success)
		{
			LayoutColumn sourceColumn;
			LayoutColumn targetColumn;
			ColumnMappings retValue = null;

			success = true;
			TransformMaps transformMaps
				= mTransformMapManager.LoadWithTransformID(transformID);
			if (transformMaps != null)
			{
				retValue = new ColumnMappings();
				foreach (TransformMap transformMap in transformMaps)
				{
					sourceColumn = RetrieveLayoutColumn(transformMap.SourceColumnID);
					if (null == sourceColumn)
					{
						success = false;
						break;
					}
					targetColumn = RetrieveLayoutColumn(transformMap.TargetColumnID);
					if (null == targetColumn)
					{
						success = false;
						break;
					}
					if (null == sourceColumn || null == targetColumn)
					{
						success = false;
						WriteLogLine("Source Column: {0} or Target Column: {1} is null."
							, transformMap.SourceColumnID, transformMap.TargetColumnID);
						break;
					}
					retValue.Add(sourceColumn, targetColumn, transformMap.MapTypeID);
				}
			}
			return retValue;
		}

		// Gets the Address Data.
		private DbResult GetAddressData(DataManager dataManager)
		{
			DbResult retValue;

			List<string> propertyNames = new List<string>()
			{
				"AddressLine1"
			};

			try
			{
				retValue = dataManager.Load(propertyNames: propertyNames);
			}
			catch (Exception e)
			{
				retValue = null;
				WriteLogLine(e.Message);
			}
			return retValue;
		}

		// Gets the Connection string.
		private string GetConnectionString(string dataConfigName
		, out string errorText)
		{
			DataConfig dataConfig = null;
			string retValue = null;

			// Get the specified DataConfig.
			errorText = null;
			DataConfigs dataConfigs = new DataConfigs();
			dataConfigs.LJCLoadData();
			if (dataConfigs != null)
			{
				dataConfig = dataConfigs.LJCGetByName(dataConfigName);
			}
			dataConfig?.GetConnectionString();
			return retValue;
		}

		// Builds and returns the Create Table SQL statement.
		private string GetCreateTableSql(DataSource dataSource)
		{
			LayoutColumns layoutColumns;
			LayoutColumns layoutPrimaryColumns;
			StringBuilder builder = null;
			string retValue;

			// Retrieve LayoutColumns.
			layoutColumns = LoadLayoutColumns(dataSource.SourceLayoutID);
			if (layoutColumns != null)
			{
				LayoutColumns primaryColumns = new LayoutColumns();

				// Add SQL statement beginning.
				string tableName = dataSource.SourceItemName;
				builder = new StringBuilder();
				builder.AppendLine("IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES");
				builder.AppendLine($" WHERE TABLE_NAME = N'{tableName}')");
				builder.AppendLine("BEGIN");
				builder.AppendLine($"CREATE TABLE[dbo].[{tableName}] (");

				// Create Column entries.
				bool first = true;
				foreach (LayoutColumn layoutColumn in layoutColumns)
				{
					DataType dataType = RetrieveDataType(layoutColumn.DataTypeID);
					if (!first)
					{
						builder.AppendLine(",");
					}
					first = false;
					builder.Append($"  [{layoutColumn.Name}]");
					builder.Append($" [{dataType.SQLName}]");

					if (layoutColumn.IdentityKey)
					{
						builder.Append(" IDENTITY(1,1)");
					}
					if ("String" == dataType.Name)
					{
						builder.Append($"({layoutColumn.Length})");
					}
					if (!layoutColumn.AllowNull)
					{
						builder.Append(" NOT");
					}
					builder.Append(" NULL");
				}

				// Get defined Primary Key columns.
				layoutPrimaryColumns = mLayoutColumnManager.LoadWithPrimaryKey(dataSource.SourceLayoutID);
				if (layoutPrimaryColumns != null)
				{
					foreach (LayoutColumn layoutColumn in layoutPrimaryColumns)
					{
						primaryColumns.Add(layoutColumn);
					}
				}

				// Add SQL statement Primary Key constraint.
				if (NetCommon.HasItems(primaryColumns))
				{
					builder.AppendLine(",");
					builder.AppendLine($"  CONSTRAINT [PK_{tableName}] PRIMARY KEY CLUSTERED");
					builder.AppendLine("  (");
					foreach (LayoutColumn primaryColumn in primaryColumns)
					{
						builder.AppendLine($"    [{primaryColumn.Name}] ASC");
					}
					builder.AppendLine("  )");
				}

				// Add SQL statement ending.
				builder.AppendLine(")");
				builder.AppendLine("END");
			}

			// Get completed SQL statement.
			retValue = builder.ToString();
			return retValue;
		}

		// Gets the text item name.
		private string GetTextItemName(string sourceItemName)
		{
			string retValue = null;

			if (NetString.HasValue(sourceItemName))
			{
				int index = sourceItemName.LastIndexOf(@"\");
				if (index > -1)
				{
					retValue = sourceItemName.Substring(index + 1);
				}
			}
			return retValue;
		}

		// Gets the Transform from and to sources.
		private bool GetTransformData(TaskTransform taskTransform
			, out DataSource sourceDataSource, out DataSource targetDataSource)
		{
			bool retValue = true;

			sourceDataSource = null;
			targetDataSource = null;
			while (retValue)
			{
				sourceDataSource = RetrieveDataSource(taskTransform.SourceDataID);
				if (null == sourceDataSource)
				{
					retValue = false;
					break;
				}

				targetDataSource = RetrieveDataSource(taskTransform.TargetDataID);
				if (null == targetDataSource)
				{
					retValue = false;
					break;
				}
				break;
			}
			return retValue;
		}

		// Updates the Address with parsed data.
		private bool UpdateAddress(DataManager dataManager
			, StandardAddress standardAddress, string addressLine1, string tableName)
		{
			StringBuilder builder;
			bool retValue = true;

			var address = standardAddress;
			builder = new StringBuilder(64);
			builder.AppendLine($"update {tableName} set ");
			builder.AppendLine($" DeliveryLine = '{address.DeliveryAddressLine}', ");
			builder.AppendLine($" AddressNumber = '{address.AddressNumber}', ");
			builder.AppendLine($" Predirectional = '{address.PreDirectional}', ");
			builder.AppendLine($" StreetName = '{address.StreetName}', ");
			builder.AppendLine($" Postdirectional = '{address.PostDirectional}', ");
			builder.AppendLine($" Suffix = '{address.Suffix}', ");
			builder.AppendLine($" UnitType = '{address.UnitType}', ");
			builder.AppendLine($" UnitNumber = '{address.UnitNumber}' ");
			builder.AppendLine($"where AddressLine1 = '{addressLine1}' ");
			string sql = builder.ToString();

			try
			{
				dataManager.ExecuteClientSql(RequestType.ExecuteSQL, sql);
			}
			catch (Exception e)
			{
				retValue = false;
				WriteLogLine(e.Message);
				WriteLogLine("Update Address SQL:\r\n{0}", sql);
			}
			return retValue;
		}
		#endregion

		#region Get Data Methods

		// Gets the LayoutColumns collection object.
		private LayoutColumns LoadLayoutColumns(int layoutID)
		{
			LayoutColumns retValue;

			retValue = mLayoutColumnManager.LoadWithLayoutID(layoutID);
			return retValue;
		}

		// Gets the DataProcess object.
		private DataProcess RetrieveDataProcess(int dataProcessID)
		{
			DataProcess retValue;

			retValue = mProcessManager.RetrieveWithID(dataProcessID);
			return retValue;
		}

		// Gets the DataSource object.
		private DataSource RetrieveDataSource(int dataSourceID)
		{
			DataSource retValue;

			retValue = mSourceManager.RetrieveWithID(dataSourceID);
			return retValue;
		}

		// Gets the DataType object.
		private DataType RetrieveDataType(short dataTypeID)
		{
			DataType retValue;

			retValue = mDataTypeManager.RetrieveWithID(dataTypeID);
			return retValue;
		}

		// Gets the SourceLayout object.
		private SourceLayout RetrieveLayout(int layoutID)
		{
			SourceLayout retValue;

			retValue = mLayoutManager.RetrieveWithID(layoutID);
			return retValue;
		}

		// Gets the LayoutColumn object.
		private LayoutColumn RetrieveLayoutColumn(short layoutColumnID)
		{
			LayoutColumn retValue;

			retValue = mLayoutColumnManager.RetrieveWithID(layoutColumnID);
			return retValue;
		}
		#endregion

		#region Write Log Methods

		// Gets the Process log file specification.
		private string GetProcessLogFileSpec(int processID, bool defaultName = false)
		{
			// Note: Also in: LJCTransformService.cs, LJCTransformProcess.cs,
			//			 ProcessGroupModule.cs and TaskSourceModule.cs.
			string retValue;

			retValue = $"Logs\\DataProcess{processID}.txt";
			if (!defaultName)
			{
				if (null == mDataProcess)
				{
					mDataProcess = RetrieveDataProcess(processID);
				}
				if (mDataProcess != null)
				{
					retValue = $"Logs\\{mDataProcess.Name}Process.txt";
				}
			}
			return retValue;
		}

		// Writes a string format to the Log file with the current date and time.
		private void WriteLog(string formatText, params object[] parameters)
		{
			NetFile.WriteLog(mLogFileSpec, formatText, parameters);
		}

		//// Writes a blank line.
		//private void WriteLogBlankLine()
		//{
		//	WriteLog("\r\n");
		//}

		// Writes string format plus cr/lf to Log file with current date and time.
		private void WriteLogLine(string formatText = null
			, params object[] parameters)
		{
			if (NetString.HasValue(formatText))
			{
				formatText += "\r\n";
				WriteLog(formatText, parameters);
			}
		}
		#endregion

		#region Class Data

		private string mDataConfigName;
		private DbServiceRef mDbServiceRef;

		private DataTypeManager mDataTypeManager;
		private LayoutColumnManager mLayoutColumnManager;
		private SourceLayoutManager mLayoutManager;
		private DataProcessManager mProcessManager;
		private DataSourceManager mSourceManager;
		private TaskTransformManager mTaskTransformManager;
		private TransformMapManager mTransformMapManager;
		private TransformMatchManager mTransformMatchManager;

		private DataProcess mDataProcess;
		private string mLogFileSpec;
		private int mProcessID;
		private TextDataReader mTextDataReader;
		#endregion
	}
}
