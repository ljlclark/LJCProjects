using LJCDataUtilityDAL;
//using LJCGenTextLib;
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

      SetConfig();
      Managers = mValues.Managers;

      InitializeControls();
      RestoreControlValues();

      Testing();
    }

    private void ModuleExit_Click(object sender, EventArgs e)
    {
      SaveControlValues();
      Close();
    }
  }
}
