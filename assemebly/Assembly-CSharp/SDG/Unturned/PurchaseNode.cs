using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000567 RID: 1383
	public class PurchaseNode : Node
	{
		// Token: 0x06002626 RID: 9766 RVA: 0x000E0153 File Offset: 0x000DE553
		public PurchaseNode(Vector3 newPoint) : this(newPoint, 0f, 0, 0u)
		{
		}

		// Token: 0x06002627 RID: 9767 RVA: 0x000E0164 File Offset: 0x000DE564
		public PurchaseNode(Vector3 newPoint, float newRadius, ushort newID, uint newCost)
		{
			this._point = newPoint;
			if (Level.isEditor)
			{
				this._model = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Edit/Purchase"))).transform;
				base.model.name = "Node";
				base.model.position = base.point;
				base.model.parent = LevelNodes.models;
			}
			if (!Level.isEditor)
			{
				this.radius = Mathf.Pow((PurchaseNode.MIN_SIZE + newRadius * (PurchaseNode.MAX_SIZE - PurchaseNode.MIN_SIZE)) / 2f, 2f);
			}
			else
			{
				this.radius = newRadius;
			}
			this.id = newID;
			this.cost = newCost;
			this._type = ENodeType.PURCHASE;
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06002628 RID: 9768 RVA: 0x000E022D File Offset: 0x000DE62D
		// (set) Token: 0x06002629 RID: 9769 RVA: 0x000E0238 File Offset: 0x000DE638
		public float radius
		{
			get
			{
				return this._radius;
			}
			set
			{
				this._radius = value;
				if (base.model != null)
				{
					float num = PurchaseNode.MIN_SIZE + this.radius * (PurchaseNode.MAX_SIZE - PurchaseNode.MIN_SIZE);
					base.model.transform.localScale = new Vector3(num, num, num);
				}
			}
		}

		// Token: 0x040017CD RID: 6093
		public static readonly float MIN_SIZE = 2f;

		// Token: 0x040017CE RID: 6094
		public static readonly float MAX_SIZE = 16f;

		// Token: 0x040017CF RID: 6095
		private float _radius;

		// Token: 0x040017D0 RID: 6096
		public ushort id;

		// Token: 0x040017D1 RID: 6097
		public uint cost;
	}
}
