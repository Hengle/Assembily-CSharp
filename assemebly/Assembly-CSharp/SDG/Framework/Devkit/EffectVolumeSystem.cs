using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using SDG.Framework.Devkit.Visibility;
using SDG.Framework.Rendering;
using SDG.Framework.Translations;
using UnityEngine;

namespace SDG.Framework.Devkit
{
	// Token: 0x0200013D RID: 317
	public static class EffectVolumeSystem
	{
		// Token: 0x060009BF RID: 2495 RVA: 0x000504DC File Offset: 0x0004E8DC
		static EffectVolumeSystem()
		{
			EffectVolumeSystem.effectVisibilityGroup.color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
			EffectVolumeSystem.effectVisibilityGroup = VisibilityManager.registerVisibilityGroup<VolumeVisibilityGroup>(EffectVolumeSystem.effectVisibilityGroup);
			if (EffectVolumeSystem.<>f__mg$cache0 == null)
			{
				EffectVolumeSystem.<>f__mg$cache0 = new GLRenderHandler(EffectVolumeSystem.handleGLRender);
			}
			GLRenderer.render += EffectVolumeSystem.<>f__mg$cache0;
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060009C0 RID: 2496 RVA: 0x00050566 File Offset: 0x0004E966
		// (set) Token: 0x060009C1 RID: 2497 RVA: 0x0005056D File Offset: 0x0004E96D
		public static List<EffectVolume> volumes { get; private set; } = new List<EffectVolume>();

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x00050575 File Offset: 0x0004E975
		// (set) Token: 0x060009C3 RID: 2499 RVA: 0x0005057C File Offset: 0x0004E97C
		public static VolumeVisibilityGroup effectVisibilityGroup { get; private set; } = new VolumeVisibilityGroup("effect_volumes", new TranslationReference("#SDG::Devkit.Visibility.Effect_Volumes"), true);

		// Token: 0x060009C4 RID: 2500 RVA: 0x00050584 File Offset: 0x0004E984
		public static void addVolume(EffectVolume volume)
		{
			EffectVolumeSystem.volumes.Add(volume);
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x00050591 File Offset: 0x0004E991
		public static void removeVolume(EffectVolume volume)
		{
			EffectVolumeSystem.volumes.Remove(volume);
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x000505A0 File Offset: 0x0004E9A0
		private static void handleGLRender()
		{
			if (!EffectVolumeSystem.effectVisibilityGroup.isVisible)
			{
				return;
			}
			foreach (EffectVolume effectVolume in EffectVolumeSystem.volumes)
			{
				GLUtility.matrix = effectVolume.transform.localToWorldMatrix;
				GLUtility.volumeHelper(effectVolume.isSelected, EffectVolumeSystem.effectVisibilityGroup);
			}
		}

		// Token: 0x0400073C RID: 1852
		[CompilerGenerated]
		private static GLRenderHandler <>f__mg$cache0;
	}
}
