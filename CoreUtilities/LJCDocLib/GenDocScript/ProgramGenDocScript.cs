
using System;

namespace GenDocScript
{
  internal class ProgramGenDocScript
  {
    static void Main()
    {
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
