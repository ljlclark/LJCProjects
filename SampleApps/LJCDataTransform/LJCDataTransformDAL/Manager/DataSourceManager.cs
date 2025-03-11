// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataSourceManager.cs
using System;
using LJCDBMessage;
using LJCDBClientLib;
using LJCNetCommon;
using System.Collections.Generic;

namespace LJCDataTransformDAL
{
	/// <summary>Provides Table specific data manipulation methods.</summary>
	public class DataSourceManager
		: ObjectManager<DataSource, DataSources>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/ObjectManagerC/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public DataSourceManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "DataSource")
			: base(dbServiceRef, dataConfigName, tableName)
		{
			// Create the list of database assigned columns.
			SetDbAssignedColumns(new string[]
				{
					DataSource.ColumnDataSourceID
				});

			// Create the list of lookup column names.
			string[] lookupColumnNames = new string[]
				{
					DataSource.ColumnName
				};
			SetLookupColumns(lookupColumnNames);
		}
		#endregion

		#region Retrieve/Load Methods

		// Loads the DataSource records that are not in TaskSource.
		/// <include path='items/LoadNotInStepTask/*' file='Doc/DataSourceManager.xml'/>
		public DataSources LoadNotInStepTask(int stepTaskID)
		{
			DataSources retValue = null;

			DbJoins dbJoins = GetLoadJoins();
			DbFilters dbFilters = GetNotInTaskSourceFilters(stepTaskID);
			retValue = Load(joins: dbJoins, filters: dbFilters);
			return retValue;
		}

		// Loads a collection of data records with the supplied value.
		/// <include path='items/LoadWithTaskID/*' file='Doc/DataSourceManager.xml'/>
		public DataSources LoadWithTaskID(int stepTaskID)
		{
			DbJoins dbJoins = GetLoadJoins();
			DbFilters dbFilters = GetLoadFilters(stepTaskID);
			return Load(filters: dbFilters, joins: dbJoins);
		}

		// Retrieves a Data Record with the supplied value.
		/// <include path='items/RetrieveWithID/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public DataSource RetrieveWithID(int id, List<string> propertyNames = null)
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
				{ DataSource.ColumnDataSourceID,  id }
			};
			return retValue;
		}

		// Get the ID key record.
		/// <include path='items/GetNameKey/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public DbColumns GetNameKey(string name)
		{
			var retValue = new DbColumns()
			{
				{ DataSource.ColumnName,  (object)name }
			};
			return retValue;
		}
		#endregion

		#region Joins

		// Creates and returns the Load Joins object.
		/// <include path='items/GetLoadJoins/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public DbJoins GetLoadJoins()
		{
			DbJoin dbJoin;
			DbJoins retValue = new DbJoins();

			// Note: JoinOn Columns must have properties in the Data Object
			// to recieve the join values.
			// The RenameAs property is required if there is another table column
			// with the same name.
			// dbColumns.Add(string columnName, string propertyName = null
			// , string renameAs = null, string dataTypeName = "String", string caption = null) 

			// Left join [TaskSource]
			//   on (([DataSource].[DataSourceID] = [TaskSource].[SourceID]))
			dbJoin = new DbJoin()
			{
				TableName = TaskSource.TableName,
				JoinOns = new DbJoinOns()
				{
					{ DataSource.ColumnDataSourceID, TaskSource.ColumnDataSourceID }
				}
			};
			retValue.Add(dbJoin);
			return retValue;
		}
		#endregion

		#region Filters

		// Creates and returns the filters object.
		/// <include path='items/GetLoadFilters/*' file='Doc/DataSourceManager.xml'/>
		public DbFilters GetLoadFilters(int stepTaskID)
		{
			DbFilters retValue = null;

			DbFilter dbFilter = new DbFilter();
			DbConditions conditions = dbFilter.ConditionSet.Conditions;
			conditions.Add("TaskSource.StepTaskID", stepTaskID.ToString());
			retValue = new DbFilters
			{
				dbFilter
			};
			return retValue;
		}

		// Creates and returns the filters object.
		/// <include path='items/GetNotInTaskSourceFilters/*' file='Doc/DataSourceManager.xml'/>
		public DbFilters GetNotInTaskSourceFilters(int stepTaskID)
		{
			DbFilters retValue = null;

			DbFilter dbFilter = new DbFilter();
			dbFilter.ConditionSet.BooleanOperator = "or";
			DbConditions dbConditions = dbFilter.ConditionSet.Conditions;
			dbConditions.Add(TaskSource.ColumnStepTaskID
				, stepTaskID.ToString(), "<>");
			dbConditions.Add("TaskSource.StepTaskID", "is null");
			retValue = new DbFilters
			{
				dbFilter
			};
			return retValue;
		}
		#endregion

		#region Other Public Methods

		// Check for duplicate unique key.
		/// <include path='items/IsDuplicate/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public bool IsDuplicate(DataSource lookupRecord, DataSource currentRecord
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
					if (lookupRecord.DataSourceID != currentRecord.DataSourceID)
					{
						// Duplicate for "Update" where unique key is modified.
						retValue = true;
					}
				}
			}
			return retValue;
		}
		#endregion
	}
}
