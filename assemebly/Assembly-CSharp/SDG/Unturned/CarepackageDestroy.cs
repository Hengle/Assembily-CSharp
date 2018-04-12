using System;
using System.Collections;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200071C RID: 1820
	public class CarepackageDestroy : MonoBehaviour
	{
		// Token: 0x060033A4 RID: 13220 RVA: 0x0014F0EC File Offset: 0x0014D4EC
		private IEnumerator cleanup()
		{
			yield return new WaitForSeconds(600f);
			BarricadeManager.damage(base.transform, 65000f, 1f, false);
			yield break;
		}

		// Token: 0x060033A5 RID: 13221 RVA: 0x0014F107 File Offset: 0x0014D507
		private void Start()
		{
			base.StartCoroutine("cleanup");
		}
	}
}
