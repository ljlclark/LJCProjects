// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCReflect.cs
using System.Reflection;

namespace LJCNetCommon5
{
  // Provides object property reflection capabilities. (D)
  /// <include path="members/LJCReflect/*" file="Doc/LJCReflect.xml"/>
  public class LJCReflect
  {
    #region Constructor Methods

    // Instantiates an instance of the class.
    /// <include path="members/LJCReflectC/*" file="Doc/LJCReflect.xml"/>
    public LJCReflect(object source)
    {
      mSource = source;
      mType = source.GetType();
      var bindingFlags = BindingFlags.Instance
        | BindingFlags.Public;
      PropertyInfos = mType.GetProperties(bindingFlags);
      mPropertyDelegates = [];
    }

    // Sets the source object and type values.
    /// <include path="members/SetSource/*" file="Doc/LJCReflect.xml"/>
    public void SetSource(object source)
    {
      mSource = source;
    }
    #endregion

    #region Methods

    // Gets the cached PropertyInfo value.
    /// <include path="members/GetPropertyInfo/*" file="Doc/LJCReflect.xml"/>
    public PropertyInfo? GetPropertyInfo(string propertyName)
    {
      PropertyInfo retValue = null;

      retValue = Array.Find(PropertyInfos, (x) => x.Name == propertyName);
      //if (null == retValue)
      //{
      //  var name = mType.Name;
      //	var text = $"{name} Property '{propertyName}' was not found"
      //		+ $" in object '{mSource.GetType().Name}'.";
      //	throw new ArgumentException(text);
      //}
      return retValue;
    }

    // Gets a list of the property names.
    /// <include path="members/GetPropertyNames/*" file="Doc/LJCReflect.xml"/>
    public List<string> GetPropertyNames()
    {
      List<string> retValue = [];

      if (PropertyInfos != null
        && PropertyInfos.Length > 0)
      {
        foreach (PropertyInfo propertyInfo in PropertyInfos)
        {
          retValue.Add(propertyInfo.Name);
        }
      }
      return retValue;
    }

    // Get the property type.
    /// <include path="members/GetPropertyType/*" file="Doc/LJCReflect.xml"/>
    public Type? GetPropertyType(string propertyName)
    {
      Type retVal = null;

      var info = GetPropertyInfo(propertyName);
      retVal = info?.PropertyType;
      return retVal;
    }

    // Checks if a property exists.
    /// <include path="members/HasProperty/*" file="Doc/LJCReflect.xml"/>
    public bool HasProperty(string propertyName)
    {
      bool retValue = true;

      var propertyInfo = GetPropertyInfo(propertyName);
      if (null == propertyInfo)
      {
        retValue = false;
      }
      return retValue;
    }
    #endregion

    #region Value Methods

    // Gets the property value as a boolean.
    /// <include path="members/GetBoolean/*" file="Doc/LJCReflect.xml"/>
    public bool GetBoolean(string propertyName)
    {
      bool retVal = default;

      var value = GetValue(propertyName);
      if (value != null
        && value.GetType() == typeof(bool))
      {
        retVal = (bool)value;
      }
      return retVal;
    }

    // Gets the property value as a byte.
    /// <include path="members/GetByte/*" file="Doc/LJCReflect.xml"/>
    public byte GetByte(string propertyName)
    {
      byte retVal = default;

      var value = GetValue(propertyName);
      if (value != null
        && value.GetType() == typeof(byte))
      {
        retVal = (byte)value;
      }
      return retVal;
    }

    // Gets the property value as a char.
    /// <include path="members/GetChar/*" file="Doc/LJCReflect.xml"/>
    public char GetChar(string propertyName)
    {
      char retVal = default;

      var value = GetValue(propertyName);
      if (value != null
        && value.GetType() == typeof(char))
      {
        retVal = (char)value;
      }
      return retVal;
    }

    // Gets the property value as a DateTime value.
    /// <include path="members/GetDateTime/*" file="Doc/LJCReflect.xml"/>
    public DateTime GetDateTime(string propertyName)
    {
      DateTime retVal = default;

      var value = GetValue(propertyName);
      if (value != null
        && value.GetType() == typeof(DateTime))
      {
        retVal = Convert.ToDateTime(value);
      }
      return retVal;
    }

    // Gets the property value as a DB date/time string.
    /// <include path="members/GetDbDateString/*" file="Doc/LJCReflect.xml"/>
    public string? GetDbDateString(string propertyName)
    {
      string retVal = default;

      if (LJC.HasValue(propertyName))
      {
        var type = GetPropertyType(propertyName);
        if (type == typeof(DateTime))
        {
          var dateTime = GetDateTime(propertyName);
          retVal = $"'{dateTime:yyyy/MM/dd HH:mm:ss}'";
        }
      }
      return retVal;
    }

    // Gets the property value as a decimal.
    /// <include path="members/GetDecimal/*" file="Doc/LJCReflect.xml"/>
    public decimal GetDecimal(string propertyName)
    {
      decimal retVal = default;

      var value = GetValue(propertyName);
      if (value != null
        && value.GetType() == typeof(decimal))
      {
        retVal = (decimal)value;
      }
      return retVal;
    }

    // Gets the property value as a double.
    /// <include path="members/GetDouble/*" file="Doc/LJCReflect.xml"/>
    public double GetDouble(string propertyName)
    {
      double retVal = default;

      var value = GetValue(propertyName);
      if (value != null
        && value.GetType() == typeof(double))
      {
        retVal = (double)value;
      }
      return retVal;
    }

    // Gets the property value as a short.
    /// <include path="members/GetInt16/*" file="Doc/LJCReflect.xml"/>
    public short GetInt16(string propertyName)
    {
      short retVal = default;

      var value = GetValue(propertyName);
      if (value != null
        && value.GetType() == typeof(short))
      {
        retVal = (short)value;
      }
      return retVal;
    }

    // Gets the property value as an integer.
    /// <include path="members/GetInt32/*" file="Doc/LJCReflect.xml"/>
    public int GetInt32(string propertyName)
    {
      int retVal = default;

      var value = GetValue(propertyName);
      if (value != null
        && value.GetType() == typeof(int))
      {
        retVal = (int)value;
      }
      return retVal;
    }

    // Gets the property value as a long.
    /// <include path="members/GetInt64/*" file="Doc/LJCReflect.xml"/>
    public long GetInt64(string propertyName)
    {
      long retVal = default;

      var value = GetValue(propertyName);
      if (value != null
        && value.GetType() == typeof(long))
      {
        retVal = (long)value;
      }
      return retVal;
    }

    // Gets the property value as a float.
    /// <include path="members/GetSingle/*" file="Doc/LJCReflect.xml"/>
    public float GetSingle(string propertyName)
    {
      float retVal = default;

      var value = GetValue(propertyName);
      if (value != null
        && value.GetType() == typeof(float))
      {
        retVal = (float)value;
      }
      return retVal;
    }

    // Gets the property value as a string.
    /// <include path="members/GetString/*" file="Doc/LJCReflect.xml"/>
    public string? GetString(string propertyName)
    {
      string retVal = null;

      var value = GetValue(propertyName);
      retVal = value?.ToString();
      return retVal;
    }

    // Gets the property value as an object using a delegate.
    /// <include path="members/GetValue/*" file="Doc/LJCReflect.xml"/>
    public object? GetValue(string? propertyName, bool throwError = true)
    {
      object retValue = null;

      if (LJC.HasValue(propertyName))
      {
        var propertyDelegate = mPropertyDelegates.LJCSearchName(propertyName);
        if (null == propertyDelegate)
        {
          var propertyInfo = GetPropertyInfo(propertyName);
          if (null == propertyInfo)
          {
            if (throwError)
            {
              var name = mType.Name;
              var text = $"{name} Property {propertyName} was not found.";
              throw new ArgumentException(text);
            }
          }
          else
          {
            propertyDelegate = mPropertyDelegates.Add(propertyInfo);
          }
        }

        if (propertyDelegate != null)
        {
          var getter = propertyDelegate.Value;
          if (getter != null)
          {
            retValue = getter(mSource);
          }
        }
      }
      return retValue;
    }

    // Gets the property value as an object using reflection.
    /// <include path="members/GetValueReflect/*" file="Doc/LJCReflect.xml"/>
    public object? GetValueReflect(string propertyName, bool throwError = true)
    {
      object retVal = null;

      if (LJC.HasValue(propertyName))
      {
        var propertyInfo = GetPropertyInfo(propertyName);
        if (null == propertyInfo)
        {
          if (throwError)
          {
            var name = mType.Name;
            var text = $"{name} Property {propertyName} was not found.";
            throw new ArgumentException(text);
          }
        }
        else
        {
          retVal = propertyInfo.GetValue(mSource, null);
        }
      }
      return retVal;
    }
    #endregion

    #region Set Methods

    // Sets the property value based on value type.
    /// <include path="members/SetPropertyValue/*" file="Doc/LJCReflect.xml"/>
    public void SetPropertyValue(string propertyName, object? value)
    {
      Type type;

      if (value != null
        && LJC.HasValue(value.ToString()))
      {
        PropertyInfo propertyInfo = GetPropertyInfo(propertyName);
        if (propertyInfo != null)
        {
          type = propertyInfo.PropertyType;
          while (true)
          {
            if (typeof(bool) == type
              || typeof(System.Nullable<bool>) == type)
            {
              try
              {
                value = Convert.ToBoolean(value);
              }
              catch
              {
                value = false;
              }
              break;
            }
            if (typeof(byte) == type)
            {
              value = Convert.ToByte(value);
              break;
            }
            if (typeof(char) == type)
            {
              value = Convert.ToChar(value);
              break;
            }
            if (typeof(DateTime) == type)
            {
              value = Convert.ToDateTime(value);
              break;
            }
            if (typeof(decimal) == type)
            {
              value = Convert.ToDecimal(value);
              break;
            }
            if (typeof(int) == type)
            {
              value = Convert.ToInt32(value);
              break;
            }
            if (typeof(long) == type)
            {
              value = Convert.ToInt64(value);
              break;
            }
            if (typeof(short) == type)
            {
              value = Convert.ToInt16(value);
              break;
            }
            if (typeof(string) == type)
            {
              break;
            }
            value = value.ToString();
            break;
          }
          SetValue(propertyName, value, propertyInfo);
        }
      }
    }

    // Sets the property value.
    /// <include path="members/SetValue/*" file="Doc/LJCReflect.xml"/>
    public void SetValue(string propertyName, object? value
      , PropertyInfo? propertyInfo = null)
    {
      PropertyInfo info;

      if (propertyInfo != null)
      {
        info = propertyInfo;
      }
      else
      {
        //info = mType.GetProperty(propertyName);
        info = GetPropertyInfo(propertyName);
      }
      if (info != null)
      {
        bool setValue = true;
        if (info.PropertyType == typeof(DateTime?))
        {
          if (value != null)
          {
            setValue = false;
            info.SetValue(mSource, Convert.ToDateTime(value), null);
          }
        }
        if (setValue)
        {
          info.SetValue(mSource, value, null);
        }
      }
    }
    #endregion

    #region Properties

    // Gets or sets the PropertyInfos value.
    /// <include path="members/PropertyInfos/*" file="Doc/LJCReflect.xml"/>
    public PropertyInfo[] PropertyInfos { get; set; }
    #endregion

    #region Class Data

    // Class Data.
    private object mSource;
    private readonly Type mType;
    private readonly LJCPropertyDelegates mPropertyDelegates;
    #endregion
  }
}
