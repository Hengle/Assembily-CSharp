using System;
using SDG.Provider;
using UnityEngine;
using UnityEngine.UI;

namespace SDG.Unturned
{
	// Token: 0x02000383 RID: 899
	public class StatTracker : MonoBehaviour
	{
		// Token: 0x1700039E RID: 926
		// (get) Token: 0x060018F7 RID: 6391 RVA: 0x0008CC30 File Offset: 0x0008B030
		// (set) Token: 0x060018F8 RID: 6392 RVA: 0x0008CC38 File Offset: 0x0008B038
		public Text statTrackerText { get; protected set; }

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x060018F9 RID: 6393 RVA: 0x0008CC41 File Offset: 0x0008B041
		// (set) Token: 0x060018FA RID: 6394 RVA: 0x0008CC49 File Offset: 0x0008B049
		public Transform statTrackerHook { get; protected set; }

		// Token: 0x060018FB RID: 6395 RVA: 0x0008CC54 File Offset: 0x0008B054
		public void updateStatTracker(bool viewmodel)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>("Economy/Attachments/Stat_Tracker"));
			gameObject.transform.SetParent(this.statTrackerHook);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
			this.statTrackerText = gameObject.GetComponentInChildren<Text>();
			if (viewmodel)
			{
				Layerer.relayer(gameObject.transform, LayerMasks.VIEWMODEL);
			}
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x0008CCC4 File Offset: 0x0008B0C4
		protected void Update()
		{
			if (this.statTrackerCallback == null)
			{
				return;
			}
			EStatTrackerType type;
			int num;
			if (!this.statTrackerCallback(out type, out num))
			{
				return;
			}
			if (this.oldStatValue == num)
			{
				return;
			}
			this.oldStatValue = num;
			this.statTrackerText.color = Provider.provider.economyService.getStatTrackerColor(type);
			this.statTrackerText.text = num.ToString("D7");
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x0008CD38 File Offset: 0x0008B138
		protected void Awake()
		{
			this.statTrackerHook = base.transform.FindChild("Stat_Tracker");
		}

		// Token: 0x04000D69 RID: 3433
		public GetStatTrackerValueHandler statTrackerCallback;

		// Token: 0x04000D6A RID: 3434
		protected int oldStatValue = -1;
	}
}
