using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using SDG.Provider;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020005CE RID: 1486
	public class Characters : MonoBehaviour
	{
		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x060029FE RID: 10750 RVA: 0x00104A4F File Offset: 0x00102E4F
		// (set) Token: 0x060029FF RID: 10751 RVA: 0x00104A56 File Offset: 0x00102E56
		public static byte selected
		{
			get
			{
				return Characters._selected;
			}
			set
			{
				Characters._selected = value;
				if (Characters.onCharacterUpdated != null)
				{
					Characters.onCharacterUpdated(Characters.selected, Characters.active);
				}
				Characters.apply();
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06002A00 RID: 10752 RVA: 0x00104A81 File Offset: 0x00102E81
		public static Character active
		{
			get
			{
				return Characters.list[(int)Characters.selected];
			}
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06002A01 RID: 10753 RVA: 0x00104A8E File Offset: 0x00102E8E
		public static List<ulong> packageSkins
		{
			get
			{
				return Characters._packageSkins;
			}
		}

		// Token: 0x06002A02 RID: 10754 RVA: 0x00104A95 File Offset: 0x00102E95
		public static void rename(string name)
		{
			Characters.active.name = name;
			if (Characters.onCharacterUpdated != null)
			{
				Characters.onCharacterUpdated(Characters.selected, Characters.active);
			}
		}

		// Token: 0x06002A03 RID: 10755 RVA: 0x00104AC0 File Offset: 0x00102EC0
		public static void skillify(EPlayerSkillset skillset)
		{
			Characters.active.skillset = skillset;
			if (Characters.onCharacterUpdated != null)
			{
				Characters.onCharacterUpdated(Characters.selected, Characters.active);
			}
			Characters.active.applyHero();
			Characters.apply();
		}

		// Token: 0x06002A04 RID: 10756 RVA: 0x00104AFA File Offset: 0x00102EFA
		public static void growFace(byte face)
		{
			Characters.active.face = face;
			Characters.apply(false, false);
		}

		// Token: 0x06002A05 RID: 10757 RVA: 0x00104B0E File Offset: 0x00102F0E
		public static void growHair(byte hair)
		{
			Characters.active.hair = hair;
			Characters.apply(false, false);
		}

		// Token: 0x06002A06 RID: 10758 RVA: 0x00104B22 File Offset: 0x00102F22
		public static void growBeard(byte beard)
		{
			Characters.active.beard = beard;
			Characters.apply(false, false);
		}

		// Token: 0x06002A07 RID: 10759 RVA: 0x00104B36 File Offset: 0x00102F36
		public static void paintSkin(Color color)
		{
			Characters.active.skin = color;
			Characters.apply(false, false);
		}

		// Token: 0x06002A08 RID: 10760 RVA: 0x00104B4A File Offset: 0x00102F4A
		public static void paintColor(Color color)
		{
			Characters.active.color = color;
			Characters.apply(false, false);
		}

		// Token: 0x06002A09 RID: 10761 RVA: 0x00104B5E File Offset: 0x00102F5E
		public static void renick(string nick)
		{
			Characters.active.nick = nick;
			if (Characters.onCharacterUpdated != null)
			{
				Characters.onCharacterUpdated(Characters.selected, Characters.active);
			}
		}

		// Token: 0x06002A0A RID: 10762 RVA: 0x00104B89 File Offset: 0x00102F89
		public static void paintMarkerColor(Color color)
		{
			Characters.active.markerColor = color;
		}

		// Token: 0x06002A0B RID: 10763 RVA: 0x00104B98 File Offset: 0x00102F98
		public static void group(CSteamID group)
		{
			if (Characters.active.group == group)
			{
				Characters.active.group = CSteamID.Nil;
			}
			else
			{
				Characters.active.group = group;
			}
			if (Characters.onCharacterUpdated != null)
			{
				Characters.onCharacterUpdated(Characters.selected, Characters.active);
			}
		}

		// Token: 0x06002A0C RID: 10764 RVA: 0x00104BF7 File Offset: 0x00102FF7
		public static void ungroup()
		{
			Characters.active.group = CSteamID.Nil;
			if (Characters.onCharacterUpdated != null)
			{
				Characters.onCharacterUpdated(Characters.selected, Characters.active);
			}
		}

		// Token: 0x06002A0D RID: 10765 RVA: 0x00104C26 File Offset: 0x00103026
		public static void hand(bool state)
		{
			Characters.active.hand = state;
			Characters.apply(false, false);
			if (Characters.onCharacterUpdated != null)
			{
				Characters.onCharacterUpdated(Characters.selected, Characters.active);
			}
		}

		// Token: 0x06002A0E RID: 10766 RVA: 0x00104C58 File Offset: 0x00103058
		public static bool isSkinEquipped(ulong instance)
		{
			return instance != 0UL && Characters.packageSkins.IndexOf(instance) != -1;
		}

		// Token: 0x06002A0F RID: 10767 RVA: 0x00104C78 File Offset: 0x00103078
		public static bool isCosmeticEquipped(ulong instance)
		{
			return instance != 0UL && (Characters.active.packageBackpack == instance || Characters.active.packageGlasses == instance || Characters.active.packageHat == instance || Characters.active.packageMask == instance || Characters.active.packagePants == instance || Characters.active.packageShirt == instance || Characters.active.packageVest == instance);
		}

		// Token: 0x06002A10 RID: 10768 RVA: 0x00104D00 File Offset: 0x00103100
		public static void package(ulong package)
		{
			int inventoryItem = Provider.provider.economyService.getInventoryItem(package);
			if (inventoryItem == 0)
			{
				return;
			}
			ushort num;
			ushort num2;
			Provider.provider.economyService.getInventoryTargetID(inventoryItem, out num, out num2);
			if (num == 0 && num2 == 0)
			{
				return;
			}
			ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, num);
			if (itemAsset == null || itemAsset.proPath == null || itemAsset.proPath.Length == 0)
			{
				if (Provider.provider.economyService.getInventorySkinID(inventoryItem) == 0)
				{
					return;
				}
				if (!Characters.packageSkins.Remove(package))
				{
					for (int i = 0; i < Characters.packageSkins.Count; i++)
					{
						ulong num3 = Characters.packageSkins[i];
						if (num3 != 0UL)
						{
							int inventoryItem2 = Provider.provider.economyService.getInventoryItem(num3);
							if (inventoryItem2 != 0)
							{
								ushort num4;
								ushort num5;
								Provider.provider.economyService.getInventoryTargetID(inventoryItem2, out num4, out num5);
								if ((num != 0 && num == num4) || (num2 != 0 && num2 == num5))
								{
									Characters.packageSkins.RemoveAt(i);
									break;
								}
							}
						}
					}
					Characters.packageSkins.Add(package);
				}
			}
			if (itemAsset != null)
			{
				if (itemAsset.type == EItemType.SHIRT)
				{
					if (Characters.active.packageShirt == package)
					{
						Characters.active.packageShirt = 0UL;
					}
					else
					{
						Characters.active.packageShirt = package;
					}
				}
				else if (itemAsset.type == EItemType.PANTS)
				{
					if (Characters.active.packagePants == package)
					{
						Characters.active.packagePants = 0UL;
					}
					else
					{
						Characters.active.packagePants = package;
					}
				}
				else if (itemAsset.type == EItemType.HAT)
				{
					if (Characters.active.packageHat == package)
					{
						Characters.active.packageHat = 0UL;
					}
					else
					{
						Characters.active.packageHat = package;
					}
				}
				else if (itemAsset.type == EItemType.BACKPACK)
				{
					if (Characters.active.packageBackpack == package)
					{
						Characters.active.packageBackpack = 0UL;
					}
					else
					{
						Characters.active.packageBackpack = package;
					}
				}
				else if (itemAsset.type == EItemType.VEST)
				{
					if (Characters.active.packageVest == package)
					{
						Characters.active.packageVest = 0UL;
					}
					else
					{
						Characters.active.packageVest = package;
					}
				}
				else if (itemAsset.type == EItemType.MASK)
				{
					if (Characters.active.packageMask == package)
					{
						Characters.active.packageMask = 0UL;
					}
					else
					{
						Characters.active.packageMask = package;
					}
				}
				else if (itemAsset.type == EItemType.GLASSES)
				{
					if (Characters.active.packageGlasses == package)
					{
						Characters.active.packageGlasses = 0UL;
					}
					else
					{
						Characters.active.packageGlasses = package;
					}
				}
			}
			Characters.apply(false, true);
			if (Characters.onCharacterUpdated != null)
			{
				Characters.onCharacterUpdated(Characters.selected, Characters.active);
			}
		}

		// Token: 0x06002A11 RID: 10769 RVA: 0x00105018 File Offset: 0x00103418
		public static bool getPackageForItemID(ushort itemID, out ulong package)
		{
			package = 0UL;
			for (int i = 0; i < Characters.packageSkins.Count; i++)
			{
				package = Characters.packageSkins[i];
				if (package != 0UL)
				{
					int inventoryItem = Provider.provider.economyService.getInventoryItem(package);
					if (inventoryItem != 0)
					{
						ushort inventoryItemID = Provider.provider.economyService.getInventoryItemID(inventoryItem);
						if (inventoryItemID != 0)
						{
							if (itemID == inventoryItemID)
							{
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06002A12 RID: 10770 RVA: 0x001050A4 File Offset: 0x001034A4
		private static bool getSlot0StatTrackerValue(out EStatTrackerType type, out int kills)
		{
			type = EStatTrackerType.NONE;
			kills = -1;
			ulong instance;
			return Characters.getPackageForItemID(Characters.active.primaryItem, out instance) && Provider.provider.economyService.getInventoryStatTrackerValue(instance, out type, out kills);
		}

		// Token: 0x06002A13 RID: 10771 RVA: 0x001050E4 File Offset: 0x001034E4
		private static bool getSlot1StatTrackerValue(out EStatTrackerType type, out int kills)
		{
			type = EStatTrackerType.NONE;
			kills = -1;
			ulong instance;
			return Characters.getPackageForItemID(Characters.active.secondaryItem, out instance) && Provider.provider.economyService.getInventoryStatTrackerValue(instance, out type, out kills);
		}

		// Token: 0x06002A14 RID: 10772 RVA: 0x00105121 File Offset: 0x00103521
		private static void apply(byte slot)
		{
			Characters.apply(slot, true);
		}

		// Token: 0x06002A15 RID: 10773 RVA: 0x0010512C File Offset: 0x0010352C
		private static void apply(byte slot, bool showItems)
		{
			if (Characters.slots[(int)slot] != null)
			{
				UnityEngine.Object.Destroy(Characters.slots[(int)slot].gameObject);
			}
			if (!showItems)
			{
				return;
			}
			ushort num = 0;
			byte[] state = null;
			if (slot == 0)
			{
				num = Characters.active.primaryItem;
				state = Characters.active.primaryState;
			}
			else if (slot == 1)
			{
				num = Characters.active.secondaryItem;
				state = Characters.active.secondaryState;
			}
			if (num == 0)
			{
				return;
			}
			ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, num);
			if (itemAsset != null)
			{
				ushort skin = 0;
				ushort num2 = 0;
				for (int i = 0; i < Characters.packageSkins.Count; i++)
				{
					ulong num3 = Characters.packageSkins[i];
					if (num3 != 0UL)
					{
						int inventoryItem = Provider.provider.economyService.getInventoryItem(num3);
						if (inventoryItem != 0)
						{
							ushort inventoryItemID = Provider.provider.economyService.getInventoryItemID(inventoryItem);
							if (inventoryItemID != 0)
							{
								if (num == inventoryItemID)
								{
									skin = Provider.provider.economyService.getInventorySkinID(inventoryItem);
									num2 = Provider.provider.economyService.getInventoryMythicID(inventoryItem);
									break;
								}
							}
						}
					}
				}
				GetStatTrackerValueHandler statTrackerCallback = null;
				if (slot == 0)
				{
					if (Characters.<>f__mg$cache0 == null)
					{
						Characters.<>f__mg$cache0 = new GetStatTrackerValueHandler(Characters.getSlot0StatTrackerValue);
					}
					statTrackerCallback = Characters.<>f__mg$cache0;
				}
				else if (slot == 1)
				{
					if (Characters.<>f__mg$cache1 == null)
					{
						Characters.<>f__mg$cache1 = new GetStatTrackerValueHandler(Characters.getSlot1StatTrackerValue);
					}
					statTrackerCallback = Characters.<>f__mg$cache1;
				}
				Transform item = ItemTool.getItem(num, skin, 100, state, false, statTrackerCallback);
				if (slot == 0)
				{
					if (itemAsset.type == EItemType.MELEE)
					{
						item.transform.parent = Characters.primaryMeleeSlot;
					}
					else if (itemAsset.slot == ESlotType.PRIMARY)
					{
						item.transform.parent = Characters.primaryLargeGunSlot;
					}
					else
					{
						item.transform.parent = Characters.primarySmallGunSlot;
					}
				}
				else if (slot == 1)
				{
					if (itemAsset.type == EItemType.MELEE)
					{
						item.transform.parent = Characters.secondaryMeleeSlot;
					}
					else
					{
						item.transform.parent = Characters.secondaryGunSlot;
					}
				}
				item.localPosition = Vector3.zero;
				item.localRotation = Quaternion.Euler(0f, 0f, 90f);
				item.localScale = Vector3.one;
				UnityEngine.Object.Destroy(item.GetComponent<Collider>());
				if (num2 != 0)
				{
					ItemTool.applyEffect(item, num2, EEffectType.THIRD);
				}
				Characters.slots[(int)slot] = item;
			}
		}

		// Token: 0x06002A16 RID: 10774 RVA: 0x001053C7 File Offset: 0x001037C7
		public static void apply()
		{
			Characters.apply(true, true);
		}

		// Token: 0x06002A17 RID: 10775 RVA: 0x001053D0 File Offset: 0x001037D0
		public static void apply(bool showItems, bool showCosmetics)
		{
			if (Characters.active == null)
			{
				Debug.LogError("Failed to find an active character.");
				return;
			}
			if (Characters.clothes == null)
			{
				Debug.LogError("Failed to find character clothes.");
				return;
			}
			Characters.character.localScale = new Vector3((float)((!Characters.active.hand) ? 1 : -1), 1f, 1f);
			if (showItems)
			{
				Characters.clothes.shirt = Characters.active.shirt;
				Characters.clothes.pants = Characters.active.pants;
				Characters.clothes.hat = Characters.active.hat;
				Characters.clothes.backpack = Characters.active.backpack;
				Characters.clothes.vest = Characters.active.vest;
				Characters.clothes.mask = Characters.active.mask;
				Characters.clothes.glasses = Characters.active.glasses;
			}
			else
			{
				Characters.clothes.shirt = 0;
				Characters.clothes.pants = 0;
				Characters.clothes.hat = 0;
				Characters.clothes.backpack = 0;
				Characters.clothes.vest = 0;
				Characters.clothes.mask = 0;
				Characters.clothes.glasses = 0;
			}
			if (showCosmetics)
			{
				if (Characters.active.packageShirt != 0UL)
				{
					Characters.clothes.visualShirt = Provider.provider.economyService.getInventoryItem(Characters.active.packageShirt);
				}
				else
				{
					Characters.clothes.visualShirt = 0;
				}
				if (Characters.active.packagePants != 0UL)
				{
					Characters.clothes.visualPants = Provider.provider.economyService.getInventoryItem(Characters.active.packagePants);
				}
				else
				{
					Characters.clothes.visualPants = 0;
				}
				if (Characters.active.packageHat != 0UL)
				{
					Characters.clothes.visualHat = Provider.provider.economyService.getInventoryItem(Characters.active.packageHat);
				}
				else
				{
					Characters.clothes.visualHat = 0;
				}
				if (Characters.active.packageBackpack != 0UL)
				{
					Characters.clothes.visualBackpack = Provider.provider.economyService.getInventoryItem(Characters.active.packageBackpack);
				}
				else
				{
					Characters.clothes.visualBackpack = 0;
				}
				if (Characters.active.packageVest != 0UL)
				{
					Characters.clothes.visualVest = Provider.provider.economyService.getInventoryItem(Characters.active.packageVest);
				}
				else
				{
					Characters.clothes.visualVest = 0;
				}
				if (Characters.active.packageMask != 0UL)
				{
					Characters.clothes.visualMask = Provider.provider.economyService.getInventoryItem(Characters.active.packageMask);
				}
				else
				{
					Characters.clothes.visualMask = 0;
				}
				if (Characters.active.packageGlasses != 0UL)
				{
					Characters.clothes.visualGlasses = Provider.provider.economyService.getInventoryItem(Characters.active.packageGlasses);
				}
				else
				{
					Characters.clothes.visualGlasses = 0;
				}
			}
			else
			{
				Characters.clothes.visualShirt = 0;
				Characters.clothes.visualPants = 0;
				Characters.clothes.visualHat = 0;
				Characters.clothes.visualBackpack = 0;
				Characters.clothes.visualVest = 0;
				Characters.clothes.visualMask = 0;
				Characters.clothes.visualGlasses = 0;
			}
			Characters.clothes.face = Characters.active.face;
			Characters.clothes.hair = Characters.active.hair;
			Characters.clothes.beard = Characters.active.beard;
			Characters.clothes.skin = Characters.active.skin;
			Characters.clothes.color = Characters.active.color;
			Characters.clothes.hand = Characters.active.hand;
			Characters.clothes.apply();
			byte b = 0;
			while ((int)b < Characters.slots.Length)
			{
				Characters.apply(b, showItems);
				b += 1;
			}
		}

		// Token: 0x06002A18 RID: 10776 RVA: 0x001057F8 File Offset: 0x00103BF8
		private static void onInventoryRefreshed()
		{
			if (Characters.clothes != null && Characters.list != null && Characters.packageSkins != null)
			{
				for (int i = Characters.packageSkins.Count - 1; i >= 0; i--)
				{
					ulong num = Characters.packageSkins[i];
					if (num != 0UL && Provider.provider.economyService.getInventoryItem(num) == 0)
					{
						Characters.packageSkins.RemoveAt(i);
					}
				}
				for (int j = 0; j < Characters.list.Length; j++)
				{
					Character character = Characters.list[j];
					if (character != null)
					{
						if (character.packageShirt != 0UL && Provider.provider.economyService.getInventoryItem(character.packageShirt) == 0)
						{
							character.packageShirt = 0UL;
						}
						if (character.packagePants != 0UL && Provider.provider.economyService.getInventoryItem(character.packagePants) == 0)
						{
							character.packagePants = 0UL;
						}
						if (character.packageHat != 0UL && Provider.provider.economyService.getInventoryItem(character.packageHat) == 0)
						{
							character.packageHat = 0UL;
						}
						if (character.packageBackpack != 0UL && Provider.provider.economyService.getInventoryItem(character.packageBackpack) == 0)
						{
							character.packageBackpack = 0UL;
						}
						if (character.packageVest != 0UL && Provider.provider.economyService.getInventoryItem(character.packageVest) == 0)
						{
							character.packageVest = 0UL;
						}
						if (character.packageMask != 0UL && Provider.provider.economyService.getInventoryItem(character.packageMask) == 0)
						{
							character.packageMask = 0UL;
						}
						if (character.packageGlasses != 0UL && Provider.provider.economyService.getInventoryItem(character.packageGlasses) == 0)
						{
							character.packageGlasses = 0UL;
						}
					}
				}
				if (!Characters.initialApply)
				{
					Characters.initialApply = true;
					Characters.apply();
				}
			}
			if (Characters.hasDropped)
			{
				return;
			}
			Characters.hasDropped = true;
			if (Characters.hasPlayed)
			{
				Provider.provider.economyService.dropInventory();
			}
		}

		// Token: 0x06002A19 RID: 10777 RVA: 0x00105A30 File Offset: 0x00103E30
		private void Update()
		{
			if (Dedicator.isDedicated)
			{
				return;
			}
			if (Characters.character == null)
			{
				return;
			}
			Characters._characterYaw = Mathf.Lerp(Characters._characterYaw, Characters.characterOffset + Characters.characterYaw, 4f * Time.deltaTime);
			Characters.character.transform.rotation = Quaternion.Euler(90f, Characters._characterYaw, 0f);
		}

		// Token: 0x06002A1A RID: 10778 RVA: 0x00105AA4 File Offset: 0x00103EA4
		private void Start()
		{
			Characters.hasDropped = false;
			if (Dedicator.isDedicated)
			{
				return;
			}
			if (!Characters.hasLoaded)
			{
				TempSteamworksEconomy economyService = Provider.provider.economyService;
				Delegate onInventoryRefreshed = economyService.onInventoryRefreshed;
				if (Characters.<>f__mg$cache2 == null)
				{
					Characters.<>f__mg$cache2 = new TempSteamworksEconomy.InventoryRefreshed(Characters.onInventoryRefreshed);
				}
				economyService.onInventoryRefreshed = (TempSteamworksEconomy.InventoryRefreshed)Delegate.Combine(onInventoryRefreshed, Characters.<>f__mg$cache2);
			}
			Characters.load();
		}

		// Token: 0x06002A1B RID: 10779 RVA: 0x00105B10 File Offset: 0x00103F10
		private void Awake()
		{
			if (Dedicator.isDedicated)
			{
				return;
			}
			Characters.character = GameObject.Find("Hero").transform;
			Characters.clothes = Characters.character.GetComponent<HumanClothes>();
			Characters.clothes.isView = true;
			Characters.slots = new Transform[(int)PlayerInventory.SLOTS];
			Characters.primaryMeleeSlot = Characters.character.FindChild("Skeleton").FindChild("Spine").FindChild("Primary_Melee");
			Characters.primaryLargeGunSlot = Characters.character.FindChild("Skeleton").FindChild("Spine").FindChild("Primary_Large_Gun");
			Characters.primarySmallGunSlot = Characters.character.FindChild("Skeleton").FindChild("Spine").FindChild("Primary_Small_Gun");
			Characters.secondaryMeleeSlot = Characters.character.FindChild("Skeleton").FindChild("Right_Hip").FindChild("Right_Leg").FindChild("Secondary_Melee");
			Characters.secondaryGunSlot = Characters.character.FindChild("Skeleton").FindChild("Right_Hip").FindChild("Right_Leg").FindChild("Secondary_Gun");
			Characters.characterOffset = Characters.character.transform.eulerAngles.y;
			Characters._characterYaw = Characters.characterOffset;
			Characters.characterYaw = 0f;
		}

		// Token: 0x06002A1C RID: 10780 RVA: 0x00105C74 File Offset: 0x00104074
		public static void load()
		{
			Characters.initialApply = false;
			Provider.provider.economyService.refreshInventory();
			if (Characters.list != null)
			{
				byte b = 0;
				while ((int)b < Characters.list.Length)
				{
					if (Characters.list[(int)b] != null)
					{
						if (Characters.onCharacterUpdated != null)
						{
							Characters.onCharacterUpdated(b, Characters.list[(int)b]);
						}
					}
					b += 1;
				}
				return;
			}
			Characters.list = new Character[(int)(Customization.FREE_CHARACTERS + Customization.PRO_CHARACTERS)];
			Characters._packageSkins = new List<ulong>();
			if (ReadWrite.fileExists("/Characters.dat", true))
			{
				Block block = ReadWrite.readBlock("/Characters.dat", true, 0);
				if (block != null)
				{
					byte b2 = block.readByte();
					if (b2 >= 12)
					{
						if (b2 >= 14)
						{
							ushort num = block.readUInt16();
							for (ushort num2 = 0; num2 < num; num2 += 1)
							{
								ulong num3 = block.readUInt64();
								if (num3 != 0UL)
								{
									Characters.packageSkins.Add(num3);
								}
							}
						}
						Characters._selected = block.readByte();
						if (!Provider.isPro && Characters.selected >= Customization.FREE_CHARACTERS)
						{
							Characters._selected = 0;
						}
						byte b3 = 0;
						while ((int)b3 < Characters.list.Length)
						{
							ushort newShirt = block.readUInt16();
							ushort newPants = block.readUInt16();
							ushort newHat = block.readUInt16();
							ushort newBackpack = block.readUInt16();
							ushort newVest = block.readUInt16();
							ushort newMask = block.readUInt16();
							ushort newGlasses = block.readUInt16();
							ulong newPackageShirt = block.readUInt64();
							ulong newPackagePants = block.readUInt64();
							ulong newPackageHat = block.readUInt64();
							ulong newPackageBackpack = block.readUInt64();
							ulong newPackageVest = block.readUInt64();
							ulong newPackageMask = block.readUInt64();
							ulong newPackageGlasses = block.readUInt64();
							ushort newPrimaryItem = block.readUInt16();
							byte[] newPrimaryState = block.readByteArray();
							ushort newSecondaryItem = block.readUInt16();
							byte[] newSecondaryState = block.readByteArray();
							byte b4 = block.readByte();
							byte b5 = block.readByte();
							byte b6 = block.readByte();
							Color color = block.readColor();
							Color color2 = block.readColor();
							Color newMarkerColor;
							if (b2 > 20)
							{
								newMarkerColor = block.readColor();
							}
							else
							{
								newMarkerColor = Customization.MARKER_COLORS[UnityEngine.Random.Range(0, Customization.MARKER_COLORS.Length)];
							}
							bool newHand = block.readBoolean();
							string newName = block.readString();
							if (b2 < 19)
							{
								newName = Provider.clientName;
							}
							string newNick = block.readString();
							CSteamID csteamID = block.readSteamID();
							byte b7 = block.readByte();
							if (!Provider.provider.communityService.checkGroup(csteamID))
							{
								csteamID = CSteamID.Nil;
							}
							if (b7 >= Customization.SKILLSETS)
							{
								b7 = 0;
							}
							if (b2 < 16)
							{
								b7 = (byte)UnityEngine.Random.Range(1, (int)Customization.SKILLSETS);
							}
							if (b2 > 16 && b2 < 20)
							{
								block.readBoolean();
							}
							if (!Provider.isPro)
							{
								if (b4 >= Customization.FACES_FREE)
								{
									b4 = (byte)UnityEngine.Random.Range(0, (int)Customization.FACES_FREE);
								}
								if (b5 >= Customization.HAIRS_FREE)
								{
									b5 = (byte)UnityEngine.Random.Range(0, (int)Customization.HAIRS_FREE);
								}
								if (b6 >= Customization.BEARDS_FREE)
								{
									b6 = 0;
								}
								if (!Customization.checkSkin(color))
								{
									color = Customization.SKINS[UnityEngine.Random.Range(0, Customization.SKINS.Length)];
								}
								if (!Customization.checkColor(color2))
								{
									color2 = Customization.COLORS[UnityEngine.Random.Range(0, Customization.COLORS.Length)];
								}
							}
							Characters.list[(int)b3] = new Character(newShirt, newPants, newHat, newBackpack, newVest, newMask, newGlasses, newPackageShirt, newPackagePants, newPackageHat, newPackageBackpack, newPackageVest, newPackageMask, newPackageGlasses, newPrimaryItem, newPrimaryState, newSecondaryItem, newSecondaryState, b4, b5, b6, color, color2, newMarkerColor, newHand, newName, newNick, csteamID, (EPlayerSkillset)b7);
							if (Characters.onCharacterUpdated != null)
							{
								Characters.onCharacterUpdated(b3, Characters.list[(int)b3]);
							}
							b3 += 1;
						}
					}
					else
					{
						byte b8 = 0;
						while ((int)b8 < Characters.list.Length)
						{
							Characters.list[(int)b8] = new Character();
							if (Characters.onCharacterUpdated != null)
							{
								Characters.onCharacterUpdated(b8, Characters.list[(int)b8]);
							}
							b8 += 1;
						}
					}
				}
			}
			else
			{
				Characters._selected = 0;
			}
			byte b9 = 0;
			while ((int)b9 < Characters.list.Length)
			{
				if (Characters.list[(int)b9] == null)
				{
					Characters.list[(int)b9] = new Character();
					if (Characters.onCharacterUpdated != null)
					{
						Characters.onCharacterUpdated(b9, Characters.list[(int)b9]);
					}
				}
				b9 += 1;
			}
			Characters.apply();
			Characters.hasLoaded = true;
		}

		// Token: 0x06002A1D RID: 10781 RVA: 0x001060FC File Offset: 0x001044FC
		public static void save()
		{
			if (!Characters.hasLoaded)
			{
				return;
			}
			Block block = new Block();
			block.writeByte(Characters.SAVEDATA_VERSION);
			block.writeUInt16((ushort)Characters.packageSkins.Count);
			ushort num = 0;
			while ((int)num < Characters.packageSkins.Count)
			{
				ulong value = Characters.packageSkins[(int)num];
				block.writeUInt64(value);
				num += 1;
			}
			block.writeByte(Characters.selected);
			byte b = 0;
			while ((int)b < Characters.list.Length)
			{
				Character character = Characters.list[(int)b];
				if (character == null)
				{
					character = new Character();
				}
				block.writeUInt16(character.shirt);
				block.writeUInt16(character.pants);
				block.writeUInt16(character.hat);
				block.writeUInt16(character.backpack);
				block.writeUInt16(character.vest);
				block.writeUInt16(character.mask);
				block.writeUInt16(character.glasses);
				block.writeUInt64(character.packageShirt);
				block.writeUInt64(character.packagePants);
				block.writeUInt64(character.packageHat);
				block.writeUInt64(character.packageBackpack);
				block.writeUInt64(character.packageVest);
				block.writeUInt64(character.packageMask);
				block.writeUInt64(character.packageGlasses);
				block.writeUInt16(character.primaryItem);
				block.writeByteArray(character.primaryState);
				block.writeUInt16(character.secondaryItem);
				block.writeByteArray(character.secondaryState);
				block.writeByte(character.face);
				block.writeByte(character.hair);
				block.writeByte(character.beard);
				block.writeColor(character.skin);
				block.writeColor(character.color);
				block.writeColor(character.markerColor);
				block.writeBoolean(character.hand);
				block.writeString(character.name);
				block.writeString(character.nick);
				block.writeSteamID(character.group);
				block.writeByte((byte)character.skillset);
				b += 1;
			}
			ReadWrite.writeBlock("/Characters.dat", true, block);
		}

		// Token: 0x040019FE RID: 6654
		public static readonly byte SAVEDATA_VERSION = 21;

		// Token: 0x040019FF RID: 6655
		private static bool hasLoaded;

		// Token: 0x04001A00 RID: 6656
		private static bool initialApply;

		// Token: 0x04001A01 RID: 6657
		public static bool hasPlayed;

		// Token: 0x04001A02 RID: 6658
		private static bool hasDropped;

		// Token: 0x04001A03 RID: 6659
		public static CharacterUpdated onCharacterUpdated;

		// Token: 0x04001A04 RID: 6660
		private static byte _selected;

		// Token: 0x04001A05 RID: 6661
		private static Character[] list;

		// Token: 0x04001A06 RID: 6662
		private static Transform character;

		// Token: 0x04001A07 RID: 6663
		public static HumanClothes clothes;

		// Token: 0x04001A08 RID: 6664
		private static Transform[] slots;

		// Token: 0x04001A09 RID: 6665
		private static Transform primaryMeleeSlot;

		// Token: 0x04001A0A RID: 6666
		private static Transform primaryLargeGunSlot;

		// Token: 0x04001A0B RID: 6667
		private static Transform primarySmallGunSlot;

		// Token: 0x04001A0C RID: 6668
		private static Transform secondaryMeleeSlot;

		// Token: 0x04001A0D RID: 6669
		private static Transform secondaryGunSlot;

		// Token: 0x04001A0E RID: 6670
		private static List<ulong> _packageSkins;

		// Token: 0x04001A0F RID: 6671
		private static float characterOffset;

		// Token: 0x04001A10 RID: 6672
		private static float _characterYaw;

		// Token: 0x04001A11 RID: 6673
		public static float characterYaw;

		// Token: 0x04001A12 RID: 6674
		[CompilerGenerated]
		private static GetStatTrackerValueHandler <>f__mg$cache0;

		// Token: 0x04001A13 RID: 6675
		[CompilerGenerated]
		private static GetStatTrackerValueHandler <>f__mg$cache1;

		// Token: 0x04001A14 RID: 6676
		[CompilerGenerated]
		private static TempSteamworksEconomy.InventoryRefreshed <>f__mg$cache2;
	}
}
