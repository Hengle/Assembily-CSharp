using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000561 RID: 1377
	public class LocationNode : Node
	{
		// Token: 0x06002608 RID: 9736 RVA: 0x000DF563 File Offset: 0x000DD963
		public LocationNode(Vector3 newPoint) : this(newPoint, string.Empty)
		{
		}

		// Token: 0x06002609 RID: 9737 RVA: 0x000DF574 File Offset: 0x000DD974
		public LocationNode(Vector3 newPoint, string newName)
		{
			this._point = newPoint;
			if (Level.isEditor)
			{
				this._model = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Edit/Location"))).transform;
				base.model.name = "Node";
				base.model.position = base.point;
				base.model.parent = LevelNodes.models;
			}
			this.name = newName;
			this._type = ENodeType.LOCATION;
		}

		// Token: 0x040017B2 RID: 6066
		public string name;
	}
}
