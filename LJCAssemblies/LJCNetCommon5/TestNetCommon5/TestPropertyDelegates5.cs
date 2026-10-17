// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestPropertyDelegates5.cs
using LJCNetCommon5;

namespace TestNetCommon5
{
  // Provides the LJCPropertyDelegate test methods.
  internal class TestPropertyDelegates
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; } = null!;
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/TestPropertyDelegates.xml'
    ///  path='members/Constructor/*'/>
    public TestPropertyDelegates()
    {
      TestCommon = new LJCTestCommon("TestPropertyDelegates");
      Run();
    }

    // Runs the test methods.
    private void Run()
    {
      // Collection Methods
      Add();

      LJCSearchName();
      LJCCreateDelegate();
    }
    #endregion
    #region Collection Methods

    // Creates and adds a PropertyDelegate object to the collection.
    private void Add()
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
      TestCommon.Write("Add()", result, compare);
    }
    #endregion

    // Returns the PropertyDelegate object if found in the list.
    private void LJCSearchName()
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
      TestCommon.Write("Add()", result, compare);
    }

    // Creates and returns the delegate for the named property.
    private void LJCCreateDelegate()
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
      TestCommon.Write("LJCCreateDelegate", result, compare);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public class TestData
  {
    /// <summary></summary>
    public string? StringValue { get; set; }

    /// <summary></summary>
    public bool BoolValue { get; set; }

    /// <summary></summary>
    public byte ByteValue { get; set; }
  }
}
