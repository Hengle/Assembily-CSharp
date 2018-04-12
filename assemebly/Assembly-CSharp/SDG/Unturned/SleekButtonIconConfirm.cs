using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020006C9 RID: 1737
	public class SleekButtonIconConfirm : SleekButtonIcon
	{
		// Token: 0x0600322D RID: 12845 RVA: 0x00146C94 File Offset: 0x00145094
		public SleekButtonIconConfirm(Texture2D newIcon, string newConfirm, string newConfirmTooltip, string newDeny, string newDenyTooltip) : base(newIcon)
		{
			this.onClickedButton = new ClickedButton(this.onClickedMainButton);
			this.confirmButton = new SleekButton();
			this.confirmButton.sizeOffset_X = -5;
			this.confirmButton.sizeScale_X = 0.5f;
			this.confirmButton.sizeScale_Y = 1f;
			this.confirmButton.text = newConfirm;
			this.confirmButton.tooltip = newConfirmTooltip;
			this.confirmButton.onClickedButton = new ClickedButton(this.onClickedConfirmButton);
			base.add(this.confirmButton);
			this.confirmButton.isVisible = false;
			this.denyButton = new SleekButton();
			this.denyButton.positionOffset_X = 5;
			this.denyButton.positionScale_X = 0.5f;
			this.denyButton.sizeOffset_X = -5;
			this.denyButton.sizeScale_X = 0.5f;
			this.denyButton.sizeScale_Y = 1f;
			this.denyButton.text = newDeny;
			this.denyButton.tooltip = newDenyTooltip;
			this.denyButton.onClickedButton = new ClickedButton(this.onClickedDenyButton);
			base.add(this.denyButton);
			this.denyButton.isVisible = false;
		}

		// Token: 0x0600322E RID: 12846 RVA: 0x00146DD6 File Offset: 0x001451D6
		public void reset()
		{
			this.isHidden = false;
			this.iconImage.isHidden = false;
			this.confirmButton.isVisible = false;
			this.denyButton.isVisible = false;
		}

		// Token: 0x0600322F RID: 12847 RVA: 0x00146E03 File Offset: 0x00145203
		private void onClickedConfirmButton(SleekButton button)
		{
			this.reset();
			if (this.onConfirmed != null)
			{
				this.onConfirmed(this);
			}
		}

		// Token: 0x06003230 RID: 12848 RVA: 0x00146E22 File Offset: 0x00145222
		private void onClickedDenyButton(SleekButton button)
		{
			this.reset();
			if (this.onDenied != null)
			{
				this.onDenied(this);
			}
		}

		// Token: 0x06003231 RID: 12849 RVA: 0x00146E41 File Offset: 0x00145241
		private void onClickedMainButton(SleekButton button)
		{
			if (!this.isInputable)
			{
				return;
			}
			this.isHidden = true;
			this.iconImage.isHidden = true;
			this.confirmButton.isVisible = true;
			this.denyButton.isVisible = true;
		}

		// Token: 0x04002239 RID: 8761
		public Confirm onConfirmed;

		// Token: 0x0400223A RID: 8762
		public Deny onDenied;

		// Token: 0x0400223B RID: 8763
		private SleekButton confirmButton;

		// Token: 0x0400223C RID: 8764
		private SleekButton denyButton;
	}
}
