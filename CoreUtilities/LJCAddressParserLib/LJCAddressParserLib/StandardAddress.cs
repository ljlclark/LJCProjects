// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// StandardAddress.cs
using System;
using System.Text;
using LJCNetCommon;

namespace LJCAddressParserLib
{
	// Provides methods to parse Address information into standardized component properties.
	/// <include path='items/StandardAddress/*' file='Doc/StandardAddress.xml'/>
	public class StandardAddress
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
		public StandardAddress()
		{
			ErrorMessages = new ErrorMessages();

			Directionals = Directionals.LJCDeserialize(out string errorText);
			if (NetString.HasValue(errorText))
			{
				AddMessage("Directionals", errorText);
			}

			UnitLookups = UnitLookups.LJCDeserialize(out errorText);
			if (NetString.HasValue(errorText))
			{
				AddMessage("UnitLookups", errorText);
			}

			Units = Units.LJCDeserialize(out errorText);
			if (NetString.HasValue(errorText))
			{
				AddMessage("Units", errorText);
			}

			SuffixLookups = SuffixLookups.LJCDeserialize();
			if (NetString.HasValue(errorText))
			{
				AddMessage("SuffixLookups", errorText);
			}

			Suffixes = Suffixes.LJCDeserialize(out errorText);
			if (NetString.HasValue(errorText))
			{
				AddMessage("Suffixes", errorText);
			}

			StateLookups = StateLookups.LJCDeserialize();
			if (NetString.HasValue(errorText))
			{
				AddMessage("StateLookups", errorText);
			}

			States = States.LJCDeserialize(out errorText);
			if (NetString.HasValue(errorText))
			{
				AddMessage("States", errorText);
			}

			RoadLookups = RoadLookups.LJCDeserialize(out errorText);
			if (NetString.HasValue(errorText))
			{
				AddMessage("RoadLookups", errorText);
			}

			PrimaryRoads = PrimaryRoads.LJCDeserialize(out errorText);
			if (NetString.HasValue(errorText))
			{
				AddMessage("PrimaryRoads", errorText);
			}

			if (Units != null)
			{
				UnitLookups = Units.LJCUpdateLookups(UnitLookups);
			}
			if (Suffixes != null)
			{
				SuffixLookups = Suffixes.LJCUpdateLookups(SuffixLookups);
			}
			if (States != null)
			{
				StateLookups = States.LJCUpdateLookups(StateLookups);
			}
		}
		#endregion

		#region Public Methods

		// Parses the combined Delivery Address and Last Line values.
		/// <include path='items/ParseMixedAddress/*' file='Doc/StandardAddress.xml'/>
		public bool ParseMixedAddress(string mixedAddress)
    {
      bool retValue;

			if (null == mixedAddress) { }  // Testing
			retValue = false;
			return retValue;
		}

		// Parses the Delivery Address into the DeliveryAddressLine Properties.
		/// <include path='items/ParseDeliveryAddressLine/*' file='Doc/StandardAddress.xml'/>
		public bool ParseDeliveryAddressLine(string deliveryAddress)
		{
			string currentToken;
			int currentIndex;
			bool retValue = true;

			SetDeliveryAddressTokens(deliveryAddress);
			currentIndex = DeliveryAddressTokens.Length - 1;

			// Check current token for Unit.
			currentToken = DeliveryAddressToken(currentIndex);
			ParseUnit(currentToken, ref currentIndex);

			// Check current token for Postdirectional.
			// Moves the currentIndex backward.
			currentToken = DeliveryAddressToken(currentIndex);
			ParsePostDirectional(currentToken, ref currentIndex);

			// Check all preceding tokens for Suffix.
			ParseSuffix(ref currentIndex);

			// Check current token for AddressNumber.
			// Moves the currentIndex forward.
			ParseAddressNumber(ref currentIndex);

			// Check current token for PreDirectional.
			// Moves the currentIndex forward.
			currentToken = DeliveryAddressToken(currentIndex);
			ParsePreDirectional(currentToken, ref currentIndex);

			// Check current token for StreetName.
			ParseStreetName(ref currentIndex);

			CreateDeliveryLine();
			return retValue;
		}

		// Parses the Last Line into the LastLine Property.
		/// <include path='items/ParseLastLine/*' file='Doc/StandardAddress.xml'/>
		public bool ParseLastLine(string lastLine)
		{
			string currentToken;
			int currentIndex;
			bool retValue;

			SetLastLineTokens(lastLine);

			// Check far right token for ZipCode-Zip4
			currentIndex = LastLineTokens.Length - 1;
			currentToken = LastLineToken(currentIndex);
			retValue = ParseZipCode(currentToken, ref currentIndex);

			// Check current token for State or Province.
			// ToDo: ?
			if (retValue)
			{
				currentToken = LastLineToken(currentIndex);
				retValue = ParseState(currentToken, ref currentIndex);
			}

			ParseCity(ref currentIndex);

			CreateLastLine();
			return retValue;
		}
		#endregion

		#region Delivery Address Methods

		// Parses a token into the PreDirectional Property.
		/// <include path='items/ParsePreDirectional/*' file='Doc/StandardAddress.xml'/>
		public bool ParsePreDirectional(string currentToken, ref int currentIndex)
		{
			Directional directional;
			bool retValue = false;

			//mPreDirectionalIndex = -1;
			PreDirectional = null;
			directional = LookupDirectional(currentToken);
			if (directional != null)
			{
				// Check that Street name is not confused with PreDirectional by making
				// sure there is at least one available value after the directional.
				// Handles example: 123 North Street
				int postStreetIndex = GetPostStreetIndex();
				if (postStreetIndex > currentIndex + 1)
				{
					retValue = true;
					//mPreDirectionalIndex = currentIndex;
					PreDirectional = directional.Code;
					currentIndex++;
					ParseMultiWordPreDirectional(PreDirectional, ref currentIndex);
				}
			}
			return retValue;
		}

		// Sets the Predirectional values.
		// Moves the currentIndex forward.
		private void ParseMultiWordPreDirectional(string precedingCode, ref int currentIndex)
		{
			Directional directional;
			string currentToken;

			if ("NS".IndexOf(precedingCode) > -1)
			{
				// Check that Street name is not confused with PreDirectional by making
				// sure there is at least one available value after the directional.
				int postStreetIndex = GetPostStreetIndex();
				if (postStreetIndex > currentIndex + 1)
				{
					currentToken = DeliveryAddressToken(currentIndex);
					directional = LookupDirectional(currentToken);
					if (directional != null
						&& "EW".IndexOf(directional.Code) > -1)
					{
						//mPreDirectionalIndex = currentIndex;
						PreDirectional = $"{precedingCode}{directional.Code}";
						currentIndex++;
					}
				}
			}
		}

		// Parses a token into the PostDirectional Property.
		/// <include path='items/ParsePostDirectional/*' file='Doc/StandardAddress.xml'/>
		public bool ParsePostDirectional(string currentToken, ref int currentIndex)
		{
			Directional directional;
			string precedingToken;
			bool retValue = false;

			mPostDirectionalIndex = -1;
			PostDirectional = null;
			directional = LookupDirectional(currentToken);
			if (directional != null)
			{
				// Check that Unit value is not confused with PostDirectional -
				// by checking the preceding token for a Unit Type value.
				// Handles example: Apt E
				precedingToken = DeliveryAddressToken(currentIndex - 1);
				Unit unit = LookupUnit(precedingToken);
				if (null == unit)
				{
					// Check that Street Name is not confused with PostDirectional -
					// by checking for Address Number.
					bool isStreetName = false;
					if (1 == currentIndex)
					{
						int tempIndex = 1;
						if (ParseAddressNumber(ref tempIndex))
						{
							isStreetName = true;
						}
					}

					// Token is a Directional.
					if (!isStreetName)
					{
						retValue = true;
						mPostDirectionalIndex = currentIndex;
						PostDirectional = directional.Code;
						GetAddressPreviousIndex(ref currentIndex);
						ParseMultiWordPostDirectional(PostDirectional, ref currentIndex);
					}
				}
			}
			return retValue;
		}

		// Sets the Postdirectional values.
		// Moves the currentIndex backward.
		private void ParseMultiWordPostDirectional(string followingCode, ref int currentIndex)
		{
			Directional directional;
			string currentToken;

			if ("EW".IndexOf(followingCode) > -1)
			{
				currentToken = DeliveryAddressToken(currentIndex);
				directional = LookupDirectional(currentToken);
				if (directional != null
					&& "NS".IndexOf(directional.Code) > -1)
				{
					// Check that Street Name is not confused with PostDirectional -
					// by checking for Address Number.
					bool isStreetName = false;
					if (1 == currentIndex)
					{
						int tempIndex = 1;
						if (ParseAddressNumber(ref tempIndex))
						{
							isStreetName = true;
						}
					}

					if (!isStreetName)
					{
						mPostDirectionalIndex = currentIndex;
						PostDirectional = $"{directional.Code}{followingCode}";
						GetAddressPreviousIndex(ref currentIndex);
					}
				}
			}
		}

		// Parses a token into the Unit Property.
		/// <include path='items/ParseUnit/*' file='Doc/StandardAddress.xml'/>
		public bool ParseUnit(string currentToken, ref int currentIndex)
		{
			Unit unit;
			bool retValue = false;

			mUnitTypeIndex = -1;
			UnitType = null;
			mUnitNumberIndex = -1;
			UnitNumber = null;

			// Get UnitNumber only.
			// Handles example: #3.
			if (currentToken.Trim().StartsWith("#"))
			{
				retValue = true;
				mUnitNumberIndex = currentIndex;
				UnitType = "#";
				UnitNumber = currentToken.Substring(1);
				GetAddressPreviousIndex(ref currentIndex);
			}

			// Get Unit only.
			// Handles example: "Apt 3" or "Front".
			if (!retValue)
			{
				unit = LookupUnit(currentToken);
				if (unit != null)
				{
					// Unit does not require range.
					// Handles example: "Front".
					if (!unit.RequiresRange)
					{
						retValue = true;
						mUnitTypeIndex = currentIndex;
						UnitType = unit.Code;
						GetAddressPreviousIndex(ref currentIndex);
					}
				}
			}

			// Get Unit and UnitNumber.
			if (!retValue)
			{
				string precedingToken = DeliveryAddressToken(currentIndex - 1);

				// Check for non-specified Unit Type.
				if ("#" == precedingToken.Trim())
				{
					retValue = true;
					UnitType = precedingToken;
				}
				else
				{
					unit = LookupUnit(precedingToken);
					if (unit != null)
					{
						retValue = true;
						UnitType = unit.Code;
					}
				}

				// Unit was found.
				if (retValue)
				{
					mUnitTypeIndex = currentIndex - 1;
					mUnitNumberIndex = currentIndex;
					UnitNumber = currentToken;
					if (GetAddressPreviousIndex(ref currentIndex))
					{
						GetAddressPreviousIndex(ref currentIndex);
					}
				}
			}
			return retValue;
		}

		// Parses any preceding suffix token into the Suffix Property.
		/// <include path='items/ParseSuffix/*' file='Doc/StandardAddress.xml'/>
		public bool ParseSuffix(ref int currentIndex)
		{
			Suffix suffix;
			bool retValue = false;

			mSuffixIndex = -1;
			Suffix = null;
			while (currentIndex > 1)
			{
				string currentToken = DeliveryAddressToken(currentIndex);
				suffix = LookupSuffix(currentToken);
				if (suffix != null)
				{
					retValue = true;
					mSuffixIndex = currentIndex;
					Suffix = suffix.Code;
					GetAddressPreviousIndex(ref currentIndex);
					break;
				}
				GetAddressPreviousIndex(ref currentIndex);
			}
			return retValue;
		}

		// Parses a token into the AddressNumber Property.
		/// <include path='items/ParseAddressNumber/*' file='Doc/StandardAddress.xml'/>
		public bool ParseAddressNumber(ref int currentIndex)
		{
			//int nextTokenIndex;
			bool retValue = false;

			string currentToken = DeliveryAddressToken(0);

			AddressNumber = GetPOBox(currentToken, ref currentIndex);
			if (NetString.HasValue(AddressNumber))
			{
				retValue = true;
			}

			if (!retValue)
			{
				// Is a number.
				if (IsInt(currentToken))
				{
					retValue = true;
				}

				// Ends with a character value.
				if (!retValue)
				{
					if (IsInt(currentToken.Substring(0, currentToken.Length - 1)))
					{
						retValue = true;
					}
				}

				// Starts with a character value.
				if (!retValue)
				{
					if (IsInt(currentToken.Substring(1, currentToken.Length - 1)))
					{
						retValue = true;
					}
				}

				// Set the parsed value property.
				if (retValue)
				{
					AddressNumber = currentToken;
					currentIndex++;
				}
			}
			return retValue;
		}

		// Parses the tokens from the currentIndex to the appropriate end Index.
		/// <include path='items/ParseStreetName/*' file='Doc/StandardAddress.xml'/>
		public bool ParseStreetName(ref int currentIndex)
		{
			bool retValue = false;

			// There should be at least one available value.
			int postStreetIndex = GetPostStreetIndex();
			if (currentIndex < postStreetIndex)
			{
				retValue = true;
			}

			// Get all tokens until the first post street value.
			StringBuilder builder = new StringBuilder(64);
			bool first = true;
			for (int index = currentIndex; index < postStreetIndex; index++)
			{
				string currentToken = DeliveryAddressToken(index);

				// Get Road token.
				string roadToken = GetRoadToken(index, currentToken);
				if (NetString.HasValue(roadToken))
				{
					currentToken = roadToken;

					// Recalculate PostStreet index as it may have changed.
					postStreetIndex = GetPostStreetIndex();
				}

				if (!first)
				{
					builder.Append(" ");
				}
				first = false;

				// Last Street value.
				if (roadToken != null
					&& index == postStreetIndex - 1)
				{
					PrimaryRoad primaryRoad = LookupRoad(roadToken);
					if (primaryRoad != null)
					{
						currentToken = primaryRoad.Code;
					}
				}
				builder.Append(currentToken);
			}
			StreetName = builder.ToString();
			return retValue;
		}

		// Creates the Standardized Delivery Address Line from the Delivery Address
		// Properties.
		/// <include path='items/CreateDeliveryLine/*' file='Doc/StandardAddress.xml'/>
		public void CreateDeliveryLine()
		{
			bool valueStarted = false;
			StringBuilder builder = new StringBuilder(64);
			builder.Append(GetSeparatedValue(ref valueStarted, AddressNumber));
			builder.Append(GetSeparatedValue(ref valueStarted, PreDirectional));
			builder.Append(GetSeparatedValue(ref valueStarted, StreetName));
			builder.Append(GetSeparatedValue(ref valueStarted, Suffix));
			builder.Append(GetSeparatedValue(ref valueStarted, PostDirectional));
			builder.Append(GetSeparatedValue(ref valueStarted, UnitType));
			builder.Append(GetSeparatedValue(ref valueStarted, UnitNumber));
			DeliveryAddressLine = builder.ToString();
		}
		#endregion

		#region Lookup Methods

		// Search Directional Code and Name.
		/// <include path='items/LookupDirectional/*' file='Doc/StandardAddress.xml'/>
		public Directional LookupDirectional(string lookupText)
		{
			Directional retValue;

			retValue = Directionals.LJCSearchCode(lookupText);
			if (null == retValue)
			{
				retValue = Directionals.LJCSearchName(lookupText);
			}
			return retValue;
		}

		// Search Units Name and Code.
		/// <include path='items/LookupUnit/*' file='Doc/StandardAddress.xml'/>
		public Unit LookupUnit(string lookupText)
		{
			UnitLookup unitLookup;
			string soundex;
			Unit retValue;

			retValue = Units.LJCSearchCode(lookupText);
			if (null == retValue)
			{
				unitLookup = UnitLookups.LJCSearchLookupName(lookupText);
				if (null == unitLookup)
				{
					soundex = NetString.CreateLSoundex(lookupText);
					unitLookup = UnitLookups.LJCSearchLSoundex(soundex);
					if (null == unitLookup)
					{
						soundex = NetString.CreatePSoundex(lookupText);
						unitLookup = UnitLookups.LJCSearchPSoundex(soundex);
					}
				}
				if (unitLookup != null)
				{
					retValue = Units.LJCSearchCode(unitLookup.Code);
				}
			}
			return retValue;
		}

		// Search the SuffixLookup LookupName and Code.
		/// <include path='items/LookupSuffix/*' file='Doc/StandardAddress.xml'/>
		public Suffix LookupSuffix(string lookupText)
		{
			SuffixLookup suffixLookup;
			string soundex;
			Suffix retValue;

			retValue = Suffixes.LJCSearchCode(lookupText);
			if (null == retValue)
			{
				suffixLookup = SuffixLookups.LJCSearchLookupName(lookupText);
				if (null == suffixLookup)
				{
					soundex = NetString.CreateLSoundex(lookupText);
					suffixLookup = SuffixLookups.LJCSearchLSoundex(soundex);
					//if (null == suffixLookup)
					//{
					//	soundex = LJCNetString.CreatePSoundex(lookupText);
					//	suffixLookup = SuffixLookups.LJCSearchPSoundex(soundex);
					//}
				}
				if (suffixLookup != null)
				{
					retValue = Suffixes.LJCSearchCode(suffixLookup.Code);
				}
			}
			return retValue;
		}

		// Search the StateLookup LookupName, LSoundex and PSoundex.
		/// <include path='items/LookupState/*' file='Doc/StandardAddress.xml'/>
		public State LookupState(string lookupText)
		{
			StateLookup stateLookup;
			string soundex;
			State retValue = null;

			stateLookup = StateLookups.LJCSearchLookupName("USA", lookupText);
			if (null == stateLookup)
			{
				soundex = NetString.CreateLSoundex(lookupText);
				stateLookup = StateLookups.LJCSearchLSoundex("USA", soundex);
				if (null == stateLookup)
				{
					soundex = NetString.CreatePSoundex(lookupText);
					stateLookup = StateLookups.LJCSearchPSoundex("USA", soundex);
				}
			}
			if (stateLookup != null)
			{
				retValue = States.LJCSearchCode("USA", stateLookup.Code);
			}
			return retValue;
		}

		// Search the RoadLookup LookupName and Code.
		/// <include path='items/LookupSuffix/*' file='Doc/StandardAddress.xml'/>
		public PrimaryRoad LookupRoad(string lookupText)
		{
			RoadLookup roadLookup;
			string soundex;
			PrimaryRoad retValue;

			retValue = PrimaryRoads.LJCSearchCode("USA", lookupText);
			if (null == retValue)
			{
				roadLookup = RoadLookups.LJCSearchLookupName("USA", lookupText);
				if (null == roadLookup)
				{
					soundex = NetString.CreateLSoundex(lookupText);
					roadLookup = RoadLookups.LJCSearchLSoundex("USA", soundex);
					if (null == roadLookup)
					{
						soundex = NetString.CreatePSoundex(lookupText);
						roadLookup = RoadLookups.LJCSearchPSoundex("USA", soundex);
					}
				}
				if (roadLookup != null)
				{
					retValue = PrimaryRoads.LJCSearchCode("USA", roadLookup.Code);
				}
			}
			return retValue;
		}
		#endregion

		#region Last Line Methods

		// Parses a token into the Zipcode Property.
		/// <include path='items/ParseZipCode/*' file='Doc/StandardAddress.xml'/>
		public bool ParseZipCode(string currentToken, ref int currentIndex)
		{
			bool retValue = true;

			Zipcode = null;
			Zip4 = null;
			//mZipcodeIndex = -1;
			//mZip4Index = -1;
			if (IsInt(currentToken))
			{
				if (IsDigits(currentToken, 4))
				{
					// Check for space separated ZipCode Zip4
					if (LastLinePreviousIndex(ref currentIndex))
					{
						// Is preceding token Zipcode.
						string previousToken = LastLineToken(currentIndex);
						if (IsInt(previousToken))
						{
							if (IsCurrentTokenZipcode(currentIndex, ref previousToken))
							{
								Zip4 = currentToken;
								Zipcode = previousToken;
							}
							else
							{
								retValue = false;
								AddMessage(previousToken
									, "Unable to identify token '{0}' as a Zipcode.");
							}

							// Move left because token was an integer.
							LastLinePreviousIndex(ref currentIndex);
						}
						else
						{
							retValue = false;
							AddMessage(previousToken
								, "Unable to identify token '{0}' as a Zipcode.");
						}
					}
				}
				else
				{
					// Is far right token Zipcode.
					if (IsDigits(currentToken, 5))
					{
						Zipcode = currentToken;
					}
					else
					{
						retValue = false;
						AddMessage(currentToken
							, "Unable to identify token '{0}' as a Zipcode.");
					}

					// Move left because token was an integer.
					LastLinePreviousIndex(ref currentIndex);
				}
			}
			return retValue;
		}

		// Parses a token into the StateOrProvince Property.
		/// <include path='items/ParseState/*' file='Doc/StandardAddress.xml'/>
		public bool ParseState(string currentToken, ref int currentIndex)
		{
			bool retValue = false;

			StateOrProvince = null;
			//mStateIndex = -1;
			State state = LookupState(currentToken);
			if (state != null)
			{
				retValue = true;
				StateOrProvince = state.Code;
				LastLinePreviousIndex(ref currentIndex);
			}

			if (!retValue)
			{
				AddMessage(currentToken
					, "Unable to identify token '{0}' as a State or Province.");
			}
			return retValue;
		}

		// Parses the tokens from index zero to the currentIndex.
		/// <include path='items/ParseCity/*' file='Doc/StandardAddress.xml'/>
		public bool ParseCity(ref int currentIndex)
		{
			bool retValue = false;

			City = null;
			//mCityIndex = -1;
			StringBuilder builder = new StringBuilder(64);
			bool first = true;
			for (int index = 0; index <= currentIndex; index++)
			{
				retValue = true;
				if (!first)
				{
					builder.Append(" ");
				}
				first = false;

				// Expand abbreviations that are like a Suffix.
				string token = LastLineTokens[index];
				Suffix suffix = LookupSuffix(token);
				if (suffix != null)
				{
					token = suffix.Name;
				}

				builder.Append(token);
			}
			City = builder.ToString();
			return retValue;
		}

		// Creates the standardized Last Line form the Last Line Properties.
		/// <include path='items/CreateLastLine/*' file='Doc/StandardAddress.xml'/>
		public void CreateLastLine()
		{
			bool valueStarted = false;
			StringBuilder builder = new StringBuilder(64);
			builder.Append(GetSeparatedValue(ref valueStarted, City));
			builder.Append(GetSeparatedValue(ref valueStarted, StateOrProvince));
			if (NetString.HasValue(Zipcode))
			{
				builder.Append("  ");
				builder.Append(Zipcode);
				if (NetString.HasValue(Zip4))
				{
					builder.Append("-");
					builder.Append(Zip4);
				}
			}
			LastLine = builder.ToString();
		}

		// Gets the value with a preceding blank if the value exists.
		/// <include path='items/GetSeparatedValue/*' file='Doc/StandardAddress.xml'/>
		public string GetSeparatedValue(ref bool valueStarted, string value)
		{
			string retValue = null;

			if (NetString.HasValue(value))
			{
				if (valueStarted)
				{
					retValue = $" {value}";
				}
				else
				{
					retValue = value;
				}
				valueStarted = true;
			}
			return retValue;
		}
		#endregion

		#region Delivery Address Helper Methods

		// Sets the currentIndex value to the previous index value if
		// it is within the token index range.
		/// <include path='items/GetAddressPreviousIndex/*' file='Doc/StandardAddress.xml'/>
		public bool GetAddressPreviousIndex(ref int currentIndex)
		{
			bool retValue = false;

			if (DeliveryAddressTokens.Length > 0
				&& DeliveryAddressTokens.Length > currentIndex)
			{
				retValue = true;
				currentIndex--;
			}
			return retValue;
		}

		// Retrieves the token at the specified index.
		/// <include path='items/DeliveryAddressToken/*' file='Doc/StandardAddress.xml'/>
		public string DeliveryAddressToken(int index)
		{
			string retValue = null;

			if (DeliveryAddressTokens.Length > 0
				&& DeliveryAddressTokens.Length > index)
			{
				retValue = DeliveryAddressTokens[index];
			}
			return retValue;
		}

		// Set data values including DeliveryAddressTokens.
		private void SetDeliveryAddressTokens(string deliveryAddress)
		{
			AddressNumber = null;
			PreDirectional = null;
			StreetName = null;
			Suffix = null;
			PostDirectional = null;
			UnitType = null;
			UnitNumber = null;
			//mPreDirectionalIndex = -1;
			mSuffixIndex = -1;
			mPostDirectionalIndex = -1;
			mUnitTypeIndex = -1;
			mUnitNumberIndex = -1;

			// Remove punctuation.
			//mOriginalDeliveryLine = deliveryAddress;
			deliveryAddress = deliveryAddress.Replace('.', ' ');

			DeliveryAddressTokens = deliveryAddress.Split(new char[] { ' ' }
				, StringSplitOptions.RemoveEmptyEntries);
		}
		#endregion

		#region Last Line Helper Methods

		// Checks if the current token is the Zip4 value.
		/// <include path='items/IsCurrentTokenZip4/*' file='Doc/StandardAddress.xml'/>
		public bool IsCurrentTokenZip4(int currentIndex, ref string currentToken)
		{
			bool retValue = false;

			currentToken = null;

			string token = LastLineToken(currentIndex);
			if (IsDigits(token, 4))
			{
				currentToken = token;
				retValue = true;
			}
			return retValue;
		}

		// Checks if the current token is the Zipcode value.
		/// <include path='items/IsCurrentTokenZipcode/*' file='Doc/StandardAddress.xml'/>
		public bool IsCurrentTokenZipcode(int currentIndex, ref string currentToken)
		{
			bool retValue = false;

			string token = LastLineToken(currentIndex);
			if (IsDigits(token, 5))
			{
				currentToken = token;
				retValue = true;
			}
			return retValue;
		}

		// Sets the currentIndex value to the previous index value if
		// it is within the token index range.
		/// <include path='items/LastLinePreviousIndex/*' file='Doc/StandardAddress.xml'/>
		public bool LastLinePreviousIndex(ref int currentIndex)
		{
			bool retValue = false;

			if (LastLineTokens.Length > 0
				&& LastLineTokens.Length > currentIndex)
			{
				retValue = true;
				currentIndex--;
			}
			return retValue;
		}

		// Retrieves the token at the specified index.
		/// <include path='items/LastLineToken/*' file='Doc/StandardAddress.xml'/>
		public string LastLineToken(int index)
		{
			string retValue = null;

			if (LastLineTokens.Length > 0
				&& LastLineTokens.Length > index)
			{
				retValue = LastLineTokens[index];
			}
			return retValue;
		}

		// Checks a token value for digits.
		/// <include path='items/IsDigits/*' file='Doc/StandardAddress.xml'/>
		public bool IsDigits(string token, int length)
		{
			bool retValue = false;

			if (IsInt(token))
			{
				if (length == token.Length)
				{
					int.TryParse(token, out int digits);
					if (digits > 0)
					{
						retValue = true;
					}
				}
			}
			return retValue;
		}

		// Checks a token value for an integer.
		/// <include path='items/IsInt/*' file='Doc/StandardAddress.xml'/>
		public bool IsInt(string token)
		{
			bool retValue = false;

			int.TryParse(token, out int digits);
			if (digits > 0)
			{
				retValue = true;
			}
			return retValue;
		}

		// Set data values including LastLineTokens.
		private void SetLastLineTokens(string lastLine)
		{
			City = null;
			StateOrProvince = null;
			Zipcode = null;
			Zip4 = null;
			//mCityIndex = -1;
			//mStateIndex = -1;
			//mZipcodeIndex = -1;
			//mZip4Index = -1;

			// New Orleans, LA  12345-1234
			// Remove punctuation.
			mOriginalLastLine = lastLine;
			lastLine = lastLine.Replace('-', ' ');
			lastLine = lastLine.Replace(',', ' ');

			LastLineTokens = lastLine.Split(new char[] { ' ' }
				, StringSplitOptions.RemoveEmptyEntries);
		}
		#endregion

		#region Private Methods

		// Get the POBox value.
		private string GetPOBox(string currentToken, ref int currentIndex)
		{
			int tokenIndex = currentIndex;
			bool isPOBox = false;
			string retValue = null;

			string currentValue = currentToken.ToLower();
			switch (currentValue)
			{
				case "p":
					tokenIndex++;
					currentValue = DeliveryAddressToken(tokenIndex).ToLower();
					if ("o" == currentValue)
					{
						tokenIndex++;
						currentValue = DeliveryAddressToken(tokenIndex).ToLower();
						if ("box" == currentValue)
						{
							isPOBox = true;
						}
					}
					else
					{
						if ("obox" == currentValue)
						{
							isPOBox = true;
						}
					}
					break;

				case "po":
					tokenIndex++;
					currentValue = DeliveryAddressToken(tokenIndex).ToLower();
					if ("box" == currentValue)
					{
						isPOBox = true;
					}
					break;

				case "pobox":
					isPOBox = true;
					break;
			}
			if (isPOBox)
			{
				retValue = "PO Box";
				currentIndex = tokenIndex + 1;
			}
			return retValue;
		}

		// Attempts to get the PrimaryRoad value.
		private string GetRoadToken(int index, string currentToken)
		{
			PrimaryRoad primaryRoad;
			PrimaryRoad nextRoad;
			string retValue = null;

			// Split Interstate and number value.
			string splitValue = GetInterstateToken(currentToken);
			if (splitValue != null)
			{
				retValue = splitValue;
			}

			if (null == retValue)
			{
				primaryRoad = LookupRoad(currentToken);

				if (primaryRoad != null)
				{
					retValue = primaryRoad.Name;
				}
				if (index < DeliveryAddressTokens.Length - 1)
				{
					int nextTokenIndex = index + 1;
					string nextToken = DeliveryAddressToken(nextTokenIndex);
					nextRoad = LookupRoad(nextToken);
					if (nextRoad != null)
					{
						// If first value is not a road value, check it for State.
						if (null == primaryRoad)
						{
							State state = LookupState(currentToken);
							if (state != null)
							{
								retValue = state.Name;
							}
						}

						// Primary Road value was originally defined as a Suffix.
						if (nextTokenIndex == mSuffixIndex)
						{
							// Clear Suffix values and ensure returned value to indicate success.
							mSuffixIndex = -1;
							Suffix = null;
							if (null == retValue)
							{
								retValue = currentToken;
							}
						}
					}
					else
					{
						// Road identifier was originally defined as PostDirectional.
						if (primaryRoad != null
							&& nextTokenIndex == mPostDirectionalIndex)
						{
							mPostDirectionalIndex = -1;
							PostDirectional = null;
						}
					}
				}
			}
			return retValue;
		}

		// Split interstate and number value.
		private string GetInterstateToken(string currentToken)
		{
			PrimaryRoad primaryRoad;
			string tokenValue;
			string alphaValue = null;
			string retValue = null;

			// Split out Interstate and number value.
			tokenValue = currentToken.ToLower();
			if (tokenValue.StartsWith("ih"))
			{
				alphaValue = "IH";
			}
			else
			{
				if (tokenValue.StartsWith("i"))
				{
					alphaValue = "I";
				}
			}

			if (alphaValue != null)
			{
				string numberValue = currentToken.Substring(alphaValue.Length);
				int.TryParse(numberValue, out int number);
				if (number > 0)
				{
					primaryRoad = LookupRoad(alphaValue);
					if (primaryRoad != null)
					{
						retValue = $"{primaryRoad.Name} {numberValue}";
					}
				}
			}
			return retValue;
		}

		// Gets the index value after the Street index.
		private int GetPostStreetIndex()
		{
			int retValue = 0;

			if (mSuffixIndex > 0)
			{
				retValue = mSuffixIndex;
			}
			if (0 == retValue && mPostDirectionalIndex > 0)
			{
				retValue = mPostDirectionalIndex;
			}
			if (0 == retValue && mUnitTypeIndex > 0)
			{
				retValue = mUnitTypeIndex;
			}
			if (0 == retValue && mUnitNumberIndex > 0)
			{
				retValue = mUnitNumberIndex;
			}
			if (0 == retValue)
			{
				retValue = DeliveryAddressTokens.Length;
			}
			return retValue;
		}

		// Adds an error message.
		private void AddMessage(string currentToken, string message)
		{
			ErrorMessage errorMessage = new ErrorMessage()
			{
				Line = mOriginalLastLine,
				Message = string.Format(message, currentToken)
			};
			ErrorMessages.Add(errorMessage);
		}
		#endregion

		#region Address Block Line Properties

		/// <summary>Gets or sets the Attention Line.</summary>
		/// <remarks>The Address Block Line 1.</remarks>
		public string AttentionLine
		{
			get { return mAttentionLine; }
			set { mAttentionLine = NetString.InitString(value); }
		}
		private string mAttentionLine;

		/// <summary>Gets or sets the Recipient Line.</summary>
		/// <remarks>The Address Block Line 2.</remarks>
		public string RecipientLine
		{
			get { return mRecipientLine; }
			set { mRecipientLine = NetString.InitString(value); }
		}
		private string mRecipientLine;

		// Gets or sets the Physical Address Line.
		/// <include path='items/PhysicalAddressLine/*' file='Doc/StandardAddress.xml'/>
		public string PhysicalAddressLine
		{
			get { return mPhysicalAddressLine; }
			set { mPhysicalAddressLine = NetString.InitString(value); }
		}
		private string mPhysicalAddressLine;

		// Gets or sets the Secondary Address Line.
		/// <include path='items/SecondaryAddressLine/*' file='Doc/StandardAddress.xml'/>
		public string SecondaryAddressLine
		{
			get { return mSecondaryAddressLine; }
			set { mSecondaryAddressLine = NetString.InitString(value); }
		}
		private string mSecondaryAddressLine;

		// Gets or sets the Delivery Address Line.
		/// <include path='items/DeliveryAddressLine/*' file='Doc/StandardAddress.xml'/>
		public string DeliveryAddressLine
		{
			get { return mDeliveryAddressLine; }
			private set { mDeliveryAddressLine = NetString.InitString(value); }
		}
		private string mDeliveryAddressLine;

		// Gets or sets the Last Line.
		/// <include path='items/LastLine/*' file='Doc/StandardAddress.xml'/>
		public string LastLine
		{
			get { return mLastLine; }
			set { mLastLine = NetString.InitString(value); }
		}
		private string mLastLine;

		/// <summary>Gets or sets the Country Line.</summary>
		/// <remarks>The Address Block Line 7.</remarks>
		public string CountryLine
		{
			get { return mCountryLine; }
			set { mCountryLine = NetString.InitString(value); }
		}
		private string mCountryLine;
		#endregion

		#region Delivery Address Properties

		/// <summary>Gets or sets the Delivery Address Tokens.</summary>
		public string[] DeliveryAddressTokens { get; private set; }
		#endregion

		#region The 7 Delivery Address Component Properties

		/// <summary>Gets or sets the Address Number value.</summary>
		public string AddressNumber
		{
			get { return mAddressNumber; }
			set { mAddressNumber = NetString.InitString(value); }
		}
		private string mAddressNumber;

		/// <summary>Gets or sets the Pre Directional value.</summary>
		public string PreDirectional
		{
			get { return mPreDirectional; }
			set { mPreDirectional = NetString.InitString(value); }
		}
		private string mPreDirectional;

		/// <summary>Gets or sets the Street Name value.</summary>
		public string StreetName
		{
			get { return mStreetName; }
			set { mStreetName = NetString.InitString(value); }
		}
		private string mStreetName;

		/// <summary>Gets or sets the Suffix value.</summary>
		public string Suffix
		{
			get { return mSuffix; }
			set { mSuffix = NetString.InitString(value); }
		}
		private string mSuffix;

		/// <summary>Gets or sets the Post Directional value.</summary>
		public string PostDirectional
		{
			get { return mPostDirectional; }
			set { mPostDirectional = NetString.InitString(value); }
		}
		private string mPostDirectional;

		/// <summary>Gets or sets the Unit Identifier.</summary>
		public string UnitType
		{
			get { return mUnitType; }
			set { mUnitType = NetString.InitString(value); }
		}
		private string mUnitType;

		/// <summary>Gets or sets the Secondary Unit Number value.</summary>
		public string UnitNumber
		{
			get { return mUnitNumber; }
			set { mUnitNumber = NetString.InitString(value); }
		}
		private string mUnitNumber;
		#endregion

		#region Last Line Properties

		/// <summary>Gets or sets the Last Line Tokens.</summary>
		public string[] LastLineTokens { get; private set; }
		#endregion

		#region The 4 Last Line Component Properties

		/// <summary>Gets or sets the City value.</summary>
		public string City
		{
			get { return mCity; }
			set { mCity = NetString.InitString(value); }
		}
		private string mCity;

		/// <summary>Gets or sets the State or Province value.</summary>
		public string StateOrProvince
		{
			get { return mStateOrProvince; }
			set { mStateOrProvince = NetString.InitString(value); }
		}
		private string mStateOrProvince;

		/// <summary>Gets or sets the Zipcode value.</summary>
		public string Zipcode
		{
			get { return mZipcode; }
			set { mZipcode = NetString.InitString(value); }
		}
		private string mZipcode;

		/// <summary>Gets or sets the Zip4 value.</summary>
		public string Zip4
		{
			get { return mZip4; }
			set { mZip4 = NetString.InitString(value); }
		}
		private string mZip4;
		#endregion

		#region Class Properties

		/// <summary>Gets or sets the Directionals collection.</summary>
		public Directionals Directionals { get; private set; }

		/// <summary>Gets or sets the Units Lookup collection.</summary>
		public UnitLookups UnitLookups { get; private set; }

		/// <summary>Gets or sets the Unit collection.</summary>
		public Units Units { get; private set; }

		/// <summary>Gets or sets the Suffix Lookup collection.</summary>
		public SuffixLookups SuffixLookups { get; private set; }

		/// <summary>Gets or sets the Suffix collection.</summary>
		public Suffixes Suffixes { get; private set; }

		/// <summary>Gets or sets the State Lookup collection.</summary>
		public StateLookups StateLookups { get; private set; }

		/// <summary>Gets or sets the State collection.</summary>
		public States States { get; private set; }

		/// <summary>Gets or sets the Road Lookup collection.</summary>
		public RoadLookups RoadLookups { get; private set; }

		/// <summary>Gets or sets the Primary Roads collection.</summary>
		public PrimaryRoads PrimaryRoads { get; private set; }

		/// <summary>Gets or sets the ErrorMessages value.</summary>
		public ErrorMessages ErrorMessages { get; private set; }
		#endregion

		#region Class Data

		//private string mOriginalDeliveryLine;
		private string mOriginalLastLine;

		//private int mPreDirectionalIndex;
		private int mSuffixIndex;
		private int mPostDirectionalIndex;
		private int mUnitTypeIndex;
		private int mUnitNumberIndex;
		//private int mCityIndex;
		//private int mStateIndex;
		//private int mZipcodeIndex;
		//private int mZip4Index;
		#endregion
	}
}
