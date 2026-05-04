// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// IDataManager.cs

namespace LJCDBClientLib5
{
  /// <summary>Provides standard data manipulation.</summary>
  public interface ILJCDataManager
  {
    #region Properties

    /// <summary>Gets or sets the pagination size.</summary>
    int PageSize { get; set; }

    /// <summary>Gets or sets the pagination start index.</summary>
    int PageStartIndex { get; set; }
    #endregion
  }
}
