// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataProcessManager.cs
using System;
using System.Collections.Generic;
using LJCNetCommon;
using LJCDBMessage;
using LJCDBClientLib;

namespace LJCDataTransformDAL
{
	// Provides DataProcess specific data manipulation methods.
	/// <include path='items/DataProcessManager/*' file='Doc/DataProcessManager.xml'/>
	public class DataProcessManager
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DataProcessManagerC/*' file='Doc/DataProcessManager.xml'/>
		public DataProcessManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "DataProcess")
		{
			DbServiceRef = dbServiceRef;
			DataConfigName = dataConfigName;
			mDataManager = new DataManager(DbServiceRef, DataConfigName, tableName);

			// Create the list of database assigned columns.
			// And make sure the AutoIncrement value is set.
			mDataManager.SetDbAssignedColumns(new string[]
				{
					DataProcess.ColumnDataProcessID
				});

			// Create the list of lookup column names.
			mDataManager.SetLookupColumns(new string[]
				{
					DataProcess.ColumnName
				});

			PageStartIndex = 0;
		}
		#endregion

		#region Public Data Methods

		// Adds a DataProcess record to the database.
		/// <include path='items/Add/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public DataProcess Add(DataProcess dataObject, List<string> propertyNames = null)
		{
			DataProcess retValue = null;

			DbResult dbResult = mDataManager.Add(dataObject, propertyNames);
			SQLStatement = mDataManager.SQLStatement;
			AffectedCount = mDataManager.AffectedCount;
			if (DbResult.HasRows(dbResult))
			{
				// Populate a data object with the result values.
				retValue = new DataProcess();
				DbCommon.SetObjectValues(dbResult.Rows[0].Values, retValue);
			}
			return retValue;
		}

		// Deletes a record with the specified ID.
		/// <include path='items/Delete/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public void Delete(DbColumns keyColumns, DbFilters filters = null)
		{
			mDataManager.Delete(keyColumns, filters);
			SQLStatement = mDataManager.SQLStatement;
			AffectedCount = mDataManager.AffectedCount;
		}

		// Returns a set of columns that match the supplied list.
		/// <include path='items/GetColumns/*' file='Doc/DataProcessManager.xml'/>
		public DbColumns GetColumns(List<string> columnNames)
		{
			return mDataManager.DataDefinition.LJCGetColumns(columnNames);
		}

		// Retrieves a collection of data records.
		/// <include path='items/Load/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public DataProcesses Load(DbColumns keyColumns = null, List<string> propertyNames = null
			, DbFilters filters = null, DbJoins joins = null)
		{
			DataProcesses retValue = null;

			DbResult dbResult = mDataManager.Load(keyColumns, propertyNames, filters
				, joins);
			SQLStatement = mDataManager.SQLStatement;
			if (DbResult.HasRows(dbResult))
			{
				if (dbResult.Rows.Count > 0)
				{
					// Populate a collection with the result records.
					retValue = CreateCollection(dbResult);
				}
			}
			return retValue;
		}

		// Retrieves a DataProcess record from the database.
		/// <include path='items/Retrieve/*' file='Doc/DataProcessManager.xml'/>
		public DataProcess Retrieve(DbColumns keyColumns, List<string> columnNames = null
			, DbFilters filters = null, DbJoins joins = null)
		{
			DataProcess retValue = null;

			DbResult dbResult = mDataManager.Retrieve(keyColumns, columnNames, filters
				, joins);
			SQLStatement = mDataManager.SQLStatement;
			if (DbResult.HasRows(dbResult))
			{
				// Populate a data object with the result values.
				// Uses retValue as an object and processes with reflection.
				retValue = new DataProcess();
				DbCommon.SetObjectValues(dbResult.Rows[0].Values, retValue);
			}
			return retValue;
		}

		// Updates the DataProcess record.
		/// <include path='items/Update/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public void Update(DataProcess dataObject, DbColumns keyColumns
			, List<string> propertyNames = null, DbFilters filters = null)
		{
			mDataManager.Update(dataObject, keyColumns, propertyNames, filters);
			SQLStatement = mDataManager.SQLStatement;
			AffectedCount = mDataManager.AffectedCount;
		}
		#endregion

		#region Retrieve/Load Methods

		// Loads the DataProcess records that are not in the group.
		/// <include path='items/LoadNotInGroup/*' file='Doc/DataProcessManager.xml'/>
		public DataProcesses LoadNotInGroup()
		{
			DataProcesses retValue;

			DbJoins dbJoins = GetGroupIDJoins(true);
			DbFilters dbFilters = GetNotInGroupFilter();
			SetOrderBySequence();
			retValue = Load(joins: dbJoins, filters: dbFilters);
			return retValue;
		}

		// Loads the DataProcess records with join on GroupID.
		/// <include path='items/LoadWithGroupID/*' file='Doc/DataProcessManager.xml'/>
		public DataProcesses LoadWithGroupID(int groupID)
		{
			DataProcesses retValue;

			DbJoins dbJoins = GetGroupIDJoins();
			DbFilters dbFilters = GetGroupIDJoinFilter(groupID, 0);
			SetOrderBySequence();
			retValue = Load(joins: dbJoins, filters: dbFilters);
			return retValue;
		}

		// Retrieves the DataProcess record with join on GroupID.
		/// <include path='items/RetrieveWithGroupID/*' file='Doc/DataProcessManager.xml'/>
		public DataProcess RetrieveWithGroupID(int groupID, int processID)
		{
			DataProcess retValue;

			DbJoins dbJoins = GetGroupIDJoins();
			DbFilters dbFilters = GetGroupIDJoinFilter(groupID, processID);
			SetOrderBySequence();
			retValue = Retrieve(null, joins: dbJoins, filters: dbFilters);
			return retValue;
		}

		// Retrieves a Data Record with the supplied value.
		/// <include path='items/RetrieveWithID/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public DataProcess RetrieveWithID(int id, List<string> propertyNames = null)
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
				{ DataProcess.ColumnDataProcessID, id }
			};
			return retValue;
		}

		// Get the ID key record.
		/// <include path='items/GetNameKey/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public DbColumns GetNameKey(string name)
		{
			var retValue = new DbColumns()
			{
				{ DataProcess.ColumnName, (object)name }
			};
			return retValue;
		}
		#endregion

		#region Joins

		// Get Join on GroupID.
		/// <include path='items/GetGroupIDJoins/*' file='Doc/DataProcessManager.xml'/>
		public DbJoins GetGroupIDJoins(bool outerJoin = false)
		{
			DbJoins retValue = new DbJoins();

			// Note: JoinOn Columns must have properties in the Data Object
			// to recieve the join values.
			// The RenameAs property is required if there is another table column
			// with the same name.
			// dbColumns.Add(string columnName, string propertyName = null
			// , string renameAs = null, string dataTypeName = "String", string caption = null) 

			// [ProcessGroupProcess].[Sequence]
			// left join [ProcessGroupProcess]
			//   on (([Process].[ProcessID] = [ProcessGroupProcess].[ProcessID]))
			DbJoin dbJoin = new DbJoin()
			{
				TableName = ProcessGroupProcess.TableName,
				JoinOns = new DbJoinOns()
				{
					{ DataProcess.ColumnDataProcessID, ProcessGroupProcess.ColumnDataProcessID }
				}
			};
			if (outerJoin)
			{
				dbJoin.JoinType = "left outer";
			}

			dbJoin.Columns = new DbColumns();
			dbJoin.Columns.Add(ProcessGroupProcess.ColumnSequence, dataTypeName: "int");
			retValue.Add(dbJoin);
			return retValue;
		}
		#endregion

		#region Filters

		// Get Filter with GroupID and ProcessID.
		/// <include path='items/GetGroupIDJoinFilter/*' file='Doc/DataProcessManager.xml'/>
		public DbFilters GetGroupIDJoinFilter(int groupID, int processID)
		{
			DbFilters retValue = new DbFilters();

			// where ((ProcessGroupProcess.ProcessGroupID = 1)
			//   and (ProcessGroupProcess.ProcessID = 1))
			DbFilter dbFilter = new DbFilter();
			DbConditions conditions = dbFilter.ConditionSet.Conditions;
			conditions.Add("ProcessGroupProcess.ProcessGroupID", groupID.ToString());
			if (processID > 0)
			{
				conditions.Add("ProcessGroupProcess.DataProcessID", processID.ToString());
			}
			retValue.Add(dbFilter);
			return retValue;
		}

		// Creates and returns the filters object.
		/// <include path='items/GetNotInGroupFilter/*' file='Doc/DataProcessManager.xml'/>
		public DbFilters GetNotInGroupFilter()
		{
			DbFilters retValue;

			DbFilter dbFilter = new DbFilter();
			DbConditions dbConditions = dbFilter.ConditionSet.Conditions;
			dbConditions.Add(ProcessGroupProcess.ColumnProcessGroupID
				, null, "is null");
			retValue = new DbFilters { dbFilter };
			return retValue;
		}
    #endregion

    #region OrderBys

    /// <summary>Sets the current OrderBy names.</summary>
    public void SetOrderBySequence() => mDataManager.OrderByNames = new List<string>()
		{
			ProcessGroupProcess.ColumnSequence
		};
		#endregion

		#region Other Public Methods

		// Check for duplicate unique key.
		/// <include path='items/IsDuplicate/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public bool IsDuplicate(DataProcess lookupRecord, DataProcess currentRecord
			, bool isUpdate = false)
		{
			bool retValue = false;

			if (lookupRecord != null)
			{
				if (!isUpdate)
				{
					// Duplicate for "New" record that already exists.
					retValue = true;
				}
				else
				{
					if (lookupRecord.DataProcessID != currentRecord.DataProcessID)
					{
						// Duplicate for "Update" where unique key is modified.
						retValue = true;
					}
				}
			}
			return retValue;
		}

		// Updates the DataProcess status.
		/// <include path='items/UpdateStatus/*' file='Doc/DataProcessManager.xml'/>
		public void UpdateStatus(DataProcess dataRecord)
		{
			var keyColumns = GetIDKey(dataRecord.DataProcessID);
			List<string> columnList = new List<string>()
			{
				DataProcess.ColumnProcessStatusID
			};
			Update(dataRecord, keyColumns, columnList);
		}
		#endregion

		#region Private Methods

		// Creates a collection from the result object.
		// This method provided because this class does not inherit from ObjectManager.
		private DataProcesses CreateCollection(DbResult dbResult)
		{
			DataProcesses retValue = new DataProcesses();

			foreach (DbRow dbRow in dbResult.Rows)
			{
				DataProcess dataRecord = new DataProcess();
				DbCommon.SetObjectValues(dbRow.Values, dataRecord);
				retValue.Add(dataRecord);
			}
			return retValue;
		}
		#endregion

		#region Properties

		/// <summary>Gets or sets the non-select affected record count.</summary>
		public int AffectedCount { get; set; }

		/// <summary>The DataConfig name.</summary>
		public string DataConfigName { get; set; }

		/// <summary>The DBServiceRef object.</summary>
		public DbServiceRef DbServiceRef { get; set; }

		/// <summary>Gets or sets the OrderBy name list.</summary>
		public List<string> OrderByNames
		{
			get { return mOrderByNames; }
			set
			{
				mOrderByNames = value;
				// Next Statement - Add
				mDataManager.OrderByNames = mOrderByNames;
			}
		}
		List<string> mOrderByNames;

		/// <summary>Gets or sets the pagination size.</summary>
		public int PageSize
		{
			get { return mPageSize; }
			set
			{
				mPageSize = value;
				mDataManager.PageSize = mPageSize;
			}
		}
		int mPageSize;

		/// <summary>Gets or sets the pagination start index.</summary>
		public int PageStartIndex
		{
			get { return mPageStartIndex; }
			set
			{
				mPageStartIndex = value;
				mDataManager.PageStartIndex = mPageStartIndex;
			}
		}
		int mPageStartIndex;

		/// <summary>Gets or sets the last SQL statement.</summary>
		public string SQLStatement { get; set; }
		#endregion

		#region Class Data

		private readonly DataManager mDataManager;
		#endregion
	}
}
