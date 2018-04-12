using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using SDG.Framework.Devkit.Visibility;
using SDG.Framework.Rendering;
using SDG.Framework.Translations;
using UnityEngine;

namespace SDG.Framework.Foliage
{
	// Token: 0x020001B1 RID: 433
	public static class FoliageVolumeSystem
	{
		// Token: 0x06000CF3 RID: 3315 RVA: 0x0005F4F0 File Offset: 0x0005D8F0
		static FoliageVolumeSystem()
		{
			FoliageVolumeSystem.foliageVisibilityGroup.color = new Color32(44, 114, 34, byte.MaxValue);
			FoliageVolumeSystem.foliageVisibilityGroup = VisibilityManager.registerVisibilityGroup<VolumeVisibilityGroup>(FoliageVolumeSystem.foliageVisibilityGroup);
			if (FoliageVolumeSystem.<>f__mg$cache0 == null)
			{
				FoliageVolumeSystem.<>f__mg$cache0 = new GLRenderHandler(FoliageVolumeSystem.handleGLRender);
			}
			GLRenderer.render += FoliageVolumeSystem.<>f__mg$cache0;
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x0005F57B File Offset: 0x0005D97B
		// (set) Token: 0x06000CF5 RID: 3317 RVA: 0x0005F582 File Offset: 0x0005D982
		public static List<FoliageVolume> additiveVolumes { get; private set; } = new List<FoliageVolume>();

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x0005F58A File Offset: 0x0005D98A
		// (set) Token: 0x06000CF7 RID: 3319 RVA: 0x0005F591 File Offset: 0x0005D991
		public static List<FoliageVolume> subtractiveVolumes { get; private set; } = new List<FoliageVolume>();

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x0005F599 File Offset: 0x0005D999
		// (set) Token: 0x06000CF9 RID: 3321 RVA: 0x0005F5A0 File Offset: 0x0005D9A0
		public static VolumeVisibilityGroup foliageVisibilityGroup { get; private set; } = new VolumeVisibilityGroup("foliage_volumes", new TranslationReference("#SDG::Devkit.Visibility.Foliage_Volumes"), true);

		// Token: 0x06000CFA RID: 3322 RVA: 0x0005F5A8 File Offset: 0x0005D9A8
		public static void addVolume(FoliageVolume volume)
		{
			if (volume.mode == FoliageVolume.EFoliageVolumeMode.ADDITIVE)
			{
				FoliageVolumeSystem.additiveVolumes.Add(volume);
			}
			else
			{
				FoliageVolumeSystem.subtractiveVolumes.Add(volume);
			}
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x0005F5D0 File Offset: 0x0005D9D0
		public static void removeVolume(FoliageVolume volume)
		{
			if (volume.mode == FoliageVolume.EFoliageVolumeMode.ADDITIVE)
			{
				FoliageVolumeSystem.additiveVolumes.RemoveFast(volume);
			}
			else
			{
				FoliageVolumeSystem.subtractiveVolumes.RemoveFast(volume);
			}
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x0005F5F8 File Offset: 0x0005D9F8
		private static void handleGLRender()
		{
			if (!FoliageVolumeSystem.foliageVisibilityGroup.isVisible)
			{
				return;
			}
			foreach (FoliageVolume foliageVolume in FoliageVolumeSystem.additiveVolumes)
			{
				GLUtility.matrix = foliageVolume.transform.localToWorldMatrix;
				GLUtility.volumeHelper(foliageVolume.isSelected, FoliageVolumeSystem.foliageVisibilityGroup);
			}
			foreach (FoliageVolume foliageVolume2 in FoliageVolumeSystem.subtractiveVolumes)
			{
				GLUtility.matrix = foliageVolume2.transform.localToWorldMatrix;
				GLUtility.volumeHelper(foliageVolume2.isSelected, FoliageVolumeSystem.foliageVisibilityGroup);
			}
		}

		// Token: 0x040008DA RID: 2266
		[CompilerGenerated]
		private static GLRenderHandler <>f__mg$cache0;
	}
}
