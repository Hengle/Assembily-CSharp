using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000786 RID: 1926
	public class MenuSurvivorsGroupUI
	{
		// Token: 0x0600376C RID: 14188 RVA: 0x001840FC File Offset: 0x001824FC
		public MenuSurvivorsGroupUI()
		{
			MenuSurvivorsGroupUI.localization = Localization.read("/Menu/Survivors/MenuSurvivorsGroup.dat");
			MenuSurvivorsGroupUI.container = new Sleek();
			MenuSurvivorsGroupUI.container.positionOffset_X = 10;
			MenuSurvivorsGroupUI.container.positionOffset_Y = 10;
			MenuSurvivorsGroupUI.container.positionScale_Y = 1f;
			MenuSurvivorsGroupUI.container.sizeOffset_X = -20;
			MenuSurvivorsGroupUI.container.sizeOffset_Y = -20;
			MenuSurvivorsGroupUI.container.sizeScale_X = 1f;
			MenuSurvivorsGroupUI.container.sizeScale_Y = 1f;
			MenuUI.container.add(MenuSurvivorsGroupUI.container);
			MenuSurvivorsGroupUI.active = false;
			MenuSurvivorsGroupUI.groups = Provider.provider.communityService.getGroups();
			MenuSurvivorsGroupUI.markerColorBox = new SleekBox();
			MenuSurvivorsGroupUI.markerColorBox.positionOffset_X = -120;
			MenuSurvivorsGroupUI.markerColorBox.positionOffset_Y = 100;
			MenuSurvivorsGroupUI.markerColorBox.positionScale_X = 0.75f;
			MenuSurvivorsGroupUI.markerColorBox.sizeOffset_X = 240;
			MenuSurvivorsGroupUI.markerColorBox.sizeOffset_Y = 30;
			MenuSurvivorsGroupUI.markerColorBox.text = MenuSurvivorsGroupUI.localization.format("Marker_Color_Box");
			MenuSurvivorsGroupUI.container.add(MenuSurvivorsGroupUI.markerColorBox);
			MenuSurvivorsGroupUI.markerColorPicker = new SleekColorPicker();
			MenuSurvivorsGroupUI.markerColorPicker.positionOffset_X = -120;
			MenuSurvivorsGroupUI.markerColorPicker.positionOffset_Y = 140;
			MenuSurvivorsGroupUI.markerColorPicker.positionScale_X = 0.75f;
			SleekColorPicker sleekColorPicker = MenuSurvivorsGroupUI.markerColorPicker;
			if (MenuSurvivorsGroupUI.<>f__mg$cache0 == null)
			{
				MenuSurvivorsGroupUI.<>f__mg$cache0 = new ColorPicked(MenuSurvivorsGroupUI.onPickedMarkerColor);
			}
			sleekColorPicker.onColorPicked = MenuSurvivorsGroupUI.<>f__mg$cache0;
			MenuSurvivorsGroupUI.container.add(MenuSurvivorsGroupUI.markerColorPicker);
			MenuSurvivorsGroupUI.groupButton = new SleekButtonIcon(null, 20);
			MenuSurvivorsGroupUI.groupButton.positionOffset_X = -120;
			MenuSurvivorsGroupUI.groupButton.positionOffset_Y = 270;
			MenuSurvivorsGroupUI.groupButton.positionScale_X = 0.75f;
			MenuSurvivorsGroupUI.groupButton.sizeOffset_X = 240;
			MenuSurvivorsGroupUI.groupButton.sizeOffset_Y = 30;
			MenuSurvivorsGroupUI.groupButton.addLabel(MenuSurvivorsGroupUI.localization.format("Group_Box_Label"), ESleekSide.LEFT);
			SleekButton sleekButton = MenuSurvivorsGroupUI.groupButton;
			if (MenuSurvivorsGroupUI.<>f__mg$cache1 == null)
			{
				MenuSurvivorsGroupUI.<>f__mg$cache1 = new ClickedButton(MenuSurvivorsGroupUI.onClickedUngroupButton);
			}
			sleekButton.onClickedButton = MenuSurvivorsGroupUI.<>f__mg$cache1;
			MenuSurvivorsGroupUI.container.add(MenuSurvivorsGroupUI.groupButton);
			MenuSurvivorsGroupUI.groupsBox = new SleekScrollBox();
			MenuSurvivorsGroupUI.groupsBox.positionOffset_X = -120;
			MenuSurvivorsGroupUI.groupsBox.positionOffset_Y = 310;
			MenuSurvivorsGroupUI.groupsBox.positionScale_X = 0.75f;
			MenuSurvivorsGroupUI.groupsBox.sizeOffset_X = 270;
			MenuSurvivorsGroupUI.groupsBox.sizeOffset_Y = -410;
			MenuSurvivorsGroupUI.groupsBox.sizeScale_Y = 1f;
			MenuSurvivorsGroupUI.groupsBox.area = new Rect(0f, 0f, 5f, (float)(MenuSurvivorsGroupUI.groups.Length * 40 - 10));
			MenuSurvivorsGroupUI.container.add(MenuSurvivorsGroupUI.groupsBox);
			for (int i = 0; i < MenuSurvivorsGroupUI.groups.Length; i++)
			{
				SleekButtonIcon sleekButtonIcon = new SleekButtonIcon(MenuSurvivorsGroupUI.groups[i].icon, 20);
				sleekButtonIcon.positionOffset_Y = i * 40;
				sleekButtonIcon.sizeOffset_X = 240;
				sleekButtonIcon.sizeOffset_Y = 30;
				sleekButtonIcon.text = MenuSurvivorsGroupUI.groups[i].name;
				SleekButton sleekButton2 = sleekButtonIcon;
				if (MenuSurvivorsGroupUI.<>f__mg$cache2 == null)
				{
					MenuSurvivorsGroupUI.<>f__mg$cache2 = new ClickedButton(MenuSurvivorsGroupUI.onClickedGroupButton);
				}
				sleekButton2.onClickedButton = MenuSurvivorsGroupUI.<>f__mg$cache2;
				MenuSurvivorsGroupUI.groupsBox.add(sleekButtonIcon);
			}
			Delegate onCharacterUpdated = Characters.onCharacterUpdated;
			if (MenuSurvivorsGroupUI.<>f__mg$cache3 == null)
			{
				MenuSurvivorsGroupUI.<>f__mg$cache3 = new CharacterUpdated(MenuSurvivorsGroupUI.onCharacterUpdated);
			}
			Characters.onCharacterUpdated = (CharacterUpdated)Delegate.Combine(onCharacterUpdated, MenuSurvivorsGroupUI.<>f__mg$cache3);
			MenuSurvivorsGroupUI.backButton = new SleekButtonIcon((Texture2D)MenuDashboardUI.icons.load("Exit"));
			MenuSurvivorsGroupUI.backButton.positionOffset_Y = -50;
			MenuSurvivorsGroupUI.backButton.positionScale_Y = 1f;
			MenuSurvivorsGroupUI.backButton.sizeOffset_X = 200;
			MenuSurvivorsGroupUI.backButton.sizeOffset_Y = 50;
			MenuSurvivorsGroupUI.backButton.text = MenuDashboardUI.localization.format("BackButtonText");
			MenuSurvivorsGroupUI.backButton.tooltip = MenuDashboardUI.localization.format("BackButtonTooltip");
			SleekButton sleekButton3 = MenuSurvivorsGroupUI.backButton;
			if (MenuSurvivorsGroupUI.<>f__mg$cache4 == null)
			{
				MenuSurvivorsGroupUI.<>f__mg$cache4 = new ClickedButton(MenuSurvivorsGroupUI.onClickedBackButton);
			}
			sleekButton3.onClickedButton = MenuSurvivorsGroupUI.<>f__mg$cache4;
			MenuSurvivorsGroupUI.backButton.fontSize = 14;
			MenuSurvivorsGroupUI.backButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			MenuSurvivorsGroupUI.container.add(MenuSurvivorsGroupUI.backButton);
		}

		// Token: 0x0600376D RID: 14189 RVA: 0x0018455C File Offset: 0x0018295C
		public static void open()
		{
			if (MenuSurvivorsGroupUI.active)
			{
				return;
			}
			MenuSurvivorsGroupUI.active = true;
			MenuSurvivorsGroupUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x0600376E RID: 14190 RVA: 0x00184589 File Offset: 0x00182989
		public static void close()
		{
			if (!MenuSurvivorsGroupUI.active)
			{
				return;
			}
			MenuSurvivorsGroupUI.active = false;
			MenuSurvivorsGroupUI.container.lerpPositionScale(0f, 1f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x0600376F RID: 14191 RVA: 0x001845B8 File Offset: 0x001829B8
		private static void onCharacterUpdated(byte index, Character character)
		{
			if (index == Characters.selected)
			{
				MenuSurvivorsGroupUI.markerColorPicker.state = character.markerColor;
				for (int i = 0; i < MenuSurvivorsGroupUI.groups.Length; i++)
				{
					if (MenuSurvivorsGroupUI.groups[i].steamID == character.group)
					{
						MenuSurvivorsGroupUI.groupButton.text = MenuSurvivorsGroupUI.groups[i].name;
						MenuSurvivorsGroupUI.groupButton.icon = MenuSurvivorsGroupUI.groups[i].icon;
						return;
					}
				}
				MenuSurvivorsGroupUI.groupButton.text = MenuSurvivorsGroupUI.localization.format("Group_Box");
				MenuSurvivorsGroupUI.groupButton.icon = null;
			}
		}

		// Token: 0x06003770 RID: 14192 RVA: 0x00184665 File Offset: 0x00182A65
		private static void onTypedNickField(SleekField field, string text)
		{
			Characters.renick(text);
		}

		// Token: 0x06003771 RID: 14193 RVA: 0x0018466D File Offset: 0x00182A6D
		private static void onPickedMarkerColor(SleekColorPicker picker, Color state)
		{
			Characters.paintMarkerColor(state);
		}

		// Token: 0x06003772 RID: 14194 RVA: 0x00184675 File Offset: 0x00182A75
		private static void onClickedGroupButton(SleekButton button)
		{
			Characters.group(MenuSurvivorsGroupUI.groups[button.positionOffset_Y / 40].steamID);
		}

		// Token: 0x06003773 RID: 14195 RVA: 0x00184690 File Offset: 0x00182A90
		private static void onClickedUngroupButton(SleekButton button)
		{
			Characters.ungroup();
		}

		// Token: 0x06003774 RID: 14196 RVA: 0x00184697 File Offset: 0x00182A97
		private static void onClickedBackButton(SleekButton button)
		{
			MenuSurvivorsUI.open();
			MenuSurvivorsGroupUI.close();
		}

		// Token: 0x040028E3 RID: 10467
		private static Local localization;

		// Token: 0x040028E4 RID: 10468
		private static Sleek container;

		// Token: 0x040028E5 RID: 10469
		public static bool active;

		// Token: 0x040028E6 RID: 10470
		private static SleekButtonIcon backButton;

		// Token: 0x040028E7 RID: 10471
		private static SteamGroup[] groups;

		// Token: 0x040028E8 RID: 10472
		private static SleekBox markerColorBox;

		// Token: 0x040028E9 RID: 10473
		private static SleekColorPicker markerColorPicker;

		// Token: 0x040028EA RID: 10474
		private static SleekButtonIcon groupButton;

		// Token: 0x040028EB RID: 10475
		private static SleekScrollBox groupsBox;

		// Token: 0x040028EC RID: 10476
		[CompilerGenerated]
		private static ColorPicked <>f__mg$cache0;

		// Token: 0x040028ED RID: 10477
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache1;

		// Token: 0x040028EE RID: 10478
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache2;

		// Token: 0x040028EF RID: 10479
		[CompilerGenerated]
		private static CharacterUpdated <>f__mg$cache3;

		// Token: 0x040028F0 RID: 10480
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache4;
	}
}
