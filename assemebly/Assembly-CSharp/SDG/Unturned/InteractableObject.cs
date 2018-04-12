using System;

namespace SDG.Unturned
{
	// Token: 0x020004D5 RID: 1237
	public class InteractableObject : InteractablePower
	{
		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06002142 RID: 8514 RVA: 0x000B5502 File Offset: 0x000B3902
		public ObjectAsset objectAsset
		{
			get
			{
				return this._objectAsset;
			}
		}

		// Token: 0x06002143 RID: 8515 RVA: 0x000B550A File Offset: 0x000B390A
		public override void updateState(Asset asset, byte[] state)
		{
			base.updateState(asset, state);
			this._objectAsset = (asset as ObjectAsset);
		}

		// Token: 0x040013CC RID: 5068
		protected ObjectAsset _objectAsset;
	}
}
