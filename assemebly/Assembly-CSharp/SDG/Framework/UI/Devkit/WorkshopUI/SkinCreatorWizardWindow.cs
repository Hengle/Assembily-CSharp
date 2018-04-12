using System;
using System.Collections.Generic;
using System.IO;
using SDG.Framework.IO.FormattedFiles;
using SDG.Framework.IO.FormattedFiles.KeyValueTables;
using SDG.Framework.Translations;
using SDG.Framework.UI.Devkit.InspectorUI;
using SDG.Framework.UI.Sleek2;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.WorkshopUI
{
	// Token: 0x020002A6 RID: 678
	public class SkinCreatorWizardWindow : Sleek2Window
	{
		// Token: 0x060013F1 RID: 5105 RVA: 0x0007F05C File Offset: 0x0007D45C
		public SkinCreatorWizardWindow()
		{
			this.objects = new List<UnityEngine.Object>();
			this.data = new SkinCreatorOutput();
			this.data.changed += this.handleOutputChanged;
			base.gameObject.name = "UGC_Skin_Creator_Wizard";
			base.tab.label.translation = new TranslatedText(new TranslationReference("SDG", "Devkit.Window.UGC_Skin_Creator_Wizard.Title"));
			base.tab.label.translation.format();
			this.inspector = new Sleek2Inspector();
			this.inspector.transform.anchorMin = new Vector2(0f, 0f);
			this.inspector.transform.anchorMax = new Vector2(1f, 1f);
			this.inspector.transform.pivot = new Vector2(0f, 1f);
			this.inspector.transform.offsetMin = new Vector2(0f, 20f);
			this.inspector.transform.offsetMax = new Vector2(0f, 0f);
			this.inspector.inspect(this.data);
			base.safePanel.addElement(this.inspector);
			this.outputButton = new Sleek2ImageTranslatedLabelButton();
			this.outputButton.transform.anchorMin = new Vector2(0f, 0f);
			this.outputButton.transform.anchorMax = new Vector2(1f, 0f);
			this.outputButton.transform.pivot = new Vector2(0.5f, 1f);
			this.outputButton.transform.offsetMin = new Vector2(0f, 0f);
			this.outputButton.transform.offsetMax = new Vector2(0f, 20f);
			this.outputButton.label.translation = new TranslatedText(new TranslationReference("#SDG::Devkit.Window.UGC_Skin_Creator_Wizard.Output.Label"));
			this.outputButton.label.translation.format();
			this.outputButton.clicked += this.handleOutputButtonClicked;
			base.safePanel.addElement(this.outputButton);
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x0007F2B4 File Offset: 0x0007D6B4
		protected Texture2D loadTexture(string path)
		{
			Texture2D texture2D = new Texture2D(2048, 2048, TextureFormat.RGBA32, true);
			texture2D.LoadImage(File.ReadAllBytes(path));
			texture2D.filterMode = FilterMode.Trilinear;
			texture2D.anisoLevel = 16;
			return texture2D;
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x0007F2F0 File Offset: 0x0007D6F0
		protected virtual void refreshPreview()
		{
			foreach (UnityEngine.Object obj in this.objects)
			{
				UnityEngine.Object.Destroy(obj);
			}
			this.objects.Clear();
			Vector3 vector = MainCamera.instance.transform.position + MainCamera.instance.transform.forward * 2f;
			this.createPreview(this.data.itemID, this.data.primarySkin.albedoPath.absolutePath, this.data.primarySkin.metallicPath.absolutePath, this.data.primarySkin.emissionPath.absolutePath, false, ref vector);
			foreach (SecondarySkinInfo secondarySkinInfo in this.data.secondarySkins)
			{
				this.createPreview(secondarySkinInfo.itemID, secondarySkinInfo.albedoPath.absolutePath, secondarySkinInfo.metallicPath.absolutePath, secondarySkinInfo.emissionPath.absolutePath, false, ref vector);
			}
			vector = MainCamera.instance.transform.position + MainCamera.instance.transform.forward * 2f + new Vector3(0f, 1f, 0f);
			foreach (ushort itemID in SkinCreatorWizardWindow.REFERENCE_ITEMS)
			{
				this.createPreview(itemID, null, null, null, true, ref vector);
			}
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x0007F4EC File Offset: 0x0007D8EC
		protected virtual void createPreview(ushort itemID, string albedoPath, string metallicPath, string emissionPath, bool isReference, ref Vector3 position)
		{
			GameObject gameObject = null;
			ItemAsset itemAsset = Assets.find(EAssetType.ITEM, itemID) as ItemAsset;
			if (itemAsset != null)
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(itemAsset.item);
			}
			if (gameObject != null)
			{
				gameObject.transform.position = position;
				position += new Vector3(1f, 0f, 0f);
				this.objects.Add(gameObject);
				bool flag = false;
				if (isReference)
				{
					if (itemAsset.albedoBase != null && !string.IsNullOrEmpty(this.data.attachmentSkin.albedoPath.absolutePath))
					{
						albedoPath = this.data.attachmentSkin.albedoPath.absolutePath;
						metallicPath = this.data.attachmentSkin.metallicPath.absolutePath;
						emissionPath = this.data.attachmentSkin.emissionPath.absolutePath;
						flag = true;
					}
					else
					{
						albedoPath = this.data.tertiarySkin.albedoPath.absolutePath;
						metallicPath = this.data.tertiarySkin.metallicPath.absolutePath;
						emissionPath = this.data.tertiarySkin.emissionPath.absolutePath;
					}
				}
				Material material;
				if (flag)
				{
					material = new Material(Shader.Find("Skins/Pattern"));
				}
				else
				{
					material = new Material(Shader.Find("Standard"));
				}
				this.objects.Add(material);
				if (!string.IsNullOrEmpty(albedoPath))
				{
					Texture2D texture2D = this.loadTexture(albedoPath);
					if (flag)
					{
						material.SetTexture("_AlbedoBase", itemAsset.albedoBase);
						material.SetTexture("_AlbedoSkin", texture2D);
					}
					else
					{
						material.SetTexture("_MainTex", texture2D);
					}
					this.objects.Add(texture2D);
				}
				if (!string.IsNullOrEmpty(metallicPath))
				{
					Texture2D texture2D2 = this.loadTexture(metallicPath);
					if (flag)
					{
						material.SetTexture("_MetallicBase", itemAsset.metallicBase);
						material.SetTexture("_MetallicSkin", texture2D2);
					}
					else
					{
						material.SetTexture("_Metallic", texture2D2);
					}
					this.objects.Add(texture2D2);
				}
				if (!string.IsNullOrEmpty(emissionPath))
				{
					Texture2D texture2D3 = this.loadTexture(emissionPath);
					if (flag)
					{
						material.SetTexture("_EmissionBase", itemAsset.emissionBase);
						material.SetTexture("_EmissionSkin", texture2D3);
					}
					else
					{
						material.SetTexture("_EmissionMap", texture2D3);
					}
					this.objects.Add(texture2D3);
				}
				HighlighterTool.skin(gameObject.transform, material);
			}
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x0007F7A8 File Offset: 0x0007DBA8
		protected virtual void output()
		{
			string absolutePath = this.data.outputPath.absolutePath;
			if (string.IsNullOrEmpty(absolutePath))
			{
				return;
			}
			if (!Directory.Exists(absolutePath))
			{
				Directory.CreateDirectory(absolutePath);
			}
			using (StreamWriter streamWriter = new StreamWriter(absolutePath + "/Skin.kvt"))
			{
				IFormattedFileWriter formattedFileWriter = new KeyValueTableWriter(streamWriter);
				formattedFileWriter.writeValue<SkinCreatorOutput>("Data", this.data);
			}
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x0007F834 File Offset: 0x0007DC34
		protected virtual void handleOutputButtonClicked(Sleek2ImageButton button)
		{
			this.output();
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x0007F83C File Offset: 0x0007DC3C
		protected virtual void handleOutputChanged(SkinCreatorOutput output)
		{
			this.refreshPreview();
		}

		// Token: 0x04000B6B RID: 2923
		private static readonly ushort[] REFERENCE_ITEMS = new ushort[]
		{
			7,
			8,
			21,
			146
		};

		// Token: 0x04000B6C RID: 2924
		public SkinCreatorOutput data;

		// Token: 0x04000B6D RID: 2925
		protected List<UnityEngine.Object> objects;

		// Token: 0x04000B6E RID: 2926
		protected Sleek2Inspector inspector;

		// Token: 0x04000B6F RID: 2927
		protected Sleek2ImageTranslatedLabelButton outputButton;
	}
}
