using System;
using System.Collections.Generic;
using SDG.Provider;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000677 RID: 1655
	public class SteamPending
	{
		// Token: 0x06003039 RID: 12345 RVA: 0x0013DED8 File Offset: 0x0013C2D8
		public SteamPending(SteamPlayerID newPlayerID, bool newPro, byte newFace, byte newHair, byte newBeard, Color newSkin, Color newColor, Color newMarkerColor, bool newHand, ulong newPackageShirt, ulong newPackagePants, ulong newPackageHat, ulong newPackageBackpack, ulong newPackageVest, ulong newPackageMask, ulong newPackageGlasses, ulong[] newPackageSkins, EPlayerSkillset newSkillset, string newLanguage, CSteamID newLobbyID)
		{
			this._playerID = newPlayerID;
			this._isPro = newPro;
			this._face = newFace;
			this._hair = newHair;
			this._beard = newBeard;
			this._skin = newSkin;
			this._color = newColor;
			this._markerColor = newMarkerColor;
			this._hand = newHand;
			this._skillset = newSkillset;
			this._language = newLanguage;
			this.packageShirt = newPackageShirt;
			this.packagePants = newPackagePants;
			this.packageHat = newPackageHat;
			this.packageBackpack = newPackageBackpack;
			this.packageVest = newPackageVest;
			this.packageMask = newPackageMask;
			this.packageGlasses = newPackageGlasses;
			this.packageSkins = newPackageSkins;
			this.lastNet = Time.realtimeSinceStartup;
			this.lastActive = -1f;
			this.guidTableIndex = 0;
			this.lobbyID = newLobbyID;
		}

		// Token: 0x0600303A RID: 12346 RVA: 0x0013DFBC File Offset: 0x0013C3BC
		public SteamPending()
		{
			this._playerID = new SteamPlayerID(CSteamID.Nil, 0, "Player Name", "Character Name", "Nick Name", CSteamID.Nil);
			this.lastNet = Time.realtimeSinceStartup;
			this.lastActive = -1f;
			this.guidTableIndex = 0;
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x0600303B RID: 12347 RVA: 0x0013E027 File Offset: 0x0013C427
		public SteamPlayerID playerID
		{
			get
			{
				return this._playerID;
			}
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x0600303C RID: 12348 RVA: 0x0013E02F File Offset: 0x0013C42F
		public bool isPro
		{
			get
			{
				return this._isPro;
			}
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x0600303D RID: 12349 RVA: 0x0013E037 File Offset: 0x0013C437
		public byte face
		{
			get
			{
				return this._face;
			}
		}

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x0600303E RID: 12350 RVA: 0x0013E03F File Offset: 0x0013C43F
		public byte hair
		{
			get
			{
				return this._hair;
			}
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x0600303F RID: 12351 RVA: 0x0013E047 File Offset: 0x0013C447
		public byte beard
		{
			get
			{
				return this._beard;
			}
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06003040 RID: 12352 RVA: 0x0013E04F File Offset: 0x0013C44F
		public Color skin
		{
			get
			{
				return this._skin;
			}
		}

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06003041 RID: 12353 RVA: 0x0013E057 File Offset: 0x0013C457
		public Color color
		{
			get
			{
				return this._color;
			}
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06003042 RID: 12354 RVA: 0x0013E05F File Offset: 0x0013C45F
		public Color markerColor
		{
			get
			{
				return this._markerColor;
			}
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06003043 RID: 12355 RVA: 0x0013E067 File Offset: 0x0013C467
		public bool hand
		{
			get
			{
				return this._hand;
			}
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x06003044 RID: 12356 RVA: 0x0013E06F File Offset: 0x0013C46F
		public bool canAcceptYet
		{
			get
			{
				return this.hasAuthentication && this.hasProof && this.hasGroup;
			}
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06003045 RID: 12357 RVA: 0x0013E090 File Offset: 0x0013C490
		public EPlayerSkillset skillset
		{
			get
			{
				return this._skillset;
			}
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06003046 RID: 12358 RVA: 0x0013E098 File Offset: 0x0013C498
		public string language
		{
			get
			{
				return this._language;
			}
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06003047 RID: 12359 RVA: 0x0013E0A0 File Offset: 0x0013C4A0
		// (set) Token: 0x06003048 RID: 12360 RVA: 0x0013E0A8 File Offset: 0x0013C4A8
		public CSteamID lobbyID { get; private set; }

		// Token: 0x06003049 RID: 12361 RVA: 0x0013E0B4 File Offset: 0x0013C4B4
		public void inventoryDetailsReady()
		{
			this.shirtItem = this.getInventoryItem(this.packageShirt);
			this.pantsItem = this.getInventoryItem(this.packagePants);
			this.hatItem = this.getInventoryItem(this.packageHat);
			this.backpackItem = this.getInventoryItem(this.packageBackpack);
			this.vestItem = this.getInventoryItem(this.packageVest);
			this.maskItem = this.getInventoryItem(this.packageMask);
			this.glassesItem = this.getInventoryItem(this.packageGlasses);
			List<int> list = new List<int>();
			List<string> list2 = new List<string>();
			List<string> list3 = new List<string>();
			for (int i = 0; i < this.packageSkins.Length; i++)
			{
				ulong num = this.packageSkins[i];
				if (num != 0UL)
				{
					int inventoryItem = this.getInventoryItem(num);
					if (inventoryItem != 0)
					{
						list.Add(inventoryItem);
						DynamicEconDetails dynamicEconDetails;
						if (this.dynamicInventoryDetails.TryGetValue(num, out dynamicEconDetails))
						{
							list2.Add(dynamicEconDetails.tags);
							list3.Add(dynamicEconDetails.dynamic_props);
						}
						else
						{
							list2.Add(string.Empty);
							list3.Add(string.Empty);
						}
					}
				}
			}
			this.skinItems = list.ToArray();
			this.skinTags = list2.ToArray();
			this.skinDynamicProps = list3.ToArray();
			this.hasProof = true;
			if (this.canAcceptYet)
			{
				Provider.sendGUIDTable(this);
			}
		}

		// Token: 0x0600304A RID: 12362 RVA: 0x0013E21C File Offset: 0x0013C61C
		public int getInventoryItem(ulong package)
		{
			if (this.inventoryDetails != null)
			{
				for (int i = 0; i < this.inventoryDetails.Length; i++)
				{
					if (this.inventoryDetails[i].m_itemId.m_SteamItemInstanceID == package)
					{
						return this.inventoryDetails[i].m_iDefinition.m_SteamItemDef;
					}
				}
			}
			return 0;
		}

		// Token: 0x04001FAB RID: 8107
		private SteamPlayerID _playerID;

		// Token: 0x04001FAC RID: 8108
		private bool _isPro;

		// Token: 0x04001FAD RID: 8109
		private byte _face;

		// Token: 0x04001FAE RID: 8110
		private byte _hair;

		// Token: 0x04001FAF RID: 8111
		private byte _beard;

		// Token: 0x04001FB0 RID: 8112
		private Color _skin;

		// Token: 0x04001FB1 RID: 8113
		private Color _color;

		// Token: 0x04001FB2 RID: 8114
		private Color _markerColor;

		// Token: 0x04001FB3 RID: 8115
		private bool _hand;

		// Token: 0x04001FB4 RID: 8116
		public int shirtItem;

		// Token: 0x04001FB5 RID: 8117
		public int pantsItem;

		// Token: 0x04001FB6 RID: 8118
		public int hatItem;

		// Token: 0x04001FB7 RID: 8119
		public int backpackItem;

		// Token: 0x04001FB8 RID: 8120
		public int vestItem;

		// Token: 0x04001FB9 RID: 8121
		public int maskItem;

		// Token: 0x04001FBA RID: 8122
		public int glassesItem;

		// Token: 0x04001FBB RID: 8123
		public int[] skinItems;

		// Token: 0x04001FBC RID: 8124
		public string[] skinTags;

		// Token: 0x04001FBD RID: 8125
		public string[] skinDynamicProps;

		// Token: 0x04001FBE RID: 8126
		public ulong packageShirt;

		// Token: 0x04001FBF RID: 8127
		public ulong packagePants;

		// Token: 0x04001FC0 RID: 8128
		public ulong packageHat;

		// Token: 0x04001FC1 RID: 8129
		public ulong packageBackpack;

		// Token: 0x04001FC2 RID: 8130
		public ulong packageVest;

		// Token: 0x04001FC3 RID: 8131
		public ulong packageMask;

		// Token: 0x04001FC4 RID: 8132
		public ulong packageGlasses;

		// Token: 0x04001FC5 RID: 8133
		public ulong[] packageSkins;

		// Token: 0x04001FC6 RID: 8134
		public SteamInventoryResult_t inventoryResult = SteamInventoryResult_t.Invalid;

		// Token: 0x04001FC7 RID: 8135
		public SteamItemDetails_t[] inventoryDetails;

		// Token: 0x04001FC8 RID: 8136
		public Dictionary<ulong, DynamicEconDetails> dynamicInventoryDetails = new Dictionary<ulong, DynamicEconDetails>();

		// Token: 0x04001FC9 RID: 8137
		public bool assignedPro;

		// Token: 0x04001FCA RID: 8138
		public bool assignedAdmin;

		// Token: 0x04001FCB RID: 8139
		public bool hasAuthentication;

		// Token: 0x04001FCC RID: 8140
		public bool hasProof;

		// Token: 0x04001FCD RID: 8141
		public bool hasGroup;

		// Token: 0x04001FCE RID: 8142
		private EPlayerSkillset _skillset;

		// Token: 0x04001FCF RID: 8143
		private string _language;

		// Token: 0x04001FD0 RID: 8144
		public float lastNet;

		// Token: 0x04001FD1 RID: 8145
		public float lastActive;

		// Token: 0x04001FD2 RID: 8146
		public ushort guidTableIndex;
	}
}
