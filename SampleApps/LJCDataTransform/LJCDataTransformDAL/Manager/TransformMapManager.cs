// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TransformMapManager.cs
using System;
using System.Collections.Generic;
using System.Text;
using LJCNetCommon;
using LJCDBMessage;
using LJCDBClientLib;

namespace LJCDataTransformDAL
{
	/// <summary>Provides Table specific data manipulation methods.</summary>
	public class TransformMapManager
		: ObjectManager<TransformMap, TransformMaps>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/TransformMapManagerC/*' file='Doc/TransformMapManager.xml'/>
		public TransformMapManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "TransformMap")
			: base(dbServiceRef, dataConfigName, tableName)
		{
			// Add Calculated and Join columns.
			// Enables populating a Data Object and adding to a grid configuration.
			DataDefinition.Add(TransformMap.ColumnSourceColumnName
				, caption: "Name");
			DataDefinition.Add(TransformMap.ColumnSourceDescription
				, caption: "Description");
			DataDefinition.Add(TransformMap.ColumnTargetColumnName
			 , caption: "Name");
			DataDefinition.Add(TransformMap.ColumnTargetDescription
				, caption: "Description");
			DataDefinition.Add(TransformMap.ColumnMapTypeName
			 , caption: "Map Type");
			DataDefinition.Add("LayoutColumnID");

			// Create the list of database assigned columns.
			SetDbAssignedColumns(new string[]
				{
					TransformMap.ColumnTransformMapID
				});

			// Create the list of lookup column names.
			SetLookupColumns(new string[]
				{
					TransformMap.ColumnTransformID,
					TransformMap.ColumnSourceColumnID,
					TransformMap.ColumnTargetColumnID
				});
		}
		#endregion

		#region Retrieve/Load Methods

		// Loads a collection of data records using the DataSource ID.
		/// <include path='items/LoadWithInValues/*' file='Doc/TransformMapManager.xml'/>
		public TransformMaps LoadWithInValues(int transformID, int sourceOrigin, string inValues)
		{
			DbJoins dbJoins = GetInValueJoins(transformID, sourceOrigin);
			DbFilters dbFilters = GetInValueFilters("LayoutColumn.LayoutColumnID", inValues);
			return Load(null, filters: dbFilters, joins: dbJoins);
		}

		// Loads a collection of data records ordered by Sequence.
		/// <include path='items/LoadWithTransformID/*' file='Doc/TransformMapManager.xml'/>
		public TransformMaps LoadWithTransformID(int transformID)
		{
			var keyColumns = GetTransformIDKey(transformID);
			DbJoins dbJoins = GetLoadJoins();
			SetOrderBySequence();
			return Load(keyColumns, joins: dbJoins);
		}

		// Retrieves a Data Record with the supplied value.
		/// <include path='items/RetrieveWithID/*' file='Doc/TransformMapManager.xml'/>
		public TransformMap RetrieveWithID(int id, int transformID)
		{
			var keyColumns = GetIDKey(id);
			DbJoins dbJoins = GetLoadJoins();
			return Retrieve(keyColumns, joins: dbJoins);
		}
		#endregion

		#region GetKey Methods

		// Get the ID key record.
		/// <include path='items/GetIDKey/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public DbColumns GetIDKey(int id)
		{
			var retValue = new DbColumns()
			{
				{ TransformMap.ColumnTransformMapID, id }
			};
			return retValue;
		}

		// Get the TransformID key record.
		/// <include path='items/GetTransformIDKey/*' file='Doc/TransformMapManager.xml'/>
		public DbColumns GetTransformIDKey(int transformID)
		{
			var retValue = new DbColumns()
			{
				{ TransformMap.ColumnTransformID, transformID }
			};
			return retValue;
		}
		#endregion

		#region Joins

		// Creates and returns the Load Joins object.
		/// <include path='items/GetInValueJoins/*' file='Doc/TransformMapManager.xml'/>
		public DbJoins GetInValueJoins(int transformID, int sourceOrigin)
		{
			DbJoin dbJoin;
			DbJoins retValue = new DbJoins();

			// Create Unique values.
			string secondValue = $"|{transformID}|";
			string sourceColumnIDName = null;
			switch (sourceOrigin)
			{
				case SourceOrigin:
					sourceColumnIDName = TransformMap.ColumnSourceColumnID;
					break;
				case TargetOrigin:
					sourceColumnIDName = TransformMap.ColumnTargetColumnID;
					break;
			}

			// LayoutColumn.LayoutColumnID
			// LayoutColumn.Name as SourceColumnName,
			// LayoutColumn.Description as SourceDescription,
			//right join LayoutColumn
			// on TransformMap.TransformID = 1
			// and sourceColumnIDName = LayoutColumn.LayoutColumnID
			dbJoin = new DbJoin()
			{
				TableName = "LayoutColumn",
				JoinType = "right",
				JoinOns = new DbJoinOns() {
					{ TransformMap.ColumnTransformID, secondValue },
					{ sourceColumnIDName, LayoutColumn.ColumnLayoutColumnID }
				},
				Columns = new DbColumns {
					// columnName, propertyName = null, renameAs = null
					//   , dataTypeName = "String", caption = null
					{ LayoutColumn.ColumnLayoutColumnID },
					{ LayoutColumn.ColumnName, TransformMap.ColumnSourceColumnName
						, TransformMap.ColumnSourceColumnName },
					{ LayoutColumn.ColumnDescription, TransformMap.ColumnSourceDescription
						, TransformMap.ColumnSourceDescription }
				}
			};
			retValue.Add(dbJoin);

			// MapType.Name as MapTypeName
			//left join MapType
			// on TransformMap.MapTypeID = MapType.MapTypeID
			dbJoin = new DbJoin()
			{
				TableName = "MapType",
				JoinType = "left",
				JoinOns = new DbJoinOns() {
					{ TransformMap.ColumnMapTypeID, "MapTypeID" }
				},
				Columns = new DbColumns {
					// columnName, propertyName = null, renameAs = null
					//   , dataTypeName = "String", caption = null
					{ "Name" , TransformMap.ColumnMapTypeName
						, TransformMap.ColumnMapTypeName }
				}
			};
			retValue.Add(dbJoin);
			return retValue;
		}

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

			// SourceColumnName
			//left join LayoutColumn
			// on TransformMap.SourceColumnID = layoutColumn.LayoutColumnID
			dbJoin = new DbJoin()
			{
				TableName = "LayoutColumn",
				JoinType = "left",
				JoinOns = new DbJoinOns() {
					{ TransformMap.ColumnSourceColumnID, LayoutColumn.ColumnLayoutColumnID }
				},
				Columns = new DbColumns {
					// columnName, propertyName = null, renameAs = null
					//   , dataTypeName = "String", caption = null
					{ LayoutColumn.ColumnName, TransformMap.ColumnSourceColumnName
						, TransformMap.ColumnSourceColumnName },
				}
			};
			retValue.Add(dbJoin);

			// TargetColumnName
			//left join LayoutColumn as lc
			// on TransformMap.TargetColumnID = lc.LayoutColumnID
			dbJoin = new DbJoin()
			{
				TableName = "LayoutColumn",
				TableAlias = "lc",
				JoinType = "left",
				JoinOns = new DbJoinOns() {
					{ TransformMap.ColumnTargetColumnID, LayoutColumn.ColumnLayoutColumnID }
				},
				Columns = new DbColumns {
					// columnName, propertyName = null, renameAs = null
					//   , dataTypeName = "String", caption = null
					{ LayoutColumn.ColumnName, TransformMap.ColumnTargetColumnName
						, TransformMap.ColumnTargetColumnName },
				}
			};
			retValue.Add(dbJoin);
			return retValue;
		}
		#endregion

		#region Filters

		// Creates and returns the filters object. 
		/// <include path='items/GetInValueFilters/*' file='Doc/TransformMapManager.xml'/>
		public DbFilters GetInValueFilters(string columnName, string inValues)
		{
			DbFilters retValue = new DbFilters();

			//where TransformMap.TransformMapID in(1, 2);
			DbFilter dbFilter = new DbFilter();
			dbFilter.ConditionSet.Conditions.Add(columnName, inValues, "in");
			retValue.Add(dbFilter);
			return retValue;
		}
		#endregion

		#region OrderBys

		// Sets the current OrderBy names.
		/// <include path='items/SetOrderBySequence/*' file='Doc/TransformMapManager.xml'/>
		public void SetOrderBySequence()
		{
			DataManager.OrderByNames = new List<string>()
			{
				//TransformMap.ColumnSequence
			};
		}
		#endregion

		private const int SourceOrigin = 1;
		private const int TargetOrigin = 2;
	}
}
