using System;

namespace SDG.Unturned
{
	// Token: 0x020005D5 RID: 1493
	public class MenuSettings
	{
		// Token: 0x06002A33 RID: 10803 RVA: 0x00106ABB File Offset: 0x00104EBB
		public static void load()
		{
			FilterSettings.load();
			PlaySettings.load();
			GraphicsSettings.load();
			ControlsSettings.load();
			OptionsSettings.load();
			MenuSettings.hasLoaded = true;
		}

		// Token: 0x06002A34 RID: 10804 RVA: 0x00106ADC File Offset: 0x00104EDC
		public static void save()
		{
			if (!MenuSettings.hasLoaded)
			{
				return;
			}
			FilterSettings.save();
			PlaySettings.save();
			GraphicsSettings.save();
			ControlsSettings.save();
			OptionsSettings.save();
		}

		// Token: 0x04001A2E RID: 6702
		private static bool hasLoaded;
	}
}
