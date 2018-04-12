using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using SDG.Framework.Translations;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x02000395 RID: 917
	public static class DedicatedUGC
	{
		// Token: 0x170003DC RID: 988
		// (get) Token: 0x060019B1 RID: 6577 RVA: 0x00090ADC File Offset: 0x0008EEDC
		// (set) Token: 0x060019B2 RID: 6578 RVA: 0x00090AE3 File Offset: 0x0008EEE3
		public static List<SteamContent> ugc { get; private set; }

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x060019B3 RID: 6579 RVA: 0x00090AEB File Offset: 0x0008EEEB
		// (set) Token: 0x060019B4 RID: 6580 RVA: 0x00090AF2 File Offset: 0x0008EEF2
		public static List<ulong> installing { get; private set; }

		// Token: 0x1400006E RID: 110
		// (add) Token: 0x060019B5 RID: 6581 RVA: 0x00090AFC File Offset: 0x0008EEFC
		// (remove) Token: 0x060019B6 RID: 6582 RVA: 0x00090B30 File Offset: 0x0008EF30
		public static event DedicatedUGCInstalledHandler installed;

		// Token: 0x060019B7 RID: 6583 RVA: 0x00090B64 File Offset: 0x0008EF64
		public static void registerItemInstallation(ulong id)
		{
			DedicatedUGC.installing.Add(id);
			Provider.serverWorkshopFileIDs.Add(id);
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x00090B7C File Offset: 0x0008EF7C
		public static void installNextItem()
		{
			if (DedicatedUGC.installing.Count == 0)
			{
				DedicatedUGC.triggerInstalled();
			}
			else
			{
				CommandWindow.Log("Downloading workshop item: " + DedicatedUGC.installing[0]);
				if (!SteamGameServerUGC.DownloadItem((PublishedFileId_t)DedicatedUGC.installing[0], true))
				{
					DedicatedUGC.installing.RemoveAt(0);
					CommandWindow.Log("Unable to download item!");
					DedicatedUGC.installNextItem();
				}
			}
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x00090BF8 File Offset: 0x0008EFF8
		private static void onItemDownloaded(DownloadItemResult_t callback)
		{
			if (DedicatedUGC.installing == null || DedicatedUGC.installing.Count == 0)
			{
				return;
			}
			if (!DedicatedUGC.installing.Remove(callback.m_nPublishedFileId.m_PublishedFileId))
			{
				return;
			}
			if (callback.m_eResult == EResult.k_EResultOK)
			{
				CommandWindow.Log("Successfully downloaded workshop item: " + callback.m_nPublishedFileId.m_PublishedFileId);
				ulong num;
				string text;
				uint num2;
				if (SteamGameServerUGC.GetItemInstallInfo(callback.m_nPublishedFileId, out num, out text, 1024u, out num2) && ReadWrite.folderExists(text, false))
				{
					if (WorkshopTool.checkMapMeta(text, false))
					{
						DedicatedUGC.ugc.Add(new SteamContent(callback.m_nPublishedFileId, text, ESteamUGCType.MAP));
						if (ReadWrite.folderExists(text + "/Bundles", false))
						{
							Assets.load(text + "/Bundles", false, false, true, EAssetOrigin.WORKSHOP, true);
						}
					}
					else if (WorkshopTool.checkLocalizationMeta(text, false))
					{
						DedicatedUGC.ugc.Add(new SteamContent(callback.m_nPublishedFileId, text, ESteamUGCType.LOCALIZATION));
					}
					else if (WorkshopTool.checkObjectMeta(text, false))
					{
						DedicatedUGC.ugc.Add(new SteamContent(callback.m_nPublishedFileId, text, ESteamUGCType.OBJECT));
						Assets.load(text, false, false, true, EAssetOrigin.WORKSHOP, true);
					}
					else if (WorkshopTool.checkItemMeta(text, false))
					{
						DedicatedUGC.ugc.Add(new SteamContent(callback.m_nPublishedFileId, text, ESteamUGCType.ITEM));
						Assets.load(text, false, false, true, EAssetOrigin.WORKSHOP, true);
					}
					else if (WorkshopTool.checkVehicleMeta(text, false))
					{
						DedicatedUGC.ugc.Add(new SteamContent(callback.m_nPublishedFileId, text, ESteamUGCType.VEHICLE));
						Assets.load(text, false, false, true, EAssetOrigin.WORKSHOP, true);
					}
					if (Directory.Exists(text + "/Translations"))
					{
						Translator.registerTranslationDirectory(text + "/Translations");
					}
					if (Directory.Exists(text + "/Content"))
					{
						Assets.searchForAndLoadContent(text + "/Content");
					}
				}
			}
			else
			{
				CommandWindow.Log("Failed downloading workshop item: " + callback.m_nPublishedFileId.m_PublishedFileId);
			}
			DedicatedUGC.installNextItem();
		}

		// Token: 0x060019BA RID: 6586 RVA: 0x00090E18 File Offset: 0x0008F218
		public static void initialize()
		{
			DedicatedUGC.ugc = new List<SteamContent>();
			DedicatedUGC.installing = new List<ulong>();
			if (DedicatedUGC.<>f__mg$cache0 == null)
			{
				DedicatedUGC.<>f__mg$cache0 = new Callback<DownloadItemResult_t>.DispatchDelegate(DedicatedUGC.onItemDownloaded);
			}
			DedicatedUGC.itemDownloaded = Callback<DownloadItemResult_t>.CreateGameServer(DedicatedUGC.<>f__mg$cache0);
			string text = string.Concat(new string[]
			{
				ReadWrite.PATH,
				ServerSavedata.directory,
				"/",
				Provider.serverID,
				"/Workshop/Steam"
			});
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			CommandWindow.Log("Workshop install folder: " + text);
			SteamGameServerUGC.BInitWorkshopForGameServer((DepotId_t)Provider.APP_ID.m_AppId, text);
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x00090ECF File Offset: 0x0008F2CF
		private static void triggerInstalled()
		{
			if (DedicatedUGC.installed != null)
			{
				DedicatedUGC.installed();
			}
		}

		// Token: 0x04000DC6 RID: 3526
		private static Callback<DownloadItemResult_t> itemDownloaded;

		// Token: 0x04000DC7 RID: 3527
		[CompilerGenerated]
		private static Callback<DownloadItemResult_t>.DispatchDelegate <>f__mg$cache0;
	}
}
