// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProcessGroupProcessManager.cs
using System;
using System.Text;
using LJCDBMessage;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCDataTransformDAL
{
	/// <summary>Provides Table specific data manipulation methods.</summary>
	public class ProcessGroupProcessManager
		: ObjectManager<ProcessGroupProcess, ProcessGroupProcesses>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/ProcessGroupProcessManagerC/*' file='Doc/ProcessGroupProcessManager.xml'/>
		public ProcessGroupProcessManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "ProcessGroupProcess")
			: base(dbServiceRef, dataConfigName, tableName)
		{
			// Create the list of lookup column names.
			// This list must include the database assigned columns
			// to contain the returned DB assigned key values.
			string[] lookupColumnNames = new string[]
			{
					ProcessGroupProcess.ColumnProcessGroupID,
					ProcessGroupProcess.ColumnDataProcessID
			};
			SetLookupColumns(lookupColumnNames);
		}
		#endregion

		#region Retrieve/Load Methods

		// Retrieves a data record with the supplied value.
		/// <include path='items/RetrieveWithIDs/*' file='Doc/ProcessGroupProcessManager.xml'/>
		public ProcessGroupProcess RetrieveWithIDs(int processGroupID, int processID)
		{
			var keyColumns = GetIDKeys(processGroupID, processID);
			return Retrieve(keyColumns);
		}
		#endregion

		#region GetKey Methods

		// Get the ID key record.
		/// <include path='items/GetIDKeys/*' file='Doc/ProcessGroupProcessManager.xml'/>
		public DbColumns GetIDKeys(int processGroupID, int processID)
		{
			var retValue = new DbColumns()
			{
				{ ProcessGroupProcess.ColumnProcessGroupID, processGroupID },
				{ ProcessGroupProcess.ColumnDataProcessID, processID }
			};
			return retValue;
		}
		#endregion

		#region Filters

		// Creates and returns the filters object.
		/// <include path='items/GetMaxSequence/*' file='Doc/ProcessGroupProcessManager.xml'/>
		public ProcessGroupProcess GetMaxSequence(int groupID)
		{
			ProcessGroupProcess retValue;

			// Create SQL statement.
			StringBuilder builder = new StringBuilder(64);
			builder.AppendLine("select ");
			builder.AppendLine(" max(sequence) as Sequence ");
			builder.AppendLine($"from {ProcessGroupProcess.TableName} ");
			builder.Append($"where {ProcessGroupProcess.ColumnProcessGroupID}"
				+ $" = {groupID}");
			string sql = builder.ToString();
			retValue = RetrieveClientSql(sql);
			return retValue;
		}
		#endregion
	}
}
