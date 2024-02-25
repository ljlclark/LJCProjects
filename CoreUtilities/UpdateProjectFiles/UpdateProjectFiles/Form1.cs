using ProjectFilesDAL;
using System.Windows.Forms;

namespace UpdateProjectFiles
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();

      //TestCodeLine();
      TestCodeGroup();
    }

    public void TestCodeLine()
    {
      var manager = new CodeLineManager();
      manager.Retrieve("LJCProjectsDev");
      ShowCodeLine(manager, "Retrieve");

      var codeLine = manager.Add("ANewCodeLine", "NewPath");
      ShowCodeLine(manager, "Add");
      manager.SortFile();
      if (codeLine != null)
      {
        codeLine.Path = $"{codeLine.Path}Updated";
        manager.Update(codeLine);
        ShowCodeLine(manager, "Update");
        manager.SortFile();
      }

      manager.Delete("ANewCodeLine");
    }

    public void TestCodeGroup()
    {
      var manager = new CodeGroupManager();
      manager.Retrieve("LJCProjectsDev", "CoreAssemblies");
      ShowCodeGroup(manager, "Retrieve");

      var codeGroup = manager.Add("LJCProjectsDev", "ANewCodeGroup", "NewPath");
      ShowCodeGroup(manager, "Add");
      manager.SortFile();
      if (codeGroup != null)
      {
        codeGroup.Path = $"{codeGroup.Path}Updated";
        manager.Update(codeGroup);
        ShowCodeGroup(manager, "Update");
        manager.SortFile();
      }

      manager.Delete("ANewCodeGroup");
    }

    public void ShowCodeLine(CodeLineManager manager, string text)
    {
      var codeLine = manager.DataObject();
      if (codeLine != null)
      {
        MessageBox.Show($"{text}\r\n{codeLine.Path}");
      }
    }

    public void ShowCodeGroup(CodeGroupManager manager, string text)
    {
      var codeGroup = manager.DataObject();
      if (codeGroup != null)
      {
        MessageBox.Show($"{text}\r\n{codeGroup.Path}");
      }
    }
  }
}
