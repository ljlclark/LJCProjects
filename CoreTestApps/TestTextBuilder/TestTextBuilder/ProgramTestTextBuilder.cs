﻿// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Program.cs
using LJCNetCommon;
using System;
using System.Security.Cryptography;

namespace TestTextBuilder
{
  class Program
  {
    static void Main()
    {
      var b = new TextBuilder()
      {
        WrapPrefix = "  "
      };
      //b.LineLimit = 20;

      // Build a wrapping Item.
      b.Indent();
      b.Item("First");
      b.Item("Second");
      b.Item("Third");
      b.Item("Fourth");
      b.Item("Fifth");
      b.Item("Sixth");
      b.Item("Seventh");
      b.Item("Eighth");
      b.Item("Nineth");
      b.Item("Tenth");
      b.Item("Eleventh");
      b.Item("Twelveth");
      b.Item("Thirteenth");
      b.Item("Fourteenth");
      b.Item("Fifteenth");
      b.Line();
      b.Line("That is a Item.");
      b.Line();
      //b.WrapPrefix = null;
      b.Text("Now is the time for all good men to come to the aid of their country.");
      b.Text("Now is the time for all good men to come to the aid of their country.");
      b.Text("Now is the time for all good men to come to the aid of their country.");
      b.Line();
      b.Text("Now is the time for all good men to come to the aid of their country. "
        + "Now is the time for all good men to come to the aid of their country. "
        + "Now is the time for all good men to come to the aid of their country.");
      var result = b.ToString();
      Console.WriteLine(result);
      Console.WriteLine("Press ENTER to continue . . .");
      Console.ReadLine();
    }
  }
}
//  Now is the time for all good men to come to the aid of their country. Now is 
//  the time for all good men to come to the aid of their country. Now is the time 
//  for all good men to come to the aid of their country.
//         1         2         3         4         5         6         7         8 
//12345678901234567890123456789012345678901234567890123456789012345678901234567890
//  Now is the time for all good men to come to the aid of their country. Now is the time for all good men to come to the aid of their country. Now is the time for all good men to come to the aid of their country.
