using System;
using SDG.Framework.Debug;
using SDG.Framework.IO.FormattedFiles;
using SDG.Framework.Translations;
using UnityEngine;

namespace SDG.Framework.Devkit.Visibility
{
	// Token: 0x0200018E RID: 398
	public class VolumeVisibilityGroup : VisibilityGroup
	{
		// Token: 0x06000BE5 RID: 3045 RVA: 0x0005B280 File Offset: 0x00059680
		public VolumeVisibilityGroup()
		{
			this.isWireframeVisible = true;
			this.wireframeColor = Color.black;
			this.wireframeDepth = EGLVisibilityDepthMode.CHECKER;
			this.isSurfaceVisible = false;
			this.surfaceColor = new Color(0f, 0f, 0f, 0.25f);
			this.surfaceDepth = EGLVisibilityDepthMode.CUTOFF;
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x0005B2DC File Offset: 0x000596DC
		public VolumeVisibilityGroup(string newInternalName, TranslationReference newDisplayName, bool newIsVisible) : base(newInternalName, newDisplayName, newIsVisible)
		{
			this.isWireframeVisible = true;
			this.wireframeColor = Color.black;
			this.wireframeDepth = EGLVisibilityDepthMode.CHECKER;
			this.isSurfaceVisible = false;
			this.surfaceColor = new Color(0f, 0f, 0f, 0.25f);
			this.surfaceDepth = EGLVisibilityDepthMode.CUTOFF;
		}

		// Token: 0x17000165 RID: 357
		// (set) Token: 0x06000BE7 RID: 3047 RVA: 0x0005B338 File Offset: 0x00059738
		public Color color
		{
			set
			{
				this.wireframeColor = value;
				this.surfaceColor = value;
				this.surfaceColor.a = this.surfaceColor.a * 0.25f;
			}
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x0005B360 File Offset: 0x00059760
		protected override void readVisibilityGroup(IFormattedFileReader reader)
		{
			base.readVisibilityGroup(reader);
			this.isWireframeVisible = reader.readValue<bool>("Is_Wireframe_Visible");
			this.wireframeColor = reader.readValue<Color>("Wireframe_Color");
			this.wireframeDepth = reader.readValue<EGLVisibilityDepthMode>("Wireframe_Depth");
			this.isSurfaceVisible = reader.readValue<bool>("Is_Surface_Visible");
			this.surfaceColor = reader.readValue<Color>("Surface_Color");
			this.surfaceDepth = reader.readValue<EGLVisibilityDepthMode>("Surface_Depth");
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0005B3DC File Offset: 0x000597DC
		protected override void writeVisibilityGroup(IFormattedFileWriter writer)
		{
			base.writeVisibilityGroup(writer);
			writer.writeValue<bool>("Is_Wireframe_Visible", this.isWireframeVisible);
			writer.writeValue<Color>("Wireframe_Color", this.wireframeColor);
			writer.writeValue<EGLVisibilityDepthMode>("Wireframe_Depth", this.wireframeDepth);
			writer.writeValue<bool>("Is_Surface_Visible", this.isSurfaceVisible);
			writer.writeValue<Color>("Surface_Color", this.surfaceColor);
			writer.writeValue<EGLVisibilityDepthMode>("Surface_Depth", this.surfaceDepth);
		}

		// Token: 0x04000865 RID: 2149
		[Inspectable("#SDG::Devkit.Visibility.Group.Volume.Is_Wireframe_Visible", null)]
		public bool isWireframeVisible;

		// Token: 0x04000866 RID: 2150
		[Inspectable("#SDG::Devkit.Visibility.Group.Volume.Wireframe_Color", null)]
		public Color wireframeColor;

		// Token: 0x04000867 RID: 2151
		[Inspectable("#SDG::Devkit.Visibility.Group.Volume.Wireframe_Depth", null)]
		public EGLVisibilityDepthMode wireframeDepth;

		// Token: 0x04000868 RID: 2152
		[Inspectable("#SDG::Devkit.Visibility.Group.Volume.Is_Surface_Visible", null)]
		public bool isSurfaceVisible;

		// Token: 0x04000869 RID: 2153
		[Inspectable("#SDG::Devkit.Visibility.Group.Volume.Surface_Color", null)]
		public Color surfaceColor;

		// Token: 0x0400086A RID: 2154
		[Inspectable("#SDG::Devkit.Visibility.Group.Volume.Surface_Depth", null)]
		public EGLVisibilityDepthMode surfaceDepth;
	}
}
