// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// FacilityManagers.cs
using System;
using LJCNetCommon;
using LJCDBClientLib;

namespace LJCFacilityManagerDAL
{
	/// <summary>Gets the Facility Manager objects.</summary>
	public class FacilityManagers
	{
		#region Constructors

		// Initializes an object instance.
		/// <summary>
		/// Initializes an object instance.
		/// </summary>
		/// <param name="dbServiceRef">The database service reference object.</param>
		/// <param name="dataConfigName">The data configuration name.</param>
		public FacilityManagers(DbServiceRef dbServiceRef, string dataConfigName)
		{
			mDbServiceRef = dbServiceRef;
			DataConfigName = dataConfigName;
		}
		#endregion

		#region Properties

		/// <summary>Gets the FacilityDbManager object.</summary>
		public FacilityDbManager FacilityDbManager
		{
			get
			{
				if (null == mFacilityDbManager)
				{
					FacilityDbManager
						= new FacilityDbManager(mDbServiceRef, DataConfigName);
				}
				return mFacilityDbManager;
			}

			private set
			{
				if (value != null)
				{
					mFacilityDbManager = value;
				}
			}
		}

		/// <summary>Gets the UnitManager object.</summary>
		public UnitManager UnitManager
		{
			get
			{
				if (null == mUnitManager)
				{
					UnitManager
						= new UnitManager(mDbServiceRef, DataConfigName);
				}
				return mUnitManager;
			}

			private set
			{
				if (value != null)
				{
					mUnitManager = value;
				}
			}
		}

		/// <summary>Gets the FixtureManager object.</summary>
		public FixtureManager FixtureManager
		{
			get
			{
				if (null == mFixtureManager)
				{
					FixtureManager
						= new FixtureManager(mDbServiceRef, DataConfigName);
				}
				return mFixtureManager;
			}

			private set
			{
				if (value != null)
				{
					mFixtureManager = value;
				}
			}
		}

		/// <summary>Gets the EquipmentManager object.</summary>
		public EquipmentManager EquipmentManager
		{
			get
			{
				if (null == mEquipmentManager)
				{
					EquipmentManager
						= new EquipmentManager(mDbServiceRef, DataConfigName);
				}
				return mEquipmentManager;
			}

			private set
			{
				if (value != null)
				{
					mEquipmentManager = value;
				}
			}
		}

		/// <summary>Gets the CodeTypeClassManager object.</summary>
		public CodeTypeClassManager CodeTypeClassManager
		{
			get
			{
				if (null == mCodeTypeClassManager)
				{
					CodeTypeClassManager
						= new CodeTypeClassManager(mDbServiceRef, DataConfigName);
				}
				return mCodeTypeClassManager;
			}

			private set
			{
				if (value != null)
				{
					mCodeTypeClassManager = value;
				}
			}
		}

		/// <summary>Gets the CodeTypeManager object.</summary>
		public CodeTypeManager CodeTypeManager
		{
			get
			{
				if (null == mCodeTypeManager)
				{
					CodeTypeManager
						= new CodeTypeManager(mDbServiceRef, DataConfigName);
				}
				return mCodeTypeManager;
			}

			private set
			{
				if (value != null)
				{
					mCodeTypeManager = value;
				}
			}
		}

		/// <summary>Gets the PersonManager object.</summary>
		public PersonManager PersonManager
		{
			get
			{
				if (null == mPersonManager)
				{
					PersonManager
						= new PersonManager(mDbServiceRef, DataConfigName);
				}
				return mPersonManager;
			}

			private set
			{
				if (value != null)
				{
					mPersonManager = value;
				}
			}
		}

		/// <summary>Gets the UnitPersonManager object.</summary>
		public UnitPersonManager UnitPersonManager
		{
			get
			{
				if (null == mUnitPersonManager)
				{
					UnitPersonManager
						= new UnitPersonManager(mDbServiceRef, DataConfigName);
				}
				return mUnitPersonManager;
			}

			private set
			{
				if (value != null)
				{
					mUnitPersonManager = value;
				}
			}
		}

		/// <summary>Gets the PersonRelationManager object.</summary>
		public PersonRelationManager PersonRelationManager
		{
			get
			{
				if (null == mPersonRelationManager)
				{
					PersonRelationManager
						= new PersonRelationManager(mDbServiceRef, DataConfigName);
				}
				return mPersonRelationManager;
			}

			private set
			{
				if (value != null)
				{
					mPersonRelationManager = value;
				}
			}
		}

		/// <summary>Gets the AddressManager object.</summary>
		public AddressManager AddressManager
		{
			get
			{
				if (null == mAddressManager)
				{
					AddressManager
						= new AddressManager(mDbServiceRef, DataConfigName);
				}
				return mAddressManager;
			}

			private set
			{
				if (value != null)
				{
					mAddressManager = value;
				}
			}
		}

		/// <summary>Gets the PersonAddressManager object.</summary>
		public PersonAddressManager PersonAddressManager
		{
			get
			{
				if (null == mPersonAddressManager)
				{
					PersonAddressManager
						= new PersonAddressManager(mDbServiceRef, DataConfigName);
				}
				return mPersonAddressManager;
			}

			private set
			{
				if (value != null)
				{
					mPersonAddressManager = value;
				}
			}
		}

		/// <summary>Gets the AccountManager object.</summary>
		public AccountManager AccountManager
		{
			get
			{
				if (null == mAccountManager)
				{
					AccountManager
						= new AccountManager(mDbServiceRef, DataConfigName);
				}
				return mAccountManager;
			}

			private set
			{
				if (value != null)
				{
					mAccountManager = value;
				}
			}
		}

		/// <summary>Gets the BusinessManager object.</summary>
		public BusinessManager BusinessManager
		{
			get
			{
				if (null == mBusinessManager)
				{
					BusinessManager
						= new BusinessManager(mDbServiceRef, DataConfigName);
				}
				return mBusinessManager;
			}

			private set
			{
				if (value != null)
				{
					mBusinessManager = value;
				}
			}
		}

		/// <summary>Gets the BusinessPersonManager object.</summary>
		public BusinessPersonManager BusinessPersonManager
		{
			get
			{
				if (null == mBusinessPersonManager)
				{
					BusinessPersonManager
						= new BusinessPersonManager(mDbServiceRef, DataConfigName);
				}
				return mBusinessPersonManager;
			}

			private set
			{
				if (value != null)
				{
					mBusinessPersonManager = value;
				}
			}
		}

		/// <summary>Gets the BusinessAddressManager object.</summary>
		public BusinessAddressManager BusinessAddressManager
		{
			get
			{
				if (null == mBusinessAddressManager)
				{
					BusinessAddressManager
						= new BusinessAddressManager(mDbServiceRef, DataConfigName);
				}
				return mBusinessAddressManager;
			}

			private set
			{
				if (value != null)
				{
					mBusinessAddressManager = value;
				}
			}
		}

		/// <summary>Gets the DataConfigName value.</summary>
		public string DataConfigName { get; private set; }
		#endregion

		#region Class Data

		private readonly DbServiceRef mDbServiceRef;

		private FacilityDbManager mFacilityDbManager;
		private UnitManager mUnitManager;
		private FixtureManager mFixtureManager;
		private EquipmentManager mEquipmentManager;
		private CodeTypeClassManager mCodeTypeClassManager;
		private CodeTypeManager mCodeTypeManager;
		private PersonManager mPersonManager;
		private UnitPersonManager mUnitPersonManager;
		private PersonRelationManager mPersonRelationManager;
		private AddressManager mAddressManager;
		private PersonAddressManager mPersonAddressManager;
		private AccountManager mAccountManager;
		private BusinessManager mBusinessManager;
		private BusinessPersonManager mBusinessPersonManager;
		private BusinessAddressManager mBusinessAddressManager;
		#endregion
	}
}
