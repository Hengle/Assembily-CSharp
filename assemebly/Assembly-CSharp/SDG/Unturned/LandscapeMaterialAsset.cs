using System;
using SDG.Framework.Debug;
using SDG.Framework.Devkit;
using SDG.Framework.Foliage;
using SDG.Framework.IO.FormattedFiles;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020003F5 RID: 1013
	public class LandscapeMaterialAsset : Asset, IDevkitAssetSpawnable
	{
		// Token: 0x06001B51 RID: 6993 RVA: 0x00097534 File Offset: 0x00095934
		public LandscapeMaterialAsset()
		{
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x0009753C File Offset: 0x0009593C
		public LandscapeMaterialAsset(Bundle bundle, Local localization, byte[] hash) : base(bundle, localization, hash)
		{
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06001B53 RID: 6995 RVA: 0x00097547 File Offset: 0x00095947
		public override EAssetType assetCategory
		{
			get
			{
				return EAssetType.NONE;
			}
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x0009754A File Offset: 0x0009594A
		public void devkitAssetSpawn()
		{
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x0009754C File Offset: 0x0009594C
		protected override void readAsset(IFormattedFileReader reader)
		{
			base.readAsset(reader);
			this.texture = reader.readValue<ContentReference<Texture2D>>("Texture");
			this.mask = reader.readValue<ContentReference<Texture2D>>("Mask");
			this.physicsMaterial = reader.readValue<EPhysicsMaterial>("Physics_Material");
			this.foliage = reader.readValue<AssetReference<FoliageInfoCollectionAsset>>("Foliage");
			this.useAutoSlope = reader.readValue<bool>("Use_Auto_Slope");
			this.autoMinAngleBegin = reader.readValue<float>("Auto_Min_Angle_Begin");
			this.autoMinAngleEnd = reader.readValue<float>("Auto_Min_Angle_End");
			this.autoMaxAngleBegin = reader.readValue<float>("Auto_Max_Angle_Begin");
			this.autoMaxAngleEnd = reader.readValue<float>("Auto_Max_Angle_End");
			this.useAutoFoundation = reader.readValue<bool>("Use_Auto_Foundation");
			this.autoRayRadius = reader.readValue<float>("Auto_Ray_Radius");
			this.autoRayLength = reader.readValue<float>("Auto_Ray_Length");
			this.autoRayMask = reader.readValue<ERayMask>("Auto_Ray_Mask");
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x00097640 File Offset: 0x00095A40
		protected override void writeAsset(IFormattedFileWriter writer)
		{
			base.writeAsset(writer);
			writer.writeValue<ContentReference<Texture2D>>("Texture", this.texture);
			writer.writeValue<ContentReference<Texture2D>>("Mask", this.mask);
			writer.writeValue<EPhysicsMaterial>("Physics_Material", this.physicsMaterial);
			writer.writeValue<AssetReference<FoliageInfoCollectionAsset>>("Foliage", this.foliage);
			writer.writeValue<bool>("Use_Auto_Slope", this.useAutoSlope);
			writer.writeValue<float>("Auto_Min_Angle_Begin", this.autoMinAngleBegin);
			writer.writeValue<float>("Auto_Min_Angle_End", this.autoMinAngleEnd);
			writer.writeValue<float>("Auto_Max_Angle_Begin", this.autoMaxAngleBegin);
			writer.writeValue<float>("Auto_Max_Angle_End", this.autoMaxAngleEnd);
			writer.writeValue<bool>("Use_Auto_Foundation", this.useAutoFoundation);
			writer.writeValue<float>("Auto_Ray_Radius", this.autoRayRadius);
			writer.writeValue<float>("Auto_Ray_Length", this.autoRayLength);
			writer.writeValue<ERayMask>("Auto_Ray_Mask", this.autoRayMask);
		}

		// Token: 0x0400102E RID: 4142
		[Inspectable("#SDG::Asset.Landscape.Material.Texture.Name", null)]
		public ContentReference<Texture2D> texture;

		// Token: 0x0400102F RID: 4143
		[Inspectable("#SDG::Asset.Landscape.Material.Mask.Name", null)]
		public ContentReference<Texture2D> mask;

		// Token: 0x04001030 RID: 4144
		[Inspectable("#SDG::Asset.Landscape.Material.Physics_Material.Name", null)]
		public EPhysicsMaterial physicsMaterial;

		// Token: 0x04001031 RID: 4145
		[Inspectable("#SDG::Asset.Landscape.Foliage.Name", null)]
		public AssetReference<FoliageInfoCollectionAsset> foliage;

		// Token: 0x04001032 RID: 4146
		[Inspectable("#SDG::Asset.Landscape.Material.Use_Auto_Slope", null)]
		public bool useAutoSlope;

		// Token: 0x04001033 RID: 4147
		[Inspectable("#SDG::Asset.Landscape.Material.Auto_Min_Angle_Begin", null)]
		public float autoMinAngleBegin;

		// Token: 0x04001034 RID: 4148
		[Inspectable("#SDG::Asset.Landscape.Material.Auto_Min_Angle_End", null)]
		public float autoMinAngleEnd;

		// Token: 0x04001035 RID: 4149
		[Inspectable("#SDG::Asset.Landscape.Material.Auto_Max_Angle_Begin", null)]
		public float autoMaxAngleBegin;

		// Token: 0x04001036 RID: 4150
		[Inspectable("#SDG::Asset.Landscape.Material.Auto_Max_Angle_End", null)]
		public float autoMaxAngleEnd;

		// Token: 0x04001037 RID: 4151
		[Inspectable("#SDG::Asset.Landscape.Material.Use_Auto_Foundation", null)]
		public bool useAutoFoundation;

		// Token: 0x04001038 RID: 4152
		[Inspectable("#SDG::Asset.Landscape.Material.Auto_Ray_Radius", null)]
		public float autoRayRadius;

		// Token: 0x04001039 RID: 4153
		[Inspectable("#SDG::Asset.Landscape.Material.Auto_Ray_Length", null)]
		public float autoRayLength;

		// Token: 0x0400103A RID: 4154
		[Inspectable("#SDG::Asset.Landscape.Material.Auto_Ray_Mask", null)]
		public ERayMask autoRayMask;
	}
}
