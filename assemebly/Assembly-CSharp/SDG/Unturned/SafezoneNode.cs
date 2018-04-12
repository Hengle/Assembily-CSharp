using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000571 RID: 1393
	public class SafezoneNode : Node
	{
		// Token: 0x0600267D RID: 9853 RVA: 0x000E3FB7 File Offset: 0x000E23B7
		public SafezoneNode(Vector3 newPoint) : this(newPoint, 0f, false, true, true)
		{
		}

		// Token: 0x0600267E RID: 9854 RVA: 0x000E3FC8 File Offset: 0x000E23C8
		public SafezoneNode(Vector3 newPoint, float newRadius, bool newHeight, bool newNoWeapons, bool newNoBuildables)
		{
			this._point = newPoint;
			if (Level.isEditor)
			{
				this._model = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Edit/Safezone"))).transform;
				base.model.name = "Node";
				base.model.position = base.point;
				base.model.parent = LevelNodes.models;
			}
			if (!Level.isEditor)
			{
				this.radius = Mathf.Pow((SafezoneNode.MIN_SIZE + newRadius * (SafezoneNode.MAX_SIZE - SafezoneNode.MIN_SIZE)) / 2f, 2f);
			}
			else
			{
				this.radius = newRadius;
			}
			this.isHeight = newHeight;
			this.noWeapons = newNoWeapons;
			this.noBuildables = newNoBuildables;
			this._type = ENodeType.SAFEZONE;
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x0600267F RID: 9855 RVA: 0x000E4099 File Offset: 0x000E2499
		// (set) Token: 0x06002680 RID: 9856 RVA: 0x000E40A4 File Offset: 0x000E24A4
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
					float num = SafezoneNode.MIN_SIZE + this.radius * (SafezoneNode.MAX_SIZE - SafezoneNode.MIN_SIZE);
					base.model.transform.localScale = new Vector3(num, num, num);
				}
			}
		}

		// Token: 0x04001815 RID: 6165
		public static readonly float MIN_SIZE = 32f;

		// Token: 0x04001816 RID: 6166
		public static readonly float MAX_SIZE = 1024f;

		// Token: 0x04001817 RID: 6167
		private float _radius;

		// Token: 0x04001818 RID: 6168
		public bool isHeight;

		// Token: 0x04001819 RID: 6169
		public bool noWeapons;

		// Token: 0x0400181A RID: 6170
		public bool noBuildables;
	}
}
