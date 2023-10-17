
namespace GenDocScript
{
  internal class ProgramGenDocScript
  {
    static void Main(string[] args)
    {
      var assemblyScript = new AssemblyScript();
      assemblyScript.Gen();
      var classScript = new ClassScript();
      classScript.Gen();
      var methodScript = new MethodScript();
      methodScript.Gen();
    }
  }
}
