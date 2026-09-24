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
      for (int selection = 1; selection <= 6; selection++)
      {
        var data = GetValues(selection);
        var lines = data.TemplateLines;

        var generateText = new GenSample();
        var sections = generateText.CreateSections(lines);

        if (sections != null)
        {
          // Testing
          LJC.XmlSerialize(typeof(Sections), sections, null
            , data.DataFile);

          var genText = new GenTextLib();
          var generatedText = genText.TextGen(sections, lines);
          File.WriteAllText(data.OutputFile, generatedText);
        }
      }
    }

    private static GenValues GetValues(int selection)
    {
      string dataFile = "";
      string templateSpec = "";
      string outputFile = "";

      switch (selection)
      {
        case 1:
          templateSpec = @"Templates\DataTemplate.cs";
          dataFile = @"TestData\TestData.xml";
          outputFile = @"Output\Data.cs";
          break;

        case 2:
          templateSpec = @"Templates\CollectionTemplate.cs";
          dataFile = @"TestData\TestCollection.xml";
          outputFile = @"Output\Collection.cs";
          break;

        case 3:
          templateSpec = @"Templates\DataManagerTemplate.cs";
          dataFile = @"TestData\TestDataManager.xml";
          outputFile = @"Output\DataManager.cs";
          break;

        case 4:
          templateSpec = @"Templates\ManagerTemplate.cs";
          dataFile = @"TestData\TestManager.xml";
          outputFile = @"Output\Manager.cs";
          break;

        case 5:
          templateSpec = @"Templates\ManagersTemplate5.cs";
          dataFile = @"TestData\TestManagers5.xml";
          outputFile = @"Output\Managers5.cs";
          break;

        case 6:
          templateSpec = @"Templates\ValuesTemplate.cs";
          dataFile = @"TestData\TestValues.xml";
          outputFile = @"Output\Values.cs";
          break;
      }

      var lines = File.ReadAllLines(templateSpec);
      var data = new GenValues()
      {
        DataFile = dataFile,
        OutputFile = outputFile,
        TemplateLines = lines,
      };
      return data;
    }

    private class GenValues()
    {
      public string DataFile { get; set; } = null!;
      public string OutputFile { get; set; } = null!;
      public string[] TemplateLines { get; set; } = null!;
    }
  }
}
