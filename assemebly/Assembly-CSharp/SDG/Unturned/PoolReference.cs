using System;
using System.Collections;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000742 RID: 1858
	public class PoolReference : MonoBehaviour
	{
		// Token: 0x06003464 RID: 13412 RVA: 0x00157078 File Offset: 0x00155478
		private IEnumerator PoolDestroy()
		{
			yield return new WaitForSeconds(this.delay);
			if (this.pool == null)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
			else
			{
				this.pool.Destroy(this);
			}
			yield break;
		}

		// Token: 0x06003465 RID: 13413 RVA: 0x00157093 File Offset: 0x00155493
		public void Destroy(float t)
		{
			if (this.pool == null)
			{
				UnityEngine.Object.Destroy(base.gameObject, t);
				return;
			}
			this.delay = t;
			base.StartCoroutine("PoolDestroy");
		}

		// Token: 0x0400239F RID: 9119
		public GameObjectPool pool;

		// Token: 0x040023A0 RID: 9120
		public bool inPool;

		// Token: 0x040023A1 RID: 9121
		private float delay;
	}
}
