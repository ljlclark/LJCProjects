// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DbViewGridColumnManager.cs
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using System.Collections.Generic;

namespace LJCDBViewDAL
{
  /// <summary>Provides table specific data manipulation methods.</summary>
  public class DbViewGridColumnManager
    : ObjectManager<DbColumn, DbColumns>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/ViewGridColumnManagerC/*' file='Doc/ViewGridColumnManager.xml'/>
    public DbViewGridColumnManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "ViewGridColumn")
      : base(dbServiceRef, dataConfigName, tableName)
    {
      // Map table names with property names or captions
      // that differ from the column names.
      MapNames(ViewGridColumn.ColumnViewDataID, caption: "View ID");
      MapNames(ViewGridColumn.ColumnViewColumnID, caption: "Column ID");
    }
    #endregion

    #region Load/Retrieve Methods

    // Loads a collection of data records ordered by Sequence.
    /// <include path='items/LoadWithViewIDBySequence/*' file='Doc/ViewGridColumnManager.xml'/>
    public DbColumns LoadWithViewIDBySequence(int viewDataID)
    {
      DbColumns keyColumns = GetViewDataIDKey(viewDataID);
      DbJoins dbJoins = GetLoadJoins();
      SetOrderBySequence();
      return Load(keyColumns, joins: dbJoins);
    }
    #endregion

    #region GetKey Methods

    // Get the ViewDataID key record.
    /// <include path='items/GetViewDataIDKey/*' file='Doc/ViewGridColumnManager.xml'/>
    public DbColumns GetViewDataIDKey(int viewDataID)
    {
      var retValue = new DbColumns()
      {
        { ViewGridColumn.ColumnViewDataID, viewDataID }
      };
      return retValue;
    }
    #endregion

    #region Joins

    // Creates and returns the Joins object.
    /// <include path='items/GetLoadJoins/*' file='Doc/ViewGridColumnManager.xml'/>
    public DbJoins GetLoadJoins()
    {
      DbJoin dbJoin;
      DbJoins retValue = new DbJoins();

      // Note: JoinOn Columns must have properties in the Data Object
      // to recieve the join column values.
      // The RenameAs property is required if there is another table column
      // with the same name.

      //select [ViewGridColumn].DBViewID, [ViewGridColumn].ViewColumnID, vc.ColumnName, vc.PropertyName,
      // vc.RenameAs, [ViewGridColumn].[Sequence], [ViewGridColumn].Width
      //from ViewGridColumn
      //left join ViewColumn as vc
      // on [ViewGridColumn].ViewColumnID = vc.ID
      //where [ViewGridColumn].DBViewID = 2
      //order by [ViewGridColumn].[Sequence];

      dbJoin = new DbJoin
      {
        TableName = ViewColumn.TableName,
        TableAlias = "vc",
        JoinType = "left",
        JoinOns = new DbJoinOns() {
          { ViewGridColumn.ColumnViewColumnID
           , $"[vc].{ViewColumn.ColumnID}" }
        },
        Columns = new DbColumns() {
          { ViewColumn.ColumnColumnName },
          { ViewColumn.ColumnPropertyName }
        }
      };
      retValue.Add(dbJoin);
      return retValue;
    }
    #endregion

    #region OrderBys

    // Sets the current OrderBy names.
    /// <include path='items/SetOrderBySequence/*' file='Doc/ViewGridColumnManager.xml'/>
    public void SetOrderBySequence() => DataManager.OrderByNames = new List<string>()
    {
      ViewGridColumn.ColumnSequence
    };
    #endregion
  }
}
