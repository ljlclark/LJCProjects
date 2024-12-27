// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TextBuilder.cs
using System;
using System.Text;

namespace LJCNetCommon
{
  /// <summary>A StringBuilder helper class.</summary>
  public class TextBuilder
  {
    #region Constructors

    /// <summary>Initializes an object instance.</summary>
    public TextBuilder(int capacity, StringBuilder builder = null)
    {
      if (null == builder)
      {
        builder = new StringBuilder(capacity);
      }
      Builder = builder;
    }
    #endregion

    #region Methods

    /// <summary>Implents the ToString() method.</summary>
    public override string ToString()
    {
      return Builder.ToString();
    }

    /// <summary>Adds a line.</summary>
    public void Line(string text = null)
    {
      Builder.AppendLine(text);
    }

    /// <summary>Adds text.</summary>
    public void Text(string text)
    {
      Builder.Append(text);
    }
    #endregion

    #region Properties

    /// <summary>The internal StringBuilder class.</summary>
    public StringBuilder Builder { get; set; }
    #endregion
  }
}
