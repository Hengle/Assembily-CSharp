using System;
using System.Collections.Generic;

namespace SDG.Unturned
{
	// Token: 0x020007C9 RID: 1993
	public class Parser
	{
		// Token: 0x06003A3F RID: 14911 RVA: 0x001BE33C File Offset: 0x001BC73C
		public static bool trySplitStart(string serial, out string start, out string end)
		{
			start = string.Empty;
			end = string.Empty;
			int num = serial.IndexOf(' ');
			if (num == -1)
			{
				return false;
			}
			start = serial.Substring(0, num);
			end = serial.Substring(num + 1, serial.Length - num - 1);
			return true;
		}

		// Token: 0x06003A40 RID: 14912 RVA: 0x001BE38C File Offset: 0x001BC78C
		public static bool trySplitEnd(string serial, out string start, out string end)
		{
			start = string.Empty;
			end = string.Empty;
			int num = serial.LastIndexOf(' ');
			if (num == -1)
			{
				return false;
			}
			start = serial.Substring(0, num);
			end = serial.Substring(num + 1, serial.Length - num - 1);
			return true;
		}

		// Token: 0x06003A41 RID: 14913 RVA: 0x001BE3DC File Offset: 0x001BC7DC
		public static string[] getComponentsFromSerial(string serial, char delimiter)
		{
			List<string> list = new List<string>();
			int num;
			for (int i = 0; i < serial.Length; i = num + 1)
			{
				num = serial.IndexOf(delimiter, i);
				if (num == -1)
				{
					list.Add(serial.Substring(i, serial.Length - i));
					break;
				}
				list.Add(serial.Substring(i, num - i));
			}
			return list.ToArray();
		}

		// Token: 0x06003A42 RID: 14914 RVA: 0x001BE448 File Offset: 0x001BC848
		public static string getSerialFromComponents(char delimiter, params object[] components)
		{
			string text = string.Empty;
			for (int i = 0; i < components.Length; i++)
			{
				text += components[i].ToString();
				if (i < components.Length - 1)
				{
					text += delimiter;
				}
			}
			return text;
		}

		// Token: 0x06003A43 RID: 14915 RVA: 0x001BE498 File Offset: 0x001BC898
		public static bool checkIP(string ip)
		{
			int num = ip.IndexOf('.');
			if (num == -1)
			{
				return false;
			}
			int num2 = ip.IndexOf('.', num + 1);
			if (num2 == -1)
			{
				return false;
			}
			int num3 = ip.IndexOf('.', num2 + 1);
			if (num3 == -1)
			{
				return false;
			}
			int num4 = ip.IndexOf('.', num3 + 1);
			return num4 == -1;
		}

		// Token: 0x06003A44 RID: 14916 RVA: 0x001BE4F8 File Offset: 0x001BC8F8
		public static uint getUInt32FromIP(string ip)
		{
			string[] array = ip.Split(new char[]
			{
				'.'
			});
			return uint.Parse(array[0]) << 24 | uint.Parse(array[1]) << 16 | uint.Parse(array[2]) << 8 | uint.Parse(array[3]);
		}

		// Token: 0x06003A45 RID: 14917 RVA: 0x001BE544 File Offset: 0x001BC944
		public static string getIPFromUInt32(uint ip)
		{
			return string.Concat(new object[]
			{
				ip >> 24 & 255u,
				".",
				ip >> 16 & 255u,
				".",
				ip >> 8 & 255u,
				".",
				ip & 255u
			});
		}
	}
}
