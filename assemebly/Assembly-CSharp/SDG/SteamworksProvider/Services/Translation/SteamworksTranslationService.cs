using System;
using SDG.Provider.Services;
using SDG.Provider.Services.Translation;
using Steamworks;

namespace SDG.SteamworksProvider.Services.Translation
{
	// Token: 0x0200037B RID: 891
	public class SteamworksTranslationService : Service, ITranslationService, IService
	{
		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06001871 RID: 6257 RVA: 0x000897BD File Offset: 0x00087BBD
		// (set) Token: 0x06001872 RID: 6258 RVA: 0x000897C5 File Offset: 0x00087BC5
		public string language { get; protected set; }

		// Token: 0x06001873 RID: 6259 RVA: 0x000897CE File Offset: 0x00087BCE
		public override void initialize()
		{
			this.language = SteamUtils.GetSteamUILanguage();
		}
	}
}
