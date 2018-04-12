using System;

namespace SDG.Unturned
{
	// Token: 0x02000398 RID: 920
	public class DialogueMessage : DialogueElement
	{
		// Token: 0x060019CD RID: 6605 RVA: 0x000915F0 File Offset: 0x0008F9F0
		public DialogueMessage(byte newID, DialoguePage[] newPages, byte[] newResponses, ushort newPrev, INPCCondition[] newConditions, INPCReward[] newRewards) : base(newID, newConditions, newRewards)
		{
			this.pages = newPages;
			this.responses = newResponses;
			this.prev = newPrev;
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x060019CE RID: 6606 RVA: 0x00091613 File Offset: 0x0008FA13
		// (set) Token: 0x060019CF RID: 6607 RVA: 0x0009161B File Offset: 0x0008FA1B
		public DialoguePage[] pages { get; protected set; }

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x060019D0 RID: 6608 RVA: 0x00091624 File Offset: 0x0008FA24
		// (set) Token: 0x060019D1 RID: 6609 RVA: 0x0009162C File Offset: 0x0008FA2C
		public byte[] responses { get; protected set; }

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x060019D2 RID: 6610 RVA: 0x00091635 File Offset: 0x0008FA35
		// (set) Token: 0x060019D3 RID: 6611 RVA: 0x0009163D File Offset: 0x0008FA3D
		public ushort prev { get; protected set; }
	}
}
