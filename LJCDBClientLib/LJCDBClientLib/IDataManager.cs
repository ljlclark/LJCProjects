// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
// IDataManager.cs
namespace LJCDBClientLib
{
  /// <summary>Provides standard data manipulation.</summary>
  public interface IDataManager
  {
    #region Properties

    /// <summary>Gets or sets the pagination size.</summary>
    int PageSize { get; set; }

    /// <summary>Gets or sets the pagination start index.</summary>
    int PageStartIndex { get; set; }
    #endregion
  }
}
