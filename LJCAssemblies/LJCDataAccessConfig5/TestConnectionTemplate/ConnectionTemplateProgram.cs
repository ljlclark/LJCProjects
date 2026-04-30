// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestConnectionTemplate.cs
using LJCDataAccessConfig5;
using LJCNetCommon5;

namespace TestConnectionTemplate5
{
  // The entry class.
  internal class ConnectionTemplateProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCConnectionTemplate");
      Console.WriteLine();
      Console.WriteLine("*** LJCConnectionTemplate ***");

      // Data Class Methods
      Clone();
      ToStringMethod();
      CompareTo();

      Console.WriteLine();
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }

    #region Data Class Methods

    // Creates and returns a clone of the object.
    private static void Clone()
    {
      var methodName = "Clone()";

      var connectionTemplate = new LJCConnectionTemplate()
      {
        Name = "TestTemplate",
        Template = "Data Source={DbServer}; Initial Catalog={Database};"
         + " Integrated Security=True",
      };

      // Test Method
      var cloneTemplate = connectionTemplate.Clone();

      var result = "";
      if (cloneTemplate != null)
      {
        result = cloneTemplate.Name;
      }
      var compare = "TestTemplate";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // The object string value.
    private static void ToStringMethod()
    {
      var methodName = "ToStringMethod()";

      var connectionTemplate = new LJCConnectionTemplate()
      {
        Name = "TestTemplate",
        Template = "Data Source={DbServer}; Initial Catalog={Database};"
         + " Integrated Security=True",
      };

      // Test Method
      var result = connectionTemplate.ToString();
      var compare = "TestTemplate";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Provides the default Sort functionality.
    private static void CompareTo()
    {
      var methodName = "CompareTo()";

      var connectionTemplate = new LJCConnectionTemplate()
      {
        Name = "TestTemplate",
        Template = "Data Source={DbServer}; Initial Catalog={Database};"
         + " Integrated Security=True",
      };
      var other = new LJCConnectionTemplate()
      {
        Name = "TestTemplate",
        Template = "Data Source={DbServer}; Initial Catalog={Database};"
         + " Integrated Security=True",
      };

      // Test Method
      var value = connectionTemplate.CompareTo(other);
      var result = value.ToString();
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
