// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// UnitSystemManager.cs
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
  public class UnitSystemManager
    : ObjectManager<UnitSystem, UnitSystems>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/ObjectManagerC/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public UnitSystemManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "UnitSystem")
      : base(dbServiceRef, dataConfigName, tableName)
    {
      // Create the list of database assigned columns.
      SetDbAssignedColumns(new string[]
      {
        UnitSystem.ColumnID
      });

      // Create the list of lookup column names.
      SetLookupColumns(new string[]
      {
        UnitSystem.ColumnName
      });
    }
    #endregion

    #region Load/Retrieve Methods

    // Loads a collection of data records ordered by Description.
    /// <include path='items/LoadByDescription/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public UnitSystems LoadByDescription(DbColumns keyColumns = null
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      return Load(keyColumns, propertyNames, filters, joins);
    }

    // Retrieves a Data Record with the Code value.
    /// <include path='items/RetrieveWithCode/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public UnitSystem RetrieveWithCode(string code, List<string> propertyNames = null)
    {
      var keyColumns = GetCodeKey(code);
      return Retrieve(keyColumns, propertyNames);
    }

    // Retrieves a Data Record with the supplied value.
    /// <include path='items/RetrieveWithID/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public UnitSystem RetrieveWithID(int id, List<string> propertyNames = null)
    {
      var keyColumns = GetIDKey(id);
      return Retrieve(keyColumns, propertyNames);
    }
    #endregion

    #region GetKey Methods

    // Gets the Code key record.
    /// <include path='items/GetCodeKey/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public DbColumns GetCodeKey(string code)
    {
      var retValue = new DbColumns()
      {
        { UnitSystem.ColumnCode, (object)code }
      };
      return retValue;
    }

    // Gets the ID key record.
    /// <include path='items/GetIDKey/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public DbColumns GetIDKey(int id)
    {
      var retValue = new DbColumns()
      {
        { UnitSystem.ColumnID, id }
      };
      return retValue;
    }
    #endregion

    #region Other Public Methods

    // Check for duplicate unique key.
    /// <include path='items/IsDuplicate/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public bool IsDuplicate(UnitSystem lookupRecord, UnitSystem currentRecord
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
