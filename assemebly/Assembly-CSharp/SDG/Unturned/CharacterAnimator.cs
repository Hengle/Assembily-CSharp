using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000431 RID: 1073
	public class CharacterAnimator : MonoBehaviour
	{
		// Token: 0x06001D56 RID: 7510 RVA: 0x0009DBCF File Offset: 0x0009BFCF
		public void sample()
		{
			base.GetComponent<Animation>().Sample();
		}

		// Token: 0x06001D57 RID: 7511 RVA: 0x0009DBDC File Offset: 0x0009BFDC
		public void mixAnimation(string name)
		{
			base.GetComponent<Animation>()[name].layer = 1;
		}

		// Token: 0x06001D58 RID: 7512 RVA: 0x0009DBF0 File Offset: 0x0009BFF0
		public void mixAnimation(string name, bool mixLeftShoulder, bool mixRightShoulder)
		{
			this.mixAnimation(name, mixLeftShoulder, mixRightShoulder, false);
		}

		// Token: 0x06001D59 RID: 7513 RVA: 0x0009DBFC File Offset: 0x0009BFFC
		public void mixAnimation(string name, bool mixLeftShoulder, bool mixRightShoulder, bool mixSkull)
		{
			if (mixLeftShoulder)
			{
				this.anim[name].AddMixingTransform(this.leftShoulder, true);
			}
			if (mixRightShoulder)
			{
				this.anim[name].AddMixingTransform(this.rightShoulder, true);
			}
			if (mixSkull)
			{
				this.anim[name].AddMixingTransform(this.skull, true);
			}
			base.GetComponent<Animation>()[name].layer = 1;
		}

		// Token: 0x06001D5A RID: 7514 RVA: 0x0009DC76 File Offset: 0x0009C076
		public void addAnimation(AnimationClip clip)
		{
			this.anim.AddClip(clip, clip.name);
			this.mixAnimation(clip.name, true, true);
		}

		// Token: 0x06001D5B RID: 7515 RVA: 0x0009DC98 File Offset: 0x0009C098
		public void removeAnimation(AnimationClip clip)
		{
			if (this.anim[clip.name] != null)
			{
				this.anim.RemoveClip(clip);
			}
		}

		// Token: 0x06001D5C RID: 7516 RVA: 0x0009DCC2 File Offset: 0x0009C0C2
		public void setAnimationSpeed(string name, float speed)
		{
			if (this.anim[name] != null)
			{
				this.anim[name].speed = speed;
			}
		}

		// Token: 0x06001D5D RID: 7517 RVA: 0x0009DCF0 File Offset: 0x0009C0F0
		public float getAnimationLength(string name)
		{
			if (this.anim[name] != null)
			{
				return this.anim[name].clip.length / base.GetComponent<Animation>()[name].speed;
			}
			return 0f;
		}

		// Token: 0x06001D5E RID: 7518 RVA: 0x0009DD42 File Offset: 0x0009C142
		public void getAnimationSample(string name, float point)
		{
			if (this.anim[name] != null)
			{
				this.anim[name].clip.SampleAnimation(base.gameObject, point);
			}
		}

		// Token: 0x06001D5F RID: 7519 RVA: 0x0009DD78 File Offset: 0x0009C178
		public bool getAnimationPlaying()
		{
			return !string.IsNullOrEmpty(base.name) && this.anim.IsPlaying(this.clip);
		}

		// Token: 0x06001D60 RID: 7520 RVA: 0x0009DD9E File Offset: 0x0009C19E
		public void state(string name)
		{
			if (this.anim[name] == null)
			{
				return;
			}
			this.anim.CrossFade(name, CharacterAnimator.BLEND);
		}

		// Token: 0x06001D61 RID: 7521 RVA: 0x0009DDC9 File Offset: 0x0009C1C9
		public bool checkExists(string name)
		{
			return this.anim[name] != null;
		}

		// Token: 0x06001D62 RID: 7522 RVA: 0x0009DDE0 File Offset: 0x0009C1E0
		public void play(string name, bool smooth)
		{
			if (this.anim[name] == null)
			{
				return;
			}
			if (this.clip != string.Empty)
			{
				this.anim.Stop(this.clip);
			}
			this.clip = name;
			if (smooth)
			{
				this.anim.CrossFade(name, CharacterAnimator.BLEND);
			}
			else
			{
				this.anim.Play(name);
			}
		}

		// Token: 0x06001D63 RID: 7523 RVA: 0x0009DE5C File Offset: 0x0009C25C
		public void stop(string name)
		{
			if (this.anim[name] == null)
			{
				return;
			}
			if (name == this.clip)
			{
				this.anim.Stop(name);
				this.clip = string.Empty;
			}
		}

		// Token: 0x06001D64 RID: 7524 RVA: 0x0009DEAC File Offset: 0x0009C2AC
		protected void init()
		{
			this.clip = string.Empty;
			this.anim = base.GetComponent<Animation>();
			this.spine = base.transform.FindChild("Skeleton").FindChild("Spine");
			this.skull = this.spine.FindChild("Skull");
			this.leftShoulder = this.spine.FindChild("Left_Shoulder");
			this.rightShoulder = this.spine.FindChild("Right_Shoulder");
		}

		// Token: 0x06001D65 RID: 7525 RVA: 0x0009DF32 File Offset: 0x0009C332
		private void Awake()
		{
			this.init();
		}

		// Token: 0x04001166 RID: 4454
		public static readonly float BLEND = 0.25f;

		// Token: 0x04001167 RID: 4455
		protected Animation anim;

		// Token: 0x04001168 RID: 4456
		protected Transform spine;

		// Token: 0x04001169 RID: 4457
		protected Transform skull;

		// Token: 0x0400116A RID: 4458
		protected Transform leftShoulder;

		// Token: 0x0400116B RID: 4459
		protected Transform rightShoulder;

		// Token: 0x0400116C RID: 4460
		protected string clip;
	}
}
