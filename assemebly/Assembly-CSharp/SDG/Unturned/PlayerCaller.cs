using System;

namespace SDG.Unturned
{
	// Token: 0x020005FE RID: 1534
	public class PlayerCaller : SteamCaller
	{
		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06002AF8 RID: 11000 RVA: 0x0010986A File Offset: 0x00107C6A
		public Player player
		{
			get
			{
				return this._player;
			}
		}

		// Token: 0x06002AF9 RID: 11001 RVA: 0x00109872 File Offset: 0x00107C72
		private void Awake()
		{
			this._channel = base.GetComponent<SteamChannel>();
			this._player = base.GetComponent<Player>();
		}

		// Token: 0x04001BC9 RID: 7113
		protected Player _player;
	}
}
