// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Form1Code.cs
using LJCDataUtilityDAL5;
using LJCDBClientLib5;
using LJCNetCommon5;

namespace LJCDataUtility5
{
  public partial class Form1 : Form
  {
    #region Setup Methods

    private void InitializeControls()
    {
      InitializeClassData();
      SetupGrids();
    }

    // Initialize the Class Data.
    private void InitializeClassData()
    {
      //var values = ValuesDataUtility.Instance;
      //values.SetConfigFile("LJCDataUtility.exe.config");
      //var errors = values.Errors;
      //if (LJC.HasText(errors))
      //{
      //  MessageBox.Show(errors, "Config Errors", MessageBoxButtons.OK
      //    , MessageBoxIcon.Error);
      //}
      //ConnectionType = values.ConnectionType;
      ////DbGroupId = values.DbGroupID;
      //if (values.Managers != null)
      //{
      //  Managers = values.Managers;
      //}
      //Settings = values.StandardSettings;
      //Text += $" - {Settings.DataConfigName}";
    }

    // Setup the data grids.
    private void SetupGrids()
    {
      MainGridCode.SetupGrid();
    }
    #endregion

    #region Properties

    internal short DbGroupId { get; set; }

    // Gets or sets the connection type value.
    internal string ConnectionType { get; set; }

    // Gets or sets the ControlValues file name.
    //internal string ControlValuesFileName { get; set; }

    // Gets or sets the InfoValue item.
    //internal ControlValue InfoValue { get; set; }

    // Gets or sets the Managers object.
    internal ManagersDataUtility Managers { get; set; }

    // Gets or sets the configuration settings.
    internal LJCStandardUISettings Settings { get; set; }

    // Gets or sets the DataTableGridCode reference.
    internal DataTableGridCode TableGridCode { get; set; }
    //private ControlValues ControlValues { get; set; }
    #endregion
  }
}
