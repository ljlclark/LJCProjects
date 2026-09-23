// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestGenSample5.cs
using LJCGenTextLib5;
using LJCGenTextXAL5;
using LJCNetCommon5;

namespace TestGenText5
{
  // Provides GenText specific test methods.
  internal class TestGenSample
  {
    // Initializes an object instance.
    public TestGenSample()
    {
      GenData();
    }

    // Generate from DataTemplate.
    private static void GenData()
    {
      var fileSpec = @"Templates\DataTemplate.cs";
      var lines = File.ReadAllLines(fileSpec);

      var generateText = new GenSample();
      var sections = generateText.CreateSections(lines);

      if (sections != null)
      {
        // Testing
        LJC.XmlSerialize(typeof(Sections), sections, null
          , "TestData.xml");

        var genText = new GenTextLib();
        var generatedText = genText.TextGen(sections, lines);
        File.WriteAllText(@"Output\Data.cs", generatedText);
      }
    }
  }
}
