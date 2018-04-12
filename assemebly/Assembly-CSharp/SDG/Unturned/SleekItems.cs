using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020006ED RID: 1773
	public class SleekItems : Sleek
	{
		// Token: 0x060032CA RID: 13002 RVA: 0x00149734 File Offset: 0x00147B34
		public SleekItems(byte newPage)
		{
			this._page = newPage;
			this._items = new List<SleekItem>();
			base.init();
			this.grid = new SleekGrid();
			this.grid.sizeScale_X = 1f;
			this.grid.sizeScale_Y = 1f;
			this.grid.texture = (Texture2D)PlayerDashboardInventoryUI.icons.load("Grid_Free");
			this.grid.onClickedGrid = new ClickedGrid(this.onClickedGrid);
			this.grid.backgroundTint = ESleekTint.FOREGROUND;
			base.add(this.grid);
		}

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x060032CB RID: 13003 RVA: 0x001497D8 File Offset: 0x00147BD8
		public byte page
		{
			get
			{
				return this._page;
			}
		}

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x060032CC RID: 13004 RVA: 0x001497E0 File Offset: 0x00147BE0
		public byte width
		{
			get
			{
				return this._width;
			}
		}

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x060032CD RID: 13005 RVA: 0x001497E8 File Offset: 0x00147BE8
		public byte height
		{
			get
			{
				return this._height;
			}
		}

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x060032CE RID: 13006 RVA: 0x001497F0 File Offset: 0x00147BF0
		public List<SleekItem> items
		{
			get
			{
				return this._items;
			}
		}

		// Token: 0x060032CF RID: 13007 RVA: 0x001497F8 File Offset: 0x00147BF8
		public override void draw(bool ignoreCulling)
		{
			base.drawChildren(ignoreCulling);
		}

		// Token: 0x060032D0 RID: 13008 RVA: 0x00149801 File Offset: 0x00147C01
		public void resize(byte newWidth, byte newHeight)
		{
			this._width = newWidth;
			this._height = newHeight;
			base.sizeOffset_X = (int)(this.width * 50);
			base.sizeOffset_Y = (int)(this.height * 50);
		}

		// Token: 0x060032D1 RID: 13009 RVA: 0x0014982F File Offset: 0x00147C2F
		public void clear()
		{
			this.items.Clear();
			base.remove();
			base.add(this.grid);
		}

		// Token: 0x060032D2 RID: 13010 RVA: 0x0014984E File Offset: 0x00147C4E
		public void updateItem(byte index, ItemJar jar)
		{
			this.items[(int)index].updateItem(jar);
		}

		// Token: 0x060032D3 RID: 13011 RVA: 0x00149864 File Offset: 0x00147C64
		public void addItem(ItemJar jar)
		{
			SleekItem sleekItem = new SleekItem(jar);
			sleekItem.positionOffset_X = (int)(jar.x * 50);
			sleekItem.positionOffset_Y = (int)(jar.y * 50);
			sleekItem.onClickedItem = new ClickedItem(this.onClickedItem);
			sleekItem.onDraggedItem = new DraggedItem(this.onDraggedItem);
			base.add(sleekItem);
			this.items.Add(sleekItem);
		}

		// Token: 0x060032D4 RID: 13012 RVA: 0x001498D0 File Offset: 0x00147CD0
		public void removeItem(ItemJar jar)
		{
			for (int i = 0; i < this.items.Count; i++)
			{
				if (this.items[i].positionOffset_X == (int)(jar.x * 50) && this.items[i].positionOffset_Y == (int)(jar.y * 50))
				{
					base.remove(this.items[i]);
					this.items.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x060032D5 RID: 13013 RVA: 0x00149956 File Offset: 0x00147D56
		private void onClickedItem(SleekItem item)
		{
			if (this.onSelectedItem != null)
			{
				this.onSelectedItem(this.page, (byte)(item.positionOffset_X / 50), (byte)(item.positionOffset_Y / 50));
			}
		}

		// Token: 0x060032D6 RID: 13014 RVA: 0x00149988 File Offset: 0x00147D88
		private void onDraggedItem(SleekItem item)
		{
			if (this.onGrabbedItem != null)
			{
				this.onGrabbedItem(this.page, (byte)(item.positionOffset_X / 50), (byte)(item.positionOffset_Y / 50), item);
			}
		}

		// Token: 0x060032D7 RID: 13015 RVA: 0x001499BC File Offset: 0x00147DBC
		private void onClickedGrid(SleekGrid grid)
		{
			byte x = (byte)((PlayerUI.window.mouse_x + PlayerUI.window.frame.x - (float)base.positionOffset_X - base.parent.frame.x + ((SleekScrollBox)base.parent).state.x) / 50f);
			byte y = (byte)((PlayerUI.window.mouse_y + PlayerUI.window.frame.y - (float)base.positionOffset_Y - base.parent.frame.y + ((SleekScrollBox)base.parent).state.y) / 50f);
			if (this.onPlacedItem != null)
			{
				this.onPlacedItem(this.page, x, y);
			}
		}

		// Token: 0x04002283 RID: 8835
		public SelectedItem onSelectedItem;

		// Token: 0x04002284 RID: 8836
		public GrabbedItem onGrabbedItem;

		// Token: 0x04002285 RID: 8837
		public PlacedItem onPlacedItem;

		// Token: 0x04002286 RID: 8838
		private SleekGrid grid;

		// Token: 0x04002287 RID: 8839
		private byte _page;

		// Token: 0x04002288 RID: 8840
		private byte _width;

		// Token: 0x04002289 RID: 8841
		private byte _height;

		// Token: 0x0400228A RID: 8842
		private List<SleekItem> _items;
	}
}
