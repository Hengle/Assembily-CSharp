using System;
using SDG.Framework.Devkit.Visibility;
using UnityEngine;

namespace SDG.Framework.Rendering
{
	// Token: 0x020001F8 RID: 504
	public class GLUtility
	{
		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000F10 RID: 3856 RVA: 0x00066938 File Offset: 0x00064D38
		public static Material LINE_FLAT_COLOR
		{
			get
			{
				if (GLUtility._LINE_FLAT_COLOR == null)
				{
					GLUtility._LINE_FLAT_COLOR = new Material(Shader.Find("GL/LineFlatColor"));
				}
				return GLUtility._LINE_FLAT_COLOR;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000F11 RID: 3857 RVA: 0x00066963 File Offset: 0x00064D63
		public static Material LINE_CHECKERED_COLOR
		{
			get
			{
				if (GLUtility._LINE_CHECKERED_COLOR == null)
				{
					GLUtility._LINE_CHECKERED_COLOR = new Material(Shader.Find("GL/LineCheckeredColor"));
				}
				return GLUtility._LINE_CHECKERED_COLOR;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000F12 RID: 3858 RVA: 0x0006698E File Offset: 0x00064D8E
		public static Material LINE_DEPTH_CHECKERED_COLOR
		{
			get
			{
				if (GLUtility._LINE_DEPTH_CHECKERED_COLOR == null)
				{
					GLUtility._LINE_DEPTH_CHECKERED_COLOR = new Material(Shader.Find("GL/LineDepthCheckeredColor"));
				}
				return GLUtility._LINE_DEPTH_CHECKERED_COLOR;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000F13 RID: 3859 RVA: 0x000669B9 File Offset: 0x00064DB9
		public static Material LINE_CHECKERED_DEPTH_CUTOFF_COLOR
		{
			get
			{
				if (GLUtility._LINE_CHECKERED_DEPTH_CUTOFF_COLOR == null)
				{
					GLUtility._LINE_CHECKERED_DEPTH_CUTOFF_COLOR = new Material(Shader.Find("GL/LineCheckeredDepthCutoffColor"));
				}
				return GLUtility._LINE_CHECKERED_DEPTH_CUTOFF_COLOR;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000F14 RID: 3860 RVA: 0x000669E4 File Offset: 0x00064DE4
		public static Material LINE_DEPTH_CUTOFF_COLOR
		{
			get
			{
				if (GLUtility._LINE_DEPTH_CUTOFF_COLOR == null)
				{
					GLUtility._LINE_DEPTH_CUTOFF_COLOR = new Material(Shader.Find("GL/LineDepthCutoffColor"));
				}
				return GLUtility._LINE_DEPTH_CUTOFF_COLOR;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000F15 RID: 3861 RVA: 0x00066A0F File Offset: 0x00064E0F
		public static Material TRI_FLAT_COLOR
		{
			get
			{
				if (GLUtility._TRI_FLAT_COLOR == null)
				{
					GLUtility._TRI_FLAT_COLOR = new Material(Shader.Find("GL/TriFlatColor"));
				}
				return GLUtility._TRI_FLAT_COLOR;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000F16 RID: 3862 RVA: 0x00066A3A File Offset: 0x00064E3A
		public static Material TRI_CHECKERED_COLOR
		{
			get
			{
				if (GLUtility._TRI_CHECKERED_COLOR == null)
				{
					GLUtility._TRI_CHECKERED_COLOR = new Material(Shader.Find("GL/TriCheckeredColor"));
				}
				return GLUtility._TRI_CHECKERED_COLOR;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000F17 RID: 3863 RVA: 0x00066A65 File Offset: 0x00064E65
		public static Material TRI_DEPTH_CHECKERED_COLOR
		{
			get
			{
				if (GLUtility._TRI_DEPTH_CHECKERED_COLOR == null)
				{
					GLUtility._TRI_DEPTH_CHECKERED_COLOR = new Material(Shader.Find("GL/TriDepthCheckeredColor"));
				}
				return GLUtility._TRI_DEPTH_CHECKERED_COLOR;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000F18 RID: 3864 RVA: 0x00066A90 File Offset: 0x00064E90
		public static Material TRI_CHECKERED_DEPTH_CUTOFF_COLOR
		{
			get
			{
				if (GLUtility._TRI_CHECKERED_DEPTH_CUTOFF_COLOR == null)
				{
					GLUtility._TRI_CHECKERED_DEPTH_CUTOFF_COLOR = new Material(Shader.Find("GL/TriCheckeredDepthCutoffColor"));
				}
				return GLUtility._TRI_CHECKERED_DEPTH_CUTOFF_COLOR;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000F19 RID: 3865 RVA: 0x00066ABB File Offset: 0x00064EBB
		public static Material TRI_DEPTH_CUTOFF_COLOR
		{
			get
			{
				if (GLUtility._TRI_DEPTH_CUTOFF_COLOR == null)
				{
					GLUtility._TRI_DEPTH_CUTOFF_COLOR = new Material(Shader.Find("GL/TriDepthCutoffColor"));
				}
				return GLUtility._TRI_DEPTH_CUTOFF_COLOR;
			}
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x00066AE8 File Offset: 0x00064EE8
		public static void volumeHelper(bool isSelected, VolumeVisibilityGroup group)
		{
			if (group.isSurfaceVisible)
			{
				Shader.EnableKeyword("GL_SHADED");
				switch (group.surfaceDepth)
				{
				case EGLVisibilityDepthMode.OVERLAP:
					GLUtility.TRI_FLAT_COLOR.SetPass(0);
					break;
				case EGLVisibilityDepthMode.CHECKER:
					GLUtility.TRI_DEPTH_CHECKERED_COLOR.SetPass(0);
					break;
				case EGLVisibilityDepthMode.CUTOFF:
					GLUtility.TRI_DEPTH_CUTOFF_COLOR.SetPass(0);
					break;
				case EGLVisibilityDepthMode.CHECKER_CUTOFF:
					GLUtility.TRI_CHECKERED_DEPTH_CUTOFF_COLOR.SetPass(0);
					break;
				}
				GL.Begin(4);
				Color c;
				if (isSelected)
				{
					c = Color.yellow;
					c.a = group.surfaceColor.a;
				}
				else
				{
					c = group.surfaceColor;
				}
				GL.Color(c);
				GLUtility.boxSolid(Vector3.zero, Vector3.one);
				GL.End();
				Shader.DisableKeyword("GL_SHADED");
			}
			if (group.isWireframeVisible)
			{
				switch (group.wireframeDepth)
				{
				case EGLVisibilityDepthMode.OVERLAP:
					GLUtility.LINE_FLAT_COLOR.SetPass(0);
					break;
				case EGLVisibilityDepthMode.CHECKER:
					GLUtility.LINE_DEPTH_CHECKERED_COLOR.SetPass(0);
					break;
				case EGLVisibilityDepthMode.CUTOFF:
					GLUtility.LINE_DEPTH_CUTOFF_COLOR.SetPass(0);
					break;
				case EGLVisibilityDepthMode.CHECKER_CUTOFF:
					GLUtility.LINE_CHECKERED_DEPTH_CUTOFF_COLOR.SetPass(0);
					break;
				}
				GL.Begin(1);
				Color c2;
				if (isSelected)
				{
					c2 = Color.yellow;
					c2.a = group.wireframeColor.a;
				}
				else
				{
					c2 = group.wireframeColor;
				}
				GL.Color(c2);
				GLUtility.boxWireframe(Vector3.zero, Vector3.one);
				GL.End();
			}
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x00066C80 File Offset: 0x00065080
		public static Vector3 getDirectionFromViewToArrow(Vector3 viewPosition, Vector3 arrowPosition, Vector3 arrowDirection)
		{
			Vector3 b = Vector3.Project(arrowPosition - viewPosition, arrowDirection);
			return (arrowPosition + b - viewPosition).normalized;
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x00066CB4 File Offset: 0x000650B4
		public static void arrow(Vector3 normal, Vector3 view)
		{
			GLUtility.line(Vector3.zero, normal);
			Vector3 a = normal * 0.75f;
			Vector3 b = Vector3.Cross(view, normal) * 0.1f;
			GLUtility.line(normal, a - b);
			GLUtility.line(normal, a + b);
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x00066D04 File Offset: 0x00065104
		public static void line(Vector3 begin, Vector3 end)
		{
			GL.Vertex(GLUtility.matrix.MultiplyPoint3x4(begin));
			GL.Vertex(GLUtility.matrix.MultiplyPoint3x4(end));
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x00066D28 File Offset: 0x00065128
		public static void boxWireframe(Vector3 center, Vector3 size)
		{
			Vector3 vector = size / 2f;
			GLUtility.line(center + new Vector3(-vector.x, -vector.y, -vector.z), center + new Vector3(vector.x, -vector.y, -vector.z));
			GLUtility.line(center + new Vector3(-vector.x, -vector.y, -vector.z), center + new Vector3(-vector.x, -vector.y, vector.z));
			GLUtility.line(center + new Vector3(-vector.x, -vector.y, vector.z), center + new Vector3(vector.x, -vector.y, vector.z));
			GLUtility.line(center + new Vector3(vector.x, -vector.y, -vector.z), center + new Vector3(vector.x, -vector.y, vector.z));
			GLUtility.line(center + new Vector3(-vector.x, -vector.y, -vector.z), center + new Vector3(-vector.x, vector.y, -vector.z));
			GLUtility.line(center + new Vector3(vector.x, -vector.y, -vector.z), center + new Vector3(vector.x, vector.y, -vector.z));
			GLUtility.line(center + new Vector3(-vector.x, -vector.y, vector.z), center + new Vector3(-vector.x, vector.y, vector.z));
			GLUtility.line(center + new Vector3(vector.x, -vector.y, vector.z), center + new Vector3(vector.x, vector.y, vector.z));
			GLUtility.line(center + new Vector3(-vector.x, vector.y, -vector.z), center + new Vector3(vector.x, vector.y, -vector.z));
			GLUtility.line(center + new Vector3(-vector.x, vector.y, -vector.z), center + new Vector3(-vector.x, vector.y, vector.z));
			GLUtility.line(center + new Vector3(-vector.x, vector.y, vector.z), center + new Vector3(vector.x, vector.y, vector.z));
			GLUtility.line(center + new Vector3(vector.x, vector.y, -vector.z), center + new Vector3(vector.x, vector.y, vector.z));
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x000670A4 File Offset: 0x000654A4
		public static void boxSolid(Vector3 center, Vector3 size)
		{
			Vector3 vector = size / 2f;
			GL.Vertex(GLUtility.matrix.MultiplyPoint3x4(center + new Vector3(-vector.x, -vector.y, vector.z)));
			GL.Vertex(GLUtility.matrix.MultiplyPoint3x4(center + new Vector3(-vector.x, vector.y, -vector.z)));
			GL.Vertex(GLUtility.matrix.MultiplyPoint3x4(center + new Vector3(-vector.x, -vector.y, -vector.z)));
			GL.Vertex(GLUtility.matrix.MultiplyPoint3x4(center + new Vector3(-vector.x, vector.y, -vector.z)));
			GL.Vertex(GLUtility.matrix.MultiplyPoint3x4(center + new Vector3(-vector.x, -vector.y, vector.z)));
			GL.Vertex(GLUtility.matrix.MultiplyPoint3x4(center + new Vector3(-vector.x, vector.y, vector.z)));
			GL.Vertex(GLUtility.matrix.MultiplyPoint3x4(center + new Vector3(vector.x, -vector.y, -vector.z)));
			GL.Vertex(GLUtility.matrix.MultiplyPoint3x4(center + new Vector3(vector.x, vector.y, -vector.z)));
			GL.Vertex(GLUtility.matrix.MultiplyPoint3x4(center + new Vector3(vector.x, -vector.y, vector.z)));
			GL.Vertex(GLUtility.matrix.MultiplyPoint3x4(center + new Vector3(vector.x, vector.y, vector.z)));
			GL.Vertex(GLUtility.matrix.MultiplyPoint3x4(center + new Vector3(vector.x, -vector.y, vector.z)));
			GL.Vertex(GLUtility.matrix.MultiplyPoint3x4(center + new Vector3(vector.x, vector.y, -vector.z)));
			GL.Vertex(GLUtility.matrix.MultiplyPoint3x4(center + new Vector3(-vector.x, -vector.y, -vector.z)));
			GL.Vertex(GLUtility.matrix.MultiplyPoint3x4(center + new Vector3(vector.x, -vector.y, -vector.z)));
			GL.Vertex(GLUtility.matrix.MultiplyPoint3x4(center + new Vector3(-vector.x, -vector.y, vector.z)));
			GL.Vertex(GLUtility.matrix.MultiplyPoint3x4(center + new Vector3(vector.x, -vector.y, vector.z)));
			GL.Vertex(GLUtility.matrix.MultiplyPoint3x4(center + new Vector3(-vector.x, -vector.y, vector.z)));
			GL.Vertex(GLUtility.matrix.MultiplyPoint3x4(center + new Vector3(vector.x, -vector.y, -vector.z)));
			GL.Vertex(GLUtility.matrix.MultiplyPoint3x4(center + new Vector3(-vector.x, vector.y, vector.z)));
			GL.Vertex(GLUtility.matrix.MultiplyPoint3x4(center + new Vector3(vector.x, vector.y, -vector.z)));
			GL.Vertex(GLUtility.matrix.MultiplyPoint3x4(center + new Vector3(-vector.x, vector.y, -vector.z)));
			GL.Vertex(GLUtility.matrix.MultiplyPoint3x4(center + new Vector3(vector.x, vector.y, -vector.z)));
			GL.Vertex(GLUtility.matrix.MultiplyPoint3x4(center + new Vector3(-vector.x, vector.y, vector.z)));
			GL.Vertex(GLUtility.matrix.MultiplyPoint3x4(center + new Vector3(vector.x, vector.y, vector.z)));
			GL.Vertex(GLUtility.matrix.MultiplyPoint3x4(center + new Vector3(-vector.x, -vector.y, -vector.z)));
			GL.Vertex(GLUtility.matrix.MultiplyPoint3x4(center + new Vector3(-vector.x, vector.y, -vector.z)));
			GL.Vertex(GLUtility.matrix.MultiplyPoint3x4(center + new Vector3(vector.x, -vector.y, -vector.z)));
			GL.Vertex(GLUtility.matrix.MultiplyPoint3x4(center + new Vector3(vector.x, vector.y, -vector.z)));
			GL.Vertex(GLUtility.matrix.MultiplyPoint3x4(center + new Vector3(vector.x, -vector.y, -vector.z)));
			GL.Vertex(GLUtility.matrix.MultiplyPoint3x4(center + new Vector3(-vector.x, vector.y, -vector.z)));
			GL.Vertex(GLUtility.matrix.MultiplyPoint3x4(center + new Vector3(vector.x, -vector.y, vector.z)));
			GL.Vertex(GLUtility.matrix.MultiplyPoint3x4(center + new Vector3(-vector.x, vector.y, vector.z)));
			GL.Vertex(GLUtility.matrix.MultiplyPoint3x4(center + new Vector3(-vector.x, -vector.y, vector.z)));
			GL.Vertex(GLUtility.matrix.MultiplyPoint3x4(center + new Vector3(-vector.x, vector.y, vector.z)));
			GL.Vertex(GLUtility.matrix.MultiplyPoint3x4(center + new Vector3(vector.x, -vector.y, vector.z)));
			GL.Vertex(GLUtility.matrix.MultiplyPoint3x4(center + new Vector3(vector.x, vector.y, vector.z)));
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x00067790 File Offset: 0x00065B90
		public static void circle(Vector3 center, float radius, Vector3 horizontalAxis, Vector3 verticalAxis, float steps = 0f)
		{
			float num = 6.28318548f;
			float num2 = 0f;
			if (steps == 0f)
			{
				steps = Mathf.Max(4f * radius, 8f);
			}
			float num3 = num / steps;
			Vector3 v = GLUtility.matrix.MultiplyPoint3x4(center + horizontalAxis * radius);
			while (num2 < num)
			{
				num2 += num3;
				float f = Mathf.Min(num2, num);
				float d = Mathf.Cos(f) * radius;
				float d2 = Mathf.Sin(f) * radius;
				Vector3 vector = GLUtility.matrix.MultiplyPoint3x4(center + horizontalAxis * d + verticalAxis * d2);
				GL.Vertex(v);
				GL.Vertex(vector);
				v = vector;
			}
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x0006784C File Offset: 0x00065C4C
		public static void circle(Vector3 center, float radius, Vector3 horizontalAxis, Vector3 verticalAxis, GLCircleOffsetHandler handleGLCircleOffset)
		{
			if (handleGLCircleOffset == null)
			{
				return;
			}
			float num = 6.28318548f;
			float num2 = 0f;
			float num3 = num / Mathf.Max(4f * radius, 8f);
			Vector3 v = GLUtility.matrix.MultiplyPoint3x4(center + horizontalAxis * radius);
			handleGLCircleOffset(ref v);
			while (num2 < num)
			{
				num2 += num3;
				float f = Mathf.Min(num2, num);
				float d = Mathf.Cos(f) * radius;
				float d2 = Mathf.Sin(f) * radius;
				Vector3 vector = GLUtility.matrix.MultiplyPoint3x4(center + horizontalAxis * d + verticalAxis * d2);
				handleGLCircleOffset(ref vector);
				GL.Vertex(v);
				GL.Vertex(vector);
				v = vector;
			}
		}

		// Token: 0x04000969 RID: 2409
		protected static Material _LINE_FLAT_COLOR;

		// Token: 0x0400096A RID: 2410
		protected static Material _LINE_CHECKERED_COLOR;

		// Token: 0x0400096B RID: 2411
		protected static Material _LINE_DEPTH_CHECKERED_COLOR;

		// Token: 0x0400096C RID: 2412
		protected static Material _LINE_CHECKERED_DEPTH_CUTOFF_COLOR;

		// Token: 0x0400096D RID: 2413
		protected static Material _LINE_DEPTH_CUTOFF_COLOR;

		// Token: 0x0400096E RID: 2414
		protected static Material _TRI_FLAT_COLOR;

		// Token: 0x0400096F RID: 2415
		protected static Material _TRI_CHECKERED_COLOR;

		// Token: 0x04000970 RID: 2416
		protected static Material _TRI_DEPTH_CHECKERED_COLOR;

		// Token: 0x04000971 RID: 2417
		protected static Material _TRI_CHECKERED_DEPTH_CUTOFF_COLOR;

		// Token: 0x04000972 RID: 2418
		protected static Material _TRI_DEPTH_CUTOFF_COLOR;

		// Token: 0x04000973 RID: 2419
		public static Matrix4x4 matrix;
	}
}
