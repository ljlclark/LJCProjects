// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCPropertyDelegates.cs
using System.Linq.Expressions;
using System.Reflection;

namespace LJCNetCommon5
{
  /// <summary>Represents a collection of PropertyDelegate objects.</summary>
  public class LJCPropertyDelegates : List<LJCPropertyDelegate>
  {
    // Creates and adds a PropertyDelegate object to the collection. (R)
    /// <include file='Doc/LJCPropertyDelegates.xml'
    ///  path='members/Add/*'/>
    public LJCPropertyDelegate? Add(PropertyInfo propertyInfo)
    {
      LJCPropertyDelegate retValue;

      retValue = LJCSearchName(propertyInfo.Name);
      if (null == retValue)
      {
        var value = LJCCreateDelegate(propertyInfo);
        if (value != null)
        {
          retValue = new LJCPropertyDelegate()
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
    /// <include file='Doc/LJCPropertyDelegates.xml'
    ///  path='members/LJCSearchName/*'/>
    public LJCPropertyDelegate? LJCSearchName(string propertyName)
    {
      LJCPropertyDelegate retValue = null;

      retValue = Find((x) => x.PropertyName == propertyName);
      return retValue;
    }

    // Creates and returns the delegate for the named property.
    /// <include file='Doc/LJCPropertyDelegates.xml'
    ///  path='members/LJCCreateDelegate/*'/>
    public static Func<object, object>? LJCCreateDelegate(PropertyInfo propertyInfo)
    {
      Func<object, object> retValue = null;

      // Create the instance parameter.
      var parameters = Expression.Parameter(typeof(object), "Name");

      // Create the expression.
      var declaringType = propertyInfo.DeclaringType;
      if (declaringType != null)
      {
        var typedParameters = Expression.TypeAs(parameters, declaringType);
        var property = Expression.Property(typedParameters, propertyInfo);
        var body = Expression.Convert(property, typeof(object));

        // Create the delegate.
        retValue = Expression.Lambda<Func<object, object>>(body
          , parameters).Compile();
      }
      return retValue;
    }
  }
}
