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

    #region Set Reflection Property Methods

    // Retrieves the Assembly reference.
    private static void SetAssembly()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("SetAssembly()", result, compare);
    }

    // Set the ConstructorInfo reference.
    private static void SetConstructorInfo()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("SetConstructorInfo()", result, compare);
    }

    // Set the FieldInfo reference.
    private static void SetFieldInfo()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("SetFieldInfo()", result, compare);
    }

    // Gets the Indexer Property info.
    private static void SetIndexerInfo()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("SetIndexerInfo()", result, compare);
    }

    // Set the MethodInfo reference.
    private static void SetMethodInfo()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("SetMethodInfo()", result, compare);
    }

    // Set the PropertyInfo reference.
    private static void SetPropertyInfo()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("SetPropertyInfo", result, compare);
    }

    // Set the Type reference.
    private static void SetTypeReference()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("SetTypeReference()", result, compare);
    }
    #endregion

    #region Get Syntax Methods

    // Creates and returns the Constructor syntax.
    private static void GetConstructorSyntax()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetConstructorSyntax()", result, compare);
    }

    // Creates and returns the Field syntax string.
    private static void GetFieldSyntax()
    {
      var result = "Not Implemented";
      var compare = "";
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
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetMethodSyntax()", result, compare);
    }

    // Creates and returns the Property syntax.
    private static void GetPropertySyntax()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetPropertySyntax()", result, compare);
    }

    // Creates and returns the Type syntax.
    private static void GetTypeSyntax()
    {
      var result = "Not Implemented";
      var compare = "";
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
