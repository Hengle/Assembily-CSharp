using System;
using SDG.Framework.Debug;

namespace SDG.Framework.Foliage
{
	// Token: 0x020001A5 RID: 421
	public class FoliageSettings
	{
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000C67 RID: 3175 RVA: 0x0005D2CD File Offset: 0x0005B6CD
		// (set) Token: 0x06000C68 RID: 3176 RVA: 0x0005D2D4 File Offset: 0x0005B6D4
		[TerminalCommandProperty("foliage.enabled", "whether to draw", false)]
		public static bool enabled
		{
			get
			{
				return FoliageSettings._enabled;
			}
			set
			{
				FoliageSettings._enabled = value;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000C69 RID: 3177 RVA: 0x0005D2DC File Offset: 0x0005B6DC
		// (set) Token: 0x06000C6A RID: 3178 RVA: 0x0005D2E3 File Offset: 0x0005B6E3
		[TerminalCommandProperty("foliage.enabled_focus", "whether to draw foliage at scope/binocular focus point", false)]
		public static bool drawFocus
		{
			get
			{
				return FoliageSettings._drawFocus;
			}
			set
			{
				FoliageSettings._drawFocus = value;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000C6B RID: 3179 RVA: 0x0005D2EB File Offset: 0x0005B6EB
		// (set) Token: 0x06000C6C RID: 3180 RVA: 0x0005D2F2 File Offset: 0x0005B6F2
		[TerminalCommandProperty("foliage.draw_distance", "how many tiles to render", 0)]
		public static int drawDistance
		{
			get
			{
				return FoliageSettings._drawDistance;
			}
			set
			{
				FoliageSettings._drawDistance = value;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000C6D RID: 3181 RVA: 0x0005D2FA File Offset: 0x0005B6FA
		// (set) Token: 0x06000C6E RID: 3182 RVA: 0x0005D301 File Offset: 0x0005B701
		[TerminalCommandProperty("foliage.draw_focus_distance", "how many tiles to render from focus point", 0)]
		public static int drawFocusDistance
		{
			get
			{
				return FoliageSettings._drawFocusDistance;
			}
			set
			{
				FoliageSettings._drawFocusDistance = value;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000C6F RID: 3183 RVA: 0x0005D309 File Offset: 0x0005B709
		// (set) Token: 0x06000C70 RID: 3184 RVA: 0x0005D310 File Offset: 0x0005B710
		[TerminalCommandProperty("foliage.instance_density", "multiplier for number of instanced meshes", 0)]
		public static float instanceDensity
		{
			get
			{
				return FoliageSettings._instanceDensity;
			}
			set
			{
				FoliageSettings._instanceDensity = value;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000C71 RID: 3185 RVA: 0x0005D318 File Offset: 0x0005B718
		// (set) Token: 0x06000C72 RID: 3186 RVA: 0x0005D31F File Offset: 0x0005B71F
		[TerminalCommandProperty("foliage.force_instancing_off", "disable instancing to test as if using an old GPU", false)]
		public static bool forceInstancingOff
		{
			get
			{
				return FoliageSettings._forceInstancingOff;
			}
			set
			{
				FoliageSettings._forceInstancingOff = value;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000C73 RID: 3187 RVA: 0x0005D327 File Offset: 0x0005B727
		// (set) Token: 0x06000C74 RID: 3188 RVA: 0x0005D32E File Offset: 0x0005B72E
		[TerminalCommandProperty("foliage.focus_distance", "how far to find focus point on ground", 0)]
		public static float focusDistance
		{
			get
			{
				return FoliageSettings._focusDistance;
			}
			set
			{
				FoliageSettings._focusDistance = value;
			}
		}

		// Token: 0x040008A1 RID: 2209
		private static bool _enabled;

		// Token: 0x040008A2 RID: 2210
		private static bool _drawFocus;

		// Token: 0x040008A3 RID: 2211
		private static int _drawDistance;

		// Token: 0x040008A4 RID: 2212
		private static int _drawFocusDistance;

		// Token: 0x040008A5 RID: 2213
		private static float _instanceDensity;

		// Token: 0x040008A6 RID: 2214
		private static bool _forceInstancingOff;

		// Token: 0x040008A7 RID: 2215
		private static float _focusDistance;
	}
}
