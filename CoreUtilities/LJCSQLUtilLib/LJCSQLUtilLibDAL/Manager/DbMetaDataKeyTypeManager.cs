// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DbMetaDataKeyTypeManager.cs
using System.Collections.Generic;
using LJCDBMessage;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCSQLUtilLibDAL
{
	// Provides DBMetaDataKeyType specific data manipulation methods.
	/// <include path='items/DbMetaDataKeyTypeManager/*' file='Doc/DbMetaDataKeyTypeManager.xml'/>
	public class DbMetaDataKeyTypeManager
		: ObjectManager<DbMetaDataKeyType, DbMetaDataKeyTypes>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DbMetaDataKeyTypeManagerC/*' file='Doc/DbMetaDataKeyTypeManager.xml'/>
		public DbMetaDataKeyTypeManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "DBMetaDataKeyType") : base(dbServiceRef, dataConfigName, tableName)
		{
			// Map table names with property names or captions
			// that differ from the column names.
			MapNames(DbMetaDataKeyType.ColumnID, caption: "ID");

			// Create the list of database assigned columns.
			SetDbAssignedColumns(new string[]
				{
					DbMetaDataKeyType.ColumnID
				});

			// Create the list of lookup column names.
			SetLookupColumns(new string[]
				{
					DbMetaDataKeyType.ColumnName
				});
		}
		#endregion

		#region Retrieve/Load Methods

		// Loads a collection of data records ordered by Description.
		/// <include path='items/LoadByDescription/*' file='Doc/DbMetaDataKeyTypeManager.xml'/>
		public DbMetaDataKeyTypes LoadByDescription()
		{
			DbJoins dbJoins = GetLoadJoins();
			SetOrderByDescription();
			return Load(null, joins: dbJoins);
		}

		// Retrieves a data record with the supplied value.
		/// <include path='items/RetrieveWithID/*' file='../../LJCDocLib/Common/Manager.xml'/>
		public DbMetaDataKeyType RetrieveWithID(int id
			, List<string> propertyNames = null)
		{
			var keyColumns = GetIDKey(id);
			return Retrieve(keyColumns, propertyNames);
		}
		#endregion

		#region GetKey Methods

		// Get the ID key record.
		/// <include path='items/GetIDKey/*' file='../../LJCDocLib/Common/Manager.xml'/>
		public DbColumns GetIDKey(int id)
		{
			var retValue = new DbColumns()
			{
				{ "ID", id, "Int32" }
			};
			return retValue;
		}
		#endregion

		#region Joins

		// Creates and returns the Load join object.
		/// <include path='items/GetLoadJoins/*' file='Doc/DbMetaDataKeyTypeManager.xml'/>
		public DbJoins GetLoadJoins()
		{
			//DbJoin dbJoin;
			DbJoins retValue = new DbJoins();

			// [CodeType].[Description] as TypeDescription
			//left join [CodeType]
			// on [BaseTable].[CodeTypeID] = CodeType.ID

			// JoinOn Columns must have a properties in the main table object
			// to recieve the join values.
			//string columnName = CodeType.ColumnDescription;
			//string propertyName = DbMetaDataKeyType.ColumnTypeDescription;
			//string renameAs = DbMetaDataKeyType.ColumnTypeDescription;
			//dbJoin = new DbJoin
			//{
			//	JoinTableName = "CodeType",
			//	JoinType = "left",
			//	JoinOns = new DbJoinOns() {
			//		{ DbMetaDataKeyType.ColumnCodeTypeID, CodeType.ColumnID }},
			//	Columns = new DbColumns() {
			//		{ columnName, propertyName, renameAs }}
			//};
			//retValue.Add(dbJoin);
			return retValue;
		}

		#endregion

		#region Filters

		// Creates and returns the filters object.
		/// <include path='items/GetTextFilters/*' file='Doc/DbMetaDataKeyTypeManager.xml'/>
		public DbFilters GetTextFilters()
		{
			DbFilters retValue;

			DbFilter dbFilter = new DbFilter();
			dbFilter.ConditionSet.Conditions.Add(DbMetaDataKeyType.ColumnDescription, "'Text'");
			retValue = new DbFilters {
				dbFilter};
			return retValue;
		}
		#endregion

		#region OrderBys

		// Sets the current OrderBy names.
		/// <include path='items/SetOrderByDescription/*' file='Doc/DbMetaDataKeyTypeManager.xml'/>
		public void SetOrderByDescription() => DataManager.OrderByNames = new List<string>()
		{
			//DbMetaDataKeyType.ColumnDescription
		};

		#endregion
	}
}
