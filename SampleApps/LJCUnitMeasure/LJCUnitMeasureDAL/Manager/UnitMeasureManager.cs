// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// UnitMeasureManager.cs
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
  public class UnitMeasureManager
    : ObjectManager<UnitMeasure, UnitMeasures>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/ObjectManagerC/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
    public UnitMeasureManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "UnitMeasure")
      : base(dbServiceRef, dataConfigName, tableName)
    {
      // Create the list of database assigned columns.
      SetDbAssignedColumns(new string[]
      {
        UnitMeasure.ColumnID
      });

      // Create the list of lookup column names.
      SetLookupColumns(new string[]
      {
        UnitMeasure.ColumnName
      });
    }
    #endregion

    #region Load/Retrieve Methods

    // Loads a collection of data records ordered by Description.
    /// <include path='items/LoadWithCodes/*' file='../Doc/UnitMeasureManager.xml'/>
    public UnitMeasures LoadWithCodes(string categoryCode, string systemCode = null)
    {
      DbColumns keyColumns;
      UnitMeasures retValue;

      var unitCategoryManager = new UnitCategoryManager(DbServiceRef
        , DataConfigName);
      var unitCategory = unitCategoryManager.RetrieveWithCode(categoryCode);
      if (systemCode != null)
      {
        var unitSystemManager = new UnitSystemManager(DbServiceRef
          , DataConfigName);
        var unitSystem = unitSystemManager.RetrieveWithCode(systemCode);
        keyColumns = new DbColumns()
        {
          { UnitMeasure.ColumnUnitCategoryID, unitCategory.ID },
          { UnitMeasure.ColumnUnitSystemID, unitSystem.ID }
        };
      }
      else
      {
        keyColumns = new DbColumns()
        {
          { UnitMeasure.ColumnUnitCategoryID, unitCategory.ID }
        };
      }

      retValue = Load(keyColumns);
      return retValue;
    }

    // Loads a collection of data records ordered by Description.
    /// <include path='items/LoadByDescription/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
    public UnitMeasures LoadByDescription(DbColumns keyColumns = null
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      SetOrderByDescription();
      return Load(keyColumns, propertyNames, filters, joins);
    }

    // Loads a collection of data records ordered by Sequence.
    /// <include path='items/LoadBySequence/*' file='../Doc/UnitMeasureManager.xml'/>
    public UnitMeasures LoadBySequence(DbColumns keyColumns)
    {
      SetOrderBySequence();
      return Load(keyColumns);
    }

    // Retrieves a Data Record with the Code value.
    /// <include path='items/RetrieveWithCode/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
    public UnitMeasure RetrieveWithCode(string code, List<string> propertyNames = null)
    {
      var keyColumns = GetCodeKey(code);
      return Retrieve(keyColumns, propertyNames);
    }

    // Retrieves a Data Record with the supplied value.
    /// <include path='items/RetrieveWithID/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
    public UnitMeasure RetrieveWithID(int id, List<string> propertyNames = null)
    {
      var keyColumns = GetIDKey(id);
      return Retrieve(keyColumns, propertyNames);
    }

    // Convert units.
    /// <include path='items/ConvertUnit/*' file='../Doc/UnitMeasureManager.xml'/>
    public object ConvertUnit(int fromUnitMeasureID, int toUnitMeasureID
      , object value)
    {
      object retValue = null;

      var unitConversionManager = new UnitConversionManager(DbServiceRef
        , DataConfigName);
      var unitConversion = unitConversionManager.RetrieveWithIDs(fromUnitMeasureID
        , toUnitMeasureID);
      if (unitConversion != null)
      {
        retValue = unitConversion.ConvertUnit(value);
      }
      return retValue;
    }
    #endregion

    #region GetKey Methods

    // Gets the Code key record.
    /// <include path='items/GetCodeKey/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
    public DbColumns GetCodeKey(string code)
    {
      var retValue = new DbColumns()
      {
        { UnitMeasure.ColumnCode, (object)code }
      };
      return retValue;
    }

    // Gets the ID key record.
    /// <include path='items/GetIDKey/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
    public DbColumns GetIDKey(int id)
    {
      var retValue = new DbColumns()
      {
        { UnitMeasure.ColumnID, id }
      };
      return retValue;
    }
    #endregion

    #region OrderBys

    /// <summary>Sets the current OrderBy names.</summary>
    public void SetOrderByDescription() => DataManager.OrderByNames = new List<string>()
    {
      UnitMeasure.ColumnDescription
    };

    /// <summary>Sets the current OrderBy names.</summary>
    public void SetOrderBySequence() => DataManager.OrderByNames = new List<string>()
    {
      UnitMeasure.ColumnSequence
    };
    #endregion

    #region Other Public Methods

    // Check for duplicate unique key.
    /// <include path='items/IsDuplicate/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
    public bool IsDuplicate(UnitMeasure lookupRecord, UnitMeasure currentRecord
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
