// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProcessStatusManager.cs
using System;
using System.Collections.Generic;
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;

namespace LJCDataTransformDAL
{
	/// <summary>Provides Table specific data manipulation methods.</summary>
	public class ProcessStatusManager
		: ObjectManager<ProcessStatus, ProcessStatuses>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/ObjectManagerC/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public ProcessStatusManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "ProcessStatus")
			: base(dbServiceRef, dataConfigName, tableName)
		{
			// Create the list of database assigned columns.
			SetDbAssignedColumns(new string[]
				{
					ProcessStatus.ColumnProcessStatusID
				});

			// Create the list of lookup column names.
			SetLookupColumns(new string[]
				{
					ProcessStatus.ColumnName
				});
		}
		#endregion

		#region Retrieve/Load Methods

		// Loads a collection of data records ordered by Description.
		/// <include path='items/LoadByDescription/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public ProcessStatuses LoadByDescription(DbColumns keyColumns = null
			, List<string> propertyNames = null, DbFilters filters = null
			, DbJoins joins = null)
		{
			SetOrderByDescription();
			return Load(keyColumns, propertyNames, filters, joins);
		}

		// Retrieves a Data Record with the supplied value.
		/// <include path='items/RetrieveWithID/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public ProcessStatus RetrieveWithID(short id, List<string> propertyNames = null)
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
				{ ProcessStatus.ColumnProcessStatusID, id }
			};
			return retValue;
		}
    #endregion

    #region OrderBys

    /// <summary>Sets the current OrderBy names.</summary>
    public void SetOrderByDescription() => DataManager.OrderByNames = new List<string>()
		{
			ProcessStatus.ColumnDescription
		};
		#endregion
	}
}
