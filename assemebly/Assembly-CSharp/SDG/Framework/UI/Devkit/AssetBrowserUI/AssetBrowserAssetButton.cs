using System;
using SDG.Framework.UI.Components;
using SDG.Framework.UI.Sleek2;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.AssetBrowserUI
{
	// Token: 0x02000229 RID: 553
	public class AssetBrowserAssetButton : Sleek2ImageButton
	{
		// Token: 0x0600105E RID: 4190 RVA: 0x0006BC18 File Offset: 0x0006A018
		public AssetBrowserAssetButton(Asset newAsset)
		{
			this.asset = newAsset;
			this.field = new Sleek2Field();
			this.field.transform.anchorMin = new Vector2(0f, 0.5f);
			this.field.transform.anchorMax = new Vector2(1f, 1f);
			this.field.transform.offsetMin = new Vector2(5f, 0f);
			this.field.transform.offsetMax = new Vector2(-5f, -5f);
			this.field.fieldComponent.text = this.asset.name;
			this.field.fieldComponent.textComponent.text = this.field.fieldComponent.text;
			this.field.submitted += this.handleFieldSubmitted;
			this.addElement(this.field);
			this.label = new Sleek2Label();
			this.label.transform.anchorMin = new Vector2(0f, 0f);
			this.label.transform.anchorMax = new Vector2(1f, 0.5f);
			this.label.transform.offsetMin = new Vector2(5f, 5f);
			this.label.transform.offsetMax = new Vector2(-5f, 0f);
			this.label.textComponent.text = '[' + this.asset.GetType().Name + ']';
			this.label.textComponent.color = Sleek2Config.darkTextColor;
			this.addElement(this.label);
			Type type = this.asset.GetType();
			Type type2 = typeof(AssetReference<>).MakeGenericType(new Type[]
			{
				type
			});
			this.dragable = base.gameObject.AddComponent<DragableSystemObject>();
			this.dragable.target = base.transform;
			this.dragable.source = Activator.CreateInstance(type2, new object[]
			{
				this.asset.GUID
			});
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x0600105F RID: 4191 RVA: 0x0006BE6A File Offset: 0x0006A26A
		// (set) Token: 0x06001060 RID: 4192 RVA: 0x0006BE72 File Offset: 0x0006A272
		public Asset asset { get; protected set; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06001061 RID: 4193 RVA: 0x0006BE7B File Offset: 0x0006A27B
		// (set) Token: 0x06001062 RID: 4194 RVA: 0x0006BE83 File Offset: 0x0006A283
		public DragableSystemObject dragable { get; protected set; }

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06001063 RID: 4195 RVA: 0x0006BE8C File Offset: 0x0006A28C
		// (set) Token: 0x06001064 RID: 4196 RVA: 0x0006BE94 File Offset: 0x0006A294
		public Sleek2Field field { get; protected set; }

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06001065 RID: 4197 RVA: 0x0006BE9D File Offset: 0x0006A29D
		// (set) Token: 0x06001066 RID: 4198 RVA: 0x0006BEA5 File Offset: 0x0006A2A5
		public Sleek2Label label { get; protected set; }

		// Token: 0x06001067 RID: 4199 RVA: 0x0006BEAE File Offset: 0x0006A2AE
		protected virtual void handleFieldSubmitted(Sleek2Field field, string value)
		{
			Assets.rename(this.asset, value);
		}
	}
}
