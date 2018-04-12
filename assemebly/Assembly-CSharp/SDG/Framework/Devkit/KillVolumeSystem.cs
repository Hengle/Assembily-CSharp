using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using SDG.Framework.Devkit.Visibility;
using SDG.Framework.Rendering;
using SDG.Framework.Translations;
using UnityEngine;

namespace SDG.Framework.Devkit
{
	// Token: 0x02000150 RID: 336
	public static class KillVolumeSystem
	{
		// Token: 0x060009E9 RID: 2537 RVA: 0x00050980 File Offset: 0x0004ED80
		static KillVolumeSystem()
		{
			KillVolumeSystem.killVisibilityGroup.color = new Color32(220, 100, 20, byte.MaxValue);
			KillVolumeSystem.killVisibilityGroup = VisibilityManager.registerVisibilityGroup<VolumeVisibilityGroup>(KillVolumeSystem.killVisibilityGroup);
			if (KillVolumeSystem.<>f__mg$cache0 == null)
			{
				KillVolumeSystem.<>f__mg$cache0 = new GLRenderHandler(KillVolumeSystem.handleGLRender);
			}
			GLRenderer.render += KillVolumeSystem.<>f__mg$cache0;
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060009EA RID: 2538 RVA: 0x00050A04 File Offset: 0x0004EE04
		// (set) Token: 0x060009EB RID: 2539 RVA: 0x00050A0B File Offset: 0x0004EE0B
		public static List<KillVolume> volumes { get; private set; } = new List<KillVolume>();

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060009EC RID: 2540 RVA: 0x00050A13 File Offset: 0x0004EE13
		// (set) Token: 0x060009ED RID: 2541 RVA: 0x00050A1A File Offset: 0x0004EE1A
		public static VolumeVisibilityGroup killVisibilityGroup { get; private set; } = new VolumeVisibilityGroup("kill_volumes", new TranslationReference("#SDG::Devkit.Visibility.Kill_Volumes"), true);

		// Token: 0x060009EE RID: 2542 RVA: 0x00050A22 File Offset: 0x0004EE22
		public static void addVolume(KillVolume volume)
		{
			KillVolumeSystem.volumes.Add(volume);
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x00050A2F File Offset: 0x0004EE2F
		public static void removeVolume(KillVolume volume)
		{
			KillVolumeSystem.volumes.RemoveFast(volume);
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x00050A3C File Offset: 0x0004EE3C
		private static void handleGLRender()
		{
			if (!KillVolumeSystem.killVisibilityGroup.isVisible)
			{
				return;
			}
			foreach (KillVolume killVolume in KillVolumeSystem.volumes)
			{
				GLUtility.matrix = killVolume.transform.localToWorldMatrix;
				GLUtility.volumeHelper(killVolume.isSelected, KillVolumeSystem.killVisibilityGroup);
			}
		}

		// Token: 0x04000746 RID: 1862
		[CompilerGenerated]
		private static GLRenderHandler <>f__mg$cache0;
	}
}
