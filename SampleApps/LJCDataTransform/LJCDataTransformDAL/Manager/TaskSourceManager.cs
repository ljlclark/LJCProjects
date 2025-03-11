// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TaskSourceManager.cs
using System;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCDataTransformDAL
{
	/// <summary>Provides Table specific data manipulation methods.</summary>
	public class TaskSourceManager
		: ObjectManager<TaskSource, TaskSources>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/TaskSourceManagerC/*' file='Doc/TaskSourceManager.xml'/>
		public TaskSourceManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "TaskSource")
			: base(dbServiceRef, dataConfigName, tableName)
		{
			// Create the list of lookup column names.
			// This list must include the database assigned columns
			// to contain the returned DB assigned key values.
			string[] lookupColumnNames = new string[]
			{
					TaskSource.ColumnStepTaskID,
					TaskSource.ColumnDataSourceID
			};
			SetLookupColumns(lookupColumnNames);
		}
		#endregion

		#region Retrieve/Load Methods

		// Retrieves a data record with the supplied value.
		/// <include path='items/RetrieveWithIDs/*' file='Doc/TaskSourceManager.xml'/>
		public TaskSource RetrieveWithIDs(int stepTaskID, int dataSourceID)
		{
			var keyColumns = GetIDKeys(stepTaskID, dataSourceID);
			return Retrieve(keyColumns);
		}
		#endregion

		#region GetKey Methods

		// Get the ID key record.
		/// <include path='items/GetIDKeys/*' file='Doc/TaskSourceManager.xml'/>
		public DbColumns GetIDKeys(int stepTaskID, int dataSourceID)
		{
			var retValue = new DbColumns()
			{
				{ TaskSource.ColumnStepTaskID, stepTaskID },
				{ TaskSource.ColumnDataSourceID, dataSourceID }
			};
			return retValue;
		}
		#endregion
	}
}
