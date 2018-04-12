using System;
using SDG.Framework.IO.Streams;
using SDG.Provider.Services.Community;
using Steamworks;

namespace SDG.SteamworksProvider.Services.Community
{
	// Token: 0x02000368 RID: 872
	public class SteamworksCommunityEntity : ICommunityEntity, INetworkStreamable
	{
		// Token: 0x060017D8 RID: 6104 RVA: 0x00088545 File Offset: 0x00086945
		public SteamworksCommunityEntity(CSteamID newSteamID)
		{
			this.steamID = newSteamID;
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x060017D9 RID: 6105 RVA: 0x00088554 File Offset: 0x00086954
		public bool isValid
		{
			get
			{
				return this.steamID.IsValid();
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x060017DA RID: 6106 RVA: 0x0008856F File Offset: 0x0008696F
		// (set) Token: 0x060017DB RID: 6107 RVA: 0x00088577 File Offset: 0x00086977
		public CSteamID steamID { get; protected set; }

		// Token: 0x060017DC RID: 6108 RVA: 0x00088580 File Offset: 0x00086980
		public void readFromStream(NetworkStream networkStream)
		{
			this.steamID = (CSteamID)networkStream.readUInt64();
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x00088593 File Offset: 0x00086993
		public void writeToStream(NetworkStream networkStream)
		{
			networkStream.writeUInt64((ulong)this.steamID);
		}

		// Token: 0x04000CC4 RID: 3268
		public static readonly SteamworksCommunityEntity INVALID = new SteamworksCommunityEntity(CSteamID.Nil);
	}
}
