using System;
using System.IO;
using SDG.Framework.Debug;
using SDG.Framework.Foliage;
using SDG.Framework.IO.FormattedFiles;
using SDG.Framework.Utilities;
using SDG.Unturned;
using UnityEngine;
using UnityEngine.Rendering;

namespace SDG.Framework.Landscapes
{
	// Token: 0x020001E3 RID: 483
	public class LandscapeTile : IFormattedFileReadable, IFormattedFileWritable, IFoliageSurface
	{
		// Token: 0x06000E7D RID: 3709 RVA: 0x00063DFC File Offset: 0x000621FC
		public LandscapeTile(LandscapeCoord newCoord)
		{
			this.gameObject = new GameObject();
			this.gameObject.name = "Tile";
			this.gameObject.tag = "Ground";
			this.gameObject.layer = LayerMasks.GROUND;
			this.gameObject.transform.parent = Landscape.instance.transform;
			this.gameObject.transform.rotation = MathUtility.IDENTITY_QUATERNION;
			this.gameObject.transform.localScale = Vector3.one;
			this.coord = newCoord;
			this.sourceHeightmap = new float[Landscape.HEIGHTMAP_RESOLUTION, Landscape.HEIGHTMAP_RESOLUTION];
			this.sourceSplatmap = new float[Landscape.SPLATMAP_RESOLUTION, Landscape.SPLATMAP_RESOLUTION, Landscape.SPLATMAP_LAYERS];
			for (int i = 0; i < Landscape.HEIGHTMAP_RESOLUTION; i++)
			{
				for (int j = 0; j < Landscape.HEIGHTMAP_RESOLUTION; j++)
				{
					this.sourceHeightmap[i, j] = 0.5f;
				}
			}
			for (int k = 0; k < Landscape.SPLATMAP_RESOLUTION; k++)
			{
				for (int l = 0; l < Landscape.SPLATMAP_RESOLUTION; l++)
				{
					this.sourceSplatmap[k, l, 0] = 1f;
				}
			}
			this.materials = new InspectableList<AssetReference<LandscapeMaterialAsset>>(Landscape.SPLATMAP_LAYERS);
			for (int m = 0; m < Landscape.SPLATMAP_LAYERS; m++)
			{
				this.materials.Add(AssetReference<LandscapeMaterialAsset>.invalid);
			}
			this.materials.canInspectorAdd = false;
			this.materials.canInspectorRemove = false;
			this.materials.inspectorChanged += this.handleMaterialsInspectorChanged;
			this.prototypes = new SplatPrototype[Landscape.SPLATMAP_LAYERS];
			for (int n = 0; n < this.prototypes.Length; n++)
			{
				SplatPrototype splatPrototype = new SplatPrototype();
				splatPrototype.texture = Texture2D.blackTexture;
				this.prototypes[n] = splatPrototype;
			}
			this.data = new TerrainData();
			this.data.splatPrototypes = this.prototypes;
			this.data.heightmapResolution = Landscape.HEIGHTMAP_RESOLUTION;
			this.data.alphamapResolution = Landscape.SPLATMAP_RESOLUTION;
			this.data.baseMapResolution = Landscape.BASEMAP_RESOLUTION;
			this.data.size = new Vector3(Landscape.TILE_SIZE, Landscape.TILE_HEIGHT, Landscape.TILE_SIZE);
			this.data.SetHeightsDelayLOD(0, 0, this.sourceHeightmap);
			this.data.SetAlphamaps(0, 0, this.sourceSplatmap);
			this.data.wavingGrassTint = Color.white;
			this.terrain = this.gameObject.AddComponent<Terrain>();
			this.terrain.terrainData = this.data;
			this.terrain.heightmapPixelError = 200f;
			this.terrain.materialType = Terrain.MaterialType.Custom;
			this.terrain.reflectionProbeUsage = ReflectionProbeUsage.Off;
			this.terrain.castShadows = false;
			this.terrain.drawHeightmap = !Dedicator.isDedicated;
			this.terrain.drawTreesAndFoliage = false;
			this.terrain.collectDetailPatches = false;
			this.terrain.Flush();
			this.collider = this.gameObject.AddComponent<TerrainCollider>();
			this.collider.terrainData = this.data;
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000E7E RID: 3710 RVA: 0x00064145 File Offset: 0x00062545
		// (set) Token: 0x06000E7F RID: 3711 RVA: 0x0006414D File Offset: 0x0006254D
		public GameObject gameObject { get; protected set; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000E80 RID: 3712 RVA: 0x00064156 File Offset: 0x00062556
		// (set) Token: 0x06000E81 RID: 3713 RVA: 0x0006415E File Offset: 0x0006255E
		public LandscapeCoord coord
		{
			get
			{
				return this._coord;
			}
			protected set
			{
				this._coord = value;
				this.updateTransform();
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000E82 RID: 3714 RVA: 0x0006416D File Offset: 0x0006256D
		public Bounds localBounds
		{
			get
			{
				return new Bounds(new Vector3(Landscape.TILE_SIZE / 2f, 0f, Landscape.TILE_SIZE / 2f), new Vector3(Landscape.TILE_SIZE, Landscape.TILE_HEIGHT, Landscape.TILE_SIZE));
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000E83 RID: 3715 RVA: 0x000641A8 File Offset: 0x000625A8
		public Bounds worldBounds
		{
			get
			{
				Bounds localBounds = this.localBounds;
				localBounds.center += new Vector3((float)this.coord.x * Landscape.TILE_SIZE, 0f, (float)this.coord.y * Landscape.TILE_SIZE);
				return localBounds;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000E84 RID: 3716 RVA: 0x00064203 File Offset: 0x00062603
		// (set) Token: 0x06000E85 RID: 3717 RVA: 0x0006420B File Offset: 0x0006260B
		[Inspectable("#SDG::Tile.Materials", null)]
		public InspectableList<AssetReference<LandscapeMaterialAsset>> materials { get; protected set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000E86 RID: 3718 RVA: 0x00064214 File Offset: 0x00062614
		// (set) Token: 0x06000E87 RID: 3719 RVA: 0x0006421C File Offset: 0x0006261C
		public SplatPrototype[] prototypes { get; protected set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000E88 RID: 3720 RVA: 0x00064225 File Offset: 0x00062625
		// (set) Token: 0x06000E89 RID: 3721 RVA: 0x0006422D File Offset: 0x0006262D
		public TerrainData data { get; protected set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000E8A RID: 3722 RVA: 0x00064236 File Offset: 0x00062636
		// (set) Token: 0x06000E8B RID: 3723 RVA: 0x0006423E File Offset: 0x0006263E
		public Terrain terrain { get; protected set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000E8C RID: 3724 RVA: 0x00064247 File Offset: 0x00062647
		// (set) Token: 0x06000E8D RID: 3725 RVA: 0x0006424F File Offset: 0x0006264F
		public TerrainCollider collider { get; protected set; }

		// Token: 0x06000E8E RID: 3726 RVA: 0x00064258 File Offset: 0x00062658
		public virtual void read(IFormattedFileReader reader)
		{
			reader = reader.readObject();
			this.coord = reader.readValue<LandscapeCoord>("Coord");
			int num = reader.readArrayLength("Materials");
			for (int i = 0; i < num; i++)
			{
				this.materials[i] = reader.readValue<AssetReference<LandscapeMaterialAsset>>(i);
			}
			this.updatePrototypes();
			this.readHeightmaps();
			this.readSplatmaps();
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x000642C1 File Offset: 0x000626C1
		public virtual void readHeightmaps()
		{
			this.readHeightmap("_Source", this.sourceHeightmap);
			this.data.SetHeightsDelayLOD(0, 0, this.sourceHeightmap);
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x000642E8 File Offset: 0x000626E8
		protected virtual void readHeightmap(string suffix, float[,] heightmap)
		{
			string path = string.Concat(new object[]
			{
				Level.info.path,
				"/Landscape/Heightmaps/Tile_",
				this.coord.x,
				'_',
				this.coord.y,
				suffix,
				".heightmap"
			});
			if (!File.Exists(path))
			{
				return;
			}
			using (FileStream fileStream = new FileStream(path, FileMode.Open))
			{
				for (int i = 0; i < Landscape.HEIGHTMAP_RESOLUTION; i++)
				{
					for (int j = 0; j < Landscape.HEIGHTMAP_RESOLUTION; j++)
					{
						ushort num = (ushort)(fileStream.ReadByte() << 8 | fileStream.ReadByte());
						float num2 = (float)num / 65535f;
						heightmap[i, j] = num2;
					}
				}
			}
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x000643EC File Offset: 0x000627EC
		public virtual void readSplatmaps()
		{
			this.readSplatmap("_Source", this.sourceSplatmap);
			this.data.SetAlphamaps(0, 0, this.sourceSplatmap);
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x00064414 File Offset: 0x00062814
		protected virtual void readSplatmap(string suffix, float[,,] splatmap)
		{
			string path = string.Concat(new object[]
			{
				Level.info.path,
				"/Landscape/Splatmaps/Tile_",
				this.coord.x,
				'_',
				this.coord.y,
				suffix,
				".splatmap"
			});
			if (!File.Exists(path))
			{
				return;
			}
			using (FileStream fileStream = new FileStream(path, FileMode.Open))
			{
				for (int i = 0; i < Landscape.SPLATMAP_RESOLUTION; i++)
				{
					for (int j = 0; j < Landscape.SPLATMAP_RESOLUTION; j++)
					{
						for (int k = 0; k < Landscape.SPLATMAP_LAYERS; k++)
						{
							byte b = (byte)fileStream.ReadByte();
							float num = (float)b / 255f;
							splatmap[i, j, k] = num;
						}
					}
				}
			}
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x0006452C File Offset: 0x0006292C
		public virtual void write(IFormattedFileWriter writer)
		{
			writer.beginObject();
			writer.writeValue<LandscapeCoord>("Coord", this.coord);
			writer.beginArray("Materials");
			for (int i = 0; i < Landscape.SPLATMAP_LAYERS; i++)
			{
				writer.writeValue<AssetReference<LandscapeMaterialAsset>>(this.materials[i]);
			}
			writer.endArray();
			writer.endObject();
			this.writeHeightmaps();
			this.writeSplatmaps();
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x0006459B File Offset: 0x0006299B
		public virtual void writeHeightmaps()
		{
			this.writeHeightmap("_Source", this.sourceHeightmap);
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x000645B0 File Offset: 0x000629B0
		protected virtual void writeHeightmap(string suffix, float[,] heightmap)
		{
			string path = string.Concat(new object[]
			{
				Level.info.path,
				"/Landscape/Heightmaps/Tile_",
				this.coord.x,
				'_',
				this.coord.y,
				suffix,
				".heightmap"
			});
			string directoryName = Path.GetDirectoryName(path);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate))
			{
				for (int i = 0; i < Landscape.HEIGHTMAP_RESOLUTION; i++)
				{
					for (int j = 0; j < Landscape.HEIGHTMAP_RESOLUTION; j++)
					{
						float num = heightmap[i, j];
						ushort num2 = (ushort)Mathf.RoundToInt(num * 65535f);
						fileStream.WriteByte((byte)(num2 >> 8 & 255));
						fileStream.WriteByte((byte)(num2 & 255));
					}
				}
			}
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x000646D8 File Offset: 0x00062AD8
		public virtual void writeSplatmaps()
		{
			this.writeSplatmap("_Source", this.sourceSplatmap);
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x000646EC File Offset: 0x00062AEC
		protected virtual void writeSplatmap(string suffix, float[,,] splatmap)
		{
			string path = string.Concat(new object[]
			{
				Level.info.path,
				"/Landscape/Splatmaps/Tile_",
				this.coord.x,
				'_',
				this.coord.y,
				suffix,
				".splatmap"
			});
			string directoryName = Path.GetDirectoryName(path);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate))
			{
				for (int i = 0; i < Landscape.SPLATMAP_RESOLUTION; i++)
				{
					for (int j = 0; j < Landscape.SPLATMAP_RESOLUTION; j++)
					{
						for (int k = 0; k < Landscape.SPLATMAP_LAYERS; k++)
						{
							float num = splatmap[i, j, k];
							byte value = (byte)Mathf.RoundToInt(num * 255f);
							fileStream.WriteByte(value);
						}
					}
				}
			}
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x00064818 File Offset: 0x00062C18
		public void updatePrototypes()
		{
			for (int i = 0; i < Landscape.SPLATMAP_LAYERS; i++)
			{
				LandscapeMaterialAsset landscapeMaterialAsset = Assets.find<LandscapeMaterialAsset>(this.materials[i]);
				if (landscapeMaterialAsset == null)
				{
					this.prototypes[i].texture = Texture2D.blackTexture;
					this.prototypes[i].normalMap = Texture2D.blackTexture;
				}
				else
				{
					this.prototypes[i].texture = Assets.load<Texture2D>(landscapeMaterialAsset.texture);
					if (this.prototypes[i].texture == null)
					{
						this.prototypes[i].texture = Texture2D.blackTexture;
					}
					this.prototypes[i].normalMap = Assets.load<Texture2D>(landscapeMaterialAsset.mask);
					if (this.prototypes[i].normalMap == null)
					{
						this.prototypes[i].normalMap = Texture2D.blackTexture;
					}
				}
			}
			this.data.splatPrototypes = this.prototypes;
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x00064914 File Offset: 0x00062D14
		protected void updateTransform()
		{
			this.gameObject.transform.position = new Vector3((float)this.coord.x * Landscape.TILE_SIZE, -Landscape.TILE_HEIGHT / 2f, (float)this.coord.y * Landscape.TILE_SIZE);
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x0006496C File Offset: 0x00062D6C
		public void convertLegacyHeightmap()
		{
			for (int i = 0; i < Landscape.HEIGHTMAP_RESOLUTION; i++)
			{
				for (int j = 0; j < Landscape.HEIGHTMAP_RESOLUTION; j++)
				{
					HeightmapCoord heightmapCoord = new HeightmapCoord(i, j);
					Vector3 worldPosition = Landscape.getWorldPosition(this.coord, heightmapCoord, this.sourceHeightmap[i, j]);
					float num = LevelGround.getConversionHeight(worldPosition);
					num /= Landscape.TILE_HEIGHT;
					num += 0.5f;
					this.sourceHeightmap[i, j] = num;
				}
			}
			this.data.SetHeights(0, 0, this.sourceHeightmap);
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x00064A08 File Offset: 0x00062E08
		public void convertLegacySplatmap()
		{
			for (int i = 0; i < Landscape.SPLATMAP_RESOLUTION; i++)
			{
				for (int j = 0; j < Landscape.SPLATMAP_RESOLUTION; j++)
				{
					SplatmapCoord splatmapCoord = new SplatmapCoord(i, j);
					Vector3 worldPosition = Landscape.getWorldPosition(this.coord, splatmapCoord);
					for (int k = 0; k < Landscape.SPLATMAP_LAYERS; k++)
					{
						float conversionWeight = LevelGround.getConversionWeight(worldPosition, k, true);
						this.sourceSplatmap[i, j, k] = conversionWeight;
					}
				}
			}
			this.data.SetAlphamaps(0, 0, this.sourceSplatmap);
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x00064AA0 File Offset: 0x00062EA0
		public void resetHeightmap()
		{
			for (int i = 0; i < Landscape.HEIGHTMAP_RESOLUTION; i++)
			{
				for (int j = 0; j < Landscape.HEIGHTMAP_RESOLUTION; j++)
				{
					this.sourceHeightmap[i, j] = 0.5f;
				}
			}
			Landscape.reconcileNeighbors(this);
			this.terrain.ApplyDelayedHeightmapModification();
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x00064AFC File Offset: 0x00062EFC
		public void resetSplatmap()
		{
			for (int i = 0; i < Landscape.SPLATMAP_RESOLUTION; i++)
			{
				for (int j = 0; j < Landscape.SPLATMAP_RESOLUTION; j++)
				{
					this.sourceSplatmap[i, j, 0] = 1f;
					for (int k = 1; k < Landscape.SPLATMAP_LAYERS; k++)
					{
						this.sourceSplatmap[i, j, k] = 0f;
					}
				}
			}
			this.data.SetAlphamaps(0, 0, this.sourceSplatmap);
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x00064B84 File Offset: 0x00062F84
		public void normalizeSplatmap()
		{
			for (int i = 0; i < Landscape.SPLATMAP_RESOLUTION; i++)
			{
				for (int j = 0; j < Landscape.SPLATMAP_RESOLUTION; j++)
				{
					float num = 0f;
					for (int k = 0; k < Landscape.SPLATMAP_LAYERS; k++)
					{
						num += this.sourceSplatmap[i, j, k];
					}
					for (int l = 0; l < Landscape.SPLATMAP_LAYERS; l++)
					{
						this.sourceSplatmap[i, j, l] /= num;
					}
				}
			}
			this.data.SetAlphamaps(0, 0, this.sourceSplatmap);
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x00064C2C File Offset: 0x0006302C
		public void applyGraphicsSettings()
		{
			if (Dedicator.isDedicated)
			{
				return;
			}
			if (SDG.Unturned.GraphicsSettings.blend)
			{
				ERenderMode renderMode = SDG.Unturned.GraphicsSettings.renderMode;
				if (renderMode != ERenderMode.FORWARD)
				{
					if (renderMode != ERenderMode.DEFERRED)
					{
						this.terrain.materialTemplate = null;
						Debug.LogError("Unknown render mode: " + SDG.Unturned.GraphicsSettings.renderMode);
					}
					else
					{
						this.terrain.materialTemplate = Resources.Load<Material>("Materials/Landscapes/Landscape_Deferred");
					}
				}
				else
				{
					this.terrain.materialTemplate = Resources.Load<Material>("Materials/Landscapes/Landscape_Forward");
				}
				this.terrain.basemapDistance = 512f;
			}
			else
			{
				this.terrain.materialTemplate = Resources.Load<Material>("Materials/Landscapes/Landscape_Classic");
				this.terrain.basemapDistance = 256f;
			}
			switch (SDG.Unturned.GraphicsSettings.terrainQuality)
			{
			case EGraphicQuality.LOW:
				this.terrain.heightmapPixelError = 64f;
				break;
			case EGraphicQuality.MEDIUM:
				this.terrain.heightmapPixelError = 32f;
				break;
			case EGraphicQuality.HIGH:
				this.terrain.heightmapPixelError = 16f;
				break;
			case EGraphicQuality.ULTRA:
				this.terrain.heightmapPixelError = 8f;
				break;
			}
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x00064D78 File Offset: 0x00063178
		public FoliageBounds getFoliageSurfaceBounds()
		{
			return new FoliageBounds(new FoliageCoord(this.coord.x * Landscape.TILE_SIZE_INT / FoliageSystem.TILE_SIZE_INT, this.coord.y * Landscape.TILE_SIZE_INT / FoliageSystem.TILE_SIZE_INT), new FoliageCoord((this.coord.x + 1) * Landscape.TILE_SIZE_INT / FoliageSystem.TILE_SIZE_INT - 1, (this.coord.y + 1) * Landscape.TILE_SIZE_INT / FoliageSystem.TILE_SIZE_INT - 1));
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x00064E04 File Offset: 0x00063204
		public bool getFoliageSurfaceInfo(Vector3 position, out Vector3 surfacePosition, out Vector3 surfaceNormal)
		{
			surfacePosition = position;
			surfacePosition.y = this.terrain.SampleHeight(position) - Landscape.TILE_HEIGHT / 2f;
			surfaceNormal = this.data.GetInterpolatedNormal((position.x - (float)this.coord.x * Landscape.TILE_SIZE) / Landscape.TILE_SIZE, (position.z - (float)this.coord.y * Landscape.TILE_SIZE) / Landscape.TILE_SIZE);
			return !LandscapeHoleUtility.isPointInsideHoleVolume(surfacePosition);
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x00064E9C File Offset: 0x0006329C
		public void bakeFoliageSurface(FoliageBakeSettings bakeSettings, FoliageTile foliageTile)
		{
			int num = (foliageTile.coord.y * FoliageSystem.TILE_SIZE_INT - this.coord.y * Landscape.TILE_SIZE_INT) / FoliageSystem.TILE_SIZE_INT * FoliageSystem.SPLATMAP_RESOLUTION_PER_TILE;
			int num2 = num + FoliageSystem.SPLATMAP_RESOLUTION_PER_TILE;
			int num3 = (foliageTile.coord.x * FoliageSystem.TILE_SIZE_INT - this.coord.x * Landscape.TILE_SIZE_INT) / FoliageSystem.TILE_SIZE_INT * FoliageSystem.SPLATMAP_RESOLUTION_PER_TILE;
			int num4 = num3 + FoliageSystem.SPLATMAP_RESOLUTION_PER_TILE;
			for (int i = num; i < num2; i++)
			{
				for (int j = num3; j < num4; j++)
				{
					SplatmapCoord splatmapCoord = new SplatmapCoord(i, j);
					float num5 = (float)this.coord.x * Landscape.TILE_SIZE + (float)splatmapCoord.y * Landscape.SPLATMAP_WORLD_UNIT;
					float num6 = (float)this.coord.y * Landscape.TILE_SIZE + (float)splatmapCoord.x * Landscape.SPLATMAP_WORLD_UNIT;
					Bounds bounds = default(Bounds);
					bounds.min = new Vector3(num5, 0f, num6);
					bounds.max = new Vector3(num5 + Landscape.SPLATMAP_WORLD_UNIT, 0f, num6 + Landscape.SPLATMAP_WORLD_UNIT);
					for (int k = 0; k < Landscape.SPLATMAP_LAYERS; k++)
					{
						float num7 = this.sourceSplatmap[i, j, k];
						if (num7 >= 0.01f)
						{
							LandscapeMaterialAsset landscapeMaterialAsset = Assets.find<LandscapeMaterialAsset>(this.materials[k]);
							if (landscapeMaterialAsset != null)
							{
								FoliageInfoCollectionAsset foliageInfoCollectionAsset = Assets.find<FoliageInfoCollectionAsset>(landscapeMaterialAsset.foliage);
								if (foliageInfoCollectionAsset != null)
								{
									foliageInfoCollectionAsset.bakeFoliage(bakeSettings, this, bounds, num7);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x0006506A File Offset: 0x0006346A
		protected virtual void handleMaterialsInspectorChanged(IInspectableList list)
		{
			this.updatePrototypes();
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x00065072 File Offset: 0x00063472
		public virtual void enable()
		{
			FoliageSystem.addSurface(this);
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x0006507A File Offset: 0x0006347A
		public virtual void disable()
		{
			FoliageSystem.removeSurface(this);
		}

		// Token: 0x04000934 RID: 2356
		protected LandscapeCoord _coord;

		// Token: 0x04000935 RID: 2357
		public float[,] sourceHeightmap;

		// Token: 0x04000936 RID: 2358
		public float[,,] sourceSplatmap;
	}
}
