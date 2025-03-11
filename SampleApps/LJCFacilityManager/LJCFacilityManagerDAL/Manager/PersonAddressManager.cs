// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// PersonAddressManager.cs
using System;
using LJCDBClientLib;

namespace LJCFacilityManagerDAL
{
	/// <summary>Provides PersonAddress specific data manipulation methods.</summary>
	public class PersonAddressManager : ObjectManager<PersonAddress, PersonAddresses>
	{
		// Initializes an object instance.
		/// <include path='items/ObjectManagerC/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public PersonAddressManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "PersonAddress") : base(dbServiceRef, dataConfigName, tableName)
		{
			// Map table names with property names or captions
			// that differ from the column names.
		}
	}
}
