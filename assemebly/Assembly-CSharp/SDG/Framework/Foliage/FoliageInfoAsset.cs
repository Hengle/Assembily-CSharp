using System;
using SDG.Framework.Debug;
using SDG.Framework.IO.FormattedFiles;
using SDG.Framework.Utilities;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.Foliage
{
	// Token: 0x0200019B RID: 411
	public abstract class FoliageInfoAsset : Asset
	{
		// Token: 0x06000C0C RID: 3084 RVA: 0x0005BE26 File Offset: 0x0005A226
		public FoliageInfoAsset()
		{
			this.resetFoliageInfo();
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x0005BE34 File Offset: 0x0005A234
		public FoliageInfoAsset(Bundle bundle, Local localization, byte[] hash) : base(bundle, localization, hash)
		{
			this.resetFoliageInfo();
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x0005BE45 File Offset: 0x0005A245
		public virtual float randomNormalPositionOffset
		{
			get
			{
				return UnityEngine.Random.Range(this.minNormalPositionOffset, this.maxNormalPositionOffset);
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000C0F RID: 3087 RVA: 0x0005BE58 File Offset: 0x0005A258
		public virtual Quaternion randomRotation
		{
			get
			{
				return Quaternion.Euler(new Vector3(UnityEngine.Random.Range(this.minRotation.x, this.maxRotation.x), UnityEngine.Random.Range(this.minRotation.y, this.maxRotation.y), UnityEngine.Random.Range(this.minRotation.z, this.maxRotation.z)));
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x0005BEC0 File Offset: 0x0005A2C0
		public virtual Vector3 randomScale
		{
			get
			{
				return new Vector3(UnityEngine.Random.Range(this.minScale.x, this.maxScale.x), UnityEngine.Random.Range(this.minScale.y, this.maxScale.y), UnityEngine.Random.Range(this.minScale.z, this.maxScale.z));
			}
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x0005BF23 File Offset: 0x0005A323
		public virtual void bakeFoliage(FoliageBakeSettings bakeSettings, IFoliageSurface surface, Bounds bounds, float surfaceWeight, float collectionWeight)
		{
			if (!this.isSurfaceWeightValid(surfaceWeight))
			{
				return;
			}
			this.bakeFoliageSteps(surface, bounds, surfaceWeight, collectionWeight, new FoliageInfoAsset.BakeFoliageStepHandler(this.handleBakeFoliageStep));
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0005BF4C File Offset: 0x0005A34C
		public virtual void addFoliageToSurface(Vector3 surfacePosition, Vector3 surfaceNormal, bool clearWhenBaked, bool followRules = false)
		{
			if (followRules && !this.isAngleValid(surfaceNormal))
			{
				return;
			}
			Vector3 position = surfacePosition + surfaceNormal * this.randomNormalPositionOffset;
			if (followRules && !this.isPositionValid(position))
			{
				return;
			}
			Quaternion quaternion = Quaternion.Lerp(MathUtility.IDENTITY_QUATERNION, Quaternion.FromToRotation(Vector3.up, surfaceNormal), this.normalRotationAlignment);
			quaternion *= Quaternion.Euler(this.normalRotationOffset);
			quaternion *= this.randomRotation;
			Vector3 randomScale = this.randomScale;
			this.addFoliage(position, quaternion, randomScale, clearWhenBaked);
		}

		// Token: 0x06000C13 RID: 3091
		public abstract int getInstanceCountInVolume(IShapeVolume volume);

		// Token: 0x06000C14 RID: 3092
		protected abstract void addFoliage(Vector3 position, Quaternion rotation, Vector3 scale, bool clearWhenBaked);

		// Token: 0x06000C15 RID: 3093 RVA: 0x0005BFE0 File Offset: 0x0005A3E0
		protected virtual void bakeFoliageSteps(IFoliageSurface surface, Bounds bounds, float surfaceWeight, float collectionWeight, FoliageInfoAsset.BakeFoliageStepHandler callback)
		{
			float num = surfaceWeight * collectionWeight;
			float num2 = bounds.size.x * bounds.size.z;
			float num3 = num2 / this.density * num;
			int num4 = Mathf.FloorToInt(num3);
			if (UnityEngine.Random.value < num3 - (float)num4)
			{
				num4++;
			}
			for (int i = 0; i < num4; i++)
			{
				callback(surface, bounds, surfaceWeight, collectionWeight);
			}
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x0005C060 File Offset: 0x0005A460
		protected virtual Vector3 getTestPosition(Bounds bounds)
		{
			float x = UnityEngine.Random.Range(-1f, 1f) * bounds.extents.x;
			float z = UnityEngine.Random.Range(-1f, 1f) * bounds.extents.z;
			return bounds.center + new Vector3(x, bounds.extents.y, z);
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x0005C0D0 File Offset: 0x0005A4D0
		protected virtual void handleBakeFoliageStep(IFoliageSurface surface, Bounds bounds, float surfaceWeight, float collectionWeight)
		{
			Vector3 testPosition = this.getTestPosition(bounds);
			Vector3 surfacePosition;
			Vector3 surfaceNormal;
			if (!surface.getFoliageSurfaceInfo(testPosition, out surfacePosition, out surfaceNormal))
			{
				return;
			}
			this.addFoliageToSurface(surfacePosition, surfaceNormal, true, false);
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x0005C100 File Offset: 0x0005A500
		protected virtual bool isAngleValid(Vector3 surfaceNormal)
		{
			float num = Vector3.Angle(Vector3.up, surfaceNormal);
			return num >= this.minSurfaceAngle && num <= this.maxSurfaceAngle;
		}

		// Token: 0x06000C19 RID: 3097
		protected abstract bool isPositionValid(Vector3 position);

		// Token: 0x06000C1A RID: 3098 RVA: 0x0005C134 File Offset: 0x0005A534
		protected virtual bool isSurfaceWeightValid(float surfaceWeight)
		{
			return surfaceWeight >= this.minSurfaceWeight && surfaceWeight <= this.maxSurfaceWeight;
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x0005C154 File Offset: 0x0005A554
		protected override void readAsset(IFormattedFileReader reader)
		{
			base.readAsset(reader);
			this.density = reader.readValue<float>("Density");
			this.minNormalPositionOffset = reader.readValue<float>("Min_Normal_Position_Offset");
			this.maxNormalPositionOffset = reader.readValue<float>("Max_Normal_Position_Offset");
			this.normalRotationOffset = reader.readValue<Vector3>("Normal_Rotation_Offset");
			if (reader.containsKey("Normal_Rotation_Alignment"))
			{
				this.normalRotationAlignment = reader.readValue<float>("Normal_Rotation_Alignment");
			}
			else
			{
				this.normalRotationAlignment = 1f;
			}
			this.minSurfaceWeight = reader.readValue<float>("Min_Weight");
			this.maxSurfaceWeight = reader.readValue<float>("Max_Weight");
			this.minSurfaceAngle = reader.readValue<float>("Min_Angle");
			this.maxSurfaceAngle = reader.readValue<float>("Max_Angle");
			this.minRotation = reader.readValue<Vector3>("Min_Rotation");
			this.maxRotation = reader.readValue<Vector3>("Max_Rotation");
			this.minScale = reader.readValue<Vector3>("Min_Scale");
			this.maxScale = reader.readValue<Vector3>("Max_Scale");
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x0005C268 File Offset: 0x0005A668
		protected override void writeAsset(IFormattedFileWriter writer)
		{
			base.writeAsset(writer);
			writer.writeValue<float>("Density", this.density);
			writer.writeValue<float>("Min_Normal_Position_Offset", this.minNormalPositionOffset);
			writer.writeValue<float>("Max_Normal_Position_Offset", this.maxNormalPositionOffset);
			writer.writeValue<Vector3>("Normal_Rotation_Offset", this.normalRotationOffset);
			writer.writeValue<float>("Normal_Rotation_Alignment", this.normalRotationAlignment);
			writer.writeValue<float>("Min_Weight", this.minSurfaceWeight);
			writer.writeValue<float>("Max_Weight", this.maxSurfaceWeight);
			writer.writeValue<float>("Min_Angle", this.minSurfaceAngle);
			writer.writeValue<float>("Max_Angle", this.maxSurfaceAngle);
			writer.writeValue<Vector3>("Min_Rotation", this.minRotation);
			writer.writeValue<Vector3>("Max_Rotation", this.maxRotation);
			writer.writeValue<Vector3>("Min_Scale", this.minScale);
			writer.writeValue<Vector3>("Max_Scale", this.maxScale);
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x0005C359 File Offset: 0x0005A759
		protected virtual void resetFoliageInfo()
		{
			this.normalRotationAlignment = 1f;
			this.maxSurfaceWeight = 1f;
			this.minScale = Vector3.one;
			this.maxScale = Vector3.one;
		}

		// Token: 0x04000879 RID: 2169
		[Inspectable("#SDG::Asset.Foliage.Info.Density.Name", null)]
		public float density;

		// Token: 0x0400087A RID: 2170
		[Inspectable("#SDG::Asset.Foliage.Info.Min_Normal_Position_Offset.Name", null)]
		public float minNormalPositionOffset;

		// Token: 0x0400087B RID: 2171
		[Inspectable("#SDG::Asset.Foliage.Info.Max_Normal_Position_Offset.Name", null)]
		public float maxNormalPositionOffset;

		// Token: 0x0400087C RID: 2172
		[Inspectable("#SDG::Asset.Foliage.Info.Normal_Rotation_Offset.Name", null)]
		public Vector3 normalRotationOffset;

		// Token: 0x0400087D RID: 2173
		[Inspectable("#SDG::Asset.Foliage.Info.Normal_Rotation_Alignment.Name", null)]
		public float normalRotationAlignment;

		// Token: 0x0400087E RID: 2174
		[Inspectable("#SDG::Asset.Foliage.Info.Min_Weight.Name", null)]
		public float minSurfaceWeight;

		// Token: 0x0400087F RID: 2175
		[Inspectable("#SDG::Asset.Foliage.Info.Max_Weight.Name", null)]
		public float maxSurfaceWeight;

		// Token: 0x04000880 RID: 2176
		[Inspectable("#SDG::Asset.Foliage.Info.Min_Angle.Name", null)]
		public float minSurfaceAngle;

		// Token: 0x04000881 RID: 2177
		[Inspectable("#SDG::Asset.Foliage.Info.Max_Angle.Name", null)]
		public float maxSurfaceAngle;

		// Token: 0x04000882 RID: 2178
		[Inspectable("#SDG::Asset.Foliage.Info.Min_Rotation.Name", null)]
		public Vector3 minRotation;

		// Token: 0x04000883 RID: 2179
		[Inspectable("#SDG::Asset.Foliage.Info.Max_Rotation.Name", null)]
		public Vector3 maxRotation;

		// Token: 0x04000884 RID: 2180
		[Inspectable("#SDG::Asset.Foliage.Info.Min_Scale.Name", null)]
		public Vector3 minScale;

		// Token: 0x04000885 RID: 2181
		[Inspectable("#SDG::Asset.Foliage.Info.Max_Scale.Name", null)]
		public Vector3 maxScale;

		// Token: 0x0200019C RID: 412
		// (Invoke) Token: 0x06000C1F RID: 3103
		protected delegate void BakeFoliageStepHandler(IFoliageSurface surface, Bounds bounds, float surfaceWeight, float collectionWeight);
	}
}
