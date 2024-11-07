// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataUtilityList.cs
using LJCDataUtilityDAL;
using LJCNetCommon;
using LJCWinFormControls;
using System;
using System.Collections.Generic;
using System.Drawing;
//using System.Text;
using System.Windows.Forms;

namespace LJCDataUtility
{
  /// <summary>
  /// 
  /// </summary>
  public partial class DataUtilityList : Form
  {
    /// <summary>
    /// 
    /// </summary>
    public DataUtilityList()
    {
      InitializeComponent();

      InitializeControls();
      RestoreControlValues();

      Testing();
    }

    #region Action Event Handlers

    #region Module
    private void ModuleRefresh_Click(object sender, EventArgs e)
    {
      ModuleGridCode.Refresh();
    }

    private void ModuleExit_Click(object sender, EventArgs e)
    {
      SaveControlValues();
      Close();
    }
    #endregion

    #region Table

    private void TableRefresh_Click(object sender, EventArgs e)
    {
      TableGridCode.Refresh();
    }
    #endregion

    #region Column

    private void ColumnRefresh_Click(object sender, EventArgs e)
    {
      ColumnGridCode.Refresh();
    }
    #endregion

    #region Key

    private void KeyRefresh_Click(object sender, EventArgs e)
    {
      KeyGridCode.Refresh();
    }
    #endregion

    #region MapTable

    private void MapTableRefresh_Click(object sender, EventArgs e)
    {
      MapTableGridCode.Refresh();
    }
    #endregion

    #region MapColumn

    private void MapColumnRefresh_Click(object sender, EventArgs e)
    {
      MapColumnGridCode.Refresh();
    }
    #endregion

    #endregion

    #region Control Event Handlers
    #endregion
  }
}
