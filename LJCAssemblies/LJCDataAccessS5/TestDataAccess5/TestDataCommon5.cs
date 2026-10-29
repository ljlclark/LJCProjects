// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDataCommon5.cs
using LJCDataAccess5;
using LJCNetCommon5;

namespace TestDataAccess5
{
  // Provides LJCDataCommon test methods.
  internal class TestDataCommon
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    public TestDataCommon()
    {
      TestCommon = new LJCTestCommon("TestDataCommon");
      Run();
    }

    // Run the tests.
    private void Run()
    {
      // Data Access Test Methods
      SetTableMapping();
      SetTableMappingMySql();

      // Data Conversion Test Methods
      GetDbDate();
      GetDbDateTime();
      GetDbDateString();
      GetMinDateTime();
      GetMinUIDateTimeString();
    }
    #endregion

    #region Data Access Test Methods

    // Sets the data adapter table mappings.
    private void SetTableMapping()
    {
      var methodName = "SetTableMapping()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Sets the data adapter table mappings.
    private void SetTableMappingMySql()
    {
      var methodName = "SetTableMappingMySql()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Data Conversion Test Methods

    // Converts the date string to a DateTime value.
    private void GetDbDate()
    {
      var methodName = "GetDbDate()";

      // Test Method
      string dateText = "";
      var dateTime = LJCDataCommon.GetDbDate(dateText);
      var result = dateTime.ToString();
      var compare = "1/1/1753 12:00:00 AM";
      TestCommon?.Write($"{methodName}1", result, compare);

      // With or without time.
      //dateText = "1492/1/25 09:00:00 AM";
      // Or
      dateText = "1/25/1492 09:00:00 AM";
      dateTime = LJCDataCommon.GetDbDate(dateText);
      result = dateTime.ToString();
      compare = "1/25/1492 12:00:00 AM";
      TestCommon?.Write($"{methodName}2", result, compare);
    }

    // Converts the date/time string to a DateTime value.
    private void GetDbDateTime()
    {
      var methodName = "GetDbDateTime()";

      // Test Method
      var dateText = "";
      var connectionType = ConnectionType.SqlServer;
      var dateTime = LJCDataCommon.GetDbDateTime(dateText, connectionType);
      var result = dateTime.ToString();
      var compare = "1/1/1753 12:00:00 AM";
      TestCommon?.Write($"{methodName}1", result, compare);

      dateText = "1492/1/25";
      connectionType = ConnectionType.SqlServer;
      dateTime = LJCDataCommon.GetDbDateTime(dateText, connectionType);
      result = dateTime.ToString();
      compare = "1/25/1492 12:00:00 AM";
      TestCommon?.Write($"{methodName}2", result, compare);

      dateText = "2025/1/25 09:01:00 AM";
      connectionType = ConnectionType.SqlServer;
      dateTime = LJCDataCommon.GetDbDateTime(dateText, connectionType);
      result = dateTime.ToString();
      compare = "1/25/2025 9:01:00 AM";
      TestCommon?.Write($"{methodName}3", result, compare);
    }

    // Formats the DateTime value to a date string in database format.
    private void GetDbDateString()
    {
      var methodName = "GetDbDateString()";

      // Test Method
      var dateTime = new DateTime(2025, 12, 25, 9, 30, 0);
      var connectionType = ConnectionType.SqlServer;
      var result = LJCDataCommon.GetDbDateString(dateTime
        , connectionType);
      var compare = "2025/12/25";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Get the minimum date/time value.
    private void GetMinDateTime()
    {
      var methodName = "GetMinDateTime()";

      var connectionType = ConnectionType.SqlServer;
      var dateTime = LJCDataCommon.GetMinDateTime(connectionType);
      var result = dateTime.ToString();
      var compare = "1/1/1753 12:00:00 AM";
      TestCommon?.Write($"{methodName}1", result, compare);

      connectionType = ConnectionType.None;
      dateTime = LJCDataCommon.GetMinDateTime(connectionType);
      result = dateTime.ToString();
      compare = "1/1/0001 12:00:00 AM";
      TestCommon?.Write($"{methodName}2", result, compare);
    }

    // Get the minimum date/time string formatted for display.
    private void GetMinUIDateTimeString()
    {
      var methodName = "GetMinUIDateTimeString()";

      // Test Method
      var connectionType = ConnectionType.SqlServer;
      var result = LJCDataCommon.GetMinUIDateTimeString(connectionType);
      var compare = "1/1/1753 12:00:00 AM";
      TestCommon?.Write($"{methodName}1", result, compare);

      connectionType = ConnectionType.None;
      result = LJCDataCommon.GetMinUIDateTimeString(connectionType);
      compare = "1/1/0001 12:00:00 AM";
      TestCommon?.Write($"{methodName}2", result, compare);
    }
    #endregion
  }
}
