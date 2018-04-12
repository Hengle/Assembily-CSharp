using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x02000842 RID: 2114
	public class PostProcessingProfile : ScriptableObject
	{
		// Token: 0x04003049 RID: 12361
		public BuiltinDebugViewsModel debugViews = new BuiltinDebugViewsModel();

		// Token: 0x0400304A RID: 12362
		public AntialiasingModel antialiasing = new AntialiasingModel();

		// Token: 0x0400304B RID: 12363
		public AmbientOcclusionModel ambientOcclusion = new AmbientOcclusionModel();

		// Token: 0x0400304C RID: 12364
		public ScreenSpaceReflectionModel screenSpaceReflection = new ScreenSpaceReflectionModel();

		// Token: 0x0400304D RID: 12365
		public DepthOfFieldModel depthOfField = new DepthOfFieldModel();

		// Token: 0x0400304E RID: 12366
		public MotionBlurModel motionBlur = new MotionBlurModel();

		// Token: 0x0400304F RID: 12367
		public EyeAdaptationModel eyeAdaptation = new EyeAdaptationModel();

		// Token: 0x04003050 RID: 12368
		public BloomModel bloom = new BloomModel();

		// Token: 0x04003051 RID: 12369
		public ColorGradingModel colorGrading = new ColorGradingModel();

		// Token: 0x04003052 RID: 12370
		public UserLutModel userLut = new UserLutModel();

		// Token: 0x04003053 RID: 12371
		public ChromaticAberrationModel chromaticAberration = new ChromaticAberrationModel();

		// Token: 0x04003054 RID: 12372
		public GrainModel grain = new GrainModel();

		// Token: 0x04003055 RID: 12373
		public VignetteModel vignette = new VignetteModel();
	}
}
