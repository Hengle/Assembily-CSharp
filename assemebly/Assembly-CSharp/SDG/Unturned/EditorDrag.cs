using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000492 RID: 1170
	public class EditorDrag
	{
		// Token: 0x06001EAF RID: 7855 RVA: 0x000A7C0B File Offset: 0x000A600B
		public EditorDrag(Transform newTransform, Vector3 newScreen)
		{
			this._transform = newTransform;
			this._screen = newScreen;
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06001EB0 RID: 7856 RVA: 0x000A7C21 File Offset: 0x000A6021
		public Transform transform
		{
			get
			{
				return this._transform;
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06001EB1 RID: 7857 RVA: 0x000A7C29 File Offset: 0x000A6029
		public Vector3 screen
		{
			get
			{
				return this._screen;
			}
		}

		// Token: 0x0400126D RID: 4717
		private Transform _transform;

		// Token: 0x0400126E RID: 4718
		private Vector3 _screen;
	}
}
