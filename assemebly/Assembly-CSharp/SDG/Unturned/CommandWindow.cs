using System;
using System.Runtime.CompilerServices;
using SDG.Framework.Debug;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000479 RID: 1145
	public class CommandWindow
	{
		// Token: 0x06001E4D RID: 7757 RVA: 0x000A5D54 File Offset: 0x000A4154
		public CommandWindow()
		{
			CommandWindow.input = new ConsoleInput();
			ConsoleInput input = CommandWindow.input;
			if (CommandWindow.<>f__mg$cache0 == null)
			{
				CommandWindow.<>f__mg$cache0 = new InputText(CommandWindow.onInputText);
			}
			input.onInputText = CommandWindow.<>f__mg$cache0;
			CommandWindow.output = new ConsoleOutput();
			if (CommandWindow.<>f__mg$cache1 == null)
			{
				CommandWindow.<>f__mg$cache1 = new Application.LogCallback(CommandWindow.onLogMessageReceived);
			}
			Application.logMessageReceived += CommandWindow.<>f__mg$cache1;
			Terminal.onMessageAdded += this.onMessageAdded;
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001E4E RID: 7758 RVA: 0x000A5DD5 File Offset: 0x000A41D5
		// (set) Token: 0x06001E4F RID: 7759 RVA: 0x000A5DDD File Offset: 0x000A41DD
		public string title
		{
			get
			{
				return this._title;
			}
			set
			{
				this._title = value;
				if (CommandWindow.output != null)
				{
					CommandWindow.output.title = this.title;
				}
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06001E50 RID: 7760 RVA: 0x000A5E00 File Offset: 0x000A4200
		// (set) Token: 0x06001E51 RID: 7761 RVA: 0x000A5E07 File Offset: 0x000A4207
		public static ConsoleInput input { get; private set; }

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06001E52 RID: 7762 RVA: 0x000A5E0F File Offset: 0x000A420F
		// (set) Token: 0x06001E53 RID: 7763 RVA: 0x000A5E16 File Offset: 0x000A4216
		public static ConsoleOutput output { get; private set; }

		// Token: 0x06001E54 RID: 7764 RVA: 0x000A5E20 File Offset: 0x000A4220
		public static void Log(object text, ConsoleColor color)
		{
			if (CommandWindow.onCommandWindowOutputted != null)
			{
				CommandWindow.onCommandWindowOutputted(text, color);
			}
			if (CommandWindow.output == null)
			{
				Debug.Log(text);
				return;
			}
			Console.ForegroundColor = color;
			if (Console.CursorLeft != 0)
			{
				CommandWindow.input.clearLine();
			}
			Console.WriteLine(text);
			CommandWindow.input.redrawInputLine();
		}

		// Token: 0x06001E55 RID: 7765 RVA: 0x000A5E7E File Offset: 0x000A427E
		public static void Log(object text)
		{
			CommandWindow.Log(text, ConsoleColor.White);
		}

		// Token: 0x06001E56 RID: 7766 RVA: 0x000A5E88 File Offset: 0x000A4288
		public static void LogError(object text)
		{
			CommandWindow.Log(text, ConsoleColor.Red);
		}

		// Token: 0x06001E57 RID: 7767 RVA: 0x000A5E92 File Offset: 0x000A4292
		public static void LogWarning(object text)
		{
			CommandWindow.Log(text, ConsoleColor.Yellow);
		}

		// Token: 0x06001E58 RID: 7768 RVA: 0x000A5E9C File Offset: 0x000A429C
		private static void onLogMessageReceived(string text, string stack, LogType type)
		{
			if (type == LogType.Exception)
			{
				CommandWindow.LogError(text + " - " + stack);
			}
		}

		// Token: 0x06001E59 RID: 7769 RVA: 0x000A5EB6 File Offset: 0x000A42B6
		private void onMessageAdded(TerminalLogMessage message, TerminalLogCategory category)
		{
			if (string.IsNullOrEmpty(message.internalText))
			{
				return;
			}
			CommandWindow.Log(category.internalName + ": " + message.internalText);
		}

		// Token: 0x06001E5A RID: 7770 RVA: 0x000A5EE8 File Offset: 0x000A42E8
		private static void onInputText(string command)
		{
			bool flag = true;
			if (CommandWindow.onCommandWindowInputted != null)
			{
				CommandWindow.onCommandWindowInputted(command, ref flag);
			}
			if (flag && !Commander.execute(CSteamID.Nil, command))
			{
				CommandWindow.LogError("?");
			}
		}

		// Token: 0x06001E5B RID: 7771 RVA: 0x000A5F2E File Offset: 0x000A432E
		public void update()
		{
			if (CommandWindow.input == null)
			{
				return;
			}
			CommandWindow.input.update();
		}

		// Token: 0x06001E5C RID: 7772 RVA: 0x000A5F45 File Offset: 0x000A4345
		public void shutdown()
		{
			if (CommandWindow.output == null)
			{
				return;
			}
			CommandWindow.output.shutdown();
		}

		// Token: 0x040011DD RID: 4573
		public static CommandWindowInputted onCommandWindowInputted;

		// Token: 0x040011DE RID: 4574
		public static CommandWindowOutputted onCommandWindowOutputted;

		// Token: 0x040011DF RID: 4575
		private string _title;

		// Token: 0x040011E0 RID: 4576
		public static bool shouldLogChat = true;

		// Token: 0x040011E1 RID: 4577
		public static bool shouldLogJoinLeave = true;

		// Token: 0x040011E2 RID: 4578
		public static bool shouldLogDeaths = true;

		// Token: 0x040011E3 RID: 4579
		public static bool shouldLogAnticheat;

		// Token: 0x040011E6 RID: 4582
		[CompilerGenerated]
		private static InputText <>f__mg$cache0;

		// Token: 0x040011E7 RID: 4583
		[CompilerGenerated]
		private static Application.LogCallback <>f__mg$cache1;
	}
}
