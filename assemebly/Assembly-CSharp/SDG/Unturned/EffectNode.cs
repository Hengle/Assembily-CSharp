using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200052D RID: 1325
	public class EffectNode : Node, IAmbianceNode
	{
		// Token: 0x060023AA RID: 9130 RVA: 0x000C60B0 File Offset: 0x000C44B0
		public EffectNode(Vector3 newPoint) : this(newPoint, ENodeShape.SPHERE, 0f, Vector3.one, 0, false, false)
		{
		}

		// Token: 0x060023AB RID: 9131 RVA: 0x000C60C8 File Offset: 0x000C44C8
		public EffectNode(Vector3 newPoint, ENodeShape newShape, float newRadius, Vector3 newBounds, ushort newID, bool newNoWater, bool newNoLighting)
		{
			this._point = newPoint;
			if (Level.isEditor)
			{
				this._model = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Edit/Effect"))).transform;
				base.model.name = "Node";
				base.model.position = base.point;
				base.model.parent = LevelNodes.models;
			}
			this.shape = newShape;
			if (!Level.isEditor)
			{
				this.radius = Mathf.Pow((EffectNode.MIN_SIZE + newRadius * (EffectNode.MAX_SIZE - EffectNode.MIN_SIZE)) / 2f, 2f);
			}
			else
			{
				this.radius = newRadius;
			}
			this._editorRadius = Mathf.Pow((EffectNode.MIN_SIZE + this.radius * (EffectNode.MAX_SIZE - EffectNode.MIN_SIZE)) / 2f, 2f);
			this.bounds = newBounds;
			this.id = newID;
			this.noWater = newNoWater;
			this.noLighting = newNoLighting;
			this._type = ENodeType.EFFECT;
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x060023AC RID: 9132 RVA: 0x000C61D7 File Offset: 0x000C45D7
		// (set) Token: 0x060023AD RID: 9133 RVA: 0x000C61E0 File Offset: 0x000C45E0
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
					this.updateScale();
				}
				if (Level.isEditor)
				{
					this._editorRadius = Mathf.Pow((EffectNode.MIN_SIZE + this.radius * (EffectNode.MAX_SIZE - EffectNode.MIN_SIZE)) / 2f, 2f);
				}
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x060023AE RID: 9134 RVA: 0x000C6243 File Offset: 0x000C4643
		public float editorRadius
		{
			get
			{
				return this._editorRadius;
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x060023AF RID: 9135 RVA: 0x000C624B File Offset: 0x000C464B
		// (set) Token: 0x060023B0 RID: 9136 RVA: 0x000C6253 File Offset: 0x000C4653
		public Vector3 bounds
		{
			get
			{
				return this._bounds;
			}
			set
			{
				this._bounds = value;
				if (base.model != null)
				{
					this.updateScale();
				}
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x060023B1 RID: 9137 RVA: 0x000C6273 File Offset: 0x000C4673
		// (set) Token: 0x060023B2 RID: 9138 RVA: 0x000C627C File Offset: 0x000C467C
		public ENodeShape shape
		{
			get
			{
				return this._shape;
			}
			set
			{
				this._shape = value;
				if (base.model != null)
				{
					base.model.GetComponent<MeshFilter>().sharedMesh = ((GameObject)Resources.Load((this.shape != ENodeShape.SPHERE) ? "Materials/Box" : "Materials/Sphere")).GetComponent<MeshFilter>().sharedMesh;
					base.model.GetComponent<SphereCollider>().enabled = (this.shape == ENodeShape.SPHERE);
					base.model.GetComponent<BoxCollider>().enabled = (this.shape == ENodeShape.BOX);
					this.updateScale();
				}
			}
		}

		// Token: 0x060023B3 RID: 9139 RVA: 0x000C6318 File Offset: 0x000C4718
		private void updateScale()
		{
			if (this.shape == ENodeShape.SPHERE)
			{
				float num = EffectNode.MIN_SIZE + this.radius * (EffectNode.MAX_SIZE - EffectNode.MIN_SIZE);
				base.model.transform.localScale = new Vector3(num, num, num);
			}
			else if (this.shape == ENodeShape.BOX)
			{
				base.model.transform.localScale = this.bounds;
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x060023B4 RID: 9140 RVA: 0x000C6388 File Offset: 0x000C4788
		// (set) Token: 0x060023B5 RID: 9141 RVA: 0x000C6390 File Offset: 0x000C4790
		public ushort id { get; set; }

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x060023B6 RID: 9142 RVA: 0x000C6399 File Offset: 0x000C4799
		// (set) Token: 0x060023B7 RID: 9143 RVA: 0x000C63A1 File Offset: 0x000C47A1
		public bool noWater { get; set; }

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x060023B8 RID: 9144 RVA: 0x000C63AA File Offset: 0x000C47AA
		// (set) Token: 0x060023B9 RID: 9145 RVA: 0x000C63B2 File Offset: 0x000C47B2
		public bool noLighting { get; set; }

		// Token: 0x040015C8 RID: 5576
		public static readonly float MIN_SIZE = 8f;

		// Token: 0x040015C9 RID: 5577
		public static readonly float MAX_SIZE = 256f;

		// Token: 0x040015CA RID: 5578
		private float _radius;

		// Token: 0x040015CB RID: 5579
		private float _editorRadius;

		// Token: 0x040015CC RID: 5580
		private Vector3 _bounds;

		// Token: 0x040015CD RID: 5581
		private ENodeShape _shape;
	}
}
