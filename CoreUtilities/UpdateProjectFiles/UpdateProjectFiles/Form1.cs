using ProjectFilesDAL;
using System.Text;
using System.Windows.Forms;

namespace UpdateProjectFiles
{
  internal partial class Form1 : Form
  {
    internal Form1()
    {
      InitializeComponent();

      //TestCodeLine();
      //TestCodeGroup();
      TestSolution();
    }

    internal void TestCodeLine()
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
      }

      manager.Delete("ANewCodeLine");
    }

    internal void TestCodeGroup()
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
      }

      manager.Delete("LJCProjectsDev", "ANewCodeGroup");
    }

    internal void TestSolution()
    {
      var manager = new SolutionManager();
      var solutionParentKey = new SolutionParentKey()
      {
        CodeLine = "LJCProjectsDev",
        CodeGroup = "CoreAssemblies"
      };
      manager.Retrieve(solutionParentKey, "LJCNetCommon");
      ShowSolution(manager, "Retrieve");
    }

    private void ShowCodeGroup(CodeGroupManager manager, string text)
    {
      var codeGroup = manager.CurrentDataObject();
      if (codeGroup != null)
      {
        var builder = new StringBuilder(256);
        builder.AppendLine(text);
        builder.AppendLine(codeGroup.Name);
        builder.AppendLine(codeGroup.Path);
        var message = builder.ToString();
        MessageBox.Show(message);
      }
    }

    private void ShowCodeLine(CodeLineManager manager, string text)
    {
      var codeLine = manager.CurrentDataObject();
      if (codeLine != null)
      {
        var builder = new StringBuilder(256);
        builder.AppendLine(text);
        builder.AppendLine(codeLine.Name);
        builder.AppendLine(codeLine.Path);
        var message = builder.ToString();
        MessageBox.Show(message);
      }
    }

    private void ShowSolution(SolutionManager manager, string text)
    {
      var solution = manager.CurrentDataObject();
      if (solution != null)
      {
        var builder = new StringBuilder(256);
        builder.AppendLine(text);
        builder.AppendLine(solution.Name);
        builder.Append(solution.Path);
        var message = builder.ToString();
        MessageBox.Show(message);
      }
    }
  }
}
