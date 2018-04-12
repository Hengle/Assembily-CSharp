using System;

namespace SDG.Unturned
{
	// Token: 0x0200051A RID: 1306
	public class AnimalSpawn
	{
		// Token: 0x06002383 RID: 9091 RVA: 0x000C5566 File Offset: 0x000C3966
		public AnimalSpawn(ushort newAnimal)
		{
			this._animal = newAnimal;
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06002384 RID: 9092 RVA: 0x000C5575 File Offset: 0x000C3975
		public ushort animal
		{
			get
			{
				return this._animal;
			}
		}

		// Token: 0x0400157F RID: 5503
		private ushort _animal;
	}
}
