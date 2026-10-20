// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestConnectionTemplates5.cs
using LJCDBConfig5;
using LJCNetCommon5;

namespace TestDBConfig5
{
  // Provides the LJCConnectionTemplates test methods.
  internal class TestConnectionTemplates
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; } = null!;
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/TestDataTemplates.xml'
    ///  path='members/Constructor/*'/>
    public TestConnectionTemplates()
    {
      TestCommon = new LJCTestCommon("LJCConnectionTemplates");
      Run();
    }

    // Runs the tests.
    private void Run()
    {
      // Constructor Test Methods
      LoadData();

      // Methods
      Add();
      Retrieve();
      Save();
    }
    #endregion

    #region Constructor Test Methods

    // Loads the template data.
    private void LoadData()
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
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Methods

    // Creates and adds the object from the supplied valus.
    private void Add()
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
    private void Retrieve()
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
    private void Save()
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
  }
}
