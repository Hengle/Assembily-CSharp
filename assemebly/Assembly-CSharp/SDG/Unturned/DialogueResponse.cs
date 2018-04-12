using System;

namespace SDG.Unturned
{
	// Token: 0x0200039A RID: 922
	public class DialogueResponse : DialogueElement
	{
		// Token: 0x060019D7 RID: 6615 RVA: 0x00091666 File Offset: 0x0008FA66
		public DialogueResponse(byte newID, byte[] newMessages, ushort newDialogue, ushort newQuest, ushort newVendor, string newText, INPCCondition[] newConditions, INPCReward[] newRewards) : base(newID, newConditions, newRewards)
		{
			this.messages = newMessages;
			this.dialogue = newDialogue;
			this.quest = newQuest;
			this.vendor = newVendor;
			this.text = newText;
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x060019D8 RID: 6616 RVA: 0x00091699 File Offset: 0x0008FA99
		// (set) Token: 0x060019D9 RID: 6617 RVA: 0x000916A1 File Offset: 0x0008FAA1
		public byte[] messages { get; protected set; }

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x060019DA RID: 6618 RVA: 0x000916AA File Offset: 0x0008FAAA
		// (set) Token: 0x060019DB RID: 6619 RVA: 0x000916B2 File Offset: 0x0008FAB2
		public ushort dialogue { get; protected set; }

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x060019DC RID: 6620 RVA: 0x000916BB File Offset: 0x0008FABB
		// (set) Token: 0x060019DD RID: 6621 RVA: 0x000916C3 File Offset: 0x0008FAC3
		public ushort quest { get; protected set; }

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x060019DE RID: 6622 RVA: 0x000916CC File Offset: 0x0008FACC
		// (set) Token: 0x060019DF RID: 6623 RVA: 0x000916D4 File Offset: 0x0008FAD4
		public ushort vendor { get; protected set; }

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x060019E0 RID: 6624 RVA: 0x000916DD File Offset: 0x0008FADD
		// (set) Token: 0x060019E1 RID: 6625 RVA: 0x000916E5 File Offset: 0x0008FAE5
		public string text { get; protected set; }
	}
}
