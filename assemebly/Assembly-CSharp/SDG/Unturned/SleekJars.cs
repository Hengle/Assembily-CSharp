using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020006EF RID: 1775
	public class SleekJars : Sleek
	{
		// Token: 0x060032DC RID: 13020 RVA: 0x00149A98 File Offset: 0x00147E98
		public SleekJars(float radius, List<InventorySearch> search)
		{
			base.init();
			float num = 6.28318548f / (float)search.Count;
			for (int i = 0; i < search.Count; i++)
			{
				ItemJar jar = search[i].jar;
				ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, jar.item.id);
				if (itemAsset != null)
				{
					SleekItem sleekItem = new SleekItem(jar);
					sleekItem.positionOffset_X = (int)(Mathf.Cos(num * (float)i) * radius) - sleekItem.sizeOffset_X / 2;
					sleekItem.positionOffset_Y = (int)(Mathf.Sin(num * (float)i) * radius) - sleekItem.sizeOffset_Y / 2;
					sleekItem.positionScale_X = 0.5f;
					sleekItem.positionScale_Y = 0.5f;
					sleekItem.onClickedItem = new ClickedItem(this.onClickedButton);
					sleekItem.onDraggedItem = new DraggedItem(this.onClickedButton);
					base.add(sleekItem);
				}
			}
		}

		// Token: 0x060032DD RID: 13021 RVA: 0x00149B88 File Offset: 0x00147F88
		public override void draw(bool ignoreCulling)
		{
			base.drawChildren(ignoreCulling);
		}

		// Token: 0x060032DE RID: 13022 RVA: 0x00149B94 File Offset: 0x00147F94
		private void onClickedButton(SleekItem item)
		{
			int num = base.search(item);
			if (num != -1 && this.onClickedJar != null)
			{
				this.onClickedJar(this, num);
			}
		}

		// Token: 0x0400228B RID: 8843
		public ClickedJar onClickedJar;
	}
}
