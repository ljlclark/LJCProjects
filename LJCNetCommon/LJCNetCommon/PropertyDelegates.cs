// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// PropertyDelegates.cs
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace LJCNetCommon
{
  /// <summary>Represents a collection of PropertyDelegate objects.</summary>
  public class PropertyDelegates : List<PropertyDelegate>
  {
    // Creates and adds a PropertyDelegate object to the collection. (R)
    /// <include path='items/Add/*' file='Doc/PropertyDelegates.xml'/>
    public PropertyDelegate Add(PropertyInfo propertyInfo)
    {
      PropertyDelegate retValue;

      retValue = LJCSearchName(propertyInfo.Name);
      if (null == retValue)
      {
        var value = LJCCreateDelegate(propertyInfo);
        if (value != null)
        {
          retValue = new PropertyDelegate()
          {
            PropertyName = propertyInfo.Name,
            Value = value
          };
          Add(retValue);
        }
      }
      return retValue;
    }

    // Returns the PropertyDelegate object if found in the list.
    /// <include path='items/LJCSearchName/*' file='Doc/PropertyDelegates.xml'/>
    public PropertyDelegate LJCSearchName(string propertyName)
    {
      PropertyDelegate retValue = null;

      retValue = Find((x) => x.PropertyName == propertyName);
      return retValue;
    }

    // Creates and returns the delegate for the named property.
    /// <include path='items/LJCCreateDelegate/*' file='Doc/PropertyDelegates.xml'/>
    public Func<object, object> LJCCreateDelegate(PropertyInfo propertyInfo)
    {
      // Create the instance parameter.
      var parameters = Expression.Parameter(typeof(object), "Name");

      // Create the expression.
      var typedParameters = Expression.TypeAs(parameters
        , propertyInfo.DeclaringType);
      var property = Expression.Property(typedParameters, propertyInfo);
      var body = Expression.Convert(property, typeof(object));

      // Create the delegate.
      return Expression.Lambda<Func<object, object>>(body
        , parameters).Compile();
    }
  }
}
