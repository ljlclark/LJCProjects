// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// RepeatItem.js

// Represents a data object.
class RepeatItem
{
  // The Constructor method.
  constructor(name = null)
  {
    this.Name = name;
    this.Replacements = new Replacements();
  }
}