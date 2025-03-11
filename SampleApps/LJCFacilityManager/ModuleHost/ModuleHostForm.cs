// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ModuleHostForm.cs
using System;
using System.Reflection;
using System.Windows.Forms;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;

namespace ModuleHost
{
	/// <summary>The Module Host form.</summary>
	public partial class ModuleHostForm : Form
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/ModuleHostFormC/*' file='Doc/ModuleHostForm.xml'/>
		public ModuleHostForm(string fileName, string moduleName)
		{
			InitializeComponent();
			FileName = fileName;
			ModuleName = moduleName;
			mModuleReference = new ModuleReference()
			{
				FileName = fileName,
				ModuleName = moduleName
			};
		}
		#endregion

		#region Form Event Handlers

		// Configures the form and loads the initial control data.
		private void ModuleHostForm_Load(object sender, EventArgs e)
		{
			// Executes the LJCInit() method if not already created.
			TabControl tabControl = mModuleReference.GetTabControl();
			if (tabControl != null)
			{
				// Add the tab pages the first time it is requested.
				foreach (TabPage tabPage in tabControl.TabPages)
				{
					if (!FormTabs.Contains(tabPage))
					{
						tabPage.Parent = FormTabs;
						SetPageCloseEventHandler(mModuleReference);
					}
				}

				// Show the selected page.
				if (mModuleReference.TabPageNames.Count > 0)
				{
					string pageName = mModuleReference.TabPageNames[0];
					TabPage page = FormTabs.TabPages[pageName];
					if (page != null)
					{
						FormTabs.SelectedTab = FormTabs.TabPages[pageName];
					}
				}
			}
			CenterToScreen();
		}
		#endregion

		#region Methods

		// Adds the Module_PageClose event handler to the module PageClose event.
		private void SetPageCloseEventHandler(ModuleReference moduleReference)
		{
			Delegate eventDelegate;

			// Get the delegate type.
			EventInfo closeEventInfo = moduleReference.GetEventInfo();
			Type delegateType = closeEventInfo.EventHandlerType;

			// Get MethodInfo for the event handler method.
			MethodInfo handlerInfo = this.GetType().GetMethod("Module_PageClose"
				, BindingFlags.NonPublic | BindingFlags.Instance);

			// Create an instance of the delegate.
			eventDelegate = Delegate.CreateDelegate(delegateType, this, handlerInfo);

			// Get the "add" accessor info of the event.
			MethodInfo addHandlerInfo = closeEventInfo.GetAddMethod();

			// Invoke the event "add" accessor with the event delegate.
			object[] addHandlerArgs = { eventDelegate };
			addHandlerInfo.Invoke(moduleReference.ControlInstance, addHandlerArgs);
		}
		#endregion

		#region Event Handlers

		// The PageClose event handler.
		private void Module_PageClose(object sender, EventArgs e)
		{
			PropertyInfo propertyInfo;
			LJCTabControl tabControl;
			TabPage closeTabPage;

			//UserControl module = sender as UserControl;
			//LJCReflect ljcReflect = new LJCReflect(module);
			//string programName = ljcReflect.GetString("LJCProgramName");
			//if (null == programName)
			//{
			//	programName = module.ProductName + ".exe";
			//}

			// Get the close tab page.
			if (null == mModuleReference)
			{
				string title = "Program Error";
				string message = "The internal Module Reference cannot be found.";
				MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				propertyInfo = mModuleReference.ControlType.GetProperty("CloseTabPage");
				MethodInfo methodInfo = propertyInfo.GetGetMethod();
				closeTabPage = (TabPage)methodInfo.Invoke(mModuleReference.ControlInstance, null);

				// Move page back to module tab control.
				tabControl = mModuleReference.GetTabControl() as LJCTabControl;
				closeTabPage.Parent = tabControl;
				if (0 == FormTabs.TabPages.Count)
				{
					Close();
				}
			}
		}
		#endregion

		#region Properties

		/// <summary>Gets or sets the FileName value.</summary>
		public string FileName
		{
			get { return mFileName; }
			set { mFileName = NetString.InitString(value); }
		}
		private string mFileName;

		/// <summary>Gets or sets the ModuleName value.</summary>
		public string ModuleName
		{
			get { return mModuleName; }
			set { mModuleName = NetString.InitString(value); }
		}
		private string mModuleName;
		#endregion

		#region Class Data

		private ModuleReference mModuleReference;
		#endregion
	}
}
