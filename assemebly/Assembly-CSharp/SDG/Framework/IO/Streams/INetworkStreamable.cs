using System;

namespace SDG.Framework.IO.Streams
{
	// Token: 0x020001CC RID: 460
	public interface INetworkStreamable
	{
		// Token: 0x06000DBB RID: 3515
		void readFromStream(NetworkStream networkStream);

		// Token: 0x06000DBC RID: 3516
		void writeToStream(NetworkStream networkStream);
	}
}
