// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Reflect5Program.cs
using LJCNetCommon5;

namespace TestReflect5
{
  // The entry class.
  internal class Reflect5Program
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCReflect");
      Console.WriteLine();
      Console.WriteLine("*** LJCReflect ***");

      // Constructor Methods
      Constructor();
      SetSource();

      // Methods
      GetPropertyInfo();
      GetPropertyNames();
      GetPropertyType();
      HasProperty();

      // Value Methods
      GetBoolean();
      GetByte();
      GetChar();
      GetDateTime();
      GetDbDateString();
      GetDecimal();
      GetDouble();
      GetInt16();
      GetInt32();
      GetInt64();
      GetSingle();
      GetString();
      GetValue();
      GetValueReflect();

      // Set Methods
      SetPropertyValue();
      SetValue();
    }

    #region Constructor Methods

    // Instantiates an instance of the class.
    private static void Constructor()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("Constructor()", result, compare);
    }

    // Sets the source object and type values.
    private static void SetSource()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("SetSource()", result, compare);
    }
    #endregion

    #region Methods

    // Gets the cached PropertyInfo value.
    private static void GetPropertyInfo()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetPropertyInfo()", result, compare);
    }

    // Gets the cached PropertyInfo value.
    private static void GetPropertyNames()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetPropertyNames()", result, compare);
    }

    // Get the property type.
    private static void GetPropertyType()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetPropertyType()", result, compare);
    }

    // Checks if a property exists.
    private static void HasProperty()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("HasProperty()", result, compare);
    }
    #endregion

    #region Value Methods

    // Gets the property value as a boolean.
    private static void GetBoolean()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetBoolean()", result, compare);
    }

    // Gets the property value as a byte.
    private static void GetByte()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetByte()", result, compare);
    }

    // Gets the property value as a char.
    private static void GetChar()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetChar()", result, compare);
    }

    // Gets the property value as a DateTime value.
    private static void GetDateTime()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetDateTime()", result, compare);
    }

    // Gets the property value as a DB date/time string.
    private static void GetDbDateString()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetDbDateString()", result, compare);
    }

    // Gets the property value as a decimal.
    private static void GetDecimal()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetDecimal()", result, compare);
    }

    // Gets the property value as a double.
    private static void GetDouble()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetDouble()", result, compare);
    }

    // Gets the property value as a short.
    private static void GetInt16()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetInt16()", result, compare);
    }

    // Gets the property value as an integer.
    private static void GetInt32()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetInt32()", result, compare);
    }

    // Gets the property value as a long.
    private static void GetInt64()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetInt64()", result, compare);
    }

    // Gets the property value as a float.
    private static void GetSingle()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetSingle()", result, compare);
    }

    // Gets the property value as a string.
    private static void GetString()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetString()", result, compare);
    }

    // Gets the property value as an object using a delegate.
    private static void GetValue()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetValue()", result, compare);
    }

    // Gets the property value as an object using reflection.
    private static void GetValueReflect()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetValueReflect()", result, compare);
    }
    #endregion

    #region Set Methods

    // Sets the property value based on value type.
    private static void SetPropertyValue()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("SetPropertyValue()", result, compare);
    }

    // Sets the property value.
    private static void SetValue()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("SetValue()", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
