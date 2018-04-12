using System;
using SDG.Framework.UI.Components;
using SDG.Framework.UI.Sleek2;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.InspectorUI.TypeInspectors
{
	// Token: 0x02000264 RID: 612
	public class Sleek2TypeReferenceInspector<T> : Sleek2KeyValueInspector where T : Asset
	{
		// Token: 0x060011F7 RID: 4599 RVA: 0x000744A4 File Offset: 0x000728A4
		public Sleek2TypeReferenceInspector()
		{
			base.name = "Type_Reference_Inspector";
			this.button = new Sleek2ImageLabelButton();
			this.button.transform.reset();
			base.valuePanel.addElement(this.button);
			DragableDestination dragableDestination = this.button.gameObject.AddComponent<DragableDestination>();
			this.destination = new DragableTypeDestination<T>();
			this.destination.typeReferenceDocked += this.handleTypeReferenceDocked;
			dragableDestination.dropHandler = this.destination;
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060011F8 RID: 4600 RVA: 0x0007452E File Offset: 0x0007292E
		// (set) Token: 0x060011F9 RID: 4601 RVA: 0x00074536 File Offset: 0x00072936
		public Sleek2ImageLabelButton button { get; protected set; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060011FA RID: 4602 RVA: 0x0007453F File Offset: 0x0007293F
		// (set) Token: 0x060011FB RID: 4603 RVA: 0x00074547 File Offset: 0x00072947
		public DragableTypeDestination<T> destination { get; protected set; }

		// Token: 0x060011FC RID: 4604 RVA: 0x00074550 File Offset: 0x00072950
		public override void inspect(ObjectInspectableInfo newInspectable)
		{
			base.inspect(newInspectable);
			if (base.inspectable == null)
			{
				return;
			}
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x00074568 File Offset: 0x00072968
		public override void refresh()
		{
			if (base.inspectable == null || !base.inspectable.canRead)
			{
				return;
			}
			TypeReference<T> typeReference = (TypeReference<T>)base.inspectable.value;
			if (typeReference.isValid)
			{
				this.button.label.textComponent.text = typeReference.ToString();
			}
			else
			{
				this.button.label.textComponent.text = "nullptr";
			}
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x000745EF File Offset: 0x000729EF
		protected virtual void handleTypeReferenceDocked(TypeReference<T> typeReference)
		{
			base.inspectable.value = typeReference;
		}
	}
}
