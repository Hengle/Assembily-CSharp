using System;
using SDG.Framework.UI.Components;
using SDG.Framework.UI.Sleek2;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.ContentBrowserUI
{
	// Token: 0x02000231 RID: 561
	public class ContentBrowserFileButton : Sleek2ImageButton
	{
		// Token: 0x0600108A RID: 4234 RVA: 0x0006CD48 File Offset: 0x0006B148
		public ContentBrowserFileButton(ContentFile newFile)
		{
			this.file = newFile;
			Sleek2Label sleek2Label = new Sleek2Label();
			sleek2Label.transform.reset();
			sleek2Label.textComponent.text = this.file.name;
			sleek2Label.textComponent.color = Sleek2Config.darkTextColor;
			this.addElement(sleek2Label);
			Type guessedType = this.file.guessedType;
			if (guessedType == null)
			{
				return;
			}
			Type type = typeof(ContentReference<>).MakeGenericType(new Type[]
			{
				guessedType
			});
			this.dragable = base.gameObject.AddComponent<DragableSystemObject>();
			this.dragable.target = base.transform;
			this.dragable.source = Activator.CreateInstance(type, new object[]
			{
				this.file.rootDirectory.name,
				this.file.path
			});
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x0600108B RID: 4235 RVA: 0x0006CE27 File Offset: 0x0006B227
		// (set) Token: 0x0600108C RID: 4236 RVA: 0x0006CE2F File Offset: 0x0006B22F
		public ContentFile file { get; protected set; }

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x0600108D RID: 4237 RVA: 0x0006CE38 File Offset: 0x0006B238
		// (set) Token: 0x0600108E RID: 4238 RVA: 0x0006CE40 File Offset: 0x0006B240
		public DragableSystemObject dragable { get; protected set; }
	}
}
