// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ArgError.js

// Check for argument errors.
class ArgError
{
  // The Constructor function.
  constructor()
  {
    this.Context = "";
    this.Error = "";
  }

  // 
  IsArray(value, name)
  {
    let retValue = false;

    retValue = this.IsValue(value, name);
    if (!retValue
      && !Array.isArray(value))
    {
      retValue = true;
      this.Error += `\r\n  ${this.Context}\r\n${name} is not an Array.`;
    }
    return retValue;
  }

  // 
  IsCollection(value, name)
  {
    let retValue = false;

    retValue = this.IsValue(value, name);
    if (!retValue
      && !Array.isArray(value.ItemArray))
    {
      retValue = true;
      this.Error = `\r\n  ${this.Context}\r\n${name} is not a Collection.`;
    }
    return retValue;
  }

  // 
  IsValue(value, name)
  {
    let retValue = false;

    if (undefined === value)
    {
      retValue = true;
      this.Error += `\r\n  ${this.Context}\r\n${name} is not defined.`;
    }
    if (!retValue
      && null == value)
    {
      retValue = true;
      this.Error += `\r\n  ${this.Context}\r\n${name} is null.`;
    }
    return retValue;
  }

  // 
  SetContext(context)
  {
    this.Error = "";
    this.Context = context;
  }

  // 
  ShowError()
  {
    let retValue = false;

    if (this.Error.length > 0)
    {
      retValue = true;
      let message = `${this.Context}\r\n`;
      message += this.Error;
      alert(message);
    }
    return retValue;
  }
}
