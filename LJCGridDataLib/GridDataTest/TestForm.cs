using LJCDataAccess;
using LJCDataAccessConfig;
using LJCDBClientLib;
using LJCDBDataAccess;
using LJCDBMessage;
using LJCGridDataLib;
using LJCNetCommon;
using LJCRegionDAL;
using LJCWinFormControls;
using System;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;

namespace GridDataTest
{
  public partial class TestForm : Form
  {
    public TestForm()
    {
      InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      var gridDataTest = new GridDataTest(TestDataGrid);
      gridDataTest.RunTests();
    }
  }

  internal class GridDataTest
  {
    internal GridDataTest(LJCDataGrid ljcDataGrid)
    {
      LJCDataGridName = ljcDataGrid;
    }

    internal void RunTests()
    {
      //var dataManager = Setup();

      // Create the Display column definitions.
      //var displayColumns = SetDisplayColumns(dataManager);
      //displayColumns = SetDisplayColumns1(dataManager);
      //displayColumns = SetDisplayColumns2();

      // Configure the grid columns.
      //LJCDataGridName.Columns.Clear();
      //LJCDataGridName.LJCAddDisplayColumns(displayColumns);

      // Create Columns, Configure and Load the grid rows.
      //LoadRows(dataManager);
      //RowAdd(dataManager);
      //RowSetValues(dataManager);

      var dataAccess = SetupSQL();

      // Create the Display column definitions.
      //var displayColumns = SetDisplayColumnsSQL(dataAccess);
      //displayColumns = SetDisplayColumnsSQL1(dataAccess);

      // Configure the grid columns.
      //LJCDataGridName.Columns.Clear();
      //LJCDataGridName.LJCAddDisplayColumns(displayColumns);

      // Create Columns, Configure and Load the grid rows.
      //LoadRowsSQL(dataAccess);
      //RowAddSQL(dataAccess);
      RowSetValuesSql(dataAccess);
    }

    #region Private DataManager Test Methods

    // Setup DataManager
    private DataManager Setup()
    {
      string connectionString;
      string providerName;
      DataManager retValue;

      // Create Data Configuration values.
      //var databaseName = "DatabaseName";
      var databaseName = "LJCData";
      //var tableName = "TableName";
      var tableName = "Province";

      bool useInternal = false;
      if (useInternal)
      {
        // Use internal configuration.
        DbConnectionStringBuilder connectionBuilder;
        connectionBuilder = new DbConnectionStringBuilder()
        {
          { "Data Source", "DataServiceName" },
          { "Initial Catalog", databaseName },
          { "Integrated Security", "True" }
        };
        connectionString = connectionBuilder.ConnectionString;
        providerName = "System.Data.SqlClient";
      }
      else
      {
        // Or use external configuration.
        var configName = "LJCData";
        DataConfigs dataConfigs = new DataConfigs();
        dataConfigs.LJCLoadData();
        var dataConfig = dataConfigs.LJCGetByName(configName);
        connectionString = dataConfig.GetConnectionString();
        providerName = dataConfig.GetProviderName();
      }

      // Create DataManager.
      var dbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(databaseName
          , connectionString, providerName)
      };
      retValue = new DataManager(dbServiceRef, null, tableName);
      return retValue;
    }

    // Configure the Display Columns from the DbColumns definition.
    private DbColumns SetDisplayColumns(DataManager dataManager)
    {
      DbColumns retValue = null;

      var dbResult = dataManager.Load();
      if (DbResult.HasData(dbResult))
      {
        // Create the Display column definitions.
        var resultGridData = new ResultGridData(LJCDataGridName);
        resultGridData.SetDisplayColumns(dbResult.Columns);
        retValue = resultGridData.DisplayColumns;
      }
      return retValue;
    }

    // Configure the Display Columns from the DbRequest object definition.
    private DbColumns SetDisplayColumns1(DataManager dataManager)
    {
      DbColumns retValue;

      // Get a View Request.
      //var dbRequest = ViewHelper.GetViewRequest("TableName", "ViewDataName");

      // Or Create the Request.
      var dbRequest = new DbRequest()
      {
        Columns = dataManager.DataDefinition,
        DataConfigName = dataManager.DataConfigName,
        RequestTypeName = RequestType.Load.ToString(),
        TableName = dataManager.TableName
      };

      // Create the Display column definitions.
      var resultGridData = new ResultGridData(LJCDataGridName);
      resultGridData.SetDisplayColumns(dbRequest);
      retValue = resultGridData.DisplayColumns;
      return retValue;
    }

    // Configure the Display Columns from the Data object properties.
    private DbColumns SetDisplayColumns2()
    {
      DbColumns retValue;

      var dataObject = new Province();

      // Create the Display column definitions.
      var resultGridData = new ResultGridData(LJCDataGridName);
      resultGridData.SetDisplayColumns(dataObject);
      retValue = resultGridData.DisplayColumns;
      return retValue;
    }

    // Loads the grid rows from the result Rows.
    private void LoadRows(DataManager dataManager)
    {
      var dbResult = dataManager.Load();
      if (DbResult.HasData(dbResult))
      {
        // Create the Display column definitions.
        var resultGridData = new ResultGridData(LJCDataGridName);
        resultGridData.SetDisplayColumns(dbResult.Columns);

        // Configure the grid columns.
        LJCDataGridName.Columns.Clear();
        LJCDataGridName.LJCAddDisplayColumns(resultGridData.DisplayColumns);

        // Load the grid rows.
        resultGridData.LoadRows(dbResult);
      }
    }

    // Adds a grid row and updates it with the DbValues.
    private void RowAdd(DataManager dataManager)
    {
      var dbResult = dataManager.Load();
      if (DbResult.HasData(dbResult))
      {
        // Create the Display column definitions.
        var resultGridData = new ResultGridData(LJCDataGridName);
        resultGridData.SetDisplayColumns(dbResult.Columns);

        // Configure the grid columns.
        LJCDataGridName.Columns.Clear();
        LJCDataGridName.LJCAddDisplayColumns(resultGridData.DisplayColumns);

        // Load the grid rows individually.
        foreach (DbRow dbRow in dbResult.Rows)
        {
          resultGridData.RowAdd(dbRow.Values);
        }
      }
    }

    // Updates a grid row with the DbValues.
    private void RowSetValues(DataManager dataManager)
    {
      var dbResult = dataManager.Load();
      if (DbResult.HasData(dbResult))
      {
        // Create the Display column definitions.
        var resultGridData = new ResultGridData(LJCDataGridName);
        resultGridData.SetDisplayColumns(dbResult.Columns);

        // Configure the grid columns.
        LJCDataGridName.Columns.Clear();
        LJCDataGridName.LJCAddDisplayColumns(resultGridData.DisplayColumns);

        // Create and load the grid rows individually.
        foreach (DbRow dbRow in dbResult.Rows)
        {
          var gridRow = LJCDataGridName.LJCRowAdd();
          resultGridData.RowSetValues(gridRow, dbRow.Values);
        }
      }
    }
    #endregion

    #region Private DataAccess Test Methods

    // Setup SQL
    private DataAccess SetupSQL()
    {
      string connectionString;
      string providerName;
      DataAccess retValue;

      // Create Data Configuration values.
      //var databaseName = "DatabaseName";
      var databaseName = "LJCData";

      bool useInternal = false;
      if (useInternal)
      {
        // Use internal configuration.
        DbConnectionStringBuilder connectionBuilder;
        connectionBuilder = new DbConnectionStringBuilder()
        {
          { "Data Source", "DataServiceName" },
          { "Initial Catalog", databaseName },
          { "Integrated Security", "True" }
        };

        connectionString = connectionBuilder.ConnectionString;
        providerName = "System.Data.SqlClient";
      }
      else
      {
        // Or use external configuration.
        var configName = "LJCData";
        DataConfigs dataConfigs = new DataConfigs();
        dataConfigs.LJCLoadData();
        var dataConfig = dataConfigs.LJCGetByName(configName);

        connectionString = dataConfig.GetConnectionString();
        providerName = dataConfig.GetProviderName();
      }

      // Create DataAccess.
      retValue = new DataAccess()
      {
        ConnectionString = connectionString,
        ProviderName = providerName
      };
      return retValue;
    }

    // Sets the Display Columns from the DataColumns object.
    private DbColumns SetDisplayColumnsSQL(DataAccess dataAccess)
    {
      DbColumns retValue = null;

      var sql = "select * from Province";
      var dataTable = dataAccess.GetDataTable(sql);
      if (NetCommon.HasData(dataTable))
      {
        var tableGridData = new TableGridData(LJCDataGridName);
        tableGridData.SetDisplayColumns(dataTable.Columns);
        retValue = tableGridData.DisplayColumns;
      }
      return retValue;
    }

    // Sets the Display Columns from the DataObject properties.
    private DbColumns SetDisplayColumnsSQL1(DataAccess dataAccess)
    {
      DbColumns retValue = null;

      var sql = "select * from Province";
      var dataTable = dataAccess.GetDataTable(sql);
      if (NetCommon.HasData(dataTable))
      {
        var province = new Province();

        var tableGridData = new TableGridData(LJCDataGridName);
        tableGridData.SetDisplayColumns(province);
        retValue = tableGridData.DisplayColumns;
      }
      return retValue;
    }

    // Loads the grid rows from the DataRows collection.
    private void LoadRowsSQL(DataAccess dataAccess)
    {
      var sql = "select * from Province";
      var dataTable = dataAccess.GetDataTable(sql);
      if (NetCommon.HasData(dataTable))
      {
        // Create the Display column definitions.
        var tableGridData = new TableGridData(LJCDataGridName);
        tableGridData.SetDisplayColumns(dataTable.Columns);

        // Configure the grid columns.
        LJCDataGridName.Columns.Clear();
        LJCDataGridName.LJCAddDisplayColumns(tableGridData.DisplayColumns);

        // Load the grid rows.
        tableGridData.LoadRows(dataTable);
      }
    }

    // Adds a grid row and updates it with the DbValues.
    private void RowAddSQL(DataAccess dataAccess)
    {
      var sql = "select * from Province";
      var dataTable = dataAccess.GetDataTable(sql);
      if (NetCommon.HasData(dataTable))
      {
        // Create the Display column definitions.
        var tableGridData = new TableGridData(LJCDataGridName);
        tableGridData.SetDisplayColumns(dataTable.Columns);

        // Configure the grid columns.
        LJCDataGridName.Columns.Clear();
        LJCDataGridName.LJCAddDisplayColumns(tableGridData.DisplayColumns);

        // Load the grid rows individually.
        foreach (DataRow row in dataTable.Rows)
        {
          tableGridData.RowAdd(row);
        }
      }
    }

    // Updates a grid row with the DbValues.
    private void RowSetValuesSql(DataAccess dataAccess)
    {
      var sql = "select * from Province";
      var dataTable = dataAccess.GetDataTable(sql);
      if (NetCommon.HasData(dataTable))
      {
        // Create the Display column definitions.
        var tableGridData = new TableGridData(LJCDataGridName);
        tableGridData.SetDisplayColumns(dataTable.Columns);

        // Configure the grid columns.
        LJCDataGridName.Columns.Clear();
        LJCDataGridName.LJCAddDisplayColumns(tableGridData.DisplayColumns);

        // Create and load the grid rows individually.
        foreach (DataRow row in dataTable.Rows)
        {
          var gridRow = LJCDataGridName.LJCRowAdd();
          tableGridData.RowSetValues(gridRow, row);
        }
      }
    }
    #endregion

    #region Class Data

    private readonly LJCDataGrid LJCDataGridName;
    #endregion
  }
}
