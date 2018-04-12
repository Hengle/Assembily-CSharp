using System;

namespace SDG.Unturned
{
	// Token: 0x02000399 RID: 921
	public class DialoguePage
	{
		// Token: 0x060019D4 RID: 6612 RVA: 0x00091646 File Offset: 0x0008FA46
		public DialoguePage(string newText)
		{
			this.text = newText;
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x060019D5 RID: 6613 RVA: 0x00091655 File Offset: 0x0008FA55
		// (set) Token: 0x060019D6 RID: 6614 RVA: 0x0009165D File Offset: 0x0008FA5D
		public string text { get; protected set; }
	}
}
