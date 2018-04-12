using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SDG.Unturned
{
	// Token: 0x020007A5 RID: 1957
	public class LevelLoader : MonoBehaviour
	{
		// Token: 0x06003903 RID: 14595 RVA: 0x001A2F10 File Offset: 0x001A1310
		public void LoadLevel(string name)
		{
			SceneManager.LoadScene(name);
		}
	}
}
