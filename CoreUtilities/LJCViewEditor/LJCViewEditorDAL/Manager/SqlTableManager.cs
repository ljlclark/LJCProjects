// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// SqlTableManager.cs
using System;
using LJCNetCommon;
using LJCDBClientLib;

namespace LJCViewEditorDAL
{
	/// <summary>Provides Table specific data manipulation methods.</summary>
	public class SqlTableManager
		: ObjectManager<SqlTable, SqlTables>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/ObjectManagerC/*' file='../../LJCDocLib/Common/Manager.xml'/>
		public SqlTableManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = null) : base(dbServiceRef, dataConfigName, tableName)
		{
			DataManager.BaseDefinition = new DbColumns()
			{
				{ "TABLE_NAME" }
			};
			DataManager.DataDefinition = DataManager.BaseDefinition.Clone();

			// Map table names with property names or captions
			// that differ from the column names.
			MapNames("TABLE_NAME", "Name");
		}
		#endregion
	}
}
