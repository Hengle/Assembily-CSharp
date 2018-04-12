using System;
using System.IO;
using SDG.Framework.Debug;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020005B2 RID: 1458
	public class Logs : MonoBehaviour
	{
		// Token: 0x060028C6 RID: 10438 RVA: 0x000F7CD8 File Offset: 0x000F60D8
		private void onMessageAdded(TerminalLogMessage message, TerminalLogCategory category)
		{
			if (this.stream == null || this.writer == null)
			{
				return;
			}
			if (string.IsNullOrEmpty(message.internalText))
			{
				return;
			}
			DateTime dateTime = new DateTime(message.timestamp);
			string text = dateTime.ToString("HH:mm:ss - dd/MM/yyyy");
			this.writer.WriteLine(string.Concat(new string[]
			{
				"[",
				text,
				"] ",
				category.internalName,
				": ",
				message.internalText
			}));
		}

		// Token: 0x060028C7 RID: 10439 RVA: 0x000F7D70 File Offset: 0x000F6170
		private void onLogMessageReceived(string text, string stack, LogType type)
		{
			if (this.stream == null || this.writer == null)
			{
				return;
			}
			switch (type)
			{
			case LogType.Error:
			case LogType.Assert:
			case LogType.Exception:
				if (!Dedicator.isDedicated)
				{
					Terminal.print(null, text, "Exceptions", "<color=" + Terminal.failColor + ">Exceptions</color>", false);
				}
				this.writer.WriteLine();
				this.writer.WriteLine(text);
				this.writer.WriteLine(stack);
				return;
			case LogType.Log:
				Terminal.print(text, null, "General", "General", true);
				return;
			}
		}

		// Token: 0x060028C8 RID: 10440 RVA: 0x000F7E20 File Offset: 0x000F6220
		public void awake()
		{
			if (string.IsNullOrEmpty(this.path))
			{
				this.path = ReadWrite.PATH;
				if (Dedicator.isDedicated)
				{
					this.path = this.path + "/Logs/Server_" + Dedicator.serverID + ".log";
				}
				else
				{
					this.path += "/Logs/Client.log";
				}
				if (!Directory.Exists(Path.GetDirectoryName(this.path)))
				{
					Directory.CreateDirectory(Path.GetDirectoryName(this.path));
				}
				if (File.Exists(this.path))
				{
					string text = ReadWrite.PATH;
					if (Dedicator.isDedicated)
					{
						text = text + "/Logs/Server_" + Dedicator.serverID + "_Prev.log";
					}
					else
					{
						text += "/Logs/Client_Prev.log";
					}
					File.Copy(this.path, text, true);
				}
			}
			if (this.stream == null)
			{
				this.stream = new FileStream(this.path, FileMode.Create, FileAccess.Write, FileShare.Write);
			}
			if (this.writer == null)
			{
				this.writer = new StreamWriter(this.stream);
				this.writer.AutoFlush = true;
			}
			Terminal.onMessageAdded += this.onMessageAdded;
			Application.logMessageReceived += this.onLogMessageReceived;
		}

		// Token: 0x060028C9 RID: 10441 RVA: 0x000F7F70 File Offset: 0x000F6370
		private void OnDestroy()
		{
			if (this.writer != null)
			{
				this.writer.Flush();
				this.writer.Close();
				this.writer.Dispose();
				this.writer = null;
			}
			if (this.stream != null)
			{
				this.stream.Close();
				this.stream.Dispose();
				this.stream = null;
			}
		}

		// Token: 0x04001982 RID: 6530
		private string path;

		// Token: 0x04001983 RID: 6531
		private FileStream stream;

		// Token: 0x04001984 RID: 6532
		private StreamWriter writer;
	}
}
