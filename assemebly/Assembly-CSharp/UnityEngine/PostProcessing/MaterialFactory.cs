using System;
using System.Collections.Generic;

namespace UnityEngine.PostProcessing
{
	// Token: 0x02000845 RID: 2117
	public sealed class MaterialFactory : IDisposable
	{
		// Token: 0x06003BDA RID: 15322 RVA: 0x001CD78E File Offset: 0x001CBB8E
		public MaterialFactory()
		{
			this.m_Materials = new Dictionary<string, Material>();
		}

		// Token: 0x06003BDB RID: 15323 RVA: 0x001CD7A4 File Offset: 0x001CBBA4
		public Material Get(string shaderName)
		{
			Material material;
			if (!this.m_Materials.TryGetValue(shaderName, out material))
			{
				Shader shader = Shader.Find(shaderName);
				if (shader == null)
				{
					throw new ArgumentException(string.Format("Shader not found ({0})", shaderName));
				}
				material = new Material(shader)
				{
					name = string.Format("PostFX - {0}", shaderName.Substring(shaderName.LastIndexOf("/") + 1)),
					hideFlags = HideFlags.DontSave
				};
				this.m_Materials.Add(shaderName, material);
			}
			return material;
		}

		// Token: 0x06003BDC RID: 15324 RVA: 0x001CD82C File Offset: 0x001CBC2C
		public void Dispose()
		{
			foreach (KeyValuePair<string, Material> keyValuePair in this.m_Materials)
			{
				Material value = keyValuePair.Value;
				GraphicsUtils.Destroy(value);
			}
			this.m_Materials.Clear();
		}

		// Token: 0x0400305C RID: 12380
		private Dictionary<string, Material> m_Materials;
	}
}
