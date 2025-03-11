// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataTypeManager.cs
using System;
using System.Collections.Generic;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCDataTransformDAL
{
	/// <summary>Provides Table specific data manipulation methods.</summary>
	public class DataTypeManager
		: ObjectManager<DataType, DataTypes>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DataTypeManagerC/*' file='Doc/DataTypeManager.xml'/>
		public DataTypeManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "DataType")
			: base(dbServiceRef, dataConfigName, tableName)
		{
			// Create the list of database assigned columns.
			SetDbAssignedColumns(new string[]
				{
					DataType.ColumnDataTypeID
				});

			// Create the list of lookup column names.
			SetLookupColumns(new string[]
				{
					DataType.ColumnName
				});
		}
		#endregion

		#region Retrieve/Load Methods

		// Retrieves a data record with the supplied value.
		/// <include path='items/RetrieveWithID/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public DataType RetrieveWithID(short id, List<string> propertyNames = null)
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
				{ DataType.ColumnDataTypeID, id }
			};
			return retValue;
		}

		// Get the ID key record.
		/// <include path='items/GetNameKey/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public DbColumns GetNameKey(string name)
		{
			var retValue = new DbColumns()
			{
				{ DataType.ColumnName, (object)name }
			};
			return retValue;
		}
		#endregion
	}
}
