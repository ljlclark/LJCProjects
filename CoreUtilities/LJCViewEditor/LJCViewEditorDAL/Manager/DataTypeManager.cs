// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataTypeManager.cs
using LJCDBClientLib;

namespace LJCViewEditorDAL
{
  /// <summary>Provides Table specific data manipulation methods.</summary>
  public class DataTypeManager
    : ObjectManager<DataType, DataTypes>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/ObjectManagerC/*' file='../../LJCDocLib/Common/Manager.xml'/>
    public DataTypeManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "DataType")
      : base(dbServiceRef, dataConfigName, tableName)
    {
      // Create the list of database assigned columns.
      SetDbAssignedColumns(new string[]
      {
        DataType.ColumnDataTypeID
      });

      // Create the list of lookup column names.
      SetLookupColumns(new string[]
      {
        DataType.ColumnName
      });
    }
    #endregion
  }
}
