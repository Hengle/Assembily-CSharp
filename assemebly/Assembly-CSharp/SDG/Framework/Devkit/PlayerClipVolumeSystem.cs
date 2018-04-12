using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using SDG.Framework.Devkit.Visibility;
using SDG.Framework.Rendering;
using SDG.Framework.Translations;
using UnityEngine;

namespace SDG.Framework.Devkit
{
	// Token: 0x02000158 RID: 344
	public static class PlayerClipVolumeSystem
	{
		// Token: 0x06000A37 RID: 2615 RVA: 0x00051334 File Offset: 0x0004F734
		static PlayerClipVolumeSystem()
		{
			PlayerClipVolumeSystem.playerClipVisibilityGroup.color = new Color32(63, 0, 0, byte.MaxValue);
			PlayerClipVolumeSystem.playerClipVisibilityGroup = VisibilityManager.registerVisibilityGroup<VolumeVisibilityGroup>(PlayerClipVolumeSystem.playerClipVisibilityGroup);
			PlayerClipVolumeSystem.navClipVisibilityGroup = new VolumeVisibilityGroup("nav_clip_volumes", new TranslationReference("#SDG::Devkit.Visibility.Nav_Clip_Volumes"), true);
			PlayerClipVolumeSystem.navClipVisibilityGroup.color = new Color32(63, 63, 0, byte.MaxValue);
			PlayerClipVolumeSystem.navClipVisibilityGroup = VisibilityManager.registerVisibilityGroup<VolumeVisibilityGroup>(PlayerClipVolumeSystem.navClipVisibilityGroup);
			if (PlayerClipVolumeSystem.<>f__mg$cache0 == null)
			{
				PlayerClipVolumeSystem.<>f__mg$cache0 = new GLRenderHandler(PlayerClipVolumeSystem.handleGLRender);
			}
			GLRenderer.render += PlayerClipVolumeSystem.<>f__mg$cache0;
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000A38 RID: 2616 RVA: 0x000513FA File Offset: 0x0004F7FA
		// (set) Token: 0x06000A39 RID: 2617 RVA: 0x00051401 File Offset: 0x0004F801
		public static List<DevkitHierarchyVolume> volumes { get; private set; } = new List<DevkitHierarchyVolume>();

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000A3A RID: 2618 RVA: 0x00051409 File Offset: 0x0004F809
		// (set) Token: 0x06000A3B RID: 2619 RVA: 0x00051410 File Offset: 0x0004F810
		public static VolumeVisibilityGroup playerClipVisibilityGroup { get; private set; } = new VolumeVisibilityGroup("player_clip_volumes", new TranslationReference("#SDG::Devkit.Visibility.Player_Clip_Volumes"), true);

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000A3C RID: 2620 RVA: 0x00051418 File Offset: 0x0004F818
		// (set) Token: 0x06000A3D RID: 2621 RVA: 0x0005141F File Offset: 0x0004F81F
		public static VolumeVisibilityGroup navClipVisibilityGroup { get; private set; }

		// Token: 0x06000A3E RID: 2622 RVA: 0x00051427 File Offset: 0x0004F827
		public static void addVolume(DevkitHierarchyVolume volume)
		{
			PlayerClipVolumeSystem.volumes.Add(volume);
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x00051434 File Offset: 0x0004F834
		public static void removeVolume(DevkitHierarchyVolume volume)
		{
			PlayerClipVolumeSystem.volumes.Remove(volume);
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x00051444 File Offset: 0x0004F844
		private static void handleGLRender()
		{
			foreach (DevkitHierarchyVolume devkitHierarchyVolume in PlayerClipVolumeSystem.volumes)
			{
				if (devkitHierarchyVolume.visibilityGroupOverride.isVisible)
				{
					GLUtility.matrix = devkitHierarchyVolume.transform.localToWorldMatrix;
					GLUtility.volumeHelper(devkitHierarchyVolume.isSelected, devkitHierarchyVolume.visibilityGroupOverride);
				}
			}
		}

		// Token: 0x04000753 RID: 1875
		[CompilerGenerated]
		private static GLRenderHandler <>f__mg$cache0;
	}
}
