using ProjectFilesDAL;
using System.Security.Policy;
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
      TestCodeGroup();
      TestSolution();
    }

    // 
    internal void TestCodeLine()
    {
      var manager = new CodeLineManager();

      // Add
      var newCodeLineName = "LJCProjects";
      var codeLine = manager.Add(newCodeLineName, "NewPath");
      ShowCodeLine(manager, "Add");

      // Load and Sort
      manager.SortFile();

      // Update
      if (codeLine != null)
      {
        codeLine.Path = $"{codeLine.Path}Updated";
        manager.Update(codeLine);
        ShowCodeLine(manager, "Update");
      }

      // Delete
      manager.Delete(newCodeLineName);
    }

    // 
    internal void TestCodeGroup()
    {
      var manager = new CodeGroupManager();

      // Retrieve
      var parentKey = "LJCProjectsDev";
      manager.Retrieve(parentKey, "CoreAssemblies");
      ShowCodeGroup(manager, "Retrieve");

      // Add
      var newCodeGroupName = "ANewCodeGroup";
      var codeGroup = manager.Add(parentKey, newCodeGroupName, "NewPath");
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
      manager.Delete(parentKey, newCodeGroupName);
    }

    // 
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
      var newSolutionName = "ANewSolution";
      int sequence = 3;
      var solution = manager.Add(parentKey, newSolutionName, sequence
        , "NewPath");
      ShowSolution(manager, "Add");

      // Load and Sort
      manager.SortFile();

      // Update
      if (solution != null)
      {
        solution.Path = $"{solution.Path}Updated";
        manager.Update(solution);
        ShowSolution(manager, "Update");
      }

      // Delete
      manager.Delete(parentKey, newSolutionName);
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
