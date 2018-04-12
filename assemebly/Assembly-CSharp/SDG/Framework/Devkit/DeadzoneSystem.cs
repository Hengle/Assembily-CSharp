using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using SDG.Framework.Devkit.Visibility;
using SDG.Framework.Rendering;
using SDG.Framework.Translations;
using UnityEngine;

namespace SDG.Framework.Devkit
{
	// Token: 0x02000125 RID: 293
	public static class DeadzoneSystem
	{
		// Token: 0x060008F3 RID: 2291 RVA: 0x0004E25C File Offset: 0x0004C65C
		static DeadzoneSystem()
		{
			DeadzoneSystem.deadzoneVisibilityGroup.color = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
			DeadzoneSystem.deadzoneVisibilityGroup = VisibilityManager.registerVisibilityGroup<VolumeVisibilityGroup>(DeadzoneSystem.deadzoneVisibilityGroup);
			if (DeadzoneSystem.<>f__mg$cache0 == null)
			{
				DeadzoneSystem.<>f__mg$cache0 = new GLRenderHandler(DeadzoneSystem.handleGLRender);
			}
			GLRenderer.render += DeadzoneSystem.<>f__mg$cache0;
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060008F4 RID: 2292 RVA: 0x0004E2DE File Offset: 0x0004C6DE
		// (set) Token: 0x060008F5 RID: 2293 RVA: 0x0004E2E5 File Offset: 0x0004C6E5
		public static List<DeadzoneVolume> volumes { get; private set; } = new List<DeadzoneVolume>();

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060008F6 RID: 2294 RVA: 0x0004E2ED File Offset: 0x0004C6ED
		// (set) Token: 0x060008F7 RID: 2295 RVA: 0x0004E2F4 File Offset: 0x0004C6F4
		public static VolumeVisibilityGroup deadzoneVisibilityGroup { get; private set; } = new VolumeVisibilityGroup("deadzone_volumes", new TranslationReference("#SDG::Devkit.Visibility.Deadzone_Volumes"), true);

		// Token: 0x060008F8 RID: 2296 RVA: 0x0004E2FC File Offset: 0x0004C6FC
		public static void addVolume(DeadzoneVolume volume)
		{
			DeadzoneSystem.volumes.Add(volume);
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0004E309 File Offset: 0x0004C709
		public static void removeVolume(DeadzoneVolume volume)
		{
			DeadzoneSystem.volumes.RemoveFast(volume);
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0004E318 File Offset: 0x0004C718
		private static void handleGLRender()
		{
			if (!DeadzoneSystem.deadzoneVisibilityGroup.isVisible)
			{
				return;
			}
			foreach (DeadzoneVolume deadzoneVolume in DeadzoneSystem.volumes)
			{
				GLUtility.matrix = deadzoneVolume.transform.localToWorldMatrix;
				GLUtility.volumeHelper(deadzoneVolume.isSelected, DeadzoneSystem.deadzoneVisibilityGroup);
			}
		}

		// Token: 0x04000704 RID: 1796
		[CompilerGenerated]
		private static GLRenderHandler <>f__mg$cache0;
	}
}
