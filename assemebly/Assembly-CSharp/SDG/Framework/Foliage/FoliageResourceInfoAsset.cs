using System;
using System.Collections.Generic;
using SDG.Framework.Debug;
using SDG.Framework.Devkit;
using SDG.Framework.IO.FormattedFiles;
using SDG.Framework.Utilities;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.Foliage
{
	// Token: 0x020001A4 RID: 420
	public class FoliageResourceInfoAsset : FoliageInfoAsset, IDevkitAssetSpawnable
	{
		// Token: 0x06000C5B RID: 3163 RVA: 0x0005D02F File Offset: 0x0005B42F
		public FoliageResourceInfoAsset()
		{
			this.resetResource();
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x0005D03D File Offset: 0x0005B43D
		public FoliageResourceInfoAsset(Bundle bundle, Local localization, byte[] hash) : base(bundle, localization, hash)
		{
			this.resetResource();
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x0005D04E File Offset: 0x0005B44E
		public void devkitAssetSpawn()
		{
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x0005D050 File Offset: 0x0005B450
		public override void bakeFoliage(FoliageBakeSettings bakeSettings, IFoliageSurface surface, Bounds bounds, float surfaceWeight, float collectionWeight)
		{
			if (!bakeSettings.bakeResources)
			{
				return;
			}
			if (bakeSettings.bakeClear)
			{
				return;
			}
			base.bakeFoliage(bakeSettings, surface, bounds, surfaceWeight, collectionWeight);
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x0005D07C File Offset: 0x0005B47C
		public override int getInstanceCountInVolume(IShapeVolume volume)
		{
			Bounds worldBounds = volume.worldBounds;
			RegionBounds regionBounds = new RegionBounds(worldBounds);
			int num = 0;
			for (byte b = regionBounds.min.x; b <= regionBounds.max.x; b += 1)
			{
				for (byte b2 = regionBounds.min.y; b2 <= regionBounds.max.y; b2 += 1)
				{
					List<ResourceSpawnpoint> list = LevelGround.trees[(int)b, (int)b2];
					foreach (ResourceSpawnpoint resourceSpawnpoint in list)
					{
						if (this.resource.isReferenceTo(resourceSpawnpoint.asset))
						{
							if (volume.containsPoint(resourceSpawnpoint.point))
							{
								num++;
							}
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x0005D178 File Offset: 0x0005B578
		protected override void addFoliage(Vector3 position, Quaternion rotation, Vector3 scale, bool clearWhenBaked)
		{
			ResourceAsset resourceAsset = Assets.find<ResourceAsset>(this.resource);
			if (resourceAsset == null)
			{
				return;
			}
			byte b = 0;
			while ((int)b < LevelGround.resources.Length)
			{
				GroundResource groundResource = LevelGround.resources[(int)b];
				if (groundResource.id == resourceAsset.id)
				{
					break;
				}
				b += 1;
			}
			LevelGround.addSpawn(position, b, clearWhenBaked);
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x0005D1D8 File Offset: 0x0005B5D8
		protected override bool isPositionValid(Vector3 position)
		{
			if (!FoliageVolumeUtility.isPointValid(position, false, true, false))
			{
				return false;
			}
			int num = Physics.OverlapSphereNonAlloc(position, this.obstructionRadius, FoliageResourceInfoAsset.OBSTRUCTION_COLLIDERS, RayMasks.BLOCK_RESOURCE);
			for (int i = 0; i < num; i++)
			{
				ObjectAsset asset = LevelObjects.getAsset(FoliageResourceInfoAsset.OBSTRUCTION_COLLIDERS[i].transform);
				if (asset != null && !asset.isSnowshoe)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x0005D244 File Offset: 0x0005B644
		protected override void readAsset(IFormattedFileReader reader)
		{
			base.readAsset(reader);
			this.resource = reader.readValue<AssetReference<ResourceAsset>>("Resource");
			if (reader.containsKey("Obstruction_Radius"))
			{
				this.obstructionRadius = reader.readValue<float>("Obstruction_Radius");
			}
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x0005D27F File Offset: 0x0005B67F
		protected override void writeAsset(IFormattedFileWriter writer)
		{
			base.writeAsset(writer);
			writer.writeValue<AssetReference<ResourceAsset>>("Resource", this.resource);
			writer.writeValue<float>("Obstruction_Radius", this.obstructionRadius);
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x0005D2AA File Offset: 0x0005B6AA
		protected virtual void resetResource()
		{
			this.obstructionRadius = 4f;
		}

		// Token: 0x0400089E RID: 2206
		private static readonly Collider[] OBSTRUCTION_COLLIDERS = new Collider[16];

		// Token: 0x0400089F RID: 2207
		[Inspectable("#SDG::Asset.Foliage.Info.Resource.Name", null)]
		public AssetReference<ResourceAsset> resource;

		// Token: 0x040008A0 RID: 2208
		[Inspectable("#SDG::Asset.Foliage.Info.Obstruction_Radius.Name", null)]
		public float obstructionRadius;
	}
}
