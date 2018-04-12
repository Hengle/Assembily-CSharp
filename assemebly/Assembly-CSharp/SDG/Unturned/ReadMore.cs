using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SDG.Unturned
{
	// Token: 0x020006BF RID: 1727
	public class ReadMore : MonoBehaviour
	{
		// Token: 0x060031E0 RID: 12768 RVA: 0x001442D6 File Offset: 0x001426D6
		public void Refresh()
		{
			base.GetComponent<Text>().text = ((!this.targetContent.activeSelf) ? this.onText : this.offText);
		}

		// Token: 0x060031E1 RID: 12769 RVA: 0x00144304 File Offset: 0x00142704
		private void onClick()
		{
			this.targetContent.SetActive(!this.targetContent.activeSelf);
			this.Refresh();
		}

		// Token: 0x060031E2 RID: 12770 RVA: 0x00144325 File Offset: 0x00142725
		private void Start()
		{
			this.targetButton.onClick.AddListener(new UnityAction(this.onClick));
		}

		// Token: 0x040021F1 RID: 8689
		public Button targetButton;

		// Token: 0x040021F2 RID: 8690
		public GameObject targetContent;

		// Token: 0x040021F3 RID: 8691
		public string onText;

		// Token: 0x040021F4 RID: 8692
		public string offText;
	}
}
