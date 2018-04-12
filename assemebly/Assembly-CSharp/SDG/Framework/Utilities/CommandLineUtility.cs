using System;
using System.Collections.Generic;
using SDG.Framework.Debug;

namespace SDG.Framework.Utilities
{
	// Token: 0x020002FF RID: 767
	public class CommandLineUtility
	{
		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06001608 RID: 5640 RVA: 0x000841A0 File Offset: 0x000825A0
		public static List<List<string>> commands
		{
			get
			{
				if (CommandLineUtility._commands == null)
				{
					CommandLineUtility._commands = new List<List<string>>();
					string[] array = Environment.CommandLine.Split(CommandLineUtility.DELIMITERS);
					if (array.Length > 1)
					{
						for (int i = 1; i < array.Length; i++)
						{
							if (!string.IsNullOrEmpty(array[i]))
							{
								string input = array[i].Trim();
								List<string> collection = TerminalUtility.splitArguments(input);
								CommandLineUtility.commands.Add(new List<string>(collection));
							}
						}
					}
				}
				return CommandLineUtility._commands;
			}
		}

		// Token: 0x04000C23 RID: 3107
		protected static readonly char[] DELIMITERS = new char[]
		{
			'-',
			'+'
		};

		// Token: 0x04000C24 RID: 3108
		protected static List<List<string>> _commands;
	}
}
