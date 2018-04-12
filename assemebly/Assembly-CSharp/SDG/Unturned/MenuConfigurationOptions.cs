using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020005D3 RID: 1491
	public class MenuConfigurationOptions : MonoBehaviour
	{
		// Token: 0x06002A2D RID: 10797 RVA: 0x00106A37 File Offset: 0x00104E37
		public static void apply()
		{
			if (!MenuConfigurationOptions.hasPlayed || !OptionsSettings.music)
			{
				MenuConfigurationOptions.hasPlayed = true;
				if (MenuConfigurationOptions.music != null)
				{
					MenuConfigurationOptions.music.enabled = OptionsSettings.music;
				}
			}
		}

		// Token: 0x06002A2E RID: 10798 RVA: 0x00106A72 File Offset: 0x00104E72
		private void Start()
		{
			MenuConfigurationOptions.apply();
		}

		// Token: 0x06002A2F RID: 10799 RVA: 0x00106A79 File Offset: 0x00104E79
		private void Awake()
		{
			MenuConfigurationOptions.music = base.GetComponent<AudioSource>();
		}

		// Token: 0x04001A2A RID: 6698
		private static bool hasPlayed;

		// Token: 0x04001A2B RID: 6699
		private static AudioSource music;
	}
}
