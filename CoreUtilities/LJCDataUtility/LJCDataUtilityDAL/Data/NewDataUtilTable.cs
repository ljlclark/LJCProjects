// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataUtilTable.cs

using LJCNetCommon;
using System.Collections.Generic;

namespace LJCDataUtilityDAL
{
  /// <summary>Represents the DataTable data.</summary>
  public class NewDataUtilTable
  {
    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public NewDataUtilTable()
    {
      // columnName, propertyName, renameAs, dataTypeName
      //   , caption, maxLength
      Columns = new DbColumns();
      Columns.Add("ID", dataTypeName: "long");
      Columns.Add("DataSiteID", dataTypeName: "long");
      Columns.Add("DataModuleID", dataTypeName: "long");
      Columns.Add("DataModuleSiteID", dataTypeName: "long");
      Columns.Add("Name");
      Columns.Add("Description");
      Columns.Add("Sequence", dataTypeName: "int");

      Columns.Add("SchemaName");
      Columns.Add("NewName");
    }
    #endregion

    #region Data Object Methods

    // Creates and returns a clone of the object.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Clone/*'/>
    public NewDataUtilTable Clone()
    {
      var retValue = new NewDataUtilTable
      {
        Columns = Columns.Clone()
      };
      return retValue;
    }
    #endregion

    #region Methods

    /// <summary>Gets the DbColumn with the supplied column name value.</summary>
    /// <include file='Doc/NewDataUtilTable.xml'
    ///  path='members/GetWithColumnName/*'/>
    public DbColumn GetWithColumnName(string columnName)
    {
      return Columns.LJCSearchColumnName(columnName);
    }

    // Gets the DbColumn with the supplied rename as value.
    /// <include file='Doc/NewDataUtilTable.xml'
    ///  path='members/GetWithRenameAs/*'/>
    public DbColumn GetWithRenameAs(string renameAs)
    {
      return Columns.LJCSearchRenameAs(renameAs);
    }
    #endregion

    #region Properties

    /// <summary>Get the DbColumn for the specified property name.</summary>
    public DbColumn this[string propertyName]
    {
      get => Columns[propertyName];
    }
    #endregion

    #region Class Data

    /// <summary>Gets the DbColumns object.</summary>
    public DbColumns Columns { get; private set; }
    #endregion
  }
}
