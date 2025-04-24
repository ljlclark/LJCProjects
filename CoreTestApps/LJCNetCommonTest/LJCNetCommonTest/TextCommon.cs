using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LJCNetCommonTest
{
  internal class TextCommon
  {

    internal TextCommon(string className)
    {
      mClassName = className;
    }

    internal void Write(string methodName, string result
      , string compare)
    {
      if (result != compare)
      {
        Console.WriteLine($"\r\n{mClassName}.{methodName}");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
      }
    }

    private string mClassName;
  }
}
