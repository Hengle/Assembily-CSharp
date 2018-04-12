using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000790 RID: 1936
	public class PlayerBarricadeMannequinUI
	{
		// Token: 0x060037D0 RID: 14288 RVA: 0x0018B4F0 File Offset: 0x001898F0
		public PlayerBarricadeMannequinUI()
		{
			PlayerBarricadeMannequinUI.localization = Localization.read("/Player/PlayerBarricadeMannequin.dat");
			PlayerBarricadeMannequinUI.container = new Sleek();
			PlayerBarricadeMannequinUI.container.positionScale_Y = 1f;
			PlayerBarricadeMannequinUI.container.positionOffset_X = 10;
			PlayerBarricadeMannequinUI.container.positionOffset_Y = 10;
			PlayerBarricadeMannequinUI.container.sizeOffset_X = -20;
			PlayerBarricadeMannequinUI.container.sizeOffset_Y = -20;
			PlayerBarricadeMannequinUI.container.sizeScale_X = 1f;
			PlayerBarricadeMannequinUI.container.sizeScale_Y = 1f;
			PlayerUI.container.add(PlayerBarricadeMannequinUI.container);
			PlayerBarricadeMannequinUI.active = false;
			PlayerBarricadeMannequinUI.mannequin = null;
			PlayerBarricadeMannequinUI.cosmeticsButton = new SleekButton();
			PlayerBarricadeMannequinUI.cosmeticsButton.positionOffset_X = -100;
			PlayerBarricadeMannequinUI.cosmeticsButton.positionOffset_Y = -135;
			PlayerBarricadeMannequinUI.cosmeticsButton.positionScale_X = 0.5f;
			PlayerBarricadeMannequinUI.cosmeticsButton.positionScale_Y = 0.5f;
			PlayerBarricadeMannequinUI.cosmeticsButton.sizeOffset_X = 200;
			PlayerBarricadeMannequinUI.cosmeticsButton.sizeOffset_Y = 30;
			PlayerBarricadeMannequinUI.cosmeticsButton.text = PlayerBarricadeMannequinUI.localization.format("Cosmetics_Button");
			PlayerBarricadeMannequinUI.cosmeticsButton.tooltip = PlayerBarricadeMannequinUI.localization.format("Cosmetics_Button_Tooltip");
			SleekButton sleekButton = PlayerBarricadeMannequinUI.cosmeticsButton;
			if (PlayerBarricadeMannequinUI.<>f__mg$cache0 == null)
			{
				PlayerBarricadeMannequinUI.<>f__mg$cache0 = new ClickedButton(PlayerBarricadeMannequinUI.onClickedCosmeticsButton);
			}
			sleekButton.onClickedButton = PlayerBarricadeMannequinUI.<>f__mg$cache0;
			PlayerBarricadeMannequinUI.container.add(PlayerBarricadeMannequinUI.cosmeticsButton);
			PlayerBarricadeMannequinUI.addButton = new SleekButton();
			PlayerBarricadeMannequinUI.addButton.positionOffset_X = -100;
			PlayerBarricadeMannequinUI.addButton.positionOffset_Y = -95;
			PlayerBarricadeMannequinUI.addButton.positionScale_X = 0.5f;
			PlayerBarricadeMannequinUI.addButton.positionScale_Y = 0.5f;
			PlayerBarricadeMannequinUI.addButton.sizeOffset_X = 200;
			PlayerBarricadeMannequinUI.addButton.sizeOffset_Y = 30;
			PlayerBarricadeMannequinUI.addButton.text = PlayerBarricadeMannequinUI.localization.format("Add_Button");
			PlayerBarricadeMannequinUI.addButton.tooltip = PlayerBarricadeMannequinUI.localization.format("Add_Button_Tooltip");
			SleekButton sleekButton2 = PlayerBarricadeMannequinUI.addButton;
			if (PlayerBarricadeMannequinUI.<>f__mg$cache1 == null)
			{
				PlayerBarricadeMannequinUI.<>f__mg$cache1 = new ClickedButton(PlayerBarricadeMannequinUI.onClickedAddButton);
			}
			sleekButton2.onClickedButton = PlayerBarricadeMannequinUI.<>f__mg$cache1;
			PlayerBarricadeMannequinUI.container.add(PlayerBarricadeMannequinUI.addButton);
			PlayerBarricadeMannequinUI.removeButton = new SleekButton();
			PlayerBarricadeMannequinUI.removeButton.positionOffset_X = -100;
			PlayerBarricadeMannequinUI.removeButton.positionOffset_Y = -55;
			PlayerBarricadeMannequinUI.removeButton.positionScale_X = 0.5f;
			PlayerBarricadeMannequinUI.removeButton.positionScale_Y = 0.5f;
			PlayerBarricadeMannequinUI.removeButton.sizeOffset_X = 200;
			PlayerBarricadeMannequinUI.removeButton.sizeOffset_Y = 30;
			PlayerBarricadeMannequinUI.removeButton.tooltip = PlayerBarricadeMannequinUI.localization.format("Remove_Button_Tooltip");
			SleekButton sleekButton3 = PlayerBarricadeMannequinUI.removeButton;
			if (PlayerBarricadeMannequinUI.<>f__mg$cache2 == null)
			{
				PlayerBarricadeMannequinUI.<>f__mg$cache2 = new ClickedButton(PlayerBarricadeMannequinUI.onClickedRemoveButton);
			}
			sleekButton3.onClickedButton = PlayerBarricadeMannequinUI.<>f__mg$cache2;
			PlayerBarricadeMannequinUI.container.add(PlayerBarricadeMannequinUI.removeButton);
			PlayerBarricadeMannequinUI.swapButton = new SleekButton();
			PlayerBarricadeMannequinUI.swapButton.positionOffset_X = -100;
			PlayerBarricadeMannequinUI.swapButton.positionOffset_Y = -15;
			PlayerBarricadeMannequinUI.swapButton.positionScale_X = 0.5f;
			PlayerBarricadeMannequinUI.swapButton.positionScale_Y = 0.5f;
			PlayerBarricadeMannequinUI.swapButton.sizeOffset_X = 200;
			PlayerBarricadeMannequinUI.swapButton.sizeOffset_Y = 30;
			PlayerBarricadeMannequinUI.swapButton.text = PlayerBarricadeMannequinUI.localization.format("Swap_Button");
			PlayerBarricadeMannequinUI.swapButton.tooltip = PlayerBarricadeMannequinUI.localization.format("Swap_Button_Tooltip");
			SleekButton sleekButton4 = PlayerBarricadeMannequinUI.swapButton;
			if (PlayerBarricadeMannequinUI.<>f__mg$cache3 == null)
			{
				PlayerBarricadeMannequinUI.<>f__mg$cache3 = new ClickedButton(PlayerBarricadeMannequinUI.onClickedSwapButton);
			}
			sleekButton4.onClickedButton = PlayerBarricadeMannequinUI.<>f__mg$cache3;
			PlayerBarricadeMannequinUI.container.add(PlayerBarricadeMannequinUI.swapButton);
			PlayerBarricadeMannequinUI.poseButton = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(PlayerBarricadeMannequinUI.localization.format("T")),
				new GUIContent(PlayerBarricadeMannequinUI.localization.format("Classic")),
				new GUIContent(PlayerBarricadeMannequinUI.localization.format("Lie"))
			});
			PlayerBarricadeMannequinUI.poseButton.positionOffset_X = -100;
			PlayerBarricadeMannequinUI.poseButton.positionOffset_Y = 25;
			PlayerBarricadeMannequinUI.poseButton.positionScale_X = 0.5f;
			PlayerBarricadeMannequinUI.poseButton.positionScale_Y = 0.5f;
			PlayerBarricadeMannequinUI.poseButton.sizeOffset_X = 200;
			PlayerBarricadeMannequinUI.poseButton.sizeOffset_Y = 30;
			PlayerBarricadeMannequinUI.poseButton.tooltip = PlayerBarricadeMannequinUI.localization.format("Pose_Button_Tooltip");
			SleekButtonState sleekButtonState = PlayerBarricadeMannequinUI.poseButton;
			if (PlayerBarricadeMannequinUI.<>f__mg$cache4 == null)
			{
				PlayerBarricadeMannequinUI.<>f__mg$cache4 = new SwappedState(PlayerBarricadeMannequinUI.onSwappedPoseState);
			}
			sleekButtonState.onSwappedState = PlayerBarricadeMannequinUI.<>f__mg$cache4;
			PlayerBarricadeMannequinUI.container.add(PlayerBarricadeMannequinUI.poseButton);
			PlayerBarricadeMannequinUI.mirrorButton = new SleekButton();
			PlayerBarricadeMannequinUI.mirrorButton.positionOffset_X = -100;
			PlayerBarricadeMannequinUI.mirrorButton.positionOffset_Y = 65;
			PlayerBarricadeMannequinUI.mirrorButton.positionScale_X = 0.5f;
			PlayerBarricadeMannequinUI.mirrorButton.positionScale_Y = 0.5f;
			PlayerBarricadeMannequinUI.mirrorButton.sizeOffset_X = 200;
			PlayerBarricadeMannequinUI.mirrorButton.sizeOffset_Y = 30;
			PlayerBarricadeMannequinUI.mirrorButton.text = PlayerBarricadeMannequinUI.localization.format("Mirror_Button");
			PlayerBarricadeMannequinUI.mirrorButton.tooltip = PlayerBarricadeMannequinUI.localization.format("Mirror_Button_Tooltip");
			SleekButton sleekButton5 = PlayerBarricadeMannequinUI.mirrorButton;
			if (PlayerBarricadeMannequinUI.<>f__mg$cache5 == null)
			{
				PlayerBarricadeMannequinUI.<>f__mg$cache5 = new ClickedButton(PlayerBarricadeMannequinUI.onClickedMirrorButton);
			}
			sleekButton5.onClickedButton = PlayerBarricadeMannequinUI.<>f__mg$cache5;
			PlayerBarricadeMannequinUI.container.add(PlayerBarricadeMannequinUI.mirrorButton);
			PlayerBarricadeMannequinUI.cancelButton = new SleekButton();
			PlayerBarricadeMannequinUI.cancelButton.positionOffset_X = -100;
			PlayerBarricadeMannequinUI.cancelButton.positionOffset_Y = 105;
			PlayerBarricadeMannequinUI.cancelButton.positionScale_X = 0.5f;
			PlayerBarricadeMannequinUI.cancelButton.positionScale_Y = 0.5f;
			PlayerBarricadeMannequinUI.cancelButton.sizeOffset_X = 200;
			PlayerBarricadeMannequinUI.cancelButton.sizeOffset_Y = 30;
			PlayerBarricadeMannequinUI.cancelButton.text = PlayerBarricadeMannequinUI.localization.format("Cancel_Button");
			PlayerBarricadeMannequinUI.cancelButton.tooltip = PlayerBarricadeMannequinUI.localization.format("Cancel_Button_Tooltip");
			SleekButton sleekButton6 = PlayerBarricadeMannequinUI.cancelButton;
			if (PlayerBarricadeMannequinUI.<>f__mg$cache6 == null)
			{
				PlayerBarricadeMannequinUI.<>f__mg$cache6 = new ClickedButton(PlayerBarricadeMannequinUI.onClickedCancelButton);
			}
			sleekButton6.onClickedButton = PlayerBarricadeMannequinUI.<>f__mg$cache6;
			PlayerBarricadeMannequinUI.container.add(PlayerBarricadeMannequinUI.cancelButton);
		}

		// Token: 0x060037D1 RID: 14289 RVA: 0x0018BB08 File Offset: 0x00189F08
		public static void open(InteractableMannequin newMannequin)
		{
			if (PlayerBarricadeMannequinUI.active)
			{
				return;
			}
			PlayerBarricadeMannequinUI.active = true;
			PlayerBarricadeMannequinUI.mannequin = newMannequin;
			PlayerBarricadeMannequinUI.addButton.text = PlayerBarricadeMannequinUI.localization.format("Add_Button", new object[]
			{
				MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.other)
			});
			PlayerBarricadeMannequinUI.removeButton.text = PlayerBarricadeMannequinUI.localization.format("Remove_Button", new object[]
			{
				MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.other)
			});
			if (PlayerBarricadeMannequinUI.mannequin != null)
			{
				PlayerBarricadeMannequinUI.poseButton.state = (int)PlayerBarricadeMannequinUI.mannequin.pose;
			}
			PlayerBarricadeMannequinUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x060037D2 RID: 14290 RVA: 0x0018BBC2 File Offset: 0x00189FC2
		public static void close()
		{
			if (!PlayerBarricadeMannequinUI.active)
			{
				return;
			}
			PlayerBarricadeMannequinUI.active = false;
			PlayerBarricadeMannequinUI.mannequin = null;
			PlayerBarricadeMannequinUI.container.lerpPositionScale(0f, 1f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x060037D3 RID: 14291 RVA: 0x0018BBF5 File Offset: 0x00189FF5
		private static void onClickedCosmeticsButton(SleekButton button)
		{
			if (PlayerBarricadeMannequinUI.mannequin != null)
			{
				BarricadeManager.updateMannequin(PlayerBarricadeMannequinUI.mannequin.transform, EMannequinUpdateMode.COSMETICS);
			}
			PlayerLifeUI.open();
			PlayerBarricadeMannequinUI.close();
		}

		// Token: 0x060037D4 RID: 14292 RVA: 0x0018BC21 File Offset: 0x0018A021
		private static void onClickedAddButton(SleekButton button)
		{
			if (PlayerBarricadeMannequinUI.mannequin != null)
			{
				BarricadeManager.updateMannequin(PlayerBarricadeMannequinUI.mannequin.transform, EMannequinUpdateMode.ADD);
			}
			PlayerLifeUI.open();
			PlayerBarricadeMannequinUI.close();
		}

		// Token: 0x060037D5 RID: 14293 RVA: 0x0018BC4D File Offset: 0x0018A04D
		private static void onClickedRemoveButton(SleekButton button)
		{
			if (PlayerBarricadeMannequinUI.mannequin != null)
			{
				BarricadeManager.updateMannequin(PlayerBarricadeMannequinUI.mannequin.transform, EMannequinUpdateMode.REMOVE);
			}
			PlayerLifeUI.open();
			PlayerBarricadeMannequinUI.close();
		}

		// Token: 0x060037D6 RID: 14294 RVA: 0x0018BC79 File Offset: 0x0018A079
		private static void onClickedSwapButton(SleekButton button)
		{
			if (PlayerBarricadeMannequinUI.mannequin != null)
			{
				BarricadeManager.updateMannequin(PlayerBarricadeMannequinUI.mannequin.transform, EMannequinUpdateMode.SWAP);
			}
			PlayerLifeUI.open();
			PlayerBarricadeMannequinUI.close();
		}

		// Token: 0x060037D7 RID: 14295 RVA: 0x0018BCA8 File Offset: 0x0018A0A8
		private static void onSwappedPoseState(SleekButtonState button, int index)
		{
			if (PlayerBarricadeMannequinUI.mannequin != null)
			{
				byte comp = PlayerBarricadeMannequinUI.mannequin.getComp(PlayerBarricadeMannequinUI.mannequin.mirror, (byte)index);
				BarricadeManager.poseMannequin(PlayerBarricadeMannequinUI.mannequin.transform, comp);
			}
		}

		// Token: 0x060037D8 RID: 14296 RVA: 0x0018BCEC File Offset: 0x0018A0EC
		private static void onClickedMirrorButton(SleekButton button)
		{
			if (PlayerBarricadeMannequinUI.mannequin != null)
			{
				bool flag = PlayerBarricadeMannequinUI.mannequin.mirror;
				flag = !flag;
				byte comp = PlayerBarricadeMannequinUI.mannequin.getComp(flag, PlayerBarricadeMannequinUI.mannequin.pose);
				BarricadeManager.poseMannequin(PlayerBarricadeMannequinUI.mannequin.transform, comp);
			}
		}

		// Token: 0x060037D9 RID: 14297 RVA: 0x0018BD3F File Offset: 0x0018A13F
		private static void onClickedCancelButton(SleekButton button)
		{
			PlayerLifeUI.open();
			PlayerBarricadeMannequinUI.close();
		}

		// Token: 0x04002998 RID: 10648
		private static Sleek container;

		// Token: 0x04002999 RID: 10649
		private static Local localization;

		// Token: 0x0400299A RID: 10650
		public static bool active;

		// Token: 0x0400299B RID: 10651
		private static InteractableMannequin mannequin;

		// Token: 0x0400299C RID: 10652
		private static SleekButton cosmeticsButton;

		// Token: 0x0400299D RID: 10653
		private static SleekButton addButton;

		// Token: 0x0400299E RID: 10654
		private static SleekButton removeButton;

		// Token: 0x0400299F RID: 10655
		private static SleekButton swapButton;

		// Token: 0x040029A0 RID: 10656
		private static SleekButtonState poseButton;

		// Token: 0x040029A1 RID: 10657
		private static SleekButton mirrorButton;

		// Token: 0x040029A2 RID: 10658
		private static SleekButton cancelButton;

		// Token: 0x040029A3 RID: 10659
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache0;

		// Token: 0x040029A4 RID: 10660
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache1;

		// Token: 0x040029A5 RID: 10661
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache2;

		// Token: 0x040029A6 RID: 10662
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache3;

		// Token: 0x040029A7 RID: 10663
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache4;

		// Token: 0x040029A8 RID: 10664
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache5;

		// Token: 0x040029A9 RID: 10665
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache6;
	}
}
