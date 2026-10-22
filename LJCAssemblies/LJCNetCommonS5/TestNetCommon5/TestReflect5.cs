// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestReflect5.cs
using LJCNetCommon5;
using System.Reflection;

namespace TestNetCommon5
{
  // Provides LJCReflect test methods.
  internal class TestReflect
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; } = null!;
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    public TestReflect()
    {
      TestCommon = new LJCTestCommon("TestReflect");
      Run();
    }

    // Runs the test methods.
    private void Run()
    {
      // Constructor Test Methods
      Constructor();
      SetSource();

      // Method Test Methods
      GetPropertyInfo();
      GetPropertyNames();
      GetPropertyType();
      HasProperty();

      // Value Test Methods
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

      // Set Test Methods
      SetPropertyValue();
      SetValue();
    }
    #endregion

    #region Constructor Test Methods

    // Instantiates an instance of the class.
    private void Constructor()
    {
      var test = new { Name = "Name", Value = "Value" };
      var reflect = new LJCReflect(test);
      var count = 0;
      PropertyInfo[] infos = reflect.PropertyInfos;
      if (LJC.HasArrayElements(infos))
      {
        count = infos.Length;
      }
      var result = count.ToString();
      var compare = "2";
      TestCommon.Write("Constructor()", result, compare);
    }

    // Sets the source object and type values.
    private void SetSource()
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
      TestCommon.Write("SetSource()", result, compare);
    }
    #endregion

    #region Method Test Methods

    // Gets the cached PropertyInfo value.
    private void GetPropertyInfo()
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
      TestCommon.Write("GetPropertyInfo()", result, compare);
    }

    // Gets the cached PropertyInfo value.
    private void GetPropertyNames()
    {
      var test = new { Name = "Name", Value = "Value" };
      var reflect = new LJCReflect(test);
      var names = reflect.GetPropertyNames();
      string result = null;
      if (LJC.HasListItems(names))
      {
        result = string.Join(", ", names);
      }
      var compare = "Name, Value";
      TestCommon.Write("GetPropertyNames()", result, compare);
    }

    // Get the property type.
    private void GetPropertyType()
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
      TestCommon.Write("GetPropertyType()", result, compare);
    }

    // Checks if a property exists.
    private void HasProperty()
    {
      var person = new ReflectPerson()
      {
        FirstName = "First",
        LastName = "Last",
      };
      var reflect = new LJCReflect(person);
      var value = reflect.HasProperty("FirstName");
      var result = value.ToString();
      var compare = "True";
      TestCommon.Write("HasProperty()", result, compare);
    }
    #endregion

    #region Value Test Methods

    // Gets the property value as a boolean.
    private void GetBoolean()
    {
      var test = new { Value = true };
      var reflect = new LJCReflect(test);
      var value = reflect.GetBoolean("Value");
      var result = value.ToString();
      var compare = "True";
      TestCommon.Write("GetBoolean()", result, compare);
    }

    // Gets the property value as a byte.
    private void GetByte()
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
      TestCommon.Write("GetByte()", result, compare);
    }

    // Gets the property value as a char.
    private void GetChar()
    {
      var test = new { Value = 'C' };
      var reflect = new LJCReflect(test);
      var value = reflect.GetChar("Value");
      var result = value.ToString();
      var compare = "C";
      TestCommon.Write("GetChar()", result, compare);
    }

    // Gets the property value as a DateTime value.
    private void GetDateTime()
    {
      var dateValue = new TestDate
      {
        Holiday = new DateTime(2026, 12, 25)
      };
      var reflect = new LJCReflect(dateValue);
      var value = reflect.GetDateTime("Holiday");
      var result = value.ToShortDateString();
      var compare = "12/25/2026";
      TestCommon.Write("GetDateTime()", result, compare);
    }

    // Gets the property value as a DB date/time string.
    private void GetDbDateString()
    {
      var dateValue = new TestDate
      {
        Holiday = new DateTime(2026, 12, 25)
      };
      var reflect = new LJCReflect(dateValue);
      var result = reflect.GetDbDateString("Holiday");
      var compare = "'2026/12/25 00:00:00'";
      TestCommon.Write("GetDbDateString()", result, compare);
    }

    // Gets the property value as a decimal.
    private void GetDecimal()
    {
      var test = new { Value = 3.14 };
      var reflect = new LJCReflect(test);
      var value = reflect.GetDouble("Value");
      var result = value.ToString();
      var compare = "3.14";
      TestCommon.Write("GetDecimal()", result, compare);
    }

    // Gets the property value as a double.
    private void GetDouble()
    {
      var test = new { Value = 3.14 };
      var reflect = new LJCReflect(test);
      var value = reflect.GetDouble("Value");
      var result = value.ToString();
      var compare = "3.14";
      TestCommon.Write("GetDouble()", result, compare);
    }

    // Gets the property value as a short.
    private void GetInt16()
    {
      var test = new { Value = (short)3 };
      var reflect = new LJCReflect(test);
      var value = reflect.GetInt16("Value");
      var result = value.ToString();
      var compare = "3";
      TestCommon.Write("GetInt16()", result, compare);
    }

    // Gets the property value as an integer.
    private void GetInt32()
    {
      var test = new { Value = 3 };
      var reflect = new LJCReflect(test);
      var value = reflect.GetInt32("Value");
      var result = value.ToString();
      var compare = "3";
      TestCommon.Write("GetInt32()", result, compare);
    }

    // Gets the property value as a long.
    private void GetInt64()
    {
      var test = new { Value = (long)3 };
      var reflect = new LJCReflect(test);
      var value = reflect.GetInt64("Value");
      var result = value.ToString();
      var compare = "3";
      TestCommon.Write("GetInt64()", result, compare);
    }

    // Gets the property value as a float.
    private void GetSingle()
    {
      // Must use "f" for float (single) or it defaults to double.
      var test = new { Value = 3.14f };
      var reflect = new LJCReflect(test);
      var value = reflect.GetSingle("Value");
      var result = value.ToString();
      var compare = "3.14";
      TestCommon.Write("GetSingle()", result, compare);
    }

    // Gets the property value as a string.
    private void GetString()
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
      TestCommon.Write("GetString()", result, compare);
    }

    // Gets the property value as an object using a delegate.
    private void GetValue()
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
      TestCommon.Write("GetValue()", result, compare);
    }

    // Gets the property value as an object using reflection.
    private void GetValueReflect()
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
      TestCommon.Write("GetValueReflect()", result, compare);
    }
    #endregion

    #region Set Test Methods

    // Sets the property value based on value type.
    private void SetPropertyValue()
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
      TestCommon.Write("SetPropertyValue()", result, compare);
    }

    // Sets the property value.
    private void SetValue()
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
      TestCommon.Write("SetValue()", result, compare);
    }
    #endregion
  }

  /// <summary>
  /// 
  /// </summary>
  public class TestDate
  {
    /// <summary></summary>
    public DateTime Holiday { get; set; }
  }

  /// <summary>
  /// 
  /// </summary>
  public class ReflectPerson
  {
    /// <summary></summary>
    public string? FirstName { get; set; }

    /// <summary></summary>
    public string? LastName { get; set; }
  }

  /// <summary>
  /// 
  /// </summary>
  public class Persons : List<ReflectPerson>
  {
    // Creates and adds the object to the collection.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <returns></returns>
    public ReflectPerson Add(string firstName, string lastName)
    {
      var retPerson = new ReflectPerson
      {
        FirstName = firstName,
        LastName = lastName,
      };
      Add(retPerson);
      return retPerson;
    }
  }
}
