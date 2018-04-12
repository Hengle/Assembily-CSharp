using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using SDG.Framework.Devkit.Visibility;
using SDG.Framework.Rendering;
using SDG.Framework.Translations;
using UnityEngine;

namespace SDG.Framework.Water
{
	// Token: 0x02000313 RID: 787
	public static class WaterSystem
	{
		// Token: 0x0600165F RID: 5727 RVA: 0x00084BD4 File Offset: 0x00082FD4
		static WaterSystem()
		{
			WaterSystem.waterVisibilityGroup.color = new Color32(50, 200, 200, byte.MaxValue);
			WaterSystem.waterVisibilityGroup = VisibilityManager.registerVisibilityGroup<VolumeVisibilityGroup>(WaterSystem.waterVisibilityGroup);
			if (WaterSystem.<>f__mg$cache0 == null)
			{
				WaterSystem.<>f__mg$cache0 = new GLRenderHandler(WaterSystem.handleGLRender);
			}
			GLRenderer.render += WaterSystem.<>f__mg$cache0;
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06001660 RID: 5728 RVA: 0x00084C5B File Offset: 0x0008305B
		// (set) Token: 0x06001661 RID: 5729 RVA: 0x00084C62 File Offset: 0x00083062
		public static List<WaterVolume> volumes { get; private set; } = new List<WaterVolume>();

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06001662 RID: 5730 RVA: 0x00084C6A File Offset: 0x0008306A
		// (set) Token: 0x06001663 RID: 5731 RVA: 0x00084C71 File Offset: 0x00083071
		public static VolumeVisibilityGroup waterVisibilityGroup { get; private set; } = new VolumeVisibilityGroup("water_volumes", new TranslationReference("#SDG::Devkit.Visibility.Water_Volumes"), true);

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06001664 RID: 5732 RVA: 0x00084C7C File Offset: 0x0008307C
		public static float worldSeaLevel
		{
			get
			{
				if (WaterSystem.seaLevelVolume != null)
				{
					return WaterSystem.seaLevelVolume.transform.TransformPoint(0f, 0.5f, 0f).y;
				}
				return -1024f;
			}
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x00084CC5 File Offset: 0x000830C5
		public static void addVolume(WaterVolume volume)
		{
			WaterSystem.volumes.Add(volume);
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x00084CD2 File Offset: 0x000830D2
		public static void removeVolume(WaterVolume volume)
		{
			WaterSystem.volumes.Remove(volume);
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x00084CE0 File Offset: 0x000830E0
		private static void handleGLRender()
		{
			if (!WaterSystem.waterVisibilityGroup.isVisible)
			{
				return;
			}
			foreach (WaterVolume waterVolume in WaterSystem.volumes)
			{
				GLUtility.matrix = waterVolume.transform.localToWorldMatrix;
				GLUtility.volumeHelper(waterVolume.isSelected, WaterSystem.waterVisibilityGroup);
			}
		}

		// Token: 0x04000C3C RID: 3132
		public static WaterVolume seaLevelVolume;

		// Token: 0x04000C3D RID: 3133
		[CompilerGenerated]
		private static GLRenderHandler <>f__mg$cache0;
	}
}
