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
    text = text.replaceAll("\'", "\"")
    this.Output += text;
  }

  // 
  Line(text)
  {
    this.Text(`${text}\r\n`);
  }

  // 
  ToString()
  {
    return this.Output;
  }
}