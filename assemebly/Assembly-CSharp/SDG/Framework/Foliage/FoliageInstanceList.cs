using System;
using System.Collections.Generic;
using SDG.Framework.Utilities;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.Foliage
{
	// Token: 0x020001A0 RID: 416
	public class FoliageInstanceList : IPoolable
	{
		// Token: 0x06000C2C RID: 3116 RVA: 0x0005C550 File Offset: 0x0005A950
		public FoliageInstanceList()
		{
			this.matrices = new List<List<Matrix4x4>>(1);
			this.clearWhenBaked = new List<List<bool>>(1);
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000C2D RID: 3117 RVA: 0x0005C570 File Offset: 0x0005A970
		// (set) Token: 0x06000C2E RID: 3118 RVA: 0x0005C578 File Offset: 0x0005A978
		public List<List<Matrix4x4>> matrices { get; protected set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000C2F RID: 3119 RVA: 0x0005C581 File Offset: 0x0005A981
		// (set) Token: 0x06000C30 RID: 3120 RVA: 0x0005C589 File Offset: 0x0005A989
		public List<List<bool>> clearWhenBaked { get; protected set; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x0005C592 File Offset: 0x0005A992
		// (set) Token: 0x06000C32 RID: 3122 RVA: 0x0005C59A File Offset: 0x0005A99A
		public bool isAssetLoaded { get; protected set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000C33 RID: 3123 RVA: 0x0005C5A3 File Offset: 0x0005A9A3
		// (set) Token: 0x06000C34 RID: 3124 RVA: 0x0005C5AB File Offset: 0x0005A9AB
		public Mesh mesh { get; protected set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000C35 RID: 3125 RVA: 0x0005C5B4 File Offset: 0x0005A9B4
		// (set) Token: 0x06000C36 RID: 3126 RVA: 0x0005C5BC File Offset: 0x0005A9BC
		public Material material { get; protected set; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000C37 RID: 3127 RVA: 0x0005C5C5 File Offset: 0x0005A9C5
		// (set) Token: 0x06000C38 RID: 3128 RVA: 0x0005C5CD File Offset: 0x0005A9CD
		public bool castShadows { get; protected set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x0005C5D6 File Offset: 0x0005A9D6
		// (set) Token: 0x06000C3A RID: 3130 RVA: 0x0005C5DE File Offset: 0x0005A9DE
		public bool tileDither { get; protected set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000C3B RID: 3131 RVA: 0x0005C5E7 File Offset: 0x0005A9E7
		// (set) Token: 0x06000C3C RID: 3132 RVA: 0x0005C5EF File Offset: 0x0005A9EF
		public int sqrDrawDistance { get; protected set; }

		// Token: 0x06000C3D RID: 3133 RVA: 0x0005C5F8 File Offset: 0x0005A9F8
		public virtual void poolClaim()
		{
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x0005C5FC File Offset: 0x0005A9FC
		public virtual void poolRelease()
		{
			this.assetReference = AssetReference<FoliageInstancedMeshInfoAsset>.invalid;
			object typeFromHandle = typeof(ListPool<Matrix4x4>);
			lock (typeFromHandle)
			{
				foreach (List<Matrix4x4> list in this.matrices)
				{
					ListPool<Matrix4x4>.release(list);
				}
				this.matrices.Clear();
			}
			object typeFromHandle2 = typeof(ListPool<bool>);
			lock (typeFromHandle2)
			{
				foreach (List<bool> list2 in this.clearWhenBaked)
				{
					ListPool<bool>.release(list2);
				}
				this.clearWhenBaked.Clear();
			}
			this.isAssetLoaded = false;
			this.mesh = null;
			this.material = null;
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x0005C72C File Offset: 0x0005AB2C
		public virtual void clearGeneratedInstances()
		{
			for (int i = 0; i < this.matrices.Count; i++)
			{
				List<Matrix4x4> list = this.matrices[i];
				List<bool> list2 = this.clearWhenBaked[i];
				for (int j = list.Count - 1; j >= 0; j--)
				{
					if (list2[j])
					{
						list.RemoveAt(j);
						list2.RemoveAt(j);
					}
				}
			}
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x0005C7A8 File Offset: 0x0005ABA8
		public virtual void applyScale()
		{
			FoliageInstancedMeshInfoAsset foliageInstancedMeshInfoAsset = Assets.find<FoliageInstancedMeshInfoAsset>(this.assetReference);
			if (foliageInstancedMeshInfoAsset == null)
			{
				return;
			}
			for (int i = 0; i < this.matrices.Count; i++)
			{
				List<Matrix4x4> list = this.matrices[i];
				List<bool> list2 = this.clearWhenBaked[i];
				for (int j = list.Count - 1; j >= 0; j--)
				{
					Matrix4x4 matrix4x = list[j];
					Vector3 position = matrix4x.GetPosition();
					Quaternion rotation = matrix4x.GetRotation();
					Vector3 randomScale = foliageInstancedMeshInfoAsset.randomScale;
					matrix4x = Matrix4x4.TRS(position, rotation, randomScale);
					list[j] = matrix4x;
				}
			}
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x0005C858 File Offset: 0x0005AC58
		protected virtual void getOrAddLists(out List<Matrix4x4> matrixList, out List<bool> clearWhenBakedList)
		{
			matrixList = null;
			foreach (List<Matrix4x4> list in this.matrices)
			{
				if (list.Count < 1023)
				{
					matrixList = list;
					break;
				}
			}
			if (matrixList == null)
			{
				object typeFromHandle = typeof(ListPool<Matrix4x4>);
				lock (typeFromHandle)
				{
					matrixList = ListPool<Matrix4x4>.claim();
				}
				this.matrices.Add(matrixList);
			}
			clearWhenBakedList = null;
			foreach (List<bool> list2 in this.clearWhenBaked)
			{
				if (list2.Count < 1023)
				{
					clearWhenBakedList = list2;
					break;
				}
			}
			if (clearWhenBakedList == null)
			{
				object typeFromHandle2 = typeof(ListPool<bool>);
				lock (typeFromHandle2)
				{
					clearWhenBakedList = ListPool<bool>.claim();
				}
				this.clearWhenBaked.Add(clearWhenBakedList);
			}
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x0005C9B4 File Offset: 0x0005ADB4
		public virtual void addInstanceRandom(FoliageInstanceGroup group)
		{
			List<Matrix4x4> list;
			List<bool> list2;
			this.getOrAddLists(out list, out list2);
			int index = UnityEngine.Random.Range(0, list.Count);
			list.Insert(index, group.matrix);
			list2.Insert(index, group.clearWhenBaked);
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x0005C9F4 File Offset: 0x0005ADF4
		public virtual void addInstanceAppend(FoliageInstanceGroup group)
		{
			List<Matrix4x4> list;
			List<bool> list2;
			this.getOrAddLists(out list, out list2);
			list.Add(group.matrix);
			list2.Add(group.clearWhenBaked);
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x0005CA28 File Offset: 0x0005AE28
		public virtual void removeInstance(int matricesIndex, int matrixIndex)
		{
			List<Matrix4x4> list = this.matrices[matricesIndex];
			List<bool> list2 = this.clearWhenBaked[matricesIndex];
			list.RemoveAt(matrixIndex);
			list2.RemoveAt(matrixIndex);
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x0005CA60 File Offset: 0x0005AE60
		public virtual void loadAsset()
		{
			if (this.isAssetLoaded)
			{
				return;
			}
			this.isAssetLoaded = true;
			FoliageInstancedMeshInfoAsset foliageInstancedMeshInfoAsset = Assets.find<FoliageInstancedMeshInfoAsset>(this.assetReference);
			if (foliageInstancedMeshInfoAsset == null)
			{
				return;
			}
			this.mesh = Assets.load<Mesh>(foliageInstancedMeshInfoAsset.mesh);
			this.material = Assets.load<Material>(foliageInstancedMeshInfoAsset.material);
			this.castShadows = foliageInstancedMeshInfoAsset.castShadows;
			this.tileDither = foliageInstancedMeshInfoAsset.tileDither;
			if (foliageInstancedMeshInfoAsset.drawDistance == -1)
			{
				this.sqrDrawDistance = -1;
			}
			else
			{
				this.sqrDrawDistance = foliageInstancedMeshInfoAsset.drawDistance * foliageInstancedMeshInfoAsset.drawDistance;
			}
		}

		// Token: 0x0400088C RID: 2188
		public AssetReference<FoliageInstancedMeshInfoAsset> assetReference;
	}
}
