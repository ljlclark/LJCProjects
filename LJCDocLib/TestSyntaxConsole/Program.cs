// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// Program.cs
using LJCNetCommon;
using System;
using System.IO;
using System.Reflection;

namespace TestSyntaxConsole
{
  // The program entry point class.
  /// <include path='items/Program/*' file='Doc/ProjectTestSyntaxConsole.xml'/>
  public class Program
  {
    // The program entry point function.
    /// <include path='items/Main/*' file='../../CommonProgram.xml'/>
    static void Main()
    {
      string backPath = @"..\..\..\..\LJC.FacilityManager";
      string folderPath = Path.Combine(backPath, @"LJC.FacilityManager\bin\Debug");
      string fileSpec = Path.Combine(folderPath, "LJC.FacilityManager.exe");
      if (File.Exists(fileSpec))
      {
        LJCAssemblyReflect assemblyReflect = new LJCAssemblyReflect();
        assemblyReflect.SetAssembly(fileSpec);

        Assembly assembly = assemblyReflect.Assembly;
        foreach (Type type in assembly.DefinedTypes)
        {
          if (false == type.Name.StartsWith("<>"))
          {
            Console.WriteLine();
            Console.WriteLine("*******************************");
            string typeSyntax = assemblyReflect.GetTypeSyntax(type);
            Console.WriteLine(typeSyntax);

            foreach (MethodInfo methodInfo in type.GetMethods())
            {
              string methodSyntax = assemblyReflect.GetMethodSyntax(methodInfo);
              if (methodSyntax != null)
              {
                Console.WriteLine(methodSyntax);
              }
            }
          }
        }
      }
      Console.ReadLine();
    }
  }
}
