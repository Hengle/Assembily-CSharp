using System;
using SDG.Framework.Devkit.Transactions;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.TransactionUI
{
	// Token: 0x0200028F RID: 655
	public class TransactionUndoButton : Sleek2ImageButton
	{
		// Token: 0x06001332 RID: 4914 RVA: 0x0007A628 File Offset: 0x00078A28
		public TransactionUndoButton(DevkitTransactionGroup newGroup)
		{
			this.group = newGroup;
			Sleek2TranslatedLabel sleek2TranslatedLabel = new Sleek2TranslatedLabel();
			sleek2TranslatedLabel.transform.reset();
			sleek2TranslatedLabel.translation = this.group.name;
			sleek2TranslatedLabel.translation.format();
			sleek2TranslatedLabel.textComponent.color = Sleek2Config.darkTextColor;
			this.addElement(sleek2TranslatedLabel);
		}

		// Token: 0x04000B05 RID: 2821
		public DevkitTransactionGroup group;
	}
}
