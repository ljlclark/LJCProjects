// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ConnectionTemplatesProgram.cs
using LJCDataAccessConfig5;
using LJCNetCommon5;

namespace TestConnectionTemplates5
{
  // The entry class.
  internal class ConnectonTemplatesProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCConnectionTemplates");
      Console.WriteLine();
      Console.WriteLine("*** LJCConnectionTemplates ***");

      // Constructor Methods
      LoadData();

      // Methods
      Add();
      Retrieve();
      Save();

      Console.WriteLine();
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }

    #region Constructor Methods

    // Loads the templates data.
    private static void LoadData()
    {
      var methodName = "LoadData()";

      // TemplateFileSpec defaults to "ConnectionTemplates.xml";
      var templates = new LJCConnectionTemplates();
      File.Delete("ConnectionTemplates.xml");

      // Test Method
      // Creates default template file if not found.
      // Default template file contains "SQLServer" connection template.
      templates.LoadData();

      // Retrieve SQLServer connection template.
      var templateName = "SQLServer";
      var template = templates.Retrieve(templateName);
      var result = "";
      if (template != null)
      {
        result = template.Name;
      }
      var compare = "SQLServer";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Methods

    // Creates and adds the object from the supplied valus.
    private static void Add()
    {
      var methodName = "Add()";

      // TemplateFileSpec defaults to "ConnectionTemplates.xml";
      var templates = new LJCConnectionTemplates();
      File.Delete("ConnectionTemplates.xml");

      // Test Method
      var name = "SQLServer";
      var templateText = "";
      templates.Add(name, templateText);

      // Retrieve SQLServer connection template.
      var templateName = "SQLServer";
      var template = templates.Retrieve(templateName);
      var result = "";
      if (template != null)
      {
        result = template.Name;
      }
      var compare = "SQLServer";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Retrieve the connection template.
    private static void Retrieve()
    {
      var methodName = "Retrieve()";

      // TemplateFileSpec defaults to "ConnectionTemplates.xml";
      var templates = new LJCConnectionTemplates();
      File.Delete("ConnectionTemplates.xml");

      // Creates default template file if not found.
      // Default template file contains "SQLServer" connection template.
      templates.LoadData();

      // Test Method
      var templateName = "SQLServer";
      var template = templates.Retrieve(templateName);
      var result = "";
      if (template != null)
      {
        result = template.Name;
      }
      var compare = "SQLServer";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Saves the config data.
    private static void Save()
    {
      var methodName = "Save()";

      // TemplateFileSpec defaults to "ConnectionTemplates.xml";
      var templates = new LJCConnectionTemplates();

      // Creates default template file if not found.
      // Default template file contains "SQLServer" connection template.
      templates.LoadData();

      // Test Method
      templates.Save();

      templates.LoadData();
      var templateName = "SQLServer";
      var template = templates.Retrieve(templateName);
      var result = "";
      if (template != null)
      {
        result = template.Name;
      }
      var compare = "SQLServer";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
