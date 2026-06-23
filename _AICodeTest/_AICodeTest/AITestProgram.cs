using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _AICodeTest
{
  internal class AITestProgram
  {
    static void Main()
    {
      CreatePerson();
      CreatePersonWithValues();
      Clone();
    }

    static void CreatePerson()
    {
      var person = new LJCPerson
      {
        ParentID = 1
      };
      // 1 changed property.
      var changedProperties = person.ChangedProperties;
      if (changedProperties.Count != 1
        && changedProperties[0] != "ParentID")
      {
        Console.WriteLine("\r\nCreatePerson()");
        Console.Write("ChangedProperties must have 1 element = ");
        Console.WriteLine("\"ParentID\".");
      }

      person.ParentID = 0;
      // No changed properties as value was set back to original.
      changedProperties = person.ChangedProperties;
      if (changedProperties.Count > 0)
      {
        Console.WriteLine("\r\nCreatePerson()");
        Console.WriteLine("ChangedProperties must have 0 elements.");
      }
    }

    static void CreatePersonWithValues()
    {
      var person = new LJCPerson(1, "Name", "Description");
      if (person.ID != 1
        || person.ParentID != 0
        || person.Name != "Name"
        || person.Description != "Description")
      {
        Console.WriteLine("\r\nCreatePersonWithValues()");
        Console.WriteLine("person must have ID = 1, ParentID = 0, Name = \"Name\"");
        Console.WriteLine(" and Description = \"Description\"");
      }
    }

    static void Clone()
    {
      var person = new LJCPerson(1, "Name", "Description")
      {
        ParentID = 1
      };
      var changedProperties = person.ChangedProperties;
      if (changedProperties.Count != 1
        && changedProperties[0] != "ParentID")
      {
        Console.WriteLine("\r\nClone() - new LJCPerson()");
        Console.Write("ChangedProperties must have 1 element = ");
        Console.WriteLine("\"ParentID\".");
      }

      // newPerson.ParentID = 1 as an original value so it shows no changed
      // properties.
      var newPerson = person.Clone();
      if (null == newPerson
        || changedProperties.Count != 1
        || changedProperties[0] != "ParentID")
      {
        Console.WriteLine("\r\nClone()");
        Console.Write("ChangedProperties must have 1 element = ");
        Console.WriteLine("\"ParentID\".");
      }
    }
  }
}
