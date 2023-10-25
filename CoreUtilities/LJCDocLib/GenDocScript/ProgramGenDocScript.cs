
using LJCDocLibDAL;
using LJCNetCommon;
using System;

namespace GenDocScript
{
  internal class ProgramGenDocScript
  {

    static void Main()
    {
      Console.WriteLine();
      Console.WriteLine($"ProgramGenDocScript");
      Console.WriteLine();

      // Set DAL config before using anywhere in the program.
      var configValues = ValuesDocGen.Instance;
      configValues.SetConfigFile("GenDocScript.exe.config");
      var settings = configValues.StandardSettings;
      NetCommon.ConsoleConfig(settings.DataConfigName);

      var assemblyGroupScript = new AssemblyGroupScript();
      assemblyGroupScript.Gen();
      var assemblyScript = new AssemblyScript();
      assemblyScript.Gen();

      var classHeadingScript = new ClassGroupHeadingScript();
      classHeadingScript.Gen();
      var classGroupScript = new ClassGroupScript();
      classGroupScript.Gen();
      var classScript = new ClassScript();
      classScript.Gen();

      var methodHeadingScript = new MethodGroupHeadingScript();
      methodHeadingScript.Gen();
      var methodGroupScript = new MethodGroupScript();
      methodGroupScript.Gen();
      var methodScript = new MethodScript();
      methodScript.Gen();

      Console.WriteLine("Press any key to continue ...");
      Console.ReadKey();
    }
  }
}
