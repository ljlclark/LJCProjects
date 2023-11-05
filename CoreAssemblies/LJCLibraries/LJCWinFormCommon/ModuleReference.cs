// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ModuleReference.cs
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using LJCNetCommon;

namespace LJCWinFormCommon
{
  /// <summary>Represents a module reference.</summary>
  public class ModuleReference : IComparable<ModuleReference>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public ModuleReference()
    {
    }
    #endregion

    #region Public Methods

    // Retrieves the Assembly reference.
    /// <include path='items/GetAssembly/*' file='Doc/ModuleReference.xml'/>
    public Assembly GetAssembly()
    {
      SetAssembly();
      return Assembly;
    }

    // Retrieves the ControlType reference.
    /// <include path='items/GetControlType/*' file='Doc/ModuleReference.xml'/>
    public Type GetControlType()
    {
      if (SetAssembly())
      {
        SetControlType();
      }
      return ControlType;
    }

    // Retrieves the ControlInstance reference.
    /// <include path='items/GetControlInstance/*' file='Doc/ModuleReference.xml'/>
    public object GetControlInstance()
    {
      if (SetAssembly())
      {
        if (SetControlType())
        {
          SetControlInstance();
        }
      }
      return ControlInstance;
    }

    // Retrieves the LJCInit() MethodInfo reference.
    /// <include path='items/GetInitMethodInfo/*' file='Doc/ModuleReference.xml'/>
    public MethodInfo GetInitMethodInfo()
    {
      bool success = false;
      MethodInfo retValue = null;

      if (SetAssembly())
      {
        if (SetControlType())
        {
          if (SetControlInstance())
          {
            success = true;
          }
        }
      }
      if (success)
      {
        SetMethodInfo("LJCInit");
        if (InitMethodInfo != null)
        {
          retValue = InitMethodInfo;
        }
      }
      return retValue;
    }

    // Retrieves the LJCTabs() MethodInfo reference.
    /// <include path='items/GetTabsMethodInfo/*' file='Doc/ModuleReference.xml'/>
    public MethodInfo GetTabsMethodInfo()
    {
      bool success = false;
      MethodInfo retValue = null;

      if (SetAssembly())
      {
        if (SetControlType())
        {
          if (SetControlInstance())
          {
            success = true;
          }
        }
      }
      if (success)
      {
        SetMethodInfo("LJCInit");
        SetMethodInfo("LJCTabs");
        if (TabsMethodInfo != null)
        {
          retValue = TabsMethodInfo;
        }
      }
      return retValue;
    }

    // Retrieves the TabControl reference.
    /// <include path='items/GetTabControl/*' file='Doc/ModuleReference.xml'/>
    public TabControl GetTabControl()
    {
      TabControl retValue = null;

      if (GetTabsMethodInfo() != null)
      {
        if (SetTabControl())
        {
          retValue = TabControl;
          SetEventInfo();
        }
      }
      return retValue;
    }

    // Retrieves the PageClose event info.
    /// <include path='items/GetEventInfo/*' file='Doc/ModuleReference.xml'/>
    public EventInfo GetEventInfo()
    {
      EventInfo retValue = null;

      GetControlInstance();
      SetEventInfo();
      if (CloseEventInfo != null)
      {
        retValue = CloseEventInfo;
      }
      return retValue;
    }

    // Adds the Module_PageClose event handler to the module PageClose event.
    /// <include path='items/SetPageCloseEventHandler/*' file='Doc/ModuleReference.xml'/>
    public void SetPageCloseEventHandler(Form parentForm)
    {
      Delegate eventDelegate;

      // Get the delegate type.
      EventInfo closeEventInfo = GetEventInfo();
      Type delegateType = closeEventInfo.EventHandlerType;

      // Get MethodInfo for the event handler method.
      MethodInfo handlerInfo = parentForm.GetType().GetMethod("Module_PageClose"
        , BindingFlags.Public | BindingFlags.Instance);

      // Create an instance of the delegate.
      eventDelegate = Delegate.CreateDelegate(delegateType, this, handlerInfo);

      // Get the "add" accessor info of the event.
      MethodInfo addHandlerInfo = closeEventInfo.GetAddMethod();

      // Invoke the event "add" accessor with the event delegate.
      object[] addHandlerArgs = { eventDelegate };
      addHandlerInfo.Invoke(ControlInstance, addHandlerArgs);
    }
    #endregion

    #region Private Methods

    // Set the Assembly reference.
    private bool SetAssembly()
    {
      string errorText;
      bool retValue = true;

      if (null == Assembly)
      {
        if (string.IsNullOrWhiteSpace(FileName))
        {
          //retValue = false;
          errorText = "The ModuleReference.FileName value is not set.";
          throw new MissingMemberException(errorText);
        }
        else
        {
          if (NetString.IsEqual(FileName, "LJC.AppManager.exe"))
          {
            //Assembly = Assembly.GetExecutingAssembly();
            Assembly = Assembly.GetEntryAssembly();
          }
          else
          {
            if (false == File.Exists(FileName))
            {
              //retValue = false;
              errorText = $"The file '{FileName}' was not found.";
              throw new FileNotFoundException(errorText);
            }
            else
            {
              Assembly = Assembly.LoadFrom(FileName);
            }
          }
        }
      }
      return retValue;
    }

    // Set the ControlType reference.
    private bool SetControlType()
    {
      string errorText;
      bool retValue = true;

      if (null == ControlType)
      {
        if (string.IsNullOrWhiteSpace(ModuleName))
        {
          //retValue = false;
          errorText = "The ModuleReference.ModuleName value is not set.";
          throw new MissingMemberException(errorText);
        }
        else
        {
          ControlType = Assembly.GetType(ModuleName);
          if (null == ControlType)
          {
            //retValue = false;
            errorText = $"The module '{ModuleName}' was not found.";
            throw new MissingMemberException(errorText);
          }
        }
      }
      return retValue;
    }

    // Set the ControlInstance reference.
    private bool SetControlInstance()
    {
      string errorText;
      bool retValue = true;

      if (null == ControlInstance)
      {
        ControlInstance = Activator.CreateInstance(ControlType);
        if (null == ControlInstance)
        {
          //retValue = false;
          errorText = $"Unable to create ControlInstance for '{ControlType.Name}'.";
          throw new InvalidOperationException(errorText);
        }
      }
      return retValue;
    }

    // Set the MethodInfo reference.
    private void SetMethodInfo(string methodName)
    {
      switch (methodName)
      {
        case "LJCInit":
          if (null == InitMethodInfo)
          {
            InitMethodInfo = ControlType.GetMethod(methodName);
            InitMethodInfo.Invoke(ControlInstance, null);
          }
          break;
        case "LJCTabs":
          if (null == TabsMethodInfo)
          {
            TabsMethodInfo = ControlType.GetMethod(methodName);
          }
          break;
      }
    }

    // Set the TabControl reference.
    private bool SetTabControl()
    {
      string errorText;
      bool retValue = true;

      if (null == TabControl)
      {
        TabControl = (TabControl)TabsMethodInfo.Invoke(ControlInstance, null);
        if (null == TabControl)
        {
          //retValue = false;
          errorText = "Unable to create TabControl.";
          throw new InvalidOperationException(errorText);
        }
        else
        {
          TabPageNames = new List<string>();
          foreach (TabPage tabPage in TabControl.TabPages)
          {
            TabPageNames.Add(tabPage.Name);
          }
        }
      }
      return retValue;
    }

    // Sets the CloseEventInfo reference.
    private void SetEventInfo()
    {
      if (null == CloseEventInfo)
      {
        CloseEventInfo = ControlType.GetEvent("PageClose");
      }
    }
    #endregion

    #region IComparable Methods

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public int CompareTo(ModuleReference other)
    {
      int retValue;

      if (null == other)
      {
        retValue = 1;
      }
      else
      {
        retValue = FileName.CompareTo(other.FileName);
        if (0 == retValue)
        {
          retValue = ModuleDisplayName.CompareTo(other.ModuleDisplayName);
        }
      }
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>The parent assembly file name.</summary>
    public string FileName { get; set; }

    /// <summary>The assembly reference.</summary>
    public Assembly Assembly { get; set; }

    /// <summary>The module name.</summary>
    public string ModuleName { get; set; }

    /// <summary>The module display name.</summary>
    public string ModuleDisplayName { get; set; }

    /// <summary>The control type reference.</summary>
    public Type ControlType { get; set; }

    /// <summary>The control object instance.</summary>
    public object ControlInstance { get; set; }

    /// <summary>The init method reference.</summary>
    public MethodInfo InitMethodInfo { get; set; }

    /// <summary>The tabs method reference.</summary>
    public MethodInfo TabsMethodInfo { get; set; }

    /// <summary>The tab control reference.</summary>
    public TabControl TabControl { get; set; }

    /// <summary>The tab control reference.</summary>
    public List<string> TabPageNames { get; private set; }

    /// <summary>The tab control reference.</summary>
    public EventInfo CloseEventInfo { get; private set; }
    #endregion
  }
}
