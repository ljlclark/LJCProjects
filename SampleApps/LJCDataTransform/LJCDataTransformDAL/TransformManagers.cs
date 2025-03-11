// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TransformManagers.cs
using System;
using LJCDBClientLib;

namespace LJCDataTransformDAL
{
	/// <summary>Gets the LJCTransformDAL Manager objects.</summary>
	public class TransformManagers
	{
		#region Constructors

		// Initializes an object instance.
		/// <summary>
		/// Initializes an object instance.
		/// </summary>
		/// <param name="dbServiceRef">The database service reference object.</param>
		/// <param name="dataConfigName">The data configuration name.</param>
		public TransformManagers(DbServiceRef dbServiceRef, string dataConfigName)
		{
			mDbServiceRef = dbServiceRef;
			DataConfigName = dataConfigName;
		}
		#endregion

		#region Properties

		/// <summary>Gets the DataConfigName value.</summary>
		public string DataConfigName { get; private set; }

		/// <summary>Gets the DataProcessManager object.</summary>
		public DataProcessManager DataProcessManager
		{
			get
			{
				if (null == mDataProcessManager)
				{
					DataProcessManager
						= new DataProcessManager(mDbServiceRef, DataConfigName);
				}
				return mDataProcessManager;
			}

			private set
			{
				if (value != null)
				{
					mDataProcessManager = value;
				}
			}
		}

		/// <summary>Gets the DataSourceManager object.</summary>
		public DataSourceManager DataSourceManager
		{
			get
			{
				if (null == mDataSourceManager)
				{
					DataSourceManager
						= new DataSourceManager(mDbServiceRef, DataConfigName);
				}
				return mDataSourceManager;
			}

			private set
			{
				if (value != null)
				{
					mDataSourceManager = value;
				}
			}
		}

		/// <summary>Gets the DataTypeManager object.</summary>
		public DataTypeManager DataTypeManager
		{
			get
			{
				if (null == mDataTypeManager)
				{
					DataTypeManager
						= new DataTypeManager(mDbServiceRef, DataConfigName);
				}
				return mDataTypeManager;
			}

			private set
			{
				if (value != null)
				{
					mDataTypeManager = value;
				}
			}
		}

		/// <summary>Gets the LayoutColumnManager object.</summary>
		public LayoutColumnManager LayoutColumnManager
		{
			get
			{
				if (null == mLayoutColumnManager)
				{
					LayoutColumnManager
						= new LayoutColumnManager(mDbServiceRef, DataConfigName);
				}
				return mLayoutColumnManager;
			}

			private set
			{
				if (value != null)
				{
					mLayoutColumnManager = value;
				}
			}
		}

		/// <summary>Gets the ProcessGroupManager object.</summary>
		public ProcessGroupManager ProcessGroupManager
		{
			get
			{
				if (null == mProcessGroupManager)
				{
					ProcessGroupManager
						= new ProcessGroupManager(mDbServiceRef, DataConfigName);
				}
				return mProcessGroupManager;
			}

			private set
			{
				if (value != null)
				{
					mProcessGroupManager = value;
				}
			}
		}

		/// <summary>Gets the ProcessGroupProcessManager object.</summary>
		public ProcessGroupProcessManager ProcessGroupProcessManager
		{
			get
			{
				if (null == mProcessGroupProcessManager)
				{
					ProcessGroupProcessManager
						= new ProcessGroupProcessManager(mDbServiceRef, DataConfigName);
				}
				return mProcessGroupProcessManager;
			}

			private set
			{
				if (value != null)
				{
					mProcessGroupProcessManager = value;
				}
			}
		}

		/// <summary>Gets the ProcessStatusManager object.</summary>
		public ProcessStatusManager ProcessStatusManager
		{
			get
			{
				if (null == mProcessStatusManager)
				{
					ProcessStatusManager
						= new ProcessStatusManager(mDbServiceRef, DataConfigName);
				}
				return mProcessStatusManager;
			}

			private set
			{
				if (value != null)
				{
					mProcessStatusManager = value;
				}
			}
		}

		/// <summary>Gets the SourceLayoutManager object.</summary>
		public SourceLayoutManager SourceLayoutManager
		{
			get
			{
				if (null == mSourceLayoutManager)
				{
					SourceLayoutManager
						= new SourceLayoutManager(mDbServiceRef, DataConfigName);
				}
				return mSourceLayoutManager;
			}

			private set
			{
				if (value != null)
				{
					mSourceLayoutManager = value;
				}
			}
		}

		/// <summary>Gets the SourceStatusManager object.</summary>
		public SourceStatusManager SourceStatusManager
		{
			get
			{
				if (null == mSourceStatusManager)
				{
					SourceStatusManager
						= new SourceStatusManager(mDbServiceRef, DataConfigName);
				}
				return mSourceStatusManager;
			}

			private set
			{
				if (value != null)
				{
					mSourceStatusManager = value;
				}
			}
		}

		/// <summary>Gets the SourceTypeManager object.</summary>
		public SourceTypeManager SourceTypeManager
		{
			get
			{
				if (null == mSourceTypeManager)
				{
					SourceTypeManager
						= new SourceTypeManager(mDbServiceRef, DataConfigName);
				}
				return mSourceTypeManager;
			}

			private set
			{
				if (value != null)
				{
					mSourceTypeManager = value;
				}
			}
		}

		/// <summary>Gets the StepManager object.</summary>
		public StepManager StepManager
		{
			get
			{
				if (null == mStepManager)
				{
					StepManager
						= new StepManager(mDbServiceRef, DataConfigName);
				}
				return mStepManager;
			}

			private set
			{
				if (value != null)
				{
					mStepManager = value;
				}
			}
		}

		/// <summary>Gets the StepTaskManager object.</summary>
		public StepTaskManager StepTaskManager
		{
			get
			{
				if (null == mStepTaskManager)
				{
					StepTaskManager
						= new StepTaskManager(mDbServiceRef, DataConfigName);
				}
				return mStepTaskManager;
			}

			private set
			{
				if (value != null)
				{
					mStepTaskManager = value;
				}
			}
		}

		/// <summary>Gets the TaskSourceManager object.</summary>
		public TaskSourceManager TaskSourceManager
		{
			get
			{
				if (null == mTaskSourceManager)
				{
					TaskSourceManager
						= new TaskSourceManager(mDbServiceRef, DataConfigName);
				}
				return mTaskSourceManager;
			}

			private set
			{
				if (value != null)
				{
					mTaskSourceManager = value;
				}
			}
		}

		/// <summary>Gets the TaskStatusManager object.</summary>
		public TaskStatusManager TaskStatusManager
		{
			get
			{
				if (null == mTaskStatusManager)
				{
					TaskStatusManager
						= new TaskStatusManager(mDbServiceRef, DataConfigName);
				}
				return mTaskStatusManager;
			}

			private set
			{
				if (value != null)
				{
					mTaskStatusManager = value;
				}
			}
		}

		/// <summary>Gets the TaskTransformManager object.</summary>
		public TaskTransformManager TaskTransformManager
		{
			get
			{
				if (null == mTaskTransformManager)
				{
					TaskTransformManager
						= new TaskTransformManager(mDbServiceRef, DataConfigName);
				}
				return mTaskTransformManager;
			}

			private set
			{
				if (value != null)
				{
					mTaskTransformManager = value;
				}
			}
		}

		/// <summary>Gets the TaskTypeManager object.</summary>
		public TaskTypeManager TaskTypeManager
		{
			get
			{
				if (null == mTaskTypeManager)
				{
					TaskTypeManager
						= new TaskTypeManager(mDbServiceRef, DataConfigName);
				}
				return mTaskTypeManager;
			}

			private set
			{
				if (value != null)
				{
					mTaskTypeManager = value;
				}
			}
		}

		/// <summary>Gets the TransformMapManager object.</summary>
		public TransformMapManager TransformMapManager
		{
			get
			{
				if (null == mTransformMapManager)
				{
					TransformMapManager
						= new TransformMapManager(mDbServiceRef, DataConfigName);
				}
				return mTransformMapManager;
			}

			private set
			{
				if (value != null)
				{
					mTransformMapManager = value;
				}
			}
		}

		/// <summary>Gets the TransformMatchManager object.</summary>
		public TransformMatchManager TransformMatchManager
		{
			get
			{
				if (null == mTransformMatchManager)
				{
					TransformMatchManager
						= new TransformMatchManager(mDbServiceRef, DataConfigName);
				}
				return mTransformMatchManager;
			}

			private set
			{
				if (value != null)
				{
					mTransformMatchManager = value;
				}
			}
		}
		#endregion

		#region Class Data

		private readonly DbServiceRef mDbServiceRef;

		private DataProcessManager mDataProcessManager;
		private DataSourceManager mDataSourceManager;
		private DataTypeManager mDataTypeManager;
		private LayoutColumnManager mLayoutColumnManager;
		private ProcessGroupManager mProcessGroupManager;
		private ProcessGroupProcessManager mProcessGroupProcessManager;
		private ProcessStatusManager mProcessStatusManager;
		private SourceLayoutManager mSourceLayoutManager;
		private SourceStatusManager mSourceStatusManager;
		private SourceTypeManager mSourceTypeManager;
		private StepManager mStepManager;
		private StepTaskManager mStepTaskManager;
		private TaskSourceManager mTaskSourceManager;
		private TaskStatusManager mTaskStatusManager;
		private TaskTransformManager mTaskTransformManager;
		private TaskTypeManager mTaskTypeManager;
		private TransformMapManager mTransformMapManager;
		private TransformMatchManager mTransformMatchManager;
		#endregion
	}
}
