using System;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.InspectorUI.TypeInspectors
{
	// Token: 0x02000277 RID: 631
	public class Sleek2QuaternionInspector : Sleek2KeyValueInspector
	{
		// Token: 0x0600127D RID: 4733 RVA: 0x00075F04 File Offset: 0x00074304
		public Sleek2QuaternionInspector()
		{
			base.name = "Quaternion_Inspector";
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

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x0600127E RID: 4734 RVA: 0x000760A5 File Offset: 0x000744A5
		// (set) Token: 0x0600127F RID: 4735 RVA: 0x000760AD File Offset: 0x000744AD
		public Sleek2FloatField field_x { get; protected set; }

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06001280 RID: 4736 RVA: 0x000760B6 File Offset: 0x000744B6
		// (set) Token: 0x06001281 RID: 4737 RVA: 0x000760BE File Offset: 0x000744BE
		public Sleek2FloatField field_y { get; protected set; }

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06001282 RID: 4738 RVA: 0x000760C7 File Offset: 0x000744C7
		// (set) Token: 0x06001283 RID: 4739 RVA: 0x000760CF File Offset: 0x000744CF
		public Sleek2FloatField field_z { get; protected set; }

		// Token: 0x06001284 RID: 4740 RVA: 0x000760D8 File Offset: 0x000744D8
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

		// Token: 0x06001285 RID: 4741 RVA: 0x0007614C File Offset: 0x0007454C
		public override void refresh()
		{
			if (base.inspectable == null || !base.inspectable.canRead)
			{
				return;
			}
			Vector3 eulerAngles = ((Quaternion)base.inspectable.value).eulerAngles;
			if (!this.field_x.fieldComponent.isFocused)
			{
				this.field_x.value = eulerAngles.x;
			}
			if (!this.field_y.fieldComponent.isFocused)
			{
				this.field_y.value = eulerAngles.y;
			}
			if (!this.field_z.fieldComponent.isFocused)
			{
				this.field_z.value = eulerAngles.z;
			}
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x00076204 File Offset: 0x00074604
		protected void handleFieldXSubmitted(Sleek2FloatField field, float value)
		{
			Vector3 eulerAngles = ((Quaternion)base.inspectable.value).eulerAngles;
			eulerAngles.x = value;
			base.inspectable.value = Quaternion.Euler(eulerAngles);
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x00076248 File Offset: 0x00074648
		protected void handleFieldYSubmitted(Sleek2FloatField field, float value)
		{
			Vector3 eulerAngles = ((Quaternion)base.inspectable.value).eulerAngles;
			eulerAngles.y = value;
			base.inspectable.value = Quaternion.Euler(eulerAngles);
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x0007628C File Offset: 0x0007468C
		protected void handleFieldZSubmitted(Sleek2FloatField field, float value)
		{
			Vector3 eulerAngles = ((Quaternion)base.inspectable.value).eulerAngles;
			eulerAngles.z = value;
			base.inspectable.value = Quaternion.Euler(eulerAngles);
		}
	}
}
