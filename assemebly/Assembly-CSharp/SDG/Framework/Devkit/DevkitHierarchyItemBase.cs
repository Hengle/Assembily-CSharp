using System;
using SDG.Framework.IO.FormattedFiles;
using UnityEngine;

namespace SDG.Framework.Devkit
{
	// Token: 0x0200012A RID: 298
	public abstract class DevkitHierarchyItemBase : MonoBehaviour, IDevkitHierarchyItem, IFormattedFileReadable, IFormattedFileWritable
	{
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000914 RID: 2324 RVA: 0x0004DDE3 File Offset: 0x0004C1E3
		// (set) Token: 0x06000915 RID: 2325 RVA: 0x0004DDEB File Offset: 0x0004C1EB
		public virtual uint instanceID { get; set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x0004DDF4 File Offset: 0x0004C1F4
		public virtual Vector3 areaSelectCenter
		{
			get
			{
				return base.transform.position;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x0004DE01 File Offset: 0x0004C201
		public virtual GameObject areaSelectGameObject
		{
			get
			{
				return base.gameObject;
			}
		}

		// Token: 0x06000918 RID: 2328
		public abstract void read(IFormattedFileReader reader);

		// Token: 0x06000919 RID: 2329
		public abstract void write(IFormattedFileWriter writer);
	}
}
