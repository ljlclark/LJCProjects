// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// BusinessAddressManager.cs
using System;
using LJCDBClientLib;

namespace LJCFacilityManagerDAL
{
	/// <summary>Provides BusinessAddress specific data manipulation methods.</summary>
	public class BusinessAddressManager : ObjectManager<BusinessAddress, BusinessAddresses>
	{
		// Initializes an object instance.
		/// <include path='items/ObjectManagerC/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public BusinessAddressManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "BusinessAddress") : base(dbServiceRef, dataConfigName, tableName)
		{
			// Map table names with property names or captions
			// that differ from the column names.
		}
	}
}
