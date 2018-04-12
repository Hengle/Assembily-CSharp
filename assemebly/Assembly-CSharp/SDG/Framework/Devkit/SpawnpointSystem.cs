using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using SDG.Framework.Devkit.Visibility;
using SDG.Framework.Rendering;
using SDG.Framework.Translations;
using UnityEngine;

namespace SDG.Framework.Devkit
{
	// Token: 0x0200015B RID: 347
	public static class SpawnpointSystem
	{
		// Token: 0x06000A53 RID: 2643 RVA: 0x000516D8 File Offset: 0x0004FAD8
		static SpawnpointSystem()
		{
			SpawnpointSystem.spawnpointVisibilityGroup = VisibilityManager.registerVisibilityGroup<VisibilityGroup>(SpawnpointSystem.spawnpointVisibilityGroup);
			if (SpawnpointSystem.<>f__mg$cache0 == null)
			{
				SpawnpointSystem.<>f__mg$cache0 = new GLRenderHandler(SpawnpointSystem.handleGLRender);
			}
			GLRenderer.render += SpawnpointSystem.<>f__mg$cache0;
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x0005173A File Offset: 0x0004FB3A
		// (set) Token: 0x06000A55 RID: 2645 RVA: 0x00051741 File Offset: 0x0004FB41
		public static List<Spawnpoint> spawnpoints { get; private set; } = new List<Spawnpoint>();

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x00051749 File Offset: 0x0004FB49
		// (set) Token: 0x06000A57 RID: 2647 RVA: 0x00051750 File Offset: 0x0004FB50
		public static VisibilityGroup spawnpointVisibilityGroup { get; private set; } = new VisibilityGroup("spawnpoints", new TranslationReference("#SDG::Devkit.Visibility.Spawnpoints"), true);

		// Token: 0x06000A58 RID: 2648 RVA: 0x00051758 File Offset: 0x0004FB58
		public static void addSpawnpoint(Spawnpoint spawnpoint)
		{
			SpawnpointSystem.spawnpoints.Add(spawnpoint);
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x00051768 File Offset: 0x0004FB68
		public static Spawnpoint getSpawnpoint(string id)
		{
			return SpawnpointSystem.spawnpoints.Find((Spawnpoint x) => x.id == id);
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00051798 File Offset: 0x0004FB98
		public static void removeSpawnpoint(Spawnpoint spawnpoint)
		{
			SpawnpointSystem.spawnpoints.Remove(spawnpoint);
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x000517A8 File Offset: 0x0004FBA8
		private static void handleGLRender()
		{
			if (!SpawnpointSystem.spawnpointVisibilityGroup.isVisible)
			{
				return;
			}
			GL.Begin(1);
			GLUtility.LINE_DEPTH_CHECKERED_COLOR.SetPass(0);
			foreach (Spawnpoint spawnpoint in SpawnpointSystem.spawnpoints)
			{
				GLUtility.matrix = spawnpoint.transform.localToWorldMatrix;
				GL.Color((!spawnpoint.isSelected) ? Color.red : Color.yellow);
				GLUtility.line(new Vector3(-0.5f, 0f, 0f), new Vector3(0.5f, 0f, 0f));
				GLUtility.line(new Vector3(0f, -0.5f, 0f), new Vector3(0f, 0.5f, 0f));
				GLUtility.line(new Vector3(0f, 0f, -0.5f), new Vector3(0f, 0f, 0.5f));
			}
			GL.End();
		}

		// Token: 0x04000759 RID: 1881
		[CompilerGenerated]
		private static GLRenderHandler <>f__mg$cache0;
	}
}
