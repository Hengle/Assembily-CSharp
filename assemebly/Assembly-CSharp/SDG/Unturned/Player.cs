using System;
using System.Collections;
using System.Collections.Generic;
using SDG.Framework.Debug;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020005FB RID: 1531
	public class Player : MonoBehaviour
	{
		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06002A82 RID: 10882 RVA: 0x0010820C File Offset: 0x0010660C
		public static bool isLoading
		{
			get
			{
				return Player.isLoadingLife || Player.isLoadingInventory || Player.isLoadingClothing;
			}
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06002A83 RID: 10883 RVA: 0x0010822A File Offset: 0x0010662A
		public static Player player
		{
			get
			{
				return Player._player;
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06002A84 RID: 10884 RVA: 0x00108231 File Offset: 0x00106631
		public static Player instance
		{
			get
			{
				return Player.player;
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06002A85 RID: 10885 RVA: 0x00108238 File Offset: 0x00106638
		public SteamChannel channel
		{
			get
			{
				return this._channel;
			}
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06002A86 RID: 10886 RVA: 0x00108240 File Offset: 0x00106640
		public PlayerAnimator animator
		{
			get
			{
				return this._animator;
			}
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06002A87 RID: 10887 RVA: 0x00108248 File Offset: 0x00106648
		public PlayerClothing clothing
		{
			get
			{
				return this._clothing;
			}
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06002A88 RID: 10888 RVA: 0x00108250 File Offset: 0x00106650
		public PlayerInventory inventory
		{
			get
			{
				return this._inventory;
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06002A89 RID: 10889 RVA: 0x00108258 File Offset: 0x00106658
		public PlayerEquipment equipment
		{
			get
			{
				return this._equipment;
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06002A8A RID: 10890 RVA: 0x00108260 File Offset: 0x00106660
		public PlayerLife life
		{
			get
			{
				return this._life;
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06002A8B RID: 10891 RVA: 0x00108268 File Offset: 0x00106668
		public PlayerCrafting crafting
		{
			get
			{
				return this._crafting;
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06002A8C RID: 10892 RVA: 0x00108270 File Offset: 0x00106670
		public PlayerSkills skills
		{
			get
			{
				return this._skills;
			}
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06002A8D RID: 10893 RVA: 0x00108278 File Offset: 0x00106678
		public PlayerMovement movement
		{
			get
			{
				return this._movement;
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06002A8E RID: 10894 RVA: 0x00108280 File Offset: 0x00106680
		public PlayerLook look
		{
			get
			{
				return this._look;
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06002A8F RID: 10895 RVA: 0x00108288 File Offset: 0x00106688
		public PlayerStance stance
		{
			get
			{
				return this._stance;
			}
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06002A90 RID: 10896 RVA: 0x00108290 File Offset: 0x00106690
		public PlayerInput input
		{
			get
			{
				return this._input;
			}
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06002A91 RID: 10897 RVA: 0x00108298 File Offset: 0x00106698
		public PlayerVoice voice
		{
			get
			{
				return this._voice;
			}
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06002A92 RID: 10898 RVA: 0x001082A0 File Offset: 0x001066A0
		public PlayerInteract interact
		{
			get
			{
				return this._interact;
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06002A93 RID: 10899 RVA: 0x001082A8 File Offset: 0x001066A8
		public PlayerWorkzone workzone
		{
			get
			{
				return this._workzone;
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06002A94 RID: 10900 RVA: 0x001082B0 File Offset: 0x001066B0
		public PlayerQuests quests
		{
			get
			{
				return this._quests;
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06002A95 RID: 10901 RVA: 0x001082B8 File Offset: 0x001066B8
		public Transform first
		{
			get
			{
				return this._first;
			}
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06002A96 RID: 10902 RVA: 0x001082C0 File Offset: 0x001066C0
		public Transform third
		{
			get
			{
				return this._third;
			}
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06002A97 RID: 10903 RVA: 0x001082C8 File Offset: 0x001066C8
		public Transform character
		{
			get
			{
				return this._character;
			}
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06002A98 RID: 10904 RVA: 0x001082D0 File Offset: 0x001066D0
		public bool isSpotOn
		{
			get
			{
				return this.itemOn || this.headlampOn;
			}
		}

		// Token: 0x06002A99 RID: 10905 RVA: 0x001082E8 File Offset: 0x001066E8
		public void playSound(AudioClip clip, float volume, float pitch, float deviation)
		{
			if (clip == null || Dedicator.isDedicated)
			{
				return;
			}
			this.sound.spatialBlend = 1f;
			deviation = Mathf.Clamp01(deviation);
			this.sound.pitch = UnityEngine.Random.Range(pitch * (1f - deviation), pitch * (1f + deviation));
			this.sound.PlayOneShot(clip, volume);
		}

		// Token: 0x06002A9A RID: 10906 RVA: 0x00108356 File Offset: 0x00106756
		public void playSound(AudioClip clip, float pitch, float deviation)
		{
			this.playSound(clip, 1f, pitch, deviation);
		}

		// Token: 0x06002A9B RID: 10907 RVA: 0x00108366 File Offset: 0x00106766
		public void playSound(AudioClip clip, float volume)
		{
			this.playSound(clip, volume, 1f, 0.1f);
		}

		// Token: 0x06002A9C RID: 10908 RVA: 0x0010837A File Offset: 0x0010677A
		public void playSound(AudioClip clip)
		{
			this.playSound(clip, 1f, 1f, 0.1f);
		}

		// Token: 0x06002A9D RID: 10909 RVA: 0x00108394 File Offset: 0x00106794
		[SteamCall]
		public void tellScreenshotDestination(CSteamID steamID)
		{
			if (this.channel.checkServer(steamID))
			{
				this.channel.longBinaryData = true;
				byte[] array = (byte[])this.channel.read(Types.BYTE_ARRAY_TYPE);
				this.channel.longBinaryData = false;
				if (array.Length > 0)
				{
					if (Dedicator.isDedicated)
					{
						ReadWrite.writeBytes(string.Concat(new string[]
						{
							ReadWrite.PATH,
							ServerSavedata.directory,
							"/",
							Provider.serverID,
							"/Spy.jpg"
						}), false, false, array);
						ReadWrite.writeBytes(string.Concat(new object[]
						{
							ReadWrite.PATH,
							ServerSavedata.directory,
							"/",
							Provider.serverID,
							"/Spy/",
							this.channel.owner.playerID.steamID.m_SteamID,
							".jpg"
						}), false, false, array);
						if (this.onPlayerSpyReady != null)
						{
							this.onPlayerSpyReady(this.channel.owner.playerID.steamID, array);
						}
						PlayerSpyReady playerSpyReady = this.screenshotsCallbacks.Dequeue();
						if (playerSpyReady != null)
						{
							playerSpyReady(this.channel.owner.playerID.steamID, array);
						}
					}
					else
					{
						ReadWrite.writeBytes("/Spy.jpg", false, true, array);
						if (Player.onSpyReady != null)
						{
							Player.onSpyReady(this.channel.owner.playerID.steamID, array);
						}
					}
				}
			}
		}

		// Token: 0x06002A9E RID: 10910 RVA: 0x0010852C File Offset: 0x0010692C
		[SteamCall]
		public void tellScreenshotRelay(CSteamID steamID)
		{
			if (this.channel.checkOwner(steamID) && this.screenshotsExpected > 0)
			{
				this.screenshotsExpected--;
				if (this.screenshotsDestination == CSteamID.Nil)
				{
					this.tellScreenshotDestination(Provider.server);
				}
				else
				{
					this.channel.longBinaryData = true;
					byte[] array = (byte[])this.channel.read(Types.BYTE_ARRAY_TYPE);
					if (array.Length > 0)
					{
						this.channel.openWrite();
						this.channel.write(array);
						this.channel.closeWrite("tellScreenshotDestination", this.screenshotsDestination, ESteamPacket.UPDATE_RELIABLE_CHUNK_BUFFER);
					}
					this.channel.longBinaryData = false;
					ReadWrite.writeBytes(string.Concat(new object[]
					{
						ReadWrite.PATH,
						ServerSavedata.directory,
						"/",
						Provider.serverID,
						"/Spy/",
						this.channel.owner.playerID.steamID.m_SteamID,
						".jpg"
					}), false, false, array);
					if (this.onPlayerSpyReady != null)
					{
						this.onPlayerSpyReady(this.channel.owner.playerID.steamID, array);
					}
					PlayerSpyReady playerSpyReady = this.screenshotsCallbacks.Dequeue();
					if (playerSpyReady != null)
					{
						playerSpyReady(this.channel.owner.playerID.steamID, array);
					}
				}
			}
		}

		// Token: 0x06002A9F RID: 10911 RVA: 0x001086B4 File Offset: 0x00106AB4
		private IEnumerator takeScreenshot()
		{
			yield return new WaitForEndOfFrame();
			if (this.screenshotRaw != null && (this.screenshotRaw.width != Screen.width || this.screenshotRaw.height != Screen.height))
			{
				UnityEngine.Object.DestroyImmediate(this.screenshotRaw);
				this.screenshotRaw = null;
			}
			if (this.screenshotRaw == null)
			{
				this.screenshotRaw = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
				this.screenshotRaw.name = "Screenshot_Raw";
				this.screenshotRaw.hideFlags = HideFlags.HideAndDontSave;
			}
			this.screenshotRaw.ReadPixels(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), 0, 0, false);
			if (this.screenshotFinal == null)
			{
				this.screenshotFinal = new Texture2D(640, 480, TextureFormat.RGB24, false);
				this.screenshotFinal.name = "Screenshot_Final";
				this.screenshotFinal.hideFlags = HideFlags.HideAndDontSave;
			}
			Color[] oldColors = this.screenshotRaw.GetPixels();
			Color[] newColors = new Color[this.screenshotFinal.width * this.screenshotFinal.height];
			float widthRatio = (float)this.screenshotRaw.width / (float)this.screenshotFinal.width;
			float heightRatio = (float)this.screenshotRaw.height / (float)this.screenshotFinal.height;
			for (int i = 0; i < this.screenshotFinal.height; i++)
			{
				int num = (int)((float)i * heightRatio) * this.screenshotRaw.width;
				int num2 = i * this.screenshotFinal.width;
				for (int j = 0; j < this.screenshotFinal.width; j++)
				{
					int num3 = (int)((float)j * widthRatio);
					newColors[num2 + j] = oldColors[num + num3];
				}
			}
			this.screenshotFinal.SetPixels(newColors);
			byte[] data = this.screenshotFinal.EncodeToJPG(33);
			if (data.Length < 30000)
			{
				this.channel.longBinaryData = true;
				this.channel.openWrite();
				this.channel.write(data);
				this.channel.closeWrite("tellScreenshotRelay", ESteamCall.SERVER, ESteamPacket.UPDATE_RELIABLE_CHUNK_BUFFER);
				this.channel.longBinaryData = false;
			}
			yield break;
		}

		// Token: 0x06002AA0 RID: 10912 RVA: 0x001086CF File Offset: 0x00106ACF
		[SteamCall]
		public void askScreenshot(CSteamID steamID)
		{
			if (this.channel.checkServer(steamID))
			{
				base.StartCoroutine(this.takeScreenshot());
			}
		}

		// Token: 0x06002AA1 RID: 10913 RVA: 0x001086EF File Offset: 0x00106AEF
		public void sendScreenshot(CSteamID destination, PlayerSpyReady callback = null)
		{
			this.screenshotsExpected++;
			this.screenshotsDestination = destination;
			this.screenshotsCallbacks.Enqueue(callback);
			this.channel.send("askScreenshot", ESteamCall.OWNER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[0]);
		}

		// Token: 0x06002AA2 RID: 10914 RVA: 0x0010872B File Offset: 0x00106B2B
		[SteamCall]
		public void askBrowserRequest(CSteamID steamID, string msg, string url)
		{
			if (this.channel.checkServer(steamID))
			{
				PlayerBrowserRequestUI.open(msg, url);
				PlayerLifeUI.close();
			}
		}

		// Token: 0x06002AA3 RID: 10915 RVA: 0x0010874A File Offset: 0x00106B4A
		public void sendBrowserRequest(string msg, string url)
		{
			this.channel.send("askBrowserRequest", ESteamCall.OWNER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				msg,
				url
			});
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x06002AA4 RID: 10916 RVA: 0x0010876D File Offset: 0x00106B6D
		// (set) Token: 0x06002AA5 RID: 10917 RVA: 0x00108775 File Offset: 0x00106B75
		public bool wantsBattlEyeLogs { get; protected set; }

		// Token: 0x06002AA6 RID: 10918 RVA: 0x0010877E File Offset: 0x00106B7E
		[SteamCall]
		public void askRequestBattlEyeLogs(CSteamID steamID)
		{
			if (this.channel.checkOwner(steamID))
			{
				if (!this.channel.owner.isAdmin)
				{
					return;
				}
				this.wantsBattlEyeLogs = !this.wantsBattlEyeLogs;
			}
		}

		// Token: 0x06002AA7 RID: 10919 RVA: 0x001087B6 File Offset: 0x00106BB6
		[TerminalCommandMethod("sv.request_battleye_logs", "if admin toggle relaying serverside battleye logs")]
		public static void requestBattlEyeLogs()
		{
			TerminalUtility.printCommandPass("Sent BattlEye logs request");
			if (Player.player == null)
			{
				return;
			}
			Player.player.channel.send("askRequestBattlEyeLogs", ESteamCall.SERVER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[0]);
		}

		// Token: 0x06002AA8 RID: 10920 RVA: 0x001087F0 File Offset: 0x00106BF0
		[SteamCall]
		public void tellTerminalRelay(CSteamID steamID, string internalMessage, string internalCategory, string displayCategory)
		{
			if (this.channel.checkServer(steamID))
			{
				Terminal.print(internalMessage, null, internalCategory, displayCategory, true);
			}
		}

		// Token: 0x06002AA9 RID: 10921 RVA: 0x0010880E File Offset: 0x00106C0E
		public void sendTerminalRelay(string internalMessage, string internalCategory, string displayCategory)
		{
			this.channel.send("tellTerminalRelay", ESteamCall.OWNER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				internalMessage,
				internalCategory,
				displayCategory
			});
		}

		// Token: 0x06002AAA RID: 10922 RVA: 0x00108838 File Offset: 0x00106C38
		[SteamCall]
		public void askTeleport(CSteamID steamID, Vector3 position, byte angle)
		{
			if (this.channel.checkServer(steamID))
			{
				base.transform.position = position + new Vector3(0f, 0.5f, 0f);
				base.transform.rotation = Quaternion.Euler(0f, (float)(angle * 2), 0f);
				this.look.updateLook();
				this.movement.updateMovement();
				this.movement.isAllowed = true;
				if (this.onPlayerTeleported != null)
				{
					this.onPlayerTeleported(this, position);
				}
			}
		}

		// Token: 0x06002AAB RID: 10923 RVA: 0x001088D3 File Offset: 0x00106CD3
		public void sendTeleport(Vector3 position, byte angle)
		{
			this.channel.send("askTeleport", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				position,
				angle
			});
		}

		// Token: 0x06002AAC RID: 10924 RVA: 0x00108900 File Offset: 0x00106D00
		[SteamCall]
		public void tellStat(CSteamID steamID, byte newStat)
		{
			if (this.channel.checkServer(steamID))
			{
				if (newStat == 0)
				{
					return;
				}
				this.trackStat((EPlayerStat)newStat);
				int num11;
				if (newStat == 2)
				{
					int num;
					if (Provider.provider.statisticsService.userStatisticsService.getStatistic("Kills_Players", out num))
					{
						Provider.provider.statisticsService.userStatisticsService.setStatistic("Kills_Players", num + 1);
					}
				}
				else if (newStat == 1)
				{
					int num2;
					if (Provider.provider.statisticsService.userStatisticsService.getStatistic("Kills_Zombies_Normal", out num2))
					{
						Provider.provider.statisticsService.userStatisticsService.setStatistic("Kills_Zombies_Normal", num2 + 1);
					}
				}
				else if (newStat == 6)
				{
					int num3;
					if (Provider.provider.statisticsService.userStatisticsService.getStatistic("Kills_Zombies_Mega", out num3))
					{
						Provider.provider.statisticsService.userStatisticsService.setStatistic("Kills_Zombies_Mega", num3 + 1);
					}
				}
				else if (newStat == 3)
				{
					int num4;
					if (Provider.provider.statisticsService.userStatisticsService.getStatistic("Found_Items", out num4))
					{
						Provider.provider.statisticsService.userStatisticsService.setStatistic("Found_Items", num4 + 1);
					}
				}
				else if (newStat == 4)
				{
					int num5;
					if (Provider.provider.statisticsService.userStatisticsService.getStatistic("Found_Resources", out num5))
					{
						Provider.provider.statisticsService.userStatisticsService.setStatistic("Found_Resources", num5 + 1);
					}
				}
				else if (newStat == 8)
				{
					int num6;
					if (Provider.provider.statisticsService.userStatisticsService.getStatistic("Kills_Animals", out num6))
					{
						Provider.provider.statisticsService.userStatisticsService.setStatistic("Kills_Animals", num6 + 1);
					}
				}
				else if (newStat == 9)
				{
					int num7;
					if (Provider.provider.statisticsService.userStatisticsService.getStatistic("Found_Crafts", out num7))
					{
						Provider.provider.statisticsService.userStatisticsService.setStatistic("Found_Crafts", num7 + 1);
					}
				}
				else if (newStat == 10)
				{
					int num8;
					if (Provider.provider.statisticsService.userStatisticsService.getStatistic("Found_Fishes", out num8))
					{
						Provider.provider.statisticsService.userStatisticsService.setStatistic("Found_Fishes", num8 + 1);
					}
				}
				else if (newStat == 11)
				{
					int num9;
					if (Provider.provider.statisticsService.userStatisticsService.getStatistic("Found_Plants", out num9))
					{
						Provider.provider.statisticsService.userStatisticsService.setStatistic("Found_Plants", num9 + 1);
					}
				}
				else if (newStat == 16)
				{
					int num10;
					if (Provider.provider.statisticsService.userStatisticsService.getStatistic("Arena_Wins", out num10))
					{
						Provider.provider.statisticsService.userStatisticsService.setStatistic("Arena_Wins", num10 + 1);
					}
				}
				else if (newStat == 17 && Provider.provider.statisticsService.userStatisticsService.getStatistic("Found_Buildables", out num11))
				{
					Provider.provider.statisticsService.userStatisticsService.setStatistic("Found_Buildables", num11 + 1);
				}
			}
		}

		// Token: 0x06002AAD RID: 10925 RVA: 0x00108C59 File Offset: 0x00107059
		protected void trackStat(EPlayerStat stat)
		{
			if (this.equipment.isSelected && this.equipment.isEquipped)
			{
				this.channel.owner.incrementStatTrackerValue(this.equipment.itemID, stat);
			}
		}

		// Token: 0x06002AAE RID: 10926 RVA: 0x00108C98 File Offset: 0x00107098
		public void sendStat(EPlayerKill kill)
		{
			if (kill == EPlayerKill.PLAYER)
			{
				this.sendStat(EPlayerStat.KILLS_PLAYERS);
			}
			else if (kill == EPlayerKill.ZOMBIE)
			{
				this.sendStat(EPlayerStat.KILLS_ZOMBIES_NORMAL);
			}
			else if (kill == EPlayerKill.MEGA)
			{
				this.sendStat(EPlayerStat.KILLS_ZOMBIES_MEGA);
			}
			else if (kill == EPlayerKill.ANIMAL)
			{
				this.sendStat(EPlayerStat.KILLS_ANIMALS);
			}
			else if (kill == EPlayerKill.RESOURCE)
			{
				this.sendStat(EPlayerStat.FOUND_RESOURCES);
			}
		}

		// Token: 0x06002AAF RID: 10927 RVA: 0x00108CFF File Offset: 0x001070FF
		public void sendStat(EPlayerStat stat)
		{
			if (!this.channel.isOwner)
			{
				this.trackStat(stat);
			}
			this.channel.send("tellStat", ESteamCall.OWNER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				(byte)stat
			});
		}

		// Token: 0x06002AB0 RID: 10928 RVA: 0x00108D3B File Offset: 0x0010713B
		[SteamCall]
		public void askMessage(CSteamID steamID, byte message)
		{
			if (this.channel.checkServer(steamID))
			{
				PlayerUI.message((EPlayerMessage)message, string.Empty);
			}
		}

		// Token: 0x06002AB1 RID: 10929 RVA: 0x00108D59 File Offset: 0x00107159
		public void sendMessage(EPlayerMessage message)
		{
			this.channel.send("askMessage", ESteamCall.OWNER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
			{
				(byte)message
			});
		}

		// Token: 0x06002AB2 RID: 10930 RVA: 0x00108D7E File Offset: 0x0010717E
		public void updateSpot(bool on)
		{
			this.itemOn = on;
			this.updateLights();
		}

		// Token: 0x06002AB3 RID: 10931 RVA: 0x00108D90 File Offset: 0x00107190
		public void updateGlassesLights(bool on)
		{
			if (this.clothing.firstClothes != null && this.clothing.firstClothes.glassesModel != null)
			{
				Transform transform = this.clothing.firstClothes.glassesModel.FindChild("Model_0");
				if (transform != null)
				{
					Transform transform2 = transform.FindChild("Light");
					if (transform2 != null)
					{
						transform2.gameObject.SetActive(on);
					}
				}
			}
			if (this.clothing.thirdClothes != null && this.clothing.thirdClothes.glassesModel != null)
			{
				Transform transform3 = this.clothing.thirdClothes.glassesModel.FindChild("Model_0");
				if (transform3 != null)
				{
					Transform transform4 = transform3.FindChild("Light");
					if (transform4 != null)
					{
						transform4.gameObject.SetActive(on);
					}
				}
			}
			if (this.clothing.characterClothes != null && this.clothing.characterClothes.glassesModel != null)
			{
				Transform transform5 = this.clothing.characterClothes.glassesModel.FindChild("Model_0");
				if (transform5 != null)
				{
					Transform transform6 = transform5.FindChild("Light");
					if (transform6 != null)
					{
						transform6.gameObject.SetActive(on);
					}
				}
			}
		}

		// Token: 0x06002AB4 RID: 10932 RVA: 0x00108F17 File Offset: 0x00107317
		public void updateHeadlamp(bool on)
		{
			this.headlampOn = on;
			this.updateLights();
		}

		// Token: 0x06002AB5 RID: 10933 RVA: 0x00108F28 File Offset: 0x00107328
		private void updateLights()
		{
			if (!Dedicator.isDedicated)
			{
				if (this.channel.isOwner)
				{
					this.firstSpot.gameObject.SetActive(this.isSpotOn && Player.player.look.perspective == EPlayerPerspective.FIRST);
					this.thirdSpot.gameObject.SetActive(this.isSpotOn && Player.player.look.perspective == EPlayerPerspective.THIRD);
				}
				else
				{
					this.thirdSpot.gameObject.SetActive(this.isSpotOn);
				}
			}
		}

		// Token: 0x06002AB6 RID: 10934 RVA: 0x00108FCA File Offset: 0x001073CA
		private void onPerspectiveUpdated(EPlayerPerspective newPerspective)
		{
			if (this.isSpotOn)
			{
				this.updateLights();
			}
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06002AB7 RID: 10935 RVA: 0x00108FDD File Offset: 0x001073DD
		// (set) Token: 0x06002AB8 RID: 10936 RVA: 0x00108FE5 File Offset: 0x001073E5
		public float rateLimitedActionsCredits { get; protected set; }

		// Token: 0x06002AB9 RID: 10937 RVA: 0x00108FF0 File Offset: 0x001073F0
		public bool tryToPerformRateLimitedAction()
		{
			bool flag = this.rateLimitedActionsCredits < 1f;
			if (flag)
			{
				this.rateLimitedActionsCredits += 1f / this.maxRateLimitedActionsPerSecond;
			}
			return flag;
		}

		// Token: 0x06002ABA RID: 10938 RVA: 0x0010902D File Offset: 0x0010742D
		protected void updateRateLimiting()
		{
			this.rateLimitedActionsCredits -= Time.deltaTime;
			if (this.rateLimitedActionsCredits < 0f)
			{
				this.rateLimitedActionsCredits = 0f;
			}
		}

		// Token: 0x06002ABB RID: 10939 RVA: 0x0010905C File Offset: 0x0010745C
		private void Update()
		{
			if (Provider.isServer)
			{
				this.updateRateLimiting();
			}
		}

		// Token: 0x06002ABC RID: 10940 RVA: 0x00109070 File Offset: 0x00107470
		private void Start()
		{
			if (this.channel.isOwner)
			{
				Player._player = this;
				this._first = base.transform.FindChild("First");
				this._third = base.transform.FindChild("Third");
				this.first.gameObject.SetActive(true);
				this.third.gameObject.SetActive(true);
				this._character = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Characters/Inspect"))).transform;
				this.character.name = "Inspect";
				this.character.transform.position = new Vector3(256f, -256f, 0f);
				this.character.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
				this.firstSpot = MainCamera.instance.transform.FindChild("Spot");
				Player.isLoadingInventory = true;
				Player.isLoadingLife = true;
				Player.isLoadingClothing = true;
				PlayerLook look = this.look;
				look.onPerspectiveUpdated = (PerspectiveUpdated)Delegate.Combine(look.onPerspectiveUpdated, new PerspectiveUpdated(this.onPerspectiveUpdated));
			}
			else
			{
				this._first = null;
				this._third = base.transform.FindChild("Third");
				this.third.gameObject.SetActive(true);
			}
			this.thirdSpot = this.third.FindChild("Skeleton").FindChild("Spine").FindChild("Skull").FindChild("Spot");
		}

		// Token: 0x06002ABD RID: 10941 RVA: 0x00109214 File Offset: 0x00107614
		private void Awake()
		{
			this._channel = base.GetComponent<SteamChannel>();
			this.agro = 0;
			this._animator = base.GetComponent<PlayerAnimator>();
			this._clothing = base.GetComponent<PlayerClothing>();
			this._inventory = base.GetComponent<PlayerInventory>();
			this._equipment = base.GetComponent<PlayerEquipment>();
			this._life = base.GetComponent<PlayerLife>();
			this._crafting = base.GetComponent<PlayerCrafting>();
			this._skills = base.GetComponent<PlayerSkills>();
			this._movement = base.GetComponent<PlayerMovement>();
			this._look = base.GetComponent<PlayerLook>();
			this._stance = base.GetComponent<PlayerStance>();
			this._input = base.GetComponent<PlayerInput>();
			this._voice = base.GetComponent<PlayerVoice>();
			this._interact = base.GetComponent<PlayerInteract>();
			this._workzone = base.GetComponent<PlayerWorkzone>();
			this._quests = base.GetComponent<PlayerQuests>();
			Transform transform = base.transform.FindChild("Sound");
			if (transform != null)
			{
				this.sound = transform.GetComponent<AudioSource>();
			}
		}

		// Token: 0x06002ABE RID: 10942 RVA: 0x00109314 File Offset: 0x00107714
		private void OnDestroy()
		{
			if (this.screenshotRaw != null)
			{
				UnityEngine.Object.DestroyImmediate(this.screenshotRaw);
				this.screenshotRaw = null;
			}
			if (this.screenshotFinal != null)
			{
				UnityEngine.Object.DestroyImmediate(this.screenshotFinal);
				this.screenshotFinal = null;
			}
			if (this.channel != null && this.channel.isOwner)
			{
				Player.isLoadingInventory = false;
				Player.isLoadingLife = false;
				Player.isLoadingClothing = false;
				this.channel.owner.commitModifiedDynamicProps();
			}
		}

		// Token: 0x06002ABF RID: 10943 RVA: 0x001093AC File Offset: 0x001077AC
		public void save()
		{
			if (this.life.isDead)
			{
				if (PlayerSavedata.fileExists(this.channel.owner.playerID, "/Player/Player.dat"))
				{
					PlayerSavedata.deleteFile(this.channel.owner.playerID, "/Player/Player.dat");
				}
			}
			else
			{
				Block block = new Block();
				block.writeByte(Player.SAVEDATA_VERSION);
				block.writeSingleVector3(base.transform.position);
				block.writeByte((byte)(base.transform.rotation.eulerAngles.y / 2f));
				PlayerSavedata.writeBlock(this.channel.owner.playerID, "/Player/Player.dat", block);
			}
			this.clothing.save();
			this.inventory.save();
			this.life.save();
			this.skills.save();
			this.animator.save();
			this.quests.save();
		}

		// Token: 0x04001B60 RID: 7008
		public static readonly byte SAVEDATA_VERSION = 1;

		// Token: 0x04001B61 RID: 7009
		public static PlayerCreated onPlayerCreated;

		// Token: 0x04001B62 RID: 7010
		public PlayerTeleported onPlayerTeleported;

		// Token: 0x04001B63 RID: 7011
		public PlayerSpyReady onPlayerSpyReady;

		// Token: 0x04001B64 RID: 7012
		public static PlayerSpyReady onSpyReady;

		// Token: 0x04001B65 RID: 7013
		public static bool isLoadingInventory;

		// Token: 0x04001B66 RID: 7014
		public static bool isLoadingLife;

		// Token: 0x04001B67 RID: 7015
		public static bool isLoadingClothing;

		// Token: 0x04001B68 RID: 7016
		public int agro;

		// Token: 0x04001B69 RID: 7017
		private static Player _player;

		// Token: 0x04001B6A RID: 7018
		protected SteamChannel _channel;

		// Token: 0x04001B6B RID: 7019
		private PlayerAnimator _animator;

		// Token: 0x04001B6C RID: 7020
		private PlayerClothing _clothing;

		// Token: 0x04001B6D RID: 7021
		private PlayerInventory _inventory;

		// Token: 0x04001B6E RID: 7022
		private PlayerEquipment _equipment;

		// Token: 0x04001B6F RID: 7023
		private PlayerLife _life;

		// Token: 0x04001B70 RID: 7024
		private PlayerCrafting _crafting;

		// Token: 0x04001B71 RID: 7025
		private PlayerSkills _skills;

		// Token: 0x04001B72 RID: 7026
		private PlayerMovement _movement;

		// Token: 0x04001B73 RID: 7027
		private PlayerLook _look;

		// Token: 0x04001B74 RID: 7028
		private PlayerStance _stance;

		// Token: 0x04001B75 RID: 7029
		private PlayerInput _input;

		// Token: 0x04001B76 RID: 7030
		private PlayerVoice _voice;

		// Token: 0x04001B77 RID: 7031
		private PlayerInteract _interact;

		// Token: 0x04001B78 RID: 7032
		private PlayerWorkzone _workzone;

		// Token: 0x04001B79 RID: 7033
		private PlayerQuests _quests;

		// Token: 0x04001B7A RID: 7034
		private Transform _first;

		// Token: 0x04001B7B RID: 7035
		private Transform _third;

		// Token: 0x04001B7C RID: 7036
		private Transform _character;

		// Token: 0x04001B7D RID: 7037
		private Transform firstSpot;

		// Token: 0x04001B7E RID: 7038
		private Transform thirdSpot;

		// Token: 0x04001B7F RID: 7039
		private bool itemOn;

		// Token: 0x04001B80 RID: 7040
		private bool headlampOn;

		// Token: 0x04001B81 RID: 7041
		private AudioSource sound;

		// Token: 0x04001B82 RID: 7042
		private int screenshotsExpected;

		// Token: 0x04001B83 RID: 7043
		private CSteamID screenshotsDestination;

		// Token: 0x04001B84 RID: 7044
		private Queue<PlayerSpyReady> screenshotsCallbacks = new Queue<PlayerSpyReady>();

		// Token: 0x04001B85 RID: 7045
		private Texture2D screenshotRaw;

		// Token: 0x04001B86 RID: 7046
		private Texture2D screenshotFinal;

		// Token: 0x04001B88 RID: 7048
		public uint maxRateLimitedActionsPerSecond = 10u;
	}
}
