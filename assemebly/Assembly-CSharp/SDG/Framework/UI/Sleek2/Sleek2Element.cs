using System;
using System.Collections.Generic;
using SDG.Framework.Translations;
using SDG.Framework.UI.Components;
using UnityEngine;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002C1 RID: 705
	public class Sleek2Element
	{
		// Token: 0x06001466 RID: 5222 RVA: 0x0006B5DC File Offset: 0x000699DC
		public Sleek2Element()
		{
			this.elements = new List<Sleek2Element>();
			this.gameObject = UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>("UI/Element"));
			this.name = "Element";
			this.transform = this.gameObject.GetComponent<RectTransform>();
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06001467 RID: 5223 RVA: 0x0006B62B File Offset: 0x00069A2B
		// (set) Token: 0x06001468 RID: 5224 RVA: 0x0006B633 File Offset: 0x00069A33
		public List<Sleek2Element> elements { get; protected set; }

		// Token: 0x1400004E RID: 78
		// (add) Token: 0x06001469 RID: 5225 RVA: 0x0006B63C File Offset: 0x00069A3C
		// (remove) Token: 0x0600146A RID: 5226 RVA: 0x0006B674 File Offset: 0x00069A74
		public event Sleek2ElementAddedHandler elementAdded;

		// Token: 0x1400004F RID: 79
		// (add) Token: 0x0600146B RID: 5227 RVA: 0x0006B6AC File Offset: 0x00069AAC
		// (remove) Token: 0x0600146C RID: 5228 RVA: 0x0006B6E4 File Offset: 0x00069AE4
		public event Sleek2ElementRemoveHandler elementRemoved;

		// Token: 0x14000050 RID: 80
		// (add) Token: 0x0600146D RID: 5229 RVA: 0x0006B71C File Offset: 0x00069B1C
		// (remove) Token: 0x0600146E RID: 5230 RVA: 0x0006B754 File Offset: 0x00069B54
		public event Sleek2ElementsClearedHandler elementsCleared;

		// Token: 0x14000051 RID: 81
		// (add) Token: 0x0600146F RID: 5231 RVA: 0x0006B78C File Offset: 0x00069B8C
		// (remove) Token: 0x06001470 RID: 5232 RVA: 0x0006B7C4 File Offset: 0x00069BC4
		public event Sleek2ElementDestroyedHandler destroyed;

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06001471 RID: 5233 RVA: 0x0006B7FA File Offset: 0x00069BFA
		// (set) Token: 0x06001472 RID: 5234 RVA: 0x0006B802 File Offset: 0x00069C02
		public GameObject gameObject { get; protected set; }

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06001473 RID: 5235 RVA: 0x0006B80B File Offset: 0x00069C0B
		// (set) Token: 0x06001474 RID: 5236 RVA: 0x0006B818 File Offset: 0x00069C18
		public string name
		{
			get
			{
				return this.gameObject.name;
			}
			set
			{
				this.gameObject.name = value;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06001475 RID: 5237 RVA: 0x0006B826 File Offset: 0x00069C26
		// (set) Token: 0x06001476 RID: 5238 RVA: 0x0006B833 File Offset: 0x00069C33
		public bool isVisible
		{
			get
			{
				return this.gameObject.activeSelf;
			}
			set
			{
				this.gameObject.SetActive(value);
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06001477 RID: 5239 RVA: 0x0006B841 File Offset: 0x00069C41
		// (set) Token: 0x06001478 RID: 5240 RVA: 0x0006B849 File Offset: 0x00069C49
		public RectTransform transform { get; protected set; }

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06001479 RID: 5241 RVA: 0x0006B852 File Offset: 0x00069C52
		// (set) Token: 0x0600147A RID: 5242 RVA: 0x0006B85A File Offset: 0x00069C5A
		public Sleek2Element parent { get; protected set; }

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x0600147B RID: 5243 RVA: 0x0006B863 File Offset: 0x00069C63
		// (set) Token: 0x0600147C RID: 5244 RVA: 0x0006B86C File Offset: 0x00069C6C
		public TranslatedText tooltip
		{
			get
			{
				return this._tooltip;
			}
			set
			{
				this._tooltip = value;
				if (this.tooltip != null && this.tooltipComponent == null)
				{
					this.tooltipComponent = this.gameObject.AddComponent<HoverTranslatedLabelTooltip>();
				}
				if (this.tooltipComponent != null)
				{
					this.tooltipComponent.translation = this.tooltip;
				}
			}
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x0006B8CF File Offset: 0x00069CCF
		public virtual void addElement(Sleek2Element element)
		{
			this.addElement(element, this.elements.Count);
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x0006B8E4 File Offset: 0x00069CE4
		public virtual void addElement(Sleek2Element element, int insertIndex)
		{
			if (element.parent != null)
			{
				element.parent.removeElement(element);
			}
			element.parent = this;
			element.transform.SetParent(this.transform, false);
			insertIndex = Mathf.Clamp(insertIndex, 0, this.elements.Count);
			this.elements.Insert(insertIndex, element);
			this.triggerElementAdded(element);
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x0006B949 File Offset: 0x00069D49
		public virtual void removeElement(Sleek2Element element)
		{
			if (element.parent != this)
			{
				return;
			}
			element.parent = null;
			element.transform.SetParent(null, false);
			this.elements.Remove(element);
			this.triggerElementRemoved(element);
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x0006B980 File Offset: 0x00069D80
		public virtual void clearElements()
		{
			for (int i = this.elements.Count - 1; i >= 0; i--)
			{
				if (this.elements[i] != null)
				{
					this.elements[i].destroy();
				}
			}
			this.elements.Clear();
			this.triggerElementsCleared();
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x0006B9E4 File Offset: 0x00069DE4
		public virtual void destroy()
		{
			if (this.parent != null)
			{
				this.parent.removeElement(this);
			}
			for (int i = this.elements.Count - 1; i >= 0; i--)
			{
				if (this.elements[i] != null)
				{
					this.elements[i].destroy();
				}
			}
			this.elements.Clear();
			UnityEngine.Object.Destroy(this.gameObject);
			this.triggerDestroyed();
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x0006BA69 File Offset: 0x00069E69
		protected virtual void triggerElementAdded(Sleek2Element element)
		{
			if (this.elementAdded != null)
			{
				this.elementAdded(this, element);
			}
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x0006BA83 File Offset: 0x00069E83
		protected virtual void triggerElementRemoved(Sleek2Element element)
		{
			if (this.elementRemoved != null)
			{
				this.elementRemoved(this, element);
			}
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x0006BA9D File Offset: 0x00069E9D
		protected virtual void triggerElementsCleared()
		{
			if (this.elementsCleared != null)
			{
				this.elementsCleared(this);
			}
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x0006BAB6 File Offset: 0x00069EB6
		protected virtual void triggerDestroyed()
		{
			if (this.destroyed != null)
			{
				this.destroyed(this);
			}
		}

		// Token: 0x04000BC4 RID: 3012
		protected HoverTranslatedLabelTooltip tooltipComponent;

		// Token: 0x04000BC5 RID: 3013
		protected TranslatedText _tooltip;
	}
}
