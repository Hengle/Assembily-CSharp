using System;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000F6 RID: 246
	[ExecuteInEditMode]
	public class UnityReferenceHelper : MonoBehaviour
	{
		// Token: 0x060007FC RID: 2044 RVA: 0x0004C9EC File Offset: 0x0004ADEC
		public string GetGUID()
		{
			return this.guid;
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0004C9F4 File Offset: 0x0004ADF4
		public void Awake()
		{
			this.Reset();
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0004C9FC File Offset: 0x0004ADFC
		public void Reset()
		{
			if (this.guid == null || this.guid == string.Empty)
			{
				this.guid = Pathfinding.Util.Guid.NewGuid().ToString();
				Debug.Log("Created new GUID - " + this.guid);
			}
			else
			{
				foreach (UnityReferenceHelper unityReferenceHelper in UnityEngine.Object.FindObjectsOfType(typeof(UnityReferenceHelper)) as UnityReferenceHelper[])
				{
					if (unityReferenceHelper != this && this.guid == unityReferenceHelper.guid)
					{
						this.guid = Pathfinding.Util.Guid.NewGuid().ToString();
						Debug.Log("Created new GUID - " + this.guid);
						return;
					}
				}
			}
		}

		// Token: 0x040006A7 RID: 1703
		[HideInInspector]
		[SerializeField]
		private string guid;
	}
}
