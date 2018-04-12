using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x020007C5 RID: 1989
	public class Hash
	{
		// Token: 0x06003A30 RID: 14896 RVA: 0x001BDF3B File Offset: 0x001BC33B
		public static byte[] SHA1(byte[] bytes)
		{
			return Hash.service.ComputeHash(bytes);
		}

		// Token: 0x06003A31 RID: 14897 RVA: 0x001BDF48 File Offset: 0x001BC348
		public static byte[] SHA1(Stream stream)
		{
			return Hash.service.ComputeHash(stream);
		}

		// Token: 0x06003A32 RID: 14898 RVA: 0x001BDF55 File Offset: 0x001BC355
		public static byte[] SHA1(string text)
		{
			return Hash.SHA1(Encoding.UTF8.GetBytes(text));
		}

		// Token: 0x06003A33 RID: 14899 RVA: 0x001BDF67 File Offset: 0x001BC367
		public static byte[] SHA1(CSteamID steamID)
		{
			return Hash.SHA1(BitConverter.GetBytes(steamID.m_SteamID));
		}

		// Token: 0x06003A34 RID: 14900 RVA: 0x001BDF7C File Offset: 0x001BC37C
		public static bool verifyHash(byte[] hash_0, byte[] hash_1)
		{
			if (hash_0.Length != 20 || hash_1.Length != 20)
			{
				return false;
			}
			for (int i = 0; i < hash_0.Length; i++)
			{
				if (hash_0[i] != hash_1[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003A35 RID: 14901 RVA: 0x001BDFC4 File Offset: 0x001BC3C4
		public static byte[] combine(params byte[][] hashes)
		{
			byte[] array = new byte[hashes.Length * 20];
			for (int i = 0; i < hashes.Length; i++)
			{
				byte[] array2 = hashes[i];
				array2.CopyTo(array, i * 20);
			}
			return Hash.SHA1(array);
		}

		// Token: 0x06003A36 RID: 14902 RVA: 0x001BE008 File Offset: 0x001BC408
		public static void log(byte[] hash)
		{
			if (hash == null || hash.Length != 20)
			{
				return;
			}
			string text = string.Empty;
			for (int i = 0; i < hash.Length; i++)
			{
				text += hash[i].ToString("X");
			}
			CommandWindow.Log(text);
		}

		// Token: 0x04002D0A RID: 11530
		private static SHA1CryptoServiceProvider service = new SHA1CryptoServiceProvider();
	}
}
