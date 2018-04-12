using System;
using SDG.Framework.IO.Streams;
using SDG.Provider.Services.Economy;
using Steamworks;

namespace SDG.SteamworksProvider.Services.Economy
{
	// Token: 0x0200036B RID: 875
	public class SteamworksEconomyItemDefinition : IEconomyItemDefinition, INetworkStreamable
	{
		// Token: 0x060017EF RID: 6127 RVA: 0x00088837 File Offset: 0x00086C37
		public SteamworksEconomyItemDefinition(SteamItemDef_t newSteamItemDef)
		{
			this.steamItemDef = newSteamItemDef;
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x060017F0 RID: 6128 RVA: 0x00088846 File Offset: 0x00086C46
		// (set) Token: 0x060017F1 RID: 6129 RVA: 0x0008884E File Offset: 0x00086C4E
		public SteamItemDef_t steamItemDef { get; protected set; }

		// Token: 0x060017F2 RID: 6130 RVA: 0x00088857 File Offset: 0x00086C57
		public void readFromStream(NetworkStream networkStream)
		{
			this.steamItemDef = (SteamItemDef_t)networkStream.readInt32();
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x0008886A File Offset: 0x00086C6A
		public void writeToStream(NetworkStream networkStream)
		{
			networkStream.writeInt32((int)this.steamItemDef);
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x00088880 File Offset: 0x00086C80
		public string getPropertyValue(string key)
		{
			uint num = 1024u;
			string result;
			SteamInventory.GetItemDefinitionProperty(this.steamItemDef, key, out result, ref num);
			return result;
		}
	}
}
