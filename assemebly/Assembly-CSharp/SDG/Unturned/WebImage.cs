using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SDG.Unturned
{
	// Token: 0x02000717 RID: 1815
	public class WebImage : MonoBehaviour
	{
		// Token: 0x06003390 RID: 13200 RVA: 0x0014E8FC File Offset: 0x0014CCFC
		private IEnumerator Download()
		{
			WWW request = new WWW(this.url);
			yield return request;
			if (string.IsNullOrEmpty(request.error))
			{
				this.texture = request.texture;
				this.texture.name = "WebImage";
				this.texture.filterMode = FilterMode.Trilinear;
				if (this.texture != null)
				{
					this.sprite = Sprite.Create(this.texture, new Rect(0f, 0f, (float)this.texture.width, (float)this.texture.height), new Vector2(0.5f, 0.5f), 100f);
					this.sprite.name = "WebSprite";
					this.targetImage.sprite = this.sprite;
					this.isExpanded = (this.texture.width <= 360 && this.texture.height <= 360);
					this.updateLayout();
				}
			}
			yield break;
		}

		// Token: 0x06003391 RID: 13201 RVA: 0x0014E917 File Offset: 0x0014CD17
		public void Refresh()
		{
			if (this.targetImage == null || string.IsNullOrEmpty(this.url))
			{
				return;
			}
			base.StartCoroutine("Download");
		}

		// Token: 0x06003392 RID: 13202 RVA: 0x0014E948 File Offset: 0x0014CD48
		protected void updateLayout()
		{
			if (this.texture == null)
			{
				return;
			}
			this.targetLayout.preferredHeight = (float)((!this.isExpanded) ? Mathf.Min(this.texture.height / 2, 360) : -1);
		}

		// Token: 0x06003393 RID: 13203 RVA: 0x0014E99B File Offset: 0x0014CD9B
		protected void onClick()
		{
			this.isExpanded = !this.isExpanded;
			this.updateLayout();
		}

		// Token: 0x06003394 RID: 13204 RVA: 0x0014E9B2 File Offset: 0x0014CDB2
		protected virtual void Start()
		{
			this.Refresh();
			this.targetButton.onClick.AddListener(new UnityAction(this.onClick));
		}

		// Token: 0x040022F7 RID: 8951
		public Image targetImage;

		// Token: 0x040022F8 RID: 8952
		public Button targetButton;

		// Token: 0x040022F9 RID: 8953
		public LayoutElement targetLayout;

		// Token: 0x040022FA RID: 8954
		public string url;

		// Token: 0x040022FB RID: 8955
		public bool isExpanded;

		// Token: 0x040022FC RID: 8956
		protected Texture2D texture;

		// Token: 0x040022FD RID: 8957
		protected Sprite sprite;
	}
}
