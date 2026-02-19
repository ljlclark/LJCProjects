// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCParse.cs

namespace LJCDocDataGenLib
{
  // Provides methods to parse source code for class, method and property
  // definitions.
  /// <include path="members/LJCParse/*" file="Doc/LJCParse.xml"/>
  internal class LJCParse
  {
    #region Constructor Methods

    // Initializes an object instance.
    /// <include path="members/Constructor/*" file="Doc/LJCParse.xml"/>
    public LJCParse()
    {
      SetModifiers();
    }
    #endregion

    #region Public Methods

    // Checks if a class definition.
    /// <include path="members/IsClass/*" file="Doc/LJCParse.xml"/>
    public string ClassName(string[] tokens)
    {
      string retValue = null;

      // class
      if (tokens.Length > 1)
      {
        if ("class" == tokens[0])
        {
          retValue = tokens[1];
        }
      }

      // public class
      if (null == retValue
        && tokens.Length > 2)
      {
        if (IsAccess(tokens[0])
          || IsAccess2(tokens[0]))
        {
          if ("class" == tokens[1])
          {
            retValue = tokens[2];
          }
        }
      }

      // public abstract class
      if (null == retValue
        && tokens.Length > 3)
      {
        if (IsAccess(tokens[0])
          && IsAccessClass2(tokens[1]))
        {
          if ("class" == tokens[2])
          {
            retValue = tokens[3];
          }
        }
      }
      return retValue;
    }

    // Checks if a method definition.
    /// <include path="members/IsMethod/*" file="Doc/LJCParse.xml"/>
    public bool IsMethod(string[] tokens)
    {
      var retValue = false;

      // [modifier] [modifier2] method()
      // if (returnsBool());
      // var name = method();

      // Method();
      if (tokens.Length > 0)
      {
        if (tokens[0].Contains("("))
        {
          retValue = true;
        }
      }

      // public Method()
      if (!retValue
        && tokens.Length > 1)
      {
        if (IsAccess(tokens[0])
          || IsAccess2(tokens[0]))
        {
          if (tokens[1].Contains("("))
          {
            retValue = true;
          }
        }
      }

      // public static Method()
      if (!retValue
        && tokens.Length > 2)
      {
        if (IsAccess(tokens[0])
          && IsAccess2(tokens[1]))
        {
          if (tokens[2].Contains("("))
          {
            retValue = true;
          }
        }
      }
      return retValue;
    }

    // Checks if a property definition.
    /// <include path="members/IsProperty/*" file="Doc/LJCParse.xml"/>
    public bool IsProperty(string[] tokens)
    {
      var retValue = false;

      // string Property
      if (tokens.Length > 1)
      {
        if (IsType(tokens[0])
          && !tokens[1].Contains("("))
        {
          retValue = true;
        }
      }

      // public string Property
      if (!retValue
        && tokens.Length > 2)
      {
        if (IsAccess(tokens[0])
          || IsAccess2(tokens[0]))
        {
          if (IsType(tokens[1])
            && !tokens[2].Contains("("))
          {
            retValue = true;
          }
        }
      }

      // public static string Property
      if (!retValue
        && tokens.Length > 3)
      {
        if (IsAccess(tokens[0])
          && IsAccess2(tokens[1]))
        {
          if (IsType(tokens[2])
            && !tokens[3].Contains("("))
          {
            retValue = true;
          }
        }
      }
      return retValue;
    }
    #endregion

    #region Private Methods

    // Checks if token is access modifier.
    private bool IsAccess(string token)
    {
      var retValue = false;

      foreach (var access in Access)
      {
        if (token == access)
        {
          retValue = true;
          break;
        }
      }
      return retValue;
    }

    // Checks if token is access2 modifier.
    private bool IsAccess2(string token)
    {
      var retValue = false;

      foreach (var access in Access2)
      {
        if (token == access)
        {
          retValue = true;
          break;
        }
      }
      return retValue;
    }

    // Checks if token is AccessClass2 modifier.
    private bool IsAccessClass2(string token)
    {
      var retValue = false;

      foreach (var access in AccessClass2)
      {
        if (token == access)
        {
          retValue = true;
          break;
        }
      }
      return retValue;
    }

    // Checks if the token is a data type.
    private bool IsType(string token)
    {
      var retValue = false;

      foreach (var dataType in DataType)
      {
        if (token == dataType)
        {
          retValue = true;
          break;
        }
      }
      return retValue;
    }

    // Sets the modifier properties.
    private void SetModifiers()
    {
      Access = new string[]
      {
        "internal",
        "private",
        "protected",
        "public",
      };

      // protected internal
      // private protected
      Access2 = new string[]
      {
        "internal",
        "override",
        "protected",
        "static",
        "virtual",
      };

      AccessClass2 = new string[]
      {
        "abstract",
        "sealed",
      };

      // In common usage order.
      DataType = new string[]
      {
        "string",
        "bool",
        "int",
        "long",
        "short",
        "String",
        "Boolean",
        "Int32",
        "Int64",
        "Int16",
        "char",
        "byte",
        "decimal",
        "double",
        "float",
        "Char",
        "Byte",
        "Decimal",
        "Double",
        "Single",
        "enum",
        "DateTime",
        "struct",
        "uint",
        "ulong",
        "ushort",
        "UInt32",
        "UInt64",
        "UInt16",
        "sbyte",
        "SByte",
      };
    }
    #endregion

    #region Properties

    // The access modifiers.
    private string[] Access { get; set; }

    // The second access modifier.
    public string[] Access2 { get; set; }

    // The second class access modifier.
    public string[] AccessClass2 { get; set; }

    // The data types.
    public string[] DataType { get; set; }
    #endregion
  }
}
