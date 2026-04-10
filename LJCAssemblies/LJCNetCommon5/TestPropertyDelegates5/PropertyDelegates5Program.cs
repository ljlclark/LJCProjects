// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// PropertyDelegates5Program.cs
using LJCNetCommon5;
using System.Net.Http.Headers;
using System.Reflection;

namespace TestPropertyDelegates5
{
  // The entry class.
  internal class PropertyDelegates5Program
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCPropertyDelegates");
      Console.WriteLine();
      Console.WriteLine("*** LJCPropertyDelegates ***");

      // Collection Methods
      Add();

      LJCSearchName();
      LJCCreateDelegate();
    }

    #region Collection Methods

    // Creates and adds a PropertyDelegate object to the collection.
    private static void Add()
    {
      // The collection of delegate objects.
      var delegates = new LJCPropertyDelegates();

      var result = "";
      var testData = new TestData
      {
        StringValue = "Test"
      };
      var testType = testData.GetType();
      var propertyInfo = testType.GetProperty("StringValue");
      if (propertyInfo != null)
      {
        // Test Method
        delegates.Add(propertyInfo);

        var findDelegate = delegates.LJCSearchName("StringValue");
        if (findDelegate != null)
        {
          result = findDelegate.PropertyName;
        }
      }
      var compare = "StringValue";
      TestCommon?.Write("Add()", result, compare);
    }
    #endregion

    // Returns the PropertyDelegate object if found in the list.
    private static void LJCSearchName()
    {
      // The collection of delegate objects.
      var delegates = new LJCPropertyDelegates();

      var result = "";
      var testData = new TestData
      {
        StringValue = "Test"
      };
      var testType = testData.GetType();
      var propertyInfo = testType.GetProperty("StringValue");
      if (propertyInfo != null)
      {
        delegates.Add(propertyInfo);

        // Test Method
        var findDelegate = delegates.LJCSearchName("StringValue");
        if (findDelegate != null)
        {
          result = findDelegate.PropertyName;
        }
      }
      var compare = "StringValue";
      TestCommon?.Write("Add()", result, compare);
    }

    // Creates and returns the delegate for the named property.
    private static void LJCCreateDelegate()
    {
      var result = "";
      var testData = new TestData
      {
        StringValue = "Test"
      };
      var testType = testData.GetType();
      var propertyInfo = testType.GetProperty("StringValue");
      if (propertyInfo != null)
      {
        // Test Method
        Func<object, object>? delegateValue;
        delegateValue = LJCPropertyDelegates.LJCCreateDelegate(propertyInfo);
        if (delegateValue != null)
        {
          var value = delegateValue(testData);
          result = LJC.GetString(value);
        }
      }
      var compare = "Test";
      TestCommon?.Write("LJCCreateDelegate", result, compare);
    }

    #region Properties

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }

  public class TestData
  {
    public string? StringValue { get; set; }
    public bool BoolValue { get; set; }
    public byte ByteValue { get; set; }
  }
}
