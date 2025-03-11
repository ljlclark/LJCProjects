// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProcessGroupManager.cs
using System;
using System.Collections.Generic;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCDataTransformDAL
{
	/// <summary>Provides Table specific data manipulation methods.</summary>
	public class ProcessGroupManager
		: ObjectManager<ProcessGroup, ProcessGroups>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/ProcessGroupManagerC/*' file='Doc/ProcessGroupManager.xml'/>
		public ProcessGroupManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "ProcessGroup")
			: base(dbServiceRef, dataConfigName, tableName)
		{
			// Create the list of database assigned columns.
			SetDbAssignedColumns(new string[]
				{
					ProcessGroup.ColumnProcessGroupID
				});

			// Create the list of lookup column names.
			SetLookupColumns(new string[]
				{
					ProcessGroup.ColumnName
				});
		}
		#endregion

		#region Retrieve/Load Methods

		// Retrieves a Data Record with the supplied value.
		/// <include path='items/RetrieveWithID/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public ProcessGroup RetrieveWithID(int id, List<string> propertyNames = null)
		{
			var keyColumns = GetIDKey(id);
			return Retrieve(keyColumns, propertyNames);
		}
		#endregion

		#region GetKey Methods

		// Get the ID key record.
		/// <include path='items/GetIDKey/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public DbColumns GetIDKey(int id)
		{
			var retValue = new DbColumns()
			{
				{ ProcessGroup.ColumnProcessGroupID, id }
			};
			return retValue;
		}

		// Get the ID key record.
		/// <include path='items/GetNameKey/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public DbColumns GetNameKey(string name)
		{
			var retValue = new DbColumns()
			{
				{ ProcessGroup.ColumnName, (object)name }
			};
			return retValue;
		}
		#endregion
	}
}
