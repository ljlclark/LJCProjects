// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCCSParser.cs

namespace LJCNetCommon5
{
  // Provides methods to parse a source line for class, method and property
  // definitions.
  /// <include path="members/LJCCSParser/*" file="Doc/LJCCSParser.xml"/>
  /// <group name="constructor">Constructor</group>
  /// <group name="methods">Methods</group>
  /// <group name="properties">Properties</group>
  public class LJCCSParser
  {
    #region Static Methods

    // Gets the method name.
    private static string? ScrubMethodName(string token)
    {
      string retValue = null;

      var index = token.IndexOf('(');
      if (index >= 0)
      {
        var length = token.Length;
        length -= length - index;
        //retValue = token.Substring(0, length);
        retValue = token[..length];
      }
      return retValue;
    }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include path="members/Constructor/*" file="Doc/LJCCSParser.xml"/>
    /// <parentGroup>constructor</parentGroup>
    public LJCCSParser()
    {
      SetModifiers();
    }
    #endregion

    #region Public Methods

    // Attempts to parse a class name.
    /// <include path="members/ClassName/*" file="Doc/LJCCSParser.xml"/>
    /// <parentGroup>methods</parentGroup>
    public string? ClassName(string line)
    {
      string retName = null;

      string[] tokens = LJCNetString.Split(line, " ");
      if (LJC.HasElements(tokens))
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
            || IsClassModifier2(tokens[1]))
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
    /// <include path="members/MethodName/*" file="Doc/LJCCSParser.xml"/>
    /// <parentGroup>methods</parentGroup>
    public string? MethodName(string line)
    {
      string retName = null;

      string[] tokens = LJCNetString.Split(line, " ");
      if (LJC.HasElements(tokens))
      {
        // [modifier] [modifier2] method()
        // if (returnsBool());
        // var name = method();

        // void Method()
        if (!LJC.HasValue(retName)
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
        if (!LJC.HasValue(retName)
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
        if (!LJC.HasValue(retName)
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
        if (!LJC.HasValue(retName)
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
    /// <include path="members/PropertyName/*" file="Doc/LJCCSParser.xml"/>
    /// <parentGroup>methods</parentGroup>
    public string? PropertyName(string line, string? nextLine)
    {
      string retName = null;

      string[] tokens = LJCNetString.Split(line, " ");
      if (LJC.HasElements(tokens))
      {
        if (!LJC.HasValue(retName))
        {
          // type Property
          if (2 == tokens.Length
            && !IsModifier(tokens[0])
            //&& !IsModifier2(tokens[0])
            && !tokens[0].StartsWith('#')
            && nextLine != null
            && nextLine.Trim().StartsWith('{'))
          {
            retName = tokens[1];

            // Not "type Method()"
            // Not "type VarName;"
            // Not "VarName ="
            if (tokens[1].Contains('(')
              || tokens[1].EndsWith(';')
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

        if (!LJC.HasValue(retName))
        {
          // modifier type Property {
          if (tokens.Length > 2
            && (IsModifier(tokens[0])
            || IsType(tokens[0]))
            && !tokens[0].StartsWith('#'))
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
              || tokens[2].EndsWith(';'))
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

      if (LJC.HasElements(ClassModifier2))
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

      if (LJC.HasElements(Modifier))
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

    // Checks if the token is a Modifier.
    private bool IsMethodModifier2(string token)
    {
      var retValue = false;

      if (LJC.HasElements(MethodModifier2))
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

      if (LJC.HasElements(MethodModifier3))
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

      if (LJC.HasElements(DataType))
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
      Modifier =
      [
        "internal",
        "private",
        "protected",
        "public",
        "static",
      ];

      MethodModifier2 =
      [
        "abstract",
        "internal",
        "override",
        "protected",
        "static",
        "virtual",
      ];

      MethodModifier3 =
      [
        "sealed",
      ];

      ClassModifier2 =
      [
        "abstract",
        "protected",
        "sealed",
        "static",
      ];

      // In common usage order.
      DataType =
      [
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
      ];
    }
    #endregion

    #region Properties

    // Gets or sets the second class access modifier.
    /// <include path="members/ClassModifier2/*" file="Doc/LJCCSParser.xml"/>
    /// <parentGroup>properties</parentGroup>
    public string[]? ClassModifier2 { get; set; }

    // Gets or sets the data types.
    /// <include path="members/DataType/*" file="Doc/LJCCSParser.xml"/>
    /// <parentGroup>properties</parentGroup>
    public string[]? DataType { get; set; }

    // Gets or sets the second method access modifier.
    /// <include path="members/MethodModifier2/*" file="Doc/LJCCSParser.xml"/>
    /// <parentGroup>properties</parentGroup>
    public string[]? MethodModifier2 { get; set; }

    // Gets or sets the third method access modifier.
    /// <include path="members/MethodModifier3/*" file="Doc/LJCCSParser.xml"/>
    /// <parentGroup>properties</parentGroup>
    public string[]? MethodModifier3 { get; set; }

    // Gets or sets the access modifiers.
    /// <include path="members/Modifier/*" file="Doc/LJCCSParser.xml"/>
    /// <parentGroup>properties</parentGroup>
    public string[]? Modifier { get; set; }

    // Gets or sets the second access modifier.
    /// <include path="members/Modifier2/*" file="Doc/LJCCSParser.xml"/>
    /// <parentGroup>properties</parentGroup>
    public string[]? Modifier2 { get; set; }
    #endregion
  }
}
