using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LJCDataUtility
{
  /// <summary>
  /// 
  /// </summary>
  public partial class DataTableDetail : Form
  {
    /// <summary>
    /// 
    /// </summary>
    public DataTableDetail()
    {
      InitializeComponent();
    }

    private void DataTableDetail_Load(object sender, EventArgs e)
    {
      AcceptButton = OKButton;
      CancelButton = FormCancelButton;

      CenterToParent();
    }

    private void FormCancelButton_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void OKButton_Click(object sender, EventArgs e)
    {
      Close();
    }
  }
}
