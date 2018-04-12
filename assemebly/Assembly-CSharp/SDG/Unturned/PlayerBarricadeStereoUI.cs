using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using SDG.Framework.Translations;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000792 RID: 1938
	public class PlayerBarricadeStereoUI
	{
		// Token: 0x060037E0 RID: 14304 RVA: 0x0018C1E8 File Offset: 0x0018A5E8
		public PlayerBarricadeStereoUI()
		{
			PlayerBarricadeStereoUI.localization = Localization.read("/Player/PlayerBarricadeStereo.dat");
			PlayerBarricadeStereoUI.container = new Sleek();
			PlayerBarricadeStereoUI.container.positionScale_Y = 1f;
			PlayerBarricadeStereoUI.container.positionOffset_X = 10;
			PlayerBarricadeStereoUI.container.positionOffset_Y = 10;
			PlayerBarricadeStereoUI.container.sizeOffset_X = -20;
			PlayerBarricadeStereoUI.container.sizeOffset_Y = -20;
			PlayerBarricadeStereoUI.container.sizeScale_X = 1f;
			PlayerBarricadeStereoUI.container.sizeScale_Y = 1f;
			PlayerUI.container.add(PlayerBarricadeStereoUI.container);
			PlayerBarricadeStereoUI.active = false;
			PlayerBarricadeStereoUI.stereo = null;
			PlayerBarricadeStereoUI.stopButton = new SleekButton();
			PlayerBarricadeStereoUI.stopButton.positionOffset_X = -200;
			PlayerBarricadeStereoUI.stopButton.positionOffset_Y = 5;
			PlayerBarricadeStereoUI.stopButton.positionScale_X = 0.5f;
			PlayerBarricadeStereoUI.stopButton.positionScale_Y = 0.9f;
			PlayerBarricadeStereoUI.stopButton.sizeOffset_X = 195;
			PlayerBarricadeStereoUI.stopButton.sizeOffset_Y = 30;
			PlayerBarricadeStereoUI.stopButton.text = PlayerBarricadeStereoUI.localization.format("Stop_Button");
			PlayerBarricadeStereoUI.stopButton.tooltip = PlayerBarricadeStereoUI.localization.format("Stop_Button_Tooltip");
			SleekButton sleekButton = PlayerBarricadeStereoUI.stopButton;
			if (PlayerBarricadeStereoUI.<>f__mg$cache1 == null)
			{
				PlayerBarricadeStereoUI.<>f__mg$cache1 = new ClickedButton(PlayerBarricadeStereoUI.onClickedStopButton);
			}
			sleekButton.onClickedButton = PlayerBarricadeStereoUI.<>f__mg$cache1;
			PlayerBarricadeStereoUI.container.add(PlayerBarricadeStereoUI.stopButton);
			PlayerBarricadeStereoUI.closeButton = new SleekButton();
			PlayerBarricadeStereoUI.closeButton.positionOffset_X = 5;
			PlayerBarricadeStereoUI.closeButton.positionOffset_Y = 5;
			PlayerBarricadeStereoUI.closeButton.positionScale_X = 0.5f;
			PlayerBarricadeStereoUI.closeButton.positionScale_Y = 0.9f;
			PlayerBarricadeStereoUI.closeButton.sizeOffset_X = 195;
			PlayerBarricadeStereoUI.closeButton.sizeOffset_Y = 30;
			PlayerBarricadeStereoUI.closeButton.text = PlayerBarricadeStereoUI.localization.format("Close_Button");
			PlayerBarricadeStereoUI.closeButton.tooltip = PlayerBarricadeStereoUI.localization.format("Close_Button_Tooltip");
			SleekButton sleekButton2 = PlayerBarricadeStereoUI.closeButton;
			if (PlayerBarricadeStereoUI.<>f__mg$cache2 == null)
			{
				PlayerBarricadeStereoUI.<>f__mg$cache2 = new ClickedButton(PlayerBarricadeStereoUI.onClickedCloseButton);
			}
			sleekButton2.onClickedButton = PlayerBarricadeStereoUI.<>f__mg$cache2;
			PlayerBarricadeStereoUI.container.add(PlayerBarricadeStereoUI.closeButton);
			PlayerBarricadeStereoUI.volumeSlider = new SleekSlider();
			PlayerBarricadeStereoUI.volumeSlider.positionOffset_X = -200;
			PlayerBarricadeStereoUI.volumeSlider.positionOffset_Y = -25;
			PlayerBarricadeStereoUI.volumeSlider.positionScale_X = 0.5f;
			PlayerBarricadeStereoUI.volumeSlider.positionScale_Y = 0.1f;
			PlayerBarricadeStereoUI.volumeSlider.sizeOffset_X = 250;
			PlayerBarricadeStereoUI.volumeSlider.sizeOffset_Y = 20;
			PlayerBarricadeStereoUI.volumeSlider.orientation = ESleekOrientation.HORIZONTAL;
			SleekSlider sleekSlider = PlayerBarricadeStereoUI.volumeSlider;
			if (PlayerBarricadeStereoUI.<>f__mg$cache3 == null)
			{
				PlayerBarricadeStereoUI.<>f__mg$cache3 = new Dragged(PlayerBarricadeStereoUI.onDraggedVolumeSlider);
			}
			sleekSlider.onDragged = PlayerBarricadeStereoUI.<>f__mg$cache3;
			PlayerBarricadeStereoUI.volumeSlider.addLabel(string.Empty, ESleekSide.RIGHT);
			PlayerBarricadeStereoUI.container.add(PlayerBarricadeStereoUI.volumeSlider);
			PlayerBarricadeStereoUI.songsBox = new SleekScrollBox();
			PlayerBarricadeStereoUI.songsBox.positionOffset_X = -200;
			PlayerBarricadeStereoUI.songsBox.positionScale_X = 0.5f;
			PlayerBarricadeStereoUI.songsBox.positionScale_Y = 0.1f;
			PlayerBarricadeStereoUI.songsBox.sizeOffset_X = 400;
			PlayerBarricadeStereoUI.songsBox.sizeScale_Y = 0.8f;
			PlayerBarricadeStereoUI.container.add(PlayerBarricadeStereoUI.songsBox);
		}

		// Token: 0x060037E1 RID: 14305 RVA: 0x0018C528 File Offset: 0x0018A928
		public static void open(InteractableStereo newStereo)
		{
			if (PlayerBarricadeStereoUI.active)
			{
				PlayerBarricadeStereoUI.close();
				return;
			}
			PlayerBarricadeStereoUI.active = true;
			PlayerBarricadeStereoUI.stereo = newStereo;
			PlayerBarricadeStereoUI.refreshSongs();
			if (PlayerBarricadeStereoUI.stereo != null)
			{
				PlayerBarricadeStereoUI.volumeSlider.state = PlayerBarricadeStereoUI.stereo.volume;
			}
			PlayerBarricadeStereoUI.updateVolumeSliderLabel();
			PlayerBarricadeStereoUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x060037E2 RID: 14306 RVA: 0x0018C599 File Offset: 0x0018A999
		public static void close()
		{
			if (!PlayerBarricadeStereoUI.active)
			{
				return;
			}
			PlayerBarricadeStereoUI.active = false;
			PlayerBarricadeStereoUI.stereo = null;
			PlayerBarricadeStereoUI.container.lerpPositionScale(0f, 1f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x060037E3 RID: 14307 RVA: 0x0018C5CC File Offset: 0x0018A9CC
		private static void refreshSongs()
		{
			PlayerBarricadeStereoUI.songs.Clear();
			Assets.find<StereoSongAsset>(PlayerBarricadeStereoUI.songs);
			PlayerBarricadeStereoUI.songsBox.remove();
			PlayerBarricadeStereoUI.songsBox.area = new Rect(0f, 0f, 5f, (float)(PlayerBarricadeStereoUI.songs.Count * 30));
			for (int i = 0; i < PlayerBarricadeStereoUI.songs.Count; i++)
			{
				StereoSongAsset stereoSongAsset = PlayerBarricadeStereoUI.songs[i];
				SleekButton sleekButton = new SleekButton();
				sleekButton.positionOffset_Y = i * 30;
				sleekButton.sizeOffset_X = -30;
				sleekButton.sizeOffset_Y = 30;
				sleekButton.sizeScale_X = 1f;
				SleekButton sleekButton2 = sleekButton;
				Delegate onClickedButton = sleekButton2.onClickedButton;
				if (PlayerBarricadeStereoUI.<>f__mg$cache0 == null)
				{
					PlayerBarricadeStereoUI.<>f__mg$cache0 = new ClickedButton(PlayerBarricadeStereoUI.onClickedPlayButton);
				}
				sleekButton2.onClickedButton = (ClickedButton)Delegate.Combine(onClickedButton, PlayerBarricadeStereoUI.<>f__mg$cache0);
				PlayerBarricadeStereoUI.songsBox.add(sleekButton);
				TranslationLeaf leaf = Translator.getLeaf(stereoSongAsset.title);
				if (leaf != null)
				{
					sleekButton.text = leaf.text;
				}
			}
		}

		// Token: 0x060037E4 RID: 14308 RVA: 0x0018C6D4 File Offset: 0x0018AAD4
		private static void updateVolumeSliderLabel()
		{
			if (PlayerBarricadeStereoUI.stereo != null)
			{
				PlayerBarricadeStereoUI.volumeSlider.updateLabel(PlayerBarricadeStereoUI.localization.format("Volume_Slider_Label", new object[]
				{
					PlayerBarricadeStereoUI.stereo.compressedVolume
				}));
			}
		}

		// Token: 0x060037E5 RID: 14309 RVA: 0x0018C722 File Offset: 0x0018AB22
		private static void onDraggedVolumeSlider(SleekSlider slider, float state)
		{
			if (PlayerBarricadeStereoUI.stereo != null)
			{
				PlayerBarricadeStereoUI.stereo.volume = state;
				BarricadeManager.updateStereoVolume(PlayerBarricadeStereoUI.stereo.transform, PlayerBarricadeStereoUI.stereo.compressedVolume);
				PlayerBarricadeStereoUI.updateVolumeSliderLabel();
			}
		}

		// Token: 0x060037E6 RID: 14310 RVA: 0x0018C760 File Offset: 0x0018AB60
		private static void onClickedPlayButton(SleekButton button)
		{
			int num = PlayerBarricadeStereoUI.songsBox.search(button);
			if (num >= PlayerBarricadeStereoUI.songs.Count)
			{
				return;
			}
			StereoSongAsset stereoSongAsset = PlayerBarricadeStereoUI.songs[num];
			if (PlayerBarricadeStereoUI.stereo != null)
			{
				BarricadeManager.updateStereoTrack(PlayerBarricadeStereoUI.stereo.transform, stereoSongAsset.GUID);
			}
		}

		// Token: 0x060037E7 RID: 14311 RVA: 0x0018C7BB File Offset: 0x0018ABBB
		private static void onClickedStopButton(SleekButton button)
		{
			if (PlayerBarricadeStereoUI.stereo != null)
			{
				BarricadeManager.updateStereoTrack(PlayerBarricadeStereoUI.stereo.transform, Guid.Empty);
			}
		}

		// Token: 0x060037E8 RID: 14312 RVA: 0x0018C7E1 File Offset: 0x0018ABE1
		private static void onClickedCloseButton(SleekButton button)
		{
			PlayerLifeUI.open();
			PlayerBarricadeStereoUI.close();
		}

		// Token: 0x040029B3 RID: 10675
		private static List<StereoSongAsset> songs = new List<StereoSongAsset>();

		// Token: 0x040029B4 RID: 10676
		private static Sleek container;

		// Token: 0x040029B5 RID: 10677
		private static Local localization;

		// Token: 0x040029B6 RID: 10678
		public static bool active;

		// Token: 0x040029B7 RID: 10679
		private static InteractableStereo stereo;

		// Token: 0x040029B8 RID: 10680
		private static SleekButton stopButton;

		// Token: 0x040029B9 RID: 10681
		private static SleekButton closeButton;

		// Token: 0x040029BA RID: 10682
		private static SleekSlider volumeSlider;

		// Token: 0x040029BB RID: 10683
		private static SleekScrollBox songsBox;

		// Token: 0x040029BC RID: 10684
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache0;

		// Token: 0x040029BD RID: 10685
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache1;

		// Token: 0x040029BE RID: 10686
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache2;

		// Token: 0x040029BF RID: 10687
		[CompilerGenerated]
		private static Dragged <>f__mg$cache3;
	}
}
