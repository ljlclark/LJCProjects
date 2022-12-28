// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ChangeTimer.cs
using System;
using System.Windows.Forms;

namespace LJCWinFormCommon
{
  // Provides Change Processing functionality.
  /// <include path='items/ChangeTimer/*' file='Doc/ProjectCommon.xml'/>
  public class ChangeTimer
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ChangeTimer()
    {
      mChangeTimer = new Timer()
      {
        Interval = 100
      };
      mChangeTimer.Tick += ChangeTimer_Tick;
    }
    #endregion

    #region Methods

    // Uses a timer to allow the list to process outstanding messages.
    /// <include path='items/DoChange/*' file='Doc/ChangeTimer.xml'/>
    public void DoChange(string changeName)
    {
      ChangeName = changeName;
      mChangeTimer.Start();
    }
    #endregion

    #region Control Event Handlers

    /// <summary>Fires the ItemChange event.</summary>
    protected void OnItemChange()
    {
      ItemChange?.Invoke(this, new EventArgs());
    }

    // The Timer event handler.
    private void ChangeTimer_Tick(object sender, EventArgs e)
    {
      mChangeTimer.Stop();
      OnItemChange();
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the ChangeName value.</summary>
    public string ChangeName { get; set; }
    #endregion

    #region Class Data

    /// <summary>The Item Change event.</summary>
    public event EventHandler<EventArgs> ItemChange;

    private readonly Timer mChangeTimer;
    #endregion
  }
}
