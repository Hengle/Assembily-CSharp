using System;
using SDG.Framework.UI.Components;
using SDG.Framework.UI.Sleek2;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.InspectorUI.TypeInspectors
{
	// Token: 0x0200025E RID: 606
	public class Sleek2ContentReferenceInspector<T> : Sleek2KeyValueInspector where T : UnityEngine.Object
	{
		// Token: 0x060011D1 RID: 4561 RVA: 0x00073B28 File Offset: 0x00071F28
		public Sleek2ContentReferenceInspector()
		{
			base.name = "Content_Reference_Inspector";
			this.button = new Sleek2ImageLabelButton();
			this.button.transform.reset();
			base.valuePanel.addElement(this.button);
			DragableDestination dragableDestination = this.button.gameObject.AddComponent<DragableDestination>();
			this.destination = new DragableContentDestination<T>();
			this.destination.contentReferenceDocked += this.handleContentReferenceDocked;
			dragableDestination.dropHandler = this.destination;
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060011D2 RID: 4562 RVA: 0x00073BB2 File Offset: 0x00071FB2
		// (set) Token: 0x060011D3 RID: 4563 RVA: 0x00073BBA File Offset: 0x00071FBA
		public Sleek2ImageLabelButton button { get; protected set; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060011D4 RID: 4564 RVA: 0x00073BC3 File Offset: 0x00071FC3
		// (set) Token: 0x060011D5 RID: 4565 RVA: 0x00073BCB File Offset: 0x00071FCB
		public DragableContentDestination<T> destination { get; protected set; }

		// Token: 0x060011D6 RID: 4566 RVA: 0x00073BD4 File Offset: 0x00071FD4
		public override void inspect(ObjectInspectableInfo newInspectable)
		{
			base.inspect(newInspectable);
			if (base.inspectable == null)
			{
				return;
			}
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x00073BEC File Offset: 0x00071FEC
		public override void refresh()
		{
			if (base.inspectable == null || !base.inspectable.canRead)
			{
				return;
			}
			ContentReference<T> reference = (ContentReference<T>)base.inspectable.value;
			ContentFile contentFile = Assets.find<T>(reference);
			if (contentFile != null)
			{
				this.button.label.textComponent.text = contentFile.name;
			}
			else if (reference.isValid)
			{
				this.button.label.textComponent.text = reference.ToString();
			}
			else
			{
				this.button.label.textComponent.text = "nullptr";
			}
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x00073CA0 File Offset: 0x000720A0
		protected virtual void handleContentReferenceDocked(ContentReference<T> contentReference)
		{
			base.inspectable.value = contentReference;
		}
	}
}
