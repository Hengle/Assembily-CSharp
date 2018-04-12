using System;
using SDG.Framework.IO.Streams;
using SDG.Provider.Services.Economy;
using Steamworks;

namespace SDG.SteamworksProvider.Services.Economy
{
	// Token: 0x0200036A RID: 874
	public class SteamworksEconomyItem : IEconomyItem, INetworkStreamable
	{
		// Token: 0x060017E6 RID: 6118 RVA: 0x00088784 File Offset: 0x00086B84
		public SteamworksEconomyItem(SteamItemDetails_t newSteamItemDetail)
		{
			this.steamItemDetail = newSteamItemDetail;
			this.itemDefinitionID = new SteamworksEconomyItemDefinition(this.steamItemDetail.m_iDefinition);
			this.itemInstanceID = new SteamworksEconomyItemInstance(this.steamItemDetail.m_itemId);
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x060017E7 RID: 6119 RVA: 0x000887D0 File Offset: 0x00086BD0
		// (set) Token: 0x060017E8 RID: 6120 RVA: 0x000887D8 File Offset: 0x00086BD8
		public SteamItemDetails_t steamItemDetail { get; protected set; }

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x060017E9 RID: 6121 RVA: 0x000887E1 File Offset: 0x00086BE1
		// (set) Token: 0x060017EA RID: 6122 RVA: 0x000887E9 File Offset: 0x00086BE9
		public IEconomyItemDefinition itemDefinitionID { get; protected set; }

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x060017EB RID: 6123 RVA: 0x000887F2 File Offset: 0x00086BF2
		// (set) Token: 0x060017EC RID: 6124 RVA: 0x000887FA File Offset: 0x00086BFA
		public IEconomyItemInstance itemInstanceID { get; protected set; }

		// Token: 0x060017ED RID: 6125 RVA: 0x00088803 File Offset: 0x00086C03
		public void readFromStream(NetworkStream networkStream)
		{
			this.itemDefinitionID.readFromStream(networkStream);
			this.itemInstanceID.readFromStream(networkStream);
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x0008881D File Offset: 0x00086C1D
		public void writeToStream(NetworkStream networkStream)
		{
			this.itemDefinitionID.writeToStream(networkStream);
			this.itemInstanceID.writeToStream(networkStream);
		}
	}
}
