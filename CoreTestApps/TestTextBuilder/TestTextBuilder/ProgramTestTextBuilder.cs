// Copyright(c) Lester J. Clark and Contributors.
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
      var x = new XMLBuilder();
      var attributes = x.StartAttributes();
      x.BeginElement("html", xmlAttributes: attributes);
      x.BeginElement("body", isIndented: false);

      x.CreateElement("Junk", "What?");
      var text = "Now is the time for all good men to come to the aid or their"
        + " country or follow the Yellow Brick Road.";
      x.CreateElement("Text", text);

      x.EndElement("body", false);
      x.EndElement("html");
      var xml = x.ToString();
      Console.WriteLine(xml);
      Console.WriteLine();

      // Build a wrapping Item.
      var b = new TextBuilder();
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
      b.Text("To be or not to be, that is the question on my lips at this moment.");
      b.Text("Two roads converged in the woods into a yellow brick road and which one should I take now. ");
      b.Text("This should be my final test.");
      b.Line();
      b.Text("Now is the time for all good men to come to the aid of their country. "
        + "To be or not to be, that is the question on my lips at this moment. "
        + "Two roads converged in the woods into a yellow brick road and which one should I take now. "
        + "This should be my final test.");
      var result = b.ToString();
      Console.WriteLine(result);
      Console.WriteLine("Press ENTER to continue . . .");
      Console.ReadLine();
    }
  }
}
