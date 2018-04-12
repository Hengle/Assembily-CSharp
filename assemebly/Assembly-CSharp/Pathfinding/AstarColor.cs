using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000050 RID: 80
	[Serializable]
	public class AstarColor
	{
		// Token: 0x06000354 RID: 852 RVA: 0x0001A6F8 File Offset: 0x00018AF8
		public AstarColor()
		{
			this._NodeConnection = new Color(1f, 1f, 1f, 0.9f);
			this._UnwalkableNode = new Color(1f, 0f, 0f, 0.5f);
			this._BoundsHandles = new Color(0.29f, 0.454f, 0.741f, 0.9f);
			this._ConnectionLowLerp = new Color(0f, 1f, 0f, 0.5f);
			this._ConnectionHighLerp = new Color(1f, 0f, 0f, 0.5f);
			this._MeshEdgeColor = new Color(0f, 0f, 0f, 0.5f);
			this._MeshColor = new Color(0.125f, 0.686f, 0f, 0.19f);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0001A7E4 File Offset: 0x00018BE4
		public static Color GetAreaColor(uint area)
		{
			if (AstarColor.AreaColors == null || (ulong)area >= (ulong)((long)AstarColor.AreaColors.Length))
			{
				return AstarMath.IntToColor((int)area, 1f);
			}
			return AstarColor.AreaColors[(int)area];
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0001A81C File Offset: 0x00018C1C
		public void OnEnable()
		{
			AstarColor.NodeConnection = this._NodeConnection;
			AstarColor.UnwalkableNode = this._UnwalkableNode;
			AstarColor.BoundsHandles = this._BoundsHandles;
			AstarColor.ConnectionLowLerp = this._ConnectionLowLerp;
			AstarColor.ConnectionHighLerp = this._ConnectionHighLerp;
			AstarColor.MeshEdgeColor = this._MeshEdgeColor;
			AstarColor.MeshColor = this._MeshColor;
			AstarColor.AreaColors = this._AreaColors;
		}

		// Token: 0x04000288 RID: 648
		public Color _NodeConnection;

		// Token: 0x04000289 RID: 649
		public Color _UnwalkableNode;

		// Token: 0x0400028A RID: 650
		public Color _BoundsHandles;

		// Token: 0x0400028B RID: 651
		public Color _ConnectionLowLerp;

		// Token: 0x0400028C RID: 652
		public Color _ConnectionHighLerp;

		// Token: 0x0400028D RID: 653
		public Color _MeshEdgeColor;

		// Token: 0x0400028E RID: 654
		public Color _MeshColor;

		// Token: 0x0400028F RID: 655
		public Color[] _AreaColors;

		// Token: 0x04000290 RID: 656
		public static Color NodeConnection = new Color(1f, 1f, 1f, 0.9f);

		// Token: 0x04000291 RID: 657
		public static Color UnwalkableNode = new Color(1f, 0f, 0f, 0.5f);

		// Token: 0x04000292 RID: 658
		public static Color BoundsHandles = new Color(0.29f, 0.454f, 0.741f, 0.9f);

		// Token: 0x04000293 RID: 659
		public static Color ConnectionLowLerp = new Color(0f, 1f, 0f, 0.5f);

		// Token: 0x04000294 RID: 660
		public static Color ConnectionHighLerp = new Color(1f, 0f, 0f, 0.5f);

		// Token: 0x04000295 RID: 661
		public static Color MeshEdgeColor = new Color(0f, 0f, 0f, 0.5f);

		// Token: 0x04000296 RID: 662
		public static Color MeshColor = new Color(0f, 0f, 0f, 0.5f);

		// Token: 0x04000297 RID: 663
		private static Color[] AreaColors;
	}
}
