// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TaskTypeManager.cs
using System;
using System.Collections.Generic;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCDataTransformDAL
{
	/// <summary>Provides Table specific data manipulation methods.</summary>
	public class TaskTypeManager
		: ObjectManager<TaskType, TaskTypes>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/TaskTypeManagerC/*' file='Doc/TaskTypeManager.xml'/>
		public TaskTypeManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "TaskType")
			: base(dbServiceRef, dataConfigName, tableName)
		{
			// Create the list of database assigned columns.
			SetDbAssignedColumns(new string[]
				{
					TaskType.ColumnTaskTypeID
				});

			// Create the list of lookup column names.
			SetLookupColumns(new string[]
				{
					TaskType.ColumnName
				});
		}
		#endregion

		#region Retrieve/Load Methods

		// Retrieves a Data Record with the supplied value.
		/// <include path='items/RetrieveWithID/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public TaskType RetrieveWithID(short id, List<string> propertyNames = null)
		{
			var keyColumns = GetIDKey(id);
			return Retrieve(keyColumns, propertyNames);
		}
		#endregion

		#region GetKey Methods

		// Get the ID key record.
		/// <include path='items/GetIDKey/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public DbColumns GetIDKey(short id)
		{
			var retValue = new DbColumns()
			{
				{ TaskType.ColumnTaskTypeID, id }
			};
			return retValue;
		}
		#endregion
	}
}
