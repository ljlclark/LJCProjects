// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDataProgram.cs

namespace TestData
{
  internal class TestDataProgram
  {
    static void Main(string[] args)
    {
      _ = new TestDataRows();

      _ = new TestDataColumn();
      _ = new TestDataColumns();

      _ = new TestDataValue();
      _ = new TestDataValues();

      _ = new TestDbColumn();
      _ = new TestDbColumns();

      _ = new TestDbValue();
      _ = new TestDbValues();
    }
  }
}
