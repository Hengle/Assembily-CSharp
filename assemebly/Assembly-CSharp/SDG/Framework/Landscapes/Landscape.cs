using System;
using System.Collections.Generic;
using SDG.Framework.Devkit;
using SDG.Framework.Devkit.Transactions;
using SDG.Framework.IO.FormattedFiles;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.Landscapes
{
	// Token: 0x020001D2 RID: 466
	public class Landscape : DevkitHierarchyItemBase, IDevkitHierarchyAutoSpawnable, IDevkitHierarchySpawnable
	{
		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x000619EE File Offset: 0x0005FDEE
		// (set) Token: 0x06000DEA RID: 3562 RVA: 0x000619F5 File Offset: 0x0005FDF5
		public static Landscape instance { get; protected set; }

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x06000DEB RID: 3563 RVA: 0x00061A00 File Offset: 0x0005FE00
		// (remove) Token: 0x06000DEC RID: 3564 RVA: 0x00061A34 File Offset: 0x0005FE34
		public static event LandscapeLoadedHandler loaded;

		// Token: 0x06000DED RID: 3565 RVA: 0x00061A68 File Offset: 0x0005FE68
		public static bool getWorldHeight(Vector3 position, out float height)
		{
			LandscapeCoord coord = new LandscapeCoord(position);
			LandscapeTile tile = Landscape.getTile(coord);
			if (tile != null)
			{
				height = tile.terrain.SampleHeight(position) - Landscape.TILE_HEIGHT / 2f;
				return true;
			}
			height = 0f;
			return false;
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x00061AB0 File Offset: 0x0005FEB0
		public static bool getWorldHeight(LandscapeCoord tileCoord, HeightmapCoord heightmapCoord, out float height)
		{
			LandscapeTile tile = Landscape.getTile(tileCoord);
			if (tile != null)
			{
				height = tile.sourceHeightmap[heightmapCoord.x, heightmapCoord.y] * Landscape.TILE_HEIGHT - Landscape.TILE_HEIGHT / 2f;
				return true;
			}
			height = 0f;
			return false;
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x00061B04 File Offset: 0x0005FF04
		public static bool getHeight01(Vector3 position, out float height)
		{
			LandscapeCoord coord = new LandscapeCoord(position);
			LandscapeTile tile = Landscape.getTile(coord);
			if (tile != null)
			{
				height = tile.terrain.SampleHeight(position) / Landscape.TILE_HEIGHT;
				return true;
			}
			height = 0f;
			return false;
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x00061B44 File Offset: 0x0005FF44
		public static bool getHeight01(LandscapeCoord tileCoord, HeightmapCoord heightmapCoord, out float height)
		{
			LandscapeTile tile = Landscape.getTile(tileCoord);
			if (tile != null)
			{
				height = tile.sourceHeightmap[heightmapCoord.x, heightmapCoord.y];
				return true;
			}
			height = 0f;
			return false;
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x00061B84 File Offset: 0x0005FF84
		public static bool getNormal(Vector3 position, out Vector3 normal)
		{
			LandscapeCoord coord = new LandscapeCoord(position);
			LandscapeTile tile = Landscape.getTile(coord);
			if (tile != null)
			{
				normal = tile.data.GetInterpolatedNormal((position.x - (float)coord.x * Landscape.TILE_SIZE) / Landscape.TILE_SIZE, (position.z - (float)coord.y * Landscape.TILE_SIZE) / Landscape.TILE_SIZE);
				return true;
			}
			normal = Vector3.up;
			return false;
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x00061C00 File Offset: 0x00060000
		public static bool getSplatmapMaterial(Vector3 position, out AssetReference<LandscapeMaterialAsset> materialAsset)
		{
			LandscapeCoord tileCoord = new LandscapeCoord(position);
			SplatmapCoord splatmapCoord = new SplatmapCoord(tileCoord, position);
			return Landscape.getSplatmapMaterial(tileCoord, splatmapCoord, out materialAsset);
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x00061C28 File Offset: 0x00060028
		public static bool getSplatmapMaterial(LandscapeCoord tileCoord, SplatmapCoord splatmapCoord, out AssetReference<LandscapeMaterialAsset> materialAsset)
		{
			int index;
			if (Landscape.getSplatmapLayer(tileCoord, splatmapCoord, out index))
			{
				materialAsset = Landscape.getTile(tileCoord).materials[index];
				return true;
			}
			materialAsset = AssetReference<LandscapeMaterialAsset>.invalid;
			return false;
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x00061C68 File Offset: 0x00060068
		public static bool getSplatmapLayer(Vector3 position, out int layer)
		{
			LandscapeCoord tileCoord = new LandscapeCoord(position);
			SplatmapCoord splatmapCoord = new SplatmapCoord(tileCoord, position);
			return Landscape.getSplatmapLayer(tileCoord, splatmapCoord, out layer);
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x00061C90 File Offset: 0x00060090
		public static bool getSplatmapLayer(LandscapeCoord tileCoord, SplatmapCoord splatmapCoord, out int layer)
		{
			LandscapeTile tile = Landscape.getTile(tileCoord);
			if (tile != null)
			{
				layer = Landscape.getSplatmapHighestWeightLayerIndex(splatmapCoord, tile.sourceSplatmap, -1);
				return true;
			}
			layer = -1;
			return false;
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x00061CC0 File Offset: 0x000600C0
		public static int getSplatmapHighestWeightLayerIndex(SplatmapCoord splatmapCoord, float[,,] currentWeights, int ignoreLayer = -1)
		{
			float num = -1f;
			int result = -1;
			for (int i = 0; i < Landscape.SPLATMAP_LAYERS; i++)
			{
				if (i != ignoreLayer)
				{
					if (currentWeights[splatmapCoord.x, splatmapCoord.y, i] > num)
					{
						num = currentWeights[splatmapCoord.x, splatmapCoord.y, i];
						result = i;
					}
				}
			}
			return result;
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x00061D2C File Offset: 0x0006012C
		public static int getSplatmapHighestWeightLayerIndex(float[] currentWeights, int ignoreLayer = -1)
		{
			float num = -1f;
			int result = -1;
			for (int i = 0; i < Landscape.SPLATMAP_LAYERS; i++)
			{
				if (i != ignoreLayer)
				{
					if (currentWeights[i] > num)
					{
						num = currentWeights[i];
						result = i;
					}
				}
			}
			return result;
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x00061D73 File Offset: 0x00060173
		public static void clearHeightmapTransactions()
		{
			Landscape.heightmapTransactions.Clear();
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x00061D7F File Offset: 0x0006017F
		public static void clearSplatmapTransactions()
		{
			Landscape.splatmapTransactions.Clear();
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x00061D8B File Offset: 0x0006018B
		public static bool isPointerInTile(Vector3 worldPosition)
		{
			return Landscape.getTile(worldPosition) != null;
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x00061D9C File Offset: 0x0006019C
		public static Vector3 getWorldPosition(LandscapeCoord tileCoord, HeightmapCoord heightmapCoord, float height)
		{
			float num = (float)tileCoord.x * Landscape.TILE_SIZE + (float)heightmapCoord.y / (float)Landscape.HEIGHTMAP_RESOLUTION_MINUS_ONE * Landscape.TILE_SIZE;
			num = (float)Mathf.RoundToInt(num);
			float y = -Landscape.TILE_HEIGHT / 2f + height * Landscape.TILE_HEIGHT;
			float num2 = (float)tileCoord.y * Landscape.TILE_SIZE + (float)heightmapCoord.x / (float)Landscape.HEIGHTMAP_RESOLUTION_MINUS_ONE * Landscape.TILE_SIZE;
			num2 = (float)Mathf.RoundToInt(num2);
			return new Vector3(num, y, num2);
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x00061E20 File Offset: 0x00060220
		public static Vector3 getWorldPosition(LandscapeCoord tileCoord, SplatmapCoord splatmapCoord)
		{
			float num = (float)tileCoord.x * Landscape.TILE_SIZE + (float)splatmapCoord.y / (float)Landscape.SPLATMAP_RESOLUTION * Landscape.TILE_SIZE;
			num = (float)Mathf.RoundToInt(num) + Landscape.HALF_SPLATMAP_WORLD_UNIT;
			float num2 = (float)tileCoord.y * Landscape.TILE_SIZE + (float)splatmapCoord.x / (float)Landscape.SPLATMAP_RESOLUTION * Landscape.TILE_SIZE;
			num2 = (float)Mathf.RoundToInt(num2) + Landscape.HALF_SPLATMAP_WORLD_UNIT;
			Vector3 vector = new Vector3(num, 0f, num2);
			float y;
			Landscape.getWorldHeight(vector, out y);
			vector.y = y;
			return vector;
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x00061EB4 File Offset: 0x000602B4
		public static void readHeightmap(Bounds worldBounds, Landscape.LandscapeReadHeightmapHandler callback)
		{
			if (callback == null)
			{
				return;
			}
			LandscapeBounds landscapeBounds = new LandscapeBounds(worldBounds);
			for (int i = landscapeBounds.min.x; i <= landscapeBounds.max.x; i++)
			{
				for (int j = landscapeBounds.min.y; j <= landscapeBounds.max.y; j++)
				{
					LandscapeCoord landscapeCoord = new LandscapeCoord(i, j);
					LandscapeTile tile = Landscape.getTile(landscapeCoord);
					if (tile != null)
					{
						HeightmapBounds heightmapBounds = new HeightmapBounds(landscapeCoord, worldBounds);
						for (int k = heightmapBounds.min.x; k < heightmapBounds.max.x; k++)
						{
							for (int l = heightmapBounds.min.y; l < heightmapBounds.max.y; l++)
							{
								HeightmapCoord heightmapCoord = new HeightmapCoord(k, l);
								float num = tile.sourceHeightmap[k, l];
								Vector3 worldPosition = Landscape.getWorldPosition(landscapeCoord, heightmapCoord, num);
								callback(landscapeCoord, heightmapCoord, worldPosition, num);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x00061FD4 File Offset: 0x000603D4
		public static void readSplatmap(Bounds worldBounds, Landscape.LandscapeReadSplatmapHandler callback)
		{
			if (callback == null)
			{
				return;
			}
			LandscapeBounds landscapeBounds = new LandscapeBounds(worldBounds);
			for (int i = landscapeBounds.min.x; i <= landscapeBounds.max.x; i++)
			{
				for (int j = landscapeBounds.min.y; j <= landscapeBounds.max.y; j++)
				{
					LandscapeCoord landscapeCoord = new LandscapeCoord(i, j);
					LandscapeTile tile = Landscape.getTile(landscapeCoord);
					if (tile != null)
					{
						SplatmapBounds splatmapBounds = new SplatmapBounds(landscapeCoord, worldBounds);
						for (int k = splatmapBounds.min.x; k < splatmapBounds.max.x; k++)
						{
							for (int l = splatmapBounds.min.y; l < splatmapBounds.max.y; l++)
							{
								SplatmapCoord splatmapCoord = new SplatmapCoord(k, l);
								for (int m = 0; m < Landscape.SPLATMAP_LAYERS; m++)
								{
									Landscape.SPLATMAP_LAYER_BUFFER[m] = tile.sourceSplatmap[k, l, m];
								}
								Vector3 worldPosition = Landscape.getWorldPosition(landscapeCoord, splatmapCoord);
								callback(landscapeCoord, splatmapCoord, worldPosition, Landscape.SPLATMAP_LAYER_BUFFER);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x00062118 File Offset: 0x00060518
		public static void writeHeightmap(Bounds worldBounds, Landscape.LandscapeWriteHeightmapHandler callback)
		{
			if (callback == null)
			{
				return;
			}
			LandscapeBounds landscapeBounds = new LandscapeBounds(worldBounds);
			for (int i = landscapeBounds.min.x; i <= landscapeBounds.max.x; i++)
			{
				for (int j = landscapeBounds.min.y; j <= landscapeBounds.max.y; j++)
				{
					LandscapeCoord landscapeCoord = new LandscapeCoord(i, j);
					LandscapeTile tile = Landscape.getTile(landscapeCoord);
					if (tile != null)
					{
						if (!Landscape.heightmapTransactions.ContainsKey(landscapeCoord))
						{
							LandscapeHeightmapTransaction landscapeHeightmapTransaction = new LandscapeHeightmapTransaction(tile);
							DevkitTransactionManager.recordTransaction(landscapeHeightmapTransaction);
							Landscape.heightmapTransactions.Add(landscapeCoord, landscapeHeightmapTransaction);
						}
						HeightmapBounds heightmapBounds = new HeightmapBounds(landscapeCoord, worldBounds);
						for (int k = heightmapBounds.min.x; k <= heightmapBounds.max.x; k++)
						{
							for (int l = heightmapBounds.min.y; l <= heightmapBounds.max.y; l++)
							{
								HeightmapCoord heightmapCoord = new HeightmapCoord(k, l);
								float num = tile.sourceHeightmap[k, l];
								Vector3 worldPosition = Landscape.getWorldPosition(landscapeCoord, heightmapCoord, num);
								tile.sourceHeightmap[k, l] = Mathf.Clamp01(callback(landscapeCoord, heightmapCoord, worldPosition, num));
							}
						}
						tile.data.SetHeightsDelayLOD(0, 0, tile.sourceHeightmap);
					}
				}
			}
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x00062290 File Offset: 0x00060690
		public static void writeSplatmap(Bounds worldBounds, Landscape.LandscapeWriteSplatmapHandler callback)
		{
			if (callback == null)
			{
				return;
			}
			LandscapeBounds landscapeBounds = new LandscapeBounds(worldBounds);
			for (int i = landscapeBounds.min.x; i <= landscapeBounds.max.x; i++)
			{
				for (int j = landscapeBounds.min.y; j <= landscapeBounds.max.y; j++)
				{
					LandscapeCoord landscapeCoord = new LandscapeCoord(i, j);
					LandscapeTile tile = Landscape.getTile(landscapeCoord);
					if (tile != null)
					{
						if (!Landscape.splatmapTransactions.ContainsKey(landscapeCoord))
						{
							LandscapeSplatmapTransaction landscapeSplatmapTransaction = new LandscapeSplatmapTransaction(tile);
							DevkitTransactionManager.recordTransaction(landscapeSplatmapTransaction);
							Landscape.splatmapTransactions.Add(landscapeCoord, landscapeSplatmapTransaction);
						}
						SplatmapBounds splatmapBounds = new SplatmapBounds(landscapeCoord, worldBounds);
						for (int k = splatmapBounds.min.x; k <= splatmapBounds.max.x; k++)
						{
							for (int l = splatmapBounds.min.y; l <= splatmapBounds.max.y; l++)
							{
								SplatmapCoord splatmapCoord = new SplatmapCoord(k, l);
								for (int m = 0; m < Landscape.SPLATMAP_LAYERS; m++)
								{
									Landscape.SPLATMAP_LAYER_BUFFER[m] = tile.sourceSplatmap[k, l, m];
								}
								Vector3 worldPosition = Landscape.getWorldPosition(landscapeCoord, splatmapCoord);
								callback(landscapeCoord, splatmapCoord, worldPosition, Landscape.SPLATMAP_LAYER_BUFFER);
								for (int n = 0; n < Landscape.SPLATMAP_LAYERS; n++)
								{
									tile.sourceSplatmap[k, l, n] = Mathf.Clamp01(Landscape.SPLATMAP_LAYER_BUFFER[n]);
								}
							}
						}
						tile.data.SetAlphamaps(0, 0, tile.sourceSplatmap);
					}
				}
			}
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x00062450 File Offset: 0x00060850
		public static void getHeightmapVertices(Bounds worldBounds, Landscape.LandscapeGetHeightmapVerticesHandler callback)
		{
			if (callback == null)
			{
				return;
			}
			LandscapeBounds landscapeBounds = new LandscapeBounds(worldBounds);
			for (int i = landscapeBounds.min.x; i <= landscapeBounds.max.x; i++)
			{
				for (int j = landscapeBounds.min.y; j <= landscapeBounds.max.y; j++)
				{
					LandscapeCoord landscapeCoord = new LandscapeCoord(i, j);
					LandscapeTile tile = Landscape.getTile(landscapeCoord);
					if (tile != null)
					{
						HeightmapBounds heightmapBounds = new HeightmapBounds(landscapeCoord, worldBounds);
						for (int k = heightmapBounds.min.x; k <= heightmapBounds.max.x; k++)
						{
							for (int l = heightmapBounds.min.y; l <= heightmapBounds.max.y; l++)
							{
								HeightmapCoord heightmapCoord = new HeightmapCoord(k, l);
								float height = tile.sourceHeightmap[k, l];
								Vector3 worldPosition = Landscape.getWorldPosition(landscapeCoord, heightmapCoord, height);
								callback(landscapeCoord, heightmapCoord, worldPosition);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x00062570 File Offset: 0x00060970
		public static void getSplatmapVertices(Bounds worldBounds, Landscape.LandscapeGetSplatmapVerticesHandler callback)
		{
			if (callback == null)
			{
				return;
			}
			LandscapeBounds landscapeBounds = new LandscapeBounds(worldBounds);
			for (int i = landscapeBounds.min.x; i <= landscapeBounds.max.x; i++)
			{
				for (int j = landscapeBounds.min.y; j <= landscapeBounds.max.y; j++)
				{
					LandscapeCoord landscapeCoord = new LandscapeCoord(i, j);
					if (Landscape.getTile(landscapeCoord) != null)
					{
						SplatmapBounds splatmapBounds = new SplatmapBounds(landscapeCoord, worldBounds);
						for (int k = splatmapBounds.min.x; k <= splatmapBounds.max.x; k++)
						{
							for (int l = splatmapBounds.min.y; l <= splatmapBounds.max.y; l++)
							{
								SplatmapCoord splatmapCoord = new SplatmapCoord(k, l);
								Vector3 worldPosition = Landscape.getWorldPosition(landscapeCoord, splatmapCoord);
								callback(landscapeCoord, splatmapCoord, worldPosition);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x0006267C File Offset: 0x00060A7C
		public static void applyLOD()
		{
			foreach (KeyValuePair<LandscapeCoord, LandscapeTile> keyValuePair in Landscape.tiles)
			{
				LandscapeTile value = keyValuePair.Value;
				value.terrain.ApplyDelayedHeightmapModification();
			}
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x000626E4 File Offset: 0x00060AE4
		public static void linkNeighbors()
		{
			foreach (KeyValuePair<LandscapeCoord, LandscapeTile> keyValuePair in Landscape.tiles)
			{
				LandscapeTile value = keyValuePair.Value;
				LandscapeTile tile = Landscape.getTile(new LandscapeCoord(value.coord.x - 1, value.coord.y));
				LandscapeTile tile2 = Landscape.getTile(new LandscapeCoord(value.coord.x, value.coord.y + 1));
				LandscapeTile tile3 = Landscape.getTile(new LandscapeCoord(value.coord.x + 1, value.coord.y));
				LandscapeTile tile4 = Landscape.getTile(new LandscapeCoord(value.coord.x, value.coord.y - 1));
				Terrain left = (tile != null) ? tile.terrain : null;
				Terrain top = (tile2 != null) ? tile2.terrain : null;
				Terrain right = (tile3 != null) ? tile3.terrain : null;
				Terrain bottom = (tile4 != null) ? tile4.terrain : null;
				value.terrain.SetNeighbors(left, top, right, bottom);
				value.terrain.Flush();
			}
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x00062870 File Offset: 0x00060C70
		public static void reconcileNeighbors(LandscapeTile tile)
		{
			LandscapeTile tile2 = Landscape.getTile(new LandscapeCoord(tile.coord.x - 1, tile.coord.y));
			if (tile2 != null)
			{
				for (int i = 0; i < Landscape.HEIGHTMAP_RESOLUTION; i++)
				{
					tile.sourceHeightmap[i, 0] = tile2.sourceHeightmap[i, Landscape.HEIGHTMAP_RESOLUTION - 1];
				}
			}
			LandscapeTile tile3 = Landscape.getTile(new LandscapeCoord(tile.coord.x, tile.coord.y - 1));
			if (tile3 != null)
			{
				for (int j = 0; j < Landscape.HEIGHTMAP_RESOLUTION; j++)
				{
					tile.sourceHeightmap[0, j] = tile3.sourceHeightmap[Landscape.HEIGHTMAP_RESOLUTION - 1, j];
				}
			}
			LandscapeTile tile4 = Landscape.getTile(new LandscapeCoord(tile.coord.x + 1, tile.coord.y));
			if (tile4 != null)
			{
				for (int k = 0; k < Landscape.HEIGHTMAP_RESOLUTION; k++)
				{
					tile.sourceHeightmap[k, Landscape.HEIGHTMAP_RESOLUTION - 1] = tile4.sourceHeightmap[k, 0];
				}
			}
			LandscapeTile tile5 = Landscape.getTile(new LandscapeCoord(tile.coord.x, tile.coord.y + 1));
			if (tile5 != null)
			{
				for (int l = 0; l < Landscape.HEIGHTMAP_RESOLUTION; l++)
				{
					tile.sourceHeightmap[Landscape.HEIGHTMAP_RESOLUTION - 1, l] = tile5.sourceHeightmap[0, l];
				}
			}
			tile.data.SetHeightsDelayLOD(0, 0, tile.sourceHeightmap);
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x00062A48 File Offset: 0x00060E48
		public static LandscapeTile addTile(LandscapeCoord coord)
		{
			if (Landscape.instance == null)
			{
				return null;
			}
			if (Landscape.tiles.ContainsKey(coord))
			{
				return null;
			}
			LandscapeTile landscapeTile = new LandscapeTile(coord);
			landscapeTile.enable();
			landscapeTile.applyGraphicsSettings();
			Landscape.tiles.Add(coord, landscapeTile);
			return landscapeTile;
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x00062A9C File Offset: 0x00060E9C
		protected static void clearTiles()
		{
			foreach (KeyValuePair<LandscapeCoord, LandscapeTile> keyValuePair in Landscape.tiles)
			{
				LandscapeTile value = keyValuePair.Value;
				value.disable();
			}
			Landscape.tiles.Clear();
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x00062B08 File Offset: 0x00060F08
		public static LandscapeTile getOrAddTile(Vector3 worldPosition)
		{
			LandscapeCoord coord = new LandscapeCoord(worldPosition);
			return Landscape.getOrAddTile(coord);
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x00062B24 File Offset: 0x00060F24
		public static LandscapeTile getTile(Vector3 worldPosition)
		{
			LandscapeCoord coord = new LandscapeCoord(worldPosition);
			return Landscape.getTile(coord);
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x00062B40 File Offset: 0x00060F40
		public static LandscapeTile getOrAddTile(LandscapeCoord coord)
		{
			LandscapeTile result;
			if (!Landscape.tiles.TryGetValue(coord, out result))
			{
				result = Landscape.addTile(coord);
			}
			return result;
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x00062B68 File Offset: 0x00060F68
		public static LandscapeTile getTile(LandscapeCoord coord)
		{
			LandscapeTile result;
			Landscape.tiles.TryGetValue(coord, out result);
			return result;
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x00062B84 File Offset: 0x00060F84
		public static bool removeTile(LandscapeCoord coord)
		{
			LandscapeTile landscapeTile;
			if (!Landscape.tiles.TryGetValue(coord, out landscapeTile))
			{
				return false;
			}
			landscapeTile.disable();
			UnityEngine.Object.Destroy(landscapeTile.gameObject);
			Landscape.tiles.Remove(coord);
			return true;
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x00062BC3 File Offset: 0x00060FC3
		public void devkitHierarchySpawn()
		{
			Landscape.addTile(new LandscapeCoord(0, 0));
			Landscape.linkNeighbors();
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x00062BD8 File Offset: 0x00060FD8
		public override void read(IFormattedFileReader reader)
		{
			reader = reader.readObject();
			int num = reader.readArrayLength("Tiles");
			for (int i = 0; i < num; i++)
			{
				reader.readArrayIndex(i);
				LandscapeTile landscapeTile = new LandscapeTile(LandscapeCoord.ZERO);
				landscapeTile.enable();
				landscapeTile.applyGraphicsSettings();
				landscapeTile.read(reader);
				if (Landscape.tiles.ContainsKey(landscapeTile.coord))
				{
					Debug.LogError("Duplicate landscape coord read: " + landscapeTile.coord);
				}
				else
				{
					Landscape.tiles.Add(landscapeTile.coord, landscapeTile);
				}
			}
			Landscape.linkNeighbors();
			Landscape.applyLOD();
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x00062C80 File Offset: 0x00061080
		public override void write(IFormattedFileWriter writer)
		{
			writer.beginObject();
			writer.beginArray("Tiles");
			foreach (KeyValuePair<LandscapeCoord, LandscapeTile> keyValuePair in Landscape.tiles)
			{
				LandscapeTile value = keyValuePair.Value;
				writer.writeValue<LandscapeTile>(value);
			}
			writer.endArray();
			writer.endObject();
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x00062D00 File Offset: 0x00061100
		protected void triggerLandscapeLoaded()
		{
			if (Landscape.loaded != null)
			{
				Landscape.loaded();
			}
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x00062D18 File Offset: 0x00061118
		protected void handleGraphicsSettingsApplied()
		{
			foreach (KeyValuePair<LandscapeCoord, LandscapeTile> keyValuePair in Landscape.tiles)
			{
				LandscapeTile value = keyValuePair.Value;
				value.applyGraphicsSettings();
			}
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x00062D7C File Offset: 0x0006117C
		protected void handlePlanarReflectionPreRender()
		{
			foreach (KeyValuePair<LandscapeCoord, LandscapeTile> keyValuePair in Landscape.tiles)
			{
				LandscapeTile value = keyValuePair.Value;
				value.terrain.basemapDistance = 0f;
			}
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x00062DE8 File Offset: 0x000611E8
		protected void handlePlanarReflectionPostRender()
		{
			float basemapDistance = (float)((!GraphicsSettings.blend) ? 256 : 512);
			foreach (KeyValuePair<LandscapeCoord, LandscapeTile> keyValuePair in Landscape.tiles)
			{
				LandscapeTile value = keyValuePair.Value;
				value.terrain.basemapDistance = basemapDistance;
			}
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x00062E6C File Offset: 0x0006126C
		protected void OnEnable()
		{
			LevelHierarchy.addItem(this);
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x00062E74 File Offset: 0x00061274
		protected void OnDisable()
		{
			LevelHierarchy.removeItem(this);
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x00062E7C File Offset: 0x0006127C
		protected void Awake()
		{
			base.name = "Landscape";
			base.gameObject.layer = LayerMasks.GROUND;
			if (Landscape.instance == null)
			{
				Landscape.instance = this;
				Landscape.clearTiles();
				if (Level.isEditor)
				{
					LandscapeHeightmapCopyPool.warmup(DevkitTransactionManager.historyLength);
					LandscapeSplatmapCopyPool.warmup(DevkitTransactionManager.historyLength);
				}
				GraphicsSettings.graphicsSettingsApplied += this.handleGraphicsSettingsApplied;
				PlanarReflection.preRender += this.handlePlanarReflectionPreRender;
				PlanarReflection.postRender += this.handlePlanarReflectionPostRender;
			}
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x00062F10 File Offset: 0x00061310
		protected void Start()
		{
			if (Landscape.instance == this)
			{
				this.triggerLandscapeLoaded();
			}
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x00062F28 File Offset: 0x00061328
		protected void OnDestroy()
		{
			if (Landscape.instance == this)
			{
				GraphicsSettings.graphicsSettingsApplied -= this.handleGraphicsSettingsApplied;
				PlanarReflection.preRender -= this.handlePlanarReflectionPreRender;
				PlanarReflection.postRender -= this.handlePlanarReflectionPostRender;
				Landscape.instance = null;
				Landscape.clearTiles();
				LandscapeHeightmapCopyPool.empty();
				LandscapeSplatmapCopyPool.empty();
			}
		}

		// Token: 0x04000901 RID: 2305
		public static readonly float TILE_SIZE = 1024f;

		// Token: 0x04000902 RID: 2306
		public static readonly int TILE_SIZE_INT = 1024;

		// Token: 0x04000903 RID: 2307
		public static readonly float TILE_HEIGHT = 2048f;

		// Token: 0x04000904 RID: 2308
		public static readonly int TILE_HEIGHT_INT = 2048;

		// Token: 0x04000905 RID: 2309
		public static readonly int HEIGHTMAP_RESOLUTION = 257;

		// Token: 0x04000906 RID: 2310
		public static readonly int HEIGHTMAP_RESOLUTION_MINUS_ONE = 256;

		// Token: 0x04000907 RID: 2311
		public static readonly float HEIGHTMAP_WORLD_UNIT = 4f;

		// Token: 0x04000908 RID: 2312
		public static readonly float HALF_HEIGHTMAP_WORLD_UNIT = 2f;

		// Token: 0x04000909 RID: 2313
		public static readonly int SPLATMAP_RESOLUTION = 256;

		// Token: 0x0400090A RID: 2314
		public static readonly int SPLATMAP_RESOLUTION_MINUS_ONE = 255;

		// Token: 0x0400090B RID: 2315
		public static readonly float SPLATMAP_WORLD_UNIT = 4f;

		// Token: 0x0400090C RID: 2316
		public static readonly float HALF_SPLATMAP_WORLD_UNIT = 2f;

		// Token: 0x0400090D RID: 2317
		public static readonly int BASEMAP_RESOLUTION = 64;

		// Token: 0x0400090E RID: 2318
		public static readonly int SPLATMAP_COUNT = 2;

		// Token: 0x0400090F RID: 2319
		public static readonly int SPLATMAP_CHANNELS = 4;

		// Token: 0x04000910 RID: 2320
		public static readonly int SPLATMAP_LAYERS = Landscape.SPLATMAP_COUNT * Landscape.SPLATMAP_CHANNELS;

		// Token: 0x04000911 RID: 2321
		protected static readonly float[] SPLATMAP_LAYER_BUFFER = new float[Landscape.SPLATMAP_LAYERS];

		// Token: 0x04000914 RID: 2324
		protected static Dictionary<LandscapeCoord, LandscapeTile> tiles = new Dictionary<LandscapeCoord, LandscapeTile>();

		// Token: 0x04000915 RID: 2325
		protected static Dictionary<LandscapeCoord, LandscapeHeightmapTransaction> heightmapTransactions = new Dictionary<LandscapeCoord, LandscapeHeightmapTransaction>();

		// Token: 0x04000916 RID: 2326
		protected static Dictionary<LandscapeCoord, LandscapeSplatmapTransaction> splatmapTransactions = new Dictionary<LandscapeCoord, LandscapeSplatmapTransaction>();

		// Token: 0x020001D3 RID: 467
		// (Invoke) Token: 0x06000E1B RID: 3611
		public delegate void LandscapeReadHeightmapHandler(LandscapeCoord tileCoord, HeightmapCoord heightmapCoord, Vector3 worldPosition, float currentHeight);

		// Token: 0x020001D4 RID: 468
		// (Invoke) Token: 0x06000E1F RID: 3615
		public delegate void LandscapeReadSplatmapHandler(LandscapeCoord tileCoord, SplatmapCoord splatmapCoord, Vector3 worldPosition, float[] currentWeights);

		// Token: 0x020001D5 RID: 469
		// (Invoke) Token: 0x06000E23 RID: 3619
		public delegate float LandscapeWriteHeightmapHandler(LandscapeCoord tileCoord, HeightmapCoord heightmapCoord, Vector3 worldPosition, float currentHeight);

		// Token: 0x020001D6 RID: 470
		// (Invoke) Token: 0x06000E27 RID: 3623
		public delegate void LandscapeWriteSplatmapHandler(LandscapeCoord tileCoord, SplatmapCoord splatmapCoord, Vector3 worldPosition, float[] currentWeights);

		// Token: 0x020001D7 RID: 471
		// (Invoke) Token: 0x06000E2B RID: 3627
		public delegate void LandscapeGetHeightmapVerticesHandler(LandscapeCoord tileCoord, HeightmapCoord heightmapCoord, Vector3 worldPosition);

		// Token: 0x020001D8 RID: 472
		// (Invoke) Token: 0x06000E2F RID: 3631
		public delegate void LandscapeGetSplatmapVerticesHandler(LandscapeCoord tileCoord, SplatmapCoord splatmapCoord, Vector3 worldPosition);
	}
}
