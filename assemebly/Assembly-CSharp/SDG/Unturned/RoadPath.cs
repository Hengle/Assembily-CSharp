using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200056E RID: 1390
	public class RoadPath
	{
		// Token: 0x06002651 RID: 9809 RVA: 0x000E142C File Offset: 0x000DF82C
		public RoadPath(Transform vertex)
		{
			this.vertex = vertex;
			this.meshRenderer = vertex.GetComponent<MeshRenderer>();
			this.tangents = new Transform[2];
			this.tangents[0] = vertex.FindChild("Tangent_0");
			this.tangents[1] = vertex.FindChild("Tangent_1");
			this.meshRenderers = new MeshRenderer[2];
			this.meshRenderers[0] = this.tangents[0].GetComponent<MeshRenderer>();
			this.meshRenderers[1] = this.tangents[1].GetComponent<MeshRenderer>();
			this.lineRenderers = new LineRenderer[2];
			this.lineRenderers[0] = this.tangents[0].GetComponent<LineRenderer>();
			this.lineRenderers[1] = this.tangents[1].GetComponent<LineRenderer>();
			this.unhighlightVertex();
			this.unhighlightTangent(0);
			this.unhighlightTangent(1);
		}

		// Token: 0x06002652 RID: 9810 RVA: 0x000E1504 File Offset: 0x000DF904
		public void highlightVertex()
		{
			this.meshRenderer.material.color = Color.red;
		}

		// Token: 0x06002653 RID: 9811 RVA: 0x000E151B File Offset: 0x000DF91B
		public void unhighlightVertex()
		{
			this.meshRenderer.material.color = Color.white;
		}

		// Token: 0x06002654 RID: 9812 RVA: 0x000E1532 File Offset: 0x000DF932
		public void highlightTangent(int index)
		{
			this.meshRenderers[index].material.color = Color.red;
			this.lineRenderers[index].material.color = Color.red;
		}

		// Token: 0x06002655 RID: 9813 RVA: 0x000E1564 File Offset: 0x000DF964
		public void unhighlightTangent(int index)
		{
			Color color;
			if (index == 0)
			{
				color = Color.yellow;
			}
			else
			{
				color = Color.blue;
			}
			this.meshRenderers[index].material.color = color;
			this.lineRenderers[index].material.color = color;
		}

		// Token: 0x06002656 RID: 9814 RVA: 0x000E15AE File Offset: 0x000DF9AE
		public void setTangent(int index, Vector3 tangent)
		{
			this.tangents[index].localPosition = tangent;
			this.lineRenderers[index].SetPosition(1, -tangent);
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x000E15D2 File Offset: 0x000DF9D2
		public void remove()
		{
			UnityEngine.Object.Destroy(this.vertex.gameObject);
		}

		// Token: 0x040017FF RID: 6143
		public Transform vertex;

		// Token: 0x04001800 RID: 6144
		private MeshRenderer meshRenderer;

		// Token: 0x04001801 RID: 6145
		public Transform[] tangents;

		// Token: 0x04001802 RID: 6146
		private MeshRenderer[] meshRenderers;

		// Token: 0x04001803 RID: 6147
		private LineRenderer[] lineRenderers;
	}
}
