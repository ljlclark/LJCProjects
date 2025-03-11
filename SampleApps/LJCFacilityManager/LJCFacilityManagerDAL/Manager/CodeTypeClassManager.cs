// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CodeTypeClassManager.cs
using System;
using System.Collections.Generic;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCFacilityManagerDAL
{
	/// <summary>Provides CodeTypeClass specific data manipulation methods.</summary>
	public class CodeTypeClassManager : ObjectManager<CodeTypeClass, CodeTypeClasses>
	{
		// Initializes an object instance.
		/// <include path='items/ObjectManagerC/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public CodeTypeClassManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "CodeTypeClass") : base(dbServiceRef, dataConfigName, tableName)
		{
			// Map table names with property names or captions
			// that differ from the column names.

			// Create the list of database assigned columns.
			// And make sure the AutoIncrement value is set.
			SetDbAssignedColumns(new string[]
			{
				CodeTypeClass.ColumnID
			});

			// Create the list of lookup column names.
			SetLookupColumns(new string[]
			{
				CodeTypeClass.ColumnDescription
			});
		}

    /// <summary>Sets the current OrderBy names.</summary>
    public void SetOrderByDescription() => DataManager.OrderByNames = new List<string>() {
				CodeTypeClass.ColumnDescription};

		/// <summary>
		/// Retrieves the assigned CodeClass ID.
		/// </summary>
		/// <param name="code">The CodeType code.</param>
		/// <returns>The CodeTypeClass ID.</returns>
		public int GetCodeClassID(string code)
		{
			CodeTypeClass record;
			int retValue = 0;

			var keyColumns = new DbColumns()
			{
				{ CodeTypeClass.ColumnCode, (object)code }
			};
			record = Retrieve(keyColumns);
			if (record != null)
			{
				retValue = record.ID;
			}
			return retValue;
		}
	}
}
