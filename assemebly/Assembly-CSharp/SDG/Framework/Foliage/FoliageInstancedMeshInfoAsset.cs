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
	// Token: 0x020001A1 RID: 417
	public class FoliageInstancedMeshInfoAsset : FoliageInfoAsset, IDevkitAssetSpawnable
	{
		// Token: 0x06000C46 RID: 3142 RVA: 0x0005CAF8 File Offset: 0x0005AEF8
		public FoliageInstancedMeshInfoAsset()
		{
			this.resetInstancedMeshInfo();
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x0005CB06 File Offset: 0x0005AF06
		public FoliageInstancedMeshInfoAsset(Bundle bundle, Local localization, byte[] hash) : base(bundle, localization, hash)
		{
			this.resetInstancedMeshInfo();
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x0005CB17 File Offset: 0x0005AF17
		public void devkitAssetSpawn()
		{
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x0005CB19 File Offset: 0x0005AF19
		public override void bakeFoliage(FoliageBakeSettings bakeSettings, IFoliageSurface surface, Bounds bounds, float surfaceWeight, float collectionWeight)
		{
			if (!bakeSettings.bakeInstancesMeshes)
			{
				return;
			}
			if (bakeSettings.bakeClear)
			{
				return;
			}
			base.bakeFoliage(bakeSettings, surface, bounds, surfaceWeight, collectionWeight);
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x0005CB44 File Offset: 0x0005AF44
		public override int getInstanceCountInVolume(IShapeVolume volume)
		{
			Bounds worldBounds = volume.worldBounds;
			FoliageBounds foliageBounds = new FoliageBounds(worldBounds);
			int num = 0;
			for (int i = foliageBounds.min.x; i <= foliageBounds.max.x; i++)
			{
				for (int j = foliageBounds.min.y; j <= foliageBounds.max.y; j++)
				{
					FoliageCoord tileCoord = new FoliageCoord(i, j);
					FoliageTile tile = FoliageSystem.getTile(tileCoord);
					if (tile != null)
					{
						if (!tile.hasInstances)
						{
							tile.readInstancesOnThread();
						}
						FoliageInstanceList foliageInstanceList;
						if (tile.instances != null && tile.instances.TryGetValue(base.getReferenceTo<FoliageInstancedMeshInfoAsset>(), out foliageInstanceList))
						{
							foreach (List<Matrix4x4> list in foliageInstanceList.matrices)
							{
								foreach (Matrix4x4 matrix in list)
								{
									if (volume.containsPoint(matrix.GetPosition()))
									{
										num++;
									}
								}
							}
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x0005CCAC File Offset: 0x0005B0AC
		protected override void addFoliage(Vector3 position, Quaternion rotation, Vector3 scale, bool clearWhenBaked)
		{
			FoliageSystem.addInstance(base.getReferenceTo<FoliageInstancedMeshInfoAsset>(), position, rotation, scale, clearWhenBaked);
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x0005CCBE File Offset: 0x0005B0BE
		protected override bool isPositionValid(Vector3 position)
		{
			return FoliageVolumeUtility.isPointValid(position, true, false, false);
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x0005CCD4 File Offset: 0x0005B0D4
		protected override void readAsset(IFormattedFileReader reader)
		{
			base.readAsset(reader);
			this.mesh = reader.readValue<ContentReference<Mesh>>("Mesh");
			this.material = reader.readValue<ContentReference<Material>>("Material");
			if (reader.containsKey("Cast_Shadows"))
			{
				this.castShadows = reader.readValue<bool>("Cast_Shadows");
			}
			else
			{
				this.castShadows = false;
			}
			if (reader.containsKey("Tile_Dither"))
			{
				this.tileDither = reader.readValue<bool>("Tile_Dither");
			}
			else
			{
				this.tileDither = true;
			}
			if (reader.containsKey("Draw_Distance"))
			{
				this.drawDistance = reader.readValue<int>("Draw_Distance");
			}
			else
			{
				this.drawDistance = -1;
			}
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x0005CD94 File Offset: 0x0005B194
		protected override void writeAsset(IFormattedFileWriter writer)
		{
			base.writeAsset(writer);
			writer.writeValue<ContentReference<Mesh>>("Mesh", this.mesh);
			writer.writeValue<ContentReference<Material>>("Material", this.material);
			writer.writeValue<bool>("Cast_Shadows", this.castShadows);
			writer.writeValue<bool>("Tile_Dither", this.tileDither);
			writer.writeValue<int>("Draw_Distance", this.drawDistance);
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x0005CDFD File Offset: 0x0005B1FD
		protected virtual void resetInstancedMeshInfo()
		{
			this.tileDither = true;
			this.drawDistance = -1;
		}

		// Token: 0x04000895 RID: 2197
		[Inspectable("#SDG::Asset.Foliage.Info.Mesh.Name", null)]
		public ContentReference<Mesh> mesh;

		// Token: 0x04000896 RID: 2198
		[Inspectable("#SDG::Asset.Foliage.Info.Material.Name", null)]
		public ContentReference<Material> material;

		// Token: 0x04000897 RID: 2199
		[Inspectable("#SDG::Asset.Foliage.Info.Cast_Shadows.Name", null)]
		public bool castShadows;

		// Token: 0x04000898 RID: 2200
		[Inspectable("#SDG::Asset.Foliage.Info.Tile_Dither.Name", null)]
		public bool tileDither;

		// Token: 0x04000899 RID: 2201
		[Inspectable("#SDG::Asset.Foliage.Info.Draw_Distance.Name", null)]
		public int drawDistance;
	}
}
