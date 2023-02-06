// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CodeTypeManager.cs
using LJCDBClientLib;
using LJCNetCommon;
using System.Collections.Generic;

namespace CVRDAL
{
  /// <summary>Provides Table specific data manipulation methods.</summary>
  public class CodeTypeManager
    : ObjectManager<CodeType, CodeTypes>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/ObjectManagerC/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
    public CodeTypeManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "CodeType")
      : base(dbServiceRef, dataConfigName, tableName)
    {
      // Create the list of database assigned columns.
      SetDbAssignedColumns(new string[]
      {
        CodeType.ColumnID
      });

      // Create the list of lookup column names.
      SetLookupColumns(new string[]
      {
        CodeType.ColumnCode
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
    /// <include path='items/RetrieveWithID/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
    public CodeType RetrieveWithID(int id, List<string> propertyNames = null)
    {
      var keyColumns = GetIDKey(id);
      return Retrieve(keyColumns, propertyNames);
    }
    #endregion

    #region GetKey Methods

    // Gets the ID key record.
    /// <include path='items/GetIDKey/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
    public DbColumns GetIDKey(int id)
    {
      var retValue = new DbColumns()
      {
        { CodeType.ColumnID, id }
      };
      return retValue;
    }
    #endregion
  }
}
