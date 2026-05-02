// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataCommonProgram.cs
using LJCDataAccess5;
using LJCNetCommon5;

namespace TestDataCommon5
{
  // The entry class.
  internal class DataCommonProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCDataCommon");
      Console.WriteLine();
      Console.WriteLine("*** LJCDataCommon ***");

      // Data Access Methods
      SetTableMapping();
      SetTableMappingMySql();

      // Data Conversion Methods
      GetDbDate();
      GetDbDateTime();
      GetDbDateString();
      GetDbDateTimeString();
      GetMinDateTime();
      GetMinUIDateTimeString();
      GetUIDateString();
      GetUIDateTimeString();
      GetUITimeString();

      Console.WriteLine();
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }

    #region Data Access Methods

    // Sets the data adapter table mappings.
    private static void SetTableMapping()
    {
      var methodName = "SetTableMapping()";

      var result = "Not Implemented";
      var compare = "";
      //TestCommon?.Write($"{methodName}", result, compare);
    }

    // Sets the data adapter table mappings.
    private static void SetTableMappingMySql()
    {
      var methodName = "SetTableMappingMySql()";

      var result = "Not Implemented";
      var compare = "";
      //TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Data Conversion Methods

    // Converts the date string to a DateTime value.
    private static void GetDbDate()
    {
      var methodName = "GetDbDate()";

      // Test Method
      string dateText = "";
      var dateTime = LJCDataCommon.GetDbDate(dateText);
      var result = dateTime.ToString();
      var compare = "1/1/1753 12:00:00 AM";
      TestCommon?.Write($"{methodName}1", result, compare);

      // With or without time.
      dateText = "1492/1/25 09:00:00 AM";
      // Or
      dateText = "1/25/1492 09:00:00 AM";
      dateTime = LJCDataCommon.GetDbDate(dateText);
      result = dateTime.ToString();
      compare = "1/25/1492 12:00:00 AM";
      TestCommon?.Write($"{methodName}2", result, compare);
    }

    // Converts the date/time string to a DateTime value.
    private static void GetDbDateTime()
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
    private static void GetDbDateString()
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

    // Formats the DateTime value to a date/time string in database format.
    private static void GetDbDateTimeString()
    {
      var methodName = "GetDbDateTimeString()";

      // Test Method
      var dateTime = new DateTime(2025, 12, 25, 21, 30, 0);
      var result = LJCDataCommon.GetDbDateTimeString(dateTime);
      var compare = "2025/12/25 21:30:00";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Get the minimum date/time value.
    private static void GetMinDateTime()
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
    private static void GetMinUIDateTimeString()
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

    // Format the date value for display.
    private static void GetUIDateString()
    {
      var methodName = "GetUIDateString()";

      var dateTime = new DateTime(2025, 12, 25, 12, 30, 10);

      // Test Method
      var result = LJCDataCommon.GetUIDateString(dateTime);
      var compare = "12/25/2025";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Format the date/time value for display.
    private static void GetUIDateTimeString()
    {
      var methodName = "GetUIDateTimeString()";

      var dateTime = new DateTime(2025, 12, 25, 12, 30, 10);

      // Test Method
      var result = LJCDataCommon.GetUIDateTimeString(dateTime);
      var compare = "12/25/2025 12:30:10";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Format the date/time to time for display.
    private static void GetUITimeString()
    {
      var methodName = "GetUITimeString()";

      var dateTime = new DateTime(2025, 12, 25, 12, 30, 10);

      // Test Method
      var result = LJCDataCommon.GetUITimeString(dateTime);
      var compare = "12:30 PM";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
