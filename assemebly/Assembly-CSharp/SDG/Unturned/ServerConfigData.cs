using System;

namespace SDG.Unturned
{
	// Token: 0x020006A1 RID: 1697
	public class ServerConfigData
	{
		// Token: 0x060031C0 RID: 12736 RVA: 0x0014324C File Offset: 0x0014164C
		public ServerConfigData()
		{
			this.VAC_Secure = true;
			this.BattlEye_Secure = true;
			this.Max_Ping_Milliseconds = 750u;
			this.Timeout_Queue_Seconds = 15f;
			this.Timeout_Game_Seconds = 30f;
			this.Max_Packets_Per_Second = 50f;
		}

		// Token: 0x04002111 RID: 8465
		public bool VAC_Secure;

		// Token: 0x04002112 RID: 8466
		public bool BattlEye_Secure;

		// Token: 0x04002113 RID: 8467
		public uint Max_Ping_Milliseconds;

		// Token: 0x04002114 RID: 8468
		public float Timeout_Queue_Seconds;

		// Token: 0x04002115 RID: 8469
		public float Timeout_Game_Seconds;

		// Token: 0x04002116 RID: 8470
		public float Max_Packets_Per_Second;
	}
}
