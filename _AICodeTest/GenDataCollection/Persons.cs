using System;
using System.Collections.Generic;
using System.Linq;

/// <include path="members/LJCPersons" file="Doc/LJCPersons.xml"/>
public class LJCPersons : IEnumerable<LJCPerson>
{
  private List<LJCPerson> _persons;

  /// <include path="members/DefaultCtor" file="Doc/LJCPersons.xml"/>
  public LJCPersons()
  {
    _persons = new List<LJCPerson>();
  }

  /// <include path="members/CopyCtor" file="Doc/LJCPersons.xml"/>
  public LJCPersons(LJCPersons persons)
  {
    _persons = new List<LJCPerson>();
    foreach (var person in persons._persons)
    {
      _persons.Add(new LJCPerson(person));
    }
  }

  /// <include path="members/Clone" file="Doc/LJCPersons.xml"/>
  public LJCPersons Clone()
  {
    return new LJCPersons(this);
  }

  /// <include path="members/Add" file="Doc/LJCPersons.xml"/>
  public LJCPerson Add(
    long id,
    long parentID,
    string name,
    string description = null)
  {
    if (Retrieve(parentID, name) != null)
    {
      throw new InvalidOperationException(
        "A person with the same ParentID and Name already exists.");
    }

    var newPerson = new LJCPerson(id, parentID, name, description);
    _persons.Add(newPerson);
    return newPerson;
  }

  /// <include path="members/AddObject" file="Doc/LJCPersons.xml"/>
  public LJCPerson AddObject(LJCPerson person)
  {
    if (Retrieve(person.ParentID, person.Name) != null)
    {
      throw new InvalidOperationException(
        "A person with the same ParentID and Name already exists.");
    }

    _persons.Add(person);
    return person;
  }

  /// <include path="members/Retrieve" file="Doc/LJCPersons.xml"/>
  public LJCPerson Retrieve(long parentID, string name)
  {
    return _persons.FirstOrDefault(p =>
      p.ParentID == parentID && p.Name == name);
  }

  /// <include path="members/Remove" file="Doc/LJCPersons.xml"/>
  public void Remove(long parentID, string name)
  {
    var personToRemove = Retrieve(parentID, name);
    if (personToRemove != null)
    {
      _persons.Remove(personToRemove);
    }
  }

  public IEnumerator<LJCPerson> GetEnumerator()
  {
    return _persons.GetEnumerator();
  }

  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  {
    return GetEnumerator();
  }
}

public class LJCPerson
{
  public long ID { get; }
  public long ParentID { get; }
  public string Name { get; }
  public string Description { get; set; }

  public LJCPerson(
    long id,
    long parentID,
    string name,
    string description = null)
  {
    ID = id;
    ParentID = parentID;
    Name = name;
    Description = description;
  }

  // Copy constructor for deep copy
  public LJCPerson(LJCPerson person)
  {
    ID = person.ID;
    ParentID = person.ParentID;
    Name = person.Name;
    Description = person.Description;
  }
}
