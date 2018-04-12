using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000519 RID: 1305
	public class AirdropNode : Node
	{
		// Token: 0x06002381 RID: 9089 RVA: 0x000C54D8 File Offset: 0x000C38D8
		public AirdropNode(Vector3 newPoint) : this(newPoint, 0)
		{
		}

		// Token: 0x06002382 RID: 9090 RVA: 0x000C54E4 File Offset: 0x000C38E4
		public AirdropNode(Vector3 newPoint, ushort newID)
		{
			this._point = newPoint;
			if (Level.isEditor)
			{
				this._model = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Edit/Airdrop"))).transform;
				base.model.name = "Node";
				base.model.position = base.point;
				base.model.parent = LevelNodes.models;
			}
			this.id = newID;
			this._type = ENodeType.AIRDROP;
		}

		// Token: 0x0400157E RID: 5502
		public ushort id;
	}
}
