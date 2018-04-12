using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000650 RID: 1616
	public class Dedicator : MonoBehaviour
	{
		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06002EBA RID: 11962 RVA: 0x00131D0A File Offset: 0x0013010A
		// (set) Token: 0x06002EBB RID: 11963 RVA: 0x00131D11 File Offset: 0x00130111
		public static CommandWindow commandWindow { get; protected set; }

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06002EBC RID: 11964 RVA: 0x00131D19 File Offset: 0x00130119
		public static bool isDedicated
		{
			get
			{
				return Dedicator._isDedicated;
			}
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06002EBD RID: 11965 RVA: 0x00131D20 File Offset: 0x00130120
		public static bool hasBattlEye
		{
			get
			{
				return Dedicator._hasBattlEye;
			}
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06002EBE RID: 11966 RVA: 0x00131D27 File Offset: 0x00130127
		public static bool isVR
		{
			get
			{
				return Dedicator._isVR;
			}
		}

		// Token: 0x06002EBF RID: 11967 RVA: 0x00131D2E File Offset: 0x0013012E
		private void Update()
		{
			if (Dedicator.isDedicated && Dedicator.commandWindow != null)
			{
				Dedicator.commandWindow.update();
			}
		}

		// Token: 0x06002EC0 RID: 11968 RVA: 0x00131D50 File Offset: 0x00130150
		public void awake()
		{
			Dedicator._isDedicated = CommandLine.tryGetServer(out Dedicator.serverVisibility, out Dedicator.serverID);
			Dedicator._hasBattlEye = (Environment.CommandLine.IndexOf("-BattlEye", StringComparison.OrdinalIgnoreCase) != -1);
			Dedicator._isVR = false;
			if (Dedicator.isDedicated)
			{
				Dedicator.commandWindow = new CommandWindow();
				Application.targetFrameRate = 50;
				AudioListener.volume = 0f;
			}
		}

		// Token: 0x06002EC1 RID: 11969 RVA: 0x00131DB7 File Offset: 0x001301B7
		private void OnApplicationQuit()
		{
			if (Dedicator.isDedicated && Dedicator.commandWindow != null)
			{
				Dedicator.commandWindow.shutdown();
			}
		}

		// Token: 0x04001E44 RID: 7748
		public static ESteamServerVisibility serverVisibility;

		// Token: 0x04001E45 RID: 7749
		public static string serverID;

		// Token: 0x04001E46 RID: 7750
		private static bool _isDedicated;

		// Token: 0x04001E47 RID: 7751
		private static bool _hasBattlEye;

		// Token: 0x04001E48 RID: 7752
		private static bool _isVR;
	}
}
