using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using SDG.Framework.Debug;
using SDG.Framework.Devkit;
using SDG.Framework.Foliage;
using SDG.Framework.Landscapes;
using SDG.Framework.Utilities;
using SDG.Framework.Water;
using UnityEngine;
using UnityEngine.Rendering;

namespace SDG.Unturned
{
	// Token: 0x02000547 RID: 1351
	public class LevelGround : MonoBehaviour
	{
		// Token: 0x0600246A RID: 9322 RVA: 0x000CBB2C File Offset: 0x000C9F2C
		static LevelGround()
		{
			if (LevelGround.<>f__mg$cache5 == null)
			{
				LevelGround.<>f__mg$cache5 = new LevelHierarchyLoaded(LevelGround.handleHiearchyLoaded);
			}
			LevelHierarchy.loaded += LevelGround.<>f__mg$cache5;
			if (LevelGround.<>f__mg$cache6 == null)
			{
				LevelGround.<>f__mg$cache6 = new FoliageSystemPreBakeTileHandler(LevelGround.handlePreBakeTile);
			}
			FoliageSystem.preBakeTile += LevelGround.<>f__mg$cache6;
			if (LevelGround.<>f__mg$cache7 == null)
			{
				LevelGround.<>f__mg$cache7 = new FoliageSystemPostBakeHandler(LevelGround.handlePostBake);
			}
			FoliageSystem.postBake += LevelGround.<>f__mg$cache7;
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x0600246C RID: 9324 RVA: 0x000CBC43 File Offset: 0x000CA043
		// (set) Token: 0x0600246D RID: 9325 RVA: 0x000CBC4A File Offset: 0x000CA04A
		[TerminalCommandProperty("landscape.triplanar_primary_size", "world units per texture coordinate", 16)]
		public static float triplanarPrimarySize
		{
			get
			{
				return LevelGround._triplanarPrimarySize;
			}
			set
			{
				LevelGround._triplanarPrimarySize = value;
				Shader.SetGlobalFloat(LevelGround._Triplanar_Primary_Size, LevelGround.triplanarPrimarySize);
				TerminalUtility.printCommandPass("Set triplanar_primary_size to: " + LevelGround.triplanarPrimarySize);
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x0600246E RID: 9326 RVA: 0x000CBC7A File Offset: 0x000CA07A
		// (set) Token: 0x0600246F RID: 9327 RVA: 0x000CBC81 File Offset: 0x000CA081
		[TerminalCommandProperty("landscape.triplanar_primary_weight", "influence of primary texture", 0.6f)]
		public static float triplanarPrimaryWeight
		{
			get
			{
				return LevelGround._triplanarPrimaryWeight;
			}
			set
			{
				LevelGround._triplanarPrimaryWeight = value;
				Shader.SetGlobalFloat(LevelGround._Triplanar_Primary_Weight, LevelGround.triplanarPrimaryWeight);
				TerminalUtility.printCommandPass("Set triplanar_primary_weight to: " + LevelGround.triplanarPrimaryWeight);
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06002470 RID: 9328 RVA: 0x000CBCB1 File Offset: 0x000CA0B1
		// (set) Token: 0x06002471 RID: 9329 RVA: 0x000CBCB8 File Offset: 0x000CA0B8
		[TerminalCommandProperty("landscape.triplanar_secondary_size", "world units per texture coordinate", 64)]
		public static float triplanarSecondarySize
		{
			get
			{
				return LevelGround._triplanarSecondarySize;
			}
			set
			{
				LevelGround._triplanarSecondarySize = value;
				Shader.SetGlobalFloat(LevelGround._Triplanar_Secondary_Size, LevelGround.triplanarSecondarySize);
				TerminalUtility.printCommandPass("Set triplanar_secondary_size to: " + LevelGround.triplanarSecondarySize);
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06002472 RID: 9330 RVA: 0x000CBCE8 File Offset: 0x000CA0E8
		// (set) Token: 0x06002473 RID: 9331 RVA: 0x000CBCEF File Offset: 0x000CA0EF
		[TerminalCommandProperty("landscape.triplanar_secondary_weight", "influence of secondary texture", 0.2f)]
		public static float triplanarSecondaryWeight
		{
			get
			{
				return LevelGround._triplanarSecondaryWeight;
			}
			set
			{
				LevelGround._triplanarSecondaryWeight = value;
				Shader.SetGlobalFloat(LevelGround._Triplanar_Secondary_Weight, LevelGround.triplanarSecondaryWeight);
				TerminalUtility.printCommandPass("Set triplanar_secondary_weight to: " + LevelGround.triplanarSecondaryWeight);
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06002474 RID: 9332 RVA: 0x000CBD1F File Offset: 0x000CA11F
		// (set) Token: 0x06002475 RID: 9333 RVA: 0x000CBD26 File Offset: 0x000CA126
		[TerminalCommandProperty("landscape.triplanar_tertiary_size", "world units per texture coordinate", 4)]
		public static float triplanarTertiarySize
		{
			get
			{
				return LevelGround._triplanarTertiarySize;
			}
			set
			{
				LevelGround._triplanarTertiarySize = value;
				Shader.SetGlobalFloat(LevelGround._Triplanar_Tertiary_Size, LevelGround.triplanarTertiarySize);
				TerminalUtility.printCommandPass("Set triplanar_tertiary_size to: " + LevelGround.triplanarTertiarySize);
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06002476 RID: 9334 RVA: 0x000CBD56 File Offset: 0x000CA156
		// (set) Token: 0x06002477 RID: 9335 RVA: 0x000CBD5D File Offset: 0x000CA15D
		[TerminalCommandProperty("landscape.triplanar_tertiary_weight", "influence of tertiary texture", 0.2f)]
		public static float triplanarTertiaryWeight
		{
			get
			{
				return LevelGround._triplanarTertiaryWeight;
			}
			set
			{
				LevelGround._triplanarTertiaryWeight = value;
				Shader.SetGlobalFloat(LevelGround._Triplanar_Tertiary_Weight, LevelGround.triplanarTertiaryWeight);
				TerminalUtility.printCommandPass("Set triplanar_tertiary_weight to: " + LevelGround.triplanarTertiaryWeight);
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06002478 RID: 9336 RVA: 0x000CBD8D File Offset: 0x000CA18D
		// (set) Token: 0x06002479 RID: 9337 RVA: 0x000CBD94 File Offset: 0x000CA194
		public static bool previewHQ
		{
			get
			{
				return LevelGround._previewHQ;
			}
			set
			{
				if (LevelGround.previewHQ == value)
				{
					return;
				}
				LevelGround._previewHQ = value;
				if (EditorTerrainMaterialsUI.previewToggle != null)
				{
					EditorTerrainMaterialsUI.previewToggle.state = LevelGround.previewHQ;
				}
				if (LevelGround.previewHQ)
				{
					LevelGround.data.SetAlphamaps(0, 0, LevelGround.alphamapHQ);
					LevelGround.data2.SetAlphamaps(0, 0, LevelGround.alphamap2HQ);
				}
				else
				{
					LevelGround.data.SetAlphamaps(0, 0, LevelGround.alphamap);
					LevelGround.data2.SetAlphamaps(0, 0, LevelGround.alphamap2);
				}
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x0600247A RID: 9338 RVA: 0x000CBE1F File Offset: 0x000CA21F
		public static GroundMaterial[] materials
		{
			get
			{
				return LevelGround._materials;
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x0600247B RID: 9339 RVA: 0x000CBE26 File Offset: 0x000CA226
		// (set) Token: 0x0600247C RID: 9340 RVA: 0x000CBE2D File Offset: 0x000CA22D
		public static SplatPrototype[] splatPrototypes { get; protected set; }

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x0600247D RID: 9341 RVA: 0x000CBE35 File Offset: 0x000CA235
		public static GroundDetail[] details
		{
			get
			{
				return LevelGround._details;
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x0600247E RID: 9342 RVA: 0x000CBE3C File Offset: 0x000CA23C
		public static GroundResource[] resources
		{
			get
			{
				return LevelGround._resources;
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x0600247F RID: 9343 RVA: 0x000CBE43 File Offset: 0x000CA243
		public static byte[] hash
		{
			get
			{
				return LevelGround._hash;
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06002480 RID: 9344 RVA: 0x000CBE4A File Offset: 0x000CA24A
		public static Transform models
		{
			get
			{
				return LevelGround._models;
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06002481 RID: 9345 RVA: 0x000CBE51 File Offset: 0x000CA251
		public static Transform models2
		{
			get
			{
				return LevelGround._models2;
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06002482 RID: 9346 RVA: 0x000CBE58 File Offset: 0x000CA258
		public static List<ResourceSpawnpoint>[,] trees
		{
			get
			{
				return LevelGround._trees;
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06002483 RID: 9347 RVA: 0x000CBE5F File Offset: 0x000CA25F
		public static int total
		{
			get
			{
				return LevelGround._total;
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06002484 RID: 9348 RVA: 0x000CBE66 File Offset: 0x000CA266
		public static bool[,] regions
		{
			get
			{
				return LevelGround._regions;
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06002485 RID: 9349 RVA: 0x000CBE6D File Offset: 0x000CA26D
		// (set) Token: 0x06002486 RID: 9350 RVA: 0x000CBE74 File Offset: 0x000CA274
		public static bool shouldInstantlyLoad { get; private set; }

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06002487 RID: 9351 RVA: 0x000CBE7C File Offset: 0x000CA27C
		public static Terrain terrain
		{
			get
			{
				return LevelGround._terrain;
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06002488 RID: 9352 RVA: 0x000CBE83 File Offset: 0x000CA283
		public static Terrain terrain2
		{
			get
			{
				return LevelGround._terrain2;
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06002489 RID: 9353 RVA: 0x000CBE8A File Offset: 0x000CA28A
		public static TerrainData data
		{
			get
			{
				return LevelGround._data;
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x0600248A RID: 9354 RVA: 0x000CBE91 File Offset: 0x000CA291
		public static TerrainData data2
		{
			get
			{
				return LevelGround._data2;
			}
		}

		// Token: 0x0600248B RID: 9355 RVA: 0x000CBE98 File Offset: 0x000CA298
		public static Vector3 checkSafe(Vector3 point)
		{
			float height = LevelGround.getHeight(point);
			if (point.y < height - 1f)
			{
				point.y = height + 0.5f;
			}
			return point;
		}

		// Token: 0x0600248C RID: 9356 RVA: 0x000CBED0 File Offset: 0x000CA2D0
		public static void undoHeight()
		{
			if (LevelGround.frameHeight <= LevelGround.reunHeight.Length - 1)
			{
				if (LevelGround.frameHeight < LevelGround.reunHeight.Length && LevelGround.reunHeight[LevelGround.frameHeight + 1] != null)
				{
					LevelGround.frameHeight++;
				}
				if (LevelGround.reunHeight[LevelGround.frameHeight] != null)
				{
					LevelGround.data.SetHeights(0, 0, LevelGround.reunHeight[LevelGround.frameHeight]);
					LevelGround.terrain.Flush();
				}
			}
		}

		// Token: 0x0600248D RID: 9357 RVA: 0x000CBF50 File Offset: 0x000CA350
		public static void undoHeight2()
		{
			if (LevelGround.frameHeight2 <= LevelGround.reunHeight2.Length - 1)
			{
				if (LevelGround.frameHeight2 < LevelGround.reunHeight2.Length - 1 && LevelGround.reunHeight2[LevelGround.frameHeight2 + 1] != null)
				{
					LevelGround.frameHeight2++;
				}
				if (LevelGround.reunHeight2[LevelGround.frameHeight2] != null)
				{
					LevelGround.data2.SetHeights(0, 0, LevelGround.reunHeight2[LevelGround.frameHeight2]);
					LevelGround.terrain2.Flush();
				}
			}
		}

		// Token: 0x0600248E RID: 9358 RVA: 0x000CBFD4 File Offset: 0x000CA3D4
		public static void redoHeight()
		{
			if (LevelGround.frameHeight >= 0)
			{
				if (LevelGround.frameHeight > 0 && LevelGround.reunHeight[LevelGround.frameHeight - 1] != null)
				{
					LevelGround.frameHeight--;
				}
				if (LevelGround.reunHeight[LevelGround.frameHeight] != null)
				{
					LevelGround.data.SetHeights(0, 0, LevelGround.reunHeight[LevelGround.frameHeight]);
					LevelGround.terrain.Flush();
				}
			}
		}

		// Token: 0x0600248F RID: 9359 RVA: 0x000CC048 File Offset: 0x000CA448
		public static void redoHeight2()
		{
			if (LevelGround.frameHeight2 >= 0)
			{
				if (LevelGround.frameHeight2 > 0 && LevelGround.reunHeight2[LevelGround.frameHeight2 - 1] != null)
				{
					LevelGround.frameHeight2--;
				}
				if (LevelGround.reunHeight2[LevelGround.frameHeight2] != null)
				{
					LevelGround.data2.SetHeights(0, 0, LevelGround.reunHeight2[LevelGround.frameHeight2]);
					LevelGround.terrain2.Flush();
				}
			}
		}

		// Token: 0x06002490 RID: 9360 RVA: 0x000CC0BC File Offset: 0x000CA4BC
		public static void registerHeight()
		{
			if (LevelGround.frameHeight > 0)
			{
				float[,] array = LevelGround.reunHeight[LevelGround.frameHeight];
				LevelGround.reunHeight = new float[LevelGround.reunHeight.Length][,];
				LevelGround.frameHeight = 0;
				LevelGround.reunHeight[0] = array;
			}
			for (int i = LevelGround.reunHeight.Length - 1; i > 0; i--)
			{
				LevelGround.reunHeight[i] = LevelGround.reunHeight[i - 1];
			}
			LevelGround.reunHeight[0] = LevelGround.data.GetHeights(0, 0, LevelGround.data.heightmapWidth, LevelGround.data.heightmapHeight);
		}

		// Token: 0x06002491 RID: 9361 RVA: 0x000CC150 File Offset: 0x000CA550
		public static void registerHeight2()
		{
			if (LevelGround.frameHeight2 > 0)
			{
				float[,] array = LevelGround.reunHeight2[LevelGround.frameHeight2];
				LevelGround.reunHeight2 = new float[LevelGround.reunHeight2.Length][,];
				LevelGround.frameHeight2 = 0;
				LevelGround.reunHeight2[0] = array;
			}
			for (int i = LevelGround.reunHeight2.Length - 1; i > 0; i--)
			{
				LevelGround.reunHeight2[i] = LevelGround.reunHeight2[i - 1];
			}
			LevelGround.reunHeight2[0] = LevelGround.data2.GetHeights(0, 0, LevelGround.data2.heightmapWidth, LevelGround.data2.heightmapHeight);
		}

		// Token: 0x06002492 RID: 9362 RVA: 0x000CC1E4 File Offset: 0x000CA5E4
		public static void undoMaterial()
		{
			if (LevelGround.frameMaterial <= LevelGround.reunMaterial.Length - 1)
			{
				if (LevelGround.frameMaterial < LevelGround.reunMaterial.Length && LevelGround.reunMaterial[LevelGround.frameMaterial + 1] != null)
				{
					LevelGround.frameMaterial++;
				}
				if (LevelGround.reunMaterial[LevelGround.frameMaterial] != null)
				{
					LevelGround.data.SetAlphamaps(0, 0, LevelGround.reunMaterial[LevelGround.frameMaterial]);
					LevelGround.alphamap = LevelGround.data.GetAlphamaps(0, 0, LevelGround.data.alphamapWidth, LevelGround.data.alphamapHeight);
				}
			}
		}

		// Token: 0x06002493 RID: 9363 RVA: 0x000CC280 File Offset: 0x000CA680
		public static void undoMaterial2()
		{
			if (LevelGround.frameMaterial2 <= LevelGround.reunMaterial2.Length - 1)
			{
				if (LevelGround.frameMaterial2 < LevelGround.reunMaterial2.Length && LevelGround.reunMaterial2[LevelGround.frameMaterial2 + 1] != null)
				{
					LevelGround.frameMaterial2++;
				}
				if (LevelGround.reunMaterial2[LevelGround.frameMaterial2] != null)
				{
					LevelGround.data2.SetAlphamaps(0, 0, LevelGround.reunMaterial2[LevelGround.frameMaterial2]);
					LevelGround.alphamap2 = LevelGround.data2.GetAlphamaps(0, 0, LevelGround.data2.alphamapWidth, LevelGround.data2.alphamapHeight);
				}
			}
		}

		// Token: 0x06002494 RID: 9364 RVA: 0x000CC31C File Offset: 0x000CA71C
		public static void redoMaterial()
		{
			if (LevelGround.frameMaterial >= 0)
			{
				if (LevelGround.frameMaterial > 0 && LevelGround.reunMaterial[LevelGround.frameMaterial - 1] != null)
				{
					LevelGround.frameMaterial--;
				}
				if (LevelGround.reunMaterial[LevelGround.frameMaterial] != null)
				{
					LevelGround.data.SetAlphamaps(0, 0, LevelGround.reunMaterial[LevelGround.frameMaterial]);
					LevelGround.alphamap = LevelGround.data.GetAlphamaps(0, 0, LevelGround.data.alphamapWidth, LevelGround.data.alphamapHeight);
				}
			}
		}

		// Token: 0x06002495 RID: 9365 RVA: 0x000CC3AC File Offset: 0x000CA7AC
		public static void redoMaterial2()
		{
			if (LevelGround.frameMaterial2 >= 0)
			{
				if (LevelGround.frameMaterial2 > 0 && LevelGround.reunMaterial2[LevelGround.frameMaterial2 - 1] != null)
				{
					LevelGround.frameMaterial2--;
				}
				if (LevelGround.reunMaterial2[LevelGround.frameMaterial2] != null)
				{
					LevelGround.data2.SetAlphamaps(0, 0, LevelGround.reunMaterial2[LevelGround.frameMaterial2]);
					LevelGround.alphamap2 = LevelGround.data2.GetAlphamaps(0, 0, LevelGround.data2.alphamapWidth, LevelGround.data2.alphamapHeight);
				}
			}
		}

		// Token: 0x06002496 RID: 9366 RVA: 0x000CC43C File Offset: 0x000CA83C
		public static void registerMaterial()
		{
			if (LevelGround.frameMaterial > 0)
			{
				float[,,] array = LevelGround.reunMaterial[LevelGround.frameMaterial];
				LevelGround.reunMaterial = new float[LevelGround.reunMaterial.Length][,,];
				LevelGround.frameMaterial = 0;
				LevelGround.reunMaterial[0] = array;
			}
			for (int i = LevelGround.reunMaterial.Length - 1; i > 0; i--)
			{
				LevelGround.reunMaterial[i] = LevelGround.reunMaterial[i - 1];
			}
			LevelGround.reunMaterial[0] = LevelGround.data.GetAlphamaps(0, 0, LevelGround.data.alphamapWidth, LevelGround.data.alphamapHeight);
		}

		// Token: 0x06002497 RID: 9367 RVA: 0x000CC4D0 File Offset: 0x000CA8D0
		public static void registerMaterial2()
		{
			if (LevelGround.frameMaterial2 > 0)
			{
				float[,,] array = LevelGround.reunMaterial[LevelGround.frameMaterial2];
				LevelGround.reunMaterial2 = new float[LevelGround.reunMaterial2.Length][,,];
				LevelGround.frameMaterial2 = 0;
				LevelGround.reunMaterial2[0] = array;
			}
			for (int i = LevelGround.reunMaterial2.Length - 1; i > 0; i--)
			{
				LevelGround.reunMaterial2[i] = LevelGround.reunMaterial2[i - 1];
			}
			LevelGround.reunMaterial2[0] = LevelGround.data2.GetAlphamaps(0, 0, LevelGround.data2.alphamapWidth, LevelGround.data2.alphamapHeight);
		}

		// Token: 0x06002498 RID: 9368 RVA: 0x000CC564 File Offset: 0x000CA964
		public static GroundMaterial getMaterial(Vector3 point)
		{
			if (LevelGround.terrain == null)
			{
				return null;
			}
			return LevelGround.materials[LevelGround.getMaterialIndex(point)];
		}

		// Token: 0x06002499 RID: 9369 RVA: 0x000CC584 File Offset: 0x000CA984
		public static int getMaterialIndex(Vector3 point)
		{
			int alphamap_X = LevelGround.getAlphamap_X(point);
			if (alphamap_X < 0 || alphamap_X >= LevelGround.data.alphamapWidth)
			{
				return 0;
			}
			int alphamap_Y = LevelGround.getAlphamap_Y(point);
			if (alphamap_Y < 0 || alphamap_Y >= LevelGround.data.alphamapWidth)
			{
				return 0;
			}
			float[,,] alphamaps = LevelGround.data.GetAlphamaps(alphamap_X, alphamap_Y, 1, 1);
			float num = 0f;
			int result = 0;
			for (int i = 0; i < LevelGround.materials.Length; i++)
			{
				if (alphamaps[0, 0, i] > num)
				{
					result = i;
					num = alphamaps[0, 0, i];
				}
			}
			return result;
		}

		// Token: 0x0600249A RID: 9370 RVA: 0x000CC628 File Offset: 0x000CAA28
		public static float getHeight(Vector3 point)
		{
			if (LevelGround.terrain == null)
			{
				float result;
				Landscape.getWorldHeight(point, out result);
				return result;
			}
			return LevelGround.terrain.SampleHeight(point);
		}

		// Token: 0x0600249B RID: 9371 RVA: 0x000CC65C File Offset: 0x000CAA5C
		public static float getConversionHeight(Vector3 point)
		{
			if (point.x < (float)(-Level.size / 2) || point.z < (float)(-Level.size / 2) || point.x > (float)(Level.size / 2) || point.z > (float)(Level.size / 2))
			{
				return LevelGround.terrain2.SampleHeight(point);
			}
			return Mathf.Max(LevelGround.terrain.SampleHeight(point), LevelGround.terrain2.SampleHeight(point));
		}

		// Token: 0x0600249C RID: 9372 RVA: 0x000CC6E4 File Offset: 0x000CAAE4
		public static float getConversionWeight(Vector3 point, int layer, bool hq)
		{
			LevelGround.previewHQ = hq;
			if (point.x < (float)(-Level.size / 2) || point.z < (float)(-Level.size / 2) || point.x > (float)(Level.size / 2) || point.z > (float)(Level.size / 2) || LevelGround.terrain2.SampleHeight(point) > LevelGround.terrain.SampleHeight(point))
			{
				int alphamap2_X = LevelGround.getAlphamap2_X(point);
				if (alphamap2_X < 0 || alphamap2_X >= LevelGround.data2.alphamapWidth)
				{
					return 0f;
				}
				int alphamap2_Y = LevelGround.getAlphamap2_Y(point);
				if (alphamap2_Y < 0 || alphamap2_Y >= LevelGround.data2.alphamapWidth)
				{
					return 0f;
				}
				float[,,] alphamaps = LevelGround.data2.GetAlphamaps(alphamap2_X, alphamap2_Y, 1, 1);
				return alphamaps[0, 0, layer];
			}
			else
			{
				int alphamap_X = LevelGround.getAlphamap_X(point);
				if (alphamap_X < 0 || alphamap_X >= LevelGround.data.alphamapWidth)
				{
					return 0f;
				}
				int alphamap_Y = LevelGround.getAlphamap_Y(point);
				if (alphamap_Y < 0 || alphamap_Y >= LevelGround.data.alphamapWidth)
				{
					return 0f;
				}
				float[,,] alphamaps2 = LevelGround.data.GetAlphamaps(alphamap_X, alphamap_Y, 1, 1);
				return alphamaps2[0, 0, layer];
			}
		}

		// Token: 0x0600249D RID: 9373 RVA: 0x000CC82C File Offset: 0x000CAC2C
		public static Vector3 getNormal(Vector3 point)
		{
			if (LevelGround.terrain == null)
			{
				Vector3 result;
				Landscape.getNormal(point, out result);
				return result;
			}
			return LevelGround.data.GetInterpolatedNormal((point.x - LevelGround.terrain.transform.position.x) / LevelGround.data.size.x, (point.z - LevelGround.terrain.transform.position.z) / LevelGround.data.size.z);
		}

		// Token: 0x0600249E RID: 9374 RVA: 0x000CC8C4 File Offset: 0x000CACC4
		public static int getDetail_X(Vector3 point)
		{
			return (int)((point.x - LevelGround.terrain.transform.position.x) / LevelGround.data.size.x * (float)LevelGround.data.detailWidth);
		}

		// Token: 0x0600249F RID: 9375 RVA: 0x000CC910 File Offset: 0x000CAD10
		public static int getDetail_Y(Vector3 point)
		{
			return (int)((point.z - LevelGround.terrain.transform.position.z) / LevelGround.data.size.z * (float)LevelGround.data.detailHeight);
		}

		// Token: 0x060024A0 RID: 9376 RVA: 0x000CC95C File Offset: 0x000CAD5C
		public static int getDetail2_X(Vector3 point)
		{
			return (int)((point.x - LevelGround.terrain2.transform.position.x) / LevelGround.data2.size.x * (float)LevelGround.data2.detailWidth);
		}

		// Token: 0x060024A1 RID: 9377 RVA: 0x000CC9A8 File Offset: 0x000CADA8
		public static int getDetail2_Y(Vector3 point)
		{
			return (int)((point.z - LevelGround.terrain2.transform.position.z) / LevelGround.data2.size.z * (float)LevelGround.data2.detailHeight);
		}

		// Token: 0x060024A2 RID: 9378 RVA: 0x000CC9F4 File Offset: 0x000CADF4
		public static int getAlphamap_X(Vector3 point)
		{
			return (int)((point.x - LevelGround.terrain.transform.position.x) / LevelGround.data.size.x * (float)LevelGround.data.alphamapWidth);
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x000CCA40 File Offset: 0x000CAE40
		public static int getAlphamap_Y(Vector3 point)
		{
			return (int)((point.z - LevelGround.terrain.transform.position.z) / LevelGround.data.size.z * (float)LevelGround.data.alphamapHeight);
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x000CCA8C File Offset: 0x000CAE8C
		public static int getAlphamap2_X(Vector3 point)
		{
			return (int)((point.x - LevelGround.terrain2.transform.position.x) / LevelGround.data2.size.x * (float)LevelGround.data2.alphamapWidth);
		}

		// Token: 0x060024A5 RID: 9381 RVA: 0x000CCAD8 File Offset: 0x000CAED8
		public static int getAlphamap2_Y(Vector3 point)
		{
			return (int)((point.z - LevelGround.terrain2.transform.position.z) / LevelGround.data2.size.z * (float)LevelGround.data2.alphamapHeight);
		}

		// Token: 0x060024A6 RID: 9382 RVA: 0x000CCB24 File Offset: 0x000CAF24
		public static int getHeightmap_X(Vector3 point)
		{
			return (int)((point.x - LevelGround.terrain.transform.position.x) / LevelGround.data.size.x * (float)LevelGround.data.heightmapWidth);
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x000CCB70 File Offset: 0x000CAF70
		public static int getHeightmap_Y(Vector3 point)
		{
			return (int)((point.z - LevelGround.terrain.transform.position.z) / LevelGround.data.size.z * (float)LevelGround.data.heightmapHeight);
		}

		// Token: 0x060024A8 RID: 9384 RVA: 0x000CCBBC File Offset: 0x000CAFBC
		public static int getHeightmap2_X(Vector3 point)
		{
			return (int)((point.x - LevelGround.terrain2.transform.position.x) / LevelGround.data2.size.x * (float)LevelGround.data2.heightmapWidth);
		}

		// Token: 0x060024A9 RID: 9385 RVA: 0x000CCC08 File Offset: 0x000CB008
		public static int getHeightmap2_Y(Vector3 point)
		{
			return (int)((point.z - LevelGround.terrain2.transform.position.z) / LevelGround.data2.size.z * (float)LevelGround.data2.heightmapHeight);
		}

		// Token: 0x060024AA RID: 9386 RVA: 0x000CCC54 File Offset: 0x000CB054
		public static void bewilder(Vector3 point)
		{
			if (Dedicator.isDedicated)
			{
				return;
			}
			if (LevelGround.terrain == null)
			{
				FoliageSystem.addCut(new AACylinderVolume(point, 6f, 8f));
				return;
			}
			int detail_X = LevelGround.getDetail_X(point);
			int detail_Y = LevelGround.getDetail_Y(point);
			int[,] details = new int[4, 4];
			for (int i = 0; i < LevelGround.details.Length; i++)
			{
				LevelGround.data.SetDetailLayer(detail_X - 2, detail_Y - 2, i, details);
			}
		}

		// Token: 0x060024AB RID: 9387 RVA: 0x000CCCD8 File Offset: 0x000CB0D8
		public static void paint(Vector3 point, int size, float noise, int layer, bool edit2)
		{
			if (edit2)
			{
				size = (int)((float)size * 0.75f);
			}
			else
			{
				size *= 3;
			}
			if (size == 0)
			{
				return;
			}
			int num;
			int num2;
			if (edit2)
			{
				num = LevelGround.getAlphamap2_X(point);
				num2 = LevelGround.getAlphamap2_Y(point);
			}
			else
			{
				num = LevelGround.getAlphamap_X(point);
				num2 = LevelGround.getAlphamap_Y(point);
			}
			int num3 = 0;
			int num4 = 0;
			int num5 = num - size / 2;
			if (num5 < 0)
			{
				num3 = -num5;
				num5 = 0;
			}
			int num6 = num2 - size / 2;
			if (num6 < 0)
			{
				num4 = -num6;
				num6 = 0;
			}
			int num7 = num + size / 2;
			int num8 = num2 + size / 2;
			if (edit2)
			{
				if (num7 > LevelGround.data2.alphamapWidth)
				{
					num7 = LevelGround.data2.alphamapWidth;
				}
				if (num8 > LevelGround.data2.alphamapHeight)
				{
					num8 = LevelGround.data2.alphamapHeight;
				}
			}
			else
			{
				if (num7 > LevelGround.data.alphamapWidth)
				{
					num7 = LevelGround.data.alphamapWidth;
				}
				if (num8 > LevelGround.data.alphamapHeight)
				{
					num8 = LevelGround.data.alphamapHeight;
				}
			}
			int num9 = num7 - num5;
			int num10 = num8 - num6;
			float[,,] alphamaps;
			if (edit2)
			{
				alphamaps = LevelGround.data2.GetAlphamaps(num5, num6, num9, num10);
			}
			else
			{
				alphamaps = LevelGround.data.GetAlphamaps(num5, num6, num9, num10);
			}
			for (int i = 0; i < num10; i++)
			{
				for (int j = 0; j < num9; j++)
				{
					float r = LevelGround.MASK.GetPixel((int)((float)(i + num4) / (float)size * (float)LevelGround.MASK.width), (int)((float)(j + num3) / (float)size * (float)LevelGround.MASK.height)).r;
					if ((double)r > 0.5 && UnityEngine.Random.value > noise)
					{
						for (int k = 0; k < LevelGround.materials.Length; k++)
						{
							if (k == layer)
							{
								alphamaps[i, j, k] = 1f;
							}
							else
							{
								alphamaps[i, j, k] = 0f;
							}
							if (edit2)
							{
								LevelGround.alphamap2[num6 + i, num5 + j, k] = alphamaps[i, j, k];
							}
							else
							{
								LevelGround.alphamap[num6 + i, num5 + j, k] = alphamaps[i, j, k];
							}
						}
					}
				}
			}
			if (edit2)
			{
				LevelGround.data2.SetAlphamaps(num5, num6, alphamaps);
			}
			else
			{
				LevelGround.data.SetAlphamaps(num5, num6, alphamaps);
			}
		}

		// Token: 0x060024AC RID: 9388 RVA: 0x000CCF8C File Offset: 0x000CB38C
		public static void adjust(Vector3 point, int size, float strength, float noise, bool edit2)
		{
			if (edit2)
			{
				size = (int)((float)size * 0.375f);
			}
			else
			{
				size = (int)((float)size * 1.5f);
			}
			if (size == 0)
			{
				return;
			}
			int num;
			int num2;
			if (edit2)
			{
				num = LevelGround.getHeightmap2_X(point);
				num2 = LevelGround.getHeightmap2_Y(point);
			}
			else
			{
				num = LevelGround.getHeightmap_X(point);
				num2 = LevelGround.getHeightmap_Y(point);
			}
			int num3 = 0;
			int num4 = 0;
			int num5 = num - size / 2;
			if (num5 < 0)
			{
				num3 = -num5;
				num5 = 0;
			}
			int num6 = num2 - size / 2;
			if (num6 < 0)
			{
				num4 = -num6;
				num6 = 0;
			}
			int num7 = num + size / 2;
			int num8 = num2 + size / 2;
			if (edit2)
			{
				if (num7 > LevelGround.data2.heightmapWidth)
				{
					num7 = LevelGround.data2.heightmapWidth;
				}
				if (num8 > LevelGround.data2.heightmapHeight)
				{
					num8 = LevelGround.data2.heightmapHeight;
				}
			}
			else
			{
				if (num7 > LevelGround.data.heightmapWidth)
				{
					num7 = LevelGround.data.heightmapWidth;
				}
				if (num8 > LevelGround.data.heightmapHeight)
				{
					num8 = LevelGround.data.heightmapHeight;
				}
			}
			int num9 = num7 - num5;
			int num10 = num8 - num6;
			float[,] heights;
			if (edit2)
			{
				heights = LevelGround.data2.GetHeights(num5, num6, num9, num10);
			}
			else
			{
				heights = LevelGround.data.GetHeights(num5, num6, num9, num10);
			}
			for (int i = 0; i < num10; i++)
			{
				for (int j = 0; j < num9; j++)
				{
					if (UnityEngine.Random.value > noise)
					{
						float r = LevelGround.MASK.GetPixel((int)((float)(i + num4) / (float)size * (float)LevelGround.MASK.width), (int)((float)(j + num3) / (float)size * (float)LevelGround.MASK.height)).r;
						heights[i, j] += Time.deltaTime * 0.25f * r * strength;
						if (heights[i, j] < 0f)
						{
							heights[i, j] = 0f;
						}
						else if (heights[i, j] > 1f)
						{
							heights[i, j] = 1f;
						}
					}
				}
			}
			if (edit2)
			{
				LevelGround.data2.SetHeights(num5, num6, heights);
				LevelGround.terrain2.Flush();
			}
			else
			{
				LevelGround.data.SetHeights(num5, num6, heights);
				LevelGround.terrain.Flush();
			}
		}

		// Token: 0x060024AD RID: 9389 RVA: 0x000CD220 File Offset: 0x000CB620
		public static void smooth(Vector3 point, int size, float strength, float noise, bool edit2)
		{
			if (edit2)
			{
				size = (int)((float)size * 0.375f);
			}
			else
			{
				size = (int)((float)size * 1.5f);
			}
			if (size == 0)
			{
				return;
			}
			int num;
			int num2;
			if (edit2)
			{
				num = LevelGround.getHeightmap2_X(point);
				num2 = LevelGround.getHeightmap2_Y(point);
			}
			else
			{
				num = LevelGround.getHeightmap_X(point);
				num2 = LevelGround.getHeightmap_Y(point);
			}
			int num3 = num - size / 2;
			if (num3 < 0)
			{
				num3 = 0;
			}
			int num4 = num2 - size / 2;
			if (num4 < 0)
			{
				num4 = 0;
			}
			int num5 = num + size / 2;
			int num6 = num2 + size / 2;
			if (edit2)
			{
				if (num5 > LevelGround.data2.heightmapWidth)
				{
					num5 = LevelGround.data2.heightmapWidth;
				}
				if (num6 > LevelGround.data2.heightmapHeight)
				{
					num6 = LevelGround.data2.heightmapHeight;
				}
			}
			else
			{
				if (num5 > LevelGround.data.heightmapWidth)
				{
					num5 = LevelGround.data.heightmapWidth;
				}
				if (num6 > LevelGround.data.heightmapHeight)
				{
					num6 = LevelGround.data.heightmapHeight;
				}
			}
			int num7 = num5 - num3;
			int num8 = num6 - num4;
			float[,] heights;
			if (edit2)
			{
				heights = LevelGround.data2.GetHeights(num3, num4, num7, num8);
			}
			else
			{
				heights = LevelGround.data.GetHeights(num3, num4, num7, num8);
			}
			float[,] array = new float[num8, num7];
			for (int i = 0; i < num8; i++)
			{
				for (int j = 0; j < num7; j++)
				{
					float r = LevelGround.MASK.GetPixel((int)((float)i / (float)size * (float)LevelGround.MASK.width), (int)((float)j / (float)size * (float)LevelGround.MASK.height)).r;
					float num9 = 0f;
					int num10 = 1;
					num9 += heights[i, j];
					if (i > 0)
					{
						num10++;
						num9 += heights[i - 1, j];
					}
					if (j > 0)
					{
						num10++;
						num9 += heights[i, j - 1];
					}
					if (i < num8 - 1)
					{
						num10++;
						num9 += heights[i + 1, j];
					}
					if (j < num7 - 1)
					{
						num10++;
						num9 += heights[i, j + 1];
					}
					num9 /= (float)num10;
					array[i, j] = Mathf.Lerp(heights[i, j], num9, r * strength);
				}
			}
			if (edit2)
			{
				LevelGround.data2.SetHeights(num3, num4, array);
				LevelGround.terrain2.Flush();
			}
			else
			{
				LevelGround.data.SetHeights(num3, num4, array);
				LevelGround.terrain.Flush();
			}
		}

		// Token: 0x060024AE RID: 9390 RVA: 0x000CD4E8 File Offset: 0x000CB8E8
		public static void flatten(Vector3 point, int size, float height, float strength, float noise, bool edit2)
		{
			if (edit2)
			{
				size = (int)((float)size * 0.375f);
			}
			else
			{
				size = (int)((float)size * 1.5f);
			}
			if (size == 0)
			{
				return;
			}
			int num;
			int num2;
			if (edit2)
			{
				num = LevelGround.getHeightmap2_X(point);
				num2 = LevelGround.getHeightmap2_Y(point);
			}
			else
			{
				num = LevelGround.getHeightmap_X(point);
				num2 = LevelGround.getHeightmap_Y(point);
			}
			int num3 = 0;
			int num4 = 0;
			int num5 = num - size / 2;
			if (num5 < 0)
			{
				num3 = -num5;
				num5 = 0;
			}
			int num6 = num2 - size / 2;
			if (num6 < 0)
			{
				num4 = -num6;
				num6 = 0;
			}
			int num7 = num + size / 2;
			int num8 = num2 + size / 2;
			if (edit2)
			{
				if (num7 > LevelGround.data2.heightmapWidth)
				{
					num7 = LevelGround.data2.heightmapWidth;
				}
				if (num8 > LevelGround.data2.heightmapHeight)
				{
					num8 = LevelGround.data2.heightmapHeight;
				}
			}
			else
			{
				if (num7 > LevelGround.data.heightmapWidth)
				{
					num7 = LevelGround.data.heightmapWidth;
				}
				if (num8 > LevelGround.data.heightmapHeight)
				{
					num8 = LevelGround.data.heightmapHeight;
				}
			}
			int num9 = num7 - num5;
			int num10 = num8 - num6;
			float[,] heights;
			if (edit2)
			{
				heights = LevelGround.data2.GetHeights(num5, num6, num9, num10);
			}
			else
			{
				heights = LevelGround.data.GetHeights(num5, num6, num9, num10);
			}
			for (int i = 0; i < num10; i++)
			{
				for (int j = 0; j < num9; j++)
				{
					if (UnityEngine.Random.value > noise)
					{
						float r = LevelGround.MASK.GetPixel((int)((float)(i + num4) / (float)size * (float)LevelGround.MASK.width), (int)((float)(j + num3) / (float)size * (float)LevelGround.MASK.height)).r;
						heights[i, j] = Mathf.Lerp(heights[i, j], height, r * strength);
					}
				}
			}
			if (edit2)
			{
				LevelGround.data2.SetHeights(num5, num6, heights);
				LevelGround.terrain2.Flush();
			}
			else
			{
				LevelGround.data.SetHeights(num5, num6, heights);
				LevelGround.terrain.Flush();
			}
		}

		// Token: 0x060024AF RID: 9391 RVA: 0x000CD728 File Offset: 0x000CBB28
		private static void bakeMaterials(int x, int y, float[,,] alphamap, TerrainData dat, int[] steepnessSorted, float steepnessMultiplier, int[] heightSorted)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			for (int i = 0; i < LevelGround.materials.Length; i++)
			{
				if (LevelGround.materials[i].isManual && (double)alphamap[x, y, i] > 0.5)
				{
					flag = true;
					flag2 = LevelGround.materials[i].ignoreSteepness;
					flag3 = LevelGround.materials[i].ignoreHeight;
					for (int j = 0; j < LevelGround.materials.Length; j++)
					{
						if (j == i)
						{
							alphamap[x, y, j] = 1f;
						}
						else
						{
							alphamap[x, y, j] = 0f;
						}
					}
					break;
				}
			}
			if (flag2 && flag3)
			{
				return;
			}
			if (!flag2)
			{
				float num = dat.GetSteepness((float)y / (float)dat.alphamapWidth, (float)x / (float)dat.alphamapHeight) / steepnessMultiplier;
				for (int k = 0; k < LevelGround.materials.Length; k++)
				{
					if (num >= LevelGround.materials[steepnessSorted[k]].steepness * (1f - LevelGround.materials[steepnessSorted[k]].transition) && LevelGround.materials[steepnessSorted[k]].steepness >= 0.01f)
					{
						int num2 = steepnessSorted[k];
						if (k >= LevelGround.materials.Length - 1 || LevelGround.materials[heightSorted[k + 1]].steepness < 0.01f)
						{
							if (flag)
							{
								float num3 = (num - LevelGround.materials[steepnessSorted[k]].steepness * (1f - LevelGround.materials[steepnessSorted[k]].transition)) / (LevelGround.materials[steepnessSorted[k]].steepness * LevelGround.materials[steepnessSorted[k]].transition);
								if (UnityEngine.Random.value > num3)
								{
									return;
								}
							}
							else if (!flag3)
							{
								float num4 = dat.GetInterpolatedHeight((float)y / (float)dat.alphamapWidth, (float)x / (float)dat.alphamapHeight) / dat.size.y;
								if (num4 >= LevelGround.materials[heightSorted[LevelGround.materials.Length - 1]].height && LevelGround.materials[heightSorted[LevelGround.materials.Length - 1]].height <= 0.99f)
								{
									float num5 = (num - LevelGround.materials[steepnessSorted[k]].steepness * (1f - LevelGround.materials[steepnessSorted[k]].transition)) / (LevelGround.materials[steepnessSorted[k]].steepness * LevelGround.materials[steepnessSorted[k]].transition);
									if (UnityEngine.Random.value > num5)
									{
										break;
									}
								}
							}
						}
						for (int l = 0; l < LevelGround.materials.Length; l++)
						{
							if (l == num2)
							{
								alphamap[x, y, l] = 1f;
							}
							else
							{
								alphamap[x, y, l] = 0f;
							}
						}
						return;
					}
				}
			}
			if (!flag3)
			{
				float num6 = dat.GetInterpolatedHeight((float)y / (float)dat.alphamapWidth, (float)x / (float)dat.alphamapHeight) / dat.size.y;
				for (int m = 0; m < LevelGround.materials.Length; m++)
				{
					if (num6 >= LevelGround.materials[heightSorted[m]].height && LevelGround.materials[heightSorted[m]].height <= 0.99f)
					{
						int num7 = heightSorted[m];
						if (m > 0 && LevelGround.materials[heightSorted[m - 1]].height <= 0.99f)
						{
							float num8 = (LevelGround.materials[heightSorted[m - 1]].height - num6) / ((LevelGround.materials[heightSorted[m - 1]].height - LevelGround.materials[heightSorted[m]].height) * LevelGround.materials[heightSorted[m]].transition);
							if (UnityEngine.Random.value > num8)
							{
								num7 = heightSorted[m - 1];
							}
						}
						for (int n = 0; n < LevelGround.materials.Length; n++)
						{
							if (n == num7)
							{
								alphamap[x, y, n] = 1f;
							}
							else
							{
								alphamap[x, y, n] = 0f;
							}
						}
						return;
					}
				}
			}
			if (flag)
			{
				return;
			}
			for (int num9 = 0; num9 < LevelGround.materials.Length; num9++)
			{
				alphamap[x, y, num9] = 0f;
			}
			alphamap[x, y, 0] = 1f;
		}

		// Token: 0x060024B0 RID: 9392 RVA: 0x000CDBC0 File Offset: 0x000CBFC0
		private static float[,,] blurMaterials(float[,,] alphamap, int width, int height)
		{
			float[,,] array = new float[width, height, LevelGround.materials.Length];
			for (int i = 0; i < LevelGround.materials.Length; i++)
			{
				for (int j = 0; j < width; j++)
				{
					for (int k = 0; k < height; k++)
					{
						if (j > 0 && k > 0 && j < width - 1 && k < height - 1)
						{
							array[j, k, i] = 0.25f * alphamap[j - 1, k, i] + 0.25f * alphamap[j, k - 1, i] + 0.25f * alphamap[j + 1, k, i] + 0.25f * alphamap[j, k + 1, i];
						}
						else
						{
							array[j, k, i] = alphamap[j, k, i];
						}
					}
				}
			}
			for (int l = 0; l < LevelGround.materials.Length; l++)
			{
				for (int m = 0; m < width; m++)
				{
					for (int n = 0; n < height; n++)
					{
						if (m > 0 && n > 0 && m < width - 1 && n < height - 1)
						{
							alphamap[m, n, l] = 0.25f * array[m - 1, n, l] + 0.25f * array[m, n - 1, l] + 0.25f * array[m + 1, n, l] + 0.25f * array[m, n + 1, l];
						}
						else
						{
							alphamap[m, n, l] = array[m, n, l];
						}
					}
				}
			}
			for (int num = 0; num < LevelGround.materials.Length; num++)
			{
				for (int num2 = 0; num2 < width; num2++)
				{
					for (int num3 = 0; num3 < height; num3++)
					{
						if (num2 > 0 && num3 > 0 && num2 < width - 1 && num3 < height - 1)
						{
							array[num2, num3, num] = 0.25f * alphamap[num2 - 1, num3, num] + 0.25f * alphamap[num2, num3 - 1, num] + 0.25f * alphamap[num2 + 1, num3, num] + 0.25f * alphamap[num2, num3 + 1, num];
						}
						else
						{
							array[num2, num3, num] = alphamap[num2, num3, num];
						}
					}
				}
			}
			return array;
		}

		// Token: 0x060024B1 RID: 9393 RVA: 0x000CDE70 File Offset: 0x000CC270
		public static void bakeMaterials(bool quality)
		{
			LevelGround.previewHQ = false;
			if (LevelGround.data.alphamapLayers > 0)
			{
				int[] array = new int[LevelGround.materials.Length];
				List<int> list = new List<int>
				{
					0,
					1,
					2,
					3,
					4,
					5,
					6,
					7
				};
				for (int i = 0; i < LevelGround.materials.Length; i++)
				{
					float num = 0f;
					int index = 0;
					for (int j = 0; j < list.Count; j++)
					{
						if (LevelGround.materials[list[j]].steepness * (1f - LevelGround.materials[list[j]].transition) >= num)
						{
							num = LevelGround.materials[list[j]].steepness;
							index = j;
						}
					}
					array[i] = list[index];
					list.RemoveAt(index);
				}
				int[] array2 = new int[LevelGround.materials.Length];
				List<int> list2 = new List<int>
				{
					0,
					1,
					2,
					3,
					4,
					5,
					6,
					7
				};
				for (int k = 0; k < LevelGround.materials.Length; k++)
				{
					float num2 = 0f;
					int index2 = 0;
					for (int l = 0; l < list2.Count; l++)
					{
						if (LevelGround.materials[list2[l]].height >= num2)
						{
							num2 = LevelGround.materials[list2[l]].height;
							index2 = l;
						}
					}
					array2[k] = list2[index2];
					list2.RemoveAt(index2);
				}
				LevelGround.alphamapHQ = LevelGround.data.GetAlphamaps(0, 0, LevelGround.data.alphamapWidth, LevelGround.data.alphamapHeight);
				for (int m = 0; m < LevelGround.data.alphamapWidth; m++)
				{
					for (int n = 0; n < LevelGround.data.alphamapHeight; n++)
					{
						LevelGround.bakeMaterials(m, n, LevelGround.alphamapHQ, LevelGround.data, array, 64f, array2);
					}
				}
				if (quality)
				{
					for (byte b = 0; b < Regions.WORLD_SIZE; b += 1)
					{
						for (byte b2 = 0; b2 < Regions.WORLD_SIZE; b2 += 1)
						{
							if (!LevelObjects.regions[(int)b, (int)b2])
							{
								List<LevelObject> list3 = LevelObjects.objects[(int)b, (int)b2];
								for (int num3 = 0; num3 < list3.Count; num3++)
								{
									list3[num3].enableCollision();
								}
							}
						}
					}
					for (int num4 = 2; num4 < LevelGround.data.alphamapWidth - 2; num4++)
					{
						for (int num5 = 2; num5 < LevelGround.data.alphamapHeight - 2; num5++)
						{
							bool flag = false;
							for (int num6 = 0; num6 < LevelGround.materials.Length; num6++)
							{
								if ((double)LevelGround.alphamapHQ[num4, num5, num6] > 0.5 && LevelGround.materials[num6].ignoreFootprint)
								{
									flag = true;
									break;
								}
							}
							if (!flag)
							{
								RaycastHit raycastHit;
								Physics.Raycast(new Vector3(-LevelGround.data.size.x / 2f + (float)num5 / (float)LevelGround.data.alphamapWidth * LevelGround.data.size.x, 256f, -LevelGround.data.size.z / 2f + (float)num4 / (float)LevelGround.data.alphamapHeight * LevelGround.data.size.z), Vector3.down, out raycastHit, 512f, RayMasks.BLOCK_GRASS);
								byte b3;
								byte b4;
								if (raycastHit.transform != null && !WaterUtility.isPointUnderwater(raycastHit.point) && (raycastHit.transform.CompareTag("Large") || raycastHit.transform.CompareTag("Medium") || raycastHit.transform.CompareTag("Environment")) && Regions.tryGetCoordinate(raycastHit.transform.position, out b3, out b4))
								{
									bool flag2 = true;
									for (int num7 = 0; num7 < LevelObjects.objects[(int)b3, (int)b4].Count; num7++)
									{
										LevelObject levelObject = LevelObjects.objects[(int)b3, (int)b4][num7];
										if (levelObject.transform == raycastHit.transform)
										{
											ObjectAsset objectAsset = (ObjectAsset)Assets.find(EAssetType.OBJECT, levelObject.id);
											if (objectAsset == null || objectAsset.isSnowshoe)
											{
												flag2 = false;
											}
											break;
										}
									}
									if (flag2)
									{
										for (int num8 = 0; num8 < LevelGround.materials.Length; num8++)
										{
											if (LevelGround.materials[num8].isFoundation)
											{
												for (int num9 = -1; num9 < 2; num9++)
												{
													for (int num10 = -1; num10 < 2; num10++)
													{
														bool flag3 = true;
														for (int num11 = 0; num11 < LevelGround.materials.Length; num11++)
														{
															if ((double)LevelGround.alphamapHQ[num4 + num9, num5 + num10, num11] > 0.5 && LevelGround.materials[num11].ignoreFootprint)
															{
																flag3 = false;
																break;
															}
														}
														if (flag3)
														{
															for (int num12 = 0; num12 < LevelGround.materials.Length; num12++)
															{
																if (num12 == num8)
																{
																	LevelGround.alphamapHQ[num4 + num9, num5 + num10, num12] = 1f;
																}
																else
																{
																	LevelGround.alphamapHQ[num4 + num9, num5 + num10, num12] = 0f;
																}
															}
														}
													}
												}
												break;
											}
										}
									}
								}
							}
						}
					}
					for (byte b5 = 0; b5 < Regions.WORLD_SIZE; b5 += 1)
					{
						for (byte b6 = 0; b6 < Regions.WORLD_SIZE; b6 += 1)
						{
							if (!LevelObjects.regions[(int)b5, (int)b6])
							{
								List<LevelObject> list4 = LevelObjects.objects[(int)b5, (int)b6];
								for (int num13 = 0; num13 < list4.Count; num13++)
								{
									list4[num13].disableCollision();
								}
							}
						}
					}
					LevelGround.alphamapHQ = LevelGround.blurMaterials(LevelGround.alphamapHQ, LevelGround.data.alphamapWidth, LevelGround.data.alphamapHeight);
					LevelGround.alphamap2HQ = LevelGround.data2.GetAlphamaps(0, 0, LevelGround.data2.alphamapWidth, LevelGround.data2.alphamapHeight);
					for (int num14 = 0; num14 < LevelGround.data2.alphamapWidth; num14++)
					{
						for (int num15 = 0; num15 < LevelGround.data2.alphamapHeight; num15++)
						{
							LevelGround.bakeMaterials(num14, num15, LevelGround.alphamap2HQ, LevelGround.data2, array, 48f, array2);
						}
					}
					LevelGround.alphamap2HQ = LevelGround.blurMaterials(LevelGround.alphamap2HQ, LevelGround.data2.alphamapWidth, LevelGround.data2.alphamapHeight);
				}
			}
			LevelGround.previewHQ = true;
		}

		// Token: 0x060024B2 RID: 9394 RVA: 0x000CE620 File Offset: 0x000CCA20
		public static void bakeDetails()
		{
			if (LevelGround.data == null || LevelGround.terrain == null)
			{
				return;
			}
			if (LevelGround.data.alphamapLayers > 0)
			{
				LevelGround.tempMap = LevelGround.data.GetAlphamaps(0, 0, LevelGround.data.alphamapWidth, LevelGround.data.alphamapHeight);
				int[,] array = new int[LevelGround.data.detailWidth, LevelGround.data.detailHeight];
				for (int i = 0; i < LevelGround.details.Length; i++)
				{
					for (int j = 0; j < LevelGround.data.detailWidth; j++)
					{
						for (int k = 0; k < LevelGround.data.detailHeight; k++)
						{
							array[j, k] = 0;
							float interpolatedHeight = LevelGround.data.GetInterpolatedHeight((float)k / (float)LevelGround.data.detailWidth, (float)j / (float)LevelGround.data.detailHeight);
							if ((Level.info.configData.Allow_Underwater_Features || LevelLighting.seaLevel > 0.99f || interpolatedHeight / Level.TERRAIN > LevelLighting.seaLevel * 0.8f) && UnityEngine.Random.value < LevelGround.details[i].chance)
							{
								for (int l = 0; l < LevelGround.materials.Length; l++)
								{
									if ((double)LevelGround.tempMap[j, k, l] > 0.5 && ((LevelGround.materials[l].isGrassy_0 && LevelGround.details[i].isGrass_0) || (LevelGround.materials[l].isGrassy_1 && LevelGround.details[i].isGrass_1) || (LevelGround.materials[l].isFlowery_0 && LevelGround.details[i].isFlower_0) || (LevelGround.materials[l].isFlowery_1 && LevelGround.details[i].isFlower_1) || (LevelGround.materials[l].isRocky && LevelGround.details[i].isRock) || (LevelGround.materials[l].isRoad && LevelGround.details[i].isRoad) || (LevelGround.materials[l].isSnowy && LevelGround.details[i].isSnow)))
									{
										if (LevelGround.details[i].prototype.prototype != null)
										{
											array[j, k] = (int)(LevelGround.details[i].density * LevelGround.tempMap[j, k, l] * 16f);
										}
										else if (UnityEngine.Random.value < LevelGround.materials[l].chance)
										{
											array[j, k] = (int)(LevelGround.details[i].density * LevelGround.materials[l].overgrowth * LevelGround.tempMap[j, k, l] * 24f);
										}
										if (array[j, k] > 0)
										{
											Vector3 point = new Vector3(-LevelGround.data.size.x / 2f + (float)k / (float)LevelGround.data.detailWidth * LevelGround.data.size.x, interpolatedHeight, -LevelGround.data.size.z / 2f + (float)j / (float)LevelGround.data.detailHeight * LevelGround.data.size.z);
											if (LandscapeHoleUtility.isPointInsideHoleVolume(point))
											{
												array[j, k] = 0;
											}
										}
										break;
									}
								}
							}
						}
					}
					LevelGround.data.SetDetailLayer(0, 0, i, array);
				}
				LevelGround.terrain.Flush();
			}
		}

		// Token: 0x060024B3 RID: 9395 RVA: 0x000CE9F8 File Offset: 0x000CCDF8
		protected static void handlePreBakeTile(FoliageBakeSettings bakeSettings, FoliageTile foliageTile)
		{
			if (!bakeSettings.bakeResources)
			{
				return;
			}
			byte b;
			byte b2;
			if (!Regions.tryGetCoordinate(foliageTile.worldBounds.center, out b, out b2))
			{
				return;
			}
			for (int i = LevelGround.trees[(int)b, (int)b2].Count - 1; i >= 0; i--)
			{
				ResourceSpawnpoint resourceSpawnpoint = LevelGround.trees[(int)b, (int)b2][i];
				if (resourceSpawnpoint.isGenerated)
				{
					Vector3 min = foliageTile.worldBounds.min;
					if (resourceSpawnpoint.point.x >= min.x && resourceSpawnpoint.point.z >= min.z)
					{
						Vector3 max = foliageTile.worldBounds.max;
						if (resourceSpawnpoint.point.x <= max.x && resourceSpawnpoint.point.z <= max.z)
						{
							resourceSpawnpoint.destroy();
							LevelGround.trees[(int)b, (int)b2].RemoveAt(i);
						}
					}
				}
			}
			LevelGround.regions[(int)b, (int)b2] = false;
		}

		// Token: 0x060024B4 RID: 9396 RVA: 0x000CEB35 File Offset: 0x000CCF35
		protected static void handlePostBake()
		{
			LevelGround.onRegionUpdated(byte.MaxValue, byte.MaxValue, EditorArea.instance.region_x, EditorArea.instance.region_y);
		}

		// Token: 0x060024B5 RID: 9397 RVA: 0x000CEB5A File Offset: 0x000CCF5A
		protected static void handleHiearchyLoaded()
		{
			if (!Dedicator.isDedicated)
			{
				LevelGround.bakeDetails();
			}
		}

		// Token: 0x060024B6 RID: 9398 RVA: 0x000CEB6C File Offset: 0x000CCF6C
		public static void addSpawn(Vector3 point, byte index, bool isGenerated = false)
		{
			byte b;
			byte b2;
			if (!Regions.tryGetCoordinate(point, out b, out b2))
			{
				return;
			}
			ResourceSpawnpoint resourceSpawnpoint = new ResourceSpawnpoint(index, LevelGround.resources[(int)index].id, point, isGenerated);
			resourceSpawnpoint.enable();
			resourceSpawnpoint.disableSkybox();
			LevelGround.trees[(int)b, (int)b2].Add(resourceSpawnpoint);
			LevelGround._total++;
		}

		// Token: 0x060024B7 RID: 9399 RVA: 0x000CEBC8 File Offset: 0x000CCFC8
		public static void removeSpawn(Vector3 point, float radius)
		{
			radius *= radius;
			for (byte b = 0; b < Regions.WORLD_SIZE; b += 1)
			{
				for (byte b2 = 0; b2 < Regions.WORLD_SIZE; b2 += 1)
				{
					List<ResourceSpawnpoint> list = new List<ResourceSpawnpoint>();
					for (int i = 0; i < LevelGround.trees[(int)b, (int)b2].Count; i++)
					{
						ResourceSpawnpoint resourceSpawnpoint = LevelGround.trees[(int)b, (int)b2][i];
						if ((resourceSpawnpoint.point - point).sqrMagnitude < radius)
						{
							resourceSpawnpoint.destroy();
						}
						else
						{
							list.Add(resourceSpawnpoint);
						}
					}
					LevelGround._trees[(int)b, (int)b2] = list;
				}
			}
		}

		// Token: 0x060024B8 RID: 9400 RVA: 0x000CEC84 File Offset: 0x000CD084
		public static void bakeGlobalResources()
		{
			if (LevelGround.isBakingResources)
			{
				LevelGround.isBakingResources = false;
				for (byte b = 0; b < Regions.WORLD_SIZE; b += 1)
				{
					for (byte b2 = 0; b2 < Regions.WORLD_SIZE; b2 += 1)
					{
						if (!LevelObjects.regions[(int)b, (int)b2])
						{
							List<LevelObject> list = LevelObjects.objects[(int)b, (int)b2];
							for (int i = 0; i < list.Count; i++)
							{
								list[i].disableCollision();
							}
						}
					}
				}
				return;
			}
			LevelGround.isBakingResources = true;
			LevelGround.isBakingSkyboxResources = false;
			LevelGround.bakeResources_X = 0;
			LevelGround.bakeResources_Y = 0;
			LevelGround.bakeResources_W = Regions.WORLD_SIZE;
			LevelGround.bakeResources_H = Regions.WORLD_SIZE;
			LevelGround.bakeResources_M = 0;
			for (byte b3 = 0; b3 < Regions.WORLD_SIZE; b3 += 1)
			{
				for (byte b4 = 0; b4 < Regions.WORLD_SIZE; b4 += 1)
				{
					if (!LevelObjects.regions[(int)b3, (int)b4])
					{
						List<LevelObject> list2 = LevelObjects.objects[(int)b3, (int)b4];
						for (int j = 0; j < list2.Count; j++)
						{
							list2[j].enableCollision();
						}
					}
				}
			}
			LevelGround.tempMap = LevelGround.data.GetAlphamaps(0, 0, LevelGround.data.alphamapWidth, LevelGround.data.alphamapHeight);
		}

		// Token: 0x060024B9 RID: 9401 RVA: 0x000CEDEC File Offset: 0x000CD1EC
		public static void bakeLocalResources()
		{
			if (LevelGround.isBakingResources)
			{
				LevelGround.isBakingResources = false;
				for (byte b = 0; b < Regions.WORLD_SIZE; b += 1)
				{
					for (byte b2 = 0; b2 < Regions.WORLD_SIZE; b2 += 1)
					{
						if (!LevelObjects.regions[(int)LevelGround.bakeResources_X, (int)LevelGround.bakeResources_Y])
						{
							List<LevelObject> list = LevelObjects.objects[(int)b, (int)b2];
							for (int i = 0; i < list.Count; i++)
							{
								list[i].disableCollision();
							}
						}
					}
				}
				return;
			}
			LevelGround.isBakingResources = true;
			LevelGround.isBakingSkyboxResources = false;
			LevelGround.bakeResources_X = (byte)Mathf.Max((int)(Editor.editor.area.region_x - 1), 0);
			LevelGround.bakeResources_Y = (byte)Mathf.Max((int)(Editor.editor.area.region_y - 1), 0);
			LevelGround.bakeResources_W = (byte)Mathf.Min((int)(Editor.editor.area.region_x + 2), (int)Regions.WORLD_SIZE);
			LevelGround.bakeResources_H = (byte)Mathf.Min((int)(Editor.editor.area.region_y + 2), (int)Regions.WORLD_SIZE);
			LevelGround.bakeResources_M = LevelGround.bakeResources_X;
			for (byte b3 = 0; b3 < Regions.WORLD_SIZE; b3 += 1)
			{
				for (byte b4 = 0; b4 < Regions.WORLD_SIZE; b4 += 1)
				{
					if (!LevelObjects.regions[(int)LevelGround.bakeResources_X, (int)LevelGround.bakeResources_Y])
					{
						List<LevelObject> list2 = LevelObjects.objects[(int)LevelGround.bakeResources_X, (int)LevelGround.bakeResources_Y];
						for (int j = 0; j < list2.Count; j++)
						{
							list2[j].enableCollision();
						}
					}
				}
			}
			LevelGround.tempMap = LevelGround.data.GetAlphamaps(0, 0, LevelGround.data.alphamapWidth, LevelGround.data.alphamapHeight);
		}

		// Token: 0x060024BA RID: 9402 RVA: 0x000CEFC8 File Offset: 0x000CD3C8
		public static void bakeSkyboxResources()
		{
			if (LevelGround.isBakingResources)
			{
				LevelGround.isBakingResources = false;
				for (byte b = 0; b < Regions.WORLD_SIZE; b += 1)
				{
					for (byte b2 = 0; b2 < Regions.WORLD_SIZE; b2 += 1)
					{
						if (!LevelObjects.regions[(int)b, (int)b2])
						{
							List<LevelObject> list = LevelObjects.objects[(int)b, (int)b2];
							for (int i = 0; i < list.Count; i++)
							{
								list[i].disableCollision();
							}
						}
					}
				}
				return;
			}
			LevelGround.isBakingResources = true;
			LevelGround.isBakingSkyboxResources = true;
			LevelGround.bakeResources_X = 0;
			LevelGround.bakeResources_Y = 0;
			LevelGround.bakeResources_W = Regions.WORLD_SIZE;
			LevelGround.bakeResources_H = Regions.WORLD_SIZE;
			LevelGround.bakeResources_M = 0;
			LevelGround.tempMap = LevelGround.data2.GetAlphamaps(0, 0, LevelGround.data2.alphamapWidth, LevelGround.data2.alphamapHeight);
		}

		// Token: 0x060024BB RID: 9403 RVA: 0x000CF0AC File Offset: 0x000CD4AC
		private static bool bakeResource()
		{
			float num = (float)(-4096 + (int)(LevelGround.bakeResources_X * Regions.REGION_SIZE));
			float num2 = (float)(-4096 + (int)(LevelGround.bakeResources_Y * Regions.REGION_SIZE));
			if (LevelGround.isBakingSkyboxResources)
			{
				if (num < LevelGround.terrain.transform.position.x - 512f || num2 < LevelGround.terrain.transform.position.z - 512f || num >= LevelGround.terrain.transform.position.x + LevelGround.data.size.x + 512f || num2 >= LevelGround.terrain.transform.position.z + LevelGround.data.size.z + 512f)
				{
					return true;
				}
				if (num >= LevelGround.terrain.transform.position.x && num2 >= LevelGround.terrain.transform.position.z && num < LevelGround.terrain.transform.position.x + LevelGround.data.size.x && num2 < LevelGround.terrain.transform.position.z + LevelGround.data.size.z)
				{
					return true;
				}
			}
			else if (num < LevelGround.terrain.transform.position.x || num2 < LevelGround.terrain.transform.position.z || num >= LevelGround.terrain.transform.position.x + LevelGround.data.size.x || num2 >= LevelGround.terrain.transform.position.z + LevelGround.data.size.z)
			{
				return true;
			}
			List<ResourceSpawnpoint> list = new List<ResourceSpawnpoint>();
			for (int i = 0; i < LevelGround.trees[(int)LevelGround.bakeResources_X, (int)LevelGround.bakeResources_Y].Count; i++)
			{
				ResourceSpawnpoint resourceSpawnpoint = LevelGround.trees[(int)LevelGround.bakeResources_X, (int)LevelGround.bakeResources_Y][i];
				if (resourceSpawnpoint.isGenerated)
				{
					resourceSpawnpoint.destroy();
				}
				else
				{
					list.Add(resourceSpawnpoint);
				}
			}
			LevelGround.trees[(int)LevelGround.bakeResources_X, (int)LevelGround.bakeResources_Y] = list;
			LevelGround.regions[(int)LevelGround.bakeResources_X, (int)LevelGround.bakeResources_Y] = false;
			for (int j = 0; j < (int)Regions.REGION_SIZE; j += 2)
			{
				for (int k = 0; k < (int)Regions.REGION_SIZE; k += 2)
				{
					byte b = (byte)UnityEngine.Random.Range(0, LevelGround.resources.Length);
					ResourceAsset resourceAsset = (ResourceAsset)Assets.find(EAssetType.RESOURCE, LevelGround.resources[(int)b].id);
					if (!LevelGround.isBakingSkyboxResources || resourceAsset.skyboxGameObject)
					{
						if (UnityEngine.Random.value > 1f - LevelGround.resources[(int)b].density * 0.25f && UnityEngine.Random.value < LevelGround.resources[(int)b].chance)
						{
							int num3;
							int num4;
							if (LevelGround.isBakingSkyboxResources)
							{
								num3 = (int)((num2 + (float)j - LevelGround.terrain2.transform.position.z) / LevelGround.data2.size.z * (float)LevelGround.data2.alphamapHeight);
								num4 = (int)((num + (float)k - LevelGround.terrain2.transform.position.x) / LevelGround.data2.size.x * (float)LevelGround.data2.alphamapWidth);
							}
							else
							{
								num3 = (int)((num2 + (float)j - LevelGround.terrain.transform.position.z) / LevelGround.data.size.z * (float)LevelGround.data.alphamapHeight);
								num4 = (int)((num + (float)k - LevelGround.terrain.transform.position.x) / LevelGround.data.size.x * (float)LevelGround.data.alphamapWidth);
							}
							for (int l = 0; l < LevelGround.materials.Length; l++)
							{
								if ((double)LevelGround.tempMap[num3, num4, l] > 0.5 && ((LevelGround.materials[l].isGrassy_0 && LevelGround.resources[(int)b].isTree_0) || (LevelGround.materials[l].isGrassy_1 && LevelGround.resources[(int)b].isTree_1) || (LevelGround.materials[l].isFlowery_0 && LevelGround.resources[(int)b].isFlower_0) || (LevelGround.materials[l].isFlowery_1 && LevelGround.resources[(int)b].isFlower_1) || (LevelGround.materials[l].isRocky && LevelGround.resources[(int)b].isRock) || (LevelGround.materials[l].isRoad && LevelGround.resources[(int)b].isRoad) || (LevelGround.materials[l].isSnowy && LevelGround.resources[(int)b].isSnow)))
								{
									if (UnityEngine.Random.value < LevelGround.materials[l].chance && (LevelGround.resources[(int)b].isRock || LevelGround.resources[(int)b].isRoad || LevelGround.resources[(int)b].isSnow || UnityEngine.Random.value < LevelGround.materials[l].overgrowth))
									{
										bool flag;
										if (LevelGround.isBakingSkyboxResources)
										{
											flag = ((num3 == 0 || (double)LevelGround.tempMap[num3 - 1, num4, l] > 0.5) && (num4 == 0 || (double)LevelGround.tempMap[num3, num4 - 1, l] > 0.5) && (num3 == LevelGround.data2.alphamapWidth - 1 || (double)LevelGround.tempMap[num3 + 1, num4, l] > 0.5) && (num4 == LevelGround.data2.alphamapHeight - 1 || (double)LevelGround.tempMap[num3, num4 + 1, l] > 0.5));
										}
										else
										{
											flag = ((num3 == 0 || (double)LevelGround.tempMap[num3 - 1, num4, l] > 0.5) && (num4 == 0 || (double)LevelGround.tempMap[num3, num4 - 1, l] > 0.5) && (num3 == LevelGround.data.alphamapWidth - 1 || (double)LevelGround.tempMap[num3 + 1, num4, l] > 0.5) && (num4 == LevelGround.data.alphamapHeight - 1 || (double)LevelGround.tempMap[num3, num4 + 1, l] > 0.5));
										}
										if (flag)
										{
											RaycastHit raycastHit;
											Physics.Raycast(new Vector3(num + (float)k, 256f, num2 + (float)j), Vector3.down, out raycastHit, 256f);
											if (raycastHit.transform != null && (raycastHit.transform == LevelGround.models || raycastHit.transform == LevelGround.models2) && resourceAsset != null)
											{
												bool flag2 = true;
												byte b2 = 0;
												while ((int)b2 < LevelPlayers.spawns.Count)
												{
													if ((raycastHit.point - LevelPlayers.spawns[(int)b2].point).sqrMagnitude < resourceAsset.radius * resourceAsset.radius)
													{
														flag2 = false;
														break;
													}
													b2 += 1;
												}
												if (flag2)
												{
													int num5 = Physics.OverlapSphereNonAlloc(raycastHit.point, resourceAsset.radius, LevelGround.obstructionColliders, RayMasks.BLOCK_RESOURCE);
													bool flag3 = false;
													for (int m = 0; m < num5; m++)
													{
														byte b3;
														byte b4;
														if (Regions.tryGetCoordinate(LevelGround.obstructionColliders[m].transform.position, out b3, out b4))
														{
															bool flag4 = true;
															for (int n = 0; n < LevelObjects.objects[(int)b3, (int)b4].Count; n++)
															{
																LevelObject levelObject = LevelObjects.objects[(int)b3, (int)b4][n];
																if (levelObject.transform == LevelGround.obstructionColliders[m].transform)
																{
																	ObjectAsset objectAsset = (ObjectAsset)Assets.find(EAssetType.OBJECT, levelObject.id);
																	if (resourceAsset == null || objectAsset.isSnowshoe)
																	{
																		flag4 = false;
																	}
																	break;
																}
															}
															if (flag4)
															{
																flag3 = true;
																break;
															}
														}
													}
													if (!flag3)
													{
														LevelGround.trees[(int)LevelGround.bakeResources_X, (int)LevelGround.bakeResources_Y].Add(new ResourceSpawnpoint(b, LevelGround.resources[(int)b].id, raycastHit.point, true));
														LevelGround._total++;
													}
												}
											}
										}
									}
									break;
								}
							}
						}
					}
				}
			}
			LevelGround.onRegionUpdated(byte.MaxValue, byte.MaxValue, Editor.editor.area.region_x, Editor.editor.area.region_y);
			return false;
		}

		// Token: 0x060024BC RID: 9404 RVA: 0x000CFAC7 File Offset: 0x000CDEC7
		public static void updateVisibility(bool isVisible)
		{
			if (isVisible)
			{
				LevelGround.terrain.editorRenderFlags = (TerrainRenderFlags)5;
			}
			else
			{
				LevelGround.terrain.editorRenderFlags = TerrainRenderFlags.heightmap;
			}
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x000CFAEC File Offset: 0x000CDEEC
		protected static void loadSplatPrototypes()
		{
			if (ReadWrite.fileExists(Level.info.path + "/Terrain/Materials.unity3d", false, false))
			{
				Bundle bundle = Bundles.getBundle(Level.info.path + "/Terrain/Materials.unity3d", false);
				UnityEngine.Object[] array = bundle.load();
				LevelGround._materials = new GroundMaterial[(int)(LevelGround.ALPHAMAPS * 4)];
				LevelGround.splatPrototypes = new SplatPrototype[LevelGround.materials.Length];
				int num = 0;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].name.IndexOf("_Mask") == -1)
					{
						LevelGround.splatPrototypes[num] = new SplatPrototype();
						LevelGround.splatPrototypes[num].texture = (Texture2D)array[i];
						LevelGround.splatPrototypes[num].normalMap = (Texture2D)bundle.load(array[i].name + "_Mask");
						LevelGround.splatPrototypes[num].tileSize = new Vector2((float)LevelGround.splatPrototypes[num].texture.width / 4f, (float)LevelGround.splatPrototypes[num].texture.height / 4f);
						LevelGround.materials[num] = new GroundMaterial(LevelGround.splatPrototypes[num]);
						num++;
					}
				}
				bundle.unload();
				if (LevelGround.data != null)
				{
					LevelGround.data.splatPrototypes = LevelGround.splatPrototypes;
				}
			}
			else
			{
				LevelGround._materials = new GroundMaterial[0];
			}
		}

		// Token: 0x060024BE RID: 9406 RVA: 0x000CFC68 File Offset: 0x000CE068
		protected static void loadTrees()
		{
			LevelGround._trees = new List<ResourceSpawnpoint>[(int)Regions.WORLD_SIZE, (int)Regions.WORLD_SIZE];
			LevelGround._total = 0;
			LevelGround._regions = new bool[(int)Regions.WORLD_SIZE, (int)Regions.WORLD_SIZE];
			LevelGround.loads = new int[(int)Regions.WORLD_SIZE, (int)Regions.WORLD_SIZE];
			LevelGround.shouldInstantlyLoad = true;
			for (byte b = 0; b < Regions.WORLD_SIZE; b += 1)
			{
				for (byte b2 = 0; b2 < Regions.WORLD_SIZE; b2 += 1)
				{
					LevelGround.loads[(int)b, (int)b2] = -1;
				}
			}
			Asset[] array = Assets.find(EAssetType.RESOURCE);
			if (ReadWrite.fileExists(Level.info.path + "/Terrain/Resources.dat", false, false))
			{
				Block block = ReadWrite.readBlock(Level.info.path + "/Terrain/Resources.dat", false, false, 0);
				byte b3 = block.readByte();
				if (b3 > 3)
				{
					LevelGround._resources = new GroundResource[array.Length];
					for (int i = 0; i < LevelGround.resources.Length; i++)
					{
						LevelGround.resources[i] = new GroundResource(array[i].id);
					}
				}
				else
				{
					LevelGround._resources = new GroundResource[array.Length];
					for (int j = 0; j < 18; j++)
					{
						LevelGround.resources[j] = new GroundResource((ushort)(j + 1));
					}
					for (int k = 18; k < LevelGround.resources.Length; k++)
					{
						LevelGround.resources[k] = new GroundResource(array[k].id);
					}
				}
				if (b3 > 1)
				{
					byte b4 = block.readByte();
					for (byte b5 = 0; b5 < b4; b5 += 1)
					{
						ushort num;
						if (b3 >= 6)
						{
							num = block.readUInt16();
						}
						else
						{
							num = LevelGround.resources[(int)b5].id;
						}
						float density = block.readSingle();
						float chance = block.readSingle();
						bool isTree_ = block.readBoolean();
						bool isTree_2 = block.readBoolean();
						bool isFlower_ = block.readBoolean();
						bool isFlower_2 = false;
						if (b3 > 7)
						{
							isFlower_2 = block.readBoolean();
						}
						bool isRock = block.readBoolean();
						bool isRoad = false;
						if (b3 > 6)
						{
							isRoad = block.readBoolean();
						}
						bool isSnow = block.readBoolean();
						foreach (GroundResource groundResource in LevelGround.resources)
						{
							if (groundResource.id == num)
							{
								groundResource.density = density;
								groundResource.chance = chance;
								groundResource.isTree_0 = isTree_;
								groundResource.isTree_1 = isTree_2;
								groundResource.isFlower_0 = isFlower_;
								groundResource.isFlower_1 = isFlower_2;
								groundResource.isRock = isRock;
								groundResource.isRoad = isRoad;
								groundResource.isSnow = isSnow;
							}
						}
					}
				}
			}
			else
			{
				LevelGround._resources = new GroundResource[array.Length];
				for (int m = 0; m < LevelGround.resources.Length; m++)
				{
					LevelGround.resources[m] = new GroundResource(array[m].id);
				}
			}
			for (byte b6 = 0; b6 < Regions.WORLD_SIZE; b6 += 1)
			{
				for (byte b7 = 0; b7 < Regions.WORLD_SIZE; b7 += 1)
				{
					LevelGround.trees[(int)b6, (int)b7] = new List<ResourceSpawnpoint>();
				}
			}
			if (ReadWrite.fileExists(Level.info.path + "/Terrain/Trees.dat", false, false))
			{
				River river = new River(Level.info.path + "/Terrain/Trees.dat", false);
				byte b8 = river.readByte();
				bool flag = true;
				if (b8 > 3)
				{
					for (byte b9 = 0; b9 < Regions.WORLD_SIZE; b9 += 1)
					{
						for (byte b10 = 0; b10 < Regions.WORLD_SIZE; b10 += 1)
						{
							ushort num2 = river.readUInt16();
							for (ushort num3 = 0; num3 < num2; num3 += 1)
							{
								if (b8 > 4)
								{
									ushort num4 = river.readUInt16();
									Vector3 vector = river.readSingleVector3();
									bool newGenerated = river.readBoolean();
									if (!Dedicator.isDedicated || Level.checkLevel(vector))
									{
										if (num4 != 0)
										{
											byte b11 = 0;
											while ((int)b11 < LevelGround.resources.Length)
											{
												if (LevelGround.resources[(int)b11].id == num4)
												{
													break;
												}
												b11 += 1;
											}
											ResourceSpawnpoint resourceSpawnpoint = new ResourceSpawnpoint(b11, num4, vector, newGenerated);
											if (resourceSpawnpoint.asset == null)
											{
												flag = false;
											}
											LevelGround.trees[(int)b9, (int)b10].Add(resourceSpawnpoint);
											LevelGround._total++;
										}
									}
								}
								else
								{
									byte b12 = river.readByte();
									Vector3 vector2 = river.readSingleVector3();
									bool newGenerated2 = river.readBoolean();
									if (!Dedicator.isDedicated || Level.checkLevel(vector2))
									{
										if ((int)b12 < LevelGround.resources.Length)
										{
											ushort id = LevelGround.resources[(int)b12].id;
											ResourceSpawnpoint resourceSpawnpoint2 = new ResourceSpawnpoint(b12, id, vector2, newGenerated2);
											if (resourceSpawnpoint2.asset == null)
											{
												flag = false;
											}
											LevelGround.trees[(int)b9, (int)b10].Add(resourceSpawnpoint2);
											LevelGround._total++;
										}
									}
								}
							}
						}
					}
				}
				if (flag)
				{
					LevelGround.hashTrees = river.getHash();
				}
				else
				{
					LevelGround.hashTrees = new byte[20];
				}
				river.closeRiver();
			}
			else
			{
				LevelGround.hashTrees = new byte[20];
			}
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x000D01F4 File Offset: 0x000CE5F4
		public static void load(ushort size)
		{
			if (Level.isEditor)
			{
				LevelGround.MASK = (Texture2D)Resources.Load("Edit/Mask");
			}
			LevelGround.isBakingResources = false;
			LevelGround.isBakingSkyboxResources = false;
			LevelGround.bakeResources_X = 0;
			LevelGround.bakeResources_Y = 0;
			LevelGround.bakeResources_W = 0;
			LevelGround.bakeResources_H = 0;
			LevelGround.bakeResources_M = 0;
			LevelGround._models = new GameObject().transform;
			LevelGround.models.name = "Ground";
			LevelGround.models.parent = Level.level;
			LevelGround.models.tag = "Ground";
			LevelGround.models.gameObject.layer = LayerMasks.GROUND;
			if (!Level.info.configData.Use_Legacy_Ground)
			{
				LevelGround.loadTrees();
				LevelGround._hash = new byte[20];
				return;
			}
			LevelGround._terrain = LevelGround.models.gameObject.AddComponent<Terrain>();
			LevelGround.terrain.name = "Ground";
			LevelGround.terrain.heightmapPixelError = 200f;
			LevelGround.terrain.materialType = Terrain.MaterialType.Custom;
			LevelGround.terrain.transform.position = new Vector3((float)(-size / 2), 0f, (float)(-size / 2));
			LevelGround.terrain.reflectionProbeUsage = ReflectionProbeUsage.Simple;
			LevelGround.terrain.castShadows = false;
			LevelGround.terrain.drawHeightmap = !Dedicator.isDedicated;
			LevelGround.terrain.drawTreesAndFoliage = !Dedicator.isDedicated;
			LevelGround.terrain.treeDistance = 0f;
			LevelGround.terrain.treeBillboardDistance = 0f;
			LevelGround.terrain.treeCrossFadeLength = 0f;
			LevelGround.terrain.treeMaximumFullLODCount = 0;
			LevelGround._previewHQ = true;
			LevelGround._data = new TerrainData();
			LevelGround.data.name = "Ground";
			LevelGround.data.heightmapResolution = (int)(size / 8);
			LevelGround.data.alphamapResolution = (int)(size / 4);
			LevelGround.data.SetDetailResolution((int)(size / 4), 16);
			LevelGround.data.size = new Vector3((float)size, Level.TERRAIN, (float)size);
			LevelGround.data.wavingGrassTint = Color.white;
			byte b = 0;
			byte b2 = 0;
			if (ReadWrite.fileExists(Level.info.path + "/Terrain/Heights.dat", false, false))
			{
				Block block = ReadWrite.readBlock(Level.info.path + "/Terrain/Heights.dat", false, false, 0);
				b = block.readByte();
				b2 = block.readByte();
			}
			if (ReadWrite.fileExists(Level.info.path + "/Terrain/Heightmap.png", false, false))
			{
				byte[] array = ReadWrite.readBytes(Level.info.path + "/Terrain/Heightmap.png", false, false);
				Texture2D texture2D = new Texture2D(LevelGround.data.heightmapWidth, LevelGround.data.heightmapHeight, TextureFormat.ARGB32, false);
				texture2D.name = "Heightmap_Load";
				texture2D.hideFlags = HideFlags.HideAndDontSave;
				texture2D.LoadImage(array);
				float[,] array2 = new float[texture2D.width, texture2D.height];
				for (int i = 0; i < texture2D.width; i++)
				{
					for (int j = 0; j < texture2D.height; j++)
					{
						if (b > 0)
						{
							byte[] value = new byte[]
							{
								(byte)(texture2D.GetPixel(i, j).r * 255f),
								(byte)(texture2D.GetPixel(i, j).g * 255f),
								(byte)(texture2D.GetPixel(i, j).b * 255f),
								(byte)(texture2D.GetPixel(i, j).a * 255f)
							};
							array2[i, j] = BitConverter.ToSingle(value, 0);
						}
						else
						{
							array2[i, j] = texture2D.GetPixel(i, j).r;
						}
					}
				}
				LevelGround.data.SetHeights(0, 0, array2);
				LevelGround.hashHeights = Hash.SHA1(array);
				UnityEngine.Object.DestroyImmediate(texture2D);
			}
			else
			{
				float[,] array3 = new float[LevelGround.data.heightmapWidth, LevelGround.data.heightmapHeight];
				for (int k = 0; k < LevelGround.data.heightmapWidth; k++)
				{
					for (int l = 0; l < LevelGround.data.heightmapHeight; l++)
					{
						array3[k, l] = 0.03f;
					}
				}
				LevelGround.data.SetHeights(0, 0, array3);
				LevelGround.hashHeights = new byte[20];
			}
			LevelGround.loadSplatPrototypes();
			if (ReadWrite.fileExists(Level.info.path + "/Terrain/Materials.dat", false, false))
			{
				Block block2 = ReadWrite.readBlock(Level.info.path + "/Terrain/Materials.dat", false, false, 0);
				byte b3 = block2.readByte();
				if (b3 > 1)
				{
					byte b4 = block2.readByte();
					for (byte b5 = 0; b5 < b4; b5 += 1)
					{
						if ((int)b5 >= LevelGround.materials.Length)
						{
							break;
						}
						LevelGround.materials[(int)b5].overgrowth = block2.readSingle();
						LevelGround.materials[(int)b5].chance = block2.readSingle();
						LevelGround.materials[(int)b5].steepness = block2.readSingle();
						if (b3 > 3)
						{
							LevelGround.materials[(int)b5].height = block2.readSingle();
						}
						else
						{
							LevelGround.materials[(int)b5].height = block2.readSingle() / 2f;
							if (LevelGround.materials[(int)b5].height == 0.5f)
							{
								LevelGround.materials[(int)b5].height = 1f;
							}
						}
						if (b3 > 6)
						{
							LevelGround.materials[(int)b5].transition = block2.readSingle();
						}
						else
						{
							LevelGround.materials[(int)b5].transition = 0f;
						}
						LevelGround.materials[(int)b5].isGrassy_0 = block2.readBoolean();
						LevelGround.materials[(int)b5].isGrassy_1 = block2.readBoolean();
						LevelGround.materials[(int)b5].isFlowery_0 = block2.readBoolean();
						if (b3 > 7)
						{
							LevelGround.materials[(int)b5].isFlowery_1 = block2.readBoolean();
						}
						LevelGround.materials[(int)b5].isRocky = block2.readBoolean();
						if (b3 > 4)
						{
							LevelGround.materials[(int)b5].isRoad = block2.readBoolean();
						}
						if (b3 > 2)
						{
							LevelGround.materials[(int)b5].isSnowy = block2.readBoolean();
						}
						LevelGround.materials[(int)b5].isFoundation = block2.readBoolean();
						if (b3 > 5)
						{
							LevelGround.materials[(int)b5].isManual = block2.readBoolean();
							LevelGround.materials[(int)b5].ignoreSteepness = block2.readBoolean();
							LevelGround.materials[(int)b5].ignoreHeight = block2.readBoolean();
							LevelGround.materials[(int)b5].ignoreFootprint = block2.readBoolean();
						}
						else
						{
							LevelGround.materials[(int)b5].isManual = !block2.readBoolean();
							LevelGround.materials[(int)b5].ignoreSteepness = LevelGround.materials[(int)b5].isManual;
							LevelGround.materials[(int)b5].ignoreHeight = LevelGround.materials[(int)b5].isManual;
							LevelGround.materials[(int)b5].ignoreFootprint = LevelGround.materials[(int)b5].isManual;
						}
					}
				}
			}
			LevelGround.alphamap = new float[LevelGround.data.alphamapWidth, LevelGround.data.alphamapHeight, LevelGround.materials.Length];
			LevelGround.alphamapHQ = new float[LevelGround.data.alphamapWidth, LevelGround.data.alphamapHeight, LevelGround.materials.Length];
			for (int m = 0; m < (int)LevelGround.ALPHAMAPS; m++)
			{
				bool flag = false;
				if (ReadWrite.fileExists(string.Concat(new object[]
				{
					Level.info.path,
					"/Terrain/AlphamapHQ_",
					m,
					".png"
				}), false, false))
				{
					byte[] data = ReadWrite.readBytes(string.Concat(new object[]
					{
						Level.info.path,
						"/Terrain/AlphamapHQ_",
						m,
						".png"
					}), false, false);
					Texture2D texture2D2 = new Texture2D(LevelGround.data.heightmapWidth, LevelGround.data.heightmapHeight, TextureFormat.ARGB32, false);
					texture2D2.name = "AlphamapHQ_Load";
					texture2D2.hideFlags = HideFlags.HideAndDontSave;
					texture2D2.LoadImage(data);
					for (int n = 0; n < texture2D2.width; n++)
					{
						for (int num = 0; num < texture2D2.height; num++)
						{
							Color pixel = texture2D2.GetPixel(n, num);
							LevelGround.alphamapHQ[n, num, m * 4] = pixel.r;
							LevelGround.alphamapHQ[n, num, m * 4 + 1] = pixel.g;
							LevelGround.alphamapHQ[n, num, m * 4 + 2] = pixel.b;
							LevelGround.alphamapHQ[n, num, m * 4 + 3] = pixel.a;
						}
					}
					UnityEngine.Object.DestroyImmediate(texture2D2);
					flag = true;
				}
				if (ReadWrite.fileExists(string.Concat(new object[]
				{
					Level.info.path,
					"/Terrain/Alphamap_",
					m,
					".png"
				}), false, false))
				{
					byte[] data2 = ReadWrite.readBytes(string.Concat(new object[]
					{
						Level.info.path,
						"/Terrain/Alphamap_",
						m,
						".png"
					}), false, false);
					Texture2D texture2D3 = new Texture2D(LevelGround.data.heightmapWidth, LevelGround.data.heightmapHeight, TextureFormat.ARGB32, false);
					texture2D3.name = "Alphamap_Load";
					texture2D3.hideFlags = HideFlags.HideAndDontSave;
					texture2D3.LoadImage(data2);
					for (int num2 = 0; num2 < texture2D3.width; num2++)
					{
						for (int num3 = 0; num3 < texture2D3.height; num3++)
						{
							Color pixel2 = texture2D3.GetPixel(num2, num3);
							LevelGround.alphamap[num2, num3, m * 4] = pixel2.r;
							LevelGround.alphamap[num2, num3, m * 4 + 1] = pixel2.g;
							LevelGround.alphamap[num2, num3, m * 4 + 2] = pixel2.b;
							LevelGround.alphamap[num2, num3, m * 4 + 3] = pixel2.a;
							if (!flag)
							{
								LevelGround.alphamapHQ[num2, num3, m * 4] = pixel2.r;
								LevelGround.alphamapHQ[num2, num3, m * 4 + 1] = pixel2.g;
								LevelGround.alphamapHQ[num2, num3, m * 4 + 2] = pixel2.b;
								LevelGround.alphamapHQ[num2, num3, m * 4 + 3] = pixel2.a;
							}
						}
					}
					UnityEngine.Object.DestroyImmediate(texture2D3);
				}
			}
			LevelGround.data.baseMapResolution = (int)(size / 8);
			LevelGround.data.baseMapResolution = (int)(size / 4);
			LevelGround.data.SetAlphamaps(0, 0, LevelGround.alphamapHQ);
			if (!Dedicator.isDedicated)
			{
				if (ReadWrite.fileExists(Level.info.path + "/Terrain/Details.unity3d", false, false))
				{
					Bundle bundle = Bundles.getBundle(Level.info.path + "/Terrain/Details.unity3d", false);
					UnityEngine.Object[] array4 = bundle.load(typeof(Texture2D));
					UnityEngine.Object[] array5 = bundle.load(typeof(GameObject));
					bundle.unload();
					List<GroundDetail> list = new List<GroundDetail>();
					List<DetailPrototype> list2 = new List<DetailPrototype>();
					for (int num4 = 0; num4 < array4.Length; num4++)
					{
						if (array4[num4].name.IndexOf("Texture_") == -1)
						{
							DetailPrototype detailPrototype = new DetailPrototype();
							detailPrototype.prototypeTexture = (Texture2D)array4[num4];
							detailPrototype.renderMode = DetailRenderMode.Grass;
							detailPrototype.dryColor = Color.white;
							detailPrototype.healthyColor = Color.white;
							float num5 = (float)detailPrototype.prototypeTexture.width / 20f;
							float num6 = (float)detailPrototype.prototypeTexture.height / 20f;
							detailPrototype.minWidth = num5 * 1.25f;
							detailPrototype.maxWidth = num5 * 1.75f;
							detailPrototype.minHeight = num6 * 1.25f;
							detailPrototype.maxHeight = num6 * 1.75f;
							detailPrototype.noiseSpread = 1f;
							list2.Add(detailPrototype);
							list.Add(new GroundDetail(detailPrototype));
						}
					}
					for (int num7 = 0; num7 < array5.Length; num7++)
					{
						if (array5[num7].name.IndexOf("Model_") == -1)
						{
							DetailPrototype detailPrototype2 = new DetailPrototype();
							detailPrototype2.prototype = (GameObject)array5[num7];
							detailPrototype2.renderMode = DetailRenderMode.VertexLit;
							detailPrototype2.usePrototypeMesh = true;
							detailPrototype2.dryColor = new Color(0.95f, 0.95f, 0.95f);
							detailPrototype2.healthyColor = Color.white;
							detailPrototype2.maxWidth = 1.5f;
							detailPrototype2.maxHeight = 1.5f;
							detailPrototype2.noiseSpread = 1f;
							list2.Add(detailPrototype2);
							list.Add(new GroundDetail(detailPrototype2));
						}
					}
					LevelGround.data.detailPrototypes = list2.ToArray();
					LevelGround._details = list.ToArray();
				}
				else
				{
					LevelGround._details = new GroundDetail[0];
				}
				if (ReadWrite.fileExists(Level.info.path + "/Terrain/Details.dat", false, false))
				{
					Block block3 = ReadWrite.readBlock(Level.info.path + "/Terrain/Details.dat", false, false, 0);
					byte b6 = block3.readByte();
					byte b7 = block3.readByte();
					for (byte b8 = 0; b8 < b7; b8 += 1)
					{
						if ((int)b8 >= LevelGround.details.Length)
						{
							break;
						}
						LevelGround.details[(int)b8].density = block3.readSingle();
						LevelGround.details[(int)b8].chance = block3.readSingle();
						LevelGround.details[(int)b8].isGrass_0 = block3.readBoolean();
						LevelGround.details[(int)b8].isGrass_1 = block3.readBoolean();
						LevelGround.details[(int)b8].isFlower_0 = block3.readBoolean();
						if (b6 > 4)
						{
							LevelGround.details[(int)b8].isFlower_1 = block3.readBoolean();
						}
						LevelGround.details[(int)b8].isRock = block3.readBoolean();
						if (b6 > 3)
						{
							LevelGround.details[(int)b8].isRoad = block3.readBoolean();
						}
						if (b6 > 2)
						{
							LevelGround.details[(int)b8].isSnow = block3.readBoolean();
						}
					}
				}
			}
			LevelGround.terrain.terrainData = LevelGround.data;
			LevelGround.terrain.detailObjectDensity = 0f;
			LevelGround.terrain.detailObjectDistance = 0f;
			LevelGround.terrain.terrainData.wavingGrassAmount = 0f;
			LevelGround.terrain.terrainData.wavingGrassSpeed = 1f;
			LevelGround.terrain.terrainData.wavingGrassStrength = 1f;
			TerrainCollider terrainCollider = LevelGround.models.gameObject.AddComponent<TerrainCollider>();
			terrainCollider.terrainData = LevelGround.data;
			LevelGround.loadTrees();
			LevelGround._hash = Hash.combine(new byte[][]
			{
				LevelGround.hashHeights,
				LevelGround.hashTrees
			});
			if (!Dedicator.isDedicated)
			{
				LevelGround._models2 = new GameObject().transform;
				LevelGround.models2.name = "Ground2";
				LevelGround.models2.parent = Level.level;
				LevelGround.models2.tag = "Ground2";
				LevelGround.models2.gameObject.layer = LayerMasks.GROUND2;
				LevelGround._terrain2 = LevelGround.models2.gameObject.AddComponent<Terrain>();
				LevelGround.terrain2.name = "Ground2";
				LevelGround.terrain2.heightmapPixelError = 200f;
				LevelGround.terrain2.materialType = Terrain.MaterialType.Custom;
				LevelGround.terrain2.transform.position = new Vector3((float)(-(float)size), 0f, (float)(-(float)size));
				LevelGround.terrain2.reflectionProbeUsage = ReflectionProbeUsage.Simple;
				LevelGround.terrain2.castShadows = false;
				LevelGround.terrain2.treeDistance = 0f;
				LevelGround.terrain2.treeBillboardDistance = 0f;
				LevelGround.terrain2.treeCrossFadeLength = 0f;
				LevelGround.terrain2.treeMaximumFullLODCount = 0;
				LevelGround._data2 = new TerrainData();
				LevelGround.data2.name = "Ground2";
				LevelGround.data2.heightmapResolution = (int)(size / 16);
				LevelGround.data2.alphamapResolution = (int)(size / 8);
				LevelGround.data2.SetDetailResolution((int)(size / 8), 16);
				LevelGround.data2.size = new Vector3((float)(size * 2), Level.TERRAIN, (float)(size * 2));
				LevelGround.data2.splatPrototypes = LevelGround.data.splatPrototypes;
				if (ReadWrite.fileExists(Level.info.path + "/Terrain/Heightmap2.png", false, false))
				{
					byte[] data3 = ReadWrite.readBytes(Level.info.path + "/Terrain/Heightmap2.png", false, false);
					Texture2D texture2D4 = new Texture2D(LevelGround.data2.heightmapWidth, LevelGround.data2.heightmapHeight, TextureFormat.ARGB32, false);
					texture2D4.name = "Heightmap2_Load";
					texture2D4.hideFlags = HideFlags.HideAndDontSave;
					texture2D4.LoadImage(data3);
					float[,] array6 = new float[texture2D4.width, texture2D4.height];
					for (int num8 = 0; num8 < texture2D4.width; num8++)
					{
						for (int num9 = 0; num9 < texture2D4.height; num9++)
						{
							if (b2 > 0)
							{
								byte[] value2 = new byte[]
								{
									(byte)(texture2D4.GetPixel(num8, num9).r * 255f),
									(byte)(texture2D4.GetPixel(num8, num9).g * 255f),
									(byte)(texture2D4.GetPixel(num8, num9).b * 255f),
									(byte)(texture2D4.GetPixel(num8, num9).a * 255f)
								};
								array6[num8, num9] = BitConverter.ToSingle(value2, 0);
							}
							else
							{
								array6[num8, num9] = texture2D4.GetPixel(num8, num9).r;
							}
						}
					}
					LevelGround.data2.SetHeights(0, 0, array6);
					UnityEngine.Object.DestroyImmediate(texture2D4);
				}
				else
				{
					float[,] array7 = new float[LevelGround.data2.heightmapWidth, LevelGround.data2.heightmapHeight];
					for (int num10 = 0; num10 < LevelGround.data2.heightmapWidth; num10++)
					{
						for (int num11 = 0; num11 < LevelGround.data2.heightmapHeight; num11++)
						{
							array7[num10, num11] = 0f;
						}
					}
					LevelGround.data2.SetHeights(0, 0, array7);
				}
				LevelGround.alphamap2 = new float[LevelGround.data2.alphamapWidth, LevelGround.data2.alphamapHeight, LevelGround.materials.Length];
				LevelGround.alphamap2HQ = new float[LevelGround.data2.alphamapWidth, LevelGround.data2.alphamapHeight, LevelGround.materials.Length];
				for (int num12 = 0; num12 < (int)LevelGround.ALPHAMAPS; num12++)
				{
					bool flag2 = false;
					if (ReadWrite.fileExists(string.Concat(new object[]
					{
						Level.info.path,
						"/Terrain/Alphamap2HQ_",
						num12,
						".png"
					}), false, false))
					{
						byte[] data4 = ReadWrite.readBytes(string.Concat(new object[]
						{
							Level.info.path,
							"/Terrain/Alphamap2HQ_",
							num12,
							".png"
						}), false, false);
						Texture2D texture2D5 = new Texture2D(LevelGround.data2.heightmapWidth, LevelGround.data2.heightmapHeight, TextureFormat.ARGB32, false);
						texture2D5.name = "Alphamap2HQ_Load";
						texture2D5.hideFlags = HideFlags.HideAndDontSave;
						texture2D5.LoadImage(data4);
						for (int num13 = 0; num13 < texture2D5.width; num13++)
						{
							for (int num14 = 0; num14 < texture2D5.height; num14++)
							{
								Color pixel3 = texture2D5.GetPixel(num13, num14);
								LevelGround.alphamap2HQ[num13, num14, num12 * 4] = pixel3.r;
								LevelGround.alphamap2HQ[num13, num14, num12 * 4 + 1] = pixel3.g;
								LevelGround.alphamap2HQ[num13, num14, num12 * 4 + 2] = pixel3.b;
								LevelGround.alphamap2HQ[num13, num14, num12 * 4 + 3] = pixel3.a;
							}
						}
						UnityEngine.Object.DestroyImmediate(texture2D5);
						flag2 = true;
					}
					if (ReadWrite.fileExists(string.Concat(new object[]
					{
						Level.info.path,
						"/Terrain/Alphamap2_",
						num12,
						".png"
					}), false, false))
					{
						byte[] data5 = ReadWrite.readBytes(string.Concat(new object[]
						{
							Level.info.path,
							"/Terrain/Alphamap2_",
							num12,
							".png"
						}), false, false);
						Texture2D texture2D6 = new Texture2D(LevelGround.data2.heightmapWidth, LevelGround.data2.heightmapHeight, TextureFormat.ARGB32, false);
						texture2D6.name = "Alphamap2_Load";
						texture2D6.hideFlags = HideFlags.HideAndDontSave;
						texture2D6.LoadImage(data5);
						for (int num15 = 0; num15 < texture2D6.width; num15++)
						{
							for (int num16 = 0; num16 < texture2D6.height; num16++)
							{
								Color pixel4 = texture2D6.GetPixel(num15, num16);
								LevelGround.alphamap2[num15, num16, num12 * 4] = pixel4.r;
								LevelGround.alphamap2[num15, num16, num12 * 4 + 1] = pixel4.g;
								LevelGround.alphamap2[num15, num16, num12 * 4 + 2] = pixel4.b;
								LevelGround.alphamap2[num15, num16, num12 * 4 + 3] = pixel4.a;
								if (!flag2)
								{
									LevelGround.alphamap2HQ[num15, num16, num12 * 4] = pixel4.r;
									LevelGround.alphamap2HQ[num15, num16, num12 * 4 + 1] = pixel4.g;
									LevelGround.alphamap2HQ[num15, num16, num12 * 4 + 2] = pixel4.b;
									LevelGround.alphamap2HQ[num15, num16, num12 * 4 + 3] = pixel4.a;
								}
							}
						}
						UnityEngine.Object.DestroyImmediate(texture2D6);
					}
				}
				LevelGround.data2.baseMapResolution = (int)(size / 8);
				LevelGround.data2.baseMapResolution = (int)(size / 4);
				LevelGround.data2.SetAlphamaps(0, 0, LevelGround.alphamap2HQ);
				LevelGround.terrain2.terrainData = LevelGround.data2;
				TerrainCollider terrainCollider2 = LevelGround.models2.gameObject.AddComponent<TerrainCollider>();
				terrainCollider2.terrainData = LevelGround.data2;
				LevelGround.data2.wavingGrassTint = Color.white;
			}
			if (Level.isEditor)
			{
				LevelGround.reunHeight = new float[16][,];
				LevelGround.reunHeight2 = new float[16][,];
				LevelGround.frameHeight = 0;
				LevelGround.frameHeight2 = 0;
				LevelGround.reunMaterial = new float[16][,,];
				LevelGround.reunMaterial2 = new float[16][,,];
				LevelGround.frameMaterial = 0;
				LevelGround.frameMaterial2 = 0;
				LevelGround.registerHeight();
				LevelGround.registerHeight2();
				LevelGround.registerMaterial();
				LevelGround.registerMaterial2();
			}
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x000D1998 File Offset: 0x000CFD98
		protected static void saveTrees()
		{
			Block block = new Block();
			block.writeByte(LevelGround.SAVEDATA_RESOURCES_VERSION);
			block.writeByte((byte)LevelGround.resources.Length);
			byte b = 0;
			while ((int)b < LevelGround.resources.Length)
			{
				block.writeUInt16(LevelGround.resources[(int)b].id);
				block.writeSingle(LevelGround.resources[(int)b].density);
				block.writeSingle(LevelGround.resources[(int)b].chance);
				block.writeBoolean(LevelGround.resources[(int)b].isTree_0);
				block.writeBoolean(LevelGround.resources[(int)b].isTree_1);
				block.writeBoolean(LevelGround.resources[(int)b].isFlower_0);
				block.writeBoolean(LevelGround.resources[(int)b].isFlower_1);
				block.writeBoolean(LevelGround.resources[(int)b].isRock);
				block.writeBoolean(LevelGround.resources[(int)b].isRoad);
				block.writeBoolean(LevelGround.resources[(int)b].isSnow);
				b += 1;
			}
			ReadWrite.writeBlock(Level.info.path + "/Terrain/Resources.dat", false, false, block);
			River river = new River(Level.info.path + "/Terrain/Trees.dat", false);
			river.writeByte(LevelGround.SAVEDATA_TREES_VERSION);
			for (byte b2 = 0; b2 < Regions.WORLD_SIZE; b2 += 1)
			{
				for (byte b3 = 0; b3 < Regions.WORLD_SIZE; b3 += 1)
				{
					List<ResourceSpawnpoint> list = LevelGround.trees[(int)b2, (int)b3];
					river.writeUInt16((ushort)list.Count);
					ushort num = 0;
					while ((int)num < list.Count)
					{
						ResourceSpawnpoint resourceSpawnpoint = list[(int)num];
						if (resourceSpawnpoint != null && resourceSpawnpoint.model != null && resourceSpawnpoint.id != 0)
						{
							river.writeUInt16(resourceSpawnpoint.id);
							river.writeSingleVector3(resourceSpawnpoint.point);
							river.writeBoolean(resourceSpawnpoint.isGenerated);
						}
						else
						{
							river.writeUInt16(0);
							river.writeSingleVector3(Vector3.zero);
							river.writeBoolean(true);
						}
						num += 1;
					}
				}
			}
			river.closeRiver();
		}

		// Token: 0x060024C1 RID: 9409 RVA: 0x000D1BBC File Offset: 0x000CFFBC
		public static void save()
		{
			if (!Level.info.configData.Use_Legacy_Ground)
			{
				LevelGround.saveTrees();
				return;
			}
			float[,] heights = LevelGround.data.GetHeights(0, 0, LevelGround.data.heightmapWidth, LevelGround.data.heightmapHeight);
			Texture2D texture2D = new Texture2D(LevelGround.data.heightmapWidth, LevelGround.data.heightmapHeight, TextureFormat.ARGB32, false);
			texture2D.name = "Heightmap_Save";
			texture2D.hideFlags = HideFlags.HideAndDontSave;
			for (int i = 0; i < texture2D.width; i++)
			{
				for (int j = 0; j < texture2D.height; j++)
				{
					byte[] bytes = BitConverter.GetBytes(heights[i, j]);
					texture2D.SetPixel(i, j, new Color((float)bytes[0] / 255f, (float)bytes[1] / 255f, (float)bytes[2] / 255f, (float)bytes[3] / 255f));
				}
			}
			byte[] bytes2 = texture2D.EncodeToPNG();
			ReadWrite.writeBytes(Level.info.path + "/Terrain/Heightmap.png", false, false, bytes2);
			UnityEngine.Object.DestroyImmediate(texture2D);
			Texture2D texture2D2 = new Texture2D(LevelGround.data.alphamapWidth, LevelGround.data.alphamapHeight, TextureFormat.ARGB32, false);
			texture2D2.name = "Alphamap_Save";
			texture2D2.hideFlags = HideFlags.HideAndDontSave;
			for (int k = 0; k < (int)LevelGround.ALPHAMAPS; k++)
			{
				for (int l = 0; l < texture2D2.width; l++)
				{
					for (int m = 0; m < texture2D2.height; m++)
					{
						texture2D2.SetPixel(l, m, new Color(LevelGround.alphamap[l, m, k * 4], LevelGround.alphamap[l, m, k * 4 + 1], LevelGround.alphamap[l, m, k * 4 + 2], LevelGround.alphamap[l, m, k * 4 + 3]));
					}
				}
				byte[] bytes3 = texture2D2.EncodeToPNG();
				ReadWrite.writeBytes(string.Concat(new object[]
				{
					Level.info.path,
					"/Terrain/Alphamap_",
					k,
					".png"
				}), false, false, bytes3);
			}
			UnityEngine.Object.DestroyImmediate(texture2D2);
			Texture2D texture2D3 = new Texture2D(LevelGround.data.alphamapWidth, LevelGround.data.alphamapHeight, TextureFormat.ARGB32, false);
			texture2D3.name = "AlphamapHQ_Save";
			texture2D3.hideFlags = HideFlags.HideAndDontSave;
			for (int n = 0; n < (int)LevelGround.ALPHAMAPS; n++)
			{
				for (int num = 0; num < texture2D3.width; num++)
				{
					for (int num2 = 0; num2 < texture2D3.height; num2++)
					{
						texture2D3.SetPixel(num, num2, new Color(LevelGround.alphamapHQ[num, num2, n * 4], LevelGround.alphamapHQ[num, num2, n * 4 + 1], LevelGround.alphamapHQ[num, num2, n * 4 + 2], LevelGround.alphamapHQ[num, num2, n * 4 + 3]));
					}
				}
				byte[] bytes4 = texture2D3.EncodeToPNG();
				ReadWrite.writeBytes(string.Concat(new object[]
				{
					Level.info.path,
					"/Terrain/AlphamapHQ_",
					n,
					".png"
				}), false, false, bytes4);
			}
			UnityEngine.Object.DestroyImmediate(texture2D3);
			Block block = new Block();
			block.writeByte(LevelGround.SAVEDATA_HEIGHTS_VERSION);
			block.writeByte(LevelGround.SAVEDATA_HEIGHTS2_VERSION);
			ReadWrite.writeBlock(Level.info.path + "/Terrain/Heights.dat", false, false, block);
			heights = LevelGround.data2.GetHeights(0, 0, LevelGround.data2.heightmapWidth, LevelGround.data2.heightmapHeight);
			texture2D = new Texture2D(LevelGround.data2.heightmapWidth, LevelGround.data2.heightmapHeight, TextureFormat.ARGB32, false);
			texture2D.name = "Heightmap2_Save";
			texture2D.hideFlags = HideFlags.HideAndDontSave;
			for (int num3 = 0; num3 < texture2D.width; num3++)
			{
				for (int num4 = 0; num4 < texture2D.height; num4++)
				{
					byte[] bytes5 = BitConverter.GetBytes(heights[num3, num4]);
					texture2D.SetPixel(num3, num4, new Color((float)bytes5[0] / 255f, (float)bytes5[1] / 255f, (float)bytes5[2] / 255f, (float)bytes5[3] / 255f));
				}
			}
			bytes2 = texture2D.EncodeToPNG();
			ReadWrite.writeBytes(Level.info.path + "/Terrain/Heightmap2.png", false, false, bytes2);
			UnityEngine.Object.DestroyImmediate(texture2D);
			texture2D2 = new Texture2D(LevelGround.data2.alphamapWidth, LevelGround.data2.alphamapHeight, TextureFormat.ARGB32, false);
			texture2D2.name = "Alphamap2_Save";
			texture2D2.hideFlags = HideFlags.HideAndDontSave;
			for (int num5 = 0; num5 < (int)LevelGround.ALPHAMAPS; num5++)
			{
				for (int num6 = 0; num6 < texture2D2.width; num6++)
				{
					for (int num7 = 0; num7 < texture2D2.height; num7++)
					{
						texture2D2.SetPixel(num6, num7, new Color(LevelGround.alphamap2[num6, num7, num5 * 4], LevelGround.alphamap2[num6, num7, num5 * 4 + 1], LevelGround.alphamap2[num6, num7, num5 * 4 + 2], LevelGround.alphamap2[num6, num7, num5 * 4 + 3]));
					}
				}
				byte[] bytes6 = texture2D2.EncodeToPNG();
				ReadWrite.writeBytes(string.Concat(new object[]
				{
					Level.info.path,
					"/Terrain/Alphamap2_",
					num5,
					".png"
				}), false, false, bytes6);
			}
			UnityEngine.Object.DestroyImmediate(texture2D2);
			texture2D3 = new Texture2D(LevelGround.data2.alphamapWidth, LevelGround.data2.alphamapHeight, TextureFormat.ARGB32, false);
			texture2D3.name = "Alphamap2HQ_Save";
			texture2D3.hideFlags = HideFlags.HideAndDontSave;
			for (int num8 = 0; num8 < (int)LevelGround.ALPHAMAPS; num8++)
			{
				for (int num9 = 0; num9 < texture2D3.width; num9++)
				{
					for (int num10 = 0; num10 < texture2D3.height; num10++)
					{
						texture2D3.SetPixel(num9, num10, new Color(LevelGround.alphamap2HQ[num9, num10, num8 * 4], LevelGround.alphamap2HQ[num9, num10, num8 * 4 + 1], LevelGround.alphamap2HQ[num9, num10, num8 * 4 + 2], LevelGround.alphamap2HQ[num9, num10, num8 * 4 + 3]));
					}
				}
				byte[] bytes7 = texture2D3.EncodeToPNG();
				ReadWrite.writeBytes(string.Concat(new object[]
				{
					Level.info.path,
					"/Terrain/Alphamap2HQ_",
					num8,
					".png"
				}), false, false, bytes7);
			}
			UnityEngine.Object.DestroyImmediate(texture2D3);
			Block block2 = new Block();
			block2.writeByte(LevelGround.SAVEDATA_MATERIALS_VERSION);
			block2.writeByte((byte)LevelGround.materials.Length);
			byte b = 0;
			while ((int)b < LevelGround.materials.Length)
			{
				block2.writeSingle(LevelGround.materials[(int)b].overgrowth);
				block2.writeSingle(LevelGround.materials[(int)b].chance);
				block2.writeSingle(LevelGround.materials[(int)b].steepness);
				block2.writeSingle(LevelGround.materials[(int)b].height);
				block2.writeSingle(LevelGround.materials[(int)b].transition);
				block2.writeBoolean(LevelGround.materials[(int)b].isGrassy_0);
				block2.writeBoolean(LevelGround.materials[(int)b].isGrassy_1);
				block2.writeBoolean(LevelGround.materials[(int)b].isFlowery_0);
				block2.writeBoolean(LevelGround.materials[(int)b].isFlowery_1);
				block2.writeBoolean(LevelGround.materials[(int)b].isRocky);
				block2.writeBoolean(LevelGround.materials[(int)b].isRoad);
				block2.writeBoolean(LevelGround.materials[(int)b].isSnowy);
				block2.writeBoolean(LevelGround.materials[(int)b].isFoundation);
				block2.writeBoolean(LevelGround.materials[(int)b].isManual);
				block2.writeBoolean(LevelGround.materials[(int)b].ignoreSteepness);
				block2.writeBoolean(LevelGround.materials[(int)b].ignoreHeight);
				block2.writeBoolean(LevelGround.materials[(int)b].ignoreFootprint);
				b += 1;
			}
			ReadWrite.writeBlock(Level.info.path + "/Terrain/Materials.dat", false, false, block2);
			Block block3 = new Block();
			block3.writeByte(LevelGround.SAVEDATA_DETAILS_VERSION);
			block3.writeByte((byte)LevelGround.details.Length);
			byte b2 = 0;
			while ((int)b2 < LevelGround.details.Length)
			{
				block3.writeSingle(LevelGround.details[(int)b2].density);
				block3.writeSingle(LevelGround.details[(int)b2].chance);
				block3.writeBoolean(LevelGround.details[(int)b2].isGrass_0);
				block3.writeBoolean(LevelGround.details[(int)b2].isGrass_1);
				block3.writeBoolean(LevelGround.details[(int)b2].isFlower_0);
				block3.writeBoolean(LevelGround.details[(int)b2].isFlower_1);
				block3.writeBoolean(LevelGround.details[(int)b2].isRock);
				block3.writeBoolean(LevelGround.details[(int)b2].isRoad);
				block3.writeBoolean(LevelGround.details[(int)b2].isSnow);
				b2 += 1;
			}
			ReadWrite.writeBlock(Level.info.path + "/Terrain/Details.dat", false, false, block3);
			LevelGround.saveTrees();
		}

		// Token: 0x060024C2 RID: 9410 RVA: 0x000D2598 File Offset: 0x000D0998
		private static void onRegionUpdated(byte old_x, byte old_y, byte new_x, byte new_y)
		{
			bool flag = true;
			LevelGround.onRegionUpdated(null, old_x, old_y, new_x, new_y, 0, ref flag);
		}

		// Token: 0x060024C3 RID: 9411 RVA: 0x000D25B4 File Offset: 0x000D09B4
		private static void onPlayerTeleported(Player player, Vector3 position)
		{
			LevelGround.shouldInstantlyLoad = true;
		}

		// Token: 0x060024C4 RID: 9412 RVA: 0x000D25BC File Offset: 0x000D09BC
		private static void onRegionUpdated(Player player, byte old_x, byte old_y, byte new_x, byte new_y, byte step, ref bool canIncrementIndex)
		{
			if (step != 0)
			{
				return;
			}
			for (byte b = 0; b < Regions.WORLD_SIZE; b += 1)
			{
				for (byte b2 = 0; b2 < Regions.WORLD_SIZE; b2 += 1)
				{
					if (LevelGround.regions[(int)b, (int)b2] && !Regions.checkArea(b, b2, new_x, new_y, LevelGround.RESOURCE_REGIONS))
					{
						LevelGround.regions[(int)b, (int)b2] = false;
						if (LevelGround.shouldInstantlyLoad)
						{
							List<ResourceSpawnpoint> list = LevelGround.trees[(int)b, (int)b2];
							for (int i = 0; i < list.Count; i++)
							{
								list[i].disable();
								if (GraphicsSettings.landmarkQuality >= EGraphicQuality.MEDIUM)
								{
									list[i].enableSkybox();
								}
							}
						}
						else
						{
							LevelGround.loads[(int)b, (int)b2] = 0;
						}
					}
				}
			}
			if (Regions.checkSafe((int)new_x, (int)new_y))
			{
				for (int j = (int)(new_x - LevelGround.RESOURCE_REGIONS); j <= (int)(new_x + LevelGround.RESOURCE_REGIONS); j++)
				{
					for (int k = (int)(new_y - LevelGround.RESOURCE_REGIONS); k <= (int)(new_y + LevelGround.RESOURCE_REGIONS); k++)
					{
						if (Regions.checkSafe((int)((byte)j), (int)((byte)k)) && !LevelGround.regions[j, k])
						{
							LevelGround.regions[j, k] = true;
							if (LevelGround.shouldInstantlyLoad)
							{
								List<ResourceSpawnpoint> list2 = LevelGround.trees[j, k];
								for (int l = 0; l < list2.Count; l++)
								{
									list2[l].enable();
									list2[l].disableSkybox();
								}
							}
							else
							{
								LevelGround.loads[j, k] = 0;
							}
						}
					}
				}
			}
			LevelGround.shouldInstantlyLoad = false;
		}

		// Token: 0x060024C5 RID: 9413 RVA: 0x000D2788 File Offset: 0x000D0B88
		private static void onPlayerCreated(Player player)
		{
			if (player.channel.isOwner)
			{
				Player player2 = Player.player;
				Delegate onPlayerTeleported = player2.onPlayerTeleported;
				if (LevelGround.<>f__mg$cache0 == null)
				{
					LevelGround.<>f__mg$cache0 = new PlayerTeleported(LevelGround.onPlayerTeleported);
				}
				player2.onPlayerTeleported = (PlayerTeleported)Delegate.Combine(onPlayerTeleported, LevelGround.<>f__mg$cache0);
				PlayerMovement movement = Player.player.movement;
				Delegate onRegionUpdated = movement.onRegionUpdated;
				if (LevelGround.<>f__mg$cache1 == null)
				{
					LevelGround.<>f__mg$cache1 = new PlayerRegionUpdated(LevelGround.onRegionUpdated);
				}
				movement.onRegionUpdated = (PlayerRegionUpdated)Delegate.Combine(onRegionUpdated, LevelGround.<>f__mg$cache1);
			}
		}

		// Token: 0x060024C6 RID: 9414 RVA: 0x000D2818 File Offset: 0x000D0C18
		private static void handleEditorAreaRegistered(EditorArea area)
		{
			Delegate onRegionUpdated = area.onRegionUpdated;
			if (LevelGround.<>f__mg$cache2 == null)
			{
				LevelGround.<>f__mg$cache2 = new EditorRegionUpdated(LevelGround.onRegionUpdated);
			}
			area.onRegionUpdated = (EditorRegionUpdated)Delegate.Combine(onRegionUpdated, LevelGround.<>f__mg$cache2);
		}

		// Token: 0x060024C7 RID: 9415 RVA: 0x000D2850 File Offset: 0x000D0C50
		private void Update()
		{
			if (!Level.isLoaded)
			{
				return;
			}
			if (Dedicator.isDedicated)
			{
				return;
			}
			if (LevelGround.loads == null || LevelGround.regions == null || LevelGround.trees == null)
			{
				return;
			}
			for (byte b = 0; b < Regions.WORLD_SIZE; b += 1)
			{
				for (byte b2 = 0; b2 < Regions.WORLD_SIZE; b2 += 1)
				{
					if (LevelGround.loads[(int)b, (int)b2] != -1)
					{
						if (LevelGround.loads[(int)b, (int)b2] >= LevelGround.trees[(int)b, (int)b2].Count)
						{
							LevelGround.loads[(int)b, (int)b2] = -1;
						}
						else
						{
							if (LevelGround.regions[(int)b, (int)b2])
							{
								if (!LevelGround.trees[(int)b, (int)b2][LevelGround.loads[(int)b, (int)b2]].isEnabled)
								{
									LevelGround.trees[(int)b, (int)b2][LevelGround.loads[(int)b, (int)b2]].enable();
								}
								if (LevelGround.trees[(int)b, (int)b2][LevelGround.loads[(int)b, (int)b2]].isSkyboxEnabled)
								{
									LevelGround.trees[(int)b, (int)b2][LevelGround.loads[(int)b, (int)b2]].disableSkybox();
								}
							}
							else
							{
								if (LevelGround.trees[(int)b, (int)b2][LevelGround.loads[(int)b, (int)b2]].isEnabled)
								{
									LevelGround.trees[(int)b, (int)b2][LevelGround.loads[(int)b, (int)b2]].disable();
								}
								if (!LevelGround.trees[(int)b, (int)b2][LevelGround.loads[(int)b, (int)b2]].isSkyboxEnabled && GraphicsSettings.landmarkQuality >= EGraphicQuality.MEDIUM)
								{
									LevelGround.trees[(int)b, (int)b2][LevelGround.loads[(int)b, (int)b2]].enableSkybox();
								}
							}
							LevelGround.loads[(int)b, (int)b2]++;
						}
					}
				}
			}
			if (!Level.isEditor)
			{
				return;
			}
			if (LevelGround.isBakingResources)
			{
				bool flag = true;
				while (flag)
				{
					flag = LevelGround.bakeResource();
					LevelGround.bakeResources_X += 1;
					if (LevelGround.bakeResources_X >= LevelGround.bakeResources_W)
					{
						LevelGround.bakeResources_X = LevelGround.bakeResources_M;
						LevelGround.bakeResources_Y += 1;
						if (LevelGround.bakeResources_Y >= LevelGround.bakeResources_H)
						{
							flag = false;
							LevelGround.isBakingResources = false;
							for (byte b3 = 0; b3 < Regions.WORLD_SIZE; b3 += 1)
							{
								for (byte b4 = 0; b4 < Regions.WORLD_SIZE; b4 += 1)
								{
									if (!LevelObjects.regions[(int)b3, (int)b4])
									{
										List<LevelObject> list = LevelObjects.objects[(int)b3, (int)b4];
										for (int i = 0; i < list.Count; i++)
										{
											list[i].disableCollision();
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060024C8 RID: 9416 RVA: 0x000D2B50 File Offset: 0x000D0F50
		protected void handlePlanarReflectionPreRender()
		{
			if (LevelGround.terrain == null)
			{
				return;
			}
			LevelGround.terrain.basemapDistance = 0f;
			LevelGround.terrain2.basemapDistance = 0f;
		}

		// Token: 0x060024C9 RID: 9417 RVA: 0x000D2B84 File Offset: 0x000D0F84
		protected void handlePlanarReflectionPostRender()
		{
			if (LevelGround.terrain == null)
			{
				return;
			}
			LevelGround.terrain.basemapDistance = (float)((!GraphicsSettings.blend) ? 256 : 512);
			LevelGround.terrain2.basemapDistance = LevelGround.terrain.basemapDistance;
		}

		// Token: 0x060024CA RID: 9418 RVA: 0x000D2BDC File Offset: 0x000D0FDC
		private void Start()
		{
			PlanarReflection.preRender += this.handlePlanarReflectionPreRender;
			PlanarReflection.postRender += this.handlePlanarReflectionPostRender;
			Delegate onPlayerCreated = Player.onPlayerCreated;
			if (LevelGround.<>f__mg$cache3 == null)
			{
				LevelGround.<>f__mg$cache3 = new PlayerCreated(LevelGround.onPlayerCreated);
			}
			Player.onPlayerCreated = (PlayerCreated)Delegate.Combine(onPlayerCreated, LevelGround.<>f__mg$cache3);
			if (LevelGround.<>f__mg$cache4 == null)
			{
				LevelGround.<>f__mg$cache4 = new EditorAreaRegisteredHandler(LevelGround.handleEditorAreaRegistered);
			}
			EditorArea.registered += LevelGround.<>f__mg$cache4;
			if (LevelGround._Triplanar_Primary_Size == -1)
			{
				LevelGround._Triplanar_Primary_Size = Shader.PropertyToID("_Triplanar_Primary_Size");
			}
			Shader.SetGlobalFloat(LevelGround._Triplanar_Primary_Size, LevelGround.triplanarPrimarySize);
			if (LevelGround._Triplanar_Primary_Weight == -1)
			{
				LevelGround._Triplanar_Primary_Weight = Shader.PropertyToID("_Triplanar_Primary_Weight");
			}
			Shader.SetGlobalFloat(LevelGround._Triplanar_Primary_Weight, LevelGround.triplanarPrimaryWeight);
			if (LevelGround._Triplanar_Secondary_Size == -1)
			{
				LevelGround._Triplanar_Secondary_Size = Shader.PropertyToID("_Triplanar_Secondary_Size");
			}
			Shader.SetGlobalFloat(LevelGround._Triplanar_Secondary_Size, LevelGround.triplanarSecondarySize);
			if (LevelGround._Triplanar_Secondary_Weight == -1)
			{
				LevelGround._Triplanar_Secondary_Weight = Shader.PropertyToID("_Triplanar_Secondary_Weight");
			}
			Shader.SetGlobalFloat(LevelGround._Triplanar_Secondary_Weight, LevelGround.triplanarSecondaryWeight);
			if (LevelGround._Triplanar_Tertiary_Size == -1)
			{
				LevelGround._Triplanar_Tertiary_Size = Shader.PropertyToID("_Triplanar_Tertiary_Size");
			}
			Shader.SetGlobalFloat(LevelGround._Triplanar_Tertiary_Size, LevelGround.triplanarTertiarySize);
			if (LevelGround._Triplanar_Tertiary_Weight == -1)
			{
				LevelGround._Triplanar_Tertiary_Weight = Shader.PropertyToID("_Triplanar_Tertiary_Weight");
			}
			Shader.SetGlobalFloat(LevelGround._Triplanar_Tertiary_Weight, LevelGround.triplanarTertiaryWeight);
		}

		// Token: 0x04001660 RID: 5728
		private static int _Triplanar_Primary_Size = -1;

		// Token: 0x04001661 RID: 5729
		private static float _triplanarPrimarySize = 16f;

		// Token: 0x04001662 RID: 5730
		private static int _Triplanar_Primary_Weight = -1;

		// Token: 0x04001663 RID: 5731
		private static float _triplanarPrimaryWeight = 0.4f;

		// Token: 0x04001664 RID: 5732
		private static int _Triplanar_Secondary_Size = -1;

		// Token: 0x04001665 RID: 5733
		private static float _triplanarSecondarySize = 64f;

		// Token: 0x04001666 RID: 5734
		private static int _Triplanar_Secondary_Weight = -1;

		// Token: 0x04001667 RID: 5735
		private static float _triplanarSecondaryWeight = 0.4f;

		// Token: 0x04001668 RID: 5736
		private static int _Triplanar_Tertiary_Size = -1;

		// Token: 0x04001669 RID: 5737
		private static float _triplanarTertiarySize = 4f;

		// Token: 0x0400166A RID: 5738
		private static int _Triplanar_Tertiary_Weight = -1;

		// Token: 0x0400166B RID: 5739
		private static float _triplanarTertiaryWeight = 0.2f;

		// Token: 0x0400166C RID: 5740
		private static Collider[] obstructionColliders = new Collider[16];

		// Token: 0x0400166D RID: 5741
		public static readonly byte SAVEDATA_HEIGHTS_VERSION = 1;

		// Token: 0x0400166E RID: 5742
		public static readonly byte SAVEDATA_HEIGHTS2_VERSION = 1;

		// Token: 0x0400166F RID: 5743
		public static readonly byte SAVEDATA_MATERIALS_VERSION = 8;

		// Token: 0x04001670 RID: 5744
		public static readonly byte SAVEDATA_DETAILS_VERSION = 5;

		// Token: 0x04001671 RID: 5745
		public static readonly byte SAVEDATA_RESOURCES_VERSION = 8;

		// Token: 0x04001672 RID: 5746
		public static readonly byte SAVEDATA_TREES_VERSION = 5;

		// Token: 0x04001673 RID: 5747
		public static readonly byte RESOURCE_REGIONS = 3;

		// Token: 0x04001674 RID: 5748
		public static readonly byte ALPHAMAPS = 2;

		// Token: 0x04001675 RID: 5749
		private static Texture2D MASK;

		// Token: 0x04001676 RID: 5750
		private static float[][,] reunHeight;

		// Token: 0x04001677 RID: 5751
		private static float[][,] reunHeight2;

		// Token: 0x04001678 RID: 5752
		private static float[][,,] reunMaterial;

		// Token: 0x04001679 RID: 5753
		private static float[][,,] reunMaterial2;

		// Token: 0x0400167A RID: 5754
		private static int frameHeight;

		// Token: 0x0400167B RID: 5755
		private static int frameHeight2;

		// Token: 0x0400167C RID: 5756
		private static int frameMaterial;

		// Token: 0x0400167D RID: 5757
		private static int frameMaterial2;

		// Token: 0x0400167E RID: 5758
		private static bool _previewHQ;

		// Token: 0x0400167F RID: 5759
		private static float[,,] alphamapHQ;

		// Token: 0x04001680 RID: 5760
		private static float[,,] alphamap2HQ;

		// Token: 0x04001681 RID: 5761
		private static float[,,] alphamap;

		// Token: 0x04001682 RID: 5762
		private static float[,,] alphamap2;

		// Token: 0x04001683 RID: 5763
		private static float[,,] tempMap;

		// Token: 0x04001684 RID: 5764
		private static bool isBakingResources;

		// Token: 0x04001685 RID: 5765
		private static bool isBakingSkyboxResources;

		// Token: 0x04001686 RID: 5766
		private static byte bakeResources_X;

		// Token: 0x04001687 RID: 5767
		private static byte bakeResources_Y;

		// Token: 0x04001688 RID: 5768
		private static byte bakeResources_W;

		// Token: 0x04001689 RID: 5769
		private static byte bakeResources_H;

		// Token: 0x0400168A RID: 5770
		private static byte bakeResources_M;

		// Token: 0x0400168B RID: 5771
		public static GroundMaterial[] _materials;

		// Token: 0x0400168D RID: 5773
		public static GroundDetail[] _details;

		// Token: 0x0400168E RID: 5774
		public static GroundResource[] _resources;

		// Token: 0x0400168F RID: 5775
		private static byte[] _hash;

		// Token: 0x04001690 RID: 5776
		private static byte[] hashHeights;

		// Token: 0x04001691 RID: 5777
		private static byte[] hashTrees;

		// Token: 0x04001692 RID: 5778
		private static Transform _models;

		// Token: 0x04001693 RID: 5779
		private static Transform _models2;

		// Token: 0x04001694 RID: 5780
		private static List<ResourceSpawnpoint>[,] _trees;

		// Token: 0x04001695 RID: 5781
		private static int _total;

		// Token: 0x04001696 RID: 5782
		private static bool[,] _regions;

		// Token: 0x04001697 RID: 5783
		private static int[,] loads;

		// Token: 0x04001699 RID: 5785
		private static Terrain _terrain;

		// Token: 0x0400169A RID: 5786
		private static Terrain _terrain2;

		// Token: 0x0400169B RID: 5787
		private static TerrainData _data;

		// Token: 0x0400169C RID: 5788
		private static TerrainData _data2;

		// Token: 0x0400169D RID: 5789
		[CompilerGenerated]
		private static PlayerTeleported <>f__mg$cache0;

		// Token: 0x0400169E RID: 5790
		[CompilerGenerated]
		private static PlayerRegionUpdated <>f__mg$cache1;

		// Token: 0x0400169F RID: 5791
		[CompilerGenerated]
		private static EditorRegionUpdated <>f__mg$cache2;

		// Token: 0x040016A0 RID: 5792
		[CompilerGenerated]
		private static PlayerCreated <>f__mg$cache3;

		// Token: 0x040016A1 RID: 5793
		[CompilerGenerated]
		private static EditorAreaRegisteredHandler <>f__mg$cache4;

		// Token: 0x040016A2 RID: 5794
		[CompilerGenerated]
		private static LevelHierarchyLoaded <>f__mg$cache5;

		// Token: 0x040016A3 RID: 5795
		[CompilerGenerated]
		private static FoliageSystemPreBakeTileHandler <>f__mg$cache6;

		// Token: 0x040016A4 RID: 5796
		[CompilerGenerated]
		private static FoliageSystemPostBakeHandler <>f__mg$cache7;
	}
}
