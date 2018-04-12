using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004E8 RID: 1256
	public class InteractableStereo : Interactable
	{
		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x060021EB RID: 8683 RVA: 0x000BB531 File Offset: 0x000B9931
		// (set) Token: 0x060021EC RID: 8684 RVA: 0x000BB539 File Offset: 0x000B9939
		public float volume
		{
			get
			{
				return this._volume;
			}
			set
			{
				this._volume = value;
				if (this.audioSource != null)
				{
					this.audioSource.volume = this.volume;
				}
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x060021ED RID: 8685 RVA: 0x000BB564 File Offset: 0x000B9964
		// (set) Token: 0x060021EE RID: 8686 RVA: 0x000BB578 File Offset: 0x000B9978
		public byte compressedVolume
		{
			get
			{
				return (byte)Mathf.RoundToInt(this.volume * 100f);
			}
			set
			{
				this.volume = Mathf.Clamp01((float)value / 100f);
			}
		}

		// Token: 0x060021EF RID: 8687 RVA: 0x000BB590 File Offset: 0x000B9990
		public void updateTrack(Guid newTrack)
		{
			this.track.GUID = newTrack;
			if (this.audioSource != null)
			{
				StereoSongAsset stereoSongAsset = Assets.find<StereoSongAsset>(this.track);
				if (stereoSongAsset != null)
				{
					this.audioSource.clip = Assets.load<AudioClip>(stereoSongAsset.song);
				}
				else
				{
					this.audioSource.clip = null;
				}
				if (this.audioSource.clip != null)
				{
					this.audioSource.Play();
				}
				else
				{
					this.audioSource.Stop();
				}
			}
		}

		// Token: 0x060021F0 RID: 8688 RVA: 0x000BB624 File Offset: 0x000B9A24
		public override void updateState(Asset asset, byte[] state)
		{
			if (!Dedicator.isDedicated)
			{
				this.audioSource = base.transform.FindChild("Audio").GetComponent<AudioSource>();
			}
			GuidBuffer guidBuffer = default(GuidBuffer);
			guidBuffer.Read(state, 0);
			this.updateTrack(guidBuffer.GUID);
			this.compressedVolume = state[16];
		}

		// Token: 0x060021F1 RID: 8689 RVA: 0x000BB67E File Offset: 0x000B9A7E
		public override void use()
		{
			PlayerBarricadeStereoUI.open(this);
			PlayerLifeUI.close();
		}

		// Token: 0x060021F2 RID: 8690 RVA: 0x000BB68B File Offset: 0x000B9A8B
		public override bool checkHint(out EPlayerMessage message, out string text, out Color color)
		{
			message = EPlayerMessage.USE;
			text = string.Empty;
			color = Color.white;
			return !PlayerUI.window.showCursor;
		}

		// Token: 0x0400143D RID: 5181
		protected float _volume;

		// Token: 0x0400143E RID: 5182
		public AssetReference<StereoSongAsset> track;

		// Token: 0x0400143F RID: 5183
		public AudioSource audioSource;
	}
}
