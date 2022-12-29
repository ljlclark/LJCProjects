// Copyright (c) Lester J Clark 2020 - All Rights Reserved
using System;
using LJCNetCommon;
using LJCDBMessage;
using LJCDBClientLib;
using System.Collections.Generic;

namespace CVRDAL
{
	/// <summary>Provides Table specific data manipulation methods.</summary>
	public class CVVisitManager
		: ObjectManager<CVVisit, CVVisits>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/ObjectManagerC/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
		public CVVisitManager(DbServiceRef dbServiceRef, string dataConfigName
			, string tableName = "CVVisit")
			: base(dbServiceRef, dataConfigName, tableName)
		{
			// Add Calculated and Join columns.
			// Enables populating a Data Object and adding to a grid configuration.
			DataDefinition.Add(CVVisit.JoinFirstName);
			DataDefinition.Add(CVVisit.JoinMiddleName);
			DataDefinition.Add(CVVisit.JoinLastName);

			// Create the list of database assigned columns.
			SetDbAssignedColumns(new string[]
				{
					CVVisit.ColumnID
				});

			// Create the list of lookup column names.
			SetLookupColumns(new string[]
				{
					CVVisit.ColumnFacilityID,
					CVVisit.ColumnCVPersonID,
					CVVisit.ColumnRegisterTime
				});
		}
		#endregion

		#region Retrieve/Load Methods

		// Deletes a Data Record with the supplied value.
		/// <include path='items/DeleteWithID/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
		public void DeleteWithID(long id)
		{
			var keyColumns = GetIDKey(id);
			Delete(keyColumns);
		}

		/// <summary>
		/// Loads a collection of DataObjects with Join data. 
		/// </summary>
		/// <returns>The DataObject collection.</returns>
		public CVVisits LoadWithJoins(int facilityID, DbFilters dbFilters = null)
		{
			var keyColumns = GetFacilityIDKey(facilityID);
			DbJoins dbJoins = GetJoins();
			return Load(keyColumns, filters: dbFilters, joins: dbJoins);
		}

		// Retrieves a Data Record with the supplied value.
		/// <include path='items/RetrieveWithID/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
		public CVVisit RetrieveWithID(long id, List<string> propertyNames = null)
		{
			var keyColumns = GetIDKey(id);
			DbJoins dbJoins = GetJoins();
			return Retrieve(keyColumns, propertyNames, joins: dbJoins);
		}
		#endregion

		#region GetKey Methods

		// Gets the ID key record.
		/// <include path='items/GetIDKey/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
		public DbColumns GetFacilityIDKey(int id)
		{
			var retValue = new DbColumns()
			{
				{ CVVisit.ColumnFacilityID, id }
			};
			return retValue;
		}

		// Gets the ID key record.
		/// <include path='items/GetIDKey/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
		public DbColumns GetIDKey(long id)
		{
			var retValue = new DbColumns()
			{
				{ CVVisit.ColumnID, id }
			};
			return retValue;
		}
		#endregion

		#region KeyItem Methods

		// Creates the RecordColumns object.
		/// <include path='items/DataColumns/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
		public DbColumns DataColumns(long id)
		{
			DbColumns retValue = null;

			// Use common data definitions.
			var keyColumns = GetIDKey((int)id);
			var dbResult = DataManager.Retrieve(keyColumns);
			if (DbResult.HasRows(dbResult))
			{
				retValue = dbResult.GetValueColumns(dbResult.Rows[0].Values);
			}
			return retValue;
		}

		// Creates the KeyItem object.
		/// <include path='items/GetKeyItem/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
		public KeyItem GetKeyItem(string propertyName, long id)
		{
			KeyItem retValue = null;

			var record = RetrieveWithID((int)id);
			if (record != null)
			{
				retValue = new KeyItem
				{
					Description = record.LastName,
					ID = id,
					MaxLength = CVPerson.LengthLastName,
					PrimaryKeyName = CVVisit.ColumnID,
					PropertyName = propertyName,
					TableName = CVVisit.TableName
				};
			}
			return retValue;
		}
		#endregion

		#region Joins

		// Creates and returns the Load Joins object.
		/// <include path='items/GetLoadJoins/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
		public DbJoins GetJoins()
		{
			DbJoin dbJoin;
			DbJoins retValue = new DbJoins();

			// Note: JoinOn Columns must have properties in the DataObject
			// to recieve the join values.
			// The RenameAs property is required if there is another table column
			// with the same name.
			// Note: dbColumns.Add(string columnName, string propertyName = null
			// , string renameAs = null, string dataTypeName = "String", string caption = null) 

			// CVPerson.FirstName,
			// CVPerson.MiddleName,
			// CVPerson.LastName
			//left join CVPerson
			// on ((CVVisit.CVPersonID = CVPerson.ID))
			dbJoin = new DbJoin
			{
				TableName = "CVPerson",
				JoinType = "left",
				JoinOns = new DbJoinOns()
				{
					{ CVVisit.ColumnCVPersonID, CVPerson.ColumnID }
				},
				Columns = new DbColumns()
				{
			    // columnName, propertyName = null, renameAs = null, dataTypeName = "String", caption = null
					{ CVPerson.ColumnFirstName },
					{ CVPerson.ColumnMiddleName },
					{ CVPerson.ColumnLastName }
				}
			};
			retValue.Add(dbJoin);
			return retValue;
		}
		#endregion
	}
}
