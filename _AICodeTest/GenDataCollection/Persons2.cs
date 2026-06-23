using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Represents a strongly typed collection of <see cref="LJCPerson"/> objects.
/// The unique key for an item is the combination of ParentID and Name.
/// </summary>
public class LJCPersons : ICollection<LJCPerson>
{
  private List<LJCPerson> persons = new List<LJCPerson>();

  // Default Constructor
  /// <include path="members/DefaultCtor" file="Doc/LJCPersons.xml"/>
  public LJCPersons()
  {
  }

  // Copy Constructor
  /// <include path="members/CopyCtor" file="Doc/LJCPersons.xml"/>
  public LJCPersons(LJCPersons persons)
  {
    if (persons == null)
    {
      throw new ArgumentNullException(nameof(persons));
    }

    foreach (LJCPerson person in persons)
    {
      // Deep copy each object
      this.persons.Add(person.Clone());
    }
  }

  // Public Method Clone()
  /// <include path="members/Clone" file="Doc/LJCPersons.xml"/>
  public LJCPersons Clone()
  {
    return new LJCPersons(this);
  }

  // Public Method Add()
  /// <include path="members/Add" file="Doc/LJCPersons.xml"/>
  public LJCPerson Add(long id, long parentID, string name,
    string description = null)
  {
    if (Retrieve(parentID, name) != null)
    {
      throw new InvalidOperationException
      (
        "A person with the same ParentID and Name already exists."
      );
    }

    LJCPerson newPerson = new LJCPerson(id, parentID, name, description);
    persons.Add(newPerson);
    return newPerson;
  }

  // Public Method AddObject()
  /// <include path="members/AddObject" file="Doc/LJCPersons.xml"/>
  public LJCPerson AddObject(LJCPerson person)
  {
    if (person == null)
    {
      throw new ArgumentNullException(nameof(person));
    }

    return Add(person.ID, person.ParentID, person.Name, person.Description);
  }

  // Public Method Retrieve()
  /// <include path="members/Retrieve" file="Doc/LJCPersons.xml"/>
  public LJCPerson Retrieve(long parentID, string name)
  {
    // Use straightforward LINQ expression
    return persons.FirstOrDefault
    (
      p => p.ParentID == parentID && p.Name == name
    );
  }

  // Public Method Remove()
  /// <include path="members/Remove" file="Doc/LJCPersons.xml"/>
  public void Remove(long parentID, string name)
  {
    LJCPerson personToRemove = Retrieve(parentID, name);
    if (personToRemove != null)
    {
      persons.Remove(personToRemove);
    }
  }

  // ICollection<LJCPerson> interface implementation
  /// <inheritdoc/>
  public IEnumerator<LJCPerson> GetEnumerator()
  {
    return persons.GetEnumerator();
  }

  /// <inheritdoc/>
  IEnumerator IEnumerable.GetEnumerator()
  {
    return GetEnumerator();
  }

  /// <inheritdoc/>
  public void Add(LJCPerson item)
  {
    AddObject(item);
  }

  /// <inheritdoc/>
  public void Clear()
  {
    persons.Clear();
  }

  /// <inheritdoc/>
  public bool Contains(LJCPerson item)
  {
    return persons.Contains(item);
  }

  /// <inheritdoc/>
  public void CopyTo(LJCPerson[] array, int arrayIndex)
  {
    persons.CopyTo(array, arrayIndex);
  }

  /// <inheritdoc/>
  public bool Remove(LJCPerson item)
  {
    if (item == null)
    {
      return false;
    }

    return persons.Remove(item);
  }

  /// <inheritdoc/>
  public int Count
  {
    get { return persons.Count; }
  }

  /// <inheritdoc/>
  public bool IsReadOnly
  {
    get { return false; }
  }
}
