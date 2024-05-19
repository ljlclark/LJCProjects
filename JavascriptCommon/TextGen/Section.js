// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Section.js

// Represents a data object.
class Section
{
  // The Constructor method.
  constructor(name = null)
  {
    this.Name = name;
    this.RepeatItems = new RepeatItems();
  }
}