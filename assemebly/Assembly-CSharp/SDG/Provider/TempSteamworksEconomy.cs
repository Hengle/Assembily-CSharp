using System;
using System.Collections.Generic;
using System.Globalization;
using SDG.Framework.Debug;
using SDG.Framework.IO.Deserialization;
using SDG.SteamworksProvider;
using SDG.Unturned;
using Steamworks;
using UnityEngine;

namespace SDG.Provider
{
	// Token: 0x02000349 RID: 841
	public class TempSteamworksEconomy
	{
		// Token: 0x0600172C RID: 5932 RVA: 0x0008596C File Offset: 0x00083D6C
		public TempSteamworksEconomy(SteamworksAppInfo newAppInfo)
		{
			this.appInfo = newAppInfo;
			TempSteamworksEconomy.econInfo = new List<UnturnedEconInfo>();
			IDeserializer deserializer = new JSONDeserializer();
			TempSteamworksEconomy.econInfo = deserializer.deserialize<List<UnturnedEconInfo>>(ReadWrite.PATH + "/EconInfo.json");
			if (this.appInfo.isDedicated)
			{
				this.inventoryResultReady = Callback<SteamInventoryResultReady_t>.CreateGameServer(new Callback<SteamInventoryResultReady_t>.DispatchDelegate(this.onInventoryResultReady));
			}
			else
			{
				this.inventoryResultReady = Callback<SteamInventoryResultReady_t>.Create(new Callback<SteamInventoryResultReady_t>.DispatchDelegate(this.onInventoryResultReady));
				this.gameOverlayActivated = Callback<GameOverlayActivated_t>.Create(new Callback<GameOverlayActivated_t>.DispatchDelegate(this.onGameOverlayActivated));
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x0600172D RID: 5933 RVA: 0x00085A63 File Offset: 0x00083E63
		public bool canOpenInventory
		{
			get
			{
				return SteamUtils.IsOverlayEnabled();
			}
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x00085A6C File Offset: 0x00083E6C
		public void open(ulong id)
		{
			SteamFriends.ActivateGameOverlayToWebPage(string.Concat(new object[]
			{
				"http://steamcommunity.com/profiles/",
				SteamUser.GetSteamID(),
				"/inventory/?sellOnLoad=1#",
				SteamUtils.GetAppID(),
				"_2_",
				id
			}));
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x0600172F RID: 5935 RVA: 0x00085AC4 File Offset: 0x00083EC4
		// (set) Token: 0x06001730 RID: 5936 RVA: 0x00085ACB File Offset: 0x00083ECB
		public static List<UnturnedEconInfo> econInfo { get; private set; }

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06001731 RID: 5937 RVA: 0x00085AD3 File Offset: 0x00083ED3
		public SteamItemDetails_t[] inventory
		{
			get
			{
				return this.inventoryDetails;
			}
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x00085ADC File Offset: 0x00083EDC
		public ulong getInventoryPackage(int item)
		{
			if (this.inventoryDetails != null)
			{
				for (int i = 0; i < this.inventoryDetails.Length; i++)
				{
					if (this.inventoryDetails[i].m_iDefinition.m_SteamItemDef == item)
					{
						return this.inventoryDetails[i].m_itemId.m_SteamItemInstanceID;
					}
				}
			}
			return 0UL;
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x00085B44 File Offset: 0x00083F44
		public int countInventoryPackages(int item)
		{
			int num = 0;
			if (this.inventoryDetails != null)
			{
				for (int i = 0; i < this.inventoryDetails.Length; i++)
				{
					if (this.inventoryDetails[i].m_iDefinition.m_SteamItemDef == item)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x00085B98 File Offset: 0x00083F98
		public bool getInventoryPackages(int item, ulong[] instances)
		{
			int num = 0;
			if (this.inventoryDetails != null)
			{
				for (int i = 0; i < this.inventoryDetails.Length; i++)
				{
					if (this.inventoryDetails[i].m_iDefinition.m_SteamItemDef == item)
					{
						instances[num] = this.inventoryDetails[i].m_itemId.m_SteamItemInstanceID;
						num++;
						if (num == instances.Length)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x00085C10 File Offset: 0x00084010
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

		// Token: 0x06001736 RID: 5942 RVA: 0x00085C78 File Offset: 0x00084078
		public string getInventoryTags(ulong instance)
		{
			DynamicEconDetails dynamicEconDetails;
			if (!this.dynamicInventoryDetails.TryGetValue(instance, out dynamicEconDetails))
			{
				return string.Empty;
			}
			return dynamicEconDetails.tags;
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x00085CA8 File Offset: 0x000840A8
		public string getInventoryDynamicProps(ulong instance)
		{
			DynamicEconDetails dynamicEconDetails;
			if (!this.dynamicInventoryDetails.TryGetValue(instance, out dynamicEconDetails))
			{
				return string.Empty;
			}
			return dynamicEconDetails.dynamic_props;
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x00085CD8 File Offset: 0x000840D8
		public bool getInventoryStatTrackerValue(ulong instance, out EStatTrackerType type, out int kills)
		{
			type = EStatTrackerType.NONE;
			kills = -1;
			DynamicEconDetails dynamicEconDetails;
			return this.dynamicInventoryDetails.TryGetValue(instance, out dynamicEconDetails) && dynamicEconDetails.getStatTrackerValue(out type, out kills);
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x00085D0C File Offset: 0x0008410C
		public string getInventoryName(int item)
		{
			UnturnedEconInfo unturnedEconInfo = TempSteamworksEconomy.econInfo.Find((UnturnedEconInfo x) => x.itemdefid == item);
			if (unturnedEconInfo == null)
			{
				return string.Empty;
			}
			return unturnedEconInfo.name;
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x00085D50 File Offset: 0x00084150
		public string getInventoryType(int item)
		{
			UnturnedEconInfo unturnedEconInfo = TempSteamworksEconomy.econInfo.Find((UnturnedEconInfo x) => x.itemdefid == item);
			if (unturnedEconInfo == null)
			{
				return string.Empty;
			}
			return unturnedEconInfo.type;
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x00085D94 File Offset: 0x00084194
		public string getInventoryDescription(int item)
		{
			UnturnedEconInfo unturnedEconInfo = TempSteamworksEconomy.econInfo.Find((UnturnedEconInfo x) => x.itemdefid == item);
			if (unturnedEconInfo == null)
			{
				return string.Empty;
			}
			return unturnedEconInfo.description;
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x00085DD8 File Offset: 0x000841D8
		public bool getInventoryMarketable(int item)
		{
			UnturnedEconInfo unturnedEconInfo = TempSteamworksEconomy.econInfo.Find((UnturnedEconInfo x) => x.itemdefid == item);
			return unturnedEconInfo != null && unturnedEconInfo.marketable;
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x00085E18 File Offset: 0x00084218
		public bool getInventoryScrapable(int item)
		{
			UnturnedEconInfo unturnedEconInfo = TempSteamworksEconomy.econInfo.Find((UnturnedEconInfo x) => x.itemdefid == item);
			return unturnedEconInfo != null && unturnedEconInfo.scrapable;
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x00085E58 File Offset: 0x00084258
		public Color getInventoryColor(int item)
		{
			UnturnedEconInfo unturnedEconInfo = TempSteamworksEconomy.econInfo.Find((UnturnedEconInfo x) => x.itemdefid == item);
			if (unturnedEconInfo == null)
			{
				return Color.white;
			}
			uint num;
			if (unturnedEconInfo.name_color != null && unturnedEconInfo.name_color.Length > 0 && uint.TryParse(unturnedEconInfo.name_color, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out num))
			{
				uint num2 = num >> 16 & 255u;
				uint num3 = num >> 8 & 255u;
				uint num4 = num & 255u;
				return new Color(num2 / 255f, num3 / 255f, num4 / 255f);
			}
			return Color.white;
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x00085F14 File Offset: 0x00084314
		public Color getStatTrackerColor(EStatTrackerType type)
		{
			if (type == EStatTrackerType.NONE)
			{
				return Color.white;
			}
			if (type == EStatTrackerType.TOTAL)
			{
				return new Color(1f, 0.5f, 0f);
			}
			if (type != EStatTrackerType.PLAYER)
			{
				return Color.black;
			}
			return new Color(1f, 0f, 0f);
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x00085F6F File Offset: 0x0008436F
		public string getStatTrackerPropertyName(EStatTrackerType type)
		{
			if (type == EStatTrackerType.TOTAL)
			{
				return "total_kills";
			}
			if (type != EStatTrackerType.PLAYER)
			{
				return null;
			}
			return "player_kills";
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x00085F94 File Offset: 0x00084394
		public ushort getInventoryMythicID(int item)
		{
			UnturnedEconInfo unturnedEconInfo = TempSteamworksEconomy.econInfo.Find((UnturnedEconInfo x) => x.itemdefid == item);
			if (unturnedEconInfo == null)
			{
				return 0;
			}
			return (ushort)unturnedEconInfo.item_effect;
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x00085FD4 File Offset: 0x000843D4
		public void getInventoryTargetID(int item, out ushort item_id, out ushort vehicle_id)
		{
			UnturnedEconInfo unturnedEconInfo = TempSteamworksEconomy.econInfo.Find((UnturnedEconInfo x) => x.itemdefid == item);
			if (unturnedEconInfo == null)
			{
				item_id = 0;
				vehicle_id = 0;
				return;
			}
			item_id = (ushort)unturnedEconInfo.item_id;
			vehicle_id = (ushort)unturnedEconInfo.vehicle_id;
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x00086024 File Offset: 0x00084424
		public ushort getInventoryItemID(int item)
		{
			ushort result;
			ushort num;
			this.getInventoryTargetID(item, out result, out num);
			return result;
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x00086040 File Offset: 0x00084440
		public ushort getInventorySkinID(int item)
		{
			UnturnedEconInfo unturnedEconInfo = TempSteamworksEconomy.econInfo.Find((UnturnedEconInfo x) => x.itemdefid == item);
			if (unturnedEconInfo == null)
			{
				return 0;
			}
			return (ushort)unturnedEconInfo.item_skin;
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x00086080 File Offset: 0x00084480
		public void consumeItem(ulong instance)
		{
			Terminal.print("Consume item: " + instance, null, Provider.STEAM_IC, Provider.STEAM_DC, true);
			SteamInventoryResult_t steamInventoryResult_t;
			SteamInventory.ConsumeItem(out steamInventoryResult_t, (SteamItemInstanceID_t)instance, 1u);
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x000860C0 File Offset: 0x000844C0
		public void exchangeInventory(int generate, params ulong[] destroy)
		{
			Debug.Log("Exchange these item instances for " + generate);
			foreach (ulong num in destroy)
			{
				int inventoryItem = this.getInventoryItem(num);
				Debug.Log(string.Concat(new object[]
				{
					inventoryItem,
					" [",
					num,
					"]"
				}));
			}
			SteamItemDef_t[] array = new SteamItemDef_t[1];
			uint[] array2 = new uint[1];
			array[0] = (SteamItemDef_t)generate;
			array2[0] = 1u;
			SteamItemInstanceID_t[] array3 = new SteamItemInstanceID_t[destroy.Length];
			uint[] array4 = new uint[destroy.Length];
			for (int j = 0; j < destroy.Length; j++)
			{
				array3[j] = (SteamItemInstanceID_t)destroy[j];
				array4[j] = 1u;
			}
			SteamInventory.ExchangeItems(out this.exchangeResult, array, array2, (uint)array.Length, array3, array4, (uint)array3.Length);
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x000861BD File Offset: 0x000845BD
		public void updateInventory()
		{
			if (Time.realtimeSinceStartup - this.lastHeartbeat < 30f)
			{
				return;
			}
			this.lastHeartbeat = Time.realtimeSinceStartup;
			SteamInventory.SendItemDropHeartbeat();
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x000861E6 File Offset: 0x000845E6
		public void dropInventory()
		{
			SteamInventory.TriggerItemDrop(out this.dropResult, (SteamItemDef_t)10000);
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x000861FE File Offset: 0x000845FE
		public void refreshInventory()
		{
			if (!SteamInventory.GetAllItems(out this.inventoryResult))
			{
				Provider.isLoadingInventory = false;
			}
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x00086218 File Offset: 0x00084618
		public void addLocalItem(SteamItemDetails_t item, string tags, string dynamic_props)
		{
			SteamItemDetails_t[] array = new SteamItemDetails_t[this.inventory.Length + 1];
			for (int i = 0; i < this.inventory.Length; i++)
			{
				array[i] = this.inventory[i];
			}
			array[this.inventory.Length] = item;
			this.inventoryDetails = array;
			this.dynamicInventoryDetails.Remove(item.m_itemId.m_SteamItemInstanceID);
			DynamicEconDetails value = default(DynamicEconDetails);
			value.tags = ((!string.IsNullOrEmpty(tags)) ? tags : string.Empty);
			value.dynamic_props = ((!string.IsNullOrEmpty(dynamic_props)) ? dynamic_props : string.Empty);
			this.dynamicInventoryDetails.Add(item.m_itemId.m_SteamItemInstanceID, value);
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x000862F8 File Offset: 0x000846F8
		private void onInventoryResultReady(SteamInventoryResultReady_t callback)
		{
			if (this.appInfo.isDedicated)
			{
				SteamPending steamPending = null;
				for (int i = 0; i < Provider.pending.Count; i++)
				{
					if (Provider.pending[i].inventoryResult == callback.m_handle)
					{
						steamPending = Provider.pending[i];
						break;
					}
				}
				if (steamPending == null)
				{
					return;
				}
				if (callback.m_result != EResult.k_EResultOK || !SteamGameServerInventory.CheckResultSteamID(callback.m_handle, steamPending.playerID.steamID))
				{
					Debug.Log(string.Concat(new object[]
					{
						"inventory auth: ",
						callback.m_result,
						" ",
						SteamGameServerInventory.CheckResultSteamID(callback.m_handle, steamPending.playerID.steamID)
					}));
					Provider.reject(steamPending.playerID.steamID, ESteamRejection.AUTH_ECON_VERIFY);
					return;
				}
				uint num = 0u;
				if (SteamGameServerInventory.GetResultItems(callback.m_handle, null, ref num) && num > 0u)
				{
					steamPending.inventoryDetails = new SteamItemDetails_t[num];
					SteamGameServerInventory.GetResultItems(callback.m_handle, steamPending.inventoryDetails, ref num);
					for (uint num2 = 0u; num2 < num; num2 += 1u)
					{
						uint num3 = 1024u;
						string text;
						SteamGameServerInventory.GetResultItemProperty(callback.m_handle, num2, "tags", out text, ref num3);
						uint num4 = 1024u;
						string text2;
						SteamGameServerInventory.GetResultItemProperty(callback.m_handle, num2, "dynamic_props", out text2, ref num4);
						DynamicEconDetails value = default(DynamicEconDetails);
						value.tags = ((!string.IsNullOrEmpty(text)) ? text : string.Empty);
						value.dynamic_props = ((!string.IsNullOrEmpty(text2)) ? text2 : string.Empty);
						steamPending.dynamicInventoryDetails.Add(steamPending.inventoryDetails[(int)((UIntPtr)num2)].m_itemId.m_SteamItemInstanceID, value);
					}
				}
				steamPending.inventoryDetailsReady();
			}
			else if (this.promoResult != SteamInventoryResult_t.Invalid && callback.m_handle == this.promoResult)
			{
				SteamInventory.DestroyResult(this.promoResult);
				this.promoResult = SteamInventoryResult_t.Invalid;
			}
			else if (this.exchangeResult != SteamInventoryResult_t.Invalid && callback.m_handle == this.exchangeResult)
			{
				SteamItemDetails_t[] array = null;
				string tags = null;
				string dynamic_props = null;
				uint num5 = 0u;
				if (SteamInventory.GetResultItems(this.exchangeResult, null, ref num5) && num5 > 0u)
				{
					array = new SteamItemDetails_t[num5];
					SteamInventory.GetResultItems(this.exchangeResult, array, ref num5);
					uint num6 = 1024u;
					SteamInventory.GetResultItemProperty(this.exchangeResult, num5 - 1u, "tags", out tags, ref num6);
					uint num7 = 1024u;
					SteamInventory.GetResultItemProperty(this.exchangeResult, num5 - 1u, "dynamic_props", out dynamic_props, ref num7);
				}
				Terminal.print("onInventoryResultReady: Exchange " + num5, null, Provider.STEAM_IC, Provider.STEAM_DC, true);
				if (array != null && num5 > 0u)
				{
					SteamItemDetails_t item = array[(int)((UIntPtr)(num5 - 1u))];
					this.addLocalItem(item, tags, dynamic_props);
					if (this.onInventoryExchanged != null)
					{
						this.onInventoryExchanged(item.m_iDefinition.m_SteamItemDef, item.m_unQuantity, item.m_itemId.m_SteamItemInstanceID);
					}
					this.refreshInventory();
				}
				SteamInventory.DestroyResult(this.exchangeResult);
				this.exchangeResult = SteamInventoryResult_t.Invalid;
			}
			else if (this.dropResult != SteamInventoryResult_t.Invalid && callback.m_handle == this.dropResult)
			{
				SteamItemDetails_t[] array2 = null;
				string tags2 = null;
				string dynamic_props2 = null;
				uint num8 = 0u;
				if (SteamInventory.GetResultItems(this.dropResult, null, ref num8) && num8 > 0u)
				{
					array2 = new SteamItemDetails_t[num8];
					SteamInventory.GetResultItems(this.dropResult, array2, ref num8);
					uint num9 = 1024u;
					SteamInventory.GetResultItemProperty(this.dropResult, 0u, "tags", out tags2, ref num9);
					uint num10 = 1024u;
					SteamInventory.GetResultItemProperty(this.dropResult, 0u, "dynamic_props", out dynamic_props2, ref num10);
				}
				Terminal.print("onInventoryResultReady: Drop " + num8, null, Provider.STEAM_IC, Provider.STEAM_DC, true);
				if (array2 != null && num8 > 0u)
				{
					SteamItemDetails_t item2 = array2[0];
					this.addLocalItem(item2, tags2, dynamic_props2);
					if (this.onInventoryDropped != null)
					{
						this.onInventoryDropped(item2.m_iDefinition.m_SteamItemDef, item2.m_unQuantity, item2.m_itemId.m_SteamItemInstanceID);
					}
					this.refreshInventory();
				}
				SteamInventory.DestroyResult(this.dropResult);
				this.dropResult = SteamInventoryResult_t.Invalid;
			}
			else if (this.inventoryResult != SteamInventoryResult_t.Invalid && callback.m_handle == this.inventoryResult)
			{
				this.dynamicInventoryDetails.Clear();
				uint num11 = 0u;
				if (SteamInventory.GetResultItems(this.inventoryResult, null, ref num11) && num11 > 0u)
				{
					this.inventoryDetails = new SteamItemDetails_t[num11];
					SteamInventory.GetResultItems(this.inventoryResult, this.inventoryDetails, ref num11);
					for (uint num12 = 0u; num12 < num11; num12 += 1u)
					{
						uint num13 = 1024u;
						string text3;
						SteamInventory.GetResultItemProperty(this.inventoryResult, num12, "tags", out text3, ref num13);
						uint num14 = 1024u;
						string text4;
						SteamInventory.GetResultItemProperty(this.inventoryResult, num12, "dynamic_props", out text4, ref num14);
						DynamicEconDetails value2 = default(DynamicEconDetails);
						value2.tags = ((!string.IsNullOrEmpty(text3)) ? text3 : string.Empty);
						value2.dynamic_props = ((!string.IsNullOrEmpty(text4)) ? text4 : string.Empty);
						this.dynamicInventoryDetails.Add(this.inventoryDetails[(int)((UIntPtr)num12)].m_itemId.m_SteamItemInstanceID, value2);
					}
				}
				if (this.onInventoryRefreshed != null)
				{
					this.onInventoryRefreshed();
				}
				this.isInventoryAvailable = true;
				Provider.isLoadingInventory = false;
				SteamInventory.DestroyResult(this.inventoryResult);
				this.inventoryResult = SteamInventoryResult_t.Invalid;
			}
			else if (this.commitResult != SteamInventoryResult_t.Invalid && callback.m_handle == this.commitResult)
			{
				Debug.Log("Commit dynamic properties result: " + callback.m_result);
				SteamInventory.DestroyResult(this.commitResult);
				this.commitResult = SteamInventoryResult_t.Invalid;
			}
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x000869AA File Offset: 0x00084DAA
		private void onGameOverlayActivated(GameOverlayActivated_t callback)
		{
			if (callback.m_bActive == 0)
			{
				this.refreshInventory();
			}
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x000869C0 File Offset: 0x00084DC0
		public void loadTranslationEconInfo()
		{
			string path = ReadWrite.PATH + "/EconInfo_" + Provider.language;
			if (!ReadWrite.fileExists(path, false, false))
			{
				path = Provider.path + Provider.language + "/EconInfo.json";
				if (!ReadWrite.fileExists(path, false, false))
				{
					return;
				}
			}
			IDeserializer deserializer = new JSONDeserializer();
			List<UnturnedEconInfo> list = new List<UnturnedEconInfo>();
			list = deserializer.deserialize<List<UnturnedEconInfo>>(path);
			using (List<UnturnedEconInfo>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					UnturnedEconInfo translatedItem = enumerator.Current;
					UnturnedEconInfo unturnedEconInfo = TempSteamworksEconomy.econInfo.Find((UnturnedEconInfo x) => x.itemdefid == translatedItem.itemdefid);
					if (unturnedEconInfo != null)
					{
						unturnedEconInfo.name = translatedItem.name;
						unturnedEconInfo.type = translatedItem.type;
						unturnedEconInfo.description = translatedItem.description;
					}
				}
			}
		}

		// Token: 0x04000C6E RID: 3182
		public TempSteamworksEconomy.InventoryRefreshed onInventoryRefreshed;

		// Token: 0x04000C6F RID: 3183
		public TempSteamworksEconomy.InventoryDropped onInventoryDropped;

		// Token: 0x04000C70 RID: 3184
		public TempSteamworksEconomy.InventoryExchanged onInventoryExchanged;

		// Token: 0x04000C71 RID: 3185
		public SteamInventoryResult_t promoResult = SteamInventoryResult_t.Invalid;

		// Token: 0x04000C72 RID: 3186
		public SteamInventoryResult_t exchangeResult = SteamInventoryResult_t.Invalid;

		// Token: 0x04000C73 RID: 3187
		public SteamInventoryResult_t dropResult = SteamInventoryResult_t.Invalid;

		// Token: 0x04000C74 RID: 3188
		public SteamInventoryResult_t wearingResult = SteamInventoryResult_t.Invalid;

		// Token: 0x04000C75 RID: 3189
		public SteamInventoryResult_t inventoryResult = SteamInventoryResult_t.Invalid;

		// Token: 0x04000C76 RID: 3190
		public SteamInventoryResult_t commitResult = SteamInventoryResult_t.Invalid;

		// Token: 0x04000C77 RID: 3191
		public SteamItemDetails_t[] inventoryDetails = new SteamItemDetails_t[0];

		// Token: 0x04000C78 RID: 3192
		public Dictionary<ulong, DynamicEconDetails> dynamicInventoryDetails = new Dictionary<ulong, DynamicEconDetails>();

		// Token: 0x04000C79 RID: 3193
		public bool isInventoryAvailable;

		// Token: 0x04000C7A RID: 3194
		private SteamworksAppInfo appInfo;

		// Token: 0x04000C7B RID: 3195
		private float lastHeartbeat;

		// Token: 0x04000C7C RID: 3196
		private Callback<SteamInventoryResultReady_t> inventoryResultReady;

		// Token: 0x04000C7D RID: 3197
		private Callback<GameOverlayActivated_t> gameOverlayActivated;

		// Token: 0x0200034A RID: 842
		// (Invoke) Token: 0x0600174F RID: 5967
		public delegate void InventoryRefreshed();

		// Token: 0x0200034B RID: 843
		// (Invoke) Token: 0x06001753 RID: 5971
		public delegate void InventoryDropped(int item, ushort quantity, ulong instance);

		// Token: 0x0200034C RID: 844
		// (Invoke) Token: 0x06001757 RID: 5975
		public delegate void InventoryExchanged(int item, ushort quantity, ulong instance);
	}
}
