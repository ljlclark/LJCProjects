// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataColumnsProgram.cs

namespace TestDataColumns5
{
  // The entry class.
  internal class DataColumnsProgram
  {
    // The entry method.
    static void Main()
    {
      _ = new TestDataColumn();
      _ = new TestDataColumns();

      _ = new TestDataValue();
      _ = new TestDataValues();

      Console.WriteLine();
      Console.WriteLine();
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }
  }
}
