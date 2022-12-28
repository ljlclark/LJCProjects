// Copyright (c) Lester J. Clark 2017-2019- All Rights Reserved
using System;
using System.Collections.Generic;
using LJCNetCommon;
using LJCDBMessage;
using LJCDBClientLib;
using LJCDBServiceLib;

namespace LJCDBViewDAL
{
	// Provides ViewGridColumn specific data manipulation methods.
	/// <include path='items/ViewGridColumnManager/*' file='Doc/ViewGridColumnManager.xml'/>
	public class ViewGridColumnManager : ObjectManager<ViewGridColumn, ViewGridColumns>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/ViewGridColumnManagerC/*' file='Doc/ViewGridColumnManager.xml'/>
		public ViewGridColumnManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "ViewGridColumn") : base(dbServiceRef, dataConfigName, tableName)
		{
			// Map table names with property names or captions
			// that differ from the column names.
			MapNames(ViewGridColumn.ColumnViewDataID, caption: "View ID");
			MapNames(ViewGridColumn.ColumnViewColumnID, caption: "Column ID");

			// Add Calculated and Join columns.
			// Enables populating a Data Object and adding to a grid configuration.
			DataDefinition.Add(ViewColumn.ColumnPropertyName);
		}
		#endregion

		#region Retrieve/Load Methods

		// Retrieves a data record with the supplied value.
		/// <include path='items/RetrieveWithIDs/*' file='Doc/ViewGridColumnManager.xml'/>
		public ViewGridColumn RetrieveWithIDs(int viewDataID, int viewColumnID)
		{
			ViewGridColumn retValue;

			var keyColumns = GetIDsKey(viewDataID, viewColumnID);
			retValue = Retrieve(keyColumns);
			return retValue;
		}

		// Retrieves a data record with the unique key values.
		/// <include path='items/RetrieveWithUniqueKey/*' file='Doc/ViewGridColumnManager.xml'/>
		public ViewGridColumn RetrieveWithUniqueKey(int viewDataID, string columnName)
		{
			ViewGridColumn retValue;

			var keyColumns = GetUniqueKey(viewDataID, columnName);
			DbJoins dbJoins = GetLoadJoins();
			retValue = Retrieve(keyColumns, joins: dbJoins);
			return retValue;
		}

		// Loads a collection of data records ordered by Sequence.
		/// <include path='items/LoadWithViewIDBySequence/*' file='Doc/ViewGridColumnManager.xml'/>
		public ViewGridColumns LoadWithViewIDBySequence(int viewDataID)
		{
			ViewGridColumns retValue;

			var keyColumns = GetViewDataIDKey(viewDataID);
			DbJoins dbJoins = GetLoadJoins();
			SetOrderBySequence();
			retValue = Load(keyColumns, joins: dbJoins);
			return retValue;
		}
		#endregion

		#region GetKey Methods

		// Get the ID key record.
		/// <include path='items/GetIDsKey/*' file='Doc/ViewGridColumnManager.xml'/>
		public DbColumns GetIDsKey(int viewDataID, int viewColumnID)
		{
			var retValue = new DbColumns()
			{
				{ ViewGridColumn.ColumnViewDataID, viewDataID },
				{ ViewGridColumn.ColumnViewColumnID, viewColumnID }
			};
			return retValue;
		}

		// Get the ViewDataID key record.
		/// <include path='items/GetViewDataIDKey/*' file='Doc/ViewGridColumnManager.xml'/>
		public DbColumns GetViewDataIDKey(int viewDataID)
		{
			var retValue = new DbColumns()
			{
				{ ViewGridColumn.ColumnViewDataID, viewDataID }
			};
			return retValue;
		}

		// Get the Unique key record.
		/// <include path='items/GetUniqueKey/*' file='Doc/ViewGridColumnManager.xml'/>
		public DbColumns GetUniqueKey(int viewDataID, string propertyName)
		{
			// Add(columnName, propertyName = null, renameAs = null
			//   , datatypeName = "String", caption = null);
			// Add(columnName, object value, dataTypeName = "String");
			var retValue = new DbColumns()
			{
				{ "PropertyName", (object)propertyName },
				{ ViewGridColumn.ColumnViewDataID, viewDataID }
			};
			return retValue;
		}
		#endregion

		#region Joins

		// Creates and returns the Joins object.
		/// <include path='items/GetLoadJoins/*' file='Doc/ViewGridColumnManager.xml'/>
		public DbJoins GetLoadJoins()
		{
			DbJoin dbJoin;
			DbJoins retValue = new DbJoins();

			// Note: JoinOn Columns must have properties in the Data Object
			// to recieve the join column values.
			// The RenameAs property is required if there is another table column
			// with the same name.

			//select [ViewGridColumn].DBViewID, [ViewGridColumn].ViewColumnID, vc.ColumnName, vc.PropertyName,
			// vc.RenameAs, [ViewGridColumn].[Sequence], [ViewGridColumn].Width
			//from ViewGridColumn
			//left join ViewColumn as vc
			// on [ViewGridColumn].ViewColumnID = vc.ID
			//where [ViewGridColumn].DBViewID = 2
			//order by [ViewGridColumn].[Sequence];

			dbJoin = new DbJoin
			{
				TableName = ViewColumn.TableName,
				TableAlias = "vc",
				JoinType = "left",
				JoinOns = new DbJoinOns() {
					{ ViewGridColumn.ColumnViewColumnID
					 , $"[vc].{ViewColumn.ColumnID}" }
				},
				Columns = new DbColumns() {
					{ ViewColumn.ColumnColumnName },
					{ ViewColumn.ColumnPropertyName }
				}
			};
			retValue.Add(dbJoin);
			return retValue;
		}
		#endregion

		#region Filters

		// Creates and returns the filters object.
		/// <include path='items/GetTextFilters/*' file='Doc/ViewGridColumnManager.xml'/>
		public DbFilters GetTextFilters()
		{
			DbFilters retValue = null;

			//DbFilter dbFilter = new DbFilter();
			//dbFilter.ConditionSet.Conditions.Add(ViewGridColumn.ColumnDescription, "'Text'");
			//retValue = new DbFilters {
			//	dbFilter};
			return retValue;
		}
		#endregion

		#region OrderBys

		// Sets the current OrderBy names.
		/// <include path='items/SetOrderBySequence/*' file='Doc/ViewGridColumnManager.xml'/>
		public void SetOrderBySequence() => DataManager.OrderByNames = new List<string>()
		{
			ViewGridColumn.ColumnSequence
		};
		#endregion

		#region Custom Data Methods

		// Updates the record if it exists, otherwise creates it.
		/// <include path='items/SaveData/*' file='Doc/ViewGridColumnManager.xml'/>
		public bool SaveData(ViewGridColumn viewGridColumn)
		{
			bool retValue = true;

			if (viewGridColumn.ViewDataID > 0
				&& viewGridColumn.ViewColumnID > 0)
			{
				// Attempt to retrieve record.
				ViewGridColumn retrieveData = RetrieveWithIDs(viewGridColumn.ViewDataID
					, viewGridColumn.ViewColumnID);
				if (null == retrieveData)
				{
					// Create record.
					if(null == Add(viewGridColumn))
					{
						retValue = false;
					}
				}
				else
				{
					// Update record.
					// Note: Changed to update only changed columns.
					if (viewGridColumn.ChangedNames.Count > 0)
					{
						var keyColumns = GetIDsKey(retrieveData.ViewDataID
							, retrieveData.ViewColumnID);
						Update(viewGridColumn, keyColumns, viewGridColumn.ChangedNames);
					}
				}
			}
			return retValue;
		}

		// Retrieves the Display Columns definition.
		/// <include path='items/GetDisplayColumns/*' file='Doc/ViewGridColumnManager.xml'/>
		public DbColumns GetDisplayColumns(int viewDataID)
		{
			DbColumns retValue = null;

			ViewGridColumns viewGridColumns = LoadWithViewIDBySequence(viewDataID);
			if (viewGridColumns != null && viewGridColumns.Count > 0)
			{
				retValue = new DbColumns();
				foreach (ViewGridColumn viewGridColumn in viewGridColumns)
				{
					DbColumn dbColumn = new DbColumn()
					{
						ColumnName = viewGridColumn.ColumnName,
						PropertyName = viewGridColumn.PropertyName
					};
					retValue.Add(dbColumn);
				}
			}
			return retValue;
		}
		#endregion
	}
}
