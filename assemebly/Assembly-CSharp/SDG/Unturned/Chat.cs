using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200058D RID: 1421
	public class Chat
	{
		// Token: 0x060027A5 RID: 10149 RVA: 0x000F0A96 File Offset: 0x000EEE96
		public Chat(SteamPlayer newPlayer, EChatMode newMode, Color newColor, bool newRich, string newSpeaker, string newText)
		{
			this.player = newPlayer;
			this.mode = newMode;
			this.color = newColor;
			this.isRich = newRich;
			this.speaker = newSpeaker;
			this.text = newText;
		}

		// Token: 0x040018CB RID: 6347
		public SteamPlayer player;

		// Token: 0x040018CC RID: 6348
		public EChatMode mode;

		// Token: 0x040018CD RID: 6349
		public Color color;

		// Token: 0x040018CE RID: 6350
		public bool isRich;

		// Token: 0x040018CF RID: 6351
		public string speaker;

		// Token: 0x040018D0 RID: 6352
		public string text;
	}
}
