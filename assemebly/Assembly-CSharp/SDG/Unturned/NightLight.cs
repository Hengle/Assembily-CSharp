using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000563 RID: 1379
	public class NightLight : MonoBehaviour
	{
		// Token: 0x0600260E RID: 9742 RVA: 0x000DF658 File Offset: 0x000DDA58
		private void onLevelLoaded(int index)
		{
			if (this.isListeningLoad)
			{
				this.isListeningLoad = false;
				Level.onLevelLoaded = (LevelLoaded)Delegate.Remove(Level.onLevelLoaded, new LevelLoaded(this.onLevelLoaded));
			}
			if (!this.isListeningTime)
			{
				this.isListeningTime = true;
				LightingManager.onDayNightUpdated = (DayNightUpdated)Delegate.Combine(LightingManager.onDayNightUpdated, new DayNightUpdated(this.onDayNightUpdated));
			}
			this.onDayNightUpdated(LightingManager.isDaytime);
		}

		// Token: 0x0600260F RID: 9743 RVA: 0x000DF6D4 File Offset: 0x000DDAD4
		private void onDayNightUpdated(bool isDaytime)
		{
			if (this.target != null)
			{
				this.target.gameObject.SetActive(!isDaytime);
			}
			if (this.material != null)
			{
				this.material.SetColor("_EmissionColor", (!isDaytime) ? Color.white : Color.black);
			}
		}

		// Token: 0x06002610 RID: 9744 RVA: 0x000DF73C File Offset: 0x000DDB3C
		private void Awake()
		{
			this.material = HighlighterTool.getMaterialInstance(base.transform);
			if (Level.isEditor)
			{
				this.onDayNightUpdated(false);
				return;
			}
			if (!this.isListeningLoad)
			{
				this.isListeningLoad = true;
				Level.onLevelLoaded = (LevelLoaded)Delegate.Combine(Level.onLevelLoaded, new LevelLoaded(this.onLevelLoaded));
			}
		}

		// Token: 0x06002611 RID: 9745 RVA: 0x000DF7A0 File Offset: 0x000DDBA0
		private void OnDestroy()
		{
			if (this.material != null)
			{
				UnityEngine.Object.DestroyImmediate(this.material);
			}
			if (this.isListeningLoad)
			{
				this.isListeningLoad = false;
				Level.onLevelLoaded = (LevelLoaded)Delegate.Remove(Level.onLevelLoaded, new LevelLoaded(this.onLevelLoaded));
			}
			if (this.isListeningTime)
			{
				this.isListeningTime = false;
				LightingManager.onDayNightUpdated = (DayNightUpdated)Delegate.Remove(LightingManager.onDayNightUpdated, new DayNightUpdated(this.onDayNightUpdated));
			}
		}

		// Token: 0x040017B5 RID: 6069
		public Light target;

		// Token: 0x040017B6 RID: 6070
		private Material material;

		// Token: 0x040017B7 RID: 6071
		private bool isListeningLoad;

		// Token: 0x040017B8 RID: 6072
		private bool isListeningTime;
	}
}
