// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TransformMatchManager.cs
using System;
using System.Collections.Generic;
using LJCNetCommon;
using LJCDBMessage;
using LJCDBClientLib;

namespace LJCDataTransformDAL
{
	/// <summary>Provides Table specific data manipulation methods.</summary>
	public class TransformMatchManager
		: ObjectManager<TransformMatch, TransformMatches>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/TransformMatchManagerC/*' file='Doc/TransformMatchManager.xml'/>
		public TransformMatchManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "TransformMatch")
			: base(dbServiceRef, dataConfigName, tableName)
		{
			// Add Calculated and Join columns.
			// Enables populating a Data Object and adding to a grid configuration.
			DataDefinition.Add(TransformMatch.ColumnSourceColumnName
				, caption: "Name");
			DataDefinition.Add(TransformMatch.ColumnSourceDescription
				, caption: "Description");
			DataDefinition.Add(TransformMatch.ColumnTargetColumnName
			 , caption: "Name");
			DataDefinition.Add(TransformMatch.ColumnTargetDescription
				, caption: "Description");

			// Create the list of database assigned columns.
			SetDbAssignedColumns(new string[]
				{
					TransformMatch.ColumnTransformMatchID
				});

			// Create the list of lookup column names.
			SetLookupColumns(new string[]
				{
					TransformMatch.ColumnTransformID,
					TransformMatch.ColumnSourceColumnID,
					TransformMatch.ColumnTargetColumnID
				});
		}
		#endregion

		#region Retrieve/Load Methods

		// Loads a collection of data records ordered by Sequence.
		/// <include path='items/LoadBySequence/*' file='Doc/TransformMatchManager.xml'/>
		public TransformMatches LoadBySequence()
		{
			SetOrderBySequence();
			return Load();
		}

		// Loads a collection of data records ordered by Sequence.
		/// <include path='items/LoadWithTransformID/*' file='Doc/TransformMatchManager.xml'/>
		public TransformMatches LoadWithTransformID(int transformID)
		{
			var keyColumns = GetTransformIDKey(transformID);
			DbJoins dbJoins = GetLoadJoins();
			SetOrderBySequence();
			return Load(keyColumns, joins: dbJoins);
		}

		// Retrieves a Data Record with the supplied value.
		/// <include path='items/RetrieveWithID/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public TransformMatch RetrieveWithID(int id, List<string> propertyNames = null)
		{
			var keyColumns = GetIDKey(id);
			DbJoins dbJoins = GetLoadJoins();
			return Retrieve(keyColumns, propertyNames, joins: dbJoins);
		}
		#endregion

		#region GetKey Methods

		// Get the ID key record.
		/// <include path='items/GetIDKey/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public DbColumns GetIDKey(int id)
		{
			var retValue = new DbColumns()
			{
				{ TransformMatch.ColumnTransformMatchID, id }
			};
			return retValue;
		}

		// Get the TransformID key record.
		/// <include path='items/GetTransformIDKey/*' file='Doc/TransformMatchManager.xml'/>
		public DbColumns GetTransformIDKey(int transformID)
		{
			var retValue = new DbColumns()
			{
				{ TransformMatch.ColumnTransformID, transformID }
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

			// SourceColumnName
			//left join LayoutColumn
			// on TransformMatch.SourceColumnID = layoutColumn.LayoutColumnID
			dbJoin = new DbJoin()
			{
				TableName = "LayoutColumn",
				JoinType = "left",
				JoinOns = new DbJoinOns() {
					{ TransformMatch.ColumnSourceColumnID, LayoutColumn.ColumnLayoutColumnID }
				},
				Columns = new DbColumns {
					// columnName, propertyName = null, renameAs = null
					//   , dataTypeName = "String", caption = null
					{ LayoutColumn.ColumnName, TransformMatch.ColumnSourceColumnName
						, TransformMatch.ColumnSourceColumnName },
				}
			};
			retValue.Add(dbJoin);

			// TargetColumnName
			//left join LayoutColumn as lc
			// on TransformMatch.TargetColumnID = lc.LayoutColumnID
			dbJoin = new DbJoin()
			{
				TableName = "LayoutColumn",
				TableAlias = "lc",
				JoinType = "left",
				JoinOns = new DbJoinOns() {
					{ TransformMatch.ColumnTargetColumnID, LayoutColumn.ColumnLayoutColumnID }
				},
				Columns = new DbColumns {
					// columnName, propertyName = null, renameAs = null
					//   , dataTypeName = "String", caption = null
					{ LayoutColumn.ColumnName, TransformMatch.ColumnTargetColumnName
						, TransformMatch.ColumnTargetColumnName },
				}
			};
			retValue.Add(dbJoin);
			return retValue;
		}
		#endregion

		#region OrderBys

		// Sets the current OrderBy names.
		/// <include path='items/SetOrderBySequence/*' file='Doc/TransformMatchManager.xml'/>
		public void SetOrderBySequence()
		{
			DataManager.OrderByNames = new List<string>()
			{
				//TransformMatch.ColumnSequence
			};
		}
		#endregion
	}
}
