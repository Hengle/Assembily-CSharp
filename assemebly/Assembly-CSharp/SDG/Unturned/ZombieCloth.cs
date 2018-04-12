using System;

namespace SDG.Unturned
{
	// Token: 0x0200057A RID: 1402
	public class ZombieCloth
	{
		// Token: 0x060026AB RID: 9899 RVA: 0x000E5195 File Offset: 0x000E3595
		public ZombieCloth(ushort newItem)
		{
			this._item = newItem;
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x060026AC RID: 9900 RVA: 0x000E51A4 File Offset: 0x000E35A4
		public ushort item
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x04001837 RID: 6199
		private ushort _item;
	}
}
