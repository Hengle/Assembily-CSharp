using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000730 RID: 1840
	public class GameObjectPoolDictionary
	{
		// Token: 0x060033F0 RID: 13296 RVA: 0x0015253A File Offset: 0x0015093A
		public GameObjectPoolDictionary()
		{
			this.pools = new Dictionary<GameObject, GameObjectPool>();
		}

		// Token: 0x060033F1 RID: 13297 RVA: 0x0015254D File Offset: 0x0015094D
		public GameObject Instantiate(GameObject prefab)
		{
			return this.Instantiate(prefab, Vector3.zero, Quaternion.identity);
		}

		// Token: 0x060033F2 RID: 13298 RVA: 0x00152560 File Offset: 0x00150960
		public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
		{
			GameObjectPool gameObjectPool;
			if (!this.pools.TryGetValue(prefab, out gameObjectPool))
			{
				gameObjectPool = new GameObjectPool(prefab);
				this.pools.Add(prefab, gameObjectPool);
			}
			return gameObjectPool.Instantiate(position, rotation);
		}

		// Token: 0x060033F3 RID: 13299 RVA: 0x0015259C File Offset: 0x0015099C
		public void Instantiate(GameObject prefab, Transform parent, string name, int count)
		{
			GameObjectPool gameObjectPool;
			if (!this.pools.TryGetValue(prefab, out gameObjectPool))
			{
				gameObjectPool = new GameObjectPool(prefab, count);
				this.pools.Add(prefab, gameObjectPool);
			}
			GameObject[] array = new GameObject[count];
			for (int i = 0; i < count; i++)
			{
				GameObject gameObject = gameObjectPool.Instantiate();
				gameObject.name = name;
				gameObject.transform.parent = parent;
				array[i] = gameObject;
			}
			for (int j = 0; j < count; j++)
			{
				gameObjectPool.Destroy(array[j].GetComponent<PoolReference>());
			}
		}

		// Token: 0x060033F4 RID: 13300 RVA: 0x00152634 File Offset: 0x00150A34
		public void Destroy(GameObject element)
		{
			if (element == null)
			{
				return;
			}
			PoolReference component = element.GetComponent<PoolReference>();
			if (component == null || component.pool == null)
			{
				UnityEngine.Object.Destroy(element);
				return;
			}
			component.pool.Destroy(component);
		}

		// Token: 0x060033F5 RID: 13301 RVA: 0x00152680 File Offset: 0x00150A80
		public void Destroy(GameObject element, float t)
		{
			if (element == null)
			{
				return;
			}
			PoolReference component = element.GetComponent<PoolReference>();
			if (component == null || component.pool == null)
			{
				UnityEngine.Object.Destroy(element);
				return;
			}
			component.Destroy(t);
		}

		// Token: 0x04002363 RID: 9059
		private Dictionary<GameObject, GameObjectPool> pools;
	}
}
