// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCReflect.cs
using System.Reflection;

namespace LJCNetCommon5
{
  // Provides object property reflection capabilities. (D)
  /// <include file='Doc/LJCReflect.xml'
  ///  path='members/LJCReflect/*'/>
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
    /// <include file='Doc/LJCReflect.xml'
    ///  path='members/SetSource/*'/>
    public void SetSource(object source)
    {
      mSource = source;
    }
    #endregion

    #region Methods

    // Gets the cached PropertyInfo value.
    /// <include file='Doc/LJCReflect.xml'
    ///  path='members/GetPropertyInfo/*'/>
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
    /// <include file='Doc/LJCReflect.xml'
    ///  path='members/GetPropertyNames/*'/>
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
    /// <include file='Doc/LJCReflect.xml'
    ///  path='members/GetPropertyType/*'/>
    public Type? GetPropertyType(string propertyName)
    {
      Type retVal = null;

      var info = GetPropertyInfo(propertyName);
      retVal = info?.PropertyType;
      return retVal;
    }

    // Checks if a property exists.
    /// <include file='Doc/LJCReflect.xml'
    ///  path='members/HasProperty/*'/>
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
    /// <include file='Doc/LJCReflect.xml'
    ///  path='members/GetBoolean/*'/>
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
    /// <include file='Doc/LJCReflect.xml'
    ///  path='members/GetByte/*'/>
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
    /// <include file='Doc/LJCReflect.xml'
    ///  path='members/GetChar/*'/>
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
    /// <include file='Doc/LJCReflect.xml'
    ///  path='members/GetDateTime/*'/>
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
    /// <include file='Doc/LJCReflect.xml'
    ///  path='members/GetDbDateString/*'/>
    public string? GetDbDateString(string propertyName)
    {
      string retVal = default;

      if (LJC.HasText(propertyName))
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
    /// <include file='Doc/LJCReflect.xml'
    ///  path='members/GetDecimal/*'/>
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
    /// <include file='Doc/LJCReflect.xml'
    ///  path='members/GetDouble/*'/>
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
    /// <include file='Doc/LJCReflect.xml'
    ///  path='members/GetInt16/*'/>
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
    /// <include file='Doc/LJCReflect.xml'
    ///  path='members/GetInt32/*'/>
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
    /// <include file='Doc/LJCReflect.xml'
    ///  path='members/GetInt64/*'/>
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
    /// <include file='Doc/LJCReflect.xml'
    ///  path='members/GetSingle/*'/>
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
    /// <include file='Doc/LJCReflect.xml'
    ///  path='members/GetString/*'/>
    public string? GetString(string propertyName)
    {
      string retVal = null;

      var value = GetValue(propertyName);
      retVal = value?.ToString();
      return retVal;
    }

    // Gets the property value as an object using a delegate.
    /// <include file='Doc/LJCReflect.xml'
    ///  path='members/GetValue/*'/>
    public object? GetValue(string? propertyName, bool throwError = true)
    {
      object retValue = null;

      if (LJC.HasText(propertyName))
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
              var text = $"{name}: Property \"{propertyName}\" was not found.";
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
    /// <include file='Doc/LJCReflect.xml'
    ///  path='members/GetValueReflect/*'/>
    public object? GetValueReflect(string propertyName, bool throwError = true)
    {
      object retVal = null;

      if (LJC.HasText(propertyName))
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
    /// <include file='Doc/LJCReflect.xml'
    ///  path='members/SetPropertyValue/*'/>
    public void SetPropertyValue(string propertyName, object? value)
    {
      Type type;

      if (value != null
        && LJC.HasText(value.ToString()))
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
    /// <include file='Doc/LJCReflect.xml'
    ///  path='members/SetValue/*'/>
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
    /// <include file='Doc/LJCReflect.xml'
    ///  path='members/PropertyInfos/*'/>
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
