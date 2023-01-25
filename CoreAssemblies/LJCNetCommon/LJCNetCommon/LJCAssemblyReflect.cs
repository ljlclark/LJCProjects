// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCassemblyReflect.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Linq;
using System.Diagnostics;

namespace LJCNetCommon
{
  // Provides Assembly Reflection methods (D)
  /// <include path='items/LJCAssemblyReflect/*' file='Doc/LJCAssemblyReflect.xml'/>
  public class LJCAssemblyReflect
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public LJCAssemblyReflect()
    {
    }
    #endregion

    #region Set Object Reflection Property Methods

    // Retrieves the Assembly reference. (R)
    /// <include path='items/SetAssembly/*' file='Doc/LJCAssemblyReflect.xml'/>
    public Assembly SetAssembly(string fileSpec)
    {
      FileSpec = fileSpec;

      Assembly = null;
      TypeName = null;
      TypeReference = null;
      MethodName = null;
      MethodInfo = null;
      if (false == string.IsNullOrWhiteSpace(FileSpec)
        && File.Exists(FileSpec))
      {
        Assembly = Assembly.LoadFrom(FileSpec);
      }
      return Assembly;
    }

    // Set the ConstructorInfo reference. (RE)
    /// <include path='items/SetConstructorInfo/*' file='Doc/LJCAssemblyReflect.xml'/>
    public ConstructorInfo SetConstructorInfo(string methodName
      , string[] parameterNames)
    {
      ParameterInfo[] parameterInfos;
      ConstructorInfo selectedConstructorInfo = null;

      ConstructorInfo = null;
      MethodName = methodName;
      MethodInfo = null;
      if (TypeReference != null)
      {
        BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public
          | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly;

        if (null == parameterNames
          || (parameterNames != null && 0 == parameterNames.Length))
        {
          // Get constructor with no parameters.
          Type[] types = new Type[0];
          ConstructorInfo = TypeReference.GetConstructor(types);
        }
        else
        {
          // Get constructor with matching parameters.
          ConstructorInfo[] constructorInfos = TypeReference.GetConstructors(bindingFlags);
          foreach (ConstructorInfo constructorInfo in constructorInfos)
          {
            parameterInfos = constructorInfo.GetParameters();
            if (parameterInfos.Length != parameterNames.Length)
            {
              continue;
            }

            // Check parameter names.
            bool isSuccess = true;
            for (int index = 0; index < parameterNames.Length; index++)
            {
              string parameterName = parameterNames[index];
              if (parameterName != parameterInfos[index].Name)
              {
                isSuccess = false;
                break;
              }
            }
            if (isSuccess)
            {
              selectedConstructorInfo = constructorInfo;
              break;
            }
          }
        }

        if (selectedConstructorInfo != null)
        {
          StringBuilder builder = new StringBuilder(64);
          bool first = true;
          builder.Append($"{TypeReference.Name}(");
          string signature;

          // Create constructor parameter type array.
          List<Type> parameterTypeList = new List<Type>();
          parameterInfos = selectedConstructorInfo.GetParameters();
          foreach (ParameterInfo parameterInfo in parameterInfos)
          {
            if (false == first)
            {
              builder.Append(", ");
            }
            first = false;
            builder.Append(parameterInfo.ParameterType.Name);
            parameterTypeList.Add(parameterInfo.ParameterType);
          }
          builder.Append(")");
          signature = builder.ToString();
          Type[] parameterTypes = new Type[parameterTypeList.Count];
          parameterTypeList.CopyTo(parameterTypes);

          // Get constructor info.
          ConstructorInfo = TypeReference.GetConstructor(parameterTypes);
          if (null == ConstructorInfo)
          {
            var errorText = $"Selected Constructor '{signature}' was not found.";
            throw new ArgumentException(errorText);
          }
        }
      }
      return ConstructorInfo;
    }

    // Set the FieldInfo reference. (RE)
    /// <include path='items/SetFieldInfo/*' file='Doc/LJCAssemblyReflect.xml'/>
    public FieldInfo SetFieldInfo(string fieldName)
    {
      FieldName = fieldName;

      FieldInfo = null;
      if (TypeReference != null
        && false == string.IsNullOrWhiteSpace(FieldName))
      {
        FieldInfo = TypeReference.GetField(FieldName);
      }
      return FieldInfo;
    }

    // Gets the Indexer Property info.
    /// <include path='items/GetIndexerInfo/*' file='Doc/LJCAssemblyReflect.xml'/>
    public PropertyInfo GetIndexerInfo(string fullName)
    {
      Type argType = null;
      PropertyInfo retValue = null;

      if ("Item" == PropertyName)
      {
        int startIndex = fullName.IndexOf('(');
        int stopIndex = fullName.IndexOf(')');
        if (startIndex > -1 && stopIndex > -1)
        {
          string typeName = fullName.Substring(startIndex + 1
            , stopIndex - startIndex - 1);
          switch (typeName)
          {
            case "System.Int32":
              argType = typeof(int);
              break;
            case "System.String":
              argType = typeof(string);
              break;
          }
          if (argType != null)
          {
            retValue = TypeReference.GetProperty(PropertyName, new Type[] { argType });
          }
        }
      }
      return retValue;
    }

    // Set the MethodInfo reference. (RE)
    /// <include path='items/SetMethodInfo/*' file='Doc/LJCAssemblyReflect.xml'/>
    public MethodInfo SetMethodInfo(string methodName, string[] parameterNames)
    {
      ParameterInfo[] parameterInfos;
      MethodInfo selectedMethodInfo = null;

      ConstructorInfo = null;
      MethodName = methodName;
      MethodInfo = null;
      if (TypeReference != null
        && NetString.HasValue(MethodName))
      {
        BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public
          | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly;

        if (null == parameterNames
          || (parameterNames != null && 0 == parameterNames.Length))
        {
          // Get method with no parameters.
          MethodInfo = TypeReference.GetMethod(methodName, bindingFlags
            , null, Type.EmptyTypes, null);
        }
        else
        {
          // Get method with matching parameters.
          MethodInfo[] methodInfos = TypeReference.GetMethods(bindingFlags).Where
            (x => x.Name.Equals(methodName)).ToArray();
          foreach (MethodInfo methodInfo in methodInfos)
          {
            try
            {
              parameterInfos = methodInfo.GetParameters();
            }
            catch (SystemException)
            {
              continue;
            }
            if (parameterInfos.Length != parameterNames.Length)
            {
              continue;
            }

            // Check parameter names.
            bool isSuccess = true;
            for (int index = 0; index < parameterNames.Length; index++)
            {
              string parameterName = parameterNames[index];
              if (parameterName != parameterInfos[index].Name)
              {
                isSuccess = false;
                break;
              }
            }
            if (isSuccess)
            {
              selectedMethodInfo = methodInfo;
              break;
            }
          }
        }

        if (selectedMethodInfo != null)
        {
          StringBuilder builder = new StringBuilder(64);
          bool first = true;
          builder.Append($"{MethodName}(");
          string signature = null;

          // Create method parameter type array.
          List<Type> parameterTypeList = new List<Type>();
          parameterInfos = selectedMethodInfo.GetParameters();
          foreach (ParameterInfo parameterInfo in parameterInfos)
          {
            if (false == first)
            {
              builder.Append(", ");
            }
            first = false;
            builder.Append(parameterInfo.ParameterType.Name);
            parameterTypeList.Add(parameterInfo.ParameterType);
          }
          builder.Append(")");
          signature = builder.ToString();
          Type[] parameterTypes = new Type[parameterTypeList.Count];
          parameterTypeList.CopyTo(parameterTypes);

          // Get method info.
          MethodInfo = TypeReference.GetMethod(MethodName, bindingFlags
            , null, parameterTypes, null);
          if (null == MethodInfo)
          {
            var errorText = $"Selected Method '{signature}' was not found.";
            throw new ArgumentException(errorText);
          }
        }
      }
      return MethodInfo;
    }

    // Set the PropertyInfo reference. (RE)
    /// <include path='items/SetPropertyInfo/*' file='Doc/LJCAssemblyReflect.xml'/>
    public PropertyInfo SetPropertyInfo(string propertyName
      , string fullName = null)
    {
      PropertyName = propertyName;

      PropertyInfo = null;
      if (TypeReference != null
        && false == string.IsNullOrWhiteSpace(PropertyName))
      {
        if ("Item" == PropertyName)
        {
          PropertyInfo = GetIndexerInfo(fullName);
        }
        else
        {
          PropertyInfo = TypeReference.GetProperty(PropertyName);
        }
      }
      return PropertyInfo;
    }

    // Set the Type reference. (RE)
    /// <include path='items/SetTypeReference/*' file='Doc/LJCAssemblyReflect.xml'/>
    public Type SetTypeReference(string typeName)
    {
      TypeName = typeName;

      TypeReference = null;
      MethodName = null;
      MethodInfo = null;
      if (Assembly != null
        && false == string.IsNullOrWhiteSpace(TypeName))
      {
        TypeReference = Assembly.GetType(TypeName);
      }
      return TypeReference;
    }
    #endregion

    #region Get Syntax Methods

    // Creates and returns the Constructor syntax. (E)
    /// <include path='items/GetConstructorSyntax/*' file='Doc/LJCAssemblyReflect.xml'/>
    public string GetConstructorSyntax(ConstructorInfo constructorInfo = null)
    {
      ConstructorInfo info = constructorInfo;
      string retValue = null;

      if (null == info)
      {
        info = ConstructorInfo;
      }

      if (info != null)
      {
        StringBuilder builder = new StringBuilder(128);

        // Access Modifier
        if (info.IsPublic)
        {
          builder.Append("public ");
        }
        if (info.IsPrivate)
        {
          builder.Append("private ");
        }
        if (info.IsStatic)
        {
          builder.Append("static ");
        }

        // Name
        builder.Append($"{TypeReference.Name}(");

        // Parameters
        ParameterInfo[] parameterInfos = info.GetParameters();
        bool isFirst = true;
        foreach (ParameterInfo parameterInfo in parameterInfos)
        {
          if (false == isFirst)
          {
            builder.Append(", ");
          }
          isFirst = false;

          if (parameterInfo.IsOut)
          {
            builder.Append("out ");
          }

          if (parameterInfo.ParameterType.IsGenericType)
          {
            builder.Append($"{GetGenericTypeSyntax(parameterInfo.ParameterType)} ");
          }
          else
          {
            builder.Append($"{parameterInfo.ParameterType.Name} ");
          }

          builder.Append($"{parameterInfo.Name}");
          if (parameterInfo.HasDefaultValue)
          {
            builder.Append(" = ");
            if (parameterInfo.ParameterType.IsEnum)
            {
              builder.Append($"{parameterInfo.ParameterType.Name}.");
            }
            if (null == parameterInfo.DefaultValue)
            {
              builder.Append("null");
            }
            else
            {
              builder.Append(parameterInfo.DefaultValue.ToString());
            }
          }
        }

        builder.Append(")");
        retValue = builder.ToString();
      }
      return retValue;
    }

    // Creates and returns the Field syntax string. (E)
    /// <include path='items/GetFieldSyntax/*' file='Doc/LJCAssemblyReflect.xml'/>
    public string GetFieldSyntax(FieldInfo fieldInfo = null)
    {
      FieldInfo info = fieldInfo;
      string retValue = null;

      if (null == info)
      {
        info = FieldInfo;
      }

      if (info != null)
      {
        StringBuilder builder = new StringBuilder(128);

        // Access Modifier
        if (info.IsPublic)
        {
          builder.Append("public ");
        }
        if (info.IsPrivate)
        {
          builder.Append("private ");
        }
        if (info.IsStatic)
        {
          builder.Append("static ");
        }

        // Field Type
        builder.Append($"{info.FieldType.Name} ");

        // Name
        builder.Append(info.Name);

        retValue = builder.ToString();
      }
      return retValue;
    }

    // Creates and returns the Generic Type syntax.
    /// <include path='items/GetGenericTypeSyntax/*' file='Doc/LJCAssemblyReflect.xml'/>
    public string GetGenericTypeSyntax(Type typeReference = null)
    {
      Type type = typeReference;
      string retValue = null;

      if (null == type)
      {
        type = TypeReference;
      }

      if (type != null
        && type.IsGenericType)
      {
        StringBuilder builder = new StringBuilder(128);
        int charIndex = type.Name.IndexOf('`');
        builder.Append($"{type.Name.Substring(0, charIndex)}<");
        bool isFirst = true;

        Type[] genericValues;
        if (type.GenericTypeArguments.Length > 0)
        {
          genericValues = type.GenericTypeArguments;
        }
        else
        {
          TypeInfo typeInfo = type.GetTypeInfo();
          genericValues = typeInfo.GenericTypeParameters;
        }

        foreach (Type typeArgument in genericValues)
        {
          if (false == isFirst)
          {
            builder.Append(", ");
          }
          isFirst = false;
          builder.Append(typeArgument.Name);
        }
        builder.Append(">");
        retValue = builder.ToString();
      }
      return retValue;
    }

    // Creates and returns the Method syntax. (E)
    /// <include path='items/GetMethodSyntax/*' file='Doc/LJCAssemblyReflect.xml'/>
    public string GetMethodSyntax(MethodInfo methodInfo = null)
    {
      MethodInfo info = methodInfo;
      string retValue = null;

      if (null == info)
      {
        info = MethodInfo;
      }

      if (info != null
        && IsNotProperty(info))
      {
        StringBuilder builder = new StringBuilder(128);

        // Access Modifier
        if (info.IsPublic)
        {
          builder.Append("public ");
        }
        if (info.IsPrivate)
        {
          builder.Append("private ");
        }
        if (info.IsStatic)
        {
          builder.Append("static ");
        }
        if (IsOverride(info))
        {
          builder.Append("override ");
        }

        // Return Type
        builder.Append($"{info.ReturnType.Name} ");

        // Name
        builder.Append($"{info.Name}(");

        // Parameters
        ParameterInfo[] parameterInfos = info.GetParameters();
        bool isFirst = true;
        foreach (ParameterInfo parameterInfo in parameterInfos)
        {
          if (false == isFirst)
          {
            builder.Append(", ");
          }
          isFirst = false;

          if (parameterInfo.IsOut)
          {
            builder.Append("out ");
          }

          if (parameterInfo.ParameterType.IsGenericType)
          {
            builder.Append($"{GetGenericTypeSyntax(parameterInfo.ParameterType)} ");
          }
          else
          {
            builder.Append($"{parameterInfo.ParameterType.Name} ");
          }

          builder.Append($"{parameterInfo.Name}");
          if (parameterInfo.HasDefaultValue)
          {
            builder.Append(" = ");
            if (parameterInfo.ParameterType.IsEnum)
            {
              builder.Append($"{parameterInfo.ParameterType.Name}.");
            }
            if (null == parameterInfo.DefaultValue)
            {
              builder.Append("null");
            }
            else
            {
              builder.Append(parameterInfo.DefaultValue.ToString());
            }
          }
        }

        builder.Append(")");
        retValue = builder.ToString();
      }
      return retValue;
    }

    // Creates and returns the Property syntax string. (E)
    /// <include path='items/GetPropertySyntax/*' file='Doc/LJCAssemblyReflect.xml'/>
    public string GetPropertySyntax(PropertyInfo propertyInfo = null)
    {
      PropertyInfo info = propertyInfo;
      string retValue = null;

      if (null == info)
      {
        info = PropertyInfo;
      }

      if (info != null)
      {
        StringBuilder builder = new StringBuilder(128);

        // Access Modifier
        MethodInfo methodInfo = info.GetMethod;

        if (methodInfo.IsPublic)
        {
          builder.Append("public ");
        }
        if (methodInfo.IsPrivate)
        {
          builder.Append("private ");
        }
        if (methodInfo.IsStatic)
        {
          builder.Append("static ");
        }

        // Return Type
        builder.Append($"{methodInfo.ReturnType.Name} ");

        // Name
        if ("Item" == info.Name)
        {
          builder.Append($"{info.Name}(");
        }
        else
        {
          builder.Append(info.Name);
        }

        // Parameters
        ParameterInfo[] parameterInfos = info.GetIndexParameters();
        if (parameterInfos.Count() > 0)
        {
          ParameterInfo parameterInfo = parameterInfos[0];
          builder.Append($"{parameterInfo.ParameterType.Name} ");
          builder.Append(parameterInfo.Name);
        }

        if ("Item" == info.Name)
        {
          builder.Append(")");
        }
        retValue = builder.ToString();
      }
      return retValue;
    }

    // Creates and returns the Type syntax. (E)
    /// <include path='items/GetTypeSyntax/*' file='Doc/LJCAssemblyReflect.xml'/>
    public string GetTypeSyntax(Type typeReference = null)
    {
      Type type = typeReference;
      string retValue = null;

      if (null == type)
      {
        type = TypeReference;
      }

      if (type != null)
      {
        StringBuilder builder = new StringBuilder(128);

        // Access Modifier
        if (type.IsPublic)
        {
          builder.Append("public ");
        }
        else
        {
          builder.Append("private ");
        }

        // Type Classification
        if (type.IsClass)
        {
          builder.Append("class ");
        }
        if (type.IsInterface)
        {
          builder.Append("interface ");
        }
        if (type.IsValueType && false == type.IsEnum)
        {
          builder.Append("struct ");
        }
        if (type.IsEnum)
        {
          builder.Append("enum ");
        }
        if (type.IsSubclassOf(typeof(Delegate))
          || type.IsSubclassOf(typeof(MulticastDelegate)))
        {
          builder.Append("delegate ");
        }

        // Name
        string name = type.Name;
        if (type.IsGenericType)
        {
          name = GetGenericTypeSyntax(type);
        }
        builder.Append($"{name} ");

        // Base Type
        bool hasQualifiers = false;
        Type baseType = type.BaseType;
        if (baseType != null)
        {
          if (baseType.IsGenericType)
          {
            hasQualifiers = true;
            builder.Append(": ");
            builder.Append(GetGenericTypeSyntax(baseType));
          }
          else
          {
            if (IsNotCommonClassification(baseType))
            {
              hasQualifiers = true;
              builder.Append($": {baseType.Name} ");
            }
          }
        }

        // Implemented Interfaces
        if (baseType != null && baseType.Name != "Enum")
        {
          foreach (Type implementedType in type.GetInterfaces())
          {
            if (IsNotCommonInterface(implementedType))
            {
              if (false == hasQualifiers)
              {
                builder.Append(": ");
              }
              else
              {
                builder.Append(", ");
              }
              if (implementedType.IsGenericType)
              {
                builder.Append(GetGenericTypeSyntax(implementedType));
              }
              else
              {
                builder.Append($"{implementedType.Name} ");
              }
              hasQualifiers = true;
            }
          }
        }

        retValue = builder.ToString();
      }
      return retValue;
    }
    #endregion

    #region Public Flag Methods

    // Indicates if the Type is not a common type.
    /// <include path='items/IsNotCommonClassification/*' file='Doc/LJCAssemblyReflect.xml'/>
    public bool IsNotCommonClassification(Type type)
    {
      bool retValue = true;

      if (type.Name == "Object"
        || type.Name == "Enum")
      {
        retValue = false;
      }
      return retValue;
    }

    // Indicates if the Interface is not a common type.
    /// <include path='items/IsNotCommonInterface/*' file='Doc/LJCAssemblyReflect.xml'/>
    public bool IsNotCommonInterface(Type type)
    {
      bool retValue = true;

      if (type.FullName.StartsWith("System.ComponentModel")
        || type.FullName.StartsWith("System.ServiceModel")
        || type.FullName.StartsWith("System.Windows.Forms"))
      {
        retValue = false;
      }

      if (retValue)
      {
        if (type.Name.StartsWith("ICloneable")
          || type.Name.StartsWith("ICollection")
          || type.Name.StartsWith("IConvertible")
          || type.Name.StartsWith("IDisposable")
          || type.Name.StartsWith("IFormattable")
          || type.Name.StartsWith("IEnumerable")
          || type.Name.StartsWith("IList")
          || type.Name.StartsWith("IReadOnly"))
        {
          retValue = false;
        }
      }
      return retValue;
    }

    // Indicates if the Method is not a property getter or setter.
    /// <include path='items/IsNotProperty/*' file='Doc/LJCAssemblyReflect.xml'/>
    public bool IsNotProperty(MethodInfo methodInfo)
    {
      bool retValue = false;

      if (methodInfo != null
        && false == methodInfo.Name.StartsWith("get_")
        && false == methodInfo.Name.StartsWith("set_"))
      {
        retValue = true;
      }
      return retValue;
    }

    // Indicates if the method is "override".
    /// <include path='items/IsOverride/*' file='Doc/LJCAssemblyReflect.xml'/>
    public bool IsOverride(MethodInfo methodInfo)
    {
      return !methodInfo.Equals(methodInfo.GetBaseDefinition());
    }

    // Indicates if the method is "public".
    /// <include path='items/IsPublic/*' file='Doc/LJCAssemblyReflect.xml'/>
    public bool IsPublic(MethodInfo methodInfo = null)
    {
      MethodInfo info = methodInfo;
      bool retValue = false;

      if (null == info)
      {
        info = MethodInfo;
      }

      if (info != null)
      {
        if (IsNotProperty(info))
        {
          if (info.IsPublic)
          {
            retValue = true;
          }
        }
      }
      else
      {
        if (ConstructorInfo != null)
        {
          if (ConstructorInfo.IsPublic)
          {
            retValue = true;
          }
        }
      }
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>The Assembly reference.</summary>
    public Assembly Assembly { get; set; }

    /// <summary>The constructor information.</summary>
    public ConstructorInfo ConstructorInfo { get; set; }

    /// <summary>The Field information.</summary>
    public FieldInfo FieldInfo { get; set; }

    /// <summary>The Field name.</summary>
    public string FieldName { get; set; }

    /// <summary>The Assembly file name.</summary>
    public string FileSpec { get; set; }

    /// <summary>The Method information.</summary>
    public MethodInfo MethodInfo { get; set; }

    /// <summary>The Method name.</summary>
    public string MethodName { get; set; }

    /// <summary>The Property information.</summary>
    public PropertyInfo PropertyInfo { get; set; }

    /// <summary>The Property name.</summary>
    public string PropertyName { get; set; }

    /// <summary>The Type name.</summary>
    public string TypeName { get; set; }

    /// <summary>The Type reference.</summary>
    public Type TypeReference { get; set; }
    #endregion
  }
}
