using ProjectFilesDAL;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UpdateProjectFiles
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();

      var manager = new CodeLineManager();
      var codeLine = manager.Retrieve("LJCProjectsDev");
      if (codeLine != null)
      {
        MessageBox.Show(codeLine.Path);
        codeLine.Path = $"{codeLine.Path}Updated";
        manager.Update(codeLine);
      }

      manager.Add("NewCodeLine", "NewPath");
      manager.Delete("NewCodeLine");
    }
  }
}
