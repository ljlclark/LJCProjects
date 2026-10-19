// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataManagerProgram.cs

namespace TestDataManager5
{
  // The entry class.
  internal class DataManagerProgram
  {
    // The entry method.
    static void Main()
    {
      _ = new TestChangedNames();
      _ = new TestDataManager();
      _ = new TestDbManager();
      _ = new TestDbServiceRef();
      _ = new TestManagerCommon();
      _ = new TestObjectManager();
      _ = new TestSqlManager();
    }
  }
}
