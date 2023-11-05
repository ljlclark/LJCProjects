// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// UnitTypeManager.cs
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using System.Collections.Generic;

namespace LJCUnitMeasureDAL
{
  /// <summary>Provides Table specific data manipulation methods.</summary>
  /// <remarks>
  /// <para>-- Library Level Remarks</para>
  /// </remarks>
  public class UnitTypeManager
    : ObjectManager<UnitType, UnitTypes>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/ObjectManagerC/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public UnitTypeManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "UnitType")
      : base(dbServiceRef, dataConfigName, tableName)
    {
      // Create the list of database assigned columns.
      // And make sure the AutoIncrement value is set.
      SetDbAssignedColumns(new string[]
      {
        UnitType.ColumnID
      });

      // Create the list of lookup column names.
      SetLookupColumns(new string[]
      {
        UnitType.ColumnName
      });
    }
    #endregion

    #region Load/Retrieve Methods

    // Retrieves a Data Record with the supplied value.
    /// <include path='items/RetrieveWithID/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public UnitType RetrieveWithID(int id, List<string> propertyNames = null)
    {
      var keyColumns = GetIDKey(id);
      return Retrieve(keyColumns, propertyNames);
    }
    #endregion

    #region GetKey Methods

    // Loads a collection of data records ordered by Description.
    /// <include path='items/LoadByDescription/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public UnitTypes LoadByDescription(DbColumns keyColumns = null
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      return Load(keyColumns, propertyNames, filters, joins);
    }

    // Gets the ID key record.
    /// <include path='items/GetIDKey/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public DbColumns GetIDKey(int id)
    {
      var retValue = new DbColumns()
      {
        { UnitType.ColumnID, id }
      };
      return retValue;
    }
    #endregion

    #region Other Public Methods

    // Check for duplicate unique key.
    /// <include path='items/IsDuplicate/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public bool IsDuplicate(UnitType lookupRecord, UnitType currentRecord
      , bool isUpdate = false)
    {
      bool retValue = false;

      if (lookupRecord != null)
      {
        if (false == isUpdate)
        {
          // Duplicate for "New" record that already exists.
          retValue = true;
        }
        else
        {
          if (lookupRecord.ID != currentRecord.ID)
          {
            // Duplicate for "Update" where unique key is modified.
            retValue = true;
          }
        }
      }
      return retValue;
    }
    #endregion
  }
}
