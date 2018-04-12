using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200066D RID: 1645
	public class SteamCaller : MonoBehaviour
	{
		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06002FB8 RID: 12216 RVA: 0x000E7548 File Offset: 0x000E5948
		public SteamChannel channel
		{
			get
			{
				return this._channel;
			}
		}

		// Token: 0x06002FB9 RID: 12217 RVA: 0x000E7550 File Offset: 0x000E5950
		private void Awake()
		{
			this._channel = base.GetComponent<SteamChannel>();
		}

		// Token: 0x04001F96 RID: 8086
		protected SteamChannel _channel;
	}
}
