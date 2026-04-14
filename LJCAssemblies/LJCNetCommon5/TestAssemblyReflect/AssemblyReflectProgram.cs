// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// AssemblyReflectProgram.cs
using LJCNetCommon5;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.Serialization.Formatters;

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

      // Set Reflection Property Methods
      SetAssembly();
      SetConstructorInfo();
      SetFieldInfo();
      SetIndexerInfo();
      SetMethodInfo();
      SetPropertyInfo();
      SetTypeReference();

      // Methods
      HasField();
      HasMethod();
      HasParmMethod();
      GetParmMethodInfo();

      // Get Syntax Methods
      GetConstructorSyntax();
      GetFieldSyntax();
      GetGenericTypeSyntax();
      GetMethodSyntax();
      GetPropertySyntax();
      GetTypeSyntax();

      // Check Type Methods
      IsNotCommonClassification();
      IsNotCommonInterface();
      IsNotProperty();
      IsOverride();
      IsPublic();
    }

    #region Set Reflection Property Methods

    // Retrieves the Assembly reference.
    private static void SetAssembly()
    {
      // Initialize Assembly
      var reflect = new LJCAssemblyReflect();

      // Test Method
      var assembly = reflect.SetAssembly(FileSpec());
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
      // Initialize Assembly and Type
      var reflect = new LJCAssemblyReflect();
      reflect.SetAssembly(FileSpec());
      reflect.SetTypeReference(TypeName());

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
      // Initialize Assembly and Type
      var reflect = new LJCAssemblyReflect();
      reflect.SetAssembly(FileSpec());
      var typeReference = reflect.SetTypeReference(TypeName());

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
      // Initialize Assembly, Type and Property
      var reflect = new LJCAssemblyReflect();
      reflect.SetAssembly(FileSpec());
      reflect.SetTypeReference(TypeName());

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
      // Initialize Assembly, Type and Method
      var reflect = new LJCAssemblyReflect();
      reflect.SetAssembly(FileSpec());
      reflect.SetTypeReference(TypeName());

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
      // Initialize Assembly, Type and Property
      var reflect = new LJCAssemblyReflect();
      reflect.SetAssembly(FileSpec());
      reflect.SetTypeReference(TypeName());

      // Test Method
      var propertyInfo = reflect.SetPropertyInfo("LJCDefaultFileName");
      var result = propertyInfo?.Name;
      var compare = "LJCDefaultFileName";
      TestCommon?.Write("SetPropertyInfo()", result, compare);
    }

    // Set the Type reference.
    private static void SetTypeReference()
    {
      // Initialize Assembly
      var reflect = new LJCAssemblyReflect();
      reflect.SetAssembly(FileSpec());

      // Test Method
      var typeReference = reflect.SetTypeReference(TypeName());
      var result = typeReference?.Name;
      var compare = "LJCDataColumns";
      TestCommon?.Write("SetTypeReference()", result, compare);
    }
    #endregion

    #region Helper Methods

    // The test file spec.
    private static string FileSpec()
    {
      return "LJCNetCommon5.dll";
    }

    // The test type name.
    private static string TypeName()
    {
      return "LJCNetCommon5.LJCDataColumns";
    }
    #endregion

    #region Methods

    // Checks if the type has a specific field.
    private static void HasField()
    {
      // Initialize Assembly and Type
      var reflect = new LJCAssemblyReflect();
      reflect.SetAssembly(FileSpec());
      reflect.SetTypeReference(TypeName());

      string fieldName = "mTestField";
      var result = "False";
      var fieldInfo = reflect.GetFieldInfo(fieldName);
      if (fieldInfo != null)
      {
        result = "True";
      }
      var compare = "True";
      TestCommon?.Write("HasField()1", result, compare);

      result = "False";
      fieldInfo = reflect.SetFieldInfo(fieldName);
      if (fieldInfo != null)
      {
        result = "True";
      }
      compare = "True";
      TestCommon?.Write("HasField()2", result, compare);
    }

    // Checks if the type has a specifec parameterless method.
    private static void HasMethod()
    {
      // Initialize Assembly and Type
      var reflect = new LJCAssemblyReflect();
      reflect.SetAssembly(FileSpec());
      reflect.SetTypeReference(TypeName());

      var methodName = "Clone";
      var result = "False";
      var methodInfo = reflect.GetMethodInfo(methodName);
      if (methodInfo != null)
      {
        result = "True";
      }
      var compare = "True";
      TestCommon?.Write("HasMethod()1", result, compare);

      result = "False";
      methodInfo = reflect.SetMethodInfo(methodName);
      if (methodInfo != null)
      {
        result = "True";
      }
      compare = "True";
      TestCommon?.Write("HasMethod()2", result, compare);
    }

    // Checks if the type has a parameterized method.
    private static void HasParmMethod()
    {
      var result = "False";
      var methodInfo = GetParmMethodInfo();
      if (methodInfo != null)
      {
        result = "True";
      }
      var compare = "True";
      TestCommon?.Write("HasParmMethod()", result, compare);
    }

    // Gets a parameterized method info object.
    private static MethodInfo? GetParmMethodInfo()
    {
      // Initialize Assembly and Type
      var reflect = new LJCAssemblyReflect();
      reflect.SetAssembly(FileSpec());
      reflect.SetTypeReference(TypeName());

      var methodName = "Add";
      string[] parmNames =
      [
        "columnName",
        "position",
        "maxLength",
      ];
      var retMethodInfo = reflect.ParmMethodInfo(methodName, parmNames);
      return retMethodInfo;
    }
    #endregion

    #region Get Syntax Methods

    // Creates and returns the Constructor syntax.
    private static void GetConstructorSyntax()
    {
      // Initialize Assembly, Type and Constructor
      var reflect = new LJCAssemblyReflect();
      reflect.SetAssembly(FileSpec());
      reflect.SetTypeReference(TypeName());
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
      // Initialize Assembly, Type and Field
      var reflect = new LJCAssemblyReflect();
      reflect.SetAssembly(FileSpec());
      reflect.SetTypeReference(TypeName());
      reflect.SetFieldInfo("mTestField");

      // Test Method
      var result = reflect.GetFieldSyntax();
      var compare = "public Int32 mTestField";
      TestCommon?.Write("GetFieldSyntax()", result, compare);
    }

    // Creates and returns the Generic Type syntax.
    private static void GetGenericTypeSyntax()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetGenericTypeSyntax()", result, compare);
    }

    // Creates and returns the Method syntax.
    private static void GetMethodSyntax()
    {
      // Initialize Assembly, Type and Method
      var reflect = new LJCAssemblyReflect();
      reflect.SetAssembly(FileSpec());
      reflect.SetTypeReference(TypeName());
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
      // Initialize Assembly, Type and Property
      var reflect = new LJCAssemblyReflect();
      reflect.SetAssembly(FileSpec());
      reflect.SetTypeReference(TypeName());
      reflect.SetPropertyInfo("LJCDefaultFileName");

      // Test Method
      var result = reflect.GetPropertySyntax();
      var compare = "public static String LJCDefaultFileName";
      TestCommon?.Write("GetPropertySyntax()", result, compare);
    }

    // Creates and returns the Type syntax.
    private static void GetTypeSyntax()
    {
      // Initialize Assembly and Type
      var reflect = new LJCAssemblyReflect();
      reflect.SetAssembly(FileSpec());
      reflect.SetTypeReference(TypeName());

      // Test Method
      var result = reflect.GetTypeSyntax();
      var compare = "public class LJCDataColumns : List<LJCDataColumn>";
      TestCommon?.Write("GetTypeSyntax()", result, compare);
    }
    #endregion

    #region Check Type Methods

    // Indicates if the Type is not a common type.
    private static void IsNotCommonClassification()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("IsNotCommonClassification()", result, compare);
    }

    // Indicates if the Interface is not a common type.
    private static void IsNotCommonInterface()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("IsNotCommonInterface()", result, compare);
    }

    // Indicates if the Method is not a property getter or setter.
    private static void IsNotProperty()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("IsNotProperty()", result, compare);
    }

    // Indicates if the method is "override".
    private static void IsOverride()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("IsOverride()", result, compare);
    }

    // Indicates if the method is "public".
    private static void IsPublic()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("IsPublic()", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
