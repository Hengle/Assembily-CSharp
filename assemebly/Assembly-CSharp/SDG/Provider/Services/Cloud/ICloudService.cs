using System;

namespace SDG.Provider.Services.Cloud
{
	// Token: 0x0200031C RID: 796
	public interface ICloudService : IService
	{
		// Token: 0x060016A9 RID: 5801
		bool read(string path, byte[] data);

		// Token: 0x060016AA RID: 5802
		bool write(string path, byte[] data, int size);

		// Token: 0x060016AB RID: 5803
		bool getSize(string path, out int size);

		// Token: 0x060016AC RID: 5804
		bool exists(string path, out bool exists);

		// Token: 0x060016AD RID: 5805
		bool delete(string path);
	}
}
