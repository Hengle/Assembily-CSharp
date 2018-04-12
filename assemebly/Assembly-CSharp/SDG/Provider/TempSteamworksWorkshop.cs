using System;
using System.Collections.Generic;
using System.IO;
using SDG.Framework.Translations;
using SDG.SteamworksProvider;
using SDG.Unturned;
using Steamworks;

namespace SDG.Provider
{
	// Token: 0x02000359 RID: 857
	public class TempSteamworksWorkshop
	{
		// Token: 0x060017A3 RID: 6051 RVA: 0x00087864 File Offset: 0x00085C64
		public TempSteamworksWorkshop(SteamworksAppInfo newAppInfo)
		{
			this.appInfo = newAppInfo;
			this.downloaded = new List<PublishedFileId_t>();
			if (!this.appInfo.isDedicated)
			{
				this.createItemResult = CallResult<CreateItemResult_t>.Create(new CallResult<CreateItemResult_t>.APIDispatchDelegate(this.onCreateItemResult));
				this.submitItemUpdateResult = CallResult<SubmitItemUpdateResult_t>.Create(new CallResult<SubmitItemUpdateResult_t>.APIDispatchDelegate(this.onSubmitItemUpdateResult));
				this.queryCompleted = CallResult<SteamUGCQueryCompleted_t>.Create(new CallResult<SteamUGCQueryCompleted_t>.APIDispatchDelegate(this.onQueryCompleted));
				this.itemDownloaded = Callback<DownloadItemResult_t>.Create(new Callback<DownloadItemResult_t>.DispatchDelegate(this.onItemDownloaded));
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x060017A4 RID: 6052 RVA: 0x000878F5 File Offset: 0x00085CF5
		public bool canOpenWorkshop
		{
			get
			{
				return SteamUtils.IsOverlayEnabled();
			}
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x000878FC File Offset: 0x00085CFC
		public void open(PublishedFileId_t id)
		{
			SteamFriends.ActivateGameOverlayToWebPage("http://steamcommunity.com/sharedfiles/filedetails/?id=" + id.m_PublishedFileId);
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x060017A6 RID: 6054 RVA: 0x00087919 File Offset: 0x00085D19
		public List<SteamContent> ugc
		{
			get
			{
				return this._ugc;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x060017A7 RID: 6055 RVA: 0x00087921 File Offset: 0x00085D21
		public List<SteamPublished> published
		{
			get
			{
				return this._published;
			}
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x0008792C File Offset: 0x00085D2C
		private void onCreateItemResult(CreateItemResult_t callback, bool io)
		{
			if (callback.m_bUserNeedsToAcceptWorkshopLegalAgreement || callback.m_eResult != EResult.k_EResultOK || io)
			{
				if (callback.m_bUserNeedsToAcceptWorkshopLegalAgreement)
				{
					Assets.errors.Add("Failed to create item because you need to accept the workshop legal agreement.");
				}
				if (callback.m_eResult != EResult.k_EResultOK)
				{
					Assets.errors.Add("Failed to create item because: " + callback.m_eResult);
				}
				if (io)
				{
					Assets.errors.Add("Failed to create item because of an IO issue.");
				}
				MenuUI.alert(Provider.localization.format("UGC_Fail"));
				return;
			}
			this.publishedFileID = callback.m_nPublishedFileId;
			this.updateUGC();
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x000879E0 File Offset: 0x00085DE0
		private void onSubmitItemUpdateResult(SubmitItemUpdateResult_t callback, bool io)
		{
			if (callback.m_bUserNeedsToAcceptWorkshopLegalAgreement || callback.m_eResult != EResult.k_EResultOK || io)
			{
				if (callback.m_bUserNeedsToAcceptWorkshopLegalAgreement)
				{
					Assets.errors.Add("Failed to submit update because you need to accept the workshop legal agreement.");
				}
				if (callback.m_eResult != EResult.k_EResultOK)
				{
					Assets.errors.Add("Failed to submit update because: " + callback.m_eResult);
				}
				if (io)
				{
					Assets.errors.Add("Failed to submit update because of an IO issue.");
				}
				MenuUI.alert(Provider.localization.format("UGC_Fail"));
				return;
			}
			MenuUI.alert(Provider.localization.format("UGC_Success"));
			Provider.provider.workshopService.open(this.publishedFileID);
			this.refreshPublished();
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x00087AB0 File Offset: 0x00085EB0
		private void onQueryCompleted(SteamUGCQueryCompleted_t callback, bool io)
		{
			if (callback.m_eResult != EResult.k_EResultOK || io)
			{
				return;
			}
			if (callback.m_unNumResultsReturned < 1u)
			{
				return;
			}
			for (uint num = 0u; num < callback.m_unNumResultsReturned; num += 1u)
			{
				SteamUGCDetails_t steamUGCDetails_t;
				SteamUGC.GetQueryUGCResult(this.ugcRequest, num, out steamUGCDetails_t);
				SteamPublished item = new SteamPublished(steamUGCDetails_t.m_rgchTitle, steamUGCDetails_t.m_nPublishedFileId);
				this.published.Add(item);
			}
			if (this.onPublishedAdded != null)
			{
				this.onPublishedAdded();
			}
			this.cleanupUGCRequest();
			this.shouldRequestAnotherPage = true;
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x00087B4C File Offset: 0x00085F4C
		public void update()
		{
			if (this.shouldRequestAnotherPage)
			{
				this.shouldRequestAnotherPage = false;
				this.ugcRequestPage += 1u;
				this.ugcRequest = SteamUGC.CreateQueryUserUGCRequest(Provider.client.GetAccountID(), EUserUGCList.k_EUserUGCList_Published, EUGCMatchingUGCType.k_EUGCMatchingUGCType_Items, EUserUGCListSortOrder.k_EUserUGCListSortOrder_CreationOrderAsc, SteamUtils.GetAppID(), SteamUtils.GetAppID(), this.ugcRequestPage);
				SteamAPICall_t hAPICall = SteamUGC.SendQueryUGCRequest(this.ugcRequest);
				this.queryCompleted.Set(hAPICall, null);
			}
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x00087BC0 File Offset: 0x00085FC0
		private void onItemDownloaded(DownloadItemResult_t callback)
		{
			if (this.installing == null || this.installing.Count == 0)
			{
				return;
			}
			this.installing.Remove(callback.m_nPublishedFileId);
			LoadingUI.updateProgress((float)(this.installed - this.installing.Count) / (float)this.installed);
			ulong num;
			string text;
			uint num2;
			if (SteamUGC.GetItemInstallInfo(callback.m_nPublishedFileId, out num, out text, 1024u, out num2) && ReadWrite.folderExists(text, false))
			{
				if (WorkshopTool.checkMapMeta(text, false))
				{
					this.ugc.Add(new SteamContent(callback.m_nPublishedFileId, text, ESteamUGCType.MAP));
					if (ReadWrite.folderExists(text + "/Bundles", false))
					{
						Assets.load(text + "/Bundles", false, false, true, EAssetOrigin.WORKSHOP, true);
					}
				}
				else if (WorkshopTool.checkLocalizationMeta(text, false))
				{
					this.ugc.Add(new SteamContent(callback.m_nPublishedFileId, text, ESteamUGCType.LOCALIZATION));
				}
				else if (WorkshopTool.checkObjectMeta(text, false))
				{
					this.ugc.Add(new SteamContent(callback.m_nPublishedFileId, text, ESteamUGCType.OBJECT));
					Assets.load(text, false, false, true, EAssetOrigin.WORKSHOP, true);
				}
				else if (WorkshopTool.checkItemMeta(text, false))
				{
					this.ugc.Add(new SteamContent(callback.m_nPublishedFileId, text, ESteamUGCType.ITEM));
					Assets.load(text, false, false, true, EAssetOrigin.WORKSHOP, true);
				}
				else if (WorkshopTool.checkVehicleMeta(text, false))
				{
					this.ugc.Add(new SteamContent(callback.m_nPublishedFileId, text, ESteamUGCType.VEHICLE));
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
			if (this.installing.Count == 0)
			{
				Provider.launch();
			}
			else
			{
				SteamUGC.DownloadItem(this.installing[0], true);
			}
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x00087DD4 File Offset: 0x000861D4
		private void cleanupUGCRequest()
		{
			if (this.ugcRequest == UGCQueryHandle_t.Invalid)
			{
				return;
			}
			SteamUGC.ReleaseQueryUGCRequest(this.ugcRequest);
			this.ugcRequest = UGCQueryHandle_t.Invalid;
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x00087E04 File Offset: 0x00086204
		public void prepareUGC(string name, string description, string path, string preview, string change, ESteamUGCType type, string tag, ESteamUGCVisibility visibility)
		{
			bool verified = File.Exists(path + "/Skin.kvt");
			this.prepareUGC(name, description, path, preview, change, type, tag, visibility, verified);
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x00087E38 File Offset: 0x00086238
		public void prepareUGC(string name, string description, string path, string preview, string change, ESteamUGCType type, string tag, ESteamUGCVisibility visibility, bool verified)
		{
			this.ugcName = name;
			this.ugcDescription = description;
			this.ugcPath = path;
			this.ugcPreview = preview;
			this.ugcChange = change;
			this.ugcType = type;
			this.ugcTag = tag;
			this.ugcVisibility = visibility;
			this.ugcVerified = verified;
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x00087E8A File Offset: 0x0008628A
		public void prepareUGC(PublishedFileId_t id)
		{
			this.publishedFileID = id;
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x00087E94 File Offset: 0x00086294
		public void createUGC(bool ugcFor)
		{
			SteamAPICall_t hAPICall = SteamUGC.CreateItem(SteamUtils.GetAppID(), (!ugcFor) ? EWorkshopFileType.k_EWorkshopFileTypeFirst : EWorkshopFileType.k_EWorkshopFileTypeMicrotransaction);
			this.createItemResult.Set(hAPICall, null);
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x00087EC8 File Offset: 0x000862C8
		public void updateUGC()
		{
			UGCUpdateHandle_t ugcupdateHandle_t = SteamUGC.StartItemUpdate(SteamUtils.GetAppID(), this.publishedFileID);
			if (this.ugcType == ESteamUGCType.MAP)
			{
				ReadWrite.writeBytes(this.ugcPath + "/Map.meta", false, false, new byte[1]);
			}
			else if (this.ugcType == ESteamUGCType.LOCALIZATION)
			{
				ReadWrite.writeBytes(this.ugcPath + "/Localization.meta", false, false, new byte[1]);
			}
			else if (this.ugcType == ESteamUGCType.OBJECT)
			{
				ReadWrite.writeBytes(this.ugcPath + "/Object.meta", false, false, new byte[1]);
			}
			else if (this.ugcType == ESteamUGCType.ITEM)
			{
				ReadWrite.writeBytes(this.ugcPath + "/Item.meta", false, false, new byte[1]);
			}
			else if (this.ugcType == ESteamUGCType.VEHICLE)
			{
				ReadWrite.writeBytes(this.ugcPath + "/Vehicle.meta", false, false, new byte[1]);
			}
			else if (this.ugcType == ESteamUGCType.SKIN)
			{
				ReadWrite.writeBytes(this.ugcPath + "/Skin.meta", false, false, new byte[1]);
			}
			SteamUGC.SetItemContent(ugcupdateHandle_t, this.ugcPath);
			if (this.ugcDescription.Length > 0)
			{
				SteamUGC.SetItemDescription(ugcupdateHandle_t, this.ugcDescription);
			}
			if (this.ugcPreview.Length > 0)
			{
				SteamUGC.SetItemPreview(ugcupdateHandle_t, this.ugcPreview);
			}
			List<string> list = new List<string>();
			if (this.ugcType == ESteamUGCType.MAP)
			{
				list.Add("Map");
			}
			else if (this.ugcType == ESteamUGCType.LOCALIZATION)
			{
				list.Add("Localization");
			}
			else if (this.ugcType == ESteamUGCType.OBJECT)
			{
				list.Add("Object");
			}
			else if (this.ugcType == ESteamUGCType.ITEM)
			{
				list.Add("Item");
			}
			else if (this.ugcType == ESteamUGCType.VEHICLE)
			{
				list.Add("Vehicle");
			}
			else if (this.ugcType == ESteamUGCType.SKIN)
			{
				list.Add("Skin");
			}
			if (this.ugcTag != null && this.ugcTag.Length > 0)
			{
				list.Add(this.ugcTag);
			}
			if (this.ugcVerified)
			{
				list.Add("Verified");
			}
			SteamUGC.SetItemTags(ugcupdateHandle_t, list.ToArray());
			if (this.ugcName.Length > 0)
			{
				SteamUGC.SetItemTitle(ugcupdateHandle_t, this.ugcName);
			}
			if (this.ugcVisibility == ESteamUGCVisibility.PUBLIC)
			{
				SteamUGC.SetItemVisibility(ugcupdateHandle_t, ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityPublic);
			}
			else if (this.ugcVisibility == ESteamUGCVisibility.FRIENDS_ONLY)
			{
				SteamUGC.SetItemVisibility(ugcupdateHandle_t, ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityFriendsOnly);
			}
			else if (this.ugcVisibility == ESteamUGCVisibility.PRIVATE)
			{
				SteamUGC.SetItemVisibility(ugcupdateHandle_t, ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityPrivate);
			}
			SteamAPICall_t hAPICall = SteamUGC.SubmitItemUpdate(ugcupdateHandle_t, this.ugcChange);
			this.submitItemUpdateResult.Set(hAPICall, null);
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x000881B0 File Offset: 0x000865B0
		public void refreshUGC()
		{
			this._ugc = new List<SteamContent>();
			uint numSubscribedItems = SteamUGC.GetNumSubscribedItems();
			PublishedFileId_t[] array = new PublishedFileId_t[numSubscribedItems];
			SteamUGC.GetSubscribedItems(array, numSubscribedItems);
			for (uint num = 0u; num < numSubscribedItems; num += 1u)
			{
				ulong num2;
				string text;
				uint num3;
				if (SteamUGC.GetItemInstallInfo(array[(int)((UIntPtr)num)], out num2, out text, 1024u, out num3) && ReadWrite.folderExists(text, false))
				{
					if (WorkshopTool.checkMapMeta(text, false))
					{
						this.ugc.Add(new SteamContent(array[(int)((UIntPtr)num)], text, ESteamUGCType.MAP));
					}
					else if (WorkshopTool.checkLocalizationMeta(text, false))
					{
						this.ugc.Add(new SteamContent(array[(int)((UIntPtr)num)], text, ESteamUGCType.LOCALIZATION));
					}
					else if (WorkshopTool.checkObjectMeta(text, false))
					{
						this.ugc.Add(new SteamContent(array[(int)((UIntPtr)num)], text, ESteamUGCType.OBJECT));
					}
					else if (WorkshopTool.checkItemMeta(text, false))
					{
						this.ugc.Add(new SteamContent(array[(int)((UIntPtr)num)], text, ESteamUGCType.ITEM));
					}
					else if (WorkshopTool.checkVehicleMeta(text, false))
					{
						this.ugc.Add(new SteamContent(array[(int)((UIntPtr)num)], text, ESteamUGCType.VEHICLE));
					}
				}
			}
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x00088314 File Offset: 0x00086714
		public void refreshPublished()
		{
			if (this.onPublishedRemoved != null)
			{
				this.onPublishedRemoved();
			}
			this.cleanupUGCRequest();
			this._published = new List<SteamPublished>();
			this.ugcRequestPage = 1u;
			this.shouldRequestAnotherPage = false;
			this.ugcRequest = SteamUGC.CreateQueryUserUGCRequest(Provider.client.GetAccountID(), EUserUGCList.k_EUserUGCList_Published, EUGCMatchingUGCType.k_EUGCMatchingUGCType_Items, EUserUGCListSortOrder.k_EUserUGCListSortOrder_CreationOrderAsc, SteamUtils.GetAppID(), SteamUtils.GetAppID(), this.ugcRequestPage);
			SteamAPICall_t hAPICall = SteamUGC.SendQueryUGCRequest(this.ugcRequest);
			this.queryCompleted.Set(hAPICall, null);
		}

		// Token: 0x04000C9D RID: 3229
		private SteamworksAppInfo appInfo;

		// Token: 0x04000C9E RID: 3230
		public TempSteamworksWorkshop.PublishedAdded onPublishedAdded;

		// Token: 0x04000C9F RID: 3231
		public TempSteamworksWorkshop.PublishedRemoved onPublishedRemoved;

		// Token: 0x04000CA0 RID: 3232
		private PublishedFileId_t publishedFileID;

		// Token: 0x04000CA1 RID: 3233
		private UGCQueryHandle_t ugcRequest;

		// Token: 0x04000CA2 RID: 3234
		private uint ugcRequestPage;

		// Token: 0x04000CA3 RID: 3235
		private bool shouldRequestAnotherPage;

		// Token: 0x04000CA4 RID: 3236
		private string ugcName;

		// Token: 0x04000CA5 RID: 3237
		private string ugcDescription;

		// Token: 0x04000CA6 RID: 3238
		private string ugcPath;

		// Token: 0x04000CA7 RID: 3239
		private string ugcPreview;

		// Token: 0x04000CA8 RID: 3240
		private string ugcChange;

		// Token: 0x04000CA9 RID: 3241
		private ESteamUGCType ugcType;

		// Token: 0x04000CAA RID: 3242
		private string ugcTag;

		// Token: 0x04000CAB RID: 3243
		private ESteamUGCVisibility ugcVisibility;

		// Token: 0x04000CAC RID: 3244
		private bool ugcVerified;

		// Token: 0x04000CAD RID: 3245
		public int installed;

		// Token: 0x04000CAE RID: 3246
		public List<PublishedFileId_t> downloaded;

		// Token: 0x04000CAF RID: 3247
		public List<PublishedFileId_t> installing;

		// Token: 0x04000CB0 RID: 3248
		private List<SteamContent> _ugc;

		// Token: 0x04000CB1 RID: 3249
		private List<SteamPublished> _published;

		// Token: 0x04000CB2 RID: 3250
		private CallResult<CreateItemResult_t> createItemResult;

		// Token: 0x04000CB3 RID: 3251
		private CallResult<SubmitItemUpdateResult_t> submitItemUpdateResult;

		// Token: 0x04000CB4 RID: 3252
		private CallResult<SteamUGCQueryCompleted_t> queryCompleted;

		// Token: 0x04000CB5 RID: 3253
		private Callback<DownloadItemResult_t> itemDownloaded;

		// Token: 0x0200035A RID: 858
		// (Invoke) Token: 0x060017B6 RID: 6070
		public delegate void PublishedAdded();

		// Token: 0x0200035B RID: 859
		// (Invoke) Token: 0x060017BA RID: 6074
		public delegate void PublishedRemoved();
	}
}
