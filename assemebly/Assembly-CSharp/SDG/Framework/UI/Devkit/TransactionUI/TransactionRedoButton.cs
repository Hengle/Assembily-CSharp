using System;
using SDG.Framework.Devkit.Transactions;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.TransactionUI
{
	// Token: 0x0200028E RID: 654
	public class TransactionRedoButton : Sleek2ImageButton
	{
		// Token: 0x06001331 RID: 4913 RVA: 0x0007A5C8 File Offset: 0x000789C8
		public TransactionRedoButton(DevkitTransactionGroup newGroup)
		{
			this.group = newGroup;
			Sleek2TranslatedLabel sleek2TranslatedLabel = new Sleek2TranslatedLabel();
			sleek2TranslatedLabel.transform.reset();
			sleek2TranslatedLabel.translation = this.group.name;
			sleek2TranslatedLabel.translation.format();
			sleek2TranslatedLabel.textComponent.color = Sleek2Config.darkTextColor;
			this.addElement(sleek2TranslatedLabel);
		}

		// Token: 0x04000B04 RID: 2820
		public DevkitTransactionGroup group;
	}
}
