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
      ShowPath(manager, "Retrieve");
      if (codeLine != null)
      {
        codeLine.Path = $"{codeLine.Path}Updated";
        manager.Update(codeLine);
        ShowPath(manager, "Update");
      }

      manager.Add("NewCodeLine", "NewPath");
      ShowPath(manager, "Add");

      manager.Delete("NewCodeLine");
    }

    public void ShowPath(CodeLineManager manager, string text)
    {
      var codeLine = manager.DataObject();
      if (codeLine != null)
      {
        MessageBox.Show($"{text}\r\n{codeLine.Path}");
      }
    }
  }
}
