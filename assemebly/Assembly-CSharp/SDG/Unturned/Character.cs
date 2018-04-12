using System;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020005CC RID: 1484
	public class Character
	{
		// Token: 0x060029F6 RID: 10742 RVA: 0x001046DC File Offset: 0x00102ADC
		public Character()
		{
			this.face = (byte)UnityEngine.Random.Range(0, (int)Customization.FACES_FREE);
			this.hair = (byte)UnityEngine.Random.Range(0, (int)Customization.HAIRS_FREE);
			this.beard = 0;
			this.skin = Customization.SKINS[UnityEngine.Random.Range(0, Customization.SKINS.Length)];
			this.color = Customization.COLORS[UnityEngine.Random.Range(0, Customization.COLORS.Length)];
			this.markerColor = Customization.MARKER_COLORS[UnityEngine.Random.Range(0, Customization.MARKER_COLORS.Length)];
			this.hand = false;
			this.name = Provider.clientName;
			this.nick = Provider.clientName;
			this.group = CSteamID.Nil;
			this.skillset = (EPlayerSkillset)UnityEngine.Random.Range(1, (int)Customization.SKILLSETS);
			this.applyHero();
		}

		// Token: 0x060029F7 RID: 10743 RVA: 0x001047C0 File Offset: 0x00102BC0
		public Character(ushort newShirt, ushort newPants, ushort newHat, ushort newBackpack, ushort newVest, ushort newMask, ushort newGlasses, ulong newPackageShirt, ulong newPackagePants, ulong newPackageHat, ulong newPackageBackpack, ulong newPackageVest, ulong newPackageMask, ulong newPackageGlasses, ushort newPrimaryItem, byte[] newPrimaryState, ushort newSecondaryItem, byte[] newSecondaryState, byte newFace, byte newHair, byte newBeard, Color newSkin, Color newColor, Color newMarkerColor, bool newHand, string newName, string newNick, CSteamID newGroup, EPlayerSkillset newSkillset)
		{
			this.shirt = newShirt;
			this.pants = newPants;
			this.hat = newHat;
			this.backpack = newBackpack;
			this.vest = newVest;
			this.mask = newMask;
			this.glasses = newGlasses;
			this.packageShirt = newPackageShirt;
			this.packagePants = newPackagePants;
			this.packageHat = newPackageHat;
			this.packageBackpack = newPackageBackpack;
			this.packageVest = newPackageVest;
			this.packageMask = newPackageMask;
			this.packageGlasses = newPackageGlasses;
			this.primaryItem = newPrimaryItem;
			this.secondaryItem = newSecondaryItem;
			this.primaryState = newPrimaryState;
			this.secondaryState = newSecondaryState;
			this.face = newFace;
			this.hair = newHair;
			this.beard = newBeard;
			this.skin = newSkin;
			this.color = newColor;
			this.markerColor = newMarkerColor;
			this.hand = newHand;
			this.name = newName;
			this.nick = newNick;
			this.group = newGroup;
			this.skillset = newSkillset;
		}

		// Token: 0x060029F8 RID: 10744 RVA: 0x001048B8 File Offset: 0x00102CB8
		public void applyHero()
		{
			this.shirt = 0;
			this.pants = 0;
			this.hat = 0;
			this.backpack = 0;
			this.vest = 0;
			this.mask = 0;
			this.glasses = 0;
			this.primaryItem = 0;
			this.primaryState = new byte[0];
			this.secondaryItem = 0;
			this.secondaryState = new byte[0];
			for (int i = 0; i < PlayerInventory.SKILLSETS_HERO[(int)((byte)this.skillset)].Length; i++)
			{
				ushort id = PlayerInventory.SKILLSETS_HERO[(int)((byte)this.skillset)][i];
				ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, id);
				if (itemAsset != null)
				{
					switch (itemAsset.type)
					{
					case EItemType.HAT:
						this.hat = id;
						break;
					case EItemType.PANTS:
						this.pants = id;
						break;
					case EItemType.SHIRT:
						this.shirt = id;
						break;
					case EItemType.MASK:
						this.mask = id;
						break;
					case EItemType.BACKPACK:
						this.backpack = id;
						break;
					case EItemType.VEST:
						this.vest = id;
						break;
					case EItemType.GLASSES:
						this.glasses = id;
						break;
					case EItemType.GUN:
					case EItemType.MELEE:
						if (itemAsset.slot == ESlotType.PRIMARY)
						{
							this.primaryItem = id;
							this.primaryState = itemAsset.getState(EItemOrigin.ADMIN);
						}
						else
						{
							this.secondaryItem = id;
							this.secondaryState = itemAsset.getState(EItemOrigin.ADMIN);
						}
						break;
					}
				}
			}
		}

		// Token: 0x040019E1 RID: 6625
		public ushort shirt;

		// Token: 0x040019E2 RID: 6626
		public ushort pants;

		// Token: 0x040019E3 RID: 6627
		public ushort hat;

		// Token: 0x040019E4 RID: 6628
		public ushort backpack;

		// Token: 0x040019E5 RID: 6629
		public ushort vest;

		// Token: 0x040019E6 RID: 6630
		public ushort mask;

		// Token: 0x040019E7 RID: 6631
		public ushort glasses;

		// Token: 0x040019E8 RID: 6632
		public ulong packageShirt;

		// Token: 0x040019E9 RID: 6633
		public ulong packagePants;

		// Token: 0x040019EA RID: 6634
		public ulong packageHat;

		// Token: 0x040019EB RID: 6635
		public ulong packageBackpack;

		// Token: 0x040019EC RID: 6636
		public ulong packageVest;

		// Token: 0x040019ED RID: 6637
		public ulong packageMask;

		// Token: 0x040019EE RID: 6638
		public ulong packageGlasses;

		// Token: 0x040019EF RID: 6639
		public ushort primaryItem;

		// Token: 0x040019F0 RID: 6640
		public byte[] primaryState;

		// Token: 0x040019F1 RID: 6641
		public ushort secondaryItem;

		// Token: 0x040019F2 RID: 6642
		public byte[] secondaryState;

		// Token: 0x040019F3 RID: 6643
		public byte face;

		// Token: 0x040019F4 RID: 6644
		public byte hair;

		// Token: 0x040019F5 RID: 6645
		public byte beard;

		// Token: 0x040019F6 RID: 6646
		public Color skin;

		// Token: 0x040019F7 RID: 6647
		public Color color;

		// Token: 0x040019F8 RID: 6648
		public Color markerColor;

		// Token: 0x040019F9 RID: 6649
		public bool hand;

		// Token: 0x040019FA RID: 6650
		public string name;

		// Token: 0x040019FB RID: 6651
		public string nick;

		// Token: 0x040019FC RID: 6652
		public CSteamID group;

		// Token: 0x040019FD RID: 6653
		public EPlayerSkillset skillset;
	}
}
