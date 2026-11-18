// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCArgError.cs

namespace LJCNetCommon5
{
  // Creates an argument error message.
  /// <include file='Doc/LJCArgError.xml'
  ///  path='members/LJCArgError/*'/>
  public class LJCArgError
  {
    #region Properties

    // Gets or sets the Class name.
    /// <include file='Doc/LJCArgError.xml'
    ///  path='members/ClassName/*'/>
    public string ClassName { get; set; }

    // Gets or sets the Method name.
    /// <include file='Doc/LJCArgError.xml'
    ///  path='members/MethodName/*'/>
    public string? MethodName
    {
      get => mMethodName;
      set
      {
        mMethodName = value?.Trim();
        if (LJC.HasText(value))
        {
          mMessage = "";
        }
      }
    }
    private string? mMethodName;
    #endregion

    #region Class Data

    private string mMessage;
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include file='Doc/LJCArgError.xml'
    ///  path='members/Constructor/*'/>
    public LJCArgError()
    {
      ClassName = "";
      mMessage = "";
    }

    // Initializes an object instance with the supplied values.
    /// <include file='Doc/LJCArgError.xml'
    ///  path='members/ParamConstructor/*'/>
    public LJCArgError(string className) : this()
    {
      ClassName = className;
    }

    // Clears the messages.
    /// <include file='Doc/LJCArgError.xml'
    ///  path='members/Clear/*'/>
    public void Clear()
    {
      mMessage = "";
    }
    #endregion

    #region Data Class Methods

    // Returns the message string.
    /// <include file='Doc/LJCArgError.xml'
    ///  path='members/ToString/*'/>
    public override string? ToString()
    {
      string retValue = null;

      if (LJC.HasText(mMessage))
      {
        retValue = $"{ClassName}\r\n{MethodName}\r\n";
        retValue += mMessage;
      }
      return retValue;
    }
    #endregion

    #region Methods

    // Adds a message using the provided values.
    /// <include file='Doc/LJCArgError.xml'
    ///  path='members/Add1/*'/>
    public void Add(object? argument, string name)
    {
      LJCNetString.AddObjectArgError(ref mMessage, argument, name);
    }

    // Adds a message.
    /// <include file='Doc/LJCArgError.xml'
    ///  path='members/Add2/*'/>
    public void Add(string message)
    {
      mMessage += message;
    }

    // Checks the ID values.
    /// <include file='Doc/LJCArgError.xml'
    ///  path='members/DbIDCheck/*'/>
    public void DbIDCheck(short dbId)
    {
      string message = "";
      if (dbId <= 0)
      {
        message += "dbId must be greater than zero.\r\n";
      }
      Add(message);
    }

    // Checks the ID values.
    /// <include file='Doc/LJCArgError.xml'
    ///  path='members/IDCheck/*'/>
    public void IDCheck(short dbId, long id)
    {
      string message = "";
      if (dbId <= 0)
      {
        message += "dbId must be greater than zero.\r\n";
      }
      if (id <= 0)
      {
        message += "id must be greater than zero.\r\n";
      }
      Add(message);
    }

    // 
    /// <include file='Doc/LJCArgError.xml'
    ///  path='members/ThrowError/*'/>
    public void ThrowError()
    {
      LJCNetString.ThrowArgError(ToString());
    }
    #endregion
  }
}
