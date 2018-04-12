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
	// Token: 0x020001A2 RID: 418
	public class FoliageObjectInfoAsset : FoliageInfoAsset, IDevkitAssetSpawnable
	{
		// Token: 0x06000C50 RID: 3152 RVA: 0x0005CE0D File Offset: 0x0005B20D
		public FoliageObjectInfoAsset()
		{
			this.resetObject();
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x0005CE1B File Offset: 0x0005B21B
		public FoliageObjectInfoAsset(Bundle bundle, Local localization, byte[] hash) : base(bundle, localization, hash)
		{
			this.resetObject();
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x0005CE2C File Offset: 0x0005B22C
		public void devkitAssetSpawn()
		{
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x0005CE2E File Offset: 0x0005B22E
		public override void bakeFoliage(FoliageBakeSettings bakeSettings, IFoliageSurface surface, Bounds bounds, float surfaceWeight, float collectionWeight)
		{
			if (!bakeSettings.bakeObjects)
			{
				return;
			}
			if (bakeSettings.bakeClear)
			{
				return;
			}
			base.bakeFoliage(bakeSettings, surface, bounds, surfaceWeight, collectionWeight);
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x0005CE58 File Offset: 0x0005B258
		public override int getInstanceCountInVolume(IShapeVolume volume)
		{
			Bounds worldBounds = volume.worldBounds;
			RegionBounds regionBounds = new RegionBounds(worldBounds);
			int num = 0;
			for (byte b = regionBounds.min.x; b <= regionBounds.max.x; b += 1)
			{
				for (byte b2 = regionBounds.min.y; b2 <= regionBounds.max.y; b2 += 1)
				{
					List<LevelObject> list = LevelObjects.objects[(int)b, (int)b2];
					foreach (LevelObject levelObject in list)
					{
						if (this.obj.isReferenceTo(levelObject.asset))
						{
							if (volume.containsPoint(levelObject.transform.position))
							{
								num++;
							}
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x0005CF5C File Offset: 0x0005B35C
		protected override void addFoliage(Vector3 position, Quaternion rotation, Vector3 scale, bool clearWhenBaked)
		{
			ObjectAsset objectAsset = Assets.find<ObjectAsset>(this.obj);
			if (objectAsset == null)
			{
				return;
			}
			LevelObjects.addDevkitObject(objectAsset.GUID, position, rotation, scale, (!clearWhenBaked) ? ELevelObjectPlacementOrigin.PAINTED : ELevelObjectPlacementOrigin.GENERATED);
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x0005CF99 File Offset: 0x0005B399
		protected override bool isPositionValid(Vector3 position)
		{
			return FoliageVolumeUtility.isPointValid(position, false, false, true);
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x0005CFAC File Offset: 0x0005B3AC
		protected override void readAsset(IFormattedFileReader reader)
		{
			base.readAsset(reader);
			this.obj = reader.readValue<AssetReference<ObjectAsset>>("Object");
			if (reader.containsKey("Obstruction_Radius"))
			{
				this.obstructionRadius = reader.readValue<float>("Obstruction_Radius");
			}
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x0005CFE7 File Offset: 0x0005B3E7
		protected override void writeAsset(IFormattedFileWriter writer)
		{
			base.writeAsset(writer);
			writer.writeValue<AssetReference<ObjectAsset>>("Object", this.obj);
			writer.writeValue<float>("Obstruction_Radius", this.obstructionRadius);
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x0005D012 File Offset: 0x0005B412
		protected virtual void resetObject()
		{
			this.obstructionRadius = 4f;
		}

		// Token: 0x0400089A RID: 2202
		[Inspectable("#SDG::Asset.Foliage.Info.Object.Name", null)]
		public AssetReference<ObjectAsset> obj;

		// Token: 0x0400089B RID: 2203
		[Inspectable("#SDG::Asset.Foliage.Info.Obstruction_Radius.Name", null)]
		public float obstructionRadius;
	}
}
