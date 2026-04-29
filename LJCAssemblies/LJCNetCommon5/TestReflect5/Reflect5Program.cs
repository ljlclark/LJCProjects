// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Reflect5Program.cs
using LJCNetCommon5;
using System.Reflection;

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

      Console.WriteLine();
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }

    #region Constructor Methods

    // Instantiates an instance of the class.
    private static void Constructor()
    {
      var test = new { Name = "Name", Value = "Value" };
      var reflect = new LJCReflect(test);
      var count = 0;
      PropertyInfo[] infos = reflect.PropertyInfos;
      if (LJC.HasElements(infos))
      {
        count = infos.Count();
      }
      var result = count.ToString();
      var compare = "2";
      TestCommon?.Write("Constructor()", result, compare);
    }

    // Sets the source object and type values.
    private static void SetSource()
    {
      var test1 = new { Name = "Name", Value = "Value" };
      var reflect = new LJCReflect(test1);
      var test2 = new { Name = "John", Value = "Doe" };
      reflect.SetSource(test2);
      object value = null;
      if (reflect.HasProperty("Name"))
      {
        value = reflect.GetValueReflect("Name");
      }
      var result = value?.ToString();
      var compare = "John";
      TestCommon?.Write("SetSource()", result, compare);
    }
    #endregion

    #region Methods

    // Gets the cached PropertyInfo value.
    private static void GetPropertyInfo()
    {
      var test = new { Name = "Name", Value = "Value" };
      var reflect = new LJCReflect(test);
      PropertyInfo info = null;
      if (reflect.HasProperty("Name"))
      {
        info = reflect.GetPropertyInfo("Name");
      }
      var value = info?.PropertyType;
      var result = value?.Name;
      var compare = "String";
      TestCommon?.Write("GetPropertyInfo()", result, compare);
    }

    // Gets the cached PropertyInfo value.
    private static void GetPropertyNames()
    {
      var test = new { Name = "Name", Value = "Value" };
      var reflect = new LJCReflect(test);
      var names = reflect.GetPropertyNames();
      string result = null;
      if (LJC.HasItems(names))
      {
        result = string.Join(", ", names);
      }
      var compare = "Name, Value";
      TestCommon?.Write("GetPropertyNames()", result, compare);
    }

    // Get the property type.
    private static void GetPropertyType()
    {
      var item = LJC.TextToBytes("C");
      var test = new { Value = item[0] };
      var reflect = new LJCReflect(test);
      Type value = null;
      if (reflect.HasProperty("Value"))
      {
        value = reflect.GetPropertyType("Value");
      }
      var result = value?.Name;
      var compare = "Byte";
      TestCommon?.Write("GetPropertyType()", result, compare);
    }

    // Checks if a property exists.
    private static void HasProperty()
    {
      var person = new Person()
      {
        FirstName = "First",
        LastName = "Last",
      };
      var reflect = new LJCReflect(person);
      var value = reflect.HasProperty("FirstName");
      var result = value.ToString();
      var compare = "True";
      TestCommon?.Write("HasProperty()", result, compare);
    }
    #endregion

    #region Value Methods

    // Gets the property value as a boolean.
    private static void GetBoolean()
    {
      var test = new { Value = true };
      var reflect = new LJCReflect(test);
      var value = reflect.GetBoolean("Value");
      var result = value.ToString();
      var compare = "True";
      TestCommon?.Write("GetBoolean()", result, compare);
    }

    // Gets the property value as a byte.
    private static void GetByte()
    {
      var item = LJC.TextToBytes("C");
      var test = new { Value = item[0] };
      var reflect = new LJCReflect(test);
      Byte? value = null;
      if (reflect.HasProperty("Value"))
      {
        value = reflect.GetByte("Value");
      }
      var result = value.ToString();
      var compare = "67";
      TestCommon?.Write("GetByte()", result, compare);
    }

    // Gets the property value as a char.
    private static void GetChar()
    {
      var test = new { Value = 'C' };
      var reflect = new LJCReflect(test);
      var value = reflect.GetChar("Value");
      var result = value.ToString();
      var compare = "C";
      TestCommon?.Write("GetChar()", result, compare);
    }

    // Gets the property value as a DateTime value.
    private static void GetDateTime()
    {
      var dateValue = new TestDate
      {
        Holiday = new DateTime(2026, 12, 25)
      };
      var reflect = new LJCReflect(dateValue);
      var value = reflect.GetDateTime("Holiday");
      var result = value.ToShortDateString();
      var compare = "12/25/2026";
      TestCommon?.Write("GetDateTime()", result, compare);
    }

    // Gets the property value as a DB date/time string.
    private static void GetDbDateString()
    {
      var dateValue = new TestDate
      {
        Holiday = new DateTime(2026, 12, 25)
      };
      var reflect = new LJCReflect(dateValue);
      var result = reflect.GetDbDateString("Holiday");
      var compare = "'2026/12/25 00:00:00'";
      TestCommon?.Write("GetDbDateString()", result, compare);
    }

    // Gets the property value as a decimal.
    private static void GetDecimal()
    {
      var test = new { Value = 3.14 };
      var reflect = new LJCReflect(test);
      var value = reflect.GetDouble("Value");
      var result = value.ToString();
      var compare = "3.14";
      TestCommon?.Write("GetDecimal()", result, compare);
    }

    // Gets the property value as a double.
    private static void GetDouble()
    {
      var test = new { Value = 3.14 };
      var reflect = new LJCReflect(test);
      var value = reflect.GetDouble("Value");
      var result = value.ToString();
      var compare = "3.14";
      TestCommon?.Write("GetDouble()", result, compare);
    }

    // Gets the property value as a short.
    private static void GetInt16()
    {
      var test = new { Value = (short)3 };
      var reflect = new LJCReflect(test);
      var value = reflect.GetInt16("Value");
      var result = value.ToString();
      var compare = "3";
      TestCommon?.Write("GetInt16()", result, compare);
    }

    // Gets the property value as an integer.
    private static void GetInt32()
    {
      var test = new { Value = 3 };
      var reflect = new LJCReflect(test);
      var value = reflect.GetInt32("Value");
      var result = value.ToString();
      var compare = "3";
      TestCommon?.Write("GetInt32()", result, compare);
    }

    // Gets the property value as a long.
    private static void GetInt64()
    {
      var test = new { Value = (long)3 };
      var reflect = new LJCReflect(test);
      var value = reflect.GetInt64("Value");
      var result = value.ToString();
      var compare = "3";
      TestCommon?.Write("GetInt64()", result, compare);
    }

    // Gets the property value as a float.
    private static void GetSingle()
    {
      // Must use "f" for float (single) or it defaults to double.
      var test = new { Value = 3.14f };
      var reflect = new LJCReflect(test);
      var value = reflect.GetSingle("Value");
      var result = value.ToString();
      var compare = "3.14";
      TestCommon?.Write("GetSingle()", result, compare);
    }

    // Gets the property value as a string.
    private static void GetString()
    {
      var persons = new Persons
      {
        { "FirstOne", "LastOne" },
        { "FirstTwo", "LastTwo" },
      };

      // Initialize with a representative object.
      var reflect = new LJCReflect(persons[0]);

      // Demonstrate loop.
      string result = "";
      foreach (var person in persons)
      {
        // Use SetSource to change the data object.
        reflect.SetSource(person);
        result = reflect.GetString("LastName");
      }

      // Use only the last result.
      var compare = "LastTwo";
      TestCommon?.Write("GetString()", result, compare);
    }

    // Gets the property value as an object using a delegate.
    private static void GetValue()
    {
      var persons = new Persons
      {
        { "FirstOne", "LastOne" },
        { "FirstTwo", "LastTwo" },
      };

      // Initialize with a representative object.
      var reflect = new LJCReflect(persons[0]);

      // Demonstrate loop.
      string result = "";
      foreach (var person in persons)
      {
        // Use SetSource to change the data object.
        reflect.SetSource(person);
        // Get value using a property delegate.
        var value = reflect.GetValue("LastName");
        result = value?.ToString();
      }

      // Use only the last result.
      var compare = "LastTwo";
      TestCommon?.Write("GetValue()", result, compare);
    }

    // Gets the property value as an object using reflection.
    private static void GetValueReflect()
    {
      var persons = new Persons
      {
        { "FirstOne", "LastOne" },
        { "FirstTwo", "LastTwo" },
      };

      // Initialize with a representative object.
      var reflect = new LJCReflect(persons[0]);

      // Demonstrate loop.
      string result = "";
      foreach (var person in persons)
      {
        // Use SetSource to change the data object.
        reflect.SetSource(person);
        // Get value without using a property delegate.
        var value = reflect.GetValueReflect("LastName");
        result = value?.ToString();
      }

      // Use only the last result.
      var compare = "LastTwo";
      TestCommon?.Write("GetValueReflect()", result, compare);
    }
    #endregion

    #region Set Methods

    // Sets the property value based on value type.
    private static void SetPropertyValue()
    {
      var persons = new Persons
      {
        { "FirstOne", "LastOne" },
        { "FirstTwo", "LastTwo" },
      };

      // Initialize with a representative object.
      var reflect = new LJCReflect(persons[0]);

      reflect.SetPropertyValue("FirstName", "FirstChanged");
      var result = reflect.GetString("FirstName");
      var compare = "FirstChanged";
      TestCommon?.Write("SetPropertyValue()", result, compare);
    }

    // Sets the property value.
    private static void SetValue()
    {
      var persons = new Persons
      {
        { "FirstOne", "LastOne" },
        { "FirstTwo", "LastTwo" },
      };

      // Initialize with a representative object.
      var reflect = new LJCReflect(persons[0]);

      reflect.SetValue("FirstName", "FirstChanged");
      var result = reflect.GetString("FirstName");
      var compare = "FirstChanged";
      TestCommon?.Write("SetValue()", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }

  public class TestDate
  {
    public DateTime Holiday { get; set; }
  }

  public class Person
  {
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
  }

  public class Persons : List<Person>
  {
    // Creates and adds the object to the collection.
    public Person Add(string firstName, string lastName)
    {
      var retPerson = new Person
      {
        FirstName = firstName,
        LastName = lastName,
      };
      Add(retPerson);
      return retPerson;
    }
  }
}
