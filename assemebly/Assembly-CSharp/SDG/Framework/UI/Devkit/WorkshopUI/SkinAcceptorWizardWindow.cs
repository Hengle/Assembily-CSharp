using System;
using System.Collections.Generic;
using System.IO;
using SDG.Framework.Debug;
using SDG.Framework.IO.FormattedFiles;
using SDG.Framework.IO.FormattedFiles.KeyValueTables;
using SDG.Framework.Translations;
using SDG.Framework.UI.Devkit.InspectorUI;
using SDG.Framework.UI.Sleek2;
using SDG.Framework.Utilities;
using SDG.Unturned;
using Steamworks;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.WorkshopUI
{
	// Token: 0x020002A0 RID: 672
	public class SkinAcceptorWizardWindow : Sleek2Window
	{
		// Token: 0x060013B1 RID: 5041 RVA: 0x0007DC68 File Offset: 0x0007C068
		public SkinAcceptorWizardWindow()
		{
			base.gameObject.name = "UGC_Skin_Acceptor_Wizard";
			base.tab.label.translation = new TranslatedText(new TranslationReference("SDG", "Devkit.Window.UGC_Skin_Acceptor_Wizard.Title"));
			base.tab.label.translation.format();
			this.inspector = new Sleek2Inspector();
			this.inspector.transform.anchorMin = new Vector2(0f, 0f);
			this.inspector.transform.anchorMax = new Vector2(1f, 1f);
			this.inspector.transform.pivot = new Vector2(0f, 1f);
			this.inspector.transform.offsetMin = new Vector2(0f, 40f);
			this.inspector.transform.offsetMax = new Vector2(0f, 0f);
			this.inspector.inspect(this);
			base.safePanel.addElement(this.inspector);
			this.inputButton = new Sleek2ImageTranslatedLabelButton();
			this.inputButton.transform.anchorMin = new Vector2(0f, 0f);
			this.inputButton.transform.anchorMax = new Vector2(1f, 0f);
			this.inputButton.transform.pivot = new Vector2(0.5f, 1f);
			this.inputButton.transform.offsetMin = new Vector2(0f, 20f);
			this.inputButton.transform.offsetMax = new Vector2(0f, 40f);
			this.inputButton.label.translation = new TranslatedText(new TranslationReference("#SDG::Devkit.Window.UGC_Skin_Acceptor_Wizard.Input.Label"));
			this.inputButton.label.translation.format();
			this.inputButton.clicked += this.handleInputButtonClicked;
			base.safePanel.addElement(this.inputButton);
			this.iconButton = new Sleek2ImageTranslatedLabelButton();
			this.iconButton.transform.anchorMin = new Vector2(0f, 0f);
			this.iconButton.transform.anchorMax = new Vector2(1f, 0f);
			this.iconButton.transform.pivot = new Vector2(0.5f, 1f);
			this.iconButton.transform.offsetMin = new Vector2(0f, 0f);
			this.iconButton.transform.offsetMax = new Vector2(0f, 20f);
			this.iconButton.label.translation = new TranslatedText(new TranslationReference("#SDG::Devkit.Window.UGC_Skin_Acceptor_Wizard.Icon.Label"));
			this.iconButton.label.translation.format();
			this.iconButton.clicked += this.handleIconButtonClicked;
			base.safePanel.addElement(this.iconButton);
			this.itemDownloaded = Callback<DownloadItemResult_t>.Create(new Callback<DownloadItemResult_t>.DispatchDelegate(this.onItemDownloaded));
			this.steamUGCQueryCompleted = CallResult<SteamUGCQueryCompleted_t>.Create(new CallResult<SteamUGCQueryCompleted_t>.APIDispatchDelegate(this.onSteamUGCQueryCompleted));
			this.personaStateChange = Callback<PersonaStateChange_t>.Create(new Callback<PersonaStateChange_t>.DispatchDelegate(this.onPersonaStateChange));
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x0007DFE0 File Offset: 0x0007C3E0
		protected virtual Texture2D importTexture(string source, string destination, string file)
		{
			if (!File.Exists(source))
			{
				return null;
			}
			if (!Directory.Exists(destination))
			{
				Directory.CreateDirectory(destination);
			}
			string text = destination + file + Path.GetExtension(source);
			if (File.Exists(text))
			{
				File.Delete(text);
			}
			File.Copy(source, text);
			return null;
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x0007E038 File Offset: 0x0007C438
		protected virtual Material importMaterial(string skinPath, SkinInfo info, string source, string destination, string file)
		{
			if (!Directory.Exists(destination))
			{
				Directory.CreateDirectory(destination);
			}
			Texture2D texture2D = this.importTexture(skinPath + info.albedoPath.absolutePath, source, "/Albedo");
			Texture2D texture2D2 = this.importTexture(skinPath + info.metallicPath.absolutePath, source, "/Metallic");
			Texture2D texture2D3 = this.importTexture(skinPath + info.emissionPath.absolutePath, source, "/Emission");
			if (texture2D == null && texture2D2 == null && texture2D3 == null)
			{
				return null;
			}
			bool flag = false;
			Material material;
			if (flag)
			{
				material = new Material(Shader.Find("Skins/Pattern"));
			}
			else
			{
				material = new Material(Shader.Find("Standard"));
				material.SetFloat("_Glossiness", 0f);
			}
			if (texture2D != null)
			{
				if (flag)
				{
					material.SetTexture("_AlbedoSkin", texture2D);
				}
				else
				{
					material.SetTexture("_MainTex", texture2D);
				}
			}
			if (texture2D2 != null)
			{
				if (flag)
				{
					material.SetTexture("_MetallicSkin", texture2D2);
				}
				else
				{
					material.SetTexture("_MetallicGlossMap", texture2D2);
				}
			}
			if (texture2D3 != null)
			{
				if (flag)
				{
					material.SetTexture("_EmissionSkin", texture2D3);
				}
				else
				{
					material.SetColor("_EmissionColor", Color.white);
					material.SetTexture("_EmissionMap", texture2D3);
				}
			}
			return material;
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x0007E1D4 File Offset: 0x0007C5D4
		protected virtual void input(string skinPath)
		{
			using (StreamReader streamReader = new StreamReader(skinPath + "/Skin.kvt"))
			{
				IFormattedFileReader formattedFileReader = new KeyValueTableReader(streamReader);
				SkinCreatorOutput skinCreatorOutput = formattedFileReader.readValue<SkinCreatorOutput>("Data");
				ItemAsset itemAsset = Assets.find(EAssetType.ITEM, skinCreatorOutput.itemID) as ItemAsset;
				string text = "Assets/Game/Sources/Skins";
				string destination = "Assets/Resources/Bundles/Skins/" + itemAsset.name + "/" + this.patternID;
				Material primarySkin = this.importMaterial(skinPath, skinCreatorOutput.primarySkin, string.Concat(new string[]
				{
					text,
					"/",
					itemAsset.name,
					"/",
					this.patternID
				}), destination, "/Skin_Primary.mat");
				Dictionary<ushort, Material> dictionary = new Dictionary<ushort, Material>();
				foreach (SecondarySkinInfo secondarySkinInfo in skinCreatorOutput.secondarySkins)
				{
					ItemAsset itemAsset2 = Assets.find(EAssetType.ITEM, secondarySkinInfo.itemID) as ItemAsset;
					if (itemAsset2 != null)
					{
						Material material = this.importMaterial(skinPath, secondarySkinInfo, string.Concat(new string[]
						{
							text,
							"/",
							itemAsset2.name,
							"/",
							this.patternID
						}), destination, "/Skin_Secondary_" + secondarySkinInfo.itemID + ".mat");
						if (!(material == null))
						{
							dictionary.Add(secondarySkinInfo.itemID, material);
						}
					}
				}
				Material tertiarySkin = this.importMaterial(skinPath, skinCreatorOutput.tertiarySkin, text + "/Tertiary/" + this.patternID, destination, "/Skin_Tertiary.mat");
				Material attachmentSkin = this.importMaterial(skinPath, skinCreatorOutput.attachmentSkin, text + "/Attachments/" + this.patternID, destination, "/Skin_Attachment.mat");
				ushort num;
				SkinEconBundleWizardWindow.setupBundle(skinCreatorOutput, this.patternID, out num);
				ESkinAcceptEconType eskinAcceptEconType = this.econType;
				EconItemDefinition econItemDefinition;
				if (eskinAcceptEconType != ESkinAcceptEconType.STORE)
				{
					if (eskinAcceptEconType != ESkinAcceptEconType.CRATE)
					{
						Debug.Log("Failed to handle econ type: " + this.econType);
						return;
					}
					econItemDefinition = new EconCrateItemDefinition
					{
						Variants = new EconCrateVariant[]
						{
							new EconCrateVariant(0, true, true, 4),
							new EconCrateVariant(1, false, true, 7),
							new EconCrateVariant(3, false, true, 7),
							new EconCrateVariant(4, false, true, 7),
							new EconCrateVariant(5, false, true, 7),
							new EconCrateVariant(6, false, true, 7),
							new EconCrateVariant(7, false, true, 7),
							new EconCrateVariant(9, false, true, 7),
							new EconCrateVariant(10, false, true, 7),
							new EconCrateVariant(11, false, true, 7),
							new EconCrateVariant(15, false, true, 7),
							new EconCrateVariant(16, false, true, 7),
							new EconCrateVariant(18, false, true, 7),
							new EconCrateVariant(21, false, true, 7),
							new EconCrateVariant(22, false, true, 7),
							new EconCrateVariant(23, false, true, 7),
							new EconCrateVariant(24, false, true, 7)
						},
						IsMarketable = true
					};
				}
				else
				{
					econItemDefinition = new EconStoreItemDefinition
					{
						Price = 100,
						Variants = new EconStoreVariant[]
						{
							new EconStoreVariant(8)
						},
						IsCommodity = false,
						IsPurchasable = true,
						IsMarketable = false
					};
				}
				econItemDefinition.SkinName = new EconName(this.econName, this.patternID);
				econItemDefinition.Description = this.econDesc;
				econItemDefinition.ItemID = skinCreatorOutput.itemID;
				econItemDefinition.SkinID = num;
				econItemDefinition.WorkshopNames = new string[]
				{
					SteamFriends.GetFriendPersonaName((CSteamID)this.workshopItemDetails.m_ulSteamIDOwner)
				};
				econItemDefinition.WorkshopIDs = new ulong[]
				{
					this.workshopItemDetails.m_nPublishedFileId.m_PublishedFileId
				};
				econItemDefinition.IsWorkshopLinked = true;
				econItemDefinition.IsLuminescent = skinCreatorOutput.primarySkin.emissionPath.isValid;
				econItemDefinition.IsDynamic = false;
				econItemDefinition.IsTradable = true;
				int num2;
				SkinEconBundleWizardWindow.setupEcon(econItemDefinition, out num2);
				this.iconItemID = skinCreatorOutput.itemID;
				this.iconSkinID = num;
				this.iconEconID = num2;
				Assets.add(new SkinAsset(false, primarySkin, dictionary, attachmentSkin, tertiarySkin)
				{
					id = num,
					name = this.patternID
				}, false);
			}
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x0007E694 File Offset: 0x0007CA94
		protected virtual void loadWorkshopItem()
		{
			ulong num;
			string text;
			uint num2;
			if (!SteamUGC.GetItemInstallInfo(this.workshopItemDetails.m_nPublishedFileId, out num, out text, 1024u, out num2))
			{
				Debug.LogError("Failed to load!");
				return;
			}
			Debug.Log("Loading: " + this.workshopItemDetails.m_nPublishedFileId);
			this.input(text);
			Debug.Log("Loaded: " + text);
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x0007E702 File Offset: 0x0007CB02
		protected void downloadWorkshopItem()
		{
			Debug.Log("Downloading...");
			SteamUGC.DownloadItem(this.workshopItemDetails.m_nPublishedFileId, true);
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x0007E720 File Offset: 0x0007CB20
		protected void onSteamUGCQueryCompleted(SteamUGCQueryCompleted_t callback, bool io)
		{
			if (callback.m_handle != this.itemHandle)
			{
				return;
			}
			Debug.Log("Queried: " + callback.m_eResult);
			if (SteamUGC.GetQueryUGCResult(this.itemHandle, 0u, out this.workshopItemDetails))
			{
				this.hasPersonaInfo = false;
				if (!SteamFriends.RequestUserInformation((CSteamID)this.workshopItemDetails.m_ulSteamIDOwner, true))
				{
					this.downloadWorkshopItem();
				}
			}
			SteamUGC.ReleaseQueryUGCRequest(this.itemHandle);
			this.itemHandle = UGCQueryHandle_t.Invalid;
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x0007E7B8 File Offset: 0x0007CBB8
		protected void queryItem(PublishedFileId_t publishedFileID)
		{
			Debug.Log("Querying...");
			if (this.itemHandle != UGCQueryHandle_t.Invalid)
			{
				SteamUGC.ReleaseQueryUGCRequest(this.itemHandle);
				this.itemHandle = UGCQueryHandle_t.Invalid;
			}
			this.itemHandle = SteamUGC.CreateQueryUGCDetailsRequest(new PublishedFileId_t[]
			{
				publishedFileID
			}, 1u);
			SteamAPICall_t hAPICall = SteamUGC.SendQueryUGCRequest(this.itemHandle);
			this.steamUGCQueryCompleted.Set(hAPICall, null);
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x0007E835 File Offset: 0x0007CC35
		protected void mainUpdated()
		{
			TimeUtility.updated -= this.mainUpdated;
			this.loadWorkshopItem();
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x0007E850 File Offset: 0x0007CC50
		protected void onItemDownloaded(DownloadItemResult_t callback)
		{
			if (callback.m_nPublishedFileId.m_PublishedFileId != this.workshopItemDetails.m_nPublishedFileId.m_PublishedFileId)
			{
				Debug.LogWarning("Download ID doesn't match!");
				return;
			}
			Debug.Log("Downloaded: " + callback.m_eResult);
			TimeUtility.updated += this.mainUpdated;
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x0007E8B5 File Offset: 0x0007CCB5
		protected void onPersonaStateChange(PersonaStateChange_t callback)
		{
			if (callback.m_ulSteamID != this.workshopItemDetails.m_ulSteamIDOwner)
			{
				return;
			}
			if (this.hasPersonaInfo)
			{
				return;
			}
			this.hasPersonaInfo = true;
			this.downloadWorkshopItem();
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x0007E8E8 File Offset: 0x0007CCE8
		protected virtual void handleInputButtonClicked(Sleek2ImageButton button)
		{
			this.queryItem((PublishedFileId_t)this.workshopID);
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x0007E8FB File Offset: 0x0007CCFB
		protected virtual void handleIconButtonClicked(Sleek2ImageButton button)
		{
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x0007E8FD File Offset: 0x0007CCFD
		public override void destroy()
		{
			this.itemDownloaded.Dispose();
			this.itemDownloaded = null;
			this.steamUGCQueryCompleted.Dispose();
			this.steamUGCQueryCompleted = null;
			this.personaStateChange.Dispose();
			this.personaStateChange = null;
			base.destroy();
		}

		// Token: 0x04000B4B RID: 2891
		[Inspectable("#SDG::Devkit.Window.UGC_Skin_Acceptor_Wizard.Workshop_ID.Name", null)]
		public ulong workshopID;

		// Token: 0x04000B4C RID: 2892
		[Inspectable("#SDG::Devkit.Window.UGC_Skin_Acceptor_Wizard.Pattern_ID.Name", null)]
		public string patternID;

		// Token: 0x04000B4D RID: 2893
		[Inspectable("#SDG::Devkit.Window.UGC_Skin_Acceptor_Wizard.Econ_Name.Name", null)]
		public string econName;

		// Token: 0x04000B4E RID: 2894
		[Inspectable("#SDG::Devkit.Window.UGC_Skin_Acceptor_Wizard.Econ_Desc.Name", null)]
		public string econDesc;

		// Token: 0x04000B4F RID: 2895
		[Inspectable("#SDG::Devkit.Window.UGC_Skin_Acceptor_Wizard.Econ_Type.Name", null)]
		public ESkinAcceptEconType econType;

		// Token: 0x04000B50 RID: 2896
		[Inspectable("#SDG::Devkit.Window.UGC_Skin_Acceptor_Wizard.Icon_Item_ID.Name", null)]
		public ushort iconItemID;

		// Token: 0x04000B51 RID: 2897
		[Inspectable("#SDG::Devkit.Window.UGC_Skin_Acceptor_Wizard.Icon_Vehicle_ID.Name", null)]
		public ushort iconVehicleID;

		// Token: 0x04000B52 RID: 2898
		[Inspectable("#SDG::Devkit.Window.UGC_Skin_Acceptor_Wizard.Icon_Skin_ID.Name", null)]
		public ushort iconSkinID;

		// Token: 0x04000B53 RID: 2899
		[Inspectable("#SDG::Devkit.Window.UGC_Skin_Acceptor_Wizard.Icon_Econ_ID.Name", null)]
		public int iconEconID;

		// Token: 0x04000B54 RID: 2900
		protected Sleek2Inspector inspector;

		// Token: 0x04000B55 RID: 2901
		protected Sleek2ImageTranslatedLabelButton inputButton;

		// Token: 0x04000B56 RID: 2902
		protected Sleek2ImageTranslatedLabelButton iconButton;

		// Token: 0x04000B57 RID: 2903
		protected UGCQueryHandle_t itemHandle = UGCQueryHandle_t.Invalid;

		// Token: 0x04000B58 RID: 2904
		protected SteamUGCDetails_t workshopItemDetails;

		// Token: 0x04000B59 RID: 2905
		protected bool hasPersonaInfo;

		// Token: 0x04000B5A RID: 2906
		protected CallResult<SteamUGCQueryCompleted_t> steamUGCQueryCompleted;

		// Token: 0x04000B5B RID: 2907
		protected Callback<DownloadItemResult_t> itemDownloaded;

		// Token: 0x04000B5C RID: 2908
		protected Callback<PersonaStateChange_t> personaStateChange;
	}
}
