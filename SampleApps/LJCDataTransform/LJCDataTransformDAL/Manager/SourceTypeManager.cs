// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// SourceTypeManager.cs
using System;
using System.Collections.Generic;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCDataTransformDAL
{
	/// <summary>Provides Table specific data manipulation methods.</summary>
	public class SourceTypeManager
		: ObjectManager<SourceType, SourceTypes>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/SourceTypeManagerC/*' file='Doc/SourceTypeManager.xml'/>
		public SourceTypeManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "SourceType")
			: base(dbServiceRef, dataConfigName, tableName)
		{
			// Create the list of database assigned columns.
			SetDbAssignedColumns(new string[]
				{
					SourceType.ColumnSourceTypeID
				});

			// Create the list of lookup column names.
			SetLookupColumns(new string[]
				{
					SourceType.ColumnName
				});
		}
		#endregion

		#region Retrieve/Load Methods

		// Retrieves a Data Record with the supplied value.
		/// <include path='items/RetrieveWithID/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public SourceType RetrieveWithID(short id, List<string> propertyNames = null)
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
				{ SourceType.ColumnSourceTypeID, id }
			};
			return retValue;
		}
		#endregion
	}
}
