using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

// Token: 0x02000071 RID: 113
public class ProceduralWorld : MonoBehaviour
{
	// Token: 0x060003CB RID: 971 RVA: 0x0001DAD0 File Offset: 0x0001BED0
	private void Start()
	{
		this.Update();
		AstarPath.active.Scan();
	}

	// Token: 0x060003CC RID: 972 RVA: 0x0001DAE4 File Offset: 0x0001BEE4
	private void Update()
	{
		Int2 @int = new Int2(Mathf.RoundToInt((this.target.position.x - this.tileSize * 0.5f) / this.tileSize), Mathf.RoundToInt((this.target.position.z - this.tileSize * 0.5f) / this.tileSize));
		this.range = ((this.range >= 1) ? this.range : 1);
		bool flag = true;
		while (flag)
		{
			flag = false;
			foreach (KeyValuePair<Int2, ProceduralWorld.ProceduralTile> keyValuePair in this.tiles)
			{
				if (Mathf.Abs(keyValuePair.Key.x - @int.x) > this.range || Mathf.Abs(keyValuePair.Key.y - @int.y) > this.range)
				{
					keyValuePair.Value.Destroy();
					this.tiles.Remove(keyValuePair.Key);
					flag = true;
					break;
				}
			}
		}
		for (int i = @int.x - this.range; i <= @int.x + this.range; i++)
		{
			for (int j = @int.y - this.range; j <= @int.y + this.range; j++)
			{
				if (!this.tiles.ContainsKey(new Int2(i, j)))
				{
					ProceduralWorld.ProceduralTile proceduralTile = new ProceduralWorld.ProceduralTile(this, i, j);
					base.StartCoroutine(proceduralTile.Generate());
					this.tiles.Add(new Int2(i, j), proceduralTile);
				}
			}
		}
		for (int k = @int.x - 1; k <= @int.x + 1; k++)
		{
			for (int l = @int.y - 1; l <= @int.y + 1; l++)
			{
				this.tiles[new Int2(k, l)].ForceFinish();
			}
		}
	}

	// Token: 0x04000332 RID: 818
	public Transform target;

	// Token: 0x04000333 RID: 819
	public ProceduralWorld.ProceduralPrefab[] prefabs;

	// Token: 0x04000334 RID: 820
	public int range;

	// Token: 0x04000335 RID: 821
	public float tileSize = 100f;

	// Token: 0x04000336 RID: 822
	public int subTiles = 20;

	// Token: 0x04000337 RID: 823
	private Dictionary<Int2, ProceduralWorld.ProceduralTile> tiles = new Dictionary<Int2, ProceduralWorld.ProceduralTile>();

	// Token: 0x02000072 RID: 114
	[Serializable]
	public class ProceduralPrefab
	{
		// Token: 0x04000338 RID: 824
		public GameObject prefab;

		// Token: 0x04000339 RID: 825
		public float density;

		// Token: 0x0400033A RID: 826
		public float perlin;

		// Token: 0x0400033B RID: 827
		public float perlinPower = 1f;

		// Token: 0x0400033C RID: 828
		public Vector2 perlinOffset = Vector2.zero;

		// Token: 0x0400033D RID: 829
		public float perlinScale = 1f;

		// Token: 0x0400033E RID: 830
		public float random = 1f;

		// Token: 0x0400033F RID: 831
		public bool singleFixed;
	}

	// Token: 0x02000073 RID: 115
	private class ProceduralTile
	{
		// Token: 0x060003CE RID: 974 RVA: 0x0001DD80 File Offset: 0x0001C180
		public ProceduralTile(ProceduralWorld world, int x, int z)
		{
			this.x = x;
			this.z = z;
			this.world = world;
			this.rnd = new System.Random(x * 10007 ^ z * 36007);
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0001DDB8 File Offset: 0x0001C1B8
		public IEnumerator Generate()
		{
			this.ie = this.InternalGenerate();
			GameObject rt = new GameObject(string.Concat(new object[]
			{
				"Tile ",
				this.x,
				" ",
				this.z
			}));
			this.root = rt.transform;
			while (this.ie != null && this.root != null && this.ie.MoveNext())
			{
				yield return this.ie.Current;
			}
			this.ie = null;
			yield break;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0001DDD3 File Offset: 0x0001C1D3
		public void ForceFinish()
		{
			while (this.ie != null && this.root != null && this.ie.MoveNext())
			{
			}
			this.ie = null;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0001DE10 File Offset: 0x0001C210
		private Vector3 RandomInside()
		{
			return new Vector3
			{
				x = ((float)this.x + (float)this.rnd.NextDouble()) * this.world.tileSize,
				z = ((float)this.z + (float)this.rnd.NextDouble()) * this.world.tileSize
			};
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0001DE74 File Offset: 0x0001C274
		private Vector3 RandomInside(float px, float pz)
		{
			return new Vector3
			{
				x = (px + (float)this.rnd.NextDouble() / (float)this.world.subTiles) * this.world.tileSize,
				z = (pz + (float)this.rnd.NextDouble() / (float)this.world.subTiles) * this.world.tileSize
			};
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0001DEE6 File Offset: 0x0001C2E6
		private Quaternion RandomYRot()
		{
			return Quaternion.Euler(360f * (float)this.rnd.NextDouble(), 0f, 360f * (float)this.rnd.NextDouble());
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0001DF18 File Offset: 0x0001C318
		private IEnumerator InternalGenerate()
		{
			Debug.Log(string.Concat(new object[]
			{
				"Generating tile ",
				this.x,
				", ",
				this.z
			}));
			int counter = 0;
			float[,] ditherMap = new float[this.world.subTiles + 2, this.world.subTiles + 2];
			for (int i = 0; i < this.world.prefabs.Length; i++)
			{
				ProceduralWorld.ProceduralPrefab pref = this.world.prefabs[i];
				if (pref.singleFixed)
				{
					Vector3 position = new Vector3(((float)this.x + 0.5f) * this.world.tileSize, 0f, ((float)this.z + 0.5f) * this.world.tileSize);
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(pref.prefab, position, Quaternion.identity);
					gameObject.transform.parent = this.root;
				}
				else
				{
					float subSize = this.world.tileSize / (float)this.world.subTiles;
					for (int k = 0; k < this.world.subTiles; k++)
					{
						for (int l = 0; l < this.world.subTiles; l++)
						{
							ditherMap[k + 1, l + 1] = 0f;
						}
					}
					for (int sx = 0; sx < this.world.subTiles; sx++)
					{
						for (int sz = 0; sz < this.world.subTiles; sz++)
						{
							float px = (float)this.x + (float)sx / (float)this.world.subTiles;
							float pz = (float)this.z + (float)sz / (float)this.world.subTiles;
							float perl = Mathf.Pow(Mathf.PerlinNoise((px + pref.perlinOffset.x) * pref.perlinScale, (pz + pref.perlinOffset.y) * pref.perlinScale), pref.perlinPower);
							float density = pref.density * Mathf.Lerp(1f, perl, pref.perlin) * Mathf.Lerp(1f, (float)this.rnd.NextDouble(), pref.random);
							float fcount = subSize * subSize * density + ditherMap[sx + 1, sz + 1];
							int count = Mathf.RoundToInt(fcount);
							ditherMap[sx + 1 + 1, sz + 1] += 0.4375f * (fcount - (float)count);
							ditherMap[sx + 1 - 1, sz + 1 + 1] += 0.1875f * (fcount - (float)count);
							ditherMap[sx + 1, sz + 1 + 1] += 0.3125f * (fcount - (float)count);
							ditherMap[sx + 1 + 1, sz + 1 + 1] += 0.0625f * (fcount - (float)count);
							for (int j = 0; j < count; j++)
							{
								Vector3 p = this.RandomInside(px, pz);
								GameObject ob = UnityEngine.Object.Instantiate<GameObject>(pref.prefab, p, this.RandomYRot());
								ob.transform.parent = this.root;
								counter++;
								if (counter % 2 == 0)
								{
									yield return null;
								}
							}
						}
					}
				}
			}
			ditherMap = null;
			yield return new WaitForSeconds(0.5f);
			if (Application.HasProLicense())
			{
				StaticBatchingUtility.Combine(this.root.gameObject);
			}
			yield break;
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0001DF34 File Offset: 0x0001C334
		public void Destroy()
		{
			Debug.Log(string.Concat(new object[]
			{
				"Destroying tile ",
				this.x,
				", ",
				this.z
			}));
			UnityEngine.Object.Destroy(this.root.gameObject);
			this.root = null;
		}

		// Token: 0x04000340 RID: 832
		private int x;

		// Token: 0x04000341 RID: 833
		private int z;

		// Token: 0x04000342 RID: 834
		private System.Random rnd;

		// Token: 0x04000343 RID: 835
		private ProceduralWorld world;

		// Token: 0x04000344 RID: 836
		private Transform root;

		// Token: 0x04000345 RID: 837
		private IEnumerator ie;
	}
}
