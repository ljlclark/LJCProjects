using LJCNetCommon;

namespace LJCDataUtilityDAL
{
  /// <summary>Represents the DataTable data.</summary>
  public class DataUtilTableSave : DbColumns
  {
    #region Constructors

    // Initializes an object instance.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public DataUtilTableSave()
    {
      // columnName, propertyName, renameAs, dataTypeName
      //   , caption, maxLength
      var dataTable = new DataUtilTableSave();
      //{
      //  [DataTableColumn.ID] = 1,
      //  [DataTableColumn.DataModuleID] = 2,
      //  [DataTableColumn.Name] = "Name",
      //  [DataTableColumn.Description] = "Description",
      //  [DataTableColumn.Sequence] = 1,
      //  [DataTableColumn.SchemaName] = "SchemaName",
      //  [DataTableColumn.NewName] = "NewName",
      //};

      // Common table values.
      var dbColumn = Add("ID", dataTypeName: "long");
      dbColumn.AutoIncrement = true;
      dbColumn.IsPrimaryKey = true;
      dbColumn.SQLTypeName = "bigint";

      dbColumn = Add("DataSiteID", dataTypeName: "long");
      dbColumn.IsPrimaryKey = true;
      dbColumn.KeyType = "Foreign";
      dbColumn.SQLTypeName = "bigint";

      dbColumn = Add("DataModuleID", dataTypeName: "long");
      dbColumn.KeyType = "Foreign";
      dbColumn.SQLTypeName = "bigint";

      dbColumn = Add("DataModuleSiteID", dataTypeName: "long");
      dbColumn.KeyType = "Foreign";
      dbColumn.SQLTypeName = "bigint";

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
    #endregion

    ///// <summary></summary>
    //public string GetColumnName(Column columnID)
    //{
    //  string retName = null;

    //  var name = Enum.GetName(typeof(Column), columnID);
    //  if (name != null)
    //  {
    //    retName = name;
    //  }
    //  return retName;
    //}

    ///// <summary></summary>
    //public object this[Column column]
    //{
    //  get
    //  {
    //    object retValue = null;
    //    var propertyName = GetColumnName(column);
    //    if (propertyName == null)
    //    {
    //      var dbColumn = this[propertyName];
    //      if (dbColumn == null)
    //      {
    //        retValue = this[propertyName].Value;
    //      }
    //    }
    //    return retValue;
    //  }

    //  set
    //  {
    //    var propertyName = GetColumnName(column);
    //    if (propertyName != null)
    //    {
    //      var dbColumn = this[propertyName];
    //      if (dbColumn != null)
    //      {
    //        dbColumn.Value = value;
    //      }
    //    }
    //  }
    //}

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

    ///// <summary></summary>
    //public enum Column : short
    //{
    //  /// <summary></summary>
    //  ID,
    //  /// <summary></summary>
    //  DataModuleID,
    //  /// <summary></summary>
    //  Name,
    //  /// <summary></summary>
    //  Description,
    //  /// <summary></summary>
    //  Sequence,
    //  /// <summary></summary>
    //  SchemaName,
    //  /// <summary></summary>
    //  NewName,
    //}
    #endregion
  }
}
