// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProcedureParamsProgram.cs
using LJCDataAccess5;
using LJCNetCommon5;
using System.Data;

namespace TestProcedureParameters5
{
  // The entry class.
  internal class ProcedureParamsProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCProcedureParameters");
      Console.WriteLine();
      Console.WriteLine("*** LJCProcedureParameters ***");

      // Constructor Methods
      ConstructorCopy();

      // Methods
      Add();
      LJCSearchName();

      Console.WriteLine();
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }

    #region Constructor Methods

    // The Copy constructor.
    private static void ConstructorCopy()
    {
      var methodName = "ConstructorCopy()";

      var parameterName = "Name";
      var dataTypeID = (int)SqlDbType.NVarChar;
      var size = 60;
      var value = "Name Value";
      var procedureParam = new LJCProcedureParameter(parameterName, dataTypeID
        , size, value);

      // Test Method
      var copyProcedureParam = new LJCProcedureParameter(procedureParam);
      var result = "";
      if (copyProcedureParam != null)
      {
        result = copyProcedureParam.ParameterName;
      }
      var compare = "Name";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Creates and adds the object from the supplied values.
    private static void Add()
    {
      var methodName = "Add()";

      var parms = new LJCProcedureParameters();
      var parameterName = "Name";
      var sqlDbTypeID = (int)SqlDbType.NVarChar;
      var mySqlDbTypeID = (int)LJCMySqlDbType.VarChar;
      var size = 60;
      var value = "Name Value";

      // Test Method
      var addParm = parms.Add(parameterName, sqlDbTypeID, mySqlDbTypeID, size
        , value);
      var result = "";
      if (addParm != null)
      {
        result = addParm.ParameterName;
      }
      var compare = "Name";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Retrieve the collection element by name.
    private static void LJCSearchName()
    {
      var methodName = "LJCSearchName()";

      var parms = new LJCProcedureParameters();
      var parameterName = "Name";
      var sqlDbTypeID = (int)SqlDbType.NVarChar;
      var mySqlDbTypeID = (int)LJCMySqlDbType.VarChar;
      var size = 60;
      var value = "Name Value";
      parms.Add(parameterName, sqlDbTypeID, mySqlDbTypeID, size, value);

      var foundParm = parms.LJCSearchName("Name");
      var result = "";
      if (foundParm != null)
      {
        result = foundParm.ParameterName;
      }
      var compare = "Name";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
