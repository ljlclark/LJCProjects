// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataConfigComboCode5.cs
using LJCControls5;
using LJCDataAccessConfig5;
using static LJCDataUtility5.DataUtilityList;

namespace LJCDataUtility5
{
  // Provides methods for the DataConfig combo.
  internal class DataConfigComboCode
  {
    #region Properties

    // Gets or sets the Combo reference.
    private LJCItemCombo ConfigCombo { get; set; }

    // Gets or sets the database id.
    internal short DbGroupId { get; set; }

    // Gets or sets the Parent List reference.
    private DataUtilityList ParentObject { get; set; }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    internal DataConfigComboCode(DataUtilityList parentObject, short dbGroupId)
    {
      // Initialize property values.
      ParentObject = parentObject;
      ParentObject.Cursor = Cursors.WaitCursor;
      DbGroupId = dbGroupId;

      // Set Combo vars.
      ConfigCombo = ParentObject.ConfigCombo;

      // Combo events.
      var combo = ConfigCombo;
      combo.SelectedIndexChanged += Combo_SelectedIndexChanged;
      combo.MouseEnter += Combo_MouseEnter;

      ParentObject.Cursor = Cursors.Default;
    }
    #endregion

    #region Item Value Methods

    // Gets the DataConfig object.
    internal LJCDataConfig? DataConfigItem()
    {
      LJCItemCombo configCombo = ParentObject.ConfigCombo;
      var retConfig = configCombo.SelectedItem as LJCDataConfig;
      return retConfig;
    }

    // Gets the selected item Name.
    internal string? ItemName()
    {
      string? retConfigName = null;

      if (ConfigCombo.SelectedIndex >= 0)
      {
        retConfigName = ConfigCombo.Text;
      }
      return retConfigName;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      ParentObject.Cursor = Cursors.WaitCursor;
      ConfigCombo.Items.Clear();

      var dataConfigs = new LJCDataConfigs();
      dataConfigs.LoadData();
      foreach (var dataConfig in dataConfigs)
      {
        ConfigCombo.Items.Add(dataConfig);
      }
      if (ConfigCombo.Items.Count > 0)
      {
        ConfigCombo.SelectedIndex = 0;
      }

      ParentObject.Cursor = Cursors.Default;
      ParentObject.DoChange(Change.Config);
    }
    #endregion

    #region Control Event Handlers

    // Handles the SelectionChanged event.
    private void Combo_SelectedIndexChanged(object? sender, EventArgs e)
    {
      ParentObject.TimedChange(Change.Config);
    }

    // Handles the MouseEnter event.
    private void Combo_MouseEnter(object? sender, EventArgs e)
    {
      ConfigCombo.Focus();
    }
    #endregion
  }
}
