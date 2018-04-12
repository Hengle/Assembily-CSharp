using System;
using SDG.Framework.Translations;
using UnityEngine;
using UnityEngine.UI;

namespace SDG.Framework.UI.Components
{
	// Token: 0x02000225 RID: 549
	public class TranslatedLabel : MonoBehaviour
	{
		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06001041 RID: 4161 RVA: 0x0006B226 File Offset: 0x00069626
		// (set) Token: 0x06001042 RID: 4162 RVA: 0x0006B230 File Offset: 0x00069630
		public TranslatedText translation
		{
			get
			{
				return this._translation;
			}
			set
			{
				if (this.translation == value)
				{
					return;
				}
				if (this.translation != null)
				{
					this.translation.changed -= this.handleTranslationChanged;
					this.translation.endListening();
				}
				this._translation = value;
				if (this.translation != null)
				{
					this.translation.changed += this.handleTranslationChanged;
				}
			}
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x0006B2A2 File Offset: 0x000696A2
		protected virtual void handleTranslationChanged(TranslatedText translation, string text)
		{
			if (this.textComponent == null)
			{
				return;
			}
			this.textComponent.text = text;
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x0006B2C2 File Offset: 0x000696C2
		protected void OnDestroy()
		{
			this.translation = null;
		}

		// Token: 0x040009E1 RID: 2529
		public Text textComponent;

		// Token: 0x040009E2 RID: 2530
		protected TranslatedText _translation;
	}
}
