using System;
using System.Collections.Generic;

namespace SDG.Framework.Debug
{
	// Token: 0x02000114 RID: 276
	public class Terminal
	{
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x0004CE2D File Offset: 0x0004B22D
		// (set) Token: 0x06000876 RID: 2166 RVA: 0x0004CE34 File Offset: 0x0004B234
		[TerminalCommandProperty("terminal.max_messages", "how many logs to hold per-category before deleting", 25)]
		public static uint maxMessages
		{
			get
			{
				return Terminal._maxMessages;
			}
			set
			{
				Terminal._maxMessages = value;
				TerminalUtility.printCommandPass("Set max_messages to: " + Terminal.maxMessages);
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000877 RID: 2167 RVA: 0x0004CE55 File Offset: 0x0004B255
		// (set) Token: 0x06000878 RID: 2168 RVA: 0x0004CE5C File Offset: 0x0004B25C
		[TerminalCommandProperty("terminal.pass_color", "hex color of success messages", "#00ff00")]
		public static string passColor
		{
			get
			{
				return Terminal._passColor;
			}
			set
			{
				Terminal._passColor = value;
				TerminalUtility.printCommandPass("Set pass_color to: " + Terminal.passColor);
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000879 RID: 2169 RVA: 0x0004CE78 File Offset: 0x0004B278
		// (set) Token: 0x0600087A RID: 2170 RVA: 0x0004CE7F File Offset: 0x0004B27F
		[TerminalCommandProperty("terminal.fail_color", "hex color of failure messages", "#ff0000")]
		public static string failColor
		{
			get
			{
				return Terminal._failColor;
			}
			set
			{
				Terminal._failColor = value;
				TerminalUtility.printCommandPass("Set fail_color to: " + Terminal.failColor);
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600087B RID: 2171 RVA: 0x0004CE9B File Offset: 0x0004B29B
		// (set) Token: 0x0600087C RID: 2172 RVA: 0x0004CEA2 File Offset: 0x0004B2A2
		[TerminalCommandProperty("terminal.highlight_color", "hex color of match highlight", "#ffff00")]
		public static string highlightColor
		{
			get
			{
				return Terminal._highlightColor;
			}
			set
			{
				Terminal._highlightColor = value;
				TerminalUtility.printCommandPass("Set highlight_color to: " + Terminal.highlightColor);
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x0600087D RID: 2173 RVA: 0x0004CEC0 File Offset: 0x0004B2C0
		// (remove) Token: 0x0600087E RID: 2174 RVA: 0x0004CEF4 File Offset: 0x0004B2F4
		public static event TerminalCategoryVisibilityChanged onCategoryVisibilityChanged;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x0600087F RID: 2175 RVA: 0x0004CF28 File Offset: 0x0004B328
		// (remove) Token: 0x06000880 RID: 2176 RVA: 0x0004CF5C File Offset: 0x0004B35C
		public static event TerminalCategoriesCleared onCategoriesCleared;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000881 RID: 2177 RVA: 0x0004CF90 File Offset: 0x0004B390
		// (remove) Token: 0x06000882 RID: 2178 RVA: 0x0004CFC4 File Offset: 0x0004B3C4
		public static event TerminalCategoryCleared onCategoryCleared;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000883 RID: 2179 RVA: 0x0004CFF8 File Offset: 0x0004B3F8
		// (remove) Token: 0x06000884 RID: 2180 RVA: 0x0004D02C File Offset: 0x0004B42C
		public static event TerminalCategoryAdded onCategoryAdded;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000885 RID: 2181 RVA: 0x0004D060 File Offset: 0x0004B460
		// (remove) Token: 0x06000886 RID: 2182 RVA: 0x0004D094 File Offset: 0x0004B494
		public static event TerminalMessageRemoved onMessageRemoved;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000887 RID: 2183 RVA: 0x0004D0C8 File Offset: 0x0004B4C8
		// (remove) Token: 0x06000888 RID: 2184 RVA: 0x0004D0FC File Offset: 0x0004B4FC
		public static event TerminalMessageAdded onMessageAdded;

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x0004D130 File Offset: 0x0004B530
		// (set) Token: 0x0600088A RID: 2186 RVA: 0x0004D137 File Offset: 0x0004B537
		public static TerminalParameterParserRegistry parserRegistry { get; protected set; }

		// Token: 0x0600088B RID: 2187 RVA: 0x0004D13F File Offset: 0x0004B53F
		public static IList<TerminalCommand> getCommands()
		{
			return Terminal.commands.Values;
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x0004D14B File Offset: 0x0004B54B
		public static IList<TerminalLogCategory> getLogs()
		{
			return Terminal.logs.Values;
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0004D158 File Offset: 0x0004B558
		public static void toggleCategoryVisibility(string internalCategory, bool newIsVisible)
		{
			TerminalLogCategory terminalLogCategory;
			if (!Terminal.logs.TryGetValue(internalCategory, out terminalLogCategory))
			{
				return;
			}
			terminalLogCategory.isVisible = newIsVisible;
			if (Terminal.onCategoryVisibilityChanged != null)
			{
				Terminal.onCategoryVisibilityChanged(terminalLogCategory);
			}
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0004D194 File Offset: 0x0004B594
		public static void clearAll()
		{
			Terminal.logs.Clear();
			if (Terminal.onCategoriesCleared != null)
			{
				Terminal.onCategoriesCleared();
			}
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0004D1B4 File Offset: 0x0004B5B4
		public static void clearCategory(string internalCategory)
		{
			TerminalLogCategory terminalLogCategory;
			if (!Terminal.logs.TryGetValue(internalCategory, out terminalLogCategory))
			{
				return;
			}
			terminalLogCategory.messages.Clear();
			if (Terminal.onCategoryCleared != null)
			{
				Terminal.onCategoryCleared(terminalLogCategory);
			}
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0004D1F4 File Offset: 0x0004B5F4
		public static void registerCommand(TerminalCommand command)
		{
			if (command == null || command.method == null || command.parameters == null)
			{
				return;
			}
			Terminal.commands.Add(command.method.command, command);
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0004D22C File Offset: 0x0004B62C
		public static void print(string internalMessage, string displayMessage, string internalCategory, string displayCategory, bool defaultIsVisible = true)
		{
			if (displayMessage == null)
			{
				displayMessage = internalMessage;
			}
			if (displayCategory == null)
			{
				displayCategory = internalCategory;
			}
			TerminalLogCategory terminalLogCategory;
			if (!Terminal.logs.TryGetValue(internalCategory, out terminalLogCategory))
			{
				terminalLogCategory = new TerminalLogCategory(internalCategory, displayCategory, defaultIsVisible);
				Terminal.logs.Add(internalCategory, terminalLogCategory);
				if (Terminal.onCategoryAdded != null)
				{
					Terminal.onCategoryAdded(terminalLogCategory);
				}
			}
			TerminalLogMessage terminalLogMessage = default(TerminalLogMessage);
			terminalLogMessage.category = terminalLogCategory;
			terminalLogMessage.timestamp = DateTime.Now.Ticks;
			terminalLogMessage.internalText = internalMessage;
			terminalLogMessage.displayText = displayMessage;
			if ((long)terminalLogCategory.messages.Count >= (long)((ulong)Terminal.maxMessages))
			{
				TerminalLogMessage message = terminalLogCategory.messages[0];
				terminalLogCategory.messages.RemoveAtFast(0);
				if (Terminal.onMessageRemoved != null)
				{
					Terminal.onMessageRemoved(message, terminalLogCategory);
				}
			}
			terminalLogCategory.messages.Add(terminalLogMessage);
			if (Terminal.onMessageAdded != null)
			{
				Terminal.onMessageAdded(terminalLogMessage, terminalLogCategory);
			}
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0004D324 File Offset: 0x0004B724
		public static void initialize()
		{
			if (Terminal.parserRegistry == null)
			{
				Terminal.parserRegistry = new TerminalParameterParserRegistry();
			}
		}

		// Token: 0x040006CB RID: 1739
		private static uint _maxMessages = 25u;

		// Token: 0x040006CC RID: 1740
		private static string _passColor = "#00ff00";

		// Token: 0x040006CD RID: 1741
		private static string _failColor = "#ff0000";

		// Token: 0x040006CE RID: 1742
		private static string _highlightColor = "#ffff00";

		// Token: 0x040006D6 RID: 1750
		private static SortedList<string, TerminalCommand> commands = new SortedList<string, TerminalCommand>();

		// Token: 0x040006D7 RID: 1751
		private static SortedList<string, TerminalLogCategory> logs = new SortedList<string, TerminalLogCategory>();
	}
}
