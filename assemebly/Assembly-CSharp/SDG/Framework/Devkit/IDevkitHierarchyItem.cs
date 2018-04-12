using System;
using SDG.Framework.IO.FormattedFiles;
using UnityEngine;

namespace SDG.Framework.Devkit
{
	// Token: 0x02000141 RID: 321
	public interface IDevkitHierarchyItem : IFormattedFileReadable, IFormattedFileWritable
	{
		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060009C9 RID: 2505
		// (set) Token: 0x060009CA RID: 2506
		uint instanceID { get; set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060009CB RID: 2507
		Vector3 areaSelectCenter { get; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060009CC RID: 2508
		GameObject areaSelectGameObject { get; }
	}
}
