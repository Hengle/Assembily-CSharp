using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200041E RID: 1054
	public class RootContentDirectory : ContentDirectory
	{
		// Token: 0x06001C9E RID: 7326 RVA: 0x0009B9B8 File Offset: 0x00099DB8
		public RootContentDirectory(AssetBundle newAssetBundle, string newName) : base(newName, null)
		{
			this.assetBundle = newAssetBundle;
			string[] allAssetNames = this.assetBundle.GetAllAssetNames();
			for (int i = 0; i < allAssetNames.Length; i++)
			{
				ContentDirectory contentDirectory = this;
				string[] array = allAssetNames[i].Split(new char[]
				{
					'/'
				});
				for (int j = 0; j < array.Length; j++)
				{
					if (j == array.Length - 1)
					{
						string newFile = array[j];
						contentDirectory.files.Add(new ContentFile(this, contentDirectory, allAssetNames[i], newFile));
					}
					else
					{
						string text = array[j];
						ContentDirectory contentDirectory2;
						if (!contentDirectory.directories.TryGetValue(text, out contentDirectory2))
						{
							contentDirectory2 = new ContentDirectory(text, contentDirectory);
							contentDirectory.directories.Add(text, contentDirectory2);
						}
						contentDirectory = contentDirectory2;
					}
				}
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001C9F RID: 7327 RVA: 0x0009BA86 File Offset: 0x00099E86
		// (set) Token: 0x06001CA0 RID: 7328 RVA: 0x0009BA8E File Offset: 0x00099E8E
		public AssetBundle assetBundle { get; protected set; }
	}
}
