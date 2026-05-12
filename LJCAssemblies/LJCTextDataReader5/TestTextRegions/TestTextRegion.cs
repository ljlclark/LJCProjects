// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestTextRegions.cs
using LJCNetCommon5;
using LJCTextDataReader5;

namespace TestTextRegions5
{
  // The entry class.
  internal class TestTextRegion
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCTextRegion");
      Console.WriteLine();
      Console.WriteLine("*** LJCTextRegion ***");

      // Collection Methods
      Add();

      // Methods
      LJCHasRegions();
      LJCIsInRegion();
      LJCSplit();

      Console.WriteLine();
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }

    #region Collection Methods

    // Creates a TextRegion from the supplied values and adds it to the
    // collection.
    private static void Add()
    {
      var methodName = "Add()";

#pragma warning disable IDE0028 // Simplify collection initialization
      var textRegions = new LJCTextRegions();
#pragma warning restore IDE0028 // Simplify collection initialization

      // Test Method
      // Normally let LJCHasRegions() add the regions.
      // This is called for direct method testing.
      textRegions.Add(13, 27);

      var index = 20;
      var value = textRegions.LJCIsInRegion(index);
      var result = value.ToString();
      var compare = "True";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Methods

    // Identifies quoted regions and returns "True" if regions are found.
    private static void LJCHasRegions()
    {
      var methodName = "LJCHasRegions()";

      var text = "First Value, \"Second, Value\", Third Value";
      var textRegions = new LJCTextRegions();

      // Test Method
      var value = textRegions.LJCHasRegions(text);
      var result = value.ToString();
      var compare = "True";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Determines if a delimiter is in a text region.
    private static void LJCIsInRegion()
    {
      var methodName = "LJCIsInRegion()";

      var text = "First Value, \"Second, Value\", Third Value";
      var textRegions = new LJCTextRegions();
      var hasRegions = textRegions.LJCHasRegions(text);

      // Test Method
      if (hasRegions)
      {
        var index = 20;
        var value = textRegions.LJCIsInRegion(index);
        var result = value.ToString();
        var compare = "True";
        TestCommon?.Write($"{methodName}", result, compare);
      }
    }

    // Splits a line of text on the delimiters not enclosed in text regions.
    private static void LJCSplit()
    {
      var methodName = "LJCSplit()";

      var text = "First Value, \"Second, Value\", Third Value";
      var textRegions = new LJCTextRegions();
      var value = textRegions.LJCHasRegions(text);

      // Test Method
      var values = textRegions.LJCSplit(text);
      var result = "";
      if (LJC.HasElements(values))
      {
        result = values[1];
      }
      var compare = "Second, Value";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
