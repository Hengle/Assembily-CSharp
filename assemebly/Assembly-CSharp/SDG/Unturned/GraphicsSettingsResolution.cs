using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200069D RID: 1693
	public class GraphicsSettingsResolution
	{
		// Token: 0x060031AF RID: 12719 RVA: 0x00142984 File Offset: 0x00140D84
		public GraphicsSettingsResolution(Resolution resolution)
		{
			this.Width = resolution.width;
			this.Height = resolution.height;
		}

		// Token: 0x060031B0 RID: 12720 RVA: 0x001429A8 File Offset: 0x00140DA8
		public GraphicsSettingsResolution()
		{
			if (Screen.resolutions.Length > 0)
			{
				Resolution resolution = Screen.resolutions[Screen.resolutions.Length - 1];
				this.Width = resolution.width;
				this.Height = resolution.height;
			}
		}

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x060031B1 RID: 12721 RVA: 0x001429FB File Offset: 0x00140DFB
		// (set) Token: 0x060031B2 RID: 12722 RVA: 0x00142A03 File Offset: 0x00140E03
		public int Width { get; set; }

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x060031B3 RID: 12723 RVA: 0x00142A0C File Offset: 0x00140E0C
		// (set) Token: 0x060031B4 RID: 12724 RVA: 0x00142A14 File Offset: 0x00140E14
		public int Height { get; set; }
	}
}
