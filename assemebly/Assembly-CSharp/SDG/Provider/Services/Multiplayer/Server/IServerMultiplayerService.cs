using System;
using System.IO;
using SDG.Provider.Services.Community;

namespace SDG.Provider.Services.Multiplayer.Server
{
	// Token: 0x02000339 RID: 825
	public interface IServerMultiplayerService : IService
	{
		// Token: 0x1700032C RID: 812
		// (get) Token: 0x060016F7 RID: 5879
		IServerInfo serverInfo { get; }

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x060016F8 RID: 5880
		bool isHosting { get; }

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x060016F9 RID: 5881
		MemoryStream stream { get; }

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x060016FA RID: 5882
		BinaryReader reader { get; }

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x060016FB RID: 5883
		BinaryWriter writer { get; }

		// Token: 0x14000068 RID: 104
		// (add) Token: 0x060016FC RID: 5884
		// (remove) Token: 0x060016FD RID: 5885
		event ServerMultiplayerServiceReadyHandler ready;

		// Token: 0x060016FE RID: 5886
		void open(uint ip, ushort port, ESecurityMode security);

		// Token: 0x060016FF RID: 5887
		void close();

		// Token: 0x06001700 RID: 5888
		bool read(out ICommunityEntity entity, byte[] data, out ulong length, int channel);

		// Token: 0x06001701 RID: 5889
		void write(ICommunityEntity entity, byte[] data, ulong length);

		// Token: 0x06001702 RID: 5890
		[Obsolete("Used by old multiplayer code, please use send without method instead.")]
		void write(ICommunityEntity entity, byte[] data, ulong length, ESendMethod method, int channel);
	}
}
