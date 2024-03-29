// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataDbView.cs
using System;
using LJCNetCommon;
using LJCDBMessage;
using LJCDBClientLib;

namespace LJCDBViewDAL
{
  // Provides DB View data access and DbRequest methods.
  /// <include path='items/ViewHelper/*' file='Doc/ViewHelper.xml'/>
  public partial class DataDbView
  {
    #region Constructors

    /// <summary>Initializes an object instance.</summary>
    /// <param name="managers">The Managers object.</param>
    public DataDbView(ManagersDbView managers)
    {
      Managers = managers;
    }
    #endregion

    #region Get View Methods

    // Retrieves the View data.
    /// <include path='items/GetViewData/*' file='Doc/ViewHelper.xml'/>
    public DbResult GetViewData(string tableName, int viewDataID)
    {
      DbResult retValue = null;

      // Get Request from definition.
      DbRequest dbRequest = GetViewRequest(tableName, viewDataID);
      if (dbRequest != null)
      {
        // Execute the Request.
        var dataConfigName = Managers.DataConfigName;
        var dbServiceRef = Managers.DbServiceRef;
        DataManager dataManager = new DataManager(dbServiceRef
          , dataConfigName, tableName);
        DbResult dbResult = dataManager.ExecuteRequest(dbRequest);
        retValue = dbResult;
      }
      return retValue;
    }

    // Creates the DbRequest object for the specified View.
    /// <include path='items/GetViewRequest1/*' file='Doc/ViewHelper.xml'/>
    public DbRequest GetViewRequest(string tableName, string viewName)
    {
      DbRequest retValue = null;

      var viewTable
        = Managers.ViewTableManager.RetrieveWithUniqueKey(tableName);
      if (viewTable != null)
      {
        var viewData
          = Managers.ViewDataManager.RetrieveWithUniqueKey(viewTable.ID
          , viewName);
        if (viewData != null)
        {
          retValue = GetViewRequest(tableName, viewData.ID);
        }
      }
      return retValue;
    }

    // Creates the DbRequest object for the specified View.
    /// <include path='items/GetViewRequest2/*' file='Doc/ViewHelper.xml'/>
    public DbRequest GetViewRequest(string tableName, int viewDataID)
    {
      DbRequest retValue = null;

      var viewData = Managers.ViewDataManager.RetrieveWithID(viewDataID);
      if (viewData != null)
      {
        var dataConfigName = Managers.DataConfigName;
        retValue = new DbRequest(RequestType.Load, tableName, dataConfigName);
        GetViewColumns(viewData.ID, retValue);
        if (null == retValue.Columns || 0 == retValue.Columns.Count)
        {
          retValue = null;
        }
        else
        {
          GetViewJoins(viewData.ID, retValue);
          GetViewFilters(viewData.ID, retValue);
          GetViewOrderBys(viewData.ID, retValue);
        }
      }
      return retValue;
    }

    // Gets the ViewGrid columns.
    /// <include path='items/GetGridColumns/*' file='Doc/ViewHelper.xml'/>
    public DbColumns GetGridColumns(int viewDataID)
    {
      return Managers.ViewGridColumnManager.GetGridColumns(viewDataID);
    }

    // Creates the DbRequest Columns.
    private void GetViewColumns(int viewDataID, DbRequest dbRequest)
    {
      // Retrieve DbColumns directly.
      dbRequest.Columns
        = Managers.ViewColumnManager.LoadDbColumnsWithParentID(viewDataID
        , dbRequest.TableName);
    }

    // Creates the DbRequest Filters.
    private void GetViewFilters(int viewDataID, DbRequest dbRequest)
    {
      var viewFilters
        = Managers.ViewFilterManager.LoadWithParentID(viewDataID);
      if (NetCommon.HasItems(viewFilters))
      {
        var manager = Managers.ViewConditionSetManager;
        dbRequest.Filters = new DbFilters();
        foreach (ViewFilter viewFilter in viewFilters)
        {
          var viewConditionSet = manager.RetrieveWithParentID(viewFilter.ID);
          ViewConditions viewConditions = null;
          if (viewConditionSet != null)
          {
            viewConditions
              = Managers.ViewConditionManager.LoadWithParentID(viewConditionSet.ID);
          }

          // Create the Condition Set.
          if (NetCommon.HasItems(viewConditions))
          {
            DbConditionSet dbConditionSet = new DbConditionSet()
            {
              BooleanOperator = viewConditionSet.BooleanOperator
            };
            foreach (ViewCondition viewCondition in viewConditions)
            {
              dbConditionSet.Conditions.Add(viewCondition.FirstValue, viewCondition.SecondValue
                , viewCondition.ComparisonOperator);
            }

            // Add the Filter with the Condition Set.
            dbRequest.Filters.Add(viewFilter.Name, dbConditionSet
              , booleanOperator: viewFilter.BooleanOperator);
          }
        }
      }
    }

    // Creates the DbRequest Joins.
    private void GetViewJoins(int viewDataID, DbRequest dbRequest)
    {
      var viewJoins = Managers.ViewJoinManager.LoadWithParentID(viewDataID);
      if (NetCommon.HasItems(viewJoins))
      {
        dbRequest.Joins = new DbJoins();
        foreach (ViewJoin viewJoin in viewJoins)
        {
          DbJoin dbJoin = dbRequest.Joins.Add(viewJoin.JoinTableName);
          dbJoin.JoinType = viewJoin.JoinType;
          dbJoin.TableAlias = viewJoin.TableAlias;

          // Add the Join On clauses.
          var ViewJoinOns
            = Managers.ViewJoinOnManager.LoadWithParentID(viewJoin.ID);
          if (NetCommon.HasItems(ViewJoinOns))
          {
            dbJoin.JoinOns = new DbJoinOns();
            foreach (ViewJoinOn ViewJoinOn in ViewJoinOns)
            {
              dbJoin.JoinOns.Add(ViewJoinOn.FromColumnName
                , ViewJoinOn.ToColumnName, ViewJoinOn.JoinOnOperator);
            }
          }

          // Add join columns.
          // Note: Converts results to DbColumns with ResultCoverter().
          dbJoin.Columns
            = Managers.ViewJoinColumnManager.LoadDbColumnsWithParentID(viewJoin.ID);
        }
      }
    }

    // Creates the DbRequest OrderBys.
    private void GetViewOrderBys(int viewDataID, DbRequest dbRequest)
    {
      var viewOrderBys
        = Managers.ViewOrderByManager.LoadWithParentID(viewDataID);
      if (NetCommon.HasItems(viewOrderBys))
      {
        foreach (ViewOrderBy viewOrderBy in viewOrderBys)
        {
          dbRequest.OrderByNames.Add(viewOrderBy.ColumnName);
        }
      }
    }
    #endregion

    #region Create Data Methods

    // Creates and returns the default ViewData record.
    /// <include path='items/CreateDefaultViewData/*' file='Doc/ViewHelper.xml'/>
    public ViewData CreateDefaultViewData(DbRequest dbRequest)
    {
      ViewData retValue;

      string name = $"{dbRequest.TableName}Standard";
      string description = $"The Standard {dbRequest.TableName} View";
      retValue = SaveRequestView(name, description, dbRequest);
      return retValue;
    }

    // Creates and returns the default ViewTable record.
    /// <include path='items/CreateDefaultViewTable/*' file='Doc/ViewHelper.xml'/>
    public ViewTable CreateDefaultViewTable(string tableName)
    {
      ViewTable retValue;

      retValue = new ViewTable()
      {
        Name = tableName,
        Description = tableName
      };
      var newRecord = Managers.ViewTableManager.Add(retValue);
      retValue.ID = newRecord.ID;
      return retValue;
    }
    #endregion

    #region Column Conversion Methods

    // Creates and returns a DbColumn object from a ViewColumn record.
    /// <include path='items/GetDbColumnFromViewColumn/*' file='Doc/ViewHelper.xml'/>
    public DbColumn GetDbColumnFromViewColumn(ViewColumn viewColumn)
    {
      DbColumn retValue = new DbColumn()
      {
        ColumnName = viewColumn.ColumnName,
        PropertyName = viewColumn.PropertyName,
        RenameAs = viewColumn.RenameAs,
        Caption = viewColumn.Caption,
        DataTypeName = viewColumn.DataTypeName
      };
      return retValue;
    }

    // Creates and returns a DbColumn object from a ViewGridColumn record.
    /// <include path='items/GetDbColumnFromViewGridColumn/*' file='Doc/ViewHelper.xml'/>
    public DbColumn GetDbColumnFromViewGridColumn(ViewGridColumn viewGridColumn)
    {
      DbColumn retValue = new DbColumn()
      {
        ColumnName = viewGridColumn.ColumnName,
        PropertyName = viewGridColumn.PropertyName,
        Caption = viewGridColumn.Caption
      };
      return retValue;
    }

    // Creates and returns a DbColumn object from a ViewJoinColumn record.
    /// <include path='items/GetDbColumnFromViewJoinColumn/*' file='Doc/ViewHelper.xml'/>
    public DbColumn GetDbColumnFromViewJoinColumn(ViewJoinColumn viewJoinColumn)
    {
      DbColumn retValue = new DbColumn()
      {
        ColumnName = viewJoinColumn.ColumnName,
        PropertyName = viewJoinColumn.PropertyName,
        RenameAs = viewJoinColumn.RenameAs,
        Caption = viewJoinColumn.Caption,
        DataTypeName = viewJoinColumn.DataTypeName
      };
      return retValue;
    }

    // Creates and returns a ViewColumn record from a DbColumn object.
    /// <include path='items/GetViewColumnFromDbColumn/*' file='Doc/ViewHelper.xml'/>
    public ViewColumn GetViewColumnFromDbColumn(DbColumn dbColumn
      , int viewDataID = 0)
    {
      ViewColumn retValue = new ViewColumn()
      {
        ViewDataID = viewDataID,
        ColumnName = dbColumn.ColumnName,
        PropertyName = dbColumn.PropertyName,
        RenameAs = dbColumn.RenameAs,
        Caption = dbColumn.Caption,
        DataTypeName = dbColumn.DataTypeName
      };
      return retValue;
    }

    //// Creates and returns a ViewColumns collection from a DbColumns collection.
    ///// <include path='items/GetViewColumnsFromDbColumns/*' file='Doc/ViewHelper.xml'/>
    //public ViewColumns GetViewColumnsFromDbColumns(DbColumns dbColumns, int viewDataID = 0)
    //{
    //	ViewColumns retValue = new ViewColumns();

    //	foreach (DbColumn dbColumn in dbColumns)
    //	{
    //		retValue.Add(GetViewColumnFromDbColumn(dbColumn, viewDataID));
    //	}
    //	return retValue;
    //}

    // Creates and returns a ViewJoinColumn record from a DbColumn object. 
    /// <include path='items/GetViewJoinColumnFromDbColumn/*' file='Doc/ViewHelper.xml'/>
    public ViewJoinColumn GetViewJoinColumnFromDbColumn(DbColumn dbColumn
      , int viewJoinID)
    {
      ViewJoinColumn retValue;

      retValue = new ViewJoinColumn()
      {
        ViewJoinID = viewJoinID,
        ColumnName = dbColumn.ColumnName,
        PropertyName = dbColumn.PropertyName,
        RenameAs = dbColumn.RenameAs,
        Caption = dbColumn.Caption,
        DataTypeName = dbColumn.DataTypeName
      };
      return retValue;
    }

    // Creates and returns a ViewJoinColumns collection from a DbColumns collection.
    /// <include path='items/GetViewJoinColumnsFromDbColumns/*' file='Doc/ViewHelper.xml'/>
    public ViewJoinColumns GetViewJoinColumnsFromDbColumns(DbColumns dbColumns
      , int viewJoinID = 0)
    {
      ViewJoinColumns retValue = new ViewJoinColumns();

      foreach (DbColumn dbColumn in dbColumns)
      {
        retValue.Add(GetViewJoinColumnFromDbColumn(dbColumn, viewJoinID));
      }
      return retValue;
    }
    #endregion

    #region Save Grid Column Methods

    // Saves the ViewGridColumns from the specified DbRequest columns.
    /// <include path='items/SaveRequestViewGridColumns/*' file='Doc/ViewHelper.xml'/>
    public ViewGridColumns SaveRequestViewGridColumns(int viewDataID
      , DbRequest dbRequest)
    {
      ViewGridColumns retValue = null;

      // Next Statement - Change 1/6/21
      if (viewDataID > 0
        && NetCommon.HasItems(dbRequest.Columns))
      {
        retValue = new ViewGridColumns();
        int sequence = 0;
        foreach (DbColumn dbColumn in dbRequest.Columns)
        {
          // Get referenced record.
          var viewColumn
            = Managers.ViewColumnManager.RetrieveWithUniqueKey(viewDataID
            , dbColumn.ColumnName);
          if (null == viewColumn)
          {
            // Create ViewColumn record?
            viewColumn = GetViewColumnFromDbColumn(dbColumn);
            // Next Statement - Add 1/6/21
            viewColumn.ViewDataID = viewDataID;
            Managers.ViewColumnManager.SaveData(viewColumn);
          }
          if (viewColumn != null)
          {
            sequence++;

            // Get Update record.
            var viewGridColumn
              = Managers.ViewGridColumnManager.RetrieveWithIDs(viewDataID
              , viewColumn.ID);

            if (null == viewGridColumn)
            {
              // Get Create record.
              viewGridColumn = new ViewGridColumn()
              {
                ViewDataID = viewDataID,
                ViewColumnID = viewColumn.ID
              };
            }

            // Set updatable values and save.
            viewGridColumn.Sequence = sequence;
            Managers.ViewGridColumnManager.SaveData(viewGridColumn);
            retValue.Add(viewGridColumn);
          }
        }
        SaveRequestJoinGridColumns(viewDataID, dbRequest, sequence);
      }
      return retValue;
    }

    //// Saves the ViewGridColumn from the specified ViewJoin column.
    ///// <include path='items/SaveJoinGridColumn/*' file='Doc/ViewHelper.xml'/>
    //private ViewGridColumn SaveJoinGridColumn(ViewJoin viewJoin, DbColumn dbColumn
    //	, int sequence)
    //{
    //	//ViewJoinColumn viewJoinColumn;
    //	ViewGridColumn retValue = null;

    //	//// Get referenced record.
    //	//viewJoinColumn = ViewJoinColumnManager.RetrieveWithUniqueKey(viewJoin, dbColumn);
    //	//if (viewJoinColumn != null)
    //	//{
    //	//	// Get join record.
    //	//	ViewColumn viewColumn = ViewColumnManager.RetrieveWithID(viewJoinColumn.ViewColumnID);

    //	//	// Get Update record.
    //	//	retValue = ViewGridColumnManager.RetrieveWithIDs(viewJoin.ViewDataID
    //	//		, viewJoinColumn.ViewColumnID);
    //	//	ErrorText = ViewGridColumnManager.ErrorText;
    //	//	if (IsSuccess)
    //	//	{
    //	//		if (null == retValue)
    //	//		{
    //	//			// Get Create record.
    //	//			retValue = new ViewGridColumn()
    //	//			{
    //	//				ViewDataID = viewJoin.ViewDataID,
    //	//				ViewColumnID = viewColumn.ID
    //	//			};
    //	//		}

    //	//		// Set updatable values and save.
    //	//		retValue.Sequence = sequence;
    //	//		ViewGridColumnManager.SaveData(retValue);
    //	//	}
    //	//}
    //	return retValue;
    //}

    // Saves the ViewGridColumns from the specified DbRequest join columns.
    /// <include path='items/SaveRequestJoinGridColumns/*' file='Doc/ViewHelper.xml'/>
    private ViewGridColumns SaveRequestJoinGridColumns(int viewDataID, DbRequest dbRequest
      , int sequence)
    {
      //ViewGridColumn viewGridColumn;
      ViewGridColumns retValue = null;

      if (NetCommon.HasItems(dbRequest.Joins))
      {
        retValue = new ViewGridColumns();
        foreach (DbJoin dbJoin in dbRequest.Joins)
        {
          var viewJoin
            = Managers.ViewJoinManager.RetrieveWithUniqueKey(viewDataID
            , dbJoin.TableName);
          if (null == viewJoin)
          {
            // Create ViewJoin record?
            viewJoin = new ViewJoin()
            {
              ViewDataID = viewDataID,
              JoinTableName = dbJoin.TableName,
              JoinType = dbJoin.JoinType,
              TableAlias = dbJoin.TableAlias
            };
            Managers.ViewJoinManager.SaveData(viewJoin);
          }
          if (viewJoin != null)
          {
            if (NetCommon.HasItems(dbJoin.Columns))
            {
              foreach (DbColumn dbColumn in dbJoin.Columns)
              {
                sequence++;
                //viewGridColumn = SaveJoinGridColumn(viewJoin, dbColumn, sequence);
                //if (viewGridColumn != null)
                //{
                //  retValue.Add(viewGridColumn);
                //}
              }
            }
          }
        }
      }
      return retValue;
    }
    #endregion

    #region Save DbRequest View Methods

    // Saves the view from the specified DbRequest object.
    /// <include path='items/SaveRequestView/*' file='Doc/ViewHelper.xml'/>
    public ViewData SaveRequestView(string viewName, string viewDescription, DbRequest dbRequest)
    {
      ViewData retValue = null;

      var viewTable = SaveRequestTable(dbRequest);
      if (viewTable != null)
      {
        retValue = SaveRequestViewData(viewTable.ID, viewName, viewDescription);
        if (retValue != null)
        {
          SaveRequestColumns(retValue.ID, dbRequest);
          SaveRequestJoins(retValue.ID, dbRequest);
          SaveRequestFilters(retValue.ID, dbRequest);
        }
      }
      return retValue;
    }

    // Saves the ViewOrderBys from the specified DbRequest object.
    internal ViewOrderBys SaveRequestOrderBy(int viewDataID, DbRequest dbRequest)
    {
      ViewOrderBys retValue = null;

      if (CheckOrderByParams(viewDataID, dbRequest))
      {
        retValue = new ViewOrderBys();
        foreach (string columnName in dbRequest.OrderByNames)
        {
          var viewOrderBy
            = Managers.ViewOrderByManager.RetrieveWithUniqueKey(columnName);
          if (null == viewOrderBy)
          {
            viewOrderBy = new ViewOrderBy()
            {
              ViewDataID = viewDataID,
              ColumnName = columnName
            };
          }

          // Set updatable values and save.
          Managers.ViewOrderByManager.SaveData(viewOrderBy);
          retValue.Add(viewOrderBy);
        }
      }
      return retValue;
    }

    // Saves the ViewColumns from the specified DbRequest object.
    private ViewColumns SaveRequestColumns(int viewDataID, DbRequest dbRequest)
    {
      ViewColumns retValue = null;

      if (CheckColumnParams(viewDataID, dbRequest)
        && NetCommon.HasItems(dbRequest.Columns))
      {
        retValue = new ViewColumns();
        foreach (DbColumn dbColumn in dbRequest.Columns)
        {
          // Get Update record.
          var viewColumn
            = Managers.ViewColumnManager.RetrieveWithUniqueKey(viewDataID
            , dbColumn.ColumnName);
          if (null == viewColumn)
          {
            // Get Create record.
            viewColumn = new ViewColumn()
            {
              ViewDataID = viewDataID,
              ColumnName = dbColumn.ColumnName
            };
          }

          // Set updatable values and save.
          viewColumn.PropertyName = dbColumn.PropertyName;
          viewColumn.RenameAs = dbColumn.RenameAs;
          viewColumn.Caption = dbColumn.Caption;
          viewColumn.DataTypeName = dbColumn.DataTypeName;
          viewColumn.Value = dbColumn.Value as string;
          Managers.ViewColumnManager.SaveData(viewColumn);
          retValue.Add(viewColumn);
        }
      }
      return retValue;
    }

    // Saves the ViewConditions from the specified DbJoin object.
    private ViewConditions SaveRequestConditions(int conditionSetID, DbConditionSet dbConditionSet)
    {
      ViewConditions retValue = null;

      if (CheckConditionParams(conditionSetID, dbConditionSet))
      {
        retValue = new ViewConditions();
        foreach (DbCondition dbCondition in dbConditionSet.Conditions)
        {
          // Get Update record.
          var viewCondition
            = Managers.ViewConditionManager.RetrieveWithUniqueKey(conditionSetID
            , dbCondition.FirstValue);
          if (null == viewCondition)
          {
            // Get Create record.
            viewCondition = new ViewCondition()
            {
              ViewConditionSetID = conditionSetID,
              FirstValue = dbCondition.FirstValue
            };
          }

          // Set updatable values and save.
          viewCondition.SecondValue = dbCondition.SecondValue;
          viewCondition.ComparisonOperator = dbCondition.ComparisonOperator;
          Managers.ViewConditionManager.SaveData(viewCondition);
          retValue.Add(viewCondition);
        }
      }
      return retValue;
    }

    // Saves the ConditionSet from the specified DbFilter object.
    private ViewConditionSet SaveRequestConditionSet(int viewFilterID, DbFilter dbFilter)
    {
      ViewConditionSet retValue = null;

      if (CheckConditionSetParams(viewFilterID, dbFilter))
      {
        // Get Update record.
        var manager = Managers.ViewConditionSetManager;
        retValue = manager.RetrieveWithParentID(viewFilterID);
        if (null == retValue)
        {
          // Get Create record.
          retValue = new ViewConditionSet()
          {
            ViewFilterID = viewFilterID
          };
        }

        // Set updatable values and save.
        string booleanOperator = "and";
        if (dbFilter.ConditionSet != null
          && dbFilter.ConditionSet.BooleanOperator != null)
        {
          booleanOperator = dbFilter.ConditionSet.BooleanOperator;
        }
        retValue.BooleanOperator = booleanOperator;
        Managers.ViewConditionSetManager.SaveData(retValue);
      }
      return retValue;
    }

    // Saves the ViewFilters from the specified DbRequest object.
    private ViewFilters SaveRequestFilters(int viewDataID, DbRequest dbRequest)
    {
      ViewFilters retValue = null;

      CheckFilterParams(viewDataID, dbRequest);
      if (NetCommon.HasItems(dbRequest.Filters))
      {
        retValue = new ViewFilters();
        foreach (DbFilter dbFilter in dbRequest.Filters)
        {
          // Get Update record.
          var viewFilter
            = Managers.ViewFilterManager.RetrieveWithUniqueKey(viewDataID
            , dbFilter.Name);
          if (null == viewFilter)
          {
            // Get Create record.
            viewFilter = new ViewFilter
            {
              ViewDataID = viewDataID,
              Name = dbFilter.Name,
              // Set updatable values.
              BooleanOperator = dbFilter.BooleanOperator
            };
            Managers.ViewFilterManager.SaveData(viewFilter);
            retValue.Add(viewFilter);

            // Save contained DbConditionSet and DbConditions.
            var conditionSet = SaveRequestConditionSet(viewFilter.ID, dbFilter);
            SaveRequestConditions(conditionSet.ID, dbFilter.ConditionSet);
          }
        }
      }
      return retValue;
    }

    // Saves the ViewJoinColumns from the specified DbJoin object.
    private ViewJoinColumns SaveRequestJoinColumns(ViewJoin viewJoin, DbJoin dbJoin)
    {
      ViewJoinColumns retValue = null;

      CheckJoinColumnParams(viewJoin, dbJoin);
      if (NetCommon.HasItems(dbJoin.Columns))
      {
        retValue = new ViewJoinColumns();
        foreach (DbColumn dbColumn in dbJoin.Columns)
        {
          // Get Update record.
          var manager = Managers.ViewJoinColumnManager;
          var viewJoinColumn = manager.RetrieveWithUnique(viewJoin.ID
            , dbColumn.PropertyName, dbColumn.RenameAs);
          //viewColumn = ViewColumnManager.SaveJoinColumn(viewJoin, dbColumn
          //	, viewJoinColumn, sequence);
          if (null == viewJoinColumn)
          {
            // Get Create record.
            viewJoinColumn = new ViewJoinColumn
            {
              ViewJoinID = viewJoin.ID,
              // Set updatable values.
              PropertyName = dbColumn.PropertyName,
              RenameAs = dbColumn.RenameAs,
              Caption = dbColumn.Caption,
              DataTypeName = dbColumn.DataTypeName
            };
            Managers.ViewJoinColumnManager.SaveData(viewJoinColumn);
            retValue.Add(viewJoinColumn);
          }
        }
      }
      return retValue;
    }

    // Saves the ViewJoinOns from the specified DbJoin object.
    private ViewJoinOns SaveRequestJoinOns(int viewJoinID, DbJoin dbJoin)
    {
      ViewJoinOns retValue = null;

      CheckJoinOnParams(viewJoinID, dbJoin);
      if (NetCommon.HasItems(dbJoin.JoinOns))
      {
        retValue = new ViewJoinOns();
        foreach (DbJoinOn dbJoinOn in dbJoin.JoinOns)
        {
          // Get Update record.
          var viewJoinOn
            = Managers.ViewJoinOnManager.RetrieveWithUniqueKey(viewJoinID
            , dbJoinOn.FromColumnName);
          if (null == viewJoinOn)
          {
            // Get Create record.
            viewJoinOn = new ViewJoinOn
            {
              ViewJoinID = viewJoinID,
              FromColumnName = dbJoinOn.FromColumnName,
              // Set updatable values.
              ToColumnName = dbJoinOn.ToColumnName,
              JoinOnOperator = dbJoinOn.JoinOnOperator
            };
            Managers.ViewJoinOnManager.SaveData(viewJoinOn);
            retValue.Add(viewJoinOn);
          }
        }
      }
      return retValue;
    }

    // Saves the ViewJoins from the specified DbRequest object.
    private ViewJoins SaveRequestJoins(int viewDataID, DbRequest dbRequest)
    {
      ViewJoins retValue = null;

      if (CheckJoinParams(viewDataID, dbRequest)
        && NetCommon.HasItems(dbRequest.Joins))
      {
        retValue = new ViewJoins();
        foreach (DbJoin dbJoin in dbRequest.Joins)
        {
          // Get Update record.
          var viewJoin
            = Managers.ViewJoinManager.RetrieveWithUniqueKey(viewDataID
            , dbJoin.TableName);
          if (null == viewJoin)
          {
            // Get Create record.
            viewJoin = new ViewJoin()
            {
              ViewDataID = viewDataID,
              JoinTableName = dbJoin.TableName
            };
          }

          // Set updatable values and save.
          viewJoin.JoinType = dbJoin.JoinType;
          viewJoin.TableAlias = dbJoin.TableAlias;
          Managers.ViewJoinManager.SaveData(viewJoin);
          retValue.Add(viewJoin);

          // Save contained DbJoinOns and DbColumns.
          SaveRequestJoinOns(viewJoin.ID, dbJoin);
          SaveRequestJoinColumns(viewJoin, dbJoin);
        }
      }
      return retValue;
    }

    // Saves the ViewTable from the specified DbRequest object.
    private ViewTable SaveRequestTable(DbRequest dbRequest)
    {
      ViewTable retValue = null;

      if (CheckTableParams(dbRequest))
      {
        // Get Update record.
        retValue
          = Managers.ViewTableManager.RetrieveWithUniqueKey(dbRequest.TableName);
        if (null == retValue)
        {
          // Get Create record.
          retValue = new ViewTable()
          {
            Name = dbRequest.TableName,
            Description = $"The {dbRequest.TableName} table."
          };
        }

        // Set updatable values and save.
        Managers.ViewTableManager.SaveData(retValue);
      }
      return retValue;
    }

    // Saves the ViewData from the specified DbRequest object.
    private ViewData SaveRequestViewData(int viewTableID, string viewName
      , string viewDescription)
    {
      ViewData retValue;

      // Get Update record.
      retValue
        = Managers.ViewDataManager.RetrieveWithUniqueKey(viewTableID
        , viewName);
      if (null == retValue)
      {
        // Get Create record.
        retValue = new ViewData()
        {
          ViewTableID = viewTableID,
          Name = viewName,
          Description = viewDescription
        };
      }

      // Set updatable values and save.
      Managers.ViewDataManager.SaveData(retValue);
      return retValue;
    }
    #endregion

    #region Check Params Private Methods

    // Checks the Column values.
    private bool CheckColumnParams(int viewDataID, DbRequest dbRequest)
    {
      string errorText;
      bool retValue = true;

      if (0 == viewDataID)
      {
        errorText = "ViewHelper.SaveRequestColumns - viewDataID == 0.";
        throw new ArgumentException(errorText);
      }
      if (null == dbRequest)
      {
        errorText = "ViewHelper.SaveRequestColumns - dbRequest is null.";
        throw new ArgumentException(errorText);
      }
      return retValue;
    }

    // Checks the Condition values.
    private bool CheckConditionParams(int conditionSetID, DbConditionSet dbConditionSet)
    {
      string errorText;
      bool retValue = true;

      if (0 == conditionSetID)
      {
        errorText = "ViewHelper.SaveRequestConditions - conditionSetID == 0.";
        throw new ArgumentException(errorText);
      }
      if (null == dbConditionSet)
      {
        errorText = "ViewHelper.SaveRequestConditions - dbConditionSet is null.";
        throw new ArgumentException(errorText);
      }
      return retValue;
    }

    // Checks the ConditionSet values.
    private bool CheckConditionSetParams(int viewFilterID, DbFilter dbFilter)
    {
      string errorText;
      bool retValue = true;

      if (0 == viewFilterID)
      {
        errorText = "ViewHelper.SaveRequestConditionSet - viewFilterID == 0.";
        throw new ArgumentException(errorText);
      }
      if (null == dbFilter)
      {
        errorText = "ViewHelper.SaveRequestConditionSet - dbFilter is null.";
        throw new ArgumentException(errorText);
      }
      return retValue;
    }

    // Checks the Filter values.
    private bool CheckFilterParams(int viewDataID, DbRequest dbRequest)
    {
      string errorText;
      bool retValue = true;

      if (0 == viewDataID)
      {
        errorText = "ViewHelper.SaveRequestFilters - viewDataID == 0.";
        throw new ArgumentException(errorText);
      }
      if (null == dbRequest)
      {
        errorText = "ViewHelper.SaveRequestFilters - dbRequest is null.";
        throw new ArgumentException(errorText);
      }
      return retValue;
    }

    // Checks the JoinColumn values.
    private bool CheckJoinColumnParams(ViewJoin viewJoin, DbJoin dbJoin)
    {
      string errorText;
      bool retValue = true;

      if (null == viewJoin)
      {
        errorText = "ViewHelper.SaveRequestJoinColumns - viewJoin is null.";
        throw new ArgumentException(errorText);
      }
      if (null == dbJoin)
      {
        errorText = "ViewHelper.SaveRequestJoinColumns - dbJoin is null.";
        throw new ArgumentException(errorText);
      }
      return retValue;
    }

    // Checks the JoinOn values.
    private bool CheckJoinOnParams(int viewJoinID, DbJoin dbJoin)
    {
      string errorText;
      bool retValue = true;

      if (0 == viewJoinID)
      {
        errorText = "ViewHelper.SaveRequestJoinOns - viewJoinID == 0.";
        throw new ArgumentException(errorText);
      }
      if (null == dbJoin)
      {
        errorText = "ViewHelper.SaveRequestJoinOns - dbJoin is null.";
        throw new ArgumentException(errorText);
      }
      return retValue;
    }

    // Checks the Join values.
    private bool CheckJoinParams(int viewDataID, DbRequest dbRequest)
    {
      string errorText;
      bool retValue = true;

      if (0 == viewDataID)
      {
        errorText = "ViewHelper.SaveRequestJoins - viewDataID == 0.";
        throw new ArgumentException(errorText);
      }
      if (null == dbRequest)
      {
        errorText = "ViewHelper.SaveRequestJoins - dbRequest is null.";
        throw new ArgumentException(errorText);
      }
      return retValue;
    }

    // Checks the OrderBy values.
    private bool CheckOrderByParams(int viewDataID, DbRequest dbRequest)
    {
      string errorText;
      bool retValue = true;

      if (0 == viewDataID)
      {
        errorText = "ViewHelper.SaveRequestOrderBys - viewDataID == 0.";
        throw new ArgumentException(errorText);
      }
      if (null == dbRequest)
      {
        errorText = "ViewHelper.SaveRequestOrderBys - dbRequest is null.";
        throw new ArgumentException(errorText);
      }
      return retValue;
    }

    // Checks the Table values.
    private bool CheckTableParams(DbRequest dbRequest)
    {
      string errorText;
      bool retValue = true;

      if (null == dbRequest)
      {
        errorText = "ViewHelper.SaveRequestTable - dbRequest is null.";
        throw new ArgumentException(errorText);
      }
      if (!NetString.HasValue(dbRequest.TableName))
      {
        errorText = "ViewHelper.SaveRequestTable - dbRequest table name is missing.";
        throw new ArgumentException(errorText);
      }
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the Managers object.</summary>
    public ManagersDbView Managers { get; set; }
    #endregion
  }
}
