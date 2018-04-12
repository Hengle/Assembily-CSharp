using System;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000462 RID: 1122
	public class CommandQueue : Command
	{
		// Token: 0x06001E19 RID: 7705 RVA: 0x000A3E88 File Offset: 0x000A2288
		public CommandQueue(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("QueueCommandText");
			this._info = this.localization.format("QueueInfoText");
			this._help = this.localization.format("QueueHelpText");
		}

		// Token: 0x06001E1A RID: 7706 RVA: 0x000A3EE4 File Offset: 0x000A22E4
		protected override void execute(CSteamID executorID, string parameter)
		{
			if (!Dedicator.isDedicated)
			{
				return;
			}
			if (parameter == "a")
			{
				SteamPending steamPending = new SteamPending();
				if (Provider.pending.Count == 1)
				{
					steamPending.lastActive = Time.realtimeSinceStartup;
				}
				Provider.pending.Add(steamPending);
				CommandWindow.Log("add dummy");
				return;
			}
			if (parameter == "r")
			{
				Provider.reject(CSteamID.Nil, ESteamRejection.PING);
				CommandWindow.Log("rmv dummy");
				return;
			}
			if (parameter == "ad")
			{
				for (int i = 0; i < 12; i++)
				{
					Provider.pending.Add(new SteamPending(new SteamPlayerID(CSteamID.Nil, 0, "dummy", "dummy", "dummy", CSteamID.Nil), true, 0, 0, 0, Color.white, Color.white, Color.white, false, 0UL, 0UL, 0UL, 0UL, 0UL, 0UL, 0UL, new ulong[0], EPlayerSkillset.NONE, "english", CSteamID.Nil));
					Provider.accept(new SteamPlayerID(CSteamID.Nil, 1, "dummy", "dummy", "dummy", CSteamID.Nil), true, true, 0, 0, 0, Color.white, Color.white, Color.white, false, 0, 0, 0, 0, 0, 0, 0, new int[0], new string[0], new string[0], EPlayerSkillset.NONE, "english", CSteamID.Nil);
				}
			}
			else if (parameter == "rd")
			{
				for (int j = Provider.clients.Count - 1; j >= 0; j--)
				{
					Provider.kick(CSteamID.Nil, string.Empty);
				}
			}
			byte b;
			if (!byte.TryParse(parameter, out b))
			{
				CommandWindow.LogError(this.localization.format("InvalidNumberErrorText", new object[]
				{
					parameter
				}));
				return;
			}
			if (b > CommandQueue.MAX_NUMBER)
			{
				CommandWindow.LogError(this.localization.format("MaxNumberErrorText", new object[]
				{
					CommandQueue.MAX_NUMBER
				}));
				return;
			}
			Provider.queueSize = b;
			CommandWindow.Log(this.localization.format("QueueText", new object[]
			{
				b
			}));
		}

		// Token: 0x040011DA RID: 4570
		public static readonly byte MAX_NUMBER = 64;
	}
}
