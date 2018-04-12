using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004C2 RID: 1218
	public class Interactable2 : MonoBehaviour
	{
		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x0600208F RID: 8335 RVA: 0x000B2F36 File Offset: 0x000B1336
		public bool hasOwnership
		{
			get
			{
				return OwnershipTool.checkToggle(this.owner, this.group);
			}
		}

		// Token: 0x06002090 RID: 8336 RVA: 0x000B2F49 File Offset: 0x000B1349
		public virtual bool checkHint(out EPlayerMessage message, out float data)
		{
			message = EPlayerMessage.NONE;
			data = 0f;
			return false;
		}

		// Token: 0x06002091 RID: 8337 RVA: 0x000B2F56 File Offset: 0x000B1356
		public virtual void use()
		{
		}

		// Token: 0x04001368 RID: 4968
		public ulong owner;

		// Token: 0x04001369 RID: 4969
		public ulong group;
	}
}
