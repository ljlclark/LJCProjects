// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataTable2.cs
using LJCNetCommon;
using System.Collections.Generic;

namespace LJCDataUtilityDAL
{
  // Represents the DataTable data.
  /// <include file='Doc/DataTable2.xml'
  ///  path='members/DataUtilTableNew/*'/>
  public class DataTable2 : DbColumns
  {
    #region Constructors

    // Initializes an object instance.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public DataTable2()
    {
      // columnName, propertyName, renameAs, dataTypeName
      //   , caption, maxLength

      // Primary Key
      var dbColumn = Add("ID", dataTypeName: "long");
      dbColumn.AutoIncrement = true;
      dbColumn.IsPrimaryKey = true;
      dbColumn.SQLTypeName = "bigint";

      // Primary Key, Foreign Key
      dbColumn = Add("DataSiteID", dataTypeName: "long");
      dbColumn.IsPrimaryKey = true;
      dbColumn.KeyType = "Foreign";
      dbColumn.SQLTypeName = "bigint";

      // Foreign Key
      dbColumn = Add("DataModuleID", dataTypeName: "long");
      dbColumn.KeyType = "Foreign";
      dbColumn.SQLTypeName = "bigint";
      dbColumn = Add("DataModuleSiteID", dataTypeName: "long");
      dbColumn.KeyType = "Foreign";
      dbColumn.SQLTypeName = "bigint";

      // Unique Key
      dbColumn = Add("Name");
      dbColumn.KeyType = "Natural";
      dbColumn.Unique = true;

      Add("Description");

      // Custom table values.
      dbColumn = Add("Sequence", dataTypeName: "int");
      dbColumn.SQLTypeName = "int";

      Add("SchemaName");
      Add("NewName");
    }

    // Initializes an object instance with the supplied object.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/CopyConstructor/*'/>
    public DataTable2(DataTable2 item) : this()
    {
      LJCSetValue(ColumnID, item.LJCGetValue(ColumnID));
      LJCSetValue(ColumnDataSiteID, item.LJCGetValue(ColumnDataSiteID));
      LJCSetValue(ColumnDataModuleID, item.LJCGetValue(ColumnDataModuleID));
      LJCSetValue(ColumnDataModuleSiteID, item.LJCGetValue(ColumnDataModuleSiteID));
      LJCSetValue(ColumnName, item.LJCGetValue(ColumnName));
      LJCSetValue(ColumnDescription, item.LJCGetValue(ColumnDescription));

      LJCSetValue(ColumnSequence, item.LJCGetValue(ColumnSequence));
      LJCSetValue(ColumnSchemaName, item.LJCGetValue(ColumnSchemaName));
      LJCSetValue(ColumnNewName, item.LJCGetValue(ColumnNewName));
    }
    #endregion

    #region Class Data

    /// <summary></summary>
    public const string ColumnID = "ID";

    /// <summary></summary>
    public const string ColumnDataSiteID = "DataSiteID";

    /// <summary></summary>
    public const string ColumnDataModuleID = "DataModuleID";

    /// <summary></summary>
    public const string ColumnDataModuleSiteID = "DataModuleSiteID";

    /// <summary></summary>
    public const string ColumnName = "Name";

    /// <summary></summary>
    public const string ColumnDescription = "Description";

    /// <summary></summary>
    public const string ColumnSequence = "Sequence";

    /// <summary></summary>
    public const string ColumnSchemaName = "SchemaName";

    /// <summary></summary>
    public const string ColumnNewName = "NewName";
    #endregion
  }

  #region Comparers

  // Sort and search on Name value.
  /// <include file='Doc/DataTable2.xml'
  ///  path='members/DataTableUniqueComparerNew/*'/>
  public class DataTable2Comparer : IComparer<DataTable2>
  {
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/ColumnNames/*'/>
    public List<string> LJCColumnNames { get; set; }

    // Compares two objects.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='items/Compare/*'/>
    public int Compare(DataTable2 x, DataTable2 y)
    {
      int retValue;

      // Check for null objects.
      retValue = NetCommon.CompareNull(x, y);

      while (true)
      {
        // Check for null values.
        if (null == LJCColumnNames
          || retValue != NetString.CompareNotNullOrEqual)
        {
          break;
        }

        foreach (string columnName in LJCColumnNames)
        {
          var xValue = x.LJCGetString(columnName);
          var yValue = y.LJCGetString(columnName);
          retValue = NetCommon.CompareNull(xValue, yValue);
          if (retValue != NetString.CompareNotNullOrEqual)
          {
            break;
          }
        }
        if (retValue != NetString.CompareNotNullOrEqual)
        {
          break;
        }

        for (int index = 0; index < LJCColumnNames.Count; index++)
        {
          var columnName = LJCColumnNames[index];
          var xValue = x.LJCGetString(columnName);
          var yValue = y.LJCGetString(columnName);

          if (index < LJCColumnNames.Count - 1)
          {
            // Compare parent keys if neither value is null.
            retValue = xValue.CompareTo(yValue);
            if (retValue != NetString.CompareEqual)
            {
              break;
            }
          }
          else
          {
            // Compare value if parent keys are equal.
            retValue = xValue.CompareTo(yValue);
          }
        }
        break;
      }
      return retValue;
    }
  }
  #endregion
}
