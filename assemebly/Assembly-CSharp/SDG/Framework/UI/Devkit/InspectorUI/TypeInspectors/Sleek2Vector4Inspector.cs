using System;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.InspectorUI.TypeInspectors
{
	// Token: 0x02000279 RID: 633
	public class Sleek2Vector4Inspector : Sleek2KeyValueInspector
	{
		// Token: 0x06001295 RID: 4757 RVA: 0x00076670 File Offset: 0x00074A70
		public Sleek2Vector4Inspector()
		{
			base.name = "Vector4_Inspector";
			this.field_x = new Sleek2FloatField();
			this.field_x.transform.reset();
			this.field_x.transform.anchorMin = new Vector2(0f, 0f);
			this.field_x.transform.anchorMax = new Vector2(0.25f, 1f);
			this.field_x.floatSubmitted += this.handleFieldXSubmitted;
			base.valuePanel.addElement(this.field_x);
			this.field_y = new Sleek2FloatField();
			this.field_y.transform.reset();
			this.field_y.transform.anchorMin = new Vector2(0.25f, 0f);
			this.field_y.transform.anchorMax = new Vector2(0.5f, 1f);
			this.field_y.floatSubmitted += this.handleFieldYSubmitted;
			base.valuePanel.addElement(this.field_y);
			this.field_z = new Sleek2FloatField();
			this.field_z.transform.reset();
			this.field_z.transform.anchorMin = new Vector2(0.5f, 0f);
			this.field_z.transform.anchorMax = new Vector2(0.75f, 1f);
			this.field_z.floatSubmitted += this.handleFieldZSubmitted;
			base.valuePanel.addElement(this.field_z);
			this.field_w = new Sleek2FloatField();
			this.field_w.transform.reset();
			this.field_w.transform.anchorMin = new Vector2(0.75f, 0f);
			this.field_w.transform.anchorMax = new Vector2(1f, 1f);
			this.field_w.floatSubmitted += this.handleFieldZSubmitted;
			base.valuePanel.addElement(this.field_w);
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06001296 RID: 4758 RVA: 0x00076892 File Offset: 0x00074C92
		// (set) Token: 0x06001297 RID: 4759 RVA: 0x0007689A File Offset: 0x00074C9A
		public Sleek2FloatField field_x { get; protected set; }

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06001298 RID: 4760 RVA: 0x000768A3 File Offset: 0x00074CA3
		// (set) Token: 0x06001299 RID: 4761 RVA: 0x000768AB File Offset: 0x00074CAB
		public Sleek2FloatField field_y { get; protected set; }

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x0600129A RID: 4762 RVA: 0x000768B4 File Offset: 0x00074CB4
		// (set) Token: 0x0600129B RID: 4763 RVA: 0x000768BC File Offset: 0x00074CBC
		public Sleek2FloatField field_z { get; protected set; }

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x0600129C RID: 4764 RVA: 0x000768C5 File Offset: 0x00074CC5
		// (set) Token: 0x0600129D RID: 4765 RVA: 0x000768CD File Offset: 0x00074CCD
		public Sleek2FloatField field_w { get; protected set; }

		// Token: 0x0600129E RID: 4766 RVA: 0x000768D8 File Offset: 0x00074CD8
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
			this.field_w.fieldComponent.interactable = base.inspectable.canWrite;
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x00076964 File Offset: 0x00074D64
		public override void refresh()
		{
			if (base.inspectable == null || !base.inspectable.canRead)
			{
				return;
			}
			Vector4 vector = (Vector4)base.inspectable.value;
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
			if (!this.field_w.fieldComponent.isFocused)
			{
				this.field_w.value = vector.w;
			}
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x00076A3C File Offset: 0x00074E3C
		protected void handleFieldXSubmitted(Sleek2FloatField field, float value)
		{
			Vector4 vector = (Vector4)base.inspectable.value;
			vector.x = value;
			base.inspectable.value = vector;
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x00076A74 File Offset: 0x00074E74
		protected void handleFieldYSubmitted(Sleek2FloatField field, float value)
		{
			Vector4 vector = (Vector4)base.inspectable.value;
			vector.y = value;
			base.inspectable.value = vector;
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x00076AAC File Offset: 0x00074EAC
		protected void handleFieldZSubmitted(Sleek2FloatField field, float value)
		{
			Vector4 vector = (Vector4)base.inspectable.value;
			vector.z = value;
			base.inspectable.value = vector;
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x00076AE4 File Offset: 0x00074EE4
		protected void handleFieldWSubmitted(Sleek2FloatField field, float value)
		{
			Vector4 vector = (Vector4)base.inspectable.value;
			vector.w = value;
			base.inspectable.value = vector;
		}
	}
}
