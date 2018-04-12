using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020006F3 RID: 1779
	public class SleekLevel : Sleek
	{
		// Token: 0x060032EC RID: 13036 RVA: 0x00149C74 File Offset: 0x00148074
		public SleekLevel(LevelInfo level, bool isEditor)
		{
			base.init();
			base.sizeOffset_X = 400;
			base.sizeOffset_Y = 100;
			this.button = new SleekButton();
			this.button.sizeOffset_X = 0;
			this.button.sizeOffset_Y = 0;
			this.button.sizeScale_X = 1f;
			this.button.sizeScale_Y = 1f;
			if (level.isEditable || !isEditor)
			{
				this.button.onClickedButton = new ClickedButton(this.onClickedButton);
			}
			base.add(this.button);
			if (ReadWrite.fileExists(level.path + "/Icon.png", false, false))
			{
				byte[] data = ReadWrite.readBytes(level.path + "/Icon.png", false, false);
				Texture2D texture2D = new Texture2D(380, 80, TextureFormat.ARGB32, false, true);
				texture2D.name = "Icon_" + level.name + "_List_Icon";
				texture2D.hideFlags = HideFlags.HideAndDontSave;
				texture2D.LoadImage(data);
				this.icon = new SleekImageTexture();
				this.icon.positionOffset_X = 10;
				this.icon.positionOffset_Y = 10;
				this.icon.sizeOffset_X = -20;
				this.icon.sizeOffset_Y = -20;
				this.icon.sizeScale_X = 1f;
				this.icon.sizeScale_Y = 1f;
				this.icon.texture = texture2D;
				this.icon.shouldDestroyTexture = true;
				this.button.add(this.icon);
			}
			this.nameLabel = new SleekLabel();
			this.nameLabel.positionOffset_Y = 10;
			this.nameLabel.sizeScale_X = 1f;
			this.nameLabel.sizeOffset_Y = 50;
			this.nameLabel.fontAlignment = TextAnchor.MiddleCenter;
			this.nameLabel.fontSize = 14;
			this.button.add(this.nameLabel);
			Local local = Localization.tryRead(level.path, false);
			if (local != null && local.has("Name"))
			{
				this.nameLabel.text = local.format("Name");
			}
			else
			{
				this.nameLabel.text = level.name;
			}
			this.infoLabel = new SleekLabel();
			this.infoLabel.positionOffset_Y = 60;
			this.infoLabel.sizeScale_X = 1f;
			this.infoLabel.sizeOffset_Y = 30;
			this.infoLabel.fontAlignment = TextAnchor.MiddleCenter;
			string text = "#SIZE";
			if (level.size == ELevelSize.TINY)
			{
				text = MenuPlaySingleplayerUI.localization.format("Tiny");
			}
			else if (level.size == ELevelSize.SMALL)
			{
				text = MenuPlaySingleplayerUI.localization.format("Small");
			}
			else if (level.size == ELevelSize.MEDIUM)
			{
				text = MenuPlaySingleplayerUI.localization.format("Medium");
			}
			else if (level.size == ELevelSize.LARGE)
			{
				text = MenuPlaySingleplayerUI.localization.format("Large");
			}
			else if (level.size == ELevelSize.INSANE)
			{
				text = MenuPlaySingleplayerUI.localization.format("Insane");
			}
			string text2 = "#TYPE";
			if (level.type == ELevelType.SURVIVAL)
			{
				text2 = MenuPlaySingleplayerUI.localization.format("Survival");
			}
			else if (level.type == ELevelType.HORDE)
			{
				text2 = MenuPlaySingleplayerUI.localization.format("Horde");
			}
			else if (level.type == ELevelType.ARENA)
			{
				text2 = MenuPlaySingleplayerUI.localization.format("Arena");
			}
			this.infoLabel.text = MenuPlaySingleplayerUI.localization.format("Info", new object[]
			{
				text,
				text2
			});
			this.button.add(this.infoLabel);
			if (!level.isEditable && isEditor)
			{
				Bundle bundle = Bundles.getBundle("/Bundles/Textures/Menu/Icons/Workshop/MenuWorkshopEditor/MenuWorkshopEditor.unity3d");
				SleekImageTexture sleekImageTexture = new SleekImageTexture();
				sleekImageTexture.positionOffset_X = 20;
				sleekImageTexture.positionOffset_Y = -20;
				sleekImageTexture.positionScale_Y = 0.5f;
				sleekImageTexture.sizeOffset_X = 40;
				sleekImageTexture.sizeOffset_Y = 40;
				sleekImageTexture.texture = (Texture2D)bundle.load("Lock");
				sleekImageTexture.backgroundTint = ESleekTint.FOREGROUND;
				this.button.add(sleekImageTexture);
				bundle.unload();
			}
			if (level.configData != null && level.configData.Status != EMapStatus.NONE)
			{
				SleekNew sleek = new SleekNew(level.configData.Status == EMapStatus.UPDATED);
				if (this.icon != null)
				{
					this.icon.add(sleek);
				}
				else
				{
					base.add(sleek);
				}
			}
			if (level.configData != null && level.configData.Category == ESingleplayerMapCategory.CURATED && level.configData.CuratedMapMode == ECuratedMapMode.TIMED)
			{
				SleekLabel sleekLabel = new SleekLabel();
				sleekLabel.positionOffset_X = -105;
				sleekLabel.positionScale_X = 1f;
				sleekLabel.sizeOffset_X = 100;
				sleekLabel.sizeOffset_Y = 30;
				sleekLabel.fontAlignment = TextAnchor.MiddleRight;
				sleekLabel.text = MenuPlaySingleplayerUI.localization.format("Curated_Map_Timed");
				sleekLabel.foregroundTint = ESleekTint.NONE;
				sleekLabel.foregroundColor = Color.green;
				if (level.configData.Status != EMapStatus.NONE)
				{
					sleekLabel.positionOffset_Y += 30;
				}
				SleekLabel sleekLabel2 = new SleekLabel();
				sleekLabel2.positionOffset_X = -205;
				sleekLabel2.positionOffset_Y = -30;
				sleekLabel2.positionScale_X = 1f;
				sleekLabel2.positionScale_Y = 1f;
				sleekLabel2.sizeOffset_X = 200;
				sleekLabel2.sizeOffset_Y = 30;
				sleekLabel2.fontAlignment = TextAnchor.MiddleRight;
				sleekLabel2.text = MenuPlaySingleplayerUI.localization.format("Curated_Map_Timestamp", new object[]
				{
					level.configData.getCuratedMapTimestamp().ToString(MenuPlaySingleplayerUI.localization.format("Curated_Map_Timestamp_Format"))
				});
				sleekLabel2.foregroundTint = ESleekTint.NONE;
				sleekLabel2.foregroundColor = Color.green;
				if (this.icon != null)
				{
					this.icon.add(sleekLabel);
					this.icon.add(sleekLabel2);
				}
				else
				{
					base.add(sleekLabel);
					base.add(sleekLabel2);
				}
			}
		}

		// Token: 0x060032ED RID: 13037 RVA: 0x0014A2AE File Offset: 0x001486AE
		private void onClickedButton(SleekButton button)
		{
			if (this.onClickedLevel != null)
			{
				this.onClickedLevel(this, (byte)(base.positionOffset_Y / 110));
			}
		}

		// Token: 0x060032EE RID: 13038 RVA: 0x0014A2D1 File Offset: 0x001486D1
		public override void draw(bool ignoreCulling)
		{
			base.drawChildren(ignoreCulling);
		}

		// Token: 0x04002294 RID: 8852
		private SleekButton button;

		// Token: 0x04002295 RID: 8853
		private SleekImageTexture icon;

		// Token: 0x04002296 RID: 8854
		private SleekLabel nameLabel;

		// Token: 0x04002297 RID: 8855
		private SleekLabel infoLabel;

		// Token: 0x04002298 RID: 8856
		public ClickedLevel onClickedLevel;
	}
}
