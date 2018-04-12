using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x02000836 RID: 2102
	[Serializable]
	public class UserLutModel : PostProcessingModel
	{
		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x06003B95 RID: 15253 RVA: 0x001CC4ED File Offset: 0x001CA8ED
		// (set) Token: 0x06003B96 RID: 15254 RVA: 0x001CC4F5 File Offset: 0x001CA8F5
		public UserLutModel.Settings settings
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

		// Token: 0x06003B97 RID: 15255 RVA: 0x001CC4FE File Offset: 0x001CA8FE
		public override void Reset()
		{
			this.m_Settings = UserLutModel.Settings.defaultSettings;
		}

		// Token: 0x04003016 RID: 12310
		[SerializeField]
		private UserLutModel.Settings m_Settings = UserLutModel.Settings.defaultSettings;

		// Token: 0x02000837 RID: 2103
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000AC6 RID: 2758
			// (get) Token: 0x06003B98 RID: 15256 RVA: 0x001CC50C File Offset: 0x001CA90C
			public static UserLutModel.Settings defaultSettings
			{
				get
				{
					return new UserLutModel.Settings
					{
						lut = null,
						contribution = 1f
					};
				}
			}

			// Token: 0x04003017 RID: 12311
			[Tooltip("Custom lookup texture (strip format, e.g. 256x16).")]
			public Texture2D lut;

			// Token: 0x04003018 RID: 12312
			[Range(0f, 1f)]
			[Tooltip("Blending factor.")]
			public float contribution;
		}
	}
}
