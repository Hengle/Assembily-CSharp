using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200078F RID: 1935
	public class PlayerBarricadeLibraryUI
	{
		// Token: 0x060037C8 RID: 14280 RVA: 0x0018AB84 File Offset: 0x00188F84
		public PlayerBarricadeLibraryUI()
		{
			PlayerBarricadeLibraryUI.localization = Localization.read("/Player/PlayerBarricadeLibrary.dat");
			PlayerBarricadeLibraryUI.container = new Sleek();
			PlayerBarricadeLibraryUI.container.positionScale_Y = 1f;
			PlayerBarricadeLibraryUI.container.positionOffset_X = 10;
			PlayerBarricadeLibraryUI.container.positionOffset_Y = 10;
			PlayerBarricadeLibraryUI.container.sizeOffset_X = -20;
			PlayerBarricadeLibraryUI.container.sizeOffset_Y = -20;
			PlayerBarricadeLibraryUI.container.sizeScale_X = 1f;
			PlayerBarricadeLibraryUI.container.sizeScale_Y = 1f;
			PlayerUI.container.add(PlayerBarricadeLibraryUI.container);
			PlayerBarricadeLibraryUI.active = false;
			PlayerBarricadeLibraryUI.library = null;
			PlayerBarricadeLibraryUI.capacityBox = new SleekBox();
			PlayerBarricadeLibraryUI.capacityBox.positionOffset_X = -100;
			PlayerBarricadeLibraryUI.capacityBox.positionOffset_Y = -135;
			PlayerBarricadeLibraryUI.capacityBox.positionScale_X = 0.5f;
			PlayerBarricadeLibraryUI.capacityBox.positionScale_Y = 0.5f;
			PlayerBarricadeLibraryUI.capacityBox.sizeOffset_X = 200;
			PlayerBarricadeLibraryUI.capacityBox.sizeOffset_Y = 30;
			PlayerBarricadeLibraryUI.capacityBox.addLabel(PlayerBarricadeLibraryUI.localization.format("Capacity_Label"), ESleekSide.LEFT);
			PlayerBarricadeLibraryUI.container.add(PlayerBarricadeLibraryUI.capacityBox);
			PlayerBarricadeLibraryUI.walletBox = new SleekBox();
			PlayerBarricadeLibraryUI.walletBox.positionOffset_X = -100;
			PlayerBarricadeLibraryUI.walletBox.positionOffset_Y = -95;
			PlayerBarricadeLibraryUI.walletBox.positionScale_X = 0.5f;
			PlayerBarricadeLibraryUI.walletBox.positionScale_Y = 0.5f;
			PlayerBarricadeLibraryUI.walletBox.sizeOffset_X = 200;
			PlayerBarricadeLibraryUI.walletBox.sizeOffset_Y = 30;
			PlayerBarricadeLibraryUI.walletBox.addLabel(PlayerBarricadeLibraryUI.localization.format("Wallet_Label"), ESleekSide.LEFT);
			PlayerBarricadeLibraryUI.container.add(PlayerBarricadeLibraryUI.walletBox);
			PlayerBarricadeLibraryUI.amountField = new SleekUInt32Field();
			PlayerBarricadeLibraryUI.amountField.positionOffset_X = -100;
			PlayerBarricadeLibraryUI.amountField.positionOffset_Y = -15;
			PlayerBarricadeLibraryUI.amountField.positionScale_X = 0.5f;
			PlayerBarricadeLibraryUI.amountField.positionScale_Y = 0.5f;
			PlayerBarricadeLibraryUI.amountField.sizeOffset_X = 200;
			PlayerBarricadeLibraryUI.amountField.sizeOffset_Y = 30;
			PlayerBarricadeLibraryUI.amountField.addLabel(PlayerBarricadeLibraryUI.localization.format("Amount_Label"), ESleekSide.LEFT);
			SleekUInt32Field sleekUInt32Field = PlayerBarricadeLibraryUI.amountField;
			if (PlayerBarricadeLibraryUI.<>f__mg$cache0 == null)
			{
				PlayerBarricadeLibraryUI.<>f__mg$cache0 = new TypedUInt32(PlayerBarricadeLibraryUI.onTypedAmountField);
			}
			sleekUInt32Field.onTypedUInt32 = PlayerBarricadeLibraryUI.<>f__mg$cache0;
			PlayerBarricadeLibraryUI.amountField.foregroundTint = ESleekTint.NONE;
			PlayerBarricadeLibraryUI.container.add(PlayerBarricadeLibraryUI.amountField);
			PlayerBarricadeLibraryUI.transactionButton = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(PlayerBarricadeLibraryUI.localization.format("Deposit"), PlayerBarricadeLibraryUI.localization.format("Deposit_Tooltip")),
				new GUIContent(PlayerBarricadeLibraryUI.localization.format("Withdraw"), PlayerBarricadeLibraryUI.localization.format("Withdraw_Tooltip"))
			});
			PlayerBarricadeLibraryUI.transactionButton.positionOffset_X = -100;
			PlayerBarricadeLibraryUI.transactionButton.positionOffset_Y = -55;
			PlayerBarricadeLibraryUI.transactionButton.positionScale_X = 0.5f;
			PlayerBarricadeLibraryUI.transactionButton.positionScale_Y = 0.5f;
			PlayerBarricadeLibraryUI.transactionButton.sizeOffset_X = 200;
			PlayerBarricadeLibraryUI.transactionButton.sizeOffset_Y = 30;
			PlayerBarricadeLibraryUI.transactionButton.addLabel(PlayerBarricadeLibraryUI.localization.format("Transaction_Label"), ESleekSide.LEFT);
			SleekButtonState sleekButtonState = PlayerBarricadeLibraryUI.transactionButton;
			if (PlayerBarricadeLibraryUI.<>f__mg$cache1 == null)
			{
				PlayerBarricadeLibraryUI.<>f__mg$cache1 = new SwappedState(PlayerBarricadeLibraryUI.onSwappedTransactionState);
			}
			sleekButtonState.onSwappedState = PlayerBarricadeLibraryUI.<>f__mg$cache1;
			PlayerBarricadeLibraryUI.container.add(PlayerBarricadeLibraryUI.transactionButton);
			PlayerBarricadeLibraryUI.taxBox = new SleekBox();
			PlayerBarricadeLibraryUI.taxBox.positionOffset_X = -100;
			PlayerBarricadeLibraryUI.taxBox.positionOffset_Y = 25;
			PlayerBarricadeLibraryUI.taxBox.positionScale_X = 0.5f;
			PlayerBarricadeLibraryUI.taxBox.positionScale_Y = 0.5f;
			PlayerBarricadeLibraryUI.taxBox.sizeOffset_X = 200;
			PlayerBarricadeLibraryUI.taxBox.sizeOffset_Y = 30;
			PlayerBarricadeLibraryUI.taxBox.addLabel(PlayerBarricadeLibraryUI.localization.format("Tax_Label"), ESleekSide.LEFT);
			PlayerBarricadeLibraryUI.taxBox.foregroundTint = ESleekTint.NONE;
			PlayerBarricadeLibraryUI.container.add(PlayerBarricadeLibraryUI.taxBox);
			PlayerBarricadeLibraryUI.netBox = new SleekBox();
			PlayerBarricadeLibraryUI.netBox.positionOffset_X = -100;
			PlayerBarricadeLibraryUI.netBox.positionOffset_Y = 65;
			PlayerBarricadeLibraryUI.netBox.positionScale_X = 0.5f;
			PlayerBarricadeLibraryUI.netBox.positionScale_Y = 0.5f;
			PlayerBarricadeLibraryUI.netBox.sizeOffset_X = 200;
			PlayerBarricadeLibraryUI.netBox.sizeOffset_Y = 30;
			PlayerBarricadeLibraryUI.netBox.addLabel(PlayerBarricadeLibraryUI.localization.format("Net_Label"), ESleekSide.LEFT);
			PlayerBarricadeLibraryUI.netBox.foregroundTint = ESleekTint.NONE;
			PlayerBarricadeLibraryUI.container.add(PlayerBarricadeLibraryUI.netBox);
			PlayerBarricadeLibraryUI.yesButton = new SleekButton();
			PlayerBarricadeLibraryUI.yesButton.positionOffset_X = -100;
			PlayerBarricadeLibraryUI.yesButton.positionOffset_Y = 105;
			PlayerBarricadeLibraryUI.yesButton.positionScale_X = 0.5f;
			PlayerBarricadeLibraryUI.yesButton.positionScale_Y = 0.5f;
			PlayerBarricadeLibraryUI.yesButton.sizeOffset_X = 95;
			PlayerBarricadeLibraryUI.yesButton.sizeOffset_Y = 30;
			PlayerBarricadeLibraryUI.yesButton.text = PlayerBarricadeLibraryUI.localization.format("Yes_Button");
			PlayerBarricadeLibraryUI.yesButton.tooltip = PlayerBarricadeLibraryUI.localization.format("Yes_Button_Tooltip");
			SleekButton sleekButton = PlayerBarricadeLibraryUI.yesButton;
			if (PlayerBarricadeLibraryUI.<>f__mg$cache2 == null)
			{
				PlayerBarricadeLibraryUI.<>f__mg$cache2 = new ClickedButton(PlayerBarricadeLibraryUI.onClickedYesButton);
			}
			sleekButton.onClickedButton = PlayerBarricadeLibraryUI.<>f__mg$cache2;
			PlayerBarricadeLibraryUI.container.add(PlayerBarricadeLibraryUI.yesButton);
			PlayerBarricadeLibraryUI.noButton = new SleekButton();
			PlayerBarricadeLibraryUI.noButton.positionOffset_X = 5;
			PlayerBarricadeLibraryUI.noButton.positionOffset_Y = 105;
			PlayerBarricadeLibraryUI.noButton.positionScale_X = 0.5f;
			PlayerBarricadeLibraryUI.noButton.positionScale_Y = 0.5f;
			PlayerBarricadeLibraryUI.noButton.sizeOffset_X = 95;
			PlayerBarricadeLibraryUI.noButton.sizeOffset_Y = 30;
			PlayerBarricadeLibraryUI.noButton.text = PlayerBarricadeLibraryUI.localization.format("No_Button");
			PlayerBarricadeLibraryUI.noButton.tooltip = PlayerBarricadeLibraryUI.localization.format("No_Button_Tooltip");
			SleekButton sleekButton2 = PlayerBarricadeLibraryUI.noButton;
			if (PlayerBarricadeLibraryUI.<>f__mg$cache3 == null)
			{
				PlayerBarricadeLibraryUI.<>f__mg$cache3 = new ClickedButton(PlayerBarricadeLibraryUI.onClickedNoButton);
			}
			sleekButton2.onClickedButton = PlayerBarricadeLibraryUI.<>f__mg$cache3;
			PlayerBarricadeLibraryUI.container.add(PlayerBarricadeLibraryUI.noButton);
		}

		// Token: 0x060037C9 RID: 14281 RVA: 0x0018B184 File Offset: 0x00189584
		public static void open(InteractableLibrary newLibrary)
		{
			if (PlayerBarricadeLibraryUI.active)
			{
				return;
			}
			PlayerBarricadeLibraryUI.active = true;
			PlayerBarricadeLibraryUI.library = newLibrary;
			if (PlayerBarricadeLibraryUI.library != null)
			{
				PlayerBarricadeLibraryUI.capacityBox.text = PlayerBarricadeLibraryUI.localization.format("Capacity_Text", new object[]
				{
					PlayerBarricadeLibraryUI.library.amount,
					PlayerBarricadeLibraryUI.library.capacity
				});
				PlayerBarricadeLibraryUI.walletBox.text = Player.player.skills.experience.ToString();
				PlayerBarricadeLibraryUI.amountField.state = 0u;
				PlayerBarricadeLibraryUI.updateTax();
			}
			PlayerBarricadeLibraryUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x060037CA RID: 14282 RVA: 0x0018B24C File Offset: 0x0018964C
		public static void close()
		{
			if (!PlayerBarricadeLibraryUI.active)
			{
				return;
			}
			PlayerBarricadeLibraryUI.active = false;
			PlayerBarricadeLibraryUI.library = null;
			PlayerBarricadeLibraryUI.container.lerpPositionScale(0f, 1f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x060037CB RID: 14283 RVA: 0x0018B280 File Offset: 0x00189680
		private static void updateTax()
		{
			if (PlayerBarricadeLibraryUI.library != null)
			{
				if (PlayerBarricadeLibraryUI.transactionButton.state == 0)
				{
					PlayerBarricadeLibraryUI.tax = (uint)Math.Ceiling(PlayerBarricadeLibraryUI.amountField.state * ((double)PlayerBarricadeLibraryUI.library.tax / 100.0));
					PlayerBarricadeLibraryUI.net = PlayerBarricadeLibraryUI.amountField.state - PlayerBarricadeLibraryUI.tax;
					PlayerBarricadeLibraryUI.amountField.foregroundColor = ((PlayerBarricadeLibraryUI.amountField.state <= Player.player.skills.experience && PlayerBarricadeLibraryUI.net + PlayerBarricadeLibraryUI.library.amount <= PlayerBarricadeLibraryUI.library.capacity) ? Palette.COLOR_G : Palette.COLOR_R);
					PlayerBarricadeLibraryUI.taxBox.foregroundColor = PlayerBarricadeLibraryUI.amountField.foregroundColor;
					PlayerBarricadeLibraryUI.netBox.foregroundColor = PlayerBarricadeLibraryUI.amountField.foregroundColor;
				}
				else
				{
					PlayerBarricadeLibraryUI.tax = 0u;
					PlayerBarricadeLibraryUI.net = PlayerBarricadeLibraryUI.amountField.state - PlayerBarricadeLibraryUI.tax;
					PlayerBarricadeLibraryUI.amountField.foregroundColor = ((PlayerBarricadeLibraryUI.net <= PlayerBarricadeLibraryUI.library.amount) ? Palette.COLOR_G : Palette.COLOR_R);
					PlayerBarricadeLibraryUI.taxBox.foregroundColor = PlayerBarricadeLibraryUI.amountField.foregroundColor;
					PlayerBarricadeLibraryUI.netBox.foregroundColor = PlayerBarricadeLibraryUI.amountField.foregroundColor;
				}
			}
			PlayerBarricadeLibraryUI.taxBox.text = PlayerBarricadeLibraryUI.tax.ToString();
			PlayerBarricadeLibraryUI.netBox.text = PlayerBarricadeLibraryUI.net.ToString();
		}

		// Token: 0x060037CC RID: 14284 RVA: 0x0018B415 File Offset: 0x00189815
		private static void onTypedAmountField(SleekUInt32Field field, uint state)
		{
			PlayerBarricadeLibraryUI.updateTax();
		}

		// Token: 0x060037CD RID: 14285 RVA: 0x0018B41C File Offset: 0x0018981C
		private static void onSwappedTransactionState(SleekButtonState button, int index)
		{
			PlayerBarricadeLibraryUI.updateTax();
		}

		// Token: 0x060037CE RID: 14286 RVA: 0x0018B424 File Offset: 0x00189824
		private static void onClickedYesButton(SleekButton button)
		{
			if (PlayerBarricadeLibraryUI.library != null)
			{
				if (PlayerBarricadeLibraryUI.transactionButton.state == 0)
				{
					if (PlayerBarricadeLibraryUI.amountField.state > Player.player.skills.experience || PlayerBarricadeLibraryUI.net + PlayerBarricadeLibraryUI.library.amount > PlayerBarricadeLibraryUI.library.capacity)
					{
						return;
					}
				}
				else if (PlayerBarricadeLibraryUI.net > PlayerBarricadeLibraryUI.library.amount)
				{
					return;
				}
				if (PlayerBarricadeLibraryUI.net > 0u)
				{
					BarricadeManager.transferLibrary(PlayerBarricadeLibraryUI.library.transform, (byte)PlayerBarricadeLibraryUI.transactionButton.state, PlayerBarricadeLibraryUI.amountField.state);
				}
			}
			PlayerLifeUI.open();
			PlayerBarricadeLibraryUI.close();
		}

		// Token: 0x060037CF RID: 14287 RVA: 0x0018B4E1 File Offset: 0x001898E1
		private static void onClickedNoButton(SleekButton button)
		{
			PlayerLifeUI.open();
			PlayerBarricadeLibraryUI.close();
		}

		// Token: 0x04002986 RID: 10630
		private static Sleek container;

		// Token: 0x04002987 RID: 10631
		private static Local localization;

		// Token: 0x04002988 RID: 10632
		public static bool active;

		// Token: 0x04002989 RID: 10633
		private static InteractableLibrary library;

		// Token: 0x0400298A RID: 10634
		private static SleekBox capacityBox;

		// Token: 0x0400298B RID: 10635
		private static SleekBox walletBox;

		// Token: 0x0400298C RID: 10636
		private static SleekUInt32Field amountField;

		// Token: 0x0400298D RID: 10637
		private static SleekButtonState transactionButton;

		// Token: 0x0400298E RID: 10638
		private static SleekBox taxBox;

		// Token: 0x0400298F RID: 10639
		private static SleekBox netBox;

		// Token: 0x04002990 RID: 10640
		private static uint tax;

		// Token: 0x04002991 RID: 10641
		private static uint net;

		// Token: 0x04002992 RID: 10642
		private static SleekButton yesButton;

		// Token: 0x04002993 RID: 10643
		private static SleekButton noButton;

		// Token: 0x04002994 RID: 10644
		[CompilerGenerated]
		private static TypedUInt32 <>f__mg$cache0;

		// Token: 0x04002995 RID: 10645
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache1;

		// Token: 0x04002996 RID: 10646
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache2;

		// Token: 0x04002997 RID: 10647
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache3;
	}
}
