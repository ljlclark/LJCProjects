// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestProcedureParam5.cs
using LJCDataAccess5;
using LJCNetCommon5;
using System.Data;

namespace TestDataAccess5
{
  // Provides LJCProcedureParam test methods.
  internal class TestProcedureParam
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    public TestProcedureParam()
    {
      TestCommon = new LJCTestCommon("TestProcedureParam");
      Run();
    }

    // Run the tests.
    private void Run()
    {
      // Constructor Test Methods
      ConstructorCopy();
      ConstructorParam();

      // Method Test Methods
      ToStringMethod();
      Clone();
      CompareTo();
    }
    #endregion

    #region Constructor Test Methods

    // The Copy constructor.
    private void ConstructorCopy()
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
    private void ConstructorParam()
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

    #region Method Test Methods

    // The object string identifier.
    private void ToStringMethod()
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
    private void Clone()
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
    private void CompareTo()
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
  }
}
