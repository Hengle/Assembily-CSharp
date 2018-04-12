using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002C4 RID: 708
	public class Sleek2Field : Sleek2Element
	{
		// Token: 0x0600148E RID: 5262 RVA: 0x00080524 File Offset: 0x0007E924
		public Sleek2Field()
		{
			base.gameObject.name = "Field";
			this.imageComponent = base.gameObject.AddComponent<Image>();
			this.imageComponent.sprite = Resources.Load<Sprite>("Sprites/UI/Button_Background");
			this.imageComponent.type = Image.Type.Sliced;
			this.fieldComponent = base.gameObject.AddComponent<InputField>();
			this.fieldComponent.onValueChanged.AddListener(new UnityAction<string>(this.handleValueChange));
			this.fieldComponent.onEndEdit.AddListener(new UnityAction<string>(this.handleEndEdit));
			Sleek2Label sleek2Label = new Sleek2Label();
			sleek2Label.transform.reset();
			sleek2Label.textComponent.color = Sleek2Config.darkTextColor;
			sleek2Label.textComponent.supportRichText = false;
			this.fieldComponent.textComponent = sleek2Label.textComponent;
			this.addElement(sleek2Label);
		}

		// Token: 0x14000052 RID: 82
		// (add) Token: 0x0600148F RID: 5263 RVA: 0x0008060C File Offset: 0x0007EA0C
		// (remove) Token: 0x06001490 RID: 5264 RVA: 0x00080644 File Offset: 0x0007EA44
		public event FieldTypedHandler typed;

		// Token: 0x14000053 RID: 83
		// (add) Token: 0x06001491 RID: 5265 RVA: 0x0008067C File Offset: 0x0007EA7C
		// (remove) Token: 0x06001492 RID: 5266 RVA: 0x000806B4 File Offset: 0x0007EAB4
		public event FieldSubmittedHandler submitted;

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06001493 RID: 5267 RVA: 0x000806EA File Offset: 0x0007EAEA
		// (set) Token: 0x06001494 RID: 5268 RVA: 0x000806F2 File Offset: 0x0007EAF2
		public Image imageComponent { get; protected set; }

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06001495 RID: 5269 RVA: 0x000806FB File Offset: 0x0007EAFB
		// (set) Token: 0x06001496 RID: 5270 RVA: 0x00080703 File Offset: 0x0007EB03
		public InputField fieldComponent { get; protected set; }

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06001497 RID: 5271 RVA: 0x0008070C File Offset: 0x0007EB0C
		// (set) Token: 0x06001498 RID: 5272 RVA: 0x00080719 File Offset: 0x0007EB19
		public virtual string text
		{
			get
			{
				return this.fieldComponent.text;
			}
			set
			{
				this.fieldComponent.text = value;
				this.fieldComponent.textComponent.text = value;
			}
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x00080738 File Offset: 0x0007EB38
		protected virtual void triggerTyped(string value)
		{
			if (this.typed != null)
			{
				this.typed(this, value);
			}
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x00080752 File Offset: 0x0007EB52
		protected virtual void triggerSubmitted(string value)
		{
			if (this.submitted != null)
			{
				this.submitted(this, value);
			}
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x0008076C File Offset: 0x0007EB6C
		protected virtual void handleValueChange(string text)
		{
			this.triggerTyped(text);
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x00080775 File Offset: 0x0007EB75
		protected virtual void handleEndEdit(string text)
		{
			this.triggerSubmitted(text);
		}
	}
}
