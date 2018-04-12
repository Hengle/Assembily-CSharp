using System;
using System.IO;
using SDG.Provider.Services.Community;

namespace SDG.Provider.Services.Multiplayer.Client
{
	// Token: 0x02000329 RID: 809
	public interface IClientMultiplayerService : IService
	{
		// Token: 0x1700031E RID: 798
		// (get) Token: 0x060016CB RID: 5835
		IServerInfo serverInfo { get; }

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x060016CC RID: 5836
		bool isConnected { get; }

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x060016CD RID: 5837
		bool isAttempting { get; }

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x060016CE RID: 5838
		MemoryStream stream { get; }

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x060016CF RID: 5839
		BinaryReader reader { get; }

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x060016D0 RID: 5840
		BinaryWriter writer { get; }

		// Token: 0x060016D1 RID: 5841
		void connect(IServerInfo newServerInfo);

		// Token: 0x060016D2 RID: 5842
		void disconnect();

		// Token: 0x060016D3 RID: 5843
		bool read(out ICommunityEntity entity, byte[] data, out ulong length, int channel);

		// Token: 0x060016D4 RID: 5844
		void write(ICommunityEntity entity, byte[] data, ulong length);

		// Token: 0x060016D5 RID: 5845
		[Obsolete("Used by old multiplayer code, please use send without method instead.")]
		void write(ICommunityEntity entity, byte[] data, ulong length, ESendMethod method, int channel);
	}
}
