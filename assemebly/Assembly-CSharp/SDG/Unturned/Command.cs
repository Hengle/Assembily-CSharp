using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x02000438 RID: 1080
	public class Command : IComparable<Command>
	{
		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001DBE RID: 7614 RVA: 0x000A095C File Offset: 0x0009ED5C
		public string command
		{
			get
			{
				return this._command;
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001DBF RID: 7615 RVA: 0x000A0964 File Offset: 0x0009ED64
		public string info
		{
			get
			{
				return this._info;
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06001DC0 RID: 7616 RVA: 0x000A096C File Offset: 0x0009ED6C
		public string help
		{
			get
			{
				return this._help;
			}
		}

		// Token: 0x06001DC1 RID: 7617 RVA: 0x000A0974 File Offset: 0x0009ED74
		protected virtual void execute(CSteamID executorID, string parameter)
		{
		}

		// Token: 0x06001DC2 RID: 7618 RVA: 0x000A0976 File Offset: 0x0009ED76
		public virtual bool check(CSteamID executorID, string method, string parameter)
		{
			if (method.ToLower() == this.command.ToLower())
			{
				this.execute(executorID, parameter);
				return true;
			}
			return false;
		}

		// Token: 0x06001DC3 RID: 7619 RVA: 0x000A099E File Offset: 0x0009ED9E
		public int CompareTo(Command other)
		{
			return this.command.CompareTo(other.command);
		}

		// Token: 0x040011D0 RID: 4560
		protected Local localization;

		// Token: 0x040011D1 RID: 4561
		protected string _command;

		// Token: 0x040011D2 RID: 4562
		protected string _info;

		// Token: 0x040011D3 RID: 4563
		protected string _help;
	}
}
