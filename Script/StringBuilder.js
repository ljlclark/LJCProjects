// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// StringBuilder.js

class StringBuilder
{
  // The Constructor method.
  constructor()
  {
    this.Output = "";
  }

  // 
  Text(text)
  {
    if (text != null)
    {
      text = text.replaceAll("\'", "\"")
    }
    this.Output += text;
  }

  // 
  Line(text)
  {
    if (null == text)
    {
      text = "";
    }
    text += "\r\n"
    this.Text(`${text}`);
  }

  // 
  ToString()
  {
    return this.Output;
  }
}