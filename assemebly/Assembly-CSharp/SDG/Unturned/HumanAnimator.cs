using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000432 RID: 1074
	public class HumanAnimator : CharacterAnimator
	{
		// Token: 0x06001D68 RID: 7528 RVA: 0x0009DF50 File Offset: 0x0009C350
		public void force()
		{
			this._lean = Mathf.Clamp(this.lean, -1f, 1f);
			this._pitch = Mathf.Clamp(this.pitch, 1f, 179f) - 90f;
			this._offset = this.offset;
		}

		// Token: 0x06001D69 RID: 7529 RVA: 0x0009DFA8 File Offset: 0x0009C3A8
		public void apply()
		{
			bool animationPlaying = base.getAnimationPlaying();
			if (animationPlaying)
			{
				this.leftShoulder.parent = this.skull;
				this.rightShoulder.parent = this.skull;
			}
			this.spine.Rotate(0f, this._pitch * 0.5f, this._lean * HumanAnimator.LEAN);
			this.skull.Rotate(0f, this._pitch * 0.5f, 0f);
			this.skull.position += this.skull.forward * this.offset;
			if (animationPlaying)
			{
				this.skull.Rotate(0f, -this.spine.localRotation.eulerAngles.x + this._pitch * 0.5f, 0f);
				this.leftShoulder.parent = this.spine;
				this.rightShoulder.parent = this.spine;
				this.skull.Rotate(0f, this.spine.localRotation.eulerAngles.x - this._pitch * 0.5f, 0f);
			}
		}

		// Token: 0x06001D6A RID: 7530 RVA: 0x0009E100 File Offset: 0x0009C500
		private void LateUpdate()
		{
			this._lean = Mathf.LerpAngle(this._lean, Mathf.Clamp(this.lean, -1f, 1f), 4f * Time.deltaTime);
			this._pitch = Mathf.LerpAngle(this._pitch, Mathf.Clamp(this.pitch, 1f, 179f) - 90f, 8f * Time.deltaTime);
			this._offset = Mathf.Lerp(this._offset, this.offset, 4f * Time.deltaTime);
			this.apply();
		}

		// Token: 0x06001D6B RID: 7531 RVA: 0x0009E19D File Offset: 0x0009C59D
		private void Awake()
		{
			base.init();
		}

		// Token: 0x0400116D RID: 4461
		public static readonly float LEAN = 20f;

		// Token: 0x0400116E RID: 4462
		private float _lean;

		// Token: 0x0400116F RID: 4463
		public float lean;

		// Token: 0x04001170 RID: 4464
		private float _pitch;

		// Token: 0x04001171 RID: 4465
		public float pitch;

		// Token: 0x04001172 RID: 4466
		private float _offset;

		// Token: 0x04001173 RID: 4467
		public float offset;
	}
}
