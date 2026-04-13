// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// AssemblyReflectProgram.cs
using LJCNetCommon5;
using System.Reflection;
using System.Reflection.Metadata;

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

    #region Methods

    private static string FileSpec()
    {
      return "LJCNetCommon5.dll";
    }

    private static string TypeName()
    {
      return "LJCNetCommon5.LJCDataColumns";
    }
    #endregion

    #region Set Reflection Property Methods

    // Retrieves the Assembly reference.
    private static void SetAssembly()
    {
      // Initialize Assembly
      var assmReflect = new LJCAssemblyReflect();

      // Test Method
      var assembly = assmReflect.SetAssembly(FileSpec());
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
      var assmReflect = new LJCAssemblyReflect();
      assmReflect.SetAssembly(FileSpec());
      assmReflect.SetTypeReference(TypeName());

      // Test Method
      // Get default constructor.
      //var cstrInfo = assmReflect.SetConstructorInfo("LJCNetCommon5", null);
      var cstrInfo = assmReflect.SetConstructorInfo("LJCDataColumns", null);
      var result = cstrInfo?.DeclaringType?.Name;
      var compare = "LJCDataColumns";
      TestCommon?.Write("SetConstructorInfo()1", result, compare);

      // Test Method
      // Get constructor with "items" parameter.
      string[] parms =
      [
        "items",
      ];
      //cstrInfo = assmReflect.SetConstructorInfo("LJCNetCommon5", parms);
      cstrInfo = assmReflect.SetConstructorInfo("LJCDataColumns", parms);
      result = cstrInfo?.DeclaringType?.Name;
      compare = "LJCDataColumns";
      TestCommon?.Write("SetConstructorInfo()2", result, compare);
    }

    // Set the FieldInfo reference.
    private static void SetFieldInfo()
    {
      // Initialize Assembly and Type
      var assmReflect = new LJCAssemblyReflect();
      assmReflect.SetAssembly(FileSpec());
      var typeReference = assmReflect.SetTypeReference(TypeName());

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
          assmReflect.SetFieldInfo(fieldInfo.Name);
          result = assmReflect.FieldInfo?.Name;
          compare = "mTestField";
        }
      }
      TestCommon?.Write("SetFieldInfo()", result, compare);
    }

    // Gets the Indexer Property info.
    private static void SetIndexerInfo()
    {
      // Initialize Assembly, Type and Property
      var assmReflect = new LJCAssemblyReflect();
      assmReflect.SetAssembly(FileSpec());
      assmReflect.SetTypeReference(TypeName());

      // Test Method
      string indexerName = "Item";
      string indexerSignature = "Item(System.String)";
      var propertyInfo = assmReflect.SetPropertyInfo(indexerName
        , indexerSignature);
      var result = propertyInfo?.Name;
      var compare = "Item";
      TestCommon?.Write("SetIndexerInfo()", result, compare);
    }

    // Set the MethodInfo reference.
    private static void SetMethodInfo()
    {
      // Initialize Assembly, Type and Method
      var assmReflect = new LJCAssemblyReflect();
      assmReflect.SetAssembly(FileSpec());
      assmReflect.SetTypeReference(TypeName());

      // Test Method
      var methodInfo = assmReflect.SetMethodInfo("HasItems");
      var result = methodInfo?.Name;
      var compare = "HasItems";
      TestCommon?.Write("SetMethodInfo()1", result, compare);

      // Test Method
      string[] parms =
      [
        "propertyName",
      ];
      methodInfo = assmReflect.SetMethodInfo("LJCGetBoolean", parms);
      result = methodInfo?.Name;
      compare = "LJCGetBoolean";
      TestCommon?.Write("SetMethodInfo()2", result, compare);
    }

    // Set the PropertyInfo reference.
    private static void SetPropertyInfo()
    {
      // Initialize Assembly, Type and Property
      var assmReflect = new LJCAssemblyReflect();
      assmReflect.SetAssembly(FileSpec());
      assmReflect.SetTypeReference(TypeName());

      // Test Method
      var propertyInfo = assmReflect.SetPropertyInfo("LJCDefaultFileName");
      var result = propertyInfo?.Name;
      var compare = "LJCDefaultFileName";
      TestCommon?.Write("SetPropertyInfo()", result, compare);
    }

    // Set the Type reference.
    private static void SetTypeReference()
    {
      // Initialize Assembly
      var assmReflect = new LJCAssemblyReflect();
      assmReflect.SetAssembly(FileSpec());

      // Test Method
      var typeReference = assmReflect.SetTypeReference(TypeName());
      var result = typeReference?.Name;
      var compare = "LJCDataColumns";
      TestCommon?.Write("SetTypeReference()", result, compare);
    }
    #endregion

    #region Get Syntax Methods

    // Creates and returns the Constructor syntax.
    private static void GetConstructorSyntax()
    {
      // Initialize Assembly, Type and Constructor
      var assmReflect = new LJCAssemblyReflect();
      assmReflect.SetAssembly(FileSpec());
      assmReflect.SetTypeReference(TypeName());
      // Get constructor with "items" parameter.
      string[] parms =
      [
        "items",
      ];
      assmReflect.SetConstructorInfo("LJCDataColumns", parms);

      // Test Method
      var result = assmReflect.GetConstructorSyntax();
      var compare = "public LJCDataColumns(LJCDataColumns items)";
      TestCommon?.Write("GetConstructorSyntax()", result, compare);
    }

    // Creates and returns the Field syntax string.
    private static void GetFieldSyntax()
    {
      // Initialize Assembly, Type and Field
      var assmReflect = new LJCAssemblyReflect();
      assmReflect.SetAssembly(FileSpec());
      assmReflect.SetTypeReference(TypeName());
      assmReflect.SetFieldInfo("mTestField");

      // Test Method
      var result = assmReflect.GetFieldSyntax();
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
      var assmReflect = new LJCAssemblyReflect();
      assmReflect.SetAssembly(FileSpec());
      assmReflect.SetTypeReference(TypeName());
      string[] parms =
      [
        "propertyName",
      ];
      assmReflect.SetMethodInfo("LJCGetBoolean", parms);

      // Test Method
      var result = assmReflect.GetMethodSyntax();
      var compare = "public Boolean LJCGetBoolean(String propertyName)";
      TestCommon?.Write("GetMethodSyntax()", result, compare);
    }

    // Creates and returns the Property syntax.
    private static void GetPropertySyntax()
    {
      // Initialize Assembly, Type and Property
      var assmReflect = new LJCAssemblyReflect();
      assmReflect.SetAssembly(FileSpec());
      assmReflect.SetTypeReference(TypeName());
      assmReflect.SetPropertyInfo("LJCDefaultFileName");

      // Test Method
      var result = assmReflect.GetPropertySyntax();
      var compare = "public static String LJCDefaultFileName";
      TestCommon?.Write("GetPropertySyntax()", result, compare);
    }

    // Creates and returns the Type syntax.
    private static void GetTypeSyntax()
    {
      // Initialize Assembly and Type
      var assmReflect = new LJCAssemblyReflect();
      assmReflect.SetAssembly(FileSpec());
      assmReflect.SetTypeReference(TypeName());

      // Test Method
      var result = assmReflect.GetTypeSyntax();
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
