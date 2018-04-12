using System;
using SDG.Framework.Translations;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.InspectorUI.TypeInspectors
{
	// Token: 0x02000263 RID: 611
	public class Sleek2TranslationReferenceInspector : Sleek2KeyValueInspector
	{
		// Token: 0x060011EC RID: 4588 RVA: 0x00074128 File Offset: 0x00072528
		public Sleek2TranslationReferenceInspector()
		{
			base.name = "Type_Reference_Inspector";
			this.nsField = new Sleek2Field();
			this.nsField.transform.anchorMin = new Vector2(0f, 0.5f);
			this.nsField.transform.anchorMax = new Vector2(0.2f, 1f);
			this.nsField.transform.sizeDelta = new Vector2(0f, 0f);
			this.nsField.submitted += this.handleNSFieldSubmitted;
			base.valuePanel.addElement(this.nsField);
			this.tokenField = new Sleek2Field();
			this.tokenField.transform.anchorMin = new Vector2(0.2f, 0.5f);
			this.tokenField.transform.anchorMax = new Vector2(1f, 1f);
			this.tokenField.transform.sizeDelta = new Vector2(0f, 0f);
			this.tokenField.submitted += this.handleTokenFieldSubmitted;
			base.valuePanel.addElement(this.tokenField);
			this.preview = new Sleek2TranslatedLabel();
			this.preview.transform.anchorMin = new Vector2(0f, 0f);
			this.preview.transform.anchorMax = new Vector2(1f, 0.5f);
			this.preview.transform.sizeDelta = new Vector2(0f, 0f);
			this.preview.translation = new TranslatedText(TranslationReference.invalid);
			base.valuePanel.addElement(this.preview);
			this.layoutComponent.preferredHeight = 60f;
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060011ED RID: 4589 RVA: 0x00074304 File Offset: 0x00072704
		// (set) Token: 0x060011EE RID: 4590 RVA: 0x0007430C File Offset: 0x0007270C
		public Sleek2Field nsField { get; protected set; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060011EF RID: 4591 RVA: 0x00074315 File Offset: 0x00072715
		// (set) Token: 0x060011F0 RID: 4592 RVA: 0x0007431D File Offset: 0x0007271D
		public Sleek2Field tokenField { get; protected set; }

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060011F1 RID: 4593 RVA: 0x00074326 File Offset: 0x00072726
		// (set) Token: 0x060011F2 RID: 4594 RVA: 0x0007432E File Offset: 0x0007272E
		public Sleek2TranslatedLabel preview { get; protected set; }

		// Token: 0x060011F3 RID: 4595 RVA: 0x00074338 File Offset: 0x00072738
		public override void inspect(ObjectInspectableInfo newInspectable)
		{
			base.inspect(newInspectable);
			if (base.inspectable == null)
			{
				return;
			}
			this.nsField.fieldComponent.interactable = base.inspectable.canWrite;
			this.tokenField.fieldComponent.interactable = base.inspectable.canWrite;
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x00074390 File Offset: 0x00072790
		public override void refresh()
		{
			if (base.inspectable == null || !base.inspectable.canRead)
			{
				return;
			}
			TranslationReference reference = (TranslationReference)base.inspectable.value;
			if (!this.nsField.fieldComponent.isFocused)
			{
				this.nsField.fieldComponent.text = reference.ns;
			}
			if (!this.tokenField.fieldComponent.isFocused)
			{
				this.tokenField.fieldComponent.text = reference.token;
			}
			this.preview.translation.reference = reference;
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x00074434 File Offset: 0x00072834
		protected void handleNSFieldSubmitted(Sleek2Field field, string value)
		{
			TranslationReference translationReference = (TranslationReference)base.inspectable.value;
			translationReference.ns = value;
			base.inspectable.value = translationReference;
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x0007446C File Offset: 0x0007286C
		protected void handleTokenFieldSubmitted(Sleek2Field field, string value)
		{
			TranslationReference translationReference = (TranslationReference)base.inspectable.value;
			translationReference.token = value;
			base.inspectable.value = translationReference;
		}
	}
}
