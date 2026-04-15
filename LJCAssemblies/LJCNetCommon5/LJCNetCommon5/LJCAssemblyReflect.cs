// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCAssemblyReflect.cs
using System.Reflection;
using System.Text;

namespace LJCNetCommon5
{
  // Provides Assembly Reflection methods
  /// <include path="members/LJCAssemblyReflect/*" file="Doc/LJCAssemblyReflect.xml"/>
  /// <group name="static">Static</group>
  /// <group name="constructor">Constructors</group>
  /// <group name="method">Methods</group>
  /// <group name="reflectionProperty">Set Reflection Properties</group>
  /// <group name="syntax">Get Syntax</group>
  /// <group name="check">Check Type</group>
  /// <group name="properties">Properties</group>
  public class LJCAssemblyReflect
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path="members/DefaultConstructor/*" file="../../../CoreUtilities/LJCGenDoc/Common/Data.xml"/>
    /// <parentGroup>constructor</parentGroup>
    public LJCAssemblyReflect()
    {
      Clear();
    }

    // Initializes an object instance.
    /// <include path="members/Clear/*" file="../../../CoreUtilities/LJCGenDoc/Common/Data.xml"/>
    /// <parentGroup>constructor</parentGroup>
    public void Clear()
    {
      Assembly = null;
      FieldInfo = null;
      FieldName = null;
      FileSpec = null;
      MethodInfo = null;
      MethodName = null;
      PropertyInfo = null;
      PropertyName = null;
      TypeName = null;
      TypeReference = null;
    }
    #endregion

    #region Static Methods

    // Gets parm names from method info.
    /// <include path="members/ParmNames/*" file="Doc/LJCAssemblyReflect.xml"/>
    /// <parentGroup>static</parentGroup>
    public static string[]? ParmNames(MethodInfo methodInfo)
    {
      string[] retParamNames = null;

      if (methodInfo != null)
      {
        var paramNames = new List<string>();
        ParameterInfo[] parmInfos;

        parmInfos = methodInfo.GetParameters();
        if (LJC.HasItems(parmInfos))
        {
          for (int index = 0; index < parmInfos.Length; index++)
          {
            ParameterInfo parmInfo = parmInfos[index];
            string parmName = parmInfo.Name;
            if (LJC.HasValue(parmName))
            {
              paramNames.Add(parmName);
            }
          }
          //retParamNames = paramNames.ToArray();
          retParamNames = [.. paramNames];
        }
      }
      return retParamNames;
    }

    // Gets parm method type names with parm names.
    /// <include path="members/ParmTypeNames/*" file="Doc/LJCAssemblyReflect.xml"/>
    /// <parentGroup>static</parentGroup>
    public static string[]? ParmTypeNames(MethodInfo methodInfo)
    {
      string[] retTypeNames = null;

      if (methodInfo != null)
      {
        var typeNames = new List<string>();
        ParameterInfo[] parmInfos;
        parmInfos = methodInfo.GetParameters();
        if (LJC.HasItems(parmInfos))
        {
          for (int index = 0; index < parmInfos.Length; index++)
          {
            ParameterInfo parmInfo = parmInfos[index];
            if (parmInfo != null)
            {
              string typeName = parmInfo.ParameterType.Name;
              if (LJC.HasValue(typeName))
              {
                typeNames.Add(typeName);
              }
            }
          }
          //retTypeNames = typeNames.ToArray();
          retTypeNames = [.. typeNames];
        }
      }
      return retTypeNames;
    }
    #endregion

    #region Methods

    // Gets a field info object.
    /// <include path="members/GetFieldInfo/*" file="Doc/LJCAssemblyReflect.xml"/>
    /// <parentGroup>method</parentGroup>
    public FieldInfo? GetFieldInfo(string fieldName)
    {
      return TypeReference?.GetField(fieldName);
    }

    // Gets a parameterless method info object.
    /// <include path="members/MethodInfo/*" file="Doc/LJCAssemblyReflect.xml"/>
    /// <parentGroup>method</parentGroup>
    public MethodInfo? GetMethodInfo(string methodName)
    {
      MethodInfo? retMethodInfo = null;

      MethodInfo[] methodInfos = TypeReference?.GetMethods().Where
        (x => x.Name.Equals(methodName)).ToArray();
      if (LJC.HasItems(methodInfos))
      {
        ParameterInfo[] parmInfos;
        foreach (MethodInfo methodInfo in methodInfos)
        {
          try { parmInfos = methodInfo.GetParameters(); }
          catch { continue; }
          if (!LJC.HasItems(parmInfos))
          {
            retMethodInfo = methodInfo;
          }
        }
      }
      return retMethodInfo;
    }

    // Get a parameterized method info object.
    /// <include path="members/ParmMethodInfosWithTypes/*" file="Doc/LJCAssemblyReflect.xml"/>
    /// <parentGroup>method</parentGroup>
    public MethodInfo? ParmMethodInfo(string methodName
      , string[] parmNames)
    {
      MethodInfo retMethodInfo = null;

      var methodInfo = ParmMethodInfoWithParms(methodName, parmNames);
      if (methodInfo != null)
      {
        var typeNames = ParmTypeNames(methodInfo);
        var parmInfos = methodInfo.GetParameters();
        if (LJC.HasItems(typeNames)
          && LJC.HasItems(parmInfos)
          && typeNames.Length == parmInfos.Length)
        {
          var found = true;
          for (int index = 0; index < typeNames.Length; index++)
          {
            string typeName = typeNames[index];
            if (typeName != parmInfos[index].ParameterType.Name)
            {
              found = false;
              break;
            }
          }
          if (found)
          {
            retMethodInfo = methodInfo;
          }
        }
      }
      return retMethodInfo;
    }

    // Gets parm method infos with method name and parm count.
    /// <include path="members/ParmMethodInfosWithCount/*" file="Doc/LJCAssemblyReflect.xml"/>
    /// <parentGroup>method</parentGroup>
    public MethodInfo[]? ParmMethodInfosWithCount(string methodName
      , int parmCount)
    {
      MethodInfo[] retMethodInfos = null;

      MethodInfo[] methodInfos = TypeReference?.GetMethods().Where
        (x => x.Name.Equals(methodName)).ToArray();
      if (LJC.HasItems(methodInfos))
      {
        var methodInfoList = new List<MethodInfo>();
        ParameterInfo[] parmInfos;

        // Check parameter count.
        foreach (MethodInfo methodInfo in methodInfos)
        {
          try { parmInfos = methodInfo.GetParameters(); }
          catch { continue; }
          if (parmInfos.Length == parmCount)
          {
            methodInfoList.Add(methodInfo);
          }
        }
        //retMethodInfos = methodInfoList.ToArray();
        retMethodInfos = [.. methodInfoList];
      }
      return retMethodInfos;
    }

    // Gets parm method info with parm names.
    /// <include path="members/ParmMethodInfosWithParms/*" file="Doc/LJCAssemblyReflect.xml"/>
    /// <parentGroup>method</parentGroup>
    public MethodInfo? ParmMethodInfoWithParms(string methodName
      , string[] parmNames)
    {
      MethodInfo retMethodInfo = null;

      var methodInfos = ParmMethodInfosWithCount(methodName, parmNames.Length);
      if (LJC.HasItems(methodInfos)
        && LJC.HasItems(parmNames))
      {
        foreach (MethodInfo methodInfo in methodInfos)
        {
          var parmInfos = methodInfo.GetParameters();
          if (!LJC.HasItems(parmInfos)
            || parmInfos.Length != parmNames.Length)
          {
            continue;
          }
          var found = true;
          for (int index = 0; index < parmNames.Length; index++)
          {
            string parmName = parmNames[index];
            if (parmName != parmInfos[index].Name)
            {
              found = false;
              break;
            }
          }
          if (found)
          {
            retMethodInfo = methodInfo;
            break;
          }
        }
      }
      return retMethodInfo;
    }
    #endregion

    #region Set Reflection Property Methods

    // Retrieves the Assembly reference.
    /// <include path="members/SetAssembly/*" file="Doc/LJCAssemblyReflect.xml"/>
    /// <parentGroup>reflectionProperty</parentGroup>
    public Assembly? SetAssembly(string fileSpec)
    {
      Clear();
      FileSpec = fileSpec;
      if (LJC.HasValue(FileSpec)
        && File.Exists(FileSpec))
      {
        Assembly = Assembly.LoadFrom(FileSpec);
      }
      return Assembly;
    }

    // Set the ConstructorInfo reference.
    /// <include path="members/SetConstructorInfo/*" file="Doc/LJCAssemblyReflect.xml"/>
    /// <parentGroup>reflectionProperty</parentGroup>
    public ConstructorInfo? SetConstructorInfo(string methodName
      , string[]? parameterNames)
    {
      ParameterInfo[] parameterInfos;
      ConstructorInfo selectedConstructorInfo = null;

      ConstructorInfo = null;
      MethodName = methodName;
      MethodInfo = null;
      if (TypeReference != null)
      {
        //BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public
        //  | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly;
        BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public
          | BindingFlags.Static | BindingFlags.DeclaredOnly;

        if (null == parameterNames
          || (parameterNames != null && 0 == parameterNames.Length))
        {
          // Get constructor with no parameters.
          Type[] types = [];
          ConstructorInfo = TypeReference.GetConstructor(types);
        }
        else
        {
          // Get constructor with matching parameters.
          ConstructorInfo[] constructorInfos = TypeReference.GetConstructors(bindingFlags);
          foreach (ConstructorInfo constructorInfo in constructorInfos)
          {
            parameterInfos = constructorInfo.GetParameters();
            if (parameterInfos.Length != parameterNames?.Length)
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
          var builder = new StringBuilder(64);
          bool first = true;
          builder.Append($"{TypeReference.Name}(");
          string signature;

          // Create constructor parameter type array.
          List<Type> parameterTypeList = [];
          parameterInfos = selectedConstructorInfo.GetParameters();
          foreach (ParameterInfo parameterInfo in parameterInfos)
          {
            if (!first)
            {
              builder.Append(", ");
            }
            first = false;
            builder.Append(parameterInfo.ParameterType.Name);
            parameterTypeList.Add(parameterInfo.ParameterType);
          }
          builder.Append(')');
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

    // Set the FieldInfo reference.
    /// <include path="members/SetFieldInfo/*" file="Doc/LJCAssemblyReflect.xml"/>
    /// <parentGroup>reflectionProperty</parentGroup>
    public FieldInfo? SetFieldInfo(string fieldName)
    {
      FieldName = fieldName;

      FieldInfo = null;
      if (TypeReference != null
        && LJC.HasValue(FieldName))
      {
        FieldInfo = TypeReference.GetField(FieldName);
      }
      return FieldInfo;
    }

    // Gets the Indexer Property info.
    /// <include path="members/GetIndexerInfo/*" file="Doc/LJCAssemblyReflect.xml"/>
    /// <parentGroup>reflectionProperty</parentGroup>
    public PropertyInfo? GetIndexerInfo(string fullName)
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
            //retValue = TypeReference?.GetProperty(PropertyName, new Type[] { argType });
            retValue = TypeReference?.GetProperty(PropertyName, [argType]);
          }
        }
      }
      return retValue;
    }

    // Set the MethodInfo reference.
    /// <include path="members/SetMethodInfo/*" file="Doc/LJCAssemblyReflect.xml"/>
    /// <parentGroup>reflectionProperty</parentGroup>
    // ToDo: Refactor with new methods.
    public MethodInfo? SetMethodInfo(string methodName
      , string[]? parameterNames = null)
    {
      ParameterInfo[] parameterInfos;
      MethodInfo selectedMethodInfo = null;

      ConstructorInfo = null;
      MethodName = methodName;
      if (MethodName.Contains('`'))
      {
        //var startIndex = 0;
        //MethodName = NetString.GetStringWithDelimiters(MethodName
        //  , MethodName[0].ToString(), ref startIndex, "`");
        var parser = new LJCTextParser()
        {
          StartIndex = 0
        };
        MethodName = parser.StringWithDelimiters(MethodName, MethodName[0].ToString(), "`");
        //MethodName = MethodName?.Substring(0, MethodName.Length - 1);
        MethodName = MethodName?[..^1];
      }

      MethodInfo = null;
      if (TypeReference != null
        && LJC.HasValue(MethodName))
      {
        BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public
          | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly;

        if (null == parameterNames
          || (parameterNames != null
          && 0 == parameterNames.Length))
        {
          // Get method with no parameters.
          MethodInfo = TypeReference.GetMethod(MethodName, bindingFlags
            , null, Type.EmptyTypes, null);
        }
        else
        {
          // Get method with matching parameters.
          //MethodInfo[] methodInfos = TypeReference.GetMethods(bindingFlags).Where
          //  (x => x.Name.Equals(MethodName)).ToArray();
          MethodInfo[] methodInfos = [.. TypeReference.GetMethods(bindingFlags).Where
            (x => x.Name.Equals(MethodName))];
          foreach (MethodInfo methodInfo in methodInfos)
          {
            try
            {
              parameterInfos = methodInfo.GetParameters();
            }
            catch (SystemException) { continue; }
            if (parameterInfos.Length != parameterNames?.Length)
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
          var builder = new StringBuilder(64);
          bool first = true;
          builder.Append($"{MethodName}(");
          string signature = null;

          // Create method parameter type array.
          //List<Type> parameterTypeList = new List<Type>();
          var parameterTypeList = new List<Type>();
          parameterInfos = selectedMethodInfo.GetParameters();
          foreach (ParameterInfo parameterInfo in parameterInfos)
          {
            if (!first)
            {
              builder.Append(", ");
            }
            first = false;
            builder.Append(parameterInfo.ParameterType.Name);
            parameterTypeList.Add(parameterInfo.ParameterType);
          }
          builder.Append(')');
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

    // Set the PropertyInfo reference.
    /// <include path="members/SetPropertyInfo/*" file="Doc/LJCAssemblyReflect.xml"/>
    /// <parentGroup>reflectionProperty</parentGroup>
    public PropertyInfo? SetPropertyInfo(string propertyName
      , string? indexerSignature = null)
    {
      PropertyName = propertyName;

      PropertyInfo = null;
      if (TypeReference != null
        && LJC.HasValue(PropertyName))
      {
        if ("Item" == PropertyName)
        {
          if (LJC.HasValue(indexerSignature))
          {
            PropertyInfo = GetIndexerInfo(indexerSignature);
          }
        }
        else
        {
          PropertyInfo = TypeReference.GetProperty(PropertyName);
        }
      }
      return PropertyInfo;
    }

    // Set the Type reference.
    /// <include path="members/SetTypeReference/*" file="Doc/LJCAssemblyReflect.xml"/>
    /// <parentGroup>reflectionProperty</parentGroup>
    public Type? SetTypeReference(string typeName)
    {
      TypeName = typeName;

      TypeReference = null;
      MethodName = null;
      MethodInfo = null;
      if (Assembly != null
        && LJC.HasValue(TypeName))
      {
        TypeReference = Assembly.GetType(TypeName);
      }
      return TypeReference;
    }
    #endregion

    #region Get Syntax Methods

    // Creates and returns the Constructor syntax.
    /// <include path="members/GetConstructorSyntax/*" file="Doc/LJCAssemblyReflect.xml"/>
    /// <parentGroup>syntax</parentGroup>
    public string? GetConstructorSyntax(ConstructorInfo? constructorInfo = null)
    {
      ConstructorInfo info = constructorInfo;
      string retValue = null;

      if (null == info)
      {
        info = ConstructorInfo;
      }

      if (info != null)
      {
        var builder = new StringBuilder(128);

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
        builder.Append($"{TypeReference?.Name}(");

        // Parameters
        ParameterInfo[] parameterInfos = info.GetParameters();
        bool isFirst = true;
        foreach (ParameterInfo parameterInfo in parameterInfos)
        {
          if (!isFirst)
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

        builder.Append(')');
        retValue = builder.ToString();
      }
      return retValue;
    }

    // Creates and returns the Field syntax string.
    /// <include path="members/GetFieldSyntax/*" file="Doc/LJCAssemblyReflect.xml"/>
    /// <parentGroup>syntax</parentGroup>
    public string? GetFieldSyntax(FieldInfo? fieldInfo = null)
    {
      FieldInfo info = fieldInfo;
      string retValue = null;

      if (null == info)
      {
        info = FieldInfo;
      }

      if (info != null)
      {
        var builder = new StringBuilder(128);

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
    /// <include path="members/GetGenericTypeSyntax/*" file="Doc/LJCAssemblyReflect.xml"/>
    /// <parentGroup>syntax</parentGroup>
    public string? GetGenericTypeSyntax(Type? typeReference = null)
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
        var builder = new StringBuilder(128);
        int charIndex = type.Name.IndexOf('`');
        //builder.Append($"{type.Name.Substring(0, charIndex)}<");
        builder.Append($"{type.Name[..charIndex]}<");
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
          if (!isFirst)
          {
            builder.Append(", ");
          }
          isFirst = false;
          builder.Append(typeArgument.Name);
        }
        builder.Append('>');
        retValue = builder.ToString();
      }
      return retValue;
    }

    // Creates and returns the Method syntax.
    /// <include path="members/GetMethodSyntax/*" file="Doc/LJCAssemblyReflect.xml"/>
    /// <parentGroup>syntax</parentGroup>
    public string? GetMethodSyntax(MethodInfo? methodInfo = null)
    {
      MethodInfo info = methodInfo;
      string retValue = null;

      if (null == info)
      {
        info = MethodInfo;
      }

      if (info != null
        && !IsProperty(info))
      {
        var builder = new StringBuilder(128);

        // Access Modifier
        if (info.IsPrivate)
        {
          builder.Append("private ");
        }
        if (info.IsPublic)
        {
          builder.Append("public ");
        }
        if (info.IsStatic)
        {
          builder.Append("static ");
        }
        if (info.IsFamily)
        {
          builder.Append("protected ");
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
          if (!isFirst)
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

        builder.Append(')');
        retValue = builder.ToString();
      }
      return retValue;
    }

    // Creates and returns the Property syntax.
    /// <include path="members/GetPropertySyntax/*" file="Doc/LJCAssemblyReflect.xml"/>
    /// <parentGroup>syntax</parentGroup>
    public string? GetPropertySyntax(PropertyInfo? propertyInfo = null)
    {
      PropertyInfo info = propertyInfo;
      string retValue = null;

      if (null == info)
      {
        info = PropertyInfo;
      }

      if (info != null)
      {
        var builder = new StringBuilder(128);

        // Access Modifier
        MethodInfo methodInfo = info.GetMethod;

        if (methodInfo != null)
        {
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
        }

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
        if (parameterInfos.Length > 0)
        {
          ParameterInfo parameterInfo = parameterInfos[0];
          builder.Append($"{parameterInfo.ParameterType.Name} ");
          builder.Append(parameterInfo.Name);
        }

        if ("Item" == info.Name)
        {
          builder.Append(')');
        }
        retValue = builder.ToString();
      }
      return retValue;
    }

    // Creates and returns the Type syntax.
    /// <include path="members/GetTypeSyntax/*" file="Doc/LJCAssemblyReflect.xml"/>
    /// <parentGroup>syntax</parentGroup>
    public string? GetTypeSyntax(Type? typeReference = null)
    {
      Type type = typeReference;
      string retValue = null;

      if (null == type)
      {
        type = TypeReference;
      }

      if (type != null)
      {
        var builder = new StringBuilder(128);

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
        if (type.IsValueType && !type.IsEnum)
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
            if (!IsCommonClassification(baseType))
            {
              hasQualifiers = true;
              builder.Append($": {baseType.Name} ");
            }
          }
        }

        // Implemented Interfaces
        if (baseType != null
          && baseType.Name != "Enum")
        {
          foreach (Type implementedType in type.GetInterfaces())
          {
            if (!IsCommonInterface(implementedType))
            {
              if (!hasQualifiers)
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

    #region Check Type Methods

    // Indicates if the Method is not a property getter or setter.
    /// <include path="members/IsNotProperty/*" file="Doc/LJCAssemblyReflect.xml"/>
    /// <parentGroup>check</parentGroup>
    public static bool IsProperty(MethodInfo methodInfo)
    {
      bool retValue = false;

      if (methodInfo != null
        && (methodInfo.Name.StartsWith("get_")
        || methodInfo.Name.StartsWith("set_")))
      {
        retValue = true;
      }
      return retValue;
    }

    // Indicates if the method is "override".
    /// <include path="members/IsOverride/*" file="Doc/LJCAssemblyReflect.xml"/>
    /// <parentGroup>check</parentGroup>
    public static bool IsOverride(MethodInfo methodInfo)
    {
      return !methodInfo.Equals(methodInfo.GetBaseDefinition());
    }

    // Indicates if the method is "public".
    /// <include path="members/IsPublic/*" file="Doc/LJCAssemblyReflect.xml"/>
    /// <parentGroup>check</parentGroup>
    public bool IsPublic(MethodInfo? methodInfo = null)
    {
      MethodInfo info = methodInfo;
      bool retValue = false;

      if (null == info)
      {
        info = MethodInfo;
      }

      if (info != null)
      {
        if (!IsProperty(info))
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

    // Indicates if the Type is a common type.
    /// <include path="members/IsNotCommonClassification/*" file="Doc/LJCAssemblyReflect.xml"/>
    /// <parentGroup>check</parentGroup>
    private static bool IsCommonClassification(Type type)
    {
      bool retValue = false;

      if (type.Name == "Object"
        || type.Name == "Enum")
      {
        retValue = true;
      }
      return retValue;
    }

    // Indicates if the Interface is a common type.
    /// <include path="members/IsNotCommonInterface/*" file="Doc/LJCAssemblyReflect.xml"/>
    /// <parentGroup>check</parentGroup>
    private static bool IsCommonInterface(Type type)
    {
      bool retValue = false;

      if (type != null)
      {
        if (type.FullName!.StartsWith("System.ComponentModel")
          || type.FullName.StartsWith("System.ServiceModel")
          || type.FullName.StartsWith("System.Windows.Forms"))
        {
          retValue = true;
        }

        if (!retValue)
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
            retValue = true;
          }
        }
      }
      return retValue;
    }
    #endregion

    #region Properties

    // The Assembly reference.
    /// <include path="members/Assembly/*" file="Doc/LJCAssemblyReflect.xml"/>
    /// <parentGroup>properties</parentGroup>
    public Assembly? Assembly { get; set; }

    // The constructor information.
    /// <include path="members/ConstructorInfo/*" file="Doc/LJCAssemblyReflect.xml"/>
    /// <parentGroup>properties</parentGroup>
    public ConstructorInfo? ConstructorInfo { get; set; }

    // The Field information.
    /// <include path="members/FieldInfo/*" file="Doc/LJCAssemblyReflect.xml"/>
    /// <parentGroup>properties</parentGroup>
    public FieldInfo? FieldInfo { get; set; }

    // The Field name.
    /// <include path="members/FieldName/*" file="Doc/LJCAssemblyReflect.xml"/>
    /// <parentGroup>properties</parentGroup>
    public string? FieldName { get; set; }

    // The Assembly file name.
    /// <include path="members/FileSpec/*" file="Doc/LJCAssemblyReflect.xml"/>
    /// <parentGroup>properties</parentGroup>
    public string? FileSpec { get; set; }

    // The Method information.
    /// <include path="members/MethodInfo/*" file="Doc/LJCAssemblyReflect.xml"/>
    /// <parentGroup>properties</parentGroup>
    public MethodInfo? MethodInfo { get; set; }

    // The Method name.
    /// <include path="members/MethodName/*" file="Doc/LJCAssemblyReflect.xml"/>
    /// <parentGroup>properties</parentGroup>
    public string? MethodName { get; set; }

    // The Property information.
    /// <include path="members/PropertyInfo/*" file="Doc/LJCAssemblyReflect.xml"/>
    /// <parentGroup>properties</parentGroup>
    public PropertyInfo? PropertyInfo { get; set; }

    // The Property name.
    /// <include path="members/PropertyName/*" file="Doc/LJCAssemblyReflect.xml"/>
    /// <parentGroup>properties</parentGroup>
    public string? PropertyName { get; set; }

    // The Type name.
    /// <include path="members/TypeName/*" file="Doc/LJCAssemblyReflect.xml"/>
    /// <parentGroup>properties</parentGroup>
    public string? TypeName { get; set; }

    // The Type reference.
    /// <include path="members/ypeReference/*" file="Doc/LJCAssemblyReflect.xml"/>
    /// <parentGroup>properties</parentGroup>
    public Type? TypeReference { get; set; }
    #endregion
  }
}
