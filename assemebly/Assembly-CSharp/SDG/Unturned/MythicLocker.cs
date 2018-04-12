using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020005F7 RID: 1527
	public class MythicLocker : MonoBehaviour
	{
		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06002A6D RID: 10861 RVA: 0x0010806E File Offset: 0x0010646E
		// (set) Token: 0x06002A6E RID: 10862 RVA: 0x00108076 File Offset: 0x00106476
		public bool isMythic
		{
			get
			{
				return this._isMythic;
			}
			set
			{
				this._isMythic = value;
				if (base.gameObject.activeInHierarchy)
				{
					this.system.gameObject.SetActive(this.isMythic);
				}
			}
		}

		// Token: 0x06002A6F RID: 10863 RVA: 0x001080A8 File Offset: 0x001064A8
		private void Update()
		{
			if (this.system == null)
			{
				return;
			}
			this.system.transform.position = base.transform.position;
			this.system.transform.rotation = base.transform.rotation;
		}

		// Token: 0x06002A70 RID: 10864 RVA: 0x00108100 File Offset: 0x00106500
		private void LateUpdate()
		{
			if (this.system == null)
			{
				return;
			}
			this.system.transform.position = base.transform.position;
			this.system.transform.rotation = base.transform.rotation;
		}

		// Token: 0x06002A71 RID: 10865 RVA: 0x00108155 File Offset: 0x00106555
		private void OnEnable()
		{
			if (this.system == null)
			{
				return;
			}
			this.system.gameObject.SetActive(this.isMythic);
		}

		// Token: 0x06002A72 RID: 10866 RVA: 0x0010817F File Offset: 0x0010657F
		private void OnDisable()
		{
			if (this.system == null)
			{
				return;
			}
			this.system.gameObject.SetActive(false);
		}

		// Token: 0x06002A73 RID: 10867 RVA: 0x001081A4 File Offset: 0x001065A4
		private void Start()
		{
			if (this.system == null)
			{
				return;
			}
			this.system.transform.parent = Level.effects;
		}

		// Token: 0x06002A74 RID: 10868 RVA: 0x001081CD File Offset: 0x001065CD
		private void OnDestroy()
		{
			if (this.system == null)
			{
				return;
			}
			UnityEngine.Object.Destroy(this.system.gameObject);
		}

		// Token: 0x04001B5E RID: 7006
		public MythicLockee system;

		// Token: 0x04001B5F RID: 7007
		private bool _isMythic = true;
	}
}
