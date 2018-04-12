using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200048E RID: 1166
	public class Editor : MonoBehaviour
	{
		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06001E93 RID: 7827 RVA: 0x000A77F5 File Offset: 0x000A5BF5
		public static Editor editor
		{
			get
			{
				return Editor._editor;
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06001E94 RID: 7828 RVA: 0x000A77FC File Offset: 0x000A5BFC
		public EditorArea area
		{
			get
			{
				return this._area;
			}
		}

		// Token: 0x06001E95 RID: 7829 RVA: 0x000A7804 File Offset: 0x000A5C04
		public virtual void init()
		{
			this._area = base.GetComponent<EditorArea>();
			Editor._editor = this;
			if (Editor.onEditorCreated != null)
			{
				Editor.onEditorCreated();
			}
		}

		// Token: 0x06001E96 RID: 7830 RVA: 0x000A782C File Offset: 0x000A5C2C
		private void Start()
		{
			this.init();
		}

		// Token: 0x06001E97 RID: 7831 RVA: 0x000A7834 File Offset: 0x000A5C34
		public static void save()
		{
			EditorInteract.save();
			EditorTerrainHeight.save();
			EditorTerrainMaterials.save();
			EditorObjects.save();
			EditorSpawns.save();
		}

		// Token: 0x0400125D RID: 4701
		public static EditorCreated onEditorCreated;

		// Token: 0x0400125E RID: 4702
		private static Editor _editor;

		// Token: 0x0400125F RID: 4703
		private EditorArea _area;
	}
}
