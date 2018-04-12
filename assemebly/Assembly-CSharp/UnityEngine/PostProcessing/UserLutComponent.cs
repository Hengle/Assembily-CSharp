using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x02000800 RID: 2048
	public sealed class UserLutComponent : PostProcessingComponentRenderTexture<UserLutModel>
	{
		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x06003B3D RID: 15165 RVA: 0x001CB230 File Offset: 0x001C9630
		public override bool active
		{
			get
			{
				UserLutModel.Settings settings = base.model.settings;
				return base.model.enabled && settings.lut != null && settings.contribution > 0f && settings.lut.height == (int)Mathf.Sqrt((float)settings.lut.width) && !this.context.interrupted;
			}
		}

		// Token: 0x06003B3E RID: 15166 RVA: 0x001CB2B4 File Offset: 0x001C96B4
		public override void Prepare(Material uberMaterial)
		{
			UserLutModel.Settings settings = base.model.settings;
			uberMaterial.EnableKeyword("USER_LUT");
			uberMaterial.SetTexture(UserLutComponent.Uniforms._UserLut, settings.lut);
			uberMaterial.SetVector(UserLutComponent.Uniforms._UserLut_Params, new Vector4(1f / (float)settings.lut.width, 1f / (float)settings.lut.height, (float)settings.lut.height - 1f, settings.contribution));
		}

		// Token: 0x06003B3F RID: 15167 RVA: 0x001CB33C File Offset: 0x001C973C
		public void OnGUI()
		{
			UserLutModel.Settings settings = base.model.settings;
			Rect position = new Rect(this.context.viewport.x * (float)Screen.width + 8f, 8f, (float)settings.lut.width, (float)settings.lut.height);
			GUI.DrawTexture(position, settings.lut);
		}

		// Token: 0x02000801 RID: 2049
		private static class Uniforms
		{
			// Token: 0x04002F55 RID: 12117
			internal static readonly int _UserLut = Shader.PropertyToID("_UserLut");

			// Token: 0x04002F56 RID: 12118
			internal static readonly int _UserLut_Params = Shader.PropertyToID("_UserLut_Params");
		}
	}
}
