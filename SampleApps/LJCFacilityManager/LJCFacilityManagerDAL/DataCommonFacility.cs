// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataCommonFacility.cs
using System;
using System.Collections.Generic;
using LJCNetCommon;

namespace LJCFacilityManagerDAL
{
	/// <summary>Provides common data methods.</summary>
	public class DataCommonFacility
	{
		#region Retrieve Record Methods

		// Retrieves the facility by ID.
		/// <include path='items/GetFacility/*' file='Doc/DataCommonFacility.xml'/>
		public static Facility GetFacility(FacilityDbManager manager, int facilityID)
		{
			Facility retValue;

			var keyColumns = new DbColumns()
			{
				{ Facility.ColumnID, facilityID }
			};
			retValue = manager.Retrieve(keyColumns);
			return retValue;
		}

		// Retrieves the Unit by ID.
		/// <include path='items/GetUnit/*' file='Doc/DataCommonFacility.xml'/>
		public static Unit GetUnit(UnitManager manager, int unitID)
		{
			Unit retValue;

			retValue = manager.RetrieveWithID(unitID);
			return retValue;
		}

		// Retrieves the Person by ID.
		/// <include path='items/GetPerson1/*' file='Doc/DataCommonFacility.xml'/>
		public static Person GetPerson(PersonManager manager, int personID)
		{
			return (GetPerson(manager, null, personID));
		}

		// Retrieves the Person by ID with the specified columns.
		/// <include path='items/GetPerson2/*' file='Doc/DataCommonFacility.xml'/>
		public static Person GetPerson(PersonManager manager
			, List<string> columnNames, int personID)
		{
			Person retValue;

			var keyColumns = new DbColumns()
			{
				{ Person.ColumnID, personID }
			};
			retValue = manager.Retrieve(keyColumns, columnNames);
			return retValue;
		}
		#endregion

		#region Retrieve Name Methods

		// Retrieves the Facility description.
		/// <include path='items/GetFacilityText/*' file='Doc/DataCommonFacility.xml'/>
		public static string GetFacilityText(FacilityDbManager manager, int facilityID)
		{
			Facility record;
			string retValue = null;

			var keyColumns = new DbColumns()
			{
				{ Facility.ColumnID, facilityID }
			};
			List<string> propertyNames = new List<string>()
			{
				Facility.ColumnDescription
			};
			record = manager.Retrieve(keyColumns, propertyNames);
			if (record != null)
			{
				retValue = record.Description;
			}
			return retValue;
		}

		// Retrieves the Unit description.
		/// <include path='items/GetUnitText/*' file='Doc/DataCommonFacility.xml'/>
		public static string GetUnitText(UnitManager manager, int unitID)
		{
			Unit record;
			string retValue = null;

			var keyColumns = new DbColumns()
			{
				{ Unit.ColumnID, unitID }
			};
			List<string> propertyNames = new List<string>()
			{
				Unit.ColumnDescription
			};
			record = manager.Retrieve(keyColumns, propertyNames);
			if (record != null)
			{
				retValue = record.Description;
			}
			return retValue;
		}

		// Retrieves the Person name.
		/// <include path='items/GetPersonName/*' file='Doc/DataCommonFacility.xml'/>
		public static string GetPersonName(PersonManager manager, int personID)
		{
			Person record;
			string retValue = null;

			var keyColumns = new DbColumns()
			{
				{ Person.ColumnID, personID }
			};
			List<string> propertyNames = new List<string>()
			{
				Person.ColumnFirstName,
				Person.ColumnMiddleInitial,
				Person.ColumnLastName
			};
			record = manager.Retrieve(keyColumns, propertyNames);
			if (record != null)
			{
				retValue = record.FullName;
			}
			return retValue;
		}

		// Retrieves the Business name.
		/// <include path='items/GetBusinessName/*' file='Doc/DataCommonFacility.xml'/>
		public static string GetBusinessName(BusinessManager manager, int businessID)
		{
			Business record;
			string retValue = null;

			var keyColumns = new DbColumns()
			{
				{ Business.ColumnID, businessID }
			};
			List<string> propertyNames = new List<string>()
			{
				Business.ColumnName
			};
			record = manager.Retrieve(keyColumns, propertyNames);
			if (record != null)
			{
				retValue = record.Name;
			}
			return retValue;
		}
		#endregion
	}
}
