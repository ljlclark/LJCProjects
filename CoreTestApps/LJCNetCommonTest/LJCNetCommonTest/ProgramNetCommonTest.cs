// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ProgramNetComonTest.cs
using System;

namespace LJCNetCommonTest
{
  // <include path='items/Program/*' file='../../../CoreUtilities/LJCDocLib/Common/Program.xml'/>
  internal class ProgramNetCommonTest
  {
    // <include path='items/Main/*' file='../../../CoreUtilities/LJCDocLib/Common/Program.xml'/>
    private static void Main()
    {
      NetCommonTest.Test();
      NetFileTest.Test();
      NetStringTest.Test();

      HTMLBuilderTest.Test();
      TextBuilderTest.Test();
      XMLBuilderTest.Test();

      Console.Write("Press any key to continue ...");
      Console.ReadKey();
    }
  }
}