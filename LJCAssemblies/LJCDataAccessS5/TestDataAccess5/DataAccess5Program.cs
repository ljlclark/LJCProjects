// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataAccess5Program.cs

namespace TestDataAccess5
{
  // The entry class.
  internal class DataAccessProgram
  {
    // The entry method.
    static void Main()
    {
      _ = new TestDataAccess();
      _ = new TestDataCommon();
      _ = new TestDbColumn();
      //_ = new TestDbColumns();
      _ = new TestProcedureParam();
      _ = new TestProcedureParams();
      //_ = new TestProviderFactory();
      //_ = new TestMySqlDataAccess();
      //_ = new TestTableMapping();
    }
  }
}