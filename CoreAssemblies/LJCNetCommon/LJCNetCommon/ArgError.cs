// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ArgError.cs
using System;

namespace LJCNetCommon
{
  /// <summary></summary>
  public class ArgError
  {
    #region Constructors

    /// <summary>Initializes an object instance.</summary>
    /// <param name="className">The Class name.</param>
    public ArgError(string className)
    {
      ClassName = className;
      mMessage = "";
    }
    #endregion

    #region Methods

    /// <summary>Returns the message string.</summary>
    /// <returns>The message string.</returns>
    public override string ToString()
    {
      string retValue = null;

      if (NetString.HasValue(mMessage))
      {
        retValue = $"{ClassName}\r\n{MethodName}\r\n";
        retValue += mMessage;
      }
      return retValue;
    }

    /// <summary>
    /// Adds a message using the provided values.
    /// </summary>
    /// <param name="argument">The argument value.</param>
    /// <param name="name">The argument name.</param>
    public void Add(object argument, string name)
    {
      NetString.AddObjectArgError(ref mMessage, argument, name);
    }

    /// <summary>Adds a message.</summary>
    /// <param name="message">The message value.</param>
    public void Add(string message)
    {
      mMessage += message;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the Class name.</summary>
    public string ClassName { get; set; }

    /// <summary>Gets or sets the Method name.</summary>
    public string MethodName
    {
      get { return mMethodName; }
      set
      {
        mMethodName = value;
        mMessage = "";
      }
    }
    private string mMethodName;
    #endregion

    #region Class Data

    private string mMessage;
    #endregion
  }
}
