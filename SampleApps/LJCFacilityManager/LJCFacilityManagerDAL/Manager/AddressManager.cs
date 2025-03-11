// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// AddressManager.cs
using System.Collections.Generic;
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using LJCRegionDAL;

namespace LJCFacilityManagerDAL
{
	/// <summary>Provides Address specific data manipulation methods.</summary>
	public class AddressManager : ObjectManager<Address, Addresses>
	{
		// Initializes an object instance.
		/// <include path='items/ObjectManagerC/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public AddressManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "Address") : base(dbServiceRef, dataConfigName, tableName)
		{
			// Map table names with property names or captions
			// that differ from the column names.
			MapNames(Address.ColumnPostalCode, caption: "Postal Code");

			// Add calculated and join columns.
			// Enables populating a Data Object and adding to a grid configuration.
			DataDefinition.Add(Address.ColumnCityStateZip, caption: "City, State Zip");
			DataDefinition.Add(RegionData.ColumnName, Address.PropertyRegionName
				, Address.PropertyRegionName, "Region Name");
			DataDefinition.Add(Province.ColumnName, Address.PropertyProvinceName
				, Address.PropertyProvinceName, "Province Name");
			DataDefinition.Add(City.ColumnName, Address.PropertyCityName
				, Address.PropertyCityName, "City Name");
			DataDefinition.Add(CitySection.ColumnName, Address.PropertyCitySectionName
				, Address.PropertyCitySectionName, "City Section Name");

			// Create the list of database assigned columns.
			SetDbAssignedColumns(new string[]
			{
				Address.ColumnID
			});

			// Create the list of lookup column names.
			SetLookupColumns(new string[]
			{
				Address.ColumnProvinceID,
				Address.ColumnCityID,
				Address.ColumnStreet,
			});
		}

		#region Retrieve/Load Methods

		/// <summary>
		/// Loads a collection of data records ordered by Description.
		/// </summary>
		/// <returns>The collection of data records.</returns>
		public Addresses LoadJoinByCodeTypeID()
		{
			Addresses retValue;

			DbJoins dbJoins = GetLoadJoins();
			SetOrderByCodeTypeID();
			retValue = Load(null, joins: dbJoins);
			return retValue;
		}

		/// <summary>
		/// Loads a collection of data records.
		/// </summary>
		/// <param name="codeTypeID">The code type ID.</param>
		/// <returns>The collection of data records.</returns>
		public Addresses LoadJoinWithCodeTypeID(int codeTypeID)
		{
			Addresses retValue;

			var keyColumns = GetCodeTypeIDKey(codeTypeID);
			DbJoins dbJoins = GetLoadJoins();
			retValue = Load(keyColumns, joins: dbJoins);
			return retValue;
		}

		/// <summary>
		/// Retrieves a data record with the supplied value.
		/// </summary>
		/// <param name="id">The ID value.</param>
		/// <returns>The data record.</returns>
		public Address RetrieveJoinWithID(int id)
		{
			Address retValue;

			var keyColumns = GetIDKey(id);
			DbJoins dbJoins = GetLoadJoins();
			retValue = Retrieve(keyColumns, joins: dbJoins);
			return retValue;
		}
		#endregion

		#region GetKey Methods

		// Get the ID key record.
		/// <include path='items/GetIDKey/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
		public DbColumns GetIDKey(int id)
		{
			var retValue = new DbColumns()
			{
				{ Address.ColumnID, id }
			};
			return retValue;
		}

		/// <summary>
		/// Get the CodeTypeID key record.
		/// </summary>
		/// <param name="codeTypeID">The ID value.</param>
		/// <returns>The CodeTypeID key record.</returns>
		public DbColumns GetCodeTypeIDKey(int codeTypeID)
		{
			var retValue = new DbColumns()
			{
				{ Address.ColumnCodeTypeID, codeTypeID }
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
			// dbColumns.Add(string columnName, string propertyName = null
			// , string renameAs = null, string dataTypeName = "String", string caption = null) 
			dbJoin = new DbJoin
			{
				TableName = "Region",
				JoinType = "left",
				JoinOns = new DbJoinOns() {
					{ Address.ColumnRegionID, RegionData.ColumnID }
					// Testing
					//{ Address.ColumnRegionID, RegionData.ColumnRegionID }
				},
				Columns = new DbColumns() {
					{ RegionData.ColumnName, Address.PropertyRegionName, Address.PropertyRegionName }
				}
			};

			// Testing
			bool isTesting = false;
			if (isTesting)
			{
				DbJoinOn dbJoinOn = dbJoin.JoinOns[0];
				dbJoinOn.JoinOns = new DbJoinOns()
				{
					{ Address.ColumnRegionID, RegionData.ColumnID }
				};
			}

			retValue.Add(dbJoin);

			dbJoin = new DbJoin
			{
				TableName = "Province",
				JoinType = "left",
				JoinOns = new DbJoinOns() {
					{ Address.ColumnProvinceID, Province.ColumnID }
				},
				Columns = new DbColumns() {
					{ Province.ColumnName, Address.PropertyProvinceName, Address.PropertyProvinceName }
				}
			};
			retValue.Add(dbJoin);

			dbJoin = new DbJoin
			{
				TableName = "City",
				JoinType = "left",
				JoinOns = new DbJoinOns() {
					{ Address.ColumnCityID, City.ColumnID }
				},
				Columns = new DbColumns() {
					{ City.ColumnName, Address.PropertyCityName, Address.PropertyCityName }
				}
			};
			retValue.Add(dbJoin);

			dbJoin = new DbJoin
			{
				TableName = "CitySection",
				JoinType = "left",
				JoinOns = new DbJoinOns() {
					{ Address.ColumnCitySectionID, CitySection.ColumnID }
				},
				Columns = new DbColumns() {
					{ CitySection.ColumnName, Address.PropertyCitySectionName, Address.PropertyCitySectionName }
				}
			};
			retValue.Add(dbJoin);

			dbJoin = new DbJoin
			{
				TableName = "CodeType",
				JoinType = "left",
				JoinOns = new DbJoinOns() {
					{ Address.ColumnCodeTypeID, CodeType.ColumnID }
				},
				Columns = new DbColumns() {
					{ CodeType.ColumnDescription, "TypeDescription", "TypeDescription" }
				}
			};
			retValue.Add(dbJoin);

			return retValue;
		}
		#endregion

		#region Filters

		/// <summary>
		/// Creates and returns the Load filters object.
		/// </summary>
		/// <param name="columnName">The column name.</param>
		/// <param name="inValues">The SQL "in" values.</param>
		/// <returns>The DbFilter object.</returns>
		public DbFilters GetLoadFilters(string columnName, string inValues)
		{
			DbFilters retValue = new DbFilters();

			bool isTesting = false;
			if (!isTesting)
			{
				DbFilter dbFilter = new DbFilter();
				dbFilter.ConditionSet.Conditions.Add(columnName, inValues, "in");
				retValue = new DbFilters {
					dbFilter};
			}
			else
			{
				DbFilter dbFilter = new DbFilter();
				DbConditions conditions = dbFilter.ConditionSet.Conditions;
				conditions.Add("FirstName", "'John'");
				conditions.Add("LastName", "'Smith'");

				// Adding an "or" filter.
				DbFilter secondFilter = new DbFilter
				{
					BooleanOperator = "or"
				};
				DbConditions secondConditions = secondFilter.ConditionSet.Conditions;
				secondConditions.Add("Street", "'Somewhere%'", "like");
				//dbFilters.Add(secondFilter);

				// where ((FirstName = 'John' and LastName = 'Smith'))
				//  or ((Street like 'Somewhere%'))

				// If the second filter is incuded inside the first as in the following
				// example,then the second filter conditions are grouped with the first
				// filter conditions.
				dbFilter.Filters.Add(secondFilter);

				// where ((FirstName = 'John' and LastName = 'Smith')
				//  or (Street like 'Somewhere%'))
			}
			return retValue;
		}
		#endregion

		#region OrderBys

		/// <summary>Sets the current OrderBy names.</summary>
		public void SetOrderByCodeTypeID() => DataManager.OrderByNames = new List<string>() {
				Address.ColumnCodeTypeID};
		#endregion
	}
}
