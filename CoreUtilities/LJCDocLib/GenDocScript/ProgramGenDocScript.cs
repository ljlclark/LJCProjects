
using System;

namespace GenDocScript
{
  internal class ProgramGenDocScript
  {
    static void Main(string[] args)
    {
      var assemblyGroupScript = new AssemblyGroupScript();
      assemblyGroupScript.Gen();
      var assemblyScript = new AssemblyScript();
      assemblyScript.Gen();

      var classGroupHeadingScript = new ClassGroupHeadingScript();
      classGroupHeadingScript.Gen();
      var classScript = new ClassScript();
      classScript.Gen();

      var methodScript = new MethodScript();
      methodScript.Gen();

      Console.WriteLine("Press any key to continue ...");
      Console.ReadKey();
    }
  }
}
