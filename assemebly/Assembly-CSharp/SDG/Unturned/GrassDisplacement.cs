using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000533 RID: 1331
	public class GrassDisplacement : MonoBehaviour
	{
		// Token: 0x060023E3 RID: 9187 RVA: 0x000C7420 File Offset: 0x000C5820
		private void Update()
		{
			Shader.SetGlobalVector(this._Grass_Displacement_Point, new Vector4(base.transform.position.x, base.transform.position.y + 0.5f, base.transform.position.z, 0f));
		}

		// Token: 0x060023E4 RID: 9188 RVA: 0x000C7481 File Offset: 0x000C5881
		private void OnEnable()
		{
			if (this._Grass_Displacement_Point == -1)
			{
				this._Grass_Displacement_Point = Shader.PropertyToID("_Grass_Displacement_Point");
			}
		}

		// Token: 0x040015F1 RID: 5617
		private int _Grass_Displacement_Point = -1;
	}
}
