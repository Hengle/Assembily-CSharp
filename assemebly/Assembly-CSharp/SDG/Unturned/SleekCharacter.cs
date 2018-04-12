using System;
using SDG.SteamworksProvider.Services.Store;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020006D0 RID: 1744
	public class SleekCharacter : Sleek
	{
		// Token: 0x0600324D RID: 12877 RVA: 0x0014728C File Offset: 0x0014568C
		public SleekCharacter(byte newIndex)
		{
			base.init();
			this.index = newIndex;
			this.button = new SleekButton();
			this.button.sizeScale_X = 1f;
			this.button.sizeScale_Y = 1f;
			this.button.onClickedButton = new ClickedButton(this.onClickedButton);
			base.add(this.button);
			this.nameLabel = new SleekLabel();
			this.nameLabel.sizeScale_X = 1f;
			this.nameLabel.sizeScale_Y = 0.33f;
			this.button.add(this.nameLabel);
			this.nickLabel = new SleekLabel();
			this.nickLabel.positionScale_Y = 0.33f;
			this.nickLabel.sizeScale_X = 1f;
			this.nickLabel.sizeScale_Y = 0.33f;
			this.button.add(this.nickLabel);
			this.skillsetLabel = new SleekLabel();
			this.skillsetLabel.positionScale_Y = 0.66f;
			this.skillsetLabel.sizeScale_X = 1f;
			this.skillsetLabel.sizeScale_Y = 0.33f;
			this.button.add(this.skillsetLabel);
			if (!Provider.isPro && this.index >= Customization.FREE_CHARACTERS)
			{
				Bundle bundle = Bundles.getBundle("/Bundles/Textures/Menu/Icons/Pro/Pro.unity3d");
				SleekImageTexture sleekImageTexture = new SleekImageTexture();
				sleekImageTexture.positionOffset_X = -20;
				sleekImageTexture.positionOffset_Y = -20;
				sleekImageTexture.positionScale_X = 0.5f;
				sleekImageTexture.positionScale_Y = 0.5f;
				sleekImageTexture.sizeOffset_X = 40;
				sleekImageTexture.sizeOffset_Y = 40;
				sleekImageTexture.texture = (Texture2D)bundle.load("Lock_Medium");
				this.button.add(sleekImageTexture);
				bundle.unload();
			}
		}

		// Token: 0x0600324E RID: 12878 RVA: 0x00147458 File Offset: 0x00145858
		public void updateCharacter(Character character)
		{
			this.nameLabel.text = MenuSurvivorsCharacterUI.localization.format("Name_Label", new object[]
			{
				character.name
			});
			this.nickLabel.text = MenuSurvivorsCharacterUI.localization.format("Nick_Label", new object[]
			{
				character.nick
			});
			this.skillsetLabel.text = MenuSurvivorsCharacterUI.localization.format("Skillset_" + (byte)character.skillset);
		}

		// Token: 0x0600324F RID: 12879 RVA: 0x001474E4 File Offset: 0x001458E4
		private void onClickedButton(SleekButton button)
		{
			if (!Provider.isPro && this.index >= Customization.FREE_CHARACTERS)
			{
				if (!Provider.provider.storeService.canOpenStore)
				{
					MenuUI.alert(MenuSurvivorsCharacterUI.localization.format("Overlay"));
					return;
				}
				Provider.provider.storeService.open(new SteamworksStorePackageID(Provider.PRO_ID.m_AppId));
			}
			else if (this.onClickedCharacter != null)
			{
				this.onClickedCharacter(this, this.index);
			}
		}

		// Token: 0x06003250 RID: 12880 RVA: 0x00147577 File Offset: 0x00145977
		public override void draw(bool ignoreCulling)
		{
			base.drawChildren(ignoreCulling);
		}

		// Token: 0x04002246 RID: 8774
		private byte index;

		// Token: 0x04002247 RID: 8775
		private SleekButton button;

		// Token: 0x04002248 RID: 8776
		private SleekLabel nameLabel;

		// Token: 0x04002249 RID: 8777
		private SleekLabel nickLabel;

		// Token: 0x0400224A RID: 8778
		private SleekLabel skillsetLabel;

		// Token: 0x0400224B RID: 8779
		public ClickedCharacter onClickedCharacter;
	}
}
