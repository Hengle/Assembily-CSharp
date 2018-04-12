using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020003BB RID: 955
	public class INPCReward
	{
		// Token: 0x06001A16 RID: 6678 RVA: 0x00091D87 File Offset: 0x00090187
		public INPCReward(string newText)
		{
			this.text = newText;
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x00091D96 File Offset: 0x00090196
		public virtual void grantReward(Player player, bool shouldSend)
		{
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x00091D98 File Offset: 0x00090198
		public virtual string formatReward(Player player)
		{
			return null;
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x00091D9C File Offset: 0x0009019C
		public virtual Sleek createUI(Player player)
		{
			string value = this.formatReward(player);
			if (string.IsNullOrEmpty(value))
			{
				return null;
			}
			SleekBox sleekBox = new SleekBox();
			sleekBox.sizeOffset_Y = 30;
			sleekBox.sizeScale_X = 1f;
			sleekBox.add(new SleekLabel
			{
				positionOffset_X = 5,
				sizeOffset_X = -10,
				sizeScale_X = 1f,
				sizeScale_Y = 1f,
				fontAlignment = TextAnchor.MiddleLeft,
				foregroundTint = ESleekTint.NONE,
				isRich = true,
				text = value
			});
			return sleekBox;
		}

		// Token: 0x04000EF3 RID: 3827
		protected string text;
	}
}
