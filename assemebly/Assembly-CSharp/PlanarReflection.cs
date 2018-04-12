using System;
using SDG.Unturned;
using UnityEngine;

// Token: 0x020007D7 RID: 2007
public class PlanarReflection : MonoBehaviour
{
	// Token: 0x1400008A RID: 138
	// (add) Token: 0x06003A96 RID: 14998 RVA: 0x001C5684 File Offset: 0x001C3A84
	// (remove) Token: 0x06003A97 RID: 14999 RVA: 0x001C56B8 File Offset: 0x001C3AB8
	public static event PlanarReflectionPreRenderHandler preRender;

	// Token: 0x1400008B RID: 139
	// (add) Token: 0x06003A98 RID: 15000 RVA: 0x001C56EC File Offset: 0x001C3AEC
	// (remove) Token: 0x06003A99 RID: 15001 RVA: 0x001C5720 File Offset: 0x001C3B20
	public static event PlanarReflectionPostRenderHandler postRender;

	// Token: 0x06003A9A RID: 15002 RVA: 0x001C5754 File Offset: 0x001C3B54
	public void Start()
	{
		if (this.sharedMaterial == null)
		{
			this.sharedMaterial = base.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material;
		}
	}

	// Token: 0x06003A9B RID: 15003 RVA: 0x001C578C File Offset: 0x001C3B8C
	private Camera CreateReflectionCameraFor(Camera cam)
	{
		string name = base.gameObject.name + "Reflection" + cam.name;
		GameObject gameObject = new GameObject(name);
		Camera camera = gameObject.AddComponent<Camera>();
		GraphicsSettings.planarReflectionNeedsUpdate = true;
		camera.nearClipPlane = cam.nearClipPlane;
		camera.farClipPlane = cam.farClipPlane;
		camera.backgroundColor = this.clearColor;
		camera.clearFlags = ((!this.reflectSkybox) ? CameraClearFlags.Color : CameraClearFlags.Skybox);
		camera.backgroundColor = Color.black;
		camera.enabled = false;
		if (!camera.targetTexture)
		{
			camera.targetTexture = this.CreateTextureFor(cam);
		}
		return camera;
	}

	// Token: 0x06003A9C RID: 15004 RVA: 0x001C5838 File Offset: 0x001C3C38
	private RenderTexture CreateTextureFor(Camera cam)
	{
		return new RenderTexture(Mathf.RoundToInt((float)cam.pixelWidth * 0.5f), Mathf.RoundToInt((float)cam.pixelHeight * 0.5f), 16)
		{
			name = "PlanarReflection_RT"
		};
	}

	// Token: 0x06003A9D RID: 15005 RVA: 0x001C587D File Offset: 0x001C3C7D
	public void RenderHelpCameras(Camera currentCam)
	{
		if (!this.reflectionCamera)
		{
			this.reflectionCamera = this.CreateReflectionCameraFor(currentCam);
		}
		this.RenderReflectionFor(currentCam, this.reflectionCamera);
	}

	// Token: 0x06003A9E RID: 15006 RVA: 0x001C58A9 File Offset: 0x001C3CA9
	public void LateUpdate()
	{
		this.helped = false;
	}

	// Token: 0x06003A9F RID: 15007 RVA: 0x001C58B4 File Offset: 0x001C3CB4
	public void WaterTileBeingRendered(Transform tr, Camera currentCam)
	{
		if (base.enabled && currentCam.CompareTag("MainCamera"))
		{
			if (this.helped)
			{
				return;
			}
			this.helped = true;
			this.RenderHelpCameras(currentCam);
			if (this.reflectionCamera != null && this.sharedMaterial != null)
			{
				this.sharedMaterial.EnableKeyword("WATER_REFLECTIVE");
				this.sharedMaterial.DisableKeyword("WATER_SIMPLE");
				this.sharedMaterial.SetTexture(this.reflectionSampler, this.reflectionCamera.targetTexture);
			}
		}
		else if (this.reflectionCamera != null && this.sharedMaterial != null)
		{
			this.sharedMaterial.DisableKeyword("WATER_REFLECTIVE");
			this.sharedMaterial.EnableKeyword("WATER_SIMPLE");
			this.sharedMaterial.SetTexture(this.reflectionSampler, null);
		}
	}

	// Token: 0x06003AA0 RID: 15008 RVA: 0x001C59AD File Offset: 0x001C3DAD
	public void OnEnable()
	{
		Shader.EnableKeyword("WATER_REFLECTIVE");
		Shader.DisableKeyword("WATER_SIMPLE");
	}

	// Token: 0x06003AA1 RID: 15009 RVA: 0x001C59C3 File Offset: 0x001C3DC3
	public void OnDisable()
	{
		Shader.EnableKeyword("WATER_SIMPLE");
		Shader.DisableKeyword("WATER_REFLECTIVE");
	}

	// Token: 0x06003AA2 RID: 15010 RVA: 0x001C59DC File Offset: 0x001C3DDC
	private void RenderReflectionFor(Camera cam, Camera reflectCamera)
	{
		if (!reflectCamera)
		{
			return;
		}
		if (this.sharedMaterial && !this.sharedMaterial.HasProperty(this.reflectionSampler))
		{
			return;
		}
		if (GraphicsSettings.planarReflectionNeedsUpdate)
		{
			GraphicsSettings.planarReflectionNeedsUpdate = false;
			switch (GraphicsSettings.planarReflectionQuality)
			{
			case EGraphicQuality.LOW:
				reflectCamera.cullingMask = PlanarReflection.CULLING_MASK_LOW;
				break;
			case EGraphicQuality.MEDIUM:
				reflectCamera.cullingMask = PlanarReflection.CULLING_MASK_MEDIUM;
				break;
			case EGraphicQuality.HIGH:
				reflectCamera.cullingMask = PlanarReflection.CULLING_MASK_HIGH;
				break;
			case EGraphicQuality.ULTRA:
				reflectCamera.cullingMask = PlanarReflection.CULLING_MASK_ULTRA;
				break;
			}
			reflectCamera.layerCullDistances = cam.layerCullDistances;
			reflectCamera.layerCullSpherical = cam.layerCullSpherical;
		}
		this.SaneCameraSettings(reflectCamera);
		GL.invertCulling = true;
		Transform transform = base.transform;
		Vector3 eulerAngles = cam.transform.eulerAngles;
		reflectCamera.transform.eulerAngles = new Vector3(-eulerAngles.x, eulerAngles.y, eulerAngles.z);
		reflectCamera.transform.position = cam.transform.position;
		Vector3 position = transform.transform.position;
		position.y = transform.position.y;
		Vector3 up = transform.transform.up;
		float w = -Vector3.Dot(up, position) - this.clipPlaneOffset;
		Vector4 plane = new Vector4(up.x, up.y, up.z, w);
		Matrix4x4 matrix4x = Matrix4x4.zero;
		matrix4x = PlanarReflection.CalculateReflectionMatrix(matrix4x, plane);
		this.oldpos = cam.transform.position;
		Vector3 position2 = matrix4x.MultiplyPoint(this.oldpos);
		reflectCamera.worldToCameraMatrix = cam.worldToCameraMatrix * matrix4x;
		Vector4 clipPlane = this.CameraSpacePlane(reflectCamera, position, up, 1f);
		reflectCamera.projectionMatrix = cam.CalculateObliqueMatrix(clipPlane);
		reflectCamera.transform.position = position2;
		Vector3 eulerAngles2 = cam.transform.eulerAngles;
		reflectCamera.transform.eulerAngles = new Vector3(-eulerAngles2.x, eulerAngles2.y, eulerAngles2.z);
		float lodBias = QualitySettings.lodBias;
		QualitySettings.lodBias = 1f;
		if (PlanarReflection.preRender != null)
		{
			PlanarReflection.preRender();
		}
		reflectCamera.Render();
		if (PlanarReflection.postRender != null)
		{
			PlanarReflection.postRender();
		}
		QualitySettings.lodBias = lodBias;
		GL.invertCulling = false;
	}

	// Token: 0x06003AA3 RID: 15011 RVA: 0x001C5C52 File Offset: 0x001C4052
	private void SaneCameraSettings(Camera helperCam)
	{
		helperCam.renderingPath = RenderingPath.Forward;
		helperCam.hdr = false;
	}

	// Token: 0x06003AA4 RID: 15012 RVA: 0x001C5C64 File Offset: 0x001C4064
	private static Matrix4x4 CalculateReflectionMatrix(Matrix4x4 reflectionMat, Vector4 plane)
	{
		reflectionMat.m00 = 1f - 2f * plane[0] * plane[0];
		reflectionMat.m01 = -2f * plane[0] * plane[1];
		reflectionMat.m02 = -2f * plane[0] * plane[2];
		reflectionMat.m03 = -2f * plane[3] * plane[0];
		reflectionMat.m10 = -2f * plane[1] * plane[0];
		reflectionMat.m11 = 1f - 2f * plane[1] * plane[1];
		reflectionMat.m12 = -2f * plane[1] * plane[2];
		reflectionMat.m13 = -2f * plane[3] * plane[1];
		reflectionMat.m20 = -2f * plane[2] * plane[0];
		reflectionMat.m21 = -2f * plane[2] * plane[1];
		reflectionMat.m22 = 1f - 2f * plane[2] * plane[2];
		reflectionMat.m23 = -2f * plane[3] * plane[2];
		reflectionMat.m30 = 0f;
		reflectionMat.m31 = 0f;
		reflectionMat.m32 = 0f;
		reflectionMat.m33 = 1f;
		return reflectionMat;
	}

	// Token: 0x06003AA5 RID: 15013 RVA: 0x001C5E1C File Offset: 0x001C421C
	private static float sgn(float a)
	{
		if (a > 0f)
		{
			return 1f;
		}
		if (a < 0f)
		{
			return -1f;
		}
		return 0f;
	}

	// Token: 0x06003AA6 RID: 15014 RVA: 0x001C5E48 File Offset: 0x001C4248
	private Vector4 CameraSpacePlane(Camera cam, Vector3 pos, Vector3 normal, float sideSign)
	{
		Vector3 v = pos + normal * this.clipPlaneOffset;
		Matrix4x4 worldToCameraMatrix = cam.worldToCameraMatrix;
		Vector3 lhs = worldToCameraMatrix.MultiplyPoint(v);
		Vector3 rhs = worldToCameraMatrix.MultiplyVector(normal).normalized * sideSign;
		return new Vector4(rhs.x, rhs.y, rhs.z, -Vector3.Dot(lhs, rhs));
	}

	// Token: 0x04002E5A RID: 11866
	private static readonly int CULLING_MASK_LOW = RayMasks.GROUND | RayMasks.GROUND2;

	// Token: 0x04002E5B RID: 11867
	private static readonly int CULLING_MASK_MEDIUM = PlanarReflection.CULLING_MASK_LOW | RayMasks.SKY | RayMasks.LARGE | RayMasks.RESOURCE | RayMasks.STRUCTURE;

	// Token: 0x04002E5C RID: 11868
	private static readonly int CULLING_MASK_HIGH = PlanarReflection.CULLING_MASK_MEDIUM | RayMasks.MEDIUM | RayMasks.BARRICADE | RayMasks.VEHICLE;

	// Token: 0x04002E5D RID: 11869
	private static readonly int CULLING_MASK_ULTRA = PlanarReflection.CULLING_MASK_HIGH | RayMasks.ENEMY | RayMasks.DEBRIS | RayMasks.ENTITY | RayMasks.AGENT;

	// Token: 0x04002E60 RID: 11872
	public LayerMask reflectionMask;

	// Token: 0x04002E61 RID: 11873
	public bool reflectSkybox;

	// Token: 0x04002E62 RID: 11874
	public Color clearColor = Color.grey;

	// Token: 0x04002E63 RID: 11875
	public string reflectionSampler = "_ReflectionTex";

	// Token: 0x04002E64 RID: 11876
	public float clipPlaneOffset = 0.07f;

	// Token: 0x04002E65 RID: 11877
	private Vector3 oldpos = Vector3.zero;

	// Token: 0x04002E66 RID: 11878
	private Camera reflectionCamera;

	// Token: 0x04002E67 RID: 11879
	private bool helped;

	// Token: 0x04002E68 RID: 11880
	public Material sharedMaterial;
}
