using System;
using SDG.Framework.Water;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000578 RID: 1400
	public class WaterHeightTransparentSort : MonoBehaviour
	{
		// Token: 0x060026A0 RID: 9888 RVA: 0x000E4CC8 File Offset: 0x000E30C8
		protected void updateRenderQueue()
		{
			if (WaterUtility.isPointUnderwater(base.transform.position))
			{
				if (LevelLighting.isSea)
				{
					this.material.renderQueue = 3100;
				}
				else
				{
					this.material.renderQueue = 2900;
				}
			}
			else if (LevelLighting.isSea)
			{
				this.material.renderQueue = 2900;
			}
			else
			{
				this.material.renderQueue = 3100;
			}
		}

		// Token: 0x060026A1 RID: 9889 RVA: 0x000E4D4D File Offset: 0x000E314D
		protected void handleIsSeaChanged(bool isSea)
		{
			this.updateRenderQueue();
		}

		// Token: 0x060026A2 RID: 9890 RVA: 0x000E4D55 File Offset: 0x000E3155
		protected void Start()
		{
			this.material = HighlighterTool.getMaterialInstance(base.transform);
			if (this.material != null)
			{
				LevelLighting.isSeaChanged += this.handleIsSeaChanged;
				this.updateRenderQueue();
			}
		}

		// Token: 0x060026A3 RID: 9891 RVA: 0x000E4D90 File Offset: 0x000E3190
		protected void OnDestroy()
		{
			if (this.material != null)
			{
				LevelLighting.isSeaChanged -= this.handleIsSeaChanged;
				UnityEngine.Object.DestroyImmediate(this.material);
				this.material = null;
			}
		}

		// Token: 0x0400182E RID: 6190
		protected bool isUnderwater;

		// Token: 0x0400182F RID: 6191
		protected Material material;
	}
}
