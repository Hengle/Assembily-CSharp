using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020005D4 RID: 1492
	public class MenuMode : MonoBehaviour
	{
		// Token: 0x06002A31 RID: 10801 RVA: 0x00106A8E File Offset: 0x00104E8E
		public void Awake()
		{
			this.desktop.SetActive(!Dedicator.isVR);
			this.virtualReality.SetActive(Dedicator.isVR);
		}

		// Token: 0x04001A2C RID: 6700
		public GameObject desktop;

		// Token: 0x04001A2D RID: 6701
		public GameObject virtualReality;
	}
}
