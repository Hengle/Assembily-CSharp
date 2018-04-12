using System;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.InspectorUI.TypeInspectors
{
	// Token: 0x02000276 RID: 630
	public class Sleek2ColorInspector : Sleek2KeyValueInspector
	{
		// Token: 0x0600126E RID: 4718 RVA: 0x00075A58 File Offset: 0x00073E58
		public Sleek2ColorInspector()
		{
			base.name = "Color_Inspector";
			this.field_r = new Sleek2FloatField();
			this.field_r.transform.reset();
			this.field_r.transform.anchorMin = new Vector2(0f, 0f);
			this.field_r.transform.anchorMax = new Vector2(0.25f, 1f);
			this.field_r.floatSubmitted += this.handleFieldRSubmitted;
			base.valuePanel.addElement(this.field_r);
			this.field_g = new Sleek2FloatField();
			this.field_g.transform.reset();
			this.field_g.transform.anchorMin = new Vector2(0.25f, 0f);
			this.field_g.transform.anchorMax = new Vector2(0.5f, 1f);
			this.field_g.floatSubmitted += this.handleFieldGSubmitted;
			base.valuePanel.addElement(this.field_g);
			this.field_b = new Sleek2FloatField();
			this.field_b.transform.reset();
			this.field_b.transform.anchorMin = new Vector2(0.5f, 0f);
			this.field_b.transform.anchorMax = new Vector2(0.75f, 1f);
			this.field_b.floatSubmitted += this.handleFieldBSubmitted;
			base.valuePanel.addElement(this.field_b);
			this.field_a = new Sleek2FloatField();
			this.field_a.transform.reset();
			this.field_a.transform.anchorMin = new Vector2(0.75f, 0f);
			this.field_a.transform.anchorMax = new Vector2(1f, 1f);
			this.field_a.floatSubmitted += this.handleFieldASubmitted;
			base.valuePanel.addElement(this.field_a);
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x0600126F RID: 4719 RVA: 0x00075C7A File Offset: 0x0007407A
		// (set) Token: 0x06001270 RID: 4720 RVA: 0x00075C82 File Offset: 0x00074082
		public Sleek2FloatField field_r { get; protected set; }

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06001271 RID: 4721 RVA: 0x00075C8B File Offset: 0x0007408B
		// (set) Token: 0x06001272 RID: 4722 RVA: 0x00075C93 File Offset: 0x00074093
		public Sleek2FloatField field_g { get; protected set; }

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06001273 RID: 4723 RVA: 0x00075C9C File Offset: 0x0007409C
		// (set) Token: 0x06001274 RID: 4724 RVA: 0x00075CA4 File Offset: 0x000740A4
		public Sleek2FloatField field_b { get; protected set; }

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06001275 RID: 4725 RVA: 0x00075CAD File Offset: 0x000740AD
		// (set) Token: 0x06001276 RID: 4726 RVA: 0x00075CB5 File Offset: 0x000740B5
		public Sleek2FloatField field_a { get; protected set; }

		// Token: 0x06001277 RID: 4727 RVA: 0x00075CC0 File Offset: 0x000740C0
		public override void inspect(ObjectInspectableInfo newInspectable)
		{
			base.inspect(newInspectable);
			if (base.inspectable == null)
			{
				return;
			}
			this.field_r.fieldComponent.interactable = base.inspectable.canWrite;
			this.field_g.fieldComponent.interactable = base.inspectable.canWrite;
			this.field_b.fieldComponent.interactable = base.inspectable.canWrite;
			this.field_a.fieldComponent.interactable = base.inspectable.canWrite;
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x00075D4C File Offset: 0x0007414C
		public override void refresh()
		{
			if (base.inspectable == null || !base.inspectable.canRead)
			{
				return;
			}
			Color color = (Color)base.inspectable.value;
			if (!this.field_r.fieldComponent.isFocused)
			{
				this.field_r.value = color.r;
			}
			if (!this.field_g.fieldComponent.isFocused)
			{
				this.field_b.value = color.g;
			}
			if (!this.field_b.fieldComponent.isFocused)
			{
				this.field_b.value = color.b;
			}
			if (!this.field_a.fieldComponent.isFocused)
			{
				this.field_a.value = color.a;
			}
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x00075E24 File Offset: 0x00074224
		protected void handleFieldRSubmitted(Sleek2FloatField field, float value)
		{
			Color color = (Color)base.inspectable.value;
			color.r = value;
			base.inspectable.value = color;
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x00075E5C File Offset: 0x0007425C
		protected void handleFieldGSubmitted(Sleek2FloatField field, float value)
		{
			Color color = (Color)base.inspectable.value;
			color.g = value;
			base.inspectable.value = color;
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x00075E94 File Offset: 0x00074294
		protected void handleFieldBSubmitted(Sleek2FloatField field, float value)
		{
			Color color = (Color)base.inspectable.value;
			color.b = value;
			base.inspectable.value = color;
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x00075ECC File Offset: 0x000742CC
		protected void handleFieldASubmitted(Sleek2FloatField field, float value)
		{
			Color color = (Color)base.inspectable.value;
			color.a = value;
			base.inspectable.value = color;
		}
	}
}
