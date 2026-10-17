// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestKeyItem5.cs
using LJCNetCommon5;

namespace TestNetCommon5
{
  // Provides the LJCKeyItem test methods.
  internal class TestKeyItem
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; } = null!;
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/TestKeyItem.xml'
    ///  path='members/Constructor/*'/>
    public TestKeyItem()
    {
      TestCommon = new LJCTestCommon("TestKeyItem");
      Run();
    }

    // Runs the test methods.
    private void Run()
    {
      // Constructor Methods
      ParmConstructor();

      // Data Methods
      Clone();
      ToStringMethod();

      // Search and Sort Methods
      CompareTo();
    }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    private void ParmConstructor()
    {
      // Test Method
      var keyItem = new LJCKeyItem("ID", 1, "The primary key.");

      var result = keyItem.PropertyName;
      var compare = "ID";
      TestCommon.Write("ParmConstructor()", result, compare);
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    private void Clone()
    {
      var keyItem = new LJCKeyItem("ID", 1, "The primary key.");

      // Test Method


      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write("Clone()", result, compare);
    }

    // The object string identifier.
    private void ToStringMethod()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write("ToStringMethod()", result, compare);
    }
    #endregion

    #region Search and Sort Methods

    // Provides the default Sort functionality.
    private void CompareTo()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write("CompareTo()", result, compare);
    }
    #endregion
  }
}
