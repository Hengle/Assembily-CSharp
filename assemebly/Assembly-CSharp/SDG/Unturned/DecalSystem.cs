using System;
using System.Collections.Generic;
using SDG.Framework.Devkit.Visibility;
using SDG.Framework.Translations;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000484 RID: 1156
	public static class DecalSystem
	{
		// Token: 0x06001E88 RID: 7816 RVA: 0x000A76F4 File Offset: 0x000A5AF4
		static DecalSystem()
		{
			DecalSystem.decalVisibilityGroup.color = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
			DecalSystem.decalVisibilityGroup = VisibilityManager.registerVisibilityGroup<VolumeVisibilityGroup>(DecalSystem.decalVisibilityGroup);
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001E89 RID: 7817 RVA: 0x000A7754 File Offset: 0x000A5B54
		// (set) Token: 0x06001E8A RID: 7818 RVA: 0x000A775B File Offset: 0x000A5B5B
		public static VolumeVisibilityGroup decalVisibilityGroup { get; private set; } = new VolumeVisibilityGroup("decal_volumes", new TranslationReference("#SDG::Devkit.Visibility.Decal_Volumes"), true);

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06001E8B RID: 7819 RVA: 0x000A7763 File Offset: 0x000A5B63
		public static HashSet<Decal> decalsDiffuse
		{
			get
			{
				return DecalSystem._decalsDiffuse;
			}
		}

		// Token: 0x06001E8C RID: 7820 RVA: 0x000A776C File Offset: 0x000A5B6C
		public static void add(Decal decal)
		{
			if (decal == null)
			{
				return;
			}
			DecalSystem.remove(decal);
			EDecalType type = decal.type;
			if (type == EDecalType.DIFFUSE)
			{
				DecalSystem.decalsDiffuse.Add(decal);
			}
		}

		// Token: 0x06001E8D RID: 7821 RVA: 0x000A77B0 File Offset: 0x000A5BB0
		public static void remove(Decal decal)
		{
			if (decal == null)
			{
				return;
			}
			EDecalType type = decal.type;
			if (type == EDecalType.DIFFUSE)
			{
				DecalSystem.decalsDiffuse.Remove(decal);
			}
		}

		// Token: 0x04001220 RID: 4640
		private static HashSet<Decal> _decalsDiffuse = new HashSet<Decal>();
	}
}
