// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestGenSample5.cs
using LJCGenTextLib5;

namespace TestGenText5
{
  // Provides GenText specific test methods.
  internal class TestGenSample
  {
    // Initializes an object instance.
    public TestGenSample()
    {
      var fileSpec = @"Templates\DataTemplate.cs";
      var lines = File.ReadAllLines(fileSpec);

      var generateText = new GenSample();
      var sections = generateText.CreateSections(lines);

      if (sections != null)
      {
        var genText = new GenTextLib();
        var generatedText = genText.TextGen(sections, lines);
        File.WriteAllText(@"Work\Data.cs", generatedText);
      }
    }
  }
}
