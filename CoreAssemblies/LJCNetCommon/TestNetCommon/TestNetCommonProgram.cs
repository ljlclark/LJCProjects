// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestNetCommonProgram.cs

namespace TestNetCommon
{
  internal class TestNetCommonProgram
  {
    static void Main()
    {
      _ = new TestDataRows();
      _ = new TestDataColumn();
      _ = new TestDataColumns();
      _ = new TestDataValue();
      _ = new TestDataValues();

      _ = new TestNetString();
      _ = new TestCSParser();

      _ = new TestHTMLBuilder();
    }
  }
}
