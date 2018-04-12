using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000704 RID: 1796
	public class SleekSlot : Sleek
	{
		// Token: 0x0600333E RID: 13118 RVA: 0x0014D1A4 File Offset: 0x0014B5A4
		public SleekSlot(byte newPage)
		{
			this._page = newPage;
			base.init();
			base.sizeOffset_X = 250;
			base.sizeOffset_Y = 150;
			this.image = new SleekImageTexture();
			this.image.sizeScale_X = 1f;
			this.image.sizeScale_Y = 1f;
			this.image.texture = (Texture2D)PlayerDashboardInventoryUI.icons.load("Slot_" + this.page + "_Free");
			this.image.backgroundTint = ESleekTint.FOREGROUND;
			base.add(this.image);
		}

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x0600333F RID: 13119 RVA: 0x0014D251 File Offset: 0x0014B651
		public SleekItem item
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x06003340 RID: 13120 RVA: 0x0014D259 File Offset: 0x0014B659
		public byte page
		{
			get
			{
				return this._page;
			}
		}

		// Token: 0x06003341 RID: 13121 RVA: 0x0014D261 File Offset: 0x0014B661
		public override void draw(bool ignoreCulling)
		{
			base.drawChildren(ignoreCulling);
		}

		// Token: 0x06003342 RID: 13122 RVA: 0x0014D26A File Offset: 0x0014B66A
		public void select()
		{
			if (this.onPlacedItem != null)
			{
				this.onPlacedItem(this.page, 0, 0);
			}
		}

		// Token: 0x06003343 RID: 13123 RVA: 0x0014D28A File Offset: 0x0014B68A
		public void updateItem(ItemJar jar)
		{
			if (this.item == null)
			{
				return;
			}
			this.item.updateItem(jar);
		}

		// Token: 0x06003344 RID: 13124 RVA: 0x0014D2A4 File Offset: 0x0014B6A4
		public void applyItem(ItemJar jar)
		{
			if (this.item != null)
			{
				base.remove(this.item);
			}
			if (jar != null)
			{
				this._item = new SleekItem(jar);
				this.item.positionOffset_X = (int)(-jar.size_x * 25);
				this.item.positionOffset_Y = (int)(-jar.size_y * 25);
				this.item.positionScale_X = 0.5f;
				this.item.positionScale_Y = 0.5f;
				this.item.updateHotkey(this.page);
				this.item.onClickedItem = new ClickedItem(this.onClickedItem);
				this.item.onDraggedItem = new DraggedItem(this.onDraggedItem);
				base.add(this.item);
			}
		}

		// Token: 0x06003345 RID: 13125 RVA: 0x0014D36F File Offset: 0x0014B76F
		private void onClickedItem(SleekItem item)
		{
			if (this.onSelectedItem != null)
			{
				this.onSelectedItem(this.page, 0, 0);
			}
		}

		// Token: 0x06003346 RID: 13126 RVA: 0x0014D38F File Offset: 0x0014B78F
		private void onDraggedItem(SleekItem item)
		{
			if (this.onGrabbedItem != null)
			{
				this.onGrabbedItem(this.page, 0, 0, item);
			}
		}

		// Token: 0x040022C3 RID: 8899
		public SelectedItem onSelectedItem;

		// Token: 0x040022C4 RID: 8900
		public GrabbedItem onGrabbedItem;

		// Token: 0x040022C5 RID: 8901
		public PlacedItem onPlacedItem;

		// Token: 0x040022C6 RID: 8902
		private SleekImageTexture image;

		// Token: 0x040022C7 RID: 8903
		private SleekItem _item;

		// Token: 0x040022C8 RID: 8904
		private byte _page;
	}
}
