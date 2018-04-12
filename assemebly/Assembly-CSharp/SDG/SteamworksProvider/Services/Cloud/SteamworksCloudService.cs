using System;
using SDG.Provider.Services;
using SDG.Provider.Services.Cloud;
using Steamworks;

namespace SDG.SteamworksProvider.Services.Cloud
{
	// Token: 0x02000367 RID: 871
	public class SteamworksCloudService : Service, ICloudService, IService
	{
		// Token: 0x060017D3 RID: 6099 RVA: 0x00088474 File Offset: 0x00086874
		public bool read(string path, byte[] data)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			int fileSize = SteamRemoteStorage.GetFileSize(path);
			if (data.Length < fileSize)
			{
				return false;
			}
			int num = SteamRemoteStorage.FileRead(path, data, fileSize);
			return num == fileSize;
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x000884C8 File Offset: 0x000868C8
		public bool write(string path, byte[] data, int size)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			return SteamRemoteStorage.FileWrite(path, data, size);
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x000884F4 File Offset: 0x000868F4
		public bool getSize(string path, out int size)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			size = SteamRemoteStorage.GetFileSize(path);
			return true;
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x00088510 File Offset: 0x00086910
		public bool exists(string path, out bool exists)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			exists = SteamRemoteStorage.FileExists(path);
			return true;
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x0008852C File Offset: 0x0008692C
		public bool delete(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			return SteamRemoteStorage.FileDelete(path);
		}
	}
}
