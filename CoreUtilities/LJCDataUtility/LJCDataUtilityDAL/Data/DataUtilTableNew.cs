using LJCNetCommon;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace LJCDataUtilityDAL
{
  // Represents the DataTable data.
  /// <include file='Doc/XDataUtilTable.xml'
  ///  path='members/XDataUtilTable/*'/>
  public class DataUtilTableNew : DbColumns
  {
    #region Constructors

    // Initializes an object instance.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public DataUtilTableNew()
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
    public DataUtilTableNew(DataUtilTableNew item) : this()
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

  /// <summary>Sort and search on Name value.</summary>
  public class DataTableUniqueComparerNew : IComparer<DataUtilTableNew>
  {
    // Compares two objects.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='items/Compare/*'/>
    public int Compare(DataUtilTableNew x, DataUtilTableNew y)
    {
      int retValue;

      // Check if for null objects.
      retValue = NetCommon.CompareNull(x, y);

      while (true)
      {
        if (retValue != NetString.CompareNotNullOrEqual)
        {
          break;
        }

        // Check if for null values.
        var xName = x.LJCGetString("Name");
        var yName = y.LJCGetString("Name");
        retValue = NetCommon.CompareNull(xName, yName);
        if (retValue != NetString.CompareNotNullOrEqual)
        {
          break;
        }

        // Compare parent keys if neither is null.
        var xDataModuleID = x.LJCGetString("DataModuleID");
        var yDataModuleID = y.LJCGetString("DataModuleID");
        retValue = xDataModuleID.CompareTo(yDataModuleID);
        if (retValue != NetString.CompareEqual)
        {
          break;
        }

        // Compare value if parent keys are equal.
        retValue = xName.CompareTo(yName);
        break;
      }
      return retValue;
    }
  }
  #endregion
}
