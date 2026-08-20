// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Form1.cs
using LJCDataAccessConfig5;
using LJCDataUtilityDAL5;
using LJCDBClientLib5;
using LJCDBDataAccess5;
using LJCNetCommon5;
using System.Configuration.Provider;

// Install-Package Microsoft.Data.SqlClient
// Install-Package System.Configuration.ConfigurationManager
// Microsoft.Extensions.Configuration

namespace LJCDataUtility5
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();

      // Initialize property values.
      // *** Begin *** Testing
      Settings = new LJCStandardUISettings();
      DbGroupId = 1;

      // *** Begin *** Testing
      var dataConfigName = "DataUtility";
      var dataConfigs = new LJCDataConfigs();
      dataConfigs.LoadData();
      var dataConfig = dataConfigs.Retrieve(dataConfigName);
      ConnectionType = "SQLServer";
      if (dataConfig.ConnectionType != null)
      {
        ConnectionType = dataConfig.ConnectionType;
      }
      Managers = new ManagersDataUtility(dataConfigName);
      // *** End ***

      TableGridCode = new DataTableGridCode(this);

      // Setup control code.
      MainGridCode = new DataTableGridCode(this);
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      InitializeControls();

      // Testing
      MainGridCode.DataRetrieve();

      CenterToScreen();
    }

    private DataTableGridCode MainGridCode { get; set; }
  }
}
