using System;
using System.Collections.Generic;
using System.IO;

namespace SDG.Unturned
{
	// Token: 0x020004AA RID: 1194
	public class Localization
	{
		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06001FE3 RID: 8163 RVA: 0x000AFD5E File Offset: 0x000AE15E
		public static List<string> messages
		{
			get
			{
				return Localization._messages;
			}
		}

		// Token: 0x06001FE4 RID: 8164 RVA: 0x000AFD65 File Offset: 0x000AE165
		public static Local tryRead(string path)
		{
			return Localization.tryRead(path, true);
		}

		// Token: 0x06001FE5 RID: 8165 RVA: 0x000AFD70 File Offset: 0x000AE170
		public static Local tryRead(string path, bool usePath)
		{
			if (ReadWrite.fileExists(path + "/" + Provider.language + ".dat", false, usePath))
			{
				return new Local(ReadWrite.readData(path + "/" + Provider.language + ".dat", false, usePath));
			}
			if (ReadWrite.fileExists(path + "/English.dat", false, usePath))
			{
				return new Local(ReadWrite.readData(path + "/English.dat", false, usePath));
			}
			return new Local();
		}

		// Token: 0x06001FE6 RID: 8166 RVA: 0x000AFDF4 File Offset: 0x000AE1F4
		public static Local read(string path)
		{
			if (ReadWrite.fileExists(Provider.path + Provider.language + path, false, false))
			{
				return new Local(ReadWrite.readData(Provider.path + Provider.language + path, false, false));
			}
			return new Local();
		}

		// Token: 0x06001FE7 RID: 8167 RVA: 0x000AFE34 File Offset: 0x000AE234
		private static void scanFile(string path)
		{
			Data data = ReadWrite.readData("/Localization/English/" + path, false, true);
			Data data2 = ReadWrite.readData(Provider.path + Provider.language + path, false, false);
			KeyValuePair<string, string>[] contents = data.getContents();
			KeyValuePair<string, string>[] contents2 = data2.getContents();
			Localization.keys.Clear();
			for (int i = 0; i < contents.Length; i++)
			{
				string key = contents[i].Key;
				bool flag = false;
				for (int j = 0; j < contents2.Length; j++)
				{
					string key2 = contents2[j].Key;
					if (key == key2)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					Localization.keys.Add(key);
				}
			}
			if (Localization.keys.Count > 0)
			{
				string text = string.Empty;
				for (int k = 0; k < Localization.keys.Count; k++)
				{
					if (k == 0)
					{
						text += Localization.keys[k];
					}
					else if (k == Localization.keys.Count - 1)
					{
						text = text + " and " + Localization.keys[k];
					}
					else
					{
						text = text + ", " + Localization.keys[k];
					}
				}
				Localization.messages.Add(string.Concat(new object[]
				{
					path,
					" has ",
					Localization.keys.Count,
					" new keys: ",
					text
				}));
			}
		}

		// Token: 0x06001FE8 RID: 8168 RVA: 0x000AFFE4 File Offset: 0x000AE3E4
		private static void scanFolder(string path)
		{
			string[] files = ReadWrite.getFiles("/Localization/English/" + path, true);
			string[] files2 = ReadWrite.getFiles(Provider.path + Provider.language + path, false);
			for (int i = 0; i < files.Length; i++)
			{
				string fileName = Path.GetFileName(files[i]);
				bool flag = false;
				for (int j = 0; j < files2.Length; j++)
				{
					string fileName2 = Path.GetFileName(files2[j]);
					if (fileName == fileName2)
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					Localization.scanFile(path + "/" + fileName);
				}
				else
				{
					Localization.messages.Add("New file \"" + fileName + "\" in " + path);
				}
			}
			string[] folders = ReadWrite.getFolders("/Localization/English/" + path, true);
			string[] folders2 = ReadWrite.getFolders(Provider.path + Provider.language + path, false);
			for (int k = 0; k < folders.Length; k++)
			{
				string fileName3 = Path.GetFileName(folders[k]);
				bool flag2 = false;
				for (int l = 0; l < folders2.Length; l++)
				{
					string fileName4 = Path.GetFileName(folders2[l]);
					if (fileName3 == fileName4)
					{
						flag2 = true;
						break;
					}
				}
				if (flag2)
				{
					Localization.scanFolder(path + "/" + fileName3);
				}
				else
				{
					Localization.messages.Add("New folder \"" + fileName3 + "\" in " + path);
				}
			}
		}

		// Token: 0x06001FE9 RID: 8169 RVA: 0x000B0174 File Offset: 0x000AE574
		public static void refresh()
		{
			if (Localization.messages == null)
			{
				Localization._messages = new List<string>();
			}
			else
			{
				Localization.messages.Clear();
			}
			Localization.scanFolder("/Player");
			Localization.scanFolder("/Menu");
			Localization.scanFolder("/Server");
			Localization.scanFolder("/Editor");
		}

		// Token: 0x04001316 RID: 4886
		private static List<string> _messages;

		// Token: 0x04001317 RID: 4887
		private static List<string> keys = new List<string>();
	}
}
