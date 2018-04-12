using System;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.InspectorUI.TypeInspectors
{
	// Token: 0x02000278 RID: 632
	public class Sleek2Vector3Inspector : Sleek2KeyValueInspector
	{
		// Token: 0x06001289 RID: 4745 RVA: 0x000762D0 File Offset: 0x000746D0
		public Sleek2Vector3Inspector()
		{
			base.name = "Vector3_Inspector";
			this.field_x = new Sleek2FloatField();
			this.field_x.transform.reset();
			this.field_x.transform.anchorMin = new Vector2(0f, 0f);
			this.field_x.transform.anchorMax = new Vector2(0.333f, 1f);
			this.field_x.floatSubmitted += this.handleFieldXSubmitted;
			base.valuePanel.addElement(this.field_x);
			this.field_y = new Sleek2FloatField();
			this.field_y.transform.reset();
			this.field_y.transform.anchorMin = new Vector2(0.333f, 0f);
			this.field_y.transform.anchorMax = new Vector2(0.667f, 1f);
			this.field_y.floatSubmitted += this.handleFieldYSubmitted;
			base.valuePanel.addElement(this.field_y);
			this.field_z = new Sleek2FloatField();
			this.field_z.transform.reset();
			this.field_z.transform.anchorMin = new Vector2(0.667f, 0f);
			this.field_z.transform.anchorMax = new Vector2(1f, 1f);
			this.field_z.floatSubmitted += this.handleFieldZSubmitted;
			base.valuePanel.addElement(this.field_z);
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x0600128A RID: 4746 RVA: 0x00076471 File Offset: 0x00074871
		// (set) Token: 0x0600128B RID: 4747 RVA: 0x00076479 File Offset: 0x00074879
		public Sleek2FloatField field_x { get; protected set; }

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x0600128C RID: 4748 RVA: 0x00076482 File Offset: 0x00074882
		// (set) Token: 0x0600128D RID: 4749 RVA: 0x0007648A File Offset: 0x0007488A
		public Sleek2FloatField field_y { get; protected set; }

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x0600128E RID: 4750 RVA: 0x00076493 File Offset: 0x00074893
		// (set) Token: 0x0600128F RID: 4751 RVA: 0x0007649B File Offset: 0x0007489B
		public Sleek2FloatField field_z { get; protected set; }

		// Token: 0x06001290 RID: 4752 RVA: 0x000764A4 File Offset: 0x000748A4
		public override void inspect(ObjectInspectableInfo newInspectable)
		{
			base.inspect(newInspectable);
			if (base.inspectable == null)
			{
				return;
			}
			this.field_x.fieldComponent.interactable = base.inspectable.canWrite;
			this.field_y.fieldComponent.interactable = base.inspectable.canWrite;
			this.field_z.fieldComponent.interactable = base.inspectable.canWrite;
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x00076518 File Offset: 0x00074918
		public override void refresh()
		{
			if (base.inspectable == null || !base.inspectable.canRead)
			{
				return;
			}
			Vector3 vector = (Vector3)base.inspectable.value;
			if (!this.field_x.fieldComponent.isFocused)
			{
				this.field_x.value = vector.x;
			}
			if (!this.field_y.fieldComponent.isFocused)
			{
				this.field_y.value = vector.y;
			}
			if (!this.field_z.fieldComponent.isFocused)
			{
				this.field_z.value = vector.z;
			}
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x000765C8 File Offset: 0x000749C8
		protected void handleFieldXSubmitted(Sleek2FloatField field, float value)
		{
			Vector3 vector = (Vector3)base.inspectable.value;
			vector.x = value;
			base.inspectable.value = vector;
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x00076600 File Offset: 0x00074A00
		protected void handleFieldYSubmitted(Sleek2FloatField field, float value)
		{
			Vector3 vector = (Vector3)base.inspectable.value;
			vector.y = value;
			base.inspectable.value = vector;
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x00076638 File Offset: 0x00074A38
		protected void handleFieldZSubmitted(Sleek2FloatField field, float value)
		{
			Vector3 vector = (Vector3)base.inspectable.value;
			vector.z = value;
			base.inspectable.value = vector;
		}
	}
}
