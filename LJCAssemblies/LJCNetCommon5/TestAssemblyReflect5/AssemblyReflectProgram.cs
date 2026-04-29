// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// AssemblyReflectProgram.cs
using LJCNetCommon5;

namespace TestAssemblyReflect5
{
  // The entry class.
  internal class AssemblyReflectProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCAssemblyReflect");
      Console.WriteLine();
      Console.WriteLine("*** LJCAssemblyReflect ***");

      // Static Methods
      ParmNames();
      ParmTypeNames();

      // Methods
      GetFieldInfo();
      GetMethodInfo();
      ParmMethodInfo();
      ParmMethodInfosWithCount();
      ParmMethodInfoWithParms();

      // Set Reflection Property Methods
      SetAssembly();
      SetConstructorInfo();
      SetFieldInfo();
      SetIndexerInfo();
      SetMethodInfo();
      SetPropertyInfo();
      SetTypeReference();

      // Get Syntax Methods
      GetConstructorSyntax();
      GetFieldSyntax();
      GetGenericTypeSyntax();
      GetMethodSyntax();
      GetPropertySyntax();
      GetTypeSyntax();

      // Check Type Methods
      IsProperty();
      IsOverride();
      IsPublic();

      Console.WriteLine();
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }

    #region Static Methods

    // Gets parm names from method info.
    private static void ParmNames()
    {
      var assemblyName = "LJCNetCommon5.dll";
      var typeName = "LJCNetCommon5.LJCDataColumns";

      // Initialize Assembly and Type
      var reflect = new LJCAssemblyReflect();
      reflect.SetAssembly(assemblyName);
      reflect.SetTypeReference(typeName);

      var methodName = "Add";
      string[] parmNames =
      [
        "columnName",
        "position",
        "maxLength",
      ];
      var methodInfo = reflect.ParmMethodInfo(methodName, parmNames);

      var result = "False";
      if (methodInfo != null)
      {
        // Test Method
        var names = LJCAssemblyReflect.ParmNames(methodInfo);
        if (LJC.HasElements(names))
        {
          result = "True";
        }
      }
      var compare = "True";
      TestCommon?.Write("ParmNames()", result, compare);
    }

    // Gets parm method type names with parm names.
    private static void ParmTypeNames()
    {
      var assemblyName = "LJCNetCommon5.dll";
      var typeName = "LJCNetCommon5.LJCDataColumns";

      // Initialize Assembly and Type
      var reflect = new LJCAssemblyReflect();
      reflect.SetAssembly(assemblyName);
      reflect.SetTypeReference(typeName);

      var methodName = "Add";
      string[] parmNames =
      [
        "columnName",
        "position",
        "maxLength",
      ];
      var methodInfo = reflect.ParmMethodInfo(methodName, parmNames);

      var result = "False";
      if (methodInfo != null)
      {
        // Test Method
        var typeNames = LJCAssemblyReflect.ParmTypeNames(methodInfo);
        if (LJC.HasElements(typeNames))
        {
          result = "True";
        }
      }
      var compare = "True";
      TestCommon?.Write("ParmTypeNames()", result, compare);
    }
    #endregion

    #region Methods

    // Gets a field info object.
    private static void GetFieldInfo()
    {
      var assemblyName = "LJCNetCommon5.dll";
      var typeName = "LJCNetCommon5.LJCDataColumns";

      // Initialize Assembly and Type
      var reflect = new LJCAssemblyReflect();
      reflect.SetAssembly(assemblyName);
      reflect.SetTypeReference(typeName);

      // Test Method
      string fieldName = "mTestField";
      var result = "False";
      var fieldInfo = reflect.GetFieldInfo(fieldName);
      if (fieldInfo != null)
      {
        result = "True";
      }
      var compare = "True";
      TestCommon?.Write("GetFieldInfo()", result, compare);
    }

    // Gets a parameterless method info object.
    private static void GetMethodInfo()
    {
      var assemblyName = "LJCNetCommon5.dll";
      var typeName = "LJCNetCommon5.LJCDataColumns";

      // Initialize Assembly and Type
      var reflect = new LJCAssemblyReflect();
      reflect.SetAssembly(assemblyName);
      reflect.SetTypeReference(typeName);

      // Test Method
      var methodName = "Clone";
      var result = "False";
      var methodInfo = reflect.GetMethodInfo(methodName);
      if (methodInfo != null)
      {
        result = "True";
      }
      var compare = "True";
      TestCommon?.Write("GetMethodInfo()", result, compare);
    }

    // Gets a parameterized method info object.
    private static void ParmMethodInfo()
    {
      var assemblyName = "LJCNetCommon5.dll";
      var typeName = "LJCNetCommon5.LJCDataColumns";

      // Initialize Assembly and Type
      var reflect = new LJCAssemblyReflect();
      reflect.SetAssembly(assemblyName);
      reflect.SetTypeReference(typeName);

      var methodName = "Add";
      string[] parmNames =
      [
        "columnName",
        "position",
        "maxLength",
      ];

      // Test Method
      var methodInfo = reflect.ParmMethodInfo(methodName, parmNames);
      var result = "False";
      if (methodInfo != null)
      {
        result = "True";
      }
      var compare = "True";
      TestCommon?.Write("ParmMethodInfo()", result, compare);
    }

    // Gets parm method infos with method name and parm count.
    private static void ParmMethodInfosWithCount()
    {
      var assemblyName = "LJCNetCommon5.dll";
      var typeName = "LJCNetCommon5.LJCDataColumns";

      // Initialize Assembly and Type
      var reflect = new LJCAssemblyReflect();
      reflect.SetAssembly(assemblyName);
      reflect.SetTypeReference(typeName);

      var methodName = "Add";
      var count = 3;

      // Test Method
      var methodInfos = reflect.ParmMethodInfosWithCount(methodName, count);
      var result = "False";
      if (methodInfos != null)
      {
        result = "True";
      }
      var compare = "True";
      TestCommon?.Write("ParmMethodInfosWithCount()", result, compare);
    }

    // Gets parm method info with parm names.
    private static void ParmMethodInfoWithParms()
    {
      var assemblyName = "LJCNetCommon5.dll";
      var typeName = "LJCNetCommon5.LJCDataColumns";

      // Initialize Assembly and Type
      var reflect = new LJCAssemblyReflect();
      reflect.SetAssembly(assemblyName);
      reflect.SetTypeReference(typeName);

      var methodName = "Add";
      string[] parmNames =
      [
        "columnName",
        "position",
        "maxLength",
      ];

      // Test Method
      var methodInfo = reflect.ParmMethodInfoWithParms(methodName, parmNames);
      var result = "False";
      if (methodInfo != null)
      {
        result = "True";
      }
      var compare = "True";
      TestCommon?.Write("ParmMethodInfoWithParms()", result, compare);
    }
    #endregion

    #region Set Reflection Property Methods

    // Retrieves the Assembly reference.
    private static void SetAssembly()
    {
      var assemblyName = "LJCNetCommon5.dll";

      // Initialize Assembly
      var reflect = new LJCAssemblyReflect();

      // Test Method
      var assembly = reflect.SetAssembly(assemblyName);
      var value = assembly?.FullName;
      var tokens = LJCNetString.Split(value);
      string result = null;
      if (LJC.HasElements(tokens))
      {
        result = tokens[0];
      }
      var compare = "LJCNetCommon5";
      TestCommon?.Write("SetAssembly()", result, compare);
    }

    // Set the ConstructorInfo reference.
    private static void SetConstructorInfo()
    {
      var assemblyName = "LJCNetCommon5.dll";
      var typeName = "LJCNetCommon5.LJCDataColumns";

      // Initialize Assembly and Type
      var reflect = new LJCAssemblyReflect();
      reflect.SetAssembly(assemblyName);
      reflect.SetTypeReference(typeName);

      // Test Method
      // Get default constructor.
      //var cstrInfo = reflect.SetConstructorInfo("LJCNetCommon5", null);
      var cstrInfo = reflect.SetConstructorInfo("LJCDataColumns", null);
      var result = cstrInfo?.DeclaringType?.Name;
      var compare = "LJCDataColumns";
      TestCommon?.Write("SetConstructorInfo()1", result, compare);

      // Test Method
      // Get constructor with "items" parameter.
      string[] parms =
      [
        "items",
      ];
      //cstrInfo = reflect.SetConstructorInfo("LJCNetCommon5", parms);
      cstrInfo = reflect.SetConstructorInfo("LJCDataColumns", parms);
      result = cstrInfo?.DeclaringType?.Name;
      compare = "LJCDataColumns";
      TestCommon?.Write("SetConstructorInfo()2", result, compare);
    }

    // Set the FieldInfo reference.
    private static void SetFieldInfo()
    {
      var assemblyName = "LJCNetCommon5.dll";
      var typeName = "LJCNetCommon5.LJCDataColumns";

      // Initialize Assembly and Type
      var reflect = new LJCAssemblyReflect();
      reflect.SetAssembly(assemblyName);
      var typeReference = reflect.SetTypeReference(typeName);

      string result = null;
      string compare = null;
      var fieldInfos = typeReference?.GetFields();
      if (LJC.HasElements(fieldInfos)
        && fieldInfos.Any(x => x.Name == "mTestField"))
      {
        var fieldInfo = typeReference?.GetField("mTestField");
        if (fieldInfo != null)
        {
          // Test Method
          reflect.SetFieldInfo(fieldInfo.Name);
          result = reflect.FieldInfo?.Name;
          compare = "mTestField";
        }
      }
      TestCommon?.Write("SetFieldInfo()", result, compare);
    }

    // Gets the Indexer Property info.
    private static void SetIndexerInfo()
    {
      var assemblyName = "LJCNetCommon5.dll";
      var typeName = "LJCNetCommon5.LJCDataColumns";

      // Initialize Assembly, Type and Property
      var reflect = new LJCAssemblyReflect();
      reflect.SetAssembly(assemblyName);
      reflect.SetTypeReference(typeName);

      // Test Method
      string indexerName = "Item";
      string indexerSignature = "Item(System.String)";
      var propertyInfo = reflect.SetPropertyInfo(indexerName
        , indexerSignature);
      var result = propertyInfo?.Name;
      var compare = "Item";
      TestCommon?.Write("SetIndexerInfo()", result, compare);
    }

    // Set the MethodInfo reference.
    private static void SetMethodInfo()
    {
      var assemblyName = "LJCNetCommon5.dll";
      var typeName = "LJCNetCommon5.LJCDataColumns";

      // Initialize Assembly, Type and Method
      var reflect = new LJCAssemblyReflect();
      reflect.SetAssembly(assemblyName);
      reflect.SetTypeReference(typeName);

      // Test Method
      var methodInfo = reflect.SetMethodInfo("HasItems");
      var result = methodInfo?.Name;
      var compare = "HasItems";
      TestCommon?.Write("SetMethodInfo()1", result, compare);

      // Test Method
      string[] parms =
      [
        "propertyName",
      ];
      methodInfo = reflect.SetMethodInfo("LJCGetBoolean", parms);
      result = methodInfo?.Name;
      compare = "LJCGetBoolean";
      TestCommon?.Write("SetMethodInfo()2", result, compare);
    }

    // Set the PropertyInfo reference.
    private static void SetPropertyInfo()
    {
      var assemblyName = "LJCNetCommon5.dll";
      var typeName = "LJCNetCommon5.LJCDataColumns";

      // Initialize Assembly, Type and Property
      var reflect = new LJCAssemblyReflect();
      reflect.SetAssembly(assemblyName);
      reflect.SetTypeReference(typeName);

      // Test Method
      var propertyInfo = reflect.SetPropertyInfo("LJCDefaultFileName");
      var result = propertyInfo?.Name;
      var compare = "LJCDefaultFileName";
      TestCommon?.Write("SetPropertyInfo()", result, compare);
    }

    // Set the Type reference.
    private static void SetTypeReference()
    {
      var assemblyName = "LJCNetCommon5.dll";
      var typeName = "LJCNetCommon5.LJCDataColumns";

      // Initialize Assembly
      var reflect = new LJCAssemblyReflect();
      reflect.SetAssembly(assemblyName);

      // Test Method
      var typeReference = reflect.SetTypeReference(typeName);
      var result = typeReference?.Name;
      var compare = "LJCDataColumns";
      TestCommon?.Write("SetTypeReference()", result, compare);
    }
    #endregion

    #region Get Syntax Methods

    // Creates and returns the Constructor syntax.
    private static void GetConstructorSyntax()
    {
      var assemblyName = "LJCNetCommon5.dll";
      var typeName = "LJCNetCommon5.LJCDataColumns";

      // Initialize Assembly, Type and Constructor
      var reflect = new LJCAssemblyReflect();
      reflect.SetAssembly(assemblyName);
      reflect.SetTypeReference(typeName);
      // Get constructor with "items" parameter.
      string[] parms =
      [
        "items",
      ];
      reflect.SetConstructorInfo("LJCDataColumns", parms);

      // Test Method
      var result = reflect.GetConstructorSyntax();
      var compare = "public LJCDataColumns(LJCDataColumns items)";
      TestCommon?.Write("GetConstructorSyntax()", result, compare);
    }

    // Creates and returns the Field syntax string.
    private static void GetFieldSyntax()
    {
      var assemblyName = "LJCNetCommon5.dll";
      var typeName = "LJCNetCommon5.LJCDataColumns";

      // Initialize Assembly, Type and Field
      var reflect = new LJCAssemblyReflect();
      reflect.SetAssembly(assemblyName);
      reflect.SetTypeReference(typeName);
      reflect.SetFieldInfo("mTestField");

      // Test Method
      var result = reflect.GetFieldSyntax();
      var compare = "public Int32 mTestField";
      TestCommon?.Write("GetFieldSyntax()", result, compare);
    }

    // Creates and returns the Generic Type syntax.
    private static void GetGenericTypeSyntax()
    {
      var assemblyName = "LJCNetCommon5.dll";
      var typeName = "LJCNetCommon5.LJCDataColumns";

      // Initialize Assembly, Type and Method
      var reflect = new LJCAssemblyReflect();
      reflect.SetAssembly(assemblyName);
      reflect.SetTypeReference(typeName);

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetGenericTypeSyntax()", result, compare);
    }

    // Creates and returns the Method syntax.
    private static void GetMethodSyntax()
    {
      var assemblyName = "LJCNetCommon5.dll";
      var typeName = "LJCNetCommon5.LJCDataColumns";

      // Initialize Assembly, Type and Method
      var reflect = new LJCAssemblyReflect();
      reflect.SetAssembly(assemblyName);
      reflect.SetTypeReference(typeName);
      string[] parms =
      [
        "propertyName",
      ];
      reflect.SetMethodInfo("LJCGetBoolean", parms);

      // Test Method
      var result = reflect.GetMethodSyntax();
      var compare = "public Boolean LJCGetBoolean(String propertyName)";
      TestCommon?.Write("GetMethodSyntax()", result, compare);
    }

    // Creates and returns the Property syntax.
    private static void GetPropertySyntax()
    {
      var assemblyName = "LJCNetCommon5.dll";
      var typeName = "LJCNetCommon5.LJCDataColumns";

      // Initialize Assembly, Type and Property
      var reflect = new LJCAssemblyReflect();
      reflect.SetAssembly(assemblyName);
      reflect.SetTypeReference(typeName);
      reflect.SetPropertyInfo("LJCDefaultFileName");

      // Test Method
      var result = reflect.GetPropertySyntax();
      var compare = "public static String LJCDefaultFileName";
      TestCommon?.Write("GetPropertySyntax()", result, compare);
    }

    // Creates and returns the Type syntax.
    private static void GetTypeSyntax()
    {
      var assemblyName = "LJCNetCommon5.dll";
      var typeName = "LJCNetCommon5.LJCDataColumns";

      // Initialize Assembly and Type
      var reflect = new LJCAssemblyReflect();
      reflect.SetAssembly(assemblyName);
      reflect.SetTypeReference(typeName);

      // Test Method
      var result = reflect.GetTypeSyntax();
      var compare = "public class LJCDataColumns : List<LJCDataColumn>";
      TestCommon?.Write("GetTypeSyntax()", result, compare);
    }
    #endregion

    #region Check Type Methods

    // Indicates if the Method is a property getter or setter.
    private static void IsProperty()
    {
      var assemblyName = "LJCNetCommon5.dll";
      var typeName = "LJCNetCommon5.LJCDataColumns";

      // Initialize Assembly, Type and Property
      var reflect = new LJCAssemblyReflect();
      reflect.SetAssembly(assemblyName);
      reflect.SetTypeReference(typeName);

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("IsProperty()", result, compare);
    }

    // Indicates if the method is "override".
    private static void IsOverride()
    {
      var assemblyName = "LJCNetCommon5.dll";
      var typeName = "LJCNetCommon5.LJCDataColumns";

      // Initialize Assembly and Type
      var reflect = new LJCAssemblyReflect();
      reflect.SetAssembly(assemblyName);
      reflect.SetTypeReference(typeName);

      var methodName = "ToString";
      var methodInfo = reflect.GetMethodInfo(methodName);

      // Test Method
      var result = "False";
      if (methodInfo != null)
      {
        var value = LJCAssemblyReflect.IsOverride(methodInfo);
        result = value.ToString();
      }
      var compare = "True";
      TestCommon?.Write("IsOverride()", result, compare);
    }

    // Indicates if the method is "public".
    private static void IsPublic()
    {
      var assemblyName = "LJCNetCommon5.dll";
      var typeName = "LJCNetCommon5.LJCDataColumns";

      // Initialize Assembly and Type
      var reflect = new LJCAssemblyReflect();
      reflect.SetAssembly(assemblyName);
      reflect.SetTypeReference(typeName);

      var methodName = "Clone";
      var methodInfo = reflect.GetMethodInfo(methodName);

      // Test Method
      var result = "False";
      if (methodInfo != null)
      {
        var value = reflect.IsPublic(methodInfo);
        result = value.ToString();
      }
      var compare = "True";
      TestCommon?.Write("IsPublic()", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
