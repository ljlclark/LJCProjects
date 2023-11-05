// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CodeTypeClassManager.cs
using LJCDBClientLib;
using LJCNetCommon;
using System.Collections.Generic;

namespace CVRDAL
{
  /// <summary>Provides Table specific data manipulation methods.</summary>
  public class CodeTypeClassManager
    : ObjectManager<CodeTypeClass, CodeTypeClasses>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/ObjectManagerC/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public CodeTypeClassManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "CodeTypeClass")
      : base(dbServiceRef, dataConfigName, tableName)
    {
      // Create the list of database assigned columns.
      SetDbAssignedColumns(new string[]
      {
        CodeTypeClass.ColumnID
      });

      // Create the list of lookup column names.
      SetLookupColumns(new string[]
      {
        CodeTypeClass.ColumnCode
      });
    }
    #endregion

    #region Load/Retrieve Methods

    /// <summary>
    /// Deletes a Data Record with the supplied values.
    /// </summary>
    /// <param name="id">The ID value.</param>
    public void DeleteWithID(int id)
    {
      var keyColumns = GetIDKey(id);
      Delete(keyColumns);
    }

    // Retrieves a Data Record with the supplied value.
    /// <include path='items/RetrieveWithID/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public CodeTypeClass RetrieveWithID(int id, List<string> propertyNames = null)
    {
      var keyColumns = GetIDKey(id);
      return Retrieve(keyColumns, propertyNames);
    }
    #endregion

    #region GetKey Methods

    // Gets the ID key record.
    /// <include path='items/GetIDKey/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public DbColumns GetIDKey(int id)
    {
      var retValue = new DbColumns()
      {
        { CodeTypeClass.ColumnID, id }
      };
      return retValue;
    }
    #endregion
  }
}
