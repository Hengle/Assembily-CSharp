using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020005F6 RID: 1526
	public class MythicLockee : MonoBehaviour
	{
		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06002A6A RID: 10858 RVA: 0x00108044 File Offset: 0x00106444
		// (set) Token: 0x06002A6B RID: 10859 RVA: 0x00108051 File Offset: 0x00106451
		public bool isMythic
		{
			get
			{
				return this.locker.isMythic;
			}
			set
			{
				this.locker.isMythic = value;
			}
		}

		// Token: 0x04001B5D RID: 7005
		public MythicLocker locker;
	}
}
