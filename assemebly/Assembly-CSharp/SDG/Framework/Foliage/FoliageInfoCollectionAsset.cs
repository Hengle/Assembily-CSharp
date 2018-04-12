using System;
using System.Collections.Generic;
using SDG.Framework.Debug;
using SDG.Framework.Devkit;
using SDG.Framework.IO.FormattedFiles;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.Foliage
{
	// Token: 0x0200019D RID: 413
	public class FoliageInfoCollectionAsset : Asset, IDevkitAssetSpawnable
	{
		// Token: 0x06000C22 RID: 3106 RVA: 0x0005C387 File Offset: 0x0005A787
		public FoliageInfoCollectionAsset()
		{
			this.elements = new List<FoliageInfoCollectionAsset.FoliageInfoCollectionElement>();
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x0005C39A File Offset: 0x0005A79A
		public FoliageInfoCollectionAsset(Bundle bundle, Local localization, byte[] hash) : base(bundle, localization, hash)
		{
			this.elements = new List<FoliageInfoCollectionAsset.FoliageInfoCollectionElement>();
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x0005C3B0 File Offset: 0x0005A7B0
		public void devkitAssetSpawn()
		{
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x0005C3B4 File Offset: 0x0005A7B4
		public virtual void bakeFoliage(FoliageBakeSettings bakeSettings, IFoliageSurface surface, Bounds bounds, float weight)
		{
			foreach (FoliageInfoCollectionAsset.FoliageInfoCollectionElement foliageInfoCollectionElement in this.elements)
			{
				FoliageInfoAsset foliageInfoAsset = Assets.find<FoliageInfoAsset>(foliageInfoCollectionElement.asset);
				if (foliageInfoAsset != null)
				{
					foliageInfoAsset.bakeFoliage(bakeSettings, surface, bounds, weight, foliageInfoCollectionElement.weight);
				}
			}
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x0005C42C File Offset: 0x0005A82C
		protected override void readAsset(IFormattedFileReader reader)
		{
			base.readAsset(reader);
			int num = reader.readArrayLength("Foliage");
			for (int i = 0; i < num; i++)
			{
				this.elements.Add(reader.readValue<FoliageInfoCollectionAsset.FoliageInfoCollectionElement>(i));
			}
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x0005C470 File Offset: 0x0005A870
		protected override void writeAsset(IFormattedFileWriter writer)
		{
			base.writeAsset(writer);
			writer.beginArray("Foliage");
			for (int i = 0; i < this.elements.Count; i++)
			{
				writer.writeValue<FoliageInfoCollectionAsset.FoliageInfoCollectionElement>(this.elements[i]);
			}
			writer.endArray();
		}

		// Token: 0x04000886 RID: 2182
		[Inspectable("#SDG::Asset.Foliage.Collection.Elements.Name", null)]
		public List<FoliageInfoCollectionAsset.FoliageInfoCollectionElement> elements;

		// Token: 0x0200019E RID: 414
		public class FoliageInfoCollectionElement : IFormattedFileReadable, IFormattedFileWritable
		{
			// Token: 0x06000C28 RID: 3112 RVA: 0x0005C4C3 File Offset: 0x0005A8C3
			public FoliageInfoCollectionElement()
			{
				this.weight = 1f;
			}

			// Token: 0x06000C29 RID: 3113 RVA: 0x0005C4D6 File Offset: 0x0005A8D6
			public virtual void read(IFormattedFileReader reader)
			{
				reader = reader.readObject();
				if (reader == null)
				{
					return;
				}
				this.asset = reader.readValue<AssetReference<FoliageInfoAsset>>("Asset");
				this.weight = reader.readValue<float>("Weight");
			}

			// Token: 0x06000C2A RID: 3114 RVA: 0x0005C509 File Offset: 0x0005A909
			public virtual void write(IFormattedFileWriter writer)
			{
				writer.beginObject();
				writer.writeValue<AssetReference<FoliageInfoAsset>>("Asset", this.asset);
				writer.writeValue<float>("Weight", this.weight);
				writer.endObject();
			}

			// Token: 0x04000887 RID: 2183
			[Inspectable("#SDG::Asset.Foliage.Collection.Element.Asset.Name", null)]
			public AssetReference<FoliageInfoAsset> asset;

			// Token: 0x04000888 RID: 2184
			[Inspectable("#SDG::Asset.Foliage.Collection.Element.Weight.Name", null)]
			public float weight;
		}
	}
}
