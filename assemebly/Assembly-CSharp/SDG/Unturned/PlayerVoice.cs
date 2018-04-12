using System;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200064D RID: 1613
	public class PlayerVoice : PlayerCaller
	{
		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06002E93 RID: 11923 RVA: 0x0012F696 File Offset: 0x0012DA96
		public bool canHearRadio
		{
			get
			{
				return this.hasWalkieTalkie || this.hasEarpiece;
			}
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06002E94 RID: 11924 RVA: 0x0012F6AC File Offset: 0x0012DAAC
		public bool hasEarpiece
		{
			get
			{
				return base.player.clothing != null && base.player.clothing.maskAsset != null && base.player.clothing.maskAsset.isEarpiece;
			}
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06002E95 RID: 11925 RVA: 0x0012F6FC File Offset: 0x0012DAFC
		// (set) Token: 0x06002E96 RID: 11926 RVA: 0x0012F704 File Offset: 0x0012DB04
		public bool isTalking { get; private set; }

		// Token: 0x06002E97 RID: 11927 RVA: 0x0012F710 File Offset: 0x0012DB10
		[SteamCall]
		public void tellVoice(CSteamID steamID, byte[] data, int length)
		{
			if (this.bufferReceive == null || this.received == null)
			{
				return;
			}
			if (base.channel.checkOwner(steamID) && !Provider.isServer)
			{
				if (!OptionsSettings.chatVoiceIn || base.channel.owner.isMuted)
				{
					return;
				}
				if (base.player.life.isDead)
				{
					return;
				}
				if (length <= 5)
				{
					return;
				}
				this.usingWalkieTalkie = (data[4] == 1);
				if (this.usingWalkieTalkie)
				{
					if (!this.canHearRadio)
					{
						return;
					}
					if (Player.player != null && Player.player.quests.radioFrequency != base.player.quests.radioFrequency)
					{
						return;
					}
				}
				for (int i = 0; i < length; i++)
				{
					data[i] = data[i + 5];
				}
				uint num;
				if (SteamUser.DecompressVoice(data, (uint)length, this.bufferReceive, (uint)this.bufferReceive.Length, out num, PlayerVoice.FREQUENCY) == EVoiceResult.k_EVoiceResultOK)
				{
					float num2 = num / 2u / PlayerVoice.FREQUENCY;
					this.playback += num2;
					int num3 = 0;
					while ((long)num3 < (long)((ulong)num))
					{
						this.received[this.write] = (float)BitConverter.ToInt16(this.bufferReceive, num3) / 32767f;
						this.received[this.write] *= OptionsSettings.voice;
						this.write++;
						if ((long)this.write >= (long)((ulong)PlayerVoice.SAMPLES))
						{
							this.write = 0;
						}
						num3 += 2;
					}
					this.audioSource.clip.SetData(this.received, 0);
					if (!this.isPlaying)
					{
						this.needsPlay = true;
						if (this.delayPlay <= 0f)
						{
							this.delayPlay = 0.3f;
						}
					}
				}
			}
		}

		// Token: 0x06002E98 RID: 11928 RVA: 0x0012F900 File Offset: 0x0012DD00
		private void Update()
		{
			if (base.channel.isOwner)
			{
				if (OptionsSettings.chatVoiceOut && Input.GetKey(ControlsSettings.voice) && !base.player.life.isDead)
				{
					if (!this.isTalking)
					{
						this.isTalking = true;
						this.wasRecording = true;
						this.lastTalk = Time.realtimeSinceStartup;
						if (this.hasWalkieTalkie)
						{
							this.audioSource.PlayOneShot(Resources.Load<AudioClip>("Sounds/General/Radio"), 0.75f);
						}
						SteamUser.StartVoiceRecording();
						SteamFriends.SetInGameVoiceSpeaking(Provider.user, this.isTalking);
						if (this.onTalked != null)
						{
							this.onTalked(this.isTalking);
						}
					}
				}
				else if ((!OptionsSettings.chatVoiceOut || !Input.GetKey(ControlsSettings.voice) || base.player.life.isDead) && this.isTalking)
				{
					this.isTalking = false;
					if (this.hasWalkieTalkie)
					{
						this.audioSource.PlayOneShot(Resources.Load<AudioClip>("Sounds/General/Radio"), 0.75f);
					}
					SteamUser.StopVoiceRecording();
					SteamFriends.SetInGameVoiceSpeaking(Provider.user, this.isTalking);
					if (this.onTalked != null)
					{
						this.onTalked(this.isTalking);
					}
				}
				if (this.wasRecording && (double)(Time.realtimeSinceStartup - this.lastTalk) > 0.1)
				{
					this.wasRecording = this.isTalking;
					this.lastTalk = Time.realtimeSinceStartup;
					uint num;
					if (SteamUser.GetAvailableVoice(out num) == EVoiceResult.k_EVoiceResultOK && num > 0u)
					{
						SteamUser.GetVoice(true, this.bufferSend, num, out num);
						if (num > 0u)
						{
							for (int i = (int)(num + 4u); i > 4; i--)
							{
								this.bufferSend[i] = this.bufferSend[i - 5];
							}
							this.bufferSend[4] = ((!this.hasWalkieTalkie) ? 0 : 1);
							if (this.hasWalkieTalkie)
							{
								int call = base.channel.getCall("tellVoice");
								int size;
								byte[] packet;
								base.channel.getPacketVoice(ESteamPacket.UPDATE_VOICE, call, out size, out packet, this.bufferSend, (int)num);
								for (int j = 0; j < Provider.clients.Count; j++)
								{
									if (Provider.clients[j].playerID.steamID != Provider.client && Provider.clients[j].player != null && Provider.clients[j].player.voice.canHearRadio && Provider.clients[j].player.quests.radioFrequency == base.player.quests.radioFrequency)
									{
										Provider.send(Provider.clients[j].playerID.steamID, ESteamPacket.UPDATE_VOICE, packet, size, base.channel.id);
									}
								}
							}
							else
							{
								base.channel.sendVoice("tellVoice", ESteamCall.PEERS, base.transform.position, EffectManager.MEDIUM, ESteamPacket.UPDATE_VOICE, this.bufferSend, (int)num);
							}
						}
					}
				}
			}
			else if (!Provider.isServer)
			{
				if (this.usingWalkieTalkie)
				{
					this.audioSource.spatialBlend = 0f;
				}
				else
				{
					this.audioSource.spatialBlend = 1f;
				}
				if (this.isPlaying)
				{
					if (this.lastPlay > this.audioSource.time)
					{
						this.played += this.audioSource.clip.length;
					}
					this.lastPlay = this.audioSource.time;
					if (this.played + this.audioSource.time >= this.playback)
					{
						this.isPlaying = false;
						this.audioSource.Stop();
						if (this.usingWalkieTalkie)
						{
							this.audioSource.PlayOneShot(Resources.Load<AudioClip>("Sounds/General/Radio"), 0.75f);
						}
						this.audioSource.time = 0f;
						this.write = 0;
						this.playback = 0f;
						this.played = 0f;
						this.lastPlay = 0f;
						this.needsPlay = false;
						this.isTalking = false;
						if (this.onTalked != null)
						{
							this.onTalked(this.isTalking);
						}
					}
				}
				else if (this.needsPlay)
				{
					this.delayPlay -= Time.deltaTime;
					if (this.delayPlay <= 0f)
					{
						this.isPlaying = true;
						this.audioSource.Play();
						if (this.usingWalkieTalkie)
						{
							this.audioSource.PlayOneShot(Resources.Load<AudioClip>("Sounds/General/Radio"), 0.75f);
						}
						this.isTalking = true;
						if (this.onTalked != null)
						{
							this.onTalked(this.isTalking);
						}
					}
				}
			}
		}

		// Token: 0x06002E99 RID: 11929 RVA: 0x0012FE1C File Offset: 0x0012E21C
		private void Start()
		{
			this.audioSource = base.GetComponent<AudioSource>();
			if (base.channel.isOwner)
			{
				this.audioSource.spatialBlend = 0f;
				this.bufferSend = new byte[8005];
			}
			else if (!Provider.isServer)
			{
				this.audioSource.clip = AudioClip.Create("Voice", (int)PlayerVoice.SAMPLES, 1, (int)PlayerVoice.FREQUENCY, false);
				this.received = new float[PlayerVoice.SAMPLES];
				this.bufferReceive = new byte[22000];
			}
		}

		// Token: 0x06002E9A RID: 11930 RVA: 0x0012FEB6 File Offset: 0x0012E2B6
		private void OnDestroy()
		{
			if (base.channel.isOwner && this.isTalking)
			{
				this.isTalking = false;
				SteamUser.StopVoiceRecording();
				SteamFriends.SetInGameVoiceSpeaking(Provider.user, this.isTalking);
			}
		}

		// Token: 0x04001E0D RID: 7693
		private static readonly uint FREQUENCY = 8000u;

		// Token: 0x04001E0E RID: 7694
		private static readonly uint LENGTH = 10u;

		// Token: 0x04001E0F RID: 7695
		private static readonly uint SAMPLES = PlayerVoice.FREQUENCY * PlayerVoice.LENGTH;

		// Token: 0x04001E10 RID: 7696
		public Talked onTalked;

		// Token: 0x04001E11 RID: 7697
		private AudioSource audioSource;

		// Token: 0x04001E12 RID: 7698
		public bool hasWalkieTalkie;

		// Token: 0x04001E13 RID: 7699
		protected bool usingWalkieTalkie;

		// Token: 0x04001E14 RID: 7700
		private float[] received;

		// Token: 0x04001E15 RID: 7701
		private byte[] bufferReceive;

		// Token: 0x04001E16 RID: 7702
		private byte[] bufferSend;

		// Token: 0x04001E17 RID: 7703
		private float playback;

		// Token: 0x04001E18 RID: 7704
		private int write;

		// Token: 0x04001E19 RID: 7705
		private bool needsPlay;

		// Token: 0x04001E1A RID: 7706
		private float delayPlay;

		// Token: 0x04001E1B RID: 7707
		private float lastPlay;

		// Token: 0x04001E1C RID: 7708
		private float played;

		// Token: 0x04001E1D RID: 7709
		private float lastTalk;

		// Token: 0x04001E1F RID: 7711
		private bool isPlaying;

		// Token: 0x04001E20 RID: 7712
		private bool wasRecording;
	}
}
