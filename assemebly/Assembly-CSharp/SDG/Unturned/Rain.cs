using System;
using SDG.Framework.Water;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000568 RID: 1384
	public class Rain : MonoBehaviour
	{
		// Token: 0x0600262C RID: 9772 RVA: 0x000E02CF File Offset: 0x000DE6CF
		private void onGraphicsSettingsApplied()
		{
			this.needsIsRainingUpdate = true;
		}

		// Token: 0x0600262D RID: 9773 RVA: 0x000E02D8 File Offset: 0x000DE6D8
		private void Update()
		{
			if (Dedicator.isDedicated)
			{
				return;
			}
			if (this._Rain_Puddle_Map != -1)
			{
				Shader.SetGlobalTexture(this._Rain_Puddle_Map, this.Puddle_Map);
			}
			if (this._Rain_Ripple_Map != -1)
			{
				Shader.SetGlobalTexture(this._Rain_Ripple_Map, this.Ripple_Map);
			}
			if (this._Rain_Water_Level != -1)
			{
				Shader.SetGlobalFloat(this._Rain_Water_Level, this.Water_Level);
			}
			if (this._Rain_Intensity != -1)
			{
				Shader.SetGlobalFloat(this._Rain_Intensity, this.Intensity);
			}
			if (this._Rain_Min_Height != -1)
			{
				if (Level.info != null && Level.info.configData.Use_Legacy_Water)
				{
					this.rainMinHeightVolume = null;
					this.rainMinHeight = LevelLighting.seaLevel * Level.TERRAIN;
				}
				else if (this.rainMinHeightVolume != WaterSystem.seaLevelVolume)
				{
					this.rainMinHeightVolume = WaterSystem.seaLevelVolume;
					this.rainMinHeight = WaterSystem.worldSeaLevel;
				}
				Shader.SetGlobalFloat(this._Rain_Min_Height, this.rainMinHeight);
			}
			if (this.Water_Level > 0.01f)
			{
				if (!this.isRaining)
				{
					this.isRaining = true;
					this.needsIsRainingUpdate = true;
				}
			}
			else if (this.isRaining)
			{
				this.isRaining = false;
				this.needsIsRainingUpdate = true;
			}
			if (this.needsIsRainingUpdate)
			{
				if (this.isRaining && GraphicsSettings.puddle)
				{
					Shader.EnableKeyword("IS_RAINING");
				}
				else
				{
					Shader.DisableKeyword("IS_RAINING");
				}
				this.needsIsRainingUpdate = false;
			}
		}

		// Token: 0x0600262E RID: 9774 RVA: 0x000E0470 File Offset: 0x000DE870
		private void OnEnable()
		{
			if (Dedicator.isDedicated)
			{
				return;
			}
			GraphicsSettings.graphicsSettingsApplied += this.onGraphicsSettingsApplied;
			if (this._Rain_Puddle_Map == -1)
			{
				this._Rain_Puddle_Map = Shader.PropertyToID("_Rain_Puddle_Map");
			}
			if (this._Rain_Ripple_Map == -1)
			{
				this._Rain_Ripple_Map = Shader.PropertyToID("_Rain_Ripple_Map");
			}
			if (this._Rain_Water_Level == -1)
			{
				this._Rain_Water_Level = Shader.PropertyToID("_Rain_Water_Level");
			}
			if (this._Rain_Intensity == -1)
			{
				this._Rain_Intensity = Shader.PropertyToID("_Rain_Intensity");
			}
			if (this._Rain_Min_Height == -1)
			{
				this._Rain_Min_Height = Shader.PropertyToID("_Rain_Min_Height");
			}
			this.isRaining = false;
			this.needsIsRainingUpdate = true;
		}

		// Token: 0x0600262F RID: 9775 RVA: 0x000E0533 File Offset: 0x000DE933
		private void OnDisable()
		{
			if (Dedicator.isDedicated)
			{
				return;
			}
			GraphicsSettings.graphicsSettingsApplied -= this.onGraphicsSettingsApplied;
		}

		// Token: 0x040017D2 RID: 6098
		private int _Rain_Puddle_Map = -1;

		// Token: 0x040017D3 RID: 6099
		private int _Rain_Ripple_Map = -1;

		// Token: 0x040017D4 RID: 6100
		private int _Rain_Water_Level = -1;

		// Token: 0x040017D5 RID: 6101
		private int _Rain_Intensity = -1;

		// Token: 0x040017D6 RID: 6102
		private int _Rain_Min_Height = -1;

		// Token: 0x040017D7 RID: 6103
		public Texture2D Puddle_Map;

		// Token: 0x040017D8 RID: 6104
		public Texture2D Ripple_Map;

		// Token: 0x040017D9 RID: 6105
		public float Water_Level;

		// Token: 0x040017DA RID: 6106
		public float Intensity;

		// Token: 0x040017DB RID: 6107
		private bool isRaining;

		// Token: 0x040017DC RID: 6108
		private bool needsIsRainingUpdate;

		// Token: 0x040017DD RID: 6109
		private WaterVolume rainMinHeightVolume;

		// Token: 0x040017DE RID: 6110
		private float rainMinHeight;
	}
}
