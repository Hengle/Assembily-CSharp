using System;

namespace SDG.Unturned
{
	// Token: 0x0200073D RID: 1853
	public class NPCTool
	{
		// Token: 0x06003446 RID: 13382 RVA: 0x00154BA0 File Offset: 0x00152FA0
		public static bool doesLogicPass<T>(ENPCLogicType logicType, T a, T b) where T : IComparable
		{
			int num = a.CompareTo(b);
			switch (logicType)
			{
			case ENPCLogicType.LESS_THAN:
				return num < 0;
			case ENPCLogicType.LESS_THAN_OR_EQUAL_TO:
				return num <= 0;
			case ENPCLogicType.EQUAL:
				return num == 0;
			case ENPCLogicType.NOT_EQUAL:
				return num != 0;
			case ENPCLogicType.GREATER_THAN_OR_EQUAL_TO:
				return num >= 0;
			case ENPCLogicType.GREATER_THAN:
				return num > 0;
			default:
				return false;
			}
		}

		// Token: 0x06003447 RID: 13383 RVA: 0x00154C10 File Offset: 0x00153010
		public static void readConditions(Data data, Local localization, string prefix, INPCCondition[] conditions, string errorMessageSource)
		{
			for (int i = 0; i < conditions.Length; i++)
			{
				if (!data.has(prefix + i + "_Type"))
				{
					Assets.errors.Add(string.Concat(new object[]
					{
						"Missing condition ",
						prefix,
						i,
						" type"
					}));
					throw new NotSupportedException(string.Concat(new object[]
					{
						"Missing condition ",
						prefix,
						i,
						" type"
					}));
				}
				ENPCConditionType enpcconditionType = (ENPCConditionType)Enum.Parse(typeof(ENPCConditionType), data.readString(prefix + i + "_Type"), true);
				string text = localization.read(prefix + i);
				text = ItemTool.filterRarityRichText(text);
				bool newShouldReset = data.has(prefix + i + "_Reset");
				ENPCLogicType newLogicType = ENPCLogicType.NONE;
				if (data.has(prefix + i + "_Logic"))
				{
					newLogicType = (ENPCLogicType)Enum.Parse(typeof(ENPCLogicType), data.readString(prefix + i + "_Logic"), true);
				}
				switch (enpcconditionType)
				{
				case ENPCConditionType.EXPERIENCE:
					if (!data.has(prefix + i + "_Value"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Experience condition ",
							prefix,
							i,
							" missing _Value in: ",
							errorMessageSource
						}));
					}
					conditions[i] = new NPCExperienceCondition(data.readUInt32(prefix + i + "_Value"), newLogicType, text, newShouldReset);
					break;
				case ENPCConditionType.REPUTATION:
					if (!data.has(prefix + i + "_Value"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Reputation condition ",
							prefix,
							i,
							" missing _Value in: ",
							errorMessageSource
						}));
					}
					conditions[i] = new NPCReputationCondition(data.readInt32(prefix + i + "_Value"), newLogicType, text);
					break;
				case ENPCConditionType.FLAG_BOOL:
					if (!data.has(prefix + i + "_ID"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Bool flag condition ",
							prefix,
							i,
							" missing _ID in: ",
							errorMessageSource
						}));
					}
					if (!data.has(prefix + i + "_Value"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Bool flag condition ",
							prefix,
							i,
							" missing _Value in: ",
							errorMessageSource
						}));
					}
					conditions[i] = new NPCBoolFlagCondition(data.readUInt16(prefix + i + "_ID"), data.readBoolean(prefix + i + "_Value"), data.has(prefix + i + "_Allow_Unset"), newLogicType, text, newShouldReset);
					break;
				case ENPCConditionType.FLAG_SHORT:
					if (!data.has(prefix + i + "_ID"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Short flag condition ",
							prefix,
							i,
							" missing _ID in: ",
							errorMessageSource
						}));
					}
					if (!data.has(prefix + i + "_Value"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Short flag condition ",
							prefix,
							i,
							" missing _Value in: ",
							errorMessageSource
						}));
					}
					conditions[i] = new NPCShortFlagCondition(data.readUInt16(prefix + i + "_ID"), data.readInt16(prefix + i + "_Value"), data.has(prefix + i + "_Allow_Unset"), newLogicType, text, newShouldReset);
					break;
				case ENPCConditionType.QUEST:
					if (!data.has(prefix + i + "_ID"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Quest condition ",
							prefix,
							i,
							" missing _ID in: ",
							errorMessageSource
						}));
					}
					if (!data.has(prefix + i + "_Status"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Quest condition ",
							prefix,
							i,
							" missing _Status in: ",
							errorMessageSource
						}));
					}
					conditions[i] = new NPCQuestCondition(data.readUInt16(prefix + i + "_ID"), (ENPCQuestStatus)Enum.Parse(typeof(ENPCQuestStatus), data.readString(prefix + i + "_Status"), true), data.has(prefix + i + "_Ignore_NPC"), newLogicType, text, newShouldReset);
					break;
				case ENPCConditionType.SKILLSET:
					if (!data.has(prefix + i + "_Value"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Skillset condition ",
							prefix,
							i,
							" missing _Value in: ",
							errorMessageSource
						}));
					}
					conditions[i] = new NPCSkillsetCondition((EPlayerSkillset)Enum.Parse(typeof(EPlayerSkillset), data.readString(prefix + i + "_Value"), true), newLogicType, text);
					break;
				case ENPCConditionType.ITEM:
					if (!data.has(prefix + i + "_ID"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Item condition ",
							prefix,
							i,
							" missing _ID in: ",
							errorMessageSource
						}));
					}
					if (!data.has(prefix + i + "_Amount"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Item condition ",
							prefix,
							i,
							" missing _Amount in: ",
							errorMessageSource
						}));
					}
					conditions[i] = new NPCItemCondition(data.readUInt16(prefix + i + "_ID"), data.readUInt16(prefix + i + "_Amount"), text, newShouldReset);
					break;
				case ENPCConditionType.KILLS_ZOMBIE:
				{
					if (!data.has(prefix + i + "_ID"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Zombie kills condition ",
							prefix,
							i,
							" missing _ID in: ",
							errorMessageSource
						}));
					}
					if (!data.has(prefix + i + "_Value"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Zombie kills condition ",
							prefix,
							i,
							" missing _Value in: ",
							errorMessageSource
						}));
					}
					EZombieSpeciality newZombie = EZombieSpeciality.NONE;
					if (data.has(prefix + i + "_Zombie"))
					{
						newZombie = (EZombieSpeciality)Enum.Parse(typeof(EZombieSpeciality), data.readString(prefix + i + "_Zombie"), true);
					}
					else
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Zombie kills condition ",
							prefix,
							i,
							" missing _Zombie in: ",
							errorMessageSource
						}));
					}
					conditions[i] = new NPCZombieKillsCondition(data.readUInt16(prefix + i + "_ID"), data.readInt16(prefix + i + "_Value"), newZombie, data.has(prefix + i + "_Spawn"), data.readByte(prefix + i + "_Nav"), text, newShouldReset);
					break;
				}
				case ENPCConditionType.KILLS_HORDE:
					if (!data.has(prefix + i + "_ID"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Horde kills condition ",
							prefix,
							i,
							" missing _ID in: ",
							errorMessageSource
						}));
					}
					if (!data.has(prefix + i + "_Value"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Horde kills condition ",
							prefix,
							i,
							" missing _Value in: ",
							errorMessageSource
						}));
					}
					if (!data.has(prefix + i + "_Nav"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Horde kills condition ",
							prefix,
							i,
							" missing _Nav in: ",
							errorMessageSource
						}));
					}
					conditions[i] = new NPCHordeKillsCondition(data.readUInt16(prefix + i + "_ID"), data.readInt16(prefix + i + "_Value"), data.readByte(prefix + i + "_Nav"), text, newShouldReset);
					break;
				case ENPCConditionType.KILLS_ANIMAL:
					if (!data.has(prefix + i + "_ID"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Animal kills condition ",
							prefix,
							i,
							" missing _ID in: ",
							errorMessageSource
						}));
					}
					if (!data.has(prefix + i + "_Value"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Animal kills condition ",
							prefix,
							i,
							" missing _Value in: ",
							errorMessageSource
						}));
					}
					if (!data.has(prefix + i + "_Animal"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Animal kills condition ",
							prefix,
							i,
							" missing _Animal in: ",
							errorMessageSource
						}));
					}
					conditions[i] = new NPCAnimalKillsCondition(data.readUInt16(prefix + i + "_ID"), data.readInt16(prefix + i + "_Value"), data.readUInt16(prefix + i + "_Animal"), text, newShouldReset);
					break;
				case ENPCConditionType.COMPARE_FLAGS:
					if (!data.has(prefix + i + "_A_ID"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Compare flags condition ",
							prefix,
							i,
							" missing _A_ID in: ",
							errorMessageSource
						}));
					}
					if (!data.has(prefix + i + "_B_ID"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Compare flags condition ",
							prefix,
							i,
							" missing _B_ID in: ",
							errorMessageSource
						}));
					}
					conditions[i] = new NPCCompareFlagsCondition(data.readUInt16(prefix + i + "_A_ID"), data.readUInt16(prefix + i + "_B_ID"), data.has(prefix + i + "_Allow_A_Unset"), data.has(prefix + i + "_Allow_B_Unset"), newLogicType, text, newShouldReset);
					break;
				case ENPCConditionType.TIME_OF_DAY:
					if (!data.has(prefix + i + "_Second"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Time of day condition ",
							prefix,
							i,
							" missing _Second in: ",
							errorMessageSource
						}));
					}
					conditions[i] = new NPCTimeOfDayCondition(data.readInt32(prefix + i + "_Second"), newLogicType, text, newShouldReset);
					break;
				case ENPCConditionType.PLAYER_LIFE_HEALTH:
					if (!data.has(prefix + i + "_Value"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Player life health condition ",
							prefix,
							i,
							" missing _Value in: ",
							errorMessageSource
						}));
					}
					conditions[i] = new NPCPlayerLifeHealthCondition(data.readInt32(prefix + i + "_Value"), newLogicType, text);
					break;
				case ENPCConditionType.PLAYER_LIFE_FOOD:
					if (!data.has(prefix + i + "_Value"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Player life food condition ",
							prefix,
							i,
							" missing _Value in: ",
							errorMessageSource
						}));
					}
					conditions[i] = new NPCPlayerLifeFoodCondition(data.readInt32(prefix + i + "_Value"), newLogicType, text);
					break;
				case ENPCConditionType.PLAYER_LIFE_WATER:
					if (!data.has(prefix + i + "_Value"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Player life water condition ",
							prefix,
							i,
							" missing _Value in: ",
							errorMessageSource
						}));
					}
					conditions[i] = new NPCPlayerLifeWaterCondition(data.readInt32(prefix + i + "_Value"), newLogicType, text);
					break;
				case ENPCConditionType.PLAYER_LIFE_VIRUS:
					if (!data.has(prefix + i + "_Value"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Player life virus condition ",
							prefix,
							i,
							" missing _Value in: ",
							errorMessageSource
						}));
					}
					conditions[i] = new NPCPlayerLifeVirusCondition(data.readInt32(prefix + i + "_Value"), newLogicType, text);
					break;
				}
			}
		}

		// Token: 0x06003448 RID: 13384 RVA: 0x00155AC4 File Offset: 0x00153EC4
		public static void readRewards(Data data, Local localization, string prefix, INPCReward[] rewards, string errorMessageSource)
		{
			for (int i = 0; i < rewards.Length; i++)
			{
				if (!data.has(prefix + i + "_Type"))
				{
					Assets.errors.Add(string.Concat(new object[]
					{
						"Missing ",
						prefix,
						i,
						" reward type"
					}));
					throw new NotSupportedException(string.Concat(new object[]
					{
						"Missing ",
						prefix,
						i,
						" reward type"
					}));
				}
				ENPCRewardType enpcrewardType = (ENPCRewardType)Enum.Parse(typeof(ENPCRewardType), data.readString(prefix + i + "_Type"), true);
				string text = localization.read(prefix + i);
				text = ItemTool.filterRarityRichText(text);
				switch (enpcrewardType)
				{
				case ENPCRewardType.EXPERIENCE:
					if (!data.has(prefix + i + "_Value"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Experience reward ",
							prefix,
							i,
							" missing _Value in: ",
							errorMessageSource
						}));
					}
					rewards[i] = new NPCExperienceReward(data.readUInt32(prefix + i + "_Value"), text);
					break;
				case ENPCRewardType.REPUTATION:
					if (!data.has(prefix + i + "_Value"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Reputation reward ",
							prefix,
							i,
							" missing _Value in: ",
							errorMessageSource
						}));
					}
					rewards[i] = new NPCReputationReward(data.readInt32(prefix + i + "_Value"), text);
					break;
				case ENPCRewardType.FLAG_BOOL:
					if (!data.has(prefix + i + "_ID"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Bool flag reward ",
							prefix,
							i,
							" missing _ID in: ",
							errorMessageSource
						}));
					}
					if (!data.has(prefix + i + "_Value"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Bool flag reward ",
							prefix,
							i,
							" missing _Value in: ",
							errorMessageSource
						}));
					}
					rewards[i] = new NPCBoolFlagReward(data.readUInt16(prefix + i + "_ID"), data.readBoolean(prefix + i + "_Value"), text);
					break;
				case ENPCRewardType.FLAG_SHORT:
					if (!data.has(prefix + i + "_ID"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Short flag reward ",
							prefix,
							i,
							" missing _ID in: ",
							errorMessageSource
						}));
					}
					if (!data.has(prefix + i + "_Value"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Short flag reward ",
							prefix,
							i,
							" missing _Value in: ",
							errorMessageSource
						}));
					}
					rewards[i] = new NPCShortFlagReward(data.readUInt16(prefix + i + "_ID"), data.readInt16(prefix + i + "_Value"), (ENPCModificationType)Enum.Parse(typeof(ENPCModificationType), data.readString(prefix + i + "_Modification"), true), text);
					break;
				case ENPCRewardType.FLAG_SHORT_RANDOM:
					if (!data.has(prefix + i + "_ID"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Random short flag reward ",
							prefix,
							i,
							" missing _ID in: ",
							errorMessageSource
						}));
					}
					if (!data.has(prefix + i + "_Min_Value"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Random short flag reward ",
							prefix,
							i,
							" missing _Min_Value in: ",
							errorMessageSource
						}));
					}
					if (!data.has(prefix + i + "_Max_Value"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Random short flag reward ",
							prefix,
							i,
							" missing _Max_Value in: ",
							errorMessageSource
						}));
					}
					rewards[i] = new NPCRandomShortFlagReward(data.readUInt16(prefix + i + "_ID"), data.readInt16(prefix + i + "_Min_Value"), data.readInt16(prefix + i + "_Max_Value"), (ENPCModificationType)Enum.Parse(typeof(ENPCModificationType), data.readString(prefix + i + "_Modification"), true), text);
					break;
				case ENPCRewardType.QUEST:
					if (!data.has(prefix + i + "_ID"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Quest reward ",
							prefix,
							i,
							" missing _ID in: ",
							errorMessageSource
						}));
					}
					rewards[i] = new NPCQuestReward(data.readUInt16(prefix + i + "_ID"), text);
					break;
				case ENPCRewardType.ITEM:
					if (!data.has(prefix + i + "_ID"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Item reward ",
							prefix,
							i,
							" missing _ID in: ",
							errorMessageSource
						}));
					}
					if (!data.has(prefix + i + "_Amount"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Item reward ",
							prefix,
							i,
							" missing _Amount in: ",
							errorMessageSource
						}));
					}
					rewards[i] = new NPCItemReward(data.readUInt16(prefix + i + "_ID"), data.readByte(prefix + i + "_Amount"), data.readUInt16(prefix + i + "_Sight"), data.readUInt16(prefix + i + "_Tactical"), data.readUInt16(prefix + i + "_Grip"), data.readUInt16(prefix + i + "_Barrel"), data.readUInt16(prefix + i + "_Magazine"), data.readByte(prefix + i + "_Ammo"), text);
					break;
				case ENPCRewardType.ITEM_RANDOM:
					if (!data.has(prefix + i + "_ID"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Random item reward ",
							prefix,
							i,
							" missing _ID in: ",
							errorMessageSource
						}));
					}
					if (!data.has(prefix + i + "_Amount"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Random item reward ",
							prefix,
							i,
							" missing _Amount in: ",
							errorMessageSource
						}));
					}
					rewards[i] = new NPCRandomItemReward(data.readUInt16(prefix + i + "_ID"), data.readByte(prefix + i + "_Amount"), text);
					break;
				case ENPCRewardType.ACHIEVEMENT:
					if (!data.has(prefix + i + "_ID"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Achievement reward ",
							prefix,
							i,
							" missing _ID in: ",
							errorMessageSource
						}));
					}
					rewards[i] = new NPCAchievementReward(data.readString(prefix + i + "_ID"), text);
					break;
				case ENPCRewardType.VEHICLE:
					if (!data.has(prefix + i + "_ID"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Vehicle reward ",
							prefix,
							i,
							" missing _ID in: ",
							errorMessageSource
						}));
					}
					if (!data.has(prefix + i + "_Spawnpoint"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Vehicle reward ",
							prefix,
							i,
							" missing _Spawnpoint in: ",
							errorMessageSource
						}));
					}
					rewards[i] = new NPCVehicleReward(data.readUInt16(prefix + i + "_ID"), data.readString(prefix + i + "_Spawnpoint"), text);
					break;
				case ENPCRewardType.TELEPORT:
					if (!data.has(prefix + i + "_Spawnpoint"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Teleport reward ",
							prefix,
							i,
							" missing _Spawnpoint in: ",
							errorMessageSource
						}));
					}
					rewards[i] = new NPCTeleportReward(data.readString(prefix + i + "_Spawnpoint"), text);
					break;
				case ENPCRewardType.EVENT:
					if (!data.has(prefix + i + "_ID"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Event reward ",
							prefix,
							i,
							" missing _ID in: ",
							errorMessageSource
						}));
					}
					rewards[i] = new NPCEventReward(data.readString(prefix + i + "_ID"), text);
					break;
				case ENPCRewardType.FLAG_MATH:
					if (!data.has(prefix + i + "_A_ID"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Math reward ",
							prefix,
							i,
							" missing _A_ID in: ",
							errorMessageSource
						}));
					}
					if (!data.has(prefix + i + "_B_ID"))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Math reward ",
							prefix,
							i,
							" missing _B_ID in: ",
							errorMessageSource
						}));
					}
					rewards[i] = new NPCFlagMathReward(data.readUInt16(prefix + i + "_A_ID"), data.readUInt16(prefix + i + "_B_ID"), (ENPCOperationType)Enum.Parse(typeof(ENPCOperationType), data.readString(prefix + i + "_Operation"), true), text);
					break;
				}
			}
		}
	}
}
