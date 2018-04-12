using System;

namespace SDG.Unturned
{
	// Token: 0x020005A5 RID: 1445
	public class ArenaPlayer
	{
		// Token: 0x06002856 RID: 10326 RVA: 0x000F4F3C File Offset: 0x000F333C
		public ArenaPlayer(SteamPlayer newSteamPlayer)
		{
			this._steamPlayer = newSteamPlayer;
			this._hasDied = false;
			PlayerLife life = this.steamPlayer.player.life;
			life.onLifeUpdated = (LifeUpdated)Delegate.Combine(life.onLifeUpdated, new LifeUpdated(this.onLifeUpdated));
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06002857 RID: 10327 RVA: 0x000F4F8E File Offset: 0x000F338E
		public SteamPlayer steamPlayer
		{
			get
			{
				return this._steamPlayer;
			}
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06002858 RID: 10328 RVA: 0x000F4F96 File Offset: 0x000F3396
		public bool hasDied
		{
			get
			{
				return this._hasDied;
			}
		}

		// Token: 0x06002859 RID: 10329 RVA: 0x000F4F9E File Offset: 0x000F339E
		private void onLifeUpdated(bool isDead)
		{
			if (isDead)
			{
				this._hasDied = true;
			}
		}

		// Token: 0x0400193B RID: 6459
		private SteamPlayer _steamPlayer;

		// Token: 0x0400193C RID: 6460
		private bool _hasDied;

		// Token: 0x0400193D RID: 6461
		public float lastAreaDamage;
	}
}
