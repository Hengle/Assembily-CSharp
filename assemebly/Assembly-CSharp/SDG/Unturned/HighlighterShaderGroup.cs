using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000732 RID: 1842
	public class HighlighterShaderGroup
	{
		// Token: 0x04002367 RID: 9063
		public Material materialTemplate;

		// Token: 0x04002368 RID: 9064
		public Dictionary<Texture2D, HighlighterBatch> batchableTextures = new Dictionary<Texture2D, HighlighterBatch>();

		// Token: 0x04002369 RID: 9065
		public FilterMode filterMode;
	}
}
