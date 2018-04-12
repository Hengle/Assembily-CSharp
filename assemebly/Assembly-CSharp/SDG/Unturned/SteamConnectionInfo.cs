using System;

namespace SDG.Unturned
{
	// Token: 0x02000673 RID: 1651
	public class SteamConnectionInfo
	{
		// Token: 0x06002FFF RID: 12287 RVA: 0x0013D84F File Offset: 0x0013BC4F
		public SteamConnectionInfo(uint newIP, ushort newPort, string newPassword)
		{
			this._ip = newIP;
			this._port = newPort;
			this._password = newPassword;
		}

		// Token: 0x06003000 RID: 12288 RVA: 0x0013D86C File Offset: 0x0013BC6C
		public SteamConnectionInfo(string newIP, ushort newPort, string newPassword)
		{
			this._ip = Parser.getUInt32FromIP(newIP);
			this._port = newPort;
			this._password = newPassword;
		}

		// Token: 0x06003001 RID: 12289 RVA: 0x0013D890 File Offset: 0x0013BC90
		public SteamConnectionInfo(string newIPPort, string newPassword)
		{
			string[] componentsFromSerial = Parser.getComponentsFromSerial(newIPPort, ':');
			this._ip = Parser.getUInt32FromIP(componentsFromSerial[0]);
			this._port = ushort.Parse(componentsFromSerial[1]);
			this._password = newPassword;
		}

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x06003002 RID: 12290 RVA: 0x0013D8CF File Offset: 0x0013BCCF
		public uint ip
		{
			get
			{
				return this._ip;
			}
		}

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x06003003 RID: 12291 RVA: 0x0013D8D7 File Offset: 0x0013BCD7
		public ushort port
		{
			get
			{
				return this._port;
			}
		}

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06003004 RID: 12292 RVA: 0x0013D8DF File Offset: 0x0013BCDF
		public string password
		{
			get
			{
				return this._password;
			}
		}

		// Token: 0x04001FA1 RID: 8097
		public uint _ip;

		// Token: 0x04001FA2 RID: 8098
		public ushort _port;

		// Token: 0x04001FA3 RID: 8099
		public string _password;
	}
}
