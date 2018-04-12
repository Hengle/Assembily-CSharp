using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200038D RID: 909
	public class Bundle
	{
		// Token: 0x06001967 RID: 6503 RVA: 0x000903A7 File Offset: 0x0008E7A7
		public Bundle(string path) : this(path, true, false)
		{
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x000903B4 File Offset: 0x0008E7B4
		public Bundle(string path, bool usePath, bool loadFromResources)
		{
			this.loadFromResources = loadFromResources;
			if (!loadFromResources && ReadWrite.fileExists(path, false, usePath))
			{
				if (this.asset == null)
				{
					this.asset = AssetBundle.LoadFromFile((!usePath) ? path : (ReadWrite.PATH + path));
				}
			}
			else
			{
				this.asset = null;
			}
			this.name = ReadWrite.fileName(path);
			if (this.asset == null)
			{
				this.resource = ReadWrite.folderPath(path).Substring(1);
			}
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x0009044F File Offset: 0x0008E84F
		public Bundle()
		{
			this.asset = null;
			this.name = "#NAME";
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x0600196A RID: 6506 RVA: 0x00090469 File Offset: 0x0008E869
		// (set) Token: 0x0600196B RID: 6507 RVA: 0x00090471 File Offset: 0x0008E871
		public AssetBundle asset { get; protected set; }

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x0600196C RID: 6508 RVA: 0x0009047A File Offset: 0x0008E87A
		// (set) Token: 0x0600196D RID: 6509 RVA: 0x00090482 File Offset: 0x0008E882
		public string resource { get; protected set; }

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x0600196E RID: 6510 RVA: 0x0009048B File Offset: 0x0008E88B
		// (set) Token: 0x0600196F RID: 6511 RVA: 0x00090493 File Offset: 0x0008E893
		public string name { get; protected set; }

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06001970 RID: 6512 RVA: 0x0009049C File Offset: 0x0008E89C
		public bool hasResource
		{
			get
			{
				return this.asset == null;
			}
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x000904AC File Offset: 0x0008E8AC
		public UnityEngine.Object load(string name)
		{
			if (!(this.asset != null))
			{
				return Resources.Load(this.resource + "/" + name);
			}
			if (this.asset.Contains(name))
			{
				UnityEngine.Object @object = this.asset.LoadAsset(name);
				if (this.convertShadersToStandard && @object.GetType() == typeof(GameObject))
				{
					if (Bundle.shader == null)
					{
						Bundle.shader = Shader.Find("Standard");
					}
					Bundle.renderers.Clear();
					((GameObject)@object).GetComponentsInChildren<Renderer>(true, Bundle.renderers);
					for (int i = 0; i < Bundle.renderers.Count; i++)
					{
						Renderer renderer = Bundle.renderers[i];
						if (!(renderer == null))
						{
							Material sharedMaterial = renderer.sharedMaterial;
							if (!(sharedMaterial == null))
							{
								sharedMaterial.shader = Bundle.shader;
							}
						}
					}
				}
				return @object;
			}
			return null;
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x000905B7 File Offset: 0x0008E9B7
		public UnityEngine.Object[] load()
		{
			if (this.asset != null)
			{
				return this.asset.LoadAllAssets();
			}
			return null;
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x000905D7 File Offset: 0x0008E9D7
		public UnityEngine.Object[] load(Type type)
		{
			if (this.asset != null)
			{
				return this.asset.LoadAllAssets(type);
			}
			return null;
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x000905F8 File Offset: 0x0008E9F8
		public void unload()
		{
			if (this.asset != null)
			{
				this.asset.Unload(false);
			}
		}

		// Token: 0x04000DAD RID: 3501
		private static Shader shader;

		// Token: 0x04000DAE RID: 3502
		private static List<Renderer> renderers = new List<Renderer>();

		// Token: 0x04000DB2 RID: 3506
		public bool convertShadersToStandard;

		// Token: 0x04000DB3 RID: 3507
		public bool loadFromResources;
	}
}
