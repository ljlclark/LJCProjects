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

      // Retrieve
      manager.Retrieve("LJCProjectsDev", "CoreAssemblies");
      ShowCodeGroup(manager, "Retrieve");

      // Add
      var codeGroup = manager.Add("LJCProjectsDev", "ANewCodeGroup", "NewPath");
      ShowCodeGroup(manager, "Add");

      // Load and Sort
      manager.SortFile();

      // Update
      if (codeGroup != null)
      {
        codeGroup.Path = $"{codeGroup.Path}Updated";
        manager.Update(codeGroup);
        ShowCodeGroup(manager, "Update");
      }

      // Delete
      manager.Delete("LJCProjectsDev", "ANewCodeGroup");
    }

    internal void TestSolution()
    {
      var manager = new SolutionManager();

      // Retrieve
      var parentKey = new SolutionParentKey()
      {
        CodeLine = "LJCProjectsDev",
        CodeGroup = "CoreAssemblies"
      };
      manager.Retrieve(parentKey, "LJCNetCommon");
      ShowSolution(manager, "Retrieve");

      // Add
      int sequence = 3;
      var solution = manager.Add(parentKey, "ANewSolution", sequence
        , "NewPath");
      ShowSolution(manager, "Add");

      // Load and Sort
      //manager.SortFile();
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
