// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// SourceStatusManager.cs
using System;
using System.Collections.Generic;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCDataTransformDAL
{
	/// <summary>Provides Table specific data manipulation methods.</summary>
	public class SourceStatusManager
		: ObjectManager<SourceStatus, SourceStatuses>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/SourceStatusManagerC/*' file='Doc/SourceStatusManager.xml'/>
		public SourceStatusManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "SourceStatus")
			: base(dbServiceRef, dataConfigName, tableName)
		{
			// Create the list of database assigned columns.
			SetDbAssignedColumns(new string[]
				{
					SourceStatus.ColumnSourceStatusID
				});

			// Create the list of lookup column names.
			SetLookupColumns(new string[]
				{
					SourceStatus.ColumnName
				});
		}
		#endregion

		#region Retrieve/Load Methods

		// Retrieves a Data Record with the supplied value.
		/// <include path='items/RetrieveWithID/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public SourceStatus RetrieveWithID(short id, List<string> propertyNames = null)
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
				{ SourceStatus.ColumnSourceStatusID, id }
			};
			return retValue;
		}
		#endregion
	}
}
