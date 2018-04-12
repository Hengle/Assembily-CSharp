using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using SDG.Framework.Devkit.Visibility;
using SDG.Framework.Rendering;
using SDG.Framework.Translations;
using SDG.Framework.Utilities;
using UnityEngine;

namespace SDG.Framework.Landscapes
{
	// Token: 0x020001DD RID: 477
	public static class LandscapeHoleSystem
	{
		// Token: 0x06000E4E RID: 3662 RVA: 0x00063458 File Offset: 0x00061858
		static LandscapeHoleSystem()
		{
			LandscapeHoleSystem._Landscape_Holes_Count = Shader.PropertyToID("_Landscape_Holes_Count");
			LandscapeHoleSystem._Landscape_Holes_List = Shader.PropertyToID("_Landscape_Holes_List");
			LandscapeHoleSystem.landscapeHoles = new Matrix4x4[LandscapeHoleSystem.MAX_LANDSCAPE_HOLES];
			LandscapeHoleSystem.holeVisibilityGroup = new VolumeVisibilityGroup("landscape_hole_volumes", new TranslationReference("#SDG::Devkit.Visibility.Landscape_Hole_Volumes"), true);
			LandscapeHoleSystem.holeVisibilityGroup.color = new Color32(71, 44, 20, byte.MaxValue);
			LandscapeHoleSystem.holeVisibilityGroup = VisibilityManager.registerVisibilityGroup<VolumeVisibilityGroup>(LandscapeHoleSystem.holeVisibilityGroup);
			if (LandscapeHoleSystem.<>f__mg$cache0 == null)
			{
				LandscapeHoleSystem.<>f__mg$cache0 = new UpdateHandler(LandscapeHoleSystem.handleUpdated);
			}
			TimeUtility.updated += LandscapeHoleSystem.<>f__mg$cache0;
			if (LandscapeHoleSystem.<>f__mg$cache1 == null)
			{
				LandscapeHoleSystem.<>f__mg$cache1 = new GLRenderHandler(LandscapeHoleSystem.handleGLRender);
			}
			GLRenderer.render += LandscapeHoleSystem.<>f__mg$cache1;
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000E4F RID: 3663 RVA: 0x0006353B File Offset: 0x0006193B
		// (set) Token: 0x06000E50 RID: 3664 RVA: 0x00063542 File Offset: 0x00061942
		public static List<LandscapeHoleVolume> volumes { get; private set; } = new List<LandscapeHoleVolume>();

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000E51 RID: 3665 RVA: 0x0006354A File Offset: 0x0006194A
		// (set) Token: 0x06000E52 RID: 3666 RVA: 0x00063551 File Offset: 0x00061951
		public static VolumeVisibilityGroup holeVisibilityGroup { get; private set; }

		// Token: 0x06000E53 RID: 3667 RVA: 0x0006355C File Offset: 0x0006195C
		public static void addVolume(LandscapeHoleVolume volume)
		{
			if (LandscapeHoleSystem.volumes.Count >= LandscapeHoleSystem.MAX_LANDSCAPE_HOLES)
			{
				return;
			}
			LandscapeHoleSystem.landscapeHoles[LandscapeHoleSystem.volumes.Count] = volume.transform.worldToLocalMatrix;
			LandscapeHoleSystem.volumes.Add(volume);
			LandscapeHoleSystem.sendToGPU();
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x000635B4 File Offset: 0x000619B4
		public static void removeVolume(LandscapeHoleVolume volume)
		{
			int num = LandscapeHoleSystem.volumes.IndexOf(volume);
			if (num < 0)
			{
				return;
			}
			LandscapeHoleSystem.volumes.RemoveAt(num);
			LandscapeHoleSystem.landscapeHoles[num] = LandscapeHoleSystem.landscapeHoles[LandscapeHoleSystem.volumes.Count];
			LandscapeHoleSystem.sendToGPU();
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x0006360E File Offset: 0x00061A0E
		private static void sendToGPU()
		{
			Shader.SetGlobalInt(LandscapeHoleSystem._Landscape_Holes_Count, LandscapeHoleSystem.volumes.Count);
			Shader.SetGlobalMatrixArray(LandscapeHoleSystem._Landscape_Holes_List, LandscapeHoleSystem.landscapeHoles);
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x00063634 File Offset: 0x00061A34
		private static void handleUpdated()
		{
			bool flag = false;
			for (int i = 0; i < LandscapeHoleSystem.volumes.Count; i++)
			{
				LandscapeHoleVolume landscapeHoleVolume = LandscapeHoleSystem.volumes[i];
				if (landscapeHoleVolume.transform.hasChanged)
				{
					LandscapeHoleSystem.landscapeHoles[i] = landscapeHoleVolume.transform.worldToLocalMatrix;
					landscapeHoleVolume.transform.hasChanged = false;
					flag = true;
				}
			}
			if (flag)
			{
				LandscapeHoleSystem.sendToGPU();
			}
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x000636B0 File Offset: 0x00061AB0
		private static void handleGLRender()
		{
			if (!LandscapeHoleSystem.holeVisibilityGroup.isVisible)
			{
				return;
			}
			foreach (LandscapeHoleVolume landscapeHoleVolume in LandscapeHoleSystem.volumes)
			{
				GLUtility.matrix = landscapeHoleVolume.transform.localToWorldMatrix;
				GLUtility.volumeHelper(landscapeHoleVolume.isSelected, LandscapeHoleSystem.holeVisibilityGroup);
			}
		}

		// Token: 0x04000922 RID: 2338
		private static readonly int MAX_LANDSCAPE_HOLES = 16;

		// Token: 0x04000925 RID: 2341
		private static int _Landscape_Holes_Count = -1;

		// Token: 0x04000926 RID: 2342
		private static int _Landscape_Holes_List = -1;

		// Token: 0x04000927 RID: 2343
		private static Matrix4x4[] landscapeHoles;

		// Token: 0x04000928 RID: 2344
		[CompilerGenerated]
		private static UpdateHandler <>f__mg$cache0;

		// Token: 0x04000929 RID: 2345
		[CompilerGenerated]
		private static GLRenderHandler <>f__mg$cache1;
	}
}
