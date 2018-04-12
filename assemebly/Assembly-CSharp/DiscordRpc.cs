using System;
using System.Runtime.InteropServices;

// Token: 0x020000F7 RID: 247
public class DiscordRpc
{
	// Token: 0x06000800 RID: 2048
	[DllImport("discord-rpc", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Initialize")]
	public static extern void Initialize(string applicationId, ref DiscordRpc.EventHandlers handlers, bool autoRegister, string optionalSteamId);

	// Token: 0x06000801 RID: 2049
	[DllImport("discord-rpc", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Shutdown")]
	public static extern void Shutdown();

	// Token: 0x06000802 RID: 2050
	[DllImport("discord-rpc", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_RunCallbacks")]
	public static extern void RunCallbacks();

	// Token: 0x06000803 RID: 2051
	[DllImport("discord-rpc", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_UpdatePresence")]
	public static extern void UpdatePresence(ref DiscordRpc.RichPresence presence);

	// Token: 0x020000F8 RID: 248
	// (Invoke) Token: 0x06000805 RID: 2053
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void ReadyCallback();

	// Token: 0x020000F9 RID: 249
	// (Invoke) Token: 0x06000809 RID: 2057
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void DisconnectedCallback(int errorCode, string message);

	// Token: 0x020000FA RID: 250
	// (Invoke) Token: 0x0600080D RID: 2061
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void ErrorCallback(int errorCode, string message);

	// Token: 0x020000FB RID: 251
	// (Invoke) Token: 0x06000811 RID: 2065
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void JoinCallback(string secret);

	// Token: 0x020000FC RID: 252
	// (Invoke) Token: 0x06000815 RID: 2069
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void SpectateCallback(string secret);

	// Token: 0x020000FD RID: 253
	public struct EventHandlers
	{
		// Token: 0x040006A8 RID: 1704
		public DiscordRpc.ReadyCallback readyCallback;

		// Token: 0x040006A9 RID: 1705
		public DiscordRpc.DisconnectedCallback disconnectedCallback;

		// Token: 0x040006AA RID: 1706
		public DiscordRpc.ErrorCallback errorCallback;

		// Token: 0x040006AB RID: 1707
		public DiscordRpc.JoinCallback joinCallback;

		// Token: 0x040006AC RID: 1708
		public DiscordRpc.SpectateCallback spectateCallback;
	}

	// Token: 0x020000FE RID: 254
	[Serializable]
	public struct RichPresence
	{
		// Token: 0x040006AD RID: 1709
		public string state;

		// Token: 0x040006AE RID: 1710
		public string details;

		// Token: 0x040006AF RID: 1711
		public long startTimestamp;

		// Token: 0x040006B0 RID: 1712
		public long endTimestamp;

		// Token: 0x040006B1 RID: 1713
		public string largeImageKey;

		// Token: 0x040006B2 RID: 1714
		public string largeImageText;

		// Token: 0x040006B3 RID: 1715
		public string smallImageKey;

		// Token: 0x040006B4 RID: 1716
		public string smallImageText;

		// Token: 0x040006B5 RID: 1717
		public string partyId;

		// Token: 0x040006B6 RID: 1718
		public int partySize;

		// Token: 0x040006B7 RID: 1719
		public int partyMax;

		// Token: 0x040006B8 RID: 1720
		public string matchSecret;

		// Token: 0x040006B9 RID: 1721
		public string joinSecret;

		// Token: 0x040006BA RID: 1722
		public string spectateSecret;

		// Token: 0x040006BB RID: 1723
		public bool instance;
	}
}
