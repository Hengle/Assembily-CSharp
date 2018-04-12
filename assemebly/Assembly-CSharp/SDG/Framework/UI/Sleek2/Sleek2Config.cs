using System;
using SDG.Framework.Debug;
using SDG.Framework.Utilities;
using UnityEngine;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002B5 RID: 693
	public class Sleek2Config
	{
		// Token: 0x17000298 RID: 664
		// (get) Token: 0x0600141E RID: 5150 RVA: 0x000808C4 File Offset: 0x0007ECC4
		// (set) Token: 0x0600141F RID: 5151 RVA: 0x000808CB File Offset: 0x0007ECCB
		[TerminalCommandProperty("sleek2.header_font_size", "big text font size", 30)]
		public static uint headerFontSize
		{
			get
			{
				return Sleek2Config._headerFontSize;
			}
			set
			{
				Sleek2Config._headerFontSize = value;
				TerminalUtility.printCommandPass("Set header_font_size to: " + Sleek2Config.headerFontSize);
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06001420 RID: 5152 RVA: 0x000808EC File Offset: 0x0007ECEC
		// (set) Token: 0x06001421 RID: 5153 RVA: 0x000808F3 File Offset: 0x0007ECF3
		[TerminalCommandProperty("sleek2.header_height", "height of big boxes", 40)]
		public static uint headerHeight
		{
			get
			{
				return Sleek2Config._headerHeight;
			}
			set
			{
				Sleek2Config._headerHeight = value;
				TerminalUtility.printCommandPass("Set header_height to: " + Sleek2Config.headerHeight);
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06001422 RID: 5154 RVA: 0x00080914 File Offset: 0x0007ED14
		// (set) Token: 0x06001423 RID: 5155 RVA: 0x0008091B File Offset: 0x0007ED1B
		[TerminalCommandProperty("sleek2.body_font_size", "small text font size", 16)]
		public static int bodyFontSize
		{
			get
			{
				return Sleek2Config._bodyFontSize;
			}
			set
			{
				Sleek2Config._bodyFontSize = value;
				TerminalUtility.printCommandPass("Set body_font_size to: " + Sleek2Config.bodyFontSize);
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06001424 RID: 5156 RVA: 0x0008093C File Offset: 0x0007ED3C
		// (set) Token: 0x06001425 RID: 5157 RVA: 0x00080943 File Offset: 0x0007ED43
		[TerminalCommandProperty("sleek2.body_height", "height of small boxes", 20)]
		public static int bodyHeight
		{
			get
			{
				return Sleek2Config._bodyHeight;
			}
			set
			{
				Sleek2Config._bodyHeight = value;
				TerminalUtility.printCommandPass("Set body_height to: " + Sleek2Config.bodyHeight);
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06001426 RID: 5158 RVA: 0x00080964 File Offset: 0x0007ED64
		// (set) Token: 0x06001427 RID: 5159 RVA: 0x0008096B File Offset: 0x0007ED6B
		[TerminalCommandProperty("sleek2.tab_width", "width of tabs", 150)]
		public static int tabWidth
		{
			get
			{
				return Sleek2Config._tabWidth;
			}
			set
			{
				Sleek2Config._tabWidth = value;
				TerminalUtility.printCommandPass("Set tab_width to: " + Sleek2Config.tabWidth);
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06001428 RID: 5160 RVA: 0x0008098C File Offset: 0x0007ED8C
		// (set) Token: 0x06001429 RID: 5161 RVA: 0x00080993 File Offset: 0x0007ED93
		[TerminalCommandProperty("sleek2.light_text_color", "hex color of white text", "#e1e1e1")]
		public static string lightTextColorHex
		{
			get
			{
				return Sleek2Config._lightTextColorHex;
			}
			set
			{
				Sleek2Config._lightTextColorHex = value;
				PaletteUtility.tryParse(Sleek2Config.lightTextColorHex, out Sleek2Config.lightTextColor);
				TerminalUtility.printCommandPass("Set light_text_color to: " + Sleek2Config.lightTextColorHex);
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x0600142A RID: 5162 RVA: 0x000809BF File Offset: 0x0007EDBF
		// (set) Token: 0x0600142B RID: 5163 RVA: 0x000809C6 File Offset: 0x0007EDC6
		[TerminalCommandProperty("sleek2.dark_text_color", "hex color of black text", "#191919")]
		public static string darkTextColorHex
		{
			get
			{
				return Sleek2Config._darkTextColorHex;
			}
			set
			{
				Sleek2Config._darkTextColorHex = value;
				PaletteUtility.tryParse(Sleek2Config.darkTextColorHex, out Sleek2Config.darkTextColor);
				TerminalUtility.printCommandPass("Set dark_text_color to: " + Sleek2Config.darkTextColorHex);
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x0600142C RID: 5164 RVA: 0x000809F2 File Offset: 0x0007EDF2
		// (set) Token: 0x0600142D RID: 5165 RVA: 0x000809F9 File Offset: 0x0007EDF9
		[TerminalCommandProperty("sleek2.dock_color", "hex color of docking points", "#f3c50f")]
		public static string dockColorHex
		{
			get
			{
				return Sleek2Config._dockColorHex;
			}
			set
			{
				Sleek2Config._dockColorHex = value;
				PaletteUtility.tryParse(Sleek2Config.dockColorHex, out Sleek2Config.dockColor);
				TerminalUtility.printCommandPass("Set dock_color to: " + Sleek2Config.dockColorHex);
			}
		}

		// Token: 0x04000BA5 RID: 2981
		private static uint _headerFontSize = 30u;

		// Token: 0x04000BA6 RID: 2982
		private static uint _headerHeight = 40u;

		// Token: 0x04000BA7 RID: 2983
		private static int _bodyFontSize = 16;

		// Token: 0x04000BA8 RID: 2984
		private static int _bodyHeight = 20;

		// Token: 0x04000BA9 RID: 2985
		private static int _tabWidth = 150;

		// Token: 0x04000BAA RID: 2986
		public static Color lightTextColor = new Color32(225, 225, 225, byte.MaxValue);

		// Token: 0x04000BAB RID: 2987
		private static string _lightTextColorHex = "#e1e1e1";

		// Token: 0x04000BAC RID: 2988
		public static Color darkTextColor = new Color32(25, 25, 25, byte.MaxValue);

		// Token: 0x04000BAD RID: 2989
		private static string _darkTextColorHex = "#191919";

		// Token: 0x04000BAE RID: 2990
		public static Color dockColor = new Color32(243, 197, 15, byte.MaxValue);

		// Token: 0x04000BAF RID: 2991
		private static string _dockColorHex = "#f3c50f";
	}
}
