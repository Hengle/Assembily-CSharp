using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200071D RID: 1821
	public class Distraction : MonoBehaviour
	{
		// Token: 0x060033A7 RID: 13223 RVA: 0x0014F1C6 File Offset: 0x0014D5C6
		public void Distract()
		{
			AlertTool.alert(base.transform.position, 24f);
			UnityEngine.Object.Destroy(this);
		}

		// Token: 0x060033A8 RID: 13224 RVA: 0x0014F1E3 File Offset: 0x0014D5E3
		private void Start()
		{
			base.Invoke("Distract", 2.5f);
		}
	}
}
