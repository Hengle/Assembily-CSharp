using System;
using SDG.Framework.Debug;
using SDG.Framework.Devkit;
using SDG.Framework.IO.FormattedFiles;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020003F7 RID: 1015
	public class MaterialPaletteAsset : Asset, IDevkitAssetSpawnable
	{
		// Token: 0x06001B5C RID: 7004 RVA: 0x00097860 File Offset: 0x00095C60
		public MaterialPaletteAsset()
		{
			this.materials = new InspectableList<ContentReference<Material>>();
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x00097873 File Offset: 0x00095C73
		public MaterialPaletteAsset(Bundle bundle, Local localization, byte[] hash) : base(bundle, localization, hash)
		{
			this.materials = new InspectableList<ContentReference<Material>>();
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x00097889 File Offset: 0x00095C89
		public void devkitAssetSpawn()
		{
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x0009788C File Offset: 0x00095C8C
		protected override void readAsset(IFormattedFileReader reader)
		{
			base.readAsset(reader);
			int num = reader.readArrayLength("Materials");
			for (int i = 0; i < num; i++)
			{
				this.materials.Add(reader.readValue<ContentReference<Material>>(i));
			}
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x000978D0 File Offset: 0x00095CD0
		protected override void writeAsset(IFormattedFileWriter writer)
		{
			base.writeAsset(writer);
			writer.beginArray("Materials");
			for (int i = 0; i < this.materials.Count; i++)
			{
				writer.writeValue<ContentReference<Material>>(this.materials[i]);
			}
			writer.endArray();
		}

		// Token: 0x0400103D RID: 4157
		[Inspectable("#SDG::Asset.Material_Palette.Materials.Name", null)]
		public InspectableList<ContentReference<Material>> materials;
	}
}
