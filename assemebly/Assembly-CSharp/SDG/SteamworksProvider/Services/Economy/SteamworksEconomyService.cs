using System;
using System.Collections.Generic;
using SDG.Provider.Services;
using SDG.Provider.Services.Economy;
using Steamworks;

namespace SDG.SteamworksProvider.Services.Economy
{
	// Token: 0x0200036E RID: 878
	public class SteamworksEconomyService : Service, IEconomyService, IService
	{
		// Token: 0x06001800 RID: 6144 RVA: 0x0008893E File Offset: 0x00086D3E
		public SteamworksEconomyService()
		{
			this.steamworksEconomyRequestHandles = new List<SteamworksEconomyRequestHandle>();
			this.steamInventoryResultReady = Callback<SteamInventoryResultReady_t>.Create(new Callback<SteamInventoryResultReady_t>.DispatchDelegate(this.onSteamInventoryResultReady));
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06001801 RID: 6145 RVA: 0x00088968 File Offset: 0x00086D68
		public bool canOpenInventory
		{
			get
			{
				return SteamUtils.IsOverlayEnabled();
			}
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x00088970 File Offset: 0x00086D70
		public IEconomyRequestHandle requestInventory(EconomyRequestReadyCallback inventoryRequestReadyCallback)
		{
			SteamInventoryResult_t steamInventoryResult;
			SteamInventory.GetAllItems(out steamInventoryResult);
			return this.addInventoryRequestHandle(steamInventoryResult, inventoryRequestReadyCallback);
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x00088990 File Offset: 0x00086D90
		public IEconomyRequestHandle requestPromo(EconomyRequestReadyCallback inventoryRequestReadyCallback)
		{
			SteamInventoryResult_t steamInventoryResult;
			SteamInventory.GrantPromoItems(out steamInventoryResult);
			return this.addInventoryRequestHandle(steamInventoryResult, inventoryRequestReadyCallback);
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x000889B0 File Offset: 0x00086DB0
		public IEconomyRequestHandle exchangeItems(IEconomyItemInstance[] inputItemInstanceIDs, uint[] inputItemQuantities, IEconomyItemDefinition[] outputItemDefinitionIDs, uint[] outputItemQuantities, EconomyRequestReadyCallback inventoryRequestReadyCallback)
		{
			if (inputItemInstanceIDs.Length != inputItemQuantities.Length)
			{
				throw new ArgumentException("Input item arrays need to be the same length.", "inputItemQuantities");
			}
			if (outputItemDefinitionIDs.Length != outputItemQuantities.Length)
			{
				throw new ArgumentException("Output item arrays need to be the same length.", "outputItemQuantities");
			}
			SteamItemInstanceID_t[] array = new SteamItemInstanceID_t[inputItemInstanceIDs.Length];
			for (int i = 0; i < inputItemInstanceIDs.Length; i++)
			{
				SteamworksEconomyItemInstance steamworksEconomyItemInstance = (SteamworksEconomyItemInstance)inputItemInstanceIDs[i];
				array[i] = steamworksEconomyItemInstance.steamItemInstanceID;
			}
			SteamItemDef_t[] array2 = new SteamItemDef_t[outputItemDefinitionIDs.Length];
			for (int j = 0; j < outputItemDefinitionIDs.Length; j++)
			{
				SteamworksEconomyItemDefinition steamworksEconomyItemDefinition = (SteamworksEconomyItemDefinition)outputItemDefinitionIDs[j];
				array2[j] = steamworksEconomyItemDefinition.steamItemDef;
			}
			SteamInventoryResult_t steamInventoryResult;
			SteamInventory.ExchangeItems(out steamInventoryResult, array2, outputItemQuantities, (uint)array2.Length, array, inputItemQuantities, (uint)array.Length);
			return this.addInventoryRequestHandle(steamInventoryResult, inventoryRequestReadyCallback);
		}

		// Token: 0x06001805 RID: 6149 RVA: 0x00088A90 File Offset: 0x00086E90
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

		// Token: 0x06001806 RID: 6150 RVA: 0x00088AE8 File Offset: 0x00086EE8
		private SteamworksEconomyRequestHandle findSteamworksEconomyRequestHandles(SteamInventoryResult_t steamInventoryResult)
		{
			return this.steamworksEconomyRequestHandles.Find((SteamworksEconomyRequestHandle handle) => handle.steamInventoryResult == steamInventoryResult);
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x00088B1C File Offset: 0x00086F1C
		private IEconomyRequestHandle addInventoryRequestHandle(SteamInventoryResult_t steamInventoryResult, EconomyRequestReadyCallback inventoryRequestReadyCallback)
		{
			SteamworksEconomyRequestHandle steamworksEconomyRequestHandle = new SteamworksEconomyRequestHandle(steamInventoryResult, inventoryRequestReadyCallback);
			this.steamworksEconomyRequestHandles.Add(steamworksEconomyRequestHandle);
			return steamworksEconomyRequestHandle;
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x00088B40 File Offset: 0x00086F40
		private IEconomyRequestResult createInventoryRequestResult(SteamInventoryResult_t steamInventoryResult)
		{
			uint num = 0u;
			SteamworksEconomyItem[] array2;
			if (SteamGameServerInventory.GetResultItems(steamInventoryResult, null, ref num) && num > 0u)
			{
				SteamItemDetails_t[] array = new SteamItemDetails_t[num];
				SteamGameServerInventory.GetResultItems(steamInventoryResult, array, ref num);
				array2 = new SteamworksEconomyItem[num];
				for (uint num2 = 0u; num2 < num; num2 += 1u)
				{
					SteamItemDetails_t newSteamItemDetail = array[(int)((UIntPtr)num2)];
					SteamworksEconomyItem steamworksEconomyItem = new SteamworksEconomyItem(newSteamItemDetail);
					array2[(int)((UIntPtr)num2)] = steamworksEconomyItem;
				}
			}
			else
			{
				array2 = new SteamworksEconomyItem[0];
			}
			return new EconomyRequestResult(EEconomyRequestState.SUCCESS, array2);
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x00088BC8 File Offset: 0x00086FC8
		private void onSteamInventoryResultReady(SteamInventoryResultReady_t callback)
		{
			SteamworksEconomyRequestHandle steamworksEconomyRequestHandle = this.findSteamworksEconomyRequestHandles(callback.m_handle);
			if (steamworksEconomyRequestHandle == null)
			{
				return;
			}
			IEconomyRequestResult inventoryRequestResult = this.createInventoryRequestResult(steamworksEconomyRequestHandle.steamInventoryResult);
			steamworksEconomyRequestHandle.triggerInventoryRequestReadyCallback(inventoryRequestResult);
			SteamInventory.DestroyResult(steamworksEconomyRequestHandle.steamInventoryResult);
		}

		// Token: 0x04000CCE RID: 3278
		private List<SteamworksEconomyRequestHandle> steamworksEconomyRequestHandles;

		// Token: 0x04000CCF RID: 3279
		private Callback<SteamInventoryResultReady_t> steamInventoryResultReady;
	}
}
