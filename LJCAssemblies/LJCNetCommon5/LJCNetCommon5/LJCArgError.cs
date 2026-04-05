// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCArgError.cs

namespace LJCNetCommon5
{
  // Creates an argument error message.
  /// <include path="members/LJCArgError/*" file="Doc/LJCArgError.xml"/>
  /// <group name="constructors">Constructors</group>
  /// <group name="dataClass">Data Class</group>
  /// <group name="methods">Methods</group>
  /// <group name="properties">Properties</group>
  public class LJCArgError
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path="members/Constructor/*" file="Doc/LJCArgError.xml"/>
    /// <parentGroup>constructors</parentGroup>
    public LJCArgError(string className)
    {
      ClassName = className;
      mMessage = "";
    }
    #endregion

    #region Data Class Methods

    // Returns the message string.
    /// <include path="members/ToString/*" file="Doc/LJCArgError.xml"/>
    /// <parentGroup>dataClass</parentGroup>
    public override string? ToString()
    {
      string retValue = null;

      if (LJC.HasValue(mMessage))
      {
        retValue = $"{ClassName}\r\n{MethodName}\r\n";
        retValue += mMessage;
      }
      return retValue;
    }
    #endregion

    #region Methods

    // Adds a message using the provided values.
    /// <include path="members/Add1/*" file="Doc/LJCArgError.xml"/>
    /// <parentGroup>methods</parentGroup>
    public void Add(object argument, string name)
    {
      LJCNetString.AddObjectArgError(ref mMessage, argument, name);
    }

    // Adds a message.
    /// <include path="members/Add2/*" file="Doc/LJCArgError.xml"/>
    /// <parentGroup>methods</parentGroup>
    public void Add(string message)
    {
      mMessage += message;
    }
    #endregion

    #region Properties

    // Gets or sets the Class name.
    /// <include path="members/ClassName/*" file="Doc/LJCArgError.xml"/>
    /// <parentGroup>properties</parentGroup>
    public string ClassName { get; set; }

    // Gets or sets the Method name.
    /// <include path="members/MethodName/*" file="Doc/LJCArgError.xml"/>
    /// <parentGroup>properties</parentGroup>
    public string? MethodName
    {
      get { return mMethodName; }
      set
      {
        if (value != null)
        {
          mMethodName = value;
          mMessage = "";
        }
      }
    }
    private string? mMethodName;
    #endregion

    #region Class Data

    private string mMessage;
    #endregion
  }
}
