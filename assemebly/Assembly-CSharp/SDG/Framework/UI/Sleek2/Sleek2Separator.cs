using System;
using SDG.Framework.UI.Components;
using UnityEngine;
using UnityEngine.UI;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002E1 RID: 737
	public class Sleek2Separator : Sleek2Element
	{
		// Token: 0x0600153B RID: 5435 RVA: 0x00082004 File Offset: 0x00080404
		public Sleek2Separator()
		{
			base.gameObject.name = "Separator";
			this.handle = base.gameObject.AddComponent<Separator>();
			this.handle.min = 0.1f;
			this.handle.max = 0.9f;
			this.handle.padding = 5f;
			this.handle.width = 8f;
			this.image = base.gameObject.AddComponent<Image>();
			this.image.type = Image.Type.Sliced;
			this.direction = Separator.EDirection.HORIZONTAL;
			base.gameObject.AddComponent<Selectable>();
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x0600153C RID: 5436 RVA: 0x000820A8 File Offset: 0x000804A8
		// (set) Token: 0x0600153D RID: 5437 RVA: 0x000820B0 File Offset: 0x000804B0
		public Separator handle { get; protected set; }

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x0600153E RID: 5438 RVA: 0x000820B9 File Offset: 0x000804B9
		// (set) Token: 0x0600153F RID: 5439 RVA: 0x000820C1 File Offset: 0x000804C1
		public Image image { get; protected set; }

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06001540 RID: 5440 RVA: 0x000820CA File Offset: 0x000804CA
		// (set) Token: 0x06001541 RID: 5441 RVA: 0x000820D8 File Offset: 0x000804D8
		public Separator.EDirection direction
		{
			get
			{
				return this.handle.direction;
			}
			set
			{
				this.handle.direction = value;
				Separator.EDirection direction = this.direction;
				if (direction != Separator.EDirection.HORIZONTAL)
				{
					if (direction == Separator.EDirection.VERTICAL)
					{
						this.image.sprite = Resources.Load<Sprite>("Sprites/UI/Separator_Vertical");
					}
				}
				else
				{
					this.image.sprite = Resources.Load<Sprite>("Sprites/UI/Separator_Horizontal");
				}
			}
		}
	}
}
