// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// BusinessPersonManager.cs
using System;
using LJCDBClientLib;

namespace LJCFacilityManagerDAL
{
	/// <summary>Provides BusinessPerson specific data manipulation methods.</summary>
	public class BusinessPersonManager : ObjectManager<BusinessPerson, BusinessPersons>
	{
		// Initializes an object instance.
		/// <include path='items/ObjectManagerC/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public BusinessPersonManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "BusinessPerson") : base(dbServiceRef, dataConfigName, tableName)
		{
			// Map table names with property names or captions
			// that differ from the column names.
		}
	}
}
