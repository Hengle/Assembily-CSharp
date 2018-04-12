using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200051E RID: 1310
	public class ArenaNode : Node
	{
		// Token: 0x06002399 RID: 9113 RVA: 0x000C5D83 File Offset: 0x000C4183
		public ArenaNode(Vector3 newPoint) : this(newPoint, 0f)
		{
		}

		// Token: 0x0600239A RID: 9114 RVA: 0x000C5D94 File Offset: 0x000C4194
		public ArenaNode(Vector3 newPoint, float newRadius)
		{
			this._point = newPoint;
			if (Level.isEditor)
			{
				this._model = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Edit/Arena"))).transform;
				base.model.name = "Node";
				base.model.position = base.point;
				base.model.parent = LevelNodes.models;
			}
			if (!Level.isEditor)
			{
				this.radius = (ArenaNode.MIN_SIZE + newRadius * (ArenaNode.MAX_SIZE - ArenaNode.MIN_SIZE)) / 2f;
			}
			else
			{
				this.radius = newRadius;
			}
			this._type = ENodeType.ARENA;
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x0600239B RID: 9115 RVA: 0x000C5E44 File Offset: 0x000C4244
		// (set) Token: 0x0600239C RID: 9116 RVA: 0x000C5E4C File Offset: 0x000C424C
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
					float num = ArenaNode.MIN_SIZE + this.radius * (ArenaNode.MAX_SIZE - ArenaNode.MIN_SIZE);
					base.model.transform.localScale = new Vector3(num, num, num);
				}
			}
		}

		// Token: 0x0400158A RID: 5514
		public static readonly float MIN_SIZE = 128f;

		// Token: 0x0400158B RID: 5515
		public static readonly float MAX_SIZE = 4096f;

		// Token: 0x0400158C RID: 5516
		private float _radius;
	}
}
