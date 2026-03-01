// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCParse.cs

using LJCNetCommon;

namespace LJCDocDataGenLib
{
  // Provides methods to parse source code for class, method and property
  // definitions.
  /// <include path="members/LJCParse/*" file="Doc/LJCParse.xml"/>
  internal class LJCCodeParse
  {
    #region Constructor Methods

    // Initializes an object instance.
    /// <include path="members/Constructor/*" file="Doc/LJCParse.xml"/>
    public LJCCodeParse()
    {
      SetModifiers();
    }
    #endregion

    #region Public Methods

    // Checks if a class definition.
    /// <include path="members/ClassName/*" file="Doc/LJCParse.xml"/>
    public string ClassName(string[] tokens)
    {
      string retName = null;

      // class
      if (tokens.Length > 1)
      {
        if ("class" == tokens[0])
        {
          retName = tokens[1];
        }
      }

      // public class
      if (null == retName
        && tokens.Length > 2)
      {
        if (IsAccess(tokens[0])
          || IsAccess2(tokens[0]))
        {
          if ("class" == tokens[1])
          {
            retName = tokens[2];
          }
        }
      }

      // public abstract class
      if (null == retName
        && tokens.Length > 3)
      {
        if (IsAccess(tokens[0])
          && IsAccessClass2(tokens[1]))
        {
          if ("class" == tokens[2])
          {
            retName = tokens[3];
          }
        }
      }
      return retName;
    }

    // Checks if a method definition.
    /// <include path="members/MethodName/*" file="Doc/LJCParse.xml"/>
    public string MethodName(string[] tokens)
    {
      string retName = null;

      // [modifier] [modifier2] method()
      // if (returnsBool());
      // var name = method();

      // void Method()
      if (!NetString.HasValue(retName)
        && tokens.Length > 1)
      {
        if (IsType(tokens[0]))
        {
          if (tokens[1].Contains("("))
          {
            retName = ScrubMethodName(tokens[1]);
          }
        }
      }

      // static void Method()
      if (!NetString.HasValue(retName)
        && tokens.Length > 2)
      {
        if ((IsAccess(tokens[0])
          || IsAccess2(tokens[0]))
          && IsType(tokens[1]))
        {
          if (tokens[2].Contains("("))
          {
            retName = ScrubMethodName(tokens[2]);
          }
        }
      }

      // public static void Method()
      if (!NetString.HasValue(retName)
        && tokens.Length > 3)
      {
        if (IsAccess(tokens[0])
          && IsAccess2(tokens[1])
          && IsType(tokens[2]))
        {
          if (tokens[3].Contains("("))
          {
            retName = ScrubMethodName(tokens[2]);
          }
        }
      }
      return retName;
    }

    // Checks if a property definition.
    /// <include path="members/PropertyName/*" file="Doc/LJCParse.xml"/>
    public string PropertyName(string[] tokens)
    {
      string retName = null;

      // type Property
      // !type name = 
      if (!NetString.HasValue(retName)
        && tokens.Length > 1)
      {
        if (IsType(tokens[0]))
        {
          if (!tokens[1].Contains("("))
          {
            var valid = true;
            if (tokens.Length > 2
              && "=" == tokens[2])
            {
              valid = false;
            }
            if (valid)
            {
              retName = tokens[1];
            }
          }
          if (!NetString.HasValue(retName)
            && tokens[0] != "#region"
            && tokens[1] != "="
            && !IsType(tokens[0])
            && !IsType(tokens[1]))
          {
            retName = tokens[1];
          }
        }
      }

      // public type Property
      if (!NetString.HasValue(retName)
        && tokens.Length > 2)
      {
        if (IsAccess(tokens[0])
          || IsAccess2(tokens[0]))
        {
          if (IsType(tokens[1])
            && tokens[2] != "="
            && !tokens[2].Contains("("))
          {
            retName = tokens[2];
          }
          if (!NetString.HasValue(retName)
            && tokens[2] != "="
            && !IsType(tokens[1])
            && !IsType(tokens[2]))
          {
            retName = tokens[2];
          }
        }
      }

      // public static type Property
      if (!NetString.HasValue(retName)
        && tokens.Length > 3)
      {
        if (IsAccess(tokens[0])
          && IsAccess2(tokens[1]))
        {
          if (IsType(tokens[2])
            && !tokens[3].Contains("("))
          {
            retName = tokens[3];
          }
          if (!NetString.HasValue(retName)
            && !IsType(tokens[2])
            && !IsType(tokens[3]))
          {
            retName = tokens[3];
          }
        }
      }
      return retName;
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

    // Gets the method name.
    private string ScrubMethodName(string token)
    {
      string retValue = null;

      var index = token.IndexOf("(");
      if (index >= 0)
      {
        var length = token.Length;
        length -= length - index;
        retValue = token.Substring(0, length);
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
        "void",
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
