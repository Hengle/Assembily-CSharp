using System;
using SDG.Framework.UI.Components;
using SDG.Framework.UI.Sleek2;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.AssetBrowserUI
{
	// Token: 0x02000299 RID: 665
	public class TypeBrowserTypeButton : Sleek2ImageButton
	{
		// Token: 0x06001390 RID: 5008 RVA: 0x0007D0FC File Offset: 0x0007B4FC
		public TypeBrowserTypeButton(Type newType)
		{
			this.type = newType;
			Sleek2Label sleek2Label = new Sleek2Label();
			sleek2Label.transform.reset();
			sleek2Label.textComponent.text = this.type.Name;
			sleek2Label.textComponent.color = Sleek2Config.darkTextColor;
			this.addElement(sleek2Label);
			Type type = typeof(TypeReference<>).MakeGenericType(new Type[]
			{
				this.type
			});
			this.dragable = base.gameObject.AddComponent<DragableSystemObject>();
			this.dragable.target = base.transform;
			this.dragable.source = Activator.CreateInstance(type, new object[]
			{
				this.type.AssemblyQualifiedName
			});
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06001391 RID: 5009 RVA: 0x0007D1BA File Offset: 0x0007B5BA
		// (set) Token: 0x06001392 RID: 5010 RVA: 0x0007D1C2 File Offset: 0x0007B5C2
		public Type type { get; protected set; }

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06001393 RID: 5011 RVA: 0x0007D1CB File Offset: 0x0007B5CB
		// (set) Token: 0x06001394 RID: 5012 RVA: 0x0007D1D3 File Offset: 0x0007B5D3
		public DragableSystemObject dragable { get; protected set; }
	}
}
