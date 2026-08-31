// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataModuleComboCode5.cs
using LJCControls5;
using LJCDataUtilityDAL5;
using LJCNetCommon5;
using static LJCDataUtility5.DataUtilityList;

namespace LJCDataUtility5
{
  // Provides methods for the DataModule combo.
  internal class DataModuleComboCode
  {
    #region Constructor Methods

    // Initializes an object instance.
    internal DataModuleComboCode(DataUtilityList parentObject, short dbGroupId)
    {
      // Initialize property values.
      ParentObject = parentObject;
      ParentObject.Cursor = Cursors.WaitCursor;
      DbGroupId = dbGroupId;

      // Set Combo vars.
      ModuleCombo = ParentObject.ModuleCombo;
      ModuleMenu = ParentObject.ModuleMenu;

      // Set Data vars.
      Managers = ParentObject.Managers;
      Reset();

      // Menu item events.
      var list = ParentObject;
      list.ModuleNew.Click += ModuleNew_Click;
      list.ModuleEdit.Click += ModuleEdit_Click;
      list.ModuleDelete.Click += ModuleDelete_Click;
      list.ModuleRefresh.Click += ModuleRefresh_Click;
      list.ModuleExit.Click += list.Exit_Click;

      // Combo events.
      var combo = ModuleCombo;
      combo.KeyDown += ModuleCombo_KeyDown;
      combo.SelectedIndexChanged += ModuleCombo_SelectedIndexChanged;
      combo.MouseEnter += Combo_MouseEnter;

      ParentObject.Cursor = Cursors.Default;
    }

    // Resets the data manager.
    public void Reset()
    {
      if (!LJC.Equals(CurrentDataConfigName, Managers.DataConfigName))
      {
        ModuleManager = Managers.DataModuleManager;
        CurrentDataConfigName = Managers.DataConfigName;
        var error = Managers.Error;
        if (LJC.HasText(error))
        {
          MessageBox.Show(error);
        }
      }
    }
    #endregion

    #region Item Value Methods

    // Gets the selected item ID.
    internal long ItemId(out short dbId, LJCItem? item = null)
    {
      long retItemId = 0;

      dbId = 0;
      if (null == item)
      {
        item = ModuleCombo.SelectedItem as LJCItem;
      }
      if (item != null)
      {
        dbId = item.DbID;
        retItemId = item.ID;
      }
      return retItemId;
    }

    // Gets the selected item Name.
    internal string? ItemName(LJCItem? item = null)
    {
      string? retName = null;

      if (null == item)
      {
        item = ModuleCombo.SelectedItem as LJCItem;
      }
      if (item != null)
      {
        retName = item.Text;
      }
      return retName;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      ParentObject.Cursor = Cursors.WaitCursor;
      ModuleCombo.Items.Clear();

      var orderByNames = new List<string>()
      {
        "Name"
      };

      if (ModuleManager != null
        && ModuleManager.Manager != null)
      {
        ModuleManager.Manager.OrderByNames = orderByNames;
        var result = ModuleManager.LoadResult();
        if (result != null
          && LJC.HasListItems(result.Rows))
        {
          foreach (var row in result.Rows)
          {
            if (LJC.HasListItems(row.Values))
            {
              RowAddValues(row.Values);
            }
          }
        }
        if (ModuleCombo.Items.Count > 0)
        {
          ModuleCombo.SelectedIndex = 0;
        }
        ModuleCombo.Select();
      }

      SetControlState();
      ParentObject.Cursor = Cursors.Default;
      ParentObject.DoChange(Change.Module);
    }

    // Adds a combo item.
    private LJCItem RowAdd(DataModule data)
    {
      var retValue = ModuleCombo.LJCAddItem(data.Id, data.DbId, data.Name);
      return retValue;
    }

    // Adds a combo item.
    private LJCItem? RowAddValues(LJCDataValues dataValues)
    {
      LJCItem? retValue = null;

      var dbId = dataValues.LJCInt16(DataModule.ColumnDbId);
      var id = dataValues.LJCInt64(DataModule.ColumnId);
      var name = dataValues.LJCString(DataModule.ColumnName);
      if (LJC.HasText(name))
      {
        retValue = ModuleCombo.LJCAddItem(id, dbId, name);
      }
      return retValue;
    }

    // Selects a row based on the key record values.
    private bool RowSelect(short dbId, long id)
    {
      bool retValue = false;

      if (dbId > 0
        && id > 0)
      {
        ParentObject.Cursor = Cursors.WaitCursor;
        foreach (LJCItem item in ModuleCombo.Items)
        {
          var rowId = ItemId(out short rowDbId, item);
          if (rowId == id
            && rowDbId == dbId)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            ModuleCombo.LJCSetByItemID(id, dbId);
            retValue = true;
            break;
          }
        }
        ParentObject.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(DataModule data)
    {
      if (ModuleCombo.SelectedItem is LJCItem)
      {
        ModuleCombo.Text = data.Name;
      }
    }

    // Sets the control states based on the current control values.
    private void SetControlState()
    {
      bool enableNew = true;
      bool enableEdit = ModuleCombo.SelectedItem != null;
      FormCommon.SetMenuState(ModuleMenu, enableNew, enableEdit);
      //ParentObject.ModuleHeading.Enabled = true;
    }
    #endregion

    #region Action Methods

    // Deletes the selected row.
    internal void Delete()
    {
      while (true)
      {
        var item = ModuleCombo.SelectedItem as LJCItem;
        if (item != null)
        {
          var title = "Delete Confirmation";
          var message = FormCommon.DeleteConfirm;
          if (DialogResult.No == MessageBox.Show(message, title
            , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
          {
            break;
          }
        }

        // Data from current item.
        var id = ItemId(out short dbID);

        var keyColumns = new LJCDataColumns()
        {
          { DataModule.ColumnId, id },
          { DataModule.ColumnDbId, dbID },
        };

        if (ModuleManager != null)
        {
          ModuleManager.Delete(keyColumns);
          if (0 == ModuleManager.AffectedCount)
          {
            var message = FormCommon.DeleteError;
            MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
              , MessageBoxIcon.Exclamation);
            break;
          }

          var index = ModuleCombo.SelectedIndex;
          if (index >= ModuleCombo.Items.Count - 1)
          {
            index -= 1;
          }
          ModuleCombo.Items.Remove(item);
          ModuleCombo.SelectedIndex = index;
          SetControlState();
        }
        ParentObject.TimedChange(Change.Module);
        break;
      }
    }

    // Displays a detail dialog to edit a record.
    internal void Edit()
    {
      // Current combo has selection.
      if (ModuleCombo.SelectedItem is LJCItem)
      {
        // Data from current item.
        var id = ItemId(out short dbId);

        var detail = new DataModuleDetail()
        {
          LJCDbId = dbId,
          LJCId = id,
          LJCManagers = Managers,
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
        detail.Dispose();
      }
    }

    // Displays a detail dialog for a new record.
    internal void New()
    {
      var detail = new DataModuleDetail
      {
        LJCDbId = DbGroupId,
        LJCManagers = Managers,
      };
      detail.LJCChange += Detail_Change;
      detail.ShowDialog();
      detail.Dispose();
    }

    // Refreshes the list.
    internal void Refresh()
    {
      ParentObject.Cursor = Cursors.WaitCursor;
      short dbId = 0;
      long id = 0;
      if (ModuleCombo.SelectedItem is LJCItem)
      {
        // Save the original row.
        id = ItemId(out dbId);
      }
      DataRetrieve();

      // Select the original row.
      if (dbId > 0
        && id > 0)
      {
        RowSelect(dbId, id);
      }
      ParentObject.Cursor = Cursors.Default;
    }

    // Shows the help page
    internal void ShowHelp()
    {
      //Help.ShowHelp(DocList, "_AppName_.chm", HelpNavigator.Topic
      //  , "_ClassName_List.html");
    }

    // Adds or updates row with detail record values.
    private void Detail_Change(object? sender, EventArgs e)
    {
      if (sender is DataModuleDetail detail)
      {
        var record = detail.LJCRecord;
        if (record != null)
        {
          if (detail.LJCIsUpdate)
          {
            RowUpdate(record);
            Refresh();
          }
          else
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            var item = RowAdd(record);
            ModuleCombo.LJCSetByItemID((int)item.ID, item.DbID);
            SetControlState();
            ParentObject.TimedChange(Change.Module);
          }
        }
      }
    }
    #endregion

    #region Action Event Handlers

    // Handles the New menu item event.
    private void ModuleNew_Click(object? sender, EventArgs e)
    {
      New();
    }

    // Handles the Edit menu item event.
    private void ModuleEdit_Click(object? sender, EventArgs e)
    {
      Edit();
    }

    // Handles the Delete menu item event.
    private void ModuleDelete_Click(object? sender, EventArgs e)
    {
      Delete();
    }

    // Handles the Refresh menu item event.
    private void ModuleRefresh_Click(object? sender, EventArgs e)
    {
      Refresh();
    }
    #endregion

    #region Control Event Handlers

    // Handles the Grid KeyDown event.
    private void ModuleCombo_KeyDown(object? sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          Edit();
          e.Handled = true;
          break;

        case Keys.F1:
          ShowHelp();
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormPoint.MenuScreenPoint(ModuleCombo
              , Control.MousePosition);
            var menu = ParentObject.ModuleMenu;
            menu.Show(position);
            menu.Select();
            e.Handled = true;
          }
          break;

          //case Keys.Tab:
          //  if (e.Shift)
          //  {
          //    ParentObject.ModuleCombo.Select();
          //  }
          //  else
          //  {
          //    ParentObject.ConfigCombo.Select();
          //  }
          //  e.Handled = true;
          //  break;
      }
    }

    // Handles the SelectionChanged event.
    private void ModuleCombo_SelectedIndexChanged(object? sender, EventArgs e)
    {
      SetControlState();
      ParentObject.TimedChange(Change.Module);
    }

    // Handles the MouseEnter event.
    private void Combo_MouseEnter(object? sender, EventArgs e)
    {
      ModuleCombo.Focus();
    }
    #endregion

    #region Properties

    // Gets or sets the current data config name.
    private string? CurrentDataConfigName { get; set; }

    // Gets or sets the database id.
    private short DbGroupId { get; set; }

    // Gets or sets the Managers reference.
    private ManagersDataUtility Managers { get; set; }

    // Gets or sets the Combo reference.
    private LJCItemCombo ModuleCombo { get; set; }

    // Gets or sets the Manager reference.
    private DataModuleManager? ModuleManager { get; set; } = null!;

    // Gets or sets the Menu reference.
    private ContextMenuStrip ModuleMenu { get; set; }

    // Gets or sets the Parent List reference.
    private DataUtilityList ParentObject { get; set; }
    #endregion
  }
}
