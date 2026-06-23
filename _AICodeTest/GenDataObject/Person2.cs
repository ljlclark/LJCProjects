using System;

/// <summary>
/// Represents a person data object.
/// </summary>
public class LJCPerson
{
  // Properties
  /// <include path="members/ID" file="Doc/LJCPersons.xml"/>
  public long ID { get; set; }

  /// <include path="members/ParentID" file="Doc/LJCPersons.xml"/>
  public long ParentID { get; set; }

  /// <include path="members/Name" file="Doc/LJCPersons.xml"/>
  public string Name { get; set; }

  /// <include path="members/Description" file="Doc/LJCPersons.xml"/>
  public string Description { get; set; }

  // Default Constructor
  /// <summary>
  /// Initializes a new instance of the <see cref="LJCPerson"/> class.
  /// </summary>
  public LJCPerson()
  {
  }

  // Parameterized Constructor
  /// <summary>
  /// Initializes a new instance of the <see cref="LJCPerson"/> class with
  /// specified values.
  /// </summary>
  /// <param name="id">The unique identifier.</param>
  /// <param name="parentID">The parent identifier.</param>
  /// <param name="name">The name.</param>
  /// <param name="description">The description.</param>
  public LJCPerson(long id, long parentID, string name, string description = null)
  {
    ID = id;
    ParentID = parentID;
    Name = name;
    Description = description;
  }

  // Copy Constructor
  /// <summary>
  /// Initializes a new instance of the <see cref="LJCPerson"/> class as a copy
  /// of an existing instance.
  /// </summary>
  /// <param name="person">The person object to copy.</param>
  public LJCPerson(LJCPerson person)
  {
    if (person == null)
    {
      throw new ArgumentNullException(nameof(person));
    }

    ID = person.ID;
    ParentID = person.ParentID;
    Name = person.Name;
    Description = person.Description;
  }

  /// <summary>
  /// Creates a deep copy of the current person object.
  /// </summary>
  /// <returns>A new <see cref="LJCPerson"/> object that is a copy of this
  /// instance.</returns>
  public LJCPerson Clone()
  {
    return new LJCPerson(this);
  }
}
