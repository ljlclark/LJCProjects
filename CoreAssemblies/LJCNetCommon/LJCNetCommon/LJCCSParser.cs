// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCCSParser.cs

using System.Linq;

namespace LJCNetCommon
{
  // Provides methods to parse a source line for class, method and property
  // definitions.
  /// <include file='Doc/LJCCSParser.xml'
  ///  path='members/LJCCSParser/*'/>
  public class LJCCSParser
  {
    #region Static Methods

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
    #endregion

    #region Properties

    // Gets or sets the second class access modifier.
    /// <include file='Doc/LJCCSParser.xml'
    ///  path='members/ClassModifier2/*'/>
    public string[] ClassModifier2 { get; set; }

    // Gets or sets the data types.
    /// <include file='Doc/LJCCSParser.xml'
    ///  path='members/DataType/*'/>
    public string[] DataType { get; set; }

    // Gets or sets the second method access modifier.
    /// <include file='Doc/LJCCSParser.xml'
    ///  path='members/MethodModifier2/*'/>
    public string[] MethodModifier2 { get; set; }

    // Gets or sets the third method access modifier.
    /// <include file='Doc/LJCCSParser.xml'
    ///  path='members/MethodModifier3/*'/>
    public string[] MethodModifier3 { get; set; }

    // Gets or sets the access modifiers.
    /// <include file='Doc/LJCCSParser.xml'
    ///  path='members/Modifier/*'/>
    public string[] Modifier { get; set; }

    // Gets or sets the second access modifier.
    /// <include file='Doc/LJCCSParser.xml'
    ///  path='members/Modifier2/*'/>
    public string[] Modifier2 { get; set; }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/LJCCSParser.xml'
    ///  path='members/Constructor/*'/>
    public LJCCSParser()
    {
      SetModifiers();
    }
    #endregion

    #region Public Methods

    // Attempts to parse a class name.
    /// <include file='Doc/LJCCSParser.xml'
    ///  path='members/ClassName/*'/>
    public string ClassName(string line)
    {
      string retName = null;

      string[] tokens = LJCNetString.Split(line, " ");
      if (LJC.HasArrayElements(tokens))
      {
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
          if (IsModifier(tokens[0])
            || IsClassModifier2(tokens[0]))
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
          if (IsModifier(tokens[0])
            && IsClassModifier2(tokens[1]))
          {
            if ("class" == tokens[2])
            {
              retName = tokens[3];
            }
          }
        }
      }
      return retName;
    }

    // Attempts to parse a method name.
    /// <include file='Doc/LJCCSParser.xml'
    ///  path='members/MethodName/*'/>
    public string MethodName(string line)
    {
      string retName = null;

      string[] tokens = LJCNetString.Split(line, " ");
      if (LJC.HasArrayElements(tokens))
      {
        // [modifier] [modifier2] method()
        // if (returnsBool());
        // var name = method();

        // void Method()
        if (!LJC.HasText(retName)
        && tokens.Length > 1)
        {
          if (IsType(tokens[0]))
          {
            if (tokens[1].Contains('('))
            {
              retName = ScrubMethodName(tokens[1]);
            }
          }
        }

        // static void Method()
        if (!LJC.HasText(retName)
          && tokens.Length > 2)
        {
          if ((IsModifier(tokens[0])
            || IsMethodModifier2(tokens[0]))
            && IsType(tokens[1]))
          {
            if (tokens[2].Contains('('))
            {
              retName = ScrubMethodName(tokens[2]);
            }
          }
        }

        // public static void Method()
        if (!LJC.HasText(retName)
          && tokens.Length > 3)
        {
          if (IsModifier(tokens[0])
            && IsMethodModifier2(tokens[1])
            && IsType(tokens[2]))
          {
            if (tokens[3].Contains('('))
            {
              retName = ScrubMethodName(tokens[3]);
            }
          }
        }

        // public static void Method()
        if (!LJC.HasText(retName)
          && tokens.Length > 4)
        {
          if (IsModifier(tokens[0])
            && IsMethodModifier2(tokens[1])
            && IsMethodModifier3(tokens[2])
            && IsType(tokens[3]))
          {
            if (tokens[4].Contains('('))
            {
              retName = ScrubMethodName(tokens[4]);
            }
          }
        }
      }
      return retName;
    }

    // Attempts to parse a property name.
    /// <include file='Doc/LJCCSParser.xml'
    ///  path='members/PropertyName/*'/>
    public string PropertyName(string line, string nextLine)
    {
      string retName = null;

      string[] tokens = LJCNetString.Split(line, " ");
      if (LJC.HasArrayElements(tokens))
      {
        if (!LJC.HasText(retName))
        {
          // type Property
          if (2 == tokens.Length
            && !IsModifier(tokens[0])
            && !tokens[0].StartsWith("#")
            && nextLine != null
            && nextLine.Trim().StartsWith("{"))
          {
            retName = tokens[1];

            // Not "type Method()"
            // Not "type VarName;"
            // Not "VarName ="
            if (tokens[1].Contains('(')
              || tokens[1].EndsWith(";")
              || "=" == tokens[1])
            {
              retName = null;
            }

            // Not "type VarName = "
            if (tokens.Length > 2
              && "=" == tokens[2]
              && tokens[2] != "{")
            {
              retName = null;
            }
          }
        }

        if (!LJC.HasText(retName))
        {
          // modifier type Property {
          if (tokens.Length > 3
            && (IsModifier(tokens[0])
            || IsType(tokens[0]))
            && !tokens[0].StartsWith("#"))
          {
            retName = tokens[2];
            if (tokens[2].Trim().StartsWith("{"))
            {
              retName = tokens[1];
            }

            // Not "type VarName = "
            // Not "modifier type Method()"
            // Not "modifier type VarName;
            if ("=" == tokens[2]
              || tokens[2].Contains('(')
              || tokens[2].EndsWith(";"))
            {
              retName = null;
            }

            // Not "mofifier type VarName ="
            if (tokens.Length > 3
              && "=" == tokens[3])
            {
              retName = null;
            }
          }
        }
      }
      return retName;
    }
    #endregion

    #region Private Methods

    // Checks if the token is a ClassModifier2.
    private bool IsClassModifier2(string token)
    {
      var retValue = false;

      if (LJC.HasArrayElements(ClassModifier2))
      {
        foreach (var classModifier in ClassModifier2)
        {
          if (token == classModifier)
          {
            retValue = true;
            break;
          }
        }
      }
      return retValue;
    }

    // Checks if the token is a Modifier.
    private bool IsModifier(string token)
    {
      var retValue = false;

      if (LJC.HasArrayElements(Modifier))
      {
        foreach (var modifier in Modifier)
        {
          if (token == modifier)
          {
            retValue = true;
            break;
          }
        }
      }
      return retValue;
    }

    // Checks if the token is a Modifier2.
    private bool IsMethodModifier2(string token)
    {
      var retValue = false;

      if (LJC.HasArrayElements(MethodModifier2))
      {
        foreach (var modifier in MethodModifier2)
        {
          if (token == modifier)
          {
            retValue = true;
            break;
          }
        }
      }
      return retValue;
    }

    // Checks if the token is a Modifier.
    private bool IsMethodModifier3(string token)
    {
      var retValue = false;

      if (LJC.HasArrayElements(MethodModifier3))
      {
        foreach (var modifier in MethodModifier3)
        {
          if (token == modifier)
          {
            retValue = true;
            break;
          }
        }
      }
      return retValue;
    }

    // Checks if the token is a data type.
    private bool IsType(string token)
    {
      var retValue = false;

      if (LJC.HasArrayElements(DataType))
      {
        foreach (var dataType in DataType)
        {
          if (token == dataType)
          {
            retValue = true;
            break;
          }
        }
      }
      return retValue;
    }

    // Sets the modifier and type properties.
    private void SetModifiers()
    {
      Modifier = new string[]
      {
        "internal",
        "private",
        "protected",
        "public",
        "static",
      };

      MethodModifier2 = new string[]
      {
        "abstract",
        "internal",
        "override",
        "protected",
        "static",
        "virtual",
      };

      MethodModifier3 = new string[]
      {
        "sealed",
      };

      // public abstract class
      // public sealed class
      ClassModifier2 = new string[]
      {
        "abstract",
        "protected",
        "sealed",
        "static",
      };

      // In common usage order.
      DataType = new string[]
      {
        "void",
        "string",
        "bool",
        "int",
        "long",
        "short",
        //
        "String",
        "Boolean",
        "Int32",
        "Int64",
        "Int16",
        //
        "decimal",
        "double",
        "float",
        "char",
        "byte",
        //
        "Decimal",
        "Double",
        "Single",
        "Char",
        "Byte",
        //
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
  }
}
