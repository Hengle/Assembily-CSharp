using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000799 RID: 1945
	public class PlayerDashboardSkillsUI
	{
		// Token: 0x0600386E RID: 14446 RVA: 0x001981F4 File Offset: 0x001965F4
		public PlayerDashboardSkillsUI()
		{
			if (PlayerDashboardSkillsUI.icons != null)
			{
				PlayerDashboardSkillsUI.icons.unload();
			}
			PlayerDashboardSkillsUI.localization = Localization.read("/Player/PlayerDashboardSkills.dat");
			PlayerDashboardSkillsUI.icons = Bundles.getBundle("/Bundles/Textures/Player/Icons/PlayerDashboardSkills/PlayerDashboardSkills.unity3d");
			PlayerDashboardSkillsUI.container = new Sleek();
			PlayerDashboardSkillsUI.container.positionScale_Y = 1f;
			PlayerDashboardSkillsUI.container.positionOffset_X = 10;
			PlayerDashboardSkillsUI.container.positionOffset_Y = 10;
			PlayerDashboardSkillsUI.container.sizeOffset_X = -20;
			PlayerDashboardSkillsUI.container.sizeOffset_Y = -20;
			PlayerDashboardSkillsUI.container.sizeScale_X = 1f;
			PlayerDashboardSkillsUI.container.sizeScale_Y = 1f;
			PlayerUI.container.add(PlayerDashboardSkillsUI.container);
			PlayerDashboardSkillsUI.active = false;
			PlayerDashboardSkillsUI.selectedSpeciality = byte.MaxValue;
			PlayerDashboardSkillsUI.backdropBox = new SleekBox();
			PlayerDashboardSkillsUI.backdropBox.positionOffset_Y = 60;
			PlayerDashboardSkillsUI.backdropBox.sizeOffset_Y = -60;
			PlayerDashboardSkillsUI.backdropBox.sizeScale_X = 1f;
			PlayerDashboardSkillsUI.backdropBox.sizeScale_Y = 1f;
			Color white = Color.white;
			white.a = 0.5f;
			PlayerDashboardSkillsUI.backdropBox.backgroundColor = white;
			PlayerDashboardSkillsUI.container.add(PlayerDashboardSkillsUI.backdropBox);
			PlayerDashboardSkillsUI.experienceBox = new SleekBox();
			PlayerDashboardSkillsUI.experienceBox.positionOffset_X = 10;
			PlayerDashboardSkillsUI.experienceBox.positionOffset_Y = -90;
			PlayerDashboardSkillsUI.experienceBox.positionScale_Y = 1f;
			PlayerDashboardSkillsUI.experienceBox.sizeOffset_X = -15;
			PlayerDashboardSkillsUI.experienceBox.sizeOffset_Y = 80;
			PlayerDashboardSkillsUI.experienceBox.sizeScale_X = 0.5f;
			PlayerDashboardSkillsUI.experienceBox.fontSize = 14;
			PlayerDashboardSkillsUI.experienceBox.foregroundColor = Palette.COLOR_Y;
			PlayerDashboardSkillsUI.experienceBox.foregroundTint = ESleekTint.NONE;
			PlayerDashboardSkillsUI.backdropBox.add(PlayerDashboardSkillsUI.experienceBox);
			for (int i = 0; i < (int)PlayerSkills.SPECIALITIES; i++)
			{
				SleekButtonIcon sleekButtonIcon = new SleekButtonIcon((Texture2D)PlayerDashboardSkillsUI.icons.load("Speciality_" + i));
				sleekButtonIcon.positionOffset_X = -85 + i * 60;
				sleekButtonIcon.positionOffset_Y = 10;
				sleekButtonIcon.positionScale_X = 0.5f;
				sleekButtonIcon.sizeOffset_X = 50;
				sleekButtonIcon.sizeOffset_Y = 50;
				sleekButtonIcon.tooltip = PlayerDashboardSkillsUI.localization.format("Speciality_" + i + "_Tooltip");
				sleekButtonIcon.iconImage.backgroundTint = ESleekTint.FOREGROUND;
				SleekButton sleekButton = sleekButtonIcon;
				if (PlayerDashboardSkillsUI.<>f__mg$cache2 == null)
				{
					PlayerDashboardSkillsUI.<>f__mg$cache2 = new ClickedButton(PlayerDashboardSkillsUI.onClickedSpecialityButton);
				}
				sleekButton.onClickedButton = PlayerDashboardSkillsUI.<>f__mg$cache2;
				PlayerDashboardSkillsUI.backdropBox.add(sleekButtonIcon);
			}
			PlayerDashboardSkillsUI.skillsScrollBox = new SleekScrollBox();
			PlayerDashboardSkillsUI.skillsScrollBox.positionOffset_X = 10;
			PlayerDashboardSkillsUI.skillsScrollBox.positionOffset_Y = 70;
			PlayerDashboardSkillsUI.skillsScrollBox.sizeOffset_X = -20;
			PlayerDashboardSkillsUI.skillsScrollBox.sizeOffset_Y = -170;
			PlayerDashboardSkillsUI.skillsScrollBox.sizeScale_X = 1f;
			PlayerDashboardSkillsUI.skillsScrollBox.sizeScale_Y = 1f;
			PlayerDashboardSkillsUI.backdropBox.add(PlayerDashboardSkillsUI.skillsScrollBox);
			PlayerDashboardSkillsUI.updateSelection(0);
			PlayerSkills playerSkills = Player.player.skills;
			Delegate onExperienceUpdated = playerSkills.onExperienceUpdated;
			if (PlayerDashboardSkillsUI.<>f__mg$cache3 == null)
			{
				PlayerDashboardSkillsUI.<>f__mg$cache3 = new ExperienceUpdated(PlayerDashboardSkillsUI.onExperienceUpdated);
			}
			playerSkills.onExperienceUpdated = (ExperienceUpdated)Delegate.Combine(onExperienceUpdated, PlayerDashboardSkillsUI.<>f__mg$cache3);
			PlayerSkills playerSkills2 = Player.player.skills;
			Delegate onBoostUpdated = playerSkills2.onBoostUpdated;
			if (PlayerDashboardSkillsUI.<>f__mg$cache4 == null)
			{
				PlayerDashboardSkillsUI.<>f__mg$cache4 = new BoostUpdated(PlayerDashboardSkillsUI.onBoostUpdated);
			}
			playerSkills2.onBoostUpdated = (BoostUpdated)Delegate.Combine(onBoostUpdated, PlayerDashboardSkillsUI.<>f__mg$cache4);
			PlayerSkills playerSkills3 = Player.player.skills;
			Delegate onSkillsUpdated = playerSkills3.onSkillsUpdated;
			if (PlayerDashboardSkillsUI.<>f__mg$cache5 == null)
			{
				PlayerDashboardSkillsUI.<>f__mg$cache5 = new SkillsUpdated(PlayerDashboardSkillsUI.onSkillsUpdated);
			}
			playerSkills3.onSkillsUpdated = (SkillsUpdated)Delegate.Combine(onSkillsUpdated, PlayerDashboardSkillsUI.<>f__mg$cache5);
		}

		// Token: 0x0600386F RID: 14447 RVA: 0x001985A6 File Offset: 0x001969A6
		public static void open()
		{
			if (PlayerDashboardSkillsUI.active)
			{
				return;
			}
			PlayerDashboardSkillsUI.active = true;
			PlayerDashboardSkillsUI.updateSelection(PlayerDashboardSkillsUI.selectedSpeciality);
			PlayerDashboardSkillsUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x06003870 RID: 14448 RVA: 0x001985DD File Offset: 0x001969DD
		public static void close()
		{
			if (!PlayerDashboardSkillsUI.active)
			{
				return;
			}
			PlayerDashboardSkillsUI.active = false;
			PlayerDashboardSkillsUI.container.lerpPositionScale(0f, 1f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x06003871 RID: 14449 RVA: 0x0019860C File Offset: 0x00196A0C
		private static void updateSelection(byte specialityIndex)
		{
			PlayerDashboardSkillsUI.skills = Player.player.skills.skills[(int)specialityIndex];
			PlayerDashboardSkillsUI.skillsScrollBox.remove();
			PlayerDashboardSkillsUI.skillsScrollBox.area = new Rect(0f, 0f, 5f, (float)(PlayerDashboardSkillsUI.skills.Length * 90 - 10));
			byte b = 0;
			while ((int)b < PlayerDashboardSkillsUI.skills.Length)
			{
				Skill skill = PlayerDashboardSkillsUI.skills[(int)b];
				SleekSkill sleekSkill = new SleekSkill(specialityIndex, b, skill);
				sleekSkill.positionOffset_Y = (int)(b * 90);
				sleekSkill.sizeOffset_X = -30;
				sleekSkill.sizeOffset_Y = 80;
				sleekSkill.sizeScale_X = 1f;
				SleekButton sleekButton = sleekSkill;
				if (PlayerDashboardSkillsUI.<>f__mg$cache0 == null)
				{
					PlayerDashboardSkillsUI.<>f__mg$cache0 = new ClickedButton(PlayerDashboardSkillsUI.onClickedSkillButton);
				}
				sleekButton.onClickedButton = PlayerDashboardSkillsUI.<>f__mg$cache0;
				PlayerDashboardSkillsUI.skillsScrollBox.add(sleekSkill);
				b += 1;
			}
			if (PlayerDashboardSkillsUI.boostButton != null)
			{
				PlayerDashboardSkillsUI.backdropBox.remove(PlayerDashboardSkillsUI.boostButton);
			}
			PlayerDashboardSkillsUI.boostButton = new SleekBoost((byte)Player.player.skills.boost);
			PlayerDashboardSkillsUI.boostButton.positionOffset_X = 5;
			PlayerDashboardSkillsUI.boostButton.positionOffset_Y = -90;
			PlayerDashboardSkillsUI.boostButton.positionScale_X = 0.5f;
			PlayerDashboardSkillsUI.boostButton.positionScale_Y = 1f;
			PlayerDashboardSkillsUI.boostButton.sizeOffset_X = -15;
			PlayerDashboardSkillsUI.boostButton.sizeOffset_Y = 80;
			PlayerDashboardSkillsUI.boostButton.sizeScale_X = 0.5f;
			SleekButton sleekButton2 = PlayerDashboardSkillsUI.boostButton;
			if (PlayerDashboardSkillsUI.<>f__mg$cache1 == null)
			{
				PlayerDashboardSkillsUI.<>f__mg$cache1 = new ClickedButton(PlayerDashboardSkillsUI.onClickedBoostButton);
			}
			sleekButton2.onClickedButton = PlayerDashboardSkillsUI.<>f__mg$cache1;
			PlayerDashboardSkillsUI.backdropBox.add(PlayerDashboardSkillsUI.boostButton);
			PlayerDashboardSkillsUI.selectedSpeciality = specialityIndex;
		}

		// Token: 0x06003872 RID: 14450 RVA: 0x001987B0 File Offset: 0x00196BB0
		private static void onClickedSpecialityButton(SleekButton button)
		{
			byte specialityIndex = (byte)((button.positionOffset_X + 85) / 60);
			PlayerDashboardSkillsUI.updateSelection(specialityIndex);
		}

		// Token: 0x06003873 RID: 14451 RVA: 0x001987D1 File Offset: 0x00196BD1
		private static void onClickedBoostButton(SleekButton button)
		{
			if (Player.player.skills.experience >= PlayerSkills.BOOST_COST)
			{
				Player.player.skills.sendBoost();
			}
		}

		// Token: 0x06003874 RID: 14452 RVA: 0x001987FC File Offset: 0x00196BFC
		private static void onClickedSkillButton(SleekButton button)
		{
			byte b = (byte)(button.positionOffset_Y / 90);
			if (PlayerDashboardSkillsUI.skills[(int)b].level < PlayerDashboardSkillsUI.skills[(int)b].max && Player.player.skills.experience >= Player.player.skills.cost((int)PlayerDashboardSkillsUI.selectedSpeciality, (int)b))
			{
				Player.player.skills.sendUpgrade(PlayerDashboardSkillsUI.selectedSpeciality, b, Input.GetKey(ControlsSettings.other));
			}
		}

		// Token: 0x06003875 RID: 14453 RVA: 0x00198879 File Offset: 0x00196C79
		private static void onExperienceUpdated(uint newExperience)
		{
			PlayerDashboardSkillsUI.experienceBox.text = PlayerDashboardSkillsUI.localization.format("Experience", new object[]
			{
				newExperience.ToString()
			});
		}

		// Token: 0x06003876 RID: 14454 RVA: 0x001988AA File Offset: 0x00196CAA
		private static void onBoostUpdated(EPlayerBoost newBoost)
		{
			PlayerDashboardSkillsUI.updateSelection(PlayerDashboardSkillsUI.selectedSpeciality);
		}

		// Token: 0x06003877 RID: 14455 RVA: 0x001988B6 File Offset: 0x00196CB6
		private static void onSkillsUpdated()
		{
			PlayerDashboardSkillsUI.updateSelection(PlayerDashboardSkillsUI.selectedSpeciality);
		}

		// Token: 0x04002AA6 RID: 10918
		public static Local localization;

		// Token: 0x04002AA7 RID: 10919
		public static Bundle icons;

		// Token: 0x04002AA8 RID: 10920
		private static Sleek container;

		// Token: 0x04002AA9 RID: 10921
		public static bool active;

		// Token: 0x04002AAA RID: 10922
		private static SleekBox backdropBox;

		// Token: 0x04002AAB RID: 10923
		private static Skill[] skills;

		// Token: 0x04002AAC RID: 10924
		private static SleekScrollBox skillsScrollBox;

		// Token: 0x04002AAD RID: 10925
		private static SleekButton boostButton;

		// Token: 0x04002AAE RID: 10926
		private static SleekBox experienceBox;

		// Token: 0x04002AAF RID: 10927
		private static byte selectedSpeciality;

		// Token: 0x04002AB0 RID: 10928
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache0;

		// Token: 0x04002AB1 RID: 10929
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache1;

		// Token: 0x04002AB2 RID: 10930
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache2;

		// Token: 0x04002AB3 RID: 10931
		[CompilerGenerated]
		private static ExperienceUpdated <>f__mg$cache3;

		// Token: 0x04002AB4 RID: 10932
		[CompilerGenerated]
		private static BoostUpdated <>f__mg$cache4;

		// Token: 0x04002AB5 RID: 10933
		[CompilerGenerated]
		private static SkillsUpdated <>f__mg$cache5;
	}
}
