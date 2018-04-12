using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200072F RID: 1839
	public class GameObjectPool
	{
		// Token: 0x060033EB RID: 13291 RVA: 0x001523F8 File Offset: 0x001507F8
		public GameObjectPool(GameObject prefab) : this(prefab, 1)
		{
		}

		// Token: 0x060033EC RID: 13292 RVA: 0x00152402 File Offset: 0x00150802
		public GameObjectPool(GameObject prefab, int count)
		{
			this.prefab = prefab;
			this.pool = new Stack<GameObject>(count);
		}

		// Token: 0x060033ED RID: 13293 RVA: 0x0015241D File Offset: 0x0015081D
		public GameObject Instantiate()
		{
			return this.Instantiate(Vector3.zero, Quaternion.identity);
		}

		// Token: 0x060033EE RID: 13294 RVA: 0x00152430 File Offset: 0x00150830
		public GameObject Instantiate(Vector3 position, Quaternion rotation)
		{
			GameObject gameObject;
			if (this.pool.Count > 0)
			{
				gameObject = this.pool.Pop();
				if (gameObject == null)
				{
					gameObject = this.Instantiate(position, rotation);
				}
				else
				{
					gameObject.transform.parent = null;
					gameObject.transform.position = position;
					gameObject.transform.rotation = rotation;
					gameObject.transform.localScale = Vector3.one;
					gameObject.SetActive(true);
				}
				PoolReference component = gameObject.GetComponent<PoolReference>();
				component.inPool = false;
			}
			else
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefab, position, rotation);
				PoolReference poolReference = gameObject.AddComponent<PoolReference>();
				poolReference.pool = this;
				poolReference.inPool = false;
			}
			return gameObject;
		}

		// Token: 0x060033EF RID: 13295 RVA: 0x001524E8 File Offset: 0x001508E8
		public void Destroy(PoolReference reference)
		{
			if (reference == null || reference.inPool || reference.pool != this)
			{
				return;
			}
			GameObject gameObject = reference.gameObject;
			gameObject.SetActive(false);
			this.pool.Push(gameObject);
			reference.inPool = true;
		}

		// Token: 0x04002361 RID: 9057
		private GameObject prefab;

		// Token: 0x04002362 RID: 9058
		private Stack<GameObject> pool;
	}
}
