using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000719 RID: 1817
	public class Acid : MonoBehaviour
	{
		// Token: 0x06003399 RID: 13209 RVA: 0x0014EC28 File Offset: 0x0014D028
		private void OnTriggerEnter(Collider other)
		{
			if (this.isExploded)
			{
				return;
			}
			if (other.isTrigger)
			{
				return;
			}
			if (other.transform.CompareTag("Agent"))
			{
				return;
			}
			this.isExploded = true;
			if (Provider.isServer)
			{
				if (Dedicator.isDedicated)
				{
					EffectManager.effect(this.effectID, this.lastPos, Vector3.up);
				}
				EffectManager.sendEffectReliable(this.effectID, EffectManager.LARGE, this.lastPos);
			}
			UnityEngine.Object.Destroy(base.transform.parent.gameObject);
		}

		// Token: 0x0600339A RID: 13210 RVA: 0x0014ECC0 File Offset: 0x0014D0C0
		private void FixedUpdate()
		{
			this.lastPos = base.transform.position;
		}

		// Token: 0x0600339B RID: 13211 RVA: 0x0014ECD3 File Offset: 0x0014D0D3
		private void Awake()
		{
			this.lastPos = base.transform.position;
		}

		// Token: 0x04002300 RID: 8960
		private bool isExploded;

		// Token: 0x04002301 RID: 8961
		private Vector3 lastPos;

		// Token: 0x04002302 RID: 8962
		public ushort effectID;
	}
}
