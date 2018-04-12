using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using SDG.Framework.IO.FormattedFiles;
using SDG.Framework.Landscapes;
using SDG.Framework.Utilities;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.Foliage
{
	// Token: 0x020001AE RID: 430
	public class FoliageTile : IFormattedFileReadable, IFormattedFileWritable
	{
		// Token: 0x06000CCE RID: 3278 RVA: 0x0005E6AC File Offset: 0x0005CAAC
		public FoliageTile(FoliageCoord newCoord)
		{
			this.coord = newCoord;
			this.hasInstances = false;
			this.canSafelyClear = true;
			this.cuts = new List<IShapeVolume>();
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000CCF RID: 3279 RVA: 0x0005E6DF File Offset: 0x0005CADF
		// (set) Token: 0x06000CD0 RID: 3280 RVA: 0x0005E6E7 File Offset: 0x0005CAE7
		public FoliageCoord coord
		{
			get
			{
				return this._coord;
			}
			protected set
			{
				this._coord = value;
				this.updateBounds();
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x0005E6F6 File Offset: 0x0005CAF6
		// (set) Token: 0x06000CD2 RID: 3282 RVA: 0x0005E6FE File Offset: 0x0005CAFE
		public bool hasInstances { get; protected set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x0005E707 File Offset: 0x0005CB07
		// (set) Token: 0x06000CD4 RID: 3284 RVA: 0x0005E70F File Offset: 0x0005CB0F
		public Bounds worldBounds { get; protected set; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x0005E718 File Offset: 0x0005CB18
		// (set) Token: 0x06000CD6 RID: 3286 RVA: 0x0005E720 File Offset: 0x0005CB20
		public bool canSafelyClear { get; protected set; }

		// Token: 0x06000CD7 RID: 3287 RVA: 0x0005E72C File Offset: 0x0005CB2C
		public void addCut(IShapeVolume cut)
		{
			this.cuts.Add(cut);
			if (!this.hasInstances)
			{
				return;
			}
			foreach (KeyValuePair<AssetReference<FoliageInstancedMeshInfoAsset>, FoliageInstanceList> keyValuePair in this.instances)
			{
				FoliageInstanceList value = keyValuePair.Value;
				for (int i = 0; i < value.matrices.Count; i++)
				{
					List<Matrix4x4> list = value.matrices[i];
					List<bool> list2 = value.clearWhenBaked[i];
					for (int j = list.Count - 1; j >= 0; j--)
					{
						if (cut.containsPoint(list[j].GetPosition()))
						{
							list.RemoveAt(j);
							list2.RemoveAt(j);
						}
					}
				}
			}
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x0005E824 File Offset: 0x0005CC24
		public bool isInstanceCut(Vector3 point)
		{
			foreach (IShapeVolume shapeVolume in this.cuts)
			{
				if (shapeVolume.containsPoint(point))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x0005E890 File Offset: 0x0005CC90
		protected FoliageInstanceList getOrAddList(Dictionary<AssetReference<FoliageInstancedMeshInfoAsset>, FoliageInstanceList> source, AssetReference<FoliageInstancedMeshInfoAsset> assetReference)
		{
			FoliageInstanceList foliageInstanceList;
			if (!source.TryGetValue(assetReference, out foliageInstanceList))
			{
				object typeFromHandle = typeof(PoolablePool<FoliageInstanceList>);
				lock (typeFromHandle)
				{
					foliageInstanceList = PoolablePool<FoliageInstanceList>.claim();
				}
				foliageInstanceList.assetReference = assetReference;
				source.Add(assetReference, foliageInstanceList);
			}
			return foliageInstanceList;
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x0005E8F0 File Offset: 0x0005CCF0
		public void addInstance(FoliageInstanceGroup instance)
		{
			if (!this.hasInstances)
			{
				return;
			}
			FoliageInstanceList orAddList = this.getOrAddList(this.instances, instance.assetReference);
			orAddList.addInstanceRandom(instance);
			this.updateBounds();
			this.canSafelyClear = false;
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x0005E931 File Offset: 0x0005CD31
		public void removeInstance(FoliageInstanceList list, int matricesIndex, int matrixIndex)
		{
			if (!this.hasInstances)
			{
				return;
			}
			list.removeInstance(matricesIndex, matrixIndex);
			this.canSafelyClear = false;
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x0005E950 File Offset: 0x0005CD50
		public void clearInstances()
		{
			this.hasInstances = false;
			if (this.instances.Count > 0)
			{
				object typeFromHandle = typeof(PoolablePool<FoliageInstanceList>);
				lock (typeFromHandle)
				{
					foreach (KeyValuePair<AssetReference<FoliageInstancedMeshInfoAsset>, FoliageInstanceList> keyValuePair in this.instances)
					{
						FoliageInstanceList value = keyValuePair.Value;
						PoolablePool<FoliageInstanceList>.release(value);
					}
				}
			}
			this.instances = null;
			this.isReadingInstances = false;
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x0005EA04 File Offset: 0x0005CE04
		public void clearGeneratedInstances()
		{
			foreach (KeyValuePair<AssetReference<FoliageInstancedMeshInfoAsset>, FoliageInstanceList> keyValuePair in this.instances)
			{
				FoliageInstanceList value = keyValuePair.Value;
				value.clearGeneratedInstances();
			}
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x0005EA68 File Offset: 0x0005CE68
		public void applyScale()
		{
			foreach (KeyValuePair<AssetReference<FoliageInstancedMeshInfoAsset>, FoliageInstanceList> keyValuePair in this.instances)
			{
				FoliageInstanceList value = keyValuePair.Value;
				value.applyScale();
			}
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x0005EACC File Offset: 0x0005CECC
		public virtual void read(IFormattedFileReader reader)
		{
			reader = reader.readObject();
			this.coord = reader.readValue<FoliageCoord>("Coord");
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x0005EAE8 File Offset: 0x0005CEE8
		public virtual void readInstancesJob()
		{
			object obj = this.thisLock;
			lock (obj)
			{
				if (!this.hasInstances && !this.isReadingInstances)
				{
					this.isReadingInstances = true;
					ThreadPool.QueueUserWorkItem(new WaitCallback(this.readInstances));
				}
			}
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x0005EB54 File Offset: 0x0005CF54
		public virtual void readInstancesOnThread()
		{
			object obj = this.thisLock;
			lock (obj)
			{
				if (!this.hasInstances && !this.isReadingInstances)
				{
					this.isReadingInstances = true;
					this.readInstances(null);
				}
			}
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x0005EBB4 File Offset: 0x0005CFB4
		protected virtual void readInstances(object stateInfo)
		{
			Dictionary<AssetReference<FoliageInstancedMeshInfoAsset>, FoliageInstanceList> source = new Dictionary<AssetReference<FoliageInstancedMeshInfoAsset>, FoliageInstanceList>();
			string path = string.Concat(new object[]
			{
				Level.info.path,
				"/Foliage/Tile_",
				this.coord.x,
				"_",
				this.coord.y,
				".foliage"
			});
			if (File.Exists(path))
			{
				using (FileStream fileStream = new FileStream(path, FileMode.Open))
				{
					BinaryReader binaryReader = new BinaryReader(fileStream);
					int num = binaryReader.ReadInt32();
					int num2 = binaryReader.ReadInt32();
					for (int i = 0; i < num2; i++)
					{
						GuidBuffer guidBuffer = default(GuidBuffer);
						object guid_BUFFER = GuidBuffer.GUID_BUFFER;
						lock (guid_BUFFER)
						{
							fileStream.Read(GuidBuffer.GUID_BUFFER, 0, 16);
							guidBuffer.Read(GuidBuffer.GUID_BUFFER, 0);
						}
						AssetReference<FoliageInstancedMeshInfoAsset> assetReference = new AssetReference<FoliageInstancedMeshInfoAsset>(guidBuffer.GUID);
						FoliageInstanceList orAddList = this.getOrAddList(source, assetReference);
						int num3 = binaryReader.ReadInt32();
						for (int j = 0; j < num3; j++)
						{
							Matrix4x4 matrix4x = default(Matrix4x4);
							for (int k = 0; k < 16; k++)
							{
								matrix4x[k] = binaryReader.ReadSingle();
							}
							bool newClearWhenBaked = num <= 2 || binaryReader.ReadBoolean();
							if (!this.isInstanceCut(matrix4x.GetPosition()))
							{
								orAddList.addInstanceAppend(new FoliageInstanceGroup(assetReference, matrix4x, newClearWhenBaked));
							}
						}
					}
				}
			}
			object obj = this.thisLock;
			lock (obj)
			{
				if (!this.hasInstances)
				{
					this.instances = source;
					this.updateBounds();
					this.hasInstances = true;
					this.isReadingInstances = false;
				}
			}
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x0005EDF4 File Offset: 0x0005D1F4
		public virtual void write(IFormattedFileWriter writer)
		{
			writer.beginObject();
			writer.writeValue<FoliageCoord>("Coord", this.coord);
			writer.endObject();
			if (this.hasInstances)
			{
				this.writeInstances();
			}
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x0005EE24 File Offset: 0x0005D224
		public virtual void writeInstances()
		{
			string path = string.Concat(new object[]
			{
				Level.info.path,
				"/Foliage/Tile_",
				this.coord.x,
				"_",
				this.coord.y,
				".foliage"
			});
			string directoryName = Path.GetDirectoryName(path);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate))
			{
				BinaryWriter binaryWriter = new BinaryWriter(fileStream);
				binaryWriter.Write(FoliageTile.FOLIAGE_FILE_VERSION);
				binaryWriter.Write(this.instances.Count);
				foreach (KeyValuePair<AssetReference<FoliageInstancedMeshInfoAsset>, FoliageInstanceList> keyValuePair in this.instances)
				{
					GuidBuffer guidBuffer = new GuidBuffer(keyValuePair.Key.GUID);
					object guid_BUFFER = GuidBuffer.GUID_BUFFER;
					lock (guid_BUFFER)
					{
						guidBuffer.Write(GuidBuffer.GUID_BUFFER, 0);
						fileStream.Write(GuidBuffer.GUID_BUFFER, 0, 16);
					}
					int num = 0;
					foreach (List<Matrix4x4> list in keyValuePair.Value.matrices)
					{
						num += list.Count;
					}
					binaryWriter.Write(num);
					for (int i = 0; i < keyValuePair.Value.matrices.Count; i++)
					{
						List<Matrix4x4> list2 = keyValuePair.Value.matrices[i];
						List<bool> list3 = keyValuePair.Value.clearWhenBaked[i];
						for (int j = 0; j < list2.Count; j++)
						{
							Matrix4x4 matrix4x = list2[j];
							for (int k = 0; k < 16; k++)
							{
								binaryWriter.Write(matrix4x[k]);
							}
							bool value = list3[j];
							binaryWriter.Write(value);
						}
					}
				}
			}
			this.canSafelyClear = true;
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x0005F0E8 File Offset: 0x0005D4E8
		protected void updateBounds()
		{
			if (this.hasInstances)
			{
				float num = Landscape.TILE_HEIGHT;
				float num2 = -Landscape.TILE_HEIGHT;
				foreach (KeyValuePair<AssetReference<FoliageInstancedMeshInfoAsset>, FoliageInstanceList> keyValuePair in this.instances)
				{
					FoliageInstanceList value = keyValuePair.Value;
					foreach (List<Matrix4x4> list in value.matrices)
					{
						foreach (Matrix4x4 matrix4x in list)
						{
							float m = matrix4x.m13;
							if (m < num)
							{
								num = m;
							}
							if (m > num2)
							{
								num2 = m;
							}
						}
					}
				}
				float num3 = num2 - num;
				this.worldBounds = new Bounds(new Vector3((float)this.coord.x * FoliageSystem.TILE_SIZE + FoliageSystem.TILE_SIZE / 2f, num + num3 / 2f, (float)this.coord.y * FoliageSystem.TILE_SIZE + FoliageSystem.TILE_SIZE / 2f), new Vector3(FoliageSystem.TILE_SIZE, num3, FoliageSystem.TILE_SIZE));
			}
			else
			{
				this.worldBounds = new Bounds(new Vector3((float)this.coord.x * FoliageSystem.TILE_SIZE + FoliageSystem.TILE_SIZE / 2f, 0f, (float)this.coord.y * FoliageSystem.TILE_SIZE + FoliageSystem.TILE_SIZE / 2f), new Vector3(FoliageSystem.TILE_SIZE, Landscape.TILE_HEIGHT, FoliageSystem.TILE_SIZE));
			}
		}

		// Token: 0x040008C6 RID: 2246
		public static readonly int FOLIAGE_FILE_VERSION = 3;

		// Token: 0x040008C7 RID: 2247
		protected FoliageCoord _coord;

		// Token: 0x040008CA RID: 2250
		public Dictionary<AssetReference<FoliageInstancedMeshInfoAsset>, FoliageInstanceList> instances;

		// Token: 0x040008CC RID: 2252
		public bool clearPostBake;

		// Token: 0x040008CD RID: 2253
		protected List<IShapeVolume> cuts;

		// Token: 0x040008CE RID: 2254
		protected bool isReadingInstances;

		// Token: 0x040008CF RID: 2255
		protected object thisLock = new object();
	}
}
