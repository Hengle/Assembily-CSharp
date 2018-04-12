using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000570 RID: 1392
	public class RoadMaterial
	{
		// Token: 0x0600267B RID: 9851 RVA: 0x000E3F38 File Offset: 0x000E2338
		public RoadMaterial(Texture2D texture)
		{
			this._material = new Material(Shader.Find("Standard/Diffuse"));
			this.material.name = "Road";
			this.material.mainTexture = texture;
			this.width = 4f;
			this.height = 1f;
			this.depth = 0.5f;
			this.offset = 0f;
			this.isConcrete = true;
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x0600267C RID: 9852 RVA: 0x000E3FAF File Offset: 0x000E23AF
		public Material material
		{
			get
			{
				return this._material;
			}
		}

		// Token: 0x0400180F RID: 6159
		private Material _material;

		// Token: 0x04001810 RID: 6160
		public float width;

		// Token: 0x04001811 RID: 6161
		public float height;

		// Token: 0x04001812 RID: 6162
		public float depth;

		// Token: 0x04001813 RID: 6163
		public float offset;

		// Token: 0x04001814 RID: 6164
		public bool isConcrete;
	}
}
