using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x02000825 RID: 2085
	[Serializable]
	public class DepthOfFieldModel : PostProcessingModel
	{
		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x06003B7C RID: 15228 RVA: 0x001CC1DD File Offset: 0x001CA5DD
		// (set) Token: 0x06003B7D RID: 15229 RVA: 0x001CC1E5 File Offset: 0x001CA5E5
		public DepthOfFieldModel.Settings settings
		{
			get
			{
				return this.m_Settings;
			}
			set
			{
				this.m_Settings = value;
			}
		}

		// Token: 0x06003B7E RID: 15230 RVA: 0x001CC1EE File Offset: 0x001CA5EE
		public override void Reset()
		{
			this.m_Settings = DepthOfFieldModel.Settings.defaultSettings;
		}

		// Token: 0x04002FDD RID: 12253
		[SerializeField]
		private DepthOfFieldModel.Settings m_Settings = DepthOfFieldModel.Settings.defaultSettings;

		// Token: 0x02000826 RID: 2086
		public enum KernelSize
		{
			// Token: 0x04002FDF RID: 12255
			Small,
			// Token: 0x04002FE0 RID: 12256
			Medium,
			// Token: 0x04002FE1 RID: 12257
			Large,
			// Token: 0x04002FE2 RID: 12258
			VeryLarge
		}

		// Token: 0x02000827 RID: 2087
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000ABC RID: 2748
			// (get) Token: 0x06003B7F RID: 15231 RVA: 0x001CC1FC File Offset: 0x001CA5FC
			public static DepthOfFieldModel.Settings defaultSettings
			{
				get
				{
					return new DepthOfFieldModel.Settings
					{
						focusDistance = 10f,
						aperture = 5.6f,
						focalLength = 50f,
						useCameraFov = false,
						kernelSize = DepthOfFieldModel.KernelSize.Medium
					};
				}
			}

			// Token: 0x04002FE3 RID: 12259
			[Min(0.1f)]
			[Tooltip("Distance to the point of focus (only used when none is specified in focusTransform).")]
			public float focusDistance;

			// Token: 0x04002FE4 RID: 12260
			[Range(0.05f, 32f)]
			[Tooltip("Ratio of aperture (known as f-stop or f-number). The smaller the value is, the shallower the depth of field is.")]
			public float aperture;

			// Token: 0x04002FE5 RID: 12261
			[Range(1f, 300f)]
			[Tooltip("Distance between the lens and the film. The larger the value is, the shallower the depth of field is.")]
			public float focalLength;

			// Token: 0x04002FE6 RID: 12262
			[Tooltip("Calculate the focal length automatically from the field-of-view value set on the camera.")]
			public bool useCameraFov;

			// Token: 0x04002FE7 RID: 12263
			[Tooltip("Convolution kernel size of the bokeh filter, which determines the maximum radius of bokeh. It also affects the performance (the larger the kernel is, the longer the GPU time is required).")]
			public DepthOfFieldModel.KernelSize kernelSize;
		}
	}
}
