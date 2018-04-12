using System;
using UnityEngine;

// Token: 0x020007DA RID: 2010
[ExecuteInEditMode]
public class WaterBase : MonoBehaviour
{
	// Token: 0x06003AAC RID: 15020 RVA: 0x001C6000 File Offset: 0x001C4400
	public void UpdateShader()
	{
		if (this.waterQuality > WaterQuality.Medium)
		{
			this.sharedMaterial.shader.maximumLOD = 501;
		}
		else if (this.waterQuality > WaterQuality.Low)
		{
			this.sharedMaterial.shader.maximumLOD = 301;
		}
		else
		{
			this.sharedMaterial.shader.maximumLOD = 201;
		}
		if (!SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth))
		{
			this.edgeBlend = false;
		}
		if (this.edgeBlend)
		{
			Shader.EnableKeyword("WATER_EDGEBLEND_ON");
			Shader.DisableKeyword("WATER_EDGEBLEND_OFF");
			if (Camera.main)
			{
				Camera.main.depthTextureMode |= DepthTextureMode.Depth;
			}
		}
		else
		{
			Shader.EnableKeyword("WATER_EDGEBLEND_OFF");
			Shader.DisableKeyword("WATER_EDGEBLEND_ON");
		}
	}

	// Token: 0x06003AAD RID: 15021 RVA: 0x001C60D9 File Offset: 0x001C44D9
	public void WaterTileBeingRendered(Transform tr, Camera currentCam)
	{
		if (currentCam && this.edgeBlend)
		{
			currentCam.depthTextureMode |= DepthTextureMode.Depth;
		}
	}

	// Token: 0x06003AAE RID: 15022 RVA: 0x001C60FF File Offset: 0x001C44FF
	public void Update()
	{
		if (this.sharedMaterial)
		{
			this.UpdateShader();
		}
	}

	// Token: 0x04002E6F RID: 11887
	public Material sharedMaterial;

	// Token: 0x04002E70 RID: 11888
	public WaterQuality waterQuality = WaterQuality.High;

	// Token: 0x04002E71 RID: 11889
	public bool edgeBlend = true;
}
