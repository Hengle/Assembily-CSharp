using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using SDG.Framework.Devkit.Visibility;
using SDG.Framework.Rendering;
using SDG.Framework.Translations;
using UnityEngine;

namespace SDG.Framework.Devkit
{
	// Token: 0x02000122 RID: 290
	public static class AmbianceSystem
	{
		// Token: 0x060008CE RID: 2254 RVA: 0x0004DBF0 File Offset: 0x0004BFF0
		static AmbianceSystem()
		{
			AmbianceSystem.ambianceVisibilityGroup.color = new Color32(0, 127, 127, byte.MaxValue);
			AmbianceSystem.ambianceVisibilityGroup = VisibilityManager.registerVisibilityGroup<VolumeVisibilityGroup>(AmbianceSystem.ambianceVisibilityGroup);
			if (AmbianceSystem.<>f__mg$cache0 == null)
			{
				AmbianceSystem.<>f__mg$cache0 = new GLRenderHandler(AmbianceSystem.handleGLRender);
			}
			GLRenderer.render += AmbianceSystem.<>f__mg$cache0;
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x0004DC70 File Offset: 0x0004C070
		// (set) Token: 0x060008D0 RID: 2256 RVA: 0x0004DC77 File Offset: 0x0004C077
		public static List<AmbianceVolume> volumes { get; private set; } = new List<AmbianceVolume>();

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060008D1 RID: 2257 RVA: 0x0004DC7F File Offset: 0x0004C07F
		// (set) Token: 0x060008D2 RID: 2258 RVA: 0x0004DC86 File Offset: 0x0004C086
		public static VolumeVisibilityGroup ambianceVisibilityGroup { get; private set; } = new VolumeVisibilityGroup("ambiance_volumes", new TranslationReference("#SDG::Devkit.Visibility.Ambiance_Volumes"), true);

		// Token: 0x060008D3 RID: 2259 RVA: 0x0004DC8E File Offset: 0x0004C08E
		public static void addVolume(AmbianceVolume volume)
		{
			AmbianceSystem.volumes.Add(volume);
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0004DC9B File Offset: 0x0004C09B
		public static void removeVolume(AmbianceVolume volume)
		{
			AmbianceSystem.volumes.RemoveFast(volume);
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0004DCA8 File Offset: 0x0004C0A8
		private static void handleGLRender()
		{
			if (!AmbianceSystem.ambianceVisibilityGroup.isVisible)
			{
				return;
			}
			foreach (AmbianceVolume ambianceVolume in AmbianceSystem.volumes)
			{
				GLUtility.matrix = ambianceVolume.transform.localToWorldMatrix;
				GLUtility.volumeHelper(ambianceVolume.isSelected, AmbianceSystem.ambianceVisibilityGroup);
			}
		}

		// Token: 0x040006F9 RID: 1785
		[CompilerGenerated]
		private static GLRenderHandler <>f__mg$cache0;
	}
}
