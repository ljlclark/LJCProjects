// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// UnitConversionManager.cs
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCUnitMeasureDAL
{
  /// <summary>Provides Table specific data manipulation methods.</summary>
  public class UnitConversionManager
    : ObjectManager<UnitConversion, UnitConversions>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/ObjectManagerC/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public UnitConversionManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "UnitConversion")
      : base(dbServiceRef, dataConfigName, tableName)
    {
      // Create the list of lookup column names.
      SetLookupColumns(new string[]
      {
        UnitConversion.ColumnFromUnitMeasureID,
        UnitConversion.ColumnToUnitMeasureID
      });
    }
    #endregion

    #region Load/Retrieve Methods

    // Retrieves a Data Record with the supplied values.
    /// <summary>
    /// Retrieves a Data Record with the supplied values.
    /// </summary>
    /// <param name="fromMeasureUnitID">The 'From' UnitID value.</param>
    /// <param name="toMeasureUnitID">The 'To' UnitID value.</param>
    /// <returns>The UnitConversion object if found.</returns>
    public UnitConversion RetrieveWithIDs(int fromMeasureUnitID, int toMeasureUnitID)
    {
      var keyColumns = GetIDKeys(fromMeasureUnitID, toMeasureUnitID);
      return Retrieve(keyColumns);
    }
    #endregion

    #region GetKey Methods

    // Gets the ID key record.
    /// <summary>
    /// Gets the ID key record.
    /// </summary>
    /// <param name="fromMeasureUnitID">The 'From' UnitID value.</param>
    /// <param name="toMeasureUnitID">The 'To' UnitID value.</param>
    /// <returns>The IDKeys value.</returns>
    public DbColumns GetIDKeys(int fromMeasureUnitID, int toMeasureUnitID)
    {
      var retValue = new DbColumns()
      {
        { UnitConversion.ColumnFromUnitMeasureID, fromMeasureUnitID },
        { UnitConversion.ColumnToUnitMeasureID, toMeasureUnitID }
      };
      return retValue;
    }
    #endregion

    #region Other Public Methods

    // Check for duplicate unique key.
    /// <include path='items/IsDuplicate/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public bool IsDuplicate(UnitConversion lookupRecord, UnitConversion currentRecord
      , bool isUpdate = false)
    {
      bool retValue = false;

      if (lookupRecord != null)
      {
        if (!isUpdate)
        {
          // Duplicate for "New" record that already exists.
          retValue = true;
        }
        else
        {
          if (lookupRecord.FromUnitMeasureID != currentRecord.FromUnitMeasureID
            && lookupRecord.ToUnitMeasureID != currentRecord.ToUnitMeasureID)
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
