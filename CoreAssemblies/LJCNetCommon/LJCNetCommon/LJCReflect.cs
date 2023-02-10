// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCReflect.cs
using System;
using System.Collections.Generic;
using System.Reflection;

namespace LJCNetCommon
{
  // Provides object property reflection capabilities. (D)
  /// <include path='items/LJCReflect/*' file='Doc/LJCReflect.xml'/>
  public class LJCReflect
  {
    #region Constructor Methods

    // Instantiates an instance of the class.
    /// <include path='items/LJCReflectC/*' file='Doc/LJCReflect.xml'/>
    public LJCReflect(object source)
    {
      mSource = source;
      mType = source.GetType();
      var bindingFlags = BindingFlags.Instance
        | BindingFlags.Public;
      PropertyInfos = mType.GetProperties(bindingFlags);
      mPropertyDelegates = new PropertyDelegates();
    }

    // Sets the source object and type values.
    /// <include path='items/SetSource/*' file='Doc/LJCReflect.xml'/>
    public void SetSource(object source)
    {
      mSource = source;
    }
    #endregion

    #region Get Methods

    // Gets the property value as a DateTime value.
    /// <include path='items/GetDateTime/*' file='Doc/LJCReflect.xml'/>
    public DateTime GetDateTime(string propertyName)
    {
      DateTime retVal = DateTime.MinValue;

      var value = GetValue(propertyName);
      if (value != null
        && value.GetType() == typeof(DateTime))
      {
        retVal = Convert.ToDateTime(value);
      }
      return retVal;
    }

    // Gets the property value as a DB date/time string.
    /// <include path='items/GetDbDateString/*' file='Doc/LJCReflect.xml'/>
    public string GetDbDateString(string propertyName)
    {
      string retVal = null;

      if (NetString.HasValue(propertyName))
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

    // Gets the property value as an integer.
    /// <include path='items/GetInt32/*' file='Doc/LJCReflect.xml'/>
    public int GetInt32(string propertyName)
    {
      int retVal = 0;

      var value = GetValue(propertyName);
      if (value != null
        && value.GetType() == typeof(int))
      {
        retVal = (int)value;
      }
      return retVal;
    }

    // Gets the cached PropertyInfo value.
    /// <include path='items/GetPropertyInfo/*' file='Doc/LJCReflect.xml'/>
    public PropertyInfo GetPropertyInfo(string propertyName)
    {
      PropertyInfo retValue = null;

      retValue = Array.Find(PropertyInfos, (x) => x.Name == propertyName);
      //if (null == retValue)
      //{
      //	var text = $"Property '{propertyName}' was not found"
      //		+ $" in object '{mSource.GetType().Name}'.";
      //	throw new ArgumentException(text);
      //}
      return retValue;
    }

    // Gets a list of the property names.
    /// <include path='items/GetPropertyNames/*' file='Doc/LJCReflect.xml'/>
    public List<string> GetPropertyNames()
    {
      List<string> retValue = new List<string>();

      if (PropertyInfos != null && PropertyInfos.Length > 0)
      {
        foreach (PropertyInfo propertyInfo in PropertyInfos)
        {
          retValue.Add(propertyInfo.Name);
        }
      }
      return retValue;
    }

    // Get the property type.
    /// <include path='items/GetPropertyType/*' file='Doc/LJCReflect.xml'/>
    public Type GetPropertyType(string propertyName)
    {
      Type retVal = null;

      var info = GetPropertyInfo(propertyName);
      if (info != null)
      {
        retVal = info.PropertyType;
      }
      return retVal;
    }

    // Gets the property value as a string.
    /// <include path='items/GetString/*' file='Doc/LJCReflect.xml'/>
    public string GetString(string propertyName)
    {
      string retVal = null;

      var value = GetValue(propertyName);
      if (value != null)
      {
        retVal = value.ToString();
      }
      return retVal;
    }

    // Gets the property value as an object using a delegate. (E)
    /// <include path='items/GetValue/*' file='Doc/LJCReflect.xml'/>
    public object GetValue(string propertyName)
    {
      object retValue = null;

      var propertyDelegate = mPropertyDelegates.LJCSearchName(propertyName);
      if (null == propertyDelegate)
      {
        var propertyInfo = GetPropertyInfo(propertyName);
        if (null == propertyInfo)
        {
          throw new ArgumentException($"Property {propertyName} was not found.");
        }
        propertyDelegate = mPropertyDelegates.Add(propertyInfo);
      }

      if (propertyDelegate != null)
      {
        var getter = propertyDelegate.Value;
        retValue = getter(mSource);
      }
      return retValue;
    }

    // Gets the property value as an object using reflection.
    /// <include path='items/GetValueReflect/*' file='Doc/LJCReflect.xml'/>
    public object GetValueReflect(string propertyName)
    {
      object retVal = null;

      if (NetString.HasValue(propertyName))
      {
        var propertyInfo = GetPropertyInfo(propertyName);
        if (propertyInfo != null)
        {
          retVal = propertyInfo.GetValue(mSource, null);
        }
      }
      return retVal;
    }
    #endregion

    #region Set Methods

    // Sets the property value based on value type. (E)
    /// <include path='items/SetPropertyValue/*' file='Doc/LJCReflect.xml'/>
    public void SetPropertyValue(string propertyName, object value)
    {
      Type type;

      if (value != null
        && NetString.HasValue(value.ToString()))
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
    /// <include path='items/SetValue/*' file='Doc/LJCReflect.xml'/>
    public void SetValue(string propertyName, object value
      , PropertyInfo propertyInfo = null)
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

    /// <summary>Gets or sets the PropertyInfos value.</summary>
    public PropertyInfo[] PropertyInfos { get; set; }
    #endregion

    #region Class Data

    // Class Data.
    private object mSource;
    private readonly Type mType;
    private readonly PropertyDelegates mPropertyDelegates;
    #endregion
  }
}
