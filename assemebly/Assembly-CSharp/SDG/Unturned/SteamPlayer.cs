using System;
using System.Collections.Generic;
using SDG.Provider;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000679 RID: 1657
	public class SteamPlayer
	{
		// Token: 0x0600304D RID: 12365 RVA: 0x0013E308 File Offset: 0x0013C708
		public SteamPlayer(SteamPlayerID newPlayerID, Transform newModel, bool newPro, bool newAdmin, int newChannel, byte newFace, byte newHair, byte newBeard, Color newSkin, Color newColor, Color newMarkerColor, bool newHand, int newShirtItem, int newPantsItem, int newHatItem, int newBackpackItem, int newVestItem, int newMaskItem, int newGlassesItem, int[] newSkinItems, string[] newSkinTags, string[] newSkinDynamicProps, EPlayerSkillset newSkillset, string newLanguage, CSteamID newLobbyID)
		{
			this._playerID = newPlayerID;
			this._model = newModel;
			this.model.name = this.playerID.characterName + " [" + this.playerID.playerName + "]";
			this.model.parent = LevelPlayers.models;
			this.model.GetComponent<SteamChannel>().id = newChannel;
			this.model.GetComponent<SteamChannel>().owner = this;
			this.model.GetComponent<SteamChannel>().isOwner = (this.playerID.steamID == Provider.client);
			this.model.GetComponent<SteamChannel>().setup();
			this._player = this.model.GetComponent<Player>();
			this._isPro = newPro;
			this._channel = newChannel;
			this.isAdmin = newAdmin;
			this.face = newFace;
			this._hair = newHair;
			this._beard = newBeard;
			this._skin = newSkin;
			this._color = newColor;
			this._markerColor = newMarkerColor;
			this._hand = newHand;
			this._skillset = newSkillset;
			this._language = newLanguage;
			this.shirtItem = newShirtItem;
			this.pantsItem = newPantsItem;
			this.hatItem = newHatItem;
			this.backpackItem = newBackpackItem;
			this.vestItem = newVestItem;
			this.maskItem = newMaskItem;
			this.glassesItem = newGlassesItem;
			this.skinItems = newSkinItems;
			this.skinTags = newSkinTags;
			this.skinDynamicProps = newSkinDynamicProps;
			this.itemSkins = new Dictionary<ushort, int>();
			this.vehicleSkins = new Dictionary<ushort, int>();
			this.modifiedItems = new HashSet<ushort>();
			for (int i = 0; i < this.skinItems.Length; i++)
			{
				int num = this.skinItems[i];
				if (num != 0)
				{
					ushort num2;
					ushort num3;
					Provider.provider.economyService.getInventoryTargetID(num, out num2, out num3);
					if (num2 > 0)
					{
						if (!this.itemSkins.ContainsKey(num2))
						{
							this.itemSkins.Add(num2, num);
						}
					}
					else if (num3 > 0 && !this.vehicleSkins.ContainsKey(num2))
					{
						this.vehicleSkins.Add(num3, num);
					}
				}
			}
			this.pings = new float[4];
			this.lastNet = Time.realtimeSinceStartup;
			this.lastChat = Time.realtimeSinceStartup;
			this.nextVote = Time.realtimeSinceStartup;
			this._joined = Time.realtimeSinceStartup;
			this.lobbyID = newLobbyID;
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x0600304E RID: 12366 RVA: 0x0013E572 File Offset: 0x0013C972
		public SteamPlayerID playerID
		{
			get
			{
				return this._playerID;
			}
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x0600304F RID: 12367 RVA: 0x0013E57A File Offset: 0x0013C97A
		public Transform model
		{
			get
			{
				return this._model;
			}
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06003050 RID: 12368 RVA: 0x0013E582 File Offset: 0x0013C982
		public Player player
		{
			get
			{
				return this._player;
			}
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06003051 RID: 12369 RVA: 0x0013E58A File Offset: 0x0013C98A
		public bool isPro
		{
			get
			{
				return (!OptionsSettings.streamer || !(this.playerID.steamID != Provider.user)) && this._isPro;
			}
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06003052 RID: 12370 RVA: 0x0013E5B8 File Offset: 0x0013C9B8
		public int channel
		{
			get
			{
				return this._channel;
			}
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06003053 RID: 12371 RVA: 0x0013E5C0 File Offset: 0x0013C9C0
		// (set) Token: 0x06003054 RID: 12372 RVA: 0x0013E5EE File Offset: 0x0013C9EE
		public bool isAdmin
		{
			get
			{
				return (!OptionsSettings.streamer || !(this.playerID.steamID != Provider.user)) && this._isAdmin;
			}
			set
			{
				this._isAdmin = value;
			}
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06003055 RID: 12373 RVA: 0x0013E5F7 File Offset: 0x0013C9F7
		public float ping
		{
			get
			{
				return this._ping;
			}
		}

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06003056 RID: 12374 RVA: 0x0013E5FF File Offset: 0x0013C9FF
		public float joined
		{
			get
			{
				return this._joined;
			}
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06003057 RID: 12375 RVA: 0x0013E607 File Offset: 0x0013CA07
		public byte hair
		{
			get
			{
				return this._hair;
			}
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06003058 RID: 12376 RVA: 0x0013E60F File Offset: 0x0013CA0F
		public byte beard
		{
			get
			{
				return this._beard;
			}
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06003059 RID: 12377 RVA: 0x0013E617 File Offset: 0x0013CA17
		public Color skin
		{
			get
			{
				return this._skin;
			}
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x0600305A RID: 12378 RVA: 0x0013E61F File Offset: 0x0013CA1F
		public Color color
		{
			get
			{
				return this._color;
			}
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x0600305B RID: 12379 RVA: 0x0013E627 File Offset: 0x0013CA27
		public Color markerColor
		{
			get
			{
				return this._markerColor;
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x0600305C RID: 12380 RVA: 0x0013E62F File Offset: 0x0013CA2F
		public bool hand
		{
			get
			{
				return this._hand;
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x0600305D RID: 12381 RVA: 0x0013E637 File Offset: 0x0013CA37
		public EPlayerSkillset skillset
		{
			get
			{
				return this._skillset;
			}
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x0600305E RID: 12382 RVA: 0x0013E63F File Offset: 0x0013CA3F
		public string language
		{
			get
			{
				return this._language;
			}
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x0600305F RID: 12383 RVA: 0x0013E647 File Offset: 0x0013CA47
		// (set) Token: 0x06003060 RID: 12384 RVA: 0x0013E64F File Offset: 0x0013CA4F
		public CSteamID lobbyID { get; private set; }

		// Token: 0x06003061 RID: 12385 RVA: 0x0013E658 File Offset: 0x0013CA58
		public bool getItemSkinItemDefID(ushort itemID, out int itemdefid)
		{
			itemdefid = 0;
			return this.itemSkins != null && this.itemSkins.TryGetValue(itemID, out itemdefid);
		}

		// Token: 0x06003062 RID: 12386 RVA: 0x0013E677 File Offset: 0x0013CA77
		public bool getVehicleSkinItemDefID(ushort vehicleID, out int itemdefid)
		{
			itemdefid = 0;
			return this.vehicleSkins != null && this.vehicleSkins.TryGetValue(vehicleID, out itemdefid);
		}

		// Token: 0x06003063 RID: 12387 RVA: 0x0013E698 File Offset: 0x0013CA98
		public bool getTagsAndDynamicPropsForItem(int item, out string tags, out string dynamic_props)
		{
			tags = string.Empty;
			dynamic_props = string.Empty;
			int i = 0;
			while (i < this.skinItems.Length)
			{
				if (this.skinItems[i] == item)
				{
					if (i < this.skinTags.Length && i < this.skinDynamicProps.Length)
					{
						tags = this.skinTags[i];
						dynamic_props = this.skinDynamicProps[i];
						return true;
					}
					return false;
				}
				else
				{
					i++;
				}
			}
			return false;
		}

		// Token: 0x06003064 RID: 12388 RVA: 0x0013E710 File Offset: 0x0013CB10
		public bool getStatTrackerValue(ushort itemID, out EStatTrackerType type, out int kills)
		{
			type = EStatTrackerType.NONE;
			kills = -1;
			int item;
			if (!this.getItemSkinItemDefID(itemID, out item))
			{
				return false;
			}
			string tags;
			string dynamic_props;
			if (!this.getTagsAndDynamicPropsForItem(item, out tags, out dynamic_props))
			{
				return false;
			}
			DynamicEconDetails dynamicEconDetails = new DynamicEconDetails(tags, dynamic_props);
			return dynamicEconDetails.getStatTrackerValue(out type, out kills);
		}

		// Token: 0x06003065 RID: 12389 RVA: 0x0013E758 File Offset: 0x0013CB58
		public void incrementStatTrackerValue(ushort itemID, EPlayerStat stat)
		{
			int num;
			if (!this.getItemSkinItemDefID(itemID, out num))
			{
				return;
			}
			string tags;
			string dynamic_props;
			if (!this.getTagsAndDynamicPropsForItem(num, out tags, out dynamic_props))
			{
				return;
			}
			DynamicEconDetails dynamicEconDetails = new DynamicEconDetails(tags, dynamic_props);
			EStatTrackerType estatTrackerType;
			int num2;
			if (!dynamicEconDetails.getStatTrackerValue(out estatTrackerType, out num2))
			{
				return;
			}
			if (estatTrackerType != EStatTrackerType.TOTAL)
			{
				if (estatTrackerType != EStatTrackerType.PLAYER)
				{
					return;
				}
				if (stat != EPlayerStat.KILLS_PLAYERS)
				{
					return;
				}
			}
			else if (stat != EPlayerStat.KILLS_ANIMALS && stat != EPlayerStat.KILLS_PLAYERS && stat != EPlayerStat.KILLS_ZOMBIES_MEGA && stat != EPlayerStat.KILLS_ZOMBIES_NORMAL)
			{
				return;
			}
			if (!this.modifiedItems.Contains(itemID))
			{
				this.modifiedItems.Add(itemID);
			}
			num2++;
			for (int i = 0; i < this.skinItems.Length; i++)
			{
				if (this.skinItems[i] == num)
				{
					if (i < this.skinDynamicProps.Length)
					{
						this.skinDynamicProps[i] = dynamicEconDetails.getPredictedDynamicPropsJsonForStatTracker(estatTrackerType, num2);
					}
					break;
				}
			}
		}

		// Token: 0x06003066 RID: 12390 RVA: 0x0013E85C File Offset: 0x0013CC5C
		public void commitModifiedDynamicProps()
		{
			if (this.modifiedItems.Count < 1)
			{
				return;
			}
			SteamInventoryUpdateHandle_t handle = SteamInventory.StartUpdateProperties();
			foreach (ushort itemID in this.modifiedItems)
			{
				ulong value;
				if (Characters.getPackageForItemID(itemID, out value))
				{
					EStatTrackerType type;
					int num;
					if (this.getStatTrackerValue(itemID, out type, out num))
					{
						string statTrackerPropertyName = Provider.provider.economyService.getStatTrackerPropertyName(type);
						if (!string.IsNullOrEmpty(statTrackerPropertyName))
						{
							SteamInventory.SetProperty(handle, new SteamItemInstanceID_t(value), statTrackerPropertyName, (long)num);
						}
					}
				}
			}
			SteamInventory.SubmitUpdateProperties(handle, out Provider.provider.economyService.commitResult);
		}

		// Token: 0x06003067 RID: 12391 RVA: 0x0013E93C File Offset: 0x0013CD3C
		public void lag(float value)
		{
			this._ping = value;
			for (int i = this.pings.Length - 1; i > 0; i--)
			{
				this.pings[i] = this.pings[i - 1];
				if (this.pings[i] > 0.001f)
				{
					this._ping += this.pings[i];
				}
			}
			this._ping /= (float)this.pings.Length;
			this.pings[0] = value;
		}

		// Token: 0x04001FD4 RID: 8148
		private SteamPlayerID _playerID;

		// Token: 0x04001FD5 RID: 8149
		private Transform _model;

		// Token: 0x04001FD6 RID: 8150
		private Player _player;

		// Token: 0x04001FD7 RID: 8151
		private bool _isPro;

		// Token: 0x04001FD8 RID: 8152
		private int _channel;

		// Token: 0x04001FD9 RID: 8153
		private bool _isAdmin;

		// Token: 0x04001FDA RID: 8154
		private float[] pings;

		// Token: 0x04001FDB RID: 8155
		private float _ping;

		// Token: 0x04001FDC RID: 8156
		private float _joined;

		// Token: 0x04001FDD RID: 8157
		public byte face;

		// Token: 0x04001FDE RID: 8158
		private byte _hair;

		// Token: 0x04001FDF RID: 8159
		private byte _beard;

		// Token: 0x04001FE0 RID: 8160
		private Color _skin;

		// Token: 0x04001FE1 RID: 8161
		private Color _color;

		// Token: 0x04001FE2 RID: 8162
		private Color _markerColor;

		// Token: 0x04001FE3 RID: 8163
		private bool _hand;

		// Token: 0x04001FE4 RID: 8164
		public int shirtItem;

		// Token: 0x04001FE5 RID: 8165
		public int pantsItem;

		// Token: 0x04001FE6 RID: 8166
		public int hatItem;

		// Token: 0x04001FE7 RID: 8167
		public int backpackItem;

		// Token: 0x04001FE8 RID: 8168
		public int vestItem;

		// Token: 0x04001FE9 RID: 8169
		public int maskItem;

		// Token: 0x04001FEA RID: 8170
		public int glassesItem;

		// Token: 0x04001FEB RID: 8171
		public int[] skinItems;

		// Token: 0x04001FEC RID: 8172
		public string[] skinTags;

		// Token: 0x04001FED RID: 8173
		public string[] skinDynamicProps;

		// Token: 0x04001FEE RID: 8174
		public Dictionary<ushort, int> itemSkins;

		// Token: 0x04001FEF RID: 8175
		public Dictionary<ushort, int> vehicleSkins;

		// Token: 0x04001FF0 RID: 8176
		public HashSet<ushort> modifiedItems;

		// Token: 0x04001FF1 RID: 8177
		private EPlayerSkillset _skillset;

		// Token: 0x04001FF2 RID: 8178
		private string _language;

		// Token: 0x04001FF4 RID: 8180
		public float lastNet;

		// Token: 0x04001FF5 RID: 8181
		public float lastPing;

		// Token: 0x04001FF6 RID: 8182
		public float lastChat;

		// Token: 0x04001FF7 RID: 8183
		public float nextVote;

		// Token: 0x04001FF8 RID: 8184
		public bool isMuted;

		// Token: 0x04001FF9 RID: 8185
		public float rpcCredits;

		// Token: 0x04001FFA RID: 8186
		public bool rpcHasAskedVehicles;
	}
}
