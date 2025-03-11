// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Main.cs
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.IO;
using System.Windows.Forms;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using LJCDBMessage;
using LJCDBClientLib;
using LJCAppManagerDAL;

namespace LJCAppManager
{
	/// <summary>
	/// Provides an application menu and workspace for dynamically loaded
	/// applications with user controls that contain LJCTabControl controls.
	/// </summary>
	public partial class Main : Form
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public Main()
		{
			Cursor = Cursors.WaitCursor;
			InitializeComponent();
			Cursor = Cursors.Default;
		}
		#endregion

		#region Form Event Handlers

		// Configures the form and loads the initial control data.
		private void Main_Load(object sender, EventArgs e)
		{
			bool success = false;

			Cursor = Cursors.WaitCursor;
			InitializeControls();

			AppManagerSplash splash = new AppManagerSplash(isSplash: true);
			splash.ShowDialog();

			// Check for current user rights.
			LogonPerson logonPerson = new LogonPerson();
			if (logonPerson != null)
			{
				if (logonPerson.IsAuthenticated())
				{
					try
					{
						mAppManagerUser = logonPerson.GetUserData();
					}
					catch (SystemException ex)
					{
						CreateTables(ex, mSettings.DataConfigName);
						mAppManagerUser = logonPerson.GetUserData();
					}

					if (mAppManagerUser != null)
					{
						success = true;
					}
				}
			}
			else
			{
				Close();
			}

			// Allow entry of alternate user.
			if (!success)
			{
				Logon dialog = new Logon();
				if (DialogResult.OK == dialog.ShowDialog())
				{
					success = true;
				}
			}

			if (!success)
			{
				Close();
			}
			else
			{
				LoadApplicationData();
				LoadMenu();
				CenterToScreen();
			}
			Cursor = Cursors.Default;
		}

		// Create the application tables.
		internal static void CreateTables(SystemException e, string dataConfigName)
		{
			string[] fileSpecs = {
				@"SQLScript\CreateAppManagerDataTables.sql"
			};

			int errorCode = ManagerCommon.GetMissingTableErrorCode(dataConfigName);
			if (e.HResult == errorCode)
			{
				if (FormCommon.CreateTablesPrompt(e.Message, fileSpecs))
				{
					if (!ManagerCommon.CreateTables(dataConfigName, fileSpecs))
					{
						throw new SystemException(e.Message);
					}
				}
			}
		}
		#endregion

		#region Module Event Handlers

		// The module PageClose event handler.
		private void Module_PageClose(object sender, EventArgs e)
		{
			ModuleReference moduleReference;
			PropertyInfo propertyInfo;
			LJCTabControl tabControl;
			TabPage closeTabPage;

			UserControl module = sender as UserControl;
			LJCReflect ljcReflect = new LJCReflect(module);
			string programName = ljcReflect.GetString("LJCProgramName");
			if (null == programName)
			{
				programName = module.ProductName + ".exe";
			}

			moduleReference = mModuleReferences.LJCSearch(programName, module.ToString()
				, new ModuleNameComparer());

			// Get the close tab page.
			if (null == moduleReference)
			{
				string title = "Program Error";
				string message = "The internal Module Reference cannot be found.";
				MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				propertyInfo = moduleReference.ControlType.GetProperty("CloseTabPage");
				MethodInfo methodInfo = propertyInfo.GetGetMethod();
				closeTabPage = (TabPage)methodInfo.Invoke(moduleReference.ControlInstance, null);

				// Move page back to module tab control.
				tabControl = moduleReference.GetTabControl() as LJCTabControl;
				closeTabPage.Parent = tabControl;
				if (0 == TileTabs.TabPages.Count)
				{
					TabSplit.Panel2Collapsed = true;
				}

				// Move all TileTabs to MainTabs.
				if (!TabSplit.Panel2Collapsed
					&& 0 == MainTabs.TabPages.Count)
				{
					foreach (TabPage page in TileTabs.TabPages)
					{
						page.Parent = MainTabs;
					}
					TabSplit.Panel2Collapsed = true;
				}
			}
			SetControlState();
		}
		#endregion

		#region Action Methods

		// Displays the selected module.
		private void DoShowModule()
		{
			ProgramReference programReference;
			ModuleReference moduleReference;
			TabControl tabControl;

			TreeNode node = MenuTree.SelectedNode;
			if (node.Parent != null)
			{
				Cursor = Cursors.WaitCursor;
				programReference = mProgramReferences.BinarySearch(node.Parent.Text);
				moduleReference = mModuleReferences.LJCSearch(programReference.FileName, node.Text);

				// Executes the LJCInit() method if not already created.
				tabControl = moduleReference.GetTabControl();
				if (tabControl != null)
				{
					// Add the tab pages the first time they are requested.
					foreach (TabPage tabPage in tabControl.TabPages)
					{
						if (!MainTabs.Contains(tabPage)
							&& !TileTabs.Contains(tabPage))
						{
							tabPage.Parent = MainTabs;
							SetPageCloseEventHandler(moduleReference);
							//moduleReference.SetPageCloseEventHandler(this);
						}
					}

					// Show the selected page.
					if (moduleReference.TabPageNames.Count > 0)
					{
						string pageName = moduleReference.TabPageNames[0];
						TabPage page = MainTabs.TabPages[pageName];
						if (page != null)
						{
							MainTabs.SelectedTab = MainTabs.TabPages[pageName];
						}
						else
						{
							page = TileTabs.TabPages[pageName];
							if (page != null)
							{
								TileTabs.SelectedTab = MainTabs.TabPages[pageName];
							}
						}
					}
				}
				Cursor = Cursors.Default;
			}
			SetControlState();
		}

		// Sets the control states based on the current control values.
		/// <include path='items/SetControlState/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void SetControlState()
		{
			MainTabMenuMove.Visible = false;
			if (MainTabs.TabPages.Count > 1)
			{
				MainTabMenuMove.Visible = true;
			}
		}
		#endregion

		#region Private Methods

		// Loads the AppManager application data.
		private void LoadApplicationData()
		{
			Cursor = Cursors.WaitCursor;
			mUserAppModules = new UserAppModules();

			// Load the data for the programs to which this user is authorized.
			var keyColumns = new DbColumns()
			{
				{ UserAppProgram.ColumnAppManagerUserID, mAppManagerUser.ID }
			};
			//userProgramManager = new UserAppProgramManager2(mDbServiceRef
			//	, mDataConfigName);
			var userProgramManager = mManagers.UserAppProgramManager;
			DbJoins dbJoins = userProgramManager.GetLoadJoins();
			mUserAppPrograms = userProgramManager.Load(keyColumns, joins: dbJoins);

			// Load the data for the modules for each program.
			foreach (UserAppProgram userAppProgram in mUserAppPrograms)
			{
				// Load the data for the program modules to which this user is authorized.
				//userModuleManager = new UserAppModuleManager2(mDbServiceRef
				//	, mDataConfigName);
				var userModuleManager = mManagers.UserAppModuleManager;
				keyColumns = userModuleManager.GetUserIDKeys(mAppManagerUser.ID
					, userAppProgram.AppProgramID);
				DbJoins joins = userModuleManager.GetLoadJoins();
				UserAppModules userAppModules = userModuleManager.Load(keyColumns
					, joins: joins);
				foreach (UserAppModule userAppModule in userAppModules)
				{
					mUserAppModules.Add(userAppModule);
				}
			}
			Cursor = Cursors.Default;
		}

		// Loads the menu with the allowed applications and modules.
		private void LoadMenu()
		{
			List<UserAppModule> userAppModules;

			Cursor = Cursors.WaitCursor;
			mProgramReferences = new ProgramReferences();
			mModuleReferences = new ModuleReferences();
			foreach (UserAppProgram userAppProgram in mUserAppPrograms)
			{
				// Create a program reference for each program and add a program node to the tree control.
				mProgramReferences.Add(userAppProgram.FileName, userAppProgram.Title);
				TreeNode appNode = MenuTree.Nodes.Add(userAppProgram.Title);
				appNode.ImageIndex = 0;
				appNode.SelectedImageIndex = 0;

				userAppModules = mUserAppModules.FindAll(x => x.AppProgramID == userAppProgram.AppProgramID);
				if (userAppModules != null)
				{
					// Create a module reference for each module and add a child node to the parent program node.
					foreach (UserAppModule userAppModule in userAppModules)
					{
						mModuleReferences.Add(userAppProgram.FileName, userAppModule.TypeName, userAppModule.ModuleTitle);
						TreeNode moduleNode = appNode.Nodes.Add(userAppModule.ModuleTitle);
						moduleNode.ImageIndex = 1;
						moduleNode.SelectedImageIndex = 1;
					}
				}
			}
			Cursor = Cursors.Default;
		}

		// Adds the Module_PageClose event handler to the module PageClose event.
		private void SetPageCloseEventHandler(ModuleReference moduleReference)
		{
			Delegate eventDelegate;

			// Get the delegate type.
			Cursor = Cursors.WaitCursor;
			EventInfo closeEventInfo = moduleReference.GetEventInfo();
			Type delegateType = closeEventInfo.EventHandlerType;

			// Get MethodInfo for the event handler method.
			MethodInfo handlerInfo = GetType().GetMethod("Module_PageClose"
				, BindingFlags.NonPublic | BindingFlags.Instance);

			// Create an instance of the delegate.
			eventDelegate = Delegate.CreateDelegate(delegateType, this, handlerInfo);

			// Get the "add" accessor info of the event.
			MethodInfo addHandlerInfo = closeEventInfo.GetAddMethod();

			// Invoke the event "add" accessor with the event delegate.
			object[] addHandlerArgs = { eventDelegate };
			addHandlerInfo.Invoke(moduleReference.ControlInstance, addHandlerArgs);
			Cursor = Cursors.Default;
		}
		#endregion

		#region Setup Methods

		// Configures the controls and loads the selection control data.
		private void InitializeControls()
		{
			// Get singleton values.
			Cursor = Cursors.WaitCursor;
			var values = ValuesAppManager.Instance;

			mSettings = values.StandardSettings;

			// Initialize Class Data.
			mManagers = new AppManagers(mSettings.DbServiceRef
			, mSettings.DataConfigName);

			// Load control data.

			// Configure controls.
			MenuTree.BackColor = Color.AliceBlue;
			MenuHeader.LJCClosePanel += MenuHeader_LJCClosePanel;

			// Set initial control values.
			NetFile.CreateFolder("ControlValues");
			mControlValuesFileName = @"ControlValues\AppManager.xml";

			ConfigureControls();
			RestoreControlValues();
			Cursor = Cursors.Default;
		}

		// Configure the initial control settings.
		private void ConfigureControls()
		{
			// Setup panel manager for main tab splitter.
			mLJCPanelManager = new LJCPanelManager(TabSplit, MainTabs, TileTabs);

			// Make sure lists scroll vertically and counter labels show.
			if (AutoScaleMode == AutoScaleMode.Font)
			{
				MenuSplit.SplitterDistance = MenuSplit.Width / 5;
				TabSplit.SplitterDistance = TabSplit.Width / 2;
			}
		}

		// Saves the control values. 
		private void SaveControlValues()
		{
			ControlValues controlValues = new ControlValues();

			// Save Splitter values.
			controlValues.Add("MenuSplit.SplitterDistance", 0, 0, 0, MenuSplit.SplitterDistance);

			// Save Window values.
			controlValues.Add(Name, Left, Top, Width, Height);

			NetCommon.XmlSerialize(controlValues.GetType(), controlValues, null
				, mControlValuesFileName);
		}

		// Restores the control values.
		private void RestoreControlValues()
		{
			ControlValue controlValue;

			if (File.Exists(mControlValuesFileName))
			{
				ControlValues = NetCommon.XmlDeserialize(typeof(ControlValues)
					, mControlValuesFileName) as ControlValues;
				if (ControlValues != null)
				{
					// Restore Window values.
					controlValue = ControlValues.LJCSearchName(Name);
					if (controlValue != null)
					{
						Left = controlValue.Left;
						Top = controlValue.Top;
						Width = controlValue.Width;
						Height = controlValue.Height;
					}

					// Restore Splitter and other values.
					FormCommon.RestoreSplitDistance(MenuSplit, ControlValues);
				}
			}
		}

		/// <summary>Gets or sets the Allow Change value.</summary>
		public ControlValues ControlValues { get; set; }
		#endregion

		#region Action Event Handlers

		#region Tree Menu

		// Displays the selected node.
		private void TreeMenuShow_Click(object sender, EventArgs e)
		{
			DoShowModule();
		}

		// Exits the application.
		private void TreeMenuExit_Click(object sender, EventArgs e)
		{
			SaveControlValues();
			Close();
		}

		// Display the help page.
		private void TreeMenuHelp_Click(object sender, EventArgs e)
		{
			Help.ShowHelp(this, "AppManager.chm", HelpNavigator.Topic
				, "MenuTree.htm");
		}

		// Displays the Splash dialog as an about dialog.
		private void TreeMenuAbout_Click(object sender, EventArgs e)
		{
			AppManagerSplash splash = new AppManagerSplash();
			splash.ShowDialog();
		}
		#endregion

		#region TabPanels

		// 
		private void MainTabMenuShow_Click(object sender, EventArgs e)
		{
			if (MainTabMenuShow.Text == "Show Menu")
			{
				MenuSplit.Panel1Collapsed = false;
				MainTabMenuShow.Text = "Hide Menu";
				TileTabMenuShow.Text = "Hide Menu";
			}
			else
			{
				MenuSplit.Panel1Collapsed = true;
				MainTabMenuShow.Text = "Show Menu";
				TileTabMenuShow.Text = "Show Menu";
			}
		}

		// 
		private void MainTabMenuMove_Click(object sender, EventArgs e)
		{
			//ToolStripItem menuItem = sender as ToolStripItem;
			if (MainTabs.TabPages.Count > 1)
			{
				TabSplit.Panel2Collapsed = false;
				MainTabs.SelectedTab.Parent = TileTabs;
			}
		}

		// 
		private void TileTabMenuShow_Click(object sender, EventArgs e)
		{
			if (MainTabMenuShow.Text == "Show Menu")
			{
				MenuSplit.Panel1Collapsed = false;
				MainTabMenuShow.Text = "Hide Menu";
				TileTabMenuShow.Text = "Hide Menu";
			}
			else
			{
				MenuSplit.Panel1Collapsed = true;
				MainTabMenuShow.Text = "Show Menu";
				TileTabMenuShow.Text = "Show Menu";
			}
		}

		// 
		private void TileTabMenuMove_Click(object sender, EventArgs e)
		{
			TileTabs.SelectedTab.Parent = MainTabs;
			if (0 == TileTabs.TabPages.Count)
			{
				TabSplit.Panel2Collapsed = true;
			}
		}
		#endregion
		#endregion

		#region Control Event Handlers

		#region MenuTree

		// Handles the control key values.
		private void MenuTree_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F1:
					Help.ShowHelp(this, "AppManager.chm", HelpNavigator.Topic
						, "MenuTree.htm");
					break;
			}
		}

		// Selects the clicked item.
		private void MenuTree_MouseDown(object sender, MouseEventArgs e)
		{
			TreeViewHitTestInfo info;

			// Set current node to selected node.
			info = MenuTree.HitTest(e.X, e.Y);
			if (info.Node != null)
			{
				if (info.Node.Parent != null)
				{
					TreeMenuShow.Visible = true;
					MenuTree.SelectedNode = info.Node;
				}
				else
				{
					TreeMenuShow.Visible = false;
				}
			}
		}

		// Displays the selected module.
		private void MenuTree_DoubleClick(object sender, EventArgs e)
		{
			DoShowModule();
		}

		// 
		private void MenuHeader_LJCClosePanel(object sender, EventArgs e)
		{
			if (MainTabs.TabCount > 0)
			{
				MenuSplit.Panel1Collapsed = true;
				MainTabMenuShow.Text = "Show Menu";
			}
		}

		// Keep node highlighted even if the treeview is not in focus.
		private void MenuTree_DrawNode(object sender, DrawTreeNodeEventArgs e)
		{
			// DrawMode = OwnerDrawText
			// FullRowSelect = false
			// HideSelection = false

			if (e.Node == null)
			{
				return;
			}

			// if treeview's HideSelection property is "True", 
			// this will always returns "False" on unfocused treeview
			bool selected = (e.State & TreeNodeStates.Selected) == TreeNodeStates.Selected;
			bool unfocused = !e.Node.TreeView.Focused;

			// we need to do owner drawing only on a selected node
			// and when the treeview is unfocused, else let the OS do it for us
			if (selected && unfocused)
			{
				Font font = e.Node.NodeFont ?? e.Node.TreeView.Font;

				//e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds);
				SolidBrush nodeColor = new SolidBrush(mSettings.EndColor);
				e.Graphics.FillRectangle(nodeColor, e.Bounds);

				TextRenderer.DrawText(e.Graphics, e.Node.Text, font, e.Bounds, SystemColors.HighlightText
					, TextFormatFlags.GlyphOverhangPadding);
			}
			else
			{
				e.DrawDefault = true;
			}
		}
		#endregion
		#endregion

		#region Class Data

		private StandardUISettings mSettings;
		private string mControlValuesFileName;
		private AppManagers mManagers;
		private AppUser mAppManagerUser;
		private UserAppPrograms mUserAppPrograms;
		private UserAppModules mUserAppModules;

		private LJCPanelManager mLJCPanelManager;
		private ProgramReferences mProgramReferences;
		private ModuleReferences mModuleReferences;
		#endregion
	}
}
