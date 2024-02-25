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
using static System.Net.Mime.MediaTypeNames;

namespace UpdateProjectFiles
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();

      TestCodeLine();
    }

    public void TestCodeLine()
    {
      var manager = new CodeLineManager();
      manager.Retrieve("LJCProjectsDev");
      ShowPath(manager, "Retrieve");

      var codeLine = manager.Add("ANewCodeLine", "NewPath");
      ShowPath(manager, "Add");
      manager.SortFile();
      if (codeLine != null)
      {
        codeLine.Path = $"{codeLine.Path}Updated";
        manager.Update(codeLine);
        ShowPath(manager, "Update");
        manager.SortFile();
      }

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
