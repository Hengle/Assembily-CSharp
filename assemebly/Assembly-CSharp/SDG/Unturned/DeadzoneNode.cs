using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000522 RID: 1314
	public class DeadzoneNode : Node, IDeadzoneNode
	{
		// Token: 0x060023A2 RID: 9122 RVA: 0x000C5F70 File Offset: 0x000C4370
		public DeadzoneNode(Vector3 newPoint) : this(newPoint, 0f)
		{
		}

		// Token: 0x060023A3 RID: 9123 RVA: 0x000C5F80 File Offset: 0x000C4380
		public DeadzoneNode(Vector3 newPoint, float newRadius)
		{
			this._point = newPoint;
			if (Level.isEditor)
			{
				this._model = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Edit/Deadzone"))).transform;
				base.model.name = "Node";
				base.model.position = base.point;
				base.model.parent = LevelNodes.models;
			}
			if (!Level.isEditor)
			{
				this.radius = Mathf.Pow((DeadzoneNode.MIN_SIZE + newRadius * (DeadzoneNode.MAX_SIZE - DeadzoneNode.MIN_SIZE)) / 2f, 2f);
			}
			else
			{
				this.radius = newRadius;
			}
			this._type = ENodeType.DEADZONE;
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x060023A4 RID: 9124 RVA: 0x000C603A File Offset: 0x000C443A
		// (set) Token: 0x060023A5 RID: 9125 RVA: 0x000C6044 File Offset: 0x000C4444
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
					float num = DeadzoneNode.MIN_SIZE + this.radius * (DeadzoneNode.MAX_SIZE - DeadzoneNode.MIN_SIZE);
					base.model.transform.localScale = new Vector3(num, num, num);
				}
			}
		}

		// Token: 0x0400158F RID: 5519
		public static readonly float MIN_SIZE = 32f;

		// Token: 0x04001590 RID: 5520
		public static readonly float MAX_SIZE = 1024f;

		// Token: 0x04001591 RID: 5521
		private float _radius;
	}
}
