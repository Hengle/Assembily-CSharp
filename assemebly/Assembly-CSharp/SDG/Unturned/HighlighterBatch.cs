using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000731 RID: 1841
	public class HighlighterBatch
	{
		// Token: 0x04002364 RID: 9060
		public Texture2D texture;

		// Token: 0x04002365 RID: 9061
		public Dictionary<Mesh, List<MeshFilter>> meshes = new Dictionary<Mesh, List<MeshFilter>>();

		// Token: 0x04002366 RID: 9062
		public List<MeshRenderer> renderers = new List<MeshRenderer>();
	}
}
