// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ValuesDocLib.cs
namespace LJCDocGenLib
{
  /// <summary>The Application values singleton class.</summary>
  public sealed class ValuesDocLib
  {
    #region Constructors

    // Initializes an instance of the object.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ValuesDocLib()
    {
    }
    #endregion

    #region Properties

    /// <summary>Gets the singleton instance.</summary>
    public static ValuesDocLib Instance
    {
      get { return mInstance; }
    }
    #endregion

    #region Class Data

    // Initialize Singleton.
    static readonly ValuesDocLib mInstance = new ValuesDocLib();
    #endregion
  }
}
