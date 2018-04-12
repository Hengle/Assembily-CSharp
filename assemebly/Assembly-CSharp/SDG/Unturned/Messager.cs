using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000562 RID: 1378
	public class Messager : MonoBehaviour
	{
		// Token: 0x0600260B RID: 9739 RVA: 0x000DF5FE File Offset: 0x000DD9FE
		private void OnTriggerStay(Collider other)
		{
			if (!Dedicator.isDedicated && other.transform.CompareTag("Player"))
			{
				this.lastTrigger = Time.realtimeSinceStartup;
			}
		}

		// Token: 0x0600260C RID: 9740 RVA: 0x000DF62A File Offset: 0x000DDA2A
		private void Update()
		{
			if (Time.realtimeSinceStartup - this.lastTrigger < 0.5f)
			{
				PlayerUI.hint(null, this.message);
			}
		}

		// Token: 0x040017B3 RID: 6067
		public EPlayerMessage message;

		// Token: 0x040017B4 RID: 6068
		private float lastTrigger;
	}
}
