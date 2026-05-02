// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProcedureParamProgram.cs
using LJCDataAccess5;
using LJCNetCommon5;
using System.Data;

namespace TestProcedureParam5
{
  // The entry class.
  internal class ProcedureParamProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCDbColumns");
      Console.WriteLine();
      Console.WriteLine("*** LJCDbColumns ***");

      // Constructor Methods
      ConstructorCopy();
      ConstructorParam();

      // Methods
      ToStringMethod();
      Clone();
      CompareTo();

      Console.WriteLine();
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }

    #region Constructor Methods

    // The Copy constructor.
    private static void ConstructorCopy()
    {
      var methodName = "ConstructorCopy()";

      // Test Method
      var procedureParam = new LJCProcedureParameter()
      {
        Direction = ParameterDirection.Input,
        MySqlDbTypeID = (int)LJCMySqlDbType.VarChar,
        ParameterName = "Name",
        Precision = 0,
        Size = 60,
        SqlDbTypeID = (int)SqlDbType.NVarChar,
        Value = "Name Value",
      };
      var newProcedureParam = new LJCProcedureParameter(procedureParam);

      var result = newProcedureParam.ParameterName;
      var compare = "Name";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    /// <summary>A Create constructor.</summary>
    private static void ConstructorParam()
    {
      var methodName = "ConstructorParam()";

      // Test Method
      var parameterName = "Name";
      var dataTypeID = (int)SqlDbType.NVarChar;
      var size = 60;
      var value = "Name Value";
      var procedureParam = new LJCProcedureParameter(parameterName, dataTypeID
        , size, value);

      var result = procedureParam.ParameterName;
      var compare = "Name";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Methods

    // The object string identifier.
    private static void ToStringMethod()
    {
      var methodName = "ToStringMethod()";

      var parameterName = "Name";
      var dataTypeID = (int)SqlDbType.NVarChar;
      var size = 60;
      var value = "Name Value";
      var procedureParam = new LJCProcedureParameter(parameterName, dataTypeID
        , size, value);

      // Test Method
      var result = procedureParam.ToString();
      var compare = "Name";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Creates and returns a clone of the object.
    private static void Clone()
    {
      var methodName = "Clone()";

      var parameterName = "Name";
      var dataTypeID = (int)SqlDbType.NVarChar;
      var size = 60;
      var value = "Name Value";
      var procedureParam = new LJCProcedureParameter(parameterName, dataTypeID
        , size, value);

      // Test Method
      var procedureValue = procedureParam.Clone();
      var result = "";
      if (procedureValue != null)
      {
        result = procedureValue.ParameterName;
      }
      var compare = "Name";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Provides the default Sort functionality.
    private static void CompareTo()
    {
      var methodName = "CompareTo()";

      var parameterName = "Name";
      var dataTypeID = (int)SqlDbType.NVarChar;
      var size = 60;
      var value = "Name Value";
      var procedureParam = new LJCProcedureParameter(parameterName, dataTypeID
        , size, value);
      var compareTo = new LJCProcedureParameter(parameterName, dataTypeID
        , size, value);

      // Test Method
      var compareValue = procedureParam.CompareTo(compareTo);
      var result = compareValue.ToString();
      var compare = LJCNetString.CompareEqual.ToString();
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
