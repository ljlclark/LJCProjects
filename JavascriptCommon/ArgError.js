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
      alert(`${this.Context}\r\n${name} is not an Array.`)
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
      alert(`${this.Context}\r\n${name} is not a Collection.`);
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
      alert(`${this.Context}\r\n${name} is not defined.`)
    }
    if (!retValue
      && null == value)
    {
      retValue = true;
      alert(`${this.Context}\r\n${name} is null.`)
    }
    return retValue;
  }

  // 
  SetContext(context)
  {
    this.Context = context;
  }
}
