using System;
using UnityEngine;

namespace SDG.Framework.Utilities
{
	// Token: 0x02000310 RID: 784
	public class TimeUtility : MonoBehaviour
	{
		// Token: 0x14000067 RID: 103
		// (add) Token: 0x06001656 RID: 5718 RVA: 0x00084AC0 File Offset: 0x00082EC0
		// (remove) Token: 0x06001657 RID: 5719 RVA: 0x00084AF4 File Offset: 0x00082EF4
		public static event UpdateHandler updated;

		// Token: 0x06001658 RID: 5720 RVA: 0x00084B28 File Offset: 0x00082F28
		protected virtual void triggerUpdated()
		{
			if (TimeUtility.updated != null)
			{
				TimeUtility.updated();
			}
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x00084B3E File Offset: 0x00082F3E
		protected virtual void Update()
		{
			this.triggerUpdated();
		}
	}
}
