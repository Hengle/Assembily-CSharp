using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020003BA RID: 954
	public class INPCCondition
	{
		// Token: 0x06001A11 RID: 6673 RVA: 0x00091C85 File Offset: 0x00090085
		public INPCCondition(string newText, bool newShouldReset)
		{
			this.text = newText;
			this.shouldReset = newShouldReset;
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x00091C9B File Offset: 0x0009009B
		public virtual bool isConditionMet(Player player)
		{
			return false;
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x00091C9E File Offset: 0x0009009E
		public virtual void applyCondition(Player player, bool shouldSend)
		{
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x00091CA0 File Offset: 0x000900A0
		public virtual string formatCondition(Player player)
		{
			return null;
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x00091CA4 File Offset: 0x000900A4
		public virtual Sleek createUI(Player player, Texture2D icon)
		{
			string value = this.formatCondition(player);
			if (string.IsNullOrEmpty(value))
			{
				return null;
			}
			SleekBox sleekBox = new SleekBox();
			sleekBox.sizeOffset_Y = 30;
			sleekBox.sizeScale_X = 1f;
			if (icon != null)
			{
				sleekBox.add(new SleekImageTexture(icon)
				{
					positionOffset_X = 5,
					positionOffset_Y = 5,
					sizeOffset_X = 20,
					sizeOffset_Y = 20
				});
			}
			SleekLabel sleekLabel = new SleekLabel();
			if (icon != null)
			{
				sleekLabel.positionOffset_X = 30;
				sleekLabel.sizeOffset_X = -35;
			}
			else
			{
				sleekLabel.positionOffset_X = 5;
				sleekLabel.sizeOffset_X = -10;
			}
			sleekLabel.sizeScale_X = 1f;
			sleekLabel.sizeScale_Y = 1f;
			sleekLabel.fontAlignment = TextAnchor.MiddleLeft;
			sleekLabel.foregroundTint = ESleekTint.NONE;
			sleekLabel.isRich = true;
			sleekLabel.text = value;
			sleekBox.add(sleekLabel);
			return sleekBox;
		}

		// Token: 0x04000EF1 RID: 3825
		protected string text;

		// Token: 0x04000EF2 RID: 3826
		protected bool shouldReset;
	}
}
