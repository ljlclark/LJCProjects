// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// Calendar.cs
using System;
using System.Windows.Forms;
using LJCNetCommon;

namespace LJCWinFormControls
{
  // The calendar form.
  /// <include path='items/Calendar/*' file='Doc/ProjectControls.xml'/>
  public partial class Calendar : Form
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public Calendar()
    {
      InitializeComponent();
    }
    #endregion

    #region Form Events Handlers

    // Configures the form and loads the initial control data.
    private void Calendar_Load(object sender, EventArgs e)
    {
      InitializeControls();

      CenterToParent();
    }
    #endregion

    #region Setup Methods

    // Configures the controls and loads the selection control data.
    /// <include path='items/InitializeControls/*' file='../../../CoreUtilities/LJCDocLib/Common/Detail.xml'/>
    private void InitializeControls()
    {
      DateTime startDate;

      if (NetString.HasValue(mStartDate)
        && mStartDate.Trim() != "/  /")
      {
        startDate = DateTime.Parse(mStartDate);
        CalendarControl.SelectionStart = startDate;
        CalendarControl.SelectionEnd = startDate;
      }

      mTimer = new Timer()
      {
        Interval = 200
      };
      mTimer.Tick += new EventHandler(Timer_Tick);
      mIsActiveSelect = false;
    }
    #endregion

    #region Action Event Handlers

    // Select the chosen date.
    private void CalendarMenuDate_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.OK;
    }

    // Select Today.
    private void MenuTodayItem_Click(object sender, EventArgs e)
    {
      CalendarControl.SelectionStart = DateTime.Now;
      CalendarControl.SelectionEnd = DateTime.Now;
    }

    // Close the Calendar.
    private void MenuCloseItem_Click(object sender, EventArgs e)
    {
      Close();
    }
    #endregion

    #region Control Event Handlers

    // Handles the KeyDown event.
    private void CalendarControl_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Escape:
          Close();
          break;

        case Keys.Enter:
          DialogResult = DialogResult.OK;
          break;
      }
    }

    // Handles the MouseDown event.
    private void CalendarControl_MouseDown(object sender, MouseEventArgs e)
    {
      if (MouseButtons.Right == e.Button)
      {
        MonthCalendar.HitTestInfo info;

        info = CalendarControl.HitTest(e.X, e.Y);
        if (info.HitArea == MonthCalendar.HitArea.Date)
        {
          CalendarControl.SelectionStart = info.Time;
        }
      }
      else
      {
        if (mIsActiveSelect)
        {
          DialogResult = DialogResult.OK;
        }
        else
        {
          mIsActiveSelect = true;
          mTimer.Start();
        }
      }
    }

    // Handles the Timer Tick event.
    void Timer_Tick(object sender, EventArgs e)
    {
      mTimer.Stop();
      mIsActiveSelect = false;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the StartDate value.</summary>
    public string LJCStartDate
    {
      get
      {
        return mStartDate;
      }
      set
      {
        mStartDate = NetString.InitString(value);
      }
    }
    private string mStartDate;

    /// <summary>Get or sets the SelectedDate value.</summary>
    public DateTime LJCSelectedDate
    {
      get
      {
        return CalendarControl.SelectionStart;
      }
    }

    /// <summary>Gets or sets the CalendarControl reference.</summary>
    public MonthCalendar LJCCalendarControl
    {
      get
      {
        return CalendarControl;
      }
    }
    #endregion

    #region Class Data

    private Timer mTimer;
    private bool mIsActiveSelect;
    #endregion
  }
}
