// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataUtilityList.cs
using LJCDataUtilityDAL;
using LJCNetCommon;
using LJCWinFormCommon;
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

      _ = new TabsFont(ljcTabControl1);

      // Testing
      ModuleGridCode.DataRetrieve();
      TableGridCode.DataRetrieve();
      ColumnGridCode.DataRetrieve();
      KeyGridCode.DataRetrieve();
      MapTableGridCode.DataRetrieve();
      MapColumnGridCode.DataRetrieve();
    }

    #region Action Event Handlers

    private void Exit_Click(object sender, EventArgs e)
    {
      SaveControlValues();
      Close();
    }
    #endregion
  }
}
