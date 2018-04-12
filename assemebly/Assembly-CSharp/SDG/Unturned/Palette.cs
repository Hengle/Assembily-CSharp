using System;
using System.Globalization;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200047B RID: 1147
	public class Palette
	{
		// Token: 0x06001E66 RID: 7782 RVA: 0x000A64FB File Offset: 0x000A48FB
		public static string hex(Color32 color)
		{
			return "#" + color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
		}

		// Token: 0x06001E67 RID: 7783 RVA: 0x000A653C File Offset: 0x000A493C
		public static Color hex(string color)
		{
			uint num;
			if (!string.IsNullOrEmpty(color) && color.Length == 7 && uint.TryParse(color.Substring(1, color.Length - 1), NumberStyles.HexNumber, CultureInfo.CurrentCulture, out num))
			{
				uint num2 = num >> 16 & 255u;
				uint num3 = num >> 8 & 255u;
				uint num4 = num & 255u;
				return new Color32((byte)num2, (byte)num3, (byte)num4, byte.MaxValue);
			}
			return Color.white;
		}

		// Token: 0x040011E9 RID: 4585
		public static readonly Color SERVER = Color.green;

		// Token: 0x040011EA RID: 4586
		public static readonly Color ADMIN = Color.cyan;

		// Token: 0x040011EB RID: 4587
		public static readonly Color PRO = new Color(0.8235294f, 0.7490196f, 0.13333334f);

		// Token: 0x040011EC RID: 4588
		public static readonly Color COLOR_W = new Color(0.7058824f, 0.7058824f, 0.7058824f);

		// Token: 0x040011ED RID: 4589
		public static readonly Color COLOR_R = new Color(0.7490196f, 0.121568628f, 0.121568628f);

		// Token: 0x040011EE RID: 4590
		public static readonly Color COLOR_G = new Color(0.121568628f, 0.5294118f, 0.121568628f);

		// Token: 0x040011EF RID: 4591
		public static readonly Color COLOR_B = new Color(0.196078435f, 0.596078455f, 0.784313738f);

		// Token: 0x040011F0 RID: 4592
		public static readonly Color COLOR_O = new Color(0.670588255f, 0.5019608f, 0.09803922f);

		// Token: 0x040011F1 RID: 4593
		public static readonly Color COLOR_Y = new Color(0.8627451f, 0.7058824f, 0.07450981f);

		// Token: 0x040011F2 RID: 4594
		public static readonly Color COLOR_P = new Color(0.41568628f, 0.274509817f, 0.427450985f);

		// Token: 0x040011F3 RID: 4595
		public static readonly Color AMBIENT = new Color(0.7f, 0.7f, 0.7f);

		// Token: 0x040011F4 RID: 4596
		public static readonly Color MYTHICAL = new Color(0.980392158f, 0.196078435f, 0.09803922f);
	}
}
