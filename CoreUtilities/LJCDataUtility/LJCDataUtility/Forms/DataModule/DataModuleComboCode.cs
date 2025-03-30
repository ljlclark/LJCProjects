// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataModuleComboCode.cs
using LJCDataUtilityDAL;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static LJCDataUtility.DataUtilityList;

namespace LJCDataUtility
{
  // Provides DataModuleCombo methods for the Parent window.
  internal class DataModuleComboCode
  {
    #region Constructors

    // Initializes an object instance.
    internal DataModuleComboCode(DataUtilityList parentObject)
    {
      // Initialize property values.
      ParentObject = parentObject;
      ParentObject.Cursor = Cursors.WaitCursor;

      // Set Combo vars.
      ModuleCombo = ParentObject.ModuleCombo;
      ModuleMenu = ParentObject.ModuleMenu;
      Managers = ParentObject.Managers;
      ModuleManager = Managers.DataModuleManager;

      // Menu item events.
      var list = ParentObject;
      list.ModuleNew.Click += ModuleNew_Click;
      list.ModuleEdit.Click += ModuleEdit_Click;
      list.ModuleDelete.Click += ModuleDelete_Click;
      list.ModuleRefresh.Click += ModuleRefresh_Click;
      list.ModuleExit.Click += list.Exit_Click;

      // Combo events.
      var combo = ModuleCombo;
      combo.KeyDown += ModuleGrid_KeyDown;
      combo.SelectedIndexChanged += ModuleGrid_SelectedIndexChanged;
      ParentObject.Cursor = Cursors.Default;
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
      ModuleManager.Manager.OrderByNames = orderByNames;
      var dataItems = ModuleManager.Load();
      if (NetCommon.HasItems(dataItems))
      {
        foreach (var dataItem in dataItems)
        {
          RowAdd(dataItem);
        }
        if (ModuleCombo.Items.Count > 0)
        {
          ModuleCombo.SelectedIndex = 0;
        }
        ModuleCombo.Select();
      }
      //SetControlState();
      ParentObject.Cursor = Cursors.Default;
      ParentObject.DoChange(Change.Module);
    }

    // Adds a grid row and updates it with the record values.
    private LJCItem RowAdd(DataModule data)
    {
      // *** Next Statement *** Demotion needs correcting.
      var retValue = ModuleCombo.LJCAddItem((int)data.ID, data.Name);
      return retValue;
    }

    // Selects a row based on the key record values.
    private bool RowSelect(int id)
    {
      bool retValue = false;

      if (id > 0)
      {
        ParentObject.Cursor = Cursors.WaitCursor;
        foreach (LJCItem item in ModuleCombo.Items)
        {
          var rowID = ParentObject.DataModuleItemID(item);
          if (rowID == id)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            ModuleCombo.LJCSetByItemID(id);
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
      ParentObject.ModuleHeading.Enabled = true;
    }
    #endregion

    #region Action Methods

    // Deletes the selected row.
    internal void Delete()
    {
      bool isContinue = true;
      var item = ModuleCombo.SelectedItem as LJCItem;
      if (item != null)
      {
        var title = "Delete Confirmation";
        var message = FormCommon.DeleteConfirm;
        if (DialogResult.No == MessageBox.Show(message, title
          , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
        {
          isContinue = false;
        }
      }

      if (isContinue)
      {
        var id = ParentObject.DataModuleItemID();
        var keyColumns = new DbColumns()
        {
          { DataModule.ColumnID, id }
        };
        ModuleManager.Delete(keyColumns);
        if (0 == ModuleManager.AffectedCount)
        {
          isContinue = false;
          var message = FormCommon.DeleteError;
          MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
            , MessageBoxIcon.Exclamation);
        }
      }

      if (isContinue)
      {
        ModuleCombo.Items.Remove(item);
        SetControlState();
        ParentObject.TimedChange(Change.Module);
      }
    }

    // Displays a detail dialog to edit a record.
    internal void Edit()
    {
      if (ModuleCombo.SelectedItem is LJCItem)
      {
        int id = ParentObject.DataModuleItemID();
        //var location = FormPoint.DialogScreenPoint(ModuleGrid);
        var detail = new DataModuleDetail()
        {
          LJCID = id,
          //LJCLocation = location,
          LJCManagers = Managers,
        };
        detail.LJCChange += Detail_Change;
        //detail.LJCLocation = FormPoint.AdjustedLocation(detail, location);
        detail.ShowDialog();
      }
    }

    // Displays a detail dialog for a new record.
    internal void New()
    {
      //var location = FormPoint.DialogScreenPoint(ModuleGrid);
      var detail = new DataModuleDetail
      {
        //LJCLocation = location,
        LJCManagers = Managers,
      };
      detail.LJCChange += Detail_Change;
      //detail.LJCLocation = FormPoint.AdjustedLocation(detail, location);
      detail.ShowDialog();
    }

    // Refreshes the list.
    internal void Refresh()
    {
      ParentObject.Cursor = Cursors.WaitCursor;
      int id = 0;
      if (ModuleCombo.SelectedItem is LJCItem)
      {
        // Save the original row.
        id = ParentObject.DataModuleItemID();
      }
      DataRetrieve();

      // Select the original row.
      if (id > 0)
      {
        RowSelect(id);
      }
      ParentObject.Cursor = Cursors.Default;
    }

    // Shows the help page
    internal void ShowHelp()
    {
      //Help.ShowHelp(DocList, "_AppName_.chm", HelpNavigator.Topic
      //  , "_ClassName_List.html");
    }

    // Adds new row or updates row with control values.
    private void Detail_Change(object sender, EventArgs e)
    {
      var detail = sender as DataModuleDetail;
      var record = detail.LJCRecord;
      if (record != null)
      {
        if (detail.LJCIsUpdate)
        {
          RowUpdate(record);
        }
        else
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          var item = RowAdd(record);
          ModuleCombo.LJCSetByItemID(item.ID);
          SetControlState();
          ParentObject.TimedChange(Change.Module);
        }
      }
    }
    #endregion

    #region Action Event Handlers

    // Handles the New menu item event.
    private void ModuleNew_Click(object sender, EventArgs e)
    {
      New();
    }

    // Handles the Edit menu item event.
    private void ModuleEdit_Click(object sender, EventArgs e)
    {
      Edit();
    }

    // Handles the Delete menu item event.
    private void ModuleDelete_Click(object sender, EventArgs e)
    {
      Delete();
    }

    // Handles the Refresh menu item event.
    private void ModuleRefresh_Click(object sender, EventArgs e)
    {
      Refresh();
    }
    #endregion

    #region Control Event Handlers

    // Handles the Grid KeyDown event.
    private void ModuleGrid_KeyDown(object sender, KeyEventArgs e)
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

        case Keys.Tab:
          MessageBox.Show("Everywhere");
          if (e.Shift)
          {
            ParentObject.ColumnTabs.Select();
          }
          else
          {
            ParentObject.ColumnTabs.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the SelectionChanged event.
    private void ModuleGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
      ParentObject.TimedChange(Change.Module);
    }
    #endregion

    #region Properties

    // Gets or sets the Parent List reference.
    private DataUtilityList ParentObject { get; set; }

    // Gets or sets the Combo reference.
    private LJCItemCombo ModuleCombo { get; set; }

    // Gets or sets the Menu reference.
    private ContextMenuStrip ModuleMenu { get; set; }

    // Gets or sets the Managers reference.
    private ManagersDataUtility Managers { get; set; }

    // Gets or sets the Manager reference.
    private DataModuleManager ModuleManager { get; set; }
    #endregion
  }
}
