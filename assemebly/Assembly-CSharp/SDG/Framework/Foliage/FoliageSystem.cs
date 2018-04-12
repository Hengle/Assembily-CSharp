using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using SDG.Framework.Devkit;
using SDG.Framework.Devkit.Visibility;
using SDG.Framework.IO.FormattedFiles;
using SDG.Framework.Translations;
using SDG.Framework.Utilities;
using SDG.Unturned;
using UnityEngine;
using UnityEngine.Rendering;

namespace SDG.Framework.Foliage
{
	// Token: 0x020001AD RID: 429
	public class FoliageSystem : DevkitHierarchyItemBase, IDevkitHierarchyAutoSpawnable, IDevkitHierarchySpawnable
	{
		// Token: 0x06000C94 RID: 3220 RVA: 0x0005D53C File Offset: 0x0005B93C
		static FoliageSystem()
		{
			FoliageSystem.foliageVisibilityGroup = VisibilityManager.registerVisibilityGroup<VisibilityGroup>(FoliageSystem.foliageVisibilityGroup);
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x0005D5C3 File Offset: 0x0005B9C3
		// (set) Token: 0x06000C97 RID: 3223 RVA: 0x0005D5CA File Offset: 0x0005B9CA
		public static FoliageSystem instance { get; private set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000C98 RID: 3224 RVA: 0x0005D5D2 File Offset: 0x0005B9D2
		// (set) Token: 0x06000C99 RID: 3225 RVA: 0x0005D5D9 File Offset: 0x0005B9D9
		public static List<IFoliageSurface> surfaces { get; private set; } = new List<IFoliageSurface>();

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000C9A RID: 3226 RVA: 0x0005D5E1 File Offset: 0x0005B9E1
		// (set) Token: 0x06000C9B RID: 3227 RVA: 0x0005D5E8 File Offset: 0x0005B9E8
		public static VisibilityGroup foliageVisibilityGroup { get; private set; } = new VisibilityGroup("foliage_instanced_meshes", new TranslationReference("#SDG::Devkit.Visibility.Foliage_Instanced_Meshes"), true);

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06000C9C RID: 3228 RVA: 0x0005D5F0 File Offset: 0x0005B9F0
		// (remove) Token: 0x06000C9D RID: 3229 RVA: 0x0005D624 File Offset: 0x0005BA24
		public static event FoliageSystemPreBakeHandler preBake;

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06000C9E RID: 3230 RVA: 0x0005D658 File Offset: 0x0005BA58
		// (remove) Token: 0x06000C9F RID: 3231 RVA: 0x0005D68C File Offset: 0x0005BA8C
		public static event FoliageSystemPreBakeTileHandler preBakeTile;

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06000CA0 RID: 3232 RVA: 0x0005D6C0 File Offset: 0x0005BAC0
		// (remove) Token: 0x06000CA1 RID: 3233 RVA: 0x0005D6F4 File Offset: 0x0005BAF4
		public static event FoliageSystemPostBakeTileHandler postBakeTile;

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06000CA2 RID: 3234 RVA: 0x0005D728 File Offset: 0x0005BB28
		// (remove) Token: 0x06000CA3 RID: 3235 RVA: 0x0005D75C File Offset: 0x0005BB5C
		public static event FoliageSystemGlobalBakeHandler globalBake;

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06000CA4 RID: 3236 RVA: 0x0005D790 File Offset: 0x0005BB90
		// (remove) Token: 0x06000CA5 RID: 3237 RVA: 0x0005D7C4 File Offset: 0x0005BBC4
		public static event FoliageSystemLocalBakeHandler localBake;

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06000CA6 RID: 3238 RVA: 0x0005D7F8 File Offset: 0x0005BBF8
		// (remove) Token: 0x06000CA7 RID: 3239 RVA: 0x0005D82C File Offset: 0x0005BC2C
		public static event FoliageSystemPostBakeHandler postBake;

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x0005D860 File Offset: 0x0005BC60
		public static int bakeQueueProgress
		{
			get
			{
				return FoliageSystem.bakeQueueTotal - FoliageSystem.bakeQueue.Count;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x0005D872 File Offset: 0x0005BC72
		// (set) Token: 0x06000CAA RID: 3242 RVA: 0x0005D879 File Offset: 0x0005BC79
		public static int bakeQueueTotal { get; private set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x0005D881 File Offset: 0x0005BC81
		// (set) Token: 0x06000CAC RID: 3244 RVA: 0x0005D888 File Offset: 0x0005BC88
		public static FoliageBakeSettings bakeSettings { get; private set; }

		// Token: 0x06000CAD RID: 3245 RVA: 0x0005D890 File Offset: 0x0005BC90
		public static void addSurface(IFoliageSurface surface)
		{
			FoliageSystem.surfaces.Add(surface);
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x0005D89D File Offset: 0x0005BC9D
		public static void removeSurface(IFoliageSurface surface)
		{
			FoliageSystem.surfaces.Remove(surface);
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x0005D8AC File Offset: 0x0005BCAC
		public static void addCut(IShapeVolume cut)
		{
			Bounds worldBounds = cut.worldBounds;
			FoliageBounds foliageBounds = new FoliageBounds(worldBounds);
			for (int i = foliageBounds.min.x; i <= foliageBounds.max.x; i++)
			{
				for (int j = foliageBounds.min.y; j <= foliageBounds.max.y; j++)
				{
					FoliageCoord tileCoord = new FoliageCoord(i, j);
					FoliageTile orAddTile = FoliageSystem.getOrAddTile(tileCoord);
					orAddTile.addCut(cut);
				}
			}
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x0005D934 File Offset: 0x0005BD34
		private static Dictionary<FoliageTile, List<IFoliageSurface>> getTileSurfacePairs()
		{
			Dictionary<FoliageTile, List<IFoliageSurface>> dictionary = new Dictionary<FoliageTile, List<IFoliageSurface>>();
			foreach (KeyValuePair<FoliageCoord, FoliageTile> keyValuePair in FoliageSystem.tiles)
			{
				FoliageTile value = keyValuePair.Value;
				if (FoliageVolumeUtility.isTileBakeable(value))
				{
					dictionary.Add(value, new List<IFoliageSurface>());
				}
			}
			foreach (IFoliageSurface foliageSurface in FoliageSystem.surfaces)
			{
				FoliageBounds foliageSurfaceBounds = foliageSurface.getFoliageSurfaceBounds();
				for (int i = foliageSurfaceBounds.min.x; i <= foliageSurfaceBounds.max.x; i++)
				{
					for (int j = foliageSurfaceBounds.min.y; j <= foliageSurfaceBounds.max.y; j++)
					{
						FoliageCoord tileCoord = new FoliageCoord(i, j);
						FoliageTile orAddTile = FoliageSystem.getOrAddTile(tileCoord);
						if (FoliageVolumeUtility.isTileBakeable(orAddTile))
						{
							List<IFoliageSurface> list;
							if (!dictionary.TryGetValue(orAddTile, out list))
							{
								list = new List<IFoliageSurface>();
								dictionary.Add(orAddTile, list);
							}
							list.Add(foliageSurface);
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x0005DAA0 File Offset: 0x0005BEA0
		private static void bakePre()
		{
			if (FoliageSystem.preBake != null)
			{
				FoliageSystem.preBake();
			}
			FoliageSystem.bakeQueue.Clear();
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x0005DAC0 File Offset: 0x0005BEC0
		public static void bakeGlobal(FoliageBakeSettings bakeSettings)
		{
			FoliageSystem.bakeSettings = bakeSettings;
			FoliageSystem.bakePre();
			FoliageSystem.bakeGlobalBegin();
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x0005DAD4 File Offset: 0x0005BED4
		private static void bakeGlobalBegin()
		{
			Dictionary<FoliageTile, List<IFoliageSurface>> tileSurfacePairs = FoliageSystem.getTileSurfacePairs();
			foreach (KeyValuePair<FoliageTile, List<IFoliageSurface>> item in tileSurfacePairs)
			{
				FoliageSystem.bakeQueue.Enqueue(item);
			}
			FoliageSystem.bakeQueueTotal = FoliageSystem.bakeQueue.Count;
			if (FoliageSystem.<>f__mg$cache0 == null)
			{
				FoliageSystem.<>f__mg$cache0 = new FoliageSystemPostBakeHandler(FoliageSystem.bakeGlobalEnd);
			}
			FoliageSystem.bakeEnd = FoliageSystem.<>f__mg$cache0;
			FoliageSystem.bakeEnd();
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x0005DB70 File Offset: 0x0005BF70
		private static void bakeGlobalEnd()
		{
			if (FoliageSystem.globalBake != null)
			{
				FoliageSystem.globalBake();
			}
			FoliageSystem.bakePost();
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x0005DB8B File Offset: 0x0005BF8B
		public static void bakeLocal(FoliageBakeSettings bakeSettings)
		{
			FoliageSystem.bakeSettings = bakeSettings;
			FoliageSystem.bakePre();
			FoliageSystem.bakeLocalBegin();
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x0005DBA0 File Offset: 0x0005BFA0
		private static void bakeLocalBegin()
		{
			FoliageSystem.bakeLocalPosition = MainCamera.instance.transform.position;
			int num = 6;
			int num2 = num * num;
			FoliageCoord foliageCoord = new FoliageCoord(FoliageSystem.bakeLocalPosition);
			Dictionary<FoliageTile, List<IFoliageSurface>> tileSurfacePairs = FoliageSystem.getTileSurfacePairs();
			for (int i = -num; i <= num; i++)
			{
				for (int j = -num; j <= num; j++)
				{
					int num3 = i * i + j * j;
					if (num3 <= num2)
					{
						FoliageCoord tileCoord = new FoliageCoord(foliageCoord.x + i, foliageCoord.y + j);
						FoliageTile tile = FoliageSystem.getTile(tileCoord);
						if (tile != null)
						{
							List<IFoliageSurface> value;
							if (tileSurfacePairs.TryGetValue(tile, out value))
							{
								KeyValuePair<FoliageTile, List<IFoliageSurface>> item = new KeyValuePair<FoliageTile, List<IFoliageSurface>>(tile, value);
								FoliageSystem.bakeQueue.Enqueue(item);
							}
						}
					}
				}
			}
			FoliageSystem.bakeQueueTotal = FoliageSystem.bakeQueue.Count;
			if (FoliageSystem.<>f__mg$cache1 == null)
			{
				FoliageSystem.<>f__mg$cache1 = new FoliageSystemPostBakeHandler(FoliageSystem.bakeLocalEnd);
			}
			FoliageSystem.bakeEnd = FoliageSystem.<>f__mg$cache1;
			FoliageSystem.bakeEnd();
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x0005DCB2 File Offset: 0x0005C0B2
		private static void bakeLocalEnd()
		{
			if (FoliageSystem.localBake != null)
			{
				FoliageSystem.localBake(FoliageSystem.bakeLocalPosition);
			}
			FoliageSystem.bakePost();
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x0005DCD2 File Offset: 0x0005C0D2
		public static void bakeCancel()
		{
			if (FoliageSystem.bakeQueue.Count == 0)
			{
				return;
			}
			FoliageSystem.bakeQueue.Clear();
			FoliageSystem.bakeEnd();
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x0005DCF8 File Offset: 0x0005C0F8
		private static void bakePreTile(FoliageBakeSettings bakeSettings, FoliageTile foliageTile)
		{
			if (!bakeSettings.bakeInstancesMeshes)
			{
				return;
			}
			if (!foliageTile.hasInstances)
			{
				foliageTile.readInstancesOnThread();
				foliageTile.clearPostBake = true;
			}
			if (foliageTile.hasInstances)
			{
				if (bakeSettings.bakeApplyScale)
				{
					foliageTile.applyScale();
				}
				else
				{
					foliageTile.clearGeneratedInstances();
				}
			}
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x0005DD52 File Offset: 0x0005C152
		private static void bakePostTile(FoliageBakeSettings bakeSettings, FoliageTile foliageTile)
		{
			if (!bakeSettings.bakeInstancesMeshes)
			{
				return;
			}
			if (foliageTile.hasInstances)
			{
				foliageTile.writeInstances();
				if (foliageTile.clearPostBake)
				{
					foliageTile.clearInstances();
				}
			}
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x0005DD84 File Offset: 0x0005C184
		private static void bake(FoliageTile tile, List<IFoliageSurface> list)
		{
			FoliageSystem.bakePreTile(FoliageSystem.bakeSettings, tile);
			if (FoliageSystem.preBakeTile != null)
			{
				FoliageSystem.preBakeTile(FoliageSystem.bakeSettings, tile);
			}
			if (!FoliageSystem.bakeSettings.bakeApplyScale)
			{
				foreach (IFoliageSurface foliageSurface in list)
				{
					foliageSurface.bakeFoliageSurface(FoliageSystem.bakeSettings, tile);
				}
			}
			FoliageSystem.bakePostTile(FoliageSystem.bakeSettings, tile);
			if (FoliageSystem.postBakeTile != null)
			{
				FoliageSystem.postBakeTile(FoliageSystem.bakeSettings, tile);
			}
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x0005DE3C File Offset: 0x0005C23C
		private static void bakePost()
		{
			if (LevelHierarchy.instance != null)
			{
				LevelHierarchy.instance.isDirty = true;
			}
			if (FoliageSystem.postBake != null)
			{
				FoliageSystem.postBake();
			}
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x0005DE68 File Offset: 0x0005C268
		public static void addInstance(AssetReference<FoliageInstancedMeshInfoAsset> assetReference, Vector3 position, Quaternion rotation, Vector3 scale, bool clearWhenBaked)
		{
			FoliageTile orAddTile = FoliageSystem.getOrAddTile(position);
			Matrix4x4 newMatrix = Matrix4x4.TRS(position, rotation, scale);
			orAddTile.addInstance(new FoliageInstanceGroup(assetReference, newMatrix, clearWhenBaked));
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x0005DE94 File Offset: 0x0005C294
		protected static void clearTiles()
		{
			foreach (KeyValuePair<FoliageCoord, FoliageTile> keyValuePair in FoliageSystem.tiles)
			{
				FoliageTile value = keyValuePair.Value;
				if (value.hasInstances)
				{
					value.clearInstances();
				}
			}
			FoliageSystem.tiles.Clear();
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x0005DF10 File Offset: 0x0005C310
		public static void drawTiles(Vector3 position, int drawDistance, Camera camera, Plane[] frustumPlanes)
		{
			int num = drawDistance * drawDistance;
			FoliageCoord foliageCoord = new FoliageCoord(position);
			for (int i = -drawDistance; i <= drawDistance; i++)
			{
				for (int j = -drawDistance; j <= drawDistance; j++)
				{
					FoliageCoord foliageCoord2 = new FoliageCoord(foliageCoord.x + i, foliageCoord.y + j);
					if (!FoliageSystem.activeTiles.ContainsKey(foliageCoord2))
					{
						FoliageTile tile = FoliageSystem.getTile(foliageCoord2);
						if (tile != null)
						{
							int num2 = i * i + j * j;
							if (num2 <= num)
							{
								float density = 1f;
								float num3 = Mathf.Sqrt((float)num2);
								if (num3 > 2f && drawDistance > 3)
								{
									density = 1f - (num3 - 2f) / (float)(drawDistance - 1);
								}
								FoliageSystem.drawTileCullingChecks(tile, num2, density, camera, frustumPlanes);
								FoliageSystem.activeTiles.Add(foliageCoord2, tile);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x0005E006 File Offset: 0x0005C406
		public static void drawTileCullingChecks(FoliageTile tile, int sqrDistance, float density, Camera camera, Plane[] frustumPlanes)
		{
			if (tile == null)
			{
				return;
			}
			if (!GeometryUtility.TestPlanesAABB(frustumPlanes, tile.worldBounds))
			{
				return;
			}
			FoliageSystem.drawTile(tile, sqrDistance, density, camera);
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x0005E02C File Offset: 0x0005C42C
		public static void drawTile(FoliageTile tile, int sqrDistance, float density, Camera camera)
		{
			if (tile == null)
			{
				return;
			}
			if (tile.hasInstances)
			{
				foreach (KeyValuePair<AssetReference<FoliageInstancedMeshInfoAsset>, FoliageInstanceList> keyValuePair in tile.instances)
				{
					FoliageInstanceList value = keyValuePair.Value;
					value.loadAsset();
					Mesh mesh = value.mesh;
					if (!(mesh == null))
					{
						Material material = value.material;
						if (!(material == null))
						{
							bool castShadows = value.castShadows;
							if (!value.tileDither)
							{
								density = 1f;
							}
							density *= FoliageSettings.instanceDensity;
							if (value.sqrDrawDistance == -1 || sqrDistance <= value.sqrDrawDistance)
							{
								if (FoliageSettings.forceInstancingOff || !SystemInfo.supportsInstancing)
								{
									foreach (List<Matrix4x4> list in value.matrices)
									{
										int num = Mathf.RoundToInt((float)list.Count * density);
										for (int i = 0; i < num; i++)
										{
											Graphics.DrawMesh(mesh, list[i], material, LayerMasks.ENVIRONMENT, camera, 0, null, castShadows, true);
										}
									}
								}
								else
								{
									ShadowCastingMode castShadows2 = (!castShadows) ? ShadowCastingMode.Off : ShadowCastingMode.On;
									foreach (List<Matrix4x4> list2 in value.matrices)
									{
										int count = Mathf.RoundToInt((float)list2.Count * density);
										Graphics.DrawMeshInstanced(mesh, 0, material, list2.GetInternalArray<Matrix4x4>(), count, null, castShadows2, true, LayerMasks.ENVIRONMENT, camera);
									}
								}
							}
						}
					}
				}
			}
			else
			{
				tile.readInstancesJob();
			}
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x0005E268 File Offset: 0x0005C668
		public static FoliageTile getOrAddTile(Vector3 worldPosition)
		{
			FoliageCoord tileCoord = new FoliageCoord(worldPosition);
			return FoliageSystem.getOrAddTile(tileCoord);
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x0005E284 File Offset: 0x0005C684
		public static FoliageTile getTile(Vector3 worldPosition)
		{
			FoliageCoord tileCoord = new FoliageCoord(worldPosition);
			return FoliageSystem.getTile(tileCoord);
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x0005E2A0 File Offset: 0x0005C6A0
		public static FoliageTile getOrAddTile(FoliageCoord tileCoord)
		{
			FoliageTile foliageTile;
			if (!FoliageSystem.tiles.TryGetValue(tileCoord, out foliageTile))
			{
				foliageTile = new FoliageTile(tileCoord);
				FoliageSystem.tiles.Add(tileCoord, foliageTile);
			}
			return foliageTile;
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x0005E2D4 File Offset: 0x0005C6D4
		public static FoliageTile getTile(FoliageCoord tileCoord)
		{
			FoliageTile result;
			FoliageSystem.tiles.TryGetValue(tileCoord, out result);
			return result;
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x0005E2F0 File Offset: 0x0005C6F0
		public void devkitHierarchySpawn()
		{
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x0005E2F4 File Offset: 0x0005C6F4
		public override void read(IFormattedFileReader reader)
		{
			reader = reader.readObject();
			int num = reader.readArrayLength("Tiles");
			for (int i = 0; i < num; i++)
			{
				reader.readArrayIndex(i);
				FoliageTile foliageTile = new FoliageTile(FoliageCoord.ZERO);
				foliageTile.read(reader);
				FoliageSystem.tiles.Add(foliageTile.coord, foliageTile);
			}
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x0005E354 File Offset: 0x0005C754
		public override void write(IFormattedFileWriter writer)
		{
			writer.beginObject();
			writer.beginArray("Tiles");
			foreach (KeyValuePair<FoliageCoord, FoliageTile> keyValuePair in FoliageSystem.tiles)
			{
				FoliageTile value = keyValuePair.Value;
				writer.writeValue<FoliageTile>(value);
			}
			writer.endArray();
			writer.endObject();
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x0005E3D4 File Offset: 0x0005C7D4
		protected void Update()
		{
			if (MainCamera.instance == null)
			{
				return;
			}
			FoliageSystem.activeTiles.Clear();
			if (FoliageSettings.enabled && FoliageSystem.foliageVisibilityGroup.isVisible)
			{
				FoliageSystem.mainCameraFrustumPlanes = GeometryUtility.CalculateFrustumPlanes(MainCamera.instance);
				FoliageSystem.drawTiles(MainCamera.instance.transform.position, FoliageSettings.drawDistance, null, FoliageSystem.mainCameraFrustumPlanes);
				if (FoliageSettings.drawFocus && FoliageSystem.isFocused && FoliageSystem.focusCamera != null)
				{
					if (MainCamera.instance == FoliageSystem.focusCamera)
					{
						FoliageSystem.focusCameraFrustumPlanes = FoliageSystem.mainCameraFrustumPlanes;
					}
					else
					{
						FoliageSystem.focusCameraFrustumPlanes = GeometryUtility.CalculateFrustumPlanes(FoliageSystem.focusCamera);
					}
					FoliageSystem.drawTiles(FoliageSystem.focusPosition, FoliageSettings.drawFocusDistance, FoliageSystem.focusCamera, FoliageSystem.focusCameraFrustumPlanes);
				}
			}
			foreach (KeyValuePair<FoliageCoord, FoliageTile> keyValuePair in FoliageSystem.prevTiles)
			{
				if (!FoliageSystem.activeTiles.ContainsKey(keyValuePair.Key))
				{
					if (keyValuePair.Value != null && keyValuePair.Value.hasInstances)
					{
						if (keyValuePair.Value.canSafelyClear)
						{
							keyValuePair.Value.clearInstances();
						}
					}
				}
			}
			FoliageSystem.prevTiles.Clear();
			foreach (KeyValuePair<FoliageCoord, FoliageTile> keyValuePair2 in FoliageSystem.activeTiles)
			{
				FoliageSystem.prevTiles.Add(keyValuePair2.Key, keyValuePair2.Value);
			}
			if (FoliageSystem.bakeQueue.Count > 0)
			{
				KeyValuePair<FoliageTile, List<IFoliageSurface>> keyValuePair3 = FoliageSystem.bakeQueue.Dequeue();
				FoliageSystem.bake(keyValuePair3.Key, keyValuePair3.Value);
				if (FoliageSystem.bakeQueue.Count == 0)
				{
					FoliageSystem.bakeEnd();
				}
			}
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x0005E600 File Offset: 0x0005CA00
		protected void OnEnable()
		{
			LevelHierarchy.addItem(this);
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x0005E608 File Offset: 0x0005CA08
		protected void OnDisable()
		{
			LevelHierarchy.removeItem(this);
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x0005E610 File Offset: 0x0005CA10
		protected void Awake()
		{
			base.name = "Foliage_System";
			base.gameObject.layer = LayerMasks.GROUND;
			if (FoliageSystem.instance == null)
			{
				FoliageSystem.instance = this;
				FoliageSystem.prevTiles.Clear();
				FoliageSystem.activeTiles.Clear();
				FoliageSystem.bakeQueue.Clear();
				FoliageSystem.clearTiles();
			}
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x0005E671 File Offset: 0x0005CA71
		protected void OnDestroy()
		{
			if (FoliageSystem.instance == this)
			{
				FoliageSystem.instance = null;
				FoliageSystem.prevTiles.Clear();
				FoliageSystem.activeTiles.Clear();
				FoliageSystem.bakeQueue.Clear();
				FoliageSystem.clearTiles();
			}
		}

		// Token: 0x040008AB RID: 2219
		public static float TILE_SIZE = 32f;

		// Token: 0x040008AC RID: 2220
		public static int TILE_SIZE_INT = 32;

		// Token: 0x040008AD RID: 2221
		public static int SPLATMAP_RESOLUTION_PER_TILE = 8;

		// Token: 0x040008B9 RID: 2233
		protected static Dictionary<FoliageCoord, FoliageTile> prevTiles = new Dictionary<FoliageCoord, FoliageTile>();

		// Token: 0x040008BA RID: 2234
		protected static Dictionary<FoliageCoord, FoliageTile> activeTiles = new Dictionary<FoliageCoord, FoliageTile>();

		// Token: 0x040008BB RID: 2235
		protected static Dictionary<FoliageCoord, FoliageTile> tiles = new Dictionary<FoliageCoord, FoliageTile>();

		// Token: 0x040008BC RID: 2236
		protected static Queue<KeyValuePair<FoliageTile, List<IFoliageSurface>>> bakeQueue = new Queue<KeyValuePair<FoliageTile, List<IFoliageSurface>>>();

		// Token: 0x040008BD RID: 2237
		protected static FoliageSystemPostBakeHandler bakeEnd;

		// Token: 0x040008BE RID: 2238
		protected static Vector3 bakeLocalPosition;

		// Token: 0x040008BF RID: 2239
		protected static Plane[] mainCameraFrustumPlanes;

		// Token: 0x040008C0 RID: 2240
		protected static Plane[] focusCameraFrustumPlanes;

		// Token: 0x040008C1 RID: 2241
		public static Vector3 focusPosition;

		// Token: 0x040008C2 RID: 2242
		public static bool isFocused;

		// Token: 0x040008C3 RID: 2243
		public static Camera focusCamera;

		// Token: 0x040008C4 RID: 2244
		[CompilerGenerated]
		private static FoliageSystemPostBakeHandler <>f__mg$cache0;

		// Token: 0x040008C5 RID: 2245
		[CompilerGenerated]
		private static FoliageSystemPostBakeHandler <>f__mg$cache1;
	}
}
